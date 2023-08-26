using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// This implementation interprets the "range" as a radius. That means, if it is extended in one direction, it is also extended in every other direction.
    /// An alternative implementation may interpret the extensions as points in a polygon.
    /// </summary>
    [Serializable]
    internal struct Vector2Range : IValueSpace<Vector2>
    {
        public string id;
        public float radius;
        public Vector2 value;

        public Vector2Range(string id, Vector2 value, float radius)
        {
            this.id = id;
            this.value = value;
            this.radius = radius;
        }

        public Vector2Range(string id, Vector2 value) : this(id, value, 0)
        {
        }

        public object Get => value;
        public string Id => id;
        public Type RecordedType => typeof(Vector2);

        public object Clone()
        {
            return new Vector2Range(id, value, radius);
        }

        public bool Contains(Vector2 value)
        {
            return radius >= Vector2.Distance(this.value, value);
        }

        public bool Contains(object value)
        {
            if (value is Vector2 v)
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

        public void Extend(Vector2 value)
        {
            radius = Mathf.Max(radius, Vector2.Distance(this.value, value));
        }

        public void Extend(object value)
        {
            if (value is Vector2 v)
            {
                Extend(v);
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name}={value}";
        }
    }
}