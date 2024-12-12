using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner: MonoBehaviour
{
    public int initialEnemyPerWave = 2;
    public int currentEnemyPerWave;

    public float spawnDelay = 0.5f;

    public int currentWave = 0;
    public float waveCooldown = 10.0f;

    public bool inCooldown;
    public float cooldownCounter = 0;

    public List<Enemy> currentEnemyAlive;

    public GameObject[] enemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemyPerWave = initialEnemyPerWave;

        StartNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        // List<Enemy> newEnemy = new List<Enemy>();
        // foreach (Enemy enemy in currentEnemyAlive)
        // {
        //     if (enemy.isDead)
        //     {
        //         enemyToRemove.Add(enemy);
        //     }
        // }
        foreach (Enemy enemy in currentEnemyAlive)
        {
            if (enemy.isDead)
            {
                currentEnemyAlive.Remove(enemy);
            }
        }
        // enemyToRemove.Clear();
        if (currentEnemyAlive.Count < currentEnemyPerWave && inCooldown == false)
        {
            StartCoroutine(WaveCooldown());
        }
        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            cooldownCounter = waveCooldown;
        }

    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;

        //currentEnemyPerWave += 1;
        StartNextWave();
    }
    private void StartNextWave()
    {
        // currentEnemyAlive.Clear();
        currentWave++;
        StartCoroutine(SpawnWave());
        currentEnemyPerWave += currentWave / 4;
    }

    private GameObject selectEnemy() {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }

    private IEnumerator SpawnWave()
    {
        int currentCount = currentEnemyAlive.Count;
        for (int i = 0 ; i < currentEnemyPerWave - currentCount; i++)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 10f), Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            var enemy = Instantiate(selectEnemy(), spawnPosition, Quaternion.identity);

            Enemy enemyScript = enemy.GetComponent<Enemy>();

            currentEnemyAlive.Add(enemyScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
