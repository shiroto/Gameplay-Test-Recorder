using System;
using UnityEditor;
using UnityEngine;

namespace TwoGuyGames.GTR.Editor
{
    internal class RecordingWindow : EditorWindow
    {
        private const string TITLE = "Gameplay Test Recorder";

        private RecordingWindowContext context;

        [MenuItem("Window/2GuyGames/Gameplay Test Recorder")]
        public static RecordingWindow GetWindow()
        {
            UnpackWindow.OpenIfNeeded();
            RecordingWindow window = GetWindow<RecordingWindow>(TITLE);
            window.Focus();
            window.Repaint();
            return window;
        }

        private void ForceInit()
        {
            context = null;
            InitIfNeeded();
        }

        private void InitIfNeeded()
        {
            if (context == null || context.State == null)
            {
                context = new RecordingWindowContext(this, new DefaultState());
            }
        }

        private void OnGUI()
        {
            InitIfNeeded();
            try
            {
                EditorGUILayout.BeginVertical();
                context.UpdateGUI();
                EditorGUILayout.EndVertical();
            }
#pragma warning disable 0168
            catch (ArgumentException ex)
#pragma warning restore
            {
                // When this window is opened for the first time we get a "ArgumentException: Getting control 0's position in a group with only 0 controls when doing repaint Aborting".
                // Don't know why. It works fine afterwards, so just catch it and move on...
#if TGG_DEV
                Debug.LogException(ex);
#endif
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        private void Update()
        {
            InitIfNeeded();
            try
            {
                context.Update();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}