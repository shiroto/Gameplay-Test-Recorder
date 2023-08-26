using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class RecordQueue : IRecordQueue
    {
        [SerializeReference]
        [HideInInspector] // Generally too big for inspector.
        private List<IRecord> records = new List<IRecord>();

        public int Count => records.Count;
        public bool IsEmpty => records.Count == 0;

        public object Clone()
        {
            return new RecordQueue { records = records.Select(r => (IRecord)r.Clone()).ToList() };
        }

        public IRecord Dequeue()
        {
            IRecord ret = records[0];
            records.RemoveAt(0);
            return ret;
        }

        public void Enqueue(IRecord rec)
        {
            Assert.IsNotNull(rec);
            records.Add(rec);
        }
    }
}