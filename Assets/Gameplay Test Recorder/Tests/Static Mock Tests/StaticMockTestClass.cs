using UnityEngine;

namespace TwoGuyGames.GTR.Core.Tests
{
    internal class StaticMockTestClass
    {
        public GameObject gameObject;
        public int result;

        public void ReadGameObject()
        {
            gameObject = StaticClass.ReturnGameObject();
        }

        public void ReadStaticClass()
        {
            result = StaticClass.ReturnIntValue();
        }
    }
}