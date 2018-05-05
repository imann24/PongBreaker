using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class MonoBehaviourExtended : MonoBehaviour 
{
	protected const float SINGLE_VALUE = 1f;
	protected const float NONE_VALUE = 0f;

	protected bool mouseIsDown = false;

	protected bool timePaused
	{
		get
		{
			return Time.timeScale == NONE_VALUE;
		}
	}
		
    protected virtual void OnEnable()
    {
        // NOTHING
    }

	protected virtual void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoad;
	}

	protected virtual void Start()
	{
		subscribeEvents();
	}

	protected virtual void OnDestroy()
	{
		unsubscribeEvents();
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
		Time.timeScale = NONE_VALUE;
	}

	protected void resumeTime()
	{
		Time.timeScale = SINGLE_VALUE;
	}

	protected virtual void OnMouseDown()
	{
		mouseIsDown = true;
	}

	protected virtual void OnMouseUp()
	{
		mouseIsDown = false;
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{

	}

	protected virtual void HandleNamedEvent(string eventName)
	{

	}

	protected virtual void subscribeEvents()
	{
		EventController.OnNamedEvent += HandleNamedEvent;
	}

	protected virtual void unsubscribeEvents()
	{
		EventController.OnNamedEvent -= HandleNamedEvent;
	}
}
