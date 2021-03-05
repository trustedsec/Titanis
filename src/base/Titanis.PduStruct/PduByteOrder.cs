using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Specifies the byte order.
	/// </summary>
	public enum PduByteOrder
	{
		Inherit = 0,
		Native,
		Other,
		LittleEndian,
		BigEndian
	}
}
