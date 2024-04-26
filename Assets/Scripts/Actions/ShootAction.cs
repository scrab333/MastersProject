using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{


    public event EventHandler<OnShootEventArgs> OnShoot;
    public static event EventHandler<OnShootEventArgs> OnAnyShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Unit targetUnit;
        public Unit shootingUnit; 
    }

    private enum State
    { 
        Aiming,
        Shooting,
        Cooloff,
    }

    [SerializeField] private AudioClip rogueShoot;
    [SerializeField] private AudioClip wizardShoot;
    [SerializeField] private AudioClip windElemtal;
    AudioSource audioSource;
     
    private State state;
    private int maxShootDistance = 7;
    private float stateTimer;
    private Unit targetUnit;
    private bool canShootBullet;
    [SerializeField] private LayerMask obstaclesLayerMask;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }


        stateTimer -= Time.deltaTime;
        switch (state)
        {
            case State.Aiming:
                Vector3 aimDir = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
                aimDir.y = 0f;

                float rotateSpeed = 10f;
                transform.forward = Vector3.Slerp(transform.forward, aimDir, Time.deltaTime * rotateSpeed);
                break;
            case State.Shooting:
                if (canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                }
                break;
            case State.Cooloff:
                break;
        }

        if (stateTimer <= 0f)
        {
            NextState();
        }

    }

    private void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                state = State.Shooting;
                float shootingStateTime = 0.1f;
                stateTimer = shootingStateTime;
                break;
            case State.Shooting:
                state = State.Cooloff;
                float coolOffStateTime = 0.5f;
                stateTimer = coolOffStateTime;
                break;
            case State.Cooloff:
                ActionComplete();
                break;
        }

    }

    private void Shoot()
    {
        audioSource = GetComponent<AudioSource>();
        diceRoll.ThrowDice();
        OnAnyShoot?.Invoke(this, new OnShootEventArgs{targetUnit = targetUnit,shootingUnit = unit});
        OnShoot?.Invoke(this, new OnShootEventArgs { targetUnit = targetUnit, shootingUnit = unit});
        if (isRogue)
        {
            audioSource.clip = rogueShoot;
            audioSource.Play();
            targetUnit.Damage(diceRoll.FindFaceResult() + 4);
        }
        else if (isWizard)
        {
            audioSource.clip = wizardShoot;
            audioSource.Play();
            targetUnit.Damage(diceRoll.FindFaceResult() + 4);
        }
        else if (isSmart)
        {
            audioSource.clip = windElemtal;
            audioSource.Play();
            targetUnit.Damage(diceRoll.FindFaceResult() + 4);
        }
        else
        {
            audioSource.clip = windElemtal;
            audioSource.Play();
            targetUnit.Damage(diceRoll.FindFaceResult());
        }
    }

    public override string GetActionName()
    {
        if (isRogue)
        {
            return "Bow";
        }
        else if (isWizard)
        {
            return "Firebolt";
        }
        else if (!isRogue || !isWizard)
        {
            return null;
        }
        else
        {
            return "What?";
        }
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();
        return GetValidActionGridPositionList(unitGridPosition);
    }


    public List<GridPosition> GetValidActionGridPositionList(GridPosition unitGridPosition)
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                for (int floor = -maxShootDistance; floor <= maxShootDistance; floor++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z, floor);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }

                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                    if (testDistance > maxShootDistance)
                    {
                        continue;
                    }

                    if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    {
                        // Grid position is empty, no Unit
                        continue;
                    }

                    Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                    if (targetUnit.IsEnemy() == unit.IsEnemy())
                    {
                        // Both Units on the same 'team'
                        continue;
                    }

                    Vector3 unitWorldPosition = LevelGrid.Instance.GetWorldPosition(unitGridPosition);
                    Vector3 shootDir = (targetUnit.GetWorldPosition() - unitWorldPosition).normalized;

                    float unitShoulderHeight = 1.7f;
                    if (Physics.Raycast(
                            unitWorldPosition + Vector3.up * unitShoulderHeight,
                            shootDir,
                            Vector3.Distance(unitWorldPosition, targetUnit.GetWorldPosition()),
                            obstaclesLayerMask))
                    {
                        // Blocked by jac's giant ass
                        continue;
                    }
                    validGridPositionList.Add(testGridPosition);
                }
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {


        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;

        canShootBullet = true;

        ActionStart(onActionComplete);
    }

    public Unit GetTargetUnit()
    {
        return targetUnit;
    }

    public int GetMaxShootDistance()
    {
        return maxShootDistance;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        if (isCloseCombat == true)
        {
            return new EnemyAIAction
            {
                gridPosition = gridPosition,
                actionValue = 0,
            };
        }
        else
        {
            return new EnemyAIAction
            {
                gridPosition = gridPosition,
                actionValue = 100 + Mathf.RoundToInt((1 - targetUnit.GetHealthNormalized()) * 100.0f),
            };
        }

    }

    public int GetTargetCountAtPosition(GridPosition gridPosition)
    {
        return GetValidActionGridPositionList(gridPosition).Count;
    }
}
