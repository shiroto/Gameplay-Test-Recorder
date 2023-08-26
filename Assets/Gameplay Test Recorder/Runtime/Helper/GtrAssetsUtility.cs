using System.IO;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    public static class GtrAssetsUtility
    {
        private static InputRecordingGlobalSettingsAsset globalSettings;

        public static InputRecordingGlobalSettingsAsset GetGlobalSettingsAsset()
        {
            if (globalSettings == null)
            {
                GetGlobalSettingsRuntime();
            }
            return globalSettings;
        }

        private static void GetGlobalSettingsRuntime()
        {
            globalSettings = Resources.Load<InputRecordingGlobalSettingsAsset>(PathUtility.GLOBAL_SETTINGS_NAME);
        }

        public static void CreateFoldersIfNeeded()
        {
            if (!Directory.Exists(PathUtility.GetAbsolutePathForGlobalSettings()))
            {
                Directory.CreateDirectory(PathUtility.GetAbsolutePathForGlobalSettings());
            }
            if (!Directory.Exists(PathUtility.GetPathForReplays()))
            {
                Directory.CreateDirectory(PathUtility.GetPathForReplays());
            }
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}