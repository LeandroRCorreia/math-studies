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

    public static float InverseLerp(Vector3 A, Vector3 B, Vector3 point)
    {
        return (point.x - A.x) / (B.x - A.x);
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

}
