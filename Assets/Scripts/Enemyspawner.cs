using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject swarmerPrefab;
    [SerializeField] private GameObject bigSwarmerPrefab;
    [SerializeField] private float swarmerInterval = 3.5f;
    [SerializeField] private float bigSwarmerInterval = 10f;

    void Start()
    {
        StartCoroutine(SpawnEnemy(swarmerInterval, swarmerPrefab));
        StartCoroutine(SpawnEnemy(bigSwarmerInterval, bigSwarmerPrefab));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f),Random.Range(3, 10f),0f);

            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
    }
}
