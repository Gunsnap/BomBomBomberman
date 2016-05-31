using UnityEngine;

public class BlockSquareVores : MonoBehaviour {

	// Start() is called after Awake(), this ensures that the matrix has alrady been built
	void Start () {
		/* Set the entry that corresonds to the obstacle's position as
		* '1' brick
		* '2' reinforcedBrick
		* 'X' steel
		*/

		if (name.Contains ("Reinforced"))
			ForbiddenTilesVores.RegisterSquare (transform.position, '2');
		else if (name.Contains ("Brick"))
			ForbiddenTilesVores.RegisterSquare (transform.position, '1');
		else if (name.Contains ("Steel"))
			ForbiddenTilesVores.RegisterSquare (transform.position, 'x');
	}
}