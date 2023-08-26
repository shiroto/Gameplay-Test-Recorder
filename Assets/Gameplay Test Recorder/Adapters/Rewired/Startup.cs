#if REWIRED
using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.RewiredAdapter
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
                if (ActiveInputUtility.IsRewiredEnabled())
                {
                    TypeRecordSettings.AddTypeToRecord(TypeInfo.Type, TypeInfo.RecordedSystems);
                    TypeRecordSettings.AddTypeToRecord(TypeInfo.TypeMouse, TypeInfo.RecordedSystems);
                    InputPatchFactory.AddInputPatcher(TypeInfo.RecordedSystems, new TypePatcher());
                    RecorderManager.AddRecorder(new MouseRecorder());
                }
            }
        }
    }
}
#endif