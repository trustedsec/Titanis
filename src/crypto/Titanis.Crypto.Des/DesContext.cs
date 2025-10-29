using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Titanis.Crypto
{
	[InlineArray(SubkeyCount)]
	struct DesSubkeys
	{
		internal const int SubkeyCount = 16;
		internal ulong subkeys;
	}

	public struct DesContext
	{
		public DesContext(ulong key64)
		{
			if (BitConverter.IsLittleEndian)
				key64 = BinaryPrimitives.ReverseEndianness(key64);

			this._pc1 = DesPrimitives.DoPC1(key64);
			DesPrimitives.CalcSubkeys(this._pc1, ref this._subkeys);
		}

		private readonly ulong _pc1;
		private DesSubkeys _subkeys;

		public readonly ulong Transform(ulong block, bool decrypt)
		{
			return DesPrimitives.Transform(in this._subkeys, block, decrypt);
		}
	}
}
