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
        health.deathDelegate += OnDeath;
        for(int i=0; i<maxHealth; i++)
        {
            Image img = Instantiate(counterPrefab, transform.position, Quaternion.identity, transform);
            img.GetComponent<RectTransform>().anchoredPosition = i * offset * Vector3.right;
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

    private void OnDeath(Vector2 direction)
    {
        OnHurt();
    }
}
