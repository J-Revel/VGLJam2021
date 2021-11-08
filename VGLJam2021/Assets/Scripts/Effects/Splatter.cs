using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splatter : MonoBehaviour
{
    public Transform splatterPrefab;
    public float minScale = 1;
    public float maxScale = 1.5f;
    
    void Start()
    {
        Health health = GetComponent<Health>();
        health.deathDelegate += OnDeath;
    }

    private void OnDeath(Vector2 direction)
    {
        float scale = Random.Range(minScale, maxScale);
        Instantiate(splatterPrefab, transform.position, Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.right, direction), Vector3.forward), LevelContainer.instance.transform).localScale = new Vector3(scale, scale, scale);
    }
}
