using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    public class RecordingEventHook : MonoBehaviour, IRecordEventHook
    {
        private bool recordingFinished;

        public event EventHandler<RecordingEventArgs> OnFixedUpdate = delegate { };

        public event EventHandler<RecordingEventArgs> OnLateUpdate = delegate { };

        public event EventHandler<RecordingEventArgs> OnRecordingStopped = delegate { };

        public event EventHandler<RecordingEventArgs> OnUpdate = delegate { };

        public IRecordingRO Recording
        {
            get;
            set;
        }

        private void FixedUpdate()
        {
            OnFixedUpdate(this, new RecordingEventArgs(Recording));
        }

        private void LateUpdate()
        {
            OnLateUpdate(this, new RecordingEventArgs(Recording));
        }

        private void OnDestroy()
        {
            OnRecordingStopped(this, new RecordingEventArgs(Recording));
        }

        private void OnEnable()
        {
            GameObject.DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            OnUpdate(this, new RecordingEventArgs(Recording));
        }
    }
}