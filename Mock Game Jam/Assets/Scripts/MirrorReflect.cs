using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MirrorReflect : MonoBehaviour
{
	public LayerMask layer;

	private LineRenderer lineRenderer;

	private void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		CalculateReflections(transform.position + new Vector3(1, 0), transform.right);
	}

	private void CalculateReflections(Vector3 position, Vector3 direction)
	{
		int reflectionIndex = 1;

		RaycastHit2D hit = Physics2D.Raycast(position, direction, 100, layer);
		if (hit.collider != null)
		{
			reflectionIndex++;

			lineRenderer.positionCount = reflectionIndex;
			lineRenderer.SetPosition(reflectionIndex - 1, hit.point);

			Debug.Log(hit.collider.gameObject.name);

			if (hit.transform.gameObject.layer == layer)
			{
				CalculateReflections(hit.point, hit.normal);
			}
		}
		else
		{
			return;
		}
	}
}
