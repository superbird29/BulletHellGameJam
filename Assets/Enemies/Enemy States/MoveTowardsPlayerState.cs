using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName ="Assets/EnemyState/MoveTowardsPlayerState")]
public class MoveTowardsPlayerState : MoveState
{

    protected override void InnerEnterState(){
        base.InnerEnterState();
        destination = GameManager.Instance._PlayerManager.transform.position;
    }

}
