using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Titanis.IO;

namespace Titanis.Asn1.Serialization
{
	public abstract class Asn1Encoding
	{
		public abstract Asn1Encoder CreateEncoder();
		public abstract Asn1Decoder CreateDecoder(IByteSource reader);
		public Asn1Decoder CreateDecoder(ReadOnlyMemory<byte> buffer)
		{
			return this.CreateDecoder(new ByteMemoryReader(buffer));
		}

	}
}
