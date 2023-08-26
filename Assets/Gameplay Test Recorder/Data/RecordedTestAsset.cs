using System.Collections.Generic;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    public class RecordedTestAsset : ScriptableObject, ISerializationCallbackReceiver, IRecordingRO
    {
        public float lastResult;
        public Recording recording;

        public IRecordConfigRO Config => recording.config;

        public string GUID => recording.GUID;

        public void DeleteRecords()
        {
            recording.DeleteRecord();
        }

        public IReadOnlyList<string> GetRecordKeys()
        {
            return recording.GetRecordKeys();
        }

        public bool IsValid()
        {
#if UNITY_EDITOR
            return recording != null
                && recording.config != null
                && recording.config.SceneGUID != null;
#else
            return true;
#endif
        }

        public void OnAfterDeserialize()
        {
            InitIfNeeded();
        }

        private void OnEnable()
        {
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out long localId);
            recording.id = guid;
#endif
        }

        public void OnBeforeSerialize()
        {
            InitIfNeeded();
            //PrintNameAndCount();
        }

        public void PrintNameAndCount()
        {
            for (int i = 0; i < recording.recordKeys.Length; i++)
            {
                string name = recording.recordKeys[i];
                string count = recording.Records[i].Count + "";
                Debug.Log($"Name={name}, Count={count}");
            }
        }

        public void Reset()
        {
            lastResult = 0;
            recording = new Recording();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        private void Awake()
        {
            InitIfNeeded();
        }

        private void InitIfNeeded()
        {
            recording = recording ?? new Recording();
        }
    }
}