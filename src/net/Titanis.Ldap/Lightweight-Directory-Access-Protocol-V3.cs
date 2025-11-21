namespace Lightweight_Directory_Access_Protocol_V3
{
	using System;
	using System.CodeDom.Compiler;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.Collections.Generic;
	using Titanis.Asn1;
	using Titanis.Asn1.Metadata;
	using Titanis.Asn1.Serialization;

	partial class Lightweight_Directory_Access_Protocol_V3Module
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static uint maxInt => 2147483647U;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Lightweight_Directory_Access_Protocol_V3Module()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private static Lightweight_Directory_Access_Protocol_V3Module _instance = new Lightweight_Directory_Access_Protocol_V3Module();
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Lightweight_Directory_Access_Protocol_V3Module Instance => _instance;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Name => "Lightweight-Directory-Access-Protocol-V3";

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Oid => "1.3.6.1.1.18";
	}

	[Asn1Sequence()]
	partial class SaslCredentials : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<SaslCredentials>, IAsn1DerDecodableValue<SaslCredentials>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] mechanism;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? credentials;
		private int v;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SaslCredentials(Byte[] mechanism, Byte[]? credentials = default)
		{
			this.mechanism = mechanism;
			this.credentials = credentials;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.credentials is not null)
				encoder.EncodeOctetStringTlv(this.credentials);
			encoder.EncodeOctetStringTlv(this.mechanism);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SaslCredentials DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new SaslCredentials(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SaslCredentials DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = SaslCredentials.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out SaslCredentials? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = SaslCredentials.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private SaslCredentials(Asn1DerDecoder decoder)
		{
			this.mechanism = decoder.DecodeOctetStringTlv();
			this.credentials = decoder.CheckTag(new Asn1Tag(0x4)) ? decoder.DecodeOctetStringTlv() : default(Byte[]);
		}
	}

	[Asn1Choice()]
	partial class AuthenticationChoice : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<AuthenticationChoice>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Byte[] Simple
		{
			get => this.simple;
			set
			{
				this.simple = value;
				this._choiceTag = ChoiceIndex.Simple;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Byte[]? simple;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SaslCredentials Sasl
		{
			get => this.sasl;
			set
			{
				this.sasl = value;
				this._choiceTag = ChoiceIndex.Sasl;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private SaslCredentials? sasl;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AuthenticationChoice()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Sasl:
					Debug.Assert(this.sasl is not null);
					encoder.EncodeValueTlv(this.sasl, new Asn1Tag(0xA0000003));
					break;
				case ChoiceIndex.Simple:
					Debug.Assert(this.simple is not null);
					encoder.EncodeOctetStringTlv(this.simple, new Asn1Tag(0x80000000));
					break;
				default:
					throw new InvalidOperationException("The object of type AuthenticationChoice has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AuthenticationChoice DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!AuthenticationChoice.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AuthenticationChoice? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x80000000)))
				instance = new AuthenticationChoice()
				{
					_choiceTag = ChoiceIndex.Simple,
					simple = decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000000))
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000003)))
				instance = new AuthenticationChoice()
				{
					_choiceTag = ChoiceIndex.Sasl,
					sasl = decoder.DecodeTaggedValue<SaslCredentials>(new Asn1Tag(0xA0000003))
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			Simple = 0x80000000,
			Sasl = 0xA0000003
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	partial class BindRequest : Asn1Implicit<BindRequest_Tagged0>, IAsn1DerDecodableTlv<BindRequest>, IAsn1DerDecodableValue<BindRequest>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BindRequest(BindRequest_Tagged0 value) : base(new Asn1Tag(0x60000000), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BindRequest DecodeValueFrom(Asn1DerDecoder decoder) => new BindRequest(decoder.DecodeValue<BindRequest_Tagged0>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BindRequest DecodeTlvFrom(Asn1DerDecoder decoder) => new BindRequest(decoder.DecodeTaggedValue<BindRequest_Tagged0>(new Asn1Tag(0x60000000)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out BindRequest? value)
		{
			if (decoder.TryDecodeTaggedValue<BindRequest_Tagged0>(new Asn1Tag(0x60000000), out var inner))
			{
				value = new BindRequest(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class BindRequest_Tagged0 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<BindRequest_Tagged0>, IAsn1DerDecodableValue<BindRequest_Tagged0>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal byte version;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] name;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AuthenticationChoice authentication;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BindRequest_Tagged0(byte version, Byte[] name, AuthenticationChoice authentication)
		{
			this.version = version;
			this.name = name;
			this.authentication = authentication;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			this.authentication.EncodeTlv(encoder);
			encoder.EncodeOctetStringTlv(this.name);
			encoder.EncodeByteTlv(this.version);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BindRequest_Tagged0 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new BindRequest_Tagged0(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BindRequest_Tagged0 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = BindRequest_Tagged0.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out BindRequest_Tagged0? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = BindRequest_Tagged0.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BindRequest_Tagged0(Asn1DerDecoder decoder)
		{
			this.version = decoder.DecodeIntegerTlvAsByte();
			this.name = decoder.DecodeOctetStringTlv();
			this.authentication = decoder.DecodeTlv<AuthenticationChoice>();
		}
	}

	[Asn1Sequence()]
	partial class LDAPResult : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<LDAPResult>, IAsn1DerDecodableValue<LDAPResult>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal LDAPResult_ResultCode resultCode;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] matchedDN;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] diagnosticMessage;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[][]? referral;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public LDAPResult(LDAPResult_ResultCode resultCode, Byte[] matchedDN, Byte[] diagnosticMessage, Byte[][]? referral = default)
		{
			this.resultCode = resultCode;
			this.matchedDN = matchedDN;
			this.diagnosticMessage = diagnosticMessage;
			this.referral = referral;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.referral is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000003), this.referral, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(r);
				});
			encoder.EncodeOctetStringTlv(this.diagnosticMessage);
			encoder.EncodeOctetStringTlv(this.matchedDN);
			encoder.EncodeEnumeratedTlv((long)this.resultCode);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static LDAPResult DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new LDAPResult(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static LDAPResult DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = LDAPResult.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out LDAPResult? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = LDAPResult.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private LDAPResult(Asn1DerDecoder decoder)
		{
			this.resultCode = (LDAPResult_ResultCode)decoder.DecodeEnumeratedTlv();
			this.matchedDN = decoder.DecodeOctetStringTlv();
			this.diagnosticMessage = decoder.DecodeOctetStringTlv();
			this.referral = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeListTlv<Byte[]>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[][]);
		}
	}

	[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
	public enum LDAPResult_ResultCode
	{
		Success = 0,
		OperationsError = 1,
		ProtocolError = 2,
		TimeLimitExceeded = 3,
		SizeLimitExceeded = 4,
		CompareFalse = 5,
		CompareTrue = 6,
		AuthMethodNotSupported = 7,
		StrongerAuthRequired = 8,
		Referral = 10,
		AdminLimitExceeded = 11,
		UnavailableCriticalExtension = 12,
		ConfidentialityRequired = 13,
		SaslBindInProgress = 14,
		NoSuchAttribute = 16,
		UndefinedAttributeType = 17,
		InappropriateMatching = 18,
		ConstraintViolation = 19,
		AttributeOrValueExists = 20,
		InvalidAttributeSyntax = 21,
		NoSuchObject = 32,
		AliasProblem = 33,
		InvalidDNSyntax = 34,
		AliasDereferencingProblem = 36,
		InappropriateAuthentication = 48,
		InvalidCredentials = 49,
		InsufficientAccessRights = 50,
		Busy = 51,
		Unavailable = 52,
		UnwillingToPerform = 53,
		LoopDetect = 54,
		NamingViolation = 64,
		ObjectClassViolation = 65,
		NotAllowedOnNonLeaf = 66,
		NotAllowedOnRDN = 67,
		EntryAlreadyExists = 68,
		ObjectClassModsProhibited = 69,
		AffectsMultipleDSAs = 71,
		Other = 80
	}

	partial class BindResponse : Asn1Implicit<BindResponse_Tagged1>, IAsn1DerDecodableTlv<BindResponse>, IAsn1DerDecodableValue<BindResponse>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BindResponse(BindResponse_Tagged1 value) : base(new Asn1Tag(0x60000001), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BindResponse DecodeValueFrom(Asn1DerDecoder decoder) => new BindResponse(decoder.DecodeValue<BindResponse_Tagged1>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BindResponse DecodeTlvFrom(Asn1DerDecoder decoder) => new BindResponse(decoder.DecodeTaggedValue<BindResponse_Tagged1>(new Asn1Tag(0x60000001)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out BindResponse? value)
		{
			if (decoder.TryDecodeTaggedValue<BindResponse_Tagged1>(new Asn1Tag(0x60000001), out var inner))
			{
				value = new BindResponse(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class BindResponse_Tagged1 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<BindResponse_Tagged1>, IAsn1DerDecodableValue<BindResponse_Tagged1>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal LDAPResult_ResultCode resultCode;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] matchedDN;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] diagnosticMessage;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[][]? referral;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? serverSaslCreds;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BindResponse_Tagged1(LDAPResult_ResultCode resultCode, Byte[] matchedDN, Byte[] diagnosticMessage, Byte[][]? referral = default, Byte[]? serverSaslCreds = default)
		{
			this.resultCode = resultCode;
			this.matchedDN = matchedDN;
			this.diagnosticMessage = diagnosticMessage;
			this.referral = referral;
			this.serverSaslCreds = serverSaslCreds;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.serverSaslCreds is not null)
				encoder.EncodeOctetStringTlv(this.serverSaslCreds, new Asn1Tag(0x80000007));
			if (this.referral is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000003), this.referral, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(r);
				});
			encoder.EncodeOctetStringTlv(this.diagnosticMessage);
			encoder.EncodeOctetStringTlv(this.matchedDN);
			encoder.EncodeEnumeratedTlv((long)this.resultCode);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BindResponse_Tagged1 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new BindResponse_Tagged1(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BindResponse_Tagged1 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = BindResponse_Tagged1.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out BindResponse_Tagged1? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = BindResponse_Tagged1.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BindResponse_Tagged1(Asn1DerDecoder decoder)
		{
			this.resultCode = (LDAPResult_ResultCode)decoder.DecodeEnumeratedTlv();
			this.matchedDN = decoder.DecodeOctetStringTlv();
			this.diagnosticMessage = decoder.DecodeOctetStringTlv();
			this.referral = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeListTlv<Byte[]>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[][]);
			this.serverSaslCreds = decoder.CheckTag(new Asn1Tag(0x80000007)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000007)) : default(Byte[]);
		}
	}

	partial class UnbindRequest : Asn1Implicit<Asn1Null>, IAsn1DerDecodableTlv<UnbindRequest>, IAsn1DerDecodableValue<UnbindRequest>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public UnbindRequest(Asn1Null value) : base(new Asn1Tag(0x40000002), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static UnbindRequest DecodeValueFrom(Asn1DerDecoder decoder) => new UnbindRequest(decoder.DecodeValue<Asn1Null>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static UnbindRequest DecodeTlvFrom(Asn1DerDecoder decoder) => new UnbindRequest(decoder.DecodeTaggedValue<Asn1Null>(new Asn1Tag(0x40000002)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out UnbindRequest? value)
		{
			if (decoder.TryDecodeTaggedValue<Asn1Null>(new Asn1Tag(0x40000002), out var inner))
			{
				value = new UnbindRequest(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class AttributeValueAssertion : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AttributeValueAssertion>, IAsn1DerDecodableValue<AttributeValueAssertion>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] attributeDesc;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] assertionValue;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttributeValueAssertion(Byte[] attributeDesc, Byte[] assertionValue)
		{
			this.attributeDesc = attributeDesc;
			this.assertionValue = assertionValue;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeOctetStringTlv(this.assertionValue);
			encoder.EncodeOctetStringTlv(this.attributeDesc);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeValueAssertion DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AttributeValueAssertion(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeValueAssertion DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AttributeValueAssertion.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AttributeValueAssertion? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AttributeValueAssertion.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private AttributeValueAssertion(Asn1DerDecoder decoder)
		{
			this.attributeDesc = decoder.DecodeOctetStringTlv();
			this.assertionValue = decoder.DecodeOctetStringTlv();
		}
	}

	[Asn1Sequence()]
	partial class SubstringFilter : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<SubstringFilter>, IAsn1DerDecodableValue<SubstringFilter>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal SubstringFilter_Substrings_Element[] substrings;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SubstringFilter(Byte[] type, SubstringFilter_Substrings_Element[] substrings)
		{
			this.type = type;
			this.substrings = substrings;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.substrings, (encoder, r) =>
			{
				r.EncodeTlv(encoder);
			});
			encoder.EncodeOctetStringTlv(this.type);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SubstringFilter DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new SubstringFilter(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SubstringFilter DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = SubstringFilter.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out SubstringFilter? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = SubstringFilter.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private SubstringFilter(Asn1DerDecoder decoder)
		{
			this.type = decoder.DecodeOctetStringTlv();
			this.substrings = decoder.DecodeListTlv<SubstringFilter_Substrings_Element>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<SubstringFilter_Substrings_Element>());
		}
	}

	[Asn1Choice()]
	partial class SubstringFilter_Substrings_Element : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<SubstringFilter_Substrings_Element>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Byte[] Initial
		{
			get => this.initial;
			set
			{
				this.initial = value;
				this._choiceTag = ChoiceIndex.Initial;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Byte[]? initial;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Byte[] Any
		{
			get => this.any;
			set
			{
				this.any = value;
				this._choiceTag = ChoiceIndex.Any;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Byte[]? any;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Byte[] Final
		{
			get => this.final;
			set
			{
				this.final = value;
				this._choiceTag = ChoiceIndex.Final;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Byte[]? final;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SubstringFilter_Substrings_Element()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Final:
					Debug.Assert(this.final is not null);
					encoder.EncodeOctetStringTlv(this.final, new Asn1Tag(0x80000002));
					break;
				case ChoiceIndex.Any:
					Debug.Assert(this.any is not null);
					encoder.EncodeOctetStringTlv(this.any, new Asn1Tag(0x80000001));
					break;
				case ChoiceIndex.Initial:
					Debug.Assert(this.initial is not null);
					encoder.EncodeOctetStringTlv(this.initial, new Asn1Tag(0x80000000));
					break;
				default:
					throw new InvalidOperationException("The object of type  has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SubstringFilter_Substrings_Element DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!SubstringFilter_Substrings_Element.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out SubstringFilter_Substrings_Element? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x80000000)))
				instance = new SubstringFilter_Substrings_Element()
				{
					_choiceTag = ChoiceIndex.Initial,
					initial = decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000000))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x80000001)))
				instance = new SubstringFilter_Substrings_Element()
				{
					_choiceTag = ChoiceIndex.Any,
					any = decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000001))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x80000002)))
				instance = new SubstringFilter_Substrings_Element()
				{
					_choiceTag = ChoiceIndex.Final,
					final = decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000002))
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			Initial = 0x80000000,
			Any = 0x80000001,
			Final = 0x80000002
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class MatchingRuleAssertion : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<MatchingRuleAssertion>, IAsn1DerDecodableValue<MatchingRuleAssertion>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? matchingRule;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] matchValue;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal bool dnAttributes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public MatchingRuleAssertion(Byte[] matchValue, Byte[]? matchingRule = default, Byte[]? type = default, bool dnAttributes = false)
		{
			this.matchingRule = matchingRule;
			this.type = type;
			this.matchValue = matchValue;
			this.dnAttributes = dnAttributes;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.dnAttributes != false)
				encoder.EncodeBoolTlv(this.dnAttributes, new Asn1Tag(0x80000004));
			encoder.EncodeOctetStringTlv(this.matchValue, new Asn1Tag(0x80000003));
			if (this.type is not null)
				encoder.EncodeOctetStringTlv(this.type, new Asn1Tag(0x80000002));
			if (this.matchingRule is not null)
				encoder.EncodeOctetStringTlv(this.matchingRule, new Asn1Tag(0x80000001));
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static MatchingRuleAssertion DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new MatchingRuleAssertion(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static MatchingRuleAssertion DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = MatchingRuleAssertion.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out MatchingRuleAssertion? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = MatchingRuleAssertion.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private MatchingRuleAssertion(Asn1DerDecoder decoder)
		{
			this.matchingRule = decoder.CheckTag(new Asn1Tag(0x80000001)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000001)) : default(Byte[]);
			this.type = decoder.CheckTag(new Asn1Tag(0x80000002)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000002)) : default(Byte[]);
			this.matchValue = decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000003));
			this.dnAttributes = decoder.CheckTag(new Asn1Tag(0x80000004)) ? decoder.DecodeBoolTlv(new Asn1Tag(0x80000004)) : false;
		}
	}

	[Asn1Choice()]
	partial class Filter : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<Filter>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Filter[] And
		{
			get => this.and;
			set
			{
				this.and = value;
				this._choiceTag = ChoiceIndex.And;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Filter[]? and;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Filter[] Or
		{
			get => this.or;
			set
			{
				this.or = value;
				this._choiceTag = ChoiceIndex.Or;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Filter[]? or;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Filter Not
		{
			get => this.not;
			set
			{
				this.not = value;
				this._choiceTag = ChoiceIndex.Not;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Filter? not;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttributeValueAssertion EqualityMatch
		{
			get => this.equalityMatch;
			set
			{
				this.equalityMatch = value;
				this._choiceTag = ChoiceIndex.EqualityMatch;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private AttributeValueAssertion? equalityMatch;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SubstringFilter Substrings
		{
			get => this.substrings;
			set
			{
				this.substrings = value;
				this._choiceTag = ChoiceIndex.Substrings;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private SubstringFilter? substrings;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttributeValueAssertion GreaterOrEqual
		{
			get => this.greaterOrEqual;
			set
			{
				this.greaterOrEqual = value;
				this._choiceTag = ChoiceIndex.GreaterOrEqual;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private AttributeValueAssertion? greaterOrEqual;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttributeValueAssertion LessOrEqual
		{
			get => this.lessOrEqual;
			set
			{
				this.lessOrEqual = value;
				this._choiceTag = ChoiceIndex.LessOrEqual;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private AttributeValueAssertion? lessOrEqual;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Byte[] Present
		{
			get => this.present;
			set
			{
				this.present = value;
				this._choiceTag = ChoiceIndex.Present;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Byte[]? present;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttributeValueAssertion ApproxMatch
		{
			get => this.approxMatch;
			set
			{
				this.approxMatch = value;
				this._choiceTag = ChoiceIndex.ApproxMatch;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private AttributeValueAssertion? approxMatch;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public MatchingRuleAssertion ExtensibleMatch
		{
			get => this.extensibleMatch;
			set
			{
				this.extensibleMatch = value;
				this._choiceTag = ChoiceIndex.ExtensibleMatch;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private MatchingRuleAssertion? extensibleMatch;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Filter()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.ExtensibleMatch:
					Debug.Assert(this.extensibleMatch is not null);
					encoder.EncodeValueTlv(this.extensibleMatch, new Asn1Tag(0xA0000009));
					break;
				case ChoiceIndex.ApproxMatch:
					Debug.Assert(this.approxMatch is not null);
					encoder.EncodeValueTlv(this.approxMatch, new Asn1Tag(0xA0000008));
					break;
				case ChoiceIndex.Present:
					Debug.Assert(this.present is not null);
					encoder.EncodeOctetStringTlv(this.present, new Asn1Tag(0x80000007));
					break;
				case ChoiceIndex.LessOrEqual:
					Debug.Assert(this.lessOrEqual is not null);
					encoder.EncodeValueTlv(this.lessOrEqual, new Asn1Tag(0xA0000006));
					break;
				case ChoiceIndex.GreaterOrEqual:
					Debug.Assert(this.greaterOrEqual is not null);
					encoder.EncodeValueTlv(this.greaterOrEqual, new Asn1Tag(0xA0000005));
					break;
				case ChoiceIndex.Substrings:
					Debug.Assert(this.substrings is not null);
					encoder.EncodeValueTlv(this.substrings, new Asn1Tag(0xA0000004));
					break;
				case ChoiceIndex.EqualityMatch:
					Debug.Assert(this.equalityMatch is not null);
					encoder.EncodeValueTlv(this.equalityMatch, new Asn1Tag(0xA0000003));
					break;
				case ChoiceIndex.Not:
					Debug.Assert(this.not is not null);
					encoder.EncodeExplicitTlv<Filter>(new Asn1Tag(0xA0000002), this.not, (encoder, r) =>
					{
						this.not.EncodeTlv(encoder);
					});
					break;
				case ChoiceIndex.Or:
					Debug.Assert(this.or is not null);
					encoder.EncodeListTlv(new Asn1Tag(0xA0000001), this.or, (encoder, r) =>
					{
						r.EncodeTlv(encoder);
					});
					break;
				case ChoiceIndex.And:
					Debug.Assert(this.and is not null);
					encoder.EncodeListTlv(new Asn1Tag(0xA0000000), this.and, (encoder, r) =>
					{
						r.EncodeTlv(encoder);
					});
					break;
				default:
					throw new InvalidOperationException("The object of type Filter has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Filter DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!Filter.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Filter? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				instance = new Filter()
				{
					_choiceTag = ChoiceIndex.And,
					and = decoder.DecodeListTlv<Filter>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<Filter>())
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000001)))
				instance = new Filter()
				{
					_choiceTag = ChoiceIndex.Or,
					or = decoder.DecodeListTlv<Filter>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<Filter>())
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000002)))
				instance = new Filter()
				{
					_choiceTag = ChoiceIndex.Not,
					not = decoder.DecodeTaggedValue<Filter>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeTlv<Filter>())
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000003)))
				instance = new Filter()
				{
					_choiceTag = ChoiceIndex.EqualityMatch,
					equalityMatch = decoder.DecodeTaggedValue<AttributeValueAssertion>(new Asn1Tag(0xA0000003))
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000004)))
				instance = new Filter()
				{
					_choiceTag = ChoiceIndex.Substrings,
					substrings = decoder.DecodeTaggedValue<SubstringFilter>(new Asn1Tag(0xA0000004))
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000005)))
				instance = new Filter()
				{
					_choiceTag = ChoiceIndex.GreaterOrEqual,
					greaterOrEqual = decoder.DecodeTaggedValue<AttributeValueAssertion>(new Asn1Tag(0xA0000005))
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000006)))
				instance = new Filter()
				{
					_choiceTag = ChoiceIndex.LessOrEqual,
					lessOrEqual = decoder.DecodeTaggedValue<AttributeValueAssertion>(new Asn1Tag(0xA0000006))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x80000007)))
				instance = new Filter()
				{
					_choiceTag = ChoiceIndex.Present,
					present = decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000007))
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000008)))
				instance = new Filter()
				{
					_choiceTag = ChoiceIndex.ApproxMatch,
					approxMatch = decoder.DecodeTaggedValue<AttributeValueAssertion>(new Asn1Tag(0xA0000008))
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000009)))
				instance = new Filter()
				{
					_choiceTag = ChoiceIndex.ExtensibleMatch,
					extensibleMatch = decoder.DecodeTaggedValue<MatchingRuleAssertion>(new Asn1Tag(0xA0000009))
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			And = 0xA0000000,
			Or = 0xA0000001,
			Not = 0xA0000002,
			EqualityMatch = 0xA0000003,
			Substrings = 0xA0000004,
			GreaterOrEqual = 0xA0000005,
			LessOrEqual = 0xA0000006,
			Present = 0x80000007,
			ApproxMatch = 0xA0000008,
			ExtensibleMatch = 0xA0000009
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	partial class SearchRequest : Asn1Implicit<SearchRequest_Tagged3>, IAsn1DerDecodableTlv<SearchRequest>, IAsn1DerDecodableValue<SearchRequest>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SearchRequest(SearchRequest_Tagged3 value) : base(new Asn1Tag(0x60000003), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SearchRequest DecodeValueFrom(Asn1DerDecoder decoder) => new SearchRequest(decoder.DecodeValue<SearchRequest_Tagged3>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SearchRequest DecodeTlvFrom(Asn1DerDecoder decoder) => new SearchRequest(decoder.DecodeTaggedValue<SearchRequest_Tagged3>(new Asn1Tag(0x60000003)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out SearchRequest? value)
		{
			if (decoder.TryDecodeTaggedValue<SearchRequest_Tagged3>(new Asn1Tag(0x60000003), out var inner))
			{
				value = new SearchRequest(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class SearchRequest_Tagged3 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<SearchRequest_Tagged3>, IAsn1DerDecodableValue<SearchRequest_Tagged3>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] baseObject;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal SearchRequest_Tagged3_Scope scope;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal SearchRequest_Tagged3_DerefAliases derefAliases;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint sizeLimit;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint timeLimit;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal bool typesOnly;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Filter filter;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[][] attributes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SearchRequest_Tagged3(Byte[] baseObject, SearchRequest_Tagged3_Scope scope, SearchRequest_Tagged3_DerefAliases derefAliases, uint sizeLimit, uint timeLimit, bool typesOnly, Filter filter, Byte[][] attributes)
		{
			this.baseObject = baseObject;
			this.scope = scope;
			this.derefAliases = derefAliases;
			this.sizeLimit = sizeLimit;
			this.timeLimit = timeLimit;
			this.typesOnly = typesOnly;
			this.filter = filter;
			this.attributes = attributes;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.attributes, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(r);
			});
			this.filter.EncodeTlv(encoder);
			encoder.EncodeBoolTlv(this.typesOnly);
			encoder.EncodeUInt32Tlv(this.timeLimit);
			encoder.EncodeUInt32Tlv(this.sizeLimit);
			encoder.EncodeEnumeratedTlv((long)this.derefAliases);
			encoder.EncodeEnumeratedTlv((long)this.scope);
			encoder.EncodeOctetStringTlv(this.baseObject);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SearchRequest_Tagged3 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new SearchRequest_Tagged3(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SearchRequest_Tagged3 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = SearchRequest_Tagged3.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out SearchRequest_Tagged3? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = SearchRequest_Tagged3.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private SearchRequest_Tagged3(Asn1DerDecoder decoder)
		{
			this.baseObject = decoder.DecodeOctetStringTlv();
			this.scope = (SearchRequest_Tagged3_Scope)decoder.DecodeEnumeratedTlv();
			this.derefAliases = (SearchRequest_Tagged3_DerefAliases)decoder.DecodeEnumeratedTlv();
			this.sizeLimit = decoder.DecodeIntegerTlvAsUInt32();
			this.timeLimit = decoder.DecodeIntegerTlvAsUInt32();
			this.typesOnly = decoder.DecodeBoolTlv();
			this.filter = decoder.DecodeTlv<Filter>();
			this.attributes = decoder.DecodeListTlv<Byte[]>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeOctetStringTlv());
		}
	}

	[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
	public enum SearchRequest_Tagged3_Scope
	{
		BaseObject = 0,
		SingleLevel = 1,
		WholeSubtree = 2
	}

	[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
	public enum SearchRequest_Tagged3_DerefAliases
	{
		NeverDerefAliases = 0,
		DerefInSearching = 1,
		DerefFindingBaseObj = 2,
		DerefAlways = 3
	}

	[Asn1Sequence()]
	partial class PartialAttribute : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PartialAttribute>, IAsn1DerDecodableValue<PartialAttribute>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[][] vals;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PartialAttribute(Byte[] type, Byte[][] vals)
		{
			this.type = type;
			this.vals = vals;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeListTlv(new Asn1Tag(0x20000011), this.vals, (encoder, r) =>
			{
				encoder.EncodeOctetStringTlv(r);
			});
			encoder.EncodeOctetStringTlv(this.type);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PartialAttribute DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PartialAttribute(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PartialAttribute DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PartialAttribute.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PartialAttribute? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PartialAttribute.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PartialAttribute(Asn1DerDecoder decoder)
		{
			this.type = decoder.DecodeOctetStringTlv();
			this.vals = decoder.DecodeListTlv<Byte[]>(new Asn1Tag(0x20000011), (encoder) => decoder.DecodeOctetStringTlv());
		}
	}

	partial class SearchResultEntry : Asn1Implicit<SearchResultEntry_Tagged4>, IAsn1DerDecodableTlv<SearchResultEntry>, IAsn1DerDecodableValue<SearchResultEntry>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SearchResultEntry(SearchResultEntry_Tagged4 value) : base(new Asn1Tag(0x60000004), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SearchResultEntry DecodeValueFrom(Asn1DerDecoder decoder) => new SearchResultEntry(decoder.DecodeValue<SearchResultEntry_Tagged4>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SearchResultEntry DecodeTlvFrom(Asn1DerDecoder decoder) => new SearchResultEntry(decoder.DecodeTaggedValue<SearchResultEntry_Tagged4>(new Asn1Tag(0x60000004)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out SearchResultEntry? value)
		{
			if (decoder.TryDecodeTaggedValue<SearchResultEntry_Tagged4>(new Asn1Tag(0x60000004), out var inner))
			{
				value = new SearchResultEntry(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class SearchResultEntry_Tagged4 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<SearchResultEntry_Tagged4>, IAsn1DerDecodableValue<SearchResultEntry_Tagged4>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] objectName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PartialAttribute[] attributes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SearchResultEntry_Tagged4(Byte[] objectName, PartialAttribute[] attributes)
		{
			this.objectName = objectName;
			this.attributes = attributes;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.attributes, (encoder, r) =>
			{
				encoder.EncodeValueTlv(r);
			});
			encoder.EncodeOctetStringTlv(this.objectName);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SearchResultEntry_Tagged4 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new SearchResultEntry_Tagged4(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SearchResultEntry_Tagged4 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = SearchResultEntry_Tagged4.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out SearchResultEntry_Tagged4? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = SearchResultEntry_Tagged4.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private SearchResultEntry_Tagged4(Asn1DerDecoder decoder)
		{
			this.objectName = decoder.DecodeOctetStringTlv();
			this.attributes = decoder.DecodeListTlv<PartialAttribute>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<PartialAttribute>());
		}
	}

	partial class SearchResultDone : Asn1Implicit<LDAPResult>, IAsn1DerDecodableTlv<SearchResultDone>, IAsn1DerDecodableValue<SearchResultDone>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SearchResultDone(LDAPResult value) : base(new Asn1Tag(0x60000005), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SearchResultDone DecodeValueFrom(Asn1DerDecoder decoder) => new SearchResultDone(decoder.DecodeValue<LDAPResult>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SearchResultDone DecodeTlvFrom(Asn1DerDecoder decoder) => new SearchResultDone(decoder.DecodeTaggedValue<LDAPResult>(new Asn1Tag(0x60000005)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out SearchResultDone? value)
		{
			if (decoder.TryDecodeTaggedValue<LDAPResult>(new Asn1Tag(0x60000005), out var inner))
			{
				value = new SearchResultDone(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class SearchResultReference : Asn1Implicit<Asn1SequenceOf<Asn1OctetString>>, IAsn1DerDecodableTlv<SearchResultReference>, IAsn1DerDecodableValue<SearchResultReference>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SearchResultReference(Asn1SequenceOf<Asn1OctetString> value) : base(new Asn1Tag(0x60000013), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SearchResultReference DecodeValueFrom(Asn1DerDecoder decoder) => new SearchResultReference(decoder.DecodeValue<Asn1SequenceOf<Asn1OctetString>>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SearchResultReference DecodeTlvFrom(Asn1DerDecoder decoder) => new SearchResultReference(decoder.DecodeTaggedValue<Asn1SequenceOf<Asn1OctetString>>(new Asn1Tag(0x60000013)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out SearchResultReference? value)
		{
			if (decoder.TryDecodeTaggedValue<Asn1SequenceOf<Asn1OctetString>>(new Asn1Tag(0x60000013), out var inner))
			{
				value = new SearchResultReference(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class ModifyRequest : Asn1Implicit<ModifyRequest_Tagged6>, IAsn1DerDecodableTlv<ModifyRequest>, IAsn1DerDecodableValue<ModifyRequest>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ModifyRequest(ModifyRequest_Tagged6 value) : base(new Asn1Tag(0x60000006), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyRequest DecodeValueFrom(Asn1DerDecoder decoder) => new ModifyRequest(decoder.DecodeValue<ModifyRequest_Tagged6>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyRequest DecodeTlvFrom(Asn1DerDecoder decoder) => new ModifyRequest(decoder.DecodeTaggedValue<ModifyRequest_Tagged6>(new Asn1Tag(0x60000006)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ModifyRequest? value)
		{
			if (decoder.TryDecodeTaggedValue<ModifyRequest_Tagged6>(new Asn1Tag(0x60000006), out var inner))
			{
				value = new ModifyRequest(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class ModifyRequest_Tagged6 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ModifyRequest_Tagged6>, IAsn1DerDecodableValue<ModifyRequest_Tagged6>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] @object;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ModifyRequest_Tagged6_Changes_Element[] changes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ModifyRequest_Tagged6(Byte[] @object, ModifyRequest_Tagged6_Changes_Element[] changes)
		{
			this.@object = @object;
			this.changes = changes;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.changes, (encoder, r) =>
			{
				encoder.EncodeValueTlv(r);
			});
			encoder.EncodeOctetStringTlv(this.@object);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyRequest_Tagged6 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ModifyRequest_Tagged6(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyRequest_Tagged6 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ModifyRequest_Tagged6.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ModifyRequest_Tagged6? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ModifyRequest_Tagged6.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ModifyRequest_Tagged6(Asn1DerDecoder decoder)
		{
			this.@object = decoder.DecodeOctetStringTlv();
			this.changes = decoder.DecodeListTlv<ModifyRequest_Tagged6_Changes_Element>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<ModifyRequest_Tagged6_Changes_Element>());
		}
	}

	[Asn1Sequence()]
	partial class ModifyRequest_Tagged6_Changes_Element : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ModifyRequest_Tagged6_Changes_Element>, IAsn1DerDecodableValue<ModifyRequest_Tagged6_Changes_Element>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ModifyRequest_Tagged6_Changes_Element_Operation operation;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PartialAttribute modification;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ModifyRequest_Tagged6_Changes_Element(ModifyRequest_Tagged6_Changes_Element_Operation operation, PartialAttribute modification)
		{
			this.operation = operation;
			this.modification = modification;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this.modification);
			encoder.EncodeEnumeratedTlv((long)this.operation);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyRequest_Tagged6_Changes_Element DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ModifyRequest_Tagged6_Changes_Element(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyRequest_Tagged6_Changes_Element DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ModifyRequest_Tagged6_Changes_Element.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ModifyRequest_Tagged6_Changes_Element? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ModifyRequest_Tagged6_Changes_Element.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ModifyRequest_Tagged6_Changes_Element(Asn1DerDecoder decoder)
		{
			this.operation = (ModifyRequest_Tagged6_Changes_Element_Operation)decoder.DecodeEnumeratedTlv();
			this.modification = decoder.DecodeTlv<PartialAttribute>();
		}
	}

	[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
	public enum ModifyRequest_Tagged6_Changes_Element_Operation
	{
		Add = 0,
		Delete = 1,
		Replace = 2
	}

	partial class ModifyResponse : Asn1Implicit<LDAPResult>, IAsn1DerDecodableTlv<ModifyResponse>, IAsn1DerDecodableValue<ModifyResponse>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ModifyResponse(LDAPResult value) : base(new Asn1Tag(0x60000007), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyResponse DecodeValueFrom(Asn1DerDecoder decoder) => new ModifyResponse(decoder.DecodeValue<LDAPResult>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyResponse DecodeTlvFrom(Asn1DerDecoder decoder) => new ModifyResponse(decoder.DecodeTaggedValue<LDAPResult>(new Asn1Tag(0x60000007)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ModifyResponse? value)
		{
			if (decoder.TryDecodeTaggedValue<LDAPResult>(new Asn1Tag(0x60000007), out var inner))
			{
				value = new ModifyResponse(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class AddRequest : Asn1Implicit<AddRequest_Tagged8>, IAsn1DerDecodableTlv<AddRequest>, IAsn1DerDecodableValue<AddRequest>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AddRequest(AddRequest_Tagged8 value) : base(new Asn1Tag(0x60000008), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AddRequest DecodeValueFrom(Asn1DerDecoder decoder) => new AddRequest(decoder.DecodeValue<AddRequest_Tagged8>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AddRequest DecodeTlvFrom(Asn1DerDecoder decoder) => new AddRequest(decoder.DecodeTaggedValue<AddRequest_Tagged8>(new Asn1Tag(0x60000008)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AddRequest? value)
		{
			if (decoder.TryDecodeTaggedValue<AddRequest_Tagged8>(new Asn1Tag(0x60000008), out var inner))
			{
				value = new AddRequest(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class AddRequest_Tagged8 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AddRequest_Tagged8>, IAsn1DerDecodableValue<AddRequest_Tagged8>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] entry;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PartialAttribute[] attributes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AddRequest_Tagged8(Byte[] entry, PartialAttribute[] attributes)
		{
			this.entry = entry;
			this.attributes = attributes;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.attributes, (encoder, r) =>
			{
				encoder.EncodeValueTlv(r);
			});
			encoder.EncodeOctetStringTlv(this.entry);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AddRequest_Tagged8 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AddRequest_Tagged8(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AddRequest_Tagged8 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AddRequest_Tagged8.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AddRequest_Tagged8? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AddRequest_Tagged8.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private AddRequest_Tagged8(Asn1DerDecoder decoder)
		{
			this.entry = decoder.DecodeOctetStringTlv();
			this.attributes = decoder.DecodeListTlv<PartialAttribute>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<PartialAttribute>());
		}
	}

	partial class AddResponse : Asn1Implicit<LDAPResult>, IAsn1DerDecodableTlv<AddResponse>, IAsn1DerDecodableValue<AddResponse>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AddResponse(LDAPResult value) : base(new Asn1Tag(0x60000009), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AddResponse DecodeValueFrom(Asn1DerDecoder decoder) => new AddResponse(decoder.DecodeValue<LDAPResult>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AddResponse DecodeTlvFrom(Asn1DerDecoder decoder) => new AddResponse(decoder.DecodeTaggedValue<LDAPResult>(new Asn1Tag(0x60000009)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AddResponse? value)
		{
			if (decoder.TryDecodeTaggedValue<LDAPResult>(new Asn1Tag(0x60000009), out var inner))
			{
				value = new AddResponse(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class DelRequest : Asn1Implicit<Asn1OctetString>, IAsn1DerDecodableTlv<DelRequest>, IAsn1DerDecodableValue<DelRequest>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public DelRequest(Asn1OctetString value) : base(new Asn1Tag(0x4000000A), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DelRequest DecodeValueFrom(Asn1DerDecoder decoder) => new DelRequest(decoder.DecodeValue<Asn1OctetString>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DelRequest DecodeTlvFrom(Asn1DerDecoder decoder) => new DelRequest(decoder.DecodeTaggedValue<Asn1OctetString>(new Asn1Tag(0x4000000A)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out DelRequest? value)
		{
			if (decoder.TryDecodeTaggedValue<Asn1OctetString>(new Asn1Tag(0x4000000A), out var inner))
			{
				value = new DelRequest(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class DelResponse : Asn1Implicit<LDAPResult>, IAsn1DerDecodableTlv<DelResponse>, IAsn1DerDecodableValue<DelResponse>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public DelResponse(LDAPResult value) : base(new Asn1Tag(0x6000000B), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DelResponse DecodeValueFrom(Asn1DerDecoder decoder) => new DelResponse(decoder.DecodeValue<LDAPResult>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DelResponse DecodeTlvFrom(Asn1DerDecoder decoder) => new DelResponse(decoder.DecodeTaggedValue<LDAPResult>(new Asn1Tag(0x6000000B)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out DelResponse? value)
		{
			if (decoder.TryDecodeTaggedValue<LDAPResult>(new Asn1Tag(0x6000000B), out var inner))
			{
				value = new DelResponse(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class ModifyDNRequest : Asn1Implicit<ModifyDNRequest_Tagged12>, IAsn1DerDecodableTlv<ModifyDNRequest>, IAsn1DerDecodableValue<ModifyDNRequest>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ModifyDNRequest(ModifyDNRequest_Tagged12 value) : base(new Asn1Tag(0x6000000C), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyDNRequest DecodeValueFrom(Asn1DerDecoder decoder) => new ModifyDNRequest(decoder.DecodeValue<ModifyDNRequest_Tagged12>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyDNRequest DecodeTlvFrom(Asn1DerDecoder decoder) => new ModifyDNRequest(decoder.DecodeTaggedValue<ModifyDNRequest_Tagged12>(new Asn1Tag(0x6000000C)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ModifyDNRequest? value)
		{
			if (decoder.TryDecodeTaggedValue<ModifyDNRequest_Tagged12>(new Asn1Tag(0x6000000C), out var inner))
			{
				value = new ModifyDNRequest(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class ModifyDNRequest_Tagged12 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ModifyDNRequest_Tagged12>, IAsn1DerDecodableValue<ModifyDNRequest_Tagged12>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] entry;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] newrdn;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal bool deleteoldrdn;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? newSuperior;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ModifyDNRequest_Tagged12(Byte[] entry, Byte[] newrdn, bool deleteoldrdn, Byte[]? newSuperior = default)
		{
			this.entry = entry;
			this.newrdn = newrdn;
			this.deleteoldrdn = deleteoldrdn;
			this.newSuperior = newSuperior;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.newSuperior is not null)
				encoder.EncodeOctetStringTlv(this.newSuperior, new Asn1Tag(0x80000000));
			encoder.EncodeBoolTlv(this.deleteoldrdn);
			encoder.EncodeOctetStringTlv(this.newrdn);
			encoder.EncodeOctetStringTlv(this.entry);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyDNRequest_Tagged12 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ModifyDNRequest_Tagged12(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyDNRequest_Tagged12 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ModifyDNRequest_Tagged12.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ModifyDNRequest_Tagged12? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ModifyDNRequest_Tagged12.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ModifyDNRequest_Tagged12(Asn1DerDecoder decoder)
		{
			this.entry = decoder.DecodeOctetStringTlv();
			this.newrdn = decoder.DecodeOctetStringTlv();
			this.deleteoldrdn = decoder.DecodeBoolTlv();
			this.newSuperior = decoder.CheckTag(new Asn1Tag(0x80000000)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000000)) : default(Byte[]);
		}
	}

	partial class ModifyDNResponse : Asn1Implicit<LDAPResult>, IAsn1DerDecodableTlv<ModifyDNResponse>, IAsn1DerDecodableValue<ModifyDNResponse>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ModifyDNResponse(LDAPResult value) : base(new Asn1Tag(0x6000000D), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyDNResponse DecodeValueFrom(Asn1DerDecoder decoder) => new ModifyDNResponse(decoder.DecodeValue<LDAPResult>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ModifyDNResponse DecodeTlvFrom(Asn1DerDecoder decoder) => new ModifyDNResponse(decoder.DecodeTaggedValue<LDAPResult>(new Asn1Tag(0x6000000D)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ModifyDNResponse? value)
		{
			if (decoder.TryDecodeTaggedValue<LDAPResult>(new Asn1Tag(0x6000000D), out var inner))
			{
				value = new ModifyDNResponse(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class CompareRequest : Asn1Implicit<CompareRequest_Tagged14>, IAsn1DerDecodableTlv<CompareRequest>, IAsn1DerDecodableValue<CompareRequest>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public CompareRequest(CompareRequest_Tagged14 value) : base(new Asn1Tag(0x6000000E), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static CompareRequest DecodeValueFrom(Asn1DerDecoder decoder) => new CompareRequest(decoder.DecodeValue<CompareRequest_Tagged14>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static CompareRequest DecodeTlvFrom(Asn1DerDecoder decoder) => new CompareRequest(decoder.DecodeTaggedValue<CompareRequest_Tagged14>(new Asn1Tag(0x6000000E)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out CompareRequest? value)
		{
			if (decoder.TryDecodeTaggedValue<CompareRequest_Tagged14>(new Asn1Tag(0x6000000E), out var inner))
			{
				value = new CompareRequest(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class CompareRequest_Tagged14 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<CompareRequest_Tagged14>, IAsn1DerDecodableValue<CompareRequest_Tagged14>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] entry;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AttributeValueAssertion ava;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public CompareRequest_Tagged14(Byte[] entry, AttributeValueAssertion ava)
		{
			this.entry = entry;
			this.ava = ava;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this.ava);
			encoder.EncodeOctetStringTlv(this.entry);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static CompareRequest_Tagged14 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new CompareRequest_Tagged14(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static CompareRequest_Tagged14 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = CompareRequest_Tagged14.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out CompareRequest_Tagged14? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = CompareRequest_Tagged14.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private CompareRequest_Tagged14(Asn1DerDecoder decoder)
		{
			this.entry = decoder.DecodeOctetStringTlv();
			this.ava = decoder.DecodeTlv<AttributeValueAssertion>();
		}
	}

	partial class CompareResponse : Asn1Implicit<LDAPResult>, IAsn1DerDecodableTlv<CompareResponse>, IAsn1DerDecodableValue<CompareResponse>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public CompareResponse(LDAPResult value) : base(new Asn1Tag(0x6000000F), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static CompareResponse DecodeValueFrom(Asn1DerDecoder decoder) => new CompareResponse(decoder.DecodeValue<LDAPResult>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static CompareResponse DecodeTlvFrom(Asn1DerDecoder decoder) => new CompareResponse(decoder.DecodeTaggedValue<LDAPResult>(new Asn1Tag(0x6000000F)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out CompareResponse? value)
		{
			if (decoder.TryDecodeTaggedValue<LDAPResult>(new Asn1Tag(0x6000000F), out var inner))
			{
				value = new CompareResponse(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class AbandonRequest : Asn1Implicit<Asn1Integer>, IAsn1DerDecodableTlv<AbandonRequest>, IAsn1DerDecodableValue<AbandonRequest>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AbandonRequest(Asn1Integer value) : base(new Asn1Tag(0x40000010), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AbandonRequest DecodeValueFrom(Asn1DerDecoder decoder) => new AbandonRequest(decoder.DecodeValue<Asn1Integer>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AbandonRequest DecodeTlvFrom(Asn1DerDecoder decoder) => new AbandonRequest(decoder.DecodeTaggedValue<Asn1Integer>(new Asn1Tag(0x40000010)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AbandonRequest? value)
		{
			if (decoder.TryDecodeTaggedValue<Asn1Integer>(new Asn1Tag(0x40000010), out var inner))
			{
				value = new AbandonRequest(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	partial class ExtendedRequest : Asn1Implicit<ExtendedRequest_Tagged23>, IAsn1DerDecodableTlv<ExtendedRequest>, IAsn1DerDecodableValue<ExtendedRequest>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ExtendedRequest(ExtendedRequest_Tagged23 value) : base(new Asn1Tag(0x60000017), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtendedRequest DecodeValueFrom(Asn1DerDecoder decoder) => new ExtendedRequest(decoder.DecodeValue<ExtendedRequest_Tagged23>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtendedRequest DecodeTlvFrom(Asn1DerDecoder decoder) => new ExtendedRequest(decoder.DecodeTaggedValue<ExtendedRequest_Tagged23>(new Asn1Tag(0x60000017)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ExtendedRequest? value)
		{
			if (decoder.TryDecodeTaggedValue<ExtendedRequest_Tagged23>(new Asn1Tag(0x60000017), out var inner))
			{
				value = new ExtendedRequest(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class ExtendedRequest_Tagged23 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ExtendedRequest_Tagged23>, IAsn1DerDecodableValue<ExtendedRequest_Tagged23>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] requestName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? requestValue;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ExtendedRequest_Tagged23(Byte[] requestName, Byte[]? requestValue = default)
		{
			this.requestName = requestName;
			this.requestValue = requestValue;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.requestValue is not null)
				encoder.EncodeOctetStringTlv(this.requestValue, new Asn1Tag(0x80000001));
			encoder.EncodeOctetStringTlv(this.requestName, new Asn1Tag(0x80000000));
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtendedRequest_Tagged23 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ExtendedRequest_Tagged23(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtendedRequest_Tagged23 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ExtendedRequest_Tagged23.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ExtendedRequest_Tagged23? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ExtendedRequest_Tagged23.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ExtendedRequest_Tagged23(Asn1DerDecoder decoder)
		{
			this.requestName = decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000000));
			this.requestValue = decoder.CheckTag(new Asn1Tag(0x80000001)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000001)) : default(Byte[]);
		}
	}

	partial class ExtendedResponse : Asn1Implicit<ExtendedResponse_Tagged24>, IAsn1DerDecodableTlv<ExtendedResponse>, IAsn1DerDecodableValue<ExtendedResponse>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ExtendedResponse(ExtendedResponse_Tagged24 value) : base(new Asn1Tag(0x60000018), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtendedResponse DecodeValueFrom(Asn1DerDecoder decoder) => new ExtendedResponse(decoder.DecodeValue<ExtendedResponse_Tagged24>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtendedResponse DecodeTlvFrom(Asn1DerDecoder decoder) => new ExtendedResponse(decoder.DecodeTaggedValue<ExtendedResponse_Tagged24>(new Asn1Tag(0x60000018)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ExtendedResponse? value)
		{
			if (decoder.TryDecodeTaggedValue<ExtendedResponse_Tagged24>(new Asn1Tag(0x60000018), out var inner))
			{
				value = new ExtendedResponse(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class ExtendedResponse_Tagged24 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ExtendedResponse_Tagged24>, IAsn1DerDecodableValue<ExtendedResponse_Tagged24>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal LDAPResult_ResultCode resultCode;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] matchedDN;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] diagnosticMessage;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[][]? referral;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? responseName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? responseValue;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ExtendedResponse_Tagged24(LDAPResult_ResultCode resultCode, Byte[] matchedDN, Byte[] diagnosticMessage, Byte[][]? referral = default, Byte[]? responseName = default, Byte[]? responseValue = default)
		{
			this.resultCode = resultCode;
			this.matchedDN = matchedDN;
			this.diagnosticMessage = diagnosticMessage;
			this.referral = referral;
			this.responseName = responseName;
			this.responseValue = responseValue;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.responseValue is not null)
				encoder.EncodeOctetStringTlv(this.responseValue, new Asn1Tag(0x8000000B));
			if (this.responseName is not null)
				encoder.EncodeOctetStringTlv(this.responseName, new Asn1Tag(0x8000000A));
			if (this.referral is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000003), this.referral, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(r);
				});
			encoder.EncodeOctetStringTlv(this.diagnosticMessage);
			encoder.EncodeOctetStringTlv(this.matchedDN);
			encoder.EncodeEnumeratedTlv((long)this.resultCode);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtendedResponse_Tagged24 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ExtendedResponse_Tagged24(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ExtendedResponse_Tagged24 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ExtendedResponse_Tagged24.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ExtendedResponse_Tagged24? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ExtendedResponse_Tagged24.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ExtendedResponse_Tagged24(Asn1DerDecoder decoder)
		{
			this.resultCode = (LDAPResult_ResultCode)decoder.DecodeEnumeratedTlv();
			this.matchedDN = decoder.DecodeOctetStringTlv();
			this.diagnosticMessage = decoder.DecodeOctetStringTlv();
			this.referral = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeListTlv<Byte[]>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[][]);
			this.responseName = decoder.CheckTag(new Asn1Tag(0x8000000A)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x8000000A)) : default(Byte[]);
			this.responseValue = decoder.CheckTag(new Asn1Tag(0x8000000B)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x8000000B)) : default(Byte[]);
		}
	}

	partial class IntermediateResponse : Asn1Implicit<IntermediateResponse_Tagged25>, IAsn1DerDecodableTlv<IntermediateResponse>, IAsn1DerDecodableValue<IntermediateResponse>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public IntermediateResponse(IntermediateResponse_Tagged25 value) : base(new Asn1Tag(0x60000019), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static IntermediateResponse DecodeValueFrom(Asn1DerDecoder decoder) => new IntermediateResponse(decoder.DecodeValue<IntermediateResponse_Tagged25>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static IntermediateResponse DecodeTlvFrom(Asn1DerDecoder decoder) => new IntermediateResponse(decoder.DecodeTaggedValue<IntermediateResponse_Tagged25>(new Asn1Tag(0x60000019)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out IntermediateResponse? value)
		{
			if (decoder.TryDecodeTaggedValue<IntermediateResponse_Tagged25>(new Asn1Tag(0x60000019), out var inner))
			{
				value = new IntermediateResponse(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}

	[Asn1Sequence()]
	partial class IntermediateResponse_Tagged25 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<IntermediateResponse_Tagged25>, IAsn1DerDecodableValue<IntermediateResponse_Tagged25>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? responseName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? responseValue;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public IntermediateResponse_Tagged25(Byte[]? responseName = default, Byte[]? responseValue = default)
		{
			this.responseName = responseName;
			this.responseValue = responseValue;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.responseValue is not null)
				encoder.EncodeOctetStringTlv(this.responseValue, new Asn1Tag(0x80000001));
			if (this.responseName is not null)
				encoder.EncodeOctetStringTlv(this.responseName, new Asn1Tag(0x80000000));
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static IntermediateResponse_Tagged25 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new IntermediateResponse_Tagged25(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static IntermediateResponse_Tagged25 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = IntermediateResponse_Tagged25.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out IntermediateResponse_Tagged25? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = IntermediateResponse_Tagged25.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private IntermediateResponse_Tagged25(Asn1DerDecoder decoder)
		{
			this.responseName = decoder.CheckTag(new Asn1Tag(0x80000000)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000000)) : default(Byte[]);
			this.responseValue = decoder.CheckTag(new Asn1Tag(0x80000001)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000001)) : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class Control : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Control>, IAsn1DerDecodableValue<Control>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[] controlType;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal bool criticality;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? controlValue;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Control(Byte[] controlType, bool criticality = false, Byte[]? controlValue = default)
		{
			this.controlType = controlType;
			this.criticality = criticality;
			this.controlValue = controlValue;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.controlValue is not null)
				encoder.EncodeOctetStringTlv(this.controlValue);
			if (this.criticality != false)
				encoder.EncodeBoolTlv(this.criticality);
			encoder.EncodeOctetStringTlv(this.controlType);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Control DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Control(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Control DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Control.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Control? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Control.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Control(Asn1DerDecoder decoder)
		{
			this.controlType = decoder.DecodeOctetStringTlv();
			this.criticality = decoder.CheckTag(new Asn1Tag(0x1)) ? decoder.DecodeBoolTlv() : false;
			this.controlValue = decoder.CheckTag(new Asn1Tag(0x4)) ? decoder.DecodeOctetStringTlv() : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class LDAPMessage : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<LDAPMessage>, IAsn1DerDecodableValue<LDAPMessage>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal uint messageID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal LDAPMessage_ProtocolOp protocolOp;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Control[]? controls;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public LDAPMessage(uint messageID, LDAPMessage_ProtocolOp protocolOp, Control[]? controls = default)
		{
			this.messageID = messageID;
			this.protocolOp = protocolOp;
			this.controls = controls;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.controls is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000000), this.controls, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			this.protocolOp.EncodeTlv(encoder);
			encoder.EncodeUInt32Tlv(this.messageID);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static LDAPMessage DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new LDAPMessage(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static LDAPMessage DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = LDAPMessage.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out LDAPMessage? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = LDAPMessage.DecodeValueFrom(decoder);
				decoder.CloseTlv(tlvFrame);
				return true;
			}
			else
			{
				instance = default;
				return false;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private LDAPMessage(Asn1DerDecoder decoder)
		{
			this.messageID = decoder.DecodeIntegerTlvAsUInt32();
			this.protocolOp = decoder.DecodeTlv<LDAPMessage_ProtocolOp>();
			this.controls = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeListTlv<Control>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<Control>()) : default(Control[]);
		}
	}

	[Asn1Choice()]
	partial class LDAPMessage_ProtocolOp : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<LDAPMessage_ProtocolOp>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BindRequest_Tagged0 BindRequest
		{
			get => this.bindRequest;
			set
			{
				this.bindRequest = value;
				this._choiceTag = ChoiceIndex.BindRequest;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BindRequest_Tagged0? bindRequest;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BindResponse_Tagged1 BindResponse
		{
			get => this.bindResponse;
			set
			{
				this.bindResponse = value;
				this._choiceTag = ChoiceIndex.BindResponse;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BindResponse_Tagged1? bindResponse;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Null UnbindRequest
		{
			get => this.unbindRequest.Value;
			set
			{
				this.unbindRequest = value;
				this._choiceTag = ChoiceIndex.UnbindRequest;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Asn1Null? unbindRequest;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SearchRequest_Tagged3 SearchRequest
		{
			get => this.searchRequest;
			set
			{
				this.searchRequest = value;
				this._choiceTag = ChoiceIndex.SearchRequest;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private SearchRequest_Tagged3? searchRequest;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SearchResultEntry_Tagged4 SearchResEntry
		{
			get => this.searchResEntry;
			set
			{
				this.searchResEntry = value;
				this._choiceTag = ChoiceIndex.SearchResEntry;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private SearchResultEntry_Tagged4? searchResEntry;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public LDAPResult SearchResDone
		{
			get => this.searchResDone;
			set
			{
				this.searchResDone = value;
				this._choiceTag = ChoiceIndex.SearchResDone;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private LDAPResult? searchResDone;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Byte[][] SearchResRef
		{
			get => this.searchResRef;
			set
			{
				this.searchResRef = value;
				this._choiceTag = ChoiceIndex.SearchResRef;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Byte[][]? searchResRef;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ModifyRequest_Tagged6 ModifyRequest
		{
			get => this.modifyRequest;
			set
			{
				this.modifyRequest = value;
				this._choiceTag = ChoiceIndex.ModifyRequest;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ModifyRequest_Tagged6? modifyRequest;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public LDAPResult ModifyResponse
		{
			get => this.modifyResponse;
			set
			{
				this.modifyResponse = value;
				this._choiceTag = ChoiceIndex.ModifyResponse;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private LDAPResult? modifyResponse;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AddRequest_Tagged8 AddRequest
		{
			get => this.addRequest;
			set
			{
				this.addRequest = value;
				this._choiceTag = ChoiceIndex.AddRequest;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private AddRequest_Tagged8? addRequest;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public LDAPResult AddResponse
		{
			get => this.addResponse;
			set
			{
				this.addResponse = value;
				this._choiceTag = ChoiceIndex.AddResponse;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private LDAPResult? addResponse;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Byte[] DelRequest
		{
			get => this.delRequest;
			set
			{
				this.delRequest = value;
				this._choiceTag = ChoiceIndex.DelRequest;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Byte[]? delRequest;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public LDAPResult DelResponse
		{
			get => this.delResponse;
			set
			{
				this.delResponse = value;
				this._choiceTag = ChoiceIndex.DelResponse;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private LDAPResult? delResponse;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ModifyDNRequest_Tagged12 ModDNRequest
		{
			get => this.modDNRequest;
			set
			{
				this.modDNRequest = value;
				this._choiceTag = ChoiceIndex.ModDNRequest;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ModifyDNRequest_Tagged12? modDNRequest;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public LDAPResult ModDNResponse
		{
			get => this.modDNResponse;
			set
			{
				this.modDNResponse = value;
				this._choiceTag = ChoiceIndex.ModDNResponse;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private LDAPResult? modDNResponse;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public CompareRequest_Tagged14 CompareRequest
		{
			get => this.compareRequest;
			set
			{
				this.compareRequest = value;
				this._choiceTag = ChoiceIndex.CompareRequest;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private CompareRequest_Tagged14? compareRequest;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public LDAPResult CompareResponse
		{
			get => this.compareResponse;
			set
			{
				this.compareResponse = value;
				this._choiceTag = ChoiceIndex.CompareResponse;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private LDAPResult? compareResponse;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public uint AbandonRequest
		{
			get => this.abandonRequest.Value;
			set
			{
				this.abandonRequest = value;
				this._choiceTag = ChoiceIndex.AbandonRequest;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private uint? abandonRequest;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ExtendedRequest_Tagged23 ExtendedReq
		{
			get => this.extendedReq;
			set
			{
				this.extendedReq = value;
				this._choiceTag = ChoiceIndex.ExtendedReq;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ExtendedRequest_Tagged23? extendedReq;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ExtendedResponse_Tagged24 ExtendedResp
		{
			get => this.extendedResp;
			set
			{
				this.extendedResp = value;
				this._choiceTag = ChoiceIndex.ExtendedResp;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ExtendedResponse_Tagged24? extendedResp;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public IntermediateResponse_Tagged25 IntermediateResponse
		{
			get => this.intermediateResponse;
			set
			{
				this.intermediateResponse = value;
				this._choiceTag = ChoiceIndex.IntermediateResponse;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private IntermediateResponse_Tagged25? intermediateResponse;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public LDAPMessage_ProtocolOp()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.IntermediateResponse:
					Debug.Assert(this.intermediateResponse is not null);
					encoder.EncodeValueTlv(this.intermediateResponse, new Asn1Tag(0x60000019));
					break;
				case ChoiceIndex.ExtendedResp:
					Debug.Assert(this.extendedResp is not null);
					encoder.EncodeValueTlv(this.extendedResp, new Asn1Tag(0x60000018));
					break;
				case ChoiceIndex.ExtendedReq:
					Debug.Assert(this.extendedReq is not null);
					encoder.EncodeValueTlv(this.extendedReq, new Asn1Tag(0x60000017));
					break;
				case ChoiceIndex.AbandonRequest:
					Debug.Assert(this.abandonRequest is not null);
					encoder.EncodeUInt32Tlv(this.abandonRequest.Value, new Asn1Tag(0x40000010));
					break;
				case ChoiceIndex.CompareResponse:
					Debug.Assert(this.compareResponse is not null);
					encoder.EncodeValueTlv(this.compareResponse, new Asn1Tag(0x6000000F));
					break;
				case ChoiceIndex.CompareRequest:
					Debug.Assert(this.compareRequest is not null);
					encoder.EncodeValueTlv(this.compareRequest, new Asn1Tag(0x6000000E));
					break;
				case ChoiceIndex.ModDNResponse:
					Debug.Assert(this.modDNResponse is not null);
					encoder.EncodeValueTlv(this.modDNResponse, new Asn1Tag(0x6000000D));
					break;
				case ChoiceIndex.ModDNRequest:
					Debug.Assert(this.modDNRequest is not null);
					encoder.EncodeValueTlv(this.modDNRequest, new Asn1Tag(0x6000000C));
					break;
				case ChoiceIndex.DelResponse:
					Debug.Assert(this.delResponse is not null);
					encoder.EncodeValueTlv(this.delResponse, new Asn1Tag(0x6000000B));
					break;
				case ChoiceIndex.DelRequest:
					Debug.Assert(this.delRequest is not null);
					encoder.EncodeOctetStringTlv(this.delRequest, new Asn1Tag(0x4000000A));
					break;
				case ChoiceIndex.AddResponse:
					Debug.Assert(this.addResponse is not null);
					encoder.EncodeValueTlv(this.addResponse, new Asn1Tag(0x60000009));
					break;
				case ChoiceIndex.AddRequest:
					Debug.Assert(this.addRequest is not null);
					encoder.EncodeValueTlv(this.addRequest, new Asn1Tag(0x60000008));
					break;
				case ChoiceIndex.ModifyResponse:
					Debug.Assert(this.modifyResponse is not null);
					encoder.EncodeValueTlv(this.modifyResponse, new Asn1Tag(0x60000007));
					break;
				case ChoiceIndex.ModifyRequest:
					Debug.Assert(this.modifyRequest is not null);
					encoder.EncodeValueTlv(this.modifyRequest, new Asn1Tag(0x60000006));
					break;
				case ChoiceIndex.SearchResRef:
					Debug.Assert(this.searchResRef is not null);
					encoder.EncodeListTlv(new Asn1Tag(0x60000013), this.searchResRef, (encoder, r) =>
					{
						encoder.EncodeOctetStringTlv(r);
					});
					break;
				case ChoiceIndex.SearchResDone:
					Debug.Assert(this.searchResDone is not null);
					encoder.EncodeValueTlv(this.searchResDone, new Asn1Tag(0x60000005));
					break;
				case ChoiceIndex.SearchResEntry:
					Debug.Assert(this.searchResEntry is not null);
					encoder.EncodeValueTlv(this.searchResEntry, new Asn1Tag(0x60000004));
					break;
				case ChoiceIndex.SearchRequest:
					Debug.Assert(this.searchRequest is not null);
					encoder.EncodeValueTlv(this.searchRequest, new Asn1Tag(0x60000003));
					break;
				case ChoiceIndex.UnbindRequest:
					Debug.Assert(this.unbindRequest is not null);
					encoder.EncodeNullTlv(this.unbindRequest.Value, new Asn1Tag(0x40000002));
					break;
				case ChoiceIndex.BindResponse:
					Debug.Assert(this.bindResponse is not null);
					encoder.EncodeValueTlv(this.bindResponse, new Asn1Tag(0x60000001));
					break;
				case ChoiceIndex.BindRequest:
					Debug.Assert(this.bindRequest is not null);
					encoder.EncodeValueTlv(this.bindRequest, new Asn1Tag(0x60000000));
					break;
				default:
					throw new InvalidOperationException("The object of type  has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static LDAPMessage_ProtocolOp DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!LDAPMessage_ProtocolOp.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out LDAPMessage_ProtocolOp? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x60000000)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.BindRequest,
					bindRequest = decoder.DecodeTaggedValue<BindRequest_Tagged0>(new Asn1Tag(0x60000000))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x60000001)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.BindResponse,
					bindResponse = decoder.DecodeTaggedValue<BindResponse_Tagged1>(new Asn1Tag(0x60000001))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x40000002)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.UnbindRequest,
					unbindRequest = decoder.DecodeNullTlv(new Asn1Tag(0x40000002))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x60000003)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.SearchRequest,
					searchRequest = decoder.DecodeTaggedValue<SearchRequest_Tagged3>(new Asn1Tag(0x60000003))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x60000004)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.SearchResEntry,
					searchResEntry = decoder.DecodeTaggedValue<SearchResultEntry_Tagged4>(new Asn1Tag(0x60000004))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x60000005)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.SearchResDone,
					searchResDone = decoder.DecodeTaggedValue<LDAPResult>(new Asn1Tag(0x60000005))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x60000013)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.SearchResRef,
					searchResRef = decoder.DecodeListTlv<Byte[]>(new Asn1Tag(0x60000013), (encoder) => decoder.DecodeOctetStringTlv())
				};
			else if (decoder.CheckTag(new Asn1Tag(0x60000006)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.ModifyRequest,
					modifyRequest = decoder.DecodeTaggedValue<ModifyRequest_Tagged6>(new Asn1Tag(0x60000006))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x60000007)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.ModifyResponse,
					modifyResponse = decoder.DecodeTaggedValue<LDAPResult>(new Asn1Tag(0x60000007))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x60000008)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.AddRequest,
					addRequest = decoder.DecodeTaggedValue<AddRequest_Tagged8>(new Asn1Tag(0x60000008))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x60000009)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.AddResponse,
					addResponse = decoder.DecodeTaggedValue<LDAPResult>(new Asn1Tag(0x60000009))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x4000000A)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.DelRequest,
					delRequest = decoder.DecodeOctetStringTlv(new Asn1Tag(0x4000000A))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x6000000B)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.DelResponse,
					delResponse = decoder.DecodeTaggedValue<LDAPResult>(new Asn1Tag(0x6000000B))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x6000000C)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.ModDNRequest,
					modDNRequest = decoder.DecodeTaggedValue<ModifyDNRequest_Tagged12>(new Asn1Tag(0x6000000C))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x6000000D)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.ModDNResponse,
					modDNResponse = decoder.DecodeTaggedValue<LDAPResult>(new Asn1Tag(0x6000000D))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x6000000E)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.CompareRequest,
					compareRequest = decoder.DecodeTaggedValue<CompareRequest_Tagged14>(new Asn1Tag(0x6000000E))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x6000000F)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.CompareResponse,
					compareResponse = decoder.DecodeTaggedValue<LDAPResult>(new Asn1Tag(0x6000000F))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x40000010)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.AbandonRequest,
					abandonRequest = decoder.DecodeIntegerTlvAsUInt32(new Asn1Tag(0x40000010))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x60000017)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.ExtendedReq,
					extendedReq = decoder.DecodeTaggedValue<ExtendedRequest_Tagged23>(new Asn1Tag(0x60000017))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x60000018)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.ExtendedResp,
					extendedResp = decoder.DecodeTaggedValue<ExtendedResponse_Tagged24>(new Asn1Tag(0x60000018))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x60000019)))
				instance = new LDAPMessage_ProtocolOp()
				{
					_choiceTag = ChoiceIndex.IntermediateResponse,
					intermediateResponse = decoder.DecodeTaggedValue<IntermediateResponse_Tagged25>(new Asn1Tag(0x60000019))
				};
			else
			{
				instance = null;
				return false;
			}

			return true;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public enum ChoiceIndex : uint
		{
			None = 0U,
			BindRequest = 1610612736U,
			BindResponse = 1610612737U,
			UnbindRequest = 1073741826U,
			SearchRequest = 1610612739U,
			SearchResEntry = 1610612740U,
			SearchResDone = 1610612741U,
			SearchResRef = 1610612755U,
			ModifyRequest = 1610612742U,
			ModifyResponse = 1610612743U,
			AddRequest = 1610612744U,
			AddResponse = 1610612745U,
			DelRequest = 1073741834U,
			DelResponse = 1610612747U,
			ModDNRequest = 1610612748U,
			ModDNResponse = 1610612749U,
			CompareRequest = 1610612750U,
			CompareResponse = 1610612751U,
			AbandonRequest = 1073741840U,
			ExtendedReq = 1610612759U,
			ExtendedResp = 1610612760U,
			IntermediateResponse = 1610612761U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}
}