using System;
using UnityEngine;


[Serializable]
public class EnemyBehaviour : ScriptableObject
{

    protected EnemyCompositeBehaviourState enemyCompositeBehaviour;


    protected EnemyStateMachine enemy;

    public void EnterBehaviour(EnemyCompositeBehaviourState state){
        enemyCompositeBehaviour = state;
        enemy = state.GetEnemy();
        Debug.Log("Entering Behaviour: " + GetType().Name.ToString());
        InnerEnterBehaviour();
    }

    public void UpdateBehaviour()
    {
        InnerUpdateBehaviour();
    }

    public void ExitBehaviour()
    {
        Debug.Log("Entering Behaviour: " + GetType().Name.ToString());
        InnerExitBehaviour();
    }

    protected virtual void NextBehaviour(){}

    protected virtual void InnerUpdateBehaviour(){}

    protected virtual void InnerEnterBehaviour(){}

    protected virtual void InnerExitBehaviour(){}
}