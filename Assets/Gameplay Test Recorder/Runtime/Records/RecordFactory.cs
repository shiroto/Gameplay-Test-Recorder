using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    public static class RecordFactory
    {
        public static IRecord CreateRecord(object obj)
        {
            if (obj == null)
            {
                return new NullRecord();
            }
            else
            {
                Type type = obj.GetType();
                if (type.IsEnum)
                {
                    type = typeof(int);
                }
                else if (obj is Type)
                {
                    type = typeof(Type);
                }
                if (type == typeof(bool))
                {
                    return new BoolRecord((bool)obj);
                }
                else if (type == typeof(float))
                {
                    return new FloatRecord((float)obj);
                }
                else if (type == typeof(double))
                {
                    return new DoubleRecord((double)obj);
                }
                else if (type == typeof(int) || type == typeof(Enum))
                {
                    return new IntRecord((int)obj);
                }
                else if (type == typeof(Vector2))
                {
                    return new Vector2Record((Vector2)obj);
                }
                else if (type == typeof(Vector3))
                {
                    return new Vector3Record((Vector3)obj);
                }
                else if (type == typeof(Vector4))
                {
                    return new Vector4Record((Vector4)obj);
                }
                else if (type == typeof(Quaternion))
                {
                    return new QuaternionRecord((Quaternion)obj);
                }
                else if (type == typeof(string))
                {
                    return new StringRecord((string)obj);
                }
                else if (type == typeof(char))
                {
                    return new CharRecord((char)obj);
                }
                else if (type == typeof(byte[]))
                {
                    return new ByteArrayRecord((byte[])obj);
                }
                else if (type == typeof(Type))
                {
                    return new TypeRecord((Type)obj);
                }
                else
                {
                    Debug.LogError($"Cannot record `{obj.GetType()}`.");
                    return null;
                }
            }
        }

        public static bool IsOfSupportedType(object obj)
        {
            Assert.IsNotNull(obj);
            return IsSupportedType(obj.GetType());
        }

        public static bool IsSupportedType(Type type)
        {
            Assert.IsNotNull(type);
            if (type.IsEnum || type == typeof(bool) || type == typeof(float) || type == typeof(double) || type == typeof(int)
                || type == typeof(Vector2) || type == typeof(Vector3) || type == typeof(Vector4) || type == typeof(Quaternion)
                || type == typeof(string) || type == typeof(char) || type == typeof(byte[]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}