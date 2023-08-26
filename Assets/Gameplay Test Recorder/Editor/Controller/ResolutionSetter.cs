using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using TwoGuyGames.GTR.Core;

namespace TwoGuyGames.GTR.Editor
{
    internal static class ResolutionSetter
    {
        private const string HARMONY_KEY = "ResolutionSetter";
        private static Harmony harmony;
        private static Vector2Int resolution;

        public static void Reset()
        {
            if (harmony != null)
            {
                harmony.UnpatchAll(HARMONY_KEY);
                harmony = null;
            }
        }

        public static void SetResolution(IRecordConfigRO config)
        {
            if (!ArgHelper.IsBatchmode())
            {
                SetGameViewSize(config);
            }
            else
            {
                InitHarmony();
                resolution = new Vector2Int(config.Resolution.x, config.Resolution.y);
                AddCanvasResizeToEventSystemUpdate();
                AddScreenSizeMock();
            }
        }

        /// <summary>
        /// This might seem redundant because we also have CanvasResolutionUtility. But we need both...for some reason.
        /// </summary>
        private static void AddCanvasResizeToEventSystemUpdate()
        {
            Type type = typeof(EventSystem);
            var update = type.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            var prefix = typeof(ResolutionSetter).GetMethod("Prefix", BindingFlags.NonPublic | BindingFlags.Static);
            harmony.CreateProcessor(update).AddPrefix(prefix).Patch();
        }

        private static void AddScreenSizeMock()
        {
            Type screenType = typeof(Screen);
            MethodInfo width = screenType.GetProperty("width").GetGetMethod();
            MethodInfo widthTranspiler = typeof(ResolutionSetter).GetMethod("WidthTranspiler", BindingFlags.NonPublic | BindingFlags.Static);
            harmony.CreateProcessor(width).AddTranspiler(widthTranspiler).Patch();
            MethodInfo height = screenType.GetProperty("height").GetGetMethod();
            MethodInfo heightTranspiler = typeof(ResolutionSetter).GetMethod("HeightTranspiler", BindingFlags.NonPublic | BindingFlags.Static);
            harmony.CreateProcessor(height).AddTranspiler(heightTranspiler).Patch();
        }

        private static EditorWindow GetMainGameView()
        {
            Assembly assembly = typeof(EditorWindow).Assembly;
            Type type = assembly.GetType("UnityEditor.GameView");
            return EditorWindow.GetWindow(type);
        }

        private static IEnumerable<CodeInstruction> HeightTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator, MethodBase method)
        {
            return new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldc_I4, resolution.y),
                new CodeInstruction(OpCodes.Ret),
            };
        }

        private static void InitHarmony()
        {
            if (harmony != null)
            {
                Reset();
            }
            harmony = new Harmony(HARMONY_KEY);
        }

        private static void Prefix()
        {
            IEnumerable<Canvas> canvases = GameObject.FindObjectsOfType<Canvas>(true).Where(c => c.renderMode == RenderMode.ScreenSpaceOverlay);
            foreach (Canvas canvas in canvases)
            {
                RectTransform t = (RectTransform)canvas.transform;
                t.sizeDelta = resolution;
            }
        }

        private static void SetGameViewSize(IRecordConfigRO config)
        {
            EditorWindow gameView = GetMainGameView();
            Type gameViewType = gameView.GetType();

            MethodInfo m = gameViewType.GetMethod("SizeSelectionCallback", BindingFlags.Public | BindingFlags.Instance);
            m.Invoke(gameView, new object[] { 0, null });

            Rect pos = gameView.position;
            resolution = new Vector2Int(config.Resolution.x, config.Resolution.y + 21); // add menu bar
            gameView.position = new Rect(pos.x, pos.y, resolution.x, resolution.y);
            gameView.position = new Rect(pos.x, pos.y, resolution.x, resolution.y); // do it twice in case it needs to undock
        }

        private static IEnumerable<CodeInstruction> WidthTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator, MethodBase method)
        {
            return new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldc_I4, resolution.x),
                new CodeInstruction(OpCodes.Ret),
            };
        }
    }
}