using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {
	//Map size
	private int sizeX;
	private int sizeY;
	private int walkSteps;

	private Map map;
	private TileLibrary tiles;

	private Tile[,] mapData;

	public void Init(int sizeX, int sizeY, int walkSteps) {
		this.sizeX = sizeX;
		this.sizeY = sizeY;
		this.walkSteps = walkSteps;

		if (map == null)
			map = Map.Instance;

		if (tiles == null)
			tiles = TileLibrary.Instance;
	}

	public Tile[,] GenerateMapData() {
		mapData = new Tile[sizeX, sizeY];

		//Create the initial tiles
		for (int x = 0; x < sizeX; x++) {
			for (int y = 0; y < sizeY; y++) {
				mapData[x, y] = new Tile(x, y);
			}
		}

		//Grab a random point near the center of the map, this will be our starting point
		Tile t = mapData[GameSeed.Instance.GetInt(sizeX / 2 - 2, sizeX /2 + 2), GameSeed.Instance.GetInt(sizeY / 2 - 2 ,sizeY / 2 + 2)];

		//Flag this tile as a ground tile so it can be used in our random walk
		t.UpdateType(TileType.Ground);

		for (int i = 0; i < walkSteps; i++) {
			//Get a ground tile at random
			//if this is the first time through this loop we can just use it
			//otherwise pick one at random
			if(i != 0) {
				t = GetRandomTileOfType(TileType.Ground);
			}

			//If for some reason a walk was not completed decrement i
			//so we can try again
			if (RandomWalk(new Vector2(t.X, t.Y)) == false)
				i--;
		}

		return mapData;
	}

	private Tile GetRandomTileOfType(TileType type) {
		List<Tile> tiles = new List<Tile>();

		//This loop starts and ends 1 early to make sure an edge tile is not chosen
		for (int x = 1; x < mapData.GetLength(0) - 1; x++) {
			for (int y = 1; y < mapData.GetLength(1) - 1; y++) {
				if (mapData[x, y].Type == type)
					tiles.Add(mapData[x, y]);
			}
		}

		return tiles[GameSeed.Instance.GetInt(tiles.Count)];
	}

	public List<GameObject> GenerateMapVisuals(Tile[,] mapData, Transform mapParent) {
		List<GameObject> mapVisuals = new List<GameObject>();

		for (int x = 0; x < mapData.GetLength(0); x++) {
			for (int y = 0; y < mapData.GetLength(1); y++) {
				GameObject tile = SimplePool.Spawn(tiles.baseTile, new Vector2(x, y), Quaternion.identity);
				tile.name = string.Format("x {0}, y {1}", x, y);
				tile.transform.SetParent(mapParent);
				SpriteRenderer sprite = tile.GetComponent<SpriteRenderer>();

				//Sanity checking because I am bad at things
				if(sprite == null) {
					Debug.LogError(string.Format("{0} does not have a sprite renderer attached! Attaching one", tile.name));
					sprite = tile.AddComponent<SpriteRenderer>();
				}

				//Use the tile library to get a tile of the correct type
				sprite.sprite = tiles.GetRandomTile(mapData[x, y].Type);

				//If the tile is a wall make sure we turn on its box collider
				if (mapData[x, y].Type == TileType.Wall)
					tile.GetComponent<BoxCollider2D>().enabled = true;

				//Make sure we assign the new tile to the mapVisuals list
				mapVisuals.Add(tile);
			}
		}

		return mapVisuals;
	}

	private bool RandomWalk(Vector2 startPosition) {
		//Which way should we walk?
		//0 north, 1 east, 2 south, 3 west
		int rand = GameSeed.Instance.GetInt(4);
		Vector2 direction;

		switch (rand) {
			case 0:
				direction = Vector2.up;
				break;
			case 1:
				direction = Vector2.right;
				break;
			case 2:
				direction = -Vector2.up;
				break;
			case 3:
				direction = Vector2.left;
				break;
			default:
				direction = Vector2.zero;
				break;
		}

		//Use the game seed to determine if we place a corridor or a room
		//On a 0 - place a corridor
		//On a 1 - place a room
		return GameSeed.Instance.GetInt(3) == 0 ? PlaceRoom(startPosition + direction) : PlaceCorridor(startPosition, direction);
	}

	private bool PlaceCorridor(Vector2 startPosition, Vector2 direction) {
		//How long is the corridor?
		//We add 1 to the corridor length max so we can get max value
		int corridorLength = GameSeed.Instance.GetInt(map.corridorLenghMin, map.corridorLengthMax + 1);

		for (int i = 0; i < corridorLength; i++) {
			//Increase our start position by direction
			startPosition += direction;

			//Is this a valid tile to change?
			//If so just continue on
			if (UpdateTile(startPosition, TileType.Ground) == false) {
				//If not
				//and this is our first time through this loop return false
				if (i == 0)
					return false;
				//else return true because we have put something down already
				//so the corridor was partially completed
				else
					return true;
			}
		}

		return true;
	}

	private bool PlaceRoom(Vector2 startPosition) {
		//How big should the room be?
		int roomSize = GameSeed.Instance.GetInt(map.roomSizeMin, map.roomSizeMax + 1);

		for (int x = 0; x < roomSize; x++) {
			for (int y = 0; y < roomSize; y++) {
				if(UpdateTile(new Vector2(startPosition.x + x, startPosition.y + y), TileType.Ground) == false) {
					//If this is our first tile changed make sure we return false
					//because it is an invalid placement
					if (x == 0 && y == 0)
						return false;
					else
						return true;
				}

			}
		}

		return true;
	}

	private bool UpdateTile(Vector2 position, TileType type) {
		//Find the tile
		Tile t = GetTileFromPosition(position);

		//Is the tile out of bounds? (A null returned from the method)
		//If so return false
		if (t == null)
			return false;

		//Change the tile to the given type
		if(t.Type != type)
			t.UpdateType(type);

		return true;
	}

	private Tile GetTileFromPosition(Vector2 position) {
		//Is the tile out of bounds?
		if (position.x <= 0 || position.y <= 0 || position.x >= sizeX - 1 || position.y >= sizeY - 1)
			return null;

		return mapData[(int)position.x, (int)position.y];
	}
}
