using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VectorBasisVisualizer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textInfo;

    [SerializeField] private Vector2 v;
    [SerializeField] private Vector2 i;
    [SerializeField] private Vector2 j;


    void OnDrawGizmos()
    {
        DrawVectorBasis();

        Gizmos.color = Color.yellow;
        Vector2 vTransformed = v.x * i + v.y * j;
        GizmosUtils.DrawVectorAtOrigin(vTransformed);
        
        textInfo.text = $"v = {v}\ni = {i}\nj = {j}\nLD = {MathUtils.IsLinearDependent(i, j)}";
    }

    private void DrawVectorBasis()
    {
        Gizmos.color = Color.red;
        GizmosUtils.DrawVectorAtOrigin(i);

        Gizmos.color = Color.green;
        GizmosUtils.DrawVectorAtOrigin(j);
    }
}
