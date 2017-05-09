using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGeneration))]

public class Map : Singleton<Map> {
	[Header("Map Size")]
	public int mapSizeX;
	public int mapSizeY;

	[Header("Map Options")]
	public int walksteps;
	[Range(1, 3)]
	public int corridorLenghMin;
	[Range(2, 4)]
	public int corridorLengthMax;
	[Range(1, 2)]
	public int roomSizeMin;
	[Range(2, 5)]
	public int roomSizeMax;

	private MapGeneration mapGen;
	private Tile[,] currentMapData;
	private List<GameObject> currentMapVisuals;

	public void Init() {
		if (mapGen == null) {
			mapGen = GetComponent<MapGeneration>();
			mapGen.Init(mapSizeX, mapSizeX, walksteps);
		}

		//Spawn a new map
		currentMapData = mapGen.GenerateMapData();
		currentMapVisuals = mapGen.GenerateMapVisuals(currentMapData, transform);
	}

	public Tile GetRandomTileOfType(TileType type) {
		List<Tile> tiles = new List<Tile>();

		//This loop starts and ends 1 early to make sure an edge tile is not chosen
		for (int x = 1; x < currentMapData.GetLength(0) - 1; x++) {
			for (int y = 1; y < currentMapData.GetLength(1) - 1; y++) {
				if (currentMapData[x, y].Type == type)
					tiles.Add(currentMapData[x, y]);
			}
		}

		return tiles[GameSeed.Instance.GetInt(tiles.Count)];
	}

	private void RegenerateMap() {
		for (int i = 0; i < currentMapVisuals.Count; i++) {
			currentMapVisuals[i].GetComponent<BoxCollider2D>().enabled = false;
			SimplePool.Despawn(currentMapVisuals[i]);
		}

		Init();
	}

	private void OnValidate() {
		if (corridorLenghMin <= 0)
			corridorLenghMin = 1;

		if (corridorLengthMax <= 0)
			corridorLengthMax = 2;

		if (corridorLenghMin > corridorLengthMax)
			corridorLenghMin = corridorLengthMax - 1;

		if (roomSizeMin <= 0)
			roomSizeMin = 1;

		if (roomSizeMax <= 0)
			roomSizeMax = 2;

		if (roomSizeMin > roomSizeMax)
			roomSizeMin = roomSizeMax - 1;
	}
}
