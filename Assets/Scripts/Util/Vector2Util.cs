using UnityEngine;
using System.Collections;

public static class Vector2Util {
	public static Vector2 Origin = new Vector2(0, 0);

	public static Vector2 Clamp (Vector2 vector, float max, float min = 0) {
		return new Vector2 (
			Mathf.Clamp(vector.x, min, max),
			Mathf.Clamp(vector.y, min, max)
		);
	}

	public static Vector2 MakeDiagonal (Vector2 vector, float max = 10.0f) {
		while (Mathf.Floor(vector.x) == 0) {
			vector.x = Random.Range(-max, max);
		}

		while (Mathf.Floor(vector.y) == 0) {
			vector.y = Random.Range(-max, max);
		}
		return vector;
	}

	public static Vector2 SpeedBoost (Vector2 vector, float boostModifier = 2) {
		if (vector.x < Global.MIN_PUCK_SPEED && vector.y < Global.MIN_PUCK_SPEED) {
			return vector * boostModifier;
		} else {
			return vector;
		}
	}
}
