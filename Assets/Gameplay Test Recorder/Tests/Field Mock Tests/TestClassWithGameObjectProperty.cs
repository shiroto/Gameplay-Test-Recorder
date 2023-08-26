using UnityEngine;

namespace TwoGuyGames.GTR.Core.Tests
{
    internal class TestClassWithGameObjectProperty
    {
        public GameObject gameObject;

        public GameObject GameObject
        {
            get
            {
                return gameObject;
            }
        }
    }
}