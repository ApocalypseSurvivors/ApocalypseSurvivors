using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public int initialEnemyPerWave = 5;
    public int currentEnemyPerWave;

    public float spawnDelay = 0.5f;

    public int currentWave = 0;
    public float waveCooldown = 10.0f;

    public bool inCooldown;
    public float cooldownCounter = 0;

    public List<Enemy> currentEnemyAlive;

    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        currentEnemyPerWave = initialEnemyPerWave;

        StartNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartNextWave()
    {
        currentEnemyAlive.Clear();
        currentWave++;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i =0; i < currentEnemyPerWave; i++)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            Enemy enemyScript = enemy.GetComponent<Enemy>();

            currentEnemyAlive.Add(enemyScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
