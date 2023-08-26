using Mono.Cecil;
using System;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    internal class RecordedType : IRecordedType
    {
        private TypeReference inputTypeRef;
        private RecordedSystems recordedSystems;
        private Type recordedType;

        private RecordedType()
        {
        }

        public static bool CreateRecordedType(Type recordedType, RecordedSystems recordedSystems, out RecordedType rt)
        {
            Assert.IsNotNull(recordedType);
            rt = new RecordedType
            {
                recordedType = recordedType,
                recordedSystems = recordedSystems
            };
            AssemblyDefinition assemblyDefinition = AssemblyResolver.ReadAssemblyFromType(recordedType);
            if (assemblyDefinition != null &&
                AssemblyResolver.TryFindTypeDefinition(
                assemblyDefinition,
                recordedType,
                out TypeDefinition typeDefinition))
            {
                rt.inputTypeRef = typeDefinition.Resolve();
                return true;
            }
            else
            {
                return false;
            }
        }

        public TypeReference GetInputTypeReference()
        {
            return inputTypeRef;
        }

        public RecordedSystems GetRecordedSystems()
        {
            return recordedSystems;
        }

        public Type GetRecordedType()
        {
            return recordedType;
        }
    }
}