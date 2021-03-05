using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis
{
	/// <summary>
	/// An unsigned 16-bit integer stored in little-endian byte order.
	/// </summary>
	/// <remarks>
	/// Use <see cref="Value"/> to get or set the value.  If the CPU architecture
	/// represents integers in a form other than little-endian byte order, the integer is flipped.
	/// </remarks>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct UInt16LE
	{
		private ushort value;

		private UInt16LE(ushort value)
		{
			this.value = value;
		}

		/// <summary>
		/// Gets or sets the raw value of the integer, in little-endian order.
		/// </summary>
		public ushort NetworkOrderValue
		{
			get => value;
			set => this.value = value;
		}
		/// <summary>
		/// Gets or sets the value of the integer, in the order of the CPU architecture.
		/// </summary>
		public ushort Value
		{
			get => BitConverter.IsLittleEndian ? value : BinaryPrimitives.ReverseEndianness(value);
			set => this.value = BitConverter.IsLittleEndian ? value : BinaryPrimitives.ReverseEndianness(value);
		}

		public static UInt16LE FromNetworkOrder(ushort value) => new UInt16LE(value);
		public static UInt16LE FromHostOrder(ushort value) => new UInt16LE(BitConverter.IsLittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));

		/// <inheritdoc/>
		public override string ToString()
			=> Value.ToString();
	}
}
