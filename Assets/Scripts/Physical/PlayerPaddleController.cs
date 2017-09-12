/*
 * Author: Isaiah Mann
 * Description:
 */

using UnityEngine;

public class PlayerPaddleController : PaddleController
{
	Vector3 fingerOffset;
	bool wasDraggedInFrame = true;

	void FixedUpdate()
	{
		#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
			updateVelocity();
		#elif UNITY_IOS || UNITY_ANDROID
			toggleKinematicOnDrag();
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

	void toggleKinematicOnDrag()
	{
		setKinematic(!wasDraggedInFrame);
		wasDraggedInFrame = false;
	}

	#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL

	protected override void OnMouseDown()
	{
		base.OnMouseDown();
		setKinematic(false);
		SetOffset(DragUtil.GetMousePosition());
	}

	void OnMouseDrag()
	{
		DragPaddle(DragUtil.GetMousePosition());
	}

	protected override void OnMouseUp()
	{
		base.OnMouseUp();
		setKinematic(true);
		if(rigibody.isKinematic)
		{
			rigibody.velocity = Vector2.zero;
		}
	}
		
	#endif

	public void SetOffset(Vector3 inputPosition)
	{
		fingerOffset = inputPosition - transform.position;
	}

	public void DragPaddle(Vector3 inputPosition) 
	{
		Vector3 newDragPosition = inputPosition - fingerOffset;
		transform.position = new Vector3(transform.position.x, newDragPosition.y, transform.position.z);
		wasDraggedInFrame = true;
	}

	void setKinematic(bool enabled)
	{
		rigibody.isKinematic = enabled;
	}
}
