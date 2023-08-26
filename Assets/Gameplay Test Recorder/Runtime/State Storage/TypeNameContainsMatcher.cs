using System;

namespace TwoGuyGames.GTR.Core
{
    internal class TypeNameContainsMatcher : ITypeMatcher
    {
        private string name;

        public TypeNameContainsMatcher(string name)
        {
            this.name = name;
        }

        public bool Matches(Type type)
        {
            return type.FullName.Contains(name);
        }
    }
}