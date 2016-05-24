namespace GridFramework {
	/// <summary>
	///   Radians or degrees.
	/// </summary>
	///
	/// <remarks>
	///   <para>
	///     This is a simple enum for specifying whether an angle is given in
	///     radians for degrees. This enum is so far only used in methods of
	///     <see cref="GFPolarGrid"><c>GFPolarGrid</c></see>, but I decided to
	///     make it global in case other grids in the future will use it was
	///     well.
	///   </para>
	/// </remarks>
	public enum AngleMode {radians = 0, degrees};
	
	/// <summary>
	///   Enum for one of the three grid planes.
	/// </summary>
	///
	/// <remarks>
	///   <para>
	///     This enumeration encapsulates the three grid planes: <c>XY</c>,
	///     <c>XZ</c> and <c>YZ</c>. You can also get the integer of
	///     enumeration items, where the integer corresponds to the missing
	///     axis (<c>X = 0</c>, <c>Y = 1</c>, <c>Z = 2</c>):
	///   </para>
	///   <code>
	///     // UnityScript:
	///     var myPlane: GridPlane = GridPlane.XZ;
	///     var planeIndex: int = (int)myPlane; // sets the variable to 1
	///     
	///     // C#
	///     GridPlane myPlane = GridPlane.XZ;
	///     int planeIndex = (int)myPlane;
	///   </code>
	/// </remarks>
	public enum GridPlane {YZ, XZ, XY};
}
