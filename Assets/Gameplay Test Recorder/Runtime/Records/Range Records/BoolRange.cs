using System;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct BoolRange : IValueSpace<bool>
    {
        public string id;
        public Range value;

        public BoolRange(string id, Range value)
        {
            this.id = id;
            this.value = value;
        }

        public BoolRange(string id, bool value) : this(id, BoolToRange(value))
        {
        }

        public enum Range : byte
        {
            FALSE, TRUE, BOTH
        }

        public object Get => value;
        public string Id => id;
        public Type RecordedType => typeof(bool);

        public static BoolRange.Range BoolToRange(bool value)
        {
            if (value)
            {
                return Range.TRUE;
            }
            else
            {
                return Range.FALSE;
            }
        }

        public object Clone()
        {
            return new BoolRange(id, value);
        }

        public bool Contains(bool value)
        {
            switch (this.value)
            {
                case Range.BOTH:
                    return true;

                case Range.TRUE:
                    return value;

                case Range.FALSE:
                    return !value;

                default:
                    throw new Exception($"Invalid range value `{this.value}`");
            }
        }

        public bool Contains(object value)
        {
            if (value is bool b)
            {
                return Contains(b);
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

        public void Extend(bool value)
        {
            switch (this.value)
            {
                case Range.BOTH:
                    break;

                case Range.TRUE:
                    if (!value)
                    {
                        this.value = Range.BOTH;
                    }
                    break;

                case Range.FALSE:
                    if (value)
                    {
                        this.value = Range.BOTH;
                    }
                    break;

                default:
                    throw new Exception($"Invalid range value `{this.value}`");
            }
        }

        public void Extend(object value)
        {
            if (value is bool b)
            {
                Extend(b);
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name}={value}";
        }
    }
}