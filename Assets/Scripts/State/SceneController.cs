/*
 * Author: Isaiah Mann
 * Description:
 */

using UnityEngine.SceneManagement;

public class SceneController : SingletonBehaviour<SceneController>
{
	public void LoadScene(GameScene scene)
	{
		SceneManager.LoadScene((int) scene);
	}
}
