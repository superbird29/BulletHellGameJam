using System;
using System.Collections;
using System.Collections.Generic;
using ND_VariaBULLET;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName ="Assets/EnemyState/MoveAsAProjectileState")]
public class MoveAsAProjectileState : MoveState
{
    private DamagerBody damagerBody;

    protected override void InnerEnterState(){
        damagerBody = enemy.GetComponent<DamagerBody>();
        if(damagerBody == null){
            Debug.Log("You forgot to attach a damager body");
            NextState();
        }
        Debug.Log("Gottem");
        damagerBody.enabled = true;
    }

    protected override void InnerExitState(){}

}