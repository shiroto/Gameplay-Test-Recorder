using System.Collections.Generic;

namespace TwoGuyGames.GTR.Core
{
    public interface IObjectStateCollection : IObjectState, ICollection<IObjectState>
    {
        bool TryGetValue(string id, out IObjectState objectState);
    }
}