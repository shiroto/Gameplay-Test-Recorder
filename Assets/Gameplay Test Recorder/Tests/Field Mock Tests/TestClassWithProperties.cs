using UnityEngine;

namespace TwoGuyGames.GTR.Core.Tests
{
    internal class TestClassWithProperties
    {
        public GameObject go;
        public int myInt;

        public GameObject GameObject1
        {
            get
            {
                return go;
            }
        }

        public GameObject GameObject2
        {
            get;
            set;
        }

        public GameObject GameObject3
        {
            get
            {
                var res = go;
                return res;
            }
        }

        public int MyInt
        {
            get
            {
                return myInt;
            }
        }

        public int MyInt2
        {
            get;
            set;
        }

        public int MyInt3
        {
            get
            {
                var res = myInt;
                return res;
            }
        }
    }
}