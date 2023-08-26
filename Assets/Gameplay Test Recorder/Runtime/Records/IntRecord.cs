using System;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct IntRecord : IRecord
    {
        public int value;

        public IntRecord(int value)
        {
            this.value = value;
        }

        public IntRecord(Enum value)
        {
            this.value = Convert.ToInt32(value);
        }

        public object Get => value;
        public Type RecordedType => typeof(int);

        public object Clone()
        {
            return new IntRecord { value = value };
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