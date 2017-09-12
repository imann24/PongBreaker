using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviourExtended 
{
	Dictionary<int, PlayerPaddleController> DraggingPaddles =
		new Dictionary<int, PlayerPaddleController>();

	// Update is called once per frame
	void FixedUpdate()
	{
		foreach(Touch touch in Input.touches) 
		{
			if(touch.phase == TouchPhase.Ended) 
			{
				DraggingPaddles.Remove(touch.fingerId);
				continue;
			}
			Vector3 touchPosition;
			GameObject paddle = DragUtil.GetTouchTarget(touch, out touchPosition);
			PlayerPaddleController controller;
			if(IsPaddle(paddle, out controller) && touch.phase == TouchPhase.Began) 
			{
				controller.SetOffset(touchPosition);
				DraggingPaddles.Add(touch.fingerId, controller);
			}
			else if(DraggingPaddles.TryGetValue(touch.fingerId, out controller) 
				&& touch.phase == TouchPhase.Moved)
			{
				controller.DragPaddle(DragUtil.GetTouchPosition(touch));
			}
		}
	}

	bool IsPaddle (GameObject paddle, out PlayerPaddleController controller) 
	{
		if(paddle != null) 
		{
			controller = paddle.GetComponent<PlayerPaddleController>();
			return controller != null;
		} 
		else 
		{
			controller = null;
			return false;
		}
	}
}
