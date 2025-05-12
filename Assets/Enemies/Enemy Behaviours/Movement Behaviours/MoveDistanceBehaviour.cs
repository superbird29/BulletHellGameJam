using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/MovementBehaviour/MoveDistanceBehaviour")]
public class MoveDistanceBehaviour : BaseMovementBehaviour
{
    public Vector3 distanceToMove = new(0f,0f,0f);
    protected override void UpdateDestination(){

        destination = enemyStartingPosition + distanceToMove;
    }

    protected override bool ShouldMoveToNextBehaviour(){ 
        return Vector3.Distance(enemy.transform.position, destination) < 0.01f;
    }
}