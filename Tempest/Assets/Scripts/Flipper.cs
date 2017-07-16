using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Rachael H. on 16 July 2017
 */
public class Flipper : IShipBase {

	public float shellSpeed;
	public float reloadTime;
	private float _currentHealth;
	public int levelNum;
	public bool straightMovement; //True if moving in only one lane for level one

	public GameObject shell;
	Rigidbody rb;
	AudioSource flipperSounds;
	AudioClip flipperShooting;
	AudioClip flipperExplosion;

	private MapManager _mapManager;
	private GameManager _gameManager;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		if (levelNum == 1) {
			straightMovement = true;
		}
	}

	// Update is called once per frame
	void Update () {
		if (straightMovement) {
			
		}
	}

	// Called to fire a projectile.
	public void Fire()
	{
	}

	// Called when a projectile damages the ship. Should call OnDeath() if it kills;
	public void TakeDamage(int dmg)
	{
		OnDeath ();
	}

	// Called when the ship dies. Add points, do game state detection, etc.
	public void OnDeath()
	{
	}
}
