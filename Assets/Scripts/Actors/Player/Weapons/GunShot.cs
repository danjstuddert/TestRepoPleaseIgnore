using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GunShot : MonoBehaviour {
	private Rigidbody2D rbody;
	private Vector2 moveVelocity;

	private Transform shotOrigin;

	public void Init(Transform shotOrigin, Vector2 shotDirection, float shotForce) {
		if (rbody == null)
			rbody = GetComponent<Rigidbody2D>();

		this.shotOrigin = shotOrigin;

		//Set our velocity
		moveVelocity = shotDirection * shotForce;

		//Make sure we are facing the right way
		float angle = Mathf.Atan2(shotDirection.y, shotDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
	}

	void FixedUpdate() {
		if(rbody.velocity != moveVelocity)
			rbody.velocity = moveVelocity;
	}

	void OnCollisionEnter2D(Collision2D col) {
		//If we hit the object we were fired from do nothing
		if(col.gameObject.tag != shotOrigin.tag) {
			if (col.gameObject.tag == "Player") {
				Debug.Log("Shot player!");
			}

            else if (col.gameObject.tag == "Enemy") {
                col.gameObject.GetComponent<Actor>().AlterHealth(-1);
            }

			SimplePool.Despawn(gameObject);
		}
	}
}
