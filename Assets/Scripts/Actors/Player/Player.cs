using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerSprite))]
[RequireComponent(typeof(PlayerWeapon))]
public class Player : Actor {
	private PlayerMove move;
	private PlayerSprite sprites;
	private PlayerWeapon weapon;

	public override void Init() {
        base.Init();

		if (move == null) {
			move = GetComponent<PlayerMove>();
			move.Init();
		}

		if(sprites == null) {
			sprites = GetComponent<PlayerSprite>();
			sprites.Init();
		}

		if (weapon == null)
			weapon = GetComponent<PlayerWeapon>();

		weapon.Init();
	}
}
