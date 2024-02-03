using UnityEngine;

public class FieldOfView2D : MonoBehaviour
{
    [SerializeField] private float viewAngle = 3f;

    [Range(0f, 180f)]
    [SerializeField] private float viewDistance = 60f;
    [SerializeField] private Transform target;


    void OnDrawGizmos()
    {
        var isInside = FIeldOfViewUtils2D.IsInsideFOV2D(transform.position, target.position, viewDistance, viewAngle);
        Color colorField = isInside ? Color.green : Color.red;

        Gizmos.color = colorField;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
        DrawFieldOfViewLine();

    }

    private void DrawFieldOfViewLine()
    {
        var deltaX = Mathf.Sin(viewAngle * Mathf.Deg2Rad * 0.5f) * viewDistance;
        var deltaY = Mathf.Cos(viewAngle * Mathf.Deg2Rad * 0.5f) * viewDistance;

        var leftLineEnd = new Vector3(-deltaX, deltaY) + transform.position;
        var rightLineEnd = new Vector3(deltaX, deltaY) + transform.position;

        Gizmos.DrawLine(transform.position, leftLineEnd);
        Gizmos.DrawLine(transform.position, rightLineEnd);
    }

}
