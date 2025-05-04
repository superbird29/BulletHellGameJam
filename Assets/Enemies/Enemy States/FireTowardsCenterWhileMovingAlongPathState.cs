using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyState/FireTowardsCenterWhileMovingAlongPathState")]
public class FireTowardsCenterWhileMovingAlongPathState : MoveAlongPathState
{
    [SerializeField] NumberOfFiringDirections numberOfFiringDirections = NumberOfFiringDirections.FOUR;

    Vector3 middleOfPlayArea;

    Vector3 direction;

    float sliceSize;

    enum NumberOfFiringDirections{
        FOUR,
        EIGHT,
        SIXTEEN
    }

    protected override void InnerEnterState()
    {
        enemy.controller.TriggerAutoFire = true;
        middleOfPlayArea = GameManager.Instance._EnemyManager.middleOfPlayArea.transform.position;
        switch(numberOfFiringDirections){
            case NumberOfFiringDirections.FOUR:
                sliceSize = 90f;
                break;
            case NumberOfFiringDirections.EIGHT:
                sliceSize = 45f;
                break;
            case NumberOfFiringDirections.SIXTEEN:
                sliceSize = 22.5f;
                break;
        }
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

        float snappedAngle = Mathf.Round(angle / sliceSize) * sliceSize;

        snappedAngle = snappedAngle % 360f;

        Debug.Log("Snapped angle: " + snappedAngle);

        enemy.controller.transform.parent.transform.rotation = Quaternion.Euler(0, 0, snappedAngle);


        base.InnerUpdateState();
    }
}