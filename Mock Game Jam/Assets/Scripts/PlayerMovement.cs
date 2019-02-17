using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float walkSpeed;
	public float sprintSpeed;

	private float speed;

	public  static Vector3 mirrorLocation;
	
	private void Update()
	{
		//Set Speed
		if (Input.GetKey(KeyCode.LeftShift))
		{
			speed = sprintSpeed;
		}
		else
		{
			speed = walkSpeed;
		}

		//Get Player Input
		float xMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
		float yMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

		//Apply Input
		transform.position += new Vector3(xMove, yMove);

		//Setup variable for MirrorMove
		if (xMove > 0)
			mirrorLocation = Vector3.right;
		else if (xMove < 0)
			mirrorLocation = -Vector3.right;
		else if (yMove > 0)
			mirrorLocation = Vector3.up;
		else if (yMove < 0)
			mirrorLocation = -Vector3.up;
		else if (xMove > 0 && yMove > 0)
			mirrorLocation = Vector3.right + Vector3.up;
		else if (xMove < 0 && yMove < 0)
			mirrorLocation = -Vector3.right - Vector3.up;
		else if (xMove > 0 && yMove < 0)
			mirrorLocation = Vector3.right - Vector3.up;
		else if (xMove < 0 && yMove > 0)
			mirrorLocation = -Vector3.right + Vector3.up;
	}
}
