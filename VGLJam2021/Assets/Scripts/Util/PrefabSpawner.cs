using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public static PrefabSpawner instance;
    public Transform prefab;

    private void Awake()
    {
        instance = this;
    }
    
    public Transform SpawnPrefab(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        return Instantiate(prefab, position, rotation, parent);
    }
}
