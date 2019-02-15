using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float walkSpeed;
	public float sprintSpeed;

	private float speed;

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
	}
}
