using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject purpleCometPrefab;
    public GameObject alienPrefab;
    public GameObject ufoPrefab;

    public float spawnInterval = 1.5f;
    public float startDelay = 2f;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        int type = Random.Range(0, 3);
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
