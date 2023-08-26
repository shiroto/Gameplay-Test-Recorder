using System.Collections.Generic;

namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// Represents a collection of multiple states, that multiple objects can be in.
    /// For instance an object with multiple fields that can be in multiple states.
    /// </summary>
    public interface IValueSpaceCollection : IValueSpace<IObjectStateCollection>, ICollection<IValueSpace>
    {
        bool TryGetValue(string id, out IValueSpace objectState);
    }
}