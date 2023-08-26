using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TwoGuyGames.GTR.Samples
{
    public class TopDown_Score : MonoBehaviour
    {
        private int scoreValue;

        [SerializeField]
        private Text scoreText;

        [SerializeField]
        private GameObject gameOverScreen;

        private void OnDisable()
        {
            TopDown_Enemy.OnEnemyDestroyed -= OnEnemyDestroyed;
            TopDownCharacterControllerBase.OnGameOver -= OnGameOver;
        }

        private void OnEnable()
        {
            TopDown_Enemy.OnEnemyDestroyed += OnEnemyDestroyed;
            TopDownCharacterControllerBase.OnGameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            gameOverScreen.SetActive(true);
        }

        private void OnEnemyDestroyed(TopDown_Enemy obj)
        {
            scoreValue++;
            scoreText.text = scoreValue.ToString("d4");
        }
    }
}