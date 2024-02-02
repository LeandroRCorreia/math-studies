using UnityEngine;

public static class FIeldOfViewUtils2D
{

    public static bool IsInsideFOV2D(Vector2 fovOrigin, Vector2 target, float viewDistance, float viewAngle)
    {
        if(target.y < fovOrigin.y)
        {
            return false;
        }

        var adjacent = target.x - fovOrigin.x;
        var opposite = target.y - fovOrigin.y;
        var hypotenuse = Mathf.Sqrt((adjacent * adjacent) + (opposite * opposite));

        if(hypotenuse > viewDistance)
        {
            return false;
        }

        var sinAngle = Mathf.Abs(adjacent) / hypotenuse;
        var sinHalfViewAngle = Mathf.Sin(viewAngle * Mathf.Deg2Rad * 0.5f );

        return sinAngle <= sinHalfViewAngle;
    }    
    
    
}
