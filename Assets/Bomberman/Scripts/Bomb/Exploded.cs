using UnityEngine;
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
			BombeRamt (other);
		} else if (other.name.Contains ("Player")) {
			#region SaveData
			//Opdater kills
			if (!placer.name.Equals (other.name)) {
				Hero placerHero = placer.GetComponent<Hero> ();
				placerHero.playerKills++;
				GlobalControl.instance.playerKills [placerHero.myGlobal] = placerHero.playerKills;
			}
			Hero otherHero = other.GetComponent<Hero> ();
			otherHero.playerKills--;
			GlobalControl.instance.playerKills [otherHero.myGlobal] = otherHero.playerKills;

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

	static void BombeRamt (Collider other) {
		//Spawn pickUp
		if (Random.Range (0, 3) == 1) {
			Spawner sp = other.GetComponent<Spawner> ();
			sp.SpawnElement (other.gameObject, other.transform.position, new Vector3 (270, 0, 0), Random.Range (0, 2));
		}

		//Updater Grid
		DestroyObject (other.gameObject);
		ForbiddenTilesVores.RegisterSquare (other.transform.position, '0');
	}
}