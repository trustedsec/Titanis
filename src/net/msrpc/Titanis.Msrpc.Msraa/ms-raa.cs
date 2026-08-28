namespace ms_raa
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct AUTHZR_ACCESS_REQUEST : IRpcFixedStruct
	{
		public uint DesiredAccess;
		public RpcPointer<ms_dtyp.RPC_SID> PrincipalSelfSid;
		public uint ObjectTypeListLength;
		public RpcPointer<ms_dtyp.OBJECT_TYPE_LIST[]> ObjectTypeList;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.DesiredAccess);
			encoder.WriteUniquePointer(this.PrincipalSelfSid);
			encoder.WriteValue(this.ObjectTypeListLength);
			encoder.WriteUniquePointer(this.ObjectTypeList);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.DesiredAccess = decoder.ReadUInt32();
			this.PrincipalSelfSid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			this.ObjectTypeListLength = decoder.ReadUInt32();
			this.ObjectTypeList = decoder.ReadUniquePointer<ms_dtyp.OBJECT_TYPE_LIST[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.PrincipalSelfSid is not null)
			{
				encoder.WriteConformantStruct(this.PrincipalSelfSid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.PrincipalSelfSid.value);
			}

			if (this.ObjectTypeList is not null)
			{
				encoder.WriteArrayHeader(this.ObjectTypeList.value);
				for (int i = 0; i < this.ObjectTypeList.value.Length; i++)
				{
					ms_dtyp.OBJECT_TYPE_LIST elem_0 = this.ObjectTypeList.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.ObjectTypeList.value.Length; i++)
				{
					ms_dtyp.OBJECT_TYPE_LIST elem_0 = this.ObjectTypeList.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.PrincipalSelfSid is not null)
			{
				this.PrincipalSelfSid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.PrincipalSelfSid.value);
			}

			if (this.ObjectTypeList is not null)
			{
				this.ObjectTypeList.value = decoder.ReadArrayHeader<ms_dtyp.OBJECT_TYPE_LIST>();
				for (int i = 0; i < this.ObjectTypeList.value.Length; i++)
				{
					ms_dtyp.OBJECT_TYPE_LIST elem_0 = this.ObjectTypeList.value[i];
					elem_0 = decoder.ReadFixedStruct<ms_dtyp.OBJECT_TYPE_LIST>(NdrAlignment.NativePtr);
					this.ObjectTypeList.value[i] = elem_0;
				}

				for (int i = 0; i < this.ObjectTypeList.value.Length; i++)
				{
					ms_dtyp.OBJECT_TYPE_LIST elem_0 = this.ObjectTypeList.value[i];
					decoder.ReadStructDeferral<ms_dtyp.OBJECT_TYPE_LIST>(ref elem_0);
					this.ObjectTypeList.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SR_SD : IRpcFixedStruct
	{
		public uint dwLength;
		public RpcPointer<byte[]> pSrSd;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwLength);
			encoder.WriteUniquePointer(this.pSrSd);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwLength = decoder.ReadUInt32();
			this.pSrSd = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pSrSd is not null)
			{
				encoder.WriteArrayHeader(this.pSrSd.value);
				for (int i = 0; i < this.pSrSd.value.Length; i++)
				{
					byte elem_0 = this.pSrSd.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pSrSd is not null)
			{
				this.pSrSd.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pSrSd.value.Length; i++)
				{
					byte elem_0 = this.pSrSd.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pSrSd.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct AUTHZR_ACCESS_REPLY : IRpcFixedStruct
	{
		public uint ResultListLength;
		public RpcPointer<uint[]> GrantedAccessMask;
		public RpcPointer<uint[]> Error;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ResultListLength);
			encoder.WriteUniquePointer(this.GrantedAccessMask);
			encoder.WriteUniquePointer(this.Error);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ResultListLength = decoder.ReadUInt32();
			this.GrantedAccessMask = decoder.ReadUniquePointer<uint[]>();
			this.Error = decoder.ReadUniquePointer<uint[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.GrantedAccessMask is not null)
			{
				encoder.WriteArrayHeader(this.GrantedAccessMask.value);
				for (int i = 0; i < this.GrantedAccessMask.value.Length; i++)
				{
					uint elem_0 = this.GrantedAccessMask.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.Error is not null)
			{
				encoder.WriteArrayHeader(this.Error.value);
				for (int i = 0; i < this.Error.value.Length; i++)
				{
					uint elem_0 = this.Error.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.GrantedAccessMask is not null)
			{
				this.GrantedAccessMask.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < this.GrantedAccessMask.value.Length; i++)
				{
					uint elem_0 = this.GrantedAccessMask.value[i];
					elem_0 = decoder.ReadUInt32();
					this.GrantedAccessMask.value[i] = elem_0;
				}
			}

			if (this.Error is not null)
			{
				this.Error.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < this.Error.value.Length; i++)
				{
					uint elem_0 = this.Error.value[i];
					elem_0 = decoder.ReadUInt32();
					this.Error.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum AUTHZ_CONTEXT_INFORMATION_CLASS : int
	{
		AuthzContextInfoUserSid = 1,
		AuthzContextInfoGroupsSids = 2,
		AuthzContextInfoRestrictedSids = 3,
		ReservedEnumValue4 = 4,
		ReservedEnumValue5 = 5,
		ReservedEnumValue6 = 6,
		ReservedEnumValue7 = 7,
		ReservedEnumValue8 = 8,
		ReservedEnumValue9 = 9,
		ReservedEnumValue10 = 10,
		ReservedEnumValue11 = 11,
		AuthzContextInfoDeviceSids = 12,
		AuthzContextInfoUserClaims = 13,
		AuthzContextInfoDeviceClaims = 14,
		ReservedEnumValue15 = 15,
		ReservedEnumValue16 = 16
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct AUTHZR_SID_AND_ATTRIBUTES : IRpcFixedStruct
	{
		public RpcPointer<ms_dtyp.RPC_SID> Sid;
		public uint Attributes;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.Sid);
			encoder.WriteValue(this.Attributes);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Sid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			this.Attributes = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Sid is not null)
			{
				encoder.WriteConformantStruct(this.Sid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Sid.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Sid is not null)
			{
				this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct AUTHZR_TOKEN_USER : IRpcFixedStruct
	{
		public AUTHZR_SID_AND_ATTRIBUTES User;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.User, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.User = decoder.ReadFixedStruct<AUTHZR_SID_AND_ATTRIBUTES>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.User);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<AUTHZR_SID_AND_ATTRIBUTES>(ref this.User);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct AUTHZR_TOKEN_GROUPS : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.Groups);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.Groups = decoder.ReadArrayHeader<AUTHZR_SID_AND_ATTRIBUTES>();
		}

		public uint GroupCount;
		public AUTHZR_SID_AND_ATTRIBUTES[] Groups;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.Groups.Length; i++)
			{
				AUTHZR_SID_AND_ATTRIBUTES elem_0 = this.Groups[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.Groups.Length; i++)
			{
				AUTHZR_SID_AND_ATTRIBUTES elem_0 = this.Groups[i];
				elem_0 = decoder.ReadFixedStruct<AUTHZR_SID_AND_ATTRIBUTES>(NdrAlignment.NativePtr);
				this.Groups[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.GroupCount);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.GroupCount = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.Groups.Length; i++)
			{
				AUTHZR_SID_AND_ATTRIBUTES elem_0 = this.Groups[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.Groups.Length; i++)
			{
				AUTHZR_SID_AND_ATTRIBUTES elem_0 = this.Groups[i];
				decoder.ReadStructDeferral<AUTHZR_SID_AND_ATTRIBUTES>(ref elem_0);
				this.Groups[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct AUTHZR_SECURITY_ATTRIBUTE_STRING_VALUE : IRpcFixedStruct
	{
		public uint Length;
		public RpcPointer<string> Value;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteUniquePointer(this.Value);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt32();
			this.Value = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Value is not null)
			{
				encoder.WriteWideCharString(this.Value.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Value is not null)
			{
				this.Value.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct AUTHZR_SECURITY_ATTRIBUTE_UNION : IRpcFixedStruct
	{
		public ushort ValueType;
		public long Int64;
		public ulong Uint64;
		public AUTHZR_SECURITY_ATTRIBUTE_STRING_VALUE String;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._8Byte);
			encoder.WriteValue(this.ValueType);
			switch ((int)this.ValueType)
			{
				case 1:
					encoder.WriteValue(this.Int64);
					break;
				case 2:
				case 6:
					encoder.WriteValue(this.Uint64);
					break;
				case 3:
					encoder.WriteFixedStruct(this.String, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._8Byte);
			this.ValueType = decoder.ReadUInt16();
			switch ((int)this.ValueType)
			{
				case 1:
					this.Int64 = decoder.ReadInt64();
					break;
				case 2:
				case 6:
					this.Uint64 = decoder.ReadUInt64();
					break;
				case 3:
					this.String = decoder.ReadFixedStruct<AUTHZR_SECURITY_ATTRIBUTE_STRING_VALUE>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.ValueType)
			{
				case 1:
					break;
				case 2:
				case 6:
					break;
				case 3:
					encoder.WriteStructDeferral(this.String);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.ValueType)
			{
				case 1:
					break;
				case 2:
				case 6:
					break;
				case 3:
					decoder.ReadStructDeferral<AUTHZR_SECURITY_ATTRIBUTE_STRING_VALUE>(ref this.String);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE : IRpcFixedStruct
	{
		public ushort ValueType;
		public AUTHZR_SECURITY_ATTRIBUTE_UNION AttributeUnion;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ValueType);
			encoder.WriteUnion(this.AttributeUnion);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ValueType = decoder.ReadUInt16();
			this.AttributeUnion = decoder.ReadUnion<AUTHZR_SECURITY_ATTRIBUTE_UNION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.AttributeUnion);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<AUTHZR_SECURITY_ATTRIBUTE_UNION>(ref this.AttributeUnion);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct AUTHZR_SECURITY_ATTRIBUTE_V1 : IRpcFixedStruct
	{
		public uint Length;
		public RpcPointer<string> Value;
		public ushort ValueType;
		public ushort Reserved;
		public uint Flags;
		public uint ValueCount;
		public RpcPointer<AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE[]> Values;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteUniquePointer(this.Value);
			encoder.WriteValue(this.ValueType);
			encoder.WriteValue(this.Reserved);
			encoder.WriteValue(this.Flags);
			encoder.WriteValue(this.ValueCount);
			encoder.WriteUniquePointer(this.Values);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt32();
			this.Value = decoder.ReadUniquePointer<string>();
			this.ValueType = decoder.ReadUInt16();
			this.Reserved = decoder.ReadUInt16();
			this.Flags = decoder.ReadUInt32();
			this.ValueCount = decoder.ReadUInt32();
			this.Values = decoder.ReadUniquePointer<AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Value is not null)
			{
				encoder.WriteWideCharString(this.Value.value);
			}

			if (this.Values is not null)
			{
				encoder.WriteArrayHeader(this.Values.value);
				for (int i = 0; i < this.Values.value.Length; i++)
				{
					AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE elem_0 = this.Values.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.Values.value.Length; i++)
				{
					AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE elem_0 = this.Values.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Value is not null)
			{
				this.Value.value = decoder.ReadWideCharString();
			}

			if (this.Values is not null)
			{
				this.Values.value = decoder.ReadArrayHeader<AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE>();
				for (int i = 0; i < this.Values.value.Length; i++)
				{
					AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE elem_0 = this.Values.value[i];
					elem_0 = decoder.ReadFixedStruct<AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE>(NdrAlignment._8Byte);
					this.Values.value[i] = elem_0;
				}

				for (int i = 0; i < this.Values.value.Length; i++)
				{
					AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE elem_0 = this.Values.value[i];
					decoder.ReadStructDeferral<AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE>(ref elem_0);
					this.Values.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct AUTHZR_SECURITY_ATTRIBUTES_INFORMATION : IRpcFixedStruct
	{
		public ushort Version;
		public ushort Reserved;
		public uint AttributeCount;
		public RpcPointer<AUTHZR_SECURITY_ATTRIBUTE_V1[]> Attributes;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Version);
			encoder.WriteValue(this.Reserved);
			encoder.WriteValue(this.AttributeCount);
			encoder.WriteUniquePointer(this.Attributes);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Version = decoder.ReadUInt16();
			this.Reserved = decoder.ReadUInt16();
			this.AttributeCount = decoder.ReadUInt32();
			this.Attributes = decoder.ReadUniquePointer<AUTHZR_SECURITY_ATTRIBUTE_V1[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Attributes is not null)
			{
				encoder.WriteArrayHeader(this.Attributes.value);
				for (int i = 0; i < this.Attributes.value.Length; i++)
				{
					AUTHZR_SECURITY_ATTRIBUTE_V1 elem_0 = this.Attributes.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Attributes.value.Length; i++)
				{
					AUTHZR_SECURITY_ATTRIBUTE_V1 elem_0 = this.Attributes.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Attributes is not null)
			{
				this.Attributes.value = decoder.ReadArrayHeader<AUTHZR_SECURITY_ATTRIBUTE_V1>();
				for (int i = 0; i < this.Attributes.value.Length; i++)
				{
					AUTHZR_SECURITY_ATTRIBUTE_V1 elem_0 = this.Attributes.value[i];
					elem_0 = decoder.ReadFixedStruct<AUTHZR_SECURITY_ATTRIBUTE_V1>(NdrAlignment.NativePtr);
					this.Attributes.value[i] = elem_0;
				}

				for (int i = 0; i < this.Attributes.value.Length; i++)
				{
					AUTHZR_SECURITY_ATTRIBUTE_V1 elem_0 = this.Attributes.value[i];
					decoder.ReadStructDeferral<AUTHZR_SECURITY_ATTRIBUTE_V1>(ref elem_0);
					this.Attributes.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct AUTHZR_CONTEXT_INFORMATION_UNION : IRpcFixedStruct
	{
		public ushort ValueType;
		public RpcPointer<AUTHZR_TOKEN_USER> pTokenUser;
		public RpcPointer<AUTHZR_TOKEN_GROUPS> pTokenGroups;
		public RpcPointer<AUTHZR_SECURITY_ATTRIBUTES_INFORMATION> pTokenClaims;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.ValueType);
			switch ((int)this.ValueType)
			{
				case 1:
					encoder.WriteUniquePointer(this.pTokenUser);
					break;
				case 2:
				case 3:
				case 12:
					encoder.WriteUniquePointer(this.pTokenGroups);
					break;
				case 13:
				case 14:
					encoder.WriteUniquePointer(this.pTokenClaims);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.ValueType = decoder.ReadUInt16();
			switch ((int)this.ValueType)
			{
				case 1:
					this.pTokenUser = decoder.ReadUniquePointer<AUTHZR_TOKEN_USER>();
					break;
				case 2:
				case 3:
				case 12:
					this.pTokenGroups = decoder.ReadUniquePointer<AUTHZR_TOKEN_GROUPS>();
					break;
				case 13:
				case 14:
					this.pTokenClaims = decoder.ReadUniquePointer<AUTHZR_SECURITY_ATTRIBUTES_INFORMATION>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.ValueType)
			{
				case 1:
					if (this.pTokenUser is not null)
					{
						encoder.WriteFixedStruct(this.pTokenUser.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.pTokenUser.value);
					}

					break;
				case 2:
				case 3:
				case 12:
					if (this.pTokenGroups is not null)
					{
						encoder.WriteConformantStruct(this.pTokenGroups.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.pTokenGroups.value);
					}

					break;
				case 13:
				case 14:
					if (this.pTokenClaims is not null)
					{
						encoder.WriteFixedStruct(this.pTokenClaims.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.pTokenClaims.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.ValueType)
			{
				case 1:
					if (this.pTokenUser is not null)
					{
						this.pTokenUser.value = decoder.ReadFixedStruct<AUTHZR_TOKEN_USER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<AUTHZR_TOKEN_USER>(ref this.pTokenUser.value);
					}

					break;
				case 2:
				case 3:
				case 12:
					if (this.pTokenGroups is not null)
					{
						this.pTokenGroups.value = decoder.ReadConformantStruct<AUTHZR_TOKEN_GROUPS>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<AUTHZR_TOKEN_GROUPS>(ref this.pTokenGroups.value);
					}

					break;
				case 13:
				case 14:
					if (this.pTokenClaims is not null)
					{
						this.pTokenClaims.value = decoder.ReadFixedStruct<AUTHZR_SECURITY_ATTRIBUTES_INFORMATION>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<AUTHZR_SECURITY_ATTRIBUTES_INFORMATION>(ref this.pTokenClaims.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct AUTHZR_CONTEXT_INFORMATION : IRpcFixedStruct
	{
		public ushort ValueType;
		public AUTHZR_CONTEXT_INFORMATION_UNION ContextInfoUnion;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ValueType);
			encoder.WriteUnion(this.ContextInfoUnion);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ValueType = decoder.ReadUInt16();
			this.ContextInfoUnion = decoder.ReadUnion<AUTHZR_CONTEXT_INFORMATION_UNION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ContextInfoUnion);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<AUTHZR_CONTEXT_INFORMATION_UNION>(ref this.ContextInfoUnion);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum AUTHZ_SECURITY_ATTRIBUTE_OPERATION : int
	{
		AUTHZ_SECURITY_ATTRIBUTE_OPERATION_NONE = 0,
		AUTHZ_SECURITY_ATTRIBUTE_OPERATION_REPLACE_ALL = 1,
		AUTHZ_SECURITY_ATTRIBUTE_OPERATION_ADD = 2,
		AUTHZ_SECURITY_ATTRIBUTE_OPERATION_DELETE = 3,
		AUTHZ_SECURITY_ATTRIBUTE_OPERATION_REPLACE = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum AUTHZ_SID_OPERATION : int
	{
		AUTHZ_SID_OPERATION_NONE = 0,
		AUTHZ_SID_OPERATION_REPLACE_ALL = 1,
		AUTHZ_SID_OPERATION_ADD = 2,
		AUTHZ_SID_OPERATION_DELETE = 3,
		AUTHZ_SID_OPERATION_REPLACE = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("0b1c2170-5732-4e0e-8cd3-d9b16f3b84d7"), RpcVersionAttribute(0, 0)]
	public partial interface authzr
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> AuthzrFreeContext(RpcPointer<RpcContextHandle>? ContextHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> AuthzrInitializeContextFromSid(uint Flags, ms_dtyp.RPC_SID Sid, RpcPointer<ms_dtyp.LARGE_INTEGER> pExpirationTime, ms_dtyp.LUID Identifier, RpcPointer<RpcContextHandle>? ContextHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> AuthzrInitializeCompoundContext(RpcContextHandle UserContextHandle, RpcContextHandle DeviceContextHandle, RpcPointer<RpcContextHandle>? CompoundContextHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> AuthzrAccessCheck(RpcContextHandle ContextHandle, uint Flags, AUTHZR_ACCESS_REQUEST pRequest, uint SecurityDescriptorCount, SR_SD[] pSecurityDescriptors, RpcPointer<AUTHZR_ACCESS_REPLY>? pReply, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> AuthzGetInformationFromContext(RpcContextHandle ContextHandle, AUTHZ_CONTEXT_INFORMATION_CLASS InfoClass, RpcPointer<RpcPointer<AUTHZR_CONTEXT_INFORMATION>?>? ppContextInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> AuthzrModifyClaims(RpcContextHandle ContextHandle, AUTHZ_CONTEXT_INFORMATION_CLASS ClaimClass, uint OperationCount, AUTHZ_SECURITY_ATTRIBUTE_OPERATION[] pClaimOperations, RpcPointer<AUTHZR_SECURITY_ATTRIBUTES_INFORMATION> pClaims, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> AuthzrModifySids(RpcContextHandle ContextHandle, AUTHZ_CONTEXT_INFORMATION_CLASS SidClass, uint OperationCount, AUTHZ_SID_OPERATION[] pSidOperations, RpcPointer<AUTHZR_TOKEN_GROUPS> pSids, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("0b1c2170-5732-4e0e-8cd3-d9b16f3b84d7")]
	public partial class authzrClientProxy : Titanis.DceRpc.Client.RpcClientProxy, authzr, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> AuthzrFreeContext(RpcPointer<RpcContextHandle>? ContextHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ContextHandle.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ContextHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> AuthzrInitializeContextFromSid(uint Flags, ms_dtyp.RPC_SID Sid, RpcPointer<ms_dtyp.LARGE_INTEGER> pExpirationTime, ms_dtyp.LUID Identifier, RpcPointer<RpcContextHandle>? ContextHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(Flags);
			encoder.WriteConformantStruct(Sid, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(Sid);
			encoder.WriteUniquePointer(pExpirationTime);
			if (pExpirationTime is not null)
			{
				encoder.WriteFixedStruct(pExpirationTime.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(pExpirationTime.value);
			}

			encoder.WriteFixedStruct(Identifier, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(Identifier);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ContextHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> AuthzrInitializeCompoundContext(RpcContextHandle UserContextHandle, RpcContextHandle DeviceContextHandle, RpcPointer<RpcContextHandle>? CompoundContextHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(UserContextHandle);
			encoder.WriteContextHandle(DeviceContextHandle);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			CompoundContextHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> AuthzrAccessCheck(RpcContextHandle ContextHandle, uint Flags, AUTHZR_ACCESS_REQUEST pRequest, uint SecurityDescriptorCount, SR_SD[] pSecurityDescriptors, RpcPointer<AUTHZR_ACCESS_REPLY>? pReply, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ContextHandle);
			encoder.WriteValue(Flags);
			encoder.WriteFixedStruct(pRequest, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRequest);
			encoder.WriteValue(SecurityDescriptorCount);
			if (pSecurityDescriptors is not null)
			{
				encoder.WriteArrayHeader(pSecurityDescriptors);
				for (int i = 0; i < pSecurityDescriptors.Length; i++)
				{
					SR_SD elem_0 = pSecurityDescriptors[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}
			}

			for (int i = 0; i < pSecurityDescriptors.Length; i++)
			{
				SR_SD elem_0 = pSecurityDescriptors[i];
				encoder.WriteStructDeferral(elem_0);
			}

			encoder.WriteFixedStruct(pReply.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pReply.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pReply.value = decoder.ReadFixedStruct<AUTHZR_ACCESS_REPLY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<AUTHZR_ACCESS_REPLY>(ref pReply.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> AuthzGetInformationFromContext(RpcContextHandle ContextHandle, AUTHZ_CONTEXT_INFORMATION_CLASS InfoClass, RpcPointer<RpcPointer<AUTHZR_CONTEXT_INFORMATION>?>? ppContextInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ContextHandle);
			encoder.WriteEnumShortValue((short)InfoClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppContextInformation.value = decoder.ReadOutFullPointer<AUTHZR_CONTEXT_INFORMATION>(ppContextInformation.value);
			if (ppContextInformation.value is not null)
			{
				ppContextInformation.value.value = decoder.ReadFixedStruct<AUTHZR_CONTEXT_INFORMATION>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<AUTHZR_CONTEXT_INFORMATION>(ref ppContextInformation.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> AuthzrModifyClaims(RpcContextHandle ContextHandle, AUTHZ_CONTEXT_INFORMATION_CLASS ClaimClass, uint OperationCount, AUTHZ_SECURITY_ATTRIBUTE_OPERATION[] pClaimOperations, RpcPointer<AUTHZR_SECURITY_ATTRIBUTES_INFORMATION> pClaims, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ContextHandle);
			encoder.WriteEnumShortValue((short)ClaimClass);
			encoder.WriteValue(OperationCount);
			if (pClaimOperations is not null)
			{
				encoder.WriteArrayHeader(pClaimOperations);
				for (int i = 0; i < pClaimOperations.Length; i++)
				{
					AUTHZ_SECURITY_ATTRIBUTE_OPERATION elem_0 = pClaimOperations[i];
					encoder.WriteEnumShortValue((short)elem_0);
				}
			}

			encoder.WriteUniquePointer(pClaims);
			if (pClaims is not null)
			{
				encoder.WriteFixedStruct(pClaims.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(pClaims.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> AuthzrModifySids(RpcContextHandle ContextHandle, AUTHZ_CONTEXT_INFORMATION_CLASS SidClass, uint OperationCount, AUTHZ_SID_OPERATION[] pSidOperations, RpcPointer<AUTHZR_TOKEN_GROUPS> pSids, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ContextHandle);
			encoder.WriteEnumShortValue((short)SidClass);
			encoder.WriteValue(OperationCount);
			if (pSidOperations is not null)
			{
				encoder.WriteArrayHeader(pSidOperations);
				for (int i = 0; i < pSidOperations.Length; i++)
				{
					AUTHZ_SID_OPERATION elem_0 = pSidOperations[i];
					encoder.WriteEnumShortValue((short)elem_0);
				}
			}

			encoder.WriteUniquePointer(pSids);
			if (pSids is not null)
			{
				encoder.WriteConformantStruct(pSids.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(pSids.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(authzr);
		private static Guid _interfaceUuid = new Guid("0b1c2170-5732-4e0e-8cd3-d9b16f3b84d7");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class authzrStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_AuthzrFreeContext(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle>? ContextHandle;
			ContextHandle = new RpcPointer<RpcContextHandle>();
			ContextHandle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.AuthzrFreeContext(ContextHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(ContextHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_AuthzrInitializeContextFromSid(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint Flags;
			ms_dtyp.RPC_SID Sid;
			RpcPointer<ms_dtyp.LARGE_INTEGER> pExpirationTime;
			ms_dtyp.LUID Identifier;
			RpcPointer<RpcContextHandle>? ContextHandle = new RpcPointer<RpcContextHandle>();
			Flags = decoder.ReadUInt32();
			Sid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref Sid);
			pExpirationTime = decoder.ReadUniquePointer<ms_dtyp.LARGE_INTEGER>();
			if (pExpirationTime is not null)
			{
				pExpirationTime.value = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref pExpirationTime.value);
			}

			Identifier = decoder.ReadFixedStruct<ms_dtyp.LUID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.LUID>(ref Identifier);
			var invokeTask = this._obj.AuthzrInitializeContextFromSid(Flags, Sid, pExpirationTime, Identifier, ContextHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(ContextHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_AuthzrInitializeCompoundContext(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle UserContextHandle;
			RpcContextHandle DeviceContextHandle;
			RpcPointer<RpcContextHandle>? CompoundContextHandle = new RpcPointer<RpcContextHandle>();
			UserContextHandle = decoder.ReadContextHandle();
			DeviceContextHandle = decoder.ReadContextHandle();
			var invokeTask = this._obj.AuthzrInitializeCompoundContext(UserContextHandle, DeviceContextHandle, CompoundContextHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(CompoundContextHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_AuthzrAccessCheck(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle ContextHandle;
			uint Flags;
			AUTHZR_ACCESS_REQUEST pRequest;
			uint SecurityDescriptorCount;
			SR_SD[] pSecurityDescriptors;
			RpcPointer<AUTHZR_ACCESS_REPLY>? pReply;
			ContextHandle = decoder.ReadContextHandle();
			Flags = decoder.ReadUInt32();
			pRequest = decoder.ReadFixedStruct<AUTHZR_ACCESS_REQUEST>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<AUTHZR_ACCESS_REQUEST>(ref pRequest);
			SecurityDescriptorCount = decoder.ReadUInt32();
			pSecurityDescriptors = decoder.ReadArrayHeader<SR_SD>();
			for (int i = 0; i < pSecurityDescriptors.Length; i++)
			{
				SR_SD elem_0 = pSecurityDescriptors[i];
				elem_0 = decoder.ReadFixedStruct<SR_SD>(NdrAlignment.NativePtr);
				pSecurityDescriptors[i] = elem_0;
			}

			for (int i = 0; i < pSecurityDescriptors.Length; i++)
			{
				SR_SD elem_0 = pSecurityDescriptors[i];
				decoder.ReadStructDeferral<SR_SD>(ref elem_0);
				pSecurityDescriptors[i] = elem_0;
			}

			pReply = new RpcPointer<AUTHZR_ACCESS_REPLY>();
			pReply.value = decoder.ReadFixedStruct<AUTHZR_ACCESS_REPLY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<AUTHZR_ACCESS_REPLY>(ref pReply.value);
			var invokeTask = this._obj.AuthzrAccessCheck(ContextHandle, Flags, pRequest, SecurityDescriptorCount, pSecurityDescriptors, pReply, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pReply.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pReply.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_AuthzGetInformationFromContext(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle ContextHandle;
			AUTHZ_CONTEXT_INFORMATION_CLASS InfoClass;
			RpcPointer<RpcPointer<AUTHZR_CONTEXT_INFORMATION>?>? ppContextInformation = new RpcPointer<RpcPointer<AUTHZR_CONTEXT_INFORMATION>?>();
			ContextHandle = decoder.ReadContextHandle();
			InfoClass = (AUTHZ_CONTEXT_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.AuthzGetInformationFromContext(ContextHandle, InfoClass, ppContextInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFullPointer(ppContextInformation.value);
			if (ppContextInformation.value is not null)
			{
				encoder.WriteFixedStruct(ppContextInformation.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppContextInformation.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_AuthzrModifyClaims(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle ContextHandle;
			AUTHZ_CONTEXT_INFORMATION_CLASS ClaimClass;
			uint OperationCount;
			AUTHZ_SECURITY_ATTRIBUTE_OPERATION[] pClaimOperations;
			RpcPointer<AUTHZR_SECURITY_ATTRIBUTES_INFORMATION> pClaims;
			ContextHandle = decoder.ReadContextHandle();
			ClaimClass = (AUTHZ_CONTEXT_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			OperationCount = decoder.ReadUInt32();
			pClaimOperations = decoder.ReadArrayHeader<AUTHZ_SECURITY_ATTRIBUTE_OPERATION>();
			for (int i = 0; i < pClaimOperations.Length; i++)
			{
				AUTHZ_SECURITY_ATTRIBUTE_OPERATION elem_0 = pClaimOperations[i];
				elem_0 = (AUTHZ_SECURITY_ATTRIBUTE_OPERATION)decoder.ReadEnumShortValue();
				pClaimOperations[i] = elem_0;
			}

			pClaims = decoder.ReadUniquePointer<AUTHZR_SECURITY_ATTRIBUTES_INFORMATION>();
			if (pClaims is not null)
			{
				pClaims.value = decoder.ReadFixedStruct<AUTHZR_SECURITY_ATTRIBUTES_INFORMATION>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<AUTHZR_SECURITY_ATTRIBUTES_INFORMATION>(ref pClaims.value);
			}

			var invokeTask = this._obj.AuthzrModifyClaims(ContextHandle, ClaimClass, OperationCount, pClaimOperations, pClaims, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_AuthzrModifySids(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle ContextHandle;
			AUTHZ_CONTEXT_INFORMATION_CLASS SidClass;
			uint OperationCount;
			AUTHZ_SID_OPERATION[] pSidOperations;
			RpcPointer<AUTHZR_TOKEN_GROUPS> pSids;
			ContextHandle = decoder.ReadContextHandle();
			SidClass = (AUTHZ_CONTEXT_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			OperationCount = decoder.ReadUInt32();
			pSidOperations = decoder.ReadArrayHeader<AUTHZ_SID_OPERATION>();
			for (int i = 0; i < pSidOperations.Length; i++)
			{
				AUTHZ_SID_OPERATION elem_0 = pSidOperations[i];
				elem_0 = (AUTHZ_SID_OPERATION)decoder.ReadEnumShortValue();
				pSidOperations[i] = elem_0;
			}

			pSids = decoder.ReadUniquePointer<AUTHZR_TOKEN_GROUPS>();
			if (pSids is not null)
			{
				pSids.value = decoder.ReadConformantStruct<AUTHZR_TOKEN_GROUPS>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<AUTHZR_TOKEN_GROUPS>(ref pSids.value);
			}

			var invokeTask = this._obj.AuthzrModifySids(ContextHandle, SidClass, OperationCount, pSidOperations, pSids, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("0b1c2170-5732-4e0e-8cd3-d9b16f3b84d7");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private authzr _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public authzrStub(authzr obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_AuthzrFreeContext, this.Invoke_AuthzrInitializeContextFromSid, this.Invoke_AuthzrInitializeCompoundContext, this.Invoke_AuthzrAccessCheck, this.Invoke_AuthzGetInformationFromContext, this.Invoke_AuthzrModifyClaims, this.Invoke_AuthzrModifySids};
		}
	}
}