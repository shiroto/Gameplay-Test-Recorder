using System;

namespace TwoGuyGames.GTR.Core
{
    public interface IRecordEventHook
    {
        event EventHandler<RecordingEventArgs> OnFixedUpdate;

        event EventHandler<RecordingEventArgs> OnLateUpdate;

        event EventHandler<RecordingEventArgs> OnRecordingStopped;

        event EventHandler<RecordingEventArgs> OnUpdate;
    }
}