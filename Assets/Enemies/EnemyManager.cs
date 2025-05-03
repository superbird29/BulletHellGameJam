using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    GameManager gameManager;

    PlayerManager playerManager;
    
    public Bounds spawnArea;

    private bool allEnemiesDead;

    bool inbetweenRounds;

    public int round;

    private EnemyRound currentRound;

    [SerializeField] List<EnemyRound> rounds;

    public List<EnemyStateMachine> currentRoundEnemies;

    void Start()
    {
        playerManager = GameManager.Instance._PlayerManager;
        spawnArea = GetComponent<BoxCollider2D>().bounds;
        round = 0;
        inbetweenRounds = true;
        currentRoundEnemies = new List<EnemyStateMachine>();
        StartNextRound(30f);
    }

    void Update()
    {
        allEnemiesDead = inbetweenRounds || (currentRoundEnemies.Count == 0 && currentRound.waves.Count == 0);

        if(allEnemiesDead && round < rounds.Count){
            StartNextRound(30f);
        }
    }

    void StartNextRound(float roundTime){
        inbetweenRounds = true;
        currentRound = Instantiate(rounds[round]);
        float timeBetweenWaves = roundTime/currentRound.waves.Count;

        for(int i = 0; i < currentRound.waves.Count;i++){
            StartCoroutine(WaveTimer(timeBetweenWaves * i,currentRound.waves[i]));
        }
        round++;
        inbetweenRounds = false;
    }

    public void AddEnemyToList(EnemyStateMachine enemy){
        currentRoundEnemies.Add(enemy);
    }

    public void RemoveEnemyFromList(EnemyStateMachine enemy){
        currentRoundEnemies.Remove(enemy);
    }

    void SpawnWave(EnemyGroupManager wave){
        Instantiate(wave,transform.position,Quaternion.identity);
        currentRound.waves.Remove(wave);
    }

    IEnumerator WaveTimer(float time,EnemyGroupManager wave){
        yield return new WaitForSeconds(time);
        SpawnWave(wave);
    }

}
