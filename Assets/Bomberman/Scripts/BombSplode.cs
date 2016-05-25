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
		Debug.Log (BombPosition);
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

	public void blastHallWithFire (float range = 1) {
		float posX = BombPosition.x;
		float posY = BombPosition.y;
		float posZ = BombPosition.z;

		//Center
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY, posZ), new Vector3(-90,0,0));

		//Mid
		for (float i = 0; i < range; i++) {
			spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY, posZ + i), new Vector3(-90,0,0), 1);//Venstre
			spawnFire.SpawnSomethingAwesome (new Vector3 (posX + i, posY, posZ), new Vector3 (-90, 90, 0), 1);//Op
			spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY, posZ - i), new Vector3 (-90, 180, 0), 1);//Højre
			spawnFire.SpawnSomethingAwesome (new Vector3 (posX - i, posY, posZ), new Vector3 (-90, 270, 0), 1);//Ned
		}

		//End
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY, posZ + range), new Vector3(-90,0,0), 2);//Venstre
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX + range, posY, posZ), new Vector3 (-90, 90, 0), 2);//Op
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY, posZ - range), new Vector3 (-90, 180, 0), 2);//Højre
		spawnFire.SpawnSomethingAwesome (new Vector3 (posX - range, posY, posZ), new Vector3 (-90, 270, 0), 2);//Ned
	}

}