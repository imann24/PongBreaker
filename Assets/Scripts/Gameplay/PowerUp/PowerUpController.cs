/*
 * Author: Isaiah Mann
 * Description: Handles spawning logic for powerup controller
 */

using System;
using System.Collections.Generic;

using UnityEngine;

public class PowerUpController : SingletonBehaviour<PowerUpController>
{
	[SerializeField]
	Transform powerUpParent;

	[SerializeField]
	float percentChanceSpawnPowerUp = 0.15f;

	[SerializeField]
	GameObject[] powerUpPrefabs;

	WeightedRandomBuffer<PowerUpBehaviour> powerUpBuffer;

	Dictionary<Type, Stack<PowerUpBehaviour>> spawnPools = new Dictionary<Type, Stack<PowerUpBehaviour>>();

	HashSet<PowerUpBehaviour> livePowerUps = new HashSet<PowerUpBehaviour>();

	protected override void Awake()
	{
		base.Awake();
		powerUpBuffer = createPowerUpBuffer();
	}

	WeightedRandomBuffer<PowerUpBehaviour> createPowerUpBuffer()
	{
		PowerUpBehaviour[] powerUps = new PowerUpBehaviour[powerUpPrefabs.Length];
		float[] weights = new float[powerUpPrefabs.Length];
		for(int i = 0; i < powerUpPrefabs.Length; i++)
		{
			powerUps[i] = powerUpPrefabs[i].GetComponent<PowerUpBehaviour>();
			weights[i] = powerUps[i].GetSpawnWeight;
		}
		return new WeightedRandomBuffer<PowerUpBehaviour>(powerUps, weights);
	}

	public bool ShouldSpawnPowerUp()
	{
		return UnityEngine.Random.Range(0f, 1.0f) <= percentChanceSpawnPowerUp;
	}

	public PowerUpBehaviour SpawnPowerUp(BrickController brick, PaddlePosition towardsPlayer)
	{
		PowerUpBehaviour powerUpPrefab = powerUpBuffer.GetRandom();
		PowerUpBehaviour powerUp;
		if(tryGetFromSpawnPool(powerUpPrefab.GetType(), out powerUp))
		{
			powerUp.gameObject.SetActive(true);
		}
		else
		{
			powerUp = Instantiate(powerUpBuffer.GetRandom(), powerUpParent);
		}
		powerUp.Place(brick);
		powerUp.Move(towardsPlayer);
		livePowerUps.Add(powerUp);
		return powerUp;
	}

	public void AddToSpawnPool(PowerUpBehaviour powerUp)
	{
		Stack<PowerUpBehaviour> powerUps;
		powerUp.gameObject.SetActive(false);
		if(!spawnPools.TryGetValue(powerUp.GetType(), out powerUps))
		{
			powerUps = new Stack<PowerUpBehaviour>();
			spawnPools[powerUp.GetType()] = powerUps;
		}
		livePowerUps.Remove(powerUp);
		powerUps.Push(powerUp);
	}

	public HashSet<PowerUpBehaviour> GetLivePowerUps()
	{
		return livePowerUps;
	}

	protected override void HandleNamedEvent(string eventName)
	{
		base.HandleNamedEvent(eventName);
		if(Global.GOAL.Equals(eventName))
		{
			despawnAllActivePowerUps();
		}
	}

	void despawnAllActivePowerUps()
	{
		HashSet<PowerUpBehaviour> powerUpsToDespawn = new HashSet<PowerUpBehaviour>(livePowerUps);
		foreach(PowerUpBehaviour powerUp in powerUpsToDespawn)
		{
			AddToSpawnPool(powerUp);
		}
	}

	bool tryGetFromSpawnPool(Type powerUpType, out PowerUpBehaviour powerUp)
	{
		Stack<PowerUpBehaviour> powerUps;
		if(spawnPools.TryGetValue(powerUpType, out powerUps) && powerUps.Count > 0)
		{
			powerUp = powerUps.Pop();
		}
		else
		{
			powerUp = null;
		}
		return powerUp != null;
	}
}
