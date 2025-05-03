using System;
using UnityEngine;


[Serializable]
public class EnemyState : ScriptableObject
{

    protected EnemyBehaviourScript enemy;

    public EnemyState nextState;

    public void EnterState(EnemyBehaviourScript enemy){
        this.enemy = enemy;
        Debug.Log("Entering State: " + this.GetType().Name.ToString());
        InnerEnterState();
    }

    public void UpdateState()
    {
        InnerUpdateState();
    }

    public void ExitState()
    {
        Debug.Log("Entering State: " + this.GetType().Name.ToString());
        InnerExitState();
    }

    protected void NextState(){
        enemy.EnterState(nextState);
    }

    protected virtual void InnerUpdateState(){}

    protected virtual void InnerEnterState(){}

    protected virtual void InnerExitState(){}
}