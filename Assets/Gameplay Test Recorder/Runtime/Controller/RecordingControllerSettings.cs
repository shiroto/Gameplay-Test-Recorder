using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class RecordingControllerSettings
    {
        public float lastResult;
        public RecordingControllerMode RecordingControllerMode;

        public ReplayFinishedMode ReplayFinishedMode;

        [SerializeField]
        private string activeAssetUID;

        [SerializeReference]
        private Recording activeRecording;

        public string ActiveAssetUID
        {
            get => activeAssetUID;
            set => activeAssetUID = value;
        }

        public Recording ActiveRecording
        {
            get => activeRecording;
            set => activeRecording = value;
        }

        /// <summary>
        /// Inputs will be recorded if the game is running.
        /// </summary>
        public void SetModeRecording()
        {
            RecordingControllerMode = RecordingControllerMode.RECORDING;
        }

        /// <summary>
        /// Inputs will be replayed if the game is running.
        /// </summary>
        public void SetModeReplaying()
        {
            RecordingControllerMode = RecordingControllerMode.REPLAYING;
        }

        /// <summary>
        /// No recording or replaying will occur.
        /// </summary>
        public void UnsetMode()
        {
            RecordingControllerMode = RecordingControllerMode.NONE;
        }
    }
}