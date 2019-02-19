﻿using UnityEngine;

public class MirrorMove : MonoBehaviour
{
	public float detectionSphereRadius;
	public LayerMask detectionMask;

	private Transform mirrorTransform = null;
	private short holdingMirrorState = -1;
	private bool turning;

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, detectionSphereRadius);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			mirrorTransform = GetMirror();
			if (mirrorTransform != null)
			{
				if (holdingMirrorState == -1)
				{
					//Set mirror's location and rotation
					mirrorTransform.rotation = transform.rotation;
					mirrorTransform.position = transform.position + (transform.right * 0.5f);

					//transform.up = Vector3.up;
					mirrorTransform.parent = transform;

					//update holding Mirror
					holdingMirrorState = 0;
				}
				else if (holdingMirrorState == 1)
				{
					mirrorTransform = null;
					PlayerMovement.canMove = true;
					turning = false;

					holdingMirrorState = -1;
				}
				else if (holdingMirrorState == 0)
				{
					holdingMirrorState = 1;
					turning = true;

					mirrorTransform.parent = null;
				}
			}
		}

		if (turning)
		{
			PlayerMovement.canMove = false;
			FaceMirror(mirrorTransform);
		}
	}

	private Transform GetMirror()
	{
		Collider2D[] mirrors = Physics2D.OverlapCircleAll(transform.position, detectionSphereRadius, detectionMask);

		if (mirrors.Length > 0)
		{
			return GetClosestMirror(mirrors);
		}
		else
		{
			return null;
		}
	}

	private void FaceMirror(Transform target)
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

		target.up = direction;
	}

	private Transform GetClosestMirror(Collider2D[] mirrors)
	{
		Transform bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;

		Vector3 currentPosition = transform.position;

		foreach (Collider2D mirror in mirrors)
		{
			Vector3 directionToTarget = mirror.transform.position - currentPosition;

			float dSqrToTarget = directionToTarget.sqrMagnitude;
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = mirror.transform;
			}
		}

		return bestTarget;
	}
}
