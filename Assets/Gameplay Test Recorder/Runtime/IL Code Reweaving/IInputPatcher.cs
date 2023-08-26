using System;

namespace TwoGuyGames.GTR.Core
{
    public interface IInputPatcher : IDisposable
    {
        void OnBeginRecording();

        void OnBeginReplaying();

        void OnEndRecording();

        void OnEndReplaying();

        void Patch(IInputPatchSettings_RO settings);
    }
}