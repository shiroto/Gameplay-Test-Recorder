using System;
using System.Collections.Generic;

namespace TwoGuyGames
{
    /// <summary>
    /// Original from https://codereview.stackexchange.com/questions/194967/get-all-combinations-of-selecting-k-elements-from-an-n-sized-array
    /// </summary>
    public static class ListCombinations
    {
        public static IEnumerable<T[]> CombinationsRosettaWoRecursion<T>(IList<T> array, int m)
        {
            if (array.Count < m)
            {
                throw new ArgumentException("Array length can't be less than number of selected elements.");
            }
            else if (m < 0)
            {
                throw new ArgumentException("Number of selected elements can't be negative.");
            }
            else if (m == 0)
            {
                yield return new T[0];
            }
            else
            {
                T[] result = new T[m];
                foreach (int[] j in CombinationsRosettaWoRecursion(m, array.Count))
                {
                    for (int i = 0; i < m; i++)
                    {
                        result[i] = array[j[i]];
                    }
                    yield return (T[])result.Clone();
                }
            }
        }

        /// <summary>
        /// Enumerate all possible m-size combinations of [0, 1, ..., n-1] array
        /// in lexicographic order (first [0, 1, 2, ..., m-1]).
        /// </summary>
        private static IEnumerable<int[]> CombinationsRosettaWoRecursion(int m, int n)
        {
            int[] result = new int[m];
            Stack<int> stack = new Stack<int>(m);
            stack.Push(0);
            while (stack.Count > 0)
            {
                int index = stack.Count - 1;
                int value = stack.Pop();
                while (value < n)
                {
                    result[index++] = value++;
                    stack.Push(value);
                    if (index != m)
                    {
                        continue;
                    }
                    yield return result;
                    break;
                }
            }
        }
    }
}