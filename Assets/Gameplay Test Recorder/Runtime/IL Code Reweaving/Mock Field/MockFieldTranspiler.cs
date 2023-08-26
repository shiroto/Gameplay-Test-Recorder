using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// This can only transpile one class at a time. Same goes for Harmony.
    /// </summary>
    internal static class MockFieldTranspiler
    {
        public static readonly MethodInfo transpiler;
        public static IReadOnlyList<FieldInfo> mockedFields;

        static MockFieldTranspiler()
        {
            transpiler = typeof(MockFieldTranspiler).GetMethod("Transpiler", BindingFlags.Static | BindingFlags.Public);
            Assert.IsNotNull(transpiler);
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator, MethodBase method)
        {
            string methodKey = MethodToKey.GetKey(method);
            return ReplaceCalls(instructions, generator, methodKey);
        }

        private static IEnumerable<CodeInstruction> ReplaceCalls(IEnumerable<CodeInstruction> instructions, ILGenerator generator, string methodKey)
        {
            LinkedList<CodeInstruction> instrList = new LinkedList<CodeInstruction>(instructions);
            //UnityEngine.Debug.Log(methodKey);
            //UnityEngine.Debug.Log(instrList.Log());
            InsertCallData data = new InsertCallData
            {
                Instructions = instrList,
                ILGenerator = generator,
                MethodKey = methodKey
            };
            foreach (FieldInfo mockedField in mockedFields)
            {
                if (RecordFactory.IsSupportedType(mockedField.FieldType))
                {
                    SearchForMockableLdflds(instrList, data, mockedField);
                }
                else
                {
                    SearchForMockableMethodCalls(instrList, data, mockedField);
                }
            }
            //UnityEngine.Debug.Log(instrList.Log());
            return instrList;
        }

        private static void SearchForMockableLdflds(LinkedList<CodeInstruction> instrList, InsertCallData data, FieldInfo mockedField)
        {
            LinkedListNode<CodeInstruction> current = instrList.First;
            while (current != null)
            {
                //UnityEngine.Debug.Log(current.Value + " " + current.Value.operand);
                if (current.Value.opcode == OpCodes.Ldfld && EqualityComparer<FieldInfo>.Default.Equals(mockedField, (FieldInfo)current.Value.operand))
                {
                    data.InputCall = current;
                    current = MockFieldLdfldTranspiler.InsertSwitch(data, mockedField.FieldType);
                }
                current = current.Next;
            }
        }

        private static void SearchForMockableMethodCalls(LinkedList<CodeInstruction> instrList, InsertCallData data, FieldInfo mockedField)
        {
            LinkedListNode<CodeInstruction> current = instrList.First;
            while (current != null)
            {
                //UnityEngine.Debug.Log(current.Value + " " + current.Value.operand);

                if (ReflectionHelper.IsMethodCall(current.Value) && current.Value.operand is MethodInfo method && !method.IsStatic)
                {
                    if (method.DeclaringType == mockedField.FieldType)
                    {
                        data.CalledMethod = method;
                        data.InputCall = current;
                        current = MockFieldMethodsTranspiler.InsertSwitch(data);
                    }
                }
                current = current.Next;
            }
        }
    }
}