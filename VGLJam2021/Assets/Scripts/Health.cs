using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 1;

    public System.Action hurtDelegate;
    public System.Action<Vector2> deathDelegate;

    public void Damage(int damage, Vector2 damageDirection)
    {
        health -=  damage;
        if(health <= 0)
        {
            deathDelegate?.Invoke(damageDirection);
            Destroy(gameObject);
        }
        else
        {
            hurtDelegate?.Invoke();
        }
    }
}
