using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : Singleton<CursorController> {
	public GameObject cursor;

	private Transform currentCursor;

	public void Init() {
		if (currentCursor == null)
			currentCursor = SimplePool.Spawn(cursor, transform.position, Quaternion.identity).transform;
	}

	void Update() {
		if (currentCursor) {
			if(Cursor.visible)
				Cursor.visible = false;

			Vector3 point = CameraController.Instance.GetMousePosition();
			point.z = 0f;

			currentCursor.transform.position = point;
		}
	}
}
