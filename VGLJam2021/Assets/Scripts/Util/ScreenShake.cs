using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Vector2 movementIntensity;
    public float rotationIntensity;
    public static ScreenShake instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }
    void Update()
    {
        transform.localPosition = new Vector3(Random.Range(-movementIntensity.x, movementIntensity.x), Random.Range(-movementIntensity.y, movementIntensity.y));
        transform.localRotation = Quaternion.AngleAxis(Random.Range(-rotationIntensity, rotationIntensity), Vector3.forward);
    }
}
