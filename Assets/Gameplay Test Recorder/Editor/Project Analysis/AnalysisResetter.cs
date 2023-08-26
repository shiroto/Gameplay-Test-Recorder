using UnityEditor;
using TwoGuyGames.GTR.Core;

namespace TwoGuyGames.GTR.Editor
{
    /// <summary>
    /// When defines were changed, reset the settings so we have to re-analyze the project.
    /// </summary>
    [InitializeOnLoad]
    public static class AnalysisResetter
    {
        static AnalysisResetter()
        {
            InputRecordingGlobalSettingsAsset.OnUsedInputChanged += OnInputChanged;
        }

        private static void OnInputChanged()
        {
            InputRecordingGlobalSettingsAsset settings = GtrAssetsUtility.GetGlobalSettingsAsset();
            if (settings != null)
            {
                settings.Clear();
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                UnityUtility.ForceRecompile();
            }
        }
    }
}