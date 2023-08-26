using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Editor
{
    [Serializable]
    internal class RecordingWindowContext
    {
        private IRecordingWindowState enteredState;
        private IRecordingWindowState exitedState;

        [SerializeReference]
        private IRecordingWindowState state;

        [SerializeField]
        private RecordingWindow window;

        public RecordingWindowContext(RecordingWindow window, IRecordingWindowState state)
        {
            Assert.IsNotNull(window);
            Assert.IsNotNull(state);
            this.window = window;
            State = state;
        }

        public IRecordingWindowState State
        {
            get => state;
            set
            {
                Assert.IsNotNull(value);
                exitedState = state;
                enteredState = value;
                state = value;
                window.Repaint();
            }
        }

        public void Update()
        {
            EnterStateIfNeeded();
            State.Update(this);
            ExitStateIfNeeded();
        }

        public void UpdateGUI()
        {
            State.UpdateGUI(this);
        }

        private void EnterStateIfNeeded()
        {
            if (enteredState != null)
            {
                enteredState.OnEnter(this);
                enteredState = null;
            }
        }

        private void ExitStateIfNeeded()
        {
            if (exitedState != null)
            {
                exitedState.OnExit(this);
                exitedState = null;
            }
        }
    }
}