using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/MovementBehaviour/MoveInACircleBehaviour")]
public class MoveInACircleBehaviour : BaseMovementBehaviour
{

    public float radius = 1f;
    public int numPoints = 30;

    public bool loopThroughPoints;

    private List<Vector3> pathPoints;

    protected int currentPointIndex = 0;

    protected override void InnerEnterBehaviour()
    {
        base.InnerEnterBehaviour();
        currentPointIndex = 0;
        pathPoints = GenerateCircleMovementVectors();
        MoveToNextDestination();
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
            MoveToNextDestination();
        }
    }

    protected override bool ShouldMoveToNextBehaviour()
    {
        return !loopThroughPoints && currentPointIndex >= pathPoints.Count;
    }

    protected List<Vector3> GenerateCircleMovementVectors()
    {
        List<Vector3> movementVectors = new List<Vector3>();
        List<Vector3> points = GenerateCirclePoints();

        for (int i = 1; i < points.Count; i++)
        {
            movementVectors.Add(points[i] - points[i - 1]);
        }

        movementVectors.Add(points[0] - points[points.Count - 1]);

        return movementVectors;
    }


    protected List<Vector3> GenerateCirclePoints()
    {
        List<Vector3> points = new List<Vector3>();

        float angleStep = 2 * Mathf.PI / numPoints;

        for (int i = 0; i < numPoints; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            points.Add(new Vector3(x, y, 0f));
        }

        return points;
    }

    private void MoveToNextDestination()
    {
        if (currentPointIndex < pathPoints.Count)
        {
            destination = enemy.transform.position + pathPoints[currentPointIndex];
        }
    }
}