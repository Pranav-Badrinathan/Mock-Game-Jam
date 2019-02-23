using UnityEngine;
using System.Collections;

public class Reciever : MonoBehaviour
{
	public Sprite recieverOff;
	public Sprite recieverOn;

	public Light bulbLight;
	public Light roomLight;

	public void Trigger(bool on)
	{
		ChangeSprite(on);
		LightsOn(on);
	}

	private void ChangeSprite(bool onOrOff)
	{
		if(onOrOff)
			gameObject.GetComponent<SpriteRenderer>().sprite = recieverOn;
		else
			gameObject.GetComponent<SpriteRenderer>().sprite = recieverOff;
	}

	private void LightsOn(bool onOrOff)
	{
		bulbLight.gameObject.SetActive(onOrOff);

		bool needCoroutine = true;

		if (onOrOff && needCoroutine)
		{
			StartCoroutine(MainLightTurnOn());
			needCoroutine = false;
		}
		else
		{
			roomLight.gameObject.SetActive(false);
			needCoroutine = true;
		}
	}

	private IEnumerator MainLightTurnOn()
	{
		yield return new WaitForSeconds(3.0f);
		roomLight.gameObject.SetActive(true);
		yield return new WaitForSeconds(2.0f);

		if (roomLight.gameObject.activeSelf)
		{
			ChangeScene.LoadRespectiveScene();
		}
	}
}
