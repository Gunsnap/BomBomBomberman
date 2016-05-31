using UnityEngine;
using System.Collections;

public class LoadData : MonoBehaviour {

	public int playerKills;

	void Start () {
		playerKills = GlobalControl.instance.playerKills;
		Debug.Log (playerKills);
	}
}