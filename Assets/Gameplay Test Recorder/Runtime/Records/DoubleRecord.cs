using System;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct DoubleRecord : IRecord
    {
        public double value;

        public DoubleRecord(double value)
        {
            this.value = value;
        }

        public object Get => value;
        public Type RecordedType => typeof(double);

        public object Clone()
        {
            return new DoubleRecord { value = value };
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