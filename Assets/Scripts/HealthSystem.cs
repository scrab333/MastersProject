using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{

    public event EventHandler OnDead;
    public event EventHandler OnDamage;

    private int health = 10;
    private int healthMax;

    private void Awake()
    {
        /*
        if (isKnight)
        {
            health = 12;
        }
        else if (isRogue || isWizard)
        {
            health = 8;
        }
        else
        {
            health = 10;
        }
        */
        healthMax = health;
    }


    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0)
        {
            health = 0;
        }

        OnDamage?.Invoke(this, EventArgs.Empty);

        if(health == 0)
        {
            Die();
        }

        Debug.Log(health);
    }

    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }


}
