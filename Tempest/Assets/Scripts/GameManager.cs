using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Ethan Zhu and Rachael H.
 */
public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject flipperPrefab;
	public int totalFlippers;
	public float spawnDelay;
	public float roundTotalTime;
	public int currentRound;
	public int nextScene;
	public int totalLives;

	public GameObject flipperShell; //Enemy Projectile

	public enum GAMESTATE {PREGAME, STARTING, PLAYING, ENDING};
	public GAMESTATE curGamestate = GAMESTATE.PREGAME;

	[HideInInspector] public Flipper[] flippers;
	[HideInInspector] public int curLives;

	private float _lastSpawn;
	private int _flipperCount;
	private float _startTime;
	private GameObject _playerRef;
	private MapManager _mapManager;

	// Use this for initialization
	void Start () {
		flippers = new Flipper[totalFlippers];
		_mapManager = GameObject.Find ("MapManager").GetComponent<MapManager> ();
		_playerRef = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator GameLoop()
	{
		curGamestate = GAMESTATE.STARTING;
		yield return StartCoroutine(RoundStarting());
		curGamestate = GAMESTATE.PLAYING;
		yield return StartCoroutine(RoundPlaying());
		curGamestate = GAMESTATE.ENDING;
		yield return StartCoroutine(RoundEnding());

		if (_playerRef == null || _playerRef.activeSelf == false)
		{
			// Back to menu if dead
			//SceneManager.LoadScene(0);
		}
		else
		{
			// Next level if win
		}
	}

	private IEnumerator RoundStarting() {
		SpawnPlayerShip ();

		yield return new WaitForSeconds(3);
		_startTime = Time.fixedTime;
	}

	private IEnumerator RoundPlaying() {

		while (curLives > 0 || roundTotalTime > Time.fixedTime - _startTime)
			yield return null;
	}

	private IEnumerator RoundEnding() {
		yield return new WaitForSeconds(3);
	}

	void SpawnPlayerShip() {
		if (_playerRef == null) {
			_playerRef = Instantiate (playerPrefab, _mapManager.mapLines [_mapManager.startMapLineIndex].GetMidPoint (), Quaternion.Euler (0f, 0f, 0f));
		} else {
			_playerRef.SetActive (true);
			//_playerRef.transform.position = 
		}
	}

	public void PlayerDied() {

	}

	private IEnumerator Spawn ()
	{
		for (float f = 1f; f >= 0; f -= 0.1f)
		{
			CreateNew ();
			yield return new WaitForSeconds (spawnDelay);
		}
	}
	//Random spawn point
	public int RandomVal()
	{
		return (int)(Random.value * (_mapManager.mapLines.Length - 1));
	}
	//Spawns new flipper enemy on field, associated with map line
	public void CreateNew()
	{
		//float _rand1;
		int _rand1;
		bool _straightMovement1;
		MapLine thisMapLine;
		Vector3 _vertex1, _vertex2, _lineCenter;
		float _mapDepth;
		if (currentRound == 1)
		{
			_straightMovement1 = true;
		}
		else
		{
			_straightMovement1 = false;
		}
		_rand1 = RandomVal ();
		thisMapLine = _mapManager.mapLines [_rand1];
		_vertex1 = thisMapLine.startPos;
		_vertex2 = thisMapLine.endPos;
		_lineCenter = (_vertex1 + _vertex2) / 2;
		_mapDepth = _mapManager.depth;
		GameObject newFlipper = Instantiate (flipperPrefab, _lineCenter + new Vector3 (0, 0, -1 * _mapDepth), flipperPrefab.GetComponent<Rigidbody> ().rotation);
	}
}
