using UnityEngine;
using UnityEditor;
using System.IO;

namespace TwoGuyGames
{
    public static class ScriptableObjectUtility
    {
        public static T CreateAssetAtPath<T>(string path, string name) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
            if (!name.EndsWith(".asset"))
            {
                name += ".asset";
            }
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + $"/{name}");
            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            return asset;
        }

        public static T CreateAssetAtPath<T>(string path) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
            return CreateAssetAtPath<T>(path, typeof(T).ToString());
        }

        /// <summary>
        //	This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        public static void CreateAssetAtSelection<T>() where T : ScriptableObject
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }
            T asset = CreateAssetAtPath<T>(path);
            Selection.activeObject = asset;
        }
    }
}