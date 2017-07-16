using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour, IShipBase {

	// The axis used to take input.
	public string inputAxis = "Horizontal";

	private MapManager _mapManager;
	private GameManager _gameManager;
	// The value of input, updated each frame.
	private float _inputValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Called each update to move sideways
	void Move(){

	}

	// Called to fire a projectile.
	public void Fire(){

	}

	// Called when a projectile damages the ship. Should call OnDeath() if it kills;
	public int TakeDamage(int dmg){
		// Since the player is dead on touch, just destroy it
		OnDeath();
	}

	// Called when the ship dies. Add points, do game state detection, etc.
	public void OnDeath(){

	}
}
