using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal class MockedField
    {
        [SerializeField]
        private string fieldName;

        [SerializeField]
        private BindingFlags flags;

        public MockedField(FieldInfo fieldInfo)
        {
            Assert.IsNotNull(fieldInfo);
            this.fieldName = fieldInfo.Name;
            GetBindingFlags(fieldInfo);
        }

        public FieldInfo GetField(Type type)
        {
            return type.GetField(fieldName, flags);
        }

        private void GetBindingFlags(FieldInfo fieldInfo)
        {
            if (fieldInfo.IsPublic)
            {
                flags |= BindingFlags.Public;
            }
            else
            {
                flags |= BindingFlags.NonPublic;
            }
            if (fieldInfo.IsStatic)
            {
                flags |= BindingFlags.Static;
            }
            else
            {
                flags |= BindingFlags.Instance;
            }
        }
    }
}