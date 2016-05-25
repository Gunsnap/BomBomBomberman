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

	public void blastHallWithFire (float range = 3) {

		Vector3 opVecDec = new Vector3 (270, 0, 0);
		Vector3 nedVecDec = new Vector3 (270, 0, 180);
		Vector3 venstreVecDec = new Vector3 (0, 270, 90);
		Vector3 hoejreVecDec = new Vector3 (0, 270, 180);

		Debug.Log (venstreVecDec);

		//Center
		spawnFire.SpawnSomethingAwesome (BombPosition, opVecDec);

		//Mid
		for (float i = 0; i < range; i++) {
			spawnFire.SpawnSomethingAwesome (BombPosition + new Vector3 (0, i, 0), opVecDec, 1);//Op
			//spawnFire.SpawnSomethingAwesome (BombPosition + new Vector3 (0, -i, 0), venstreVecDec, 1);//Ned
			//spawnFire.SpawnSomethingAwesome (BombPosition + new Vector3 (i, 0, 0), venstreVecDec, 1);//Højre
			spawnFire.SpawnSomethingAwesome (BombPosition + new Vector3 (-i, 0, 0), venstreVecDec, 1);//Venstre
		}

		//End
		//spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY + range, posZ), nulVec, 2);//Venstre
		spawnFire.SpawnSomethingAwesome (BombPosition + new Vector3 (0, range, 0), opVecDec, 2);//Op
		//spawnFire.SpawnSomethingAwesome (new Vector3 (posX, posY - range, posZ), new Vector3 (270, 0, 180), 2);//Højre
		//spawnFire.SpawnSomethingAwesome (new Vector3 (posX - range, posY, posZ), new Vector3 (270, 0, 270), 2);//Ned
	}

}