using UnityEngine;
using System.Collections;

public class Screenshotscript : MonoBehaviour {

	void Update () {
		if(Input.GetKeyDown(KeyCode.P)){
			Application.CaptureScreenshot(PlayerPrefs.GetInt("SS")+ ".png");
			PlayerPrefs.SetInt("SS",PlayerPrefs.GetInt("SS")+1);
		}
	}
}
