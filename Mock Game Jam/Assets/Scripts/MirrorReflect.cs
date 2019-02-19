using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MirrorReflect : MonoBehaviour
{
	public int maxReflectionCount = 5;
	public float maxDistance = 200;

	private LineRenderer lineRenderer;

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
		/*
        public int laserDistance = 100; //max raycasting distance
        public int laserLimit = 10; //the laser can be reflected this many times
        public LineRenderer laserRenderer; //the line renderer
        */

		int laserReflected = 1; //How many times it got reflected
		int vertexCounter = 1; //How many line segments are there
		bool loopActive = true; //Is the reflecting loop active?
		Vector2 laserDirection = transform.up; //direction of the next laser
		Vector2 lastLaserPosition = transform.position; //origin of the next laser

		lineRenderer.positionCount = 1;
		lineRenderer.SetPosition(0, transform.position);

		while (loopActive)
		{
			RaycastHit2D hit = Physics2D.Raycast(lastLaserPosition, laserDirection, maxDistance);

			if (hit)
			{
				laserReflected++;
				vertexCounter += 3;
				lineRenderer.positionCount = vertexCounter;
				lineRenderer.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));
				lineRenderer.SetPosition(vertexCounter - 2, hit.point);
				lineRenderer.SetPosition(vertexCounter - 1, hit.point);
				lastLaserPosition = hit.point;
				laserDirection = Vector3.Reflect(laserDirection, hit.normal);
			}
			else
			{
				laserReflected++;
				vertexCounter++;
				lineRenderer.positionCount = vertexCounter;
				lineRenderer.SetPosition(vertexCounter - 1, lastLaserPosition + (laserDirection.normalized * maxDistance));

				loopActive = false;
			}
			if (laserReflected > maxReflectionCount)
				loopActive = false;
		}
	}
}