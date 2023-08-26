using UnityEngine;

namespace TwoGuyGames.GameplayInspector.Examples
{
    public class FrameCounter : MonoBehaviour
    {
        public Rect position;
        private float averageFps;
        private int averageOverFrameCount = 40;
        private float currentFps;
        private int currentFrame;
        private float[] lastDeltas;

        private void OnGUI()
        {
            GUI.TextArea(position, $"Current FPS: {currentFps}\nAverage FPS: {averageFps}");
        }

        private void Start()
        {
            lastDeltas = new float[averageOverFrameCount];
        }

        private void Update()
        {
            currentFps = 1 / Time.deltaTime;
            lastDeltas[currentFrame] = currentFps;
            currentFrame++;
            if (currentFrame == averageOverFrameCount)
            {
                currentFrame = 0;
                averageFps = 0;
                foreach (float f in lastDeltas)
                {
                    averageFps += f;
                }
                averageFps /= averageOverFrameCount;
            }
        }
    }
}