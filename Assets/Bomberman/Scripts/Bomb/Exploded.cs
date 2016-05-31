using UnityEngine;
using System.Collections;

public class Exploded : MonoBehaviour {

	float putTime;
	float splosionDelay;

	void Start () {
		putTime = Time.time;
		splosionDelay = .3f;
	}

	void Update () {
		float timeNow = Time.time;
		if (timeNow > putTime + splosionDelay) {
			DestroyObject (this.gameObject);
			Destroy (this);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.name.Contains ("BrickBlock")) {
			BombeRamt (other);
		} else if (other.name.Contains ("SteelBlock")) {
			Debug.Log ("Steel ramt");
		} else {
			Debug.Log ("Trigger på " + other.name);
		}
	}

	static void BombeRamt (Collider other) {
		//Spawn pickUp
		if (Random.Range (0, 2) == 1) {
			Spawner sp = other.GetComponent<Spawner> ();
			sp.SpawnElement (other.transform.position, new Vector3 (270, 0, 0), Random.Range (0, 2));
		}

		//Updater Grid
		DestroyObject (other.gameObject);
		ForbiddenTilesVores.RegisterSquare (other.transform.position, true);
	}

}