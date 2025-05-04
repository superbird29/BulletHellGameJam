using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName ="Assets/EnemyState/FireTowardsPlayerWhileMovingState")]
public class FireTowardsPlayerWhileMovingState : FireWhileMovingState
{

    protected override void InnerUpdateState()
    {
        
        Vector3 direction = GameManager.Instance._PlayerManager.transform.position - enemy.controller.transform.parent.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemy.controller.transform.parent.transform.rotation = Quaternion.Euler(0, 0, angle);

        base.InnerUpdateState();
    }
}