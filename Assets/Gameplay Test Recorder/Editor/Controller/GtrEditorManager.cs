using TwoGuyGames.GTR.Core;
using UnityEditor;
using UnityEngine;

namespace TwoGuyGames.GTR.Editor
{
    public static class GtrEditorManager
    {
        private static ReplayEndedEventArgs lastArgs;

        public static void AnalyzeProject()
        {
            UnityUtility.ForceRecompile();
            InputReferenceSearcher.AnalyzeProject(GtrAssetsUtility.GetGlobalSettingsAsset());
        }

        public static void ExtendLastRecording()
        {
            string path = AssetDatabase.GUIDToAssetPath(lastArgs.Recording.GUID);
            RecordedTestAsset asset = AssetDatabase.LoadAssetAtPath<RecordedTestAsset>(path);
            asset.recording.endState.Extend(lastArgs.ReplayState);
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void Init()
        {
            RecordingController.OnReset += Reset;
            RecordingCallbackController.OnInit += OnSetupRecording;
            RecordingCallbackController.OnStopRecording += OnStopRecording;
            ReplayCallbackController.OnInit += OnSetupReplaying;
            ReplayCallbackController.OnStopReplaying += OnStopReplaying;
            ReplayCallbackController.OnReplayEnded += OnReplayEnded;
        }

        public static void Reset()
        {
            UnityUtility.ForceRecompile();
        }

        private static void OnReplayEnded(object sender, ReplayEventArgs args)
        {
            lastArgs = (ReplayEndedEventArgs)args;
        }

        private static void OnSetupRecording(object sender, RecordingEventArgs args)
        {
            SetWindowResolution(args.Recording);
        }

        private static void OnSetupReplaying(object sender, ReplayEventArgs args)
        {
            SetWindowResolution(args.Recording);
        }

        private static void OnStopRecording(object sender, RecordingEventArgs args)
        {
            ResolutionSetter.Reset();
        }

        private static void OnStopReplaying(object sender, ReplayEventArgs args)
        {
            ResolutionSetter.Reset();
            switch (RecordingController.ReplayFinishedBehaviour)
            {
                case ReplayFinishedMode.EXIT_PLAY_MODE:
                    EditorApplication.isPlaying = false;
                    break;

                case ReplayFinishedMode.PAUSE:
                    EditorApplication.isPaused = true;
                    break;

                case ReplayFinishedMode.KEEP_RUNNING:
                    break;

                default:
                    throw new System.NotImplementedException(RecordingController.ReplayFinishedBehaviour + "");
            }
        }

        private static void SetWindowResolution(IRecordingRO recordTo)
        {
            if (recordTo == null)
            {
                Debug.LogError("No recording.");
            }
            if (recordTo.Config == null)
            {
                Debug.LogError("No config.");
            }
            ResolutionSetter.SetResolution(recordTo.Config);
        }
    }
}