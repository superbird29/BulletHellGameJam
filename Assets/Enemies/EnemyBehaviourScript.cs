using System;
using System.Collections;
using System.Collections.Generic;
using ND_VariaBULLET;
using UnityEngine;

public class EnemyBehaviourScript : MonoBehaviour
{
    EnemyState currentState;

    [SerializeReference]
    public EnemyState startingState;

    public float enemySpeed;

    private bool changingState = false;

    public BasePattern controller;


    void Start()
    {
        controller = GetComponentInChildren<BasePattern>();
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
        EnemyState newState = ScriptableObject.Instantiate(state);
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
        Debug.Log("Time: " + time);
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
}
