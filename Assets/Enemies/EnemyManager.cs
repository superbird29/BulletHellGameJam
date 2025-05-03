using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    GameManager gameManager;

    PlayerManager playerManager;
    
    List<EnemyStateMachine> enemies;

    void Start()
    {
        playerManager = GameManager.Instance._PlayerManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(){
        //Do this
    }

    IEnumerator CooldownTimer(float time, Action action){
        yield return new WaitForSeconds(time);
        action.Invoke();
    }

}
