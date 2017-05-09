using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController> {
	public GameObject playerObject;

	private Player currentPlayer;

	public void Init() {
		//If there is no current player spawn one
		if (currentPlayer == null)
			currentPlayer = SimplePool.Spawn(playerObject, Vector2.zero, Quaternion.identity).GetComponent<Player>();

		//Move the player to a valid tile
		Tile t = Map.Instance.GetRandomTileOfType(TileType.Ground);
		currentPlayer.transform.position = new Vector2(t.X, t.Y);
		currentPlayer.Init();
	}
}
