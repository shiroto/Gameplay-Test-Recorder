using System.Linq;
using TwoGuyGames.GTR.Core;

namespace TwoGuyGames.GTR.Editor
{
    public static class RecordingInputTypeHelper
    {
        public static bool ContainsInputSystem(IRecordingRO recording)
        {
            return recording.GetRecordKeys().Contains(RecordedSystems.UNITY_INPUT_SYSTEM.ToString());
        }

        public static bool ContainsLegacyInput(IRecordingRO recording)
        {
            return recording.GetRecordKeys().Contains(RecordedSystems.UNITY_INPUT_MANAGER.ToString());
        }

        public static bool ContainsRewired(IRecordingRO recording)
        {
            return recording.GetRecordKeys().Contains(RecordedSystems.REWIRED.ToString());
        }
    }
}