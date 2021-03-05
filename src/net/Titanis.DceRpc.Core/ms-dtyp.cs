#pragma warning disable

namespace ms_dtyp
{
	using System;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct FILETIME : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwLowDateTime);
			encoder.WriteValue(this.dwHighDateTime);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.dwLowDateTime = decoder.ReadUInt32();
			this.dwHighDateTime = decoder.ReadUInt32();
		}
		public uint dwLowDateTime;
		public uint dwHighDateTime;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct GUID : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Data1);
			encoder.WriteValue(this.Data2);
			encoder.WriteValue(this.Data3);
			if ((this.Data4 == null))
			{
				this.Data4 = new byte[8];
			}
			for (int i = 0; (i < 8); i++
			)
			{
				byte elem_0 = this.Data4[i];
				encoder.WriteValue(elem_0);
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Data1 = decoder.ReadUInt32();
			this.Data2 = decoder.ReadUInt16();
			this.Data3 = decoder.ReadUInt16();
			if ((this.Data4 == null))
			{
				this.Data4 = new byte[8];
			}
			for (int i = 0; (i < 8); i++
			)
			{
				byte elem_0 = this.Data4[i];
				elem_0 = decoder.ReadByte();
				this.Data4[i] = elem_0;
			}
		}
		public uint Data1;
		public ushort Data2;
		public ushort Data3;
		public byte[] Data4;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct LARGE_INTEGER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.QuadPart);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.QuadPart = decoder.ReadInt64();
		}
		public long QuadPart;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct EVENT_DESCRIPTOR : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Id);
			encoder.WriteValue(this.Version);
			encoder.WriteValue(this.Channel);
			encoder.WriteValue(this.Level);
			encoder.WriteValue(this.Opcode);
			encoder.WriteValue(this.Task);
			encoder.WriteValue(this.Keyword);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Id = decoder.ReadUInt16();
			this.Version = decoder.ReadUnsignedChar();
			this.Channel = decoder.ReadUnsignedChar();
			this.Level = decoder.ReadUnsignedChar();
			this.Opcode = decoder.ReadUnsignedChar();
			this.Task = decoder.ReadUInt16();
			this.Keyword = decoder.ReadUInt64();
		}
		public ushort Id;
		public byte Version;
		public byte Channel;
		public byte Level;
		public byte Opcode;
		public ushort Task;
		public ulong Keyword;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct Unnamed_3 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.KernelTime);
			encoder.WriteValue(this.UserTime);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.KernelTime = decoder.ReadUInt32();
			this.UserTime = decoder.ReadUInt32();
		}
		public uint KernelTime;
		public uint UserTime;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct LUID : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.LowPart);
			encoder.WriteValue(this.HighPart);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.LowPart = decoder.ReadUInt32();
			this.HighPart = decoder.ReadInt32();
		}
		public uint LowPart;
		public int HighPart;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct MULTI_SZ : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.Value);
			encoder.WriteValue(this.nChar);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Value = decoder.ReadPointer<char>();
			this.nChar = decoder.ReadUInt32();
		}
		public RpcPointer<char> Value;
		public uint nChar;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Value))
			{
				encoder.WriteValue(this.Value.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Value))
			{
				this.Value.value = decoder.ReadWideChar();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct RPC_UNICODE_STRING : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteValue(this.MaximumLength);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt16();
			this.MaximumLength = decoder.ReadUInt16();
			this.Buffer = decoder.ReadPointer<ArraySegment<char>>();
		}
		public ushort Length;
		public ushort MaximumLength;
		public RpcPointer<ArraySegment<char>> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value, true);
				for (int i = 0; (i < this.Buffer.value.Count); i++
				)
				{
					char elem_0 = this.Buffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArraySegmentHeader<char>();
				for (int i = 0; (i < this.Buffer.value.Count); i++
				)
				{
					char elem_0 = this.Buffer.value.Item(i);
					elem_0 = decoder.ReadWideChar();
					this.Buffer.value.Item(i) = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_100 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv100_platform_id);
			encoder.WritePointer(this.sv100_name);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv100_platform_id = decoder.ReadUInt32();
			this.sv100_name = decoder.ReadPointer<string>();
		}
		public uint sv100_platform_id;
		public RpcPointer<string> sv100_name;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.sv100_name))
			{
				encoder.WriteWideCharString(this.sv100_name.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.sv100_name))
			{
				this.sv100_name.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_101 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv101_platform_id);
			encoder.WritePointer(this.sv101_name);
			encoder.WriteValue(this.sv101_version_major);
			encoder.WriteValue(this.sv101_version_minor);
			encoder.WriteValue(this.sv101_version_type);
			encoder.WritePointer(this.sv101_comment);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv101_platform_id = decoder.ReadUInt32();
			this.sv101_name = decoder.ReadPointer<string>();
			this.sv101_version_major = decoder.ReadUInt32();
			this.sv101_version_minor = decoder.ReadUInt32();
			this.sv101_version_type = decoder.ReadUInt32();
			this.sv101_comment = decoder.ReadPointer<string>();
		}
		public uint sv101_platform_id;
		public RpcPointer<string> sv101_name;
		public uint sv101_version_major;
		public uint sv101_version_minor;
		public uint sv101_version_type;
		public RpcPointer<string> sv101_comment;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.sv101_name))
			{
				encoder.WriteWideCharString(this.sv101_name.value);
			}
			if ((null != this.sv101_comment))
			{
				encoder.WriteWideCharString(this.sv101_comment.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.sv101_name))
			{
				this.sv101_name.value = decoder.ReadWideCharString();
			}
			if ((null != this.sv101_comment))
			{
				this.sv101_comment.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SYSTEMTIME : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wYear);
			encoder.WriteValue(this.wMonth);
			encoder.WriteValue(this.wDayOfWeek);
			encoder.WriteValue(this.wDay);
			encoder.WriteValue(this.wHour);
			encoder.WriteValue(this.wMinute);
			encoder.WriteValue(this.wSecond);
			encoder.WriteValue(this.wMilliseconds);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.wYear = decoder.ReadUInt16();
			this.wMonth = decoder.ReadUInt16();
			this.wDayOfWeek = decoder.ReadUInt16();
			this.wDay = decoder.ReadUInt16();
			this.wHour = decoder.ReadUInt16();
			this.wMinute = decoder.ReadUInt16();
			this.wSecond = decoder.ReadUInt16();
			this.wMilliseconds = decoder.ReadUInt16();
		}
		public ushort wYear;
		public ushort wMonth;
		public ushort wDayOfWeek;
		public ushort wDay;
		public ushort wHour;
		public ushort wMinute;
		public ushort wSecond;
		public ushort wMilliseconds;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct UINT128 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.lower);
			encoder.WriteValue(this.upper);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.lower = decoder.ReadUInt64();
			this.upper = decoder.ReadUInt64();
		}
		public ulong lower;
		public ulong upper;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct ULARGE_INTEGER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.QuadPart);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.QuadPart = decoder.ReadUInt64();
		}
		public ulong QuadPart;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct RPC_SID_IDENTIFIER_AUTHORITY : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((this.Value == null))
			{
				this.Value = new byte[6];
			}
			for (int i = 0; (i < 6); i++
			)
			{
				byte elem_0 = this.Value[i];
				encoder.WriteValue(elem_0);
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((this.Value == null))
			{
				this.Value = new byte[6];
			}
			for (int i = 0; (i < 6); i++
			)
			{
				byte elem_0 = this.Value[i];
				elem_0 = decoder.ReadByte();
				this.Value[i] = elem_0;
			}
		}
		public byte[] Value;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct OBJECT_TYPE_LIST : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteValue(this.Remaining);
			encoder.WritePointer(this.ObjectType);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt16();
			this.Remaining = decoder.ReadUInt32();
			this.ObjectType = decoder.ReadPointer<System.Guid>();
		}
		public ushort Level;
		public uint Remaining;
		public RpcPointer<System.Guid> ObjectType;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.ObjectType))
			{
				encoder.WriteValue(this.ObjectType.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.ObjectType))
			{
				this.ObjectType.value = decoder.ReadUuid();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct ACE_HEADER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.AceType);
			encoder.WriteValue(this.AceFlags);
			encoder.WriteValue(this.AceSize);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.AceType = decoder.ReadUnsignedChar();
			this.AceFlags = decoder.ReadUnsignedChar();
			this.AceSize = decoder.ReadUInt16();
		}
		public byte AceType;
		public byte AceFlags;
		public ushort AceSize;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SYSTEM_MANDATORY_LABEL_ACE : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Header, Titanis.DceRpc.NdrAlignment._2Byte);
			encoder.WriteValue(this.Mask);
			encoder.WriteValue(this.SidStart);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Header = decoder.ReadFixedStruct<ACE_HEADER>(Titanis.DceRpc.NdrAlignment._2Byte);
			this.Mask = decoder.ReadUInt32();
			this.SidStart = decoder.ReadUInt32();
		}
		public ACE_HEADER Header;
		public uint Mask;
		public uint SidStart;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Header);
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ACE_HEADER>(ref this.Header);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct TOKEN_MANDATORY_POLICY : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Policy);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Policy = decoder.ReadUInt32();
		}
		public uint Policy;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct MANDATORY_INFORMATION : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.AllowedAccess);
			encoder.WriteValue(this.WriteAllowed);
			encoder.WriteValue(this.ReadAllowed);
			encoder.WriteValue(this.ExecuteAllowed);
			encoder.WriteFixedStruct(this.MandatoryPolicy, Titanis.DceRpc.NdrAlignment._4Byte);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.AllowedAccess = decoder.ReadUInt32();
			this.WriteAllowed = decoder.ReadUnsignedChar();
			this.ReadAllowed = decoder.ReadUnsignedChar();
			this.ExecuteAllowed = decoder.ReadUnsignedChar();
			this.MandatoryPolicy = decoder.ReadFixedStruct<TOKEN_MANDATORY_POLICY>(Titanis.DceRpc.NdrAlignment._4Byte);
		}
		public uint AllowedAccess;
		public byte WriteAllowed;
		public byte ReadAllowed;
		public byte ExecuteAllowed;
		public TOKEN_MANDATORY_POLICY MandatoryPolicy;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.MandatoryPolicy);
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<TOKEN_MANDATORY_POLICY>(ref this.MandatoryPolicy);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct CLAIM_SECURITY_ATTRIBUTE_OCTET_STRING_RELATIVE : Titanis.DceRpc.IRpcConformantStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt32();
		}
		public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.OctetString);
		}
		public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.OctetString = decoder.ReadArrayHeader<byte>();
		}
		public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder)
		{
			for (int i = 0; (i < this.OctetString.Length); i++
			)
			{
				byte elem_0 = this.OctetString[i];
				encoder.WriteValue(elem_0);
			}
		}
		public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder)
		{
			for (int i = 0; (i < this.OctetString.Length); i++
			)
			{
				byte elem_0 = this.OctetString[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.OctetString[i] = elem_0;
			}
		}
		public uint Length;
		public byte[] OctetString;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct RPC_SID : Titanis.DceRpc.IRpcConformantStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Revision);
			encoder.WriteValue(this.SubAuthorityCount);
			encoder.WriteFixedStruct(this.IdentifierAuthority, Titanis.DceRpc.NdrAlignment._1Byte);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Revision = decoder.ReadUnsignedChar();
			this.SubAuthorityCount = decoder.ReadUnsignedChar();
			this.IdentifierAuthority = decoder.ReadFixedStruct<RPC_SID_IDENTIFIER_AUTHORITY>(Titanis.DceRpc.NdrAlignment._1Byte);
		}
		public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.SubAuthority);
		}
		public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.SubAuthority = decoder.ReadArrayHeader<uint>();
		}
		public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder)
		{
			for (int i = 0; (i < this.SubAuthority.Length); i++
			)
			{
				uint elem_0 = this.SubAuthority[i];
				encoder.WriteValue(elem_0);
			}
		}
		public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder)
		{
			for (int i = 0; (i < this.SubAuthority.Length); i++
			)
			{
				uint elem_0 = this.SubAuthority[i];
				elem_0 = decoder.ReadUInt32();
				this.SubAuthority[i] = elem_0;
			}
		}
		public byte Revision;
		public byte SubAuthorityCount;
		public RPC_SID_IDENTIFIER_AUTHORITY IdentifierAuthority;
		public uint[] SubAuthority;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.IdentifierAuthority);
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<RPC_SID_IDENTIFIER_AUTHORITY>(ref this.IdentifierAuthority);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct ACL : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.AclRevision);
			encoder.WriteValue(this.Sbz1);
			encoder.WriteValue(this.AclSize);
			encoder.WriteValue(this.AceCount);
			encoder.WriteValue(this.Sbz2);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.AclRevision = decoder.ReadUnsignedChar();
			this.Sbz1 = decoder.ReadUnsignedChar();
			this.AclSize = decoder.ReadUInt16();
			this.AceCount = decoder.ReadUInt16();
			this.Sbz2 = decoder.ReadUInt16();
		}
		public byte AclRevision;
		public byte Sbz1;
		public ushort AclSize;
		public ushort AceCount;
		public ushort Sbz2;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SECURITY_DESCRIPTOR : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Revision);
			encoder.WriteValue(this.Sbz1);
			encoder.WriteValue(this.Control);
			encoder.WritePointer(this.Owner);
			encoder.WritePointer(this.Group);
			encoder.WritePointer(this.Sacl);
			encoder.WritePointer(this.Dacl);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Revision = decoder.ReadUnsignedChar();
			this.Sbz1 = decoder.ReadUnsignedChar();
			this.Control = decoder.ReadUInt16();
			this.Owner = decoder.ReadPointer<RPC_SID>();
			this.Group = decoder.ReadPointer<RPC_SID>();
			this.Sacl = decoder.ReadPointer<ACL>();
			this.Dacl = decoder.ReadPointer<ACL>();
		}
		public byte Revision;
		public byte Sbz1;
		public ushort Control;
		public RpcPointer<RPC_SID> Owner;
		public RpcPointer<RPC_SID> Group;
		public RpcPointer<ACL> Sacl;
		public RpcPointer<ACL> Dacl;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Owner))
			{
				encoder.WriteConformantStruct(this.Owner.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Owner.value);
			}
			if ((null != this.Group))
			{
				encoder.WriteConformantStruct(this.Group.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Group.value);
			}
			if ((null != this.Sacl))
			{
				encoder.WriteFixedStruct(this.Sacl.value, Titanis.DceRpc.NdrAlignment._2Byte);
				encoder.WriteStructDeferral(this.Sacl.value);
			}
			if ((null != this.Dacl))
			{
				encoder.WriteFixedStruct(this.Dacl.value, Titanis.DceRpc.NdrAlignment._2Byte);
				encoder.WriteStructDeferral(this.Dacl.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Owner))
			{
				this.Owner.value = decoder.ReadConformantStruct<RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<RPC_SID>(ref this.Owner.value);
			}
			if ((null != this.Group))
			{
				this.Group.value = decoder.ReadConformantStruct<RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<RPC_SID>(ref this.Group.value);
			}
			if ((null != this.Sacl))
			{
				this.Sacl.value = decoder.ReadFixedStruct<ACL>(Titanis.DceRpc.NdrAlignment._2Byte);
				decoder.ReadStructDeferral<ACL>(ref this.Sacl.value);
			}
			if ((null != this.Dacl))
			{
				this.Dacl.value = decoder.ReadFixedStruct<ACL>(Titanis.DceRpc.NdrAlignment._2Byte);
				decoder.ReadStructDeferral<ACL>(ref this.Dacl.value);
			}
		}
	}
}
