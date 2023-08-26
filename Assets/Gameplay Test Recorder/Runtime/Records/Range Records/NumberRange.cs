using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct NumberRange :
        IValueSpace<double>, IValueSpace<float>,
        IValueSpace<int>, IValueSpace<uint>,
        IValueSpace<short>, IValueSpace<ushort>,
        IValueSpace<byte>, IValueSpace<sbyte>
    {
        public string id;
        public double max;
        public double min;

        public NumberRange(string id, double min, double max)
        {
            this.id = id;
            if (min > max)
            {
                this.max = min;
                this.min = max;
            }
            else
            {
                this.min = min;
                this.max = max;
            }
        }

        public object Get => new Vector2((float)min, (float)max);
        public string Id => id;
        public Type RecordedType => typeof(double);

        public object Clone()
        {
            return new NumberRange(id, min, max);
        }

        public bool Contains(double value)
        {
            return value >= min && value <= max;
        }

        public bool Contains(object value)
        {
            if (value is string || value is bool) // Number only strings and bools can also be converted to double, but we only accept number types.
            {
                return false;
            }
            try
            {
                return Contains(Convert.ToDouble(value));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Contains(int value)
        {
            return Contains((double)value);
        }

        public bool Contains(float value)
        {
            return Contains((double)value);
        }

        public bool Contains(byte value)
        {
            return Contains((double)value);
        }

        public bool Contains(sbyte value)
        {
            return Contains((double)value);
        }

        public bool Contains(uint value)
        {
            return Contains((double)value);
        }

        public bool Contains(short value)
        {
            return Contains((double)value);
        }

        public bool Contains(ushort value)
        {
            return Contains((double)value);
        }

        public bool Equals(IRecord other)
        {
            return Get.Equals(other.Get);
        }

        public void Extend(double value)
        {
            if (value < min)
            {
                min = value;
            }
            else if (value > max)
            {
                max = value;
            }
        }

        public void Extend(object value)
        {
            if (value is string || value is bool) // Number only strings and bools can also be converted to double, but we only accept number types.
            {
                return;
            }
            try
            {
                Extend(Convert.ToDouble(value));
            }
            catch (Exception ex)
            {
                Debug.LogError($"Could not extend NumberRange with `{value}`.");
                Debug.LogException(ex);
            }
        }

        public void Extend(int value)
        {
            Extend((double)value);
        }

        public void Extend(float value)
        {
            Extend((double)value);
        }

        public void Extend(byte value)
        {
            Extend((double)value);
        }

        public void Extend(sbyte value)
        {
            Extend((double)value);
        }

        public void Extend(uint value)
        {
            Extend((double)value);
        }

        public void Extend(short value)
        {
            Extend((double)value);
        }

        public void Extend(ushort value)
        {
            Extend((double)value);
        }

        public override string ToString()
        {
            return $"{GetType().Name}={min}-{max}";
        }
    }
}