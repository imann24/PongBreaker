/*
 * Author: Isaiah Mann
 * Description: AI behaviour of paddle
 */

using System.Collections;
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
    [SerializeField]
    Vector2 stickPowerUpRangeTimeRange = new Vector2(1, 2);
    PaddleController opponent;

    public override void Attach(PhysicalObjectController objectToAttach)
    {
        base.Attach(objectToAttach);
        StartCoroutine(detachOnDelay());
    }

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
            if(hasPuckAttached)
            {
                followHighestPriorityObject();
            }
            else
            {
                avoidOpponent();
            }
            executeMovement();
        }
	}

    void avoidOpponent() 
    {
        if(opponent == null && (opponent = gameplay.GetOpponent(this)) == null)
        {
            Debug.LogErrorFormat("Paddle {0} cannot find opponent", this);
        }
        // TODO: Need Side Bounds for Board
        // TODO: If opponent is more left, move right
        // TODO: If opponent is more right, move left

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
	}

    void executeMovement()
    {
        if (travelPositions.Count > dampening)
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

    IEnumerator detachOnDelay() 
    {
        yield return new WaitForSeconds(Random.Range(stickPowerUpRangeTimeRange.x, stickPowerUpRangeTimeRange.y));
        detachAll();
    }
}
