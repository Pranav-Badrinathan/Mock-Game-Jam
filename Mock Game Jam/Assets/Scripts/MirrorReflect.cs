using UnityEditor;
using UnityEngine;

/*
 * Projectile reflection demonstration in Unity 3D
 * 
 * Demonstrates a projectile reflecting in 3D space a variable number of times.
 * Reflections are calculated using Raycast's and Vector3.Reflect
 * 
 * Developed on World of Zero: https://youtu.be/GttdLYKEJAM
 */
public class MirrorReflect : MonoBehaviour
{
	public int maxReflectionCount = 5;
	public float maxStepDistance = 200;

	void OnDrawGizmos()
	{
		Handles.color = Color.red;
		Handles.ArrowHandleCap(0, this.transform.position + this.transform.right, this.transform.rotation, 0.5f, EventType.Repaint);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, 0.25f);

		DrawPredictedReflectionPattern(this.transform.position + this.transform.right, this.transform.right, maxReflectionCount);
	}

	private void DrawPredictedReflectionPattern(Vector2 position, Vector2 direction, int reflectionsRemaining)
	{
		if (reflectionsRemaining == 0)
		{
			return;
		}

		Vector2 startingPosition = position;
		Ray2D ray = new Ray2D(transform.position, transform.right);

		ray = new Ray2D(position, direction);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
		if (hit)
		{
			direction = Vector3.Reflect(direction, hit.normal);
			position = hit.point;
		}
		else
		{
			position += direction * maxStepDistance;
		}

		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(startingPosition, position);

		DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
	}
}