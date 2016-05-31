using UnityEngine;
using System.Collections;

public class UIloadNextAuto : MonoBehaviour {

	IEnumerator Start() {
		yield return new WaitForSeconds (5f);
		Application.LoadLevel ("Main");
	}

}
