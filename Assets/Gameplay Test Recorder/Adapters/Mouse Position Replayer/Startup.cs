using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.MousePositionReplayer
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
                TypeRecordSettings.AddTypeToRecord(typeof(UnityEngine.Input), RecordedSystems.UNITY_INPUT_MANAGER);
                ReplayerManager.AddReplayer(new MouseReplayer());
            }
        }
    }
}