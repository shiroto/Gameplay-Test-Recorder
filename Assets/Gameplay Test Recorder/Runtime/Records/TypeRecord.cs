using System;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct TypeRecord : IRecord
    {
        public SerializableSystemType value;

        public TypeRecord(Type value)
        {
            this.value = new SerializableSystemType(value);
        }

        public object Get => value.SystemType;
        public Type RecordedType => typeof(Type);

        public object Clone()
        {
            return new TypeRecord { value = value };
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