using System;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwoGuyGames
{
    public static class ListUtil
    {
        /// <summary>
        /// Checks contents and order of two lists for equality.
        /// </summary>
        public static bool ContentEquals<T>(this IList<T> a, IList<T> b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            else if (a == null || b == null)
            {
                return false;
            }
            else if (a.Count != b.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < a.Count; i++)
                {
                    T o1 = a[i];
                    T o2 = b[i];
                    if (o1 == null && o2 != null || o1 != null && o2 == null)
                    {
                        return false;
                    }
                    else if (!EqualityComparer<T>.Default.Equals(o1, o2))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static bool ContentEquals<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            Assert.IsNotNull(a);
            Assert.IsNotNull(b);
            return ContentEquals(a.ToList(), b.ToList());
        }

        public static bool Exists<T>(this IEnumerable<T> list, Func<T, bool> evaluator)
        {
            Assert.IsNotNull(list);
            Assert.IsNotNull(evaluator);
            foreach (T entry in list)
            {
                if (evaluator(entry))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the best entry in the list, based on the evaluator function. Higher values are better.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="evaluator"></param>
        /// <returns></returns>
        public static T GetBest<T>(this IEnumerable<T> list, Func<T, int> evaluator)
        {
            Assert.IsNotNull(list);
            Assert.IsNotNull(evaluator);
            T best = default(T);
            int bestValue = int.MinValue;
            int value;
            foreach (T entry in list)
            {
                value = EvaluateEntry(evaluator, ref best, ref bestValue, entry);
            }
            return best;
        }

        public static T2 GetEntryOrDefault<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, T2 @default = default)
        {
            Assert.IsNotNull(dictionary);
            if (dictionary.TryGetValue(key, out T2 value))
            {
                return value;
            }
            else
            {
                return @default;
            }
        }

        public static int GetHashCodeContent(this IEnumerable list)
        {
            Assert.IsNotNull(list);
            unchecked
            {
                int hashCode = 1660436204;
                foreach (object o in list)
                {
                    hashCode = hashCode * -1521134295 + (o == null ? 0 : o.GetHashCode());
                }
                return hashCode;
            }
        }

        public static T GetRandomEntry<T>(this IList<T> list)
        {
            Assert.IsNotNull(list);
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static int IndexOf<T>(this IEnumerable<T> list, T obj)
        {
            Assert.IsNotNull(list);
            return list.ToList().IndexOf(obj);
        }

        public static string Log<T>(this IEnumerable<T> list)
        {
            Assert.IsNotNull(list);
            StringBuilder sb = new StringBuilder();
            foreach (T t in list)
            {
                sb.AppendLine(t.ToString());
            }
            return sb.ToString();
        }

        private static int EvaluateEntry<T>(Func<T, int> evaluator, ref T best, ref int bestValue, T entry)
        {
            Assert.IsNotNull(evaluator);
            int value = evaluator(entry);
            if (value > bestValue)
            {
                bestValue = value;
                best = entry;
            }
            return value;
        }
    }
}