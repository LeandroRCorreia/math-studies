using TMPro;
using UnityEngine;

[System.Serializable]
public struct Matrix2x2
{

    public Matrix2x2(float m00, float m01, float m10, float m11)
    {
        this.m00 = m00;
        this.m01 = m01;
        this.m10 = m10;
        this.m11 = m11;

    }

    public float m00;
    public float m01;

    public float m10;
    public float m11;

    public Vector2 i => new(m00, m01);
    public Vector2 j => new(m10, m11);

    public static Matrix2x2 identify => new(1, 0, 0, 1);

    public static Vector2 operator *(Matrix2x2 b, Vector2 a)
    {
        float x = b.m00 * a.x + b.m01 * a.y;
        float y = b.m10 * a.x + b.m11 * a.y;
        return new Vector2(x, y);
    }

    public static Matrix2x2 operator *(Matrix2x2 a, Matrix2x2 b)
    {
        float m00 = a.m00 * b.m00 + a.m01 * b.m10;
        float m01 = a.m00 * b.m01 + a.m01 * b.m11;
        float m10 = a.m10 * b.m00 + a.m11 * b.m10;
        float m11 = a.m10 * b.m01 + a.m11 * b.m11;

        return new Matrix2x2(m00, m01, m10, m11);
    }

}

[ExecuteAlways]
public class MatrixVisualizer : MonoBehaviour
{
    [Header("UI")]
    
    [SerializeField] private TextMeshProUGUI textInfo;

    [Space]
    
    [SerializeField] private Vector2 v;
    [SerializeField] private Matrix2x2[] matrixForTransformations;

    private static void DrawBasisAndVector(Matrix2x2 matrixResult, Vector2 vectorResult)
    {
        Gizmos.color = Color.red;
        GizmosUtils.DrawVectorAtOrigin(matrixResult.i);
        Gizmos.color = Color.green;
        GizmosUtils.DrawVectorAtOrigin(matrixResult.j);
        Gizmos.color = Color.yellow;
        GizmosUtils.DrawVectorAtOrigin(vectorResult);
    }

    void OnDrawGizmos()
    {
        Matrix2x2 matrixResult = Matrix2x2.identify;
        Vector2 vectorResult = v;

        foreach (var matrix in matrixForTransformations)
        {
            matrixResult *= matrix;
        }
        vectorResult = matrixResult * vectorResult;

        textInfo.text = $"V = {v}\n{matrixResult.i}\n{matrixResult.j}";

        DrawBasisAndVector(matrixResult, vectorResult);
    }

}
