using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    public static class RecordingStorageHelper
    {
        private const string EXTENSION = ".json";

        static RecordingStorageHelper()

        {
            GtrAssetsUtility.CreateFoldersIfNeeded();
        }

        public static IReadOnlyList<Recording> LoadAllRecordings()
        {
            string[] paths = Directory.GetFiles(Application.dataPath, "*" + EXTENSION, SearchOption.AllDirectories);
            List<Recording> recordings = new List<Recording>(paths.Length);
            foreach (string path in paths)
            {
                string id = Path.GetFileName(path);
                if (LoadRecording(id, out Recording r))
                {
                    recordings.Add(r);
                }
            }
            return recordings;
        }

        public static bool LoadRecording(string id, out Recording recording)
        {
            Assert.IsFalse(string.IsNullOrEmpty(id));
            try
            {
                string json = File.ReadAllText(GetPath(id));
                recording = JsonUtility.FromJson<Recording>(json);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                recording = null;
                return false;
            }
        }

        public static bool LoadRecordingAndAsset(string id, out Recording recording)
        {
            Assert.IsFalse(string.IsNullOrEmpty(id));
            try
            {
                string json = File.ReadAllText(GetPath(id));
                recording = JsonUtility.FromJson<Recording>(json);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                recording = null;
                return false;
            }
        }

        public static bool SaveRecording(Recording recording)
        {
            Assert.IsNotNull(recording);
            try
            {
                string json = JsonUtility.ToJson(recording);
#if !UNITY_EDITOR
                string path = GetPath(Guid.NewGuid() + " JsonReplay");
#else
                string path = GetPath(recording.id);
#endif
                File.WriteAllText(path, json);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return false;
            }
        }

        private static string GetPath(string id)
        {
            return PathUtility.GetPathForReplays() + "/" + id + EXTENSION;
        }
    }
}