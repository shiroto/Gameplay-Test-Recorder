using System;
using System.Collections.Generic;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    internal class DefaultComparisonWheights : IStateDifferenceWheights
    {
        public static readonly DefaultComparisonWheights INSTANCE = new DefaultComparisonWheights();

        private static Dictionary<Type, float> wheights;

        static DefaultComparisonWheights()
        {
            wheights = new Dictionary<Type, float>
            {
                [typeof(float)] = ReplayResultHelper.SMALL_DIFFERENCE,
                [typeof(double)] = ReplayResultHelper.SMALL_DIFFERENCE,
                [typeof(Vector2)] = ReplayResultHelper.SMALL_DIFFERENCE,
                [typeof(Vector3)] = ReplayResultHelper.SMALL_DIFFERENCE,
                [typeof(Vector4)] = ReplayResultHelper.SMALL_DIFFERENCE,
                [typeof(Quaternion)] = ReplayResultHelper.SMALL_DIFFERENCE,
                [typeof(Rect)] = ReplayResultHelper.SMALL_DIFFERENCE,

                [typeof(sbyte)] = ReplayResultHelper.MEDIUM_DIFFERENCE,
                [typeof(byte)] = ReplayResultHelper.MEDIUM_DIFFERENCE,
                [typeof(ushort)] = ReplayResultHelper.MEDIUM_DIFFERENCE,
                [typeof(short)] = ReplayResultHelper.MEDIUM_DIFFERENCE,
                [typeof(uint)] = ReplayResultHelper.MEDIUM_DIFFERENCE,
                [typeof(int)] = ReplayResultHelper.MEDIUM_DIFFERENCE,

                [typeof(bool)] = ReplayResultHelper.BIG_DIFFERENCE,
                [typeof(Enum)] = ReplayResultHelper.BIG_DIFFERENCE,
                [typeof(string)] = ReplayResultHelper.BIG_DIFFERENCE,
                [typeof(char)] = ReplayResultHelper.BIG_DIFFERENCE,
            };
        }

        public float GetMultiplyer(Type type)
        {
            return wheights[type];
        }
    }
}