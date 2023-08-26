using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal struct QuaternionRange : IValueSpace<Quaternion>
    {
        public float angle;
        public string id;
        public Quaternion value;

        public QuaternionRange(string id, Quaternion value, float angle)
        {
            this.id = id;
            this.value = value;
            this.angle = angle;
        }

        public QuaternionRange(string id, Quaternion value) : this(id, value, 0)
        {
        }

        public object Get => value;
        public string Id => id;
        public Type RecordedType => typeof(Quaternion);

        public object Clone()
        {
            return new QuaternionRange(id, value, angle);
        }

        public bool Contains(object value)
        {
            if (value is Quaternion v)
            {
                return Contains(v);
            }
            else
            {
                return false;
            }
        }

        public bool Contains(Quaternion value)
        {
            return Quaternion.Angle(this.value, value) <= angle;
        }

        public bool Equals(IRecord other)
        {
            return Get.Equals(other.Get);
        }

        public void Extend(object value)
        {
            if (value is Quaternion v)
            {
                Extend(v);
            }
        }

        public void Extend(Quaternion value)
        {
            angle = Mathf.Max(angle, Quaternion.Angle(this.value, value));
        }

        public override string ToString()
        {
            return $"{GetType().Name}={value}";
        }
    }
}