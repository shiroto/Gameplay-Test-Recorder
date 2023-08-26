using System;

namespace TwoGuyGames.GTR.Core
{
    internal class SubclassMatcher : ITypeMatcher
    {
        private Type type;

        public SubclassMatcher(Type type)
        {
            this.type = type;
        }

        public bool Matches(Type type)
        {
            return type.IsSubclassOf(this.type);
        }
    }
}