using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {
	[Header("Seed")]
	public string seed;
	public bool useRandomSeed;

	// Use this for initialization
	void Start () {
		//Make sure we setup our seed correctly
		GameSeed.Instance.Init(useRandomSeed ? System.DateTime.Now.Ticks.ToString().GetHashCode() : seed.GetHashCode());

		Map.Instance.Init();
		PlayerController.Instance.Init();
		CameraController.Instance.Init();

		CursorController.Instance.Init();
	}
}
