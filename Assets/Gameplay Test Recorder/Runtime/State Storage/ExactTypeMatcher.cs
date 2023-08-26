using System;

namespace TwoGuyGames.GTR.Core
{
    internal class ExactTypeMatcher : ITypeMatcher
    {
        private Type type;

        public ExactTypeMatcher(Type type)
        {
            this.type = type;
        }

        public bool Matches(Type type)
        {
            return type.Equals(this.type);
        }
    }
}