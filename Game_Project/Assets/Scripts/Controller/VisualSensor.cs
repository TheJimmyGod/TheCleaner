using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualSensor : MonoBehaviour
{
    [SerializeField]
    private float viewRaidus;
    [SerializeField]
    private float viewAngle;
    public LayerMask target;
    public LayerMask obstacleMask;

    public float angle
    {
        get { return viewAngle; }
    }

    public bool FindingTarget()
    {
        Collider[] collider = Physics.OverlapSphere(transform.localPosition, viewRaidus, target);
        if (collider.Length == 0)
            return false;
        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].transform.localPosition.y > transform.localPosition.y + 1.2f)
                continue;
            Transform targetTransform = collider[i].transform;
            Vector3 direction = (targetTransform.position - transform.position).normalized;
            if (Vector3.Dot(direction, transform.forward) > Mathf.Cos(viewAngle))
            {
                float distance = Vector3.Distance(transform.position, targetTransform.position);
                return !Physics.Raycast(transform.position, direction, distance, obstacleMask);
            }
        }
        return false;
    }
}
