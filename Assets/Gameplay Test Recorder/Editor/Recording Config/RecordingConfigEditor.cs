using TwoGuyGames.GTR.Core;
using UnityEditor;
using UnityEngine;

namespace TwoGuyGames.GTR.Editor
{
    internal class RecordingConfigEditor : RecordingConfig, IRecordConfigEditor
    {
        [SerializeField]
        private SceneAsset scene;

        public SceneAsset Scene
        {
            get
            {
                return scene;
            }
            set
            {
                scene = value;
            }
        }
    }
}