using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    public AudioClip music;

    private void Start()
    {
        MusicPlayer.instance.PlayMusic(music);
    }
}
