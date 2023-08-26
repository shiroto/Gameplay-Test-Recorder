using System;
using TwoGuyGames.GTR.Core;
using UnityEditor;
using UnityEngine;

namespace TwoGuyGames.GTR.Editor
{
    /// <summary>
    /// Triggered when there are no Global Settings.
    /// </summary>
    internal class UnpackWindow : EditorWindow
    {
        private const string TITLE = "GTR Setup";

        [SerializeField]
        private bool inputManager;

        [SerializeField]
        private bool inputSystem;

        [SerializeField]
        private bool rewired;

        public static UnpackWindow GetWindow()
        {
            try
            {
                UnpackWindow window = GetWindow<UnpackWindow>(TITLE);
                window.Focus();
                window.Repaint();
                return window;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return null;
            }
        }

        public static bool IsSetup()
        {
            return GtrAssetsUtility.GetGlobalSettingsAsset() != null;
        }

        public static UnpackWindow OpenIfNeeded()
        {
            if (!IsSetup())
            {
                if (!EditorWindow.HasOpenInstances<UnpackWindow>())
                {
                    return GetWindow();
                }
            }
            return null;
        }

        private void CreateSettingsAsset()
        {
            InputRecordingGlobalSettingsAsset asset = TestFolderUtil_Editor.CreateGlobalSettingsAsset();
            if (inputManager)
            {
                asset.ToggleUsedInput(RecordedSystems.UNITY_INPUT_MANAGER);
            }
            if (inputSystem)
            {
                asset.ToggleUsedInput(RecordedSystems.UNITY_INPUT_SYSTEM);
            }
            if (rewired)
            {
                asset.ToggleUsedInput(RecordedSystems.REWIRED);
            }
        }

        private void DrawToggles()
        {
            inputManager = EditorGUILayout.Toggle("Unity Input Manager (Old)", inputManager);
            inputSystem = EditorGUILayout.Toggle("Unity Input System (New)", inputSystem);
            rewired = EditorGUILayout.Toggle("Rewired", rewired);
        }

        private bool IsNewAndOldUnityInput()
        {
            return inputManager && inputSystem;
        }

        private bool IsOnlyRewired()
        {
            return rewired && !inputManager && !inputSystem;
        }

        private void OnGUI()
        {
            try
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.HelpBox("Hello! This seems to be the first time you are using GTR. Please select the input system(s) you want to use below. Make sure the appropriate libraries are installed.", MessageType.Info);
                DrawToggles();
                ShowWarnings();
                if (GUILayout.Button("Unpack"))
                {
                    UnpackSelected();
                    CreateSettingsAsset();
                    Close();
                }
                EditorGUILayout.EndVertical();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        private void ShowWarnings()
        {
            if (IsOnlyRewired())
            {
                EditorGUILayout.HelpBox("Note that you only selected Rewired. GUI input with Rewired is not yet supported. To record GUI interactions, select one of Unity's input systems as well.", MessageType.Warning);
            }
            if (IsNewAndOldUnityInput())
            {
                EditorGUILayout.HelpBox("Note that you selected Unity Input Manager and Unity Input System. It is recommended that you only use one of the two.", MessageType.Warning);
            }
            if (inputSystem)
            {
                EditorGUILayout.HelpBox("Note that Unity Input System is not yet fully supported. Please refer to README for more information.", MessageType.Warning);
            }
        }

        private void UnpackSelected()
        {
            if (inputManager)
            {
                Zip.UnpackInputManager();
            }
            if (inputSystem)
            {
                Zip.UnpackInputSystem();
            }
            if (rewired)
            {
                Zip.UnpackRewired();
            }
        }
    }
}