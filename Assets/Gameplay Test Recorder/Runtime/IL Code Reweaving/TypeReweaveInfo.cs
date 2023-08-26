using System;
using System.Collections.Generic;
using System.Reflection;

namespace TwoGuyGames.GTR.Core
{
    public struct TypeReweaveInfo
    {
        public string method;
        public Type type;

        public TypeReweaveInfo(Type type, string method)
        {
            this.type = type;
            this.method = method;
        }

        public override bool Equals(object obj)
        {
            return obj is TypeReweaveInfo info &&
                   EqualityComparer<Type>.Default.Equals(type, info.type) &&
                   EqualityComparer<string>.Default.Equals(method, info.method);
        }

        public override int GetHashCode()
        {
            int hashCode = -1386533499;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(type);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(method);
            return hashCode;
        }

        public override string ToString()
        {
            return $"{type}:{method}";
        }
    }
}