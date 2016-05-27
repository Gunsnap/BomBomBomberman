using UnityEngine;
using System.Collections;

public class hero : MonoBehaviour {
	
	/// Bruges den ikke kun som et startdelay? - DVS
	static float tidGemt;

	/// Bruges til at smide en bombe
	private Spawner sp;

	/// Bruges til at animere spilleren
	public Animator animator;

	// Bomb
	/// A bomb may be placed.
	private bool AllowBomb;
	//public float BombRate = 0.5f;
	//private float nextBomb = 0.0f;
	//private int range = 1;

	void Start () {
		tidGemt = Time.time + 1f;
		sp = gameObject.GetComponent<Spawner> ();
		animator = gameObject.GetComponent<Animator> ();
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
			if (Input.GetKeyDown ("space")) {
				//nextBomb = tid + BombRate;
				animator.SetTrigger ("PutBomb");
				sp.SpawnElement (transform.position, new Vector3 (270, 0, 0));
				AllowBomb = false;
			} else {
				Debug.Log ("Der må smides bombe, men der er ikke trykket");
			}
		} else {
			Debug.Log ("Du må ikke smide en bombe");
		}

	}
	// Lukker update


	void OnTriggerEnter(Collider other) {

		string playerColli = other.name;

		if(playerColli.Contains("Speed-UpPickup")){
			DestroyObject (other.gameObject);
		} else if (playerColli.Contains("Fire-UpPickup")) {
			DestroyObject (other.gameObject);
		} else if (playerColli.Contains("Explosion")) {
			Debug.Log ("Du' døøøøj");
		}

	}

}
// Lukker class