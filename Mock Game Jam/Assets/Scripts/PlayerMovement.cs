using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float walkSpeed;
	public float sprintSpeed;

	private float speed;

	public static bool canMove = true;

	private void FixedUpdate()
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
			float xMove = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * speed;
			float yMove = Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime * speed;

			Debug.Log(xMove);

			//Apply Input
			transform.Translate(new Vector3(xMove, yMove), Space.World);

			//Rotate camera to face the cursor
			RotateToCursor();
		}
	}

	private void RotateToCursor()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

		transform.up = direction;
	}
}
