using UnityEngine;

[RequireComponent (typeof(GFRectGrid))]
public class SetupForbiddenTilesVores : MonoBehaviour {

	// Awake is called before Start()
	void Awake () {
		//We will build the matrix based on the grid that is attached to this object.
		//All entries are true by default, then each obstacle will mark its entry as false
		ForbiddenTilesVores.Initialize (GetComponent<GFRectGrid> ());
	}

	void OnGUI () {
		GUI.TextArea (new Rect (10, 10, 140, 230), ForbiddenTilesVores.MatrixToString ());
	}
}