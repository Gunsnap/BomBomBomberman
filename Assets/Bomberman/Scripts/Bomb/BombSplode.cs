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
		if (Time.time > putTime + bombDelay) {
			blastHallWithFire ();
			DestroyObject (gameObject);
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
		bool up = true;
		bool down = true;
		bool left = true;
		bool right = true;

		for (float i = 0; i <= range; i++) {
			int element = i < range ? 1 : 2;

			GameObject senesteFlamme;

			if (up) {
				senesteFlamme = sp.SpawnElement (bombPos + new Vector3 (0, i, 0), opVecDec, element);
				char ramte = ForbiddenTilesVores.CheckSquare (senesteFlamme.transform.position + new Vector3 (0, 1, 0));
				up = setFalseAndDestroy (senesteFlamme, ramte, up);
			}

			if (down) {
				senesteFlamme = sp.SpawnElement (bombPos + new Vector3 (0, -i, 0), nedVecDec, element);
				char ramte = ForbiddenTilesVores.CheckSquare (senesteFlamme.transform.position + new Vector3 (0, -1, 0));
				down = setFalseAndDestroy (senesteFlamme, ramte, down);
			}

			if (right) {
				senesteFlamme = sp.SpawnElement (bombPos + new Vector3 (i, 0, 0), hoejreVecDec, element);
				char ramte = ForbiddenTilesVores.CheckSquare (senesteFlamme.transform.position + new Vector3 (1, 0, 0));
				right = setFalseAndDestroy (senesteFlamme, ramte, right);
			}

			if (left) {
				senesteFlamme = sp.SpawnElement (bombPos + new Vector3 (-i, 0, 0), venstreVecDec, element);
				char ramte = ForbiddenTilesVores.CheckSquare (senesteFlamme.transform.position + new Vector3 (-1, 0, 0));
				left = setFalseAndDestroy (senesteFlamme, ramte, left);
			}
		}
	}

	static bool setFalseAndDestroy (GameObject senesteFlamme, char ramte, bool retning) {
		if (!ramte.Equals ('0')) {
			retning = false;
			if (ramte.Equals ('x'))
				DestroyObject (senesteFlamme);
		}
		return retning;
	}
}