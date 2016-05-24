#pragma strict

// Make a grid rotate and render it using Vectrosity

public var width: float = 7.0;
public var material: Material;
public var texture: Texture;

private var grid: GFGrid;

#if GRID_FRAMEWORK_VECTROSITY

private var gridLine: Vectrosity.VectorLine;

function Start () {
	// In order for the rendering to align properly with the grid the grid has
	// to be at the world's origin
	var tempPos: Vector3 = transform.position;
	transform.position = Vector3.zero;
	grid = GetComponent.<GFGrid>();

	if(width < 1.0) width = 1.0;
	
	#if GRID_FRAMEWORK_VECTROSITY_4
	gridLine = new Vectrosity.VectorLine("Rotating Lines", grid.GetVectrosityPoints(), material, width);
	#else
	gridLine = new Vectrosity.VectorLine("Rotating Lines", grid.GetVectrosityPoints(), texture, width);
	gridLine.material = material;
	#endif
	gridLine.color = Color.green;
	gridLine.drawTransform = transform;
	gridLine.Draw3DAuto();
	
	transform.position = tempPos;
}

function Update () {
	// Rotate the grid
	transform.Rotate(10*Vector3.right * Time.deltaTime);
	transform.Rotate(15*Vector3.up * Time.deltaTime, Space.World);
}

#endif

