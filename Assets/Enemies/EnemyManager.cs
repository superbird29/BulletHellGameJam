using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Bounds spawnArea;

    private bool allEnemiesDead;

    bool inbetweenRounds = false;

    bool inbetweenLevels = false;

    public int roundCount;

    public int levelCount;

    private int currentWaveCount = 0;

    private bool spawnNextWave;

    private bool waveActive;

    private EnemyRound currentRound;

    private EnemyLevel currentLevel;

    private List<EnemyRound> rounds;

    [SerializeField] List<EnemyLevel> levels;

    public List<EnemyStateMachine> currentRoundEnemies;

    public GameObject middleOfPlayArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>().bounds;
        levelCount = 0;
        currentRoundEnemies = new List<EnemyStateMachine>();
        allEnemiesDead = true;
        StartNextLevel();
    }

    void Update()
    {

        if (!waveActive || inbetweenRounds || inbetweenLevels) return;

        if (spawnNextWave)
        {
            if (currentWaveCount + 1 < currentRound.waves.Count)
            {
                waveActive = false;
                currentWaveCount++;
                SpawnWave(currentRound.waves[currentWaveCount]);
            }
            else if (roundCount < rounds.Count)
            {
                StartNextRound(30f);
            }
            else if (levelCount < levels.Count)
            {
                StartNextLevel();
            }
        }

        if (waveActive && currentRoundEnemies.Count == 0)
        {
            spawnNextWave = true;
        }
    }

    public void StartNextRound(float roundTime)
    {
        inbetweenRounds = true;
        currentWaveCount = 0;
        currentRound = Instantiate(rounds[roundCount]);
        float timeBetweenWaves = roundTime / currentRound.waves.Count;
        SpawnWave(currentRound.waves[currentWaveCount]);
        for (int i = 0; i < currentRound.waves.Count; i++)
        {
            StartCoroutine(WaveTimer(timeBetweenWaves * i + 1, currentRound.waves[currentWaveCount]));
        }
        roundCount++;
        inbetweenRounds = false;
    }

    public void StartNextLevel()
    {
        inbetweenLevels = true;
        waveActive = false;
        inbetweenRounds = true;
        
        currentLevel = Instantiate(levels[levelCount]);

        rounds = currentLevel.rounds;
        roundCount = 0;

        currentWaveCount = 0;
        levelCount++;

        StartNextRound(30f);
        inbetweenLevels = false;
    }

    public void AddEnemyToList(EnemyStateMachine enemy)
    {
        currentRoundEnemies.Add(enemy);
    }

    public void RemoveEnemyFromList(EnemyStateMachine enemy)
    {
        currentRoundEnemies.Remove(enemy);
    }

    void SpawnWave(EnemyGroupManager wave)
    {
        Instantiate(wave, transform.position, Quaternion.identity);
        currentRound.waves.Remove(wave);
        spawnNextWave = false;
        waveActive = true;
    }

    IEnumerator WaveTimer(float time, EnemyGroupManager wave)
    {
        yield return new WaitForSeconds(time);
        if (currentRound.waves.Contains(wave))
        {
            spawnNextWave = true;
        }
    }

}
