using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {
	public bool Rotate;
	public float rotspeed = 1f;
	public SkinnedMeshRenderer SkinnedMesh;
	bool Eyes;
	void Start () {
		//SkinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();
	}

	public void OpenCloseEyes(){
		Eyes = !Eyes;
	}

	public void ToggleRot(){
		Rotate = !Rotate;
	}

	void LateUpdate(){
		if(SkinnedMesh!=null){
			SkinnedMesh.SetBlendShapeWeight(2,Eyes?100f:0f);
		}
	}
	
	void Update () {

		if(Rotate){
			transform.Rotate(0,rotspeed,0);
		}
	}
}
