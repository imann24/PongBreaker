using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PaddleController : PhysicalObjectController 
{
	protected PuckController puck;

	protected float speed = Global.BASE_PADDLE_SPEED;

	protected GameObject myGoal;

	protected PaddlePosition paddlePosition
	{
		get;
		private set;
	}

	Vector3 startingPosition;

	protected override void Awake()
	{
		base.Awake();
		startingPosition = transform.position;
	}

	protected override void Start()
	{
		base.Start();
		StateController.Instance.CurrentGame.OnRestart.Subscribe(resetPosition);
	}

	public void Initialize(PuckController puck, PaddlePosition paddlePosition, GameObject myGoal)
	{
		this.puck = puck;
		this.paddlePosition = paddlePosition;
		this.myGoal = myGoal;
	}

	public override string ToString ()
	{
		return string.Format("[PaddleController: {0}]", paddlePosition);
	}

	public float GetYPosition()
	{
		return transform.position.y;	
	}

	void resetPosition()
	{
		transform.position = startingPosition;
	}
}
