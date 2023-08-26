using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using TwoGuyGames.GTR.Core;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Editor
{
    internal static class TypeAnalyzer
    {
        public static RecordedSystems FindInputSolutionsInType(AssemblyDefinition assembly, IReadOnlyCollection<IRecordedType> recordedTypes, Type typeToSearch)
        {
            try
            {
                Assert.IsNotNull(recordedTypes);
                Assert.IsNotNull(typeToSearch);
                RecordedSystems solutions = RecordedSystems.NONE;
                if (assembly != null) // There are some assemblies that seem to fade in and out of existence when some libs are called. So we just skip those.
                {
                    solutions = SearchTypeIfNeeded(assembly, recordedTypes, typeToSearch, solutions);
                }
                else
                {
                    Debug.LogError(typeToSearch);
                }
                return solutions;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Could not search `{typeToSearch}`. Continuing...\n{ex.Message}");
                return RecordedSystems.NONE;
            }
        }

        private static RecordedSystems AddInputIfAvailable(IReadOnlyCollection<IRecordedType> recordedTypes, RecordedSystems solutions, Instruction instruction)
        {
            if (ReflectionHelper.IsMethodCall(instruction) && instruction.Operand is MethodReference method)
            {
                solutions = AddInputIfNotPresent(recordedTypes, solutions, method);
            }
            return solutions;
        }

        private static RecordedSystems AddInputIfNotPresent(IReadOnlyCollection<IRecordedType> recordedTypes, RecordedSystems solutions, MethodReference method)
        {
            IRecordedType recordedType = GetInputType(recordedTypes, method);
            if (recordedType != null && !solutions.HasFlag(recordedType.GetRecordedSystems()))
            {
                solutions |= recordedType.GetRecordedSystems();
            }
            return solutions;
        }

        private static IRecordedType GetInputType(IReadOnlyCollection<IRecordedType> recordedTypes, MethodReference method)
        {
            foreach (IRecordedType recordedType in recordedTypes)
            {
                if (IsInputType(method, recordedType))
                {
                    return recordedType;
                }
            }
            return null;
        }

        private static bool IsAnonymous(Type type)
        {
            return type.AssemblyQualifiedName.Contains("<>");
        }

        private static bool IsInputType(MethodReference method, IRecordedType recordedType)
        {
            return method.DeclaringType.FullName.Equals(recordedType.GetInputTypeReference().FullName);
        }

        private static bool IsNotInputType(IReadOnlyCollection<IRecordedType> recordedTypes, Type typeToSearch)
        {
            return recordedTypes.Exists(type => typeToSearch == type.GetRecordedType());
        }

        private static RecordedSystems SearchMethodForInputType(IReadOnlyCollection<IRecordedType> recordedTypes, RecordedSystems solutions, MethodDefinition m)
        {
            foreach (Instruction instruction in m.Body.Instructions)
            {
                solutions = AddInputIfAvailable(recordedTypes, solutions, instruction);
            }
            return solutions;
        }

        private static RecordedSystems SearchMethods(IReadOnlyCollection<IRecordedType> recordedTypes, TypeDefinition typeDef)
        {
            RecordedSystems solutions = RecordedSystems.NONE;
            foreach (MethodDefinition m in typeDef.Methods)
            {
                if (m.Body != null)
                {
                    solutions = SearchMethodForInputType(recordedTypes, solutions, m);
                }
            }
            return solutions;
        }

        private static RecordedSystems SearchType(AssemblyDefinition assembly, IReadOnlyCollection<IRecordedType> recordedTypes, Type typeToSearch)
        {
            if (AssemblyResolver.TryFindTypeDefinition(assembly, typeToSearch, out TypeDefinition typeDef))
            {
                RecordedSystems solutions = SearchMethods(recordedTypes, typeDef);
                return solutions;
            }
            else
            {
                return RecordedSystems.NONE;
            }
        }

        private static RecordedSystems SearchTypeIfNeeded(AssemblyDefinition assembly, IReadOnlyCollection<IRecordedType> recordedTypes, Type typeToSearch, RecordedSystems solutions)
        {
            if (!IsNotInputType(recordedTypes, typeToSearch) && !IsAnonymous(typeToSearch))
            {
                solutions = SearchType(assembly, recordedTypes, typeToSearch);
            }
            return solutions;
        }
    }
}