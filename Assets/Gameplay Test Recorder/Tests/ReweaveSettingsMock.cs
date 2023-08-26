using System;
using System.Collections.Generic;
using System.Linq;

namespace TwoGuyGames.GTR.Core.Tests
{
    internal class ReweaveSettingsMock : IInputPatchSettings_RO
    {
        public RecordedSystems InputSolution;
        public List<TypeToPatch> typeToReweave;

        public ReweaveSettingsMock()
        {
            typeToReweave = new List<TypeToPatch>();
        }

        public RecordedSystems GetInputSolutions()
        {
            return InputSolution;
        }

        public IReadOnlyList<TypeToPatch> GetTypesToPatch()
        {
            return typeToReweave;
        }

        public void MergeDuplicateTypes()
        {
            Dictionary<Type, TypeToPatch> newList = new Dictionary<Type, TypeToPatch>();
            foreach (TypeToPatch ttr in typeToReweave)
            {
                if (newList.TryGetValue(ttr.Target, out TypeToPatch ttr_existing))
                {
                    newList[ttr.Target] = TypeToPatch.Merge(ttr_existing, ttr);
                }
                else
                {
                    newList[ttr.Target] = ttr;
                }
            }
            typeToReweave = newList.Values.ToList();
        }
    }
}