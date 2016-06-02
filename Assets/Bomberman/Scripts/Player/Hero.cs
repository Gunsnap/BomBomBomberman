using UnityEngine;
using System.Collections;
using System;

public class Hero : MonoBehaviour {
	/// Bruges til at smide en bombe
	private Spawner sp;

	/// Bruges til at animere spilleren
	public Animator animator;

	public int myGlobal;
	public int kills;
	public uint distance;

	// Bomb
	/// Max antal bomber af gangen.
	public uint bombsMax;
	public uint bombsDown;
	/// Længden på flammer
	public uint bombRange;
	public float fuseTime;
	public bool sickFuse;

	void Awake () {
		GetComponentInParent<GameState> ().livingPlayers++;

		GlobalControl gc = GetComponentInParent<GlobalControl> ();

		/*for (int n = 0; n < gc.playerName.Length; n++) {
			if (name.Equals (gc.playerName [n])) {
				myGlobal = n + 1;
				break;
			} else {
				myGlobal = gc.playerDistance.Length;
			}
		}*/

		/*string[] tmpPlayerName = gc.playerName;
		string[] tmpPlayerNick = gc.playerNick;
		int[] tmpPlayerKills = gc.playerKills;
		uint[] tmpPlayerDistance = gc.playerDistance;
		uint[] tmpPowerUpCount = gc.powerUpCount;*/

		/*if (myGlobal == 0)
			for (int i = 0; i < tmpPlayerKills.Length; i++) {
				gc.playerKills [i] = tmpPlayerKills [i];
				gc.playerDistance [i] = tmpPlayerDistance [i];
			}*/

		/*kills = gc.playerKills [myGlobal];
		distance = gc.playerDistance [myGlobal];*/
	}

	void Start () {
		sp = GetComponent<Spawner> ();
		animator = GetComponent<Animator> ();

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