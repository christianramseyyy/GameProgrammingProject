using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject purpleCometPrefab;
    public GameObject alienPrefab;
    public GameObject ufoPrefab;

    public float spawnInterval = 1.5f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        int type = Random.Range(0, 3); // 0 = Comet, 1 = Alien, 2 = UFO
        GameObject prefabToSpawn = null;

        switch (type)
        {
            case 0: prefabToSpawn = purpleCometPrefab; break;
            case 1: prefabToSpawn = alienPrefab; break;
            case 2: prefabToSpawn = ufoPrefab; break;
        }

        float x = Random.Range(-3f, 3f);
        Vector2 spawnPos = new Vector2(x, 6f);

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
