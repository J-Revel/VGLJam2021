using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeIn : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float appearDuration = 3;
    private float time = 0; 
    
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        time += Time.deltaTime;
        canvasGroup.alpha = time / appearDuration;
        if(time > appearDuration)
            enabled = false;
    }
}
