using System;
using System.Collections.Generic;
using TwoGuyGames.GTR.Core;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.SystemRandomAdapter
{
    /// <summary>
    /// Reweaving for Legacy Unity Input System.
    /// Harmony: https://harmony.pardeike.net/articles/intro.html
    /// CIL instructions: https://en.wikipedia.org/wiki/List_of_CIL_instructions
    /// </summary>
    internal class TypePatcher : PatcherBase
    {
        private List<TypeReweaveInfo> typesToReweave;

        public TypePatcher() : base(TypeInfo.Key)
        {
        }

        protected override RecordedSystems SupportedSolution => TypeInfo.RecordedSystems;

        public override void OnBeginRecording()
        {
            // Mark usage.
            ValueRecorder.Store(true, TypeInfo.Key);
        }

        public override void Patch(IInputPatchSettings_RO settings)
        {
            Assert.IsNotNull(settings);
            List<Type> types = new List<Type>();
            types.Add(TypeInfo.Type);
            NonStaticTypeTranspiler.typeToReplace = types;
            AddTranspilerToTypes(settings, NonStaticTypeTranspiler.transpiler);
        }
    }
}