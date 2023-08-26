using System;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct BoolRecord : IRecord
    {
        public bool value;

        public BoolRecord(bool value)
        {
            this.value = value;
        }

        public object Get => value;

        public Type RecordedType => typeof(bool);

        public object Clone()
        {
            return new BoolRecord { value = value };
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