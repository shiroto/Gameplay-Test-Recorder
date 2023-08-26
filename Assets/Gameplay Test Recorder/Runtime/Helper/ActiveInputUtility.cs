using System;

namespace TwoGuyGames.GTR.Core
{
    public static class ActiveInputUtility
    {
        public static bool IsEnabled(RecordedSystems inputSolution)
        {
            bool res = true;
            if (inputSolution.HasFlag(RecordedSystems.UNITY_INPUT_MANAGER))
            {
                res &= IsLegacyInputEnabled();
            }
            if (inputSolution.HasFlag(RecordedSystems.UNITY_INPUT_SYSTEM))
            {
                res &= IsInputSystemEnabled();
            }
            if (inputSolution.HasFlag(RecordedSystems.REWIRED))
            {
                res &= IsRewiredEnabled();
            }
            return res;
        }

        public static bool IsInputSystemEnabled()
        {
            InputRecordingGlobalSettingsAsset settings = GtrAssetsUtility.GetGlobalSettingsAsset();
            if (settings == null)
            {
                return false;
            }
            return settings.UsesInput(RecordedSystems.UNITY_INPUT_SYSTEM);
        }

        public static bool IsLegacyInputEnabled()
        {
            return LegacyInputManagerHelper.Enabled;
        }

        public static bool IsRewiredEnabled()
        {
            InputRecordingGlobalSettingsAsset settings = GtrAssetsUtility.GetGlobalSettingsAsset();
            if (settings == null)
            {
                return false;
            }
            return settings.UsesInput(RecordedSystems.REWIRED);
        }
    }
}