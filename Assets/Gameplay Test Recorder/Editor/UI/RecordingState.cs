using System;
using TwoGuyGames.GTR.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoGuyGames.GTR.Editor
{
    [Serializable]
    internal class RecordingState : IRecordingWindowState
    {
        private string createName = "New Recording";
        private RecordedTestAsset recordAsset;

        public void OnEnter(RecordingWindowContext context)
        {
        }

        public void OnExit(RecordingWindowContext context)
        {
            RecordingController.UnsetMode();
        }

        public void Update(RecordingWindowContext context)
        {
        }

        public void UpdateGUI(RecordingWindowContext context)
        {
            EditorGUILayout.BeginVertical();
            EditorLayoutUtility.DrawInHorizontal(CreateNewAsset);
            Utility.HorizontalLine();
            EditorLayoutUtility.DrawInVertical(() => AssetPanel.DrawAsset(ref recordAsset));
            Utility.HorizontalLine();
            DrawRecordingControlsIfNeeded();
            Utility.HorizontalLine();
            DrawExit(context);
            EditorGUILayout.EndVertical();
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

        private static void DrawExit(RecordingWindowContext context)
        {
            if (RecordingController.IsInactive && !EditorApplication.isPlaying && GUILayout.Button("Back"))
            {
                context.State = new DefaultState();
            }
        }

        private void ApplyConfigToAsset()
        {
            EditorUtility.SetDirty(recordAsset);
        }

        private void CreateNewAsset()
        {
            EditorGUILayout.LabelField("Create New Recording");
            createName = EditorGUILayout.TextField(createName);
            if (GUILayout.Button("Create"))
            {
                recordAsset = TestFolderUtil_Editor.CreateRecordedTestAsset(createName);
                ApplyCurrentSceneToConfig(recordAsset.recording.config);
            }
        }

        private void DrawRecordingControls()
        {
            if (!RecordingController.IsRecording)
            {
                DrawStartRecording();
            }
            else
            {
                DrawStopRecording();
            }
        }

        private void DrawRecordingControlsIfNeeded()
        {
            if (recordAsset != null)
            {
                if (recordAsset.IsValid())
                {
                    DrawRecordingControls();
                }
                else
                {
                    EditorGUILayout.HelpBox("Cannot start recording. Scene missing.", MessageType.Error);
                }
            }
        }

        private void DrawStartRecording()
        {
            if (!recordAsset.recording.IsEmpty)
            {
                EditorGUILayout.HelpBox("A replay has already been recorded to this asset. Recording again will overwrite the existing replay!", MessageType.Warning);
            }
            if (!RecordingController.IsPatchApplied)
            {
                EditorGUILayout.HelpBox("The necessary patches to code have not yet been applied. Loading the scene will take longer than usual.", MessageType.Info);
            }
            if (GUILayout.Button("Start Recording"))
            {
                ApplyConfigToAsset();
                RecordingController.SetActiveAsset(recordAsset);
                RecordingController.SetupAndStartRecording(recordAsset.recording);
            }
        }

        private void DrawStopRecording()
        {
            if (GUILayout.Button("Stop Recording"))
            {
                RecordingController.StopRecording();
                EditorApplication.isPlaying = false;
            }
        }
    }
}