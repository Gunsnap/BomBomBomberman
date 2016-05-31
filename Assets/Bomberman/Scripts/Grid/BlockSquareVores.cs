using UnityEngine;

public class BlockSquareVores : MonoBehaviour {

	// Start() is called after Awake(), this ensures that the matrix has alrady been built
	void Start () {
		//Set the entry that corresonds to the obstacle's position as false
		ForbiddenTilesVores.RegisterSquare (transform.position, false);
	}

	void OnCollisionEnter (Collision coli) {
		//FIXME bliver ikke brugt
		Debug.Log (coli.collider.name + " har Collision med " + name);
	}
}