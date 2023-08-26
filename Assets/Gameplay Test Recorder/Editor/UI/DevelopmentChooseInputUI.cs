using TwoGuyGames.GTR.Core;
using UnityEditor;
using UnityEngine;

namespace TwoGuyGames.GTR.Editor
{
    internal static class DevelopmentChooseInputUI
    {
        private static InputRecordingGlobalSettingsAsset asset;

        public static void DrawDefines()
        {
            // Only draw these for our development.
#if TGG_DEV
            if (asset == null)
            {
                asset = GtrAssetsUtility.GetGlobalSettingsAsset();
            }
            DrawLegacy();
            DrawInputSystem();
            DrawRewired();
            Utility.HorizontalLine();
#endif
        }

        private static void DrawInputSystem()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Input System"))
            {
                asset.ToggleUsedInput(RecordedSystems.UNITY_INPUT_SYSTEM);
            }
            GUILayout.Toggle(ActiveInputUtility.IsInputSystemEnabled(), "");
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawLegacy()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Legacy Input"))
            {
                asset.ToggleUsedInput(RecordedSystems.UNITY_INPUT_MANAGER);
            }
            GUILayout.Toggle(ActiveInputUtility.IsLegacyInputEnabled(), "");
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawRewired()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Rewired"))
            {
                asset.ToggleUsedInput(RecordedSystems.REWIRED);
            }
            GUILayout.Toggle(ActiveInputUtility.IsRewiredEnabled(), "");
            EditorGUILayout.EndHorizontal();
        }
    }
}