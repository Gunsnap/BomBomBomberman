using UnityEngine;
using System.Collections;
using System;

public class Hero : MonoBehaviour {
	/// Bruges til at smide en bombe
	private Spawner sp;

	/// Bruges til at animere spilleren
	public Animator animator;

	public int myGlobal;
	public int playerKills;

	// Bomb
	/// Max antal bomber af gangen.
	public uint bombsMax = 1;
	public uint bombsDown = 0;
	/// Længden på flammer
	public uint bombRange = 0;

	void Start () {
		sp = GetComponent<Spawner> ();
		animator = GetComponent<Animator> ();

		int[] tmpInt = GlobalControl.instance.playerKills;
		GlobalControl.instance.playerKills = new int[tmpInt.Length + 1];

		int i = 0;
		for (; i < tmpInt.Length; i++) {
			GlobalControl.instance.playerKills [i] = tmpInt [i];
		}
		myGlobal = i;
		GlobalControl.instance.playerKills [i] = playerKills;
	}

	void Update () {
		if (bombsDown < bombsMax) {
			bool placerBombe = false;

			if (name.Equals ("Player1") && Input.GetKeyDown (KeyCode.Space))
				placerBombe = true;
			else if (name.Equals ("Player2") && Input.GetKeyDown (KeyCode.Return))
				placerBombe = true;
			

			if (placerBombe) {
				animator.SetTrigger ("PutBomb");

				// Placer bomben i grid
				Vector3 placePos = transform.position;
				placePos.x = (int)placePos.x + .5f;
				placePos.y = (int)placePos.y + .5f;

				sp.SpawnElement (gameObject, placePos, new Vector3 (270, 0, 0), 0, bombRange);
				bombsDown++;
			}
		}

	}
	// Lukker update

	//FIXME smid PickUp over på Pickup
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
		}
	}
}
// Lukker class