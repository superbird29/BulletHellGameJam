using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName ="Assets/EnemyState/FireWhileMovingState")]
public class FireWhileMovingState : EnemyState
{

    protected Vector3 startingPosition;
    public Vector3 distanceToMove = new(0,0,0);
    public float speed = 0f;

    public bool useEnemySpeed = true;

    protected Vector3 destination;

    protected override void InnerEnterState()
    {
        startingPosition = enemy.transform.position;
        destination = startingPosition + distanceToMove;
        enemy.controller.TriggerAutoFire = true;
        if(useEnemySpeed)
            speed = enemy.enemySpeed;
    }

    protected override void InnerExitState()
    {
        enemy.controller.TriggerAutoFire = false;
    }

    protected override void InnerUpdateState()
    {
        Vector3 enemyPosition = enemy.transform.position;

        enemy.transform.position = Vector3.MoveTowards(enemyPosition,destination,speed);

        if(enemyPosition == destination){
            NextState();
        }
    }
}