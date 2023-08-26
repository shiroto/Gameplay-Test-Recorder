using System.Collections.Generic;
using System.Linq;
using TwoGuyGames.GTR.Core;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace TwoGuyGames.GTR.InputSystemRecorder
{
    public class InputSystemRecorder : IRecorder
    {
        private const string END_OF_DEVICES = "END OF DEVICES";
        private const string FRAME = "Frame";
        private const string START_OF_DEVICES = "START OF DEVICES";
        private static readonly string KEY = RecordedSystems.UNITY_INPUT_SYSTEM.ToString();
        public string Key => KEY;

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
            InputSystem.onEvent += OnEvent;
            InputSystem.onAfterUpdate += OnAfterUpdate;
            Store(START_OF_DEVICES);
            List<InputDevice> devices = InputSystem.devices.ToList();
            devices.ForEach(StoreDevice);
            Store(END_OF_DEVICES);
        }

        public void StopRecording(RecordingEventArgs args)
        {
            InputSystem.onEvent -= OnEvent;
            InputSystem.onAfterUpdate -= OnAfterUpdate;
        }

        public void Update(RecordingEventArgs args)
        {
        }

        private static T Store<T>(T value)
        {
            return ValueRecorder.Store<T>(value, KEY);
        }

        private static void StoreDevice(InputDevice d)
        {
            Store(d.deviceId);
            Store(d.layout);
        }

        private void OnAfterUpdate()
        {
            if (RecordingController.IsRecording)
            {
                // Mark end of frame.
                Store(FRAME);
            }
        }

        private void OnEvent(InputEventPtr inputEvent, InputDevice device)
        {
            RecordEvent(device, inputEvent);
        }

        private void RecordEvent(InputDevice device, InputEventPtr inputEvent)
        {
            byte[] bytes = EventSerializationUtility.GetBytes(inputEvent);
            Store(bytes);
            Store(device.deviceId);
        }
    }
}