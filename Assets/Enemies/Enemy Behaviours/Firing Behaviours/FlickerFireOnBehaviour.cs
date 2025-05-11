using System;
using ND_VariaBULLET;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/FiringBehaviour/FlickerFireOnBehaviour")]
public class FlickerFireOnBehaviour : BaseFiringBehaviour
{

    protected bool firing = false;

    private int baseTriggerDelay = 0;

    private float basePauseRate = 0;

    private float baseShotRate = 0;

    private FireBullet enemyFireBullet;

    [SerializeField] int repititions = 3;

    [SerializeField] float firingCooldown = 1f;

    [Range(.05f,100f)]
    [SerializeField] float timeBetweenShots = .1f;

    protected override void InnerEnterBehaviour()
    {
        baseTriggerDelay = enemy.controller.TriggerDelay;
        enemy.controller.TriggerAutoFire = false;
        enemy.controller.TriggerDelay = 0;
        enemyFireBullet = enemy.controller.GetComponentInChildren<FireBullet>();
        basePauseRate = enemyFireBullet.PauseRate;
        baseShotRate = enemyFireBullet.ShotRate;
        enemyFireBullet.PauseRate = 1;
        enemyFireBullet.ShotRate = 200;
        base.InnerEnterBehaviour();
    }

    protected override void InnerUpdateBehaviour()
    {
        if(!firing){
            firing = true;
            for(int i = 0;i < repititions;i++){
                enemy.StartCooldown(timeBetweenShots * i,FlickerFiringOn);
                
            }
            enemy.StartCooldown(firingCooldown + (timeBetweenShots * repititions + 1),FinishFiring);
        }
        base.InnerUpdateBehaviour();
    }

    protected override void InnerExitBehaviour()
    {
        enemy.controller.TriggerAutoFire = false;
        enemy.controller.TriggerDelay = baseTriggerDelay;
        enemyFireBullet.PauseRate = basePauseRate;
        base.InnerExitBehaviour();
    }

    public void FlickerFiringOn(){
        enemy.controller.TriggerAutoFire = true;
        enemy.StartCooldown(.01f,TurnFiringOff);
    }

    public void TurnFiringOff(){
        enemy.controller.TriggerAutoFire = false;
    }

    public void FinishFiring(){
        firing = false;
    }
}
