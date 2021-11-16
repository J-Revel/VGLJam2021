using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadLevelAfterDelay : MonoBehaviour
{
    public float delay = 3;
    private float time = 0;
    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time > delay)
        {
            LevelSpawner.instance.StopLevel();
            Destroy(this);
        }
    }
}
