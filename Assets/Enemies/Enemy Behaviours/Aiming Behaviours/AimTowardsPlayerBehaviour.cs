using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/AimingBehaviour/AimTowardsPlayerBehaviour")]
public class AimTowardsPlayerBehaviour : BaseAimingBehaviour
{
    protected override void UpdateAimingAngle()
    {
        Vector3 direction = GameManager.Instance._PlayerManager.transform.position - enemy.controller.transform.parent.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}