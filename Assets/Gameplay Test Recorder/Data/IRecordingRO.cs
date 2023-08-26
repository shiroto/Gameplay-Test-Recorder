using System.Collections.Generic;

namespace TwoGuyGames.GTR.Core
{
    public interface IRecordingRO
    {
        IRecordConfigRO Config
        {
            get;
        }

        string GUID
        {
            get;
        }

        IReadOnlyList<string> GetRecordKeys();
    }
}