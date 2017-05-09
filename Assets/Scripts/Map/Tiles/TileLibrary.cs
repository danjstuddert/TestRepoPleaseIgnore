using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLibrary : Singleton<TileLibrary> {
	public GameObject baseTile;
	public List<Sprite> wallTiles;
	public List<Sprite> groundTiles;

	public Sprite GetRandomTile(TileType type) {
		switch (type) {
			case TileType.Ground:
				return groundTiles[GameSeed.Instance.GetInt(groundTiles.Count)];
			case TileType.Wall:
				return wallTiles[GameSeed.Instance.GetInt(wallTiles.Count)];
			default:
				Debug.LogError("Requesting invalid tile type");
				return null;
		}
	}
}
