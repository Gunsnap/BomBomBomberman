#pragma strict

private var linearVelocity   : float = 0.25f;
private var angularVelocity  : float = 1.00f;
private var originalPosition : Vector3   ;
private var originalRotation : Quaternion;

function Start() {
	originalPosition = transform.position;
	originalRotation = transform.rotation;
}

function Update() {
	var linear  : Vector3 = Vector3.zero;
	var angular : Vector3 = Vector3.zero;

	// Rotation in Euler angles
	var euler: Vector3 = transform.rotation.eulerAngles;

	// Input is hard-coded to keys, not a good idea, but makes the script
	// portable across different projects.

	linear.x += Input.GetKey("d") ?  1.0f : 0.0f;
	linear.x += Input.GetKey("a") ? -1.0f : 0.0f;
	linear.y += Input.GetKey("space") ?  1.0f : 0.0f;
	linear.y += Input.GetKey("x") ? -1.0f : 0.0f;
	linear.z += Input.GetKey("w") ?  1.0f : 0.0f;
	linear.z += Input.GetKey("s") ? -1.0f : 0.0f;

	angular.x += Input.GetKey("r") ?  1.0f : 0.0f;
	angular.x += Input.GetKey("f") ? -1.0f : 0.0f;
	angular.y += Input.GetKey("c") ?  1.0f : 0.0f;
	angular.y += Input.GetKey("z") ? -1.0f : 0.0f;
	angular.z += Input.GetKey("q") ?  1.0f : 0.0f;
	angular.z += Input.GetKey("e") ? -1.0f : 0.0f;

	transform.position += linear.x * linearVelocity * transform.right;
	transform.position += linear.y * linearVelocity * transform.up;
	transform.position += linear.z * linearVelocity * transform.forward;
	euler += angular * angularVelocity;
	transform.rotation = Quaternion.Euler(euler);

	// Reset key
	if (Input.GetKey("backspace")) {
		transform.position = originalPosition;
		transform.rotation = originalRotation;
	}
}

function OnGUI() {
	var message : String = "Fly around using the following controls:\n"
		+ "WASD - sideways and forward\n"
		+ "Space & X - Up & Down\n"
		+ "\n"
		+ "R & F - Pitch\n"
		+ "Q & E - Roll\n"
		+ "Z & C - Yaw\n"
		+ "\n"
		+ "Backspace - Reset";
	GUI.TextArea (new Rect (10, 10, 400, 150), message);
}

