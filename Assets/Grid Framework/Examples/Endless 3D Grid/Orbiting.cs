using UnityEngine;

///<summary>A simple script that makes an object orbit around its parent.</summary>
public class Orbiting : MonoBehaviour {

	///<summary>Length of a "year" in Earth days.</summary>
	public float _orbitalPeriod = 365.0f;
	
	///<summary>Scale the orbital period by this.</summary>
	private const float velocityScale = 100.0f;

	private Transform t;

	void Start() {
		t = GetComponent<Transform>();
	}


	void Update () {
		if (!t.parent) {return;}

		var a = (Time.time % 365.0f) * velocityScale / _orbitalPeriod;
		var d = Vector3.Magnitude(t.position - t.parent.position);

		// Please forgive me for using a circular orbit instead of an elliptical one.
		t.position = t.parent.position + new Vector3(Mathf.Sin(a), 0.0f, Mathf.Cos(a)) * d;
	}
}
