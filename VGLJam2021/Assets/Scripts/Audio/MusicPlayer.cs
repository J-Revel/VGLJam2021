using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    private AudioSource source;
    public float fadeDuration = 2;
    private float fadeTime;
    private AudioClip pendingClip;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(pendingClip != null)
        {
            if(fadeTime < fadeDuration / 2 && fadeTime + Time.deltaTime >= fadeDuration / 2)
            {
                source.clip = pendingClip;
                source.Play();
            }
            fadeTime += Time.deltaTime;
            source.volume = (Mathf.Abs(fadeTime/fadeDuration * 2 - 1));
            if(fadeTime > fadeDuration)
                pendingClip = null;
        }

    }

    public void PlayMusic(AudioClip clip)
    {
        if(clip != source.clip)
        {
            pendingClip = clip;
            fadeTime = 0;
        }
    }
}
