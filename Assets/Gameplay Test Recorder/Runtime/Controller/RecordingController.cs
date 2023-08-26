using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace TwoGuyGames.GTR.Core
{
    [Preserve]
    public static class RecordingController
    {
        private const string INIT_ERROR = "Could not initialize Replay. Try finding input references again. If this problem persists after that, please contact support at `support@2guygames.com`.\n";
        private const string LOG_PATH = "/../Logs/2GuyGames/GTR/";
        private const string SETTINGS_KEY = "TwoGuyGames/InputRecording/RecordingController/settings";
        private static IInputPatcher patcher;
        private static RecordingControllerSettings settings;

        public static event Action OnReset = delegate { };

        public static event EventHandler OnPatched = delegate { };

        /// <summary>
        /// Do NOT rename for this is used by code patching.
        /// </summary>
        public static RecordingControllerMode ActiveMode
        {
            get
            {
                return Settings.RecordingControllerMode;
            }
            set
            {
                Settings.RecordingControllerMode = value;
                SaveSettings();
            }
        }

        public static bool IsInactive => ActiveMode == RecordingControllerMode.NONE;

        public static bool IsPatchApplied
        {
            get;
            private set;
        }

        public static bool IsProjectAnalyzed => GtrAssetsUtility.GetGlobalSettingsAsset().IsReady();
        public static bool IsRecording => ActiveMode == RecordingControllerMode.RECORDING;

        public static bool IsReplaying => ActiveMode == RecordingControllerMode.REPLAYING;

        public static float LastResult
        {
            get
            {
                return settings.lastResult;
            }
            set
            {
                settings.lastResult = value;
                SaveSettings();
            }
        }

        public static ReplayFinishedMode ReplayFinishedBehaviour
        {
            get
            {
                return Settings.ReplayFinishedMode;
            }
            set
            {
                Settings.ReplayFinishedMode = value;
            }
        }

        private static RecordingControllerSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    LoadSettingsIfNeeded();
                }
                return settings;
            }
        }

        /// <summary>
        /// Recompile to enable recording and replaying.
        /// Return true if initialization was successful.
        /// </summary>
        public static bool ApplyPatches()
        {
            if (IsPatchApplied)
            {
                return true;
            }
            try
            {
                InputRecordingGlobalSettingsAsset globalSettingsAsset = GtrAssetsUtility.GetGlobalSettingsAsset();
                RecordedSystems solutions = globalSettingsAsset.settings.GetInputSolutions();
                patcher = InputPatchFactory.CreateInstance(solutions);
                patcher.Patch(globalSettingsAsset.settings);
                IsPatchApplied = true;
                OnPatched(null, null);
                Debug.Log("RecordingController initialization finished");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError(INIT_ERROR);
                Debug.LogException(ex);
                Reset();
                return false;
            }
        }

        public static void Reset()
        {
            RecordingController.ActiveMode = RecordingControllerMode.NONE;
            if (IsPatchApplied)
            {
                ResetCode();
                IsPatchApplied = false;
            }
            OnReset();
        }

        /// <summary>
        /// No recording or replaying will occur.
        /// </summary>
        public static void UnsetMode()
        {
            ActiveMode = RecordingControllerMode.NONE;
        }

        public static void SetActiveAsset(RecordedTestAsset asset)
        {
#if UNITY_EDITOR
            if (UnityEditor.AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out string guid, out long localId))
            {
                Settings.ActiveAssetUID = guid;
            }
            else
            {
                Debug.LogError($"Could not find GUID for `{asset.name}`");
            }
#endif
        }

        #region RECORDING

        /// <summary>
        /// Inputs will be recorded if the game is running.
        /// </summary>
        public static void SetModeRecording()
        {
            ActiveMode = RecordingControllerMode.RECORDING;
        }

        /// <summary>
        /// Load scene, enter play mode, focus game view and start recording.
        /// </summary>
        public static void SetupAndStartRecording(Recording recordTo)
        {
            Assert.AreEqual(RecordingControllerMode.NONE, ActiveMode, $"Can't start recording from `{ActiveMode}`.");
            Assert.IsNotNull(recordTo);
            Settings.ActiveRecording = recordTo;
            SetModeRecording();
            SaveSettings();
            RecordingControllerEditorUtility.LoadScene(recordTo);
            RecordingControllerEditorUtility.FocusGameView();
            RecordingControllerEditorUtility.EnterPlayMode();
        }

        /// <summary>
        /// Just start recording. Doesn't trigger patching, scene loading or playmode change. All of these must have happened befhorehand.
        /// </summary>
        public static void StartRecording(Recording recordTo)
        {
            Settings.ActiveRecording = recordTo;
            SetModeRecording();
            SaveSettings();
            PrepareForRecording();
        }

        public static void StopRecording()
        {
            StopRecordingIfNeeded();
        }

        #endregion RECORDING

        #region REPLAYING

        /// <summary>
        /// Inputs will be replayed if the game is running.
        /// </summary>
        public static void SetModeReplaying()
        {
            ActiveMode = RecordingControllerMode.REPLAYING;
        }

        /// <summary>
        /// Load scene, enter playmode, focus game view and start replaying.
        /// </summary>
        /// <param name="replayFrom"></param>
        public static void SetupAndStartReplaying(Recording replayFrom)
        {
            Assert.AreEqual(RecordingControllerMode.NONE, ActiveMode, $"Can't start replaying from `{ActiveMode}`.");
            Assert.IsNotNull(replayFrom);
            Assert.IsTrue(replayFrom.IsValid());
            RecordingControllerEditorUtility.LoadScene(replayFrom);
            SetModeReplaying();
            Settings.ActiveRecording = replayFrom;
            SaveSettings();
            RecordingControllerEditorUtility.FocusGameView();
            RecordingControllerEditorUtility.EnterPlayMode();
        }

        /// <summary>
        /// Just start replaying. Doesn't care about patching, scene or playmode.
        /// </summary>
        public static void StartReplaying(Recording replayFrom)
        {
            SetModeReplaying();
            Settings.ActiveRecording = replayFrom;
            SaveSettings();
            PrepareForReplay();
        }

        public static void StopReplaying()
        {
            StopReplayingIfNeeded();
        }

        private static void CheckEndState()
        {
            try
            {
                IObjectStateCollection replayState = SceneStateFactory.CreateStateFromCurrentScene(DefaultUnstoredTypes.INSTANCE);
                IValueSpaceCollection recordState = Settings.ActiveRecording.endState;
                ComparisonLogger logger = new ComparisonLogger();
                float result = RecordStateComparer.INSTANCE.Compare(recordState, replayState, logger);
                logger.Log($"Total difference: {result}");
                string path = $"{Application.dataPath}{LOG_PATH}{settings.ActiveRecording.id} {DateTime.Now.ToString("yy_M_d-HH_m_s")} log.txt";
                logger.SaveTo(path);
                LastResult = result;
                ReplayCallbackController.ReplayEnded(new ReplayEndedEventArgs(settings.ActiveRecording, replayState, result));
                Debug.Log($"Replay result: {result}");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        #endregion REPLAYING

        #region STATIC TRIGGERS

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CallBeginRecordingOrReplaying()
        {
#if !UNITY_EDITOR
            Settings.ActiveRecording.config.Resolution = new Vector2Int(Screen.width, Screen.height);
#endif
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
#endif
                if (!IsInactive)
                {
                    if (IsRecording)
                    {
                        patcher.OnBeginRecording();
                        RecordingCallbackController.StartRecording(settings.ActiveRecording);
                    }
                    else if (IsReplaying)
                    {
                        patcher.OnBeginReplaying();
                        ReplayCallbackController.StartReplaying(settings.ActiveRecording);
                    }
                }
        }

        /// <summary>
        /// This initializes recording and replaying before the scene is loaded.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void PatchBeforeSceneLoad()
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
#endif
                if (!IsInactive)
                {
                    PrepareForRecording();
                    PrepareForReplay();
                }
        }

        /// <summary>
        /// This triggers patching when the domain is being reloaded, but only if we are set to Recording or Replaying.
        /// That means this triggers only when we enter the playmode without having applied patches beforehand.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void PatchOnReload()
        {
#if !UNITY_EDITOR
            SetupBuildRecording();
#endif
            ApplyPatchesIfNeeded();
        }

        #endregion STATIC TRIGGERS

        private static void SetupBuildRecording()
        {
            ActiveMode = RecordingControllerMode.RECORDING;
            Settings.ActiveRecording = new Recording("replay");
        }

        private static void ApplyPatchesIfNeeded()
        {
            if (!IsInactive && !IsPatchApplied)
            {
                ApplyPatches();
            }
        }

        private static void LoadSettingsIfNeeded()
        {
            string json = PlayerPrefs.GetString(SETTINGS_KEY);
            if (!string.IsNullOrEmpty(json))
            {
                settings = JsonUtility.FromJson<RecordingControllerSettings>(json);
            }
            else
            {
                settings = new RecordingControllerSettings();
            }
        }

        private static void PrepareForRecording()
        {
            if (IsRecording)
            {
                RecordingCallbackController.Init(Settings.ActiveRecording, EndRecordingAndSafe);
                ValueRecorder.InitRecording(Settings.ActiveRecording.config);
            }
        }

        private static void PrepareForReplay()
        {
            if (Settings.ActiveRecording == null)
            {
                Debug.LogError("No replay loaded!");
                return;
            }
            if (IsReplaying)
            {
                ReplayCallbackController.Init(Settings.ActiveRecording, EndReplay);
                ValueRecorder.InitReplay(Settings.ActiveRecording);
            }
        }

        private static void ResetCode()
        {
            InputRecordingGlobalSettingsAsset globalSettingsAsset = GtrAssetsUtility.GetGlobalSettingsAsset();
            RecordedSystems solutions = globalSettingsAsset.settings.GetInputSolutions();
            patcher?.Dispose();
            ActiveMode = RecordingControllerMode.NONE;
        }

        private static void SaveRecording()
        {
            if (Settings == null)
            {
                Debug.LogError("No settings found.");
            }
            if (Settings.ActiveRecording == null)
            {
                Debug.LogError("No active recording.");
            }
            Settings.ActiveRecording.frameCount = ValueRecorder.GetFrameCount();
            IReadOnlyDictionary<string, ValueRecorder> recorders = ValueRecorder.GetRecorders();
            Settings.ActiveRecording.recordKeys = recorders.Keys.ToArray();
            Settings.ActiveRecording.Records = recorders.Values.Select(r => r.Record).ToArray();
            SaveState();
#if !UNITY_EDITOR
            RecordingStorageHelper.SaveRecording(settings.ActiveRecording);
#else
            SaveRecordingToAsset(Settings.ActiveAssetUID, settings.ActiveRecording);
#endif
            ValueRecorder.Reset();
        }

        private static void SaveRecordingToAsset(string assetId, Recording recording)
        {
#if UNITY_EDITOR
            if (!string.IsNullOrEmpty(assetId))
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(assetId);
                RecordedTestAsset activeAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<RecordedTestAsset>(path);
                activeAsset.recording = recording;
            }
#endif
        }

        private static void SaveSettings()
        {
            string json = JsonUtility.ToJson(settings);
            PlayerPrefs.SetString(SETTINGS_KEY, json);
            PlayerPrefs.Save();
        }

        private static void SaveState()
        {
            IObjectStateCollection state = SceneStateFactory.CreateStateFromCurrentScene(DefaultUnstoredTypes.INSTANCE);
            settings.ActiveRecording.endState = StateToRange.ConvertToSpace(state);
        }

        private static void StopRecordingIfNeeded(IRecordEventHook eventHook = null)
        {
            if (IsRecording)
            {
                RecordingCallbackController.StopRecording(settings.ActiveRecording);
            }
        }

        private static void EndRecordingAndSafe()
        {
            patcher?.OnEndRecording();
            SaveRecording();
            UnsetMode();
        }

        private static void StopReplayingIfNeeded(IRecordEventHook eventHook = null)
        {
            if (IsReplaying)
            {
                ReplayCallbackController.StopReplaying(settings.ActiveRecording);
            }
        }

        private static void EndReplay()
        {
            patcher?.OnEndReplaying();
            CheckEndState();
            UnsetMode();
        }
    }
}