using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

	public bool running;
	public int livingPlayers;

	// Use this for initialization
	void Start () {
		running = true;
		livingPlayers = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (running && livingPlayers < 2) {
			running = false;
		}

		if (!running) {
			LoadNewScene ();
		}
	}

	public void LoadNewScene () {
		SceneManager.LoadScene ("UIScoreOverview");
	}
}