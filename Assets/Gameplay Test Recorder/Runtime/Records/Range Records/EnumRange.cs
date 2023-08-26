using System;
using System.Collections.Generic;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct EnumRange : IValueSpace<int>
    {
        public string id;
        public List<int> value;

        public EnumRange(string id, int value)
        {
            this.id = id;
            this.value = new List<int>
            {
                (int)value
            };
        }

        public EnumRange(string id, List<int> value)
        {
            this.id = id;
            this.value = value;
        }

        public object Get => value;
        public string Id => id;
        public Type RecordedType => typeof(Enum);

        public object Clone()
        {
            return new EnumRange(id, value);
        }

        public bool Contains(int value)
        {
            return this.value.Contains(value);
        }

        public bool Contains(object value)
        {
            if (value is int i)
            {
                return Contains(i);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(IRecord other)
        {
            return Get.Equals(other.Get);
        }

        public void Extend(int value)
        {
            if (!Contains(value))
            {
                this.value.Add(value);
            }
        }

        public void Extend(object value)
        {
            if (value is int i)
            {
                Extend(i);
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name}={value}";
        }
    }
}