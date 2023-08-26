using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class SerializableMethodInfo
    {
        [SerializeField]
        private SerializableSystemType[] args;

        [SerializeField]
        private BindingFlags flags;

        [SerializeField]
        private Mode mode;

        [SerializeField]
        private string name;

        public SerializableMethodInfo(MethodInfo methodInfo, Mode mode = Mode.STORE_BY_FLAGS)
        {
            Assert.IsNotNull(methodInfo);
            this.name = methodInfo.Name;
            this.mode = mode;
            if (mode == Mode.STORE_BY_ARGS)
            {
                ParameterInfo[] args = methodInfo.GetParameters();
                this.args = new SerializableSystemType[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    this.args[i] = new SerializableSystemType(args[i].ParameterType);
                }
            }
            else if (mode == Mode.STORE_BY_FLAGS)
            {
                GetBindingFlags(methodInfo);
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Only store by args if there are multiple methods with the same name and same number of arguments.
        /// </summary>
        public enum Mode
        {
            STORE_BY_FLAGS,
            STORE_BY_ARGS,
        }

        public override bool Equals(object obj)
        {
            return obj is SerializableMethodInfo info &&
                   EqualityComparer<SerializableSystemType[]>.Default.Equals(args, info.args) &&
                   flags == info.flags &&
                   mode == info.mode &&
                   name == info.name;
        }

        public override int GetHashCode()
        {
            int hashCode = -1843979760;
            hashCode = hashCode * -1521134295 + EqualityComparer<SerializableSystemType[]>.Default.GetHashCode(args);
            hashCode = hashCode * -1521134295 + flags.GetHashCode();
            hashCode = hashCode * -1521134295 + mode.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            return hashCode;
        }

        public MethodInfo GetMethod(Type type)
        {
            MethodInfo mi;
            if (mode == Mode.STORE_BY_ARGS)
            {
                mi = type.GetMethod(name, GetArgTypes());
            }
            else if (mode == Mode.STORE_BY_FLAGS)
            {
                mi = type.GetMethod(name, flags);
            }
            else
            {
                throw new Exception();
            }
            if (mi == null)
            {
                Debug.Log("");
            }
            return mi;
        }

        private Type[] GetArgTypes()
        {
            return args.Select(a => a.SystemType).ToArray();
        }

        private void GetBindingFlags(MethodInfo methodInfo)
        {
            if (methodInfo.IsPublic)
            {
                flags |= BindingFlags.Public;
            }
            else
            {
                flags |= BindingFlags.NonPublic;
            }
            if (methodInfo.IsStatic)
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