using System;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    public class ReplayEventArgs : EventArgs
    {
        public ReplayEventArgs(IRecordingRO recording)
        {
            Assert.IsNotNull(recording);
            Recording = recording;
        }

        public IRecordingRO Recording
        {
            get;
            protected set;
        }
    }
}