using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// This implementation interprets the "range" as a radius. That means, if it is extended in one direction, it is also extended in every other direction.
    /// An alternative implementation may interpret the extensions as points in a polygon.
    /// </summary>
    [Serializable]
    internal struct Vector4Range : IValueSpace<Vector4>
    {
        public string id;
        public float radius;
        public Vector4 value;

        public Vector4Range(string id, Vector4 value, float radius)
        {
            this.id = id;
            this.value = value;
            this.radius = radius;
        }

        public Vector4Range(string id, Vector4 value) : this(id, value, 0)
        {
        }

        public object Get => value;
        public string Id => id;
        public Type RecordedType => typeof(Vector4);

        public object Clone()
        {
            return new Vector4Range(id, value, radius);
        }

        public bool Contains(object value)
        {
            if (value is Vector4 v)
            {
                return Contains(v);
            }
            else
            {
                return false;
            }
        }

        public bool Contains(Vector4 value)
        {
            return radius >= Vector4.Distance(this.value, value);
        }

        public bool Equals(IRecord other)
        {
            return Get.Equals(other.Get);
        }

        public void Extend(object value)
        {
            if (value is Vector4 v)
            {
                Extend(v);
            }
        }

        public void Extend(Vector4 value)
        {
            radius = Mathf.Max(radius, Vector4.Distance(this.value, value));
        }

        public override string ToString()
        {
            return $"{GetType().Name}={value}";
        }
    }
}