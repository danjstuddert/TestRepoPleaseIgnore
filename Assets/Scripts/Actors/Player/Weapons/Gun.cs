using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireType { Single, Burst, Auto }

[CreateAssetMenu(fileName ="New Gun", menuName = "Create New Gun", order = 1)]
[System.Serializable]
public class Gun : ScriptableObject {
	[Header("Weapon")]
	public Sprite GunSprite;

	[Header("Shot")]
	public GameObject shotObject;
	public AudioClip shotSound;
	public FireType fireType;
	public float rateOfFire;		//Measured in seconds
	public float shotForce;
}
