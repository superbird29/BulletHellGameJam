using System;
using ND_VariaBULLET;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName ="Assets/EnemyState/MoveAsAProjectileState")]
public class MoveAsAProjectileState : MoveTowardsPlayerState
{
    private DamagerBody damagerBody;

    protected override void InnerEnterState(){
        base.InnerEnterState();
        damagerBody = enemy.GetComponent<DamagerBody>();
        if(damagerBody == null){
            Debug.Log("You forgot to attach a damager body");
            NextState();
        }
        Debug.Log("Gottem");
        damagerBody.enabled = true;
    }

    protected override void InnerExitState(){
    }

}