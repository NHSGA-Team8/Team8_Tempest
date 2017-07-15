using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

	// Vertices of a map, from left to right
	// The Z axis does not matter; all ships / enemies have a set value of that
	public Vector3[] mapVertices; 

	// Whether the map loops on the two edges.
	// This should be true for complete shapes (like a O shape) and false for incomplete shapes (like a U shape)
	public bool isLoop;

	// MapLines are generated at Start(), and they represent a line in the map.
	private MapLine[] mapLines;

	// Use this for initialization
	void Start () {
		mapLines = new MapLine[mapVertices.Length - 1];
		for (int i = 0; i < mapLines.Length; i++) {
			mapLines [i] = new MapLine (mapVertices[i], mapVertices[i+1]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Create stuff that forms the map
	public void CreateOutline(){

	}

	// return the normal vector of the line and the pos
	public Quaternion GetVerticeNormal(Vector3 pos){

		Quaternion finalQuat = Quaternion.Euler(0f,0f,0f);

		// TODO Determine which edge the pos is on

		// TODO If not on edge, return null

		// TODO Calculate the normal on the line

		// Return the Quaternion
		return finalQuat;

	}
}
