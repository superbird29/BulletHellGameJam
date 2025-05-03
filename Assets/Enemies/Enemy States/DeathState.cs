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
        GameManager.Instance._EnemyManager.RemoveEnemyFromList(enemy);
    }

    protected override void InnerExitState(){}

}
