using UnityEngine;
using System.Collections;

public class GlobalControl : MonoBehaviour {
	public static GlobalControl instance;

	public int playerKills;

	void Awake () {
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}
}