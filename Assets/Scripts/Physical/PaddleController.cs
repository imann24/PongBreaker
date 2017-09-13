using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PaddleController : PhysicalObjectController 
{
	protected PuckController puck;

	public void Initialize(PuckController puck, PaddlePosition paddlePosition)
	{
		this.puck = puck;
		this.paddlePosition = paddlePosition;
	}

	protected PaddlePosition paddlePosition
	{
		get;
		private set;
	}

	protected float speed = Global.BASE_PADDLE_SPEED;

	public override string ToString ()
	{
		return string.Format("[PaddleController: {0}]", paddlePosition);
	}
}
