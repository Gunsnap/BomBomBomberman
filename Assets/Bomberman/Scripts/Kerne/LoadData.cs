using UnityEngine;
using System.Collections;

public class LoadData : MonoBehaviour {

	public int[] playerKills;
	public uint[] playerDistance;
	public int[] roundNr;
	public uint[] winner;
	public uint[] powerUpNr;

	void Start () {
		playerKills = GlobalControl.instance.playerKills;
		playerDistance = GlobalControl.instance.playerDistance;
		roundNr = GlobalControl.instance.roundNr;
		winner = GlobalControl.instance.winner;
		powerUpNr = GlobalControl.instance.powerUpNr;

		foreach (var item in playerKills) {
			Debug.Log (item);
		}
	}
}