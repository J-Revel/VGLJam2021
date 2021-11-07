using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornSpawnFX : MonoBehaviour
{
    public Transform toSpawnAtEnd;
    private Vector3 direction;
    private Vector3 startPosition;

    public float minDistance = 2;
    public float maxDistance = 4;
    public float minHeight = 0.5f;
    public float maxHeight = 1.5f;
    public float minRotationSpeed = 30;
    public float maxRotationSpeed = 180;
    private float rotationSpeed = 0;

    private float distance;
    private float height;

    public float duration = 1;
    private float time;

    void Start()
    {
        startPosition = transform.position;
        direction = transform.right;
        distance = Random.Range(minDistance, maxDistance);
        height = Random.Range(minHeight, maxHeight);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    void Update()
    {
        time += Time.deltaTime;
        float ratio = time / duration;
        
        transform.position = startPosition + direction * distance * ratio + Vector3.up * Mathf.Sin(ratio * Mathf.PI) * height;
        transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward);
        if(ratio > 1)
        {
            Instantiate(toSpawnAtEnd, startPosition + direction * distance, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
