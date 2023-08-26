using System;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct StringRecord : IRecord
    {
        public string value;

        public StringRecord(string value)
        {
            this.value = value;
        }

        public object Get => value;
        public Type RecordedType => typeof(string);

        public object Clone()
        {
            return new StringRecord { value = value };
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