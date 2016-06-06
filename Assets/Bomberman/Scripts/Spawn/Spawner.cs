using UnityEngine;
using System.Collections;


// Link til hjælp: www.youtube.com/watch?v=M_xXmpI0GYs

public class Spawner : MonoBehaviour {

	public GameObject[] whatToSpawnPrefab;
	public GameObject[] whatToSpawnClone;

	public GameObject SpawnElement (GameObject placer, Vector3 SpawnPos, Vector3 rot, int element = 0, float fuseTime = 5) {
		whatToSpawnClone [element] = (GameObject)Instantiate (whatToSpawnPrefab [element], SpawnPos, Quaternion.Euler (rot));

		return whatToSpawnClone [element];
	}
}