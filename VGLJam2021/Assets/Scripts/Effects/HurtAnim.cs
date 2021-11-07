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
    private Vector3 startScale;

    void Start()
    {
        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponentInParent<Health>().hurtDelegate += OnHurt;
        startScale = transform.localScale;
    }

    void Update()
    {
        if(animStarted)
        {
            animTime += Time.deltaTime;
            float animRatio = 1 - Mathf.Abs(animTime / duration - 0.5f) * 2;
            float scale = targetScale + (1 - animRatio) * (1 - targetScale);
            transform.localScale = startScale *scale;
            float angle = animRatio * targetAngle;
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, animRatio);
            if(animTime > duration)
            {
                animStarted = false;
                transform.localScale = startScale;
                transform.localRotation = Quaternion.identity;
                spriteRenderer.color = Color.white;
            }
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
