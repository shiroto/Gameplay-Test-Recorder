using System;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct CharRecord : IRecord
    {
        public char value;

        public CharRecord(char value)
        {
            this.value = value;
        }

        public object Get => value;
        public Type RecordedType => typeof(char);

        public object Clone()
        {
            return new CharRecord { value = value };
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