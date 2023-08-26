using System;
using System.Collections.Generic;

namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// Strings dont have a range.
    /// </summary>
    [Serializable]
    internal struct CharRange : IValueSpace<char>
    {
        public string id;
        public List<char> values;

        public CharRange(string id, char value)
        {
            this.id = id;
            this.values = new List<char>()
            {
                value
            };
        }

        public CharRange(string id, List<char> values)
        {
            this.id = id;
            this.values = values;
        }

        public object Get => values;
        public string Id => id;
        public Type RecordedType => typeof(char);

        public object Clone()
        {
            return new CharRange(id, values);
        }

        public bool Contains(char value)
        {
            return values.Contains(value);
        }

        public bool Contains(object value)
        {
            if (value is char s)
            {
                return Contains(s);
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

        public void Extend(char value)
        {
            if (!values.Contains(value))
            {
                values.Add(value);
            }
        }

        public void Extend(object value)
        {
            if (value is char s)
            {
                Extend(s);
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name}={values.Log()}";
        }
    }
}