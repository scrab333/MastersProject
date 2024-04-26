using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAction : BaseAction
{
    [SerializeField] private AudioClip beekeeperHeal;
    [SerializeField] private AudioClip wizardFireball;
    AudioSource audioSource;


    [SerializeField] private Transform grenadeProjectilePrefab;
    [SerializeField] private Transform healProjectilePrefab;


    private int maxThrowDistance = 7;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("Update", 1);
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }
    }


    public override string GetActionName()
    {
        if (isWizard)
        {
            return "Fireball";
        }
        else if (isBeeKeeper)
        {
            return "Heal missile";
        }
        else if (!isWizard)
        {
            return null;
        }
        else
        {
            return "What";
        }
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 0,
        };
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxThrowDistance; x <= maxThrowDistance; x++)
        {
            for (int z = -maxThrowDistance; z <= maxThrowDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z, 0);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxThrowDistance)
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        audioSource = GetComponent<AudioSource>();
        if (isBeeKeeper)
        {
            audioSource.clip = beekeeperHeal;
            Transform healProjectileTransform = Instantiate(healProjectilePrefab, unit.GetWorldPosition(), Quaternion.identity);
            GrenadeProjectile healProjectile = healProjectileTransform.GetComponent<GrenadeProjectile>();
            healProjectile.Setup(gridPosition, OnGrenadeBehaviourComplete);
            audioSource.Play();
        }
        else if (isWizard)
        {

            audioSource.clip = wizardFireball;
            Transform grenadeProjectileTransform = Instantiate(grenadeProjectilePrefab, unit.GetWorldPosition(), Quaternion.identity);
            GrenadeProjectile grenadeProjectile = grenadeProjectileTransform.GetComponent<GrenadeProjectile>();
            grenadeProjectile.Setup(gridPosition, OnGrenadeBehaviourComplete);
            audioSource.Play();
            animator.SetBool("Attack", true);
        }

        ActionStart(onActionComplete);
    }

    private void OnGrenadeBehaviourComplete()
    {
        ActionComplete();
        animator.SetBool("Attack", false);
    }

}
