using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 1;
    private float invincibilityTime = 0;
    public float invincibilityDuration = 0;

    public System.Action hurtDelegate;
    public System.Action<Vector2> deathDelegate;
    public bool destroyOnDeath = true;
    private void Update()
    {
        invincibilityTime -= Time.deltaTime;
    }

    public void Damage(int damage, Vector2 damageDirection)
    {
        if(!enabled || invincibilityTime > 0) return;
        health -=  damage;
        if(health <= 0)
        {
            deathDelegate?.Invoke(damageDirection);
            enabled = false;
            if(destroyOnDeath)
                Destroy(gameObject);
        }
        else
        {
            hurtDelegate?.Invoke();
            invincibilityTime = invincibilityDuration;
        }
    }
}
