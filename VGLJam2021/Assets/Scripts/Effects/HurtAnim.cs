using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAnim : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color targetColor;
    public float duration = 0.4f;
    public float minTargetAngle = 2;
    public float maxTargetAngle = 20;
    private bool animStarted = false;
    private float animTime = 0;
    public float minTargetScale = 0.7f;
    public float maxTargetScale = 0.9f;
    private float targetAngle;
    private float targetScale;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponentInParent<Health>().hurtDelegate += OnHurt;
    }

    void Update()
    {
        if(animStarted)
        {
            animTime += Time.deltaTime;
            if(animTime > duration)
            {
                animStarted = false;
            }
            float animRatio = 1 - Mathf.Abs(animTime / duration - 0.5f) * 2;
            float scale = targetScale + (1 - animRatio) * (1 - targetScale);
            transform.localScale = new Vector3(scale, scale, scale);
            float angle = animRatio * targetAngle;
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, animRatio);
        }
    }

    public void OnHurt()
    {
        animStarted = true;
        animTime = 0;
        targetAngle = Random.Range(minTargetAngle, maxTargetAngle);
        targetScale = Random.Range(minTargetScale, maxTargetScale);
    }
}
