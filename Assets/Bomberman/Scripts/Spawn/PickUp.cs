using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {
	
	void OnTriggerEnter (Collider other) {
		if (!other.name.Equals ("Floor") && !other.name.Contains ("Beam")) {
			bool skalDoe = true;
			MovePlayer mover;

			switch (name) {
			case "Speed-UpPickup(Clone)":
				mover = other.GetComponent<MovePlayer> ();
				mover.roamingTime -= 0.1f;
				break;
			case "Fire-UpPickup(Clone)":
				other.GetComponent<Hero> ().bombRange++;
				break;
			case "Fire-DownPickup":
				other.GetComponent<Hero> ().bombRange--;
				break;
			case "Speed-DownPickup":
				mover = other.GetComponent<MovePlayer> ();
				mover.roamingTime += 0.1f;
				break;
			case "ReversePickup":
				break;
			case "PoopPickup":
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

}