using TwoGuyGames.GTR.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TwoGuyGames.GTR.InputSystemRecorder
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
            Mouse mouse = Mouse.current;
            ValueRecorder.Store((Vector3)mouse.position.ReadValue(), Key);
            ValueRecorder.Store(mouse.leftButton.ReadValue() == 1, Key);
        }
    }
}