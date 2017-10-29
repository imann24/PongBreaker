using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PaddleController : PhysicalObjectController 
{
	Vector3 startingPosition;

	protected PuckController puck;

	protected float speed = Global.BASE_PADDLE_SPEED;

	protected PaddlePosition paddlePosition
	{
		get;
		private set;
	}

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

	public void Initialize(PuckController puck, PaddlePosition paddlePosition)
	{
		this.puck = puck;
		this.paddlePosition = paddlePosition;
	}

	public override string ToString ()
	{
		return string.Format("[PaddleController: {0}]", paddlePosition);
	}

	void resetPosition()
	{
		transform.position = startingPosition;
	}
}
