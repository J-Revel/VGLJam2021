using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 1;
    private float invincibilityTime = 0;
    public float invincibilityDuration = 0;
    public int deathScore = 0;
    public int deathComboIndex = 0; 
    public ScoreBonusEffect bonusEffect;
    public Color deathBonusColor;

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
            if(deathScore > 0)
            {
                int score = ScoreSystem.instance.AddScore(deathScore, deathComboIndex);
                ScoreBonusEffect effect = Instantiate(bonusEffect, transform.position, Quaternion.identity);
                effect.color = deathBonusColor;
                effect.score = score;
            }
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
