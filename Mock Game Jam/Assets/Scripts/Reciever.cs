using UnityEngine;

public class Reciever : MonoBehaviour
{
	public Sprite recieverOff;
	public Sprite recieverOn;

	public new Light light;

	public void Trigger(bool on)
	{
		ChangeSprite(on);
		LightsOn();
	}

	private void ChangeSprite(bool onOrOff)
	{
		if(onOrOff)
			gameObject.GetComponent<SpriteRenderer>().sprite = recieverOn;
		else
			gameObject.GetComponent<SpriteRenderer>().sprite = recieverOff;
	}

	private void LightsOn()
	{
		//light.gameObject.SetActive(true);
	}
}
