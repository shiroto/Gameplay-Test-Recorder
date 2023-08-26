using System;
using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.TimeReplayer
{
    internal class TimeReplayer : IReplayer
    {
        private int frame;
        private TimeUI timeUI;
        public string Key => "Timekeeper";

        public void FixedUpdate(ReplayEventArgs args)
        {
        }

        public void Init(ReplayEventArgs args)
        {
        }

        public void LateUpdate(ReplayEventArgs args)
        {
            frame++;
        }

        public void StartReplaying(ReplayEventArgs args)
        {
            frame = 0;
            GameObject timePrefab = Resources.Load<GameObject>("Time Canvas");
            GameObject go = GameObject.Instantiate(timePrefab);
            go.hideFlags = HideFlags.HideInHierarchy;
            timeUI = go.GetComponent<TimeUI>();
        }

        public void StopReplaying(ReplayEventArgs args)
        {
        }

        public void Update(ReplayEventArgs args)
        {
            SetRecordedTime();
            SetReplayTime();
            timeUI.SetFrame(frame + "");
        }

        private void SetRecordedTime()
        {
            float time = ValueRecorder.NextInput<float>(Key) * 1000;
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(time);
            timeUI.SetRecordedTime(timeSpan.ToString("mm\\:ss\\:fff"));
        }

        private void SetReplayTime()
        {
            float time = Time.time * 1000;
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(time);
            timeUI.SetReplayTime(timeSpan.ToString("mm\\:ss\\:fff"));
        }
    }
}