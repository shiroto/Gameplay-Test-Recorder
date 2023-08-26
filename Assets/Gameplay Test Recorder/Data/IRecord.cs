using System;

namespace TwoGuyGames.GTR.Core
{
    public interface IRecord : ICloneable, IEquatable<IRecord>
    {
        object Get
        {
            get;
        }

        Type RecordedType
        {
            get;
        }
    }
}