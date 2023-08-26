using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct QuaternionRecord : IRecord
    {
        public Quaternion value;

        public QuaternionRecord(Quaternion value)
        {
            this.value = value;
        }

        public object Get => value;
        public Type RecordedType => typeof(Quaternion);

        public object Clone()
        {
            return new QuaternionRecord { value = value };
        }

        public bool Equals(IRecord other)
        {
            return other != null && Get.Equals(other.Get);
        }

        public override string ToString()
        {
            return $"{GetType().Name}={value}";
        }
    }
}