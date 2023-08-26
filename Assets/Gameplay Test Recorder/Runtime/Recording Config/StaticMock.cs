using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class StaticMock
    {
        [SerializeField]
        private SerializableSystemType callee;

        [SerializeField]
        private SerializableMethodInfo[] methods;

        public StaticMock(Type callee, params SerializableMethodInfo[] methods)
        {
            Assert.IsNotNull(callee);
            this.callee = new SerializableSystemType(callee);
            if (methods == null)
            {
                this.methods = new SerializableMethodInfo[0];
            }
            else
            {
                this.methods = methods;
            }
        }

        public Type Callee => callee.SystemType;

        /// <summary>
        /// Returns methods that are to be recorded. When nothing is returned EVERYTHING should be recorded.
        /// </summary>
        public IReadOnlyList<MethodInfo> GetMethods()
        {
            return methods.Select(m => m.GetMethod(Callee)).ToArray();
        }

        public bool IsMockedMethod(MethodInfo methodInfo)
        {
            if (methods.Length == 0)
            {
                return true;
            }
            foreach (SerializableMethodInfo serializedMethodInfo in methods)
            {
                MethodInfo mi = serializedMethodInfo.GetMethod(Callee);
                if (mi.Equals(methodInfo))
                {
                    return true;
                }
            }
            return false;
        }
    }
}