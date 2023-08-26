using System.Collections;
using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class SystemRandomEnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemyPrefab;

        [SerializeField]
        private float maxSpawnWaitTime;

        private System.Random random;

        [SerializeField]
        private float spawnRate;

        private float spawnWait;

        [SerializeField]
        private float y;

        private void Awake()
        {
            random = new System.Random();
        }

        private IEnumerator DoSpawn()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(spawnWait);
                spawnWait = (float)random.NextDouble() * maxSpawnWaitTime;
                float yPos = (float)(random.NextDouble() * y * 2) - y;
                GameObject.Instantiate(enemyPrefab, new Vector3(transform.position.x, yPos, 0), Quaternion.identity);
            }
        }

        private void OnDisable()
        {
            GameOver.OnGameOver -= OnGameOver;
        }

        private void OnDrawGizmosSelected()
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(transform.position.x, -y), new Vector3(transform.position.x, y));
            Gizmos.color = oldColor;
        }

        private void OnEnable()
        {
            GameOver.OnGameOver += OnGameOver;
            StartCoroutine(DoSpawn());
        }

        private void OnGameOver()
        {
            gameObject.SetActive(false);
        }
    }
}