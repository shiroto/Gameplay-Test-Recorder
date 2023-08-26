using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.TimeRecorder
{
#if UNITY_EDITOR

    [UnityEditor.InitializeOnLoad]
#endif
    public static class TimeRecorderStartup
    {
        private static bool isInitialized;

        static TimeRecorderStartup()
        {
            Init();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                RecorderManager.AddRecorder(new TimeRecorder());
            }
        }
    }
}