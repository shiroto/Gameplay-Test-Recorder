#if REWIRED
using Rewired;
using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.RewiredAdapter
{
    internal class MouseRecorder : IRecorder
    {
        private Mouse mouse;
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
            mouse = ReInput.controllers.Mouse;
        }

        public void StopRecording(RecordingEventArgs args)
        {
        }

        public void Update(RecordingEventArgs args)
        {
            ValueRecorder.Store((Vector3)mouse.screenPosition, Key);
            ValueRecorder.Store(mouse.GetButton(0), Key);
        }
    }
}
#endif