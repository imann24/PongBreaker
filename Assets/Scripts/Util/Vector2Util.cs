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
		if (Mathf.Floor(vector.x) == 0) {
			vector.x = Random.Range(-max, max);
		}

		if (Mathf.Floor(vector.y) == 0) {
			vector.y = Random.Range(-max, max);
		}
		return vector;
	}
}
