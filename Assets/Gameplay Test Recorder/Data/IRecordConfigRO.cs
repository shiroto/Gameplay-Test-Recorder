using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    public interface IRecordConfigRO
    {
        int Framerate
        {
            get;
        }

        Vector2Int Resolution
        {
            get;
        }

        string SceneGUID
        {
            get;
        }
    }
}