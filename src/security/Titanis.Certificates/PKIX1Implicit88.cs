namespace PKIX1Implicit88
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
	using ORAddress = PKIX1Explicit88.ORAddress;
	using Name = PKIX1Explicit88.Name;
	using DirectoryString = PKIX1Explicit88.DirectoryString;
	using Attribute = PKIX1Explicit88.Attribute;

	partial class PKIX1Implicit88Module
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce => new Asn1Oid("2.5.29");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_authorityKeyIdentifier => new Asn1Oid("2.5.29.35");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_subjectKeyIdentifier => new Asn1Oid("2.5.29.14");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_keyUsage => new Asn1Oid("2.5.29.15");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_privateKeyUsagePeriod => new Asn1Oid("2.5.29.16");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_certificatePolicies => new Asn1Oid("2.5.29.32");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid anyPolicy => new Asn1Oid("2.5.29.32.0");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_policyMappings => new Asn1Oid("2.5.29.33");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_subjectAltName => new Asn1Oid("2.5.29.17");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_issuerAltName => new Asn1Oid("2.5.29.18");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_subjectDirectoryAttributes => new Asn1Oid("2.5.29.9");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_basicConstraints => new Asn1Oid("2.5.29.19");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_nameConstraints => new Asn1Oid("2.5.29.30");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_policyConstraints => new Asn1Oid("2.5.29.36");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_cRLDistributionPoints => new Asn1Oid("2.5.29.31");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_extKeyUsage => new Asn1Oid("2.5.29.37");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid anyExtendedKeyUsage => new Asn1Oid("2.5.29.37.0");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_kp_serverAuth => new Asn1Oid("1.3.6.1.5.5.7.3.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_kp_clientAuth => new Asn1Oid("1.3.6.1.5.5.7.3.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_kp_codeSigning => new Asn1Oid("1.3.6.1.5.5.7.3.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_kp_emailProtection => new Asn1Oid("1.3.6.1.5.5.7.3.4");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_kp_timeStamping => new Asn1Oid("1.3.6.1.5.5.7.3.8");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_kp_OCSPSigning => new Asn1Oid("1.3.6.1.5.5.7.3.9");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_inhibitAnyPolicy => new Asn1Oid("2.5.29.54");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_freshestCRL => new Asn1Oid("2.5.29.46");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pe_authorityInfoAccess => new Asn1Oid("1.3.6.1.5.5.7.1.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_pe_subjectInfoAccess => new Asn1Oid("1.3.6.1.5.5.7.1.11");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_cRLNumber => new Asn1Oid("2.5.29.20");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_issuingDistributionPoint => new Asn1Oid("2.5.29.28");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_deltaCRLIndicator => new Asn1Oid("2.5.29.27");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_cRLReasons => new Asn1Oid("2.5.29.21");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_certificateIssuer => new Asn1Oid("2.5.29.29");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_holdInstructionCode => new Asn1Oid("2.5.29.23");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid holdInstruction => new Asn1Oid("2.2.840.10040.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_holdinstruction_none => new Asn1Oid("2.2.840.10040.2.1");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_holdinstruction_callissuer => new Asn1Oid("2.2.840.10040.2.2");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_holdinstruction_reject => new Asn1Oid("2.2.840.10040.2.3");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Titanis.Asn1.Asn1Oid id_ce_invalidityDate => new Asn1Oid("2.5.29.24");

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit digitalSignature => new Asn1NamedBit("digitalSignature", 0);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit nonRepudiation => new Asn1NamedBit("nonRepudiation", 1);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit keyEncipherment => new Asn1NamedBit("keyEncipherment", 2);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit dataEncipherment => new Asn1NamedBit("dataEncipherment", 3);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit keyAgreement => new Asn1NamedBit("keyAgreement", 4);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit keyCertSign => new Asn1NamedBit("keyCertSign", 5);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit cRLSign => new Asn1NamedBit("cRLSign", 6);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit encipherOnly => new Asn1NamedBit("encipherOnly", 7);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit decipherOnly => new Asn1NamedBit("decipherOnly", 8);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit unused => new Asn1NamedBit("unused", 0);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit keyCompromise => new Asn1NamedBit("keyCompromise", 1);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit cACompromise => new Asn1NamedBit("cACompromise", 2);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit affiliationChanged => new Asn1NamedBit("affiliationChanged", 3);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit superseded => new Asn1NamedBit("superseded", 4);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit cessationOfOperation => new Asn1NamedBit("cessationOfOperation", 5);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit certificateHold => new Asn1NamedBit("certificateHold", 6);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit privilegeWithdrawn => new Asn1NamedBit("privilegeWithdrawn", 7);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1NamedBit aACompromise => new Asn1NamedBit("aACompromise", 8);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PKIX1Implicit88Module()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private static PKIX1Implicit88Module _instance = new PKIX1Implicit88Module();
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PKIX1Implicit88Module Instance => _instance;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Name => "PKIX1Implicit88";

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Oid => "1.3.6.1.5.5.7.0.19";
	}

	[Asn1Sequence()]
	partial class AnotherName : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AnotherName>, IAsn1DerDecodableValue<AnotherName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid type_id;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Any value;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AnotherName(Asn1Oid type_id, Asn1Any value)
		{
			this.type_id = type_id;
			this.value = value;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<Asn1Any>(new Asn1Tag(0xA0000000), this.value, (encoder, r) =>
			{
				this.value.EncodeTlv(encoder);
			});
			encoder.EncodeOidTlv(this.type_id);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AnotherName DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AnotherName(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AnotherName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AnotherName.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AnotherName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AnotherName.DecodeValueFrom(decoder);
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
		private AnotherName(Asn1DerDecoder decoder)
		{
			this.type_id = decoder.DecodeOidTlv();
			this.value = decoder.DecodeTaggedValue<Asn1Any>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<Asn1Any>());
		}
	}

	[Asn1Sequence()]
	partial class EDIPartyName : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<EDIPartyName>, IAsn1DerDecodableValue<EDIPartyName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal DirectoryString? nameAssigner;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal DirectoryString partyName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EDIPartyName(DirectoryString partyName, DirectoryString? nameAssigner = default)
		{
			this.nameAssigner = nameAssigner;
			this.partyName = partyName;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeExplicitTlv<DirectoryString>(new Asn1Tag(0xA0000001), this.partyName, (encoder, r) =>
			{
				this.partyName.EncodeTlv(encoder);
			});
			if (this.nameAssigner is not null)
				encoder.EncodeExplicitTlv<DirectoryString>(new Asn1Tag(0xA0000000), this.nameAssigner, (encoder, r) =>
				{
					this.nameAssigner.EncodeTlv(encoder);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EDIPartyName DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new EDIPartyName(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static EDIPartyName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = EDIPartyName.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out EDIPartyName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = EDIPartyName.DecodeValueFrom(decoder);
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
		private EDIPartyName(Asn1DerDecoder decoder)
		{
			this.nameAssigner = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<DirectoryString>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<DirectoryString>()) : default(DirectoryString);
			this.partyName = decoder.DecodeTaggedValue<DirectoryString>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<DirectoryString>());
		}
	}

	[Asn1Choice()]
	partial class GeneralName : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<GeneralName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AnotherName OtherName
		{
			get => this.otherName;
			set
			{
				this.otherName = value;
				this._choiceTag = ChoiceIndex.OtherName;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private AnotherName? otherName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public IA5String Rfc822Name
		{
			get => this.rfc822Name.Value;
			set
			{
				this.rfc822Name = value;
				this._choiceTag = ChoiceIndex.Rfc822Name;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private IA5String? rfc822Name;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public IA5String DNSName
		{
			get => this.dNSName.Value;
			set
			{
				this.dNSName = value;
				this._choiceTag = ChoiceIndex.DNSName;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private IA5String? dNSName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public ORAddress X400Address
		{
			get => this.x400Address;
			set
			{
				this.x400Address = value;
				this._choiceTag = ChoiceIndex.X400Address;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ORAddress? x400Address;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Name DirectoryName
		{
			get => this.directoryName;
			set
			{
				this.directoryName = value;
				this._choiceTag = ChoiceIndex.DirectoryName;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Name? directoryName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public EDIPartyName EdiPartyName
		{
			get => this.ediPartyName;
			set
			{
				this.ediPartyName = value;
				this._choiceTag = ChoiceIndex.EdiPartyName;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private EDIPartyName? ediPartyName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public IA5String UniformResourceIdentifier
		{
			get => this.uniformResourceIdentifier.Value;
			set
			{
				this.uniformResourceIdentifier = value;
				this._choiceTag = ChoiceIndex.UniformResourceIdentifier;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private IA5String? uniformResourceIdentifier;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Byte[] IPAddress
		{
			get => this.iPAddress;
			set
			{
				this.iPAddress = value;
				this._choiceTag = ChoiceIndex.IPAddress;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Byte[]? iPAddress;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Oid RegisteredID
		{
			get => this.registeredID.Value;
			set
			{
				this.registeredID = value;
				this._choiceTag = ChoiceIndex.RegisteredID;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Asn1Oid? registeredID;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public GeneralName()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.RegisteredID:
					Debug.Assert(this.registeredID is not null);
					encoder.EncodeOidTlv(this.registeredID.Value);
					break;
				case ChoiceIndex.IPAddress:
					Debug.Assert(this.iPAddress is not null);
					encoder.EncodeOctetStringTlv(this.iPAddress);
					break;
				case ChoiceIndex.UniformResourceIdentifier:
					Debug.Assert(this.uniformResourceIdentifier is not null);
					encoder.EncodeStringTlv(this.uniformResourceIdentifier.Value);
					break;
				case ChoiceIndex.EdiPartyName:
					Debug.Assert(this.ediPartyName is not null);
					encoder.EncodeValueTlv(this.ediPartyName, new Asn1Tag(0xA0000005));
					break;
				case ChoiceIndex.DirectoryName:
					Debug.Assert(this.directoryName is not null);
					encoder.EncodeExplicitTlv<Name>(new Asn1Tag(0xA0000004), this.directoryName, (encoder, r) =>
					{
						this.directoryName.EncodeTlv(encoder);
					});
					break;
				case ChoiceIndex.X400Address:
					Debug.Assert(this.x400Address is not null);
					encoder.EncodeValueTlv(this.x400Address, new Asn1Tag(0xA0000003));
					break;
				case ChoiceIndex.DNSName:
					Debug.Assert(this.dNSName is not null);
					encoder.EncodeStringTlv(this.dNSName.Value);
					break;
				case ChoiceIndex.Rfc822Name:
					Debug.Assert(this.rfc822Name is not null);
					encoder.EncodeStringTlv(this.rfc822Name.Value);
					break;
				case ChoiceIndex.OtherName:
					Debug.Assert(this.otherName is not null);
					encoder.EncodeValueTlv(this.otherName, new Asn1Tag(0xA0000000));
					break;
				default:
					throw new InvalidOperationException("The object of type GeneralName has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static GeneralName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!GeneralName.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out GeneralName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				instance = new GeneralName()
				{
					_choiceTag = ChoiceIndex.OtherName,
					otherName = decoder.DecodeTaggedValue<AnotherName>(new Asn1Tag(0xA0000000))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x80000001)))
				instance = new GeneralName()
				{
					_choiceTag = ChoiceIndex.Rfc822Name,
					rfc822Name = decoder.DecodeStringTlv<IA5String>(new Asn1Tag(0x80000001))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x80000002)))
				instance = new GeneralName()
				{
					_choiceTag = ChoiceIndex.DNSName,
					dNSName = decoder.DecodeStringTlv<IA5String>(new Asn1Tag(0x80000002))
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000003)))
				instance = new GeneralName()
				{
					_choiceTag = ChoiceIndex.X400Address,
					x400Address = ORAddress.DecodeTlvFrom(decoder)
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000004)))
				instance = new GeneralName()
				{
					_choiceTag = ChoiceIndex.DirectoryName,
					directoryName = decoder.DecodeTaggedValue<Name>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeTlv<Name>())
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000005)))
				instance = new GeneralName()
				{
					_choiceTag = ChoiceIndex.EdiPartyName,
					ediPartyName = EDIPartyName.DecodeTlvFrom(decoder)
				};
			else if (decoder.CheckTag(new Asn1Tag(0x80000006)))
				instance = new GeneralName()
				{
					_choiceTag = ChoiceIndex.UniformResourceIdentifier,
					uniformResourceIdentifier = decoder.DecodeStringTlv<IA5String>(new Asn1Tag(0x80000006))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x80000007)))
				instance = new GeneralName()
				{
					_choiceTag = ChoiceIndex.IPAddress,
					iPAddress = decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000007))
				};
			else if (decoder.CheckTag(new Asn1Tag(0x80000008)))
				instance = new GeneralName()
				{
					_choiceTag = ChoiceIndex.RegisteredID,
					registeredID = decoder.DecodeOidTlv(new Asn1Tag(0x80000008))
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
			OtherName = 0xA0000000,
			Rfc822Name = 0x80000001,
			DNSName = 0x80000002,
			X400Address = 0xA0000003,
			DirectoryName = 0xA0000004,
			EdiPartyName = 0xA0000005,
			UniformResourceIdentifier = 0x80000006,
			IPAddress = 0x80000007,
			RegisteredID = 0x80000008
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class AuthorityKeyIdentifier : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AuthorityKeyIdentifier>, IAsn1DerDecodableValue<AuthorityKeyIdentifier>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? keyIdentifier;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName[]? authorityCertIssuer;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger? authorityCertSerialNumber;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AuthorityKeyIdentifier(Byte[]? keyIdentifier = default, GeneralName[]? authorityCertIssuer = default, System.Numerics.BigInteger? authorityCertSerialNumber = default)
		{
			this.keyIdentifier = keyIdentifier;
			this.authorityCertIssuer = authorityCertIssuer;
			this.authorityCertSerialNumber = authorityCertSerialNumber;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.authorityCertSerialNumber is not null)
				encoder.EncodeBigIntegerTlv(this.authorityCertSerialNumber.Value);
			if (this.authorityCertIssuer is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000001), this.authorityCertIssuer, (encoder, r) =>
				{
					r.EncodeTlv(encoder);
				});
			if (this.keyIdentifier is not null)
				encoder.EncodeOctetStringTlv(this.keyIdentifier);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AuthorityKeyIdentifier DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AuthorityKeyIdentifier(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AuthorityKeyIdentifier DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AuthorityKeyIdentifier.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AuthorityKeyIdentifier? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AuthorityKeyIdentifier.DecodeValueFrom(decoder);
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
		private AuthorityKeyIdentifier(Asn1DerDecoder decoder)
		{
			this.keyIdentifier = decoder.CheckTag(new Asn1Tag(0x80000000)) ? decoder.DecodeOctetStringTlv(new Asn1Tag(0x80000000)) : default(Byte[]);
			this.authorityCertIssuer = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeListTlv<GeneralName>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeTlv<GeneralName>()) : default(GeneralName[]);
			this.authorityCertSerialNumber = decoder.CheckTag(new Asn1Tag(0x80000002)) ? decoder.DecodeIntegerTlvAsBigInteger(new Asn1Tag(0x80000002)) : default(System.Numerics.BigInteger?);
		}
	}

	[FlagsAttribute(), GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
	public enum KeyUsage : ushort
	{
		DigitalSignature = 0x8000,
		NonRepudiation = 0x4000,
		KeyEncipherment = 0x2000,
		DataEncipherment = 0x1000,
		KeyAgreement = 0x800,
		KeyCertSign = 0x400,
		CRLSign = 0x200,
		EncipherOnly = 0x100,
		DecipherOnly = 0x80
	}

	[Asn1Sequence()]
	partial class PrivateKeyUsagePeriod : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PrivateKeyUsagePeriod>, IAsn1DerDecodableValue<PrivateKeyUsagePeriod>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? notBefore;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralizedTime? notAfter;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PrivateKeyUsagePeriod(GeneralizedTime? notBefore = default, GeneralizedTime? notAfter = default)
		{
			this.notBefore = notBefore;
			this.notAfter = notAfter;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.notAfter is not null)
				encoder.EncodeDateTimeTlv(this.notAfter.Value);
			if (this.notBefore is not null)
				encoder.EncodeDateTimeTlv(this.notBefore.Value);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PrivateKeyUsagePeriod DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PrivateKeyUsagePeriod(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PrivateKeyUsagePeriod DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PrivateKeyUsagePeriod.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PrivateKeyUsagePeriod? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PrivateKeyUsagePeriod.DecodeValueFrom(decoder);
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
		private PrivateKeyUsagePeriod(Asn1DerDecoder decoder)
		{
			this.notBefore = decoder.CheckTag(new Asn1Tag(0x80000000)) ? decoder.DecodeDateTimeTlv(new Asn1Tag(0x80000000)) : default(GeneralizedTime?);
			this.notAfter = decoder.CheckTag(new Asn1Tag(0x80000001)) ? decoder.DecodeDateTimeTlv(new Asn1Tag(0x80000001)) : default(GeneralizedTime?);
		}
	}

	[Asn1Sequence()]
	partial class PolicyQualifierInfo : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PolicyQualifierInfo>, IAsn1DerDecodableValue<PolicyQualifierInfo>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid policyQualifierId;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Any qualifier;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PolicyQualifierInfo(Asn1Oid policyQualifierId, Asn1Any qualifier)
		{
			this.policyQualifierId = policyQualifierId;
			this.qualifier = qualifier;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			this.qualifier.EncodeTlv(encoder);
			encoder.EncodeOidTlv(this.policyQualifierId);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PolicyQualifierInfo DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PolicyQualifierInfo(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PolicyQualifierInfo DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PolicyQualifierInfo.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PolicyQualifierInfo? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PolicyQualifierInfo.DecodeValueFrom(decoder);
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
		private PolicyQualifierInfo(Asn1DerDecoder decoder)
		{
			this.policyQualifierId = decoder.DecodeOidTlv();
			this.qualifier = decoder.DecodeTlv<Asn1Any>();
		}
	}

	[Asn1Sequence()]
	partial class PolicyInformation : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PolicyInformation>, IAsn1DerDecodableValue<PolicyInformation>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid policyIdentifier;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal PolicyQualifierInfo[]? policyQualifiers;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PolicyInformation(Asn1Oid policyIdentifier, PolicyQualifierInfo[]? policyQualifiers = default)
		{
			this.policyIdentifier = policyIdentifier;
			this.policyQualifiers = policyQualifiers;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.policyQualifiers is not null)
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.policyQualifiers, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			encoder.EncodeOidTlv(this.policyIdentifier);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PolicyInformation DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PolicyInformation(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PolicyInformation DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PolicyInformation.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PolicyInformation? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PolicyInformation.DecodeValueFrom(decoder);
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
		private PolicyInformation(Asn1DerDecoder decoder)
		{
			this.policyIdentifier = decoder.DecodeOidTlv();
			this.policyQualifiers = decoder.CheckTag(new Asn1Tag(0x20000010)) ? decoder.DecodeListTlv<PolicyQualifierInfo>(new Asn1Tag(0x20000010), (encoder) => PolicyQualifierInfo.DecodeTlvFrom(decoder)) : default(PolicyQualifierInfo[]);
		}
	}

	[Asn1Choice()]
	partial class DisplayText : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<DisplayText>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public IA5String Ia5String
		{
			get => this.ia5String.Value;
			set
			{
				this.ia5String = value;
				this._choiceTag = ChoiceIndex.Ia5String;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private IA5String? ia5String;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Iso646String VisibleString
		{
			get => this.visibleString.Value;
			set
			{
				this.visibleString = value;
				this._choiceTag = ChoiceIndex.VisibleString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private Iso646String? visibleString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BMPString BmpString
		{
			get => this.bmpString.Value;
			set
			{
				this.bmpString = value;
				this._choiceTag = ChoiceIndex.BmpString;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private BMPString? bmpString;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public String Utf8String
		{
			get => this.utf8String;
			set
			{
				this.utf8String = value;
				this._choiceTag = ChoiceIndex.Utf8String;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private String? utf8String;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public DisplayText()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.Utf8String:
					Debug.Assert(this.utf8String is not null);
					encoder.EncodeUtf8StringTlv(this.utf8String);
					break;
				case ChoiceIndex.BmpString:
					Debug.Assert(this.bmpString is not null);
					encoder.EncodeStringTlv(this.bmpString.Value);
					break;
				case ChoiceIndex.VisibleString:
					Debug.Assert(this.visibleString is not null);
					encoder.EncodeStringTlv(this.visibleString.Value);
					break;
				case ChoiceIndex.Ia5String:
					Debug.Assert(this.ia5String is not null);
					encoder.EncodeStringTlv(this.ia5String.Value);
					break;
				default:
					throw new InvalidOperationException("The object of type DisplayText has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DisplayText DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!DisplayText.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out DisplayText? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x16)))
				instance = new DisplayText()
				{
					_choiceTag = ChoiceIndex.Ia5String,
					ia5String = decoder.DecodeStringTlv<IA5String>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1A)))
				instance = new DisplayText()
				{
					_choiceTag = ChoiceIndex.VisibleString,
					visibleString = decoder.DecodeStringTlv<Iso646String>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0x1E)))
				instance = new DisplayText()
				{
					_choiceTag = ChoiceIndex.BmpString,
					bmpString = decoder.DecodeStringTlv<BMPString>()
				};
			else if (decoder.CheckTag(new Asn1Tag(0xC)))
				instance = new DisplayText()
				{
					_choiceTag = ChoiceIndex.Utf8String,
					utf8String = decoder.DecodeUtf8StringTlv()
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
			Ia5String = 22U,
			VisibleString = 26U,
			BmpString = 30U,
			Utf8String = 12U
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class NoticeReference : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<NoticeReference>, IAsn1DerDecodableValue<NoticeReference>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal DisplayText organization;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger[] noticeNumbers;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NoticeReference(DisplayText organization, System.Numerics.BigInteger[] noticeNumbers)
		{
			this.organization = organization;
			this.noticeNumbers = noticeNumbers;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.noticeNumbers, (encoder, r) =>
			{
				encoder.EncodeBigIntegerTlv(r);
			});
			this.organization.EncodeTlv(encoder);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NoticeReference DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new NoticeReference(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NoticeReference DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = NoticeReference.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out NoticeReference? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = NoticeReference.DecodeValueFrom(decoder);
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
		private NoticeReference(Asn1DerDecoder decoder)
		{
			this.organization = decoder.DecodeTlv<DisplayText>();
			this.noticeNumbers = decoder.DecodeListTlv<System.Numerics.BigInteger>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeIntegerTlvAsBigInteger());
		}
	}

	[Asn1Sequence()]
	partial class UserNotice : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<UserNotice>, IAsn1DerDecodableValue<UserNotice>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal NoticeReference? noticeRef;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal DisplayText? explicitText;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public UserNotice(NoticeReference? noticeRef = default, DisplayText? explicitText = default)
		{
			this.noticeRef = noticeRef;
			this.explicitText = explicitText;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.explicitText is not null)
				this.explicitText.EncodeTlv(encoder);
			if (this.noticeRef is not null)
				encoder.EncodeValueTlv(this.noticeRef);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static UserNotice DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new UserNotice(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static UserNotice DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = UserNotice.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out UserNotice? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = UserNotice.DecodeValueFrom(decoder);
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
		private UserNotice(Asn1DerDecoder decoder)
		{
			this.noticeRef = decoder.CheckTag(new Asn1Tag(0x20000010)) ? NoticeReference.DecodeTlvFrom(decoder) : default(NoticeReference);
			this.explicitText = decoder.TryDecodeTlv<DisplayText>(out this.explicitText) ? this.explicitText : default(DisplayText);
		}
	}

	[Asn1Sequence()]
	partial class PolicyMappings_Element : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PolicyMappings_Element>, IAsn1DerDecodableValue<PolicyMappings_Element>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid issuerDomainPolicy;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid subjectDomainPolicy;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PolicyMappings_Element(Asn1Oid issuerDomainPolicy, Asn1Oid subjectDomainPolicy)
		{
			this.issuerDomainPolicy = issuerDomainPolicy;
			this.subjectDomainPolicy = subjectDomainPolicy;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeOidTlv(this.subjectDomainPolicy);
			encoder.EncodeOidTlv(this.issuerDomainPolicy);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PolicyMappings_Element DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PolicyMappings_Element(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PolicyMappings_Element DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PolicyMappings_Element.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PolicyMappings_Element? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PolicyMappings_Element.DecodeValueFrom(decoder);
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
		private PolicyMappings_Element(Asn1DerDecoder decoder)
		{
			this.issuerDomainPolicy = decoder.DecodeOidTlv();
			this.subjectDomainPolicy = decoder.DecodeOidTlv();
		}
	}

	[Asn1Sequence()]
	partial class BasicConstraints : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<BasicConstraints>, IAsn1DerDecodableValue<BasicConstraints>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal bool cA;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger? pathLenConstraint;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public BasicConstraints(bool cA = false, System.Numerics.BigInteger? pathLenConstraint = default)
		{
			this.cA = cA;
			this.pathLenConstraint = pathLenConstraint;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.pathLenConstraint is not null)
				encoder.EncodeBigIntegerTlv(this.pathLenConstraint.Value);
			if (this.cA != false)
				encoder.EncodeBoolTlv(this.cA);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BasicConstraints DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new BasicConstraints(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static BasicConstraints DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = BasicConstraints.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out BasicConstraints? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = BasicConstraints.DecodeValueFrom(decoder);
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
		private BasicConstraints(Asn1DerDecoder decoder)
		{
			this.cA = decoder.CheckTag(new Asn1Tag(0x1)) ? decoder.DecodeBoolTlv() : false;
			this.pathLenConstraint = decoder.CheckTag(new Asn1Tag(0x2)) ? decoder.DecodeIntegerTlvAsBigInteger() : default(System.Numerics.BigInteger?);
		}
	}

	[Asn1Sequence()]
	partial class GeneralSubtree : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<GeneralSubtree>, IAsn1DerDecodableValue<GeneralSubtree>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName @base;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger minimum;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger? maximum;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public GeneralSubtree(GeneralName @base, System.Numerics.BigInteger minimum = default, System.Numerics.BigInteger? maximum = default)
		{
			this.@base = @base;
			this.minimum = minimum;
			this.maximum = maximum;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.maximum is not null)
				encoder.EncodeBigIntegerTlv(this.maximum.Value);
			if (this.minimum != 0)
				encoder.EncodeBigIntegerTlv(this.minimum);
			this.@base.EncodeTlv(encoder);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static GeneralSubtree DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new GeneralSubtree(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static GeneralSubtree DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = GeneralSubtree.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out GeneralSubtree? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = GeneralSubtree.DecodeValueFrom(decoder);
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
		private GeneralSubtree(Asn1DerDecoder decoder)
		{
			this.@base = decoder.DecodeTlv<GeneralName>();
			this.minimum = decoder.CheckTag(new Asn1Tag(0x80000000)) ? decoder.DecodeIntegerTlvAsBigInteger(new Asn1Tag(0x80000000)) : 0;
			this.maximum = decoder.CheckTag(new Asn1Tag(0x80000001)) ? decoder.DecodeIntegerTlvAsBigInteger(new Asn1Tag(0x80000001)) : default(System.Numerics.BigInteger?);
		}
	}

	[Asn1Sequence()]
	partial class NameConstraints : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<NameConstraints>, IAsn1DerDecodableValue<NameConstraints>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralSubtree[]? permittedSubtrees;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralSubtree[]? excludedSubtrees;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NameConstraints(GeneralSubtree[]? permittedSubtrees = default, GeneralSubtree[]? excludedSubtrees = default)
		{
			this.permittedSubtrees = permittedSubtrees;
			this.excludedSubtrees = excludedSubtrees;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.excludedSubtrees is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000001), this.excludedSubtrees, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
			if (this.permittedSubtrees is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000000), this.permittedSubtrees, (encoder, r) =>
				{
					encoder.EncodeValueTlv(r);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NameConstraints DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new NameConstraints(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NameConstraints DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = NameConstraints.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out NameConstraints? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = NameConstraints.DecodeValueFrom(decoder);
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
		private NameConstraints(Asn1DerDecoder decoder)
		{
			this.permittedSubtrees = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeListTlv<GeneralSubtree>(new Asn1Tag(0xA0000000), (encoder) => GeneralSubtree.DecodeTlvFrom(decoder)) : default(GeneralSubtree[]);
			this.excludedSubtrees = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeListTlv<GeneralSubtree>(new Asn1Tag(0xA0000001), (encoder) => GeneralSubtree.DecodeTlvFrom(decoder)) : default(GeneralSubtree[]);
		}
	}

	[Asn1Sequence()]
	partial class PolicyConstraints : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<PolicyConstraints>, IAsn1DerDecodableValue<PolicyConstraints>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger? requireExplicitPolicy;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal System.Numerics.BigInteger? inhibitPolicyMapping;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PolicyConstraints(System.Numerics.BigInteger? requireExplicitPolicy = default, System.Numerics.BigInteger? inhibitPolicyMapping = default)
		{
			this.requireExplicitPolicy = requireExplicitPolicy;
			this.inhibitPolicyMapping = inhibitPolicyMapping;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.inhibitPolicyMapping is not null)
				encoder.EncodeBigIntegerTlv(this.inhibitPolicyMapping.Value);
			if (this.requireExplicitPolicy is not null)
				encoder.EncodeBigIntegerTlv(this.requireExplicitPolicy.Value);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PolicyConstraints DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new PolicyConstraints(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static PolicyConstraints DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = PolicyConstraints.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out PolicyConstraints? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = PolicyConstraints.DecodeValueFrom(decoder);
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
		private PolicyConstraints(Asn1DerDecoder decoder)
		{
			this.requireExplicitPolicy = decoder.CheckTag(new Asn1Tag(0x80000000)) ? decoder.DecodeIntegerTlvAsBigInteger(new Asn1Tag(0x80000000)) : default(System.Numerics.BigInteger?);
			this.inhibitPolicyMapping = decoder.CheckTag(new Asn1Tag(0x80000001)) ? decoder.DecodeIntegerTlvAsBigInteger(new Asn1Tag(0x80000001)) : default(System.Numerics.BigInteger?);
		}
	}

	[Asn1Choice()]
	partial class DistributionPointName : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<DistributionPointName>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public GeneralName[] FullName
		{
			get => this.fullName;
			set
			{
				this.fullName = value;
				this._choiceTag = ChoiceIndex.FullName;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private GeneralName[]? fullName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public PKIX1Explicit88.AttributeTypeAndValue[] NameRelativeToCRLIssuer
		{
			get => this.nameRelativeToCRLIssuer;
			set
			{
				this.nameRelativeToCRLIssuer = value;
				this._choiceTag = ChoiceIndex.NameRelativeToCRLIssuer;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private PKIX1Explicit88.AttributeTypeAndValue[]? nameRelativeToCRLIssuer;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public DistributionPointName()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.NameRelativeToCRLIssuer:
					Debug.Assert(this.nameRelativeToCRLIssuer is not null);
					encoder.EncodeListTlv(new Asn1Tag(0xA0000001), this.nameRelativeToCRLIssuer, (encoder, r) =>
					{
						encoder.EncodeValueTlv(r);
					});
					break;
				case ChoiceIndex.FullName:
					Debug.Assert(this.fullName is not null);
					encoder.EncodeListTlv(new Asn1Tag(0xA0000000), this.fullName, (encoder, r) =>
					{
						r.EncodeTlv(encoder);
					});
					break;
				default:
					throw new InvalidOperationException("The object of type DistributionPointName has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DistributionPointName DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!DistributionPointName.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out DistributionPointName? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				instance = new DistributionPointName()
				{
					_choiceTag = ChoiceIndex.FullName,
					fullName = decoder.DecodeListTlv<GeneralName>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<GeneralName>())
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000001)))
				instance = new DistributionPointName()
				{
					_choiceTag = ChoiceIndex.NameRelativeToCRLIssuer,
					nameRelativeToCRLIssuer = decoder.DecodeListTlv<PKIX1Explicit88.AttributeTypeAndValue>(new Asn1Tag(0xA0000001), (encoder) => PKIX1Explicit88.AttributeTypeAndValue.DecodeTlvFrom(decoder))
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
			FullName = 0xA0000000,
			NameRelativeToCRLIssuer = 0xA0000001
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[FlagsAttribute(), GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
	public enum ReasonFlags : ushort
	{
		Unused = 0x8000,
		KeyCompromise = 0x4000,
		CACompromise = 0x2000,
		AffiliationChanged = 0x1000,
		Superseded = 0x800,
		CessationOfOperation = 0x400,
		CertificateHold = 0x200,
		PrivilegeWithdrawn = 0x100,
		AACompromise = 0x80
	}

	[Asn1Sequence()]
	partial class DistributionPoint : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<DistributionPoint>, IAsn1DerDecodableValue<DistributionPoint>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal DistributionPointName? distributionPoint;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ReasonFlags? reasons;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName[]? cRLIssuer;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public DistributionPoint(DistributionPointName? distributionPoint = default, ReasonFlags? reasons = default, GeneralName[]? cRLIssuer = default)
		{
			this.distributionPoint = distributionPoint;
			this.reasons = reasons;
			this.cRLIssuer = cRLIssuer;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.cRLIssuer is not null)
				encoder.EncodeListTlv(new Asn1Tag(0xA0000002), this.cRLIssuer, (encoder, r) =>
				{
					r.EncodeTlv(encoder);
				});
			if (this.reasons is not null)
				encoder.EncodeBitStringTlv((ulong)this.reasons.Value, 9, new Asn1Tag(0x80000001));
			if (this.distributionPoint is not null)
				encoder.EncodeExplicitTlv<DistributionPointName>(new Asn1Tag(0xA0000000), this.distributionPoint, (encoder, r) =>
				{
					this.distributionPoint.EncodeTlv(encoder);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DistributionPoint DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new DistributionPoint(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static DistributionPoint DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = DistributionPoint.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out DistributionPoint? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = DistributionPoint.DecodeValueFrom(decoder);
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
		private DistributionPoint(Asn1DerDecoder decoder)
		{
			this.distributionPoint = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<DistributionPointName>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<DistributionPointName>()) : default(DistributionPointName);
			this.reasons = decoder.CheckTag(new Asn1Tag(0x80000001)) ? (ReasonFlags)decoder.DecodeBitStringTlv(new Asn1Tag(0x80000001)).ToUInt64() : default(ReasonFlags?);
			this.cRLIssuer = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeListTlv<GeneralName>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeTlv<GeneralName>()) : default(GeneralName[]);
		}
	}

	[Asn1Sequence()]
	partial class AccessDescription : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<AccessDescription>, IAsn1DerDecodableValue<AccessDescription>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid accessMethod;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralName accessLocation;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public AccessDescription(Asn1Oid accessMethod, GeneralName accessLocation)
		{
			this.accessMethod = accessMethod;
			this.accessLocation = accessLocation;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			this.accessLocation.EncodeTlv(encoder);
			encoder.EncodeOidTlv(this.accessMethod);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AccessDescription DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new AccessDescription(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static AccessDescription DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = AccessDescription.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out AccessDescription? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = AccessDescription.DecodeValueFrom(decoder);
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
		private AccessDescription(Asn1DerDecoder decoder)
		{
			this.accessMethod = decoder.DecodeOidTlv();
			this.accessLocation = decoder.DecodeTlv<GeneralName>();
		}
	}

	[Asn1Sequence()]
	partial class IssuingDistributionPoint : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<IssuingDistributionPoint>, IAsn1DerDecodableValue<IssuingDistributionPoint>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal DistributionPointName? distributionPoint;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal bool onlyContainsUserCerts;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal bool onlyContainsCACerts;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ReasonFlags? onlySomeReasons;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal bool indirectCRL;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal bool onlyContainsAttributeCerts;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public IssuingDistributionPoint(DistributionPointName? distributionPoint = default, bool onlyContainsUserCerts = false, bool onlyContainsCACerts = false, ReasonFlags? onlySomeReasons = default, bool indirectCRL = false, bool onlyContainsAttributeCerts = false)
		{
			this.distributionPoint = distributionPoint;
			this.onlyContainsUserCerts = onlyContainsUserCerts;
			this.onlyContainsCACerts = onlyContainsCACerts;
			this.onlySomeReasons = onlySomeReasons;
			this.indirectCRL = indirectCRL;
			this.onlyContainsAttributeCerts = onlyContainsAttributeCerts;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.onlyContainsAttributeCerts != false)
				encoder.EncodeBoolTlv(this.onlyContainsAttributeCerts);
			if (this.indirectCRL != false)
				encoder.EncodeBoolTlv(this.indirectCRL);
			if (this.onlySomeReasons is not null)
				encoder.EncodeBitStringTlv((ulong)this.onlySomeReasons.Value, 9, new Asn1Tag(0x80000003));
			if (this.onlyContainsCACerts != false)
				encoder.EncodeBoolTlv(this.onlyContainsCACerts);
			if (this.onlyContainsUserCerts != false)
				encoder.EncodeBoolTlv(this.onlyContainsUserCerts);
			if (this.distributionPoint is not null)
				encoder.EncodeExplicitTlv<DistributionPointName>(new Asn1Tag(0xA0000000), this.distributionPoint, (encoder, r) =>
				{
					this.distributionPoint.EncodeTlv(encoder);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static IssuingDistributionPoint DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new IssuingDistributionPoint(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static IssuingDistributionPoint DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = IssuingDistributionPoint.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out IssuingDistributionPoint? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = IssuingDistributionPoint.DecodeValueFrom(decoder);
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
		private IssuingDistributionPoint(Asn1DerDecoder decoder)
		{
			this.distributionPoint = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<DistributionPointName>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeTlv<DistributionPointName>()) : default(DistributionPointName);
			this.onlyContainsUserCerts = decoder.CheckTag(new Asn1Tag(0x80000001)) ? decoder.DecodeBoolTlv(new Asn1Tag(0x80000001)) : false;
			this.onlyContainsCACerts = decoder.CheckTag(new Asn1Tag(0x80000002)) ? decoder.DecodeBoolTlv(new Asn1Tag(0x80000002)) : false;
			this.onlySomeReasons = decoder.CheckTag(new Asn1Tag(0x80000003)) ? (ReasonFlags)decoder.DecodeBitStringTlv(new Asn1Tag(0x80000003)).ToUInt64() : default(ReasonFlags?);
			this.indirectCRL = decoder.CheckTag(new Asn1Tag(0x80000004)) ? decoder.DecodeBoolTlv(new Asn1Tag(0x80000004)) : false;
			this.onlyContainsAttributeCerts = decoder.CheckTag(new Asn1Tag(0x80000005)) ? decoder.DecodeBoolTlv(new Asn1Tag(0x80000005)) : false;
		}
	}

	[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
	public enum CRLReason
	{
		Unspecified = 0,
		KeyCompromise = 1,
		CACompromise = 2,
		AffiliationChanged = 3,
		Superseded = 4,
		CessationOfOperation = 5,
		CertificateHold = 6,
		RemoveFromCRL = 8,
		PrivilegeWithdrawn = 9,
		AACompromise = 10
	}
}