using UnityEngine;
using System.Collections;


// Link til hjælp: www.youtube.com/watch?v=M_xXmpI0GYs

public class Spawner : MonoBehaviour {

	public GameObject[] whatToSpawnPrefab;
	public GameObject[] whatToSpawnClone;

	public GameObject SpawnElement (GameObject placer, Vector3 SpawnPos, Vector3 rot, int element = 0, uint range = 0, float fuseTime = 5) {
		if (whatToSpawnPrefab [element].name.Contains ("TNT")) {
			whatToSpawnClone [element] = (GameObject)Instantiate (whatToSpawnPrefab [element], SpawnPos, Quaternion.Euler (rot));
			BombSplode bombe = whatToSpawnClone [element].gameObject.GetComponent<BombSplode> ();
			bombe.range = range;
			bombe.placer = placer;
			bombe.bombDelay = fuseTime;
		} else if (whatToSpawnPrefab [element].name.Contains ("Sickness")) {
			int sygdom = Random.Range (0, 4);
			SicknessSpawn (placer, SpawnPos, rot, element, sygdom, fuseTime);
		} else {
			whatToSpawnClone [element] = (GameObject)Instantiate (whatToSpawnPrefab [element], SpawnPos, Quaternion.Euler (rot));
		}

		return whatToSpawnClone [element];
	}

	void SicknessSpawn (GameObject placer, Vector3 SpawnPos, Vector3 rot, int element, int sygdom, float fuseTime) {
		whatToSpawnClone [element] = (GameObject)Instantiate (whatToSpawnPrefab [element], SpawnPos, Quaternion.Euler (rot));
		switch (sygdom) {
		case 0:
			whatToSpawnClone [element].name = "Fire-DownPickup";
			break;
		case 1:
			whatToSpawnClone [element].name = "Speed-DownPickup";
			break;
		case 2:
			whatToSpawnClone [element].name = "ReversePickup";
			break;
		case 3:
			whatToSpawnClone [element].name = "PoopPickup";
			//FIXME float tmpFlo = placer.GetComponent<Hero> ().fuseTime;
			//placer.GetComponent<Hero> ().fuseTime = tmpFlo / 2;
			break;
		default:
			//FIXME dette vil Thomas gerne have - DVS
			Debug.Log ("Tallet er " + sygdom);
			break;
		}
	}
}