using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    public static class ReplayCallbackController
    {
        private static Action endReplay;

        private static ReplayingEventHook eventHook;

        public static event EventHandler<ReplayEventArgs> OnFixedUpdate = delegate { };

        public static event EventHandler<ReplayEventArgs> OnInit = delegate { };

        public static event EventHandler<ReplayEventArgs> OnLateUpdate = delegate { };

        public static event EventHandler<ReplayEventArgs> OnStartReplaying = delegate { };

        public static event EventHandler<ReplayEventArgs> OnStopReplaying = delegate { };

        public static event EventHandler<ReplayEventArgs> OnReplayEnded = delegate { };

        public static event EventHandler<ReplayEventArgs> OnUpdate = delegate { };

        internal static void ReplayEnded(ReplayEndedEventArgs args)
        {
            OnReplayEnded(typeof(ReplayCallbackController), args);
        }

        internal static void Init(IRecordingRO recording, Action endReplay)
        {
            Assert.IsNotNull(recording);
            Assert.IsNotNull(endReplay);
            ReplayCallbackController.endReplay = endReplay;
            if (ArgHelper.IsBatchmode())
            {
                CanvasResolutionUtility canvasUtility = new GameObject("Canvas Resolution").AddComponent<CanvasResolutionUtility>();
                canvasUtility.resolution = recording.Config.Resolution;
            }
            InitEventHook(recording);
            OnInit(typeof(ReplayCallbackController), new ReplayEventArgs(recording));
        }

        private static void InitEventHook(IRecordingRO recording)
        {
            eventHook = new GameObject("update hook") { hideFlags = HideFlags.HideInHierarchy }.AddComponent<ReplayingEventHook>();
            eventHook.OnFixedUpdate += EventHook_OnFixedUpdate;
            eventHook.OnLateUpdate += EventHook_OnLateUpdate;
            eventHook.OnReplayStopped += EventHook_OnReplayFinished;
            eventHook.OnUpdate += EventHook_OnUpdate;
            eventHook.Recording = recording;
        }

        internal static void StartReplaying(IRecordingRO recording)
        {
            OnStartReplaying(typeof(ReplayCallbackController), new ReplayEventArgs(recording));
        }

        internal static void StopReplaying(IRecordingRO recording)
        {
            if (eventHook != null)
            {
                GameObjectUtil.Destroy(eventHook.gameObject);
#if UNITY_EDITOR
                if (!UnityEditor.EditorApplication.isPlaying)
                {
                    EventHook_OnReplayFinished(typeof(ReplayCallbackController), new ReplayEventArgs(recording));
                }
#endif
            }
        }

        private static void EventHook_OnFixedUpdate(object sender, ReplayEventArgs args)
        {
            OnFixedUpdate(typeof(ReplayCallbackController), args);
        }

        private static void EventHook_OnLateUpdate(object sender, ReplayEventArgs args)
        {
            OnLateUpdate(typeof(ReplayCallbackController), args);
        }

        private static void EventHook_OnReplayFinished(object sender, ReplayEventArgs args)
        {
            OnStopReplaying(typeof(ReplayCallbackController), args);
            endReplay();
        }

        private static void EventHook_OnUpdate(object sender, ReplayEventArgs args)
        {
            OnUpdate(typeof(ReplayCallbackController), args);
        }
    }
}