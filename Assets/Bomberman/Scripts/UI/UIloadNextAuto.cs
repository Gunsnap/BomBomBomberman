using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIloadNextAuto : MonoBehaviour {

	IEnumerator Start () {
		yield return new WaitForSeconds (5f);
		SceneManager.LoadScene ("Level2");
	}

}