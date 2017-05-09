using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
	public Gun startingWeapon;
	public string weaponChildName;

	private Gun currentWeapon;
	private Transform weaponBarrel;
	private SpriteRenderer weaponSprite;

	private bool canShoot;
	private float shotTimer;

	public void Init() {
		currentWeapon = startingWeapon;
		
		if(weaponBarrel == null)
			weaponBarrel = transform.FindChild(weaponChildName).GetChild(0);

		if (weaponSprite == null)
			weaponSprite = transform.FindChild(weaponChildName).GetComponent<SpriteRenderer>();

		weaponSprite.sprite = currentWeapon.GunSprite;
		canShoot = true;
	}

	void Update() {
		if (Input.GetButtonDown("Fire1") && canShoot) {
			canShoot = false;
			Shoot();
			
		}

		if(canShoot == false) {
			shotTimer += Time.deltaTime;

			if(shotTimer >= currentWeapon.rateOfFire) {
				shotTimer = 0f;
				canShoot = true;
			}
		}
	}

	private void Shoot() {
		//Spawn the shot
		GunShot shot = SimplePool.Spawn(currentWeapon.shotObject, weaponBarrel.position, Quaternion.identity).GetComponent<GunShot>();

		//So we can have the bullet fly correctly find the direction to the mouse pointer
		Vector2 heading = CameraController.Instance.GetMousePosition() - transform.position;
		float distance = heading.magnitude;

		shot.Init(transform, heading / distance, currentWeapon.shotForce);
	}
}
