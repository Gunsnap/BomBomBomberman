using UnityEngine;
using System.Collections;
using System;

public class hero : MonoBehaviour {
	/// Bruges til at smide en bombe
	private Spawner sp;

	/// Bruges til at animere spilleren
	public Animator animator;

	// Bomb
	/// Max antal bomber af gangen.
	public uint bombsMax = 1;
	public uint bombsDown = 0;
	/// Længden på flammer
	public uint bombRange = 0;

	void Start () {
		sp = GetComponent<Spawner> ();
		animator = GetComponent<Animator> ();
	}

	void Update () {
		if (bombsDown < bombsMax) {
			if (name.Equals ("Player1")) {
				if (Input.GetKeyDown ("space")) {
					animator.SetTrigger ("PutBomb");

					// Placer bomben i grid
					Vector3 placePos = transform.position;
					placePos.x = (int)placePos.x + .5f;
					placePos.y = (int)placePos.y + .5f;

					sp.SpawnElement (gameObject, placePos, new Vector3 (270, 0, 0), 0, bombRange);
					bombsDown++;
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
			bombRange++;
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