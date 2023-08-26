using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class TopDown_SystemEnemyRandomSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemyPrefab;

        [SerializeField]
        private float maxSpawnWaitTime;

        private System.Random random;

        [SerializeField]
        private float spawnRate;

        private float spawnWait;

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
                Vector3 origin = transform.position;
                Vector3 range = transform.localScale / 2f;
                Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), Random.Range(-range.z, range.z));
                Vector3 randomPoint = origin + randomRange;
                GameObject.Instantiate(enemyPrefab, randomPoint, Quaternion.identity);
            }
        }

        private void OnDisable()
        {
            TopDownCharacterControllerBase.OnGameOver -= OnGameOver;
        }

        private void OnDrawGizmosSelected()
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, transform.localScale);
            Gizmos.color = oldColor;
        }

        private void OnEnable()
        {
            TopDownCharacterControllerBase.OnGameOver += OnGameOver;
            StartCoroutine(DoSpawn());
        }

        private void OnGameOver()
        {
            gameObject.SetActive(false);
        }
    }
}