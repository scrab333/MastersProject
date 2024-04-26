using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class HealthSystem : MonoBehaviour
{

    public event EventHandler OnDead;
    public event EventHandler OnDamage;
    public WinLoseCondition winLoseCondition;


    private int health = 10;
    private int healthMax;
    private Animator animator;

    public BaseAction baseAction;

    public LevelSystem levelSystem;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void Awake()
    {
        if (baseAction.isKnight)
        {
            health = 30;
        }
        else if (baseAction.isRogue || baseAction.isWizard)
        {
            health = 20;
        }
        else if (baseAction.isBeeKeeper)
        {
            health = 24;
        }
        else if (baseAction.isCloseCombat && !baseAction.isAselios)
        {
            health = 12;
        }
        else if (baseAction.isAselios)
        {
            health = 100;
        }
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
            levelSystem.LevelUp();
            Die();
            animator.SetBool("Death", true);

        }
    }

    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
        winLoseCondition.RemoveCharacter(gameObject);
    }

    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }


}
