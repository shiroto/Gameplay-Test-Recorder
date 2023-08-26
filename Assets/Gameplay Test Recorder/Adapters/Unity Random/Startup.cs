using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.UnityRandomAdapter
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
                TypeRecordSettings.AddTypeToRecord(TypeInfo.Type, TypeInfo.RecordedSystems);
                InputPatchFactory.AddInputPatcher(TypeInfo.RecordedSystems, new TypePatcher());
            }
        }
    }
}