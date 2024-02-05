using UnityEngine;

public class LineIntersectionVisualizer : MonoBehaviour
{

    [SerializeField] private Transform PointA;
    [SerializeField] private Transform PointB;

    [SerializeField] private Transform PointC;
    [SerializeField] private Transform PointD;


    [SerializeField] private float playerPathT = 0.0f;

    private const float radiusSphere = 0.1f;

    private Vector2 A => PointA.position;
    private Vector2 B => PointB.position;
    private Vector2 C => PointC.position;
    private Vector2 D => PointD.position;
    

    void OnDrawGizmos()
    {
        Vector2 wallIntersectionPoint = GetWallIntersectionPoint();
        var wallIntersectionPointT = MathUtils.InverseLerp(A, B, wallIntersectionPoint);

        playerPathT = Mathf.Clamp(playerPathT, 0.0f, wallIntersectionPointT);
        var playerPath = MathUtils.LerpUnclamped(A, B, playerPathT);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(playerPath, radiusSphere);

        Gizmos.color = Color.blue;
        DrawSpheres();
        DrawLine();
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(wallIntersectionPoint, radiusSphere);

    }

    private Vector2 GetWallIntersectionPoint()
    {
        MathUtils.FindLineEquation(A, B, out float m1, out float c1);
        MathUtils.FindLineEquation(C, D, out float m2, out float c2);

        var wallIntersectionPosition = Vector2.zero;
        if (!Mathf.Approximately(m1, m2))
        {
            var intersectionX = MathUtils.CalculateXOnLineIntersection(m1, c1, m2, c2);
            var intersectionY = MathUtils.CalculateYOnLineEquation(intersectionX, m1, c1);
            wallIntersectionPosition = new Vector3(intersectionX, intersectionY);
        }

        return wallIntersectionPosition;
    }

    private void DrawSpheres()
    {

        //Line Spheres
        Gizmos.DrawSphere(A, radiusSphere);
        Gizmos.DrawSphere(B, radiusSphere);
        Gizmos.DrawSphere(C, radiusSphere);
        Gizmos.DrawSphere(D, radiusSphere);
    }

    private void DrawLine()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(A, B);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(C, D);
    }

}
