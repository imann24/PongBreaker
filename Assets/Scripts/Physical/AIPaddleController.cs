/*
 * Author: Isaiah Mann
 * Description:
 */

using System.Collections.Generic;
using UnityEngine;

public class AIPaddleController : PaddleController
{
	Queue<float> puckPositions = new Queue<float>();

	int dampening = 5;
	float slowdown = 0.75f;

	void Update()
	{
		matchPuckPosition();
	}

	void matchPuckPosition()
	{
		puckPositions.Enqueue(puck.GetYPosition());
		if(puck && puckPositions.Count > dampening)
		{
			Vector3 position = transform.localPosition;	
			position.y = Mathf.Lerp(puckPositions.Dequeue(), position.y, slowdown);
			transform.localPosition = position;
		}
	}
}
