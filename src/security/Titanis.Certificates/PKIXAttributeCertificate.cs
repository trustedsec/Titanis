namespace PKIXAttributeCertificate
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
	using GeneralNames = PKIX1Implicit88.GeneralNames;
	using AlgorithmIdentifier = PKIX1Explicit88.AlgorithmIdentifier;
	using Attribute = PKIX1Explicit88.Attribute;
	using Extension = PKIX1Explicit88.Extension;
	using Extensions = PKIX1Explicit88.Extensions;
	using GeneralName = PKIX1Implicit88.GeneralName;

	partial class PKIXAttributeCertificateModule
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pe_ac_auditIdentity => new Asn1Oid("1.3.6.1.5.5.7.1.4");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pe_aaControls => new Asn1Oid("1.3.6.1.5.5.7.1.6");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pe_ac_proxying => new Asn1Oid("1.3.6.1.5.5.7.1.10");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_targetInformation => new Asn1Oid("2.5.29.55");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_aca => new Asn1Oid("1.3.6.1.5.5.7.10");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_aca_authenticationInfo => new Asn1Oid("1.3.6.1.5.5.7.10.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_aca_accessIdentity => new Asn1Oid("1.3.6.1.5.5.7.10.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_aca_chargingIdentity => new Asn1Oid("1.3.6.1.5.5.7.10.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_aca_group => new Asn1Oid("1.3.6.1.5.5.7.10.4");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_aca_encAttrs => new Asn1Oid("1.3.6.1.5.5.7.10.6");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_role => new Asn1Oid("2.5.4.72");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_at_clearance => new Asn1Oid("2.5.1.5.55");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte v2 => 1;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit unmarked => new Asn1NamedBit("unmarked", 0);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit unclassified => new Asn1NamedBit("unclassified", 1);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit restricted => new Asn1NamedBit("restricted", 2);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit confidential => new Asn1NamedBit("confidential", 3);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit secret => new Asn1NamedBit("secret", 4);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit topSecret => new Asn1NamedBit("topSecret", 5);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PKIXAttributeCertificateModule()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private static PKIXAttributeCertificateModule _instance = new PKIXAttributeCertificateModule();
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PKIXAttributeCertificateModule Instance => _instance;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Name => "PKIXAttributeCertificate";

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Oid => "1.3.6.1.5.5.7.0.12";
	}

	[Asn1Sequence()]
	partial class IssuerSerial : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<IssuerSerial>, IAsn1DerDecodableValue<IssuerSerial>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName[] issuer;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger serial;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString? issuerUID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public IssuerSerial(GeneralName[] issuer, System.Numerics.BigInteger serial, Asn1BitString? issuerUID = default)
		{
			this.issuer = issuer;
			this.serial = serial;
			this.issuerUID = issuerUID;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.issuerUID is not null)
				encoder.EncodeBitStringTlv(this.issuerUID.Value);
			encoder.EncodeBigIntegerTlv(this.serial);
			encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.issuer, (encoder, r) =>
			{
				r.EncodeTlv(encoder);
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static IssuerSerial DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new IssuerSerial(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static IssuerSerial DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = IssuerSerial.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out IssuerSerial? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = IssuerSerial.DecodeValueFrom(decoder);
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
		private IssuerSerial(Asn1DerDecoder decoder)
		{
			this.issuer = decoder.DecodeListTlv<GeneralName>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<GeneralName>());
			this.serial = decoder.DecodeIntegerTlvAsBigInteger();
			this.issuerUID = decoder.CheckTag(new Asn1Tag(0x3)) ? decoder.DecodeBitStringTlv() : default(Asn1BitString? );
		}
	}

	[Asn1Sequence()]
	partial class ObjectDigestInfo : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ObjectDigestInfo>, IAsn1DerDecodableValue<ObjectDigestInfo>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ObjectDigestInfo_DigestedObjectType digestedObjectType;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid? otherObjectTypeID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AlgorithmIdentifier digestAlgorithm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString objectDigest;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ObjectDigestInfo(ObjectDigestInfo_DigestedObjectType digestedObjectType, AlgorithmIdentifier digestAlgorithm, Asn1BitString objectDigest, Asn1Oid? otherObjectTypeID = default)
		{
			this.digestedObjectType = digestedObjectType;
			this.otherObjectTypeID = otherObjectTypeID;
			this.digestAlgorithm = digestAlgorithm;
			this.objectDigest = objectDigest;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBitStringTlv(this.objectDigest);
			encoder.EncodeValueTlv(this.digestAlgorithm);
			if (this.otherObjectTypeID is not null)
				encoder.EncodeOidTlv(this.otherObjectTypeID.Value);
			encoder.EncodeEnumeratedTlv((long)this.digestedObjectType);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ObjectDigestInfo DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ObjectDigestInfo(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ObjectDigestInfo DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ObjectDigestInfo.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ObjectDigestInfo? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ObjectDigestInfo.DecodeValueFrom(decoder);
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
		private ObjectDigestInfo(Asn1DerDecoder decoder)
		{
			this.digestedObjectType = (ObjectDigestInfo_DigestedObjectType)decoder.DecodeEnumeratedTlv();
			this.otherObjectTypeID = decoder.CheckTag(new Asn1Tag(0x6)) ? decoder.DecodeOidTlv() : default(Asn1Oid? );
			this.digestAlgorithm = decoder.DecodeTlv<AlgorithmIdentifier>();
			this.objectDigest = decoder.DecodeBitStringTlv();
		}
	}

	[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
	public enum ObjectDigestInfo_DigestedObjectType
	{
		PublicKey = 0,
		PublicKeyCert = 1,
		OtherObjectTypes = 2
	}

	[Asn1Sequence()]
	partial class Holder : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Holder>, IAsn1DerDecodableValue<Holder>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal IssuerSerial? baseCertificateID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName[]? entityName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ObjectDigestInfo? objectDigestInfo;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Holder(IssuerSerial? baseCertificateID = default, GeneralName[]? entityName = default, ObjectDigestInfo? objectDigestInfo = default)
		{
			this.baseCertificateID = baseCertificateID;
			this.entityName = entityName;
			this.objectDigestInfo = objectDigestInfo;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.objectDigestInfo is not null)
				encoder.EncodeValueTlv(this.objectDigestInfo, new Asn1Tag(0xA0000002));
			if (this.entityName is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000001), this.entityName, (encoder, r) =>
				{
					r.EncodeTlv(encoder);
				});
			if (this.baseCertificateID is not null)
				encoder.EncodeValueTlv(this.baseCertificateID, new Asn1Tag(0xA0000000));
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Holder DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Holder(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Holder DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Holder.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Holder? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Holder.DecodeValueFrom(decoder);
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
		private Holder(Asn1DerDecoder decoder)
		{
			this.baseCertificateID = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<IssuerSerial>(new Asn1Tag(0xA0000000)) : default(IssuerSerial);
			this.entityName = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeListTlv<GeneralName>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<GeneralName>()) : default(GeneralName[]);
			this.objectDigestInfo = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<ObjectDigestInfo>(new Asn1Tag(0xA0000002)) : default(ObjectDigestInfo);
		}
	}

	[Asn1Sequence()]
	partial class V2Form : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<V2Form>, IAsn1DerDecodableValue<V2Form>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName[]? issuerName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal IssuerSerial? baseCertificateID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ObjectDigestInfo? objectDigestInfo;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public V2Form(GeneralName[]? issuerName = default, IssuerSerial? baseCertificateID = default, ObjectDigestInfo? objectDigestInfo = default)
		{
			this.issuerName = issuerName;
			this.baseCertificateID = baseCertificateID;
			this.objectDigestInfo = objectDigestInfo;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.objectDigestInfo is not null)
				encoder.EncodeValueTlv(this.objectDigestInfo, new Asn1Tag(0xA0000001));
			if (this.baseCertificateID is not null)
				encoder.EncodeValueTlv(this.baseCertificateID, new Asn1Tag(0xA0000000));
			if (this.issuerName is not null)
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.issuerName, (encoder, r) =>
				{
					r.EncodeTlv(encoder);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static V2Form DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new V2Form(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static V2Form DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = V2Form.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out V2Form? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = V2Form.DecodeValueFrom(decoder);
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
		private V2Form(Asn1DerDecoder decoder)
		{
			this.issuerName = decoder.CheckTag(new Asn1Tag(0x20000010)) ? decoder.DecodeListTlv<GeneralName>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<GeneralName>()) : default(GeneralName[]);
			this.baseCertificateID = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<IssuerSerial>(new Asn1Tag(0xA0000000)) : default(IssuerSerial);
			this.objectDigestInfo = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<ObjectDigestInfo>(new Asn1Tag(0xA0000001)) : default(ObjectDigestInfo);
		}
	}

	[Asn1Choice()]
	partial class AttCertIssuer : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<AttCertIssuer>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public GeneralName[] V1Form
		{
			get => this.v1Form;
			set
			{
				this.v1Form = value;
				this._choiceTag = ChoiceIndex.V1Form;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private GeneralName[]? v1Form;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public V2Form V2Form
		{
			get => this.v2Form;
			set
			{
				this.v2Form = value;
				this._choiceTag = ChoiceIndex.V2Form;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private V2Form? v2Form;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttCertIssuer()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.V2Form:
					Debug.Assert(this.v2Form is not null);
					encoder.EncodeValueTlv(this.v2Form, new Asn1Tag(0xA0000000));
					break;
				case ChoiceIndex.V1Form:
					Debug.Assert(this.v1Form is not null);
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.v1Form, (encoder, r) =>
					{
						r.EncodeTlv(encoder);
					});
					break;
				default:
					throw new InvalidOperationException("The object of type AttCertIssuer has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttCertIssuer DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!AttCertIssuer.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AttCertIssuer? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
				instance = new AttCertIssuer()
				{
					_choiceTag = ChoiceIndex.V1Form,
					v1Form = decoder.DecodeListTlv<GeneralName>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<GeneralName>())
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				instance = new AttCertIssuer()
				{
					_choiceTag = ChoiceIndex.V2Form,
					v2Form = decoder.DecodeTaggedValue<V2Form>(new Asn1Tag(0xA0000000))
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
			V1Form = 536870928U,
			V2Form = 0xA0000000
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class AttCertValidityPeriod : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AttCertValidityPeriod>, IAsn1DerDecodableValue<AttCertValidityPeriod>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime notBeforeTime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime notAfterTime;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttCertValidityPeriod(GeneralizedTime notBeforeTime, GeneralizedTime notAfterTime)
		{
			this.notBeforeTime = notBeforeTime;
			this.notAfterTime = notAfterTime;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeDateTimeTlv(this.notAfterTime);
			encoder.EncodeDateTimeTlv(this.notBeforeTime);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttCertValidityPeriod DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AttCertValidityPeriod(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttCertValidityPeriod DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AttCertValidityPeriod.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AttCertValidityPeriod? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AttCertValidityPeriod.DecodeValueFrom(decoder);
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
		private AttCertValidityPeriod(Asn1DerDecoder decoder)
		{
			this.notBeforeTime = decoder.DecodeDateTimeTlv();
			this.notAfterTime = decoder.DecodeDateTimeTlv();
		}
	}

	[Asn1Sequence()]
	partial class AttributeCertificateInfo : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AttributeCertificateInfo>, IAsn1DerDecodableValue<AttributeCertificateInfo>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger version;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Holder holder;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AttCertIssuer issuer;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AlgorithmIdentifier signature;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger serialNumber;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AttCertValidityPeriod attrCertValidityPeriod;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Attribute[] attributes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString? issuerUniqueID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Extension[]? extensions;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttributeCertificateInfo(System.Numerics.BigInteger version, Holder holder, AttCertIssuer issuer, AlgorithmIdentifier signature, System.Numerics.BigInteger serialNumber, AttCertValidityPeriod attrCertValidityPeriod, Attribute[] attributes, Asn1BitString? issuerUniqueID = default, Extension[]? extensions = default)
		{
			this.version = version;
			this.holder = holder;
			this.issuer = issuer;
			this.signature = signature;
			this.serialNumber = serialNumber;
			this.attrCertValidityPeriod = attrCertValidityPeriod;
			this.attributes = attributes;
			this.issuerUniqueID = issuerUniqueID;
			this.extensions = extensions;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.extensions is not null)
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.extensions, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			if (this.issuerUniqueID is not null)
				encoder.EncodeBitStringTlv(this.issuerUniqueID.Value);
			encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.attributes, (encoder, r) =>
			{
				encoder.EncodeValueTlv(r);
			});
			encoder.EncodeValueTlv(this.attrCertValidityPeriod);
			encoder.EncodeBigIntegerTlv(this.serialNumber);
			encoder.EncodeValueTlv(this.signature);
			this.issuer.EncodeTlv(encoder);
			encoder.EncodeValueTlv(this.holder);
			encoder.EncodeBigIntegerTlv(this.version);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeCertificateInfo DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AttributeCertificateInfo(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeCertificateInfo DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AttributeCertificateInfo.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AttributeCertificateInfo? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AttributeCertificateInfo.DecodeValueFrom(decoder);
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
		private AttributeCertificateInfo(Asn1DerDecoder decoder)
		{
			this.version = decoder.DecodeIntegerTlvAsBigInteger();
			this.holder = decoder.DecodeTlv<Holder>();
			this.issuer = decoder.DecodeTlv<AttCertIssuer>();
			this.signature = decoder.DecodeTlv<AlgorithmIdentifier>();
			this.serialNumber = decoder.DecodeIntegerTlvAsBigInteger();
			this.attrCertValidityPeriod = decoder.DecodeTlv<AttCertValidityPeriod>();
			this.attributes = decoder.DecodeListTlv<Attribute>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<Attribute>());
			this.issuerUniqueID = decoder.CheckTag(new Asn1Tag(0x3)) ? decoder.DecodeBitStringTlv() : default(Asn1BitString? );
			this.extensions = decoder.CheckTag(new Asn1Tag(0x20000010)) ? decoder.DecodeListTlv<Extension>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<Extension>()) : default(Extension[]);
		}
	}

	[Asn1Sequence()]
	partial class AttributeCertificate : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AttributeCertificate>, IAsn1DerDecodableValue<AttributeCertificate>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AttributeCertificateInfo acinfo;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AlgorithmIdentifier signatureAlgorithm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString signatureValue;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttributeCertificate(AttributeCertificateInfo acinfo, AlgorithmIdentifier signatureAlgorithm, Asn1BitString signatureValue)
		{
			this.acinfo = acinfo;
			this.signatureAlgorithm = signatureAlgorithm;
			this.signatureValue = signatureValue;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBitStringTlv(this.signatureValue);
			encoder.EncodeValueTlv(this.signatureAlgorithm);
			encoder.EncodeValueTlv(this.acinfo);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeCertificate DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AttributeCertificate(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeCertificate DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AttributeCertificate.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AttributeCertificate? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AttributeCertificate.DecodeValueFrom(decoder);
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
		private AttributeCertificate(Asn1DerDecoder decoder)
		{
			this.acinfo = decoder.DecodeTlv<AttributeCertificateInfo>();
			this.signatureAlgorithm = decoder.DecodeTlv<AlgorithmIdentifier>();
			this.signatureValue = decoder.DecodeBitStringTlv();
		}
	}

	[Asn1Sequence()]
	partial class TargetCert : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<TargetCert>, IAsn1DerDecodableValue<TargetCert>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal IssuerSerial targetCertificate;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName? targetName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ObjectDigestInfo? certDigestInfo;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TargetCert(IssuerSerial targetCertificate, GeneralName? targetName = default, ObjectDigestInfo? certDigestInfo = default)
		{
			this.targetCertificate = targetCertificate;
			this.targetName = targetName;
			this.certDigestInfo = certDigestInfo;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.certDigestInfo is not null)
				encoder.EncodeValueTlv(this.certDigestInfo);
			if (this.targetName is not null)
				this.targetName.EncodeTlv(encoder);
			encoder.EncodeValueTlv(this.targetCertificate);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TargetCert DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new TargetCert(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static TargetCert DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = TargetCert.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out TargetCert? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = TargetCert.DecodeValueFrom(decoder);
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
		private TargetCert(Asn1DerDecoder decoder)
		{
			this.targetCertificate = decoder.DecodeTlv<IssuerSerial>();
			this.targetName = decoder.TryDecodeTlv<GeneralName>(out this.targetName) ? this.targetName : default(GeneralName);
			this.certDigestInfo = decoder.CheckTag(new Asn1Tag(0x20000010)) ? decoder.DecodeTlv<ObjectDigestInfo>() : default(ObjectDigestInfo);
		}
	}

	[Asn1Choice()]
	partial class Target : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<Target>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public GeneralName TargetName
		{
			get => this.targetName;
			set
			{
				this.targetName = value;
				this._choiceTag = ChoiceIndex.TargetName;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private GeneralName? targetName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public GeneralName TargetGroup
		{
			get => this.targetGroup;
			set
			{
				this.targetGroup = value;
				this._choiceTag = ChoiceIndex.TargetGroup;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private GeneralName? targetGroup;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public TargetCert TargetCert
		{
			get => this.targetCert;
			set
			{
				this.targetCert = value;
				this._choiceTag = ChoiceIndex.TargetCert;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private TargetCert? targetCert;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Target()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.TargetCert:
					Debug.Assert(this.targetCert is not null);
					encoder.EncodeValueTlv(this.targetCert, new Asn1Tag(0xA0000002));
					break;
				case ChoiceIndex.TargetGroup:
					Debug.Assert(this.targetGroup is not null);
					encoder.EncodeExplicitTlv<GeneralName>(new Asn1Tag(0xA0000001), this.targetGroup, (encoder, r) =>
					{
						this.targetGroup.EncodeTlv(encoder);
					});
					break;
				case ChoiceIndex.TargetName:
					Debug.Assert(this.targetName is not null);
					encoder.EncodeExplicitTlv<GeneralName>(new Asn1Tag(0xA0000000), this.targetName, (encoder, r) =>
					{
						this.targetName.EncodeTlv(encoder);
					});
					break;
				default:
					throw new InvalidOperationException("The object of type Target has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Target DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!Target.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Target? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				instance = new Target()
				{
					_choiceTag = ChoiceIndex.TargetName,
					targetName = decoder.DecodeTaggedValue<GeneralName>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<GeneralName>())
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000001)))
				instance = new Target()
				{
					_choiceTag = ChoiceIndex.TargetGroup,
					targetGroup = decoder.DecodeTaggedValue<GeneralName>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<GeneralName>())
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000002)))
				instance = new Target()
				{
					_choiceTag = ChoiceIndex.TargetCert,
					targetCert = decoder.DecodeTaggedValue<TargetCert>(new Asn1Tag(0xA0000002))
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
			TargetName = 0xA0000000,
			TargetGroup = 0xA0000001,
			TargetCert = 0xA0000002
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class IetfAttrSyntax : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<IetfAttrSyntax>, IAsn1DerDecodableValue<IetfAttrSyntax>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName[]? policyAuthority;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal IetfAttrSyntax_Values_Element[] values;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public IetfAttrSyntax(IetfAttrSyntax_Values_Element[] values, GeneralName[]? policyAuthority = default)
		{
			this.policyAuthority = policyAuthority;
			this.values = values;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.values, (encoder, r) =>
			{
				r.EncodeTlv(encoder);
			});
			if (this.policyAuthority is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000000), this.policyAuthority, (encoder, r) =>
				{
					r.EncodeTlv(encoder);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static IetfAttrSyntax DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new IetfAttrSyntax(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static IetfAttrSyntax DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = IetfAttrSyntax.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out IetfAttrSyntax? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = IetfAttrSyntax.DecodeValueFrom(decoder);
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
		private IetfAttrSyntax(Asn1DerDecoder decoder)
		{
			this.policyAuthority = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeListTlv<GeneralName>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<GeneralName>()) : default(GeneralName[]);
			this.values = decoder.DecodeListTlv<IetfAttrSyntax_Values_Element>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<IetfAttrSyntax_Values_Element>());
		}
	}

	[Asn1Choice()]
	partial class IetfAttrSyntax_Values_Element : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<IetfAttrSyntax_Values_Element>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Byte[] Octets
		{
			get => this.octets;
			set
			{
				this.octets = value;
				this._choiceTag = ChoiceIndex.Octets;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Byte[]? octets;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Oid Oid
		{
			get => this.oid.Value;
			set
			{
				this.oid = value;
				this._choiceTag = ChoiceIndex.Oid;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Asn1Oid? oid;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public String String
		{
			get => this.@string;
			set
			{
				this.@string = value;
				this._choiceTag = ChoiceIndex.String;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private String? @string;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public IetfAttrSyntax_Values_Element()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.String:
					Debug.Assert(this.@string is not null);
					encoder.EncodeUtf8StringTlv(this.@string);
					break;
				case ChoiceIndex.Oid:
					Debug.Assert(this.oid is not null);
					encoder.EncodeOidTlv(this.oid.Value);
					break;
				case ChoiceIndex.Octets:
					Debug.Assert(this.octets is not null);
					encoder.EncodeOctetStringTlv(this.octets);
					break;
				default:
					throw new InvalidOperationException("The object of type  has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static IetfAttrSyntax_Values_Element DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!IetfAttrSyntax_Values_Element.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out IetfAttrSyntax_Values_Element? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x4)))
				instance = new IetfAttrSyntax_Values_Element()
				{
					_choiceTag = ChoiceIndex.Octets,
					octets = decoder.DecodeOctetStringTlv()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x6)))
				instance = new IetfAttrSyntax_Values_Element()
				{
					_choiceTag = ChoiceIndex.Oid,
					oid = decoder.DecodeOidTlv()
				};
			else if (decoder.CheckTag(new Asn1Tag(0xC)))
				instance = new IetfAttrSyntax_Values_Element()
				{
					_choiceTag = ChoiceIndex.String,
					@string = decoder.DecodeUtf8StringTlv()
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
			Octets = 4U,
			Oid = 6U,
			String = 12U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class SvceAuthInfo : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<SvceAuthInfo>, IAsn1DerDecodableValue<SvceAuthInfo>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName service;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName ident;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? authInfo;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SvceAuthInfo(GeneralName service, GeneralName ident, Byte[]? authInfo = default)
		{
			this.service = service;
			this.ident = ident;
			this.authInfo = authInfo;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.authInfo is not null)
				encoder.EncodeOctetStringTlv(this.authInfo);
			this.ident.EncodeTlv(encoder);
			this.service.EncodeTlv(encoder);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SvceAuthInfo DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new SvceAuthInfo(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SvceAuthInfo DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = SvceAuthInfo.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out SvceAuthInfo? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = SvceAuthInfo.DecodeValueFrom(decoder);
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
		private SvceAuthInfo(Asn1DerDecoder decoder)
		{
			this.service = decoder.DecodeTlv<GeneralName>();
			this.ident = decoder.DecodeTlv<GeneralName>();
			this.authInfo = decoder.CheckTag(new Asn1Tag(0x4)) ? decoder.DecodeOctetStringTlv() : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class RoleSyntax : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<RoleSyntax>, IAsn1DerDecodableValue<RoleSyntax>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName[]? roleAuthority;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName roleName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public RoleSyntax(GeneralName roleName, GeneralName[]? roleAuthority = default)
		{
			this.roleAuthority = roleAuthority;
			this.roleName = roleName;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<GeneralName>(new Asn1Tag(0xA0000001), this.roleName, (encoder, r) =>
			{
				this.roleName.EncodeTlv(encoder);
			});
			if (this.roleAuthority is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000000), this.roleAuthority, (encoder, r) =>
				{
					r.EncodeTlv(encoder);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static RoleSyntax DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new RoleSyntax(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static RoleSyntax DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = RoleSyntax.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out RoleSyntax? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = RoleSyntax.DecodeValueFrom(decoder);
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
		private RoleSyntax(Asn1DerDecoder decoder)
		{
			this.roleAuthority = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeListTlv<GeneralName>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<GeneralName>()) : default(GeneralName[]);
			this.roleName = decoder.DecodeTaggedValue<GeneralName>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<GeneralName>());
		}
	}

	[FlagsAttribute(), Titanis.Asn1.BitCountAttribute(6), GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
	public enum ClassList : byte
	{
		Unmarked = 0x80,
		Unclassified = 0x40,
		Restricted = 0x20,
		Confidential = 0x10,
		Secret = 0x8,
		TopSecret = 0x4
	}

	[Asn1Sequence()]
	partial class SecurityCategory : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<SecurityCategory>, IAsn1DerDecodableValue<SecurityCategory>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid type;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Any value;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public SecurityCategory(Asn1Oid type, Asn1Any value)
		{
			this.type = type;
			this.value = value;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Asn1Any>(new Asn1Tag(0xA0000001), this.value, (encoder, r) =>
			{
				this.value.EncodeTlv(encoder);
			});
			encoder.EncodeOidTlv(this.type, new Asn1Tag(0x80000000));
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SecurityCategory DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new SecurityCategory(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SecurityCategory DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = SecurityCategory.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out SecurityCategory? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = SecurityCategory.DecodeValueFrom(decoder);
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
		private SecurityCategory(Asn1DerDecoder decoder)
		{
			this.type = decoder.DecodeOidTlv(new Asn1Tag(0x80000000));
			this.value = decoder.DecodeTaggedValue<Asn1Any>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<Asn1Any>());
		}
	}

	[Asn1Sequence()]
	partial class Clearance : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<Clearance>, IAsn1DerDecodableValue<Clearance>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid policyId;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ClassList classList;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal SecurityCategory[]? securityCategories;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Clearance(Asn1Oid policyId, ClassList classList = ClassList.Unclassified, SecurityCategory[]? securityCategories = default)
		{
			this.policyId = policyId;
			this.classList = classList;
			this.securityCategories = securityCategories;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.securityCategories is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000002), this.securityCategories, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			if (this.classList != ClassList.Unclassified)
				encoder.EncodeBitStringTlv((ulong)this.classList, 6, new Asn1Tag(0x80000001));
			encoder.EncodeOidTlv(this.policyId, new Asn1Tag(0x80000000));
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Clearance DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new Clearance(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Clearance DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = Clearance.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out Clearance? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = Clearance.DecodeValueFrom(decoder);
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
		private Clearance(Asn1DerDecoder decoder)
		{
			this.policyId = decoder.DecodeOidTlv(new Asn1Tag(0x80000000));
			this.classList = decoder.CheckTag(new Asn1Tag(0x80000001)) ? (ClassList)decoder.DecodeBitStringTlv<ClassList>(new Asn1Tag(0x80000001)) : ClassList.Unclassified;
			this.securityCategories = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeListTlv<SecurityCategory>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeTlv<SecurityCategory>()) : default(SecurityCategory[]);
		}
	}

	[Asn1Sequence()]
	partial class AAControls : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AAControls>, IAsn1DerDecodableValue<AAControls>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger? pathLenConstraint;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid[]? permittedAttrs;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid[]? excludedAttrs;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal bool permitUnSpecified;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AAControls(System.Numerics.BigInteger? pathLenConstraint = default, Asn1Oid[]? permittedAttrs = default, Asn1Oid[]? excludedAttrs = default, bool permitUnSpecified = true)
		{
			this.pathLenConstraint = pathLenConstraint;
			this.permittedAttrs = permittedAttrs;
			this.excludedAttrs = excludedAttrs;
			this.permitUnSpecified = permitUnSpecified;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.permitUnSpecified != true)
				encoder.EncodeBoolTlv(this.permitUnSpecified);
			if (this.excludedAttrs is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000001), this.excludedAttrs, (encoder, r) =>
				{
					encoder.EncodeOidTlv(r);
				});
			if (this.permittedAttrs is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000000), this.permittedAttrs, (encoder, r) =>
				{
					encoder.EncodeOidTlv(r);
				});
			if (this.pathLenConstraint is not null)
				encoder.EncodeBigIntegerTlv(this.pathLenConstraint.Value);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AAControls DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AAControls(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AAControls DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AAControls.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AAControls? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AAControls.DecodeValueFrom(decoder);
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
		private AAControls(Asn1DerDecoder decoder)
		{
			this.pathLenConstraint = decoder.CheckTag(new Asn1Tag(0x2)) ? decoder.DecodeIntegerTlvAsBigInteger() : default(System.Numerics.BigInteger? );
			this.permittedAttrs = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeListTlv<Asn1Oid>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeOidTlv()) : default(Asn1Oid[]);
			this.excludedAttrs = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeListTlv<Asn1Oid>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOidTlv()) : default(Asn1Oid[]);
			this.permitUnSpecified = decoder.CheckTag(new Asn1Tag(0x1)) ? decoder.DecodeBoolTlv() : true;
		}
	}

	[Asn1Sequence()]
	partial class ACClearAttrs : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<ACClearAttrs>, IAsn1DerDecodableValue<ACClearAttrs>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName acIssuer;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger acSerial;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Attribute[] attrs;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ACClearAttrs(GeneralName acIssuer, System.Numerics.BigInteger acSerial, Attribute[] attrs)
		{
			this.acIssuer = acIssuer;
			this.acSerial = acSerial;
			this.attrs = attrs;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.attrs, (encoder, r) =>
			{
				encoder.EncodeValueTlv(r);
			});
			encoder.EncodeBigIntegerTlv(this.acSerial);
			this.acIssuer.EncodeTlv(encoder);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ACClearAttrs DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new ACClearAttrs(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static ACClearAttrs DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = ACClearAttrs.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out ACClearAttrs? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = ACClearAttrs.DecodeValueFrom(decoder);
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
		private ACClearAttrs(Asn1DerDecoder decoder)
		{
			this.acIssuer = decoder.DecodeTlv<GeneralName>();
			this.acSerial = decoder.DecodeIntegerTlvAsBigInteger();
			this.attrs = decoder.DecodeListTlv<Attribute>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<Attribute>());
		}
	}
}