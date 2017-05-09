using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour {
	public string playerSpriteName;
	public string weaponSpriteName;

	private Transform playerSprite;
	private Transform weaponSprite;

	public void Init() {
		if (playerSprite == null)
			playerSprite = transform.FindChild(playerSpriteName);
		if (weaponSprite == null)
			weaponSprite = transform.FindChild(weaponSpriteName);
	}

	void Update() {
		UpdatePlayerFacing();
		UpdateWeaponFacing();
	}

	private void UpdatePlayerFacing() {
		if (playerSprite) {
			if (CameraController.Instance.GetMousePosition().x < playerSprite.position.x)
				playerSprite.localScale = new Vector3(-1, 1, 1);
			else
				playerSprite.localScale = new Vector3(1, 1, 1);
		}
	}

	private void UpdateWeaponFacing() {
		if (weaponSprite) {
			if (CameraController.Instance.GetMousePosition().x < weaponSprite.position.x)
				weaponSprite.localScale = new Vector3(-1, 1, 1);
			else
				weaponSprite.localScale = new Vector3(1, 1, 1);
		}

		//Point towards the mouse
		Vector3 pos = CameraController.Instance.GetScreenPoint(weaponSprite.position);
		Vector3 dir = Input.mousePosition - pos;

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		weaponSprite.rotation = Quaternion.AngleAxis(weaponSprite.localScale.x > 0 ? angle : angle - 180f, Vector3.forward);
	}
}
