using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.TimeRecorder
{
    internal class TimeRecorder : IRecorder
    {
        public string Key => "Timekeeper";

        public void FixedUpdate(RecordingEventArgs args)
        {
        }

        public void Init(RecordingEventArgs args)
        {
        }

        public void LateUpdate(RecordingEventArgs args)
        {
        }

        public void StartRecording(RecordingEventArgs args)
        {
        }

        public void StopRecording(RecordingEventArgs args)
        {
        }

        public void Update(RecordingEventArgs args)
        {
            ValueRecorder.Store(Time.time, Key);
        }
    }
}