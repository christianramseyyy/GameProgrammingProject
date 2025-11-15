using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject alienPrefab;

    //public GameObject purpleCometPrefab;
    //public GameObject alienPrefab;
    //public GameObject ufoPrefab;

    public float spawnInterval = 1.5f;
    public float minSpawnInterval = 0.4f;
    public float maxSpawnRate = 8f;
    public float difficultyIncreaseRate = 0.05f;
    public float difficultyTimer;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }

        if (difficultyTimer > 5)
        {
            increaseDifficulty();
            difficultyTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        // bottom left
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // top right
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject alien = (GameObject)Instantiate(alienPrefab);
        alien.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }

    private void increaseDifficulty()
    {
        spawnInterval -= difficultyIncreaseRate;

        if(spawnInterval < minSpawnInterval)
        {
            spawnInterval = minSpawnInterval;
        }

        Debug.Log("Difficulty increased. New spawn interval: " + spawnInterval);
    }
}
