using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// Since some types are not serializable, this class records the method calls and tries to record return values.
    /// </summary>
    internal static class MockFieldLdfldTranspiler
    {
        /// <summary>
        /// <summary>
        /// The whole idea here is to create a switch statement, which changes the behaviour
        /// of the input processing class depending on <see cref="RecordingController.ActiveMode"/>.
        ///
        /// When set to <see cref="RecordingController.Mode.NONE"/>, the original method is executed.
        /// Example: var x = Input.mousePosition;
        /// When set to <see cref="RecordingController.Mode.RECORDING"/>, the original code is executed and its return value will be stored.
        /// Example: var x = ValueRecorder.Store(METHOD_KEY, Input.mousePosition);
        /// When set to <see cref="RecordingController.Mode.Replaying"/>, the value will be taken from the recording.
        /// Example: var x= ValueRecorder.NextInput<Vector3>(METHOD_KEY);
        ///
        /// Methods that are called to get input can have an arbitrary amount of parameters, which can be put on the stack at any point in time.
        /// For that reason, the switch envelopes only the method call, not its parameters. This way, all parameters are on the stack when the switch is entered.
        /// For the default case, the original method is called is usual. Recording also uses the original method, which consumes the parameters on the stack.
        /// Replaying pops all parameters from the stack before NextInput is called and consumes them that way.
        /// </summary>
        public static LinkedListNode<CodeInstruction> InsertSwitch(InsertCallData data, Type mockedType)
        {
            Label recordLabel = data.ILGenerator.DefineLabel();
            Label replayLabel = data.ILGenerator.DefineLabel();
            Label defaultLabel = data.ILGenerator.DefineLabel();
            Label switchEndLabel = data.ILGenerator.DefineLabel();

            LinkedListNode<CodeInstruction> defaultBreak = CreateDefaultCase(data, defaultLabel, switchEndLabel);
            LinkedListNode<CodeInstruction> recordBreak = CreateRecordCase(data, recordLabel, switchEndLabel, defaultBreak, data.MethodKey, mockedType);
            LinkedListNode<CodeInstruction> replayBreak = CreateReplayCase(data, data.MethodKey, replayLabel, switchEndLabel, recordBreak, mockedType);

            // End of cases. Add the break label to the next instruction after the switch.
            LinkedListNode<CodeInstruction> lastNode = replayBreak;
            LinkedListNode<CodeInstruction> nodeAfterSwitch = lastNode.Next;
            nodeAfterSwitch.Value.labels.Add(switchEndLabel);

            CreateValueChecks(data, recordLabel, replayLabel, defaultLabel);

            return lastNode;
        }

        /// Insert a goto after the specified node and return the new node.
        /// </summary>
        private static LinkedListNode<CodeInstruction> AddGotoLabel(LinkedList<CodeInstruction> methodInstructions, LinkedListNode<CodeInstruction> addAfterNode, Label gotoLabel)
        {
            CodeInstruction @break = new CodeInstruction(OpCodes.Br_S)
            {
                operand = gotoLabel
            };
            return methodInstructions.AddAfter(addAfterNode, @break);
        }

        private static LinkedListNode<CodeInstruction> CreateDefaultCase(InsertCallData data, Label defaultLabel, Label switchEndLabel)
        {
            data.InputCall.Value.labels.Add(defaultLabel); // Add the default-case label to the original call.
            LinkedListNode<CodeInstruction> defaultBreak = AddGotoLabel(data.Instructions, data.InputCall, switchEndLabel);
            return defaultBreak;
        }

        private static LinkedListNode<CodeInstruction> CreateRecordCase(InsertCallData data, Label recordLabel, Label switchEndLabel, LinkedListNode<CodeInstruction> defaultBreak, string key, Type mockedType)
        {
            CodeInstruction inputCallClone = (CodeInstruction)data.InputCall.Value.Clone(); // copy original call
            inputCallClone.labels.Add(recordLabel);
            LinkedListNode<CodeInstruction> inputCallCloneNode = data.Instructions.AddAfter(defaultBreak, inputCallClone);
            // add key argument for record
            LinkedListNode<CodeInstruction> recordKeyNode = data.Instructions.AddAfter(inputCallCloneNode, new CodeInstruction(OpCodes.Ldstr, key));
            // add record call
            CodeInstruction recordInstruction =
                CodeInstruction.Call("TwoGuyGames.GTR.Core.ValueRecorder:Store",
                                    generics: new[] { mockedType });
            LinkedListNode<CodeInstruction> recordCall = data.Instructions.AddAfter(recordKeyNode, recordInstruction); // Add the recording call. This will put the value back on the stack via return.
            LinkedListNode<CodeInstruction> recordBreak = AddGotoLabel(data.Instructions, recordCall, switchEndLabel);

            // debug logging
            //CodeInstruction stringOnStackInstruction = new CodeInstruction(OpCodes.Ldstr, "record");
            //CodeInstruction debugInstruction = CodeInstruction.Call("UnityEngine.Debug:Log", new[] { typeof(object) });
            //LinkedListNode<CodeInstruction> onStackNode = data.Instructions.AddAfter(recordCall, stringOnStackInstruction);
            //data.Instructions.AddAfter(onStackNode, debugInstruction);

            return recordBreak;
        }

        private static LinkedListNode<CodeInstruction> CreateReplayCase(InsertCallData data, string methodKey, Label replayLabel, Label switchEndLabel, LinkedListNode<CodeInstruction> recordBreak, Type mockedType)
        {
            CodeInstruction popThis = new CodeInstruction(OpCodes.Pop);
            LinkedListNode<CodeInstruction> popNode = data.Instructions.AddAfter(recordBreak, popThis);
            LinkedListNode<CodeInstruction> replayKeyNode = data.Instructions.AddAfter(popNode, new CodeInstruction(OpCodes.Ldstr, methodKey)); // push key argument onto stack
            CodeInstruction replayInstruction = CodeInstruction.Call("TwoGuyGames.GTR.Core.ValueRecorder:NextInput", generics: new[] { mockedType });
            LinkedListNode<CodeInstruction> replayCall = data.Instructions.AddAfter(replayKeyNode, replayInstruction);
            LinkedListNode<CodeInstruction> replayBreak = AddGotoLabel(data.Instructions, replayCall, switchEndLabel);
            recordBreak.Next.Value.labels.Add(replayLabel);

            // debug logging
            //CodeInstruction stringOnStackInstruction = new CodeInstruction(OpCodes.Ldstr, "replay");
            //CodeInstruction debugInstruction = CodeInstruction.Call("UnityEngine.Debug:Log", new[] { typeof(object) });
            //LinkedListNode<CodeInstruction> onStackNode = data.Instructions.AddAfter(replayCall, stringOnStackInstruction);
            //data.Instructions.AddAfter(onStackNode, debugInstruction);

            return replayBreak;
        }

        private static void CreateValueChecks(InsertCallData data, Label recordLabel, Label replayLabel, Label defaultLabel)
        {
            // check if recording
            CodeInstruction getControllerValue = CodeInstruction.Call("TwoGuyGames.GTR.Core.RecordingController:get_ActiveMode");
            LinkedListNode<CodeInstruction> getControllerValueNode = data.Instructions.AddBefore(data.InputCall, getControllerValue);
            CodeInstruction ldcRecording = new CodeInstruction(OpCodes.Ldc_I4_1); // compare with 1
            LinkedListNode<CodeInstruction> ldcRecordingNode = data.Instructions.AddAfter(getControllerValueNode, ldcRecording);
            CodeInstruction compareRecording = new CodeInstruction(OpCodes.Beq_S)
            {
                operand = recordLabel // goto record if true
            }; // do comparison
            LinkedListNode<CodeInstruction> compareRecordingNode = data.Instructions.AddAfter(ldcRecordingNode, compareRecording);

            // check if replaying
            CodeInstruction getControllerValue2 = CodeInstruction.Call("TwoGuyGames.GTR.Core.RecordingController:get_ActiveMode");
            LinkedListNode<CodeInstruction> getControllerValue2Node = data.Instructions.AddAfter(compareRecordingNode, getControllerValue2);
            CodeInstruction ldcReplaying = new CodeInstruction(OpCodes.Ldc_I4_2); // compare with 2
            LinkedListNode<CodeInstruction> ldcReplayingNode = data.Instructions.AddAfter(getControllerValue2Node, ldcReplaying);
            CodeInstruction compareReplaying = new CodeInstruction(OpCodes.Beq_S)
            {
                operand = replayLabel // goto replay if true
            }; // do comparison
            LinkedListNode<CodeInstruction> compareReplayingNode = data.Instructions.AddAfter(ldcReplayingNode, compareReplaying);

            // else do default
            CodeInstruction gotoDefault = new CodeInstruction(OpCodes.Br_S)
            {
                operand = defaultLabel // goto default
            };
            data.Instructions.AddAfter(compareReplayingNode, gotoDefault);
        }
    }
}