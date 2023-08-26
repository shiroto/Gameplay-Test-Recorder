using UnityEngine;

namespace TwoGuyGames.GTR.Core.Tests
{
    internal static class StaticClass
    {
        public static GameObject gameObject;
        public static int intValue;

        public static GameObject ReturnGameObject()
        {
            return gameObject;
        }

        public static int ReturnIntValue()
        {
            return intValue;
        }
    }
}