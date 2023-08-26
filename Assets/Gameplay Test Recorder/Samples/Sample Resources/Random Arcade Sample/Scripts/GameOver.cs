using System;
using UnityEngine;
using UnityEngine.Events;

namespace TwoGuyGames.GTR.Samples
{
    public class GameOver : MonoBehaviour
    {
        public UnityEvent onGameOver;

        public static event Action OnGameOver = delegate { };

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("GAME OVER");
            OnGameOver();
            onGameOver.Invoke();
        }
    }
}