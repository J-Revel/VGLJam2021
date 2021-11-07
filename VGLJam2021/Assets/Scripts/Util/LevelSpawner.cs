using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public static LevelSpawner instance;
    public GameObject levelPrefab;
    private GameObject levelInstance;

    private void Awake()
    {
        instance = this;
    }
    
    public void ResetLevel()
    {
        Destroy(levelInstance);
        levelInstance = Instantiate(levelPrefab);
    }

    public void StopLevel()
    {
        Destroy(levelInstance);
    }

    public void StartLevel()
    {
        levelInstance = Instantiate(levelPrefab);
    }

    public void Start()
    {
    }
}
