using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Asn1
{
	/// <summary>
	/// Specifies the number of bits used in a bit string <see langword="enum"/>.
	/// </summary>
	/// <remarks>
	/// This attribute is used by <see cref="Asn1BitString{TEnum}"/>.  The
	/// bits start at the MSB and progress toward the LSB.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Enum)]
	public sealed class BitCountAttribute : Attribute
	{
		public BitCountAttribute(int bitCount)
		{
			BitCount = bitCount;
		}

		/// <summary>
		/// Gets the number of bits.
		/// </summary>
		public int BitCount { get; }
	}
}
