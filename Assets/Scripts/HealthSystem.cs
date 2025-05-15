using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDead;
    public event EventHandler OnDamage;
    public event EventHandler OnHeal;

    [SerializeField] private int health = 100;

    private int healthMax;

    private void Awake()
    {
        healthMax = health;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if(health < 0)
        {
            health = 0;
        }

        OnDamage?.Invoke(this, EventArgs.Empty);

        if (health == 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        if (health < healthMax)
        {
            health += healAmount;
            if (health > healthMax)
            {
                health = healthMax;
            }
        }

        OnHeal?.Invoke(this, EventArgs.Empty);
    }


    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealhNormalized()
    {
        return (float)health / healthMax;
    }
}
