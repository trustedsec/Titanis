using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Specifies the alignment of the type within NDR or NDR64.
	/// </summary>
	/// <remarks>
	/// The values of members in this enum do not necessarily correspond to byte alignment values
	/// since the actual alignment depends on the encoding (whether NDR or NDR64).
	/// </remarks>
	/// <seealso cref="NdrAlignmentExtensions.CombineWith(NdrAlignment, NdrAlignment)"/>
	public enum NdrAlignment
	{
		None = 0,
		_1Byte = 1,
		_2Byte = 2,
		_4Byte = 4,
		NativePtr = 5,
		_8Byte = 8,
	}

	public static class NdrAlignmentExtensions
	{
		/// <summary>
		/// Combines two <see cref="NdrAlignment"/> values returning the stricter alignment.
		/// </summary>
		/// <param name="alignment">First alignment</param>
		/// <param name="other">Other alignment</param>
		/// <returns>A <see cref="NdrAlignment"/> for the stricter alignment</returns>
		public static NdrAlignment CombineWith(this NdrAlignment alignment, NdrAlignment other)
			=> (NdrAlignment)Math.Max((int)alignment, (int)other);
	}
}
