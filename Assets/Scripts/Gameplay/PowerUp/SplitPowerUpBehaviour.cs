/*
 * Author: Isaiah Mann
 * Description: Power Up which splits into multiple pucks
 */

using UnityEngine;

public class SplitPowerUpBehaviour : PowerUpBehaviour
{
	[SerializeField]
	int numberOfPucks = 3;

	public override void Use(PaddleController player)
	{
		base.Use(player);
		int countToSpawn = numberOfPucks - 1;
		gameplay.SpawnPucks(player.ClosestPuck.transform.position, countToSpawn);
	}
}
