using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace TwoGuyGames.GTR.Core
{
    public struct InsertCallData
    {
        public MethodInfo CalledMethod;
        public ILGenerator ILGenerator;
        public LinkedListNode<CodeInstruction> InputCall;
        public LinkedList<CodeInstruction> Instructions;
        public string MethodKey;

        public delegate LinkedListNode<CodeInstruction> InsertCall(InsertCallData data);
    }
}