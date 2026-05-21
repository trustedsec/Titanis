using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

namespace Titanis.Ldap
{

	class PagingSearchControlValue : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<PagingSearchControlValue>, IAsn1DerDecodableValue<PagingSearchControlValue>
	{
		public PagingSearchControlValue(int size, byte[] cookie)
		{
			this.size = size;
			this.cookie = cookie;
		}


		public int size { get; set; }
		public byte[] cookie { get; set; }

		public static Asn1Tag StaticTag => new Asn1Tag(Asn1PredefTag.Sequence, Asn1TagFlags.Constructed);
		public Asn1Tag Tag => StaticTag;

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			var pos = encoder.Position;

			encoder.EncodeOctetStringTlv(this.cookie);
			encoder.EncodeInt32Tlv(this.size);

			encoder.EncodeCloseTlvHeader(this.Tag, pos);
		}

		public static PagingSearchControlValue DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			return decoder.DecodeTaggedValue<PagingSearchControlValue>(StaticTag);
		}

		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PagingSearchControlValue? value)
		{
			if (decoder.CheckTag(StaticTag))
			{
				value = DecodeTlvFrom(decoder);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}

		public static PagingSearchControlValue DecodeValueFrom(Asn1DerDecoder decoder)
		{
			return new PagingSearchControlValue(
				decoder.DecodeIntegerTlvAsInt32(),
				decoder.DecodeOctetStringTlv()
				);
		}
	}

	// [MS-ADTS] § 3.1.1.3.4.1.3 LDAP_SERVER_DIRSYNC_OID
	class DirSyncRequestValue : IAsn1DerEncodableValue, IAsn1DerEncodableTlv,
		IAsn1DerDecodableValue<DirSyncRequestValue>, IAsn1DerDecodableTlv<DirSyncRequestValue>
	{
		public DirSyncRequestValue(
			DirSyncFlags flags,
			int maxBytes = 0x100000,
			byte[]? cookie = null)
		{
			this.flags = flags;
			this.maxBytes = maxBytes;
			this.cookie = cookie;
		}

		public DirSyncFlags flags;
		public int maxBytes = 0x100000;
		public byte[]? cookie;

		public Asn1Tag Tag => StaticTag;
		public static Asn1Tag StaticTag => new Asn1Tag(Asn1PredefTag.Sequence, Asn1TagFlags.Constructed);

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			var pos = encoder.Position;
			this.EncodeValue(encoder);
			encoder.EncodeCloseTlvHeader(this.Tag, pos);
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.cookie != null)
				encoder.EncodeOctetStringTlv(this.cookie);
			encoder.EncodeInt32Tlv((int)maxBytes);
			encoder.EncodeInt32Tlv((int)this.flags);
		}

		public static DirSyncRequestValue DecodeValueFrom(Asn1DerDecoder decoder)
		{
			return new DirSyncRequestValue(
				(DirSyncFlags)decoder.DecodeIntegerTlvAsInt32(),
				decoder.DecodeIntegerTlvAsInt32(),
				(decoder.PeekTag() == Asn1PredefTag.OctetString)
				? decoder.DecodeOctetStringTlv() : null
				);
		}

		public static DirSyncRequestValue DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var frame = decoder.DecodeTlvStart(StaticTag);
			var value = DecodeValueFrom(decoder);
			decoder.CloseTlv(frame);
			return value;
		}

		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out DirSyncRequestValue? value)
		{
			if (decoder.TryDecodeTlv<DirSyncRequestValue>(out value))
				return true;
			else
			{
				value = default;
				return false;
			}
		}
	}

	internal enum DirSyncFlags : uint
	{
		ObjectSecurity = 1,
		AncestorsFirstOrder = 0x800,
		PublicDataOnly = 0x2000,
		IncrementalValues = 0x80000000,
	}
}
