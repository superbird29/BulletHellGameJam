using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/MovementBehaviour/MoveInASpiralBehaviour")]
public class MoveInASpiralBehaviour : BaseMovementBehaviour
{

    public float radius = 1f;
    public int numPoints = 30;

    public float downwardSpeed = .1f;

    private List<Vector3> pathPoints;

    protected int currentPointIndex = 0;

    protected override void InnerEnterBehaviour()
    {
        base.InnerEnterBehaviour();
        currentPointIndex = 0;
        pathPoints = GenerateSpiralMovementVectors();
        MoveToNextDestination();
    }

    protected override void UpdateDestination()
    {
        if (Vector3.Distance(enemy.transform.position, destination) < 0.01f)
        {
            currentPointIndex++;
            if (currentPointIndex >= pathPoints.Count)
            {
                currentPointIndex = 0;
            }
            MoveToNextDestination();
        }
    }

    protected List<Vector3> GenerateSpiralMovementVectors()
    {
        List<Vector3> movementVectors = new List<Vector3>();
        List<Vector3> points = GenerateSpiralPoints();

        for (int i = 1; i < points.Count; i++)
        {
            movementVectors.Add(points[i] - points[i - 1]);
        }

        return movementVectors;
    }


    protected List<Vector3> GenerateSpiralPoints()
    {
        List<Vector3> points = new List<Vector3>();

        float angleStep = 2 * Mathf.PI / numPoints;
        float currentY = 0f;  

        for (int i = 0; i < numPoints; i++)
        {
            float angle = i * angleStep;
            
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            
            
            points.Add(new Vector3(x, y, 0));
            currentY -= downwardSpeed;  
        }

        return points;
    }

    private void MoveToNextDestination()
    {
        if (currentPointIndex < pathPoints.Count)
        {
            destination = enemy.transform.position + pathPoints[currentPointIndex];
        } else {
            currentPointIndex = 0;
            destination = enemy.transform.position + pathPoints[currentPointIndex];
        }
        Debug.Log(destination);
    }
}