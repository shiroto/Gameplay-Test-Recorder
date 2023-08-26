using System;

namespace TwoGuyGames.GTR.Core
{
    public interface IReplayEventHook
    {
        event EventHandler<ReplayEventArgs> OnFixedUpdate;

        event EventHandler<ReplayEventArgs> OnLateUpdate;

        event EventHandler<ReplayEventArgs> OnReplayStopped;

        event EventHandler<ReplayEventArgs> OnUpdate;
    }
}