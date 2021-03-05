using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Titanis.IO;

namespace Titanis.Asn1.Serialization
{
	public sealed class Asn1DerEncoding : Asn1Encoding
	{
		public static Asn1DerEncoding Instance { get; } = new Asn1DerEncoding();

		public override Asn1Decoder CreateDecoder(IByteSource reader)
		{
			return new Asn1DerDecoder(reader);
		}

		public static Asn1DerDecoder CreateDerDecoder(IByteSource reader)
		{
			return new Asn1DerDecoder(reader);
		}

		public static Asn1DerDecoder CreateDerDecoder(ReadOnlyMemory<byte> buffer)
		{
			return new Asn1DerDecoder(new ByteMemoryReader(buffer));
		}

		public override Asn1Encoder CreateEncoder()
		{
			ByteWriter writer = new ByteWriter(0x20, ByteWriterOptions.Reverse);
			return new Asn1DerEncoder(this, writer);
		}

		public static Asn1DerEncoder CreateDerEncoder()
		{
			ByteWriter writer = new ByteWriter(0x20, ByteWriterOptions.Reverse);
			return new Asn1DerEncoder(Instance, writer);
		}
	}
}
