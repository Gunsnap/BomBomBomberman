using UnityEngine;
using System.Collections;

public class Exploded : MonoBehaviour {

	float putTime;
	float splosionDelay;

	void Start () {
		putTime = Time.time;
		splosionDelay = .8f;
	}

	void Update () {
		float timeNow = Time.time;
		if (timeNow > putTime + splosionDelay)
			DestroyObject (this.gameObject);
	}
}