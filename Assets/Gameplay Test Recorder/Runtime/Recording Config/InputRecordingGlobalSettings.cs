using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class InputRecordingGlobalSettings : IInputPatchSettings_RO
    {
        [SerializeField]
        private List<TypeToPatch> typesToPatch;

        [SerializeField]
        private RecordedSystems usedInput;

        public InputRecordingGlobalSettings()
        {
            typesToPatch = new List<TypeToPatch>();
        }

        public bool IsEmpty => typesToPatch.Count == 0;

        public RecordedSystems UsedInput
        {
            get
            {
                return usedInput;
            }
            set
            {
                usedInput = value;
            }
        }

        public void AddTypeToReweave(TypeToPatch ttr)
        {
            Assert.IsNotNull(ttr);
            typesToPatch.Add(ttr);
        }

        public void Clear()
        {
            typesToPatch?.Clear();
        }

        public RecordedSystems GetInputSolutions()
        {
            RecordedSystems inputSolution = 0;
            foreach (TypeToPatch ttr in typesToPatch)
            {
                inputSolution |= ttr.RecordedSystems;
            }
            return inputSolution;
        }

        public IReadOnlyList<TypeToPatch> GetTypesToPatch()
        {
            return typesToPatch;
        }

        public bool IsReady()
        {
            if (ActiveInputUtility.IsInputSystemEnabled())
            {
                return true; // no patching needed for input system
            }
            else
            {
                return !IsEmpty;
            }
        }

        public void MergeDuplicateTypes()
        {
            Dictionary<Type, TypeToPatch> newList = new Dictionary<Type, TypeToPatch>();
            if (typesToPatch != null)
            {
                foreach (TypeToPatch ttr in typesToPatch)
                {
                    if (ttr.Target == null)
                    {
                        throw new Exception("TypeToPatch.Target was null.");
                    }
                    if (newList.TryGetValue(ttr.Target, out TypeToPatch ttr_existing))
                    {
                        newList[ttr.Target] = TypeToPatch.Merge(ttr_existing, ttr);
                    }
                    else
                    {
                        newList[ttr.Target] = ttr;
                    }
                }
                typesToPatch = newList.Values.ToList();
            }
        }
    }
}