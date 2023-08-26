using HarmonyLib;
using Mono.Cecil.Cil;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    public static class ReflectionHelper
    {
        public const BindingFlags AllDeclared = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        public static MethodInfo[] FindDeclaredMethods(Type type)
        {
            Assert.IsNotNull(type);
            MethodInfo[] methods = type
                .GetMethods(AllDeclared)
                .Where(m => m.HasMethodBody())
                .ToArray();
            return methods;
        }

        public static MethodInfo[] FindDeclaredNonGenericMethods(Type type)
        {
            Assert.IsNotNull(type);
            MethodInfo[] methods = type
                .GetMethods(AllDeclared)
                .Where(m => !m.IsGenericMethod)
                .Where(m => m.HasMethodBody())
                .ToArray();
            return methods;
        }

        public static bool IsMethodCall(CodeInstruction instruction)
        {
            return instruction.opcode.Name == "call" || instruction.opcode.Name == "callvirt";
        }

        public static bool IsMethodCall(Instruction instruction)
        {
            return instruction.OpCode.Name == "call" || instruction.OpCode.Name == "callvirt";
        }

        public static bool IsPushField(CodeInstruction instruction)
        {
            return instruction.opcode.Name == "ldflda" || instruction.opcode.Name == "ldfld";
        }
    }
}