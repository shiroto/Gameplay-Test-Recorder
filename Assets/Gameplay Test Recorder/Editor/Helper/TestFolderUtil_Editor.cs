using UnityEditor;
using UnityEngine;
using TwoGuyGames.GTR.Core;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Editor
{
    public static class TestFolderUtil_Editor
    {
        public static RecordedTestAsset CreateRecordedTestAsset(string name)
        {
            Assert.IsNotNull(name);
            string location = PathUtility.GetRelativePathForReplays();
            RecordedTestAsset settings = ScriptableObjectUtility.CreateAssetAtPath<RecordedTestAsset>(location, name);
            return settings;
        }

        public static InputRecordingGlobalSettingsAsset CreateGlobalSettingsAsset()
        {
            InputRecordingGlobalSettingsAsset globalSettings = GtrAssetsUtility.GetGlobalSettingsAsset();
            if (globalSettings == null)
            {
                GtrAssetsUtility.CreateFoldersIfNeeded();
                InputRecordingGlobalSettingsAsset[] array = Resources.FindObjectsOfTypeAll<InputRecordingGlobalSettingsAsset>();
                if (array.Length > 0)
                {
                    globalSettings = array[0];
                }
                if (globalSettings == null)
                {
                    globalSettings = CreateGlobalSettings();
                }
            }
            return globalSettings;
        }

        private static InputRecordingGlobalSettingsAsset CreateGlobalSettings()
        {
            InputRecordingGlobalSettingsAsset globalSettings;
            string path = PathUtility.GetRelativePathForGlobalSettings();
            Debug.Log($"Creating Global Recordings Settings at `{path}`");
            globalSettings = ScriptableObjectUtility.CreateAssetAtPath<InputRecordingGlobalSettingsAsset>(path, PathUtility.GetGlobalSettingsNameWithExtension());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return globalSettings;
        }

        private static string GetGlobalSettingsPath()
        {
            return PathUtility.GetAbsolutePathForGlobalSettings() + "/" + PathUtility.GetGlobalSettingsNameWithExtension();
        }
    }
}