using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    public static class RecordingCallbackController
    {
        private static Action endRecordingAndSafe;
        private static RecordingEventHook eventHook;

        public static event EventHandler<RecordingEventArgs> OnFixedUpdate = delegate { };

        public static event EventHandler<RecordingEventArgs> OnInit = delegate { };

        public static event EventHandler<RecordingEventArgs> OnLateUpdate = delegate { };

        public static event EventHandler<RecordingEventArgs> OnStartRecording = delegate { };

        public static event EventHandler<RecordingEventArgs> OnStopRecording = delegate { };

        public static event EventHandler<RecordingEventArgs> OnUpdate = delegate { };

        public static void Init(IRecordingRO recording, Action endRecordingAndSafe)
        {
            Assert.IsNotNull(recording);
            Assert.IsNotNull(endRecordingAndSafe);
            RecordingCallbackController.endRecordingAndSafe = endRecordingAndSafe;
            InitEventHook(recording);
            OnInit(typeof(RecordingCallbackController), new RecordingEventArgs(recording));
        }

        private static void InitEventHook(IRecordingRO recording)
        {
            eventHook = new GameObject("update hook")
            { hideFlags = HideFlags.HideInHierarchy }
                                    .AddComponent<RecordingEventHook>();
            eventHook.OnFixedUpdate += EventHook_OnFixedUpdate;
            eventHook.OnUpdate += EventHook_OnUpdate;
            eventHook.OnRecordingStopped += EventHook_OnRecordingStopped;
            eventHook.OnLateUpdate += EventHook_OnLateUpdate;
            eventHook.Recording = recording;
        }

        internal static void StartRecording(IRecordingRO recording)
        {
            OnStartRecording(typeof(RecordingCallbackController), new RecordingEventArgs(recording));
        }

        internal static void StopRecording(IRecordingRO recording)
        {
            if (eventHook != null)
            {
                GameObjectUtil.Destroy(eventHook.gameObject);
#if UNITY_EDITOR
                if (!UnityEditor.EditorApplication.isPlaying)
                {
                    EventHook_OnRecordingStopped(typeof(RecordingCallbackController), new RecordingEventArgs(recording));
                }
#endif
            }
        }

        private static void EventHook_OnFixedUpdate(object sender, RecordingEventArgs args)
        {
            OnFixedUpdate(typeof(RecordingCallbackController), args);
        }

        private static void EventHook_OnLateUpdate(object sender, RecordingEventArgs args)
        {
            OnLateUpdate(typeof(RecordingCallbackController), args);
        }

        private static void EventHook_OnRecordingStopped(object sender, RecordingEventArgs args)
        {
            OnStopRecording(typeof(RecordingCallbackController), args);
            endRecordingAndSafe();
        }

        private static void EventHook_OnUpdate(object sender, RecordingEventArgs args)
        {
            OnUpdate(typeof(RecordingCallbackController), args);
        }
    }
}