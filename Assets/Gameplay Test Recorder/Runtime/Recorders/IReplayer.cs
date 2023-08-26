namespace TwoGuyGames.GTR.Core
{
    public interface IReplayer
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
        void FixedUpdate(ReplayEventArgs args);

        /// <summary>
        /// Called when recording is initialized.
        /// </summary>
        void Init(ReplayEventArgs args);

        /// <summary>
        /// Same as MonoBehaviour.LateUpdate.
        /// </summary>
        void LateUpdate(ReplayEventArgs args);

        /// <summary>
        /// Called when recording starts.
        /// </summary>
        void StartReplaying(ReplayEventArgs args);

        /// <summary>
        /// Called when recording ends.
        /// </summary>
        void StopReplaying(ReplayEventArgs args);

        /// <summary>
        /// Same as MonoBehaviour.Update.
        /// </summary>
        void Update(ReplayEventArgs args);
    }
}