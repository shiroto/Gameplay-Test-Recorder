using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.LowLevel;

namespace TwoGuyGames.GTR.InputSystemRecorder
{
    internal unsafe static class EventSerializationUtility
    {
        public static byte[] GetBytes(InputEventPtr eventPtr)
        {
            uint sizeInBytes = eventPtr.sizeInBytes;
            byte[] buffer = new byte[sizeInBytes];
            fixed (byte* bufferPtr = buffer)
            {
                UnsafeUtility.MemCpy(bufferPtr, eventPtr.data, sizeInBytes);
            }
            return buffer;
        }
    }
}