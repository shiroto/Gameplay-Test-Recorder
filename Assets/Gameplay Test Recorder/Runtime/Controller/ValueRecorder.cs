using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Scripting;

namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// !!!DO NOT RENAME!!!
    /// This class is called when patched code wants to store or retrieve values for recording and replaying respectively.
    /// This class is very important and very delicate, tread carefully.
    /// This class and its public methods are referenced by simple strings -> do not change any names or everything will fall apart quicker than that relationship you had in high school!
    /// !!!DO NOT RENAME!!!
    /// </summary>
    [Preserve]
    public class ValueRecorder
    {
        private static IRecordConfigRO config;
        private static Recording currentRecording;
        private static int frameCount;
        private static Dictionary<string, ValueRecorder> recorders = new Dictionary<string, ValueRecorder>();
        private string name;
        private RecordQueue record;

        private ValueRecorder(string name) : this(name, new RecordQueue())
        {
        }

        private ValueRecorder(string name, RecordQueue q)
        {
            this.name = name;
            record = q;
        }

        public static IRecordConfigRO Config => config;
        private bool IsEmpty => record.IsEmpty;
        public RecordQueue Record => record;

        public static int GetFrameCount()
        {
            return frameCount;
        }

        public static IReadOnlyDictionary<string, ValueRecorder> GetRecorders()
        {
            return recorders;
        }

        public static void InitRecording(IRecordConfigRO c)
        {
            Assert.IsNotNull(c);
            Reset();
            config = c;
            ApplyConfig();
            RecordingCallbackController.OnLateUpdate += IncrementFrameCount;
            RecordingCallbackController.OnStopRecording += OnStopRecording;
        }

        public static void InitReplay(Recording recording)
        {
            Assert.IsNotNull(recording.recordKeys);
            Assert.IsNotNull(recording.Records);
            Assert.AreEqual(recording.recordKeys.Length, recording.Records.Length);
            Reset();
            currentRecording = recording;
            config = recording.config;
            ApplyConfig();
            ReplayCallbackController.OnLateUpdate += OnLateUpdate;
            ReplayCallbackController.OnStopReplaying += OnStopReplay;
            for (int i = 0; i < recording.recordKeys.Length; i++)
            {
                string name = recording.recordKeys[i];
                recorders[name] = new ValueRecorder(name, (RecordQueue)recording.Records[i].Clone());
            }
        }

        /// <summary>
        /// !!!DO NOT RENAME!!!
        /// Retrieve values for replaying.
        /// </summary>
        public static T NextInput<T>(string key)
        {
            if (recorders.TryGetValue(key, out ValueRecorder recorder))
            {
                T input = recorder._NextInput<T>();
                //Debug.Log($"`{key}`->`{input}`");
                return input;
            }
            else
            {
                //Debug.Log($"`{key}`->`{default}`");
                return default;
            }
        }

        public static void OnStopRecording(object sender, RecordingEventArgs args)
        {
            RecordingCallbackController.OnLateUpdate -= IncrementFrameCount;
        }

        public static void OnStopReplay(object sender, ReplayEventArgs args)
        {
            ReplayCallbackController.OnLateUpdate -= OnLateUpdate;
        }

        public static void Reset()
        {
            currentRecording = null;
            recorders.Clear();
            frameCount = 0;
            config = null;
        }

        /// <summary>
        /// !!!DO NOT RENAME!!!
        /// Store values for recording.
        /// </summary>
        public static T Store<T>(T value, string key)
        {
            //Debug.Log($"`{key}` recording value `{value}`.");
            ValueRecorder recorder;
            if (!recorders.TryGetValue(key, out recorder))
            {
                recorder = new ValueRecorder(key);
                recorders[key] = recorder;
            }
            var ret = recorder._Store(value);
            return ret;
        }

        private static void ApplyConfig()
        {
            Application.targetFrameRate = config.Framerate;
        }

        private static T GetDefaultValue<T>()
        {
            if (typeof(T) == typeof(string))
            {
                return (T)(object)"";
            }
            else
            {
                return default;
            }
        }

        private static void IncrementFrameCount(object sender, RecordingEventArgs args)
        {
            frameCount++;
        }

        private static void OnLateUpdate(object sender, ReplayEventArgs args)
        {
            frameCount++;
            if (frameCount >= currentRecording.frameCount)
            {
                RecordingController.StopReplaying();
            }
        }

        private T _NextInput<T>()
        {
            object value = null;
            try
            {
                if (!record.IsEmpty)
                {
                    value = record.Dequeue().Get;
                    return (T)value;
                }
                else
                {
                    //Debug.Log($"End of record! `{name}`");
                    return GetDefaultValue<T>();
                }
            }
            catch (InvalidCastException)
            {
                Debug.LogError($"Could not cast `{value.GetType()}` to `{typeof(T)}`.");
                return GetDefaultValue<T>();
            }
        }

        private T _Store<T>(T value)
        {
            record.Enqueue(RecordFactory.CreateRecord(value));
            return value;
        }
    }
}