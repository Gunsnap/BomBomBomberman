#pragma strict

public var width: float = 10.0;
public var material: Material;
public var texture: Texture;

private var grid: GFGrid;

//the array of possible colours
private var colors: Color[];
//the array of assigned colours
private var lineColors: List.<Color32>;

private var iterator: int = 0; //for looping though the arrays
private var doChangeColor: boolean = true;

#if GRID_FRAMEWORK_VECTROSITY

private var gridLine: Vectrosity.VectorLine;
function Start () {
	var tempPos: Vector3 = transform.position;
	// in order for the rendering to align properly with the grid the grid has
	// to be at the world's origin
	transform.position = Vector3.zero;
	
	grid = GetComponent.<GFGrid>();
	Mathf.Max(width, 1.0);

	var points = grid.GetVectrosityPoints();
	
	//list possible colous and then assign them to the line segments
	colors = new Color[7];
	colors[0]=Color.white;
	colors[1]=Color.red;
	colors[2]=Color.green;
	colors[3]=Color.blue;
	colors[4]=Color.yellow;
	colors[5]=Color.cyan;
	colors[6]=Color.magenta;

	lineColors = new List.<Color32>();
	for(var i: int = 0; i < points.Count / 2; i++){
		//i % colors.Length returns always a number between 0 and the amout of
		//colous we have listed. it increments every time and when the maximum
		//has been reached it reverts back to zero
		lineColors.Add(colors[i % colors.Length]);
	}
	
	#if GRID_FRAMEWORK_VECTROSITY_4
	gridLine = new Vectrosity.VectorLine("Rotating Lines", points, material, width);
	#else
	gridLine = new Vectrosity.VectorLine("Rotating Lines", points, texture, width);
	gridLine.material = material;
	#endif

	gridLine.SetColors(lineColors);
	gridLine.drawTransform = transform;
	gridLine.Draw3DAuto();
	transform.position = tempPos;
}

function delayChanging(){
	// wait a while before allowing to change colours again
	yield WaitForSeconds(Random.Range(0.2, 1.0));
	doChangeColor = true;
}

function Update () {
	delayChanging();
	if(doChangeColor){
		//pick a random colour for the current line and apply it
		lineColors[iterator] = colors[Random.Range(0, 7)];
		gridLine.SetColors(lineColors);
	
		iterator++; //next line
		// 0 -> 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 0 -> 1 -> 2 -> ...
		iterator = iterator % lineColors.Count;
		doChangeColor = false;
	}
	//rotate the grid
	transform.Rotate(-15*Vector3.right * Time.deltaTime);
	transform.Rotate(10*Vector3.up * Time.deltaTime, Space.World);
}

#endif

