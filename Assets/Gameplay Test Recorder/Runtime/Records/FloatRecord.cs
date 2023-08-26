using System;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct FloatRecord : IRecord
    {
        public float value;

        public FloatRecord(float value)
        {
            this.value = value;
        }

        public object Get => value;
        public Type RecordedType => typeof(float);

        public object Clone()
        {
            return new FloatRecord { value = value };
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