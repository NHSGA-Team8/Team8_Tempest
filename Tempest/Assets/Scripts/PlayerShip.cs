using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour, IShipBase {

	// The axis used to take input.
	public string inputAxis = "Horizontal";
	public Rigidbody bullet;
	public Transform fireTransform;
	public int maxBullets = 7;
	public float fireCooldown = 0.2f;
	public MapLine curMapLine;

	// References to the MapManager and GameManager
	private MapManager _mapManager;
	private GameManager _gameManager;
	// The value of input, updated each frame.
	private float _inputValue;
	private Quaternion _desiredRotation;
	private int _curBullets;
	private float _lastFire;
	private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
		_curBullets = 0;
		_rigidbody = GetComponent<Rigidbody> ();
		_mapManager = GameObject.Find ("MapManager").GetComponent<MapManager> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){

		_inputValue = Input.GetAxis (inputAxis);

		Move ();

		if (Input.GetKey (KeyCode.Space) && _lastFire + fireCooldown < Time.fixedTime && _curBullets < maxBullets) {
			Fire ();
			_lastFire = Time.fixedTime;
		}
	}

	// Called each update to move sideways
	void Move(){
		Vector3 newPos;

		MapLine newMapLine = curMapLine.UpdateMovement (transform.position, Time.deltaTime * _inputValue, out newPos);

		_rigidbody.MovePosition (newPos);
	}

	// Called to fire a projectile.
	public void Fire(){
		_curBullets++;

		Rigidbody shellInstance = Instantiate (bullet, fireTransform.position, fireTransform.rotation) as Rigidbody;
		shellInstance.GetComponent<PlayerBullet> ().SetShip (gameObject);
		shellInstance.velocity = 10f * (fireTransform.forward); 
	}

	// Called when a projectile damages the ship. Should call OnDeath() if it kills;
	public void TakeDamage(int dmg){
		// Since the player is dead on touch, just destroy it
		OnDeath();
	}

	// Called when the ship dies. Add points, do game state detection, etc.
	public void OnDeath(){

	}

	public void BulletDestroyed() {
		_curBullets--;
	}

	public MapManager getMapManager() {
		return _mapManager;
	}
}
