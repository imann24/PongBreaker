using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour {

	Dictionary<int, PaddleController> DraggingPaddles = new Dictionary<int, PaddleController>();

	// Update is called once per frame
	void Update () {
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Ended) {
				DraggingPaddles.Remove(touch.fingerId);
				continue;
			}

			Vector3 touchPosition;
			GameObject paddle = DragUtil.GetTouchTarget(touch, out touchPosition);
			PaddleController controller;
		
			if (IsPaddle(paddle, out controller) && touch.phase == TouchPhase.Began) {
				controller.SetOffset(touchPosition);
				DraggingPaddles.Add(touch.fingerId, controller);
			} else if (DraggingPaddles.TryGetValue(touch.fingerId, out controller) && touch.phase == TouchPhase.Moved) {
				controller.DragPaddle(DragUtil.GetTouchPosition(touch));
			}

			print(DraggingPaddles.ContainsKey(touch.fingerId));
			print(touch.phase == TouchPhase.Moved);
		}
	}

	bool IsPaddle (GameObject paddle, out PaddleController controller) {
		if (paddle != null) {
			controller = paddle.GetComponent<PaddleController>();
			return controller != null;
		} else {
			controller = null;
			return false;
		}
	}
}
