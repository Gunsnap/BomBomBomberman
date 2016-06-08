using UnityEngine;

public class PickUp : MonoBehaviour {
	
	void OnTriggerEnter (Collider other) {
		if (!other.name.Equals ("Floor") && !other.name.Contains ("Beam")) {
			bool skalDoe = true;
			MovePlayer mover;

			switch (name) {
			case "Speed-UpPickup(Clone)":
				// Gå hurtigere
				mover = other.GetComponent<MovePlayer> ();
				mover.roamingTime -= 0.1f;
				break;
			case "Fire-UpPickup(Clone)":
				// Flammer større
				other.GetComponent<Hero> ().bombRange++;
				break;
			case "Fire-DownPickup":
				// Flammer mindre
				other.GetComponent<Hero> ().bombRange--;
				break;
			case "Speed-DownPickup":
				// Gå langsommere
				mover = other.GetComponent<MovePlayer> ();
				mover.roamingTime += 0.1f;
				break;
			case "ReversePickup":
				//TODO gå modsat
				break;
			case "PoopPickup":
				//TODO smid en bombe hver gang du kan, dog kun i en periode
				//FIXME smid ikke alle i samme felt
				//float tmpFlo = placer.GetComponent<Hero> ().fuseTime;
				//placer.GetComponent<Hero> ().fuseTime = tmpFlo / 2;
				break;
			default:
				skalDoe = false;
				break;
			}

			//Kill me!
			if (skalDoe) {
				other.GetComponent<Hero> ().myPowerUpCount++;
				DestroyObject (gameObject);
			}
		}
	}

}