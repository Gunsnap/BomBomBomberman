using UnityEngine;

/// <summary>Adjust the spacing and rending range on the fly.</summary>
///
/// As the camera moves in XZ-direction the rendering range is adjusted, and in
/// Y-direction the spacing is adjusted. As the camera moves upwards the grid
/// area to render increases, so by increasing the spacing we keep the number
/// of lines to render constant. Rendering too many lines makes performance
/// suffer and looks ugly.
///
/// We will create a "fading" effect using two grids where one grid fades in
/// and the other one fades out.
///
/// It is assumed the grid has its origin at `(0, 0, 0)`. The grid is
/// encapsulated as a new grid structure local to the script. This level
/// of abstractions allows us to keep the script's `Update` method clean.
[RequireComponent(typeof(Camera))]
public class InfinityGrid3D : MonoBehaviour {

	/// <summary>Structure representing the compound level grid.</summary>
	///
	/// There are two modes: Dual and Flex. Flex mode is the default, one grid
	/// is stretched according to the camera's height to give the impression that
	/// the grid stays the same and the world scales instead.
	///
	/// Dual mode is similar to Unity's editor grid in that it uses two grids:
	/// the primary grid and a larger secondary grid. As the camera rises the
	/// primary grid fades out and the secondary one fades in. Once the primary
	/// grid vanishes the secondary becomes the primary and the secondary
	/// becomes ten times as large.
	///
	/// Dual mode works, but I couldn't get the rendering range to adjust to
	/// make it look good like in the editor. That's the only shortcoming, so
	/// feel free to experiment.
	private struct Grid {

		/// <summary>Mode of the new grid.</summary>
		public enum Mode {
			/// <summary>Two grids, like Unity Editor.</summary>
			Dual,
			/// <summary>One continuously expanding grid.</summary>
			Flex
		}

		/// <summary>Main grid for display.</summary>
		private readonly GFRectGrid  primary;

		/// <summary>Secondary, larger grid (only for dual mode).</summary>
		private readonly GFRectGrid secondary;

		/// <summary>Spacing of all axes of primary grid.</summary>
		private          float spacing;

		/// <summary>Current mode of the grid.</summary>
		private Mode mode;

		/// <summary>The spacing of the grid (read-only).</summary>
		public float Spacing {
			get {return primary.spacing.x;}
		}

		public Vector3 RenderFrom {
			set {
				primary.renderFrom   = value;
				secondary.renderFrom = value / spacing;
			}
		}

		public Vector3 RenderTo {
			set {
				primary.renderTo   = value;
				secondary.renderTo = value / spacing;
			}
		}

		/// <summary>Construct a new grid made of two rectangular grids.</summary>
		public Grid(GFRectGrid primary, GFRectGrid secondary, float height) {
			this.primary   =   primary;
			this.secondary = secondary;
			this.spacing   =     10.0f;
			this.mode      = Mode.Flex;

			this.Update(height);
		}

		public Vector3 WorldToGrid(Vector3 world) {
			return primary.WorldToGrid(world);
		}

		public void Update(float height) {
			UpdateSpacing(height);
			UpdateAlpha(height);
		}

		private void UpdateSpacing(float height) {
			if (mode == Mode.Dual) {
				float level = height >= 0 ? Mathf.FloorToInt(height / step) : Mathf.CeilToInt(height / step);

				primary.spacing   = Mathf.Pow(spacing, level) * Vector3.one;
				secondary.spacing = spacing * primary.spacing;
			} else {
				primary.spacing = Mathf.Max(0.5f, height) * Vector3.one;
			}
		}

		private void UpdateAlpha(float height) {
			Color x, z;
			float primaryA, secondaryA;
			if (mode == Mode.Dual) {
				var ratio = height / step - Mathf.Floor(height / step);  //between 0 and 1
				primaryA   = 1.0f - ratio;
				secondaryA = 0.0f + ratio;
			} else {
				primaryA   = 1.0f;
				secondaryA = 0.0f;
			}
		
			x = primary.axisColors.x;
			z = primary.axisColors.z;
			x.a = primaryA;
			z.a = primaryA;
			primary.axisColors.x = x;
			primary.axisColors.z = z;
		
			x = secondary.axisColors.x;
			z = secondary.axisColors.z;
			x.a = secondaryA;
			z.a = secondaryA;
			secondary.axisColors.x = x;
			secondary.axisColors.z = z;
		}
	}

	///<summary>The primary grid.</summary>
	public GFRectGrid _grid0;
	///<summary>The secondary grid (only for dual mode).</summary>
	public GFRectGrid _grid1;

	/// <summary>At what height steps to change grids. (Dual mode only)</summary>
	private const float step = 10.0f;

	/// <summary>Array storing the two grids to swap</summary>
	private Grid grid;

	///<summary>Aspect ratio between spacing and camera distance.</summary>
	private const float spacingRatio = 1.0f;

	///<summary>Size of the grid in both directions.</summary>
	private const float gridSize = 20.0f;

	private const float farPlaneScale = 15.0f;

	///<summary>The grid camera.</summary>
	private Camera cam;

	void Start(){
		cam = GetComponent<Camera>();
		grid = new Grid(_grid0, _grid1, transform.position.y);
	}

	void Update() {
		// Distance of the far plane scales linearly with the camera height
		grid.Update(Mathf.Abs(transform.position.y));
		cam.farClipPlane = farPlaneScale * Mathf.Max(0.5f, Mathf.Abs(cam.transform.position.y));

		// Adjust the rendering range of the grid to be around the camera.
		Vector3 pos = grid.WorldToGrid(cam.transform.position);
		pos.y = 0;
		grid.RenderFrom = pos - gridSize * new Vector3(1, 0, 1);
		grid.RenderTo   = pos + gridSize * new Vector3(1, 0, 1);
	}
}
