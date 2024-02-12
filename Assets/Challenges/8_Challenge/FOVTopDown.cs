using Unity.VisualScripting;
using UnityEngine;

public class FOVTopDown : MonoBehaviour
{
    [SerializeField] private float viewDistance;
    [SerializeField] [Range(0, 180)] private float viewAngle;
    [SerializeField] private Transform target;

    void OnDrawGizmos()
    {
        var leftDirLine = Quaternion.Euler(0, -viewAngle * 0.5f, 0) * transform.forward;
        var rightDirLine = Quaternion.Euler(0, viewAngle * 0.5f, 0) * transform.forward;

        Gizmos.color = CanSeeTarget(target) ? Color.green : Color.red;

        Gizmos.DrawRay(transform.position, leftDirLine * viewDistance);
        Gizmos.DrawRay(transform.position, rightDirLine * viewDistance);

        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }

    private bool CanSeeTarget(Transform target)
    {
        if(target == null) return false;

        var toTarget = target.position - transform.position;
        if(toTarget.sqrMagnitude > viewDistance * viewDistance) return false;

        var dot = Vector3.Dot(transform.forward, toTarget);
        if(dot < 0) return false;

        var coshalfToTarget = dot / (toTarget.magnitude * transform.forward.magnitude);
        var halfAngleToTarget = Mathf.Acos(coshalfToTarget) * Mathf.Rad2Deg;
        return halfAngleToTarget <= (viewAngle * 0.5f);
    }

}
