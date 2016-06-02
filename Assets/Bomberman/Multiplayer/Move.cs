using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Move : MonoBehaviour {

	float speed = 10.0f;

	void Update () {
		float translation = CrossPlatformInputManager.GetAxis ("Vertical") * speed;
		float translation2 = CrossPlatformInputManager.GetAxis ("Horizontal") * speed;

		translation *= Time.deltaTime;
		translation2 *= Time.deltaTime;
		transform.Translate (0, translation, 0);
		transform.Translate (translation2, 0, 0);
	}
}
