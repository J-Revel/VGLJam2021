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
    public float targetDistortion = 1;
    private bool enemiesPushed = false;

    public EnemiesPush pushEffect;


    void Start()
    {
        GetComponent<Health>().hurtDelegate += OnDamageReceived;
        
    }

    void Update()
    {
        if(playing)
        {
            time += Time.deltaTime;
            float intensityRatio = 1 - Mathf.Abs(time / duration * 2 - 1);
            ScreenShake.instance.rotationIntensity = intensityRatio * targetAngle;
            ScreenShake.instance.movementIntensity = new Vector2(intensityRatio * targetDisplacement, intensityRatio * targetDisplacement);
            PostProcessController.instance.distortion = -Mathf.Sin(time / duration * Mathf.PI * 2) * targetDistortion;
            
            if(time > duration / 2 && !enemiesPushed)
            {
                enemiesPushed = true;
                pushEffect.Push();
            }

            if(time > duration)
            {
                playing = false;
                ScreenShake.instance.rotationIntensity = 0;
                ScreenShake.instance.movementIntensity = Vector2.zero;
                PostProcessController.instance.distortion = 0;
            }
        }
    }

    private void OnDamageReceived()
    {
        playing = true;
        time = 0;
        enemiesPushed = false;
    }
}
