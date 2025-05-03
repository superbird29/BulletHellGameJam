using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] int rows;

    [SerializeField] int columns;

    [SerializeField] float staggerRowDistance;

    [SerializeField] float spawnAreaPaddingLR;

    [SerializeField] float spawnAreaPaddingTB;

    private Bounds spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>().bounds;
        float cellWidth = spawnArea.size.x / columns;
        float cellHeight = spawnArea.size.y / rows;

        Vector2 bottomLeft = new Vector2(spawnArea.min.x, spawnArea.min.y);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float x = bottomLeft.x + (col + (row % 2 == 1 ? 1.0f : 0.5f)) * cellWidth;
                float y = bottomLeft.y + (row + 0.5f) * cellHeight;
                Instantiate(enemyPrefab,new Vector3(x,y,0f),Quaternion.identity);
            }
        }
    }
}
