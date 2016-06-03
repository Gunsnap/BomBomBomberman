using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIloadNextAuto : MonoBehaviour {

	IEnumerator Start () {
		yield return new WaitForSeconds (5f);

		var gc = GlobalControl.instance;

		if (gc != null)
			SceneManager.LoadScene (gc.baneID);
		else if (SceneManager.GetActiveScene ().name == "HotSeatStart")
			SceneManager.LoadScene ("Level1");
		else
			SceneManager.LoadScene ("Level2");
	}

}