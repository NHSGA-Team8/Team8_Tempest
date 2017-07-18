﻿using System.Collections;
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
	public string inputAxis = "Horizontal";
	public GameObject explodePrefab;

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
	private float _inputValue;
	private int _isCW = 0; //isClockWise: 1 = CW
	private int _currPlayerNum;
	private AudioSource _audioSource;

	Rigidbody rb;
	//Audio
	public AudioClip soundFire;
	public AudioClip soundDeath;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		_reloaded = true;
		//respawnTime = 0.2f;
		//_rand = Random.value * _mapManager.mapVertices.Length;
		//_rand = RandomVal ();
		//print(Console.WriteLine(MapManager.mapVertices[1]));
		_audioSource = GetComponent<AudioSource> ();
		_mapManager = GameObject.Find("MapManager").GetComponent<MapManager> ();

		if (levelNum == 1)
		{
			_straightMovement = true;
		}
		else
		{
			_straightMovement = false;
		}

		/*
		if (Random.value > 0.5)
			_isCW = 1;
		else
			_isCW = -1;
		*/
	}

	// Update is called once per frame
	void Update ()
	{
		if (rb.position.z <= 0) //In case the player ship is flying in after respawning?
		{
			Vector3 newPos;
			MapLine newMapLine;
			//transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
			rb.MovePosition (new Vector3 (transform.position.x, transform.position.y, 0));
			rb.constraints = RigidbodyConstraints.FreezePositionZ;
			_currPlayerNum = GameObject.Find ("Player").GetComponent<PlayerShip> ().curMapLine.GetLineNum ();
			if (_isCW == 0)
			{
				int _beCW = _currPlayerNum - thisMapLine.GetLineNum ();
				int _beCCW = _mapManager.mapLines.Length - _currPlayerNum + thisMapLine.GetLineNum ();
				if (_beCW > _beCCW)
				{
					_isCW = 1;
				}
				else if (_beCW < _beCCW)
				{
					_isCW = -1;
				}
				else //Equal distance from player
				{
					if (Random.value > 0.5)
					{
						_isCW = 1;
					}
					else
					{
						_isCW = -1;
					}
				}
			}
			//Move (_isCW);
			thisMapLine.UpdateMovement (transform.position, Time.deltaTime * _isCW * movementForce * 0.2f, out newPos, out newMapLine);
			rb.MovePosition (new Vector3(newPos.x, newPos.y, 0));
			if (newMapLine != null)
			{
				thisMapLine = newMapLine;
			}
		}
		else if (_straightMovement)
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
	}
	/*
	void Move(int dir){
			Vector3 newPos;
			MapLine newMapLine;
			thisMapLine.UpdateMovement (transform.position, Time.deltaTime * dir * movementForce, out newPos, out newMapLine);
			//rb.MovePosition (newPos);
			rb.MovePosition (new Vector3(newPos.x, newPos.y, 0));
			if (newMapLine != null)
			{
				thisMapLine = newMapLine;
			}
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
		GameObject newExplosion = Instantiate (explodePrefab, gameObject.transform.position, gameObject.transform.rotation);
		AudioSource explosionSource = newExplosion.GetComponent<AudioSource> ();
		explosionSource.clip = soundDeath;
		explosionSource.Play ();
		gameObject.SetActive (false); // Disable enemy
	}

	//void OnCollisionEnter(Collision collider) {
	void onTriggerEnter(Collider other)
	{
		/*
		if (collider.gameObject.GetComponent<PlayerShip> ()) {
			collider.gameObject.GetComponent<PlayerShip> ().TakeDamage (1);
			OnDeath ();
		}
		*/
		if (other.GetType() == typeof(PlayerShip)) {
			other.gameObject.GetComponent<PlayerShip> ().TakeDamage (1);
			OnDeath ();
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

	public void SetMapLine(MapLine newML) {
		thisMapLine = newML;
	}
}
