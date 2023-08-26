using System;
using System.Collections.Generic;

namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// Strings dont have a range.
    /// </summary>
    [Serializable]
    internal struct StringRange : IValueSpace<string>
    {
        public string id;
        public List<string> values;

        public StringRange(string id, string value)
        {
            this.id = id;
            this.values = new List<string>()
            {
                value
            };
        }

        public StringRange(string id, List<string> values)
        {
            this.id = id;
            this.values = values;
        }

        public object Get => values;
        public string Id => id;
        public Type RecordedType => typeof(string);

        public object Clone()
        {
            return new StringRange(id, values);
        }

        public bool Contains(string value)
        {
            return values.Contains(value);
        }

        public bool Contains(object value)
        {
            if (value is string s)
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

        public void Extend(string value)
        {
            if (!values.Contains(value))
            {
                values.Add(value);
            }
        }

        public void Extend(object value)
        {
            if (value is string s)
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