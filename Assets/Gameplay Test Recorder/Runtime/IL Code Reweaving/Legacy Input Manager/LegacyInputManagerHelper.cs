using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    internal static class LegacyInputManagerHelper
    {
        public static bool Enabled
        {
            get
            {
                InputRecordingGlobalSettingsAsset settings = GtrAssetsUtility.GetGlobalSettingsAsset();
                if (settings == null)
                {
                    return false;
                }
                return settings.UsesInput(RecordedSystems.UNITY_INPUT_MANAGER);
            }
        }

        public static Type InputType => typeof(Input);

        public static void ThrowIfDisabled()
        {
            if (!Enabled)
            {
                throw new Exception("Unity Legacy Input is disabled!");
            }
        }
    }
}