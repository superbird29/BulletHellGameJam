using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/AimingBehaviour/AimOneDirectionBehaviour")]
public class AimOneDirectionBehaviour : BaseAimingBehaviour
{
    [SerializeField] FireDirection direction;

    public enum FireDirection
    {
        Right,
        Up,
        Left,
        Down
    }

    protected override void UpdateAimingAngle()
    {
        switch(direction){
            case FireDirection.Right:
                angle = 0f;
                break;
            case FireDirection.Left:
                angle = 180f;
                break;
            case FireDirection.Up:
                angle = 90f;
                break;
            case FireDirection.Down:
                angle = 270f;
                break;
            default:
                angle = 0f;
                break;
        }
    }
}