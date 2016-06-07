using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {
	public bool running;
	public int livingPlayers;

	float tidStart;

	void Start () {
		running = true;
		tidStart = Time.time + 3f;
	}

	void Update () {
		if (Time.time > tidStart) {
			if (running && livingPlayers < 2)
				running = false;

			if (!running)
				LoadNewScene ();
		}
	}

	public void LoadNewScene () {
		// Gemmer hvor vi kommer fra, så vi kan komme tilbage.
		GlobalControl.instance.baneID = SceneManager.GetActiveScene ().name;

		// Loader scenen med overblik over hvem der har vundet og sådan noget.
		SceneManager.LoadScene ("UIScoreOverview");
	}
}