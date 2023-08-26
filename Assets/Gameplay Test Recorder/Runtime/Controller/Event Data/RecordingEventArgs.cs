using System;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    public class RecordingEventArgs : EventArgs
    {
        public RecordingEventArgs(IRecordingRO recording)
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