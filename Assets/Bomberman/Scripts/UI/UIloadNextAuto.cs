using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIloadNextAuto : MonoBehaviour {

	IEnumerator Start () {
		// Starter med at vente i X sekunder, hvorefter resten af koden udføres.
		yield return new WaitForSeconds (5f);

		var gc = GlobalControl.instance;

		// Hvis gc er oprettet spørger vi hvilken bane vi kom fra, hvorefter vi går tilbage til den.
		if (gc != null)
			SceneManager.LoadScene (gc.baneID);
		// Ellers ser vi på om vi kommer fra 'HotSeat' eller 'Multiplayer'
		else if (SceneManager.GetActiveScene ().name == "HotSeatStart")
			SceneManager.LoadScene ("Level1");
		else
			SceneManager.LoadScene ("Level2");
	}

}