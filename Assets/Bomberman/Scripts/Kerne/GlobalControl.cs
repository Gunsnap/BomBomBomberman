using UnityEngine;
using System.Collections;

public class GlobalControl : MonoBehaviour {
	public static GlobalControl instance;

	public int[] playerKills;
	public uint[] playerDistance;
	public uint[] roundNr;
	public uint[] winner;
	public uint[] powerUpNr;

	void Awake () {
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}
}