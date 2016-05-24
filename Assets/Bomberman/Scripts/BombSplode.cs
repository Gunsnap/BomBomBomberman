using UnityEngine;
using System.Collections;

public class BombSplode : MonoBehaviour {
	public float explodeSec = 2.5f;
	public float putTime;
	public float bombDelay = 3.5f;
	public float timeNow;

	private Spawner spawnFire;

	Vector3 BombPosition;

	void Start () {
		putTime = Time.time;
		var BombObject = GameObject.Find (this.name);
		BombPosition = BombObject.transform.position;

		spawnFire = gameObject.GetComponent<Spawner> ();
		Debug.Log ("Bombe startet");
	}

	void Update () {
		timeNow = Time.time;
		if (timeNow > putTime + bombDelay) {
			removeBomb ();
		}
	}

	public void removeBomb () {
		Debug.Log ("Fjern bombe");
		blastHallWithFire ();
		DestroyObject (this.gameObject);
	}

	public void blastHallWithFire (float range = 3) {
		float posX = BombPosition.x;
		float posY = BombPosition.y;
		float posZ = BombPosition.z;

		//Center
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY, posZ), Vector3.zero);

		//Mid
		for (float i = 0; i < range; i++) {
			spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY, posZ + i), Vector3.zero, 1);//Venstre
			spawnFire.SpawnSomethingAwesome (new Vector3 (posX + i, posY, posZ), new Vector3 (0, 90, 0), 1);//Op
			spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY, posZ - i), new Vector3 (0, 180, 0), 1);//Højre
			spawnFire.SpawnSomethingAwesome (new Vector3 (posX - i, posY, posZ), new Vector3 (0, 270, 0), 1);//Ned
		}

		//End
		/*
		spawnFire.SpawnSomethingAwesome (Vector3.Dot(), Vector3.zero, 2);//Venstre
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX + range, posY, posZ), new Vector3 (0, 90, 0), 2);//Op
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY, posZ - range), new Vector3 (0, 180, 0), 2);//Højre
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX - range, posY, posZ), new Vector3 (0, 270, 0), 2);//Ned
		*/
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY, posZ + range), Vector3.zero, 2);//Venstre
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX + range, posY, posZ), new Vector3 (0, 90, 0), 2);//Op
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY, posZ - range), new Vector3 (0, 180, 0), 2);//Højre
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX - range, posY, posZ), new Vector3 (0, 270, 0), 2);//Ned
	}

}