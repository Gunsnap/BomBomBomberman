﻿using UnityEngine;
using System.Collections;

public class Exploded : MonoBehaviour {

	float putTime;
	float splosionDelay;
	public GameObject placer;

	void Start () {
		putTime = Time.time;
		splosionDelay = .3f;
	}

	void Update () {
		float timeNow = Time.time;
		if (timeNow > putTime + splosionDelay) {
			DestroyObject (this.gameObject);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.name.Contains ("BrickBlock")) {
			//Spawn pickUp
			if (Random.Range (0, 3) == 1) {
				Spawner sp = other.GetComponent<Spawner> ();
				sp.SpawnElement (other.gameObject, other.transform.position, new Vector3 (270, 0, 0), Random.Range (0, 3));
			}

			//Updater Grid
			DestroyObject (other.gameObject);
			ForbiddenTilesVores.RegisterSquare (other.transform.position, '0');
		} else if (other.name.Contains ("Player")) {
			#region SaveData
			//Opdater kills
			if (!placer.name.Equals (other.name)) {
				Hero placerHero = placer.GetComponent<Hero> ();
				placerHero.kills++;
				GlobalControl.instance.playerKills [placerHero.myGlobal] = placerHero.kills;
			}
			Hero otherHero = other.GetComponent<Hero> ();
			otherHero.kills--;
			GlobalControl.instance.playerKills [otherHero.myGlobal] = otherHero.kills;

			other.gameObject.GetComponentInParent <GameState> ().livingPlayers--;
			#endregion

			MovePlayer mover = other.GetComponent<MovePlayer> ();
			mover.doMove = false;
			mover.allowMove = false;

			Animator playerAni = other.GetComponent<Animator> ();
			playerAni.SetBool ("Run", false);
			playerAni.SetBool ("Win", false);
			playerAni.SetTrigger ("GameEnd");
		} else {
			Debug.Log ("Trigger på " + other.name);
		}
	}
}