using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace Titanis.IO
{
	public static class ByteOrdering
	{
		public static uint FromBE(uint be) => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(be) : be;
		public static uint ToBE(uint be) => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(be) : be;
		public static int FromBE(int be) => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(be) : be;
		public static int ToBE(int be) => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(be) : be;

		public static ushort FromBE(ushort be) => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(be) : be;
		public static ushort ToBE(ushort be) => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(be) : be;
	}
}
