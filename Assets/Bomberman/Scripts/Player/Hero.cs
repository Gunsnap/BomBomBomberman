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
	public uint bombsMax;
	public uint bombsDown;
	/// Længden på flammer
	public uint bombRange;
	public float fuseTime;
	public bool sickFuse;

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

		sickFuse = false;
		fuseTime = 3.5f;
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

				sp.SpawnElement (gameObject, placePos, new Vector3 (270, 0, 0), 0, bombRange, fuseTime);
				bombsDown++;
			}
		}

		if (sickFuse) {
			float tidSyg = Time.time + 7f;
			if (Time.time > tidSyg) {
				sickFuse = false;
				fuseTime = 3.5f;
			}
		}

	}
	// Lukker update
}
// Lukker class