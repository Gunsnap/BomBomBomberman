using UnityEngine;
using UnityEngine.SceneManagement;

public class Exploded : MonoBehaviour {
	/// Tid der går inden flammen skal forsvinde.
	float splosionDelay;

	/// Spilleren der har placeret bomben
	public GameObject placer;

	void Start () {
		splosionDelay = .5f + Time.time;
	}

	/// Når tiden er gået fjernes flammen
	void Update () {
		if (Time.time > splosionDelay)
			DestroyObject (this.gameObject);
	}

	/** Kigger på hvad flammen rammer. */
	void OnTriggerEnter (Collider other) {
		if (other.name.Contains ("BrickBlock")) {
			//Spawn pickUp, 1 ud af 3 gange.
			if (Random.Range (0, 3) == 1) {
				GameObject pickup = other.GetComponent<Spawner> ().SpawnElement (
					                    other.transform.position, new Vector3 (270, 0, 0), Random.Range (0, 3));

				// Hvis det er en sygdom ændrer vi navnet så det kan bruges.
				if (pickup.name.Contains ("Sickness"))
					SicknessSpawn (pickup);
			}

			//Updater Grid
			DestroyObject (other.gameObject);
			ForbiddenTilesVores.RegisterSquare (other.transform.position, '0');
		} else if (other.name.Contains ("Player")) {
			SaveData (other);

			// Stopper den ramte spiller, så denne ikke længere kan bevæge sig.
			MovePlayer mover = other.GetComponent<MovePlayer> ();
			mover.doMove = false;
			mover.allowMove = false;

			// Sætter den ramte spillers animation til at denne har tabt.
			Animator playerAni = other.GetComponent<Animator> ();
			playerAni.SetBool ("Run", false);
			playerAni.SetBool ("Win", false);
			playerAni.SetTrigger ("GameEnd");
		}
	}

	/** Laver sygdommen om til en specifik sygdom. Bruges i 'PickUp.cs'. */
	void SicknessSpawn (GameObject obj) {
		obj.transform.position -= new Vector3 (0, 0, .5f);
		switch (Random.Range (0, 4)) {
		case 0:
			obj.name = "Fire-DownPickup";
			break;
		case 1:
			obj.name = "Speed-DownPickup";
			break;
		case 2:
			obj.name = "ReversePickup";
			break;
		case 3:
			obj.name = "PoopPickup";
			break;
		}
	}

	/** Gemmer data om hvem der er ramt og at der er én færre spillere i live. */
	void SaveData (Collider other) {
		GlobalControl gc = GlobalControl.instance;
		// Opdater kills for den ramte spiller.
		Hero hitHero = other.GetComponent<Hero> ();
		hitHero.myKills--;
		gc.playerKills [hitHero.myGlobalID] = hitHero.myKills;
		// Hvis det ikke er selvmord, så opdateres kills for spilleren der har placeret bomben.
		if (!placer.name.Equals (other.name)) {
			Hero placerHero = placer.GetComponent<Hero> ();
			placerHero.myKills++;
			gc.playerKills [placerHero.myGlobalID] = placerHero.myKills;
		}
		// Fortæller GameState at der er én mindre spiller i live.
		GameState gs;
		if (SceneManager.GetActiveScene ().name.Equals ("Level1"))
			gs = other.gameObject.GetComponentInParent<GameState> ();
		else
			gs = GameObject.Find ("GameGrid").GetComponent<GameState> ();
		gs.livingPlayers--;
	}
}