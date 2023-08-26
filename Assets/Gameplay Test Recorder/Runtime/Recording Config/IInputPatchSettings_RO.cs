using System.Collections.Generic;

namespace TwoGuyGames.GTR.Core
{
    public interface IInputPatchSettings_RO
    {
        RecordedSystems GetInputSolutions();

        IReadOnlyList<TypeToPatch> GetTypesToPatch();

        void MergeDuplicateTypes();
    }
}