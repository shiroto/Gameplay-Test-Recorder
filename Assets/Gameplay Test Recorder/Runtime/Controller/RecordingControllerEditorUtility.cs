using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    internal static class RecordingControllerEditorUtility
    {
        public static void EnterPlayMode()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = true;
#endif
        }

        public static void FocusGameView()
        {
#if UNITY_EDITOR
            if (!Application.isBatchMode)
            {
                UnityEditor.EditorApplication.ExecuteMenuItem("Window/General/Game");
            }
#endif
        }

        public static void LoadScene(Recording recording)
        {
#if UNITY_EDITOR
            Assert.IsFalse(UnityEditor.EditorApplication.isPlaying, "Cannot use LoadScene while playing.");
            RecordingConfig config = recording.config;
            Assert.IsNotNull(config.SceneGUID, "No scene selected!");
            string scenePath = UnityEditor.AssetDatabase.GUIDToAssetPath(config.SceneGUID);
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scenePath, UnityEditor.SceneManagement.OpenSceneMode.Single);
#endif
        }
    }
}