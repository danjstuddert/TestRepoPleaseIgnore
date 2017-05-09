using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSeed : Singleton<GameSeed> {
	private System.Random currentSeed;

	public void Init(int seed) {
		currentSeed = new System.Random(seed);
	}

	public int GetInt() {
		return currentSeed.Next();
	}

	public int GetInt(int max) {
		return currentSeed.Next(max);
	}

	public int GetInt(int min, int max) {
		if (max >= min)
			return currentSeed.Next(min, max);

		//if for some reason min is greater than max, switch them
		int temp = max;
		max = min;
		min = temp;

		return currentSeed.Next(min, max);
	}

	public float GetFloat() {
		return (float)currentSeed.NextDouble();
	}

	public float GetFloat(int min, int max) {
		return (float)currentSeed.NextDouble() * (max - min) + min;
	}

	public float GetUniformFloat() {
		double mantissa = (currentSeed.NextDouble() * 2.0) - 1.0;
		double exponent = Math.Pow(2.0, currentSeed.Next(-126, 128));
		return (float)(mantissa * exponent);
	}
}
