using UnityEditor;

namespace TwoGuyGames.GTR.Editor
{
    internal class UnityUtility
    {
        public static void ForceRecompile()
        {
            AssetDatabase.StartAssetEditing();
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
            for (int i = 0; i < allAssetPaths.Length; i += 1)
            {
                MonoScript script = AssetDatabase.LoadAssetAtPath(allAssetPaths[i], typeof(MonoScript)) as MonoScript;
                if (script != null)
                {
                    AssetDatabase.ImportAsset(allAssetPaths[i]);
                    break;
                }
            }
            AssetDatabase.StopAssetEditing();
        }
    }
}