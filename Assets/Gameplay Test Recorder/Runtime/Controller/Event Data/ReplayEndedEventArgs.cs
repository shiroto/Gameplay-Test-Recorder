using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    public class ReplayEndedEventArgs : ReplayEventArgs
    {
        public ReplayEndedEventArgs(IRecordingRO recording, IObjectStateCollection replayState, float result) : base(recording)
        {
            Assert.IsNotNull(replayState);
            ReplayState = replayState;
            Result = result;
        }

        public IObjectStateCollection ReplayState
        {
            get;
            protected set;
        }

        public float Result
        {
            get;
            protected set;
        }
    }
}