using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public TextMeshProUGUI WaveOverUI;
    public TextMeshProUGUI cooldownCounterUI;
    public TextMeshProUGUI currentWaveUI;
    // Start is called before the first frame update
    void Start()
    {
        currentEnemyPerWave = initialEnemyPerWave;

        StartNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        List<Enemy> enemyToRemove = new List<Enemy>();
        foreach (Enemy enemy in currentEnemyAlive)
        {
            if (enemy.isDead)
            {
                enemyToRemove.Add(enemy);
            }
        }
        foreach (Enemy enemy in currentEnemyAlive)
        {
            if (enemy.isDead)
            {
                currentEnemyAlive.Remove(enemy);
            }
        }
        enemyToRemove.Clear();
        if (currentEnemyAlive.Count ==0 && inCooldown == false)
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

        cooldownCounterUI.text = cooldownCounter.ToString("F0");
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        WaveOverUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;

        WaveOverUI.gameObject.SetActive(false);
        //currentEnemyPerWave += 1;
        StartNextWave();
    }
    private void StartNextWave()
    {
        currentEnemyAlive.Clear();
        currentWave++;
        currentWaveUI.text = "Wave: " + currentWave.ToString();
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
