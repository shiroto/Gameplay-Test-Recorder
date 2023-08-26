using System;
using System.Collections.Generic;
using System.Linq;
using TwoGuyGames.GTR.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace TwoGuyGames.GTR.InputSystemRecorder
{
    public class InputSystemReplayer : IReplayer
    {
        private const string START_OF_DEVICES = "START OF DEVICES";
        private static readonly string KEY = RecordedSystems.UNITY_INPUT_SYSTEM.ToString();
        private List<InputDevice> removedDevices = new List<InputDevice>();
        private Dictionary<int, InputDevice> virtualDevices = new Dictionary<int, InputDevice>();
        public string Key => KEY;

        public void FixedUpdate(ReplayEventArgs args)
        {
        }

        public void Init(ReplayEventArgs args)
        {
        }

        public void LateUpdate(ReplayEventArgs args)
        {
        }

        public void StartReplaying(ReplayEventArgs args)
        {
            InputSystem.onBeforeUpdate += OnBeforeUpdate;
            virtualDevices.Clear();
            removedDevices = InputSystem.devices.ToList();
            removedDevices.ForEach(device => InputSystem.RemoveDevice(device));
            AddVirtualDevices();
        }

        public void StopReplaying(ReplayEventArgs args)
        {
            Debug.Log("STOP");
            InputSystem.onBeforeUpdate -= OnBeforeUpdate;
            foreach (KeyValuePair<int, InputDevice> dev in virtualDevices)
            {
                InputSystem.RemoveDevice(dev.Value);
            }
            virtualDevices.Clear();
            removedDevices.ForEach(device => InputSystem.AddDevice(device));
            removedDevices.Clear();
        }

        public void Update(ReplayEventArgs args)
        {
        }

        private static bool IsReadingDevice(out int deviceId)
        {
            object data = NextInput<object>();
            if (data is int i)
            {
                deviceId = i;
                return true;
            }
            else
            {
                deviceId = -1;
                return false;
            }
        }

        private static bool IsValidData(object rec, out byte[] data)
        {
            // This will stop replaying either when reaching end of frame marker or list of records is empty.
            data = rec as byte[];
            return data != null && data.Length != 0;
        }

        private static T NextInput<T>()
        {
            return ValueRecorder.NextInput<T>(KEY);
        }

        private void AddVirtualDevices()
        {
            string s = NextInput<string>();
            if (s.Equals(START_OF_DEVICES))
            {
                while (IsReadingDevice(out int deviceId))
                {
                    string layout = NextInput<string>();
                    MakeSureToHaveDevice(deviceId, layout);
                }
            }
            else
            {
                throw new Exception("Expected to read devices, but was not found.");
            }
        }

        private void MakeSureToHaveDevice(int deviceId, string deviceLayout)
        {
            if (!virtualDevices.ContainsKey(deviceId))
            {
                InputDevice virtualDevice = InputSystem.AddDevice(deviceLayout);
                virtualDevices[deviceId] = virtualDevice;
            }
        }

        private void OnBeforeUpdate()
        {
            ReplayFrame();
        }

        private void QueueEvent(int deviceId, byte[] data)
        {
            if (virtualDevices.TryGetValue(deviceId, out InputDevice device))
            {
                InputEventPtr input = EventSerializationUtility.FromBytes(device, data);
                InputSystem.QueueEvent(input);
            }
            else
            {
                Debug.LogError($"Could not find device with id `{deviceId}`.");
            }
        }

        private void ReplayFrame()
        {
            bool reachedEndOfFrame = false;
            while (!reachedEndOfFrame)
            {
                object rec = NextInput<object>();
                if (IsValidData(rec, out byte[] data))
                {
                    QueueEvent(NextInput<int>(), data);
                }
                else
                {
                    reachedEndOfFrame = true;
                }
            }
        }
    }
}