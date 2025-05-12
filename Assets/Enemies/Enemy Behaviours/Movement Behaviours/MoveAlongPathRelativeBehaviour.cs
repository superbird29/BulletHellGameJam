using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Assets/EnemyBehaviour/MovementBehaviour/MoveAlongPathRelativeBehaviour")]
public class MoveAlongPathRelativeBehaviour : MoveAlongPathBehaviour
{

    [SerializeField] RelativeAxis relativeAxis;


    enum RelativeAxis
    {
        X,
        Y
    };

    protected override void UsePathPoints()
    {
        if (currentPointIndex < pathPoints.Count)
        {
            Vector3 realPoint;

            switch (relativeAxis)
            {
                case RelativeAxis.X:
                    realPoint = new(pathPoints[currentPointIndex].x + enemy.transform.position.x, 
                    pathPoints[currentPointIndex].y, 
                    0);
                    break;
                case RelativeAxis.Y:
                    realPoint = new(pathPoints[currentPointIndex].x, 
                    pathPoints[currentPointIndex].y + enemy.transform.position.y, 
                    0);
                    break;
                default:
                    realPoint = pathPoints[currentPointIndex];
                    break;
            }

            destination = realPoint;
        }
    }
}