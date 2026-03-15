using UnityEngine;


public class EnemySpawnerSword : MonoBehaviour
{
    public GameObject enemyPrefabFacingLeft;
    public GameObject enemyPrefabFacingRight;
    public int spawnYMin = -4;
    public int spawnYMax = 4;
    public float spawnXLeft = -20f;  // Left side spawn
    public float spawnXRight = 5f;
    public float spawnRate = 1f;
    public float spawnX;
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnRate);
  
    }

    void SpawnEnemy()
    {

        int left  = Random.Range(0,1);

        if (left > 0.5f)
        {
            
            spawnX = spawnXRight;
            int randomY = Random.Range(spawnYMin, spawnYMax);
            Vector3 spawnPos = new Vector3(spawnX, randomY, 0f);

            Instantiate(enemyPrefabFacingLeft, spawnPos, Quaternion.identity);
            Debug.Log("SpawnRight");
        }
        else
        {
            spawnX = spawnXLeft;
            int randomY = Random.Range(spawnYMin, spawnYMax);
            Vector3 spawnPos = new Vector3(spawnX, randomY, 0f);
            Instantiate(enemyPrefabFacingRight, spawnPos, Quaternion.identity);
            Debug.Log("SpawnLeft");
        }
    }
}