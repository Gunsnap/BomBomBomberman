#pragma strict

// Length of a "year" in Earth days.
var _orbitalPeriod : float = 365.0f;

// Scale the orbital period by this.
private var velocityScale : float = 100.0f;

private var t: Transform ;

function Start() {
	t = GetComponent.<Transform>();
}


function Update () {
	if (!t.parent) {return;}

	var a = (Time.time % 365.0f) * velocityScale / _orbitalPeriod;
	var d = Vector3.Magnitude(t.position - t.parent.position);

	// Please forgive me for using a circular orbit instead of an elliptical one.
	t.position = t.parent.position + new Vector3(Mathf.Sin(a), 0.0f, Mathf.Cos(a)) * d;
}

