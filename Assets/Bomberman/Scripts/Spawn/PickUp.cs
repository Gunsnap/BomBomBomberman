using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {
	
	void OnTriggerEnter (Collider other) {
		bool skalDoe = true;
		switch (name) {
		case "Speed-UpPickup(Clone)":
			MovePlayer mover = other.GetComponent<MovePlayer> ();
			mover.roamingTime -= 0.1f;
			break;
		case "Fire-UpPickup(Clone)":
			other.GetComponent<Hero> ().bombRange++;
			break;
		default:
			skalDoe = false;
			break;
		}

		//Kill me!
		if (skalDoe)
			DestroyObject (gameObject);
	}

}