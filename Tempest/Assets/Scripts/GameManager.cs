using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject flipperPrefab;
	public int totalFlippers;
	public float spawnDelay;
	public float roundTotalTime;
	public int currentRound;
	public int nextScene;
	public int totalLives;

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

}
