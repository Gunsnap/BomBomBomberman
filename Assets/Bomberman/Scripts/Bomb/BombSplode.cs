using UnityEngine;

public class BombSplode : MonoBehaviour {
	float putTime;
	public float bombDelay;
	public float range;

	bool up;
	bool down;
	bool left;
	bool right;

	void Start () {
		putTime = Time.time;
		bombDelay = 3.5f;

		up = true;
		down = true;
		left = true;
		right = true;
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
		for (float i = 0; i <= range; i++) {
			int element = i < range ? 1 : 2;

			GameObject senesteFlamme;

			if (up) {
				senesteFlamme = sp.SpawnElement (bombPos + new Vector3 (0, i, 0), opVecDec, element);
				bool lovligt = ForbiddenTilesVores.CheckSquare (senesteFlamme.transform.position + new Vector3 (0, 1, 0));
				if (!lovligt) {
					up = false;
					//DestroyObject (senesteFlamme);
				}
			}

			if (down) {
				senesteFlamme = sp.SpawnElement (bombPos + new Vector3 (0, -i, 0), nedVecDec, element);
				bool lovligt = ForbiddenTilesVores.CheckSquare (senesteFlamme.transform.position + new Vector3 (0, -1, 0));
				if (!lovligt)
					down = false;
			}

			if (right) {
				senesteFlamme = sp.SpawnElement (bombPos + new Vector3 (i, 0, 0), hoejreVecDec, element);
				bool lovligt = ForbiddenTilesVores.CheckSquare (senesteFlamme.transform.position + new Vector3 (1, 0, 0));
				if (!lovligt)
					right = false;
			}

			if (left) {
				senesteFlamme = sp.SpawnElement (bombPos + new Vector3 (-i, 0, 0), venstreVecDec, element);
				bool lovligt = ForbiddenTilesVores.CheckSquare (senesteFlamme.transform.position + new Vector3 (-1, 0, 0));
				if (!lovligt)
					left = false;
			}
		}
	}

}