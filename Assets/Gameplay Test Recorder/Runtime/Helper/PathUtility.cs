using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    public static class PathUtility
    {
        public const string GLOBAL_SETTINGS_EXTENSION = ".asset";
        public const string GLOBAL_SETTINGS_NAME = "GTR Global Settings";

        public static string GetGlobalSettingsNameWithExtension()
        {
            return GLOBAL_SETTINGS_NAME + GLOBAL_SETTINGS_EXTENSION;
        }

        public static string GetAbsolutePathForGlobalSettings()
        {
            return Application.dataPath + "/Gameplay Test Recorder/Resources";
        }

        public static string GetRelativePathForGlobalSettings()
        {
            return "Assets/Gameplay Test Recorder/Resources";
        }

        public static string GetRelativePathForReplays()
        {
            return "Assets/Gameplay Test Recorder/Replays";
        }

        public static string GetPathForReplays()
        {
#if !UNITY_EDITOR
            return Application.persistentDataPath + "/Gameplay Test Recorder/Replays";
#else
            return Application.dataPath + "/Gameplay Test Recorder/Replays";
#endif
        }
    }
}