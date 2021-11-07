using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemy;
    void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, transform.rotation, transform.parent);
    }
}
