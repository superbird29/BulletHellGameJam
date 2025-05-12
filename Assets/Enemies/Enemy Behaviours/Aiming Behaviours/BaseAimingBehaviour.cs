using System;
using UnityEngine;


[Serializable]
public class BaseAimingBehaviour : EnemyBehaviour
{
    public BaseAimingBehaviour nextBehaviour;

    protected float angle;
    protected override void NextBehaviour(){
        enemyCompositeBehaviour.NextBehaviour(nextBehaviour);
    }

    protected override void InnerUpdateBehaviour()
    {
        UpdateAimingAngle();
        enemy.controller.transform.parent.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected virtual void UpdateAimingAngle()
    {
    }

}