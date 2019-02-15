using UnityEngine;

public class MirrorMove : MonoBehaviour
{
	public float detectionSphereRadius;
	public LayerMask detectionMask;

	private void Update()
	{
		Collider2D[] mirrors = Physics2D.OverlapCircleAll(transform.position, detectionSphereRadius);

		if (mirrors != null)
		{
			Debug.Log(GetClosestMirror(mirrors));
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, detectionSphereRadius);
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
