using UnityEditor;
using TwoGuyGames.GTR.Core;

namespace TwoGuyGames.GTR.Editor
{
    [InitializeOnLoad]
    internal class Startup
    {
        static Startup()
        {
            // Safety trigger to reset the controller e.g. when Unity crashed.
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                RecordingController.UnsetMode();
            }
            UnpackWindow.OpenIfNeeded();
            GtrEditorManager.Init();
        }
    }
}