using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct Vector2Record : IRecord
    {
        public Vector2 value;

        public Vector2Record(Vector2 value)
        {
            this.value = value;
        }

        public object Get => value;
        public Type RecordedType => typeof(Vector2);

        public object Clone()
        {
            return new Vector2Record { value = value };
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