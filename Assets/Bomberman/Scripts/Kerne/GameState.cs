using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

	public bool running;
	public int livingPlayers;

	float tidStart;

	// Use this for initialization
	void Start () {
		running = true;
		tidStart = Time.time + 3f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > tidStart) {
			if (running && livingPlayers < 2) {
				running = false;
			}

			if (!running) {
				LoadNewScene ();
			}
		}
	}

	public void LoadNewScene () {
		var gc = GlobalControl.instance;
		gc.baneID = SceneManager.GetActiveScene ().name;

		SceneManager.LoadScene ("UIScoreOverview");
	}
}