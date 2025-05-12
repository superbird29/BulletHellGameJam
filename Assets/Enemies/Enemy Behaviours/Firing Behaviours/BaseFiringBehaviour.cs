using System;
using ND_VariaBULLET;
using UnityEngine;


[Serializable]
public class BaseFiringBehaviour : EnemyBehaviour
{
    public BaseFiringBehaviour nextBehaviour;

    [SerializeField] float shotSpeed = 10f;

    protected override void InnerEnterBehaviour()
    {
        
        enemy.controller.GetComponentInChildren<FireBullet>().ShotSpeed = shotSpeed;
        base.InnerEnterBehaviour();
    }

    protected override void NextBehaviour(){
        enemyCompositeBehaviour.NextBehaviour(nextBehaviour);
    }

}