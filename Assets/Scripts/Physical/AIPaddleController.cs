/*
 * Author: Isaiah Mann
 * Description: AI behaviour of paddle
 */

using System.Collections.Generic;
using UnityEngine;

public class AIPaddleController : PaddleController
{
	Game game;
	Queue<float> puckPositions = new Queue<float>();

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
	}

	void Update()
	{
		if(game.Running)
		{
			matchPuckPosition();
		}
	}

	void matchPuckPosition()
	{
		if(shouldFollowPuck(puck))
		{
			puckPositions.Enqueue(puck.GetYPosition());
			if(puckPositions.Count > dampening)
			{
				Vector3 position = transform.localPosition;	
				position.y = Mathf.Lerp(puckPositions.Dequeue(), position.y, slowdown);
				transform.localPosition = position;
			}
		}
	}

	bool shouldFollowPuck(PuckController puck)
	{
		if(puck && puck.IsAlive)
		{
			float puckXPosition = puck.GetXPosition();
			return inRange >= Mathf.Abs(puckXPosition - transform.position.x) && !isPuckInGoal(puck);
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
}
