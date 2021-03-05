using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis
{
	/// <summary>
	/// An unsigned 32-bit integer in little-endian byte order.
	/// </summary>
	/// <remarks>
	/// Use <see cref="Value"/> to get or set the value.  If the CPU architecture
	/// represents integers in a form other than little-endian byte order, the integer is flipped.
	/// </remarks>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct UInt32LE
	{
		private uint value;

		/// <summary>
		/// Gets or sets the raw value of the integer, in network byte order.
		/// </summary>
		public uint NetworkOrderValue
		{
			get => this.value;
			set => this.value = value;
		}
		/// <summary>
		/// Gets or sets the value of the integer, in the order of the CPU architecture.
		/// </summary>
		public uint Value
		{
			get => BitConverter.IsLittleEndian ? this.value : BinaryPrimitives.ReverseEndianness(this.value);
			set => this.value = BitConverter.IsLittleEndian ? value : BinaryPrimitives.ReverseEndianness(value);
		}

		/// <inheritdoc/>
		public override string ToString()
			=> this.Value.ToString();
	}
}
