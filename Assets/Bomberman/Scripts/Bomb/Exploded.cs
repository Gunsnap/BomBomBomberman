using UnityEngine;
using System.Collections;

public class Exploded : MonoBehaviour {

	float putTime;
	float splosionDelay;

	void Start () {
		putTime = Time.time;
		splosionDelay = .8f;
	}

	void Update () {
		float timeNow = Time.time;
		if (timeNow > putTime + splosionDelay)
			DestroyObject (this.gameObject);
	}

	void OnTriggerEnter (Collider other) {
		if (other.name.Contains ("Player")) {
			Animator playerAni = other.gameObject.GetComponent<hero> ().animator;
			playerAni.SetBool ("Win", false);
			playerAni.SetTrigger ("GameEnd");
		} else if (other.name.Contains ("BrickBlock")) {
			BombeRamt (other);
		}
	}

	static void BombeRamt (Collider other) {
		//Spawn pickUp
		if (Random.Range (0, 2) == 1) {
			Spawner sp = other.gameObject.GetComponent<Spawner> ();
			sp.SpawnElement (other.transform.position, new Vector3 (270, 0, 0), Random.Range (0, 2));
		}

		//Updater Grid
		DestroyObject (other.gameObject);
		ForbiddenTilesVores.RegisterSquare (other.gameObject.transform.position, true);
	}

}