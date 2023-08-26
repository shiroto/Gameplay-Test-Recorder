using UnityEditor;
using UnityEngine;

namespace TwoGuyGames.GTR.Editor
{
    internal static class ObjectFieldExtension
    {
        /// <summary>
        /// Return true if the asset changed.
        /// </summary>
        public static bool ObjectField<T>(ref T asset, bool allowSceneObjects) where T : UnityEngine.Object
        {
            return ObjectField<T>(null, ref asset, allowSceneObjects);
        }

        public static bool ObjectField<T>(GUIContent label, ref T asset, bool allowSceneObjects) where T : UnityEngine.Object
        {
            T old = asset;
            if (label == null)
            {
                asset = (T)EditorGUILayout.ObjectField(asset, typeof(T), allowSceneObjects);
            }
            else
            {
                asset = (T)EditorGUILayout.ObjectField(label, asset, typeof(T), allowSceneObjects);
            }
            if (old == null && asset == null)
            {
                return false;
            }
            else if (old == null ^ asset == null)
            {
                return true;
            }
            else
            {
                return !old.Equals(asset);
            }
        }
    }
}