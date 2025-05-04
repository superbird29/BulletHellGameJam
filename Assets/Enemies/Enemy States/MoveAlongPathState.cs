using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyState/MoveAlongPathState")]
public class MoveAlongPathState : EnemyState
{
    public float speed = 0f;

    public bool useEnemySpeed = true;

    public bool usePointsAsVectors = false;

    public bool loopThroughPoints;

    public List<Vector3> pathPoints;

    Vector3 startingPosition;
    Vector3 destination;

    protected int currentPointIndex = 0;
    protected override void InnerEnterState()
    {
        currentPointIndex = 0;

        if (usePointsAsVectors)
        {
            startingPosition = enemy.transform.position;
            List<Vector3> pathVectors = new();
            for (int i = 1; i < pathPoints.Count; i++)
            {
                pathVectors.Add(pathPoints[i] - pathPoints[i - 1]);
            }
            destination = startingPosition + pathVectors[0];
            pathPoints = pathVectors;
        }
        if (useEnemySpeed)
            speed = enemy.enemySpeed;
    }

    protected override void InnerUpdateState()
    {
        Debug.Log("Moving!");
        if (usePointsAsVectors)
        {
            UsePathDistances();
        }
        else
        {
            usePathPoints();
        }
        if (Vector3.Distance(enemy.transform.position, destination) < 0.01f)
        {
            currentPointIndex++;
            if (loopThroughPoints && currentPointIndex >= pathPoints.Count)
            {
                currentPointIndex = 0;
            }
        }
    }

    private void UsePathDistances()
    {
        Vector3 enemyPosition = enemy.transform.position;

        enemy.transform.position = Vector3.MoveTowards(enemyPosition, destination, speed);
        if (Vector3.Distance(enemy.transform.position, destination) < 0.01f)
        {
            startingPosition = enemy.transform.position;
            destination = startingPosition + pathPoints[currentPointIndex];
        }
    }

    private void usePathPoints()
    {
        if (currentPointIndex < pathPoints.Count)
        {
            destination = pathPoints[currentPointIndex];
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, destination, speed);
        }
    }
}