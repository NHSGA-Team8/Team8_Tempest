using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : IShipBase {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	// Called to fire a projectile.
	public void Fire()
	{
	}

	// Called when a projectile damages the ship. Should call OnDeath() if it kills;
	public void TakeDamage(int dmg)
	{
	}

	// Called when the ship dies. Add points, do game state detection, etc.
	public void OnDeath()
	{
	}
}
