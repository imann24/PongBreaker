/*
 * Author: Isaiah Mann
 * Description: Power Up which makes the paddle wider than usual
 */

using System.Collections;

using UnityEngine;

public class StretchPowerUpBehaviour : PowerUpBehaviour
{
	[SerializeField]
	float scaleFactor = 2.0f;

	public override void Use(PaddleController player)
	{
		base.Use(player); 
		IEnumerator scaleCoroutine = descaleAfter(player, player.ScaleX, duration);
		player.ScaleX = scaleFactor;
		player.StartCoroutine(scaleCoroutine);
	}


	IEnumerator descaleAfter(PaddleController player, float previousScaleX, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		player.ScaleX = previousScaleX;
	}
}
