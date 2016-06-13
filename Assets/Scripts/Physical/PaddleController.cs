using UnityEngine;
using System.Collections;

public class PaddleController : PhysicalObjectController {
	public PlayerID Player;

	float speed = Global.BASE_PADDLE_SPEED;

	void FixedUpdate () {
		UpdateVelocity();
	}
		
	void UpdateVelocity () {
		rigibody.velocity = new Vector2(0, Input.GetAxis(CastingUtil.IDToString(Player)) * speed);
	}
}
