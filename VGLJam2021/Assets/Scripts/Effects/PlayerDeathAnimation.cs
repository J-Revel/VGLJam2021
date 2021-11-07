using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathAnimation : MonoBehaviour
{
    public Health health;
    private bool playing = false;
    private float animTime = 0;
    private float deathAnimDuration = 3;
    public float zoomDuration = 0.5f;
    public float zoomTargetDistance = 5;
    public CameraBehaviour cameraBehaviour;
    private Vector3 cameraStartPos;
    public GameObject gameoverMenuPrefab;
    
    void Start()
    {
        health = GetComponent<Health>();
        health.deathDelegate += OnDeath;
    }

    void Update()
    {
        if(playing)
        {
            animTime += Time.unscaledDeltaTime;
            cameraBehaviour.transform.position = Vector3.Lerp(cameraStartPos, health.transform.position - Vector3.forward * zoomTargetDistance, animTime / zoomDuration);
            if(animTime >= zoomDuration)
            {
                Time.timeScale = 1;
            }
            if(animTime >= zoomDuration + deathAnimDuration)
            {
                MenuSpawner.instance.SpawnMenu(gameoverMenuPrefab);
                playing = false;
            }
        }
    }

    private void OnDeath(Vector2 direction)
    {
        Time.timeScale = 0;
        playing = true;
        cameraBehaviour.enabled = false;
        cameraStartPos = cameraBehaviour.transform.position;
        ScreenShake.instance.enabled = false;
        PlayerController.instance.GetComponent<AnimatedSprite>().SelectAnim("Die");
        PlayerController.instance.enabled = false;
        Rigidbody2D rigidbody = PlayerController.instance.GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector2.zero;
    }
}
