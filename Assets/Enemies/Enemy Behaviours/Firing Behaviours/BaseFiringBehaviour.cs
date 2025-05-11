using System;
using UnityEngine;


[Serializable]
public class BaseFiringBehaviour : EnemyBehaviour
{
    public BaseFiringBehaviour nextBehaviour;
    protected override void NextBehaviour(){
        enemyCompositeBehaviour.NextBehaviour(nextBehaviour);
    }

}