using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class TopDown_Enemy : MonoBehaviour
    {
        private GameObject player;

        [SerializeField]
        private float moveSpeed = 5;

        private bool playerAlive = false;

        public static event Action<TopDown_Enemy> OnEnemyDestroyed = delegate { };

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerAlive = true;
            }
        }

        private void Update()
        {
            if (playerAlive)
            {
                transform.LookAt(player.transform);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                GameObject.Destroy(gameObject);
                OnEnemyDestroyed(this);
            }
        }

        private void OnEnable()
        {
            TopDownCharacterControllerBase.OnGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            TopDownCharacterControllerBase.OnGameOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            playerAlive = false;
        }
    }
}