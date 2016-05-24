#pragma strict

// Make a bouncing grid and render it using Vectrosity. The bouncing is handled
// by the physics engine

public var width: float = 10.0;
public var material: Material;
public var texture: Texture;
public var colors: Color = new Color(1.0, 0.0, 0.0, 1.0);

private var grid: GFGrid;

#if GRID_FRAMEWORK_VECTROSITY
private var gridLine: Vectrosity.VectorLine;

function Start () {
	// In order for the rendering to align properly with the grid the grid has
	// to be at the world's origin
	var tempPos: Vector3 = transform.position;
	transform.position = Vector3.zero;
	grid = GetComponent.<GFGrid>();
	
	Mathf.Max(width, 1.0);
	
	#if GRID_FRAMEWORK_VECTROSITY_4
	gridLine = new Vectrosity.VectorLine("Bouncy Lines", grid.GetVectrosityPoints(), material, width);
	#else
	gridLine = new Vectrosity.VectorLine("Bouncy Lines", grid.GetVectrosityPoints(), texture, width);
	gridLine.material = material;
	#endif
	gridLine.SetColor(colors);
	gridLine.drawTransform = transform;
	gridLine.Draw3DAuto();
	
	transform.position = tempPos;
	transform.rotation = Random.rotation; //apply an initial random rotation
}

#endif

