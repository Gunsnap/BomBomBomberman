﻿using UnityEngine;
using System.Collections;
using System;

public class Hero : MonoBehaviour {
	/// Bruges til at smide en bombe
	private Spawner sp;

	/// Bruges til at animere spilleren
	public Animator animator;

	// GlobalControl
	public int myGlobalID;
	public string myName;
	public string myNick;
	public int myKills;
	public uint myDistance;
	public uint myPowerUpCount;

	// Bomb
	/// Max antal bomber af gangen.
	public uint bombsMax;
	public uint bombsDown;
	/// Længden på flammer
	public uint bombRange;
	public float fuseTime;
	public bool sickFuse;

	void Start () {
		#region Global

		GetComponentInParent<GameState> ().livingPlayers++;

		var gc = GlobalControl.instance;

		// Finder ud af om jeg findes i gc
		for (int n = 0; n < gc.playerName.Length; n++) {
			if (name.Equals (gc.playerName [n])) {
				myGlobalID = n;
				break;
			} else {
				myGlobalID = gc.playerName.Length;
			}
		}

		// Hvis jeg ikke findes så forlænges gc med en ny
		if (myGlobalID == gc.playerName.Length) {
			string[] tmpPlayerName = gc.playerName;
			string[] tmpPlayerNick = gc.playerNick;
			int[] tmpPlayerKills = gc.playerKills;
			uint[] tmpPlayerDistance = gc.playerDistance;
			uint[] tmpPlayerPowerUpCount = gc.powerUpCount;

			int nyLength = tmpPlayerName.Length + 1;
			gc.playerName = new string[nyLength];
			gc.playerNick = new string[nyLength];
			gc.playerKills = new int[nyLength];
			gc.playerDistance = new uint[nyLength];
			gc.powerUpCount = new uint[nyLength];

			int i = 0;
			for (; i < nyLength - 1; i++) {
				gc.playerName [i] = tmpPlayerName [i];
				gc.playerNick [i] = tmpPlayerNick [i];
				gc.playerKills [i] = tmpPlayerKills [i];
				gc.playerDistance [i] = tmpPlayerDistance [i];
				gc.powerUpCount [i] = tmpPlayerPowerUpCount [i];
			}

			gc.playerName [i] = name;
			gc.playerNick [i] = "Unknown Warrior";
			gc.playerKills [i] = 0;
			gc.playerDistance [i] = 0;
			gc.powerUpCount [i] = 0;
		}

		// Gemmer tidligere stats fra gc
		myName = gc.playerName [myGlobalID];
		myNick = gc.playerNick [myGlobalID];
		myKills = gc.playerKills [myGlobalID];
		myDistance = gc.playerDistance [myGlobalID];
		myPowerUpCount = gc.powerUpCount [myGlobalID];

		#endregion Global

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