using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float walkSpeed;
	public float sprintSpeed;

	private float speed;

	public static Vector3 mirrorLocation;
	public static Vector3 eulers;

	public static bool canMove = true;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		if (canMove)
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

			Debug.Log(xMove);

			//Apply Input
			transform.position += new Vector3(xMove, yMove);

			//Setup variable for MirrorMove
			if (xMove > 0)
			{
				mirrorLocation = transform.right;
				eulers.z = 0;
			}
			else if (xMove < 0)
			{
				mirrorLocation = -transform.right;
				eulers.z = 180;
			}
			else if (yMove > 0)
			{
				mirrorLocation = transform.up;
				eulers.z = 90;
			}
			else if (yMove < 0)
			{
				mirrorLocation = -transform.up;
				eulers.z = 270;
			}
			else if (xMove > 0 && yMove > 0)
			{
				mirrorLocation = transform.right + transform.up;
			}
			else if (xMove < 0 && yMove < 0)
			{
				mirrorLocation = -transform.right - transform.up;
			}
			else if (xMove > 0 && yMove < 0)
			{
				mirrorLocation = transform.right - transform.up;
			}
			else if (xMove < 0 && yMove > 0)
			{
				mirrorLocation = -transform.right + transform.up;
			}
		}
	}
}
