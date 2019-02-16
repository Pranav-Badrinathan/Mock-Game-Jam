using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class MirrorReflect : MonoBehaviour
{
	//the attached line renderer
	private LineRenderer lineRenderer;

	//the number of reflections
	public int nReflections = 2;
	//max length
	public float maxLength = 100f;

	//private int pointCount;
	void Awake()
	{
		//get the attached LineRenderer component  
		lineRenderer = GetComponent<LineRenderer>();
	}
	void Update()
	{
		//clamp the number of reflections between 1 and int capacity  
		nReflections = Mathf.Clamp(nReflections, 1, nReflections);

		Ray2D ray = new Ray2D(transform.position, transform.right);

		//start with just the origin
		lineRenderer.positionCount = 1;
		lineRenderer.SetPosition(0, transform.position);
		float remainingLength = maxLength;
		//bounce up to n times
		for (int i = 0; i < nReflections; i++)
		{
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength);
			// ray cast
			if (hit.collider != null)
			{
				//we hit, update line renderer
				lineRenderer.positionCount += 1;
				lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
				// update remaining length and set up ray for next loop
				remainingLength -= Vector3.Distance(ray.origin, hit.point);
				ray = new Ray2D(hit.point, Vector3.Reflect(ray.direction, hit.normal));
				// break loop if we don't hit a Mirror
				if (hit.collider.tag != "Mirror")
					break;
			}
			else
			{
				// We didn't hit anything, draw line to end of ramainingLength
				lineRenderer.positionCount += 1;
				lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
				break;
			}
		}
	}
}