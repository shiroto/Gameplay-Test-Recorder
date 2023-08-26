using TwoGuyGames.GTR.Core;
using UnityEditor;

namespace TwoGuyGames.GTR.Editor
{
    internal interface IRecordConfigEditor : IRecordConfigRO
    {
        SceneAsset Scene
        {
            get;
        }
    }
}