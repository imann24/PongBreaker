using UnityEngine;
using System.Collections;

public class BrickSpawner : MonoBehaviour 
{
	public GameObject BrickPrefab;

	BrickController[] bricks;

	public Sprite DefaultBrickSprite;

	void Awake()
	{
		Init();
	}

	void Init()
	{
		Subscribe();
		GenerateListOfBricks();
	}

	void OnDestroy()
	{
		Unsubscribe();
	}
	
	void Subscribe()
	{
		EventControler.OnNamedEvent += HandleNamedEvent;
	}

	void Unsubscribe() 
	{
		EventControler.OnNamedEvent -= HandleNamedEvent;
	}

	void HandleNamedEvent(string eventName) 
	{
		if(eventName == EventList.GOAL) 
		{
			StartCoroutine(FillInBricksOnDelay());
		}
	}

	void GenerateListOfBricks() 
	{
		bricks = GetComponentsInChildren<BrickController>();
	}

	void FillInDestroyedBricks()
	{
		foreach(BrickController brick in bricks)
		{
			SpawnBrick(brick);
		}
	}

	IEnumerator FillInBricksOnDelay(float timeDelay = 0.5f) 
	{
		yield return new WaitForSeconds(timeDelay);
		FillInDestroyedBricks();
	}

	BrickController SpawnBrick(BrickController brick) 
	{
		brick.Spawn(DefaultBrickSprite);
		return brick;
	}
}
