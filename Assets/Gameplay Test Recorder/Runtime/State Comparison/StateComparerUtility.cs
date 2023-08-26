using System;
using System.Collections.Generic;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    internal class StateComparerUtility
    {
        private static Dictionary<Type, IStateComparer> comparers;

        public static float Compare(Type type, IValueSpace x, IRecord y, IStateDifferenceWheights wheights)
        {
            try
            {
                if (!x.Contains(y.Get))
                {
                    return wheights.GetMultiplyer(type);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"type=`{type}`, x=`{x}`, y=`{y}`");
                Debug.LogException(ex);
                return float.MaxValue;
            }
        }
    }
}