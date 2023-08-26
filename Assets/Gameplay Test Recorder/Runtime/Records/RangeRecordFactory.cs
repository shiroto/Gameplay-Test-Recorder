using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    public static class RangeRecordFactory
    {
        public static IValueSpace CreateRange(string id, IRecord record)
        {
            return CreateRange(id, record.Get);
        }

        public static IValueSpace<T> CreateRange<T>(T value)
        {
            return (IValueSpace<T>)CreateRange("", (object)value);
        }

        public static IValueSpace<T> CreateRange<T>(string id, T value)
        {
            return (IValueSpace<T>)CreateRange(id, (object)value);
        }

        public static IValueSpace CreateRange(string id, object value)
        {
            if (value.GetType().IsEnum)
            {
                return new EnumRange(id, (int)value);
            }
            else if (value.GetType() == typeof(bool))
            {
                return new BoolRange(id, (bool)value);
            }
            else if (value.GetType() == typeof(float) || value.GetType() == typeof(double)
                || value.GetType() == typeof(sbyte) || value.GetType() == typeof(byte)
                || value.GetType() == typeof(uint) || value.GetType() == typeof(int)
                || value.GetType() == typeof(ushort) || value.GetType() == typeof(short))
            {
                double convertedValue = (double)Convert.ChangeType(value, typeof(double));
                return new NumberRange(id, convertedValue, convertedValue);
            }
            else if (value.GetType() == typeof(Vector2))
            {
                return new Vector2Range(id, (Vector2)value);
            }
            else if (value.GetType() == typeof(Vector3))
            {
                return new Vector3Range(id, (Vector3)value);
            }
            else if (value.GetType() == typeof(Vector4))
            {
                return new Vector4Range(id, (Vector4)value);
            }
            else if (value.GetType() == typeof(Quaternion))
            {
                return new QuaternionRange(id, (Quaternion)value);
            }
            else if (value.GetType() == typeof(string))
            {
                return new StringRange(id, (string)value);
            }
            else if (value.GetType() == typeof(char))
            {
                return new CharRange(id, (char)value);
            }
            else
            {
                Debug.LogError($"Cannot create range for `{value.GetType()}`.");
                return null;
            }
        }
    }
}