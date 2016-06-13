using UnityEngine;
using System.Collections;

public class PuckController : PhysicalObjectController {

	// Use this for initialization
	void Start () {
		RandomStartingVelocity();
	}

	void FixedUpdate () {
		CapSpeed();
	}

	void CapSpeed () {
		rigibody.velocity = Vector2Util.Clamp(rigibody.velocity, Global.MAX_PUCK_SPEED, -Global.MAX_PUCK_SPEED);
	}

	void PreventPerpendicularPaths () {
		rigibody.velocity = Vector2Util.MakeDiagonal(rigibody.velocity);
	}

	void RandomStartingVelocity () {
		rigibody.velocity = new Vector2 (
			Random.Range (0, Global.MAX_PUCK_STARTING_SPEED),
			Random.Range (0, Global.MAX_PUCK_STARTING_SPEED)
		);
	}

	// TODO: Find a way to prevent straight horizontal/vertical paths
}
