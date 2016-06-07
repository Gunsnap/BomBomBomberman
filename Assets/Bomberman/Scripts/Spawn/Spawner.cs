using UnityEngine;

// Link til hjælp: www.youtube.com/watch?v=M_xXmpI0GYs

public class Spawner : MonoBehaviour {
	/// Indeholder de objekter der kan klones.
	public GameObject[] whatToSpawnPrefab;
	/// Indeholder de objekter der er klonet ind på banen.
	public GameObject[] whatToSpawnClone;

	/** Spawner et objekt ind på banen. ud fra position, rotation og hvilket element i Prefab */
	public GameObject SpawnElement (Vector3 SpawnPos, Vector3 rot, int element = 0) {
		whatToSpawnClone [element] = (GameObject)Instantiate (whatToSpawnPrefab [element], SpawnPos, Quaternion.Euler (rot));

		return whatToSpawnClone [element];
	}
}