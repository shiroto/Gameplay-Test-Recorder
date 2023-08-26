using UnityEngine;
using UnityEngine.UI;

namespace TwoGuyGames.GTR.Samples
{
    public class Score : MonoBehaviour
    {
        private int scoreValue;

        [SerializeField]
        private Text scoreText;

        private void OnDisable()
        {
            EnemyController.OnShipDestroyed -= OnShipDestroyed;
        }

        private void OnEnable()
        {
            EnemyController.OnShipDestroyed += OnShipDestroyed;
        }

        private void OnShipDestroyed(EnemyController obj)
        {
            scoreValue++;
            scoreText.text = scoreValue.ToString("d4");
        }
    }
}
