using System;

namespace TwoGuyGames.GTR.Core
{
    public interface IRecordQueue : ICloneable
    {
        int Count { get; }
        bool IsEmpty { get; }

        IRecord Dequeue();

        void Enqueue(IRecord obj);
    }
}