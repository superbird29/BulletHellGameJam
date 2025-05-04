using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyState/MoveAlongPathState")]
public class MoveAlongPathState : EnemyState
{
    public float speed = 0f;

    public bool useEnemySpeed = true;

    public List<Vector3> pathPoints;

    protected int currentPointIndex = 0;
    protected override void InnerEnterState()
    {
        currentPointIndex = 0;
        if (useEnemySpeed)
            speed = enemy.enemySpeed;
    }

    protected override void InnerUpdateState()
    {
        Debug.Log("Moving!");

        if (currentPointIndex < pathPoints.Count)
        {
            Vector3 target = pathPoints[currentPointIndex];
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, speed);

            if (Vector3.Distance(enemy.transform.position, target) < 0.01f)
            {
                currentPointIndex++;
            }
        }
    }
}