using UnityEditor;
using UnityEngine;

namespace TwoGuyGames.GTR.Editor
{
    internal static class Utility
    {
        public static void DrawVerticalLine()
        {
            GUIStyle line;
            line = new GUIStyle();
            line.normal.background = EditorGUIUtility.whiteTexture;
            line.margin = new RectOffset(0, 0, 4, 4);
            line.stretchHeight = true;
            line.fixedWidth = 1;
            GUI.color = Color.black;
            GUILayout.Box(GUIContent.none, line);
            GUI.color = Color.white;
        }

        public static void HorizontalLine()
        {
            GUIStyle line;
            line = new GUIStyle();
            line.normal.background = EditorGUIUtility.whiteTexture;
            line.margin = new RectOffset(0, 0, 4, 4);
            line.fixedHeight = 1;
            line.stretchWidth = true;
            GUI.color = Color.black;
            GUILayout.Box(GUIContent.none, line);
            GUI.color = Color.white;
        }
    }
}