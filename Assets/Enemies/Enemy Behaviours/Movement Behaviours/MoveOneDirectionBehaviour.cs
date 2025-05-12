using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/MovementBehaviour/MoveOneDirectionBehaviour")]
public class MoveOneDirectionBehaviour : BaseMovementBehaviour
{

    [SerializeField] MovementDirection movementDirection;

    private Vector3 movementVector = Vector3.down;

    enum MovementDirection{
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    protected override void InnerEnterBehaviour()
    {
        switch(movementDirection){
            case MovementDirection.UP:
                movementVector = Vector3.up;
                break;
            case MovementDirection.DOWN:
                movementVector = Vector3.down;
                break;
            case MovementDirection.LEFT:
                movementVector = Vector3.left;
                break;
            case MovementDirection.RIGHT:
                movementVector = Vector3.right;
                break;
        }
        base.InnerEnterBehaviour();
    }

    protected override void UpdateDestination(){
        destination = enemy.transform.position + movementVector;
    }
}