using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Bounds spawnArea;

    private bool inbetweenRounds = false;
    private bool inbetweenLevels = false;

    public int roundCount;
    public int levelCount;

    private EnemyRound currentRound;
    private EnemyLevel currentLevel;

    private Queue<EnemyGroupManager> waveQueue;
    private List<EnemyRound> rounds;

    [SerializeField] List<EnemyLevel> levels;

    public List<EnemyStateMachine> currentRoundEnemies;

    public GameObject middleOfPlayArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>().bounds;
        levelCount = 0;
        currentRoundEnemies = new List<EnemyStateMachine>();
        StartNextLevel();
    }

    void Update()
    {
        if (inbetweenLevels || inbetweenRounds) return;

        if (currentRoundEnemies.Count == 0 && waveQueue.Count == 0)
        {
            GameManager.Instance._RoundManager.EndRoundRewards();

            if (roundCount < rounds.Count)
            {
                StartNextRound(GameManager.Instance._RoundManager.roundDuration);
            }
            else if (levelCount < levels.Count)
            {
                StartNextLevel();
            }
        }
    }

    public void StartNextLevel()
    {
        inbetweenLevels = true;

        currentLevel = Instantiate(levels[levelCount]);
        rounds = currentLevel.rounds;
        roundCount = 0;
        levelCount++;

        StartNextRound(GameManager.Instance._RoundManager.roundDuration);
        inbetweenLevels = false;
    }

    public void StartNextRound(float roundTime)
    {
        inbetweenRounds = true;

        currentRound = Instantiate(rounds[roundCount]);
        waveQueue = new Queue<EnemyGroupManager>(currentRound.waves);

        roundCount++;
        currentRoundEnemies.Clear();

        StartCoroutine(ManageWaveSequence(roundTime));
        inbetweenRounds = false;
    }

IEnumerator ManageWaveSequence(float roundDuration)
{
    float timeBetweenWaves = roundDuration / Mathf.Max(1, waveQueue.Count);

    while (waveQueue.Count > 0)
    {
        var wave = waveQueue.Dequeue();
        SpawnWave(wave);

        yield return new WaitUntil(() => currentRoundEnemies.Count > 0);
        
        float elapsed = 0f;

        while (elapsed < timeBetweenWaves)
        {
            if (currentRoundEnemies.Count == 0)
                break;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Make sure we don't move on until current wave is fully cleared
        yield return new WaitUntil(() => currentRoundEnemies.Count == 0);
    }
}


    void SpawnWave(EnemyGroupManager wave)
    {
        Instantiate(wave, transform.position, Quaternion.identity);
    }

    public void AddEnemyToList(EnemyStateMachine enemy)
    {
        if (!currentRoundEnemies.Contains(enemy))
            currentRoundEnemies.Add(enemy);
    }

    public void RemoveEnemyFromList(EnemyStateMachine enemy)
    {
        if (currentRoundEnemies.Contains(enemy))
            currentRoundEnemies.Remove(enemy);
    }
}
