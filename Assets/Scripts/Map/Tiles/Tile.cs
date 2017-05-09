using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Wall, Ground}

public class Tile {
	public int X { get; private set; }
	public int Y { get; private set; }
	public TileType Type { get; private set; }

	public Tile(int x, int y, TileType type = TileType.Wall) {
		X = x;
		Y = y;

		//Here we set everything to a default of wall, so that
		//every tile that is not flagged as a ground tile in the 
		//random walk function will still be able to give a sprite 
		//to the map generation when it is creating visuals
		Type = type;
	}

	public void UpdateType(TileType newType) {
		Type = newType;
	}
}
