using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PaddleController : PhysicalObjectController {
	public PlayerID Player;

	Vector3 fingerOffset;

	float speed = Global.BASE_PADDLE_SPEED;

	void FixedUpdate () {
		UpdateVelocity();
	}
		
	void UpdateVelocity () {
		rigibody.velocity = new Vector2(0, Input.GetAxis(PlayerUtil.IDToString(Player)) * speed);
	}

	#if UNITY_STANDALONE || UNITY_EDITOR
	void OnMouseDown () {
		SetOffset(DragUtil.GetMousePosition());
	}
		
	void OnMouseDrag () {
		DragPaddle(DragUtil.GetMousePosition());
	}
	#endif

	public void SetOffset (Vector3 inputPosition) {
		fingerOffset = inputPosition - transform.position;
	}

	public void DragPaddle (Vector3 inputPosition) {
		Vector3 newDragPosition = inputPosition - fingerOffset;
		transform.position = new Vector3(transform.position.x, newDragPosition.y, transform.position.z);
	}
}
