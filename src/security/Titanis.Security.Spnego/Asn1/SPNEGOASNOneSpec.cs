namespace SPNEGOASNOneSpec
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

	partial class SPNEGOASNOneSpecModule
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private SPNEGOASNOneSpecModule()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private static SPNEGOASNOneSpecModule _instance = new SPNEGOASNOneSpecModule();
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static SPNEGOASNOneSpecModule Instance => _instance;

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Name => "SPNEGOASNOneSpec";

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public string Oid => "1.3.6.1.5.5.2.4.2";
	}

	[FlagsAttribute(), GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
	public enum ContextFlags : uint
	{
		DelegFlag = 0x80000000,
		MutualFlag = 0x40000000,
		ReplayFlag = 0x20000000,
		SequenceFlag = 0x10000000,
		AnonFlag = 0x8000000,
		ConfFlag = 0x4000000,
		IntegFlag = 0x2000000
	}

	[Asn1Sequence()]
	partial class NegTokenInit : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<NegTokenInit>, IAsn1DerDecodableValue<NegTokenInit>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid[] mechTypes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ContextFlags? reqFlags;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? mechToken;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? mechListMIC;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NegTokenInit(Asn1Oid[] mechTypes, ContextFlags? reqFlags = default, Byte[]? mechToken = default, Byte[]? mechListMIC = default)
		{
			this.mechTypes = mechTypes;
			this.reqFlags = reqFlags;
			this.mechToken = mechToken;
			this.mechListMIC = mechListMIC;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.mechListMIC is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000003), this.mechListMIC, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.mechListMIC);
				});
			if (this.mechToken is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000002), this.mechToken, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.mechToken);
				});
			if (this.reqFlags is not null)
				encoder.EncodeExplicitTlv<ContextFlags>(new Asn1Tag(0xA0000001), this.reqFlags.Value, (encoder, r) =>
				{
					encoder.EncodeBitStringTlv((ulong)this.reqFlags.Value, 32);
				});
			encoder.EncodeExplicitTlv<Asn1Oid[]>(new Asn1Tag(0xA0000000), this.mechTypes, (encoder, r) =>
			{
				encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.mechTypes, (encoder, r) =>
				{
					encoder.EncodeOidTlv(r);
				});
			});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NegTokenInit DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new NegTokenInit(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NegTokenInit DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = NegTokenInit.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out NegTokenInit? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = NegTokenInit.DecodeValueFrom(decoder);
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
		private NegTokenInit(Asn1DerDecoder decoder)
		{
			this.mechTypes = decoder.DecodeTaggedValue<Asn1Oid[]>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeListTlv<Asn1Oid>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeOidTlv()));
			this.reqFlags = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<ContextFlags>(new Asn1Tag(0xA0000001), (encoder) => (ContextFlags)decoder.DecodeBitStringTlv().ToUInt64()) : default(ContextFlags);
			this.mechToken = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
			this.mechListMIC = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class NegTokenResp : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<NegTokenResp>, IAsn1DerDecodableValue<NegTokenResp>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal NegTokenResp_NegState_Tagged0? negState;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid? supportedMech;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? responseToken;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? mechListMIC;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NegTokenResp(NegTokenResp_NegState_Tagged0? negState = default, Asn1Oid? supportedMech = default, Byte[]? responseToken = default, Byte[]? mechListMIC = default)
		{
			this.negState = negState;
			this.supportedMech = supportedMech;
			this.responseToken = responseToken;
			this.mechListMIC = mechListMIC;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.mechListMIC is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000003), this.mechListMIC, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.mechListMIC);
				});
			if (this.responseToken is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000002), this.responseToken, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.responseToken);
				});
			if (this.supportedMech is not null)
				encoder.EncodeExplicitTlv<Asn1Oid>(new Asn1Tag(0xA0000001), this.supportedMech.Value, (encoder, r) =>
				{
					encoder.EncodeOidTlv(this.supportedMech.Value);
				});
			if (this.negState is not null)
				encoder.EncodeExplicitTlv<NegTokenResp_NegState_Tagged0>(new Asn1Tag(0xA0000000), this.negState.Value, (encoder, r) =>
				{
					encoder.EncodeEnumeratedTlv((long)this.negState.Value);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NegTokenResp DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new NegTokenResp(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NegTokenResp DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = NegTokenResp.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out NegTokenResp? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = NegTokenResp.DecodeValueFrom(decoder);
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
		private NegTokenResp(Asn1DerDecoder decoder)
		{
			this.negState = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<NegTokenResp_NegState_Tagged0>(new Asn1Tag(0xA0000000), (encoder) => (NegTokenResp_NegState_Tagged0)decoder.DecodeEnumeratedTlv()) : default(NegTokenResp_NegState_Tagged0);
			this.supportedMech = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<Asn1Oid>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOidTlv()) : default(Asn1Oid);
			this.responseToken = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
			this.mechListMIC = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000003), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
		}
	}

	[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
	public enum NegTokenResp_NegState_Tagged0
	{
		Accept_completed = 0,
		Accept_incomplete = 1,
		Reject = 2,
		Request_mic = 3
	}

	[Asn1Choice()]
	partial class NegotiationToken : IAsn1DerEncodableTlv, IAsn1DerDecodableTlv<NegotiationToken>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NegTokenInit NegTokenInit
		{
			get => this.negTokenInit;
			set
			{
				this.negTokenInit = value;
				this._choiceTag = ChoiceIndex.NegTokenInit;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private NegTokenInit? negTokenInit;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NegTokenResp NegTokenResp
		{
			get => this.negTokenResp;
			set
			{
				this.negTokenResp = value;
				this._choiceTag = ChoiceIndex.NegTokenResp;
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private NegTokenResp? negTokenResp;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NegotiationToken()
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag((uint)this._choiceTag);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			switch (this._choiceTag)
			{
				case ChoiceIndex.NegTokenResp:
					Debug.Assert(this.negTokenResp is not null);
					encoder.EncodeExplicitTlv<NegTokenResp>(new Asn1Tag(0xA0000001), this.negTokenResp, (encoder, r) =>
					{
						encoder.EncodeValueTlv(this.negTokenResp);
					});
					break;
				case ChoiceIndex.NegTokenInit:
					Debug.Assert(this.negTokenInit is not null);
					encoder.EncodeExplicitTlv<NegTokenInit>(new Asn1Tag(0xA0000000), this.negTokenInit, (encoder, r) =>
					{
						encoder.EncodeValueTlv(this.negTokenInit);
					});
					break;
				default:
					throw new InvalidOperationException("The object of type NegotiationToken has not been initialized.");
			}
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NegotiationToken DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			if (!NegotiationToken.TryDecodeTlvFrom(decoder, out var instance))
				throw new InvalidDataException();
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out NegotiationToken? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0xA0000000)))
				instance = new NegotiationToken()
				{
					_choiceTag = ChoiceIndex.NegTokenInit,
					negTokenInit = decoder.DecodeTaggedValue<NegTokenInit>(new Asn1Tag(0xA0000000), (encoder) => NegTokenInit.DecodeTlvFrom(decoder))
				};
			else if (decoder.CheckTag(new Asn1Tag(0xA0000001)))
				instance = new NegotiationToken()
				{
					_choiceTag = ChoiceIndex.NegTokenResp,
					negTokenResp = decoder.DecodeTaggedValue<NegTokenResp>(new Asn1Tag(0xA0000001), (encoder) => NegTokenResp.DecodeTlvFrom(decoder))
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
			NegTokenInit = 0xA0000000,
			NegTokenResp = 0xA0000001
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		private ChoiceIndex _choiceTag;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ChoiceIndex SelectedChoice => this._choiceTag;
	}

	[Asn1Sequence()]
	partial class NegHints : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<NegHints>, IAsn1DerDecodableValue<NegHints>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal GeneralString? hintName;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? hintAddress;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NegHints(GeneralString? hintName = default, Byte[]? hintAddress = default)
		{
			this.hintName = hintName;
			this.hintAddress = hintAddress;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.hintAddress is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000001), this.hintAddress, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.hintAddress);
				});
			if (this.hintName is not null)
				encoder.EncodeExplicitTlv<GeneralString>(new Asn1Tag(0xA0000000), this.hintName.Value, (encoder, r) =>
				{
					encoder.EncodeStringTlv(this.hintName.Value);
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NegHints DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new NegHints(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NegHints DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = NegHints.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out NegHints? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = NegHints.DecodeValueFrom(decoder);
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
		private NegHints(Asn1DerDecoder decoder)
		{
			this.hintName = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<GeneralString>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeStringTlv<GeneralString>()) : default(GeneralString);
			this.hintAddress = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000001), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
		}
	}

	[Asn1Sequence()]
	partial class NegTokenInit2 : IAsn1DerEncodableTlv, IAsn1DerEncodableValue, IAsn1DerDecodableTlv<NegTokenInit2>, IAsn1DerDecodableValue<NegTokenInit2>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Asn1Oid[]? mechTypes;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal ContextFlags? reqFlags;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? mechToken;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal NegHints? negHints;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		internal Byte[]? mechListMIC;
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NegTokenInit2(Asn1Oid[]? mechTypes = default, ContextFlags? reqFlags = default, Byte[]? mechToken = default, NegHints? negHints = default, Byte[]? mechListMIC = default)
		{
			this.mechTypes = mechTypes;
			this.reqFlags = reqFlags;
			this.mechToken = mechToken;
			this.negHints = negHints;
			this.mechListMIC = mechListMIC;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public Asn1Tag Tag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static Asn1Tag StaticTag => new Asn1Tag(0x20000010);

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeValue(Asn1DerEncoder encoder)
		{
			if (this.mechListMIC is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000004), this.mechListMIC, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.mechListMIC);
				});
			if (this.negHints is not null)
				encoder.EncodeExplicitTlv<NegHints>(new Asn1Tag(0xA0000003), this.negHints, (encoder, r) =>
				{
					encoder.EncodeValueTlv(this.negHints);
				});
			if (this.mechToken is not null)
				encoder.EncodeExplicitTlv<Byte[]>(new Asn1Tag(0xA0000002), this.mechToken, (encoder, r) =>
				{
					encoder.EncodeOctetStringTlv(this.mechToken);
				});
			if (this.reqFlags is not null)
				encoder.EncodeExplicitTlv<ContextFlags>(new Asn1Tag(0xA0000001), this.reqFlags.Value, (encoder, r) =>
				{
					encoder.EncodeBitStringTlv((ulong)this.reqFlags.Value, 32);
				});
			if (this.mechTypes is not null)
				encoder.EncodeExplicitTlv<Asn1Oid[]>(new Asn1Tag(0xA0000000), this.mechTypes, (encoder, r) =>
				{
					encoder.EncodeListTlv(new Asn1Tag(0x20000010), this.mechTypes, (encoder, r) =>
					{
						encoder.EncodeOidTlv(r);
					});
				});
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NegTokenInit2 DecodeValueFrom(Asn1DerDecoder decoder)
		{
			var instance = new NegTokenInit2(decoder);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NegTokenInit2 DecodeTlvFrom(Asn1DerDecoder decoder)
		{
			var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
			var instance = NegTokenInit2.DecodeValueFrom(decoder);
			decoder.CloseTlv(tlvFrame);
			return instance;
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out NegTokenInit2? instance)
		{
			if (decoder.CheckTag(new Asn1Tag(0x20000010)))
			{
				var tlvFrame = decoder.DecodeTlvStart(new Asn1Tag(0x20000010));
				instance = NegTokenInit2.DecodeValueFrom(decoder);
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
		private NegTokenInit2(Asn1DerDecoder decoder)
		{
			this.mechTypes = decoder.CheckTag(new Asn1Tag(0xA0000000)) ? decoder.DecodeTaggedValue<Asn1Oid[]>(new Asn1Tag(0xA0000000), (encoder) => decoder.DecodeListTlv<Asn1Oid>(new Asn1Tag(0x20000010), (encoder) => decoder.DecodeOidTlv())) : default(Asn1Oid[]);
			this.reqFlags = decoder.CheckTag(new Asn1Tag(0xA0000001)) ? decoder.DecodeTaggedValue<ContextFlags>(new Asn1Tag(0xA0000001), (encoder) => (ContextFlags)decoder.DecodeBitStringTlv().ToUInt64()) : default(ContextFlags);
			this.mechToken = decoder.CheckTag(new Asn1Tag(0xA0000002)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000002), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
			this.negHints = decoder.CheckTag(new Asn1Tag(0xA0000003)) ? decoder.DecodeTaggedValue<NegHints>(new Asn1Tag(0xA0000003), (encoder) => NegHints.DecodeTlvFrom(decoder)) : default(NegHints);
			this.mechListMIC = decoder.CheckTag(new Asn1Tag(0xA0000004)) ? decoder.DecodeTaggedValue<Byte[]>(new Asn1Tag(0xA0000004), (encoder) => decoder.DecodeOctetStringTlv()) : default(Byte[]);
		}
	}

	partial class NegotiationToken2 : Asn1Explicit<NegTokenInit2>, IAsn1DerDecodableTlv<NegotiationToken2>, IAsn1DerDecodableValue<NegotiationToken2>
	{
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public NegotiationToken2(NegTokenInit2 value) : base(new Asn1Tag(0xA0000000), value)
		{
		}

		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NegotiationToken2 DecodeValueFrom(Asn1DerDecoder decoder) => new NegotiationToken2(decoder.DecodeTlv<NegTokenInit2>());
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static NegotiationToken2 DecodeTlvFrom(Asn1DerDecoder decoder) => new NegotiationToken2(decoder.DecodeExplicitTaggedTlv<NegTokenInit2>(new Asn1Tag(0xA0000000)));
		[GeneratedCodeAttribute("Animus ASN.1 Compiler", "0.9.8")]
		public static bool TryDecodeTlvFrom(Asn1DerDecoder decoder, [NotNullWhen(true)] out NegotiationToken2? value)
		{
			if (decoder.TryDecodeExplicitTaggedTlv<NegTokenInit2>(new Asn1Tag(0xA0000000), out var inner))
			{
				value = new NegotiationToken2(inner);
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}
	}
}