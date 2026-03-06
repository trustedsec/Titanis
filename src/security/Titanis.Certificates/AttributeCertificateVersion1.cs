namespace AttributeCertificateVersion1
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
	using IssuerSerial = PKIXAttributeCertificate.IssuerSerial;
	using GeneralNames = PKIX1Implicit88.GeneralNames;
	using GeneralName = PKIX1Implicit88.GeneralName;
	using AlgorithmIdentifier = PKIX1Explicit88.AlgorithmIdentifier;
	using AttCertValidityPeriod = PKIXAttributeCertificate.AttCertValidityPeriod;
	using Attribute = PKIX1Explicit88.Attribute;
	using Extensions = PKIX1Explicit88.Extensions;
	using Extension = PKIX1Explicit88.Extension;

	partial class AttributeCertificateVersion1Module
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static byte v1 => 0;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private AttributeCertificateVersion1Module()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private static AttributeCertificateVersion1Module _instance = new AttributeCertificateVersion1Module();
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeCertificateVersion1Module Instance => _instance;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Name => "AttributeCertificateVersion1";

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Oid => "1.2.840.113549.1.9.16.0.15";
	}

	[Asn1Sequence()]
	partial class AttributeCertificateInfoV1 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AttributeCertificateInfoV1>, IAsn1DerDecodableValue<AttributeCertificateInfoV1>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger version;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AttributeCertificateInfoV1_Subject subject;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName[] issuer;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AlgorithmIdentifier signature;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger serialNumber;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AttCertValidityPeriod attCertValidityPeriod;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Attribute[] attributes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString? issuerUniqueID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Extension[]? extensions;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttributeCertificateInfoV1(AttributeCertificateInfoV1_Subject subject, GeneralName[] issuer, AlgorithmIdentifier signature, System.Numerics.BigInteger serialNumber, AttCertValidityPeriod attCertValidityPeriod, Attribute[] attributes, System.Numerics.BigInteger version = default, Asn1BitString? issuerUniqueID = default, Extension[]? extensions = default)
		{
			this.version = version;
			this.subject = subject;
			this.issuer = issuer;
			this.signature = signature;
			this.serialNumber = serialNumber;
			this.attCertValidityPeriod = attCertValidityPeriod;
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
			encoder.EncodeValueTlv(this.attCertValidityPeriod);
			encoder.EncodeBigIntegerTlv(this.serialNumber);
			encoder.EncodeValueTlv(this.signature);
			encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.issuer, (encoder, r) =>
			{
				r.EncodeTlv(encoder);
			});
			this.subject.EncodeTlv(encoder);
			if (this.version != 0)
				encoder.EncodeBigIntegerTlv(this.version);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeCertificateInfoV1 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AttributeCertificateInfoV1(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeCertificateInfoV1 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AttributeCertificateInfoV1.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AttributeCertificateInfoV1? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AttributeCertificateInfoV1.DecodeValueFrom(decoder);
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
		private AttributeCertificateInfoV1(Asn1DerDecoder decoder)
		{
			this.version = decoder.CheckTag(new Asn1Tag(0x2)) ? decoder.DecodeIntegerTlvAsBigInteger() : 0;
			this.subject = decoder.DecodeTlv<AttributeCertificateInfoV1_Subject>();
			this.issuer = decoder.DecodeListTlv<GeneralName>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<GeneralName>());
			this.signature = AlgorithmIdentifier.DecodeTlvFrom(decoder);
			this.serialNumber = decoder.DecodeIntegerTlvAsBigInteger();
			this.attCertValidityPeriod = AttCertValidityPeriod.DecodeTlvFrom(decoder);
			this.attributes = decoder.DecodeListTlv<Attribute>(new Asn1Tag(0x20000010), (encoder) => Attribute.DecodeTlvFrom(decoder));
			this.issuerUniqueID = decoder.CheckTag(new Asn1Tag(0x3)) ? decoder.DecodeBitStringTlv() : default(Asn1BitString? );
			this.extensions = decoder.CheckTag(new Asn1Tag(0x20000010)) ? decoder.DecodeListTlv<Extension>(new Asn1Tag(0x20000010), (encoder) => Extension.DecodeTlvFrom(decoder)) : default(Extension[]);
		}
	}

	[Asn1Choice()]
	partial class AttributeCertificateInfoV1_Subject : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<AttributeCertificateInfoV1_Subject>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public IssuerSerial BaseCertificateID
		{
			get => this.baseCertificateID;
			set
			{
				this.baseCertificateID = value;
				this._choiceTag = ChoiceIndex.BaseCertificateID;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private IssuerSerial? baseCertificateID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public GeneralName[] SubjectName
		{
			get => this.subjectName;
			set
			{
				this.subjectName = value;
				this._choiceTag = ChoiceIndex.SubjectName;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private GeneralName[]? subjectName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttributeCertificateInfoV1_Subject()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.SubjectName:
					Debug.Assert(this.subjectName is not null);
					encoder.EncodeExplicitTlv<GeneralName[]>(new Asn1Tag(0xA0000001), this.subjectName, (encoder, r) =>
					{
						encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.subjectName, (encoder, r) =>
						{
							r.EncodeTlv(encoder);
						});
					});
					break;
				case ChoiceIndex.BaseCertificateID:
					Debug.Assert(this.baseCertificateID is not null);
					encoder.EncodeExplicitTlv<IssuerSerial>(new Asn1Tag(0xA0000000), this.baseCertificateID, (encoder, r) =>
					{
						encoder.EncodeValueTlv(this.baseCertificateID);
					});
					break;
				default:
					throw new InvalidOperationException("The object of type  has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeCertificateInfoV1_Subject DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!AttributeCertificateInfoV1_Subject.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AttributeCertificateInfoV1_Subject? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				instance = new AttributeCertificateInfoV1_Subject()
				{
					_choiceTag = ChoiceIndex.BaseCertificateID,
					baseCertificateID = decoder.DecodeTaggedValue<IssuerSerial>(new Asn1Tag(0xA0000000), (encoder) => IssuerSerial.DecodeTlvFrom(decoder))
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000001)))
				instance = new AttributeCertificateInfoV1_Subject()
				{
					_choiceTag = ChoiceIndex.SubjectName,
					subjectName = decoder.DecodeTaggedValue<GeneralName[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeListTlv<GeneralName>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeTlv<GeneralName>()))
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
			BaseCertificateID = 0xA0000000,
			SubjectName = 0xA0000001
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class AttributeCertificateV1 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AttributeCertificateV1>, IAsn1DerDecodableValue<AttributeCertificateV1>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AttributeCertificateInfoV1 acInfo;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal AlgorithmIdentifier signatureAlgorithm;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1BitString signature;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AttributeCertificateV1(AttributeCertificateInfoV1 acInfo, AlgorithmIdentifier signatureAlgorithm, Asn1BitString signature)
		{
			this.acInfo = acInfo;
			this.signatureAlgorithm = signatureAlgorithm;
			this.signature = signature;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBitStringTlv(this.signature);
			encoder.EncodeValueTlv(this.signatureAlgorithm);
			encoder.EncodeValueTlv(this.acInfo);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeCertificateV1 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AttributeCertificateV1(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AttributeCertificateV1 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AttributeCertificateV1.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AttributeCertificateV1? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AttributeCertificateV1.DecodeValueFrom(decoder);
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
		private AttributeCertificateV1(Asn1DerDecoder decoder)
		{
			this.acInfo = AttributeCertificateInfoV1.DecodeTlvFrom(decoder);
			this.signatureAlgorithm = AlgorithmIdentifier.DecodeTlvFrom(decoder);
			this.signature = decoder.DecodeBitStringTlv();
		}
	}
}