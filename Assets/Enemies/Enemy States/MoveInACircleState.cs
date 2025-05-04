using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyState/MoveInACircleState")]
public class MoveInACircleState : EnemyState
{
    public float speed = 0f;

    public bool useEnemySpeed = true;

    public float radius = 1f;
    public int numPoints = 30;
    public bool loopThroughPoints = false;

    List<Vector3> pathPoints;

    Vector3 startingPosition;
    Vector3 destination;

    protected int currentPointIndex = 0;

    protected override void InnerEnterState()
    {
        currentPointIndex = 0;
        pathPoints = GenerateCircleMovementVectors();
        MoveToNextDestination();

        if (useEnemySpeed)
            speed = enemy.enemySpeed;
    }

    private void MoveToNextDestination()
    {
        startingPosition = enemy.transform.position;
        destination = startingPosition + pathPoints[currentPointIndex];
        Debug.Log("Destination " + currentPointIndex + ": " + destination);
        Debug.Log("Starting Position " + currentPointIndex + ": " + startingPosition);
    }

    protected override void InnerUpdateState()
    {
        UsePathDistances();
    }

    private void UsePathDistances()
    {
        Vector3 enemyPosition = enemy.transform.position;

        enemy.transform.position = Vector3.MoveTowards(enemyPosition, destination, speed);
        if (Vector3.Distance(enemy.transform.position, destination) < 0.01f)
        {
            currentPointIndex++;
            if(loopThroughPoints && currentPointIndex >= pathPoints.Count){
                currentPointIndex = 0;
            }else if(currentPointIndex >= pathPoints.Count){
                enemy.EnterState(nextState);
            }

            MoveToNextDestination();
        }
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



}