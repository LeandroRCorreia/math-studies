using UnityEngine;

enum HandnessOperation
{
    Left,
    Right

}

public class OrthognalSystemVisualizer : MonoBehaviour
{
    [SerializeField] private Vector3 v;
    [SerializeField] private Vector3 w;
    [SerializeField] private HandnessOperation operationHandess;

    [SerializeField] private bool ForceOrthognalSystem;

    private Vector3 HandedVector => IsLeftHand ? Vector3.Cross(v, w) : Vector3.Cross(v, w) * -1;
    private bool IsLeftHand => HandnessOperation.Left == operationHandess;


    void OnDrawGizmos()
    {
        if (ForceOrthognalSystem)
        {
            DrawOrthogonalSystem();
        }
        else
        {
            DrawVectors();
        }

    }

    private void DrawVectors()
    {
        Gizmos.color = Color.red;
        GizmosUtils.DrawVectorAtOrigin(v);
        Gizmos.color = Color.yellow;
        GizmosUtils.DrawVectorAtOrigin(w);
        Gizmos.color = Color.magenta;
        GizmosUtils.DrawVectorAtOrigin(HandedVector);
    }

    private void DrawOrthogonalSystem()
    {
        var n = HandedVector.normalized;
        var wCopy = Vector3.Cross(n, v).normalized;
        var vCopy = v.normalized;

        Gizmos.color = Color.red;
        GizmosUtils.DrawVectorAtOrigin(vCopy);
        Gizmos.color = Color.yellow;
        GizmosUtils.DrawVectorAtOrigin(wCopy);
        Gizmos.color = Color.magenta;
        GizmosUtils.DrawVectorAtOrigin(n);

    }

}
