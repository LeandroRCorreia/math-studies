using TMPro;
using UnityEngine;

public class LineVisualizer : MonoBehaviour
{

    [SerializeField] private Transform PointA;
    [SerializeField] private Transform PointB;
    [SerializeField] private float spheresRadius = 5f;
    [SerializeField] private TextMeshProUGUI lineEquationTxt;



    private Vector2 A => PointA.position;
    private Vector2 B => PointB.position;


    void OnDrawGizmos()
    {
        DrawPoints();

        MathUtils.FindLineEquation(A, B, out float m, out float c);

        const int maxDecimal = 3;
        var angleFormated = (Mathf.Atan(m) * Mathf.Rad2Deg).ToString("F" + maxDecimal);
        lineEquationTxt.text = $"y = {m.ToString("F" + maxDecimal)}x + {c.ToString("F" + maxDecimal)} (Angle => {angleFormated})";

        Gizmos.color = Color.white;
        Gizmos.DrawLine(A, new(B.x, A.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(A, B);

    }

    private void DrawPoints()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(A, spheresRadius);
        Gizmos.DrawSphere(B, spheresRadius);
    }

}
