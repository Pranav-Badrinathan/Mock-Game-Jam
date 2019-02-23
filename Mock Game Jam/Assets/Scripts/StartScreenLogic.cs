using UnityEngine;

public class StartScreenLogic : MonoBehaviour
{
	public Color firstColor;
	public Color secondColor;

	private Color newColor;

	public float colorChangeSpeed;

	private void Awake()
	{
		newColor = firstColor;
	}

	private void Update()
	{
		LerpColor();
	}

	private void LerpColor()
	{
		if (gameObject.GetComponent<Camera>().backgroundColor == firstColor)
			newColor = secondColor;
		else if (gameObject.GetComponent<Camera>().backgroundColor == secondColor)
			newColor = firstColor;

		gameObject.GetComponent<Camera>().backgroundColor = Color.Lerp(gameObject.GetComponent<Camera>().backgroundColor, newColor, Time.deltaTime * colorChangeSpeed);
	}

	public void Play()
	{
		ChangeScene.LoadRespectiveScene();
	}

	public void Quit()
	{
		Application.Quit();
	}
}
