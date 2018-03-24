/*
 * Author: Isaiah Mann
 * Description: AI behaviour of paddle
 */

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIPaddleController : PaddleController
{
	PowerUpController powerUps;
	Game game;
	Queue<float> travelPositions = new Queue<float>();

	[Header("Tuning")]
	[SerializeField]
	int dampening = 7;
	[SerializeField]
	float slowdown = 0.8f;
	[SerializeField]
	float inRange = 6.25f;

	protected override void Start()
	{
		base.Start();
		game = StateController.Instance.CurrentGame;
		powerUps = PowerUpController.Instance;
	}

	protected override void Update()
	{
		base.Update();
		if(game.Running)
		{
			followHighestPriorityObject();
		}
	}

	void followHighestPriorityObject()
	{
		PuckController closestPuck = getClosestPuck();
		PowerUpBehaviour powerUp;
		if(tryGetPowerUpToFollow(closestPuck, out powerUp))
		{
			followObject(powerUp);
		}
		else if(shouldFollowPuck(closestPuck))
		{
			followObject(closestPuck);	
		}
	}

	void followObject(PhysicalObjectController physicalObject)
	{
		travelPositions.Enqueue(physicalObject.GetYPosition());
		if(travelPositions.Count > dampening)
		{
			Vector3 position = transform.localPosition;	
			position.y = Mathf.Lerp(travelPositions.Dequeue(), position.y, slowdown);
			transform.localPosition = position;
		}
	}

	bool shouldFollowPuck(PuckController puck)
	{
		if(puck && puck.IsAlive)
		{
			return inRange >= getXDistanceFrom(puck) && !isPuckInGoal(puck);
		}
		else
		{
			return false;
		}
	}

	bool isPuckInGoal(PuckController puck) {
		return Mathf.Abs(puck.GetXPosition() - myGoal.transform.position.x) > 
			Mathf.Abs(transform.position.x - myGoal.transform.position.x);
	}

	bool tryGetPowerUpToFollow(PuckController puck, out PowerUpBehaviour powerUpMatch)
	{
		PhysicalObjectController physicalObject;
		if(tryGetClosestInstance(powerUps.GetLivePowerUps().ToList().ConvertAll(powerUp => powerUp as PhysicalObjectController), out physicalObject, getXDistanceFrom(puck)) &&
		   physicalObject is PowerUpBehaviour)
		{
			powerUpMatch = physicalObject as PowerUpBehaviour;
			return true;
		}
		else
		{
			powerUpMatch = null;
			return false;
		}
	}
}
