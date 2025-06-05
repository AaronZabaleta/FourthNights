using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public LayerMask groundLayer;
    public float spawnIntervalMin = 10f;
    public float spawnIntervalMax = 30f;
    public float spawnRadius = 50f;
    public int maxAttempts = 10;

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    System.Collections.IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPos.y = 100f;

            if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit, 200f, groundLayer))
            {
                Instantiate(enemyPrefab, hit.point, Quaternion.identity);
                break;
            }
        }
    }
}
