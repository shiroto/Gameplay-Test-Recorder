using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    internal static class HarmonyExtension
    {
        public static void AddTranspilerToMethods(this Harmony harmony, IEnumerable<MethodInfo> methods, MethodInfo transpiler)
        {
            // This cannot be parallelized, because PatchProcessor internally locks on a static member.
            foreach (MethodInfo method in methods)
            {
                if (!method.ContainsGenericParameters)
                {
                    try
                    {
                        PatchProcessor processor = harmony.CreateProcessor(method);
                        processor.AddTranspiler(transpiler);
                        processor.Patch();
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Error on `{method.DeclaringType}.{method.Name}`");
                        Debug.LogException(ex);
                    }
                }
                else
                {
                    // generics are not supported by harmony
                    Debug.LogError($"Cannot patch generic method `{method.DeclaringType}.{method.Name}`!");
                }
            }
        }
    }
}