using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class MonoBehaviourExtended : MonoBehaviour 
{
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
}
