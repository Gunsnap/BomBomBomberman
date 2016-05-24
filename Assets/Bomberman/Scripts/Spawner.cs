using UnityEngine;
using System.Collections;


// Link til hjælp: www.youtube.com/watch?v=M_xXmpI0GYs

public class Spawner : MonoBehaviour {

	public GameObject[] whatToSpawnPrefab;
	public GameObject[] whatToSpawnClone;

	public void SpawnSomethingAwesome (Vector3 SpawnPos, Vector3 rot, int element = 0) {
		Debug.Log (SpawnPos);
		whatToSpawnClone [element] = Instantiate (whatToSpawnPrefab [element], SpawnPos, Quaternion.Euler (rot)) as GameObject;
	}
}