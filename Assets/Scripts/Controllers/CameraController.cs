using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController> {
	[Header("Camera Follow")]
	[Range(0.1f, 0.5f)]
	public float percFromPlayer;
	public float lerpTime;

	private Camera mainCam;
	private Transform player;

	private float velocity;
	private Vector3 targetPos;

	public void Init() {
		if (mainCam == null)
			mainCam = Camera.main;

		if (player == null)
			player = GameObject.FindWithTag("Player").transform;

		mainCam.transform.position = new Vector3(player.position.x, player.position.y, mainCam.transform.position.z);
	}

	void Update() {
		if(player != null)
			UpdateCameraPosition();
	}

	private void UpdateCameraPosition() {
		//Find the point between the player's position and the mouse pointers
		Vector3 point = GetMousePosition() - player.position;
		point.z = mainCam.transform.position.z;

		point.Normalize();
		Vector3 target = player.position + (percFromPlayer * point);
		target.z = mainCam.transform.position.z;

		mainCam.transform.position = target;
	}

	public Vector3 GetMousePosition() {
		return mainCam.ScreenToWorldPoint(Input.mousePosition);
	}

	public Vector3 GetScreenPoint(Vector3 worldPosition) {
		return mainCam.WorldToScreenPoint(worldPosition);
	}
}
