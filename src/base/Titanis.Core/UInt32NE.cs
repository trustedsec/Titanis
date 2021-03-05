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
	/// An unsigned 32-bit integer in network byte order.
	/// </summary>
	/// <remarks>
	/// Use <see cref="Value"/> to get or set the value.  If the CPU architecture
	/// represents integers in a form other than network order, the integer is flipped.
	/// </remarks>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct UInt32NE
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
			get => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(this.value) : this.value;
			set => this.value = BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(value) : value;
		}

		/// <inheritdoc/>
		public override string ToString()
			=> this.Value.ToString();
	}
}
