using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/AimingBehaviour/AimTowardsCenterBehaviour")]
public class AimTowardsCenterBehaviour : BaseAimingBehaviour
{
    [SerializeField] NumberOfFiringDirections numberOfFiringDirections = NumberOfFiringDirections.FOUR;

    Vector3 middleOfPlayArea;

    float sliceSize;

    enum NumberOfFiringDirections{
        FOUR,
        EIGHT,
        SIXTEEN
    }

    protected override void InnerEnterBehaviour()
    {
        middleOfPlayArea = GameManager.Instance._EnemyManager.middleOfPlayArea.GetComponent<BoxCollider2D>().bounds.center;
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
        base.InnerEnterBehaviour();
    }

    protected override void UpdateAimingAngle()
    {
        Vector3 direction = middleOfPlayArea - enemy.transform.position;
        float angleToCenter = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        
        angleToCenter = (angleToCenter + 360f) % 360f;

        float snappedAngle = Mathf.Round(angleToCenter / sliceSize) * sliceSize;

        angle = snappedAngle % 360f;
    }
}