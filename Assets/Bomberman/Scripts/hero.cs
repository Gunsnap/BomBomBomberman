using UnityEngine;
using System.Collections;

public class hero : MonoBehaviour {

	//Tid
	static int SekTidGemt;

	// Spawner
	private Spawner sp;

	// Bomb
	public bool putbomb;
	public float BombRate = 0.5f;
	private bool AllowBomb;
	//	private float nextBomb = 0.0f;
	//	private int range = 1;


	// Use this for initialization
	void Start () {
		Debug.Log ("HeroScript startet");
		SekTidGemt = 2;
		sp = gameObject.GetComponent<Spawner> ();
	}
	
	// Update is called once per frame
	void Update () {

		float tid = Time.fixedTime;
		int SekTid = (int)tid; // Tid spillet har været i gang

		// player1 pos
		Vector3 PlayerPos = GameObject.Find ("Player1").transform.position;

		// Bombe
		if (SekTidGemt < SekTid) {
			AllowBomb = true;
			SekTidGemt = SekTid;
		} 

		if (AllowBomb) {
			if (Input.GetKeyDown ("space")) {
				//nextBomb = tid + BombRate;
				putbomb = true;
				sp.SpawnElement (PlayerPos, new Vector3 (270, 0, 0));
				AllowBomb = false;
			} else {
				putbomb = false;
				Debug.Log ("Der må smides bombe, men der er ikke trykket");
			}
		} else {
			Debug.Log ("Du må ikke smide en bombe");
		}

	}
	// Lukker update
}
// Lukker class
