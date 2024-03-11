using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VisionTrig : MonoBehaviour
{
    EnemyController controller;

    public float viewRadius;
    [Range(0, 360f)] public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    public Transform playerTarget;

    bool hasPlayerInSight;

    private void Start()
    {
        controller = GetComponentInParent<EnemyController>();
        StartCoroutine(FindTargetWithDelay(0.2f));
    }
    
    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    if(!hasPlayerInSight)
                    {
                        controller.SawPlayer(target);
                    }

                    hasPlayerInSight = true;
                }
            }
        }

        if (visibleTargets.Count == 0 && hasPlayerInSight)
        {
            hasPlayerInSight = false;
            controller.SwitchState("PATROL");
        }


    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.y;

        return new Vector3(MathF.Sin(angleInDegrees * Mathf.Deg2Rad), 0, MathF.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
