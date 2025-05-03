using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] int rows;

    [SerializeField] int columns;

    [SerializeField] bool staggerRows;

    [SerializeField] SpawnZone zone;

    private Bounds spawnArea;

    private EnemyManager enemyManager;

    enum SpawnZone
    {
        Right,
        Middle,
        Left,
        All
    }

    

    void Start()
    {
        enemyManager = GameManager.Instance._EnemyManager;
        spawnArea = adjustSpawnArea(enemyManager.spawnArea);
        float cellWidth = spawnArea.size.x / columns;
        float cellHeight = spawnArea.size.y / rows;

        Vector2 bottomLeft = new Vector2(spawnArea.min.x, spawnArea.min.y);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float x = bottomLeft.x + (col + (staggerRows && row % 2 == 1 ? 1.0f : 0.5f)) * cellWidth;
                float y = bottomLeft.y + (row + 0.5f) * cellHeight;
                Instantiate(enemyPrefab,new Vector3(x,y,0f),Quaternion.identity);
            }
        }
    }

    Bounds adjustSpawnArea(Bounds originalArea){
        if(zone == SpawnZone.All) return originalArea;
        
        float thirdWidth = originalArea.size.x / 3f;
        Vector3 size = new Vector3(thirdWidth, originalArea.size.y, originalArea.size.z);

        Vector3 center = originalArea.min + new Vector3(thirdWidth / 2f, originalArea.size.y / 2f, 0);

        switch (zone)
        {
            case SpawnZone.Middle:
                center.x += thirdWidth;
                break;
            case SpawnZone.Right:
                center.x += 2f * thirdWidth;
                break;
        }

        return new Bounds(center, size);
    }
}
