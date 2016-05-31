using UnityEngine;
using System.Collections;


// Link til hjælp: www.youtube.com/watch?v=M_xXmpI0GYs

public class Spawner : MonoBehaviour {

	public GameObject[] whatToSpawnPrefab;
	public GameObject[] whatToSpawnClone;

	public GameObject SpawnElement (Vector3 SpawnPos, Vector3 rot, int element = 0, int range = 0) {
		if (whatToSpawnPrefab [element].name.Contains ("TNT")) {
			whatToSpawnClone [element] = (GameObject)Instantiate (whatToSpawnPrefab [element], SpawnPos, Quaternion.Euler (rot));
			whatToSpawnClone [element].gameObject.GetComponent<BombSplode> ().range = range;
		} else {
			whatToSpawnClone [element] = (GameObject)Instantiate (whatToSpawnPrefab [element], SpawnPos, Quaternion.Euler (rot));
		}

		return whatToSpawnClone [element];
	}
}