using System;
using TwoGuyGames.GTR.Core;
using UnityEditor;
using UnityEngine;

namespace TwoGuyGames.GTR.Editor
{
    [Serializable]
    internal class DefaultState : IRecordingWindowState
    {
        public void OnEnter(RecordingWindowContext context)
        {
            RecordingController.UnsetMode();
        }

        public void OnExit(RecordingWindowContext context)
        {
        }

        public void Update(RecordingWindowContext context)
        {
        }

        public void UpdateGUI(RecordingWindowContext context)
        {
            if (!UnpackWindow.IsSetup())
            {
                EditorGUILayout.HelpBox("GTR has not been setup yet!", MessageType.Warning);
                if (GUILayout.Button("Open Setup Window"))
                {
                    UnpackWindow.OpenIfNeeded();
                }
            }
            try
            {
                EditorGUILayout.BeginVertical();
                DevelopmentChooseInputUI.DrawDefines();
                if (!RecordingController.IsPatchApplied)
                {
                    DrawAnalyzeProject();
                    Utility.HorizontalLine();
                }
                if (RecordingController.IsProjectAnalyzed)
                {
                    ReplayPanel(context);
                    Utility.HorizontalLine();
                    DrawTestRunnerRefresh();
                }
                EditorGUILayout.EndVertical();
            }
            catch
            {
                // WHY IS THIS EMPTY???
            }
        }

        private static void ApplyPatchesIfNeeded(RecordingWindowContext context)
        {
            if (!RecordingController.IsPatchApplied && EditorSettingsHelper.IsReloadDomainDisabled())
            {
                if (!RecordingController.ApplyPatches())
                {
                    context.State = new DefaultState();
                }
            }
        }

        private static void DrawAnalyzeProject()
        {
            DrawDomainReloadInfo();
            EditorGUILayout.HelpBox("Analyze the project if this is the first time using GTR. Also re-analyze when you add new classes that handle input or change them.", MessageType.Info);
            if (GUILayout.Button("Analyze Project"))
            {
                GtrEditorManager.AnalyzeProject();
            }
        }

        private static void DrawDomainReloadInfo()
        {
            if (!EditorSettingsHelper.IsReloadDomainDisabled())
            {
                EditorGUILayout.HelpBox("Disabling domain reload in the editor settings will improve performance for recording and replaying and IS NEEDED FOR REPLAYS USING INPUT SYSTEM.", MessageType.Warning);
                if (GUILayout.Button("Disable Domain Reload on Play"))
                {
                    EditorSettingsHelper.DisableDomainReload();
                }
                Utility.HorizontalLine();
            }
        }

        private static void DrawFastLoading(RecordingWindowContext context)
        {
            if (EditorSettingsHelper.IsReloadDomainDisabled())
            {
                if (RecordingController.IsPatchApplied)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Fast Loading Enabled");
                    if (GUILayout.Button("Reset"))
                    {
                        RecordingController.Reset();
                    }
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    if (GUILayout.Button("Enable Fast Loading"))
                    {
                        ApplyPatchesIfNeeded(context);
                    }
                }
            }
        }

        private static void DrawTestRunnerRefresh()
        {
            EditorGUILayout.HelpBox("Replays can be run within the Unity Test Runner. Refresh here to add new replays and remove deleted ones.", MessageType.Info);
            if (GUILayout.Button("Refresh Unity Test Runner"))
            {
                TestRunnerHelper.RefreshReplayTestAssembly();
            }
        }

        private static void ReplayPanel(RecordingWindowContext context)
        {
            DrawFastLoading(context);
            if (GUILayout.Button("Record"))
            {
                context.State = new RecordingState();
            }
            if (GUILayout.Button("Replay"))
            {
                context.State = new ReplayingState();
            }
            if (GUILayout.Button("Import from JSON"))
            {
                context.State = new ImportJsonState();
            }
        }
    }
}