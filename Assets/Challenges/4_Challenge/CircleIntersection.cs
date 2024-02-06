using System;
using Unity.VisualScripting;
using UnityEngine;

public class CircleIntersection : MonoBehaviour
{
    [SerializeField] private Transform circleTransform;
    [SerializeField] private float circleRadius = 5f;

    [SerializeField] private Transform startLine;
    [SerializeField] private Transform endLine;

    private Vector2 StartLine => startLine.position;
    private Vector2 EndLine => endLine.position;

    private Vector2 circlePosition => circleTransform.position;

    private const float linePointIntersectionRadius = 0.1f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(circleTransform.position, circleRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(StartLine, EndLine);


        if(MathUtils.CircleLineIntersection(StartLine, EndLine, circlePosition, circleRadius, out var point1, out var point2))
        {
            
            var isP1Valid = MathUtils.IsPointInInfiniteLine(StartLine, EndLine, point1);
            var isP2Valid = MathUtils.IsPointInInfiniteLine(StartLine, EndLine, point2);

            if(isP1Valid)
            {
                Gizmos.DrawSphere(point1, linePointIntersectionRadius);
                
            }
            if(isP2Valid)
            {
                Gizmos.DrawSphere(point2, linePointIntersectionRadius);

            }

        }
 
    }


}
