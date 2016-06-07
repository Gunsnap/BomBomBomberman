using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
		var gc = GlobalControl.instance;
		roundNr = gc.roundNr + 1;
		roundTotal = gc.roundTotal;

		winner = gc.winner;

		playerName = gc.playerName;
		playerNick = gc.playerNick;
		playerKills = gc.playerKills;
		playerDistance = gc.playerDistance;
		powerUpCount = gc.powerUpCount;

		int j = playerNick.Length - 1;
		Text[] egenskaber = GetComponentInChildren<Image> ().GetComponentsInChildren<Text> ();
		for (int i = 0; i < egenskaber.Length; i++) {
			Text navn = egenskaber [i];
			if (!navn.name.Contains ("lbl") && navn.name.Equals ("Name")) {
				Text kills = egenskaber [i + 1];
				Text points = egenskaber [i + 2];
				Text powers = egenskaber [i + 3];
				if (j >= 0) {
					navn.text = playerNick [j];
					kills.text = playerKills [j].ToString ();
					points.text = playerDistance [j].ToString ();
					powers.text = powerUpCount [j].ToString ();
					j--;
				}
			}
		}

//		Debug.Log ("Runde " + roundNr + " af " + roundTotal);
//		//Debug.Log ("Vinder denne runde: " + winner [roundNr - 1]);
//
//		for (int i = 0; i < playerKills.Length; i++) {
//			Debug.Log ("Player " + i);
//			Debug.Log ("playerKills " + playerKills [i]);
//			Debug.Log ("playerDistance " + playerDistance [i]);
//			Debug.Log ("powerUpNr " + powerUpCount [i]);
//		}


	}
}