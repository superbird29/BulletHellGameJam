using System;
using UnityEngine;


[Serializable]
public class BaseMovementBehaviour : EnemyBehaviour
{

    protected Vector3 enemyStartingPosition;

    protected Vector3 destination;
    public float speed = 0f;
    public bool useEnemySpeed = true;

    public BaseMovementBehaviour nextBehaviour;
    protected override void NextBehaviour(){
        enemyCompositeBehaviour.NextBehaviour(nextBehaviour);
    }

    protected override void InnerEnterBehaviour(){
        enemyStartingPosition = enemy.transform.position;
        if(useEnemySpeed)
            speed = enemy.enemySpeed;
    }

    protected override void InnerUpdateBehaviour()
    {
        UpdateDestination();
        Vector3 enemyPosition = enemy.transform.position;
        enemy.transform.position = Vector3.MoveTowards(enemyPosition,destination,speed);
        if(ShouldMoveToNextBehaviour()){
            enemyCompositeBehaviour.NextBehaviour(nextBehaviour);
        }
    }

    protected virtual void UpdateDestination(){}

    protected virtual bool ShouldMoveToNextBehaviour(){ return false;}

}