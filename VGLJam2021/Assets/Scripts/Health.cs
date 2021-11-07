using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 1;

    public System.Action hurtDelegate;
    public System.Action<Vector2> deathDelegate;
    public bool destroyOnDeath = true;

    public void Damage(int damage, Vector2 damageDirection)
    {
        if(!enabled) return;
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
        }
    }
}
