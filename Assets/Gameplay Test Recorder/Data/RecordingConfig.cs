using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class RecordingConfig : IRecordConfigRO
    {
        [SerializeField]
        [Range(1, 120)]
        private int framerate;

        [SerializeField]
        private Vector2Int resolution;

        [SerializeField]
        private string sceneGuid;

        public RecordingConfig()
        {
            framerate = 30;
            resolution = new Vector2Int(600, 480);
            sceneGuid = "";
        }

        public int Framerate
        {
            get
            {
                return framerate;
            }
            set
            {
                Assert.IsTrue(value > 0, "Framerate must be greater than 0.");
                framerate = value;
            }
        }

        public Vector2Int Resolution
        {
            get
            {
                return resolution;
            }
            set
            {
                resolution = value;
            }
        }

        public string SceneGUID
        {
            get
            {
                return sceneGuid;
            }
            set
            {
                sceneGuid = value;
            }
        }
    }
}