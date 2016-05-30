using UnityEngine;

public class BombSplode : MonoBehaviour {
	float putTime;
	public float bombDelay;
	public float range;

	void Start () {
		putTime = Time.time;
		bombDelay = 3.5f;
	}

	void Update () {
		float timeNow = Time.time;

		//Når der er gået bombDelay sekunder sprænger bomben
		if (timeNow > putTime + bombDelay) {
			blastHallWithFire ();
			DestroyObject (this.gameObject);
		}
	}

	//Flammer spawnes
	public void blastHallWithFire () {
		Spawner sp = gameObject.GetComponent<Spawner> ();
		Vector3 bombPos = transform.position;

		//Retninger
		Vector3 opVecDec = new Vector3 (270, 0, 0);
		Vector3 nedVecDec = new Vector3 (90, 180, 0);
		Vector3 hoejreVecDec = new Vector3 (0, 90, 270);
		Vector3 venstreVecDec = new Vector3 (0, 270, 90);

		//Center
		sp.SpawnElement (bombPos, opVecDec);

		//Mid & End
		for (float i = 0; i <= range; i++) {
			int element = i < range ? 1 : 2;
			sp.SpawnElement (bombPos + new Vector3 (0, i, 0), opVecDec, element);//Op

			//FIXME
			Exploded flamme = sp.whatToSpawnClone [element].gameObject.GetComponent<Exploded> ();
			//Collider bc = flamme.GetComponent<Collider> ();
			//Debug.Log ("Collider " + bc.name);

			sp.SpawnElement (bombPos + new Vector3 (0, -i, 0), nedVecDec, element);//Ned
			sp.SpawnElement (bombPos + new Vector3 (i, 0, 0), hoejreVecDec, element);//Højre
			sp.SpawnElement (bombPos + new Vector3 (-i, 0, 0), venstreVecDec, element);//Venstre
		}
	}

}