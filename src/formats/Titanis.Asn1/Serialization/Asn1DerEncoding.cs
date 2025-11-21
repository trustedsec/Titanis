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

		public sealed override Asn1Decoder CreateDecoder(IByteSource reader)
			=> this.CreateDecoder(reader, Asn1DerDecoderOptions.None);

		public Asn1Decoder CreateDecoder(IByteSource reader, Asn1DerDecoderOptions options)
		{
			return new Asn1DerDecoder(reader, options);
		}

		public static Asn1DerDecoder CreateDerDecoder(IByteSource reader, Asn1DerDecoderOptions options = Asn1DerDecoderOptions.None)
		{
			return new Asn1DerDecoder(reader, options);
		}

		public static Asn1DerDecoder CreateDerDecoder(ReadOnlyMemory<byte> buffer, Asn1DerDecoderOptions options = Asn1DerDecoderOptions.None)
		{
			return new Asn1DerDecoder(new ByteMemoryReader(buffer), options);
		}

		public sealed override Asn1Encoder CreateEncoder()
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
