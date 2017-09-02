/*
 * Author: Isaiah Mann
 * Description: Singleton Pattern
 */

using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviourExtended where T : class
{
	public static T Instance
	{
		get
		{
			return _instance;
		}
	}

	static T _instance;

	protected bool markedForDestruction
	{
		get;
		private set;
	}

	[SerializeField]
	bool preserveOnSceneLoad = true;

	protected override void Awake()
	{
		base.Awake();
		if(_instance == null)
		{
			_instance = this as T;
			if(preserveOnSceneLoad)
			{
				DontDestroyOnLoad(transform.root);
			}
		}
		else
		{
			markedForDestruction = true;
			Destroy(gameObject);
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if(_instance == this as T)
		{
			_instance = null;
		}
	}

	protected override void OnSceneLoad(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadMode)
	{
		if(!markedForDestruction)
		{
			base.OnSceneLoad(scene, loadMode);
		}
	}

}
