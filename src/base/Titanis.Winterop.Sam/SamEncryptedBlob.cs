using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Winterop.Sam
{
	public ref struct SamEncryptedBlob
	{
		internal SamEncryptedBlob(ReadOnlySpan<byte> bytes)
		{
			this.Bytes = bytes;
		}

		public ReadOnlySpan<byte> Bytes { get; }
		public bool IsEmpty => this.Bytes.Length == 0;

		public int KeyId => BinaryPrimitives.ReadUInt16LittleEndian(this.Bytes.Slice(0, 2));
		public int Revision => BinaryPrimitives.ReadUInt16LittleEndian(this.Bytes.Slice(2, 2));
		public int EncryptedDataOffset => BinaryPrimitives.ReadUInt16LittleEndian(this.Bytes.Slice(4, 4));
		public ReadOnlySpan<byte> Salt => this.Bytes.Slice(8, 16);
		public ReadOnlySpan<byte> EncryptedData => this.Bytes.Slice(24);
	}
}
