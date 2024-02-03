using UnityEngine;

public static class MathUtils
{

    public static void FindLineEquation(Vector3 PointA, Vector3 PointB, out float m, out float c)
    {
        var deltaX = PointB.x - PointA.x;
        var deltaY = PointB.y - PointA.y;
        m = deltaY / deltaX;
        c = -m * PointA.x + PointA.y;
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
