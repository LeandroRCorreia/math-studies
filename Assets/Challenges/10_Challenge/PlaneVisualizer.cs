using UnityEngine;

enum PlaneVisualizerOperation
{
    none,
    isInFront,
    projectPoint,
    projectVector
}

public struct MyPlane
{

    public Vector3 point;
    public Vector3 n;

    public float Distance => Vector3.Dot(point, n);

    public MyPlane(in Vector3 p1, in Vector3 p2, in Vector3 p3)
    {
        point = p1;
        n = Vector3.Cross(p2 - p1, p3 - p1).normalized;

    }

    public bool IsInFront(in Vector3 pointFront)
    {
        return Vector3.Dot(pointFront, n) - Vector3.Dot(point, n) > 0;
    }

    public Vector3 ProjectPoint(in Vector3 point, out Vector3 projection)
    {
        var pointToProject = point - this.point;
        projection = -1 * Vector3.Dot(n, pointToProject) * n;
        var pointProject = pointToProject - Vector3.Dot(n, pointToProject) * n;


        return pointProject;
    }

    public Vector3 ProjectVector(in Vector3 vector, out Vector3 projection)
    {
        projection = -1 * Vector3.Dot(n, vector) * n;
        
        var vectorProjected = vector - Vector3.Dot(n, vector) * n;
        return vectorProjected;
    }

}

public class PlaneVisualizer : MonoBehaviour
{
    [SerializeField] private Vector3 point;

    [SerializeField] private Vector3 p1;
    [SerializeField] private Vector3 p2;
    [SerializeField] private Vector3 p3;
    [SerializeField] PlaneVisualizerOperation operation;

    [SerializeField] private float planeSize = 10;


    const float sphereRadius = 0.1f;

    void OnDrawGizmos()
    {
        MyPlane myPlane = DrawAndSetupPlaneVisualizer();
        PlaneOperation(myPlane);

    }

    private void PlaneOperation(in MyPlane myPlane)
    {
        switch (operation)
        {
            case PlaneVisualizerOperation.none:
                break;
            case PlaneVisualizerOperation.isInFront:
                IsPointInFrontOfPlane(myPlane);
                break;
            case PlaneVisualizerOperation.projectPoint:
                ProjectPointOnPlane(myPlane);
                break;
            case PlaneVisualizerOperation.projectVector:
                ProjectVectorOnPlane(myPlane);
                break;
        }

    }

    private MyPlane DrawAndSetupPlaneVisualizer()
    {
        DrawAxis();
        var myPlane = new MyPlane(p1, p2, p3);
        GizmosUtils.DrawPlane(myPlane.n, p1, Vector3.one * planeSize);
        Gizmos.color = Color.magenta;
        GizmosUtils.DrawVector(p1, myPlane.n);
        DrawPoints();

        var perpendicularVectorFromOrigin = myPlane.Distance * myPlane.n;
        Gizmos.color = Color.white;
        GizmosUtils.DrawVectorAtOrigin(perpendicularVectorFromOrigin, 1);
        return myPlane;
    }

    private static void DrawAxis()
    {
        const float AxisLenght = 1;
        Gizmos.color = Color.red;
        GizmosUtils.DrawVectorAtOrigin(Vector3.right * AxisLenght);
        Gizmos.color = Color.green;
        GizmosUtils.DrawVectorAtOrigin(Vector3.up * AxisLenght);
        Gizmos.color = Color.blue;
        GizmosUtils.DrawVectorAtOrigin(Vector3.forward * AxisLenght);
    }

    private void DrawPoints()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(p1, sphereRadius);
        Gizmos.DrawSphere(p2, sphereRadius);
        Gizmos.DrawSphere(p3, sphereRadius);
    }

    private void IsPointInFrontOfPlane(in MyPlane myPlane)
    {
        Gizmos.color = myPlane.IsInFront(point) ? Color.green : Color.red;
        Gizmos.DrawSphere(point, sphereRadius);
    }

    private void ProjectPointOnPlane(in MyPlane myPlane)
    {
        var projectedPoint = myPlane.ProjectPoint(point, out var projection);
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(point, sphereRadius);
        Gizmos.color = Color.blue;
        GizmosUtils.DrawVector(p1, projectedPoint);
        Gizmos.color = Color.green;
        GizmosUtils.DrawVector(point, projection, 4);
    }

    private void ProjectVectorOnPlane(in MyPlane myPlane)
    {
        var projected = myPlane.ProjectVector(point, out var projectionVInN);

        Gizmos.color = Color.cyan;
        GizmosUtils.DrawVector(p1, point);
        Gizmos.color = Color.red;
        GizmosUtils.DrawVector(p1 + point, projectionVInN);
        GizmosUtils.DrawVector(p1, projected);
    }

}
