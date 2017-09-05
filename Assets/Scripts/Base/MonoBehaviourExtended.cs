using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class MonoBehaviourExtended : MonoBehaviour 
{
	protected bool timePaused
	{
		get
		{
			return Time.timeScale == 0;
		}
	}
		
	protected virtual void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoad;
	}

	protected virtual void Start()
	{

	}

	protected virtual void OnDestroy()
	{

	}

	protected virtual void OnSceneLoad(Scene scene, LoadSceneMode loadMode)
	{
		OnSceneLoad(scene.buildIndex);
	}

	protected virtual void OnSceneLoad(int sceneIndex)
	{
		
	}

	protected void pauseTime()
	{
		Time.timeScale = 0;
	}

	protected void resumeTime()
	{
		Time.timeScale = 1;
	}
}
