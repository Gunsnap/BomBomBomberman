using UnityEngine;
using System.Collections;

public class hero : MonoBehaviour {

	//Tid
	static float tidGemt;

	// Spawner
	private Spawner sp;

	// Animator
	private Animator animator;

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

	// Bomb
	/// <summary>A bomb may be placed.</summary>
	private bool AllowBomb;
	//public float BombRate = 0.5f;
	//private float nextBomb = 0.0f;
	//private int range = 1;

	void Start () {
		Debug.Log ("HeroScript startet");
		tidGemt = Time.time + 1f;
		sp = gameObject.GetComponent<Spawner> ();
		animator = gameObject.GetComponent<Animator> ();

		grid = ForbiddenTiles.movementGrid;

		//make a check to prevent getting stuck in a null exception
		if (grid) {
			//snap to the grid  no matter where we are
			grid.AlignTransform (transform);
		}
	}

	void Update () {
		// Tid spillet har kørt.
		float tid = Time.time;

		// Bombe
		if (tidGemt < tid) {
			AllowBomb = true;
			tidGemt = tid;
		} 

		if (AllowBomb) {
			if (Input.GetKeyDown ("space")) {
				//nextBomb = tid + BombRate;
				animator.SetTrigger ("PutBomb");
				sp.SpawnElement (transform.position, new Vector3 (270, 0, 0));
				AllowBomb = false;
			} else {
				Debug.Log ("Der må smides bombe, men der er ikke trykket");
			}
		} else {
			Debug.Log ("Du må ikke smide en bombe");
		}


		if (!grid)
			return;

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
			if (ForbiddenTiles.CheckSquare (goal)) {
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
	// Lukker update

	/** Finder ud af om man må bevæge sig i den givne retning */
	Vector3 FindNextFace () {
		//we will be operating in grid space, so convert the position
		Vector3 newPosition = grid.WorldToGrid (transform.localPosition);

		//Add one grid unit onto position in the picked direction
		if (Input.GetKey (KeyCode.W))
			newPosition = newPosition + new Vector3 (0, 1, 0);
		else if (Input.GetKey (KeyCode.S))
			newPosition = newPosition + new Vector3 (0, -1, 0);
		else if (Input.GetKey (KeyCode.D))
			newPosition = newPosition + new Vector3 (1, 0, 0);
		else if (Input.GetKey (KeyCode.A))
			newPosition = newPosition + new Vector3 (-1, 0, 0);
		else {
			animator.SetBool ("Run", false);
			doMove = false;
			return grid.GridToWorld (newPosition);
		}
		animator.SetBool ("Run", true);

		//if we would wander off beyond the size of the grid turn the other way around
		for (int j = 0; j < 2; j++) {
			if (Mathf.Abs (newPosition [j]) > grid.size [j])
				newPosition [j] -= Mathf.Sign (newPosition [j]) * 2.0f;
		}

		//return the position in world space
		return grid.GridToWorld (newPosition);
	}
	// Lukker FindNextFace

}
// Lukker class