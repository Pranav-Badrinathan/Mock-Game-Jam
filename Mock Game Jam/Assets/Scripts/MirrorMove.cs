using UnityEngine;

public class MirrorMove : MonoBehaviour
{
	public float detectionSphereRadius;
	public LayerMask detectionMask;

	private Transform mirrorTransform = null;
	private bool holdingMirror;

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

			if (mirrorTransform != null && !holdingMirror)
			{
				//Set mirror's location and rotation
				mirrorTransform.rotation = transform.rotation;
				mirrorTransform.position = transform.position + (PlayerMovement.mirrorLocation * 0.5f);

				//update holding Mirror
				holdingMirror = true;

				Debug.Log("in");
			}
			else if (mirrorTransform != null && holdingMirror)
			{
				mirrorTransform = null;
				holdingMirror = false;
			}
		}

		if (mirrorTransform != null)
		{
			//Set mirror's location and rotation
			mirrorTransform.rotation = transform.rotation;
			mirrorTransform.position = transform.position + (PlayerMovement.mirrorLocation * 0.5f);
		}
	}

	private Transform GetMirror()
	{
		Collider2D[] mirrors = Physics2D.OverlapCircleAll(transform.position, detectionSphereRadius, detectionMask);

		if (mirrors.Length > 0)
		{
			Debug.Log("returning");
			return GetClosestMirror(mirrors);
		}
		else
		{
			Debug.Log("null?");
			return null;
		}
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
			if (dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = mirror.transform;
			}
		}

		return bestTarget;
	}
}
