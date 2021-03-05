namespace Titanis.DceRpc
{
	/// <summary>
	/// Marks a struct as fixed (i.e., not conformant).
	/// </summary>
	/// <remarks>
	/// This is used as a type constraint for generated structures.
	/// </remarks>
	public interface IRpcFixedStruct : IRpcStruct
	{

	}
}
