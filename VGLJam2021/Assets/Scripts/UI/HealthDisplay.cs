using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Image counterPrefab;
    public float offset;
    public int maxHealth;
    public Sprite onSprite;
    public Sprite offSprite;
    private Health health;
    private List<Image> images = new List<Image>();

    void Start()
    {
        health = PlayerController.instance.GetComponent<Health>();
        health.hurtDelegate += OnHurt;
        for(int i=0; i<maxHealth; i++)
        {
            Image img = Instantiate(counterPrefab, transform.position + i * offset * Vector3.right, Quaternion.identity, transform);
            img.sprite = health.health > i ? onSprite : offSprite;
            images.Add(img);
        }
    }

    private void OnHurt()
    {
        for(int i=0; i<maxHealth; i++)
        {
            images[i].sprite = health.health > i ? onSprite : offSprite;
        }
    }
}
