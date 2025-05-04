using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyState/FireTowardsCenterWhileMovingAlongPathState")]
public class FireTowardsCenterWhileMovingAlongPathState : MoveAlongPathState
{

    Vector3 middleOfPlayArea;

    Vector3 direction;

    protected override void InnerEnterState()
    {
        enemy.controller.TriggerAutoFire = true;
        middleOfPlayArea = GameManager.Instance._EnemyManager.middleOfPlayArea.transform.position;
        base.InnerEnterState();
    }

    protected override void InnerExitState()
    {
        enemy.controller.TriggerAutoFire = false;
        base.InnerExitState();
    }
    protected override void InnerUpdateState()
    {
        Vector3 direction = middleOfPlayArea - enemy.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        
        angle = (angle + 360f) % 360f;

        float snappedAngle = Mathf.Round(angle / 45f) * 45f;

        snappedAngle = snappedAngle % 360f;

        Debug.Log("Snapped angle: " + snappedAngle);

        enemy.controller.transform.parent.transform.rotation = Quaternion.Euler(0, 0, snappedAngle);


        base.InnerUpdateState();
    }
}