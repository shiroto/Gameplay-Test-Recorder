#if REWIRED
using System;
using System.Collections.Generic;
using TwoGuyGames.GTR.Core;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.RewiredAdapter
{
    /// <summary>
    /// Reweaving for Rewired.
    /// Harmony: https://harmony.pardeike.net/articles/intro.html
    /// CIL instructions: https://en.wikipedia.org/wiki/List_of_CIL_instructions
    /// </summary>
    internal class TypePatcher : PatcherBase
    {
        public TypePatcher() : base(TypeInfo.Key)
        {
        }

        protected override RecordedSystems SupportedSolution => RecordedSystems.REWIRED;

        public override void OnBeginRecording()
        {
            // Mark that rewired is used.
            ValueRecorder.Store(true, SupportedSolution.ToString());
        }

        public override void Patch(IInputPatchSettings_RO settings)
        {
            Assert.IsNotNull(settings);
            List<Type> types = new List<Type>
            {
                TypeInfo.Type,
                TypeInfo.TypeMouse
            };
            NonStaticTypeTranspiler.typeToReplace = types;
            AddTranspilerToTypes(settings, NonStaticTypeTranspiler.transpiler);
        }
    }
}
#endif