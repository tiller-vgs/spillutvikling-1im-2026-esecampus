using UnityEngine;

public class EnemySpawnerSword : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnYMin = -4f;
    public float spawnYMax = 4f;
    public float spawnX = -20f;  // Left side spawn
    public float spawnRate = 1f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnRate);
    }

    void SpawnEnemy()
    {
        float randomY = Random.Range(spawnYMin, spawnYMax);
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0f);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}