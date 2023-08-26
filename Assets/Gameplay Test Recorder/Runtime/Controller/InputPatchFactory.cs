using System;
using System.Collections.Generic;

namespace TwoGuyGames.GTR.Core
{
    public static class InputPatchFactory
    {
        private static Dictionary<RecordedSystems, IInputPatcher> availablePatchers = new Dictionary<RecordedSystems, IInputPatcher>();

        public static void AddInputPatcher(RecordedSystems systems, IInputPatcher patcher)
        {
            if (availablePatchers.ContainsKey(systems))
            {
                throw new Exception($"InputSolution `{systems}` already has a patcher!");
            }
            availablePatchers[systems] = patcher;
        }

        public static IInputPatcher CreateInstance(RecordedSystems solution)
        {
            List<IInputPatcher> patchers = new List<IInputPatcher>();
            AddInputSystem(patchers);
            patchers.Add(new FieldMocker());
            patchers.Add(new StaticMocker());
            return new InputPatchCollection(patchers.ToArray());
        }

        private static void AddInputSystem(List<IInputPatcher> patchers)
        {
            foreach (var kvp in availablePatchers)
            {
                if (ActiveInputUtility.IsEnabled(kvp.Key))
                {
                    patchers.Add(kvp.Value);
                }
            }
        }
    }
}