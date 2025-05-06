using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/EnemyCompositeBehaviourState")]
public class EnemyCompositeBehaviourState : EnemyState
{

    [SerializeField] BaseMovementBehaviour startingMovementBehaviour;

    [SerializeField] BaseFiringBehaviour startingFiringBehaviour;

    [SerializeField] BaseAimingBehaviour startingAimingBehaviour;

    protected BaseMovementBehaviour currentMovementBehaviour;

    protected bool changingMovementBehaviour = false;

    protected BaseFiringBehaviour currentFiringBehaviour;

    protected bool changingFiringBehaviour = false;

    protected BaseAimingBehaviour currentAimingBehaviour;

    protected bool changingAimingBehaviour = false;

    public EnemyStateMachine GetEnemy() { return enemy; }

    protected override void InnerUpdateState()
    {
        if (currentMovementBehaviour != null && !changingMovementBehaviour)
        {
            currentMovementBehaviour.UpdateBehaviour();
        }

        if (currentFiringBehaviour != null && !changingFiringBehaviour)
        {
            currentFiringBehaviour.UpdateBehaviour();
        }

        if (currentAimingBehaviour != null && !changingAimingBehaviour)
        {
            currentAimingBehaviour.UpdateBehaviour();
        }
    }

    protected override void InnerEnterState()
    {
        if (startingMovementBehaviour != null)
        {
            currentMovementBehaviour = startingMovementBehaviour;
            currentMovementBehaviour.EnterBehaviour(this);
        }

        if (startingFiringBehaviour != null)
        {
            currentFiringBehaviour = startingFiringBehaviour;
            currentFiringBehaviour.EnterBehaviour(this);
        }

        if (startingAimingBehaviour != null)
        {
            currentAimingBehaviour = startingAimingBehaviour;
            currentAimingBehaviour.EnterBehaviour(this);
        }
    }

    protected override void InnerExitState()
    {
        if (currentMovementBehaviour != null && !changingMovementBehaviour)
        {
            currentMovementBehaviour.ExitBehaviour();
        }

        if (currentFiringBehaviour != null && !changingFiringBehaviour)
        {
            currentFiringBehaviour.ExitBehaviour();
        }

        if (currentAimingBehaviour != null && !changingAimingBehaviour)
        {
            currentAimingBehaviour.ExitBehaviour();
        }
    }

    public void NextBehaviour(BaseMovementBehaviour nextBehaviour)
    {
        changingMovementBehaviour = true;
        BaseMovementBehaviour newBehaviour = Instantiate(nextBehaviour);
        if (currentMovementBehaviour != null)
        {
            currentMovementBehaviour.ExitBehaviour();
        }
        currentMovementBehaviour = newBehaviour;
        currentMovementBehaviour.EnterBehaviour(this);
        changingMovementBehaviour = false;
    }

    public void NextBehaviour(BaseFiringBehaviour nextBehaviour)
    {
        changingFiringBehaviour = true;
        BaseFiringBehaviour newBehaviour = Instantiate(nextBehaviour);
        if (currentFiringBehaviour != null)
        {
            currentFiringBehaviour.ExitBehaviour();
        }
        currentFiringBehaviour = newBehaviour;
        currentFiringBehaviour.EnterBehaviour(this);
        changingFiringBehaviour = false;
    }

    public void NextBehaviour(BaseAimingBehaviour nextBehaviour)
    {
        changingAimingBehaviour = true;
        BaseAimingBehaviour newBehaviour = Instantiate(nextBehaviour);
        if (currentAimingBehaviour != null)
        {
            currentAimingBehaviour.ExitBehaviour();
        }
        currentAimingBehaviour = newBehaviour;
        currentAimingBehaviour.EnterBehaviour(this);
        changingAimingBehaviour = false;
    }
}