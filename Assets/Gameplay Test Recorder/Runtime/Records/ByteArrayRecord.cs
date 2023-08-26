using System;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct ByteArrayRecord : IRecord
    {
        public byte[] value;

        public ByteArrayRecord(byte[] value)
        {
            this.value = (byte[])value.Clone();
        }

        public object Get => value;
        public Type RecordedType => typeof(byte[]);

        public object Clone()
        {
            return new ByteArrayRecord { value = value };
        }

        public bool Equals(IRecord other)
        {
            return other != null && ListUtil.ContentEquals(value, other.Get as byte[]);
        }

        public override string ToString()
        {
            return $"{GetType().Name}={value}";
        }
    }
}