using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {
	public static Vector2 RoundToNearest(this Vector2 v) {
		return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
	}

	public static Vector2 GetHeading(this Vector2 location, Vector2 target) {
		Vector2 heading = target - location;
		float distance = heading.magnitude;
		return heading / distance;
	}
}
