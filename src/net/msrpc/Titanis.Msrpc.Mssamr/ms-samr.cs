namespace ms_samr
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct RPC_STRING : IRpcFixedStruct
	{
		public ushort Length;
		public ushort MaximumLength;
		public RpcPointer<ArraySegment<byte>> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteValue(this.MaximumLength);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt16();
			this.MaximumLength = decoder.ReadUInt16();
			this.Buffer = decoder.ReadUniquePointer<ArraySegment<byte>>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value, true);
				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					byte elem_0 = this.Buffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					byte elem_0 = this.Buffer.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					this.Buffer.value.Item(i) = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct OLD_LARGE_INTEGER : IRpcFixedStruct
	{
		public uint LowPart;
		public int HighPart;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.LowPart);
			encoder.WriteValue(this.HighPart);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LowPart = decoder.ReadUInt32();
			this.HighPart = decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ENCRYPTED_LM_OWF_PASSWORD : IRpcFixedStruct
	{
		public byte[] data;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			if (this.data == null)
				this.data = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.data[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			if (this.data == null)
				this.data = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.data[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.data[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_ULONG_ARRAY : IRpcFixedStruct
	{
		public uint Count;
		public RpcPointer<uint[]> Element;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Count);
			encoder.WriteUniquePointer(this.Element);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Count = decoder.ReadUInt32();
			this.Element = decoder.ReadUniquePointer<uint[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Element is not null)
			{
				encoder.WriteArrayHeader(this.Element.value);
				for (int i = 0; i < this.Element.value.Length; i++)
				{
					uint elem_0 = this.Element.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Element is not null)
			{
				this.Element.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < this.Element.value.Length; i++)
				{
					uint elem_0 = this.Element.value[i];
					elem_0 = decoder.ReadUInt32();
					this.Element.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_SID_INFORMATION : IRpcFixedStruct
	{
		public RpcPointer<ms_dtyp.RPC_SID> SidPointer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.SidPointer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.SidPointer = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.SidPointer is not null)
			{
				encoder.WriteConformantStruct(this.SidPointer.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.SidPointer.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.SidPointer is not null)
			{
				this.SidPointer.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.SidPointer.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_PSID_ARRAY : IRpcFixedStruct
	{
		public uint Count;
		public RpcPointer<SAMPR_SID_INFORMATION[]> Sids;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Count);
			encoder.WriteUniquePointer(this.Sids);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Count = decoder.ReadUInt32();
			this.Sids = decoder.ReadUniquePointer<SAMPR_SID_INFORMATION[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Sids is not null)
			{
				encoder.WriteArrayHeader(this.Sids.value);
				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Sids is not null)
			{
				this.Sids.value = decoder.ReadArrayHeader<SAMPR_SID_INFORMATION>();
				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
					elem_0 = decoder.ReadFixedStruct<SAMPR_SID_INFORMATION>(NdrAlignment.NativePtr);
					this.Sids.value[i] = elem_0;
				}

				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
					decoder.ReadStructDeferral<SAMPR_SID_INFORMATION>(ref elem_0);
					this.Sids.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_PSID_ARRAY_OUT : IRpcFixedStruct
	{
		public uint Count;
		public RpcPointer<SAMPR_SID_INFORMATION[]> Sids;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Count);
			encoder.WriteUniquePointer(this.Sids);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Count = decoder.ReadUInt32();
			this.Sids = decoder.ReadUniquePointer<SAMPR_SID_INFORMATION[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Sids is not null)
			{
				encoder.WriteArrayHeader(this.Sids.value);
				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Sids is not null)
			{
				this.Sids.value = decoder.ReadArrayHeader<SAMPR_SID_INFORMATION>();
				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
					elem_0 = decoder.ReadFixedStruct<SAMPR_SID_INFORMATION>(NdrAlignment.NativePtr);
					this.Sids.value[i] = elem_0;
				}

				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
					decoder.ReadStructDeferral<SAMPR_SID_INFORMATION>(ref elem_0);
					this.Sids.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_RETURNED_USTRING_ARRAY : IRpcFixedStruct
	{
		public uint Count;
		public RpcPointer<ms_dtyp.RPC_UNICODE_STRING[]> Element;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Count);
			encoder.WriteUniquePointer(this.Element);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Count = decoder.ReadUInt32();
			this.Element = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Element is not null)
			{
				encoder.WriteArrayHeader(this.Element.value);
				for (int i = 0; i < this.Element.value.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Element.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Element.value.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Element.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Element is not null)
			{
				this.Element.value = decoder.ReadArrayHeader<ms_dtyp.RPC_UNICODE_STRING>();
				for (int i = 0; i < this.Element.value.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Element.value[i];
					elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
					this.Element.value[i] = elem_0;
				}

				for (int i = 0; i < this.Element.value.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Element.value[i];
					decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
					this.Element.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum SID_NAME_USE : int
	{
		SidTypeUser = 1,
		SidTypeGroup = 2,
		SidTypeDomain = 3,
		SidTypeAlias = 4,
		SidTypeWellKnownGroup = 5,
		SidTypeDeletedAccount = 6,
		SidTypeInvalid = 7,
		SidTypeUnknown = 8,
		SidTypeComputer = 9,
		SidTypeLabel = 10
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct RPC_SHORT_BLOB : IRpcFixedStruct
	{
		public ushort Length;
		public ushort MaximumLength;
		public RpcPointer<ArraySegment<ushort>> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteValue(this.MaximumLength);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt16();
			this.MaximumLength = decoder.ReadUInt16();
			this.Buffer = decoder.ReadUniquePointer<ArraySegment<ushort>>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value, true);
				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					ushort elem_0 = this.Buffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArraySegmentHeader<ushort>();
				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					ushort elem_0 = this.Buffer.value.Item(i);
					elem_0 = decoder.ReadUInt16();
					this.Buffer.value.Item(i) = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_RID_ENUMERATION : IRpcFixedStruct
	{
		public uint RelativeId;
		public ms_dtyp.RPC_UNICODE_STRING Name;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.RelativeId);
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.RelativeId = decoder.ReadUInt32();
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_ENUMERATION_BUFFER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SAMPR_RID_ENUMERATION[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SAMPR_RID_ENUMERATION[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_RID_ENUMERATION elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_RID_ENUMERATION elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SAMPR_RID_ENUMERATION>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_RID_ENUMERATION elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SAMPR_RID_ENUMERATION>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_RID_ENUMERATION elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SAMPR_RID_ENUMERATION>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_SR_SECURITY_DESCRIPTOR : IRpcFixedStruct
	{
		public uint Length;
		public RpcPointer<byte[]> SecurityDescriptor;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteUniquePointer(this.SecurityDescriptor);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt32();
			this.SecurityDescriptor = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.SecurityDescriptor is not null)
			{
				encoder.WriteArrayHeader(this.SecurityDescriptor.value);
				for (int i = 0; i < this.SecurityDescriptor.value.Length; i++)
				{
					byte elem_0 = this.SecurityDescriptor.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.SecurityDescriptor is not null)
			{
				this.SecurityDescriptor.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.SecurityDescriptor.value.Length; i++)
				{
					byte elem_0 = this.SecurityDescriptor.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.SecurityDescriptor.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct GROUP_MEMBERSHIP : IRpcFixedStruct
	{
		public uint RelativeId;
		public uint Attributes;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.RelativeId);
			encoder.WriteValue(this.Attributes);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.RelativeId = decoder.ReadUInt32();
			this.Attributes = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_GET_GROUPS_BUFFER : IRpcFixedStruct
	{
		public uint MembershipCount;
		public RpcPointer<GROUP_MEMBERSHIP[]> Groups;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.MembershipCount);
			encoder.WriteUniquePointer(this.Groups);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.MembershipCount = decoder.ReadUInt32();
			this.Groups = decoder.ReadUniquePointer<GROUP_MEMBERSHIP[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Groups is not null)
			{
				encoder.WriteArrayHeader(this.Groups.value);
				for (int i = 0; i < this.Groups.value.Length; i++)
				{
					GROUP_MEMBERSHIP elem_0 = this.Groups.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.Groups.value.Length; i++)
				{
					GROUP_MEMBERSHIP elem_0 = this.Groups.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Groups is not null)
			{
				this.Groups.value = decoder.ReadArrayHeader<GROUP_MEMBERSHIP>();
				for (int i = 0; i < this.Groups.value.Length; i++)
				{
					GROUP_MEMBERSHIP elem_0 = this.Groups.value[i];
					elem_0 = decoder.ReadFixedStruct<GROUP_MEMBERSHIP>(NdrAlignment._4Byte);
					this.Groups.value[i] = elem_0;
				}

				for (int i = 0; i < this.Groups.value.Length; i++)
				{
					GROUP_MEMBERSHIP elem_0 = this.Groups.value[i];
					decoder.ReadStructDeferral<GROUP_MEMBERSHIP>(ref elem_0);
					this.Groups.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_GET_MEMBERS_BUFFER : IRpcFixedStruct
	{
		public uint MemberCount;
		public RpcPointer<uint[]> Members;
		public RpcPointer<uint[]> Attributes;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.MemberCount);
			encoder.WriteUniquePointer(this.Members);
			encoder.WriteUniquePointer(this.Attributes);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.MemberCount = decoder.ReadUInt32();
			this.Members = decoder.ReadUniquePointer<uint[]>();
			this.Attributes = decoder.ReadUniquePointer<uint[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Members is not null)
			{
				encoder.WriteArrayHeader(this.Members.value);
				for (int i = 0; i < this.Members.value.Length; i++)
				{
					uint elem_0 = this.Members.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.Attributes is not null)
			{
				encoder.WriteArrayHeader(this.Attributes.value);
				for (int i = 0; i < this.Attributes.value.Length; i++)
				{
					uint elem_0 = this.Attributes.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Members is not null)
			{
				this.Members.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < this.Members.value.Length; i++)
				{
					uint elem_0 = this.Members.value[i];
					elem_0 = decoder.ReadUInt32();
					this.Members.value[i] = elem_0;
				}
			}

			if (this.Attributes is not null)
			{
				this.Attributes.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < this.Attributes.value.Length; i++)
				{
					uint elem_0 = this.Attributes.value[i];
					elem_0 = decoder.ReadUInt32();
					this.Attributes.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_REVISION_INFO_V1 : IRpcFixedStruct
	{
		public uint Revision;
		public uint SupportedFeatures;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Revision);
			encoder.WriteValue(this.SupportedFeatures);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Revision = decoder.ReadUInt32();
			this.SupportedFeatures = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_REVISION_INFO : IRpcFixedStruct
	{
		public uint unionSwitch;
		public SAMPR_REVISION_INFO_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._4Byte);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment._4Byte);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._4Byte);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<SAMPR_REVISION_INFO_V1>(NdrAlignment._4Byte);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteStructDeferral(this.V1);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					decoder.ReadStructDeferral<SAMPR_REVISION_INFO_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct USER_DOMAIN_PASSWORD_INFORMATION : IRpcFixedStruct
	{
		public ushort MinPasswordLength;
		public uint PasswordProperties;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.MinPasswordLength);
			encoder.WriteValue(this.PasswordProperties);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.MinPasswordLength = decoder.ReadUInt16();
			this.PasswordProperties = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum DOMAIN_SERVER_ENABLE_STATE : int
	{
		DomainServerEnabled = 1,
		DomainServerDisabled = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DOMAIN_STATE_INFORMATION : IRpcFixedStruct
	{
		public DOMAIN_SERVER_ENABLE_STATE DomainServerState;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.DomainServerState);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.DomainServerState = (DOMAIN_SERVER_ENABLE_STATE)decoder.ReadEnumShortValue();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum DOMAIN_SERVER_ROLE : int
	{
		DomainServerRoleBackup = 2,
		DomainServerRolePrimary = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DOMAIN_PASSWORD_INFORMATION : IRpcFixedStruct
	{
		public ushort MinPasswordLength;
		public ushort PasswordHistoryLength;
		public uint PasswordProperties;
		public OLD_LARGE_INTEGER MaxPasswordAge;
		public OLD_LARGE_INTEGER MinPasswordAge;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.MinPasswordLength);
			encoder.WriteValue(this.PasswordHistoryLength);
			encoder.WriteValue(this.PasswordProperties);
			encoder.WriteFixedStruct(this.MaxPasswordAge, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.MinPasswordAge, NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.MinPasswordLength = decoder.ReadUInt16();
			this.PasswordHistoryLength = decoder.ReadUInt16();
			this.PasswordProperties = decoder.ReadUInt32();
			this.MaxPasswordAge = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.MinPasswordAge = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.MaxPasswordAge);
			encoder.WriteStructDeferral(this.MinPasswordAge);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.MaxPasswordAge);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.MinPasswordAge);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DOMAIN_LOGOFF_INFORMATION : IRpcFixedStruct
	{
		public OLD_LARGE_INTEGER ForceLogoff;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ForceLogoff, NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ForceLogoff = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ForceLogoff);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.ForceLogoff);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DOMAIN_SERVER_ROLE_INFORMATION : IRpcFixedStruct
	{
		public DOMAIN_SERVER_ROLE DomainServerRole;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.DomainServerRole);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.DomainServerRole = (DOMAIN_SERVER_ROLE)decoder.ReadEnumShortValue();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DOMAIN_MODIFIED_INFORMATION : IRpcFixedStruct
	{
		public OLD_LARGE_INTEGER DomainModifiedCount;
		public OLD_LARGE_INTEGER CreationTime;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.DomainModifiedCount, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.CreationTime, NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.DomainModifiedCount = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.CreationTime = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.DomainModifiedCount);
			encoder.WriteStructDeferral(this.CreationTime);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.DomainModifiedCount);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.CreationTime);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DOMAIN_MODIFIED_INFORMATION2 : IRpcFixedStruct
	{
		public OLD_LARGE_INTEGER DomainModifiedCount;
		public OLD_LARGE_INTEGER CreationTime;
		public OLD_LARGE_INTEGER ModifiedCountAtLastPromotion;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.DomainModifiedCount, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.CreationTime, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.ModifiedCountAtLastPromotion, NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.DomainModifiedCount = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.CreationTime = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.ModifiedCountAtLastPromotion = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.DomainModifiedCount);
			encoder.WriteStructDeferral(this.CreationTime);
			encoder.WriteStructDeferral(this.ModifiedCountAtLastPromotion);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.DomainModifiedCount);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.CreationTime);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.ModifiedCountAtLastPromotion);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_GENERAL_INFORMATION : IRpcFixedStruct
	{
		public OLD_LARGE_INTEGER ForceLogoff;
		public ms_dtyp.RPC_UNICODE_STRING OemInformation;
		public ms_dtyp.RPC_UNICODE_STRING DomainName;
		public ms_dtyp.RPC_UNICODE_STRING ReplicaSourceNodeName;
		public OLD_LARGE_INTEGER DomainModifiedCount;
		public uint DomainServerState;
		public uint DomainServerRole;
		public byte UasCompatibilityRequired;
		public uint UserCount;
		public uint GroupCount;
		public uint AliasCount;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ForceLogoff, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.OemInformation, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.DomainName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.ReplicaSourceNodeName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.DomainModifiedCount, NdrAlignment._4Byte);
			encoder.WriteValue(this.DomainServerState);
			encoder.WriteValue(this.DomainServerRole);
			encoder.WriteValue(this.UasCompatibilityRequired);
			encoder.WriteValue(this.UserCount);
			encoder.WriteValue(this.GroupCount);
			encoder.WriteValue(this.AliasCount);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ForceLogoff = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.OemInformation = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.DomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.ReplicaSourceNodeName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.DomainModifiedCount = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.DomainServerState = decoder.ReadUInt32();
			this.DomainServerRole = decoder.ReadUInt32();
			this.UasCompatibilityRequired = decoder.ReadUnsignedChar();
			this.UserCount = decoder.ReadUInt32();
			this.GroupCount = decoder.ReadUInt32();
			this.AliasCount = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ForceLogoff);
			encoder.WriteStructDeferral(this.OemInformation);
			encoder.WriteStructDeferral(this.DomainName);
			encoder.WriteStructDeferral(this.ReplicaSourceNodeName);
			encoder.WriteStructDeferral(this.DomainModifiedCount);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.ForceLogoff);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.OemInformation);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.DomainName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ReplicaSourceNodeName);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.DomainModifiedCount);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_GENERAL_INFORMATION2 : IRpcFixedStruct
	{
		public SAMPR_DOMAIN_GENERAL_INFORMATION I1;
		public ms_dtyp.LARGE_INTEGER LockoutDuration;
		public ms_dtyp.LARGE_INTEGER LockoutObservationWindow;
		public ushort LockoutThreshold;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.I1, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LockoutDuration, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.LockoutObservationWindow, NdrAlignment._8Byte);
			encoder.WriteValue(this.LockoutThreshold);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.I1 = decoder.ReadFixedStruct<SAMPR_DOMAIN_GENERAL_INFORMATION>(NdrAlignment.NativePtr);
			this.LockoutDuration = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.LockoutObservationWindow = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.LockoutThreshold = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.I1);
			encoder.WriteStructDeferral(this.LockoutDuration);
			encoder.WriteStructDeferral(this.LockoutObservationWindow);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SAMPR_DOMAIN_GENERAL_INFORMATION>(ref this.I1);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LockoutDuration);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LockoutObservationWindow);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_OEM_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING OemInformation;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.OemInformation, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.OemInformation = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.OemInformation);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.OemInformation);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_NAME_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING DomainName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.DomainName, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.DomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.DomainName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.DomainName);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_REPLICATION_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING ReplicaSourceNodeName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ReplicaSourceNodeName, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ReplicaSourceNodeName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ReplicaSourceNodeName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ReplicaSourceNodeName);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_LOCKOUT_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.LARGE_INTEGER LockoutDuration;
		public ms_dtyp.LARGE_INTEGER LockoutObservationWindow;
		public ushort LockoutThreshold;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LockoutDuration, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.LockoutObservationWindow, NdrAlignment._8Byte);
			encoder.WriteValue(this.LockoutThreshold);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LockoutDuration = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.LockoutObservationWindow = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.LockoutThreshold = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LockoutDuration);
			encoder.WriteStructDeferral(this.LockoutObservationWindow);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LockoutDuration);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LockoutObservationWindow);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum DOMAIN_INFORMATION_CLASS : int
	{
		DomainPasswordInformation = 1,
		DomainGeneralInformation = 2,
		DomainLogoffInformation = 3,
		DomainOemInformation = 4,
		DomainNameInformation = 5,
		DomainReplicationInformation = 6,
		DomainServerRoleInformation = 7,
		DomainModifiedInformation = 8,
		DomainStateInformation = 9,
		DomainGeneralInformation2 = 11,
		DomainLockoutInformation = 12,
		DomainModifiedInformation2 = 13
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_INFO_BUFFER : IRpcFixedStruct
	{
		public DOMAIN_INFORMATION_CLASS unionSwitch;
		public DOMAIN_PASSWORD_INFORMATION Password;
		public SAMPR_DOMAIN_GENERAL_INFORMATION General;
		public DOMAIN_LOGOFF_INFORMATION Logoff;
		public SAMPR_DOMAIN_OEM_INFORMATION Oem;
		public SAMPR_DOMAIN_NAME_INFORMATION Name;
		public DOMAIN_SERVER_ROLE_INFORMATION Role;
		public SAMPR_DOMAIN_REPLICATION_INFORMATION Replication;
		public DOMAIN_MODIFIED_INFORMATION Modified;
		public DOMAIN_STATE_INFORMATION State;
		public SAMPR_DOMAIN_GENERAL_INFORMATION2 General2;
		public SAMPR_DOMAIN_LOCKOUT_INFORMATION Lockout;
		public DOMAIN_MODIFIED_INFORMATION2 Modified2;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._8Byte);
			encoder.WriteEnumShortValue((short)this.unionSwitch);
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteFixedStruct(this.Password, NdrAlignment._4Byte);
					break;
				case 2:
					encoder.WriteFixedStruct(this.General, NdrAlignment.NativePtr);
					break;
				case 3:
					encoder.WriteFixedStruct(this.Logoff, NdrAlignment._4Byte);
					break;
				case 4:
					encoder.WriteFixedStruct(this.Oem, NdrAlignment.NativePtr);
					break;
				case 5:
					encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
					break;
				case 7:
					encoder.WriteFixedStruct(this.Role, NdrAlignment.ShortEnum);
					break;
				case 6:
					encoder.WriteFixedStruct(this.Replication, NdrAlignment.NativePtr);
					break;
				case 8:
					encoder.WriteFixedStruct(this.Modified, NdrAlignment._4Byte);
					break;
				case 9:
					encoder.WriteFixedStruct(this.State, NdrAlignment.ShortEnum);
					break;
				case 11:
					encoder.WriteFixedStruct(this.General2, NdrAlignment._8Byte);
					break;
				case 12:
					encoder.WriteFixedStruct(this.Lockout, NdrAlignment._8Byte);
					break;
				case 13:
					encoder.WriteFixedStruct(this.Modified2, NdrAlignment._4Byte);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._8Byte);
			this.unionSwitch = (DOMAIN_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			switch ((int)this.unionSwitch)
			{
				case 1:
					this.Password = decoder.ReadFixedStruct<DOMAIN_PASSWORD_INFORMATION>(NdrAlignment._4Byte);
					break;
				case 2:
					this.General = decoder.ReadFixedStruct<SAMPR_DOMAIN_GENERAL_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 3:
					this.Logoff = decoder.ReadFixedStruct<DOMAIN_LOGOFF_INFORMATION>(NdrAlignment._4Byte);
					break;
				case 4:
					this.Oem = decoder.ReadFixedStruct<SAMPR_DOMAIN_OEM_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 5:
					this.Name = decoder.ReadFixedStruct<SAMPR_DOMAIN_NAME_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 7:
					this.Role = decoder.ReadFixedStruct<DOMAIN_SERVER_ROLE_INFORMATION>(NdrAlignment.ShortEnum);
					break;
				case 6:
					this.Replication = decoder.ReadFixedStruct<SAMPR_DOMAIN_REPLICATION_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 8:
					this.Modified = decoder.ReadFixedStruct<DOMAIN_MODIFIED_INFORMATION>(NdrAlignment._4Byte);
					break;
				case 9:
					this.State = decoder.ReadFixedStruct<DOMAIN_STATE_INFORMATION>(NdrAlignment.ShortEnum);
					break;
				case 11:
					this.General2 = decoder.ReadFixedStruct<SAMPR_DOMAIN_GENERAL_INFORMATION2>(NdrAlignment._8Byte);
					break;
				case 12:
					this.Lockout = decoder.ReadFixedStruct<SAMPR_DOMAIN_LOCKOUT_INFORMATION>(NdrAlignment._8Byte);
					break;
				case 13:
					this.Modified2 = decoder.ReadFixedStruct<DOMAIN_MODIFIED_INFORMATION2>(NdrAlignment._4Byte);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteStructDeferral(this.Password);
					break;
				case 2:
					encoder.WriteStructDeferral(this.General);
					break;
				case 3:
					encoder.WriteStructDeferral(this.Logoff);
					break;
				case 4:
					encoder.WriteStructDeferral(this.Oem);
					break;
				case 5:
					encoder.WriteStructDeferral(this.Name);
					break;
				case 7:
					encoder.WriteStructDeferral(this.Role);
					break;
				case 6:
					encoder.WriteStructDeferral(this.Replication);
					break;
				case 8:
					encoder.WriteStructDeferral(this.Modified);
					break;
				case 9:
					encoder.WriteStructDeferral(this.State);
					break;
				case 11:
					encoder.WriteStructDeferral(this.General2);
					break;
				case 12:
					encoder.WriteStructDeferral(this.Lockout);
					break;
				case 13:
					encoder.WriteStructDeferral(this.Modified2);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					decoder.ReadStructDeferral<DOMAIN_PASSWORD_INFORMATION>(ref this.Password);
					break;
				case 2:
					decoder.ReadStructDeferral<SAMPR_DOMAIN_GENERAL_INFORMATION>(ref this.General);
					break;
				case 3:
					decoder.ReadStructDeferral<DOMAIN_LOGOFF_INFORMATION>(ref this.Logoff);
					break;
				case 4:
					decoder.ReadStructDeferral<SAMPR_DOMAIN_OEM_INFORMATION>(ref this.Oem);
					break;
				case 5:
					decoder.ReadStructDeferral<SAMPR_DOMAIN_NAME_INFORMATION>(ref this.Name);
					break;
				case 7:
					decoder.ReadStructDeferral<DOMAIN_SERVER_ROLE_INFORMATION>(ref this.Role);
					break;
				case 6:
					decoder.ReadStructDeferral<SAMPR_DOMAIN_REPLICATION_INFORMATION>(ref this.Replication);
					break;
				case 8:
					decoder.ReadStructDeferral<DOMAIN_MODIFIED_INFORMATION>(ref this.Modified);
					break;
				case 9:
					decoder.ReadStructDeferral<DOMAIN_STATE_INFORMATION>(ref this.State);
					break;
				case 11:
					decoder.ReadStructDeferral<SAMPR_DOMAIN_GENERAL_INFORMATION2>(ref this.General2);
					break;
				case 12:
					decoder.ReadStructDeferral<SAMPR_DOMAIN_LOCKOUT_INFORMATION>(ref this.Lockout);
					break;
				case 13:
					decoder.ReadStructDeferral<DOMAIN_MODIFIED_INFORMATION2>(ref this.Modified2);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum DOMAIN_DISPLAY_INFORMATION : int
	{
		DomainDisplayUser = 1,
		DomainDisplayMachine = 2,
		DomainDisplayGroup = 3,
		DomainDisplayOemUser = 4,
		DomainDisplayOemGroup = 5
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_DISPLAY_USER : IRpcFixedStruct
	{
		public uint Index;
		public uint Rid;
		public uint AccountControl;
		public ms_dtyp.RPC_UNICODE_STRING AccountName;
		public ms_dtyp.RPC_UNICODE_STRING AdminComment;
		public ms_dtyp.RPC_UNICODE_STRING FullName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Index);
			encoder.WriteValue(this.Rid);
			encoder.WriteValue(this.AccountControl);
			encoder.WriteFixedStruct(this.AccountName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.FullName, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Index = decoder.ReadUInt32();
			this.Rid = decoder.ReadUInt32();
			this.AccountControl = decoder.ReadUInt32();
			this.AccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.AccountName);
			encoder.WriteStructDeferral(this.AdminComment);
			encoder.WriteStructDeferral(this.FullName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AccountName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_DISPLAY_MACHINE : IRpcFixedStruct
	{
		public uint Index;
		public uint Rid;
		public uint AccountControl;
		public ms_dtyp.RPC_UNICODE_STRING AccountName;
		public ms_dtyp.RPC_UNICODE_STRING AdminComment;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Index);
			encoder.WriteValue(this.Rid);
			encoder.WriteValue(this.AccountControl);
			encoder.WriteFixedStruct(this.AccountName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Index = decoder.ReadUInt32();
			this.Rid = decoder.ReadUInt32();
			this.AccountControl = decoder.ReadUInt32();
			this.AccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.AccountName);
			encoder.WriteStructDeferral(this.AdminComment);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AccountName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_DISPLAY_GROUP : IRpcFixedStruct
	{
		public uint Index;
		public uint Rid;
		public uint Attributes;
		public ms_dtyp.RPC_UNICODE_STRING AccountName;
		public ms_dtyp.RPC_UNICODE_STRING AdminComment;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Index);
			encoder.WriteValue(this.Rid);
			encoder.WriteValue(this.Attributes);
			encoder.WriteFixedStruct(this.AccountName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Index = decoder.ReadUInt32();
			this.Rid = decoder.ReadUInt32();
			this.Attributes = decoder.ReadUInt32();
			this.AccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.AccountName);
			encoder.WriteStructDeferral(this.AdminComment);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AccountName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_DISPLAY_OEM_USER : IRpcFixedStruct
	{
		public uint Index;
		public RPC_STRING OemAccountName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Index);
			encoder.WriteFixedStruct(this.OemAccountName, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Index = decoder.ReadUInt32();
			this.OemAccountName = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.OemAccountName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<RPC_STRING>(ref this.OemAccountName);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_DISPLAY_OEM_GROUP : IRpcFixedStruct
	{
		public uint Index;
		public RPC_STRING OemAccountName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Index);
			encoder.WriteFixedStruct(this.OemAccountName, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Index = decoder.ReadUInt32();
			this.OemAccountName = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.OemAccountName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<RPC_STRING>(ref this.OemAccountName);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_DISPLAY_USER_BUFFER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SAMPR_DOMAIN_DISPLAY_USER[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SAMPR_DOMAIN_DISPLAY_USER[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_USER elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_USER elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SAMPR_DOMAIN_DISPLAY_USER>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_USER elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_USER>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_USER elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_USER>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_DISPLAY_MACHINE_BUFFER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SAMPR_DOMAIN_DISPLAY_MACHINE[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SAMPR_DOMAIN_DISPLAY_MACHINE[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_MACHINE elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_MACHINE elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SAMPR_DOMAIN_DISPLAY_MACHINE>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_MACHINE elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_MACHINE>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_MACHINE elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_MACHINE>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_DISPLAY_GROUP_BUFFER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SAMPR_DOMAIN_DISPLAY_GROUP[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SAMPR_DOMAIN_DISPLAY_GROUP[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_GROUP elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_GROUP elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SAMPR_DOMAIN_DISPLAY_GROUP>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_GROUP elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_GROUP>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_GROUP elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_GROUP>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_DISPLAY_OEM_USER_BUFFER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SAMPR_DOMAIN_DISPLAY_OEM_USER[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SAMPR_DOMAIN_DISPLAY_OEM_USER[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_OEM_USER elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_OEM_USER elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SAMPR_DOMAIN_DISPLAY_OEM_USER>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_OEM_USER elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_OEM_USER>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_OEM_USER elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_OEM_USER>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DOMAIN_DISPLAY_OEM_GROUP_BUFFER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SAMPR_DOMAIN_DISPLAY_OEM_GROUP[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SAMPR_DOMAIN_DISPLAY_OEM_GROUP[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_OEM_GROUP elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_OEM_GROUP elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SAMPR_DOMAIN_DISPLAY_OEM_GROUP>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_OEM_GROUP elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_OEM_GROUP>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SAMPR_DOMAIN_DISPLAY_OEM_GROUP elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_OEM_GROUP>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_DISPLAY_INFO_BUFFER : IRpcFixedStruct
	{
		public DOMAIN_DISPLAY_INFORMATION unionSwitch;
		public SAMPR_DOMAIN_DISPLAY_USER_BUFFER UserInformation;
		public SAMPR_DOMAIN_DISPLAY_MACHINE_BUFFER MachineInformation;
		public SAMPR_DOMAIN_DISPLAY_GROUP_BUFFER GroupInformation;
		public SAMPR_DOMAIN_DISPLAY_OEM_USER_BUFFER OemUserInformation;
		public SAMPR_DOMAIN_DISPLAY_OEM_GROUP_BUFFER OemGroupInformation;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.unionSwitch);
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteFixedStruct(this.UserInformation, NdrAlignment.NativePtr);
					break;
				case 2:
					encoder.WriteFixedStruct(this.MachineInformation, NdrAlignment.NativePtr);
					break;
				case 3:
					encoder.WriteFixedStruct(this.GroupInformation, NdrAlignment.NativePtr);
					break;
				case 4:
					encoder.WriteFixedStruct(this.OemUserInformation, NdrAlignment.NativePtr);
					break;
				case 5:
					encoder.WriteFixedStruct(this.OemGroupInformation, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = (DOMAIN_DISPLAY_INFORMATION)decoder.ReadEnumShortValue();
			switch ((int)this.unionSwitch)
			{
				case 1:
					this.UserInformation = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_USER_BUFFER>(NdrAlignment.NativePtr);
					break;
				case 2:
					this.MachineInformation = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_MACHINE_BUFFER>(NdrAlignment.NativePtr);
					break;
				case 3:
					this.GroupInformation = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_GROUP_BUFFER>(NdrAlignment.NativePtr);
					break;
				case 4:
					this.OemUserInformation = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_OEM_USER_BUFFER>(NdrAlignment.NativePtr);
					break;
				case 5:
					this.OemGroupInformation = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_OEM_GROUP_BUFFER>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteStructDeferral(this.UserInformation);
					break;
				case 2:
					encoder.WriteStructDeferral(this.MachineInformation);
					break;
				case 3:
					encoder.WriteStructDeferral(this.GroupInformation);
					break;
				case 4:
					encoder.WriteStructDeferral(this.OemUserInformation);
					break;
				case 5:
					encoder.WriteStructDeferral(this.OemGroupInformation);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_USER_BUFFER>(ref this.UserInformation);
					break;
				case 2:
					decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_MACHINE_BUFFER>(ref this.MachineInformation);
					break;
				case 3:
					decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_GROUP_BUFFER>(ref this.GroupInformation);
					break;
				case 4:
					decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_OEM_USER_BUFFER>(ref this.OemUserInformation);
					break;
				case 5:
					decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_OEM_GROUP_BUFFER>(ref this.OemGroupInformation);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct GROUP_ATTRIBUTE_INFORMATION : IRpcFixedStruct
	{
		public uint Attributes;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Attributes);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Attributes = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_GROUP_GENERAL_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Name;
		public uint Attributes;
		public uint MemberCount;
		public ms_dtyp.RPC_UNICODE_STRING AdminComment;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
			encoder.WriteValue(this.Attributes);
			encoder.WriteValue(this.MemberCount);
			encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.Attributes = decoder.ReadUInt32();
			this.MemberCount = decoder.ReadUInt32();
			this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
			encoder.WriteStructDeferral(this.AdminComment);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_GROUP_NAME_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Name;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_GROUP_ADM_COMMENT_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING AdminComment;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.AdminComment);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum GROUP_INFORMATION_CLASS : int
	{
		GroupGeneralInformation = 1,
		GroupNameInformation = 2,
		GroupAttributeInformation = 3,
		GroupAdminCommentInformation = 4,
		GroupReplicationInformation = 5
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_GROUP_INFO_BUFFER : IRpcFixedStruct
	{
		public GROUP_INFORMATION_CLASS unionSwitch;
		public SAMPR_GROUP_GENERAL_INFORMATION General;
		public SAMPR_GROUP_NAME_INFORMATION Name;
		public GROUP_ATTRIBUTE_INFORMATION Attribute;
		public SAMPR_GROUP_ADM_COMMENT_INFORMATION AdminComment;
		public SAMPR_GROUP_GENERAL_INFORMATION DoNotUse;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.unionSwitch);
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteFixedStruct(this.General, NdrAlignment.NativePtr);
					break;
				case 2:
					encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
					break;
				case 3:
					encoder.WriteFixedStruct(this.Attribute, NdrAlignment._4Byte);
					break;
				case 4:
					encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
					break;
				case 5:
					encoder.WriteFixedStruct(this.DoNotUse, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = (GROUP_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			switch ((int)this.unionSwitch)
			{
				case 1:
					this.General = decoder.ReadFixedStruct<SAMPR_GROUP_GENERAL_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 2:
					this.Name = decoder.ReadFixedStruct<SAMPR_GROUP_NAME_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 3:
					this.Attribute = decoder.ReadFixedStruct<GROUP_ATTRIBUTE_INFORMATION>(NdrAlignment._4Byte);
					break;
				case 4:
					this.AdminComment = decoder.ReadFixedStruct<SAMPR_GROUP_ADM_COMMENT_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 5:
					this.DoNotUse = decoder.ReadFixedStruct<SAMPR_GROUP_GENERAL_INFORMATION>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteStructDeferral(this.General);
					break;
				case 2:
					encoder.WriteStructDeferral(this.Name);
					break;
				case 3:
					encoder.WriteStructDeferral(this.Attribute);
					break;
				case 4:
					encoder.WriteStructDeferral(this.AdminComment);
					break;
				case 5:
					encoder.WriteStructDeferral(this.DoNotUse);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					decoder.ReadStructDeferral<SAMPR_GROUP_GENERAL_INFORMATION>(ref this.General);
					break;
				case 2:
					decoder.ReadStructDeferral<SAMPR_GROUP_NAME_INFORMATION>(ref this.Name);
					break;
				case 3:
					decoder.ReadStructDeferral<GROUP_ATTRIBUTE_INFORMATION>(ref this.Attribute);
					break;
				case 4:
					decoder.ReadStructDeferral<SAMPR_GROUP_ADM_COMMENT_INFORMATION>(ref this.AdminComment);
					break;
				case 5:
					decoder.ReadStructDeferral<SAMPR_GROUP_GENERAL_INFORMATION>(ref this.DoNotUse);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_ALIAS_GENERAL_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Name;
		public uint MemberCount;
		public ms_dtyp.RPC_UNICODE_STRING AdminComment;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
			encoder.WriteValue(this.MemberCount);
			encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.MemberCount = decoder.ReadUInt32();
			this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
			encoder.WriteStructDeferral(this.AdminComment);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_ALIAS_NAME_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Name;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_ALIAS_ADM_COMMENT_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING AdminComment;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.AdminComment);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum ALIAS_INFORMATION_CLASS : int
	{
		AliasGeneralInformation = 1,
		AliasNameInformation = 2,
		AliasAdminCommentInformation = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_ALIAS_INFO_BUFFER : IRpcFixedStruct
	{
		public ALIAS_INFORMATION_CLASS unionSwitch;
		public SAMPR_ALIAS_GENERAL_INFORMATION General;
		public SAMPR_ALIAS_NAME_INFORMATION Name;
		public SAMPR_ALIAS_ADM_COMMENT_INFORMATION AdminComment;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.unionSwitch);
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteFixedStruct(this.General, NdrAlignment.NativePtr);
					break;
				case 2:
					encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
					break;
				case 3:
					encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = (ALIAS_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			switch ((int)this.unionSwitch)
			{
				case 1:
					this.General = decoder.ReadFixedStruct<SAMPR_ALIAS_GENERAL_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 2:
					this.Name = decoder.ReadFixedStruct<SAMPR_ALIAS_NAME_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 3:
					this.AdminComment = decoder.ReadFixedStruct<SAMPR_ALIAS_ADM_COMMENT_INFORMATION>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteStructDeferral(this.General);
					break;
				case 2:
					encoder.WriteStructDeferral(this.Name);
					break;
				case 3:
					encoder.WriteStructDeferral(this.AdminComment);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					decoder.ReadStructDeferral<SAMPR_ALIAS_GENERAL_INFORMATION>(ref this.General);
					break;
				case 2:
					decoder.ReadStructDeferral<SAMPR_ALIAS_NAME_INFORMATION>(ref this.Name);
					break;
				case 3:
					decoder.ReadStructDeferral<SAMPR_ALIAS_ADM_COMMENT_INFORMATION>(ref this.AdminComment);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_ENCRYPTED_USER_PASSWORD : IRpcFixedStruct
	{
		public byte[] Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			if (this.Buffer == null)
				this.Buffer = new byte[516];
			for (int i = 0; i < 516; i++)
			{
				byte elem_0 = this.Buffer[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			if (this.Buffer == null)
				this.Buffer = new byte[516];
			for (int i = 0; i < 516; i++)
			{
				byte elem_0 = this.Buffer[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.Buffer[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_ENCRYPTED_USER_PASSWORD_NEW : IRpcFixedStruct
	{
		public byte[] Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			if (this.Buffer == null)
				this.Buffer = new byte[532];
			for (int i = 0; i < 532; i++)
			{
				byte elem_0 = this.Buffer[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			if (this.Buffer == null)
				this.Buffer = new byte[532];
			for (int i = 0; i < 532; i++)
			{
				byte elem_0 = this.Buffer[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.Buffer[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct USER_PRIMARY_GROUP_INFORMATION : IRpcFixedStruct
	{
		public uint PrimaryGroupId;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.PrimaryGroupId);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.PrimaryGroupId = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct USER_CONTROL_INFORMATION : IRpcFixedStruct
	{
		public uint UserAccountControl;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.UserAccountControl);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.UserAccountControl = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct USER_EXPIRES_INFORMATION : IRpcFixedStruct
	{
		public OLD_LARGE_INTEGER AccountExpires;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.AccountExpires, NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.AccountExpires = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.AccountExpires);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.AccountExpires);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_LOGON_HOURS : IRpcFixedStruct
	{
		public ushort UnitsPerWeek;
		public RpcPointer<ArraySegment<byte>> LogonHours;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.UnitsPerWeek);
			encoder.WriteUniquePointer(this.LogonHours);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.UnitsPerWeek = decoder.ReadUInt16();
			this.LogonHours = decoder.ReadUniquePointer<ArraySegment<byte>>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.LogonHours is not null)
			{
				encoder.WriteArrayHeader(this.LogonHours.value, true);
				for (int i = 0; i < this.LogonHours.value.Count; i++)
				{
					byte elem_0 = this.LogonHours.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.LogonHours is not null)
			{
				this.LogonHours.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < this.LogonHours.value.Count; i++)
				{
					byte elem_0 = this.LogonHours.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					this.LogonHours.value.Item(i) = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_ALL_INFORMATION : IRpcFixedStruct
	{
		public OLD_LARGE_INTEGER LastLogon;
		public OLD_LARGE_INTEGER LastLogoff;
		public OLD_LARGE_INTEGER PasswordLastSet;
		public OLD_LARGE_INTEGER AccountExpires;
		public OLD_LARGE_INTEGER PasswordCanChange;
		public OLD_LARGE_INTEGER PasswordMustChange;
		public ms_dtyp.RPC_UNICODE_STRING UserName;
		public ms_dtyp.RPC_UNICODE_STRING FullName;
		public ms_dtyp.RPC_UNICODE_STRING HomeDirectory;
		public ms_dtyp.RPC_UNICODE_STRING HomeDirectoryDrive;
		public ms_dtyp.RPC_UNICODE_STRING ScriptPath;
		public ms_dtyp.RPC_UNICODE_STRING ProfilePath;
		public ms_dtyp.RPC_UNICODE_STRING AdminComment;
		public ms_dtyp.RPC_UNICODE_STRING WorkStations;
		public ms_dtyp.RPC_UNICODE_STRING UserComment;
		public ms_dtyp.RPC_UNICODE_STRING Parameters;
		public RPC_SHORT_BLOB LmOwfPassword;
		public RPC_SHORT_BLOB NtOwfPassword;
		public ms_dtyp.RPC_UNICODE_STRING PrivateData;
		public SAMPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor;
		public uint UserId;
		public uint PrimaryGroupId;
		public uint UserAccountControl;
		public uint WhichFields;
		public SAMPR_LOGON_HOURS LogonHours;
		public ushort BadPasswordCount;
		public ushort LogonCount;
		public ushort CountryCode;
		public ushort CodePage;
		public byte LmPasswordPresent;
		public byte NtPasswordPresent;
		public byte PasswordExpired;
		public byte PrivateDataSensitive;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LastLogon, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.LastLogoff, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.PasswordLastSet, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.AccountExpires, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.PasswordCanChange, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.PasswordMustChange, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.UserName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.FullName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.HomeDirectory, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.HomeDirectoryDrive, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.ScriptPath, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.ProfilePath, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.WorkStations, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.UserComment, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.Parameters, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LmOwfPassword, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.NtOwfPassword, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.PrivateData, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.SecurityDescriptor, NdrAlignment.NativePtr);
			encoder.WriteValue(this.UserId);
			encoder.WriteValue(this.PrimaryGroupId);
			encoder.WriteValue(this.UserAccountControl);
			encoder.WriteValue(this.WhichFields);
			encoder.WriteFixedStruct(this.LogonHours, NdrAlignment.NativePtr);
			encoder.WriteValue(this.BadPasswordCount);
			encoder.WriteValue(this.LogonCount);
			encoder.WriteValue(this.CountryCode);
			encoder.WriteValue(this.CodePage);
			encoder.WriteValue(this.LmPasswordPresent);
			encoder.WriteValue(this.NtPasswordPresent);
			encoder.WriteValue(this.PasswordExpired);
			encoder.WriteValue(this.PrivateDataSensitive);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LastLogon = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.LastLogoff = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.PasswordLastSet = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.AccountExpires = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.PasswordCanChange = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.PasswordMustChange = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.HomeDirectory = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.HomeDirectoryDrive = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.ScriptPath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.ProfilePath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.WorkStations = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.UserComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.Parameters = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.LmOwfPassword = decoder.ReadFixedStruct<RPC_SHORT_BLOB>(NdrAlignment.NativePtr);
			this.NtOwfPassword = decoder.ReadFixedStruct<RPC_SHORT_BLOB>(NdrAlignment.NativePtr);
			this.PrivateData = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.SecurityDescriptor = decoder.ReadFixedStruct<SAMPR_SR_SECURITY_DESCRIPTOR>(NdrAlignment.NativePtr);
			this.UserId = decoder.ReadUInt32();
			this.PrimaryGroupId = decoder.ReadUInt32();
			this.UserAccountControl = decoder.ReadUInt32();
			this.WhichFields = decoder.ReadUInt32();
			this.LogonHours = decoder.ReadFixedStruct<SAMPR_LOGON_HOURS>(NdrAlignment.NativePtr);
			this.BadPasswordCount = decoder.ReadUInt16();
			this.LogonCount = decoder.ReadUInt16();
			this.CountryCode = decoder.ReadUInt16();
			this.CodePage = decoder.ReadUInt16();
			this.LmPasswordPresent = decoder.ReadUnsignedChar();
			this.NtPasswordPresent = decoder.ReadUnsignedChar();
			this.PasswordExpired = decoder.ReadUnsignedChar();
			this.PrivateDataSensitive = decoder.ReadUnsignedChar();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LastLogon);
			encoder.WriteStructDeferral(this.LastLogoff);
			encoder.WriteStructDeferral(this.PasswordLastSet);
			encoder.WriteStructDeferral(this.AccountExpires);
			encoder.WriteStructDeferral(this.PasswordCanChange);
			encoder.WriteStructDeferral(this.PasswordMustChange);
			encoder.WriteStructDeferral(this.UserName);
			encoder.WriteStructDeferral(this.FullName);
			encoder.WriteStructDeferral(this.HomeDirectory);
			encoder.WriteStructDeferral(this.HomeDirectoryDrive);
			encoder.WriteStructDeferral(this.ScriptPath);
			encoder.WriteStructDeferral(this.ProfilePath);
			encoder.WriteStructDeferral(this.AdminComment);
			encoder.WriteStructDeferral(this.WorkStations);
			encoder.WriteStructDeferral(this.UserComment);
			encoder.WriteStructDeferral(this.Parameters);
			encoder.WriteStructDeferral(this.LmOwfPassword);
			encoder.WriteStructDeferral(this.NtOwfPassword);
			encoder.WriteStructDeferral(this.PrivateData);
			encoder.WriteStructDeferral(this.SecurityDescriptor);
			encoder.WriteStructDeferral(this.LogonHours);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.LastLogon);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.LastLogoff);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordLastSet);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.AccountExpires);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordCanChange);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordMustChange);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectory);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectoryDrive);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ScriptPath);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ProfilePath);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.WorkStations);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserComment);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Parameters);
			decoder.ReadStructDeferral<RPC_SHORT_BLOB>(ref this.LmOwfPassword);
			decoder.ReadStructDeferral<RPC_SHORT_BLOB>(ref this.NtOwfPassword);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.PrivateData);
			decoder.ReadStructDeferral<SAMPR_SR_SECURITY_DESCRIPTOR>(ref this.SecurityDescriptor);
			decoder.ReadStructDeferral<SAMPR_LOGON_HOURS>(ref this.LogonHours);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_GENERAL_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING UserName;
		public ms_dtyp.RPC_UNICODE_STRING FullName;
		public uint PrimaryGroupId;
		public ms_dtyp.RPC_UNICODE_STRING AdminComment;
		public ms_dtyp.RPC_UNICODE_STRING UserComment;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.UserName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.FullName, NdrAlignment.NativePtr);
			encoder.WriteValue(this.PrimaryGroupId);
			encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.UserComment, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.PrimaryGroupId = decoder.ReadUInt32();
			this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.UserComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.UserName);
			encoder.WriteStructDeferral(this.FullName);
			encoder.WriteStructDeferral(this.AdminComment);
			encoder.WriteStructDeferral(this.UserComment);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserComment);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_PREFERENCES_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING UserComment;
		public ms_dtyp.RPC_UNICODE_STRING Reserved1;
		public ushort CountryCode;
		public ushort CodePage;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.UserComment, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.Reserved1, NdrAlignment.NativePtr);
			encoder.WriteValue(this.CountryCode);
			encoder.WriteValue(this.CodePage);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.UserComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.Reserved1 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.CountryCode = decoder.ReadUInt16();
			this.CodePage = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.UserComment);
			encoder.WriteStructDeferral(this.Reserved1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserComment);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Reserved1);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_PARAMETERS_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Parameters;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Parameters, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Parameters = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Parameters);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Parameters);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_LOGON_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING UserName;
		public ms_dtyp.RPC_UNICODE_STRING FullName;
		public uint UserId;
		public uint PrimaryGroupId;
		public ms_dtyp.RPC_UNICODE_STRING HomeDirectory;
		public ms_dtyp.RPC_UNICODE_STRING HomeDirectoryDrive;
		public ms_dtyp.RPC_UNICODE_STRING ScriptPath;
		public ms_dtyp.RPC_UNICODE_STRING ProfilePath;
		public ms_dtyp.RPC_UNICODE_STRING WorkStations;
		public OLD_LARGE_INTEGER LastLogon;
		public OLD_LARGE_INTEGER LastLogoff;
		public OLD_LARGE_INTEGER PasswordLastSet;
		public OLD_LARGE_INTEGER PasswordCanChange;
		public OLD_LARGE_INTEGER PasswordMustChange;
		public SAMPR_LOGON_HOURS LogonHours;
		public ushort BadPasswordCount;
		public ushort LogonCount;
		public uint UserAccountControl;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.UserName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.FullName, NdrAlignment.NativePtr);
			encoder.WriteValue(this.UserId);
			encoder.WriteValue(this.PrimaryGroupId);
			encoder.WriteFixedStruct(this.HomeDirectory, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.HomeDirectoryDrive, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.ScriptPath, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.ProfilePath, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.WorkStations, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LastLogon, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.LastLogoff, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.PasswordLastSet, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.PasswordCanChange, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.PasswordMustChange, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.LogonHours, NdrAlignment.NativePtr);
			encoder.WriteValue(this.BadPasswordCount);
			encoder.WriteValue(this.LogonCount);
			encoder.WriteValue(this.UserAccountControl);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.UserId = decoder.ReadUInt32();
			this.PrimaryGroupId = decoder.ReadUInt32();
			this.HomeDirectory = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.HomeDirectoryDrive = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.ScriptPath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.ProfilePath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.WorkStations = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.LastLogon = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.LastLogoff = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.PasswordLastSet = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.PasswordCanChange = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.PasswordMustChange = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.LogonHours = decoder.ReadFixedStruct<SAMPR_LOGON_HOURS>(NdrAlignment.NativePtr);
			this.BadPasswordCount = decoder.ReadUInt16();
			this.LogonCount = decoder.ReadUInt16();
			this.UserAccountControl = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.UserName);
			encoder.WriteStructDeferral(this.FullName);
			encoder.WriteStructDeferral(this.HomeDirectory);
			encoder.WriteStructDeferral(this.HomeDirectoryDrive);
			encoder.WriteStructDeferral(this.ScriptPath);
			encoder.WriteStructDeferral(this.ProfilePath);
			encoder.WriteStructDeferral(this.WorkStations);
			encoder.WriteStructDeferral(this.LastLogon);
			encoder.WriteStructDeferral(this.LastLogoff);
			encoder.WriteStructDeferral(this.PasswordLastSet);
			encoder.WriteStructDeferral(this.PasswordCanChange);
			encoder.WriteStructDeferral(this.PasswordMustChange);
			encoder.WriteStructDeferral(this.LogonHours);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectory);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectoryDrive);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ScriptPath);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ProfilePath);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.WorkStations);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.LastLogon);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.LastLogoff);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordLastSet);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordCanChange);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordMustChange);
			decoder.ReadStructDeferral<SAMPR_LOGON_HOURS>(ref this.LogonHours);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_ACCOUNT_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING UserName;
		public ms_dtyp.RPC_UNICODE_STRING FullName;
		public uint UserId;
		public uint PrimaryGroupId;
		public ms_dtyp.RPC_UNICODE_STRING HomeDirectory;
		public ms_dtyp.RPC_UNICODE_STRING HomeDirectoryDrive;
		public ms_dtyp.RPC_UNICODE_STRING ScriptPath;
		public ms_dtyp.RPC_UNICODE_STRING ProfilePath;
		public ms_dtyp.RPC_UNICODE_STRING AdminComment;
		public ms_dtyp.RPC_UNICODE_STRING WorkStations;
		public OLD_LARGE_INTEGER LastLogon;
		public OLD_LARGE_INTEGER LastLogoff;
		public SAMPR_LOGON_HOURS LogonHours;
		public ushort BadPasswordCount;
		public ushort LogonCount;
		public OLD_LARGE_INTEGER PasswordLastSet;
		public OLD_LARGE_INTEGER AccountExpires;
		public uint UserAccountControl;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.UserName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.FullName, NdrAlignment.NativePtr);
			encoder.WriteValue(this.UserId);
			encoder.WriteValue(this.PrimaryGroupId);
			encoder.WriteFixedStruct(this.HomeDirectory, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.HomeDirectoryDrive, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.ScriptPath, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.ProfilePath, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.WorkStations, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LastLogon, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.LastLogoff, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.LogonHours, NdrAlignment.NativePtr);
			encoder.WriteValue(this.BadPasswordCount);
			encoder.WriteValue(this.LogonCount);
			encoder.WriteFixedStruct(this.PasswordLastSet, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.AccountExpires, NdrAlignment._4Byte);
			encoder.WriteValue(this.UserAccountControl);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.UserId = decoder.ReadUInt32();
			this.PrimaryGroupId = decoder.ReadUInt32();
			this.HomeDirectory = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.HomeDirectoryDrive = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.ScriptPath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.ProfilePath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.WorkStations = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.LastLogon = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.LastLogoff = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.LogonHours = decoder.ReadFixedStruct<SAMPR_LOGON_HOURS>(NdrAlignment.NativePtr);
			this.BadPasswordCount = decoder.ReadUInt16();
			this.LogonCount = decoder.ReadUInt16();
			this.PasswordLastSet = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.AccountExpires = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(NdrAlignment._4Byte);
			this.UserAccountControl = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.UserName);
			encoder.WriteStructDeferral(this.FullName);
			encoder.WriteStructDeferral(this.HomeDirectory);
			encoder.WriteStructDeferral(this.HomeDirectoryDrive);
			encoder.WriteStructDeferral(this.ScriptPath);
			encoder.WriteStructDeferral(this.ProfilePath);
			encoder.WriteStructDeferral(this.AdminComment);
			encoder.WriteStructDeferral(this.WorkStations);
			encoder.WriteStructDeferral(this.LastLogon);
			encoder.WriteStructDeferral(this.LastLogoff);
			encoder.WriteStructDeferral(this.LogonHours);
			encoder.WriteStructDeferral(this.PasswordLastSet);
			encoder.WriteStructDeferral(this.AccountExpires);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectory);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectoryDrive);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ScriptPath);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ProfilePath);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.WorkStations);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.LastLogon);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.LastLogoff);
			decoder.ReadStructDeferral<SAMPR_LOGON_HOURS>(ref this.LogonHours);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordLastSet);
			decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.AccountExpires);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_A_NAME_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING UserName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.UserName, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.UserName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserName);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_F_NAME_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING FullName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.FullName, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.FullName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_NAME_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING UserName;
		public ms_dtyp.RPC_UNICODE_STRING FullName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.UserName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.FullName, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.UserName);
			encoder.WriteStructDeferral(this.FullName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_HOME_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING HomeDirectory;
		public ms_dtyp.RPC_UNICODE_STRING HomeDirectoryDrive;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.HomeDirectory, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.HomeDirectoryDrive, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.HomeDirectory = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.HomeDirectoryDrive = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.HomeDirectory);
			encoder.WriteStructDeferral(this.HomeDirectoryDrive);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectory);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectoryDrive);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_SCRIPT_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING ScriptPath;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ScriptPath, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ScriptPath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ScriptPath);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ScriptPath);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_PROFILE_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING ProfilePath;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ProfilePath, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ProfilePath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ProfilePath);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ProfilePath);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_ADMIN_COMMENT_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING AdminComment;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.AdminComment);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_WORKSTATIONS_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING WorkStations;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.WorkStations, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.WorkStations = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.WorkStations);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.WorkStations);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_LOGON_HOURS_INFORMATION : IRpcFixedStruct
	{
		public SAMPR_LOGON_HOURS LogonHours;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LogonHours, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LogonHours = decoder.ReadFixedStruct<SAMPR_LOGON_HOURS>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LogonHours);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SAMPR_LOGON_HOURS>(ref this.LogonHours);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_INTERNAL1_INFORMATION : IRpcFixedStruct
	{
		public ENCRYPTED_LM_OWF_PASSWORD EncryptedNtOwfPassword;
		public ENCRYPTED_LM_OWF_PASSWORD EncryptedLmOwfPassword;
		public byte NtPasswordPresent;
		public byte LmPasswordPresent;
		public byte PasswordExpired;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.EncryptedNtOwfPassword, NdrAlignment._1Byte);
			encoder.WriteFixedStruct(this.EncryptedLmOwfPassword, NdrAlignment._1Byte);
			encoder.WriteValue(this.NtPasswordPresent);
			encoder.WriteValue(this.LmPasswordPresent);
			encoder.WriteValue(this.PasswordExpired);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EncryptedNtOwfPassword = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(NdrAlignment._1Byte);
			this.EncryptedLmOwfPassword = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(NdrAlignment._1Byte);
			this.NtPasswordPresent = decoder.ReadUnsignedChar();
			this.LmPasswordPresent = decoder.ReadUnsignedChar();
			this.PasswordExpired = decoder.ReadUnsignedChar();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.EncryptedNtOwfPassword);
			encoder.WriteStructDeferral(this.EncryptedLmOwfPassword);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref this.EncryptedNtOwfPassword);
			decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref this.EncryptedLmOwfPassword);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_INTERNAL4_INFORMATION : IRpcFixedStruct
	{
		public SAMPR_USER_ALL_INFORMATION I1;
		public SAMPR_ENCRYPTED_USER_PASSWORD UserPassword;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.I1, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.UserPassword, NdrAlignment._1Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.I1 = decoder.ReadFixedStruct<SAMPR_USER_ALL_INFORMATION>(NdrAlignment.NativePtr);
			this.UserPassword = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.I1);
			encoder.WriteStructDeferral(this.UserPassword);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SAMPR_USER_ALL_INFORMATION>(ref this.I1);
			decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD>(ref this.UserPassword);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_INTERNAL4_INFORMATION_NEW : IRpcFixedStruct
	{
		public SAMPR_USER_ALL_INFORMATION I1;
		public SAMPR_ENCRYPTED_USER_PASSWORD_NEW UserPassword;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.I1, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.UserPassword, NdrAlignment._1Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.I1 = decoder.ReadFixedStruct<SAMPR_USER_ALL_INFORMATION>(NdrAlignment.NativePtr);
			this.UserPassword = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD_NEW>(NdrAlignment._1Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.I1);
			encoder.WriteStructDeferral(this.UserPassword);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SAMPR_USER_ALL_INFORMATION>(ref this.I1);
			decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD_NEW>(ref this.UserPassword);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_INTERNAL5_INFORMATION : IRpcFixedStruct
	{
		public SAMPR_ENCRYPTED_USER_PASSWORD UserPassword;
		public byte PasswordExpired;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.UserPassword, NdrAlignment._1Byte);
			encoder.WriteValue(this.PasswordExpired);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.UserPassword = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
			this.PasswordExpired = decoder.ReadUnsignedChar();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.UserPassword);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD>(ref this.UserPassword);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_INTERNAL5_INFORMATION_NEW : IRpcFixedStruct
	{
		public SAMPR_ENCRYPTED_USER_PASSWORD_NEW UserPassword;
		public byte PasswordExpired;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.UserPassword, NdrAlignment._1Byte);
			encoder.WriteValue(this.PasswordExpired);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.UserPassword = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD_NEW>(NdrAlignment._1Byte);
			this.PasswordExpired = decoder.ReadUnsignedChar();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.UserPassword);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD_NEW>(ref this.UserPassword);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum USER_INFORMATION_CLASS : int
	{
		UserGeneralInformation = 1,
		UserPreferencesInformation = 2,
		UserLogonInformation = 3,
		UserLogonHoursInformation = 4,
		UserAccountInformation = 5,
		UserNameInformation = 6,
		UserAccountNameInformation = 7,
		UserFullNameInformation = 8,
		UserPrimaryGroupInformation = 9,
		UserHomeInformation = 10,
		UserScriptInformation = 11,
		UserProfileInformation = 12,
		UserAdminCommentInformation = 13,
		UserWorkStationsInformation = 14,
		UserControlInformation = 16,
		UserExpiresInformation = 17,
		UserInternal1Information = 18,
		UserParametersInformation = 20,
		UserAllInformation = 21,
		UserInternal4Information = 23,
		UserInternal5Information = 24,
		UserInternal4InformationNew = 25,
		UserInternal5InformationNew = 26
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAMPR_USER_INFO_BUFFER : IRpcFixedStruct
	{
		public USER_INFORMATION_CLASS unionSwitch;
		public SAMPR_USER_GENERAL_INFORMATION General;
		public SAMPR_USER_PREFERENCES_INFORMATION Preferences;
		public SAMPR_USER_LOGON_INFORMATION Logon;
		public SAMPR_USER_LOGON_HOURS_INFORMATION LogonHours;
		public SAMPR_USER_ACCOUNT_INFORMATION Account;
		public SAMPR_USER_NAME_INFORMATION Name;
		public SAMPR_USER_A_NAME_INFORMATION AccountName;
		public SAMPR_USER_F_NAME_INFORMATION FullName;
		public USER_PRIMARY_GROUP_INFORMATION PrimaryGroup;
		public SAMPR_USER_HOME_INFORMATION Home;
		public SAMPR_USER_SCRIPT_INFORMATION Script;
		public SAMPR_USER_PROFILE_INFORMATION Profile;
		public SAMPR_USER_ADMIN_COMMENT_INFORMATION AdminComment;
		public SAMPR_USER_WORKSTATIONS_INFORMATION WorkStations;
		public USER_CONTROL_INFORMATION Control;
		public USER_EXPIRES_INFORMATION Expires;
		public SAMPR_USER_INTERNAL1_INFORMATION Internal1;
		public SAMPR_USER_PARAMETERS_INFORMATION Parameters;
		public SAMPR_USER_ALL_INFORMATION All;
		public SAMPR_USER_INTERNAL4_INFORMATION Internal4;
		public SAMPR_USER_INTERNAL5_INFORMATION Internal5;
		public SAMPR_USER_INTERNAL4_INFORMATION_NEW Internal4New;
		public SAMPR_USER_INTERNAL5_INFORMATION_NEW Internal5New;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.unionSwitch);
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteFixedStruct(this.General, NdrAlignment.NativePtr);
					break;
				case 2:
					encoder.WriteFixedStruct(this.Preferences, NdrAlignment.NativePtr);
					break;
				case 3:
					encoder.WriteFixedStruct(this.Logon, NdrAlignment.NativePtr);
					break;
				case 4:
					encoder.WriteFixedStruct(this.LogonHours, NdrAlignment.NativePtr);
					break;
				case 5:
					encoder.WriteFixedStruct(this.Account, NdrAlignment.NativePtr);
					break;
				case 6:
					encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
					break;
				case 7:
					encoder.WriteFixedStruct(this.AccountName, NdrAlignment.NativePtr);
					break;
				case 8:
					encoder.WriteFixedStruct(this.FullName, NdrAlignment.NativePtr);
					break;
				case 9:
					encoder.WriteFixedStruct(this.PrimaryGroup, NdrAlignment._4Byte);
					break;
				case 10:
					encoder.WriteFixedStruct(this.Home, NdrAlignment.NativePtr);
					break;
				case 11:
					encoder.WriteFixedStruct(this.Script, NdrAlignment.NativePtr);
					break;
				case 12:
					encoder.WriteFixedStruct(this.Profile, NdrAlignment.NativePtr);
					break;
				case 13:
					encoder.WriteFixedStruct(this.AdminComment, NdrAlignment.NativePtr);
					break;
				case 14:
					encoder.WriteFixedStruct(this.WorkStations, NdrAlignment.NativePtr);
					break;
				case 16:
					encoder.WriteFixedStruct(this.Control, NdrAlignment._4Byte);
					break;
				case 17:
					encoder.WriteFixedStruct(this.Expires, NdrAlignment._4Byte);
					break;
				case 18:
					encoder.WriteFixedStruct(this.Internal1, NdrAlignment._1Byte);
					break;
				case 20:
					encoder.WriteFixedStruct(this.Parameters, NdrAlignment.NativePtr);
					break;
				case 21:
					encoder.WriteFixedStruct(this.All, NdrAlignment.NativePtr);
					break;
				case 23:
					encoder.WriteFixedStruct(this.Internal4, NdrAlignment.NativePtr);
					break;
				case 24:
					encoder.WriteFixedStruct(this.Internal5, NdrAlignment._1Byte);
					break;
				case 25:
					encoder.WriteFixedStruct(this.Internal4New, NdrAlignment.NativePtr);
					break;
				case 26:
					encoder.WriteFixedStruct(this.Internal5New, NdrAlignment._1Byte);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = (USER_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			switch ((int)this.unionSwitch)
			{
				case 1:
					this.General = decoder.ReadFixedStruct<SAMPR_USER_GENERAL_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 2:
					this.Preferences = decoder.ReadFixedStruct<SAMPR_USER_PREFERENCES_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 3:
					this.Logon = decoder.ReadFixedStruct<SAMPR_USER_LOGON_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 4:
					this.LogonHours = decoder.ReadFixedStruct<SAMPR_USER_LOGON_HOURS_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 5:
					this.Account = decoder.ReadFixedStruct<SAMPR_USER_ACCOUNT_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 6:
					this.Name = decoder.ReadFixedStruct<SAMPR_USER_NAME_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 7:
					this.AccountName = decoder.ReadFixedStruct<SAMPR_USER_A_NAME_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 8:
					this.FullName = decoder.ReadFixedStruct<SAMPR_USER_F_NAME_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 9:
					this.PrimaryGroup = decoder.ReadFixedStruct<USER_PRIMARY_GROUP_INFORMATION>(NdrAlignment._4Byte);
					break;
				case 10:
					this.Home = decoder.ReadFixedStruct<SAMPR_USER_HOME_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 11:
					this.Script = decoder.ReadFixedStruct<SAMPR_USER_SCRIPT_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 12:
					this.Profile = decoder.ReadFixedStruct<SAMPR_USER_PROFILE_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 13:
					this.AdminComment = decoder.ReadFixedStruct<SAMPR_USER_ADMIN_COMMENT_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 14:
					this.WorkStations = decoder.ReadFixedStruct<SAMPR_USER_WORKSTATIONS_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 16:
					this.Control = decoder.ReadFixedStruct<USER_CONTROL_INFORMATION>(NdrAlignment._4Byte);
					break;
				case 17:
					this.Expires = decoder.ReadFixedStruct<USER_EXPIRES_INFORMATION>(NdrAlignment._4Byte);
					break;
				case 18:
					this.Internal1 = decoder.ReadFixedStruct<SAMPR_USER_INTERNAL1_INFORMATION>(NdrAlignment._1Byte);
					break;
				case 20:
					this.Parameters = decoder.ReadFixedStruct<SAMPR_USER_PARAMETERS_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 21:
					this.All = decoder.ReadFixedStruct<SAMPR_USER_ALL_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 23:
					this.Internal4 = decoder.ReadFixedStruct<SAMPR_USER_INTERNAL4_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 24:
					this.Internal5 = decoder.ReadFixedStruct<SAMPR_USER_INTERNAL5_INFORMATION>(NdrAlignment._1Byte);
					break;
				case 25:
					this.Internal4New = decoder.ReadFixedStruct<SAMPR_USER_INTERNAL4_INFORMATION_NEW>(NdrAlignment.NativePtr);
					break;
				case 26:
					this.Internal5New = decoder.ReadFixedStruct<SAMPR_USER_INTERNAL5_INFORMATION_NEW>(NdrAlignment._1Byte);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteStructDeferral(this.General);
					break;
				case 2:
					encoder.WriteStructDeferral(this.Preferences);
					break;
				case 3:
					encoder.WriteStructDeferral(this.Logon);
					break;
				case 4:
					encoder.WriteStructDeferral(this.LogonHours);
					break;
				case 5:
					encoder.WriteStructDeferral(this.Account);
					break;
				case 6:
					encoder.WriteStructDeferral(this.Name);
					break;
				case 7:
					encoder.WriteStructDeferral(this.AccountName);
					break;
				case 8:
					encoder.WriteStructDeferral(this.FullName);
					break;
				case 9:
					encoder.WriteStructDeferral(this.PrimaryGroup);
					break;
				case 10:
					encoder.WriteStructDeferral(this.Home);
					break;
				case 11:
					encoder.WriteStructDeferral(this.Script);
					break;
				case 12:
					encoder.WriteStructDeferral(this.Profile);
					break;
				case 13:
					encoder.WriteStructDeferral(this.AdminComment);
					break;
				case 14:
					encoder.WriteStructDeferral(this.WorkStations);
					break;
				case 16:
					encoder.WriteStructDeferral(this.Control);
					break;
				case 17:
					encoder.WriteStructDeferral(this.Expires);
					break;
				case 18:
					encoder.WriteStructDeferral(this.Internal1);
					break;
				case 20:
					encoder.WriteStructDeferral(this.Parameters);
					break;
				case 21:
					encoder.WriteStructDeferral(this.All);
					break;
				case 23:
					encoder.WriteStructDeferral(this.Internal4);
					break;
				case 24:
					encoder.WriteStructDeferral(this.Internal5);
					break;
				case 25:
					encoder.WriteStructDeferral(this.Internal4New);
					break;
				case 26:
					encoder.WriteStructDeferral(this.Internal5New);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					decoder.ReadStructDeferral<SAMPR_USER_GENERAL_INFORMATION>(ref this.General);
					break;
				case 2:
					decoder.ReadStructDeferral<SAMPR_USER_PREFERENCES_INFORMATION>(ref this.Preferences);
					break;
				case 3:
					decoder.ReadStructDeferral<SAMPR_USER_LOGON_INFORMATION>(ref this.Logon);
					break;
				case 4:
					decoder.ReadStructDeferral<SAMPR_USER_LOGON_HOURS_INFORMATION>(ref this.LogonHours);
					break;
				case 5:
					decoder.ReadStructDeferral<SAMPR_USER_ACCOUNT_INFORMATION>(ref this.Account);
					break;
				case 6:
					decoder.ReadStructDeferral<SAMPR_USER_NAME_INFORMATION>(ref this.Name);
					break;
				case 7:
					decoder.ReadStructDeferral<SAMPR_USER_A_NAME_INFORMATION>(ref this.AccountName);
					break;
				case 8:
					decoder.ReadStructDeferral<SAMPR_USER_F_NAME_INFORMATION>(ref this.FullName);
					break;
				case 9:
					decoder.ReadStructDeferral<USER_PRIMARY_GROUP_INFORMATION>(ref this.PrimaryGroup);
					break;
				case 10:
					decoder.ReadStructDeferral<SAMPR_USER_HOME_INFORMATION>(ref this.Home);
					break;
				case 11:
					decoder.ReadStructDeferral<SAMPR_USER_SCRIPT_INFORMATION>(ref this.Script);
					break;
				case 12:
					decoder.ReadStructDeferral<SAMPR_USER_PROFILE_INFORMATION>(ref this.Profile);
					break;
				case 13:
					decoder.ReadStructDeferral<SAMPR_USER_ADMIN_COMMENT_INFORMATION>(ref this.AdminComment);
					break;
				case 14:
					decoder.ReadStructDeferral<SAMPR_USER_WORKSTATIONS_INFORMATION>(ref this.WorkStations);
					break;
				case 16:
					decoder.ReadStructDeferral<USER_CONTROL_INFORMATION>(ref this.Control);
					break;
				case 17:
					decoder.ReadStructDeferral<USER_EXPIRES_INFORMATION>(ref this.Expires);
					break;
				case 18:
					decoder.ReadStructDeferral<SAMPR_USER_INTERNAL1_INFORMATION>(ref this.Internal1);
					break;
				case 20:
					decoder.ReadStructDeferral<SAMPR_USER_PARAMETERS_INFORMATION>(ref this.Parameters);
					break;
				case 21:
					decoder.ReadStructDeferral<SAMPR_USER_ALL_INFORMATION>(ref this.All);
					break;
				case 23:
					decoder.ReadStructDeferral<SAMPR_USER_INTERNAL4_INFORMATION>(ref this.Internal4);
					break;
				case 24:
					decoder.ReadStructDeferral<SAMPR_USER_INTERNAL5_INFORMATION>(ref this.Internal5);
					break;
				case 25:
					decoder.ReadStructDeferral<SAMPR_USER_INTERNAL4_INFORMATION_NEW>(ref this.Internal4New);
					break;
				case 26:
					decoder.ReadStructDeferral<SAMPR_USER_INTERNAL5_INFORMATION_NEW>(ref this.Internal5New);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum PASSWORD_POLICY_VALIDATION_TYPE : int
	{
		SamValidateAuthentication = 1,
		SamValidatePasswordChange = 2,
		SamValidatePasswordReset = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAM_VALIDATE_PASSWORD_HASH : IRpcFixedStruct
	{
		public uint Length;
		public RpcPointer<byte[]> Hash;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteUniquePointer(this.Hash);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt32();
			this.Hash = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Hash is not null)
			{
				encoder.WriteArrayHeader(this.Hash.value);
				for (int i = 0; i < this.Hash.value.Length; i++)
				{
					byte elem_0 = this.Hash.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Hash is not null)
			{
				this.Hash.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.Hash.value.Length; i++)
				{
					byte elem_0 = this.Hash.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.Hash.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAM_VALIDATE_PERSISTED_FIELDS : IRpcFixedStruct
	{
		public uint PresentFields;
		public ms_dtyp.LARGE_INTEGER PasswordLastSet;
		public ms_dtyp.LARGE_INTEGER BadPasswordTime;
		public ms_dtyp.LARGE_INTEGER LockoutTime;
		public uint BadPasswordCount;
		public uint PasswordHistoryLength;
		public RpcPointer<SAM_VALIDATE_PASSWORD_HASH[]> PasswordHistory;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.PresentFields);
			encoder.WriteFixedStruct(this.PasswordLastSet, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.BadPasswordTime, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.LockoutTime, NdrAlignment._8Byte);
			encoder.WriteValue(this.BadPasswordCount);
			encoder.WriteValue(this.PasswordHistoryLength);
			encoder.WriteUniquePointer(this.PasswordHistory);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.PresentFields = decoder.ReadUInt32();
			this.PasswordLastSet = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.BadPasswordTime = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.LockoutTime = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.BadPasswordCount = decoder.ReadUInt32();
			this.PasswordHistoryLength = decoder.ReadUInt32();
			this.PasswordHistory = decoder.ReadUniquePointer<SAM_VALIDATE_PASSWORD_HASH[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.PasswordLastSet);
			encoder.WriteStructDeferral(this.BadPasswordTime);
			encoder.WriteStructDeferral(this.LockoutTime);
			if (this.PasswordHistory is not null)
			{
				encoder.WriteArrayHeader(this.PasswordHistory.value);
				for (int i = 0; i < this.PasswordHistory.value.Length; i++)
				{
					SAM_VALIDATE_PASSWORD_HASH elem_0 = this.PasswordHistory.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.PasswordHistory.value.Length; i++)
				{
					SAM_VALIDATE_PASSWORD_HASH elem_0 = this.PasswordHistory.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.PasswordLastSet);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.BadPasswordTime);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LockoutTime);
			if (this.PasswordHistory is not null)
			{
				this.PasswordHistory.value = decoder.ReadArrayHeader<SAM_VALIDATE_PASSWORD_HASH>();
				for (int i = 0; i < this.PasswordHistory.value.Length; i++)
				{
					SAM_VALIDATE_PASSWORD_HASH elem_0 = this.PasswordHistory.value[i];
					elem_0 = decoder.ReadFixedStruct<SAM_VALIDATE_PASSWORD_HASH>(NdrAlignment.NativePtr);
					this.PasswordHistory.value[i] = elem_0;
				}

				for (int i = 0; i < this.PasswordHistory.value.Length; i++)
				{
					SAM_VALIDATE_PASSWORD_HASH elem_0 = this.PasswordHistory.value[i];
					decoder.ReadStructDeferral<SAM_VALIDATE_PASSWORD_HASH>(ref elem_0);
					this.PasswordHistory.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum SAM_VALIDATE_VALIDATION_STATUS : int
	{
		SamValidateSuccess = 0,
		SamValidatePasswordMustChange = 1,
		SamValidateAccountLockedOut = 2,
		SamValidatePasswordExpired = 3,
		SamValidatePasswordIncorrect = 4,
		SamValidatePasswordIsInHistory = 5,
		SamValidatePasswordTooShort = 6,
		SamValidatePasswordTooLong = 7,
		SamValidatePasswordNotComplexEnough = 8,
		SamValidatePasswordTooRecent = 9,
		SamValidatePasswordFilterError = 10
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAM_VALIDATE_STANDARD_OUTPUT_ARG : IRpcFixedStruct
	{
		public SAM_VALIDATE_PERSISTED_FIELDS ChangedPersistedFields;
		public SAM_VALIDATE_VALIDATION_STATUS ValidationStatus;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ChangedPersistedFields, NdrAlignment._8Byte);
			encoder.WriteEnumShortValue((short)this.ValidationStatus);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ChangedPersistedFields = decoder.ReadFixedStruct<SAM_VALIDATE_PERSISTED_FIELDS>(NdrAlignment._8Byte);
			this.ValidationStatus = (SAM_VALIDATE_VALIDATION_STATUS)decoder.ReadEnumShortValue();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ChangedPersistedFields);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SAM_VALIDATE_PERSISTED_FIELDS>(ref this.ChangedPersistedFields);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAM_VALIDATE_AUTHENTICATION_INPUT_ARG : IRpcFixedStruct
	{
		public SAM_VALIDATE_PERSISTED_FIELDS InputPersistedFields;
		public byte PasswordMatched;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.InputPersistedFields, NdrAlignment._8Byte);
			encoder.WriteValue(this.PasswordMatched);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.InputPersistedFields = decoder.ReadFixedStruct<SAM_VALIDATE_PERSISTED_FIELDS>(NdrAlignment._8Byte);
			this.PasswordMatched = decoder.ReadUnsignedChar();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.InputPersistedFields);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SAM_VALIDATE_PERSISTED_FIELDS>(ref this.InputPersistedFields);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAM_VALIDATE_PASSWORD_CHANGE_INPUT_ARG : IRpcFixedStruct
	{
		public SAM_VALIDATE_PERSISTED_FIELDS InputPersistedFields;
		public ms_dtyp.RPC_UNICODE_STRING ClearPassword;
		public ms_dtyp.RPC_UNICODE_STRING UserAccountName;
		public SAM_VALIDATE_PASSWORD_HASH HashedPassword;
		public byte PasswordMatch;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.InputPersistedFields, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.ClearPassword, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.UserAccountName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.HashedPassword, NdrAlignment.NativePtr);
			encoder.WriteValue(this.PasswordMatch);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.InputPersistedFields = decoder.ReadFixedStruct<SAM_VALIDATE_PERSISTED_FIELDS>(NdrAlignment._8Byte);
			this.ClearPassword = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.UserAccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.HashedPassword = decoder.ReadFixedStruct<SAM_VALIDATE_PASSWORD_HASH>(NdrAlignment.NativePtr);
			this.PasswordMatch = decoder.ReadUnsignedChar();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.InputPersistedFields);
			encoder.WriteStructDeferral(this.ClearPassword);
			encoder.WriteStructDeferral(this.UserAccountName);
			encoder.WriteStructDeferral(this.HashedPassword);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SAM_VALIDATE_PERSISTED_FIELDS>(ref this.InputPersistedFields);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ClearPassword);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserAccountName);
			decoder.ReadStructDeferral<SAM_VALIDATE_PASSWORD_HASH>(ref this.HashedPassword);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAM_VALIDATE_PASSWORD_RESET_INPUT_ARG : IRpcFixedStruct
	{
		public SAM_VALIDATE_PERSISTED_FIELDS InputPersistedFields;
		public ms_dtyp.RPC_UNICODE_STRING ClearPassword;
		public ms_dtyp.RPC_UNICODE_STRING UserAccountName;
		public SAM_VALIDATE_PASSWORD_HASH HashedPassword;
		public byte PasswordMustChangeAtNextLogon;
		public byte ClearLockout;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.InputPersistedFields, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.ClearPassword, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.UserAccountName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.HashedPassword, NdrAlignment.NativePtr);
			encoder.WriteValue(this.PasswordMustChangeAtNextLogon);
			encoder.WriteValue(this.ClearLockout);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.InputPersistedFields = decoder.ReadFixedStruct<SAM_VALIDATE_PERSISTED_FIELDS>(NdrAlignment._8Byte);
			this.ClearPassword = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.UserAccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.HashedPassword = decoder.ReadFixedStruct<SAM_VALIDATE_PASSWORD_HASH>(NdrAlignment.NativePtr);
			this.PasswordMustChangeAtNextLogon = decoder.ReadUnsignedChar();
			this.ClearLockout = decoder.ReadUnsignedChar();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.InputPersistedFields);
			encoder.WriteStructDeferral(this.ClearPassword);
			encoder.WriteStructDeferral(this.UserAccountName);
			encoder.WriteStructDeferral(this.HashedPassword);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SAM_VALIDATE_PERSISTED_FIELDS>(ref this.InputPersistedFields);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ClearPassword);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserAccountName);
			decoder.ReadStructDeferral<SAM_VALIDATE_PASSWORD_HASH>(ref this.HashedPassword);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAM_VALIDATE_INPUT_ARG : IRpcFixedStruct
	{
		public PASSWORD_POLICY_VALIDATION_TYPE unionSwitch;
		public SAM_VALIDATE_AUTHENTICATION_INPUT_ARG ValidateAuthenticationInput;
		public SAM_VALIDATE_PASSWORD_CHANGE_INPUT_ARG ValidatePasswordChangeInput;
		public SAM_VALIDATE_PASSWORD_RESET_INPUT_ARG ValidatePasswordResetInput;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._8Byte);
			encoder.WriteEnumShortValue((short)this.unionSwitch);
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteFixedStruct(this.ValidateAuthenticationInput, NdrAlignment._8Byte);
					break;
				case 2:
					encoder.WriteFixedStruct(this.ValidatePasswordChangeInput, NdrAlignment._8Byte);
					break;
				case 3:
					encoder.WriteFixedStruct(this.ValidatePasswordResetInput, NdrAlignment._8Byte);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._8Byte);
			this.unionSwitch = (PASSWORD_POLICY_VALIDATION_TYPE)decoder.ReadEnumShortValue();
			switch ((int)this.unionSwitch)
			{
				case 1:
					this.ValidateAuthenticationInput = decoder.ReadFixedStruct<SAM_VALIDATE_AUTHENTICATION_INPUT_ARG>(NdrAlignment._8Byte);
					break;
				case 2:
					this.ValidatePasswordChangeInput = decoder.ReadFixedStruct<SAM_VALIDATE_PASSWORD_CHANGE_INPUT_ARG>(NdrAlignment._8Byte);
					break;
				case 3:
					this.ValidatePasswordResetInput = decoder.ReadFixedStruct<SAM_VALIDATE_PASSWORD_RESET_INPUT_ARG>(NdrAlignment._8Byte);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteStructDeferral(this.ValidateAuthenticationInput);
					break;
				case 2:
					encoder.WriteStructDeferral(this.ValidatePasswordChangeInput);
					break;
				case 3:
					encoder.WriteStructDeferral(this.ValidatePasswordResetInput);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					decoder.ReadStructDeferral<SAM_VALIDATE_AUTHENTICATION_INPUT_ARG>(ref this.ValidateAuthenticationInput);
					break;
				case 2:
					decoder.ReadStructDeferral<SAM_VALIDATE_PASSWORD_CHANGE_INPUT_ARG>(ref this.ValidatePasswordChangeInput);
					break;
				case 3:
					decoder.ReadStructDeferral<SAM_VALIDATE_PASSWORD_RESET_INPUT_ARG>(ref this.ValidatePasswordResetInput);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SAM_VALIDATE_OUTPUT_ARG : IRpcFixedStruct
	{
		public PASSWORD_POLICY_VALIDATION_TYPE unionSwitch;
		public SAM_VALIDATE_STANDARD_OUTPUT_ARG ValidateAuthenticationOutput;
		public SAM_VALIDATE_STANDARD_OUTPUT_ARG ValidatePasswordChangeOutput;
		public SAM_VALIDATE_STANDARD_OUTPUT_ARG ValidatePasswordResetOutput;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._8Byte);
			encoder.WriteEnumShortValue((short)this.unionSwitch);
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteFixedStruct(this.ValidateAuthenticationOutput, NdrAlignment._8Byte);
					break;
				case 2:
					encoder.WriteFixedStruct(this.ValidatePasswordChangeOutput, NdrAlignment._8Byte);
					break;
				case 3:
					encoder.WriteFixedStruct(this.ValidatePasswordResetOutput, NdrAlignment._8Byte);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._8Byte);
			this.unionSwitch = (PASSWORD_POLICY_VALIDATION_TYPE)decoder.ReadEnumShortValue();
			switch ((int)this.unionSwitch)
			{
				case 1:
					this.ValidateAuthenticationOutput = decoder.ReadFixedStruct<SAM_VALIDATE_STANDARD_OUTPUT_ARG>(NdrAlignment._8Byte);
					break;
				case 2:
					this.ValidatePasswordChangeOutput = decoder.ReadFixedStruct<SAM_VALIDATE_STANDARD_OUTPUT_ARG>(NdrAlignment._8Byte);
					break;
				case 3:
					this.ValidatePasswordResetOutput = decoder.ReadFixedStruct<SAM_VALIDATE_STANDARD_OUTPUT_ARG>(NdrAlignment._8Byte);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteStructDeferral(this.ValidateAuthenticationOutput);
					break;
				case 2:
					encoder.WriteStructDeferral(this.ValidatePasswordChangeOutput);
					break;
				case 3:
					encoder.WriteStructDeferral(this.ValidatePasswordResetOutput);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					decoder.ReadStructDeferral<SAM_VALIDATE_STANDARD_OUTPUT_ARG>(ref this.ValidateAuthenticationOutput);
					break;
				case 2:
					decoder.ReadStructDeferral<SAM_VALIDATE_STANDARD_OUTPUT_ARG>(ref this.ValidatePasswordChangeOutput);
					break;
				case 3:
					decoder.ReadStructDeferral<SAM_VALIDATE_STANDARD_OUTPUT_ARG>(ref this.ValidatePasswordResetOutput);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("12345778-1234-abcd-ef00-0123456789ac"), RpcVersionAttribute(1, 0)]
	public partial interface samr
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrConnect(RpcPointer<char> ServerName, RpcPointer<RpcContextHandle> ServerHandle, uint DesiredAccess, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrCloseHandle(RpcPointer<RpcContextHandle> SamHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrSetSecurityObject(RpcContextHandle ObjectHandle, uint SecurityInformation, SAMPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrQuerySecurityObject(RpcContextHandle ObjectHandle, uint SecurityInformation, RpcPointer<RpcPointer<SAMPR_SR_SECURITY_DESCRIPTOR>> SecurityDescriptor, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum4NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrLookupDomainInSamServer(RpcContextHandle ServerHandle, ms_dtyp.RPC_UNICODE_STRING Name, RpcPointer<RpcPointer<ms_dtyp.RPC_SID>> DomainId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrEnumerateDomainsInSamServer(RpcContextHandle ServerHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrOpenDomain(RpcContextHandle ServerHandle, uint DesiredAccess, ms_dtyp.RPC_SID DomainId, RpcPointer<RpcContextHandle> DomainHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrQueryInformationDomain(RpcContextHandle DomainHandle, DOMAIN_INFORMATION_CLASS DomainInformationClass, RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>> Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrSetInformationDomain(RpcContextHandle DomainHandle, DOMAIN_INFORMATION_CLASS DomainInformationClass, SAMPR_DOMAIN_INFO_BUFFER DomainInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrCreateGroupInDomain(RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING Name, uint DesiredAccess, RpcPointer<RpcContextHandle> GroupHandle, RpcPointer<uint> RelativeId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrEnumerateGroupsInDomain(RpcContextHandle DomainHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrCreateUserInDomain(RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING Name, uint DesiredAccess, RpcPointer<RpcContextHandle> UserHandle, RpcPointer<uint> RelativeId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrEnumerateUsersInDomain(RpcContextHandle DomainHandle, RpcPointer<uint> EnumerationContext, uint UserAccountControl, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrCreateAliasInDomain(RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING AccountName, uint DesiredAccess, RpcPointer<RpcContextHandle> AliasHandle, RpcPointer<uint> RelativeId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrEnumerateAliasesInDomain(RpcContextHandle DomainHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrGetAliasMembership(RpcContextHandle DomainHandle, SAMPR_PSID_ARRAY SidArray, RpcPointer<SAMPR_ULONG_ARRAY> Membership, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrLookupNamesInDomain(RpcContextHandle DomainHandle, uint Count, ArraySegment<ms_dtyp.RPC_UNICODE_STRING> Names, RpcPointer<SAMPR_ULONG_ARRAY> RelativeIds, RpcPointer<SAMPR_ULONG_ARRAY> Use, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrLookupIdsInDomain(RpcContextHandle DomainHandle, uint Count, ArraySegment<uint> RelativeIds, RpcPointer<SAMPR_RETURNED_USTRING_ARRAY> Names, RpcPointer<SAMPR_ULONG_ARRAY> Use, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrOpenGroup(RpcContextHandle DomainHandle, uint DesiredAccess, uint GroupId, RpcPointer<RpcContextHandle> GroupHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrQueryInformationGroup(RpcContextHandle GroupHandle, GROUP_INFORMATION_CLASS GroupInformationClass, RpcPointer<RpcPointer<SAMPR_GROUP_INFO_BUFFER>> Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrSetInformationGroup(RpcContextHandle GroupHandle, GROUP_INFORMATION_CLASS GroupInformationClass, SAMPR_GROUP_INFO_BUFFER Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrAddMemberToGroup(RpcContextHandle GroupHandle, uint MemberId, uint Attributes, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrDeleteGroup(RpcPointer<RpcContextHandle> GroupHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrRemoveMemberFromGroup(RpcContextHandle GroupHandle, uint MemberId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrGetMembersInGroup(RpcContextHandle GroupHandle, RpcPointer<RpcPointer<SAMPR_GET_MEMBERS_BUFFER>> Members, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrSetMemberAttributesOfGroup(RpcContextHandle GroupHandle, uint MemberId, uint Attributes, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrOpenAlias(RpcContextHandle DomainHandle, uint DesiredAccess, uint AliasId, RpcPointer<RpcContextHandle> AliasHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrQueryInformationAlias(RpcContextHandle AliasHandle, ALIAS_INFORMATION_CLASS AliasInformationClass, RpcPointer<RpcPointer<SAMPR_ALIAS_INFO_BUFFER>> Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrSetInformationAlias(RpcContextHandle AliasHandle, ALIAS_INFORMATION_CLASS AliasInformationClass, SAMPR_ALIAS_INFO_BUFFER Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrDeleteAlias(RpcPointer<RpcContextHandle> AliasHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrAddMemberToAlias(RpcContextHandle AliasHandle, ms_dtyp.RPC_SID MemberId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrRemoveMemberFromAlias(RpcContextHandle AliasHandle, ms_dtyp.RPC_SID MemberId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrGetMembersInAlias(RpcContextHandle AliasHandle, RpcPointer<SAMPR_PSID_ARRAY_OUT> Members, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrOpenUser(RpcContextHandle DomainHandle, uint DesiredAccess, uint UserId, RpcPointer<RpcContextHandle> UserHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrDeleteUser(RpcPointer<RpcContextHandle> UserHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrQueryInformationUser(RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>> Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrSetInformationUser(RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, SAMPR_USER_INFO_BUFFER Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrChangePasswordUser(RpcContextHandle UserHandle, byte LmPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmEncryptedWithNewLm, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewLmEncryptedWithOldLm, byte NtPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldNtEncryptedWithNewNt, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewNtEncryptedWithOldNt, byte NtCrossEncryptionPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewNtEncryptedWithNewLm, byte LmCrossEncryptionPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewLmEncryptedWithNewNt, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrGetGroupsForUser(RpcContextHandle UserHandle, RpcPointer<RpcPointer<SAMPR_GET_GROUPS_BUFFER>> Groups, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrQueryDisplayInformation(RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, uint Index, uint EntryCount, uint PreferredMaximumLength, RpcPointer<uint> TotalAvailable, RpcPointer<uint> TotalReturned, RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrGetDisplayEnumerationIndex(RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, ms_dtyp.RPC_UNICODE_STRING Prefix, RpcPointer<uint> Index, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum42NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum43NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrGetUserDomainPasswordInformation(RpcContextHandle UserHandle, RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION> PasswordInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrRemoveMemberFromForeignDomain(RpcContextHandle DomainHandle, ms_dtyp.RPC_SID MemberSid, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrQueryInformationDomain2(RpcContextHandle DomainHandle, DOMAIN_INFORMATION_CLASS DomainInformationClass, RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>> Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrQueryInformationUser2(RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>> Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrQueryDisplayInformation2(RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, uint Index, uint EntryCount, uint PreferredMaximumLength, RpcPointer<uint> TotalAvailable, RpcPointer<uint> TotalReturned, RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrGetDisplayEnumerationIndex2(RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, ms_dtyp.RPC_UNICODE_STRING Prefix, RpcPointer<uint> Index, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrCreateUser2InDomain(RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING Name, uint AccountType, uint DesiredAccess, RpcPointer<RpcContextHandle> UserHandle, RpcPointer<uint> GrantedAccess, RpcPointer<uint> RelativeId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrQueryDisplayInformation3(RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, uint Index, uint EntryCount, uint PreferredMaximumLength, RpcPointer<uint> TotalAvailable, RpcPointer<uint> TotalReturned, RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrAddMultipleMembersToAlias(RpcContextHandle AliasHandle, SAMPR_PSID_ARRAY MembersBuffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrRemoveMultipleMembersFromAlias(RpcContextHandle AliasHandle, SAMPR_PSID_ARRAY MembersBuffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrOemChangePasswordUser2(RpcPointer<RPC_STRING> ServerName, RPC_STRING UserName, RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldLm, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmOwfPasswordEncryptedWithNewLm, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrUnicodeChangePasswordUser2(RpcPointer<ms_dtyp.RPC_UNICODE_STRING> ServerName, ms_dtyp.RPC_UNICODE_STRING UserName, RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldNt, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldNtOwfPasswordEncryptedWithNewNt, byte LmPresent, RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldLm, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmOwfPasswordEncryptedWithNewNt, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrGetDomainPasswordInformation(RpcPointer<ms_dtyp.RPC_UNICODE_STRING> Unused, RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION> PasswordInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrConnect2(string ServerName, RpcPointer<RpcContextHandle> ServerHandle, uint DesiredAccess, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrSetInformationUser2(RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, SAMPR_USER_INFO_BUFFER Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum59NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum60NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum61NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrConnect4(string ServerName, RpcPointer<RpcContextHandle> ServerHandle, uint ClientRevision, uint DesiredAccess, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum63NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrConnect5(string ServerName, uint DesiredAccess, uint InVersion, SAMPR_REVISION_INFO InRevisionInfo, RpcPointer<uint> OutVersion, RpcPointer<SAMPR_REVISION_INFO> OutRevisionInfo, RpcPointer<RpcContextHandle> ServerHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrRidToSid(RpcContextHandle ObjectHandle, uint Rid, RpcPointer<RpcPointer<ms_dtyp.RPC_SID>> Sid, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrSetDSRMPassword(RpcPointer<ms_dtyp.RPC_UNICODE_STRING> Unused, uint UserId, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> EncryptedNtOwfPassword, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> SamrValidatePassword(PASSWORD_POLICY_VALIDATION_TYPE ValidationType, SAM_VALIDATE_INPUT_ARG InputArg, RpcPointer<RpcPointer<SAM_VALIDATE_OUTPUT_ARG>> OutputArg, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum68NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum69NotUsedOnWire(CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("12345778-1234-abcd-ef00-0123456789ac")]
	public partial class samrClientProxy : Titanis.DceRpc.Client.RpcClientProxy, samr, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrConnect(RpcPointer<char> ServerName, RpcPointer<RpcContextHandle> ServerHandle, uint DesiredAccess, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(ServerName);
			if (ServerName is not null)
			{
				encoder.WriteValue(ServerName.value);
			}

			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ServerHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrCloseHandle(RpcPointer<RpcContextHandle> SamHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(SamHandle.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			SamHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrSetSecurityObject(RpcContextHandle ObjectHandle, uint SecurityInformation, SAMPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ObjectHandle);
			encoder.WriteValue(SecurityInformation);
			encoder.WriteFixedStruct(SecurityDescriptor, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(SecurityDescriptor);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrQuerySecurityObject(RpcContextHandle ObjectHandle, uint SecurityInformation, RpcPointer<RpcPointer<SAMPR_SR_SECURITY_DESCRIPTOR>> SecurityDescriptor, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ObjectHandle);
			encoder.WriteValue(SecurityInformation);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			SecurityDescriptor.value = decoder.ReadOutUniquePointer<SAMPR_SR_SECURITY_DESCRIPTOR>(SecurityDescriptor.value);
			if (SecurityDescriptor.value is not null)
			{
				SecurityDescriptor.value.value = decoder.ReadFixedStruct<SAMPR_SR_SECURITY_DESCRIPTOR>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<SAMPR_SR_SECURITY_DESCRIPTOR>(ref SecurityDescriptor.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum4NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrLookupDomainInSamServer(RpcContextHandle ServerHandle, ms_dtyp.RPC_UNICODE_STRING Name, RpcPointer<RpcPointer<ms_dtyp.RPC_SID>> DomainId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ServerHandle);
			encoder.WriteFixedStruct(Name, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Name);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			DomainId.value = decoder.ReadOutUniquePointer<ms_dtyp.RPC_SID>(DomainId.value);
			if (DomainId.value is not null)
			{
				DomainId.value.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref DomainId.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrEnumerateDomainsInSamServer(RpcContextHandle ServerHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ServerHandle);
			encoder.WriteValue(EnumerationContext.value);
			encoder.WriteValue(PreferedMaximumLength);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			EnumerationContext.value = decoder.ReadUInt32();
			Buffer.value = decoder.ReadOutUniquePointer<SAMPR_ENUMERATION_BUFFER>(Buffer.value);
			if (Buffer.value is not null)
			{
				Buffer.value.value = decoder.ReadFixedStruct<SAMPR_ENUMERATION_BUFFER>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<SAMPR_ENUMERATION_BUFFER>(ref Buffer.value.value);
			}

			CountReturned.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrOpenDomain(RpcContextHandle ServerHandle, uint DesiredAccess, ms_dtyp.RPC_SID DomainId, RpcPointer<RpcContextHandle> DomainHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ServerHandle);
			encoder.WriteValue(DesiredAccess);
			encoder.WriteConformantStruct(DomainId, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(DomainId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			DomainHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrQueryInformationDomain(RpcContextHandle DomainHandle, DOMAIN_INFORMATION_CLASS DomainInformationClass, RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>> Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteEnumShortValue((short)DomainInformationClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Buffer.value = decoder.ReadOutUniquePointer<SAMPR_DOMAIN_INFO_BUFFER>(Buffer.value);
			if (Buffer.value is not null)
			{
				Buffer.value.value = decoder.ReadUnion<SAMPR_DOMAIN_INFO_BUFFER>();
				decoder.ReadStructDeferral<SAMPR_DOMAIN_INFO_BUFFER>(ref Buffer.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrSetInformationDomain(RpcContextHandle DomainHandle, DOMAIN_INFORMATION_CLASS DomainInformationClass, SAMPR_DOMAIN_INFO_BUFFER DomainInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteEnumShortValue((short)DomainInformationClass);
			encoder.WriteUnion(DomainInformation);
			encoder.WriteStructDeferral(DomainInformation);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrCreateGroupInDomain(RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING Name, uint DesiredAccess, RpcPointer<RpcContextHandle> GroupHandle, RpcPointer<uint> RelativeId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteFixedStruct(Name, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Name);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			GroupHandle.value = decoder.ReadContextHandle();
			RelativeId.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrEnumerateGroupsInDomain(RpcContextHandle DomainHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteValue(EnumerationContext.value);
			encoder.WriteValue(PreferedMaximumLength);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			EnumerationContext.value = decoder.ReadUInt32();
			Buffer.value = decoder.ReadOutUniquePointer<SAMPR_ENUMERATION_BUFFER>(Buffer.value);
			if (Buffer.value is not null)
			{
				Buffer.value.value = decoder.ReadFixedStruct<SAMPR_ENUMERATION_BUFFER>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<SAMPR_ENUMERATION_BUFFER>(ref Buffer.value.value);
			}

			CountReturned.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrCreateUserInDomain(RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING Name, uint DesiredAccess, RpcPointer<RpcContextHandle> UserHandle, RpcPointer<uint> RelativeId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteFixedStruct(Name, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Name);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			UserHandle.value = decoder.ReadContextHandle();
			RelativeId.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrEnumerateUsersInDomain(RpcContextHandle DomainHandle, RpcPointer<uint> EnumerationContext, uint UserAccountControl, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteValue(EnumerationContext.value);
			encoder.WriteValue(UserAccountControl);
			encoder.WriteValue(PreferedMaximumLength);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			EnumerationContext.value = decoder.ReadUInt32();
			Buffer.value = decoder.ReadOutUniquePointer<SAMPR_ENUMERATION_BUFFER>(Buffer.value);
			if (Buffer.value is not null)
			{
				Buffer.value.value = decoder.ReadFixedStruct<SAMPR_ENUMERATION_BUFFER>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<SAMPR_ENUMERATION_BUFFER>(ref Buffer.value.value);
			}

			CountReturned.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrCreateAliasInDomain(RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING AccountName, uint DesiredAccess, RpcPointer<RpcContextHandle> AliasHandle, RpcPointer<uint> RelativeId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteFixedStruct(AccountName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(AccountName);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			AliasHandle.value = decoder.ReadContextHandle();
			RelativeId.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrEnumerateAliasesInDomain(RpcContextHandle DomainHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteValue(EnumerationContext.value);
			encoder.WriteValue(PreferedMaximumLength);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			EnumerationContext.value = decoder.ReadUInt32();
			Buffer.value = decoder.ReadOutUniquePointer<SAMPR_ENUMERATION_BUFFER>(Buffer.value);
			if (Buffer.value is not null)
			{
				Buffer.value.value = decoder.ReadFixedStruct<SAMPR_ENUMERATION_BUFFER>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<SAMPR_ENUMERATION_BUFFER>(ref Buffer.value.value);
			}

			CountReturned.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrGetAliasMembership(RpcContextHandle DomainHandle, SAMPR_PSID_ARRAY SidArray, RpcPointer<SAMPR_ULONG_ARRAY> Membership, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteFixedStruct(SidArray, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(SidArray);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Membership.value = decoder.ReadFixedStruct<SAMPR_ULONG_ARRAY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SAMPR_ULONG_ARRAY>(ref Membership.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrLookupNamesInDomain(RpcContextHandle DomainHandle, uint Count, ArraySegment<ms_dtyp.RPC_UNICODE_STRING> Names, RpcPointer<SAMPR_ULONG_ARRAY> RelativeIds, RpcPointer<SAMPR_ULONG_ARRAY> Use, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteValue(Count);
			encoder.WriteArrayHeader(Names, true);
			for (int i = 0; i < Names.Count; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names.Item(i);
				encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
			}

			for (int i = 0; i < Names.Count; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names.Item(i);
				encoder.WriteStructDeferral(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			RelativeIds.value = decoder.ReadFixedStruct<SAMPR_ULONG_ARRAY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SAMPR_ULONG_ARRAY>(ref RelativeIds.value);
			Use.value = decoder.ReadFixedStruct<SAMPR_ULONG_ARRAY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SAMPR_ULONG_ARRAY>(ref Use.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrLookupIdsInDomain(RpcContextHandle DomainHandle, uint Count, ArraySegment<uint> RelativeIds, RpcPointer<SAMPR_RETURNED_USTRING_ARRAY> Names, RpcPointer<SAMPR_ULONG_ARRAY> Use, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteValue(Count);
			encoder.WriteArrayHeader(RelativeIds, true);
			for (int i = 0; i < RelativeIds.Count; i++)
			{
				uint elem_0 = RelativeIds.Item(i);
				encoder.WriteValue(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Names.value = decoder.ReadFixedStruct<SAMPR_RETURNED_USTRING_ARRAY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SAMPR_RETURNED_USTRING_ARRAY>(ref Names.value);
			Use.value = decoder.ReadFixedStruct<SAMPR_ULONG_ARRAY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SAMPR_ULONG_ARRAY>(ref Use.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrOpenGroup(RpcContextHandle DomainHandle, uint DesiredAccess, uint GroupId, RpcPointer<RpcContextHandle> GroupHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteValue(DesiredAccess);
			encoder.WriteValue(GroupId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			GroupHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrQueryInformationGroup(RpcContextHandle GroupHandle, GROUP_INFORMATION_CLASS GroupInformationClass, RpcPointer<RpcPointer<SAMPR_GROUP_INFO_BUFFER>> Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(GroupHandle);
			encoder.WriteEnumShortValue((short)GroupInformationClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Buffer.value = decoder.ReadOutUniquePointer<SAMPR_GROUP_INFO_BUFFER>(Buffer.value);
			if (Buffer.value is not null)
			{
				Buffer.value.value = decoder.ReadUnion<SAMPR_GROUP_INFO_BUFFER>();
				decoder.ReadStructDeferral<SAMPR_GROUP_INFO_BUFFER>(ref Buffer.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrSetInformationGroup(RpcContextHandle GroupHandle, GROUP_INFORMATION_CLASS GroupInformationClass, SAMPR_GROUP_INFO_BUFFER Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(GroupHandle);
			encoder.WriteEnumShortValue((short)GroupInformationClass);
			encoder.WriteUnion(Buffer);
			encoder.WriteStructDeferral(Buffer);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrAddMemberToGroup(RpcContextHandle GroupHandle, uint MemberId, uint Attributes, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(GroupHandle);
			encoder.WriteValue(MemberId);
			encoder.WriteValue(Attributes);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrDeleteGroup(RpcPointer<RpcContextHandle> GroupHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(GroupHandle.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			GroupHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrRemoveMemberFromGroup(RpcContextHandle GroupHandle, uint MemberId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(GroupHandle);
			encoder.WriteValue(MemberId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrGetMembersInGroup(RpcContextHandle GroupHandle, RpcPointer<RpcPointer<SAMPR_GET_MEMBERS_BUFFER>> Members, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(GroupHandle);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Members.value = decoder.ReadOutUniquePointer<SAMPR_GET_MEMBERS_BUFFER>(Members.value);
			if (Members.value is not null)
			{
				Members.value.value = decoder.ReadFixedStruct<SAMPR_GET_MEMBERS_BUFFER>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<SAMPR_GET_MEMBERS_BUFFER>(ref Members.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrSetMemberAttributesOfGroup(RpcContextHandle GroupHandle, uint MemberId, uint Attributes, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(GroupHandle);
			encoder.WriteValue(MemberId);
			encoder.WriteValue(Attributes);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrOpenAlias(RpcContextHandle DomainHandle, uint DesiredAccess, uint AliasId, RpcPointer<RpcContextHandle> AliasHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteValue(DesiredAccess);
			encoder.WriteValue(AliasId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			AliasHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrQueryInformationAlias(RpcContextHandle AliasHandle, ALIAS_INFORMATION_CLASS AliasInformationClass, RpcPointer<RpcPointer<SAMPR_ALIAS_INFO_BUFFER>> Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AliasHandle);
			encoder.WriteEnumShortValue((short)AliasInformationClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Buffer.value = decoder.ReadOutUniquePointer<SAMPR_ALIAS_INFO_BUFFER>(Buffer.value);
			if (Buffer.value is not null)
			{
				Buffer.value.value = decoder.ReadUnion<SAMPR_ALIAS_INFO_BUFFER>();
				decoder.ReadStructDeferral<SAMPR_ALIAS_INFO_BUFFER>(ref Buffer.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrSetInformationAlias(RpcContextHandle AliasHandle, ALIAS_INFORMATION_CLASS AliasInformationClass, SAMPR_ALIAS_INFO_BUFFER Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AliasHandle);
			encoder.WriteEnumShortValue((short)AliasInformationClass);
			encoder.WriteUnion(Buffer);
			encoder.WriteStructDeferral(Buffer);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrDeleteAlias(RpcPointer<RpcContextHandle> AliasHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AliasHandle.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			AliasHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrAddMemberToAlias(RpcContextHandle AliasHandle, ms_dtyp.RPC_SID MemberId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AliasHandle);
			encoder.WriteConformantStruct(MemberId, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(MemberId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrRemoveMemberFromAlias(RpcContextHandle AliasHandle, ms_dtyp.RPC_SID MemberId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AliasHandle);
			encoder.WriteConformantStruct(MemberId, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(MemberId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrGetMembersInAlias(RpcContextHandle AliasHandle, RpcPointer<SAMPR_PSID_ARRAY_OUT> Members, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AliasHandle);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Members.value = decoder.ReadFixedStruct<SAMPR_PSID_ARRAY_OUT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SAMPR_PSID_ARRAY_OUT>(ref Members.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrOpenUser(RpcContextHandle DomainHandle, uint DesiredAccess, uint UserId, RpcPointer<RpcContextHandle> UserHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteValue(DesiredAccess);
			encoder.WriteValue(UserId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			UserHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrDeleteUser(RpcPointer<RpcContextHandle> UserHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(UserHandle.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			UserHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrQueryInformationUser(RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>> Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(36);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(UserHandle);
			encoder.WriteEnumShortValue((short)UserInformationClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Buffer.value = decoder.ReadOutUniquePointer<SAMPR_USER_INFO_BUFFER>(Buffer.value);
			if (Buffer.value is not null)
			{
				Buffer.value.value = decoder.ReadUnion<SAMPR_USER_INFO_BUFFER>();
				decoder.ReadStructDeferral<SAMPR_USER_INFO_BUFFER>(ref Buffer.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrSetInformationUser(RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, SAMPR_USER_INFO_BUFFER Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(37);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(UserHandle);
			encoder.WriteEnumShortValue((short)UserInformationClass);
			encoder.WriteUnion(Buffer);
			encoder.WriteStructDeferral(Buffer);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrChangePasswordUser(RpcContextHandle UserHandle, byte LmPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmEncryptedWithNewLm, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewLmEncryptedWithOldLm, byte NtPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldNtEncryptedWithNewNt, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewNtEncryptedWithOldNt, byte NtCrossEncryptionPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewNtEncryptedWithNewLm, byte LmCrossEncryptionPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewLmEncryptedWithNewNt, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(38);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(UserHandle);
			encoder.WriteValue(LmPresent);
			encoder.WriteUniquePointer(OldLmEncryptedWithNewLm);
			if (OldLmEncryptedWithNewLm is not null)
			{
				encoder.WriteFixedStruct(OldLmEncryptedWithNewLm.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(OldLmEncryptedWithNewLm.value);
			}

			encoder.WriteUniquePointer(NewLmEncryptedWithOldLm);
			if (NewLmEncryptedWithOldLm is not null)
			{
				encoder.WriteFixedStruct(NewLmEncryptedWithOldLm.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(NewLmEncryptedWithOldLm.value);
			}

			encoder.WriteValue(NtPresent);
			encoder.WriteUniquePointer(OldNtEncryptedWithNewNt);
			if (OldNtEncryptedWithNewNt is not null)
			{
				encoder.WriteFixedStruct(OldNtEncryptedWithNewNt.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(OldNtEncryptedWithNewNt.value);
			}

			encoder.WriteUniquePointer(NewNtEncryptedWithOldNt);
			if (NewNtEncryptedWithOldNt is not null)
			{
				encoder.WriteFixedStruct(NewNtEncryptedWithOldNt.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(NewNtEncryptedWithOldNt.value);
			}

			encoder.WriteValue(NtCrossEncryptionPresent);
			encoder.WriteUniquePointer(NewNtEncryptedWithNewLm);
			if (NewNtEncryptedWithNewLm is not null)
			{
				encoder.WriteFixedStruct(NewNtEncryptedWithNewLm.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(NewNtEncryptedWithNewLm.value);
			}

			encoder.WriteValue(LmCrossEncryptionPresent);
			encoder.WriteUniquePointer(NewLmEncryptedWithNewNt);
			if (NewLmEncryptedWithNewNt is not null)
			{
				encoder.WriteFixedStruct(NewLmEncryptedWithNewNt.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(NewLmEncryptedWithNewNt.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrGetGroupsForUser(RpcContextHandle UserHandle, RpcPointer<RpcPointer<SAMPR_GET_GROUPS_BUFFER>> Groups, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(39);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(UserHandle);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Groups.value = decoder.ReadOutUniquePointer<SAMPR_GET_GROUPS_BUFFER>(Groups.value);
			if (Groups.value is not null)
			{
				Groups.value.value = decoder.ReadFixedStruct<SAMPR_GET_GROUPS_BUFFER>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<SAMPR_GET_GROUPS_BUFFER>(ref Groups.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrQueryDisplayInformation(RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, uint Index, uint EntryCount, uint PreferredMaximumLength, RpcPointer<uint> TotalAvailable, RpcPointer<uint> TotalReturned, RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(40);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteEnumShortValue((short)DisplayInformationClass);
			encoder.WriteValue(Index);
			encoder.WriteValue(EntryCount);
			encoder.WriteValue(PreferredMaximumLength);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			TotalAvailable.value = decoder.ReadUInt32();
			TotalReturned.value = decoder.ReadUInt32();
			Buffer.value = decoder.ReadUnion<SAMPR_DISPLAY_INFO_BUFFER>();
			decoder.ReadStructDeferral<SAMPR_DISPLAY_INFO_BUFFER>(ref Buffer.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrGetDisplayEnumerationIndex(RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, ms_dtyp.RPC_UNICODE_STRING Prefix, RpcPointer<uint> Index, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(41);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteEnumShortValue((short)DisplayInformationClass);
			encoder.WriteFixedStruct(Prefix, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Prefix);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Index.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum42NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(42);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum43NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(43);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrGetUserDomainPasswordInformation(RpcContextHandle UserHandle, RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION> PasswordInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(44);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(UserHandle);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			PasswordInformation.value = decoder.ReadFixedStruct<USER_DOMAIN_PASSWORD_INFORMATION>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<USER_DOMAIN_PASSWORD_INFORMATION>(ref PasswordInformation.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrRemoveMemberFromForeignDomain(RpcContextHandle DomainHandle, ms_dtyp.RPC_SID MemberSid, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(45);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteConformantStruct(MemberSid, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(MemberSid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrQueryInformationDomain2(RpcContextHandle DomainHandle, DOMAIN_INFORMATION_CLASS DomainInformationClass, RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>> Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(46);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteEnumShortValue((short)DomainInformationClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Buffer.value = decoder.ReadOutUniquePointer<SAMPR_DOMAIN_INFO_BUFFER>(Buffer.value);
			if (Buffer.value is not null)
			{
				Buffer.value.value = decoder.ReadUnion<SAMPR_DOMAIN_INFO_BUFFER>();
				decoder.ReadStructDeferral<SAMPR_DOMAIN_INFO_BUFFER>(ref Buffer.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrQueryInformationUser2(RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>> Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(47);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(UserHandle);
			encoder.WriteEnumShortValue((short)UserInformationClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Buffer.value = decoder.ReadOutUniquePointer<SAMPR_USER_INFO_BUFFER>(Buffer.value);
			if (Buffer.value is not null)
			{
				Buffer.value.value = decoder.ReadUnion<SAMPR_USER_INFO_BUFFER>();
				decoder.ReadStructDeferral<SAMPR_USER_INFO_BUFFER>(ref Buffer.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrQueryDisplayInformation2(RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, uint Index, uint EntryCount, uint PreferredMaximumLength, RpcPointer<uint> TotalAvailable, RpcPointer<uint> TotalReturned, RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(48);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteEnumShortValue((short)DisplayInformationClass);
			encoder.WriteValue(Index);
			encoder.WriteValue(EntryCount);
			encoder.WriteValue(PreferredMaximumLength);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			TotalAvailable.value = decoder.ReadUInt32();
			TotalReturned.value = decoder.ReadUInt32();
			Buffer.value = decoder.ReadUnion<SAMPR_DISPLAY_INFO_BUFFER>();
			decoder.ReadStructDeferral<SAMPR_DISPLAY_INFO_BUFFER>(ref Buffer.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrGetDisplayEnumerationIndex2(RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, ms_dtyp.RPC_UNICODE_STRING Prefix, RpcPointer<uint> Index, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(49);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteEnumShortValue((short)DisplayInformationClass);
			encoder.WriteFixedStruct(Prefix, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Prefix);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Index.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrCreateUser2InDomain(RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING Name, uint AccountType, uint DesiredAccess, RpcPointer<RpcContextHandle> UserHandle, RpcPointer<uint> GrantedAccess, RpcPointer<uint> RelativeId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(50);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteFixedStruct(Name, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Name);
			encoder.WriteValue(AccountType);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			UserHandle.value = decoder.ReadContextHandle();
			GrantedAccess.value = decoder.ReadUInt32();
			RelativeId.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrQueryDisplayInformation3(RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, uint Index, uint EntryCount, uint PreferredMaximumLength, RpcPointer<uint> TotalAvailable, RpcPointer<uint> TotalReturned, RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(51);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(DomainHandle);
			encoder.WriteEnumShortValue((short)DisplayInformationClass);
			encoder.WriteValue(Index);
			encoder.WriteValue(EntryCount);
			encoder.WriteValue(PreferredMaximumLength);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			TotalAvailable.value = decoder.ReadUInt32();
			TotalReturned.value = decoder.ReadUInt32();
			Buffer.value = decoder.ReadUnion<SAMPR_DISPLAY_INFO_BUFFER>();
			decoder.ReadStructDeferral<SAMPR_DISPLAY_INFO_BUFFER>(ref Buffer.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrAddMultipleMembersToAlias(RpcContextHandle AliasHandle, SAMPR_PSID_ARRAY MembersBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(52);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AliasHandle);
			encoder.WriteFixedStruct(MembersBuffer, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(MembersBuffer);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrRemoveMultipleMembersFromAlias(RpcContextHandle AliasHandle, SAMPR_PSID_ARRAY MembersBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(53);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AliasHandle);
			encoder.WriteFixedStruct(MembersBuffer, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(MembersBuffer);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrOemChangePasswordUser2(RpcPointer<RPC_STRING> ServerName, RPC_STRING UserName, RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldLm, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmOwfPasswordEncryptedWithNewLm, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(54);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(ServerName);
			if (ServerName is not null)
			{
				encoder.WriteFixedStruct(ServerName.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ServerName.value);
			}

			encoder.WriteFixedStruct(UserName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(UserName);
			encoder.WriteUniquePointer(NewPasswordEncryptedWithOldLm);
			if (NewPasswordEncryptedWithOldLm is not null)
			{
				encoder.WriteFixedStruct(NewPasswordEncryptedWithOldLm.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(NewPasswordEncryptedWithOldLm.value);
			}

			encoder.WriteUniquePointer(OldLmOwfPasswordEncryptedWithNewLm);
			if (OldLmOwfPasswordEncryptedWithNewLm is not null)
			{
				encoder.WriteFixedStruct(OldLmOwfPasswordEncryptedWithNewLm.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(OldLmOwfPasswordEncryptedWithNewLm.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrUnicodeChangePasswordUser2(RpcPointer<ms_dtyp.RPC_UNICODE_STRING> ServerName, ms_dtyp.RPC_UNICODE_STRING UserName, RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldNt, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldNtOwfPasswordEncryptedWithNewNt, byte LmPresent, RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldLm, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmOwfPasswordEncryptedWithNewNt, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(55);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(ServerName);
			if (ServerName is not null)
			{
				encoder.WriteFixedStruct(ServerName.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ServerName.value);
			}

			encoder.WriteFixedStruct(UserName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(UserName);
			encoder.WriteUniquePointer(NewPasswordEncryptedWithOldNt);
			if (NewPasswordEncryptedWithOldNt is not null)
			{
				encoder.WriteFixedStruct(NewPasswordEncryptedWithOldNt.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(NewPasswordEncryptedWithOldNt.value);
			}

			encoder.WriteUniquePointer(OldNtOwfPasswordEncryptedWithNewNt);
			if (OldNtOwfPasswordEncryptedWithNewNt is not null)
			{
				encoder.WriteFixedStruct(OldNtOwfPasswordEncryptedWithNewNt.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(OldNtOwfPasswordEncryptedWithNewNt.value);
			}

			encoder.WriteValue(LmPresent);
			encoder.WriteUniquePointer(NewPasswordEncryptedWithOldLm);
			if (NewPasswordEncryptedWithOldLm is not null)
			{
				encoder.WriteFixedStruct(NewPasswordEncryptedWithOldLm.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(NewPasswordEncryptedWithOldLm.value);
			}

			encoder.WriteUniquePointer(OldLmOwfPasswordEncryptedWithNewNt);
			if (OldLmOwfPasswordEncryptedWithNewNt is not null)
			{
				encoder.WriteFixedStruct(OldLmOwfPasswordEncryptedWithNewNt.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(OldLmOwfPasswordEncryptedWithNewNt.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrGetDomainPasswordInformation(RpcPointer<ms_dtyp.RPC_UNICODE_STRING> Unused, RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION> PasswordInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(56);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(Unused);
			if (Unused is not null)
			{
				encoder.WriteFixedStruct(Unused.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(Unused.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			PasswordInformation.value = decoder.ReadFixedStruct<USER_DOMAIN_PASSWORD_INFORMATION>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<USER_DOMAIN_PASSWORD_INFORMATION>(ref PasswordInformation.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrConnect2(string ServerName, RpcPointer<RpcContextHandle> ServerHandle, uint DesiredAccess, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(57);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ServerHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrSetInformationUser2(RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, SAMPR_USER_INFO_BUFFER Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(58);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(UserHandle);
			encoder.WriteEnumShortValue((short)UserInformationClass);
			encoder.WriteUnion(Buffer);
			encoder.WriteStructDeferral(Buffer);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum59NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(59);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum60NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(60);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum61NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(61);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrConnect4(string ServerName, RpcPointer<RpcContextHandle> ServerHandle, uint ClientRevision, uint DesiredAccess, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(62);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(ClientRevision);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ServerHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum63NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(63);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrConnect5(string ServerName, uint DesiredAccess, uint InVersion, SAMPR_REVISION_INFO InRevisionInfo, RpcPointer<uint> OutVersion, RpcPointer<SAMPR_REVISION_INFO> OutRevisionInfo, RpcPointer<RpcContextHandle> ServerHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(64);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(DesiredAccess);
			encoder.WriteValue(InVersion);
			encoder.WriteUnion(InRevisionInfo);
			encoder.WriteStructDeferral(InRevisionInfo);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			OutVersion.value = decoder.ReadUInt32();
			OutRevisionInfo.value = decoder.ReadUnion<SAMPR_REVISION_INFO>();
			decoder.ReadStructDeferral<SAMPR_REVISION_INFO>(ref OutRevisionInfo.value);
			ServerHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrRidToSid(RpcContextHandle ObjectHandle, uint Rid, RpcPointer<RpcPointer<ms_dtyp.RPC_SID>> Sid, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(65);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ObjectHandle);
			encoder.WriteValue(Rid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Sid.value = decoder.ReadOutUniquePointer<ms_dtyp.RPC_SID>(Sid.value);
			if (Sid.value is not null)
			{
				Sid.value.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref Sid.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrSetDSRMPassword(RpcPointer<ms_dtyp.RPC_UNICODE_STRING> Unused, uint UserId, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> EncryptedNtOwfPassword, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(66);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(Unused);
			if (Unused is not null)
			{
				encoder.WriteFixedStruct(Unused.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(Unused.value);
			}

			encoder.WriteValue(UserId);
			encoder.WriteUniquePointer(EncryptedNtOwfPassword);
			if (EncryptedNtOwfPassword is not null)
			{
				encoder.WriteFixedStruct(EncryptedNtOwfPassword.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(EncryptedNtOwfPassword.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> SamrValidatePassword(PASSWORD_POLICY_VALIDATION_TYPE ValidationType, SAM_VALIDATE_INPUT_ARG InputArg, RpcPointer<RpcPointer<SAM_VALIDATE_OUTPUT_ARG>> OutputArg, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(67);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteEnumShortValue((short)ValidationType);
			encoder.WriteUnion(InputArg);
			encoder.WriteStructDeferral(InputArg);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			OutputArg.value = decoder.ReadOutUniquePointer<SAM_VALIDATE_OUTPUT_ARG>(OutputArg.value);
			if (OutputArg.value is not null)
			{
				OutputArg.value.value = decoder.ReadUnion<SAM_VALIDATE_OUTPUT_ARG>();
				decoder.ReadStructDeferral<SAM_VALIDATE_OUTPUT_ARG>(ref OutputArg.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum68NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(68);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum69NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(69);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		public sealed override Type InterfaceType => typeof(samr);
		private static Guid _interfaceUuid = new Guid("12345778-1234-abcd-ef00-0123456789ac");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class samrStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrConnect(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> ServerName;
			RpcPointer<RpcContextHandle> ServerHandle = new RpcPointer<RpcContextHandle>();
			uint DesiredAccess;
			ServerName = decoder.ReadUniquePointer<char>();
			if (ServerName is not null)
			{
				ServerName.value = decoder.ReadWideChar();
			}

			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrConnect(ServerName, ServerHandle, DesiredAccess, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(ServerHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrCloseHandle(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> SamHandle;
			SamHandle = new RpcPointer<RpcContextHandle>();
			SamHandle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.SamrCloseHandle(SamHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(SamHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrSetSecurityObject(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle ObjectHandle;
			uint SecurityInformation;
			SAMPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor;
			ObjectHandle = decoder.ReadContextHandle();
			SecurityInformation = decoder.ReadUInt32();
			SecurityDescriptor = decoder.ReadFixedStruct<SAMPR_SR_SECURITY_DESCRIPTOR>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SAMPR_SR_SECURITY_DESCRIPTOR>(ref SecurityDescriptor);
			var invokeTask = this._obj.SamrSetSecurityObject(ObjectHandle, SecurityInformation, SecurityDescriptor, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrQuerySecurityObject(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle ObjectHandle;
			uint SecurityInformation;
			RpcPointer<RpcPointer<SAMPR_SR_SECURITY_DESCRIPTOR>> SecurityDescriptor = new RpcPointer<RpcPointer<SAMPR_SR_SECURITY_DESCRIPTOR>>();
			ObjectHandle = decoder.ReadContextHandle();
			SecurityInformation = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrQuerySecurityObject(ObjectHandle, SecurityInformation, SecurityDescriptor, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(SecurityDescriptor.value);
			if (SecurityDescriptor.value is not null)
			{
				encoder.WriteFixedStruct(SecurityDescriptor.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(SecurityDescriptor.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum4NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum4NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrLookupDomainInSamServer(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle ServerHandle;
			ms_dtyp.RPC_UNICODE_STRING Name;
			RpcPointer<RpcPointer<ms_dtyp.RPC_SID>> DomainId = new RpcPointer<RpcPointer<ms_dtyp.RPC_SID>>();
			ServerHandle = decoder.ReadContextHandle();
			Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name);
			var invokeTask = this._obj.SamrLookupDomainInSamServer(ServerHandle, Name, DomainId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(DomainId.value);
			if (DomainId.value is not null)
			{
				encoder.WriteConformantStruct(DomainId.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(DomainId.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrEnumerateDomainsInSamServer(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle ServerHandle;
			RpcPointer<uint> EnumerationContext;
			RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>>();
			uint PreferedMaximumLength;
			RpcPointer<uint> CountReturned = new RpcPointer<uint>();
			ServerHandle = decoder.ReadContextHandle();
			EnumerationContext = new RpcPointer<uint>();
			EnumerationContext.value = decoder.ReadUInt32();
			PreferedMaximumLength = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrEnumerateDomainsInSamServer(ServerHandle, EnumerationContext, Buffer, PreferedMaximumLength, CountReturned, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(EnumerationContext.value);
			encoder.WriteUniquePointer(Buffer.value);
			if (Buffer.value is not null)
			{
				encoder.WriteFixedStruct(Buffer.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(Buffer.value.value);
			}

			encoder.WriteValue(CountReturned.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrOpenDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle ServerHandle;
			uint DesiredAccess;
			ms_dtyp.RPC_SID DomainId;
			RpcPointer<RpcContextHandle> DomainHandle = new RpcPointer<RpcContextHandle>();
			ServerHandle = decoder.ReadContextHandle();
			DesiredAccess = decoder.ReadUInt32();
			DomainId = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref DomainId);
			var invokeTask = this._obj.SamrOpenDomain(ServerHandle, DesiredAccess, DomainId, DomainHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(DomainHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrQueryInformationDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			DOMAIN_INFORMATION_CLASS DomainInformationClass;
			RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>>();
			DomainHandle = decoder.ReadContextHandle();
			DomainInformationClass = (DOMAIN_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.SamrQueryInformationDomain(DomainHandle, DomainInformationClass, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(Buffer.value);
			if (Buffer.value is not null)
			{
				encoder.WriteUnion(Buffer.value.value);
				encoder.WriteStructDeferral(Buffer.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrSetInformationDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			DOMAIN_INFORMATION_CLASS DomainInformationClass;
			SAMPR_DOMAIN_INFO_BUFFER DomainInformation;
			DomainHandle = decoder.ReadContextHandle();
			DomainInformationClass = (DOMAIN_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			DomainInformation = decoder.ReadUnion<SAMPR_DOMAIN_INFO_BUFFER>();
			decoder.ReadStructDeferral<SAMPR_DOMAIN_INFO_BUFFER>(ref DomainInformation);
			var invokeTask = this._obj.SamrSetInformationDomain(DomainHandle, DomainInformationClass, DomainInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrCreateGroupInDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			ms_dtyp.RPC_UNICODE_STRING Name;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> GroupHandle = new RpcPointer<RpcContextHandle>();
			RpcPointer<uint> RelativeId = new RpcPointer<uint>();
			DomainHandle = decoder.ReadContextHandle();
			Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrCreateGroupInDomain(DomainHandle, Name, DesiredAccess, GroupHandle, RelativeId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(GroupHandle.value);
			encoder.WriteValue(RelativeId.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrEnumerateGroupsInDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			RpcPointer<uint> EnumerationContext;
			RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>>();
			uint PreferedMaximumLength;
			RpcPointer<uint> CountReturned = new RpcPointer<uint>();
			DomainHandle = decoder.ReadContextHandle();
			EnumerationContext = new RpcPointer<uint>();
			EnumerationContext.value = decoder.ReadUInt32();
			PreferedMaximumLength = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrEnumerateGroupsInDomain(DomainHandle, EnumerationContext, Buffer, PreferedMaximumLength, CountReturned, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(EnumerationContext.value);
			encoder.WriteUniquePointer(Buffer.value);
			if (Buffer.value is not null)
			{
				encoder.WriteFixedStruct(Buffer.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(Buffer.value.value);
			}

			encoder.WriteValue(CountReturned.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrCreateUserInDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			ms_dtyp.RPC_UNICODE_STRING Name;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> UserHandle = new RpcPointer<RpcContextHandle>();
			RpcPointer<uint> RelativeId = new RpcPointer<uint>();
			DomainHandle = decoder.ReadContextHandle();
			Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrCreateUserInDomain(DomainHandle, Name, DesiredAccess, UserHandle, RelativeId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(UserHandle.value);
			encoder.WriteValue(RelativeId.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrEnumerateUsersInDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			RpcPointer<uint> EnumerationContext;
			uint UserAccountControl;
			RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>>();
			uint PreferedMaximumLength;
			RpcPointer<uint> CountReturned = new RpcPointer<uint>();
			DomainHandle = decoder.ReadContextHandle();
			EnumerationContext = new RpcPointer<uint>();
			EnumerationContext.value = decoder.ReadUInt32();
			UserAccountControl = decoder.ReadUInt32();
			PreferedMaximumLength = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrEnumerateUsersInDomain(DomainHandle, EnumerationContext, UserAccountControl, Buffer, PreferedMaximumLength, CountReturned, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(EnumerationContext.value);
			encoder.WriteUniquePointer(Buffer.value);
			if (Buffer.value is not null)
			{
				encoder.WriteFixedStruct(Buffer.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(Buffer.value.value);
			}

			encoder.WriteValue(CountReturned.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrCreateAliasInDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			ms_dtyp.RPC_UNICODE_STRING AccountName;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> AliasHandle = new RpcPointer<RpcContextHandle>();
			RpcPointer<uint> RelativeId = new RpcPointer<uint>();
			DomainHandle = decoder.ReadContextHandle();
			AccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref AccountName);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrCreateAliasInDomain(DomainHandle, AccountName, DesiredAccess, AliasHandle, RelativeId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(AliasHandle.value);
			encoder.WriteValue(RelativeId.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrEnumerateAliasesInDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			RpcPointer<uint> EnumerationContext;
			RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>>();
			uint PreferedMaximumLength;
			RpcPointer<uint> CountReturned = new RpcPointer<uint>();
			DomainHandle = decoder.ReadContextHandle();
			EnumerationContext = new RpcPointer<uint>();
			EnumerationContext.value = decoder.ReadUInt32();
			PreferedMaximumLength = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrEnumerateAliasesInDomain(DomainHandle, EnumerationContext, Buffer, PreferedMaximumLength, CountReturned, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(EnumerationContext.value);
			encoder.WriteUniquePointer(Buffer.value);
			if (Buffer.value is not null)
			{
				encoder.WriteFixedStruct(Buffer.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(Buffer.value.value);
			}

			encoder.WriteValue(CountReturned.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrGetAliasMembership(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			SAMPR_PSID_ARRAY SidArray;
			RpcPointer<SAMPR_ULONG_ARRAY> Membership = new RpcPointer<SAMPR_ULONG_ARRAY>();
			DomainHandle = decoder.ReadContextHandle();
			SidArray = decoder.ReadFixedStruct<SAMPR_PSID_ARRAY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SAMPR_PSID_ARRAY>(ref SidArray);
			var invokeTask = this._obj.SamrGetAliasMembership(DomainHandle, SidArray, Membership, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(Membership.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Membership.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrLookupNamesInDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			uint Count;
			ArraySegment<ms_dtyp.RPC_UNICODE_STRING> Names;
			RpcPointer<SAMPR_ULONG_ARRAY> RelativeIds = new RpcPointer<SAMPR_ULONG_ARRAY>();
			RpcPointer<SAMPR_ULONG_ARRAY> Use = new RpcPointer<SAMPR_ULONG_ARRAY>();
			DomainHandle = decoder.ReadContextHandle();
			Count = decoder.ReadUInt32();
			Names = decoder.ReadArraySegmentHeader<ms_dtyp.RPC_UNICODE_STRING>();
			for (int i = 0; i < Names.Count; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names.Item(i);
				elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				Names.Item(i) = elem_0;
			}

			for (int i = 0; i < Names.Count; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names.Item(i);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
				Names.Item(i) = elem_0;
			}

			var invokeTask = this._obj.SamrLookupNamesInDomain(DomainHandle, Count, Names, RelativeIds, Use, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(RelativeIds.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(RelativeIds.value);
			encoder.WriteFixedStruct(Use.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Use.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrLookupIdsInDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			uint Count;
			ArraySegment<uint> RelativeIds;
			RpcPointer<SAMPR_RETURNED_USTRING_ARRAY> Names = new RpcPointer<SAMPR_RETURNED_USTRING_ARRAY>();
			RpcPointer<SAMPR_ULONG_ARRAY> Use = new RpcPointer<SAMPR_ULONG_ARRAY>();
			DomainHandle = decoder.ReadContextHandle();
			Count = decoder.ReadUInt32();
			RelativeIds = decoder.ReadArraySegmentHeader<uint>();
			for (int i = 0; i < RelativeIds.Count; i++)
			{
				uint elem_0 = RelativeIds.Item(i);
				elem_0 = decoder.ReadUInt32();
				RelativeIds.Item(i) = elem_0;
			}

			var invokeTask = this._obj.SamrLookupIdsInDomain(DomainHandle, Count, RelativeIds, Names, Use, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(Names.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Names.value);
			encoder.WriteFixedStruct(Use.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Use.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrOpenGroup(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			uint DesiredAccess;
			uint GroupId;
			RpcPointer<RpcContextHandle> GroupHandle = new RpcPointer<RpcContextHandle>();
			DomainHandle = decoder.ReadContextHandle();
			DesiredAccess = decoder.ReadUInt32();
			GroupId = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrOpenGroup(DomainHandle, DesiredAccess, GroupId, GroupHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(GroupHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrQueryInformationGroup(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle GroupHandle;
			GROUP_INFORMATION_CLASS GroupInformationClass;
			RpcPointer<RpcPointer<SAMPR_GROUP_INFO_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_GROUP_INFO_BUFFER>>();
			GroupHandle = decoder.ReadContextHandle();
			GroupInformationClass = (GROUP_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.SamrQueryInformationGroup(GroupHandle, GroupInformationClass, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(Buffer.value);
			if (Buffer.value is not null)
			{
				encoder.WriteUnion(Buffer.value.value);
				encoder.WriteStructDeferral(Buffer.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrSetInformationGroup(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle GroupHandle;
			GROUP_INFORMATION_CLASS GroupInformationClass;
			SAMPR_GROUP_INFO_BUFFER Buffer;
			GroupHandle = decoder.ReadContextHandle();
			GroupInformationClass = (GROUP_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			Buffer = decoder.ReadUnion<SAMPR_GROUP_INFO_BUFFER>();
			decoder.ReadStructDeferral<SAMPR_GROUP_INFO_BUFFER>(ref Buffer);
			var invokeTask = this._obj.SamrSetInformationGroup(GroupHandle, GroupInformationClass, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrAddMemberToGroup(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle GroupHandle;
			uint MemberId;
			uint Attributes;
			GroupHandle = decoder.ReadContextHandle();
			MemberId = decoder.ReadUInt32();
			Attributes = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrAddMemberToGroup(GroupHandle, MemberId, Attributes, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrDeleteGroup(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> GroupHandle;
			GroupHandle = new RpcPointer<RpcContextHandle>();
			GroupHandle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.SamrDeleteGroup(GroupHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(GroupHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrRemoveMemberFromGroup(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle GroupHandle;
			uint MemberId;
			GroupHandle = decoder.ReadContextHandle();
			MemberId = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrRemoveMemberFromGroup(GroupHandle, MemberId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrGetMembersInGroup(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle GroupHandle;
			RpcPointer<RpcPointer<SAMPR_GET_MEMBERS_BUFFER>> Members = new RpcPointer<RpcPointer<SAMPR_GET_MEMBERS_BUFFER>>();
			GroupHandle = decoder.ReadContextHandle();
			var invokeTask = this._obj.SamrGetMembersInGroup(GroupHandle, Members, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(Members.value);
			if (Members.value is not null)
			{
				encoder.WriteFixedStruct(Members.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(Members.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrSetMemberAttributesOfGroup(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle GroupHandle;
			uint MemberId;
			uint Attributes;
			GroupHandle = decoder.ReadContextHandle();
			MemberId = decoder.ReadUInt32();
			Attributes = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrSetMemberAttributesOfGroup(GroupHandle, MemberId, Attributes, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrOpenAlias(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			uint DesiredAccess;
			uint AliasId;
			RpcPointer<RpcContextHandle> AliasHandle = new RpcPointer<RpcContextHandle>();
			DomainHandle = decoder.ReadContextHandle();
			DesiredAccess = decoder.ReadUInt32();
			AliasId = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrOpenAlias(DomainHandle, DesiredAccess, AliasId, AliasHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(AliasHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrQueryInformationAlias(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle AliasHandle;
			ALIAS_INFORMATION_CLASS AliasInformationClass;
			RpcPointer<RpcPointer<SAMPR_ALIAS_INFO_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_ALIAS_INFO_BUFFER>>();
			AliasHandle = decoder.ReadContextHandle();
			AliasInformationClass = (ALIAS_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.SamrQueryInformationAlias(AliasHandle, AliasInformationClass, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(Buffer.value);
			if (Buffer.value is not null)
			{
				encoder.WriteUnion(Buffer.value.value);
				encoder.WriteStructDeferral(Buffer.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrSetInformationAlias(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle AliasHandle;
			ALIAS_INFORMATION_CLASS AliasInformationClass;
			SAMPR_ALIAS_INFO_BUFFER Buffer;
			AliasHandle = decoder.ReadContextHandle();
			AliasInformationClass = (ALIAS_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			Buffer = decoder.ReadUnion<SAMPR_ALIAS_INFO_BUFFER>();
			decoder.ReadStructDeferral<SAMPR_ALIAS_INFO_BUFFER>(ref Buffer);
			var invokeTask = this._obj.SamrSetInformationAlias(AliasHandle, AliasInformationClass, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrDeleteAlias(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> AliasHandle;
			AliasHandle = new RpcPointer<RpcContextHandle>();
			AliasHandle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.SamrDeleteAlias(AliasHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(AliasHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrAddMemberToAlias(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle AliasHandle;
			ms_dtyp.RPC_SID MemberId;
			AliasHandle = decoder.ReadContextHandle();
			MemberId = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref MemberId);
			var invokeTask = this._obj.SamrAddMemberToAlias(AliasHandle, MemberId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrRemoveMemberFromAlias(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle AliasHandle;
			ms_dtyp.RPC_SID MemberId;
			AliasHandle = decoder.ReadContextHandle();
			MemberId = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref MemberId);
			var invokeTask = this._obj.SamrRemoveMemberFromAlias(AliasHandle, MemberId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrGetMembersInAlias(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle AliasHandle;
			RpcPointer<SAMPR_PSID_ARRAY_OUT> Members = new RpcPointer<SAMPR_PSID_ARRAY_OUT>();
			AliasHandle = decoder.ReadContextHandle();
			var invokeTask = this._obj.SamrGetMembersInAlias(AliasHandle, Members, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(Members.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Members.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrOpenUser(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			uint DesiredAccess;
			uint UserId;
			RpcPointer<RpcContextHandle> UserHandle = new RpcPointer<RpcContextHandle>();
			DomainHandle = decoder.ReadContextHandle();
			DesiredAccess = decoder.ReadUInt32();
			UserId = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrOpenUser(DomainHandle, DesiredAccess, UserId, UserHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(UserHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrDeleteUser(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> UserHandle;
			UserHandle = new RpcPointer<RpcContextHandle>();
			UserHandle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.SamrDeleteUser(UserHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(UserHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrQueryInformationUser(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle UserHandle;
			USER_INFORMATION_CLASS UserInformationClass;
			RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>>();
			UserHandle = decoder.ReadContextHandle();
			UserInformationClass = (USER_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.SamrQueryInformationUser(UserHandle, UserInformationClass, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(Buffer.value);
			if (Buffer.value is not null)
			{
				encoder.WriteUnion(Buffer.value.value);
				encoder.WriteStructDeferral(Buffer.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrSetInformationUser(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle UserHandle;
			USER_INFORMATION_CLASS UserInformationClass;
			SAMPR_USER_INFO_BUFFER Buffer;
			UserHandle = decoder.ReadContextHandle();
			UserInformationClass = (USER_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			Buffer = decoder.ReadUnion<SAMPR_USER_INFO_BUFFER>();
			decoder.ReadStructDeferral<SAMPR_USER_INFO_BUFFER>(ref Buffer);
			var invokeTask = this._obj.SamrSetInformationUser(UserHandle, UserInformationClass, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrChangePasswordUser(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle UserHandle;
			byte LmPresent;
			RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmEncryptedWithNewLm;
			RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewLmEncryptedWithOldLm;
			byte NtPresent;
			RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldNtEncryptedWithNewNt;
			RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewNtEncryptedWithOldNt;
			byte NtCrossEncryptionPresent;
			RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewNtEncryptedWithNewLm;
			byte LmCrossEncryptionPresent;
			RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewLmEncryptedWithNewNt;
			UserHandle = decoder.ReadContextHandle();
			LmPresent = decoder.ReadUnsignedChar();
			OldLmEncryptedWithNewLm = decoder.ReadUniquePointer<ENCRYPTED_LM_OWF_PASSWORD>();
			if (OldLmEncryptedWithNewLm is not null)
			{
				OldLmEncryptedWithNewLm.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref OldLmEncryptedWithNewLm.value);
			}

			NewLmEncryptedWithOldLm = decoder.ReadUniquePointer<ENCRYPTED_LM_OWF_PASSWORD>();
			if (NewLmEncryptedWithOldLm is not null)
			{
				NewLmEncryptedWithOldLm.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref NewLmEncryptedWithOldLm.value);
			}

			NtPresent = decoder.ReadUnsignedChar();
			OldNtEncryptedWithNewNt = decoder.ReadUniquePointer<ENCRYPTED_LM_OWF_PASSWORD>();
			if (OldNtEncryptedWithNewNt is not null)
			{
				OldNtEncryptedWithNewNt.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref OldNtEncryptedWithNewNt.value);
			}

			NewNtEncryptedWithOldNt = decoder.ReadUniquePointer<ENCRYPTED_LM_OWF_PASSWORD>();
			if (NewNtEncryptedWithOldNt is not null)
			{
				NewNtEncryptedWithOldNt.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref NewNtEncryptedWithOldNt.value);
			}

			NtCrossEncryptionPresent = decoder.ReadUnsignedChar();
			NewNtEncryptedWithNewLm = decoder.ReadUniquePointer<ENCRYPTED_LM_OWF_PASSWORD>();
			if (NewNtEncryptedWithNewLm is not null)
			{
				NewNtEncryptedWithNewLm.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref NewNtEncryptedWithNewLm.value);
			}

			LmCrossEncryptionPresent = decoder.ReadUnsignedChar();
			NewLmEncryptedWithNewNt = decoder.ReadUniquePointer<ENCRYPTED_LM_OWF_PASSWORD>();
			if (NewLmEncryptedWithNewNt is not null)
			{
				NewLmEncryptedWithNewNt.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref NewLmEncryptedWithNewNt.value);
			}

			var invokeTask = this._obj.SamrChangePasswordUser(UserHandle, LmPresent, OldLmEncryptedWithNewLm, NewLmEncryptedWithOldLm, NtPresent, OldNtEncryptedWithNewNt, NewNtEncryptedWithOldNt, NtCrossEncryptionPresent, NewNtEncryptedWithNewLm, LmCrossEncryptionPresent, NewLmEncryptedWithNewNt, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrGetGroupsForUser(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle UserHandle;
			RpcPointer<RpcPointer<SAMPR_GET_GROUPS_BUFFER>> Groups = new RpcPointer<RpcPointer<SAMPR_GET_GROUPS_BUFFER>>();
			UserHandle = decoder.ReadContextHandle();
			var invokeTask = this._obj.SamrGetGroupsForUser(UserHandle, Groups, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(Groups.value);
			if (Groups.value is not null)
			{
				encoder.WriteFixedStruct(Groups.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(Groups.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrQueryDisplayInformation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			DOMAIN_DISPLAY_INFORMATION DisplayInformationClass;
			uint Index;
			uint EntryCount;
			uint PreferredMaximumLength;
			RpcPointer<uint> TotalAvailable = new RpcPointer<uint>();
			RpcPointer<uint> TotalReturned = new RpcPointer<uint>();
			RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer = new RpcPointer<SAMPR_DISPLAY_INFO_BUFFER>();
			DomainHandle = decoder.ReadContextHandle();
			DisplayInformationClass = (DOMAIN_DISPLAY_INFORMATION)decoder.ReadEnumShortValue();
			Index = decoder.ReadUInt32();
			EntryCount = decoder.ReadUInt32();
			PreferredMaximumLength = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrQueryDisplayInformation(DomainHandle, DisplayInformationClass, Index, EntryCount, PreferredMaximumLength, TotalAvailable, TotalReturned, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(TotalAvailable.value);
			encoder.WriteValue(TotalReturned.value);
			encoder.WriteUnion(Buffer.value);
			encoder.WriteStructDeferral(Buffer.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrGetDisplayEnumerationIndex(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			DOMAIN_DISPLAY_INFORMATION DisplayInformationClass;
			ms_dtyp.RPC_UNICODE_STRING Prefix;
			RpcPointer<uint> Index = new RpcPointer<uint>();
			DomainHandle = decoder.ReadContextHandle();
			DisplayInformationClass = (DOMAIN_DISPLAY_INFORMATION)decoder.ReadEnumShortValue();
			Prefix = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Prefix);
			var invokeTask = this._obj.SamrGetDisplayEnumerationIndex(DomainHandle, DisplayInformationClass, Prefix, Index, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(Index.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum42NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum42NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum43NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum43NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrGetUserDomainPasswordInformation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle UserHandle;
			RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION> PasswordInformation = new RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION>();
			UserHandle = decoder.ReadContextHandle();
			var invokeTask = this._obj.SamrGetUserDomainPasswordInformation(UserHandle, PasswordInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(PasswordInformation.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(PasswordInformation.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrRemoveMemberFromForeignDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			ms_dtyp.RPC_SID MemberSid;
			DomainHandle = decoder.ReadContextHandle();
			MemberSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref MemberSid);
			var invokeTask = this._obj.SamrRemoveMemberFromForeignDomain(DomainHandle, MemberSid, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrQueryInformationDomain2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			DOMAIN_INFORMATION_CLASS DomainInformationClass;
			RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>>();
			DomainHandle = decoder.ReadContextHandle();
			DomainInformationClass = (DOMAIN_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.SamrQueryInformationDomain2(DomainHandle, DomainInformationClass, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(Buffer.value);
			if (Buffer.value is not null)
			{
				encoder.WriteUnion(Buffer.value.value);
				encoder.WriteStructDeferral(Buffer.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrQueryInformationUser2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle UserHandle;
			USER_INFORMATION_CLASS UserInformationClass;
			RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>>();
			UserHandle = decoder.ReadContextHandle();
			UserInformationClass = (USER_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.SamrQueryInformationUser2(UserHandle, UserInformationClass, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(Buffer.value);
			if (Buffer.value is not null)
			{
				encoder.WriteUnion(Buffer.value.value);
				encoder.WriteStructDeferral(Buffer.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrQueryDisplayInformation2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			DOMAIN_DISPLAY_INFORMATION DisplayInformationClass;
			uint Index;
			uint EntryCount;
			uint PreferredMaximumLength;
			RpcPointer<uint> TotalAvailable = new RpcPointer<uint>();
			RpcPointer<uint> TotalReturned = new RpcPointer<uint>();
			RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer = new RpcPointer<SAMPR_DISPLAY_INFO_BUFFER>();
			DomainHandle = decoder.ReadContextHandle();
			DisplayInformationClass = (DOMAIN_DISPLAY_INFORMATION)decoder.ReadEnumShortValue();
			Index = decoder.ReadUInt32();
			EntryCount = decoder.ReadUInt32();
			PreferredMaximumLength = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrQueryDisplayInformation2(DomainHandle, DisplayInformationClass, Index, EntryCount, PreferredMaximumLength, TotalAvailable, TotalReturned, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(TotalAvailable.value);
			encoder.WriteValue(TotalReturned.value);
			encoder.WriteUnion(Buffer.value);
			encoder.WriteStructDeferral(Buffer.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrGetDisplayEnumerationIndex2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			DOMAIN_DISPLAY_INFORMATION DisplayInformationClass;
			ms_dtyp.RPC_UNICODE_STRING Prefix;
			RpcPointer<uint> Index = new RpcPointer<uint>();
			DomainHandle = decoder.ReadContextHandle();
			DisplayInformationClass = (DOMAIN_DISPLAY_INFORMATION)decoder.ReadEnumShortValue();
			Prefix = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Prefix);
			var invokeTask = this._obj.SamrGetDisplayEnumerationIndex2(DomainHandle, DisplayInformationClass, Prefix, Index, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(Index.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrCreateUser2InDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			ms_dtyp.RPC_UNICODE_STRING Name;
			uint AccountType;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> UserHandle = new RpcPointer<RpcContextHandle>();
			RpcPointer<uint> GrantedAccess = new RpcPointer<uint>();
			RpcPointer<uint> RelativeId = new RpcPointer<uint>();
			DomainHandle = decoder.ReadContextHandle();
			Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name);
			AccountType = decoder.ReadUInt32();
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrCreateUser2InDomain(DomainHandle, Name, AccountType, DesiredAccess, UserHandle, GrantedAccess, RelativeId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(UserHandle.value);
			encoder.WriteValue(GrantedAccess.value);
			encoder.WriteValue(RelativeId.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrQueryDisplayInformation3(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle DomainHandle;
			DOMAIN_DISPLAY_INFORMATION DisplayInformationClass;
			uint Index;
			uint EntryCount;
			uint PreferredMaximumLength;
			RpcPointer<uint> TotalAvailable = new RpcPointer<uint>();
			RpcPointer<uint> TotalReturned = new RpcPointer<uint>();
			RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer = new RpcPointer<SAMPR_DISPLAY_INFO_BUFFER>();
			DomainHandle = decoder.ReadContextHandle();
			DisplayInformationClass = (DOMAIN_DISPLAY_INFORMATION)decoder.ReadEnumShortValue();
			Index = decoder.ReadUInt32();
			EntryCount = decoder.ReadUInt32();
			PreferredMaximumLength = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrQueryDisplayInformation3(DomainHandle, DisplayInformationClass, Index, EntryCount, PreferredMaximumLength, TotalAvailable, TotalReturned, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(TotalAvailable.value);
			encoder.WriteValue(TotalReturned.value);
			encoder.WriteUnion(Buffer.value);
			encoder.WriteStructDeferral(Buffer.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrAddMultipleMembersToAlias(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle AliasHandle;
			SAMPR_PSID_ARRAY MembersBuffer;
			AliasHandle = decoder.ReadContextHandle();
			MembersBuffer = decoder.ReadFixedStruct<SAMPR_PSID_ARRAY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SAMPR_PSID_ARRAY>(ref MembersBuffer);
			var invokeTask = this._obj.SamrAddMultipleMembersToAlias(AliasHandle, MembersBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrRemoveMultipleMembersFromAlias(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle AliasHandle;
			SAMPR_PSID_ARRAY MembersBuffer;
			AliasHandle = decoder.ReadContextHandle();
			MembersBuffer = decoder.ReadFixedStruct<SAMPR_PSID_ARRAY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SAMPR_PSID_ARRAY>(ref MembersBuffer);
			var invokeTask = this._obj.SamrRemoveMultipleMembersFromAlias(AliasHandle, MembersBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrOemChangePasswordUser2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RPC_STRING> ServerName;
			RPC_STRING UserName;
			RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldLm;
			RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmOwfPasswordEncryptedWithNewLm;
			ServerName = decoder.ReadUniquePointer<RPC_STRING>();
			if (ServerName is not null)
			{
				ServerName.value = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<RPC_STRING>(ref ServerName.value);
			}

			UserName = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<RPC_STRING>(ref UserName);
			NewPasswordEncryptedWithOldLm = decoder.ReadUniquePointer<SAMPR_ENCRYPTED_USER_PASSWORD>();
			if (NewPasswordEncryptedWithOldLm is not null)
			{
				NewPasswordEncryptedWithOldLm.value = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD>(ref NewPasswordEncryptedWithOldLm.value);
			}

			OldLmOwfPasswordEncryptedWithNewLm = decoder.ReadUniquePointer<ENCRYPTED_LM_OWF_PASSWORD>();
			if (OldLmOwfPasswordEncryptedWithNewLm is not null)
			{
				OldLmOwfPasswordEncryptedWithNewLm.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref OldLmOwfPasswordEncryptedWithNewLm.value);
			}

			var invokeTask = this._obj.SamrOemChangePasswordUser2(ServerName, UserName, NewPasswordEncryptedWithOldLm, OldLmOwfPasswordEncryptedWithNewLm, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrUnicodeChangePasswordUser2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> ServerName;
			ms_dtyp.RPC_UNICODE_STRING UserName;
			RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldNt;
			RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldNtOwfPasswordEncryptedWithNewNt;
			byte LmPresent;
			RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldLm;
			RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmOwfPasswordEncryptedWithNewNt;
			ServerName = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
			if (ServerName is not null)
			{
				ServerName.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref ServerName.value);
			}

			UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref UserName);
			NewPasswordEncryptedWithOldNt = decoder.ReadUniquePointer<SAMPR_ENCRYPTED_USER_PASSWORD>();
			if (NewPasswordEncryptedWithOldNt is not null)
			{
				NewPasswordEncryptedWithOldNt.value = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD>(ref NewPasswordEncryptedWithOldNt.value);
			}

			OldNtOwfPasswordEncryptedWithNewNt = decoder.ReadUniquePointer<ENCRYPTED_LM_OWF_PASSWORD>();
			if (OldNtOwfPasswordEncryptedWithNewNt is not null)
			{
				OldNtOwfPasswordEncryptedWithNewNt.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref OldNtOwfPasswordEncryptedWithNewNt.value);
			}

			LmPresent = decoder.ReadUnsignedChar();
			NewPasswordEncryptedWithOldLm = decoder.ReadUniquePointer<SAMPR_ENCRYPTED_USER_PASSWORD>();
			if (NewPasswordEncryptedWithOldLm is not null)
			{
				NewPasswordEncryptedWithOldLm.value = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD>(ref NewPasswordEncryptedWithOldLm.value);
			}

			OldLmOwfPasswordEncryptedWithNewNt = decoder.ReadUniquePointer<ENCRYPTED_LM_OWF_PASSWORD>();
			if (OldLmOwfPasswordEncryptedWithNewNt is not null)
			{
				OldLmOwfPasswordEncryptedWithNewNt.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref OldLmOwfPasswordEncryptedWithNewNt.value);
			}

			var invokeTask = this._obj.SamrUnicodeChangePasswordUser2(ServerName, UserName, NewPasswordEncryptedWithOldNt, OldNtOwfPasswordEncryptedWithNewNt, LmPresent, NewPasswordEncryptedWithOldLm, OldLmOwfPasswordEncryptedWithNewNt, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrGetDomainPasswordInformation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> Unused;
			RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION> PasswordInformation = new RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION>();
			Unused = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
			if (Unused is not null)
			{
				Unused.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Unused.value);
			}

			var invokeTask = this._obj.SamrGetDomainPasswordInformation(Unused, PasswordInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(PasswordInformation.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(PasswordInformation.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrConnect2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<RpcContextHandle> ServerHandle = new RpcPointer<RpcContextHandle>();
			uint DesiredAccess;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrConnect2(ServerName, ServerHandle, DesiredAccess, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(ServerHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrSetInformationUser2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle UserHandle;
			USER_INFORMATION_CLASS UserInformationClass;
			SAMPR_USER_INFO_BUFFER Buffer;
			UserHandle = decoder.ReadContextHandle();
			UserInformationClass = (USER_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			Buffer = decoder.ReadUnion<SAMPR_USER_INFO_BUFFER>();
			decoder.ReadStructDeferral<SAMPR_USER_INFO_BUFFER>(ref Buffer);
			var invokeTask = this._obj.SamrSetInformationUser2(UserHandle, UserInformationClass, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum59NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum59NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum60NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum60NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum61NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum61NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrConnect4(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<RpcContextHandle> ServerHandle = new RpcPointer<RpcContextHandle>();
			uint ClientRevision;
			uint DesiredAccess;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			ClientRevision = decoder.ReadUInt32();
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrConnect4(ServerName, ServerHandle, ClientRevision, DesiredAccess, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(ServerHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum63NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum63NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrConnect5(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint DesiredAccess;
			uint InVersion;
			SAMPR_REVISION_INFO InRevisionInfo;
			RpcPointer<uint> OutVersion = new RpcPointer<uint>();
			RpcPointer<SAMPR_REVISION_INFO> OutRevisionInfo = new RpcPointer<SAMPR_REVISION_INFO>();
			RpcPointer<RpcContextHandle> ServerHandle = new RpcPointer<RpcContextHandle>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			DesiredAccess = decoder.ReadUInt32();
			InVersion = decoder.ReadUInt32();
			InRevisionInfo = decoder.ReadUnion<SAMPR_REVISION_INFO>();
			decoder.ReadStructDeferral<SAMPR_REVISION_INFO>(ref InRevisionInfo);
			var invokeTask = this._obj.SamrConnect5(ServerName, DesiredAccess, InVersion, InRevisionInfo, OutVersion, OutRevisionInfo, ServerHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(OutVersion.value);
			encoder.WriteUnion(OutRevisionInfo.value);
			encoder.WriteStructDeferral(OutRevisionInfo.value);
			encoder.WriteContextHandle(ServerHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrRidToSid(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle ObjectHandle;
			uint Rid;
			RpcPointer<RpcPointer<ms_dtyp.RPC_SID>> Sid = new RpcPointer<RpcPointer<ms_dtyp.RPC_SID>>();
			ObjectHandle = decoder.ReadContextHandle();
			Rid = decoder.ReadUInt32();
			var invokeTask = this._obj.SamrRidToSid(ObjectHandle, Rid, Sid, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(Sid.value);
			if (Sid.value is not null)
			{
				encoder.WriteConformantStruct(Sid.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(Sid.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrSetDSRMPassword(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> Unused;
			uint UserId;
			RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> EncryptedNtOwfPassword;
			Unused = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
			if (Unused is not null)
			{
				Unused.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Unused.value);
			}

			UserId = decoder.ReadUInt32();
			EncryptedNtOwfPassword = decoder.ReadUniquePointer<ENCRYPTED_LM_OWF_PASSWORD>();
			if (EncryptedNtOwfPassword is not null)
			{
				EncryptedNtOwfPassword.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref EncryptedNtOwfPassword.value);
			}

			var invokeTask = this._obj.SamrSetDSRMPassword(Unused, UserId, EncryptedNtOwfPassword, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SamrValidatePassword(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			PASSWORD_POLICY_VALIDATION_TYPE ValidationType;
			SAM_VALIDATE_INPUT_ARG InputArg;
			RpcPointer<RpcPointer<SAM_VALIDATE_OUTPUT_ARG>> OutputArg = new RpcPointer<RpcPointer<SAM_VALIDATE_OUTPUT_ARG>>();
			ValidationType = (PASSWORD_POLICY_VALIDATION_TYPE)decoder.ReadEnumShortValue();
			InputArg = decoder.ReadUnion<SAM_VALIDATE_INPUT_ARG>();
			decoder.ReadStructDeferral<SAM_VALIDATE_INPUT_ARG>(ref InputArg);
			var invokeTask = this._obj.SamrValidatePassword(ValidationType, InputArg, OutputArg, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(OutputArg.value);
			if (OutputArg.value is not null)
			{
				encoder.WriteUnion(OutputArg.value.value);
				encoder.WriteStructDeferral(OutputArg.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum68NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum68NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum69NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum69NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		private static Guid _interfaceUuid = new Guid("12345778-1234-abcd-ef00-0123456789ac");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private samr _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public samrStub(samr obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] { this.Invoke_SamrConnect, this.Invoke_SamrCloseHandle, this.Invoke_SamrSetSecurityObject, this.Invoke_SamrQuerySecurityObject, this.Invoke_Opnum4NotUsedOnWire, this.Invoke_SamrLookupDomainInSamServer, this.Invoke_SamrEnumerateDomainsInSamServer, this.Invoke_SamrOpenDomain, this.Invoke_SamrQueryInformationDomain, this.Invoke_SamrSetInformationDomain, this.Invoke_SamrCreateGroupInDomain, this.Invoke_SamrEnumerateGroupsInDomain, this.Invoke_SamrCreateUserInDomain, this.Invoke_SamrEnumerateUsersInDomain, this.Invoke_SamrCreateAliasInDomain, this.Invoke_SamrEnumerateAliasesInDomain, this.Invoke_SamrGetAliasMembership, this.Invoke_SamrLookupNamesInDomain, this.Invoke_SamrLookupIdsInDomain, this.Invoke_SamrOpenGroup, this.Invoke_SamrQueryInformationGroup, this.Invoke_SamrSetInformationGroup, this.Invoke_SamrAddMemberToGroup, this.Invoke_SamrDeleteGroup, this.Invoke_SamrRemoveMemberFromGroup, this.Invoke_SamrGetMembersInGroup, this.Invoke_SamrSetMemberAttributesOfGroup, this.Invoke_SamrOpenAlias, this.Invoke_SamrQueryInformationAlias, this.Invoke_SamrSetInformationAlias, this.Invoke_SamrDeleteAlias, this.Invoke_SamrAddMemberToAlias, this.Invoke_SamrRemoveMemberFromAlias, this.Invoke_SamrGetMembersInAlias, this.Invoke_SamrOpenUser, this.Invoke_SamrDeleteUser, this.Invoke_SamrQueryInformationUser, this.Invoke_SamrSetInformationUser, this.Invoke_SamrChangePasswordUser, this.Invoke_SamrGetGroupsForUser, this.Invoke_SamrQueryDisplayInformation, this.Invoke_SamrGetDisplayEnumerationIndex, this.Invoke_Opnum42NotUsedOnWire, this.Invoke_Opnum43NotUsedOnWire, this.Invoke_SamrGetUserDomainPasswordInformation, this.Invoke_SamrRemoveMemberFromForeignDomain, this.Invoke_SamrQueryInformationDomain2, this.Invoke_SamrQueryInformationUser2, this.Invoke_SamrQueryDisplayInformation2, this.Invoke_SamrGetDisplayEnumerationIndex2, this.Invoke_SamrCreateUser2InDomain, this.Invoke_SamrQueryDisplayInformation3, this.Invoke_SamrAddMultipleMembersToAlias, this.Invoke_SamrRemoveMultipleMembersFromAlias, this.Invoke_SamrOemChangePasswordUser2, this.Invoke_SamrUnicodeChangePasswordUser2, this.Invoke_SamrGetDomainPasswordInformation, this.Invoke_SamrConnect2, this.Invoke_SamrSetInformationUser2, this.Invoke_Opnum59NotUsedOnWire, this.Invoke_Opnum60NotUsedOnWire, this.Invoke_Opnum61NotUsedOnWire, this.Invoke_SamrConnect4, this.Invoke_Opnum63NotUsedOnWire, this.Invoke_SamrConnect5, this.Invoke_SamrRidToSid, this.Invoke_SamrSetDSRMPassword, this.Invoke_SamrValidatePassword, this.Invoke_Opnum68NotUsedOnWire, this.Invoke_Opnum69NotUsedOnWire };
		}
	}
}