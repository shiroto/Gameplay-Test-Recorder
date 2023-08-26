using System.Collections.Generic;
using TwoGuyGames.GTR.Core;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.UnityRandomAdapter
{
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
            StaticTypeTranspiler.staticTypeToReplace = TypeInfo.Type;
            AddTranspilerToTypes(settings, StaticTypeTranspiler.transpiler);
        }
    }
}