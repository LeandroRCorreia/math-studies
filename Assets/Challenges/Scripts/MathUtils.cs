using UnityEngine;

public static class MathUtils
{

    public static void FindLineEquation(Vector3 start, Vector3 end, out float m, out float c)
    {
        var deltaX = end.x - start.x;
        var deltaY = end.y - start.y;
        m = deltaY / deltaX;
        c = -m * start.x + start.y;
    }

    public static float CalculateXOnLineIntersection(float m1, float c1, float m2, float c2)
    {
        return (c2 - c1) / (m1 - m2);
    }

    public static float CalculateYOnLineEquation(float x, float m, float c)
    {
        return m * x + c;
    }

    public static Vector3 LerpUnclamped(Vector3 from, Vector3 to, float t)
    {
        return from + (to - from) * t; 
    }

    public static float LerpUnclamped(float from, float to, float t)
    {
        return from + (to - from) * t;
    }

    public static float InverseLerp(Vector3 A, Vector3 B, Vector3 point)
    {
        return (point.x - A.x) / (B.x - A.x);
    }

    public static float CalculateSqrtDiscriminant(float a, float b, float c)
    {
        return b * b - 4 * a * c;
    }

    public static bool CircleLineIntersection(
        Vector3 startLine,
        Vector3 endLine,
        Vector3 circlePosition,
        float circleRadius,
        out Vector3 point1,
        out Vector3 point2
    )
    {
        FindLineEquation(startLine, endLine, out float lineM, out float lineC);
        var k = lineC - circlePosition.y;

        var a = lineM*lineM + 1;
        var b = 2 * lineM * k - 2 * circlePosition.x;
        var c = k * k - circleRadius * circleRadius + circlePosition.x * circlePosition.x;
        var discriminant = CalculateSqrtDiscriminant(a, b, c);

        if(discriminant < 0)
        {
            point1 = point2 = Vector3.zero;
            return false;
        }

        var sqrtDiscriminant = Mathf.Sqrt(discriminant);

        var x1 = (-b + sqrtDiscriminant) / (2 * a);
        var y1 = CalculateYOnLineEquation(x1, lineM, lineC);

        var x2 = (-b - sqrtDiscriminant) / (2 * a);
        var y2 = CalculateYOnLineEquation(x2, lineM, lineC);

        point1 = new Vector3(x1, y1);
        point2 = new Vector3(x2, y2);
        
        return true;
    }   

    public static bool IsPointInInfiniteLine(Vector3 start, Vector3 end, Vector3 point)
    {
        float t = InverseLerp(start, end, point);

        return t >= 0 && t <= 1;
    }


}
