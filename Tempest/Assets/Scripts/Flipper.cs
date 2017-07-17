using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Rachael H. on 16 July 2017
 */
public class Flipper : MonoBehaviour, IShipBase
{
	//Public
	public float movementForce;
	public float shellSpeed;
	public float reloadTime;
	public int levelNum;
	public GameObject flipperShell;

	//Private
	private float _currentHealth;
	private bool _straightMovement; //True if moving in only one lane for level one
	private bool _reloaded;
	private MapManager _mapManager; //How do I use the same _mapManager as that of the player ship if it's private?
	private GameManager _gameManager;
	private float rand;

	Rigidbody rb;
	AudioSource flipperSounds;
	AudioClip flipperShooting;
	AudioClip flipperExplosion;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		_reloaded = true;
		rand = Random.value * _mapManager.mapVertices.Length;
		//GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>
		//rand = Random.value * (PlayerShip.getMapManager().mapVertices.Length - 2);
		//# of mapLines = # of mapVertices - 1
		//Subtract another 1 to be able to access rand + 1
		if (levelNum == 1)
		{
			_straightMovement = true;
		}
		else
		{
			_straightMovement = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (_straightMovement)
		{
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
			rb.AddForce (movementForce * transform.forward * Time.deltaTime);
		}
		else
		{
			
		}
	}

	// Called to fire a projectile.
	public void Fire()
	{
		GameObject newFlipperShell = Instantiate (flipperShell);
		newFlipperShell.GetComponent<Rigidbody> ().AddForce (shellSpeed * transform.forward * Time.deltaTime);
	}

	// Called when a projectile damages the ship. Should call OnDeath() if it kills;
	public void TakeDamage(int dmg)
	{
		OnDeath ();
	}

	// Called when the ship dies. Add points, do game state detection, etc.
	public void OnDeath()
	{
		gameObject.SetActive (false);
	}

	//public void Transistion()
}
