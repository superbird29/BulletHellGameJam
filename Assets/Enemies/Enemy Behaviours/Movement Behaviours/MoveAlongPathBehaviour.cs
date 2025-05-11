using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/MovementBehaviour/MoveAlongPathBehaviour")]
public class MoveAlongPathBehaviour : BaseMovementBehaviour
{

    public bool usePointsAsVectors = false;

    public bool loopThroughPoints;

    public List<Vector3> pathPoints;

    protected int currentPointIndex = 0;

    protected override void InnerEnterBehaviour()
    {
        base.InnerEnterBehaviour();
        currentPointIndex = 0;

        if (usePointsAsVectors)
        {
            List<Vector3> pathVectors = new();
            for (int i = 1; i < pathPoints.Count; i++)
            {
                pathVectors.Add(pathPoints[i] - pathPoints[i - 1]);
            }
            destination = enemyStartingPosition + pathVectors[0];
            pathPoints = pathVectors;
        }
        else{
            destination = pathPoints[0];
        }
    }

    protected override void UpdateDestination()
    {
        if (Vector3.Distance(enemy.transform.position, destination) < 0.01f)
        {
            currentPointIndex++;
            if (loopThroughPoints && currentPointIndex >= pathPoints.Count)
            {
                currentPointIndex = 0;
            }
        }
        if (usePointsAsVectors)
        {
            UsePathDistances();
        }
        else
        {
            UsePathPoints();
        }
    }

    protected override bool ShouldMoveToNextBehaviour()
    {
        return !loopThroughPoints && currentPointIndex >= pathPoints.Count;
    }

    protected virtual void UsePathDistances()
    {
        if (Vector3.Distance(enemy.transform.position, destination) < 0.01f)
        {
            if (currentPointIndex < pathPoints.Count)
            {
                destination = enemy.transform.position + pathPoints[currentPointIndex];
            }
        }
    }

    protected virtual void UsePathPoints()
    {
        if (currentPointIndex < pathPoints.Count)
        {
            destination = pathPoints[currentPointIndex];
        }
    }
}