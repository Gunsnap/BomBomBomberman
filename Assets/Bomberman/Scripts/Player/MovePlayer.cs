using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	// Movement
	/// <summary> The grid of the world!.</summary>
	private GFRectGrid grid;
	/// <summary>How long it takes to move from one tile to another.</summary>
	public float roamingTime = 1.0f;
	/// <summary>Whether the object is to move or not.</summary>
	private bool doMove = false;
	/// <summary>Where the object will move to.</summary>
	private Vector3 goal;
	/// <summary>How fast to move.</summary>
	private float roamingSpeed;

	// Animator
	private Animator animator;
	public Vector3 rotation;

	// Use this for initialization
	void Start () {
		grid = ForbiddenTilesVores.movementGrid;

		//make a check to prevent getting stuck in a null exception
		if (grid) {
			//snap to the grid  no matter where we are
			grid.AlignTransform (transform);
		}

		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!grid)
			return;

		transform.rotation = Quaternion.Euler (rotation);

		if (doMove) {
			//move towards the desination
			Vector3 newPosition = transform.position;
			newPosition.x = Mathf.MoveTowards (transform.position.x, goal.x, roamingSpeed * Time.deltaTime);
			newPosition.y = Mathf.MoveTowards (transform.position.y, goal.y, roamingSpeed * Time.deltaTime);
			transform.position = newPosition;
			//check if we reached the destination (use a certain tolerance so we don't miss the point becase of rounding errors)
			if (Mathf.Abs (transform.position.x - goal.x) < 0.01f && Mathf.Abs (transform.position.y - goal.y) < 0.01f) {
				doMove = false;
				//FIXME animator.SetBool ("Run", false);
			}
			//if we did stop moving
		} else {
			//make sure the time is always positive
			if (roamingTime < 0.01f)
				roamingTime = 0.01f;
			//find the next destination
			goal = FindNextFace ();
			//--- let's check if the goal is allowed, if not we will pick another direction during the next frame ---
			if (ForbiddenTilesVores.CheckSquare (goal)) {
				//calculate speed by dividing distance (one of the two distances will be 0, we need the other one) through time
				roamingSpeed = Mathf.Max (Mathf.Abs (transform.position.x - goal.x), Mathf.Abs (transform.position.y - goal.y)) / roamingTime;
				//resume movement with the new goal
				doMove = true;
			} else {
				//FIXME animator.SetBool ("Run", false);
				Debug.Log ("hit the obstacle");
			}
		}
	}
	//Lukker Update

	/** Finder ud af om man må bevæge sig i den givne retning */
	Vector3 FindNextFace () {
		//we will be operating in grid space, so convert the position
		Vector3 newPosition = grid.WorldToGrid (transform.localPosition);

		//Retninger
		Vector3 opVec = new Vector3 (270, 0, 0);
		Vector3 nedVec = new Vector3 (90, 180, 0);
		Vector3 hoejreVec = new Vector3 (0, 90, 270);
		Vector3 venstreVec = new Vector3 (0, 270, 90);

		//Add one grid unit onto position in the picked direction
		if (Input.GetKey (KeyCode.W)) {
			newPosition = newPosition + new Vector3 (0, 1, 0);
			rotation = opVec;
		} else if (Input.GetKey (KeyCode.S)) {
			newPosition = newPosition + new Vector3 (0, -1, 0);
			rotation = nedVec;
		} else if (Input.GetKey (KeyCode.D)) {
			newPosition = newPosition + new Vector3 (1, 0, 0);
			rotation = hoejreVec;
		} else if (Input.GetKey (KeyCode.A)) {
			newPosition = newPosition + new Vector3 (-1, 0, 0);
			rotation = venstreVec;
		} else {
			animator.SetBool ("Run", false);
			doMove = false;
			return grid.GridToWorld (newPosition);
		}
		animator.SetBool ("Run", true);

		/* Understående skaber problemer da det på en eller anden måde tror at grid.size altid er 5x5
		//if we would wander off beyond the size of the grid turn the other way around
		for (int j = 0; j < 2; j++) {
			Debug.Log ("newPosition [j] " + Mathf.Abs (newPosition [j]) + " > grid.size [j] " + grid.size [j]);
			if (Mathf.Abs (newPosition [j]) > grid.size [j])
				newPosition [j] -= Mathf.Sign (newPosition [j]) * 1.0f;
		}*/
		//return the position in world space
		return grid.GridToWorld (newPosition);
	}
	// Lukker FindNextFace
}