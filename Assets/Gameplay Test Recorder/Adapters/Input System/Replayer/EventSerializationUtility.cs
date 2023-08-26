using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace TwoGuyGames.GTR.InputSystemRecorder
{
    internal unsafe static class EventSerializationUtility
    {
        public static InputEventPtr FromBytes(InputDevice device, byte[] buffer)
        {
            using (StateEvent.From(device, out InputEventPtr eventPtr))
            {
                fixed (byte* bufferPtr = buffer)
                {
                    UnsafeUtility.MemCpy(eventPtr.data, bufferPtr, buffer.Length);
                }
                eventPtr.time = Time.time;
                eventPtr.deviceId = device.deviceId;
                return eventPtr;
            }
        }
    }
}