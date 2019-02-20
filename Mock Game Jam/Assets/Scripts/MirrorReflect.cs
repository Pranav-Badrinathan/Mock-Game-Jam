using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MirrorReflect : MonoBehaviour
{
	public int maxReflectionCount = 5;
	public float maxDistance = 200;

	private LineRenderer lineRenderer;

	public new string tag;

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		DrawReflection();
	}

	private void DrawReflection()
	{
		int reflections = 1; //How many times it got reflected
		int vertexCounter = 1; //How many line segments are there
		bool loopActive = true; //Is the reflecting loop active?
		Vector2 lineDirection = -transform.right; //direction of the next laser
		Vector2 lastLinePosition = transform.position; //origin of the next laser

		lineRenderer.positionCount = 1;
		lineRenderer.SetPosition(0, transform.position);

		while (loopActive)
		{
			RaycastHit2D hit = Physics2D.Raycast(lastLinePosition, lineDirection, maxDistance);

			if (hit)
			{
				reflections++;
				vertexCounter += 3;
				lineRenderer.positionCount = vertexCounter;
				lineRenderer.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hit.point, lastLinePosition, 0.01f));
				lineRenderer.SetPosition(vertexCounter - 2, hit.point);
				lineRenderer.SetPosition(vertexCounter - 1, hit.point);
				lastLinePosition = hit.point;
				lineDirection = Vector3.Reflect(lineDirection, hit.normal);

				if (hit.collider.gameObject.tag != tag)
				{
					break;
				}
			}
			else
			{
				reflections++;
				vertexCounter++;
				lineRenderer.positionCount = vertexCounter;
				lineRenderer.SetPosition(vertexCounter - 1, lastLinePosition + (lineDirection.normalized * maxDistance));

				loopActive = false;
			}
			if (reflections > maxReflectionCount)
				loopActive = false;
		}
	}
}