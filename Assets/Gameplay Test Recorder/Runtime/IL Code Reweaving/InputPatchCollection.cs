namespace TwoGuyGames.GTR.Core
{
    internal class InputPatchCollection : IInputPatcher
    {
        private IInputPatcher[] subPatchers;

        public InputPatchCollection(params IInputPatcher[] reweavers)
        {
            this.subPatchers = reweavers;
        }

        public void Dispose()
        {
            foreach (IInputPatcher patch in subPatchers)
            {
                patch.Dispose();
            }
        }

        public void OnBeginRecording()
        {
            foreach (IInputPatcher patch in subPatchers)
            {
                patch.OnBeginRecording();
            }
        }

        public void OnBeginReplaying()
        {
            foreach (IInputPatcher patch in subPatchers)
            {
                patch.OnBeginReplaying();
            }
        }

        public void OnEndRecording()
        {
            foreach (IInputPatcher patch in subPatchers)
            {
                patch.OnEndRecording();
            }
        }

        public void OnEndReplaying()
        {
            foreach (IInputPatcher patch in subPatchers)
            {
                patch.OnEndReplaying();
            }
        }

        public void Patch(IInputPatchSettings_RO settings)
        {
            settings.MergeDuplicateTypes();
            foreach (IInputPatcher patch in subPatchers)
            {
                patch.Patch(settings);
            }
        }
    }
}