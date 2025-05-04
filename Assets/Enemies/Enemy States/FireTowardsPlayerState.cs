using System;
using System.Collections;
using ND_VariaBULLET;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName ="Assets/EnemyState/FireTowardsPlayerState")]
public class FireTowardsPlayerState : FiringState
{

    protected override void InnerUpdateState(){

        Vector3 direction = GameManager.Instance._PlayerManager.transform.position - enemy.controller.transform.parent.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemy.controller.transform.parent.transform.rotation = Quaternion.Euler(0, 0, angle);

        base.InnerUpdateState();
    }
}