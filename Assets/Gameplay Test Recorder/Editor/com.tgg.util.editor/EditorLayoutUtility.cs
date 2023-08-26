using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames
{
    public static class EditorLayoutUtility
    {
        private static IDictionary<string, Vector2> scrollPositions = new Dictionary<string, Vector2>();

        public static void DrawInHorizontal(Action guiMethod, params GUILayoutOption[] options)
        {
            DrawInHorizontal(guiMethod, null, options);
        }
        public static void DrawInHorizontal(Action guiMethod, GUIStyle style, params GUILayoutOption[] options)
        {
            DrawInHorizontal((Rect r) => { guiMethod(); }, style, options);
        }

        public static void DrawInHorizontal(Action<Rect> guiMethod, params GUILayoutOption[] options)
        {
            DrawInHorizontal(guiMethod, null, options);
        }
        public static void DrawInHorizontal(Action<Rect> guiMethod,GUIStyle style, params GUILayoutOption[] options)
        {
            Assert.IsNotNull(guiMethod);
            Rect rect;
            if(style != null)
                rect = EditorGUILayout.BeginHorizontal(style, options);    
            else
                rect = EditorGUILayout.BeginHorizontal(options);
            guiMethod(rect);
            EditorGUILayout.EndHorizontal();
        }
        public static void DrawInScrollView(Action guiMethod, params GUILayoutOption[] options)
        {
            DrawInScrollView(guiMethod, null, options);
        }

        public static void DrawInScrollView(Action guiMethod, GUIStyle style, params GUILayoutOption[] options)
        {
            Assert.IsNotNull(guiMethod);
            string key = guiMethod.Method.DeclaringType.FullName + guiMethod.Method.Name; 
            if (style == null)
                scrollPositions[key] = EditorGUILayout.BeginScrollView(GetOrCreateScrollPosition(key), options);
            else
                scrollPositions[key] = EditorGUILayout.BeginScrollView(GetOrCreateScrollPosition(key), style, options);
            guiMethod();
            EditorGUILayout.EndScrollView();
        }


        public static void DrawInVertical(Action guiMethod, params GUILayoutOption[] options)
        {
            DrawInVertical(guiMethod, null, options);
        }
        public static void DrawInVertical(Action guiMethod, GUIStyle style, params GUILayoutOption[] options)
        {
            DrawInVertical((Rect r) => { guiMethod(); }, style, options);
        }



        public static void DrawInVertical(Action<Rect> guiMethod, params GUILayoutOption[] options)
        {
            DrawInVertical(guiMethod, null, options);
        }
        public static void DrawInVertical(Action<Rect> guiMethod,GUIStyle style, params GUILayoutOption[] options)
        {
            Assert.IsNotNull(guiMethod);
            Rect rect;
            if (style == null)
                rect = EditorGUILayout.BeginVertical(options);
            else
                rect = EditorGUILayout.BeginVertical(style, options);
            guiMethod(rect);
            EditorGUILayout.EndVertical();
        }

        private static Vector2 GetOrCreateScrollPosition(string key)
        {
            if (scrollPositions.TryGetValue(key, out Vector2 position))
            {
                return position;
            }
            else
            {
                return Vector2.zero;
            }
        }

        public static void DrawSpace()
        {
            EditorGUILayout.Space();
        }
        public static void DrawSpace(float s)
        {
            EditorGUILayout.Space(s);
        }
    }
}
