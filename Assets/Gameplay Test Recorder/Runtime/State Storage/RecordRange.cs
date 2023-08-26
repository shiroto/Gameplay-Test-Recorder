using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class RecordRange : IObjectState, IRecord
    {
        [SerializeField]
        private string id;

        [SerializeReference]
        private IValueSpace record;

        public RecordRange(string id, IValueSpace record)
        {
            Assert.IsNotNull(id);
            Assert.IsNotNull(record);
            this.record = record;
            this.id = id;
        }

        public object Get => record.Get;
        public string Id => id;
        public IRecord Record => record;
        public Type RecordedType => typeof(IValueSpace);

        public object Clone()
        {
            return new RecordState(Id, (IRecord)record.Clone());
        }

        public bool Equals(IRecord other)
        {
            return other != null
                && other is RecordRange rs
                && Id.Equals(rs.Id)
                && record.Equals(rs.record);
        }

        public bool StateMatches(IObjectState other)
        {
            return other != null && Id.Equals(other.Id) && record.Contains(other.Record);
        }

        public override string ToString()
        {
            return id;
        }
    }
}