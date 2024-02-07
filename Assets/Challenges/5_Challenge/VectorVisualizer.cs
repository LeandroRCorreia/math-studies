using TMPro;
using UnityEngine;

[System.Serializable]
public struct MyVector3
{
    public float x;
    public float y;
    public float z;

    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;

    }

    public readonly float sqrMagnitude => x * x + y * y + z * z;
    public readonly float Magnitude => Mathf.Sqrt(x * x + y * y + z * z);
    public readonly MyVector3 normalized => this * (1.0f / Magnitude);


    public static implicit operator Vector3(in MyVector3 a) => new(a.x, a.y, a.z);

    public static MyVector3 operator +(in MyVector3 a, in MyVector3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);

    public static MyVector3 operator *(in MyVector3 a, float scalar) => new(a.x * scalar, a.y * scalar, a.z * scalar);
    public static MyVector3 operator *(float scalar, in MyVector3 a) => new(a.x * scalar, a.y * scalar, a.z * scalar);

    public override readonly string ToString()
    {
        return $"({x}, {y}, {z})";
    }

}

enum Operation
{
    None = -1,
    Add = 0,
    Scale = 1,
    Normalize = 2
    
}

public class VectorVisualizer : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField] TextMeshProUGUI infoText;

    [Header("Values")]
    [Space]

    [SerializeField] MyVector3 v;
    [SerializeField] MyVector3 w;

    [Space]

    [SerializeField] private float scaler;
    [SerializeField] private Operation operationVector;

    private void NoneOperation()
    {
        Gizmos.color = Color.red;
        GizmosUtils.DrawLine(new Vector3(v.x, 0, 0), new Vector3(v.x, 0, v.z), 1);

        Gizmos.color = Color.green;
        GizmosUtils.DrawLine(new Vector3(v.x, 0, v.z), new Vector3(v.x, v.y, v.z), 1);

        Gizmos.color = Color.blue;
        GizmosUtils.DrawLine(new Vector3(0, 0, v.z), new Vector3(v.x, 0, v.z), 1);

        infoText.text = $"Magnitude: {v.sqrMagnitude}";

    }

    private void AddOperation()
    {
        Gizmos.color = Color.cyan;
        GizmosUtils.DrawVectorAtOrigin(w);

        Gizmos.color = Color.gray;
        GizmosUtils.DrawLine(w, v);

        infoText.text = $"result {v + w}";

    }

    private void ScaleOperation()
    {
        MyVector3 magnitudeVector = v * scaler;
        
        Gizmos.color = Color.cyan;
        GizmosUtils.DrawVectorAtOrigin(magnitudeVector);

        infoText.text = $"Scaled Magnitude: {magnitudeVector.Magnitude}";
    }

    private void NormalizeOperation()
    {
        Gizmos.color = Color.cyan;
        GizmosUtils.DrawVectorAtOrigin(v.normalized);

        infoText.text = $"Normalized Magnitude: {v.normalized.sqrMagnitude}";
    }

    private static void DrawVectorBasis()
    {
        var axisX = new MyVector3(5, 0, 0);
        var axisY = new MyVector3(0, 5, 0);
        var axisZ = new MyVector3(0, 0, 5);

        Gizmos.color = Color.red;
        GizmosUtils.DrawVectorAtOrigin(axisX);
        Gizmos.color = Color.green;
        GizmosUtils.DrawVectorAtOrigin(axisY);
        Gizmos.color = Color.blue;
        GizmosUtils.DrawVectorAtOrigin(axisZ);
    }

    void OnDrawGizmos()
    {
        DrawVectorBasis();
        
        Gizmos.color = Color.yellow;
        GizmosUtils.DrawVectorAtOrigin(v);

        switch(operationVector)
        {
            case Operation.None:
                NoneOperation();
                break;
            case Operation.Add:
                AddOperation();
                break;
            case Operation.Scale:
                ScaleOperation();
                break;
            case Operation.Normalize:
                NormalizeOperation();
                break;
        }

    }

}
