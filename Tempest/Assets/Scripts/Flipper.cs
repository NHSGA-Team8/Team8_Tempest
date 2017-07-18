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
		//_rand = RandomVal ();
		//print(Console.WriteLine(MapManager.mapVertices[1]));

		_mapManager = GameObject.Find("MapManager").GetComponent<MapManager> ();

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
			//rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
			//rb.AddForce (-1 * movementForce * transform.forward * Time.deltaTime);

			rb.MovePosition (transform.position + transform.forward * (Time.deltaTime * movementForce * -1));
		}
		else
		{
			//Move forward by one or a few pixels
			//While moving to next section of map
		}
		/*
		for (float f = 1f; f >= 0; f -= 0.1f)
		{
			createNew ();
			//yield return new WaitForSeconds (respawnTime);
		}
		*/
	}

	/*
	IEnumerator Spawn ()
	{
	}
	*/

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

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.GetComponent<PlayerShip> ()) {
			
			collision.gameObject.GetComponent<PlayerShip> ().TakeDamage (1);
			Destroy (gameObject);
		}
	}


	public bool GetStraightMovement()
	{
		return _straightMovement;
	}
	public void SetStraightMovement(bool isStraight)
	{
		_straightMovement = isStraight;
	}
}
