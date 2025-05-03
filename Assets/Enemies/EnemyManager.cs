using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    public Bounds spawnArea;

    private bool allEnemiesDead;

    bool inbetweenRounds = false;

    public int round;

    private EnemyRound currentRound;

    [SerializeField] List<EnemyRound> rounds;

    public List<EnemyStateMachine> currentRoundEnemies;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>().bounds;
        round = 0;
        currentRoundEnemies = new List<EnemyStateMachine>();
        allEnemiesDead = true;
    }

    void Update()
    {
        if(allEnemiesDead && round < rounds.Count){
            StartNextRound(30f);
        }

        allEnemiesDead = inbetweenRounds || (currentRoundEnemies.Count == 0 && currentRound.waves.Count == 0);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        { 
            Destroy(collision.gameObject); 
        } 
    }
}
