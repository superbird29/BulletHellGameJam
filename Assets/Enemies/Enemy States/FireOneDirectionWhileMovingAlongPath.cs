using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyState/FireOneDirectionWhileMovingAlongPath")]
public class FireOneDirectionWhileMovingAlongPath : MoveAlongPathState
{

    Vector3 middleOfPlayArea;

    [SerializeField] FireDirection direction;

    public enum FireDirection
    {
        Right,
        Up,
        Left,
        Down
    }

    protected override void InnerEnterState()
    {
        enemy.controller.TriggerAutoFire = true;
        base.InnerEnterState();
    }

    protected override void InnerExitState()
    {
        enemy.controller.TriggerAutoFire = false;
        base.InnerExitState();
    }
    protected override void InnerUpdateState()
    {
        float snappedAngle;

        switch(direction){
            case FireDirection.Right:
                snappedAngle = 0f;
                break;
            case FireDirection.Left:
                snappedAngle = 180f;
                break;
            case FireDirection.Up:
                snappedAngle = 90f;
                break;
            case FireDirection.Down:
                snappedAngle = 270f;
                break;
            default:
                snappedAngle = 0f;
                break;
        }

        enemy.controller.transform.parent.transform.rotation = Quaternion.Euler(0, 0, snappedAngle);


        base.InnerUpdateState();
    }
}