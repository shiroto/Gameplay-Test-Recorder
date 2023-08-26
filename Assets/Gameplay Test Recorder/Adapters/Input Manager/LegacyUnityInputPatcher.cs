using System.Collections.Generic;
using TwoGuyGames.GTR.Core;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.InputManagerAdapter
{
    /// <summary>
    /// Reweaving for Legacy Unity Input System.
    /// Harmony: https://harmony.pardeike.net/articles/intro.html
    /// CIL instructions: https://en.wikipedia.org/wiki/List_of_CIL_instructions
    /// </summary>
    internal class LegacyUnityInputPatcher : PatcherBase
    {
        public static readonly string KEY = RecordedSystems.UNITY_INPUT_MANAGER.ToString();

        private List<TypeReweaveInfo> typesToReweave;

        public LegacyUnityInputPatcher() : base(KEY)
        {
        }

        protected override RecordedSystems SupportedSolution => RecordedSystems.UNITY_INPUT_MANAGER;

        public override void OnBeginRecording()
        {
            // Mark that legacy input is used.
            ValueRecorder.Store(true, KEY);
        }

        public override void Patch(IInputPatchSettings_RO settings)
        {
            Assert.IsNotNull(settings);
            StaticTypeTranspiler.staticTypeToReplace = typeof(UnityEngine.Input);
            AddTranspilerToTypes(settings, StaticTypeTranspiler.transpiler);
        }
    }
}