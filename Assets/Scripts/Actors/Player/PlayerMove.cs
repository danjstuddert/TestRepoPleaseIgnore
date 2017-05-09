using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour {
	public float moveSpeed;

	private Vector2 moveVelocity;
	private Rigidbody2D rBody;

	public void Init() {
		rBody = GetComponent<Rigidbody2D>();
		rBody.gravityScale = 0;
		rBody.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void Update() {
		CalculateVelocity();
	}

	void FixedUpdate() {
		rBody.velocity = moveVelocity;
	}

	private void CalculateVelocity() {
		moveVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
	}
}
