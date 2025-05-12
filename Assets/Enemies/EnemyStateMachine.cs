using System;
using System.Collections;
using System.Collections.Generic;
using ND_VariaBULLET;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    EnemyState currentState;

    public DeathState deathState;

    [SerializeReference]
    public EnemyState startingState;

    public float enemySpeed;

    private bool changingState = false;

    public BasePattern controller;

    public GameManager gameManager;

    EnemyManager enemyManager;


    void Start()
    {
        enemyManager = GameManager.Instance._EnemyManager;
        enemyManager.AddEnemyToList(this);
        if(controller != null){
        controller.TriggerAutoFire = false;
        }
        EnterState(startingState);
    }

    void FixedUpdate() {
        if (!changingState)
            currentState.UpdateState();
    }

    public void EnterState(EnemyState state){
        changingState = true;
        EnemyState newState = Instantiate(state);
        if(currentState != null){
        currentState.ExitState();
        }
        currentState = newState;
        newState.EnterState(this);
        changingState = false;
    }

    public void StartCooldown(float time, Action action){
        StartCoroutine(CooldownTimer(time,action));
    }

    public void DisableMe(){
        this.enabled = false;
    }

    IEnumerator CooldownTimer(float time, Action action){
        yield return new WaitForSeconds(time);
        action.Invoke();
    }

    private void OnDestroy() {
        enemyManager.RemoveEnemyFromList(this);
    }
}
