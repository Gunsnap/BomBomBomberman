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
	}

	void Update () {
		timeNow = Time.time;
		if (timeNow > putTime + bombDelay)
			removeBomb ();
	}

	public void removeBomb () {
		blastHallWithFire ();
		DestroyObject (this.gameObject);
	}

	public void blastHallWithFire (float range = 3) {
		//Retninger
		Vector3 opVecDec = new Vector3 (270, 0, 0);
		Vector3 nedVecDec = new Vector3 (90, 180, 0);
		Vector3 hoejreVecDec = new Vector3 (0, 90, 270);
		Vector3 venstreVecDec = new Vector3 (0, 270, 90);

		//Center
		spawnFire.SpawnSomethingAwesome (BombPosition, opVecDec);

		//Mid & End
		for (float i = 0; i <= range; i++) {
			int element = i < range ? 1 : 2;
			spawnFire.SpawnSomethingAwesome (BombPosition + new Vector3 (0, i, 0), opVecDec, element);//Op
			spawnFire.SpawnSomethingAwesome (BombPosition + new Vector3 (0, -i, 0), nedVecDec, element);//Ned
			spawnFire.SpawnSomethingAwesome (BombPosition + new Vector3 (i, 0, 0), hoejreVecDec, element);//Højre
			spawnFire.SpawnSomethingAwesome (BombPosition + new Vector3 (-i, 0, 0), venstreVecDec, element);//Venstre
		}
	}

}