using UnityEngine;
using System.Collections;


// Link til hjælp: www.youtube.com/watch?v=M_xXmpI0GYs

public class Spawner : MonoBehaviour {

	public GameObject[] whatToSpawnPrefab;
	public GameObject[] whatToSpawnClone;

	public GameObject SpawnElement (GameObject placer, Vector3 SpawnPos, Vector3 rot, int element = 0, uint range = 0) {
		if (whatToSpawnPrefab [element].name.Contains ("TNT")) {
			whatToSpawnClone [element] = (GameObject)Instantiate (whatToSpawnPrefab [element], SpawnPos, Quaternion.Euler (rot));
			BombSplode bombe = whatToSpawnClone [element].gameObject.GetComponent<BombSplode> ();
			bombe.range = range;
			bombe.placer = placer;
		} else {
			whatToSpawnClone [element] = (GameObject)Instantiate (whatToSpawnPrefab [element], SpawnPos, Quaternion.Euler (rot));
		}

		return whatToSpawnClone [element];
	}
}