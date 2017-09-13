/*
 * Author: Isaiah Mann
 * Description:
 */

using UnityEngine;

public class PlayerPaddleController : PaddleController
{
	public int TouchCount 
	{
		get;
		private set;
	}

	public bool IsKinematic
	{
		get
		{
			return rigibody.isKinematic;
		}
	}

	public Vector2 Velocity
	{
		get
		{
			return rigibody.velocity;
		}
	}
		
	Vector3 fingerOffset;

	void FixedUpdate()
	{
		#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
			updateVelocity();
		#endif
	}
		
	void updateVelocity()
	{
		float speed = Input.GetAxis(PlayerUtil.IDToString(paddlePosition)) * this.speed;
		rigibody.velocity = new Vector2(NONE_VALUE, speed);
		if(mouseIsDown)
		{
			if(rigibody.isKinematic)
			{
				setKinematic(false);
			}
		}
		else
		{
			setKinematic(speed == NONE_VALUE);
		}
	}
		
	#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL

	protected override void OnMouseDown()
	{
		base.OnMouseDown();
		StartDrag(DragUtil.GetMousePosition());
	}

	void OnMouseDrag()
	{
		DragPaddle(DragUtil.GetMousePosition());
	}

	protected override void OnMouseUp()
	{
		base.OnMouseUp();
		if(rigibody.isKinematic)
		{
			rigibody.velocity = Vector2.zero;
		}
		EndDrag();
	}
		
	#endif

	public void StartDrag(Vector3 inputPosition)
	{
		setOffset(inputPosition);
		TouchCount++;
		setKinematic(false);
	}

	public void EndDrag()
	{
		TouchCount--;
		setKinematic(true);
	}

	void setOffset(Vector3 inputPosition)
	{
		fingerOffset = inputPosition - transform.position;
	}

	public void DragPaddle(Vector3 inputPosition) 
	{
		Vector3 newDragPosition = inputPosition - fingerOffset;
		transform.position = new Vector3(transform.position.x, newDragPosition.y, transform.position.z);
	}

	void setKinematic(bool enabled)
	{
		rigibody.isKinematic = enabled;
		if(rigibody.isKinematic)
		{
			zeroOutVelocity();
		}
	}

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		base.OnCollisionEnter2D(collision);
		zeroOutVelocity();
	}

	void zeroOutVelocity()
	{
		rigibody.velocity = Vector2.zero;
	}
}
