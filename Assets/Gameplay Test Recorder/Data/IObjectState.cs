using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    public interface IObjectState
    {
        string Id
        {
            get;
        }

        IRecord Record
        {
            get;
        }

        bool StateMatches(IObjectState other);
    }
}