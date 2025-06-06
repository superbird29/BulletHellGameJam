using System;
using System.Collections;
using ND_VariaBULLET;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName ="Assets/EnemyState/FiringState")]
public class FiringState : EnemyState
{
    protected bool firing = false;
    protected bool finishedFiring = false;

    public float firingTime = 5f;

    protected override void InnerUpdateState(){

        if(finishedFiring){
            NextState();
        }

        if(!firing){
            enemy.controller.TriggerAutoFire = true;
            firing = true;
            enemy.StartCooldown(firingTime,FinishFiring);
        }

    }

    protected override void InnerEnterState(){
        firing = false;
        finishedFiring = false;
    }

    protected override void InnerExitState(){
        
    }

    public void FinishFiring(){
        enemy.controller.TriggerAutoFire = false;
        finishedFiring = true;
    }
}