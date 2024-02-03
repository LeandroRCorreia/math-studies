using UnityEngine;

public class LerpVisualizer : MonoBehaviour
{

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private float sphereRadius = 2f;

    [SerializeField] private float lerpVisualizerT;

    [SerializeField] private float infiniteLineEquation = 50f;

    private Vector2 A => pointA.position;
    private Vector2 B => pointB.position;

    void OnDrawGizmos()
    {
        DrawInfiniteLines();

        DrawSideLines();
        DrawSideSpheres();
        
        DrawLineBeetwenPoints();

        DrawLerpedSphere();

        DrawLinePoints();
    }

    private void DrawSideSpheres()
    {
        Gizmos.color = Color.white;
        var lerpX = MathUtils.LerpUnclamped(A.x, B.x, lerpVisualizerT);
        var lerpY = MathUtils.LerpUnclamped(A.y, B.y, lerpVisualizerT);

        Gizmos.DrawSphere(new Vector2(lerpX, A.y), sphereRadius);
        Gizmos.DrawSphere(new Vector2(A.x, lerpY), sphereRadius);
    }

    private void DrawSideLines()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(A, new Vector2(B.x, A.y));
        Gizmos.DrawLine(A, new Vector2(A.x, B.y));
    }

    private void DrawLerpedSphere()
    {
        Gizmos.color = Color.green;
        Vector3 lerpPosition = MathUtils.LerpUnclamped(A, B, lerpVisualizerT);
        Gizmos.DrawSphere(lerpPosition, sphereRadius);
    }

    private void DrawLineBeetwenPoints()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(A, B);
    }

    private void DrawLinePoints()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(A, sphereRadius);
        Gizmos.DrawSphere(B, sphereRadius);
    }

    private void DrawInfiniteLines()
    {
        Gizmos.color = Color.white;
        Vector3 infiniteLineEquationA = MathUtils.LerpUnclamped(A, B, infiniteLineEquation);
        Vector3 infiniteLineEquationB = MathUtils.LerpUnclamped(A, B, -infiniteLineEquation);
        Gizmos.DrawLine(A, infiniteLineEquationA);
        Gizmos.DrawLine(B, infiniteLineEquationB);
    }

}
