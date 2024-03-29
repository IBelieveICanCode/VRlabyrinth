﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FieldOfView : MonoBehaviour
{
	public float viewRadius;
	[Range(0, 180)]
	public float viewAngle;

	public LayerMask TargetMask;
	public LayerMask ObstacleMask;

	[HideInInspector]
	public List<Transform> VisibleTargets = new List<Transform>();
	private Timer _timer;

	public void SetupView()
	{
		_timer = new Timer(1, FindVisibleTargets, false);
		_timer.Restart();
	}

	public GameObject ReturnTarget()
	{
		if (VisibleTargets.Count > 0)
			return VisibleTargets[0].gameObject;
		else
			return null;
	}
	void FindVisibleTargets()
	{
		VisibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, TargetMask);
		if (targetsInViewRadius == null) return;
		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);
				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask))
				{
						VisibleTargets.Add(target);
				}
			}
		}
	}

    private void OnDestroy()
    {
		_timer.Dispose();
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}