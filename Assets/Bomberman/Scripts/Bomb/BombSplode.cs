using UnityEngine;

public class BombSplode : MonoBehaviour {
	float putTime;
	public float bombDelay;

	/// Længden på flammer
	public uint range;

	/// Hvem der har placeret mig
	public GameObject placer;

	void Start () {
		putTime = Time.time;
		//FIXME (placer.GetComponent<hero> () as hero).farve til bomben
	}

	void Update () {
		if (Time.time > putTime + bombDelay) {
			blastHallWithFire ();
			DestroyObject (gameObject);
			(placer.GetComponent<Hero> () as Hero).bombsDown--;
		}
	}

	/** Flammer spawnes */
	public void blastHallWithFire () {
		Spawner sp = gameObject.GetComponent<Spawner> ();
		Vector3 bombPos = transform.position;
		GameObject senesteFlamme;

		// Retninger
		Vector3 opVecDec = new Vector3 (270, 0, 0);
		Vector3 nedVecDec = new Vector3 (90, 180, 0);
		Vector3 hoejreVecDec = new Vector3 (0, 90, 270);
		Vector3 venstreVecDec = new Vector3 (0, 270, 90);

		// Center
		senesteFlamme = sp.SpawnElement (bombPos, opVecDec);
		senesteFlamme.GetComponent<Exploded> ().placer = placer;

		// Mid & End
		bool up = true;
		bool down = true;
		bool left = true;
		bool right = true;

		for (float i = 0; i <= range; i++) {
			/* Hvis enden er nået sættes element til at være flammeEnd
			 * ellers sættes den til at være flammeMid */
			int element = i < range ? 1 : 2;

			if (up) {
				senesteFlamme = sp.SpawnElement (bombPos + new Vector3 (0, i, 0), opVecDec, element);
				senesteFlamme.GetComponent<Exploded> ().placer = placer;
				char ramte = ForbiddenTilesVores.CheckSquare (senesteFlamme.transform.position + new Vector3 (0, 1, 0));
				up = setFalseAndDestroy (senesteFlamme, ramte, up);
			}

			if (down) {
				senesteFlamme = sp.SpawnElement (bombPos + new Vector3 (0, -i, 0), nedVecDec, element);
				senesteFlamme.GetComponent<Exploded> ().placer = placer;
				char ramte = ForbiddenTilesVores.CheckSquare (senesteFlamme.transform.position + new Vector3 (0, -1, 0));
				down = setFalseAndDestroy (senesteFlamme, ramte, down);
			}

			if (right) {
				senesteFlamme = sp.SpawnElement (bombPos + new Vector3 (i, 0, 0), hoejreVecDec, element);
				senesteFlamme.GetComponent<Exploded> ().placer = placer;
				char ramte = ForbiddenTilesVores.CheckSquare (senesteFlamme.transform.position + new Vector3 (1, 0, 0));
				right = setFalseAndDestroy (senesteFlamme, ramte, right);
			}

			if (left) {
				senesteFlamme = sp.SpawnElement (bombPos + new Vector3 (-i, 0, 0), venstreVecDec, element);
				senesteFlamme.GetComponent<Exploded> ().placer = placer;
				char ramte = ForbiddenTilesVores.CheckSquare (senesteFlamme.transform.position + new Vector3 (-1, 0, 0));
				left = setFalseAndDestroy (senesteFlamme, ramte, left);
			}
		}
	}

	/** Ser på om man rammer noget og hvis det er noget der ikke kan fjernes forsvinder flammen. */
	static bool setFalseAndDestroy (GameObject senesteFlamme, char ramte, bool retning) {
		if (!ramte.Equals ('0')) {
			retning = false;
			if (ramte.Equals ('x'))
				DestroyObject (senesteFlamme);
		}
		return retning;
	}
}