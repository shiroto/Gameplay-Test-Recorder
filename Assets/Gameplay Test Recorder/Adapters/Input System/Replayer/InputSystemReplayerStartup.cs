using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.InputSystemRecorder
{
    public static class InputSystemReplayerStartup
    {
        private static bool isInitialized;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                if (ActiveInputUtility.IsInputSystemEnabled())
                {
                    ReplayerManager.AddReplayer(new InputSystemReplayer());
                }
            }
        }
    }
}