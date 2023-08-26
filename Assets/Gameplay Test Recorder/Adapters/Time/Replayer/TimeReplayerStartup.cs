using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.TimeReplayer
{
#if UNITY_EDITOR

    [UnityEditor.InitializeOnLoad]
#endif
    public static class TimeReplayerStartup
    {
        private static bool isInitialized;

        static TimeReplayerStartup()
        {
            Init();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                ReplayerManager.AddReplayer(new TimeReplayer());
            }
        }
    }
}