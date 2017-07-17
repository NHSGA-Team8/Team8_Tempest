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
	public GameObject player;
	public GameObject flipperEnemy;
	public float respawnTime;
	public MapLine thisMapLine;

	//Private
	private float _currentHealth;
	private bool _straightMovement; //True if moving in only one lane for level one
	private bool _reloaded;
	private MapManager _mapManager; //How do I use the same _mapManager as that of the player ship if it's private?
	private GameManager _gameManager;
	private int _rand; //private float _rand;
	private Vector3 _vertex1;
	private Vector3 _vertex2;
	private Vector3 _lineCenter;
	private float _mapDepth;

	Rigidbody rb;
	//Audio
	AudioSource flipperSounds;
	AudioClip flipperShooting;
	AudioClip flipperExplosion;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		_reloaded = true;
		//respawnTime = 0.2f;
		//_rand = Random.value * _mapManager.mapVertices.Length;
		_rand = random ();
		//print(Console.WriteLine(MapManager.mapVertices[1]));
		_mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
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
	void Update ()
	{
		if (_straightMovement)
		{
			//Only move in Z direction, aka depth
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
			rb.AddForce (movementForce * transform.forward * Time.deltaTime);
		}
		else
		{
			//Move forward by one or a few pixels
			//While moving to next section of map
		}
		for (float f = 1f; f >= 0; f -= 0.1f)
		{
			createNew ();
			//yield return new WaitForSeconds (respawnTime);
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
		gameObject.SetActive (false); // Disable enemy
	}

	//public void Transistion()

	//public float random(GameObject ship)
	//public float random()
	public int random()
	{
		//rand = Random.value * _mapManager.mapVertices.Length;
		//GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>
		//rand = Random.value * (PlayerShip.getMapManager().mapVertices.Length - 2);
		//# of mapLines = # of mapVertices - 1
		//Subtract another 1 to be able to access rand + 1
		//rand = Random.value * (ship.GetComponent<PlayerShip>.getMapManager().mapVertices.Length - 1);

		//return Random.value * (player.GetComponent<PlayerShip> ().getMapManager().mapVertices.Length - 2);
		//return (int)(Random.value * (player.GetComponent<PlayerShip> ().getMapManager().mapLines.Length - 1));
		return (int)(Random.value * (_mapManager.mapLines.Length - 1));
		//return 0.0f;
	}

	public void onTriggerEnter2D()
	{
	}
	//Spawns new flipper enemy on field, associated with map line
	public void createNew()
	{

		//float _rand1;
		int _rand1;
		if (levelNum == 1)
		{
			_straightMovement = true;
		}
		else
		{
			_straightMovement = false;
		}
		_rand1 = random ();
		//_vertex1 = player.GetComponent<PlayerShip> ().getMapManager ().mapVertices [_rand];
		//_vertex2 = player.GetComponent<PlayerShip> ().getMapManager ().mapVertices [_rand + 1];
		//thisMapLine = player.GetComponent<PlayerShip> ().getMapManager ().mapLines [_rand1];
		thisMapLine = _mapManager.mapLines [_rand1];
		_vertex1 = thisMapLine.startPos;
		_vertex2 = thisMapLine.endPos;
		_lineCenter = (_vertex1 + _vertex2) / 2;
		//_mapDepth = player.GetComponent<PlayerShip> ().getMapManager ().getDepth ();
		//_mapDepth = player.GetComponent<PlayerShip> ().getMapManager ().depth;
		_mapDepth = _mapManager.depth;
		//GameObject newFlipper = Instantiate (flipperEnemy, new Vector3 (_lineCenter.x, _lineCenter.y, _lineCenter.z - _mapDepth));

		GameObject newFlipper = Instantiate (flipperEnemy, _lineCenter + new Vector3 (0, 0, -1 * _mapDepth), rb.rotation);
	}

}
