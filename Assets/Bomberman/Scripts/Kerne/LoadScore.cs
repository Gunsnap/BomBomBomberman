using UnityEngine;
using System.Collections;

public class LoadData : MonoBehaviour {

	public int[] playerKills;
	public uint[] playerDistance;
	public uint[] roundNr;
	public uint[] winner;
	public uint[] powerUpNr;

	void Start () {
		playerKills = GlobalControl.instance.playerKills;
		playerDistance = GlobalControl.instance.playerDistance;
		roundNr = GlobalControl.instance.roundNr;
		winner = GlobalControl.instance.winner;
		powerUpNr = GlobalControl.instance.powerUpNr;

		Debug.Log ("Runde " + roundNr);
		Debug.Log ("Vinder: " + winner);
		for (int i = 0; i < playerKills.Length; i++) {
			Debug.Log ("Player " + i);
			Debug.Log ("playerKills " + playerKills [i]);
			Debug.Log ("playerDistance " + playerDistance [i]);
			Debug.Log ("powerUpNr " + powerUpNr [i]);
		}
	}
}