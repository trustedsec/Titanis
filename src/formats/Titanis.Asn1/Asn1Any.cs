using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using Titanis.Asn1.Serialization;
using Titanis.IO;

namespace Titanis.Asn1
{
	/// <summary>
	/// Represents a value marked <c>ANY</c>.
	/// </summary>
	public class Asn1Any : IAsn1Tag, IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<Asn1Any>
	{
		public Asn1Any(Asn1Tag tag, byte[] tlvBytes)
		{
			if (tag.IsEmpty) throw new ArgumentNullException(nameof(tag));
			if (tlvBytes is null) throw new ArgumentNullException(nameof(tlvBytes));

			this.Tag = tag;
			this.TlvBytes = tlvBytes;

#if DEBUG
			Asn1DerDecoder testDecoder = new Asn1DerDecoder(new ByteMemoryReader(tlvBytes), Asn1DerDecoderOptions.AllowBer);
			var actualTag = testDecoder.PeekTag();
			Debug.Assert(actualTag == tag);
#endif
		}

		/// <summary>
		/// Bytes of entire TLV.
		/// </summary>
		public byte[] TlvBytes { get; }
		/// <inheritdoc/>
		public Asn1Tag Tag { get; }

		public static Asn1Any CreateFromObject<T>(T obj)
			where T : IAsn1DerEncodableTlv
		{
			var writer = new ByteWriter(0, ByteWriterOptions.Reverse);
			Asn1DerEncoder encoder = new Asn1DerEncoder(Asn1DerEncoding.Instance, writer);
			obj.EncodeTlv(encoder);

			return new Asn1Any(obj.Tag, encoder.GetBytes().ToArray());
		}

		static Asn1Any IAsn1DerDecodableTlv<Asn1Any>.DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tag = decoder.PeekTag();
			var content = decoder.DecodeNextTupleAsBytes();
			var any = new Asn1Any(tag, content);
			return any;
		}

		static bool IAsn1DerDecodableTlv<Asn1Any>.TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Asn1Any? instance)
		{
			var tag = decoder.PeekTag();
			if (tag.IsEmpty)
			{
				instance = null;
				return false;
			}
			else
			{
				var content = decoder.DecodeNextTupleAsBytes();
				instance = new Asn1Any(tag, content);
				return true;
			}
		}

		/// <inheritdoc/>
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.WriteBytes(this.TlvBytes.AsSpan());
		}
	}
}
