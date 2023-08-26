using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        public static event Action<EnemyController> OnShipDestroyed = delegate { };

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject.Destroy(gameObject);
            OnShipDestroyed(this);
        }

        private void OnDisable()
        {
            GameOver.OnGameOver -= OnGameOver;
        }

        private void OnEnable()
        {
            GameOver.OnGameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.simulated = false;
        }

        private void Start()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
        }
    }
}