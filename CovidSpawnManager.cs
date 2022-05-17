using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovidSpawnManager : MonoBehaviour
{
    [Header("Managers")]
    public GameManager gameManager;

    private GameObject spawnLocation;
    private int newSpawn;
    private int badSpawns;

    public IEnumerator SpawnTarget()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(gameManager.spawnRate);
            GenerateSpawnPoint();
            SpawnEnemy();
            badSpawns++;

            if(badSpawns > 8)
            {
                SpawnPowerup();
                badSpawns = 0;
            }
        }
    }

    public void GenerateSpawnPoint()
    {
        newSpawn = Random.Range(0, gameManager.spawns.Length);
        spawnLocation = gameManager.spawns[newSpawn];
    }

    public void SpawnPowerup()
    {
        int index = Random.Range(0, gameManager.goodObjects.Count);
        Instantiate(gameManager.goodObjects[index], spawnLocation.transform.position,
            spawnLocation.transform.rotation);
    }

    public void SpawnEnemy()
    {
        int index = Random.Range(0, gameManager.badObjects.Count);
        Instantiate(gameManager.badObjects[index], spawnLocation.transform.position,
            spawnLocation.transform.rotation);
    }

}
