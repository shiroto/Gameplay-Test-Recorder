using System;

namespace TwoGuyGames.GTR.Core
{
    [Flags]
    public enum RecordedSystems
    {
        NONE = 0,
        UNITY_INPUT_MANAGER = 1,
        UNITY_INPUT_SYSTEM = UNITY_INPUT_MANAGER << 1,
        REWIRED = UNITY_INPUT_SYSTEM << 1,
        UNITY_RANDOM = REWIRED << 1,
        SYSTEM_RANDOM = UNITY_RANDOM << 1,
    }
}