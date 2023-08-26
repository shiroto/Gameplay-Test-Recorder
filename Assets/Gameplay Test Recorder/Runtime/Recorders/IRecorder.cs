namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// Interface that allows to record miscellanious pieces of data, like mouse positions or keyboard presses.
    /// </summary>
    public interface IRecorder
    {
        /// <summary>
        /// Key used to store values inside <see cref="ValueRecorder"/>.
        /// </summary>
        string Key
        {
            get;
        }

        /// <summary>
        /// Same as MonoBehaviour.FixedUpdate.
        /// </summary>
        void FixedUpdate(RecordingEventArgs args);

        /// <summary>
        /// Called when recording is initialized.
        /// </summary>
        void Init(RecordingEventArgs args);

        /// <summary>
        /// Same as MonoBehaviour.LateUpdate.
        /// </summary>
        void LateUpdate(RecordingEventArgs args);

        /// <summary>
        /// Called when recording starts.
        /// </summary>
        void StartRecording(RecordingEventArgs args);

        /// <summary>
        /// Called when recording ends.
        /// </summary>
        void StopRecording(RecordingEventArgs args);

        /// <summary>
        /// Same as MonoBehaviour.Update.
        /// </summary>
        void Update(RecordingEventArgs args);
    }
}