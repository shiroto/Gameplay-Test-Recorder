using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.InputSystemRecorder
{
#if UNITY_EDITOR

    [UnityEditor.InitializeOnLoad]
#endif
    public static class InputSystemRecorderStartup
    {
        private static bool isInitialized;

        static InputSystemRecorderStartup()
        {
            Init();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                if (ActiveInputUtility.IsInputSystemEnabled())
                {
                    RecorderManager.AddRecorder(new InputSystemRecorder());
                    RecorderManager.AddRecorder(new MouseRecorder());
                }
            }
        }
    }
}