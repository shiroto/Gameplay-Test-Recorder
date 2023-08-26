namespace TwoGuyGames.GTR.Editor
{
    internal interface IRecordingWindowState
    {
        void OnEnter(RecordingWindowContext context);

        void OnExit(RecordingWindowContext context);

        void Update(RecordingWindowContext context);

        void UpdateGUI(RecordingWindowContext context);
    }
}