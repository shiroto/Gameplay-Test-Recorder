namespace TwoGuyGames.GTR.Editor
{
    public static class EditorSettingsHelper
    {
        public static void DisableDomainReload()
        {
            UnityEditor.EditorSettings.enterPlayModeOptionsEnabled = true;
            if (!UnityEditor.EditorSettings.enterPlayModeOptions.HasFlag(UnityEditor.EnterPlayModeOptions.DisableDomainReload))
            {
                UnityEditor.EditorSettings.enterPlayModeOptions |= UnityEditor.EnterPlayModeOptions.DisableDomainReload;
            }
        }

        public static bool IsReloadDomainDisabled()
        {
            return UnityEditor.EditorSettings.enterPlayModeOptionsEnabled
                && UnityEditor.EditorSettings.enterPlayModeOptions.HasFlag(UnityEditor.EnterPlayModeOptions.DisableDomainReload);
        }
    }
}