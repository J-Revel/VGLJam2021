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
    public Wave[] easyWaves;
    public Wave[] mediumWaves;
    public Wave[] hardWaves;
    public AnimationCurve easyWaveCurve;
    public AnimationCurve mediumWaveCurve;
    public AnimationCurve hardWaveCurve;
    public Transform[] spawnPoints;
    public float startSpawnDelay = 5;
    public float endSpawnDelay = 2;
    public float spawnTime = 10;
    public Transform spawnParent;
    private float difficultyTime = 0;
    public float difficultyDuration = 120;

    void Start()
    {
        
    }

    void Update()
    {
        difficultyTime += Time.deltaTime;
        float difficultyRatio = difficultyTime / difficultyDuration;
        float spawnDelay = startSpawnDelay + (endSpawnDelay - startSpawnDelay) * difficultyRatio;
        spawnTime -= Time.deltaTime;
        if(spawnTime < 0)
        {
            spawnTime += spawnDelay;
            float easyWeight = easyWaveCurve.Evaluate(difficultyRatio);
            float mediumWeight = mediumWaveCurve.Evaluate(difficultyRatio);
            float hardWeight = hardWaveCurve.Evaluate(difficultyRatio);
            float easyProbability = easyWeight / (easyWeight + mediumWeight + hardWeight);
            float mediumProbability = easyProbability + mediumWeight / (easyWeight + mediumWeight + hardWeight);
            float hardProbability = mediumProbability + mediumWeight / (easyWeight + mediumWeight + hardWeight);
            float randomValue = Random.value;
            Wave[] waves = null;
            if(randomValue < easyProbability)
            {
                waves = easyWaves;
            }
            else if(randomValue < mediumProbability)
            {
                waves = mediumWaves;
            }
            else
            {
                waves = hardWaves;
            }
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
