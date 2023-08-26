using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct Vector3Record : IRecord
    {
        public Vector3 value;

        public Vector3Record(Vector3 value)
        {
            this.value = value;
        }

        public object Get => value;
        public Type RecordedType => typeof(Vector3);

        public object Clone()
        {
            return new Vector3Record { value = value };
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