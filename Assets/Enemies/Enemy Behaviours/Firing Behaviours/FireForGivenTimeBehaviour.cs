using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/FiringBehaviour/FireForGivenTimeBehaviour")]
public class FireForGivenTimeBehaviour : BaseFiringBehaviour
{

    protected bool firing = false;
    protected bool finishedFiring = false;

    [SerializeField] float firingTime = 5f;

    protected override void InnerEnterBehaviour()
    {
        enemy.controller.TriggerAutoFire = true;
        base.InnerEnterBehaviour();
    }

    protected override void InnerUpdateBehaviour()
    {
        if(finishedFiring){
            NextBehaviour();
        }

        if(!firing){
            firing = true;
            enemy.StartCooldown(firingTime,FinishFiring);
        }
        base.InnerUpdateBehaviour();
    }

    protected override void InnerExitBehaviour()
    {
        enemy.controller.TriggerAutoFire = false;
        base.InnerExitBehaviour();
    }

    public void FinishFiring(){
        finishedFiring = true;
    }
}