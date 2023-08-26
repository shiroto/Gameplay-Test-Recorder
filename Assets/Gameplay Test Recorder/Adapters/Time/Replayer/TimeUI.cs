using UnityEngine;
using UnityEngine.UI;

namespace TwoGuyGames.GTR.TimeReplayer
{
    public class TimeUI : MonoBehaviour
    {
        public Text frame;
        public Text recordedTime;
        public Text replayTime;

        public void SetFrame(string s)
        {
            frame.text = s;
        }

        public void SetRecordedTime(string s)
        {
            recordedTime.text = s;
        }

        public void SetReplayTime(string s)
        {
            replayTime.text = s;
        }
    }
}