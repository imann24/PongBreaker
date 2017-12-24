using UnityEngine;
using System.Collections;

public class BrickSpawner : MonoBehaviourExtended 
{
	public GameObject BrickPrefab;

	BrickController[] bricks;

	public Sprite DefaultBrickSprite;

	protected override void Awake()
	{
		base.Awake();
		Init();
	}

	void Init()
	{
		GenerateListOfBricks();
	}


	protected override void HandleNamedEvent(string eventName) 
	{
		base.HandleNamedEvent(eventName);
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
