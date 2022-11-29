using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Boss Fields
    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;

    // Enemy Fields
    public GameObject[] enemyPrefabs;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;

    // Powerup Fields
    public GameObject[] powerupPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        SpawnPowerup();
        SpawnEnemyWave(1);

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            // spawn boss every x number of waves
            if(waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
                    
                // spawn 1 extra powerup for each boss wave
                SpawnPowerup();
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }

            // Spawn 1 Powerup at each Wave start!
            SpawnPowerup();         
        }

    }
    
    private void SpawnPowerup()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);

            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), enemyPrefabs[randomEnemy].transform.rotation);
            
        }
    }

    void SpawnBossWave(int currentRound)
    {
        int miniEnemiesToSpawn;

        // We don't want to divide by 0!
        if(bossRound != 0)
        {
            miniEnemiesToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemiesToSpawn = 1;
        }

        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemiesToSpawn;
    }

    public void SpawnMiniEnemies(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }
}
