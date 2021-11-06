using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtScreenShake : MonoBehaviour
{
    public float duration = 0.5f;
    public float time = 0;
    public bool playing = false;
    public float targetDisplacement = 1;
    public float targetAngle = 20;


    void Start()
    {
        GetComponent<Health>().hurtDelegate += OnDamageReceived;
    }

    void Update()
    {
        if(playing)
        {
            time += Time.deltaTime;
            if(time > duration)
                playing = false;
            float intensityRatio = 1 - Mathf.Abs(time / duration * 2 - 1);
            ScreenShake.instance.rotationIntensity = intensityRatio * targetAngle;
            ScreenShake.instance.movementIntensity = new Vector2(intensityRatio * targetDisplacement, intensityRatio * targetDisplacement);

        }
    }

    private void OnDamageReceived()
    {
        playing = true;
        time = 0;
    }
}
