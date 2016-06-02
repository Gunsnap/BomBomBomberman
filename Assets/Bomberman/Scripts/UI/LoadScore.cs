using UnityEngine;
using System.Collections;

public class LoadScore : MonoBehaviour {

	public uint roundNr;
	public uint roundTotal;

	public uint[] winner;

	public string[] playerName;
	public string[] playerNick;
	public int[] playerKills;
	public uint[] playerDistance;
	public uint[] powerUpCount;

	void Start () {
		roundNr = GlobalControl.instance.roundNr + 1;
		roundTotal = GlobalControl.instance.roundTotal;

		winner = GlobalControl.instance.winner;

		playerName = GlobalControl.instance.playerName;
		playerNick = GlobalControl.instance.playerNick;
		playerKills = GlobalControl.instance.playerKills;
		playerDistance = GlobalControl.instance.playerDistance;
		powerUpCount = GlobalControl.instance.powerUpCount;

		Debug.Log ("Runde " + roundNr + " af " + roundTotal);
		//Debug.Log ("Vinder denne runde: " + winner [roundNr - 1]);

		for (int i = 0; i < playerKills.Length; i++) {
			Debug.Log ("Player " + i);
			Debug.Log ("playerKills " + playerKills [i]);
			Debug.Log ("playerDistance " + playerDistance [i]);
			Debug.Log ("powerUpNr " + powerUpCount [i]);
		}


	}
}