using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoGuyGames.GTR.Core
{
    internal static class AssetPanel
    {
        private static readonly GUIContent assetGuiGui = new GUIContent("Record to", "Asset to record the replay to.");
        private static readonly GUIContent framerateGui = new GUIContent("Framerate", "Framerate of the replay. Pick a framerate that your game can handle at all times, as random stutters can cause the replay to fail.");
        private static readonly GUIContent replaySceneGui = new GUIContent("Scene", "Start scene of the replay.");
        private static readonly GUIContent resolutionGui = new GUIContent("Resolution", "Resolution (of the Game View) of this replay. Editor will be forced into this resolution upon replaying.");

        private static RecordedTestAsset currentRecording;
        private static SceneAsset currentScene;

        public static void DrawAsset(ref RecordedTestAsset recordAsset)
        {
            EditorGUILayout.LabelField("Asset Info");
            try
            {
                recordAsset = (RecordedTestAsset)EditorGUILayout.ObjectField(assetGuiGui, recordAsset, typeof(RecordedTestAsset), false);
                if (recordAsset != currentRecording)
                {
                    try
                    {
                        string scenePath = AssetDatabase.GUIDToAssetPath(recordAsset.Config.SceneGUID);
                        currentScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
                    }
                    catch
                    {
                        // no valid scene
                    }
                    currentRecording = recordAsset;
                }
                DrawConfig(recordAsset);
            }
            catch (Exception e)
            {
                if (ExitGUIUtility.ShouldRethrowException(e))
                {
                    return;
                }
                Debug.LogException(e);
            }
        }

        private static void ApplyCurrentSceneToConfig(RecordingConfig config)
        {
            Scene scene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(sceneAsset, out string guid, out long localId))
            {
                config.SceneGUID = guid;
            }
            else
            {
                Debug.LogError($"Could not find GUID of {scene}");
            }
        }

        private static void DrawConfig(RecordedTestAsset recordAsset)
        {
            if (recordAsset != null)
            {
                RecordingConfig config = recordAsset.recording.config;
                EditorLayoutUtility.DrawInHorizontal(() => DrawScene(config));
                EditorLayoutUtility.DrawInHorizontal(() => DrawFramerate(config));
                EditorLayoutUtility.DrawInHorizontal(() => DrawResolution(config));
                recordAsset.recording.config = config;
            }
        }

        private static void DrawFramerate(RecordingConfig config)
        {
            config.Framerate = Mathf.Clamp(EditorGUILayout.IntSlider(framerateGui, config.Framerate, 1, 120), 1, 120);
        }

        private static void DrawResolution(RecordingConfig config)
        {
            config.Resolution = EditorGUILayout.Vector2IntField(resolutionGui, config.Resolution);
            if (GUILayout.Button("Current"))
            {
                config.Resolution = GetGameViewAspect();
            }
        }

        private static void DrawScene(RecordingConfig config)
        {
            SceneAsset oldScene = currentScene;
            currentScene = EditorGUILayout.ObjectField(replaySceneGui, currentScene, typeof(SceneAsset), false) as SceneAsset;
            if (currentScene != null && currentScene.Equals(oldScene) && AssetDatabase.TryGetGUIDAndLocalFileIdentifier(currentScene, out string guid, out long localId))
            {
                config.SceneGUID = guid;
            }
            if (GUILayout.Button("Current"))
            {
                ApplyCurrentSceneToConfig(config);
            }
        }

        private static Vector2Int GetGameViewAspect()
        {
            EditorWindow gameView = GetMainGameView();
            PropertyInfo prop = gameView.GetType().GetProperty("currentGameViewSize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            object gvsize = prop.GetValue(gameView, new object[0] { });
            Type gvSizeType = gvsize.GetType();

            //int y = (int)gvSizeType.GetProperty("height", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(gvsize, new object[0] { });
            //int x = (int)gvSizeType.GetProperty("width", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(gvsize, new object[0] { });

            int x = Mathf.CeilToInt(gameView.position.width);
            int y = Mathf.CeilToInt(gameView.position.height);

            return new Vector2Int(x, y);
        }

        private static EditorWindow GetMainGameView()
        {
            Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
            Type type = assembly.GetType("UnityEditor.GameView");
            return EditorWindow.GetWindow(type);
        }
    }
}