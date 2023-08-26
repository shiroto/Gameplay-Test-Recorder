using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class RecordState : IObjectState, IRecord
    {
        [SerializeField]
        private string id;

        [SerializeReference]
        private IRecord state;

        public RecordState(string id, IRecord record)
        {
            Assert.IsNotNull(id);
            Assert.IsNotNull(record);
            this.state = record;
            this.id = id;
        }

        public object Get => state.Get;
        public string Id => id;
        public IRecord Record => state;
        public Type RecordedType => typeof(IRecord);

        public object Clone()
        {
            return new RecordState(Id, (IRecord)state.Clone());
        }

        public bool Equals(IRecord other)
        {
            return other != null
                && other is RecordState rs
                && Id.Equals(rs.Id)
                && state.Equals(rs.state);
        }

        public bool StateEquals(RecordState other)
        {
            return other != null && Id.Equals(other.Id) && state.Equals(other.state);
        }

        public bool StateMatches(IObjectState other)
        {
            if (other is RecordState fs)
            {
                return StateEquals(fs);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return id;
        }
    }
}