using System;
using TwoGuyGames.GTR.Core;
using UnityEditor;
using UnityEngine;

namespace TwoGuyGames.GTR.Editor
{
    [Serializable]
    internal class ReplayingState : IRecordingWindowState
    {
        private static readonly GUIContent onReplayEndGui = new GUIContent("On Replay End", "Behaviour when replay has finished. PAUSE: Pause the playmode, control is handed over to you. KEEP_RUNNING: Control is handed over to you without pausing. EXIT_PLAY_MODE: Stop playmode altogether.");
        private float lastReplayDiff;
        private RecordedTestAsset recordAsset;

        public void OnEnter(RecordingWindowContext context)
        {
            RecordingController.ReplayFinishedBehaviour = ReplayFinishedMode.PAUSE;
            ReplayCallbackController.OnReplayEnded += OnReplayFinished;
        }

        public void OnExit(RecordingWindowContext context)
        {
            RecordingController.UnsetMode();
        }

        public void Update(RecordingWindowContext context)
        {
        }

        public void UpdateGUI(RecordingWindowContext context)
        {
            EditorGUILayout.BeginVertical();
            EditorLayoutUtility.DrawInHorizontal(DrawAsset);
            EditorLayoutUtility.DrawInHorizontal(DrawAutoExit);
            EditorGUILayout.HelpBox("Do not unfocus the game view during replay.", MessageType.Warning);
            ReplayControls();
            LastReplay();
            Utility.HorizontalLine();
            DrawExit(context);
            EditorGUILayout.EndVertical();
        }

        private static void DrawExit(RecordingWindowContext context)
        {
            if (RecordingController.IsInactive && !EditorApplication.isPlaying && GUILayout.Button("Back"))
            {
                context.State = new DefaultState();
            }
        }

        private static void PatchInfo()
        {
            if (!RecordingController.IsPatchApplied)
            {
                EditorGUILayout.HelpBox("The necessary patches to code have not yet been applied. Loading the scene will take longer than usual.", MessageType.Info);
            }
        }

        private void ControllerInactiveControls()
        {
            if (EditorApplication.isPlaying && GUILayout.Button("Exit Play Mode"))
            {
                EditorApplication.isPlaying = false;
            }
            else if (!EditorApplication.isPlaying && GUILayout.Button("Start Replay"))
            {
                RecordingController.SetupAndStartReplaying(recordAsset.recording);
            }
        }

        private void DrawAsset()
        {
            EditorGUILayout.PrefixLabel("Replay from");
            //recordAsset = (RecordedTestAsset)EditorGUILayout.ObjectField(recordAsset, typeof(RecordedTestAsset), false);
            try
            {
                recordAsset = (RecordedTestAsset)EditorGUILayout.ObjectField(recordAsset, typeof(RecordedTestAsset), false);
            }
            catch (Exception e)
            {
                if (ExitGUIUtility.ShouldRethrowException(e))
                {
                    //throw;
                    return;
                }
                Debug.LogException(e);
            }
        }

        private void DrawAutoExit()
        {
            EditorGUILayout.PrefixLabel(onReplayEndGui);
            RecordingController.ReplayFinishedBehaviour = (ReplayFinishedMode)EditorGUILayout.EnumPopup(RecordingController.ReplayFinishedBehaviour);
        }

        private void LastReplay()
        {
            if (RecordingController.IsInactive && lastReplayDiff >= ReplayResultHelper.MEDIUM_DIFFERENCE)
            {
                EditorGUILayout.LabelField($"The last replay went differently than expected. (diff=`{lastReplayDiff}`).");
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"Was it still correct?");
                if (GUILayout.Button("Yes"))
                {
                    GtrEditorManager.ExtendLastRecording();
                    lastReplayDiff = 0;
                }
                if (GUILayout.Button("No"))
                {
                    lastReplayDiff = 0;
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        private void OnReplayFinished(object sender, ReplayEventArgs args)
        {
            ReplayEndedEventArgs endedArgs = (ReplayEndedEventArgs)args;
            lastReplayDiff = endedArgs.Result;
        }

        private void ReplayControls()
        {
            if (recordAsset != null)
            {
                PatchInfo();
                StartStopButtons();
            }
        }

        private void StartStopButtons()
        {
            if (RecordingController.IsInactive)
            {
                ControllerInactiveControls();
            }
            else if (RecordingController.IsReplaying && GUILayout.Button("Stop Replay"))
            {
                RecordingController.StopReplaying();
            }
        }
    }
}