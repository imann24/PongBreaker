using UnityEngine;
using System.Collections;

public static class DragUtil {
	public static Vector3 GetMousePosition () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		return ray.origin;
	}

	public static Vector3 GetTouchPosition (Touch touch) {
		return Camera.main.ScreenToWorldPoint(touch.position);
	}

	public static GameObject GetTouchTarget (Touch touch, out Vector3 touchPosition) {
		touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
		RaycastHit2D hit = Physics2D.Raycast (touchPosition, Vector2.zero);

		if (hit.collider != null) {
			Debug.Log(hit.collider.gameObject);
			return hit.collider.gameObject;
		} else {
			return null;
		}
	}
}
