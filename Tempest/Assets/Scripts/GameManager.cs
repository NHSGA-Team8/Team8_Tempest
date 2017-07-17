using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject flipperPrefab;
	public int totalFlippers;
	public float roundTotalTime;
	public int currentRound;
	public int nextScene;

	private float _lastSpawn;
	private int _flipperCount;
	private Flipper[] flippers;

	// Use this for initialization
	void Start () {
		flippers = new Flipper[totalFlippers];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
