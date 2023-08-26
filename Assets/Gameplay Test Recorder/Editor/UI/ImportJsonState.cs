using System.IO;
using TwoGuyGames.GTR.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoGuyGames.GTR.Editor
{
    internal class ImportJsonState : IRecordingWindowState
    {
        private string file;

        public void OnEnter(RecordingWindowContext context)
        {
        }

        public void OnExit(RecordingWindowContext context)
        {
        }

        public void Update(RecordingWindowContext context)
        {
        }

        public void UpdateGUI(RecordingWindowContext context)
        {
            if (GUILayout.Button("Select File"))
            {
                file = EditorUtility.OpenFilePanel("Select Replay", "", "json");
            }
            file = EditorGUILayout.TextField("Path:", file);
            if (GUILayout.Button("Import"))
            {
                string name = Path.GetFileName(file);
                RecordedTestAsset recordingAsset = TestFolderUtil_Editor.CreateRecordedTestAsset(name);
                string text = File.ReadAllText(file);
                recordingAsset.recording = JsonUtility.FromJson<Recording>(text);
                string path = SceneUtility.GetScenePathByBuildIndex(0);
                SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
                AssetDatabase.TryGetGUIDAndLocalFileIdentifier(sceneAsset, out string guid, out long localId);
                recordingAsset.recording.config.SceneGUID = guid;
                EditorUtility.SetDirty(recordingAsset);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            if (GUILayout.Button("Back"))
            {
                context.State = new DefaultState();
            }
        }
    }
}