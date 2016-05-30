using UnityEngine;
using System.Collections;

public class Exploded : MonoBehaviour {

	float putTime;
	float splosionDelay;
	public Collider[] others;

	void Start () {
		putTime = Time.time;
		splosionDelay = .3f;

		others.Initialize ();
	}

	void Update () {
		float timeNow = Time.time;
		if (timeNow > putTime + splosionDelay) {
			DestroyObject (this.gameObject);
			Destroy (this);
		}
	}

	/*
	 * FIXME bliver ikke kaldt!
	void OnCollisionEnter (Collision coll) {
		Collider[] tmpCols = others;
		others = new Collider[others.Length + 1];
		for (int i = 0; i < tmpCols.Length; i++) {
			others [i] = tmpCols [i];
		}
		others [others.Length - 1] = coll.collider;
		foreach (Collider col in others) {
			Debug.Log (name + " ramte " + col.name);
		}
	}*/

	void OnTriggerEnter (Collider other) {
		if (other.name.Contains ("BrickBlock")) {
			BombeRamt (other);
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