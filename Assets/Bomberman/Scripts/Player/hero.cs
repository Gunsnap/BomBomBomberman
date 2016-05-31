using UnityEngine;
using System.Collections;
using System;

public class hero : MonoBehaviour {
	
	/// Bruges den ikke kun som et startdelay? - DVS
	static float tidGemt;

	/// Bruges til at smide en bombe
	private Spawner sp;

	/// Bruges til at animere spilleren
	public Animator animator;

	// Bomb
	/// A bomb may be placed.
	bool AllowBomb = false;
	int range = 0;

	void Start () {
		tidGemt = Time.time + 1f;
		sp = GetComponent<Spawner> ();
		animator = GetComponent<Animator> ();
	}

	void Update () {
		// Tid spillet har kørt.
		float tid = Time.time;

		// Bombe
		if (tidGemt < tid) {
			AllowBomb = true;
			tidGemt = tid;
		} 

		if (AllowBomb) {
			if (name.Equals ("Player1")) {
				if (Input.GetKeyDown ("space")) {
					animator.SetTrigger ("PutBomb");

					// Placer bomben i grid
					Vector3 placePos = transform.position;
					placePos.x = (int)placePos.x + .5f;
					placePos.y = (int)placePos.y + .5f;
					sp.SpawnElement (placePos, new Vector3 (270, 0, 0), 0, range);

					AllowBomb = false;
				} else {
					Debug.Log ("Der må smides bombe, men der er ikke trykket");
				}
			}
		} else {
			Debug.Log ("Du må ikke smide en bombe");
		}

	}
	// Lukker update


	void OnTriggerEnter (Collider other) {

		string playerColli = other.name;
		if (playerColli.Contains ("Speed-UpPickup")) {
			DestroyObject (other.gameObject);

			MovePlayer mover;
			mover = GetComponent<MovePlayer> ();
			mover.roamingTime -= 0.1f;
		} else if (playerColli.Contains ("Fire-UpPickup")) {
			DestroyObject (other.gameObject);
			range++;
		} else if (playerColli.Contains ("Beam")) {
			MovePlayer mover;
			mover = GetComponent<MovePlayer> ();
			mover.doMove = false;
			mover.allowMove = false;

			Animator playerAni = GetComponent<Animator> ();
			playerAni.SetBool ("Run", false);
			playerAni.SetBool ("Win", false);
			playerAni.SetTrigger ("GameEnd");
		}

	}
}
// Lukker class