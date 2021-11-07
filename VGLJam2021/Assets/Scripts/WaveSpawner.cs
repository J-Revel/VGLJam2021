using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    public Transform[] enemies;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float spawnDelay = 5;
    public float spawnTime = 10;
    public Transform spawnParent;

    void Start()
    {
        
    }

    void Update()
    {
        spawnTime -= Time.deltaTime;
        if(spawnTime < 0)
        {
            spawnTime += spawnDelay;
            int waveIndex = Random.Range(0, waves.Length);
            List<Transform> availableSpawnPoints = new List<Transform>();
            for(int i=0; i<spawnPoints.Length; i++)
            {
                availableSpawnPoints.Add(spawnPoints[i]);
            }
            for(int i=0; i<waves[waveIndex].enemies.Length; i++)
            {
                Transform[] enemies = waves[waveIndex].enemies;
                int spawnPointIndex = Random.Range(0, availableSpawnPoints.Count);
                Transform spawnPoint = availableSpawnPoints[spawnPointIndex];
                availableSpawnPoints.RemoveAt(spawnPointIndex);
                Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint.position, spawnPoint.rotation, spawnParent);
            }
        }
    }
}
