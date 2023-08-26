using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct Vector4Record : IRecord
    {
        public Vector4 value;

        public Vector4Record(Vector4 value)
        {
            this.value = value;
        }

        public object Get => value;
        public Type RecordedType => typeof(Vector4);

        public object Clone()
        {
            return new Vector4Record { value = value };
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