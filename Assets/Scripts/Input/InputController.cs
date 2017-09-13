using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class InputController : MonoBehaviourExtended 
{
	Dictionary<int, PlayerPaddleController> DraggingPaddles =
		new Dictionary<int, PlayerPaddleController>();

	// Update is called once per frame
	void FixedUpdate()
	{
		if(Input.touches.Length == NONE_VALUE)
		{
			zeroOutTouches();
		}
		else
		{
			updateAllTouches();
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

	void zeroOutTouches()
	{
		foreach(int touchId in DraggingPaddles.Keys)
		{
			PlayerPaddleController controller = DraggingPaddles[touchId];
			controller.EndDrag();
		}
		DraggingPaddles.Clear();
	}

	void updateAllTouches()
	{
		foreach(Touch touch in Input.touches) 
		{
			updateTouch(touch);
		}
	}

	void updateTouch(Touch touch)
	{
		PlayerPaddleController controller;
		Vector3 touchPosition;
		GameObject paddle = DragUtil.GetTouchTarget(touch, out touchPosition);
		if(touch.phase == TouchPhase.Ended)
		{
			if(DraggingPaddles.TryGetValue(touch.fingerId, out controller))	
			{
				controller.EndDrag();
				DraggingPaddles.Remove(touch.fingerId);
			}
		}
		else if(IsPaddle(paddle, out controller) && touch.phase == TouchPhase.Began) 
		{
			if(!DraggingPaddles.ContainsKey(touch.fingerId))
			{
				DraggingPaddles.Add(touch.fingerId, controller);
				controller.StartDrag(touchPosition);
			}
		}
		else if(DraggingPaddles.TryGetValue(touch.fingerId, out controller) 
			&& touch.phase == TouchPhase.Moved)
		{
			controller.DragPaddle(DragUtil.GetTouchPosition(touch));
		}
	}
}
