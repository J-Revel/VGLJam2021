using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float bounceForce = 10;
    public float animDuration = 0.2f;
    private float animTime = 0;
    public Vector3 animScale = new Vector3(0.8f, 1, 0.8f);
    private List<ContactPoint2D> contacts = new List<ContactPoint2D>();
    public Transform[] rendererTransforms;
    private AudioSource bumpSource;
    private RandomSound randomSound;

    void Start()
    {
        randomSound = GetComponent<RandomSound>();
        bumpSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        contacts.Add(collision.contacts[0]);
    }

    public void PlayBounceAnim()
    {
        animTime = animDuration;
        bumpSource.clip = randomSound.clips[Random.Range(0, randomSound.clips.Length)];
        bumpSource.Play();
    }

    void Update()
    {
        if(animTime > 0)
        {
            animTime -= Time.deltaTime;
            float scaleRatio = (Mathf.Cos(animTime / animDuration * Mathf.PI * 2) + 1) / 2;
            Vector3 newScale = Vector3.one * scaleRatio + animScale * (1 - scaleRatio);
            foreach(Transform rendererTransform in rendererTransforms)
                rendererTransform.localScale = newScale;
        }
        else
        {
            foreach(Transform rendererTransform in rendererTransforms)
                rendererTransform.localScale = Vector3.one;
        }
        foreach(ContactPoint2D contact in contacts)
        {
            Vector2 force = -contact.normal * bounceForce;
            contact.rigidbody?.AddForce(force, ForceMode2D.Impulse);
            animTime = animDuration;
            bumpSource.clip = randomSound.clips[Random.Range(0, randomSound.clips.Length)];
            bumpSource.Play();
        }
        contacts.Clear();
    }
}
