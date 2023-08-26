using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    public static class RecorderManager
    {
        private static bool isInitialized = false;
        private static List<IRecorder> recorders = new List<IRecorder>();
        private static HashSet<string> usedKeys = new HashSet<string>();

        public static void AddRecorder(IRecorder recorder)
        {
            Assert.IsNotNull(recorder);
            Assert.IsFalse(usedKeys.Contains(recorder.Key), $"There is already a recorder registered with key `{recorder.Key}`.");
            recorders.Add(recorder);
        }

        private static void FixedUpdate(object sender, RecordingEventArgs args)
        {
            recorders.ForEach(r => r.FixedUpdate(args));
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                RecordingCallbackController.OnInit += InitRecorders;
                RecordingCallbackController.OnFixedUpdate += FixedUpdate;
                RecordingCallbackController.OnLateUpdate += LateUpdate;
                RecordingCallbackController.OnStartRecording += StartRecording;
                RecordingCallbackController.OnStopRecording += StopRecording;
                RecordingCallbackController.OnUpdate += Update;
            }
        }

        private static void InitRecorders(object sender, RecordingEventArgs args)
        {
            recorders.ForEach(r => r.Init(args));
        }

        private static void LateUpdate(object sender, RecordingEventArgs args)
        {
            recorders.ForEach(r => r.LateUpdate(args));
        }

        private static void StartRecording(object sender, RecordingEventArgs args)
        {
            recorders.ForEach(r => r.StartRecording(args));
        }

        private static void StopRecording(object sender, RecordingEventArgs args)
        {
            recorders.ForEach(r => r.StopRecording(args));
        }

        private static void Update(object sender, RecordingEventArgs args)
        {
            recorders.ForEach(r => r.Update(args));
        }
    }
}