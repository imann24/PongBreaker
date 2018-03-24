/*
 * Author: Isaiah Mann
 * Description: Power Up which which makes the puck stick to the paddle
 */

using UnityEngine;

public class StickPowerUpBehaviour : PowerUpBehaviour
{
	[SerializeField]
	float puckLaunchSpeed = 25.0f;

	public override void Use(PaddleController player)
	{
		PuckController puck = player.ClosestPuck;
		puck.StopMotion();
		player.SetObjectDetachSpeed(puckLaunchSpeed);
		player.Attach(puck);
	}
}
