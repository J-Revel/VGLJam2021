using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSprite : MonoBehaviour
{
    private Color startColor;
    public float duration;
    public SpriteRenderer spriteRenderer;
    private float time;

    void Start()
    {
        startColor = spriteRenderer.color;
    }

    void Update()
    {
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a * (1 - time / duration));
        time += Time.deltaTime;
        if(time > duration)
        {
            Destroy(gameObject);
        }
    }
}
