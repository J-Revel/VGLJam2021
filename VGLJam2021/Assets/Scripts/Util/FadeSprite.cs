using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSprite : MonoBehaviour
{
    public float duration;
    public SpriteRenderer spriteRenderer;
    private float time;

    void Start()
    {
        
    }

    void Update()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1 - time / duration);
        time += Time.deltaTime;
        if(time > duration)
        {
            Destroy(gameObject);
        }
    }
}
