using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName ="Assets/EnemyState/DeathState")]
public class DeathState : EnemyState
{
    protected override void InnerUpdateState(){}

    protected override void InnerEnterState(){
        enemy.transform.gameObject.SetActive(false);
    }

    protected override void InnerExitState(){}

}
