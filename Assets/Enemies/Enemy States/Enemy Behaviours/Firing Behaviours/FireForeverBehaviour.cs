using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/FiringBehaviour/FireForeverBehaviour")]
public class FireForeverBehaviour : BaseFiringBehaviour
{

    protected override void InnerEnterBehaviour()
    {
        enemy.controller.TriggerAutoFire = true;
        base.InnerEnterBehaviour();
    }

    protected override void InnerExitBehaviour()
    {
        enemy.controller.TriggerAutoFire = false;
        base.InnerExitBehaviour();
    }
}