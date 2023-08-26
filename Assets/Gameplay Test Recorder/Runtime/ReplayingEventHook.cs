using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    public class ReplayingEventHook : MonoBehaviour, IReplayEventHook
    {
        public event EventHandler<ReplayEventArgs> OnFixedUpdate = delegate { };

        public event EventHandler<ReplayEventArgs> OnLateUpdate = delegate { };

        public event EventHandler<ReplayEventArgs> OnReplayStopped = delegate { };

        public event EventHandler<ReplayEventArgs> OnUpdate = delegate { };

        public IRecordingRO Recording
        {
            get;
            set;
        }

        private void FixedUpdate()
        {
            OnFixedUpdate(this, new ReplayEventArgs(Recording));
        }

        private void LateUpdate()
        {
            OnLateUpdate(this, new ReplayEventArgs(Recording));
        }

        private void OnDestroy()
        {
            OnReplayStopped(this, new ReplayEventArgs(Recording));
        }

        private void OnEnable()
        {
            GameObject.DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            OnUpdate(this, new ReplayEventArgs(Recording));
        }
    }
}