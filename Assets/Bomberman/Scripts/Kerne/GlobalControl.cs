using UnityEngine;

public class GlobalControl : MonoBehaviour {
	public static GlobalControl instance;

	public uint roundNr;
	public uint roundTotal;

	public string baneID;

	public uint[] winner;

	public string[] playerName;
	public string[] playerNick;
	public int[] playerKills;
	public uint[] playerDistance;
	public uint[] powerUpCount;

	void Awake () {
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}
}