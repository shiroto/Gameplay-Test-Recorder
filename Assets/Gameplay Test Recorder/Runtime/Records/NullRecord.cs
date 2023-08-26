using System;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    internal class NullRecord : IRecord
    {
        public object Get => null;

        public Type RecordedType => null;

        public object Clone()
        {
            return new NullRecord();
        }

        public bool Equals(IRecord other)
        {
            return other is NullRecord;
        }
    }
}