using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeomHelper {

	public static float GetAngle (Vector2 vector)
	{
		var position = vector.normalized;
		float angle = Mathf.Atan2 (position.y, position.x) * Mathf.Rad2Deg;
		if (angle < 0) {
			angle += 360;
		}
		return angle;
	}

	public static Vector3 GetCircleRandomPosition() {
		float radius = Camera.main.orthographicSize * Camera.main.aspect * 1.25f;
		float x = Random.Range(-radius, radius);
		float y = (Random.value > 0.5f ? 1 : -1) * Mathf.Sqrt (Mathf.Pow (radius, 2f) - Mathf.Pow (x, 2f));
		return new Vector3(x, y, 0);
	}

}
