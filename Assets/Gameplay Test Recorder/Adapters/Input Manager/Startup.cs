using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.InputManagerAdapter
{
#if UNITY_EDITOR

    [UnityEditor.InitializeOnLoad]
#endif
    public static class Startup
    {
        private static bool isInitialized;

        static Startup()
        {
            Init();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                if (ActiveInputUtility.IsLegacyInputEnabled())
                {
                    TypeRecordSettings.AddTypeToRecord(typeof(UnityEngine.Input), RecordedSystems.UNITY_INPUT_MANAGER);
                    InputPatchFactory.AddInputPatcher(RecordedSystems.UNITY_INPUT_MANAGER, new LegacyUnityInputPatcher());
                    RecorderManager.AddRecorder(new MouseRecorder());
                }
            }
        }
    }
}