using UnityEngine;

public class ButtonScript : MonoBehaviour {

	public void LoadScene (int level) {
		UnityEngine.SceneManagement.SceneManager.LoadScene (level);
	}
}