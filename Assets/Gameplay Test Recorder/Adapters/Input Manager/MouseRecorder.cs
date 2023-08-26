using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.InputManagerAdapter
{
    internal class MouseRecorder : IRecorder
    {
        public string Key => "MOUSE";

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
            ValueRecorder.Store(Input.mousePosition, Key);
            ValueRecorder.Store(Input.GetMouseButton(0), Key);
        }
    }
}