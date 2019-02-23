using UnityEngine.SceneManagement;

public class ChangeScene
{
	public static void LoadRespectiveScene()
	{
		if (SceneManager.GetActiveScene().buildIndex == 5)
			LoadStartScene();
		else
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public static void LoadStartScene()
	{
		SceneManager.LoadScene(0);
	}
}
