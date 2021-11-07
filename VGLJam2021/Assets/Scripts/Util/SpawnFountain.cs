using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFountain : MonoBehaviour
{
    public int spawnCount = 5;
    public float interval = 0.1f;
    private float time;
    public float maxAngleOffset = 20;
    public Transform toSpawn;
    

    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time > interval)
        {
            time -= interval;
            float angle = Random.Range(-maxAngleOffset, maxAngleOffset);
            spawnCount--;
            Instantiate(toSpawn, transform.position, transform.rotation * Quaternion.AngleAxis(angle, Vector3.forward), LevelContainer.instance.transform);
            if(spawnCount <= 0) Destroy(gameObject);
        }
    }
}
