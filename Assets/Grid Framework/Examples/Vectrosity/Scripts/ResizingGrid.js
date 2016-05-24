#pragma strict
// Make a resizing grid and render it using Vectrosity. In reality you
// shouldn't call Resize every frame for performance reasons.

public var width: float = 7.0;
public var material: Material;
public var texture: Texture;

//private var grid: GFRectGrid;
private var grid: GFHexGrid;
private var tempPos: Vector3;

//some toggling variables for resizing
private var growingRadius = true;
private var growingSizeX = true;
private var growingSizeY = true;

#if GRID_FRAMEWORK_VECTROSITY

private var gridLine: Vectrosity.VectorLine;

function Start () {
	// in order for the rendering to align properly with the grid the grid has to be at the world's origin
	tempPos = transform.position;
	transform.position = Vector3.zero;
	grid = GetComponent.<GFHexGrid>();
	
	if(width < 1.0) width = 1.0;
	
	#if GRID_FRAMEWORK_VECTROSITY_4
	gridLine = new Vectrosity.VectorLine("Resizing Lines", grid.GetVectrosityPoints(), material, width);
	#else
	gridLine = new Vectrosity.VectorLine("Resizing Lines", grid.GetVectrosityPoints(), texture, width);
	gridLine.material = material;
	#endif
	gridLine.color = Color.yellow;
	gridLine.drawTransform = transform;
	gridLine.Draw3DAuto();
	
	transform.position = tempPos;
}

function Update () {
	resizeGrid();
	
	// in order for the rendering to align properly with the grid the grid has to be at the world's origin
	tempPos = transform.position;
	transform.position = Vector3.zero;
	// Vectrosity 3-
	//gridLine.Resize(grid.GetVectrosityPoints()); //calculate the new grid points
	// Vectrosity 4+
	gridLine.points3.Clear();
	gridLine.points3.AddRange(grid.GetVectrosityPoints());
	transform.position = tempPos;
}

function resizeGrid(){
	if(growingRadius) {
		grid.radius = Mathf.MoveTowards(grid.radius, 3.0, Random.Range(0.25, 0.5)*Time.deltaTime);
		if(Mathf.Abs(grid.radius - 3.0) < 0.01)
			growingRadius = false;
	} else {
		grid.radius = Mathf.MoveTowards(grid.radius, 2.0, Random.Range(0.25, 0.5)*Time.deltaTime);
		if(Mathf.Abs(grid.radius - 2.0) < 0.01)
			growingRadius = true;
	}
	
	if(growingSizeX){
		grid.size.x = Mathf.MoveTowards(grid.size.x, 8.0, Random.Range(2.0, 3.0)*Time.deltaTime);
		if(Mathf.Abs(grid.size.x - 8.0) < 0.03)
			growingSizeX = false;
	} else{
		grid.size.x = Mathf.MoveTowards(grid.size.x, 3.0, Random.Range(1.0, 3.0)*Time.deltaTime);
		if(Mathf.Abs(grid.size.x - 3.0) < 0.01)
			growingSizeX = true;
	}
	
	if(growingSizeY){
		grid.size.y = Mathf.MoveTowards(grid.size.y, 4.0, Random.Range(1.0, 2.0)*Time.deltaTime);
		if(Mathf.Abs(grid.size.y - 4.0) < 0.03)
			growingSizeY = false;
	} else{
		grid.size.y = Mathf.MoveTowards(grid.size.y, 2.0, Random.Range(1.0, 2.0)*Time.deltaTime);
		if(Mathf.Abs(grid.size.y - 2.0) < 0.01)
			growingSizeY = true;
	}
}

#endif

