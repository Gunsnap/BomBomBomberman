using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

	public void LoadScene (int level) {
		//Application.LoadLevel (level); // Obsolete - DVS
		SceneManager.LoadScene (level);
	}
}