using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class Recording : IRecordingRO, ISerializationCallbackReceiver
    {
        public static readonly Vector2Int BATCHMODE_RESOLUTION = new Vector2Int(600, 480);
        public RecordingConfig config;

        [HideInInspector]
        [SerializeReference]
        public IValueSpaceCollection endState;

        public int frameCount;
        public string id;
        public string[] recordKeys;
        private RecordQueue[] records;

        [HideInInspector]
        [SerializeField]
        private byte[] serializedRecords;

        public Recording()
        {
            config = new RecordingConfig();
            recordKeys = new string[0];
            records = new RecordQueue[0];
        }

        public Recording(string id) : this()
        {
            this.id = id;
        }

        public IRecordConfigRO Config => config;

        public string GUID => id;

        public bool IsEmpty => recordKeys.Length == 0;

        public RecordQueue[] Records
        {
            get
            {
                if (records == null && serializedRecords != null)
                {
                    OnAfterDeserialize();
                }
                return records;
            }
            set
            {
                records = value;
            }
        }

        public void DeleteRecord()
        {
            serializedRecords = null;
            recordKeys = new string[0];
            records = new RecordQueue[0];
        }

        public IReadOnlyList<string> GetRecordKeys()
        {
            return recordKeys;
        }

        public bool IsValid()
        {
            return config != null;
        }

        public void OnAfterDeserialize()
        {
            if (serializedRecords != null)
            {
                string[] recs = SerializationHelper.Unzip(serializedRecords).Split('\n');
                List<RecordQueue> list = new List<RecordQueue>(recs.Length);
                foreach (string r in recs)
                {
                    if (!string.IsNullOrEmpty(r))
                    {
                        list.Add(JsonUtility.FromJson<RecordQueue>(r));
                    }
                }
                records = list.ToArray();
            }
        }

        public void OnBeforeSerialize()
        {
            if (records != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (RecordQueue q in records)
                {
                    sb.AppendLine(JsonUtility.ToJson(q));
                }
                serializedRecords = SerializationHelper.Zip(sb.ToString());
            }
        }
    }
}