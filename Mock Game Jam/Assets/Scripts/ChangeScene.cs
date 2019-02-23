using UnityEngine.SceneManagement;

public class ChangeScene
{
	public static void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
