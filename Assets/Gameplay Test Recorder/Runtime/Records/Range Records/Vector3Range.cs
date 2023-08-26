using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// This implementation interprets the "range" as a radius. That means, if it is extended in one direction, it is also extended in every other direction.
    /// An alternative implementation may interpret the extensions as points in a polygon.
    /// </summary>
    [Serializable]
    internal struct Vector3Range : IValueSpace<Vector3>
    {
        public string id;
        public float radius;

        public Vector3 value;

        public Vector3Range(string id, Vector3 value, float radius)
        {
            this.id = id;
            this.value = value;
            this.radius = radius;
        }

        public Vector3Range(string id, Vector3 value) : this(id, value, 0)
        {
        }

        public object Get => value;
        public string Id => id;
        public Type RecordedType => typeof(Vector3);

        public object Clone()
        {
            return new Vector3Range(id, value, radius);
        }

        public bool Contains(Vector3 value)
        {
            return radius >= Vector3.Distance(this.value, value);
        }

        public bool Contains(object value)
        {
            if (value is Vector3 v)
            {
                return Contains(v);
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

        public void Extend(object value)
        {
            if (value is Vector3 v)
            {
                Extend(v);
            }
        }

        public void Extend(Vector3 value)
        {
            radius = Mathf.Max(radius, Vector3.Distance(this.value, value));
        }

        public override string ToString()
        {
            return $"{GetType().Name}={value}";
        }
    }
}