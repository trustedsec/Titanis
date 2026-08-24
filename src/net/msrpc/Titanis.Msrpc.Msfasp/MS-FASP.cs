namespace MS_FASP
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_STORE_TYPE : int
	{
		FW_STORE_TYPE_INVALID = 0,
		FW_STORE_TYPE_GP_RSOP = 1,
		FW_STORE_TYPE_LOCAL = 2,
		FW_STORE_TYPE_NOT_USED_VALUE_3 = 3,
		FW_STORE_TYPE_NOT_USED_VALUE_4 = 4,
		FW_STORE_TYPE_DYNAMIC = 5,
		FW_STORE_TYPE_GPO = 6,
		FW_STORE_TYPE_DEFAULTS = 7,
		FW_STORE_TYPE_NOT_USED_VALUE_8 = 8,
		FW_STORE_TYPE_NOT_USED_VALUE_9 = 9,
		FW_STORE_TYPE_NOT_USED_VALUE_10 = 10,
		FW_STORE_TYPE_NOT_USED_VALUE_11 = 11,
		FW_STORE_TYPE_NOT_USED_VALUE_12 = 12,
		FW_STORE_TYPE_MAX = 13
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_TRANSACTIONAL_STATE : int
	{
		FW_TRANSACTIONAL_STATE_NONE = 0,
		FW_TRANSACTIONAL_STATE_NO_FLUSH = 1,
		FW_TRANSACTIONAL_STATE_MAX = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_PROFILE_TYPE : uint
	{
		FW_PROFILE_TYPE_INVALID = 0U,
		FW_PROFILE_TYPE_DOMAIN = 1U,
		FW_PROFILE_TYPE_STANDARD = 2U,
		FW_PROFILE_TYPE_PRIVATE = 2U,
		FW_PROFILE_TYPE_PUBLIC = 4U,
		FW_PROFILE_TYPE_ALL = 2147483647U,
		FW_PROFILE_TYPE_CURRENT = 0x80000000,
		FW_PROFILE_TYPE_NONE = 0x80000001
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_POLICY_ACCESS_RIGHT : int
	{
		FW_POLICY_ACCESS_RIGHT_INVALID = 0,
		FW_POLICY_ACCESS_RIGHT_READ = 1,
		FW_POLICY_ACCESS_RIGHT_READ_WRITE = 2,
		FW_POLICY_ACCESS_RIGHT_MAX = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_POLICY_STORE_FLAGS : int
	{
		FW_POLICY_STORE_FLAGS_NONE = 0,
		FW_POLICY_STORE_FLAGS_DELETE_DYNAMIC_RULES_AFTER_CLOSE = 1,
		FW_POLICY_STORE_FLAGS_OPEN_GP_CACHE = 2,
		FW_POLICY_STORE_FLAGS_USE_GP_CACHE = 4,
		FW_POLICY_STORE_FLAGS_SAVE_GP_CACHE = 8,
		FW_POLICY_STORE_FLAGS_NOT_USED_VALUE_16 = 16,
		FW_POLICY_STORE_FLAGS_MAX = 32
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_RULE_DUPLICATE_STATUS_FLAGS : int
	{
		FW_DUPLICATE_STATUS_FLAGS_EVALUATING = 1,
		FW_DUPLICATE_STATUS_FLAGS_HAS_DUPLICATE = 2,
		FW_DUPLICATE_STATUS_FLAGS_IS_ENFORCED = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_IPV4_SUBNET : IRpcFixedStruct
	{
		public uint dwAddress;
		public uint dwSubNetMask;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwAddress);
			encoder.WriteValue(this.dwSubNetMask);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwAddress = decoder.ReadUInt32();
			this.dwSubNetMask = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_IPV4_SUBNET_LIST : IRpcFixedStruct
	{
		public uint dwNumEntries;
		public RpcPointer<FW_IPV4_SUBNET[]> pSubNets;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwNumEntries);
			encoder.WriteUniquePointer(this.pSubNets);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwNumEntries = decoder.ReadUInt32();
			this.pSubNets = decoder.ReadUniquePointer<FW_IPV4_SUBNET[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pSubNets is not null)
			{
				encoder.WriteArrayHeader(this.pSubNets.value);
				for (int i = 0; i < this.pSubNets.value.Length; i++)
				{
					FW_IPV4_SUBNET elem_0 = this.pSubNets.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.pSubNets.value.Length; i++)
				{
					FW_IPV4_SUBNET elem_0 = this.pSubNets.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pSubNets is not null)
			{
				this.pSubNets.value = decoder.ReadArrayHeader<FW_IPV4_SUBNET>();
				for (int i = 0; i < this.pSubNets.value.Length; i++)
				{
					FW_IPV4_SUBNET elem_0 = this.pSubNets.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_IPV4_SUBNET>(NdrAlignment._4Byte);
					this.pSubNets.value[i] = elem_0;
				}

				for (int i = 0; i < this.pSubNets.value.Length; i++)
				{
					FW_IPV4_SUBNET elem_0 = this.pSubNets.value[i];
					decoder.ReadStructDeferral<FW_IPV4_SUBNET>(ref elem_0);
					this.pSubNets.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_IPV6_SUBNET : IRpcFixedStruct
	{
		public byte[] Address;
		public uint dwNumPrefixBits;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			if (this.Address == null)
				this.Address = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.Address[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(this.dwNumPrefixBits);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			if (this.Address == null)
				this.Address = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.Address[i];
				elem_0 = decoder.ReadByte();
				this.Address[i] = elem_0;
			}

			this.dwNumPrefixBits = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_IPV6_SUBNET_LIST : IRpcFixedStruct
	{
		public uint dwNumEntries;
		public RpcPointer<FW_IPV6_SUBNET[]> pSubNets;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwNumEntries);
			encoder.WriteUniquePointer(this.pSubNets);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwNumEntries = decoder.ReadUInt32();
			this.pSubNets = decoder.ReadUniquePointer<FW_IPV6_SUBNET[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pSubNets is not null)
			{
				encoder.WriteArrayHeader(this.pSubNets.value);
				for (int i = 0; i < this.pSubNets.value.Length; i++)
				{
					FW_IPV6_SUBNET elem_0 = this.pSubNets.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.pSubNets.value.Length; i++)
				{
					FW_IPV6_SUBNET elem_0 = this.pSubNets.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pSubNets is not null)
			{
				this.pSubNets.value = decoder.ReadArrayHeader<FW_IPV6_SUBNET>();
				for (int i = 0; i < this.pSubNets.value.Length; i++)
				{
					FW_IPV6_SUBNET elem_0 = this.pSubNets.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_IPV6_SUBNET>(NdrAlignment._4Byte);
					this.pSubNets.value[i] = elem_0;
				}

				for (int i = 0; i < this.pSubNets.value.Length; i++)
				{
					FW_IPV6_SUBNET elem_0 = this.pSubNets.value[i];
					decoder.ReadStructDeferral<FW_IPV6_SUBNET>(ref elem_0);
					this.pSubNets.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_IPV4_ADDRESS_RANGE : IRpcFixedStruct
	{
		public uint dwBegin;
		public uint dwEnd;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwBegin);
			encoder.WriteValue(this.dwEnd);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwBegin = decoder.ReadUInt32();
			this.dwEnd = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_IPV6_ADDRESS_RANGE : IRpcFixedStruct
	{
		public byte[] Begin;
		public byte[] End;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			if (this.Begin == null)
				this.Begin = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.Begin[i];
				encoder.WriteValue(elem_0);
			}

			if (this.End == null)
				this.End = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.End[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			if (this.Begin == null)
				this.Begin = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.Begin[i];
				elem_0 = decoder.ReadByte();
				this.Begin[i] = elem_0;
			}

			if (this.End == null)
				this.End = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.End[i];
				elem_0 = decoder.ReadByte();
				this.End[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_IPV4_RANGE_LIST : IRpcFixedStruct
	{
		public uint dwNumEntries;
		public RpcPointer<FW_IPV4_ADDRESS_RANGE[]> pRanges;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwNumEntries);
			encoder.WriteUniquePointer(this.pRanges);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwNumEntries = decoder.ReadUInt32();
			this.pRanges = decoder.ReadUniquePointer<FW_IPV4_ADDRESS_RANGE[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pRanges is not null)
			{
				encoder.WriteArrayHeader(this.pRanges.value);
				for (int i = 0; i < this.pRanges.value.Length; i++)
				{
					FW_IPV4_ADDRESS_RANGE elem_0 = this.pRanges.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.pRanges.value.Length; i++)
				{
					FW_IPV4_ADDRESS_RANGE elem_0 = this.pRanges.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pRanges is not null)
			{
				this.pRanges.value = decoder.ReadArrayHeader<FW_IPV4_ADDRESS_RANGE>();
				for (int i = 0; i < this.pRanges.value.Length; i++)
				{
					FW_IPV4_ADDRESS_RANGE elem_0 = this.pRanges.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_IPV4_ADDRESS_RANGE>(NdrAlignment._4Byte);
					this.pRanges.value[i] = elem_0;
				}

				for (int i = 0; i < this.pRanges.value.Length; i++)
				{
					FW_IPV4_ADDRESS_RANGE elem_0 = this.pRanges.value[i];
					decoder.ReadStructDeferral<FW_IPV4_ADDRESS_RANGE>(ref elem_0);
					this.pRanges.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_IPV6_RANGE_LIST : IRpcFixedStruct
	{
		public uint dwNumEntries;
		public RpcPointer<FW_IPV6_ADDRESS_RANGE[]> pRanges;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwNumEntries);
			encoder.WriteUniquePointer(this.pRanges);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwNumEntries = decoder.ReadUInt32();
			this.pRanges = decoder.ReadUniquePointer<FW_IPV6_ADDRESS_RANGE[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pRanges is not null)
			{
				encoder.WriteArrayHeader(this.pRanges.value);
				for (int i = 0; i < this.pRanges.value.Length; i++)
				{
					FW_IPV6_ADDRESS_RANGE elem_0 = this.pRanges.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._1Byte);
				}

				for (int i = 0; i < this.pRanges.value.Length; i++)
				{
					FW_IPV6_ADDRESS_RANGE elem_0 = this.pRanges.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pRanges is not null)
			{
				this.pRanges.value = decoder.ReadArrayHeader<FW_IPV6_ADDRESS_RANGE>();
				for (int i = 0; i < this.pRanges.value.Length; i++)
				{
					FW_IPV6_ADDRESS_RANGE elem_0 = this.pRanges.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_IPV6_ADDRESS_RANGE>(NdrAlignment._1Byte);
					this.pRanges.value[i] = elem_0;
				}

				for (int i = 0; i < this.pRanges.value.Length; i++)
				{
					FW_IPV6_ADDRESS_RANGE elem_0 = this.pRanges.value[i];
					decoder.ReadStructDeferral<FW_IPV6_ADDRESS_RANGE>(ref elem_0);
					this.pRanges.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_PORT_RANGE : IRpcFixedStruct
	{
		public ushort wBegin;
		public ushort wEnd;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wBegin);
			encoder.WriteValue(this.wEnd);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wBegin = decoder.ReadUInt16();
			this.wEnd = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_PORT_RANGE_LIST : IRpcFixedStruct
	{
		public uint dwNumEntries;
		public RpcPointer<FW_PORT_RANGE[]> pPorts;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwNumEntries);
			encoder.WriteUniquePointer(this.pPorts);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwNumEntries = decoder.ReadUInt32();
			this.pPorts = decoder.ReadUniquePointer<FW_PORT_RANGE[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pPorts is not null)
			{
				encoder.WriteArrayHeader(this.pPorts.value);
				for (int i = 0; i < this.pPorts.value.Length; i++)
				{
					FW_PORT_RANGE elem_0 = this.pPorts.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._2Byte);
				}

				for (int i = 0; i < this.pPorts.value.Length; i++)
				{
					FW_PORT_RANGE elem_0 = this.pPorts.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pPorts is not null)
			{
				this.pPorts.value = decoder.ReadArrayHeader<FW_PORT_RANGE>();
				for (int i = 0; i < this.pPorts.value.Length; i++)
				{
					FW_PORT_RANGE elem_0 = this.pPorts.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_PORT_RANGE>(NdrAlignment._2Byte);
					this.pPorts.value[i] = elem_0;
				}

				for (int i = 0; i < this.pPorts.value.Length; i++)
				{
					FW_PORT_RANGE elem_0 = this.pPorts.value[i];
					decoder.ReadStructDeferral<FW_PORT_RANGE>(ref elem_0);
					this.pPorts.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_PORT_KEYWORD : int
	{
		FW_PORT_KEYWORD_NONE = 0,
		FW_PORT_KEYWORD_DYNAMIC_RPC_PORTS = 1,
		FW_PORT_KEYWORD_RPC_EP = 2,
		FW_PORT_KEYWORD_TEREDO_PORT = 4,
		FW_PORT_KEYWORD_IP_TLS_IN = 8,
		FW_PORT_KEYWORD_IP_TLS_OUT = 16,
		FW_PORT_KEYWORD_DHCP = 32,
		FW_PORT_KEYWORD_PLAYTO_DISCOVERY = 64,
		FW_PORT_KEYWORD_MDNS = 128,
		FW_PORT_KEYWORD_CORTANA_OUT = 256,
		FW_PORT_KEYWORD_PROXIMAL_TCP_CDP = 512,
		FW_PORT_KEYWORD_MAX = 1024,
		FW_PORT_KEYWORD_MAX_V2_1 = 8,
		FW_PORT_KEYWORD_MAX_V2_10 = 32,
		FW_PORT_KEYWORD_MAX_V2_20 = 128,
		FW_PORT_KEYWORD_MAX_V2_24 = 256,
		FW_PORT_KEYWORD_MAX_V2_25 = 512
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_PORTS : IRpcFixedStruct
	{
		public ushort wPortKeywords;
		public FW_PORT_RANGE_LIST Ports;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wPortKeywords);
			encoder.WriteFixedStruct(this.Ports, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wPortKeywords = decoder.ReadUInt16();
			this.Ports = decoder.ReadFixedStruct<FW_PORT_RANGE_LIST>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Ports);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_PORT_RANGE_LIST>(ref this.Ports);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_ICMP_TYPE_CODE : IRpcFixedStruct
	{
		public byte bType;
		public ushort wCode;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.bType);
			encoder.WriteValue(this.wCode);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.bType = decoder.ReadByte();
			this.wCode = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_ICMP_TYPE_CODE_LIST : IRpcFixedStruct
	{
		public uint dwNumEntries;
		public RpcPointer<FW_ICMP_TYPE_CODE[]> pEntries;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwNumEntries);
			encoder.WriteUniquePointer(this.pEntries);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwNumEntries = decoder.ReadUInt32();
			this.pEntries = decoder.ReadUniquePointer<FW_ICMP_TYPE_CODE[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pEntries is not null)
			{
				encoder.WriteArrayHeader(this.pEntries.value);
				for (int i = 0; i < this.pEntries.value.Length; i++)
				{
					FW_ICMP_TYPE_CODE elem_0 = this.pEntries.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._2Byte);
				}

				for (int i = 0; i < this.pEntries.value.Length; i++)
				{
					FW_ICMP_TYPE_CODE elem_0 = this.pEntries.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pEntries is not null)
			{
				this.pEntries.value = decoder.ReadArrayHeader<FW_ICMP_TYPE_CODE>();
				for (int i = 0; i < this.pEntries.value.Length; i++)
				{
					FW_ICMP_TYPE_CODE elem_0 = this.pEntries.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE>(NdrAlignment._2Byte);
					this.pEntries.value[i] = elem_0;
				}

				for (int i = 0; i < this.pEntries.value.Length; i++)
				{
					FW_ICMP_TYPE_CODE elem_0 = this.pEntries.value[i];
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE>(ref elem_0);
					this.pEntries.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_INTERFACE_LUIDS : IRpcFixedStruct
	{
		public uint dwNumLUIDs;
		public RpcPointer<Guid[]> pLUIDs;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwNumLUIDs);
			encoder.WriteUniquePointer(this.pLUIDs);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwNumLUIDs = decoder.ReadUInt32();
			this.pLUIDs = decoder.ReadUniquePointer<Guid[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pLUIDs is not null)
			{
				encoder.WriteArrayHeader(this.pLUIDs.value);
				for (int i = 0; i < this.pLUIDs.value.Length; i++)
				{
					Guid elem_0 = this.pLUIDs.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pLUIDs is not null)
			{
				this.pLUIDs.value = decoder.ReadArrayHeader<Guid>();
				for (int i = 0; i < this.pLUIDs.value.Length; i++)
				{
					Guid elem_0 = this.pLUIDs.value[i];
					elem_0 = decoder.ReadUuid();
					this.pLUIDs.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_DIRECTION : int
	{
		FW_DIR_INVALID = 0,
		FW_DIR_IN = 1,
		FW_DIR_OUT = 2,
		FW_DIR_MAX = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_INTERFACE_TYPE : int
	{
		FW_INTERFACE_TYPE_ALL = 0,
		FW_INTERFACE_TYPE_LAN = 1,
		FW_INTERFACE_TYPE_WIRELESS = 2,
		FW_INTERFACE_TYPE_REMOTE_ACCESS = 4,
		FW_INTERFACE_TYPE_MOBILE_BBAND = 8,
		FW_INTERFACE_TYPE_MAX = 16,
		FW_INTERFACE_TYPE_MAX_V2_23 = 8
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_ADDRESS_KEYWORD : int
	{
		FW_ADDRESS_KEYWORD_NONE = 0,
		FW_ADDRESS_KEYWORD_LOCAL_SUBNET = 1,
		FW_ADDRESS_KEYWORD_DNS = 2,
		FW_ADDRESS_KEYWORD_DHCP = 4,
		FW_ADDRESS_KEYWORD_WINS = 8,
		FW_ADDRESS_KEYWORD_DEFAULT_GATEWAY = 16,
		FW_ADDRESS_KEYWORD_INTRANET = 32,
		FW_ADDRESS_KEYWORD_INTERNET = 64,
		FW_ADDRESS_KEYWORD_PLAYTO_RENDERERS = 128,
		FW_ADDRESS_KEYWORD_REMOTE_INTRANET = 256,
		FW_ADDRESS_KEYWORD_CAPTIVE_PORTAL = 512,
		FW_ADDRESS_KEYWORD_INTERNAL_LOCAL_ADDRESSES = 1024,
		FW_ADDRESS_KEYWORD_MAX_V2_10 = 32,
		FW_ADDRESS_KEYWORD_MAX_V2_29 = 512,
		FW_ADDRESS_KEYWORD_MAX_V2_33 = 1024,
		FW_ADDRESS_KEYWORD_MAX = 2048
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_ADDRESSES : IRpcFixedStruct
	{
		public uint dwV4AddressKeywords;
		public uint dwV6AddressKeywords;
		public FW_IPV4_SUBNET_LIST V4SubNets;
		public FW_IPV4_RANGE_LIST V4Ranges;
		public FW_IPV6_SUBNET_LIST V6SubNets;
		public FW_IPV6_RANGE_LIST V6Ranges;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwV4AddressKeywords);
			encoder.WriteValue(this.dwV6AddressKeywords);
			encoder.WriteFixedStruct(this.V4SubNets, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.V4Ranges, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.V6SubNets, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.V6Ranges, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwV4AddressKeywords = decoder.ReadUInt32();
			this.dwV6AddressKeywords = decoder.ReadUInt32();
			this.V4SubNets = decoder.ReadFixedStruct<FW_IPV4_SUBNET_LIST>(NdrAlignment.NativePtr);
			this.V4Ranges = decoder.ReadFixedStruct<FW_IPV4_RANGE_LIST>(NdrAlignment.NativePtr);
			this.V6SubNets = decoder.ReadFixedStruct<FW_IPV6_SUBNET_LIST>(NdrAlignment.NativePtr);
			this.V6Ranges = decoder.ReadFixedStruct<FW_IPV6_RANGE_LIST>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.V4SubNets);
			encoder.WriteStructDeferral(this.V4Ranges);
			encoder.WriteStructDeferral(this.V6SubNets);
			encoder.WriteStructDeferral(this.V6Ranges);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_IPV4_SUBNET_LIST>(ref this.V4SubNets);
			decoder.ReadStructDeferral<FW_IPV4_RANGE_LIST>(ref this.V4Ranges);
			decoder.ReadStructDeferral<FW_IPV6_SUBNET_LIST>(ref this.V6SubNets);
			decoder.ReadStructDeferral<FW_IPV6_RANGE_LIST>(ref this.V6Ranges);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_DYNAMIC_KEYWORD_ADDRESS_ID_LIST : IRpcFixedStruct
	{
		public uint dwNumIds;
		public RpcPointer<Guid[]> ids;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwNumIds);
			encoder.WriteUniquePointer(this.ids);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwNumIds = decoder.ReadUInt32();
			this.ids = decoder.ReadUniquePointer<Guid[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ids is not null)
			{
				encoder.WriteArrayHeader(this.ids.value);
				for (int i = 0; i < this.ids.value.Length; i++)
				{
					Guid elem_0 = this.ids.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ids is not null)
			{
				this.ids.value = decoder.ReadArrayHeader<Guid>();
				for (int i = 0; i < this.ids.value.Length; i++)
				{
					Guid elem_0 = this.ids.value[i];
					elem_0 = decoder.ReadUuid();
					this.ids.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_DYNAMIC_KEYWORD_ADDRESS_FLAGS : int
	{
		FW_DYNAMIC_KEYWORD_ADDRESS_FLAGS_NONE = 0,
		FW_DYNAMIC_KEYWORD_ADDRESS_FLAGS_AUTO_RESOLVE = 1,
		FW_DYNAMIC_KEYWORD_ADDRESS_FLAGS_MAX = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_DYNAMIC_KEYWORD_ORIGIN_TYPE : int
	{
		FW_DYNAMIC_KEYWORD_ORIGIN_INVALID = 0,
		FW_DYNAMIC_KEYWORD_ORIGIN_LOCAL = 1,
		FW_DYNAMIC_KEYWORD_ORIGIN_MDM = 2,
		FW_DYNAMIC_KEYWORD_ORIGIN_MAX = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_DYNAMIC_KEYWORD_ADDRESS0 : IRpcFixedStruct
	{
		public Guid id;
		public RpcPointer<string> keyword;
		public uint flags;
		public RpcPointer<string> addresses;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.id);
			encoder.WriteUniquePointer(this.keyword);
			encoder.WriteValue(this.flags);
			encoder.WriteUniquePointer(this.addresses);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.id = decoder.ReadUuid();
			this.keyword = decoder.ReadUniquePointer<string>();
			this.flags = decoder.ReadUInt32();
			this.addresses = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.keyword is not null)
			{
				encoder.WriteWideCharString(this.keyword.value);
			}

			if (this.addresses is not null)
			{
				encoder.WriteWideCharString(this.addresses.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.keyword is not null)
			{
				this.keyword.value = decoder.ReadWideCharString();
			}

			if (this.addresses is not null)
			{
				this.addresses.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_DYNAMIC_KEYWORD_ADDRESS_DATA0 : IRpcFixedStruct
	{
		public FW_DYNAMIC_KEYWORD_ADDRESS0 dynamicKeywordAddress;
		public RpcPointer<FW_DYNAMIC_KEYWORD_ADDRESS_DATA0> next;
		public ushort schemaVersion;
		public FW_DYNAMIC_KEYWORD_ORIGIN_TYPE originType;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.dynamicKeywordAddress, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.next);
			encoder.WriteValue(this.schemaVersion);
			encoder.WriteEnumShortValue((short)this.originType);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dynamicKeywordAddress = decoder.ReadFixedStruct<FW_DYNAMIC_KEYWORD_ADDRESS0>(NdrAlignment.NativePtr);
			this.next = decoder.ReadUniquePointer<FW_DYNAMIC_KEYWORD_ADDRESS_DATA0>();
			this.schemaVersion = decoder.ReadUInt16();
			this.originType = (FW_DYNAMIC_KEYWORD_ORIGIN_TYPE)decoder.ReadEnumShortValue();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.dynamicKeywordAddress);
			if (this.next is not null)
			{
				encoder.WriteFixedStruct(this.next.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.next.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_DYNAMIC_KEYWORD_ADDRESS0>(ref this.dynamicKeywordAddress);
			if (this.next is not null)
			{
				this.next.value = decoder.ReadFixedStruct<FW_DYNAMIC_KEYWORD_ADDRESS_DATA0>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_DYNAMIC_KEYWORD_ADDRESS_DATA0>(ref this.next.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_DYNAMIC_KEYWORD_ADDRESS_ENUM_FLAGS : int
	{
		FW_DYNAMIC_KEYWORD_ADDRESS_ENUM_FLAGS_NONE = 0,
		FW_DYNAMIC_KEYWORD_ADDRESS_ENUM_FLAGS_AUTO_RESOLVE = 1,
		FW_DYNAMIC_KEYWORD_ADDRESS_ENUM_FLAGS_NON_AUTO_RESOLVE = 2,
		FW_DYNAMIC_KEYWORD_ADDRESS_ENUM_FLAGS_ALL = 3,
		FW_DYNAMIC_KEYWORD_ADDRESS_ENUM_FLAGS_MAX = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_DYNAMIC_KEYWORD_ADDRESS_INTERNAL : IRpcFixedStruct
	{
		public RpcPointer<FW_DYNAMIC_KEYWORD_ADDRESS_INTERNAL> next;
		public ushort schemaVersion;
		public Guid id;
		public RpcPointer<string> keyword;
		public uint flags;
		public FW_ADDRESSES addresses;
		public FW_DYNAMIC_KEYWORD_ORIGIN_TYPE originType;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.next);
			encoder.WriteValue(this.schemaVersion);
			encoder.WriteValue(this.id);
			encoder.WriteUniquePointer(this.keyword);
			encoder.WriteValue(this.flags);
			encoder.WriteFixedStruct(this.addresses, NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.originType);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.next = decoder.ReadUniquePointer<FW_DYNAMIC_KEYWORD_ADDRESS_INTERNAL>();
			this.schemaVersion = decoder.ReadUInt16();
			this.id = decoder.ReadUuid();
			this.keyword = decoder.ReadUniquePointer<string>();
			this.flags = decoder.ReadUInt32();
			this.addresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.originType = (FW_DYNAMIC_KEYWORD_ORIGIN_TYPE)decoder.ReadEnumShortValue();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.next is not null)
			{
				encoder.WriteFixedStruct(this.next.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.next.value);
			}

			if (this.keyword is not null)
			{
				encoder.WriteWideCharString(this.keyword.value);
			}

			encoder.WriteStructDeferral(this.addresses);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.next is not null)
			{
				this.next.value = decoder.ReadFixedStruct<FW_DYNAMIC_KEYWORD_ADDRESS_INTERNAL>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_DYNAMIC_KEYWORD_ADDRESS_INTERNAL>(ref this.next.value);
			}

			if (this.keyword is not null)
			{
				this.keyword.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.addresses);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_TRUST_TUPLE_KEYWORD : int
	{
		FW_TRUST_TUPLE_KEYWORD_NONE = 0,
		FW_TRUST_TUPLE_KEYWORD_PROXIMITY = 1,
		FW_TRUST_TUPLE_KEYWORD_PROXIMITY_SHARING = 2,
		FW_TRUST_TUPLE_KEYWORD_WFD_PRINT = 4,
		FW_TRUST_TUPLE_KEYWORD_WFD_DISPLAY = 8,
		FW_TRUST_TUPLE_KEYWORD_WFD_DEVICES = 16,
		FW_TRUST_TUPLE_KEYWORD_WFD_KM_DRIVER = 32,
		FW_TRUST_TUPLE_KEYWORD_UPNP = 64,
		FW_TRUST_TUPLE_KEYWORD_WFD_CDP = 128,
		FW_TRUST_TUPLE_KEYWORD_MAX = 256,
		FW_TRUST_TUPLE_KEYWORD_MAX_V2_20 = 4,
		FW_TRUST_TUPLE_KEYWORD_MAX_V2_26 = 32,
		FW_TRUST_TUPLE_KEYWORD_MAX_V2_27 = 128
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_RULE_STATUS : uint
	{
		FW_RULE_STATUS_OK = 65536U,
		FW_RULE_STATUS_PARTIALLY_IGNORED = 131072U,
		FW_RULE_STATUS_IGNORED = 262144U,
		FW_RULE_STATUS_PARSING_ERROR = 524288U,
		FW_RULE_STATUS_PARSING_ERROR_NAME = 524289U,
		FW_RULE_STATUS_PARSING_ERROR_DESC = 524290U,
		FW_RULE_STATUS_PARSING_ERROR_APP = 524291U,
		FW_RULE_STATUS_PARSING_ERROR_SVC = 524292U,
		FW_RULE_STATUS_PARSING_ERROR_RMA = 524293U,
		FW_RULE_STATUS_PARSING_ERROR_RUA = 524294U,
		FW_RULE_STATUS_PARSING_ERROR_EMBD = 524295U,
		FW_RULE_STATUS_PARSING_ERROR_RULE_ID = 524296U,
		FW_RULE_STATUS_PARSING_ERROR_PHASE1_AUTH = 524297U,
		FW_RULE_STATUS_PARSING_ERROR_PHASE2_CRYPTO = 524298U,
		FW_RULE_STATUS_PARSING_ERROR_PHASE2_AUTH = 524299U,
		FW_RULE_STATUS_PARSING_ERROR_RESOLVE_APP = 524300U,
		FW_RULE_STATUS_PARSING_ERROR_MAINMODE_ID = 524301U,
		FW_RULE_STATUS_PARSING_ERROR_PHASE1_CRYPTO = 524302U,
		FW_RULE_STATUS_PARSING_ERROR_REMOTE_ENDPOINTS = 524303U,
		FW_RULE_STATUS_PARSING_ERROR_REMOTE_ENDPOINT_FQDN = 524304U,
		FW_RULE_STATUS_PARSING_ERROR_KEY_MODULE = 524305U,
		FW_RULE_STATUS_PARSING_ERROR_LUA = 524306U,
		FW_RULE_STATUS_PARSING_ERROR_FWD_LIFETIME = 524307U,
		FW_RULE_STATUS_PARSING_ERROR_TRANSPORT_MACHINE_AUTHZ_SDDL = 524308U,
		FW_RULE_STATUS_PARSING_ERROR_TRANSPORT_USER_AUTHZ_SDDL = 524309U,
		FW_RULE_STATUS_PARSING_ERROR_NETNAMES_STRING = 524310U,
		FW_RULE_STATUS_PARSING_ERROR_SECURITY_REALM_ID_STRING = 524311U,
		FW_RULE_STATUS_PARSING_ERROR_FQBN_STRING = 524312U,
		FW_RULE_STATUS_SEMANTIC_ERROR = 1048576U,
		FW_RULE_STATUS_SEMANTIC_ERROR_RULE_ID = 1048592U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PORTS = 1048608U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PORT_KEYW = 1048609U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PORT_RANGE = 1048610U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PORTRANGE_RESTRICTION = 1048611U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ADDR_V4_SUBNETS = 1048640U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ADDR_V6_SUBNETS = 1048641U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ADDR_V4_RANGES = 1048642U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ADDR_V6_RANGES = 1048643U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ADDR_RANGE = 1048644U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ADDR_MASK = 1048645U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ADDR_PREFIX = 1048646U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ADDR_KEYW = 1048647U,
		FW_RULE_STATUS_SEMANTIC_ERROR_LADDR_PROP = 1048648U,
		FW_RULE_STATUS_SEMANTIC_ERROR_RADDR_PROP = 1048649U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ADDR_V6 = 1048650U,
		FW_RULE_STATUS_SEMANTIC_ERROR_LADDR_INTF = 1048651U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ADDR_V4 = 1048652U,
		FW_RULE_STATUS_SEMANTIC_ERROR_TUNNEL_ENDPOINT_ADDR = 1048653U,
		FW_RULE_STATUS_SEMANTIC_ERROR_DTE_VER = 1048654U,
		FW_RULE_STATUS_SEMANTIC_ERROR_DTE_MISMATCH_ADDR = 1048655U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PROFILE = 1048656U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ICMP = 1048672U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ICMP_CODE = 1048673U,
		FW_RULE_STATUS_SEMANTIC_ERROR_IF_ID = 1048688U,
		FW_RULE_STATUS_SEMANTIC_ERROR_IF_TYPE = 1048689U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ACTION = 1048704U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ALLOW_BYPASS = 1048705U,
		FW_RULE_STATUS_SEMANTIC_ERROR_DO_NOT_SECURE = 1048706U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ACTION_BLOCK_IS_ENCRYPTED_SECURE = 1048707U,
		FW_RULE_STATUS_SEMANTIC_ERROR_INCOMPATIBLE_FLAG_OR_ACTION_WITH_SECURITY_REALM = 1048708U,
		FW_RULE_STATUS_SEMANTIC_ERROR_DIR = 1048720U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PROT = 1048736U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PROT_PROP = 1048737U,
		FW_RULE_STATUS_SEMANTIC_ERROR_DEFER_EDGE_PROP = 1048738U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ALLOW_BYPASS_OUTBOUND = 1048739U,
		FW_RULE_STATUS_SEMANTIC_ERROR_DEFER_USER_INVALID_RULE = 1048740U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS = 1048752U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_AUTO_AUTH = 1048753U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_AUTO_BLOCK = 1048754U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_AUTO_DYN_RPC = 1048755U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_AUTHENTICATE_ENCRYPT = 1048756U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_AUTH_WITH_ENC_NEGOTIATE_VER = 1048757U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_AUTH_WITH_ENC_NEGOTIATE = 1048758U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_ESP_NO_ENCAP_VER = 1048759U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_ESP_NO_ENCAP = 1048760U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_TUNNEL_AUTH_MODES_VER = 1048761U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_TUNNEL_AUTH_MODES = 1048762U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_IP_HTTPS_VER = 1048763U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_IP_TLS_VER = 1048763U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PORTRANGE_VER = 1048764U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_ADDRS_TRAVERSE_DEFER_VER = 1048765U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_AUTH_WITH_ENC_NEGOTIATE_OUTBOUND = 1048766U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_AUTHENTICATE_WITH_OUTBOUND_BYPASS_VER = 1048767U,
		FW_RULE_STATUS_SEMANTIC_ERROR_REMOTE_AUTH_LIST = 1048768U,
		FW_RULE_STATUS_SEMANTIC_ERROR_REMOTE_USER_LIST = 1048769U,
		FW_RULE_STATUS_SEMANTIC_ERROR_LOCAL_USER_LIST = 1048770U,
		FW_RULE_STATUS_SEMANTIC_ERROR_LUA_VER = 1048771U,
		FW_RULE_STATUS_SEMANTIC_ERROR_LOCAL_USER_OWNER = 1048772U,
		FW_RULE_STATUS_SEMANTIC_ERROR_LOCAL_USER_OWNER_VER = 1048773U,
		FW_RULE_STATUS_SEMANTIC_ERROR_LUA_CONDITIONAL_VER = 1048774U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_SYSTEMOS_GAMEOS = 1048775U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_CORTANA_VER = 1048776U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_REMOTENAME = 1048777U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_ALLOW_PROFILE_CROSSING_VER = 1048784U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_LOCAL_ONLY_MAPPED_VER = 1048785U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PLATFORM = 1048800U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PLATFORM_OP_VER = 1048801U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PLATFORM_OP = 1048802U,
		FW_RULE_STATUS_SEMANTIC_ERROR_DTE_NOANY_ADDR = 1048816U,
		FW_RULE_STATUS_SEMANTIC_ERROR_TUNNEL_EXEMPT_WITH_GATEWAY = 1048817U,
		FW_RULE_STATUS_SEMANTIC_ERROR_TUNNEL_EXEMPT_VER = 1048818U,
		FW_RULE_STATUS_SEMANTIC_ERROR_ADDR_KEYWORD_VER = 1048819U,
		FW_RULE_STATUS_SEMANTIC_ERROR_KEY_MODULE_VER = 1048820U,
		FW_RULE_STATUS_SEMANTIC_ERROR_APP_CONTAINER_PACKAGE_ID = 1048832U,
		FW_RULE_STATUS_SEMANTIC_ERROR_APP_CONTAINER_PACKAGE_ID_VER = 1048833U,
		FW_RULE_STATUS_SEMANTIC_ERROR_TRUST_TUPLE_KEYWORD_INCOMPATIBLE = 1049088U,
		FW_RULE_STATUS_SEMANTIC_ERROR_TRUST_TUPLE_KEYWORD_INVALID = 1049089U,
		FW_RULE_STATUS_SEMANTIC_ERROR_TRUST_TUPLE_KEYWORD_VER = 1049090U,
		FW_RULE_STATUS_SEMANTIC_ERROR_INTERFACE_TYPES_VER = 1049345U,
		FW_RULE_STATUS_SEMANTIC_ERROR_NETNAMES_VER = 1049601U,
		FW_RULE_STATUS_SEMANTIC_ERROR_SECURITY_REALM_ID_VER = 1049602U,
		FW_RULE_STATUS_SEMANTIC_ERROR_SYSTEMOS_GAMEOS_VER = 1049603U,
		FW_RULE_STATUS_SEMANTIC_ERROR_DEVMODE_VER = 1049604U,
		FW_RULE_STATUS_SEMANTIC_ERROR_REMOTE_SERVERNAME_VER = 1049605U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FQBN_VER = 1049606U,
		FW_RULE_STATUS_SEMANTIC_ERROR_COMPARTMENT_ID_VER = 1049607U,
		FW_RULE_STATUS_SEMANTIC_ERROR_CALLOUT_AND_AUDIT_VER = 1049608U,
		FW_RULE_STATUS_SEMANTIC_ERROR_APPCONTAINER_LOOPBACK_VER = 1049609U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_AUTH_SET_ID = 1049856U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE2_CRYPTO_SET_ID = 1049872U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_CRYPTO_SET_ID = 1049873U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_KEY_MANAGER_DICTATE_VER = 1049874U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_KEY_MANAGER_NOTIFY_VER = 1049875U,
		FW_RULE_STATUS_SEMANTIC_ERROR_TRANSPORT_MACHINE_AUTHZ_VER = 1049876U,
		FW_RULE_STATUS_SEMANTIC_ERROR_TRANSPORT_USER_AUTHZ_VER = 1049877U,
		FW_RULE_STATUS_SEMANTIC_ERROR_TRANSPORT_MACHINE_AUTHZ_ON_TUNNEL = 1049878U,
		FW_RULE_STATUS_SEMANTIC_ERROR_TRANSPORT_USER_AUTHZ_ON_TUNNEL = 1049879U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PER_RULE_AND_GLOBAL_AUTHZ = 1049880U,
		FW_RULE_STATUS_SEMANTIC_ERROR_FLAGS_SECURITY_REALM = 1049881U,
		FW_RULE_STATUS_SEMANTIC_ERROR_SET_ID = 1052672U,
		FW_RULE_STATUS_SEMANTIC_ERROR_IPSEC_PHASE = 1052688U,
		FW_RULE_STATUS_SEMANTIC_ERROR_EMPTY_SUITES = 1052704U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_AUTH_METHOD = 1052720U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE2_AUTH_METHOD = 1052721U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_METHOD_ANONYMOUS = 1052722U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_METHOD_DUPLICATE = 1052723U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_METHOD_VER = 1052724U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_SUITE_FLAGS = 1052736U,
		FW_RULE_STATUS_SEMANTIC_ERROR_HEALTH_CERT = 1052737U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_SIGNCERT_VER = 1052738U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_INTERMEDIATE_CA_VER = 1052739U,
		FW_RULE_STATUS_SEMANTIC_ERROR_MACHINE_SHKEY = 1052752U,
		FW_RULE_STATUS_SEMANTIC_ERROR_CA_NAME = 1052768U,
		FW_RULE_STATUS_SEMANTIC_ERROR_MIXED_CERTS = 1052769U,
		FW_RULE_STATUS_SEMANTIC_ERROR_NON_CONTIGUOUS_CERTS = 1052770U,
		FW_RULE_STATUS_SEMANTIC_ERROR_MIXED_CA_TYPE_IN_BLOCK = 1052771U,
		FW_RULE_STATUS_SEMANTIC_ERROR_MACHINE_USER_AUTH = 1052784U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_CERT_CRITERIA_VER = 1052785U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_CERT_CRITERIA_VER_MISMATCH = 1052786U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_CERT_CRITERIA_RENEWAL_HASH = 1052787U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_CERT_CRITERIA_INVALID_HASH = 1052788U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_CERT_CRITERIA_INVALID_EKU = 1052789U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_CERT_CRITERIA_INVALID_NAME_TYPE = 1052790U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_CERT_CRITERIA_INVALID_NAME = 1052791U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_CERT_CRITERIA_INVALID_CRITERIA_TYPE = 1052792U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_CERT_CRITERIA_MISSING_CRITERIA = 1052793U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PROXY_SERVER = 1052800U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_PROXY_SERVER_VER = 1052801U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_CRYPTO_NON_DEFAULT_ID = 1069056U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_CRYPTO_FLAGS = 1069057U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_CRYPTO_TIMEOUT_MINUTES = 1069058U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_CRYPTO_TIMEOUT_SESSIONS = 1069059U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_CRYPTO_KEY_EXCHANGE = 1069060U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_CRYPTO_ENCRYPTION = 1069061U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_CRYPTO_HASH = 1069062U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_CRYPTO_ENCRYPTION_VER = 1069063U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_CRYPTO_HASH_VER = 1069064U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE1_CRYPTO_KEY_EXCH_VER = 1069065U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE2_CRYPTO_PFS = 1069088U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE2_CRYPTO_PROTOCOL = 1069089U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE2_CRYPTO_ENCRYPTION = 1069090U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE2_CRYPTO_HASH = 1069091U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE2_CRYPTO_TIMEOUT_MINUTES = 1069092U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE2_CRYPTO_TIMEOUT_KBYTES = 1069093U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE2_CRYPTO_ENCRYPTION_VER = 1069094U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE2_CRYPTO_HASH_VER = 1069095U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PHASE2_CRYPTO_PFS_VER = 1069096U,
		FW_RULE_STATUS_SEMANTIC_ERROR_CRYPTO_ENCR_HASH = 1069120U,
		FW_RULE_STATUS_SEMANTIC_ERROR_CRYPTO_ENCR_HASH_COMPAT = 1069121U,
		FW_RULE_STATUS_SEMANTIC_ERROR_SCHEMA_VERSION = 1069136U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_OR_AND_CONDITIONS = 1073152U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_AND_CONDITIONS = 1073153U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_CONDITION_KEY = 1073154U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_CONDITION_MATCH_TYPE = 1073155U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_CONDITION_DATA_TYPE = 1073156U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_CONDITION_KEY_AND_DATA_TYPE = 1073157U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_KEYS_PROTOCOL_PORT = 1073158U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_KEY_PROFILE = 1073159U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_KEY_STATUS = 1073160U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_KEY_FILTERID = 1073161U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_KEY_APP_PATH = 1073168U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_KEY_PROTOCOL = 1073169U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_KEY_LOCAL_PORT = 1073170U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_KEY_REMOTE_PORT = 1073171U,
		FW_RULE_STATUS_SEMANTIC_ERROR_QUERY_KEY_SVC_NAME = 1073173U,
		FW_RULE_STATUS_SEMANTIC_ERROR_REQUIRE_IN_CLEAR_OUT_ON_TRANSPORT = 1077248U,
		FW_RULE_STATUS_SEMANTIC_ERROR_BYPASS_TUNNEL_IF_SECURE_ON_TRANSPORT = 1077249U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_NOENCAP_ON_TUNNEL = 1077250U,
		FW_RULE_STATUS_SEMANTIC_ERROR_AUTH_NOENCAP_ON_PSK = 1077251U,
		FW_RULE_STATUS_SEMANTIC_ERROR_REMOTE_DYNAMIC_KEYWORD_ADDRESSES = 1077252U,
		FW_RULE_STATUS_SEMANTIC_ERROR_PACKAGE_FAMILY_NAME_FIELD_NOT_FOUND = 1077253U,
		FW_RULE_STATUS_RUNTIME_ERROR = 2097152U,
		FW_RULE_STATUS_RUNTIME_ERROR_PHASE1_AUTH_NOT_FOUND = 2097153U,
		FW_RULE_STATUS_RUNTIME_ERROR_PHASE2_AUTH_NOT_FOUND = 2097154U,
		FW_RULE_STATUS_RUNTIME_ERROR_PHASE2_CRYPTO_NOT_FOUND = 2097155U,
		FW_RULE_STATUS_RUNTIME_ERROR_AUTH_MCHN_SHKEY_MISMATCH = 2097156U,
		FW_RULE_STATUS_RUNTIME_ERROR_PHASE1_CRYPTO_NOT_FOUND = 2097157U,
		FW_RULE_STATUS_RUNTIME_ERROR_AUTH_NOENCAP_ON_TUNNEL = 2097158U,
		FW_RULE_STATUS_RUNTIME_ERROR_AUTH_NOENCAP_ON_PSK = 2097159U,
		FW_RULE_STATUS_RUNTIME_ERROR_KEY_MODULE_AUTH_MISMATCH = 2097160U,
		FW_RULE_STATUS_ERROR = 3670016U,
		FW_RULE_STATUS_ALL = 0xFFFF0000
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_RULE_STATUS_CLASS : uint
	{
		FW_RULE_STATUS_CLASS_OK = 65536U,
		FW_RULE_STATUS_CLASS_PARTIALLY_IGNORED = 131072U,
		FW_RULE_STATUS_CLASS_IGNORED = 262144U,
		FW_RULE_STATUS_CLASS_PARSING_ERROR = 524288U,
		FW_RULE_STATUS_CLASS_SEMANTIC_ERROR = 1048576U,
		FW_RULE_STATUS_CLASS_RUNTIME_ERROR = 2097152U,
		FW_RULE_STATUS_CLASS_ERROR = 3670016U,
		FW_RULE_STATUS_CLASS_ALL = 0xFFFF0000
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_OBJECT_CTRL_FLAG : int
	{
		FW_OBJECT_CTRL_FLAG_INCLUDE_METADATA = 1
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_ENFORCEMENT_STATE : int
	{
		FW_ENFORCEMENT_STATE_INVALID = 0,
		FW_ENFORCEMENT_STATE_FULL = 1,
		FW_ENFORCEMENT_STATE_WF_OFF_IN_PROFILE = 2,
		FW_ENFORCEMENT_STATE_CATEGORY_OFF = 3,
		FW_ENFORCEMENT_STATE_DISABLED_OBJECT = 4,
		FW_ENFORCEMENT_STATE_INACTIVE_PROFILE = 5,
		FW_ENFORCEMENT_STATE_LOCAL_ADDRESS_RESOLUTION_EMPTY = 6,
		FW_ENFORCEMENT_STATE_REMOTE_ADDRESS_RESOLUTION_EMPTY = 7,
		FW_ENFORCEMENT_STATE_LOCAL_PORT_RESOLUTION_EMPTY = 8,
		FW_ENFORCEMENT_STATE_REMOTE_PORT_RESOLUTION_EMPTY = 9,
		FW_ENFORCEMENT_STATE_INTERFACE_RESOLUTION_EMPTY = 10,
		FW_ENFORCEMENT_STATE_APPLICATION_RESOLUTION_EMPTY = 11,
		FW_ENFORCEMENT_STATE_REMOTE_MACHINE_EMPTY = 12,
		FW_ENFORCEMENT_STATE_REMOTE_USER_EMPTY = 13,
		FW_ENFORCEMENT_STATE_LOCAL_GLOBAL_OPEN_PORTS_DISALLOWED = 14,
		FW_ENFORCEMENT_STATE_LOCAL_AUTHORIZED_APPLICATIONS_DISALLOWED = 15,
		FW_ENFORCEMENT_STATE_LOCAL_FIREWALL_RULES_DISALLOWED = 16,
		FW_ENFORCEMENT_STATE_LOCAL_CONSEC_RULES_DISALLOWED = 17,
		FW_ENFORCEMENT_STATE_MISMATCHED_PLATFORM = 18,
		FW_ENFORCEMENT_STATE_OPTIMIZED_OUT = 19,
		FW_ENFORCEMENT_STATE_LOCAL_USER_EMPTY = 20,
		FW_ENFORCEMENT_STATE_TRANSPORT_MACHINE_SD_EMPTY = 21,
		FW_ENFORCEMENT_STATE_TRANSPORT_USER_SD_EMPTY = 22,
		FW_ENFORCEMENT_STATE_TUPLE_RESOLUTION_EMPTY = 23,
		FW_ENFORCEMENT_STATE_NETNAME_RESOLUTION_EMPTY = 24,
		FW_ENFORCEMENT_STATE_DUPLICATE = 25,
		FW_ENFORCEMENT_STATE_MAX = 26
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_OBJECT_METADATA : IRpcFixedStruct
	{
		public ulong qwFilterContextID;
		public uint dwNumEntries;
		public RpcPointer<FW_ENFORCEMENT_STATE[]> pEnforcementStates;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.qwFilterContextID);
			encoder.WriteValue(this.dwNumEntries);
			encoder.WriteUniquePointer(this.pEnforcementStates);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.qwFilterContextID = decoder.ReadUInt64();
			this.dwNumEntries = decoder.ReadUInt32();
			this.pEnforcementStates = decoder.ReadUniquePointer<FW_ENFORCEMENT_STATE[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pEnforcementStates is not null)
			{
				encoder.WriteArrayHeader(this.pEnforcementStates.value);
				for (int i = 0; i < this.pEnforcementStates.value.Length; i++)
				{
					FW_ENFORCEMENT_STATE elem_0 = this.pEnforcementStates.value[i];
					encoder.WriteEnumShortValue((short)elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pEnforcementStates is not null)
			{
				this.pEnforcementStates.value = decoder.ReadArrayHeader<FW_ENFORCEMENT_STATE>();
				for (int i = 0; i < this.pEnforcementStates.value.Length; i++)
				{
					FW_ENFORCEMENT_STATE elem_0 = this.pEnforcementStates.value[i];
					elem_0 = (FW_ENFORCEMENT_STATE)decoder.ReadEnumShortValue();
					this.pEnforcementStates.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_OS_PLATFORM_OP : int
	{
		FW_OS_PLATFORM_OP_EQ = 0,
		FW_OS_PLATFORM_OP_GTEQ = 1,
		FW_OS_PLATFORM_OP_MAX = 2,
		FW_OS_PLATFORM_OP_FIELD_SIZE = 5,
		FW_OS_PLATFORM_OP_FIELD_MASK = 248
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_OS_PLATFORM : IRpcFixedStruct
	{
		public byte bPlatform;
		public byte bMajorVersion;
		public byte bMinorVersion;
		public byte Reserved;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.bPlatform);
			encoder.WriteValue(this.bMajorVersion);
			encoder.WriteValue(this.bMinorVersion);
			encoder.WriteValue(this.Reserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.bPlatform = decoder.ReadByte();
			this.bMajorVersion = decoder.ReadByte();
			this.bMinorVersion = decoder.ReadByte();
			this.Reserved = decoder.ReadByte();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_OS_PLATFORM_LIST : IRpcFixedStruct
	{
		public uint dwNumEntries;
		public RpcPointer<FW_OS_PLATFORM[]> pPlatforms;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwNumEntries);
			encoder.WriteUniquePointer(this.pPlatforms);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwNumEntries = decoder.ReadUInt32();
			this.pPlatforms = decoder.ReadUniquePointer<FW_OS_PLATFORM[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pPlatforms is not null)
			{
				encoder.WriteArrayHeader(this.pPlatforms.value);
				for (int i = 0; i < this.pPlatforms.value.Length; i++)
				{
					FW_OS_PLATFORM elem_0 = this.pPlatforms.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._1Byte);
				}

				for (int i = 0; i < this.pPlatforms.value.Length; i++)
				{
					FW_OS_PLATFORM elem_0 = this.pPlatforms.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pPlatforms is not null)
			{
				this.pPlatforms.value = decoder.ReadArrayHeader<FW_OS_PLATFORM>();
				for (int i = 0; i < this.pPlatforms.value.Length; i++)
				{
					FW_OS_PLATFORM elem_0 = this.pPlatforms.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_OS_PLATFORM>(NdrAlignment._1Byte);
					this.pPlatforms.value[i] = elem_0;
				}

				for (int i = 0; i < this.pPlatforms.value.Length; i++)
				{
					FW_OS_PLATFORM elem_0 = this.pPlatforms.value[i];
					decoder.ReadStructDeferral<FW_OS_PLATFORM>(ref elem_0);
					this.pPlatforms.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_NETWORK_NAMES : IRpcFixedStruct
	{
		public uint dwNumEntries;
		public RpcPointer<RpcPointer<string>[]> wszNames;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwNumEntries);
			encoder.WriteUniquePointer(this.wszNames);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwNumEntries = decoder.ReadUInt32();
			this.wszNames = decoder.ReadUniquePointer<RpcPointer<string>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wszNames is not null)
			{
				encoder.WriteArrayHeader(this.wszNames.value);
				for (int i = 0; i < this.wszNames.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.wszNames.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.wszNames.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.wszNames.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteWideCharString(elem_0.value);
					}
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wszNames is not null)
			{
				this.wszNames.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < this.wszNames.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.wszNames.value[i];
					elem_0 = decoder.ReadUniquePointer<string>();
					this.wszNames.value[i] = elem_0;
				}

				for (int i = 0; i < this.wszNames.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.wszNames.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadWideCharString();
					}

					this.wszNames.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_RULE_ORIGIN_TYPE : int
	{
		FW_RULE_ORIGIN_INVALID = 0,
		FW_RULE_ORIGIN_LOCAL = 1,
		FW_RULE_ORIGIN_GP = 2,
		FW_RULE_ORIGIN_DYNAMIC = 3,
		FW_RULE_ORIGIN_AUTOGEN = 4,
		FW_RULE_ORIGIN_HARDCODED = 5,
		FW_RULE_ORIGIN_MDM = 6,
		FW_RULE_ORIGIN_MAX = 7,
		FW_RULE_ORIGIN_HOST_LOCAL = 8,
		FW_RULE_ORIGIN_HOST_GP = 9,
		FW_RULE_ORIGIN_HOST_DYNAMIC = 10,
		FW_RULE_ORIGIN_HOST_MDM = 11,
		FW_RULE_ORIGIN_HOST_MAX = 12
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_ENUM_RULES_FLAGS : int
	{
		FW_ENUM_RULES_FLAG_NONE = 0,
		FW_ENUM_RULES_FLAG_RESOLVE_NAME = 1,
		FW_ENUM_RULES_FLAG_RESOLVE_DESCRIPTION = 2,
		FW_ENUM_RULES_FLAG_RESOLVE_APPLICATION = 4,
		FW_ENUM_RULES_FLAG_RESOLVE_KEYWORD = 8,
		FW_ENUM_RULES_FLAG_RESOLVE_GPO_NAME = 16,
		FW_ENUM_RULES_FLAG_EFFECTIVE = 32,
		FW_ENUM_RULES_FLAG_INCLUDE_METADATA = 64,
		FW_ENUM_RULES_FLAG_MAX = 128
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_RULE_ACTION : int
	{
		FW_RULE_ACTION_INVALID = 0,
		FW_RULE_ACTION_ALLOW_BYPASS = 1,
		FW_RULE_ACTION_BLOCK = 2,
		FW_RULE_ACTION_ALLOW = 3,
		FW_RULE_ACTION_MAX = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_RULE_FLAGS : int
	{
		FW_RULE_FLAGS_NONE = 0,
		FW_RULE_FLAGS_ACTIVE = 1,
		FW_RULE_FLAGS_AUTHENTICATE = 2,
		FW_RULE_FLAGS_AUTHENTICATE_WITH_ENCRYPTION = 4,
		FW_RULE_FLAGS_ROUTEABLE_ADDRS_TRAVERSE = 8,
		FW_RULE_FLAGS_LOOSE_SOURCE_MAPPED = 16,
		FW_RULE_FLAGS_MAX_V2_1 = 32,
		FW_RULE_FLAGS_AUTH_WITH_NO_ENCAPSULATION = 32,
		FW_RULE_FLAGS_MAX_V2_9 = 64,
		FW_RULE_FLAGS_AUTH_WITH_ENC_NEGOTIATE = 64,
		FW_RULE_FLAGS_ROUTEABLE_ADDRS_TRAVERSE_DEFER_APP = 128,
		FW_RULE_FLAGS_ROUTEABLE_ADDRS_TRAVERSE_DEFER_USER = 256,
		FW_RULE_FLAGS_AUTHENTICATE_BYPASS_OUTBOUND = 512,
		FW_RULE_FLAGS_MAX_V2_10 = 1024,
		FW_RULE_FLAGS_ALLOW_PROFILE_CROSSING = 1024,
		FW_RULE_FLAGS_LOCAL_ONLY_MAPPED = 2048,
		FW_RULE_FLAGS_MAX_V2_20 = 4096,
		FW_RULE_FLAGS_LUA_CONDITIONAL_ACE = 4096,
		FW_RULE_FLAGS_BIND_TO_INTERFACE = 8192,
		FW_RULE_FLAGS_MAX = 16384
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_RULE_FLAGS2 : int
	{
		FW_RULE_FLAGS2_NONE = 0,
		FW_RULE_FLAGS2_SYSTEMOS_ONLY = 1,
		FW_RULE_FLAGS2_GAMEOS_ONLY = 2,
		FW_RULE_FLAGS2_DEVMODE = 4,
		FW_RULE_FLAGS_MAX_V2_26 = 8,
		FW_RULE_FLAGS2_NOT_USED_VALUE_8 = 8,
		FW_RULE_FLAGS2_EMPTY_REMOTENAME = 16,
		FW_RULE_FLAGS2_NOT_REMOTENAME = 32,
		FW_RULE_FLAGS2_NOT_USED_VALUE_64 = 64,
		FW_RULE_FLAGS2_CALLOUT_AND_AUDIT = 128,
		FW_RULE_FLAGS2_APP_LOOPBACK = 256,
		FW_RULE_FLAGS2_NOT_USED_VALUE_512 = 512,
		FW_RULE_FLAGS2_NOT_USED_VALUE_1024 = 1024,
		FW_RULE_FLAGS2_NOT_USED_VALUE_2048 = 2048,
		FW_RULE_FLAGS2_INDIRECT_NAME_RESOLVED = 4096,
		FW_RULE_FLAGS2_INDIRECT_DESCRIPTION_RESOLVED = 8192,
		FW_RULE_FLAGS2_MAX = 8192
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_2 : IRpcFixedStruct
	{
		public FW_PORTS LocalPorts;
		public FW_PORTS RemotePorts;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LocalPorts, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemotePorts, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LocalPorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.RemotePorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LocalPorts);
			encoder.WriteStructDeferral(this.RemotePorts);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_PORTS>(ref this.LocalPorts);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.RemotePorts);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_1 : IRpcFixedStruct
	{
		public ushort wIpProtocol;
		public Unnamed_2 __unnamed_0;
		public FW_ICMP_TYPE_CODE_LIST V4TypeCodeList;
		public FW_ICMP_TYPE_CODE_LIST V6TypeCodeList;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.wIpProtocol);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 1:
					encoder.WriteFixedStruct(this.V4TypeCodeList, NdrAlignment.NativePtr);
					break;
				case 58:
					encoder.WriteFixedStruct(this.V6TypeCodeList, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.wIpProtocol = decoder.ReadUInt16();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_2>(NdrAlignment.NativePtr);
					break;
				case 1:
					this.V4TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
				case 58:
					this.V6TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 1:
					encoder.WriteStructDeferral(this.V4TypeCodeList);
					break;
				case 58:
					encoder.WriteStructDeferral(this.V6TypeCodeList);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					decoder.ReadStructDeferral<Unnamed_2>(ref this.__unnamed_0);
					break;
				case 1:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V4TypeCodeList);
					break;
				case 58:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V6TypeCodeList);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_RULE2_0 : IRpcFixedStruct
	{
		public RpcPointer<FW_RULE2_0> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_DIRECTION Direction;
		public ushort wIpProtocol;
		public Unnamed_1 unnamed_1;
		public FW_ADDRESSES LocalAddresses;
		public FW_ADDRESSES RemoteAddresses;
		public FW_INTERFACE_LUIDS LocalInterfaceIds;
		public uint dwLocalInterfaceTypes;
		public RpcPointer<string> wszLocalApplication;
		public RpcPointer<string> wszLocalService;
		public FW_RULE_ACTION Action;
		public ushort wFlags;
		public RpcPointer<string> wszRemoteMachineAuthorizationList;
		public RpcPointer<string> wszRemoteUserAuthorizationList;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_STATUS Status;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public uint Reserved;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteEnumShortValue((short)this.Direction);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteUnion(this.unnamed_1);
			encoder.WriteFixedStruct(this.LocalAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemoteAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalInterfaceIds, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwLocalInterfaceTypes);
			encoder.WriteUniquePointer(this.wszLocalApplication);
			encoder.WriteUniquePointer(this.wszLocalService);
			encoder.WriteEnumShortValue((short)this.Action);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszRemoteMachineAuthorizationList);
			encoder.WriteUniquePointer(this.wszRemoteUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteValue((int)this.Status);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue(this.Reserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_RULE2_0>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Direction = (FW_DIRECTION)decoder.ReadEnumShortValue();
			this.wIpProtocol = decoder.ReadUInt16();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_1>();
			this.LocalAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.RemoteAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.LocalInterfaceIds = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
			this.dwLocalInterfaceTypes = decoder.ReadUInt32();
			this.wszLocalApplication = decoder.ReadUniquePointer<string>();
			this.wszLocalService = decoder.ReadUniquePointer<string>();
			this.Action = (FW_RULE_ACTION)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.wszRemoteMachineAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszRemoteUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Reserved = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.unnamed_1);
			encoder.WriteStructDeferral(this.LocalAddresses);
			encoder.WriteStructDeferral(this.RemoteAddresses);
			encoder.WriteStructDeferral(this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				encoder.WriteWideCharString(this.wszLocalApplication.value);
			}

			if (this.wszLocalService is not null)
			{
				encoder.WriteWideCharString(this.wszLocalService.value);
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteMachineAuthorizationList.value);
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteUserAuthorizationList.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_RULE2_0>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_0>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<Unnamed_1>(ref this.unnamed_1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.LocalAddresses);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.RemoteAddresses);
			decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				this.wszLocalApplication.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalService is not null)
			{
				this.wszLocalService.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				this.wszRemoteMachineAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				this.wszRemoteUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_4 : IRpcFixedStruct
	{
		public FW_PORTS LocalPorts;
		public FW_PORTS RemotePorts;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LocalPorts, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemotePorts, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LocalPorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.RemotePorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LocalPorts);
			encoder.WriteStructDeferral(this.RemotePorts);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_PORTS>(ref this.LocalPorts);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.RemotePorts);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_3 : IRpcFixedStruct
	{
		public ushort wIpProtocol;
		public Unnamed_4 __unnamed_0;
		public FW_ICMP_TYPE_CODE_LIST V4TypeCodeList;
		public FW_ICMP_TYPE_CODE_LIST V6TypeCodeList;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.wIpProtocol);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 1:
					encoder.WriteFixedStruct(this.V4TypeCodeList, NdrAlignment.NativePtr);
					break;
				case 58:
					encoder.WriteFixedStruct(this.V6TypeCodeList, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.wIpProtocol = decoder.ReadUInt16();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_4>(NdrAlignment.NativePtr);
					break;
				case 1:
					this.V4TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
				case 58:
					this.V6TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 1:
					encoder.WriteStructDeferral(this.V4TypeCodeList);
					break;
				case 58:
					encoder.WriteStructDeferral(this.V6TypeCodeList);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					decoder.ReadStructDeferral<Unnamed_4>(ref this.__unnamed_0);
					break;
				case 1:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V4TypeCodeList);
					break;
				case 58:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V6TypeCodeList);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_RULE2_10 : IRpcFixedStruct
	{
		public RpcPointer<FW_RULE2_10> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_DIRECTION Direction;
		public ushort wIpProtocol;
		public Unnamed_3 unnamed_1;
		public FW_ADDRESSES LocalAddresses;
		public FW_ADDRESSES RemoteAddresses;
		public FW_INTERFACE_LUIDS LocalInterfaceIds;
		public uint dwLocalInterfaceTypes;
		public RpcPointer<string> wszLocalApplication;
		public RpcPointer<string> wszLocalService;
		public FW_RULE_ACTION Action;
		public ushort wFlags;
		public RpcPointer<string> wszRemoteMachineAuthorizationList;
		public RpcPointer<string> wszRemoteUserAuthorizationList;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_STATUS Status;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public uint Reserved;
		public RpcPointer<FW_OBJECT_METADATA[]> pMetaData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteEnumShortValue((short)this.Direction);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteUnion(this.unnamed_1);
			encoder.WriteFixedStruct(this.LocalAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemoteAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalInterfaceIds, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwLocalInterfaceTypes);
			encoder.WriteUniquePointer(this.wszLocalApplication);
			encoder.WriteUniquePointer(this.wszLocalService);
			encoder.WriteEnumShortValue((short)this.Action);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszRemoteMachineAuthorizationList);
			encoder.WriteUniquePointer(this.wszRemoteUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteValue((int)this.Status);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue(this.Reserved);
			encoder.WriteUniquePointer(this.pMetaData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_RULE2_10>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Direction = (FW_DIRECTION)decoder.ReadEnumShortValue();
			this.wIpProtocol = decoder.ReadUInt16();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_3>();
			this.LocalAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.RemoteAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.LocalInterfaceIds = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
			this.dwLocalInterfaceTypes = decoder.ReadUInt32();
			this.wszLocalApplication = decoder.ReadUniquePointer<string>();
			this.wszLocalService = decoder.ReadUniquePointer<string>();
			this.Action = (FW_RULE_ACTION)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.wszRemoteMachineAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszRemoteUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Reserved = decoder.ReadUInt32();
			this.pMetaData = decoder.ReadUniquePointer<FW_OBJECT_METADATA[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.unnamed_1);
			encoder.WriteStructDeferral(this.LocalAddresses);
			encoder.WriteStructDeferral(this.RemoteAddresses);
			encoder.WriteStructDeferral(this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				encoder.WriteWideCharString(this.wszLocalApplication.value);
			}

			if (this.wszLocalService is not null)
			{
				encoder.WriteWideCharString(this.wszLocalService.value);
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteMachineAuthorizationList.value);
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteUserAuthorizationList.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}

			if (this.pMetaData is not null)
			{
				encoder.WriteArrayHeader(this.pMetaData.value);
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_RULE2_10>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_10>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<Unnamed_3>(ref this.unnamed_1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.LocalAddresses);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.RemoteAddresses);
			decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				this.wszLocalApplication.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalService is not null)
			{
				this.wszLocalService.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				this.wszRemoteMachineAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				this.wszRemoteUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}

			if (this.pMetaData is not null)
			{
				this.pMetaData.value = decoder.ReadArrayHeader<FW_OBJECT_METADATA>();
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_OBJECT_METADATA>(NdrAlignment._8Byte);
					this.pMetaData.value[i] = elem_0;
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					decoder.ReadStructDeferral<FW_OBJECT_METADATA>(ref elem_0);
					this.pMetaData.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_6 : IRpcFixedStruct
	{
		public FW_PORTS LocalPorts;
		public FW_PORTS RemotePorts;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LocalPorts, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemotePorts, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LocalPorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.RemotePorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LocalPorts);
			encoder.WriteStructDeferral(this.RemotePorts);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_PORTS>(ref this.LocalPorts);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.RemotePorts);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_5 : IRpcFixedStruct
	{
		public ushort wIpProtocol;
		public Unnamed_6 __unnamed_0;
		public FW_ICMP_TYPE_CODE_LIST V4TypeCodeList;
		public FW_ICMP_TYPE_CODE_LIST V6TypeCodeList;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.wIpProtocol);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 1:
					encoder.WriteFixedStruct(this.V4TypeCodeList, NdrAlignment.NativePtr);
					break;
				case 58:
					encoder.WriteFixedStruct(this.V6TypeCodeList, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.wIpProtocol = decoder.ReadUInt16();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_6>(NdrAlignment.NativePtr);
					break;
				case 1:
					this.V4TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
				case 58:
					this.V6TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 1:
					encoder.WriteStructDeferral(this.V4TypeCodeList);
					break;
				case 58:
					encoder.WriteStructDeferral(this.V6TypeCodeList);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					decoder.ReadStructDeferral<Unnamed_6>(ref this.__unnamed_0);
					break;
				case 1:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V4TypeCodeList);
					break;
				case 58:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V6TypeCodeList);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_RULE2_20 : IRpcFixedStruct
	{
		public RpcPointer<FW_RULE2_20> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_DIRECTION Direction;
		public ushort wIpProtocol;
		public Unnamed_5 unnamed_1;
		public FW_ADDRESSES LocalAddresses;
		public FW_ADDRESSES RemoteAddresses;
		public FW_INTERFACE_LUIDS LocalInterfaceIds;
		public uint dwLocalInterfaceTypes;
		public RpcPointer<string> wszLocalApplication;
		public RpcPointer<string> wszLocalService;
		public FW_RULE_ACTION Action;
		public ushort wFlags;
		public RpcPointer<string> wszRemoteMachineAuthorizationList;
		public RpcPointer<string> wszRemoteUserAuthorizationList;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_STATUS Status;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public uint Reserved;
		public RpcPointer<FW_OBJECT_METADATA[]> pMetaData;
		public RpcPointer<string> wszLocalUserAuthorizationList;
		public RpcPointer<string> wszPackageId;
		public RpcPointer<string> wszLocalUserOwner;
		public uint dwTrustTupleKeywords;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteEnumShortValue((short)this.Direction);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteUnion(this.unnamed_1);
			encoder.WriteFixedStruct(this.LocalAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemoteAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalInterfaceIds, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwLocalInterfaceTypes);
			encoder.WriteUniquePointer(this.wszLocalApplication);
			encoder.WriteUniquePointer(this.wszLocalService);
			encoder.WriteEnumShortValue((short)this.Action);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszRemoteMachineAuthorizationList);
			encoder.WriteUniquePointer(this.wszRemoteUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteValue((int)this.Status);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue(this.Reserved);
			encoder.WriteUniquePointer(this.pMetaData);
			encoder.WriteUniquePointer(this.wszLocalUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszPackageId);
			encoder.WriteUniquePointer(this.wszLocalUserOwner);
			encoder.WriteValue(this.dwTrustTupleKeywords);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_RULE2_20>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Direction = (FW_DIRECTION)decoder.ReadEnumShortValue();
			this.wIpProtocol = decoder.ReadUInt16();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_5>();
			this.LocalAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.RemoteAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.LocalInterfaceIds = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
			this.dwLocalInterfaceTypes = decoder.ReadUInt32();
			this.wszLocalApplication = decoder.ReadUniquePointer<string>();
			this.wszLocalService = decoder.ReadUniquePointer<string>();
			this.Action = (FW_RULE_ACTION)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.wszRemoteMachineAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszRemoteUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Reserved = decoder.ReadUInt32();
			this.pMetaData = decoder.ReadUniquePointer<FW_OBJECT_METADATA[]>();
			this.wszLocalUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszPackageId = decoder.ReadUniquePointer<string>();
			this.wszLocalUserOwner = decoder.ReadUniquePointer<string>();
			this.dwTrustTupleKeywords = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.unnamed_1);
			encoder.WriteStructDeferral(this.LocalAddresses);
			encoder.WriteStructDeferral(this.RemoteAddresses);
			encoder.WriteStructDeferral(this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				encoder.WriteWideCharString(this.wszLocalApplication.value);
			}

			if (this.wszLocalService is not null)
			{
				encoder.WriteWideCharString(this.wszLocalService.value);
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteMachineAuthorizationList.value);
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteUserAuthorizationList.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}

			if (this.pMetaData is not null)
			{
				encoder.WriteArrayHeader(this.pMetaData.value);
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserAuthorizationList.value);
			}

			if (this.wszPackageId is not null)
			{
				encoder.WriteWideCharString(this.wszPackageId.value);
			}

			if (this.wszLocalUserOwner is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserOwner.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_RULE2_20>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_20>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<Unnamed_5>(ref this.unnamed_1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.LocalAddresses);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.RemoteAddresses);
			decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				this.wszLocalApplication.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalService is not null)
			{
				this.wszLocalService.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				this.wszRemoteMachineAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				this.wszRemoteUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}

			if (this.pMetaData is not null)
			{
				this.pMetaData.value = decoder.ReadArrayHeader<FW_OBJECT_METADATA>();
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_OBJECT_METADATA>(NdrAlignment._8Byte);
					this.pMetaData.value[i] = elem_0;
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					decoder.ReadStructDeferral<FW_OBJECT_METADATA>(ref elem_0);
					this.pMetaData.value[i] = elem_0;
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				this.wszLocalUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszPackageId is not null)
			{
				this.wszPackageId.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalUserOwner is not null)
			{
				this.wszLocalUserOwner.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_8 : IRpcFixedStruct
	{
		public FW_PORTS LocalPorts;
		public FW_PORTS RemotePorts;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LocalPorts, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemotePorts, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LocalPorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.RemotePorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LocalPorts);
			encoder.WriteStructDeferral(this.RemotePorts);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_PORTS>(ref this.LocalPorts);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.RemotePorts);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_7 : IRpcFixedStruct
	{
		public ushort wIpProtocol;
		public Unnamed_8 __unnamed_0;
		public FW_ICMP_TYPE_CODE_LIST V4TypeCodeList;
		public FW_ICMP_TYPE_CODE_LIST V6TypeCodeList;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.wIpProtocol);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 1:
					encoder.WriteFixedStruct(this.V4TypeCodeList, NdrAlignment.NativePtr);
					break;
				case 58:
					encoder.WriteFixedStruct(this.V6TypeCodeList, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.wIpProtocol = decoder.ReadUInt16();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_8>(NdrAlignment.NativePtr);
					break;
				case 1:
					this.V4TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
				case 58:
					this.V6TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 1:
					encoder.WriteStructDeferral(this.V4TypeCodeList);
					break;
				case 58:
					encoder.WriteStructDeferral(this.V6TypeCodeList);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					decoder.ReadStructDeferral<Unnamed_8>(ref this.__unnamed_0);
					break;
				case 1:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V4TypeCodeList);
					break;
				case 58:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V6TypeCodeList);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_RULE2_24 : IRpcFixedStruct
	{
		public RpcPointer<FW_RULE2_24> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_DIRECTION Direction;
		public ushort wIpProtocol;
		public Unnamed_7 unnamed_1;
		public FW_ADDRESSES LocalAddresses;
		public FW_ADDRESSES RemoteAddresses;
		public FW_INTERFACE_LUIDS LocalInterfaceIds;
		public uint dwLocalInterfaceTypes;
		public RpcPointer<string> wszLocalApplication;
		public RpcPointer<string> wszLocalService;
		public FW_RULE_ACTION Action;
		public ushort wFlags;
		public RpcPointer<string> wszRemoteMachineAuthorizationList;
		public RpcPointer<string> wszRemoteUserAuthorizationList;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_STATUS Status;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public uint Reserved;
		public RpcPointer<FW_OBJECT_METADATA[]> pMetaData;
		public RpcPointer<string> wszLocalUserAuthorizationList;
		public RpcPointer<string> wszPackageId;
		public RpcPointer<string> wszLocalUserOwner;
		public uint dwTrustTupleKeywords;
		public FW_NETWORK_NAMES OnNetworkNames;
		public RpcPointer<string> wszSecurityRealmId;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteEnumShortValue((short)this.Direction);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteUnion(this.unnamed_1);
			encoder.WriteFixedStruct(this.LocalAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemoteAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalInterfaceIds, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwLocalInterfaceTypes);
			encoder.WriteUniquePointer(this.wszLocalApplication);
			encoder.WriteUniquePointer(this.wszLocalService);
			encoder.WriteEnumShortValue((short)this.Action);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszRemoteMachineAuthorizationList);
			encoder.WriteUniquePointer(this.wszRemoteUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteValue((int)this.Status);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue(this.Reserved);
			encoder.WriteUniquePointer(this.pMetaData);
			encoder.WriteUniquePointer(this.wszLocalUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszPackageId);
			encoder.WriteUniquePointer(this.wszLocalUserOwner);
			encoder.WriteValue(this.dwTrustTupleKeywords);
			encoder.WriteFixedStruct(this.OnNetworkNames, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.wszSecurityRealmId);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_RULE2_24>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Direction = (FW_DIRECTION)decoder.ReadEnumShortValue();
			this.wIpProtocol = decoder.ReadUInt16();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_7>();
			this.LocalAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.RemoteAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.LocalInterfaceIds = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
			this.dwLocalInterfaceTypes = decoder.ReadUInt32();
			this.wszLocalApplication = decoder.ReadUniquePointer<string>();
			this.wszLocalService = decoder.ReadUniquePointer<string>();
			this.Action = (FW_RULE_ACTION)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.wszRemoteMachineAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszRemoteUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Reserved = decoder.ReadUInt32();
			this.pMetaData = decoder.ReadUniquePointer<FW_OBJECT_METADATA[]>();
			this.wszLocalUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszPackageId = decoder.ReadUniquePointer<string>();
			this.wszLocalUserOwner = decoder.ReadUniquePointer<string>();
			this.dwTrustTupleKeywords = decoder.ReadUInt32();
			this.OnNetworkNames = decoder.ReadFixedStruct<FW_NETWORK_NAMES>(NdrAlignment.NativePtr);
			this.wszSecurityRealmId = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.unnamed_1);
			encoder.WriteStructDeferral(this.LocalAddresses);
			encoder.WriteStructDeferral(this.RemoteAddresses);
			encoder.WriteStructDeferral(this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				encoder.WriteWideCharString(this.wszLocalApplication.value);
			}

			if (this.wszLocalService is not null)
			{
				encoder.WriteWideCharString(this.wszLocalService.value);
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteMachineAuthorizationList.value);
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteUserAuthorizationList.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}

			if (this.pMetaData is not null)
			{
				encoder.WriteArrayHeader(this.pMetaData.value);
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserAuthorizationList.value);
			}

			if (this.wszPackageId is not null)
			{
				encoder.WriteWideCharString(this.wszPackageId.value);
			}

			if (this.wszLocalUserOwner is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserOwner.value);
			}

			encoder.WriteStructDeferral(this.OnNetworkNames);
			if (this.wszSecurityRealmId is not null)
			{
				encoder.WriteWideCharString(this.wszSecurityRealmId.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_RULE2_24>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_24>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<Unnamed_7>(ref this.unnamed_1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.LocalAddresses);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.RemoteAddresses);
			decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				this.wszLocalApplication.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalService is not null)
			{
				this.wszLocalService.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				this.wszRemoteMachineAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				this.wszRemoteUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}

			if (this.pMetaData is not null)
			{
				this.pMetaData.value = decoder.ReadArrayHeader<FW_OBJECT_METADATA>();
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_OBJECT_METADATA>(NdrAlignment._8Byte);
					this.pMetaData.value[i] = elem_0;
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					decoder.ReadStructDeferral<FW_OBJECT_METADATA>(ref elem_0);
					this.pMetaData.value[i] = elem_0;
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				this.wszLocalUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszPackageId is not null)
			{
				this.wszPackageId.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalUserOwner is not null)
			{
				this.wszLocalUserOwner.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_NETWORK_NAMES>(ref this.OnNetworkNames);
			if (this.wszSecurityRealmId is not null)
			{
				this.wszSecurityRealmId.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_10 : IRpcFixedStruct
	{
		public FW_PORTS LocalPorts;
		public FW_PORTS RemotePorts;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LocalPorts, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemotePorts, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LocalPorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.RemotePorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LocalPorts);
			encoder.WriteStructDeferral(this.RemotePorts);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_PORTS>(ref this.LocalPorts);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.RemotePorts);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_9 : IRpcFixedStruct
	{
		public ushort wIpProtocol;
		public Unnamed_10 __unnamed_0;
		public FW_ICMP_TYPE_CODE_LIST V4TypeCodeList;
		public FW_ICMP_TYPE_CODE_LIST V6TypeCodeList;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.wIpProtocol);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 1:
					encoder.WriteFixedStruct(this.V4TypeCodeList, NdrAlignment.NativePtr);
					break;
				case 58:
					encoder.WriteFixedStruct(this.V6TypeCodeList, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.wIpProtocol = decoder.ReadUInt16();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_10>(NdrAlignment.NativePtr);
					break;
				case 1:
					this.V4TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
				case 58:
					this.V6TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 1:
					encoder.WriteStructDeferral(this.V4TypeCodeList);
					break;
				case 58:
					encoder.WriteStructDeferral(this.V6TypeCodeList);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					decoder.ReadStructDeferral<Unnamed_10>(ref this.__unnamed_0);
					break;
				case 1:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V4TypeCodeList);
					break;
				case 58:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V6TypeCodeList);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_RULE2_25 : IRpcFixedStruct
	{
		public RpcPointer<FW_RULE2_25> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_DIRECTION Direction;
		public ushort wIpProtocol;
		public Unnamed_9 unnamed_1;
		public FW_ADDRESSES LocalAddresses;
		public FW_ADDRESSES RemoteAddresses;
		public FW_INTERFACE_LUIDS LocalInterfaceIds;
		public uint dwLocalInterfaceTypes;
		public RpcPointer<string> wszLocalApplication;
		public RpcPointer<string> wszLocalService;
		public FW_RULE_ACTION Action;
		public ushort wFlags;
		public RpcPointer<string> wszRemoteMachineAuthorizationList;
		public RpcPointer<string> wszRemoteUserAuthorizationList;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_STATUS Status;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public uint Reserved;
		public RpcPointer<FW_OBJECT_METADATA[]> pMetaData;
		public RpcPointer<string> wszLocalUserAuthorizationList;
		public RpcPointer<string> wszPackageId;
		public RpcPointer<string> wszLocalUserOwner;
		public uint dwTrustTupleKeywords;
		public FW_NETWORK_NAMES OnNetworkNames;
		public RpcPointer<string> wszSecurityRealmId;
		public ushort wFlags2;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteEnumShortValue((short)this.Direction);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteUnion(this.unnamed_1);
			encoder.WriteFixedStruct(this.LocalAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemoteAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalInterfaceIds, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwLocalInterfaceTypes);
			encoder.WriteUniquePointer(this.wszLocalApplication);
			encoder.WriteUniquePointer(this.wszLocalService);
			encoder.WriteEnumShortValue((short)this.Action);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszRemoteMachineAuthorizationList);
			encoder.WriteUniquePointer(this.wszRemoteUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteValue((int)this.Status);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue(this.Reserved);
			encoder.WriteUniquePointer(this.pMetaData);
			encoder.WriteUniquePointer(this.wszLocalUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszPackageId);
			encoder.WriteUniquePointer(this.wszLocalUserOwner);
			encoder.WriteValue(this.dwTrustTupleKeywords);
			encoder.WriteFixedStruct(this.OnNetworkNames, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.wszSecurityRealmId);
			encoder.WriteValue(this.wFlags2);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_RULE2_25>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Direction = (FW_DIRECTION)decoder.ReadEnumShortValue();
			this.wIpProtocol = decoder.ReadUInt16();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_9>();
			this.LocalAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.RemoteAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.LocalInterfaceIds = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
			this.dwLocalInterfaceTypes = decoder.ReadUInt32();
			this.wszLocalApplication = decoder.ReadUniquePointer<string>();
			this.wszLocalService = decoder.ReadUniquePointer<string>();
			this.Action = (FW_RULE_ACTION)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.wszRemoteMachineAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszRemoteUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Reserved = decoder.ReadUInt32();
			this.pMetaData = decoder.ReadUniquePointer<FW_OBJECT_METADATA[]>();
			this.wszLocalUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszPackageId = decoder.ReadUniquePointer<string>();
			this.wszLocalUserOwner = decoder.ReadUniquePointer<string>();
			this.dwTrustTupleKeywords = decoder.ReadUInt32();
			this.OnNetworkNames = decoder.ReadFixedStruct<FW_NETWORK_NAMES>(NdrAlignment.NativePtr);
			this.wszSecurityRealmId = decoder.ReadUniquePointer<string>();
			this.wFlags2 = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.unnamed_1);
			encoder.WriteStructDeferral(this.LocalAddresses);
			encoder.WriteStructDeferral(this.RemoteAddresses);
			encoder.WriteStructDeferral(this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				encoder.WriteWideCharString(this.wszLocalApplication.value);
			}

			if (this.wszLocalService is not null)
			{
				encoder.WriteWideCharString(this.wszLocalService.value);
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteMachineAuthorizationList.value);
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteUserAuthorizationList.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}

			if (this.pMetaData is not null)
			{
				encoder.WriteArrayHeader(this.pMetaData.value);
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserAuthorizationList.value);
			}

			if (this.wszPackageId is not null)
			{
				encoder.WriteWideCharString(this.wszPackageId.value);
			}

			if (this.wszLocalUserOwner is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserOwner.value);
			}

			encoder.WriteStructDeferral(this.OnNetworkNames);
			if (this.wszSecurityRealmId is not null)
			{
				encoder.WriteWideCharString(this.wszSecurityRealmId.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_RULE2_25>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_25>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<Unnamed_9>(ref this.unnamed_1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.LocalAddresses);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.RemoteAddresses);
			decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				this.wszLocalApplication.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalService is not null)
			{
				this.wszLocalService.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				this.wszRemoteMachineAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				this.wszRemoteUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}

			if (this.pMetaData is not null)
			{
				this.pMetaData.value = decoder.ReadArrayHeader<FW_OBJECT_METADATA>();
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_OBJECT_METADATA>(NdrAlignment._8Byte);
					this.pMetaData.value[i] = elem_0;
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					decoder.ReadStructDeferral<FW_OBJECT_METADATA>(ref elem_0);
					this.pMetaData.value[i] = elem_0;
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				this.wszLocalUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszPackageId is not null)
			{
				this.wszPackageId.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalUserOwner is not null)
			{
				this.wszLocalUserOwner.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_NETWORK_NAMES>(ref this.OnNetworkNames);
			if (this.wszSecurityRealmId is not null)
			{
				this.wszSecurityRealmId.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_12 : IRpcFixedStruct
	{
		public FW_PORTS LocalPorts;
		public FW_PORTS RemotePorts;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LocalPorts, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemotePorts, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LocalPorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.RemotePorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LocalPorts);
			encoder.WriteStructDeferral(this.RemotePorts);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_PORTS>(ref this.LocalPorts);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.RemotePorts);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_11 : IRpcFixedStruct
	{
		public ushort wIpProtocol;
		public Unnamed_12 __unnamed_0;
		public FW_ICMP_TYPE_CODE_LIST V4TypeCodeList;
		public FW_ICMP_TYPE_CODE_LIST V6TypeCodeList;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.wIpProtocol);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 1:
					encoder.WriteFixedStruct(this.V4TypeCodeList, NdrAlignment.NativePtr);
					break;
				case 58:
					encoder.WriteFixedStruct(this.V6TypeCodeList, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.wIpProtocol = decoder.ReadUInt16();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_12>(NdrAlignment.NativePtr);
					break;
				case 1:
					this.V4TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
				case 58:
					this.V6TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 1:
					encoder.WriteStructDeferral(this.V4TypeCodeList);
					break;
				case 58:
					encoder.WriteStructDeferral(this.V6TypeCodeList);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					decoder.ReadStructDeferral<Unnamed_12>(ref this.__unnamed_0);
					break;
				case 1:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V4TypeCodeList);
					break;
				case 58:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V6TypeCodeList);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_RULE2_26 : IRpcFixedStruct
	{
		public RpcPointer<FW_RULE2_26> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_DIRECTION Direction;
		public ushort wIpProtocol;
		public Unnamed_11 unnamed_1;
		public FW_ADDRESSES LocalAddresses;
		public FW_ADDRESSES RemoteAddresses;
		public FW_INTERFACE_LUIDS LocalInterfaceIds;
		public uint dwLocalInterfaceTypes;
		public RpcPointer<string> wszLocalApplication;
		public RpcPointer<string> wszLocalService;
		public FW_RULE_ACTION Action;
		public ushort wFlags;
		public RpcPointer<string> wszRemoteMachineAuthorizationList;
		public RpcPointer<string> wszRemoteUserAuthorizationList;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_STATUS Status;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public uint Reserved;
		public RpcPointer<FW_OBJECT_METADATA[]> pMetaData;
		public RpcPointer<string> wszLocalUserAuthorizationList;
		public RpcPointer<string> wszPackageId;
		public RpcPointer<string> wszLocalUserOwner;
		public uint dwTrustTupleKeywords;
		public FW_NETWORK_NAMES OnNetworkNames;
		public RpcPointer<string> wszSecurityRealmId;
		public ushort wFlags2;
		public FW_NETWORK_NAMES RemoteOutServerNames;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteEnumShortValue((short)this.Direction);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteUnion(this.unnamed_1);
			encoder.WriteFixedStruct(this.LocalAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemoteAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalInterfaceIds, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwLocalInterfaceTypes);
			encoder.WriteUniquePointer(this.wszLocalApplication);
			encoder.WriteUniquePointer(this.wszLocalService);
			encoder.WriteEnumShortValue((short)this.Action);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszRemoteMachineAuthorizationList);
			encoder.WriteUniquePointer(this.wszRemoteUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteValue((int)this.Status);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue(this.Reserved);
			encoder.WriteUniquePointer(this.pMetaData);
			encoder.WriteUniquePointer(this.wszLocalUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszPackageId);
			encoder.WriteUniquePointer(this.wszLocalUserOwner);
			encoder.WriteValue(this.dwTrustTupleKeywords);
			encoder.WriteFixedStruct(this.OnNetworkNames, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.wszSecurityRealmId);
			encoder.WriteValue(this.wFlags2);
			encoder.WriteFixedStruct(this.RemoteOutServerNames, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_RULE2_26>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Direction = (FW_DIRECTION)decoder.ReadEnumShortValue();
			this.wIpProtocol = decoder.ReadUInt16();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_11>();
			this.LocalAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.RemoteAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.LocalInterfaceIds = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
			this.dwLocalInterfaceTypes = decoder.ReadUInt32();
			this.wszLocalApplication = decoder.ReadUniquePointer<string>();
			this.wszLocalService = decoder.ReadUniquePointer<string>();
			this.Action = (FW_RULE_ACTION)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.wszRemoteMachineAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszRemoteUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Reserved = decoder.ReadUInt32();
			this.pMetaData = decoder.ReadUniquePointer<FW_OBJECT_METADATA[]>();
			this.wszLocalUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszPackageId = decoder.ReadUniquePointer<string>();
			this.wszLocalUserOwner = decoder.ReadUniquePointer<string>();
			this.dwTrustTupleKeywords = decoder.ReadUInt32();
			this.OnNetworkNames = decoder.ReadFixedStruct<FW_NETWORK_NAMES>(NdrAlignment.NativePtr);
			this.wszSecurityRealmId = decoder.ReadUniquePointer<string>();
			this.wFlags2 = decoder.ReadUInt16();
			this.RemoteOutServerNames = decoder.ReadFixedStruct<FW_NETWORK_NAMES>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.unnamed_1);
			encoder.WriteStructDeferral(this.LocalAddresses);
			encoder.WriteStructDeferral(this.RemoteAddresses);
			encoder.WriteStructDeferral(this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				encoder.WriteWideCharString(this.wszLocalApplication.value);
			}

			if (this.wszLocalService is not null)
			{
				encoder.WriteWideCharString(this.wszLocalService.value);
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteMachineAuthorizationList.value);
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteUserAuthorizationList.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}

			if (this.pMetaData is not null)
			{
				encoder.WriteArrayHeader(this.pMetaData.value);
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserAuthorizationList.value);
			}

			if (this.wszPackageId is not null)
			{
				encoder.WriteWideCharString(this.wszPackageId.value);
			}

			if (this.wszLocalUserOwner is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserOwner.value);
			}

			encoder.WriteStructDeferral(this.OnNetworkNames);
			if (this.wszSecurityRealmId is not null)
			{
				encoder.WriteWideCharString(this.wszSecurityRealmId.value);
			}

			encoder.WriteStructDeferral(this.RemoteOutServerNames);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_RULE2_26>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_26>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<Unnamed_11>(ref this.unnamed_1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.LocalAddresses);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.RemoteAddresses);
			decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				this.wszLocalApplication.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalService is not null)
			{
				this.wszLocalService.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				this.wszRemoteMachineAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				this.wszRemoteUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}

			if (this.pMetaData is not null)
			{
				this.pMetaData.value = decoder.ReadArrayHeader<FW_OBJECT_METADATA>();
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_OBJECT_METADATA>(NdrAlignment._8Byte);
					this.pMetaData.value[i] = elem_0;
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					decoder.ReadStructDeferral<FW_OBJECT_METADATA>(ref elem_0);
					this.pMetaData.value[i] = elem_0;
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				this.wszLocalUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszPackageId is not null)
			{
				this.wszPackageId.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalUserOwner is not null)
			{
				this.wszLocalUserOwner.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_NETWORK_NAMES>(ref this.OnNetworkNames);
			if (this.wszSecurityRealmId is not null)
			{
				this.wszSecurityRealmId.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_NETWORK_NAMES>(ref this.RemoteOutServerNames);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_14 : IRpcFixedStruct
	{
		public FW_PORTS LocalPorts;
		public FW_PORTS RemotePorts;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LocalPorts, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemotePorts, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LocalPorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.RemotePorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LocalPorts);
			encoder.WriteStructDeferral(this.RemotePorts);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_PORTS>(ref this.LocalPorts);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.RemotePorts);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_13 : IRpcFixedStruct
	{
		public ushort wIpProtocol;
		public Unnamed_14 __unnamed_0;
		public FW_ICMP_TYPE_CODE_LIST V4TypeCodeList;
		public FW_ICMP_TYPE_CODE_LIST V6TypeCodeList;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.wIpProtocol);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 1:
					encoder.WriteFixedStruct(this.V4TypeCodeList, NdrAlignment.NativePtr);
					break;
				case 58:
					encoder.WriteFixedStruct(this.V6TypeCodeList, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.wIpProtocol = decoder.ReadUInt16();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_14>(NdrAlignment.NativePtr);
					break;
				case 1:
					this.V4TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
				case 58:
					this.V6TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 1:
					encoder.WriteStructDeferral(this.V4TypeCodeList);
					break;
				case 58:
					encoder.WriteStructDeferral(this.V6TypeCodeList);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					decoder.ReadStructDeferral<Unnamed_14>(ref this.__unnamed_0);
					break;
				case 1:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V4TypeCodeList);
					break;
				case 58:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V6TypeCodeList);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_RULE2_27 : IRpcFixedStruct
	{
		public RpcPointer<FW_RULE2_27> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_DIRECTION Direction;
		public ushort wIpProtocol;
		public Unnamed_13 unnamed_1;
		public FW_ADDRESSES LocalAddresses;
		public FW_ADDRESSES RemoteAddresses;
		public FW_INTERFACE_LUIDS LocalInterfaceIds;
		public uint dwLocalInterfaceTypes;
		public RpcPointer<string> wszLocalApplication;
		public RpcPointer<string> wszLocalService;
		public FW_RULE_ACTION Action;
		public ushort wFlags;
		public RpcPointer<string> wszRemoteMachineAuthorizationList;
		public RpcPointer<string> wszRemoteUserAuthorizationList;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_STATUS Status;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public uint Reserved;
		public RpcPointer<FW_OBJECT_METADATA[]> pMetaData;
		public RpcPointer<string> wszLocalUserAuthorizationList;
		public RpcPointer<string> wszPackageId;
		public RpcPointer<string> wszLocalUserOwner;
		public uint dwTrustTupleKeywords;
		public FW_NETWORK_NAMES OnNetworkNames;
		public RpcPointer<string> wszSecurityRealmId;
		public ushort wFlags2;
		public FW_NETWORK_NAMES RemoteOutServerNames;
		public RpcPointer<string> wszFqbn;
		public uint compartmentId;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteEnumShortValue((short)this.Direction);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteUnion(this.unnamed_1);
			encoder.WriteFixedStruct(this.LocalAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemoteAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalInterfaceIds, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwLocalInterfaceTypes);
			encoder.WriteUniquePointer(this.wszLocalApplication);
			encoder.WriteUniquePointer(this.wszLocalService);
			encoder.WriteEnumShortValue((short)this.Action);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszRemoteMachineAuthorizationList);
			encoder.WriteUniquePointer(this.wszRemoteUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteValue((int)this.Status);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue(this.Reserved);
			encoder.WriteUniquePointer(this.pMetaData);
			encoder.WriteUniquePointer(this.wszLocalUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszPackageId);
			encoder.WriteUniquePointer(this.wszLocalUserOwner);
			encoder.WriteValue(this.dwTrustTupleKeywords);
			encoder.WriteFixedStruct(this.OnNetworkNames, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.wszSecurityRealmId);
			encoder.WriteValue(this.wFlags2);
			encoder.WriteFixedStruct(this.RemoteOutServerNames, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.wszFqbn);
			encoder.WriteValue(this.compartmentId);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_RULE2_27>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Direction = (FW_DIRECTION)decoder.ReadEnumShortValue();
			this.wIpProtocol = decoder.ReadUInt16();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_13>();
			this.LocalAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.RemoteAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.LocalInterfaceIds = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
			this.dwLocalInterfaceTypes = decoder.ReadUInt32();
			this.wszLocalApplication = decoder.ReadUniquePointer<string>();
			this.wszLocalService = decoder.ReadUniquePointer<string>();
			this.Action = (FW_RULE_ACTION)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.wszRemoteMachineAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszRemoteUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Reserved = decoder.ReadUInt32();
			this.pMetaData = decoder.ReadUniquePointer<FW_OBJECT_METADATA[]>();
			this.wszLocalUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszPackageId = decoder.ReadUniquePointer<string>();
			this.wszLocalUserOwner = decoder.ReadUniquePointer<string>();
			this.dwTrustTupleKeywords = decoder.ReadUInt32();
			this.OnNetworkNames = decoder.ReadFixedStruct<FW_NETWORK_NAMES>(NdrAlignment.NativePtr);
			this.wszSecurityRealmId = decoder.ReadUniquePointer<string>();
			this.wFlags2 = decoder.ReadUInt16();
			this.RemoteOutServerNames = decoder.ReadFixedStruct<FW_NETWORK_NAMES>(NdrAlignment.NativePtr);
			this.wszFqbn = decoder.ReadUniquePointer<string>();
			this.compartmentId = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.unnamed_1);
			encoder.WriteStructDeferral(this.LocalAddresses);
			encoder.WriteStructDeferral(this.RemoteAddresses);
			encoder.WriteStructDeferral(this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				encoder.WriteWideCharString(this.wszLocalApplication.value);
			}

			if (this.wszLocalService is not null)
			{
				encoder.WriteWideCharString(this.wszLocalService.value);
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteMachineAuthorizationList.value);
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteUserAuthorizationList.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}

			if (this.pMetaData is not null)
			{
				encoder.WriteArrayHeader(this.pMetaData.value);
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserAuthorizationList.value);
			}

			if (this.wszPackageId is not null)
			{
				encoder.WriteWideCharString(this.wszPackageId.value);
			}

			if (this.wszLocalUserOwner is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserOwner.value);
			}

			encoder.WriteStructDeferral(this.OnNetworkNames);
			if (this.wszSecurityRealmId is not null)
			{
				encoder.WriteWideCharString(this.wszSecurityRealmId.value);
			}

			encoder.WriteStructDeferral(this.RemoteOutServerNames);
			if (this.wszFqbn is not null)
			{
				encoder.WriteWideCharString(this.wszFqbn.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_RULE2_27>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_27>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<Unnamed_13>(ref this.unnamed_1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.LocalAddresses);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.RemoteAddresses);
			decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				this.wszLocalApplication.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalService is not null)
			{
				this.wszLocalService.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				this.wszRemoteMachineAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				this.wszRemoteUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}

			if (this.pMetaData is not null)
			{
				this.pMetaData.value = decoder.ReadArrayHeader<FW_OBJECT_METADATA>();
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_OBJECT_METADATA>(NdrAlignment._8Byte);
					this.pMetaData.value[i] = elem_0;
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					decoder.ReadStructDeferral<FW_OBJECT_METADATA>(ref elem_0);
					this.pMetaData.value[i] = elem_0;
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				this.wszLocalUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszPackageId is not null)
			{
				this.wszPackageId.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalUserOwner is not null)
			{
				this.wszLocalUserOwner.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_NETWORK_NAMES>(ref this.OnNetworkNames);
			if (this.wszSecurityRealmId is not null)
			{
				this.wszSecurityRealmId.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_NETWORK_NAMES>(ref this.RemoteOutServerNames);
			if (this.wszFqbn is not null)
			{
				this.wszFqbn.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_16 : IRpcFixedStruct
	{
		public FW_PORTS LocalPorts;
		public FW_PORTS RemotePorts;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LocalPorts, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemotePorts, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LocalPorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.RemotePorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LocalPorts);
			encoder.WriteStructDeferral(this.RemotePorts);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_PORTS>(ref this.LocalPorts);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.RemotePorts);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_15 : IRpcFixedStruct
	{
		public ushort wIpProtocol;
		public Unnamed_16 __unnamed_0;
		public FW_ICMP_TYPE_CODE_LIST V4TypeCodeList;
		public FW_ICMP_TYPE_CODE_LIST V6TypeCodeList;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.wIpProtocol);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 1:
					encoder.WriteFixedStruct(this.V4TypeCodeList, NdrAlignment.NativePtr);
					break;
				case 58:
					encoder.WriteFixedStruct(this.V6TypeCodeList, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.wIpProtocol = decoder.ReadUInt16();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_16>(NdrAlignment.NativePtr);
					break;
				case 1:
					this.V4TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
				case 58:
					this.V6TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 1:
					encoder.WriteStructDeferral(this.V4TypeCodeList);
					break;
				case 58:
					encoder.WriteStructDeferral(this.V6TypeCodeList);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					decoder.ReadStructDeferral<Unnamed_16>(ref this.__unnamed_0);
					break;
				case 1:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V4TypeCodeList);
					break;
				case 58:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V6TypeCodeList);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_RULE2_31 : IRpcFixedStruct
	{
		public RpcPointer<FW_RULE2_31> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_DIRECTION Direction;
		public ushort wIpProtocol;
		public Unnamed_15 unnamed_1;
		public FW_ADDRESSES LocalAddresses;
		public FW_ADDRESSES RemoteAddresses;
		public FW_INTERFACE_LUIDS LocalInterfaceIds;
		public uint dwLocalInterfaceTypes;
		public RpcPointer<string> wszLocalApplication;
		public RpcPointer<string> wszLocalService;
		public FW_RULE_ACTION Action;
		public ushort wFlags;
		public RpcPointer<string> wszRemoteMachineAuthorizationList;
		public RpcPointer<string> wszRemoteUserAuthorizationList;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_STATUS Status;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public uint Reserved;
		public RpcPointer<FW_OBJECT_METADATA[]> pMetaData;
		public RpcPointer<string> wszLocalUserAuthorizationList;
		public RpcPointer<string> wszPackageId;
		public RpcPointer<string> wszLocalUserOwner;
		public uint dwTrustTupleKeywords;
		public FW_NETWORK_NAMES OnNetworkNames;
		public RpcPointer<string> wszSecurityRealmId;
		public ushort wFlags2;
		public FW_NETWORK_NAMES RemoteOutServerNames;
		public RpcPointer<string> wszFqbn;
		public uint compartmentId;
		public Guid providerContextKey;
		public FW_DYNAMIC_KEYWORD_ADDRESS_ID_LIST RemoteDynamicKeywordAddresses;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteEnumShortValue((short)this.Direction);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteUnion(this.unnamed_1);
			encoder.WriteFixedStruct(this.LocalAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemoteAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalInterfaceIds, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwLocalInterfaceTypes);
			encoder.WriteUniquePointer(this.wszLocalApplication);
			encoder.WriteUniquePointer(this.wszLocalService);
			encoder.WriteEnumShortValue((short)this.Action);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszRemoteMachineAuthorizationList);
			encoder.WriteUniquePointer(this.wszRemoteUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteValue((int)this.Status);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue(this.Reserved);
			encoder.WriteUniquePointer(this.pMetaData);
			encoder.WriteUniquePointer(this.wszLocalUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszPackageId);
			encoder.WriteUniquePointer(this.wszLocalUserOwner);
			encoder.WriteValue(this.dwTrustTupleKeywords);
			encoder.WriteFixedStruct(this.OnNetworkNames, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.wszSecurityRealmId);
			encoder.WriteValue(this.wFlags2);
			encoder.WriteFixedStruct(this.RemoteOutServerNames, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.wszFqbn);
			encoder.WriteValue(this.compartmentId);
			encoder.WriteValue(this.providerContextKey);
			encoder.WriteFixedStruct(this.RemoteDynamicKeywordAddresses, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_RULE2_31>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Direction = (FW_DIRECTION)decoder.ReadEnumShortValue();
			this.wIpProtocol = decoder.ReadUInt16();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_15>();
			this.LocalAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.RemoteAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.LocalInterfaceIds = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
			this.dwLocalInterfaceTypes = decoder.ReadUInt32();
			this.wszLocalApplication = decoder.ReadUniquePointer<string>();
			this.wszLocalService = decoder.ReadUniquePointer<string>();
			this.Action = (FW_RULE_ACTION)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.wszRemoteMachineAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszRemoteUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Reserved = decoder.ReadUInt32();
			this.pMetaData = decoder.ReadUniquePointer<FW_OBJECT_METADATA[]>();
			this.wszLocalUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszPackageId = decoder.ReadUniquePointer<string>();
			this.wszLocalUserOwner = decoder.ReadUniquePointer<string>();
			this.dwTrustTupleKeywords = decoder.ReadUInt32();
			this.OnNetworkNames = decoder.ReadFixedStruct<FW_NETWORK_NAMES>(NdrAlignment.NativePtr);
			this.wszSecurityRealmId = decoder.ReadUniquePointer<string>();
			this.wFlags2 = decoder.ReadUInt16();
			this.RemoteOutServerNames = decoder.ReadFixedStruct<FW_NETWORK_NAMES>(NdrAlignment.NativePtr);
			this.wszFqbn = decoder.ReadUniquePointer<string>();
			this.compartmentId = decoder.ReadUInt32();
			this.providerContextKey = decoder.ReadUuid();
			this.RemoteDynamicKeywordAddresses = decoder.ReadFixedStruct<FW_DYNAMIC_KEYWORD_ADDRESS_ID_LIST>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.unnamed_1);
			encoder.WriteStructDeferral(this.LocalAddresses);
			encoder.WriteStructDeferral(this.RemoteAddresses);
			encoder.WriteStructDeferral(this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				encoder.WriteWideCharString(this.wszLocalApplication.value);
			}

			if (this.wszLocalService is not null)
			{
				encoder.WriteWideCharString(this.wszLocalService.value);
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteMachineAuthorizationList.value);
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteUserAuthorizationList.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}

			if (this.pMetaData is not null)
			{
				encoder.WriteArrayHeader(this.pMetaData.value);
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserAuthorizationList.value);
			}

			if (this.wszPackageId is not null)
			{
				encoder.WriteWideCharString(this.wszPackageId.value);
			}

			if (this.wszLocalUserOwner is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserOwner.value);
			}

			encoder.WriteStructDeferral(this.OnNetworkNames);
			if (this.wszSecurityRealmId is not null)
			{
				encoder.WriteWideCharString(this.wszSecurityRealmId.value);
			}

			encoder.WriteStructDeferral(this.RemoteOutServerNames);
			if (this.wszFqbn is not null)
			{
				encoder.WriteWideCharString(this.wszFqbn.value);
			}

			encoder.WriteStructDeferral(this.RemoteDynamicKeywordAddresses);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_RULE2_31>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_31>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<Unnamed_15>(ref this.unnamed_1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.LocalAddresses);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.RemoteAddresses);
			decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				this.wszLocalApplication.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalService is not null)
			{
				this.wszLocalService.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				this.wszRemoteMachineAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				this.wszRemoteUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}

			if (this.pMetaData is not null)
			{
				this.pMetaData.value = decoder.ReadArrayHeader<FW_OBJECT_METADATA>();
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_OBJECT_METADATA>(NdrAlignment._8Byte);
					this.pMetaData.value[i] = elem_0;
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					decoder.ReadStructDeferral<FW_OBJECT_METADATA>(ref elem_0);
					this.pMetaData.value[i] = elem_0;
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				this.wszLocalUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszPackageId is not null)
			{
				this.wszPackageId.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalUserOwner is not null)
			{
				this.wszLocalUserOwner.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_NETWORK_NAMES>(ref this.OnNetworkNames);
			if (this.wszSecurityRealmId is not null)
			{
				this.wszSecurityRealmId.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_NETWORK_NAMES>(ref this.RemoteOutServerNames);
			if (this.wszFqbn is not null)
			{
				this.wszFqbn.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_DYNAMIC_KEYWORD_ADDRESS_ID_LIST>(ref this.RemoteDynamicKeywordAddresses);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_18 : IRpcFixedStruct
	{
		public FW_PORTS LocalPorts;
		public FW_PORTS RemotePorts;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LocalPorts, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemotePorts, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LocalPorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.RemotePorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LocalPorts);
			encoder.WriteStructDeferral(this.RemotePorts);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_PORTS>(ref this.LocalPorts);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.RemotePorts);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_17 : IRpcFixedStruct
	{
		public ushort wIpProtocol;
		public Unnamed_18 __unnamed_0;
		public FW_ICMP_TYPE_CODE_LIST V4TypeCodeList;
		public FW_ICMP_TYPE_CODE_LIST V6TypeCodeList;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.wIpProtocol);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 1:
					encoder.WriteFixedStruct(this.V4TypeCodeList, NdrAlignment.NativePtr);
					break;
				case 58:
					encoder.WriteFixedStruct(this.V6TypeCodeList, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.wIpProtocol = decoder.ReadUInt16();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_18>(NdrAlignment.NativePtr);
					break;
				case 1:
					this.V4TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
				case 58:
					this.V6TypeCodeList = decoder.ReadFixedStruct<FW_ICMP_TYPE_CODE_LIST>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 1:
					encoder.WriteStructDeferral(this.V4TypeCodeList);
					break;
				case 58:
					encoder.WriteStructDeferral(this.V6TypeCodeList);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.wIpProtocol)
			{
				case 6:
				case 17:
					decoder.ReadStructDeferral<Unnamed_18>(ref this.__unnamed_0);
					break;
				case 1:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V4TypeCodeList);
					break;
				case 58:
					decoder.ReadStructDeferral<FW_ICMP_TYPE_CODE_LIST>(ref this.V6TypeCodeList);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_RULE : IRpcFixedStruct
	{
		public RpcPointer<FW_RULE> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_DIRECTION Direction;
		public ushort wIpProtocol;
		public Unnamed_17 unnamed_1;
		public FW_ADDRESSES LocalAddresses;
		public FW_ADDRESSES RemoteAddresses;
		public FW_INTERFACE_LUIDS LocalInterfaceIds;
		public uint dwLocalInterfaceTypes;
		public RpcPointer<string> wszLocalApplication;
		public RpcPointer<string> wszLocalService;
		public FW_RULE_ACTION Action;
		public ushort wFlags;
		public RpcPointer<string> wszRemoteMachineAuthorizationList;
		public RpcPointer<string> wszRemoteUserAuthorizationList;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_STATUS Status;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public uint Reserved;
		public RpcPointer<FW_OBJECT_METADATA[]> pMetaData;
		public RpcPointer<string> wszLocalUserAuthorizationList;
		public RpcPointer<string> wszPackageId;
		public RpcPointer<string> wszLocalUserOwner;
		public uint dwTrustTupleKeywords;
		public FW_NETWORK_NAMES OnNetworkNames;
		public RpcPointer<string> wszSecurityRealmId;
		public ushort wFlags2;
		public FW_NETWORK_NAMES RemoteOutServerNames;
		public RpcPointer<string> wszFqbn;
		public uint compartmentId;
		public Guid providerContextKey;
		public FW_DYNAMIC_KEYWORD_ADDRESS_ID_LIST RemoteDynamicKeywordAddresses;
		public RpcPointer<string> wszPackageFamilyName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteEnumShortValue((short)this.Direction);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteUnion(this.unnamed_1);
			encoder.WriteFixedStruct(this.LocalAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.RemoteAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalInterfaceIds, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwLocalInterfaceTypes);
			encoder.WriteUniquePointer(this.wszLocalApplication);
			encoder.WriteUniquePointer(this.wszLocalService);
			encoder.WriteEnumShortValue((short)this.Action);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszRemoteMachineAuthorizationList);
			encoder.WriteUniquePointer(this.wszRemoteUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteValue((int)this.Status);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue(this.Reserved);
			encoder.WriteUniquePointer(this.pMetaData);
			encoder.WriteUniquePointer(this.wszLocalUserAuthorizationList);
			encoder.WriteUniquePointer(this.wszPackageId);
			encoder.WriteUniquePointer(this.wszLocalUserOwner);
			encoder.WriteValue(this.dwTrustTupleKeywords);
			encoder.WriteFixedStruct(this.OnNetworkNames, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.wszSecurityRealmId);
			encoder.WriteValue(this.wFlags2);
			encoder.WriteFixedStruct(this.RemoteOutServerNames, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.wszFqbn);
			encoder.WriteValue(this.compartmentId);
			encoder.WriteValue(this.providerContextKey);
			encoder.WriteFixedStruct(this.RemoteDynamicKeywordAddresses, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.wszPackageFamilyName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_RULE>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Direction = (FW_DIRECTION)decoder.ReadEnumShortValue();
			this.wIpProtocol = decoder.ReadUInt16();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_17>();
			this.LocalAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.RemoteAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.LocalInterfaceIds = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
			this.dwLocalInterfaceTypes = decoder.ReadUInt32();
			this.wszLocalApplication = decoder.ReadUniquePointer<string>();
			this.wszLocalService = decoder.ReadUniquePointer<string>();
			this.Action = (FW_RULE_ACTION)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.wszRemoteMachineAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszRemoteUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Reserved = decoder.ReadUInt32();
			this.pMetaData = decoder.ReadUniquePointer<FW_OBJECT_METADATA[]>();
			this.wszLocalUserAuthorizationList = decoder.ReadUniquePointer<string>();
			this.wszPackageId = decoder.ReadUniquePointer<string>();
			this.wszLocalUserOwner = decoder.ReadUniquePointer<string>();
			this.dwTrustTupleKeywords = decoder.ReadUInt32();
			this.OnNetworkNames = decoder.ReadFixedStruct<FW_NETWORK_NAMES>(NdrAlignment.NativePtr);
			this.wszSecurityRealmId = decoder.ReadUniquePointer<string>();
			this.wFlags2 = decoder.ReadUInt16();
			this.RemoteOutServerNames = decoder.ReadFixedStruct<FW_NETWORK_NAMES>(NdrAlignment.NativePtr);
			this.wszFqbn = decoder.ReadUniquePointer<string>();
			this.compartmentId = decoder.ReadUInt32();
			this.providerContextKey = decoder.ReadUuid();
			this.RemoteDynamicKeywordAddresses = decoder.ReadFixedStruct<FW_DYNAMIC_KEYWORD_ADDRESS_ID_LIST>(NdrAlignment.NativePtr);
			this.wszPackageFamilyName = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.unnamed_1);
			encoder.WriteStructDeferral(this.LocalAddresses);
			encoder.WriteStructDeferral(this.RemoteAddresses);
			encoder.WriteStructDeferral(this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				encoder.WriteWideCharString(this.wszLocalApplication.value);
			}

			if (this.wszLocalService is not null)
			{
				encoder.WriteWideCharString(this.wszLocalService.value);
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteMachineAuthorizationList.value);
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteUserAuthorizationList.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}

			if (this.pMetaData is not null)
			{
				encoder.WriteArrayHeader(this.pMetaData.value);
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserAuthorizationList.value);
			}

			if (this.wszPackageId is not null)
			{
				encoder.WriteWideCharString(this.wszPackageId.value);
			}

			if (this.wszLocalUserOwner is not null)
			{
				encoder.WriteWideCharString(this.wszLocalUserOwner.value);
			}

			encoder.WriteStructDeferral(this.OnNetworkNames);
			if (this.wszSecurityRealmId is not null)
			{
				encoder.WriteWideCharString(this.wszSecurityRealmId.value);
			}

			encoder.WriteStructDeferral(this.RemoteOutServerNames);
			if (this.wszFqbn is not null)
			{
				encoder.WriteWideCharString(this.wszFqbn.value);
			}

			encoder.WriteStructDeferral(this.RemoteDynamicKeywordAddresses);
			if (this.wszPackageFamilyName is not null)
			{
				encoder.WriteWideCharString(this.wszPackageFamilyName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_RULE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<Unnamed_17>(ref this.unnamed_1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.LocalAddresses);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.RemoteAddresses);
			decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.LocalInterfaceIds);
			if (this.wszLocalApplication is not null)
			{
				this.wszLocalApplication.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalService is not null)
			{
				this.wszLocalService.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteMachineAuthorizationList is not null)
			{
				this.wszRemoteMachineAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszRemoteUserAuthorizationList is not null)
			{
				this.wszRemoteUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}

			if (this.pMetaData is not null)
			{
				this.pMetaData.value = decoder.ReadArrayHeader<FW_OBJECT_METADATA>();
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_OBJECT_METADATA>(NdrAlignment._8Byte);
					this.pMetaData.value[i] = elem_0;
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					decoder.ReadStructDeferral<FW_OBJECT_METADATA>(ref elem_0);
					this.pMetaData.value[i] = elem_0;
				}
			}

			if (this.wszLocalUserAuthorizationList is not null)
			{
				this.wszLocalUserAuthorizationList.value = decoder.ReadWideCharString();
			}

			if (this.wszPackageId is not null)
			{
				this.wszPackageId.value = decoder.ReadWideCharString();
			}

			if (this.wszLocalUserOwner is not null)
			{
				this.wszLocalUserOwner.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_NETWORK_NAMES>(ref this.OnNetworkNames);
			if (this.wszSecurityRealmId is not null)
			{
				this.wszSecurityRealmId.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_NETWORK_NAMES>(ref this.RemoteOutServerNames);
			if (this.wszFqbn is not null)
			{
				this.wszFqbn.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_DYNAMIC_KEYWORD_ADDRESS_ID_LIST>(ref this.RemoteDynamicKeywordAddresses);
			if (this.wszPackageFamilyName is not null)
			{
				this.wszPackageFamilyName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_PROFILE_CONFIG : int
	{
		FW_PROFILE_CONFIG_INVALID = 0,
		FW_PROFILE_CONFIG_ENABLE_FW = 1,
		FW_PROFILE_CONFIG_DISABLE_STEALTH_MODE = 2,
		FW_PROFILE_CONFIG_SHIELDED = 3,
		FW_PROFILE_CONFIG_DISABLE_UNICAST_RESPONSES_TO_MULTICAST_BROADCAST = 4,
		FW_PROFILE_CONFIG_LOG_DROPPED_PACKETS = 5,
		FW_PROFILE_CONFIG_LOG_SUCCESS_CONNECTIONS = 6,
		FW_PROFILE_CONFIG_LOG_IGNORED_RULES = 7,
		FW_PROFILE_CONFIG_LOG_MAX_FILE_SIZE = 8,
		FW_PROFILE_CONFIG_LOG_FILE_PATH = 9,
		FW_PROFILE_CONFIG_DISABLE_INBOUND_NOTIFICATIONS = 10,
		FW_PROFILE_CONFIG_AUTH_APPS_ALLOW_USER_PREF_MERGE = 11,
		FW_PROFILE_CONFIG_GLOBAL_PORTS_ALLOW_USER_PREF_MERGE = 12,
		FW_PROFILE_CONFIG_ALLOW_LOCAL_POLICY_MERGE = 13,
		FW_PROFILE_CONFIG_ALLOW_LOCAL_IPSEC_POLICY_MERGE = 14,
		FW_PROFILE_CONFIG_DISABLED_INTERFACES = 15,
		FW_PROFILE_CONFIG_DEFAULT_OUTBOUND_ACTION = 16,
		FW_PROFILE_CONFIG_DEFAULT_INBOUND_ACTION = 17,
		FW_PROFILE_CONFIG_DISABLE_STEALTH_MODE_IPSEC_SECURED_PACKET_EXEMPTION = 18,
		FW_PROFILE_CONFIG_MAX = 19
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_GLOBAL_CONFIG_IPSEC_EXEMPT_VALUES : int
	{
		FW_GLOBAL_CONFIG_IPSEC_EXEMPT_NONE = 0,
		FW_GLOBAL_CONFIG_IPSEC_EXEMPT_NEIGHBOR_DISC = 1,
		FW_GLOBAL_CONFIG_IPSEC_EXEMPT_ICMP = 2,
		FW_GLOBAL_CONFIG_IPSEC_EXEMPT_ROUTER_DISC = 4,
		FW_GLOBAL_CONFIG_IPSEC_EXEMPT_NEIGHBOR_DISC_RFC = 5,
		FW_GLOBAL_CONFIG_IPSEC_EXEMPT_DHCP = 8,
		FW_GLOBAL_CONFIG_IPSEC_EXEMPT_MAX = 16
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_GLOBAL_CONFIG_PRESHARED_KEY_ENCODING_VALUES : int
	{
		FW_GLOBAL_CONFIG_PRESHARED_KEY_ENCODING_NONE = 0,
		FW_GLOBAL_CONFIG_PRESHARED_KEY_ENCODING_UTF_8 = 1,
		FW_GLOBAL_CONFIG_PRESHARED_KEY_ENCODING_MAX = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_GLOBAL_CONFIG_IPSEC_THROUGH_NAT_VALUES : int
	{
		FW_GLOBAL_CONFIG_IPSEC_THROUGH_NAT_NEVER = 0,
		FW_GLOBAL_CONFIG_IPSEC_THROUGH_NAT_SERVER_BEHIND_NAT = 1,
		FW_GLOBAL_CONFIG_IPSEC_THROUGH_NAT_SERVER_AND_CLIENT_BEHIND_NAT = 2,
		FW_GLOBAL_CONFIG_IPSEC_THROUGH_NAT_MAX = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_GLOBAL_CONFIG_ENABLE_PACKET_QUEUE_FLAGS : int
	{
		FW_GLOBAL_CONFIG_PACKET_QUEUE_NONE = 0,
		FW_GLOBAL_CONFIG_PACKET_QUEUE_INBOUND = 1,
		FW_GLOBAL_CONFIG_PACKET_QUEUE_FORWARD = 2,
		FW_GLOBAL_CONFIG_PACKET_QUEUE_MAX = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_GLOBAL_CONFIG : int
	{
		FW_GLOBAL_CONFIG_INVALID = 0,
		FW_GLOBAL_CONFIG_POLICY_VERSION_SUPPORTED = 1,
		FW_GLOBAL_CONFIG_CURRENT_PROFILE = 2,
		FW_GLOBAL_CONFIG_DISABLE_STATEFUL_FTP = 3,
		FW_GLOBAL_CONFIG_DISABLE_STATEFUL_PPTP = 4,
		FW_GLOBAL_CONFIG_SA_IDLE_TIME = 5,
		FW_GLOBAL_CONFIG_PRESHARED_KEY_ENCODING = 6,
		FW_GLOBAL_CONFIG_IPSEC_EXEMPT = 7,
		FW_GLOBAL_CONFIG_CRL_CHECK = 8,
		FW_GLOBAL_CONFIG_IPSEC_THROUGH_NAT = 9,
		FW_GLOBAL_CONFIG_POLICY_VERSION = 10,
		FW_GLOBAL_CONFIG_BINARY_VERSION_SUPPORTED = 11,
		FW_GLOBAL_CONFIG_IPSEC_TUNNEL_REMOTE_MACHINE_AUTHORIZATION_LIST = 12,
		FW_GLOBAL_CONFIG_IPSEC_TUNNEL_REMOTE_USER_AUTHORIZATION_LIST = 13,
		FW_GLOBAL_CONFIG_OPPORTUNISTICALLY_MATCH_AUTH_SET_PER_KM = 14,
		FW_GLOBAL_CONFIG_IPSEC_TRANSPORT_REMOTE_MACHINE_AUTHORIZATION_LIST = 15,
		FW_GLOBAL_CONFIG_IPSEC_TRANSPORT_REMOTE_USER_AUTHORIZATION_LIST = 16,
		FW_GLOBAL_CONFIG_ENABLE_PACKET_QUEUE = 17,
		FW_GLOBAL_CONFIG_MAX = 18
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_CONFIG_FLAGS : int
	{
		FW_CONFIG_FLAG_RETURN_DEFAULT_IF_NOT_FOUND = 1
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_NETWORK : IRpcFixedStruct
	{
		public RpcPointer<string> pszName;
		public FW_PROFILE_TYPE ProfileType;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pszName);
			encoder.WriteValue((int)this.ProfileType);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pszName = decoder.ReadUniquePointer<string>();
			this.ProfileType = (FW_PROFILE_TYPE)decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszName is not null)
			{
				encoder.WriteWideCharString(this.pszName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszName is not null)
			{
				this.pszName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_ADAPTER : IRpcFixedStruct
	{
		public RpcPointer<string> pszFriendlyName;
		public Guid Guid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pszFriendlyName);
			encoder.WriteValue(this.Guid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pszFriendlyName = decoder.ReadUniquePointer<string>();
			this.Guid = decoder.ReadUuid();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszFriendlyName is not null)
			{
				encoder.WriteWideCharString(this.pszFriendlyName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszFriendlyName is not null)
			{
				this.pszFriendlyName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_DIAG_APP : IRpcFixedStruct
	{
		public RpcPointer<string> pszAppPath;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pszAppPath);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pszAppPath = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszAppPath is not null)
			{
				encoder.WriteWideCharString(this.pszAppPath.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszAppPath is not null)
			{
				this.pszAppPath.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_RULE_CATEGORY : int
	{
		FW_RULE_CATEGORY_BOOT = 0,
		FW_RULE_CATEGORY_STEALTH = 1,
		FW_RULE_CATEGORY_FIREWALL = 2,
		FW_RULE_CATEGORY_CONSEC = 3,
		FW_RULE_CATEGORY_MAX = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_PRODUCT : IRpcFixedStruct
	{
		public uint dwFlags;
		public uint dwNumRuleCategories;
		public RpcPointer<FW_RULE_CATEGORY[]> pRuleCategories;
		public RpcPointer<string> pszDisplayName;
		public RpcPointer<string> pszPathToSignedProductExe;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwFlags);
			encoder.WriteValue(this.dwNumRuleCategories);
			encoder.WriteUniquePointer(this.pRuleCategories);
			encoder.WriteUniquePointer(this.pszDisplayName);
			encoder.WriteUniquePointer(this.pszPathToSignedProductExe);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwFlags = decoder.ReadUInt32();
			this.dwNumRuleCategories = decoder.ReadUInt32();
			this.pRuleCategories = decoder.ReadUniquePointer<FW_RULE_CATEGORY[]>();
			this.pszDisplayName = decoder.ReadUniquePointer<string>();
			this.pszPathToSignedProductExe = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pRuleCategories is not null)
			{
				encoder.WriteArrayHeader(this.pRuleCategories.value);
				for (int i = 0; i < this.pRuleCategories.value.Length; i++)
				{
					FW_RULE_CATEGORY elem_0 = this.pRuleCategories.value[i];
					encoder.WriteValue((int)elem_0);
				}
			}

			if (this.pszDisplayName is not null)
			{
				encoder.WriteWideCharString(this.pszDisplayName.value);
			}

			if (this.pszPathToSignedProductExe is not null)
			{
				encoder.WriteWideCharString(this.pszPathToSignedProductExe.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pRuleCategories is not null)
			{
				this.pRuleCategories.value = decoder.ReadArrayHeader<FW_RULE_CATEGORY>();
				for (int i = 0; i < this.pRuleCategories.value.Length; i++)
				{
					FW_RULE_CATEGORY elem_0 = this.pRuleCategories.value[i];
					elem_0 = (FW_RULE_CATEGORY)decoder.ReadInt32();
					this.pRuleCategories.value[i] = elem_0;
				}
			}

			if (this.pszDisplayName is not null)
			{
				this.pszDisplayName.value = decoder.ReadWideCharString();
			}

			if (this.pszPathToSignedProductExe is not null)
			{
				this.pszPathToSignedProductExe.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_IP_VERSION : int
	{
		FW_IP_VERSION_INVALID = 0,
		FW_IP_VERSION_V4 = 1,
		FW_IP_VERSION_V6 = 2,
		FW_IP_VERSION_MAX = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_IPSEC_PHASE : int
	{
		FW_IPSEC_PHASE_INVALID = 0,
		FW_IPSEC_PHASE_1 = 1,
		FW_IPSEC_PHASE_2 = 2,
		FW_IPSEC_PHASE_MAX = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_CS_RULE_FLAGS : int
	{
		FW_CS_RULE_FLAGS_NONE = 0,
		FW_CS_RULE_FLAGS_ACTIVE = 1,
		FW_CS_RULE_FLAGS_DTM = 2,
		FW_CS_RULE_FLAGS_TUNNEL_BYPASS_IF_ENCRYPTED = 8,
		FW_CS_RULE_FLAGS_OUTBOUND_CLEAR = 16,
		FW_CS_RULE_FLAGS_APPLY_AUTHZ = 32,
		FW_CS_RULE_FLAGS_KEY_MANAGER_ALLOW_DICTATE_KEY = 64,
		FW_CS_RULE_FLAGS_KEY_MANAGER_ALLOW_NOTIFY_KEY = 128,
		FW_CS_RULE_FLAGS_SECURITY_REALM = 256,
		FW_CS_RULE_FLAGS_TUNNEL_TYPE_POINT_TO_SITE = 512,
		FW_CS_RULE_FLAGS_MAX = 1024,
		FW_CS_RULE_FLAGS_MAX_V2_1 = 2,
		FW_CS_RULE_FLAGS_MAX_V2_8 = 4,
		FW_CS_RULE_FLAGS_MAX_V2_10 = 64,
		FW_CS_RULE_FLAGS_MAX_V2_20 = 256
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_CS_RULE_ACTION : int
	{
		FW_CS_RULE_ACTION_INVALID = 0,
		FW_CS_RULE_ACTION_SECURE_SERVER = 1,
		FW_CS_RULE_ACTION_BOUNDARY = 2,
		FW_CS_RULE_ACTION_SECURE = 3,
		FW_CS_RULE_ACTION_DO_NOT_SECURE = 4,
		FW_CS_RULE_ACTION_MAX = 5
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_CS_RULE2_0 : IRpcFixedStruct
	{
		public RpcPointer<FW_CS_RULE2_0> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_ADDRESSES Endpoint1;
		public FW_ADDRESSES Endpoint2;
		public FW_INTERFACE_LUIDS LocalInterfaceIds;
		public uint dwLocalInterfaceTypes;
		public uint dwLocalTunnelEndpointV4;
		public byte[] LocalTunnelEndpointV6;
		public uint dwRemoteTunnelEndpointV4;
		public byte[] RemoteTunnelEndpointV6;
		public FW_PORTS Endpoint1Ports;
		public FW_PORTS Endpoint2Ports;
		public ushort wIpProtocol;
		public RpcPointer<string> wszPhase1AuthSet;
		public RpcPointer<string> wszPhase2CryptoSet;
		public RpcPointer<string> wszPhase2AuthSet;
		public FW_CS_RULE_ACTION Action;
		public ushort wFlags;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public FW_RULE_STATUS Status;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteFixedStruct(this.Endpoint1, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.Endpoint2, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalInterfaceIds, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwLocalInterfaceTypes);
			encoder.WriteValue(this.dwLocalTunnelEndpointV4);
			if (this.LocalTunnelEndpointV6 == null)
				this.LocalTunnelEndpointV6 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.LocalTunnelEndpointV6[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(this.dwRemoteTunnelEndpointV4);
			if (this.RemoteTunnelEndpointV6 == null)
				this.RemoteTunnelEndpointV6 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.RemoteTunnelEndpointV6[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteFixedStruct(this.Endpoint1Ports, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.Endpoint2Ports, NdrAlignment.NativePtr);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteUniquePointer(this.wszPhase1AuthSet);
			encoder.WriteUniquePointer(this.wszPhase2CryptoSet);
			encoder.WriteUniquePointer(this.wszPhase2AuthSet);
			encoder.WriteEnumShortValue((short)this.Action);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue((int)this.Status);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_CS_RULE2_0>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Endpoint1 = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.Endpoint2 = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.LocalInterfaceIds = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
			this.dwLocalInterfaceTypes = decoder.ReadUInt32();
			this.dwLocalTunnelEndpointV4 = decoder.ReadUInt32();
			if (this.LocalTunnelEndpointV6 == null)
				this.LocalTunnelEndpointV6 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.LocalTunnelEndpointV6[i];
				elem_0 = decoder.ReadByte();
				this.LocalTunnelEndpointV6[i] = elem_0;
			}

			this.dwRemoteTunnelEndpointV4 = decoder.ReadUInt32();
			if (this.RemoteTunnelEndpointV6 == null)
				this.RemoteTunnelEndpointV6 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.RemoteTunnelEndpointV6[i];
				elem_0 = decoder.ReadByte();
				this.RemoteTunnelEndpointV6[i] = elem_0;
			}

			this.Endpoint1Ports = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.Endpoint2Ports = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.wIpProtocol = decoder.ReadUInt16();
			this.wszPhase1AuthSet = decoder.ReadUniquePointer<string>();
			this.wszPhase2CryptoSet = decoder.ReadUniquePointer<string>();
			this.wszPhase2AuthSet = decoder.ReadUniquePointer<string>();
			this.Action = (FW_CS_RULE_ACTION)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.Endpoint1);
			encoder.WriteStructDeferral(this.Endpoint2);
			encoder.WriteStructDeferral(this.LocalInterfaceIds);
			encoder.WriteStructDeferral(this.Endpoint1Ports);
			encoder.WriteStructDeferral(this.Endpoint2Ports);
			if (this.wszPhase1AuthSet is not null)
			{
				encoder.WriteWideCharString(this.wszPhase1AuthSet.value);
			}

			if (this.wszPhase2CryptoSet is not null)
			{
				encoder.WriteWideCharString(this.wszPhase2CryptoSet.value);
			}

			if (this.wszPhase2AuthSet is not null)
			{
				encoder.WriteWideCharString(this.wszPhase2AuthSet.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_CS_RULE2_0>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CS_RULE2_0>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.Endpoint1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.Endpoint2);
			decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.LocalInterfaceIds);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.Endpoint1Ports);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.Endpoint2Ports);
			if (this.wszPhase1AuthSet is not null)
			{
				this.wszPhase1AuthSet.value = decoder.ReadWideCharString();
			}

			if (this.wszPhase2CryptoSet is not null)
			{
				this.wszPhase2CryptoSet.value = decoder.ReadWideCharString();
			}

			if (this.wszPhase2AuthSet is not null)
			{
				this.wszPhase2AuthSet.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_KEY_MODULE : int
	{
		FW_KEY_MODULE_DEFAULT = 0,
		FW_KEY_MODULE_IKEv1 = 1,
		FW_KEY_MODULE_AUTHIP = 2,
		FW_KEY_MODULE_IKEv2 = 4,
		FW_KEY_MODULE_MAX = 8
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_CS_RULE2_10 : IRpcFixedStruct
	{
		public RpcPointer<FW_CS_RULE2_10> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_ADDRESSES Endpoint1;
		public FW_ADDRESSES Endpoint2;
		public FW_INTERFACE_LUIDS LocalInterfaceIds;
		public uint dwLocalInterfaceTypes;
		public uint dwLocalTunnelEndpointV4;
		public byte[] LocalTunnelEndpointV6;
		public uint dwRemoteTunnelEndpointV4;
		public byte[] RemoteTunnelEndpointV6;
		public FW_PORTS Endpoint1Ports;
		public FW_PORTS Endpoint2Ports;
		public ushort wIpProtocol;
		public RpcPointer<string> wszPhase1AuthSet;
		public RpcPointer<string> wszPhase2CryptoSet;
		public RpcPointer<string> wszPhase2AuthSet;
		public FW_CS_RULE_ACTION Action;
		public ushort wFlags;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public FW_RULE_STATUS Status;
		public RpcPointer<string> wszMMParentRuleId;
		public uint Reserved;
		public RpcPointer<FW_OBJECT_METADATA[]> pMetaData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteFixedStruct(this.Endpoint1, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.Endpoint2, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalInterfaceIds, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwLocalInterfaceTypes);
			encoder.WriteValue(this.dwLocalTunnelEndpointV4);
			if (this.LocalTunnelEndpointV6 == null)
				this.LocalTunnelEndpointV6 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.LocalTunnelEndpointV6[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(this.dwRemoteTunnelEndpointV4);
			if (this.RemoteTunnelEndpointV6 == null)
				this.RemoteTunnelEndpointV6 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.RemoteTunnelEndpointV6[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteFixedStruct(this.Endpoint1Ports, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.Endpoint2Ports, NdrAlignment.NativePtr);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteUniquePointer(this.wszPhase1AuthSet);
			encoder.WriteUniquePointer(this.wszPhase2CryptoSet);
			encoder.WriteUniquePointer(this.wszPhase2AuthSet);
			encoder.WriteEnumShortValue((short)this.Action);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue((int)this.Status);
			encoder.WriteUniquePointer(this.wszMMParentRuleId);
			encoder.WriteValue(this.Reserved);
			encoder.WriteUniquePointer(this.pMetaData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_CS_RULE2_10>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Endpoint1 = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.Endpoint2 = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.LocalInterfaceIds = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
			this.dwLocalInterfaceTypes = decoder.ReadUInt32();
			this.dwLocalTunnelEndpointV4 = decoder.ReadUInt32();
			if (this.LocalTunnelEndpointV6 == null)
				this.LocalTunnelEndpointV6 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.LocalTunnelEndpointV6[i];
				elem_0 = decoder.ReadByte();
				this.LocalTunnelEndpointV6[i] = elem_0;
			}

			this.dwRemoteTunnelEndpointV4 = decoder.ReadUInt32();
			if (this.RemoteTunnelEndpointV6 == null)
				this.RemoteTunnelEndpointV6 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.RemoteTunnelEndpointV6[i];
				elem_0 = decoder.ReadByte();
				this.RemoteTunnelEndpointV6[i] = elem_0;
			}

			this.Endpoint1Ports = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.Endpoint2Ports = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.wIpProtocol = decoder.ReadUInt16();
			this.wszPhase1AuthSet = decoder.ReadUniquePointer<string>();
			this.wszPhase2CryptoSet = decoder.ReadUniquePointer<string>();
			this.wszPhase2AuthSet = decoder.ReadUniquePointer<string>();
			this.Action = (FW_CS_RULE_ACTION)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.wszMMParentRuleId = decoder.ReadUniquePointer<string>();
			this.Reserved = decoder.ReadUInt32();
			this.pMetaData = decoder.ReadUniquePointer<FW_OBJECT_METADATA[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.Endpoint1);
			encoder.WriteStructDeferral(this.Endpoint2);
			encoder.WriteStructDeferral(this.LocalInterfaceIds);
			encoder.WriteStructDeferral(this.Endpoint1Ports);
			encoder.WriteStructDeferral(this.Endpoint2Ports);
			if (this.wszPhase1AuthSet is not null)
			{
				encoder.WriteWideCharString(this.wszPhase1AuthSet.value);
			}

			if (this.wszPhase2CryptoSet is not null)
			{
				encoder.WriteWideCharString(this.wszPhase2CryptoSet.value);
			}

			if (this.wszPhase2AuthSet is not null)
			{
				encoder.WriteWideCharString(this.wszPhase2AuthSet.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}

			if (this.wszMMParentRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszMMParentRuleId.value);
			}

			if (this.pMetaData is not null)
			{
				encoder.WriteArrayHeader(this.pMetaData.value);
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_CS_RULE2_10>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CS_RULE2_10>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.Endpoint1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.Endpoint2);
			decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.LocalInterfaceIds);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.Endpoint1Ports);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.Endpoint2Ports);
			if (this.wszPhase1AuthSet is not null)
			{
				this.wszPhase1AuthSet.value = decoder.ReadWideCharString();
			}

			if (this.wszPhase2CryptoSet is not null)
			{
				this.wszPhase2CryptoSet.value = decoder.ReadWideCharString();
			}

			if (this.wszPhase2AuthSet is not null)
			{
				this.wszPhase2AuthSet.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}

			if (this.wszMMParentRuleId is not null)
			{
				this.wszMMParentRuleId.value = decoder.ReadWideCharString();
			}

			if (this.pMetaData is not null)
			{
				this.pMetaData.value = decoder.ReadArrayHeader<FW_OBJECT_METADATA>();
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_OBJECT_METADATA>(NdrAlignment._8Byte);
					this.pMetaData.value[i] = elem_0;
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					decoder.ReadStructDeferral<FW_OBJECT_METADATA>(ref elem_0);
					this.pMetaData.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_CS_RULE : IRpcFixedStruct
	{
		public RpcPointer<FW_CS_RULE> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_ADDRESSES Endpoint1;
		public FW_ADDRESSES Endpoint2;
		public FW_INTERFACE_LUIDS LocalInterfaceIds;
		public uint dwLocalInterfaceTypes;
		public uint dwLocalTunnelEndpointV4;
		public byte[] LocalTunnelEndpointV6;
		public uint dwRemoteTunnelEndpointV4;
		public byte[] RemoteTunnelEndpointV6;
		public FW_PORTS Endpoint1Ports;
		public FW_PORTS Endpoint2Ports;
		public ushort wIpProtocol;
		public RpcPointer<string> wszPhase1AuthSet;
		public RpcPointer<string> wszPhase2CryptoSet;
		public RpcPointer<string> wszPhase2AuthSet;
		public FW_CS_RULE_ACTION Action;
		public ushort wFlags;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public FW_RULE_STATUS Status;
		public RpcPointer<string> wszMMParentRuleId;
		public uint Reserved;
		public RpcPointer<FW_OBJECT_METADATA[]> pMetaData;
		public RpcPointer<string> wszRemoteTunnelEndpointFqdn;
		public FW_ADDRESSES RemoteTunnelEndpoints;
		public uint dwKeyModules;
		public uint FwdPathSALifetime;
		public RpcPointer<string> wszTransportMachineAuthzSDDL;
		public RpcPointer<string> wszTransportUserAuthzSDDL;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteFixedStruct(this.Endpoint1, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.Endpoint2, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalInterfaceIds, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwLocalInterfaceTypes);
			encoder.WriteValue(this.dwLocalTunnelEndpointV4);
			if (this.LocalTunnelEndpointV6 == null)
				this.LocalTunnelEndpointV6 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.LocalTunnelEndpointV6[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(this.dwRemoteTunnelEndpointV4);
			if (this.RemoteTunnelEndpointV6 == null)
				this.RemoteTunnelEndpointV6 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.RemoteTunnelEndpointV6[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteFixedStruct(this.Endpoint1Ports, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.Endpoint2Ports, NdrAlignment.NativePtr);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteUniquePointer(this.wszPhase1AuthSet);
			encoder.WriteUniquePointer(this.wszPhase2CryptoSet);
			encoder.WriteUniquePointer(this.wszPhase2AuthSet);
			encoder.WriteEnumShortValue((short)this.Action);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue((int)this.Status);
			encoder.WriteUniquePointer(this.wszMMParentRuleId);
			encoder.WriteValue(this.Reserved);
			encoder.WriteUniquePointer(this.pMetaData);
			encoder.WriteUniquePointer(this.wszRemoteTunnelEndpointFqdn);
			encoder.WriteFixedStruct(this.RemoteTunnelEndpoints, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwKeyModules);
			encoder.WriteValue(this.FwdPathSALifetime);
			encoder.WriteUniquePointer(this.wszTransportMachineAuthzSDDL);
			encoder.WriteUniquePointer(this.wszTransportUserAuthzSDDL);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_CS_RULE>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Endpoint1 = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.Endpoint2 = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.LocalInterfaceIds = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
			this.dwLocalInterfaceTypes = decoder.ReadUInt32();
			this.dwLocalTunnelEndpointV4 = decoder.ReadUInt32();
			if (this.LocalTunnelEndpointV6 == null)
				this.LocalTunnelEndpointV6 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.LocalTunnelEndpointV6[i];
				elem_0 = decoder.ReadByte();
				this.LocalTunnelEndpointV6[i] = elem_0;
			}

			this.dwRemoteTunnelEndpointV4 = decoder.ReadUInt32();
			if (this.RemoteTunnelEndpointV6 == null)
				this.RemoteTunnelEndpointV6 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.RemoteTunnelEndpointV6[i];
				elem_0 = decoder.ReadByte();
				this.RemoteTunnelEndpointV6[i] = elem_0;
			}

			this.Endpoint1Ports = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.Endpoint2Ports = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.wIpProtocol = decoder.ReadUInt16();
			this.wszPhase1AuthSet = decoder.ReadUniquePointer<string>();
			this.wszPhase2CryptoSet = decoder.ReadUniquePointer<string>();
			this.wszPhase2AuthSet = decoder.ReadUniquePointer<string>();
			this.Action = (FW_CS_RULE_ACTION)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.wszMMParentRuleId = decoder.ReadUniquePointer<string>();
			this.Reserved = decoder.ReadUInt32();
			this.pMetaData = decoder.ReadUniquePointer<FW_OBJECT_METADATA[]>();
			this.wszRemoteTunnelEndpointFqdn = decoder.ReadUniquePointer<string>();
			this.RemoteTunnelEndpoints = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.dwKeyModules = decoder.ReadUInt32();
			this.FwdPathSALifetime = decoder.ReadUInt32();
			this.wszTransportMachineAuthzSDDL = decoder.ReadUniquePointer<string>();
			this.wszTransportUserAuthzSDDL = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.Endpoint1);
			encoder.WriteStructDeferral(this.Endpoint2);
			encoder.WriteStructDeferral(this.LocalInterfaceIds);
			encoder.WriteStructDeferral(this.Endpoint1Ports);
			encoder.WriteStructDeferral(this.Endpoint2Ports);
			if (this.wszPhase1AuthSet is not null)
			{
				encoder.WriteWideCharString(this.wszPhase1AuthSet.value);
			}

			if (this.wszPhase2CryptoSet is not null)
			{
				encoder.WriteWideCharString(this.wszPhase2CryptoSet.value);
			}

			if (this.wszPhase2AuthSet is not null)
			{
				encoder.WriteWideCharString(this.wszPhase2AuthSet.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}

			if (this.wszMMParentRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszMMParentRuleId.value);
			}

			if (this.pMetaData is not null)
			{
				encoder.WriteArrayHeader(this.pMetaData.value);
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			if (this.wszRemoteTunnelEndpointFqdn is not null)
			{
				encoder.WriteWideCharString(this.wszRemoteTunnelEndpointFqdn.value);
			}

			encoder.WriteStructDeferral(this.RemoteTunnelEndpoints);
			if (this.wszTransportMachineAuthzSDDL is not null)
			{
				encoder.WriteWideCharString(this.wszTransportMachineAuthzSDDL.value);
			}

			if (this.wszTransportUserAuthzSDDL is not null)
			{
				encoder.WriteWideCharString(this.wszTransportUserAuthzSDDL.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_CS_RULE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CS_RULE>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.Endpoint1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.Endpoint2);
			decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.LocalInterfaceIds);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.Endpoint1Ports);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.Endpoint2Ports);
			if (this.wszPhase1AuthSet is not null)
			{
				this.wszPhase1AuthSet.value = decoder.ReadWideCharString();
			}

			if (this.wszPhase2CryptoSet is not null)
			{
				this.wszPhase2CryptoSet.value = decoder.ReadWideCharString();
			}

			if (this.wszPhase2AuthSet is not null)
			{
				this.wszPhase2AuthSet.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}

			if (this.wszMMParentRuleId is not null)
			{
				this.wszMMParentRuleId.value = decoder.ReadWideCharString();
			}

			if (this.pMetaData is not null)
			{
				this.pMetaData.value = decoder.ReadArrayHeader<FW_OBJECT_METADATA>();
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_OBJECT_METADATA>(NdrAlignment._8Byte);
					this.pMetaData.value[i] = elem_0;
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					decoder.ReadStructDeferral<FW_OBJECT_METADATA>(ref elem_0);
					this.pMetaData.value[i] = elem_0;
				}
			}

			if (this.wszRemoteTunnelEndpointFqdn is not null)
			{
				this.wszRemoteTunnelEndpointFqdn.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.RemoteTunnelEndpoints);
			if (this.wszTransportMachineAuthzSDDL is not null)
			{
				this.wszTransportMachineAuthzSDDL.value = decoder.ReadWideCharString();
			}

			if (this.wszTransportUserAuthzSDDL is not null)
			{
				this.wszTransportUserAuthzSDDL.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_AUTH_METHOD : int
	{
		FW_AUTH_METHOD_INVALID = 0,
		FW_AUTH_METHOD_ANONYMOUS = 1,
		FW_AUTH_METHOD_MACHINE_KERB = 2,
		FW_AUTH_METHOD_MACHINE_SHKEY = 3,
		FW_AUTH_METHOD_MACHINE_NTLM = 4,
		FW_AUTH_METHOD_MACHINE_CERT = 5,
		FW_AUTH_METHOD_USER_KERB = 6,
		FW_AUTH_METHOD_USER_CERT = 7,
		FW_AUTH_METHOD_USER_NTLM = 8,
		FW_AUTH_METHOD_MACHINE_RESERVED = 9,
		FW_AUTH_METHOD_USER_RESERVED = 10,
		FW_AUTH_METHOD_MAX = 11,
		FW_AUTH_METHOD_MAX_2_10 = 9
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_AUTH_SUITE_FLAGS : int
	{
		FW_AUTH_SUITE_FLAGS_NONE = 0,
		FW_AUTH_SUITE_FLAGS_CERT_EXCLUDE_CA_NAME = 1,
		FW_AUTH_SUITE_FLAGS_HEALTH_CERT = 2,
		FW_AUTH_SUITE_FLAGS_PERFORM_CERT_ACCOUNT_MAPPING = 4,
		FW_AUTH_SUITE_FLAGS_CERT_SIGNING_ECDSA256 = 8,
		FW_AUTH_SUITE_FLAGS_CERT_SIGNING_ECDSA384 = 16,
		FW_AUTH_SUITE_FLAGS_MAX_V2_1 = 32,
		FW_AUTH_SUITE_FLAGS_INTERMEDIATE_CA = 32,
		FW_AUTH_SUITE_FLAGS_MAX_V2_10 = 64,
		FW_AUTH_SUITE_FLAGS_ALLOW_PROXY = 64,
		FW_AUTH_SUITE_FLAGS_MAX = 128
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_20 : IRpcFixedStruct
	{
		public RpcPointer<string> wszCAName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.wszCAName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wszCAName = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wszCAName is not null)
			{
				encoder.WriteWideCharString(this.wszCAName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wszCAName is not null)
			{
				this.wszCAName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_21 : IRpcFixedStruct
	{
		public RpcPointer<string> wszSHKey;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.wszSHKey);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wszSHKey = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wszSHKey is not null)
			{
				encoder.WriteWideCharString(this.wszSHKey.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wszSHKey is not null)
			{
				this.wszSHKey.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_19 : IRpcFixedStruct
	{
		public FW_AUTH_METHOD Method;
		public Unnamed_20 __unnamed_0;
		public Unnamed_21 __unnamed_1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.Method);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.Method)
			{
				case 5:
				case 7:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 3:
					encoder.WriteFixedStruct(this.__unnamed_1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.Method = (FW_AUTH_METHOD)decoder.ReadEnumShortValue();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.Method)
			{
				case 5:
				case 7:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_20>(NdrAlignment.NativePtr);
					break;
				case 3:
					this.__unnamed_1 = decoder.ReadFixedStruct<Unnamed_21>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.Method)
			{
				case 5:
				case 7:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 3:
					encoder.WriteStructDeferral(this.__unnamed_1);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.Method)
			{
				case 5:
				case 7:
					decoder.ReadStructDeferral<Unnamed_20>(ref this.__unnamed_0);
					break;
				case 3:
					decoder.ReadStructDeferral<Unnamed_21>(ref this.__unnamed_1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_AUTH_SUITE2_10 : IRpcFixedStruct
	{
		public FW_AUTH_METHOD Method;
		public ushort wFlags;
		public Unnamed_19 unnamed_1;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.Method);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUnion(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Method = (FW_AUTH_METHOD)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_19>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<Unnamed_19>(ref this.unnamed_1);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_CERT_CRITERIA_NAME_TYPE : int
	{
		FW_CERT_CRITERIA_NAME_NONE = 0,
		FW_CERT_CRITERIA_NAME_DNS = 1,
		FW_CERT_CRITERIA_NAME_UPN = 2,
		FW_CERT_CRITERIA_NAME_RFC822 = 3,
		FW_CERT_CRITERIA_NAME_CN = 4,
		FW_CERT_CRITERIA_NAME_OU = 5,
		FW_CERT_CRITERIA_NAME_O = 6,
		FW_CERT_CRITERIA_NAME_DC = 7,
		FW_CERT_CRITERIA_NAME_MAX = 8
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_CERT_CRITERIA_TYPE : int
	{
		FW_CERT_CRITERIA_TYPE_BOTH = 0,
		FW_CERT_CRITERIA_TYPE_SELECTION = 1,
		FW_CERT_CRITERIA_TYPE_VALIDATION = 2,
		FW_CERT_CRITERIA_TYPE_MAX = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_AUTH_CERT_CRITERIA_FLAGS : int
	{
		FW_AUTH_CERT_CRITERIA_FLAGS_NONE = 0,
		FW_AUTH_CERT_CRITERIA_FLAGS_FOLLOW_RENEWAL = 1,
		FW_AUTH_CERT_CRITERIA_FLAGS_MAX = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_CERT_CRITERIA : IRpcFixedStruct
	{
		public ushort wSchemaVersion;
		public ushort wFlags;
		public FW_CERT_CRITERIA_TYPE CertCriteriaType;
		public FW_CERT_CRITERIA_NAME_TYPE NameType;
		public RpcPointer<string> wszName;
		public uint dwNumEku;
		public RpcPointer<RpcPointer<string>[]> ppEku;
		public RpcPointer<string> wszHash;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteValue(this.wFlags);
			encoder.WriteEnumShortValue((short)this.CertCriteriaType);
			encoder.WriteEnumShortValue((short)this.NameType);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteValue(this.dwNumEku);
			encoder.WriteUniquePointer(this.ppEku);
			encoder.WriteUniquePointer(this.wszHash);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wFlags = decoder.ReadUInt16();
			this.CertCriteriaType = (FW_CERT_CRITERIA_TYPE)decoder.ReadEnumShortValue();
			this.NameType = (FW_CERT_CRITERIA_NAME_TYPE)decoder.ReadEnumShortValue();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.dwNumEku = decoder.ReadUInt32();
			this.ppEku = decoder.ReadUniquePointer<RpcPointer<string>[]>();
			this.wszHash = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.ppEku is not null)
			{
				encoder.WriteArrayHeader(this.ppEku.value);
				for (int i = 0; i < this.ppEku.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.ppEku.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.ppEku.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.ppEku.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteUnsignedCharString(elem_0.value);
					}
				}
			}

			if (this.wszHash is not null)
			{
				encoder.WriteWideCharString(this.wszHash.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.ppEku is not null)
			{
				this.ppEku.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < this.ppEku.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.ppEku.value[i];
					elem_0 = decoder.ReadUniquePointer<string>();
					this.ppEku.value[i] = elem_0;
				}

				for (int i = 0; i < this.ppEku.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.ppEku.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadUnsignedCharString();
					}

					this.ppEku.value[i] = elem_0;
				}
			}

			if (this.wszHash is not null)
			{
				this.wszHash.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_23 : IRpcFixedStruct
	{
		public RpcPointer<string> wszCAName;
		public RpcPointer<FW_CERT_CRITERIA> pCertCriteria;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.wszCAName);
			encoder.WriteUniquePointer(this.pCertCriteria);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wszCAName = decoder.ReadUniquePointer<string>();
			this.pCertCriteria = decoder.ReadUniquePointer<FW_CERT_CRITERIA>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wszCAName is not null)
			{
				encoder.WriteWideCharString(this.wszCAName.value);
			}

			if (this.pCertCriteria is not null)
			{
				encoder.WriteFixedStruct(this.pCertCriteria.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pCertCriteria.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wszCAName is not null)
			{
				this.wszCAName.value = decoder.ReadWideCharString();
			}

			if (this.pCertCriteria is not null)
			{
				this.pCertCriteria.value = decoder.ReadFixedStruct<FW_CERT_CRITERIA>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CERT_CRITERIA>(ref this.pCertCriteria.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_24 : IRpcFixedStruct
	{
		public RpcPointer<string> wszSHKey;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.wszSHKey);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wszSHKey = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wszSHKey is not null)
			{
				encoder.WriteWideCharString(this.wszSHKey.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wszSHKey is not null)
			{
				this.wszSHKey.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_25 : IRpcFixedStruct
	{
		public RpcPointer<string> wszProxyServer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.wszProxyServer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wszProxyServer = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wszProxyServer is not null)
			{
				encoder.WriteWideCharString(this.wszProxyServer.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wszProxyServer is not null)
			{
				this.wszProxyServer.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_22 : IRpcFixedStruct
	{
		public FW_AUTH_METHOD Method;
		public Unnamed_23 __unnamed_0;
		public Unnamed_24 __unnamed_1;
		public Unnamed_25 __unnamed_2;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.Method);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.Method)
			{
				case 5:
				case 7:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 3:
					encoder.WriteFixedStruct(this.__unnamed_1, NdrAlignment.NativePtr);
					break;
				case 2:
				case 6:
					encoder.WriteFixedStruct(this.__unnamed_2, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.Method = (FW_AUTH_METHOD)decoder.ReadEnumShortValue();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.Method)
			{
				case 5:
				case 7:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_23>(NdrAlignment.NativePtr);
					break;
				case 3:
					this.__unnamed_1 = decoder.ReadFixedStruct<Unnamed_24>(NdrAlignment.NativePtr);
					break;
				case 2:
				case 6:
					this.__unnamed_2 = decoder.ReadFixedStruct<Unnamed_25>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.Method)
			{
				case 5:
				case 7:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 3:
					encoder.WriteStructDeferral(this.__unnamed_1);
					break;
				case 2:
				case 6:
					encoder.WriteStructDeferral(this.__unnamed_2);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.Method)
			{
				case 5:
				case 7:
					decoder.ReadStructDeferral<Unnamed_23>(ref this.__unnamed_0);
					break;
				case 3:
					decoder.ReadStructDeferral<Unnamed_24>(ref this.__unnamed_1);
					break;
				case 2:
				case 6:
					decoder.ReadStructDeferral<Unnamed_25>(ref this.__unnamed_2);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_AUTH_SUITE : IRpcFixedStruct
	{
		public FW_AUTH_METHOD Method;
		public ushort wFlags;
		public Unnamed_22 unnamed_1;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.Method);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUnion(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Method = (FW_AUTH_METHOD)decoder.ReadEnumShortValue();
			this.wFlags = decoder.ReadUInt16();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_22>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<Unnamed_22>(ref this.unnamed_1);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_AUTH_SET_FLAGS : int
	{
		FW_AUTH_SET_FLAGS_NONE = 0,
		FW_AUTH_SET_FLAGS_MAX = 1
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_AUTH_SET2_10 : IRpcFixedStruct
	{
		public RpcPointer<FW_AUTH_SET2_10> pNext;
		public ushort wSchemaVersion;
		public FW_IPSEC_PHASE IpSecPhase;
		public RpcPointer<string> wszSetId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public RpcPointer<string> wszEmbeddedContext;
		public uint dwNumSuites;
		public RpcPointer<FW_AUTH_SUITE2_10[]> pSuites;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public FW_RULE_STATUS Status;
		public uint dwAuthSetFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteEnumShortValue((short)this.IpSecPhase);
			encoder.WriteUniquePointer(this.wszSetId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteValue(this.dwNumSuites);
			encoder.WriteUniquePointer(this.pSuites);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue((int)this.Status);
			encoder.WriteValue(this.dwAuthSetFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_AUTH_SET2_10>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			this.wszSetId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.dwNumSuites = decoder.ReadUInt32();
			this.pSuites = decoder.ReadUniquePointer<FW_AUTH_SUITE2_10[]>();
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.dwAuthSetFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszSetId is not null)
			{
				encoder.WriteWideCharString(this.wszSetId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			if (this.pSuites is not null)
			{
				encoder.WriteArrayHeader(this.pSuites.value);
				for (int i = 0; i < this.pSuites.value.Length; i++)
				{
					FW_AUTH_SUITE2_10 elem_0 = this.pSuites.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.pSuites.value.Length; i++)
				{
					FW_AUTH_SUITE2_10 elem_0 = this.pSuites.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_AUTH_SET2_10>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_AUTH_SET2_10>(ref this.pNext.value);
			}

			if (this.wszSetId is not null)
			{
				this.wszSetId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			if (this.pSuites is not null)
			{
				this.pSuites.value = decoder.ReadArrayHeader<FW_AUTH_SUITE2_10>();
				for (int i = 0; i < this.pSuites.value.Length; i++)
				{
					FW_AUTH_SUITE2_10 elem_0 = this.pSuites.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_AUTH_SUITE2_10>(NdrAlignment.NativePtr);
					this.pSuites.value[i] = elem_0;
				}

				for (int i = 0; i < this.pSuites.value.Length; i++)
				{
					FW_AUTH_SUITE2_10 elem_0 = this.pSuites.value[i];
					decoder.ReadStructDeferral<FW_AUTH_SUITE2_10>(ref elem_0);
					this.pSuites.value[i] = elem_0;
				}
			}

			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_AUTH_SET : IRpcFixedStruct
	{
		public RpcPointer<FW_AUTH_SET> pNext;
		public ushort wSchemaVersion;
		public FW_IPSEC_PHASE IpSecPhase;
		public RpcPointer<string> wszSetId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public RpcPointer<string> wszEmbeddedContext;
		public uint dwNumSuites;
		public RpcPointer<FW_AUTH_SUITE[]> pSuites;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public FW_RULE_STATUS Status;
		public uint dwAuthSetFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteEnumShortValue((short)this.IpSecPhase);
			encoder.WriteUniquePointer(this.wszSetId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteValue(this.dwNumSuites);
			encoder.WriteUniquePointer(this.pSuites);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue((int)this.Status);
			encoder.WriteValue(this.dwAuthSetFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_AUTH_SET>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			this.wszSetId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.dwNumSuites = decoder.ReadUInt32();
			this.pSuites = decoder.ReadUniquePointer<FW_AUTH_SUITE[]>();
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.dwAuthSetFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszSetId is not null)
			{
				encoder.WriteWideCharString(this.wszSetId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			if (this.pSuites is not null)
			{
				encoder.WriteArrayHeader(this.pSuites.value);
				for (int i = 0; i < this.pSuites.value.Length; i++)
				{
					FW_AUTH_SUITE elem_0 = this.pSuites.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.pSuites.value.Length; i++)
				{
					FW_AUTH_SUITE elem_0 = this.pSuites.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_AUTH_SET>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_AUTH_SET>(ref this.pNext.value);
			}

			if (this.wszSetId is not null)
			{
				this.wszSetId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			if (this.pSuites is not null)
			{
				this.pSuites.value = decoder.ReadArrayHeader<FW_AUTH_SUITE>();
				for (int i = 0; i < this.pSuites.value.Length; i++)
				{
					FW_AUTH_SUITE elem_0 = this.pSuites.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_AUTH_SUITE>(NdrAlignment.NativePtr);
					this.pSuites.value[i] = elem_0;
				}

				for (int i = 0; i < this.pSuites.value.Length; i++)
				{
					FW_AUTH_SUITE elem_0 = this.pSuites.value[i];
					decoder.ReadStructDeferral<FW_AUTH_SUITE>(ref elem_0);
					this.pSuites.value[i] = elem_0;
				}
			}

			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_CRYPTO_KEY_EXCHANGE_TYPE : int
	{
		FW_CRYPTO_KEY_EXCHANGE_NONE = 0,
		FW_CRYPTO_KEY_EXCHANGE_DH1 = 1,
		FW_CRYPTO_KEY_EXCHANGE_DH2 = 2,
		FW_CRYPTO_KEY_EXCHANGE_ECDH256 = 3,
		FW_CRYPTO_KEY_EXCHANGE_ECDH384 = 4,
		FW_CRYPTO_KEY_EXCHANGE_DH2048 = 5,
		FW_CRYPTO_KEY_EXCHANGE_DH24 = 6,
		FW_CRYPTO_KEY_EXCHANGE_MAX = 7,
		FW_CRYPTO_KEY_EXCHANGE_DH14 = 5,
		FW_CRYPTO_KEY_EXCHANGE_MAX_V2_10 = 6
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_CRYPTO_ENCRYPTION_TYPE : int
	{
		FW_CRYPTO_ENCRYPTION_NONE = 0,
		FW_CRYPTO_ENCRYPTION_DES = 1,
		FW_CRYPTO_ENCRYPTION_3DES = 2,
		FW_CRYPTO_ENCRYPTION_AES128 = 3,
		FW_CRYPTO_ENCRYPTION_AES192 = 4,
		FW_CRYPTO_ENCRYPTION_AES256 = 5,
		FW_CRYPTO_ENCRYPTION_AES_GCM128 = 6,
		FW_CRYPTO_ENCRYPTION_AES_GCM192 = 7,
		FW_CRYPTO_ENCRYPTION_AES_GCM256 = 8,
		FW_CRYPTO_ENCRYPTION_MAX = 9,
		FW_CRYPTO_ENCRYPTION_MAX_V2_0 = 6
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_CRYPTO_HASH_TYPE : int
	{
		FW_CRYPTO_HASH_NONE = 0,
		FW_CRYPTO_HASH_MD5 = 1,
		FW_CRYPTO_HASH_SHA1 = 2,
		FW_CRYPTO_HASH_SHA256 = 3,
		FW_CRYPTO_HASH_SHA384 = 4,
		FW_CRYPTO_HASH_AES_GMAC128 = 5,
		FW_CRYPTO_HASH_AES_GMAC192 = 6,
		FW_CRYPTO_HASH_AES_GMAC256 = 7,
		FW_CRYPTO_HASH_MAX = 8,
		FW_CRYPTO_HASH_MAX_V2_0 = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_CRYPTO_PROTOCOL_TYPE : int
	{
		FW_CRYPTO_PROTOCOL_INVALID = 0,
		FW_CRYPTO_PROTOCOL_AH = 1,
		FW_CRYPTO_PROTOCOL_ESP = 2,
		FW_CRYPTO_PROTOCOL_BOTH = 3,
		FW_CRYPTO_PROTOCOL_AUTH_NO_ENCAP = 4,
		FW_CRYPTO_PROTOCOL_MAX = 5,
		FW_CRYPTO_PROTOCOL_MAX_2_1 = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_CRYPTO_SET_FLAGS : int
	{
		FW_CRYPTO_SET_FLAGS_NONE = 0,
		FW_CRYPTO_SET_FLAGS_MAX = 1
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_PHASE1_CRYPTO_SUITE : IRpcFixedStruct
	{
		public FW_CRYPTO_KEY_EXCHANGE_TYPE KeyExchange;
		public FW_CRYPTO_ENCRYPTION_TYPE Encryption;
		public FW_CRYPTO_HASH_TYPE Hash;
		public uint dwP1CryptoSuiteFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.KeyExchange);
			encoder.WriteEnumShortValue((short)this.Encryption);
			encoder.WriteEnumShortValue((short)this.Hash);
			encoder.WriteValue(this.dwP1CryptoSuiteFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.KeyExchange = (FW_CRYPTO_KEY_EXCHANGE_TYPE)decoder.ReadEnumShortValue();
			this.Encryption = (FW_CRYPTO_ENCRYPTION_TYPE)decoder.ReadEnumShortValue();
			this.Hash = (FW_CRYPTO_HASH_TYPE)decoder.ReadEnumShortValue();
			this.dwP1CryptoSuiteFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_PHASE2_CRYPTO_SUITE : IRpcFixedStruct
	{
		public FW_CRYPTO_PROTOCOL_TYPE Protocol;
		public FW_CRYPTO_HASH_TYPE AhHash;
		public FW_CRYPTO_HASH_TYPE EspHash;
		public FW_CRYPTO_ENCRYPTION_TYPE Encryption;
		public uint dwTimeoutMinutes;
		public uint dwTimeoutKBytes;
		public uint dwP2CryptoSuiteFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.Protocol);
			encoder.WriteEnumShortValue((short)this.AhHash);
			encoder.WriteEnumShortValue((short)this.EspHash);
			encoder.WriteEnumShortValue((short)this.Encryption);
			encoder.WriteValue(this.dwTimeoutMinutes);
			encoder.WriteValue(this.dwTimeoutKBytes);
			encoder.WriteValue(this.dwP2CryptoSuiteFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Protocol = (FW_CRYPTO_PROTOCOL_TYPE)decoder.ReadEnumShortValue();
			this.AhHash = (FW_CRYPTO_HASH_TYPE)decoder.ReadEnumShortValue();
			this.EspHash = (FW_CRYPTO_HASH_TYPE)decoder.ReadEnumShortValue();
			this.Encryption = (FW_CRYPTO_ENCRYPTION_TYPE)decoder.ReadEnumShortValue();
			this.dwTimeoutMinutes = decoder.ReadUInt32();
			this.dwTimeoutKBytes = decoder.ReadUInt32();
			this.dwP2CryptoSuiteFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_PHASE1_CRYPTO_FLAGS : int
	{
		FW_PHASE1_CRYPTO_FLAGS_NONE = 0,
		FW_PHASE1_CRYPTO_FLAGS_DO_NOT_SKIP_DH = 1,
		FW_PHASE1_CRYPTO_FLAGS_MAX = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_PHASE2_CRYPTO_PFS : int
	{
		FW_PHASE2_CRYPTO_PFS_INVALID = 0,
		FW_PHASE2_CRYPTO_PFS_DISABLE = 1,
		FW_PHASE2_CRYPTO_PFS_PHASE1 = 2,
		FW_PHASE2_CRYPTO_PFS_DH1 = 3,
		FW_PHASE2_CRYPTO_PFS_DH2 = 4,
		FW_PHASE2_CRYPTO_PFS_DH2048 = 5,
		FW_PHASE2_CRYPTO_PFS_ECDH256 = 6,
		FW_PHASE2_CRYPTO_PFS_ECDH384 = 7,
		FW_PHASE2_CRYPTO_PFS_DH24 = 8,
		FW_PHASE2_CRYPTO_PFS_MAX = 9,
		FW_PHASE2_CRYPTO_PFS_MAX_V2_10 = 8
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_27 : IRpcFixedStruct
	{
		public ushort wFlags;
		public uint dwNumPhase1Suites;
		public RpcPointer<FW_PHASE1_CRYPTO_SUITE[]> pPhase1Suites;
		public uint dwTimeOutMinutes;
		public uint dwTimeOutSessions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wFlags);
			encoder.WriteValue(this.dwNumPhase1Suites);
			encoder.WriteUniquePointer(this.pPhase1Suites);
			encoder.WriteValue(this.dwTimeOutMinutes);
			encoder.WriteValue(this.dwTimeOutSessions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wFlags = decoder.ReadUInt16();
			this.dwNumPhase1Suites = decoder.ReadUInt32();
			this.pPhase1Suites = decoder.ReadUniquePointer<FW_PHASE1_CRYPTO_SUITE[]>();
			this.dwTimeOutMinutes = decoder.ReadUInt32();
			this.dwTimeOutSessions = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pPhase1Suites is not null)
			{
				encoder.WriteArrayHeader(this.pPhase1Suites.value);
				for (int i = 0; i < this.pPhase1Suites.value.Length; i++)
				{
					FW_PHASE1_CRYPTO_SUITE elem_0 = this.pPhase1Suites.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.pPhase1Suites.value.Length; i++)
				{
					FW_PHASE1_CRYPTO_SUITE elem_0 = this.pPhase1Suites.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pPhase1Suites is not null)
			{
				this.pPhase1Suites.value = decoder.ReadArrayHeader<FW_PHASE1_CRYPTO_SUITE>();
				for (int i = 0; i < this.pPhase1Suites.value.Length; i++)
				{
					FW_PHASE1_CRYPTO_SUITE elem_0 = this.pPhase1Suites.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_PHASE1_CRYPTO_SUITE>(NdrAlignment._4Byte);
					this.pPhase1Suites.value[i] = elem_0;
				}

				for (int i = 0; i < this.pPhase1Suites.value.Length; i++)
				{
					FW_PHASE1_CRYPTO_SUITE elem_0 = this.pPhase1Suites.value[i];
					decoder.ReadStructDeferral<FW_PHASE1_CRYPTO_SUITE>(ref elem_0);
					this.pPhase1Suites.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_28 : IRpcFixedStruct
	{
		public FW_PHASE2_CRYPTO_PFS Pfs;
		public uint dwNumPhase2Suites;
		public RpcPointer<FW_PHASE2_CRYPTO_SUITE[]> pPhase2Suites;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.Pfs);
			encoder.WriteValue(this.dwNumPhase2Suites);
			encoder.WriteUniquePointer(this.pPhase2Suites);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Pfs = (FW_PHASE2_CRYPTO_PFS)decoder.ReadEnumShortValue();
			this.dwNumPhase2Suites = decoder.ReadUInt32();
			this.pPhase2Suites = decoder.ReadUniquePointer<FW_PHASE2_CRYPTO_SUITE[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pPhase2Suites is not null)
			{
				encoder.WriteArrayHeader(this.pPhase2Suites.value);
				for (int i = 0; i < this.pPhase2Suites.value.Length; i++)
				{
					FW_PHASE2_CRYPTO_SUITE elem_0 = this.pPhase2Suites.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.pPhase2Suites.value.Length; i++)
				{
					FW_PHASE2_CRYPTO_SUITE elem_0 = this.pPhase2Suites.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pPhase2Suites is not null)
			{
				this.pPhase2Suites.value = decoder.ReadArrayHeader<FW_PHASE2_CRYPTO_SUITE>();
				for (int i = 0; i < this.pPhase2Suites.value.Length; i++)
				{
					FW_PHASE2_CRYPTO_SUITE elem_0 = this.pPhase2Suites.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_PHASE2_CRYPTO_SUITE>(NdrAlignment._4Byte);
					this.pPhase2Suites.value[i] = elem_0;
				}

				for (int i = 0; i < this.pPhase2Suites.value.Length; i++)
				{
					FW_PHASE2_CRYPTO_SUITE elem_0 = this.pPhase2Suites.value[i];
					decoder.ReadStructDeferral<FW_PHASE2_CRYPTO_SUITE>(ref elem_0);
					this.pPhase2Suites.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_26 : IRpcFixedStruct
	{
		public FW_IPSEC_PHASE IpSecPhase;
		public Unnamed_27 __unnamed_0;
		public Unnamed_28 __unnamed_1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.IpSecPhase);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.IpSecPhase)
			{
				case 1:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 2:
					encoder.WriteFixedStruct(this.__unnamed_1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.IpSecPhase)
			{
				case 1:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_27>(NdrAlignment.NativePtr);
					break;
				case 2:
					this.__unnamed_1 = decoder.ReadFixedStruct<Unnamed_28>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.IpSecPhase)
			{
				case 1:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 2:
					encoder.WriteStructDeferral(this.__unnamed_1);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.IpSecPhase)
			{
				case 1:
					decoder.ReadStructDeferral<Unnamed_27>(ref this.__unnamed_0);
					break;
				case 2:
					decoder.ReadStructDeferral<Unnamed_28>(ref this.__unnamed_1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_CRYPTO_SET : IRpcFixedStruct
	{
		public RpcPointer<FW_CRYPTO_SET> pNext;
		public ushort wSchemaVersion;
		public FW_IPSEC_PHASE IpSecPhase;
		public RpcPointer<string> wszSetId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public RpcPointer<string> wszEmbeddedContext;
		public Unnamed_26 unnamed_1;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public FW_RULE_STATUS Status;
		public uint dwCryptoSetFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteEnumShortValue((short)this.IpSecPhase);
			encoder.WriteUniquePointer(this.wszSetId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteUnion(this.unnamed_1);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue((int)this.Status);
			encoder.WriteValue(this.dwCryptoSetFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_CRYPTO_SET>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			this.wszSetId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_26>();
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.dwCryptoSetFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszSetId is not null)
			{
				encoder.WriteWideCharString(this.wszSetId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.unnamed_1);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_CRYPTO_SET>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CRYPTO_SET>(ref this.pNext.value);
			}

			if (this.wszSetId is not null)
			{
				this.wszSetId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<Unnamed_26>(ref this.unnamed_1);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_BYTE_BLOB : IRpcFixedStruct
	{
		public uint dwSize;
		public RpcPointer<byte[]> Blob;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwSize);
			encoder.WriteUniquePointer(this.Blob);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwSize = decoder.ReadUInt32();
			this.Blob = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Blob is not null)
			{
				encoder.WriteArrayHeader(this.Blob.value);
				for (int i = 0; i < this.Blob.value.Length; i++)
				{
					byte elem_0 = this.Blob.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Blob is not null)
			{
				this.Blob.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.Blob.value.Length; i++)
				{
					byte elem_0 = this.Blob.value[i];
					elem_0 = decoder.ReadByte();
					this.Blob.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_COOKIE_PAIR : IRpcFixedStruct
	{
		public ulong Initiator;
		public ulong Responder;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Initiator);
			encoder.WriteValue(this.Responder);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Initiator = decoder.ReadUInt64();
			this.Responder = decoder.ReadUInt64();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_PHASE1_KEY_MODULE_TYPE : int
	{
		FW_PHASE1_KEY_MODULE_INVALID = 0,
		FW_PHASE1_KEY_MODULE_IKE = 1,
		FW_PHASE1_KEY_MODULE_AUTH_IP = 2,
		FW_PHASE1_KEY_MODULE_MAX = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_CERT_INFO : IRpcFixedStruct
	{
		public FW_BYTE_BLOB SubjectName;
		public uint dwCertFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.SubjectName, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwCertFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.SubjectName = decoder.ReadFixedStruct<FW_BYTE_BLOB>(NdrAlignment.NativePtr);
			this.dwCertFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.SubjectName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_BYTE_BLOB>(ref this.SubjectName);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_30 : IRpcFixedStruct
	{
		public FW_CERT_INFO MyCert;
		public FW_CERT_INFO PeerCert;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.MyCert, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.PeerCert, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.MyCert = decoder.ReadFixedStruct<FW_CERT_INFO>(NdrAlignment.NativePtr);
			this.PeerCert = decoder.ReadFixedStruct<FW_CERT_INFO>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.MyCert);
			encoder.WriteStructDeferral(this.PeerCert);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_CERT_INFO>(ref this.MyCert);
			decoder.ReadStructDeferral<FW_CERT_INFO>(ref this.PeerCert);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_31 : IRpcFixedStruct
	{
		public RpcPointer<string> wszMyId;
		public RpcPointer<string> wszPeerId;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.wszMyId);
			encoder.WriteUniquePointer(this.wszPeerId);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wszMyId = decoder.ReadUniquePointer<string>();
			this.wszPeerId = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wszMyId is not null)
			{
				encoder.WriteWideCharString(this.wszMyId.value);
			}

			if (this.wszPeerId is not null)
			{
				encoder.WriteWideCharString(this.wszPeerId.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wszMyId is not null)
			{
				this.wszMyId.value = decoder.ReadWideCharString();
			}

			if (this.wszPeerId is not null)
			{
				this.wszPeerId.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_29 : IRpcFixedStruct
	{
		public FW_AUTH_METHOD AuthMethod;
		public Unnamed_30 __unnamed_0;
		public Unnamed_31 __unnamed_1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.AuthMethod);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.AuthMethod)
			{
				case 5:
				case 7:
					encoder.WriteFixedStruct(this.__unnamed_0, NdrAlignment.NativePtr);
					break;
				case 2:
				case 6:
				case 9:
				case 10:
					encoder.WriteFixedStruct(this.__unnamed_1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.AuthMethod = (FW_AUTH_METHOD)decoder.ReadEnumShortValue();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.AuthMethod)
			{
				case 5:
				case 7:
					this.__unnamed_0 = decoder.ReadFixedStruct<Unnamed_30>(NdrAlignment.NativePtr);
					break;
				case 2:
				case 6:
				case 9:
				case 10:
					this.__unnamed_1 = decoder.ReadFixedStruct<Unnamed_31>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.AuthMethod)
			{
				case 5:
				case 7:
					encoder.WriteStructDeferral(this.__unnamed_0);
					break;
				case 2:
				case 6:
				case 9:
				case 10:
					encoder.WriteStructDeferral(this.__unnamed_1);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.AuthMethod)
			{
				case 5:
				case 7:
					decoder.ReadStructDeferral<Unnamed_30>(ref this.__unnamed_0);
					break;
				case 2:
				case 6:
				case 9:
				case 10:
					decoder.ReadStructDeferral<Unnamed_31>(ref this.__unnamed_1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_AUTH_INFO : IRpcFixedStruct
	{
		public FW_AUTH_METHOD AuthMethod;
		public Unnamed_29 unnamed_1;
		public uint dwAuthInfoFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.AuthMethod);
			encoder.WriteUnion(this.unnamed_1);
			encoder.WriteValue(this.dwAuthInfoFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.AuthMethod = (FW_AUTH_METHOD)decoder.ReadEnumShortValue();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_29>();
			this.dwAuthInfoFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<Unnamed_29>(ref this.unnamed_1);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_ENDPOINTS : IRpcFixedStruct
	{
		public FW_IP_VERSION IpVersion;
		public uint dwSourceV4Address;
		public uint dwDestinationV4Address;
		public byte[] SourceV6Address;
		public byte[] DestinationV6Address;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.IpVersion);
			encoder.WriteValue(this.dwSourceV4Address);
			encoder.WriteValue(this.dwDestinationV4Address);
			if (this.SourceV6Address == null)
				this.SourceV6Address = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.SourceV6Address[i];
				encoder.WriteValue(elem_0);
			}

			if (this.DestinationV6Address == null)
				this.DestinationV6Address = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.DestinationV6Address[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.IpVersion = (FW_IP_VERSION)decoder.ReadEnumShortValue();
			this.dwSourceV4Address = decoder.ReadUInt32();
			this.dwDestinationV4Address = decoder.ReadUInt32();
			if (this.SourceV6Address == null)
				this.SourceV6Address = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.SourceV6Address[i];
				elem_0 = decoder.ReadByte();
				this.SourceV6Address[i] = elem_0;
			}

			if (this.DestinationV6Address == null)
				this.DestinationV6Address = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.DestinationV6Address[i];
				elem_0 = decoder.ReadByte();
				this.DestinationV6Address[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_PHASE1_SA_DETAILS : IRpcFixedStruct
	{
		public ulong SaId;
		public FW_PHASE1_KEY_MODULE_TYPE KeyModuleType;
		public FW_ENDPOINTS Endpoints;
		public FW_PHASE1_CRYPTO_SUITE SelectedProposal;
		public uint dwProposalLifetimeKBytes;
		public uint dwProposalLifetimeMinutes;
		public uint dwProposalMaxNumPhase2;
		public FW_COOKIE_PAIR CookiePair;
		public RpcPointer<FW_AUTH_INFO> pFirstAuth;
		public RpcPointer<FW_AUTH_INFO> pSecondAuth;
		public uint dwP1SaFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.SaId);
			encoder.WriteEnumShortValue((short)this.KeyModuleType);
			encoder.WriteFixedStruct(this.Endpoints, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.SelectedProposal, NdrAlignment._4Byte);
			encoder.WriteValue(this.dwProposalLifetimeKBytes);
			encoder.WriteValue(this.dwProposalLifetimeMinutes);
			encoder.WriteValue(this.dwProposalMaxNumPhase2);
			encoder.WriteFixedStruct(this.CookiePair, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pFirstAuth);
			encoder.WriteUniquePointer(this.pSecondAuth);
			encoder.WriteValue(this.dwP1SaFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.SaId = decoder.ReadUInt64();
			this.KeyModuleType = (FW_PHASE1_KEY_MODULE_TYPE)decoder.ReadEnumShortValue();
			this.Endpoints = decoder.ReadFixedStruct<FW_ENDPOINTS>(NdrAlignment._4Byte);
			this.SelectedProposal = decoder.ReadFixedStruct<FW_PHASE1_CRYPTO_SUITE>(NdrAlignment._4Byte);
			this.dwProposalLifetimeKBytes = decoder.ReadUInt32();
			this.dwProposalLifetimeMinutes = decoder.ReadUInt32();
			this.dwProposalMaxNumPhase2 = decoder.ReadUInt32();
			this.CookiePair = decoder.ReadFixedStruct<FW_COOKIE_PAIR>(NdrAlignment._8Byte);
			this.pFirstAuth = decoder.ReadUniquePointer<FW_AUTH_INFO>();
			this.pSecondAuth = decoder.ReadUniquePointer<FW_AUTH_INFO>();
			this.dwP1SaFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Endpoints);
			encoder.WriteStructDeferral(this.SelectedProposal);
			encoder.WriteStructDeferral(this.CookiePair);
			if (this.pFirstAuth is not null)
			{
				encoder.WriteFixedStruct(this.pFirstAuth.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pFirstAuth.value);
			}

			if (this.pSecondAuth is not null)
			{
				encoder.WriteFixedStruct(this.pSecondAuth.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pSecondAuth.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_ENDPOINTS>(ref this.Endpoints);
			decoder.ReadStructDeferral<FW_PHASE1_CRYPTO_SUITE>(ref this.SelectedProposal);
			decoder.ReadStructDeferral<FW_COOKIE_PAIR>(ref this.CookiePair);
			if (this.pFirstAuth is not null)
			{
				this.pFirstAuth.value = decoder.ReadFixedStruct<FW_AUTH_INFO>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_AUTH_INFO>(ref this.pFirstAuth.value);
			}

			if (this.pSecondAuth is not null)
			{
				this.pSecondAuth.value = decoder.ReadFixedStruct<FW_AUTH_INFO>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_AUTH_INFO>(ref this.pSecondAuth.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_PHASE2_TRAFFIC_TYPE : int
	{
		FW_PHASE2_TRAFFIC_TYPE_INVALID = 0,
		FW_PHASE2_TRAFFIC_TYPE_TRANSPORT = 1,
		FW_PHASE2_TRAFFIC_TYPE_TUNNEL = 2,
		FW_PHASE2_TRAFFIC_TYPE_MAX = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_PHASE2_SA_DETAILS : IRpcFixedStruct
	{
		public ulong SaId;
		public FW_DIRECTION Direction;
		public FW_ENDPOINTS Endpoints;
		public ushort wLocalPort;
		public ushort wRemotePort;
		public ushort wIpProtocol;
		public FW_PHASE2_CRYPTO_SUITE SelectedProposal;
		public FW_PHASE2_CRYPTO_PFS Pfs;
		public Guid TransportFilterId;
		public uint dwP2SaFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.SaId);
			encoder.WriteEnumShortValue((short)this.Direction);
			encoder.WriteFixedStruct(this.Endpoints, NdrAlignment._4Byte);
			encoder.WriteValue(this.wLocalPort);
			encoder.WriteValue(this.wRemotePort);
			encoder.WriteValue(this.wIpProtocol);
			encoder.WriteFixedStruct(this.SelectedProposal, NdrAlignment._4Byte);
			encoder.WriteEnumShortValue((short)this.Pfs);
			encoder.WriteValue(this.TransportFilterId);
			encoder.WriteValue(this.dwP2SaFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.SaId = decoder.ReadUInt64();
			this.Direction = (FW_DIRECTION)decoder.ReadEnumShortValue();
			this.Endpoints = decoder.ReadFixedStruct<FW_ENDPOINTS>(NdrAlignment._4Byte);
			this.wLocalPort = decoder.ReadUInt16();
			this.wRemotePort = decoder.ReadUInt16();
			this.wIpProtocol = decoder.ReadUInt16();
			this.SelectedProposal = decoder.ReadFixedStruct<FW_PHASE2_CRYPTO_SUITE>(NdrAlignment._4Byte);
			this.Pfs = (FW_PHASE2_CRYPTO_PFS)decoder.ReadEnumShortValue();
			this.TransportFilterId = decoder.ReadUuid();
			this.dwP2SaFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Endpoints);
			encoder.WriteStructDeferral(this.SelectedProposal);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_ENDPOINTS>(ref this.Endpoints);
			decoder.ReadStructDeferral<FW_PHASE2_CRYPTO_SUITE>(ref this.SelectedProposal);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_PROFILE_CONFIG_VALUE : IRpcFixedStruct
	{
		public FW_PROFILE_CONFIG configID;
		public RpcPointer<string> wszStr;
		public RpcPointer<FW_INTERFACE_LUIDS> pDisabledInterfaces;
		public RpcPointer<uint> pdwVal;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.configID);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.configID)
			{
				case 9:
					encoder.WriteUniquePointer(this.wszStr);
					break;
				case 15:
					encoder.WriteUniquePointer(this.pDisabledInterfaces);
					break;
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 10:
				case 11:
				case 12:
				case 13:
				case 14:
				case 16:
				case 17:
				case 18:
					encoder.WriteUniquePointer(this.pdwVal);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.configID = (FW_PROFILE_CONFIG)decoder.ReadEnumShortValue();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.configID)
			{
				case 9:
					this.wszStr = decoder.ReadUniquePointer<string>();
					break;
				case 15:
					this.pDisabledInterfaces = decoder.ReadUniquePointer<FW_INTERFACE_LUIDS>();
					break;
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 10:
				case 11:
				case 12:
				case 13:
				case 14:
				case 16:
				case 17:
				case 18:
					this.pdwVal = decoder.ReadUniquePointer<uint>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.configID)
			{
				case 9:
					if (this.wszStr is not null)
					{
						encoder.WriteWideCharString(this.wszStr.value);
					}

					break;
				case 15:
					if (this.pDisabledInterfaces is not null)
					{
						encoder.WriteFixedStruct(this.pDisabledInterfaces.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.pDisabledInterfaces.value);
					}

					break;
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 10:
				case 11:
				case 12:
				case 13:
				case 14:
				case 16:
				case 17:
				case 18:
					if (this.pdwVal is not null)
					{
						encoder.WriteValue(this.pdwVal.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.configID)
			{
				case 9:
					if (this.wszStr is not null)
					{
						this.wszStr.value = decoder.ReadWideCharString();
					}

					break;
				case 15:
					if (this.pDisabledInterfaces is not null)
					{
						this.pDisabledInterfaces.value = decoder.ReadFixedStruct<FW_INTERFACE_LUIDS>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<FW_INTERFACE_LUIDS>(ref this.pDisabledInterfaces.value);
					}

					break;
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 10:
				case 11:
				case 12:
				case 13:
				case 14:
				case 16:
				case 17:
				case 18:
					if (this.pdwVal is not null)
					{
						this.pdwVal.value = decoder.ReadUInt32();
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_MM_RULE : IRpcFixedStruct
	{
		public RpcPointer<FW_MM_RULE> pNext;
		public ushort wSchemaVersion;
		public RpcPointer<string> wszRuleId;
		public RpcPointer<string> wszName;
		public RpcPointer<string> wszDescription;
		public uint dwProfiles;
		public FW_ADDRESSES Endpoint1;
		public FW_ADDRESSES Endpoint2;
		public RpcPointer<string> wszPhase1AuthSet;
		public RpcPointer<string> wszPhase1CryptoSet;
		public ushort wFlags;
		public RpcPointer<string> wszEmbeddedContext;
		public FW_OS_PLATFORM_LIST PlatformValidityList;
		public FW_RULE_ORIGIN_TYPE Origin;
		public RpcPointer<string> wszGPOName;
		public FW_RULE_STATUS Status;
		public uint Reserved;
		public RpcPointer<FW_OBJECT_METADATA[]> pMetaData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNext);
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteUniquePointer(this.wszRuleId);
			encoder.WriteUniquePointer(this.wszName);
			encoder.WriteUniquePointer(this.wszDescription);
			encoder.WriteValue(this.dwProfiles);
			encoder.WriteFixedStruct(this.Endpoint1, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.Endpoint2, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.wszPhase1AuthSet);
			encoder.WriteUniquePointer(this.wszPhase1CryptoSet);
			encoder.WriteValue(this.wFlags);
			encoder.WriteUniquePointer(this.wszEmbeddedContext);
			encoder.WriteFixedStruct(this.PlatformValidityList, NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.Origin);
			encoder.WriteUniquePointer(this.wszGPOName);
			encoder.WriteValue((int)this.Status);
			encoder.WriteValue(this.Reserved);
			encoder.WriteUniquePointer(this.pMetaData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNext = decoder.ReadUniquePointer<FW_MM_RULE>();
			this.wSchemaVersion = decoder.ReadUInt16();
			this.wszRuleId = decoder.ReadUniquePointer<string>();
			this.wszName = decoder.ReadUniquePointer<string>();
			this.wszDescription = decoder.ReadUniquePointer<string>();
			this.dwProfiles = decoder.ReadUInt32();
			this.Endpoint1 = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.Endpoint2 = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.wszPhase1AuthSet = decoder.ReadUniquePointer<string>();
			this.wszPhase1CryptoSet = decoder.ReadUniquePointer<string>();
			this.wFlags = decoder.ReadUInt16();
			this.wszEmbeddedContext = decoder.ReadUniquePointer<string>();
			this.PlatformValidityList = decoder.ReadFixedStruct<FW_OS_PLATFORM_LIST>(NdrAlignment.NativePtr);
			this.Origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.wszGPOName = decoder.ReadUniquePointer<string>();
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
			this.Reserved = decoder.ReadUInt32();
			this.pMetaData = decoder.ReadUniquePointer<FW_OBJECT_METADATA[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNext is not null)
			{
				encoder.WriteFixedStruct(this.pNext.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				encoder.WriteWideCharString(this.wszRuleId.value);
			}

			if (this.wszName is not null)
			{
				encoder.WriteWideCharString(this.wszName.value);
			}

			if (this.wszDescription is not null)
			{
				encoder.WriteWideCharString(this.wszDescription.value);
			}

			encoder.WriteStructDeferral(this.Endpoint1);
			encoder.WriteStructDeferral(this.Endpoint2);
			if (this.wszPhase1AuthSet is not null)
			{
				encoder.WriteWideCharString(this.wszPhase1AuthSet.value);
			}

			if (this.wszPhase1CryptoSet is not null)
			{
				encoder.WriteWideCharString(this.wszPhase1CryptoSet.value);
			}

			if (this.wszEmbeddedContext is not null)
			{
				encoder.WriteWideCharString(this.wszEmbeddedContext.value);
			}

			encoder.WriteStructDeferral(this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				encoder.WriteWideCharString(this.wszGPOName.value);
			}

			if (this.pMetaData is not null)
			{
				encoder.WriteArrayHeader(this.pMetaData.value);
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNext is not null)
			{
				this.pNext.value = decoder.ReadFixedStruct<FW_MM_RULE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_MM_RULE>(ref this.pNext.value);
			}

			if (this.wszRuleId is not null)
			{
				this.wszRuleId.value = decoder.ReadWideCharString();
			}

			if (this.wszName is not null)
			{
				this.wszName.value = decoder.ReadWideCharString();
			}

			if (this.wszDescription is not null)
			{
				this.wszDescription.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.Endpoint1);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.Endpoint2);
			if (this.wszPhase1AuthSet is not null)
			{
				this.wszPhase1AuthSet.value = decoder.ReadWideCharString();
			}

			if (this.wszPhase1CryptoSet is not null)
			{
				this.wszPhase1CryptoSet.value = decoder.ReadWideCharString();
			}

			if (this.wszEmbeddedContext is not null)
			{
				this.wszEmbeddedContext.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_OS_PLATFORM_LIST>(ref this.PlatformValidityList);
			if (this.wszGPOName is not null)
			{
				this.wszGPOName.value = decoder.ReadWideCharString();
			}

			if (this.pMetaData is not null)
			{
				this.pMetaData.value = decoder.ReadArrayHeader<FW_OBJECT_METADATA>();
				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_OBJECT_METADATA>(NdrAlignment._8Byte);
					this.pMetaData.value[i] = elem_0;
				}

				for (int i = 0; i < this.pMetaData.value.Length; i++)
				{
					FW_OBJECT_METADATA elem_0 = this.pMetaData.value[i];
					decoder.ReadStructDeferral<FW_OBJECT_METADATA>(ref elem_0);
					this.pMetaData.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_MATCH_KEY : int
	{
		FW_MATCH_KEY_PROFILE = 0,
		FW_MATCH_KEY_STATUS = 1,
		FW_MATCH_KEY_OBJECTID = 2,
		FW_MATCH_KEY_FILTERID = 3,
		FW_MATCH_KEY_APP_PATH = 4,
		FW_MATCH_KEY_PROTOCOL = 5,
		FW_MATCH_KEY_LOCAL_PORT = 6,
		FW_MATCH_KEY_REMOTE_PORT = 7,
		FW_MATCH_KEY_GROUP = 8,
		FW_MATCH_KEY_SVC_NAME = 9,
		FW_MATCH_KEY_DIRECTION = 10,
		FW_MATCH_KEY_LOCAL_USER_OWNER = 11,
		FW_MATCH_KEY_PACKAGE_ID = 12,
		FW_MATCH_KEY_FQBN = 13,
		FW_MATCH_KEY_COMPARTMENT_ID = 14,
		FW_MATCH_KEY_REMOTE_USER_AUTH_LIST = 15,
		FW_MATCH_KEY_PACKAGE_FAMILY_NAME = 16,
		FW_MATCH_KEY_MAX = 17
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_DATA_TYPE : int
	{
		FW_DATA_TYPE_EMPTY = 0,
		FW_DATA_TYPE_UINT8 = 1,
		FW_DATA_TYPE_UINT16 = 2,
		FW_DATA_TYPE_UINT32 = 3,
		FW_DATA_TYPE_UINT64 = 4,
		FW_DATA_TYPE_UNICODE_STRING = 5
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_33 : IRpcFixedStruct
	{
		public RpcPointer<string> wszString;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.wszString);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wszString = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wszString is not null)
			{
				encoder.WriteWideCharString(this.wszString.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wszString is not null)
			{
				this.wszString.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct Unnamed_32 : IRpcFixedStruct
	{
		public FW_DATA_TYPE type;
		public byte uInt8;
		public ushort uInt16;
		public uint uInt32;
		public ulong uInt64;
		public Unnamed_33 __unnamed_4;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._8Byte);
			encoder.WriteEnumShortValue((short)this.type);
			encoder.AlignUnionArm(NdrAlignment._8Byte);
			switch ((int)this.type)
			{
				case 1:
					encoder.WriteValue(this.uInt8);
					break;
				case 2:
					encoder.WriteValue(this.uInt16);
					break;
				case 3:
					encoder.WriteValue(this.uInt32);
					break;
				case 4:
					encoder.WriteValue(this.uInt64);
					break;
				case 5:
					encoder.WriteFixedStruct(this.__unnamed_4, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._8Byte);
			this.type = (FW_DATA_TYPE)decoder.ReadEnumShortValue();
			decoder.AlignUnionArm(NdrAlignment._8Byte);
			switch ((int)this.type)
			{
				case 1:
					this.uInt8 = decoder.ReadByte();
					break;
				case 2:
					this.uInt16 = decoder.ReadUInt16();
					break;
				case 3:
					this.uInt32 = decoder.ReadUInt32();
					break;
				case 4:
					this.uInt64 = decoder.ReadUInt64();
					break;
				case 5:
					this.__unnamed_4 = decoder.ReadFixedStruct<Unnamed_33>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.type)
			{
				case 1:
					break;
				case 2:
					break;
				case 3:
					break;
				case 4:
					break;
				case 5:
					encoder.WriteStructDeferral(this.__unnamed_4);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.type)
			{
				case 1:
					break;
				case 2:
					break;
				case 3:
					break;
				case 4:
					break;
				case 5:
					decoder.ReadStructDeferral<Unnamed_33>(ref this.__unnamed_4);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_MATCH_VALUE : IRpcFixedStruct
	{
		public FW_DATA_TYPE type;
		public Unnamed_32 unnamed_1;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.type);
			encoder.WriteUnion(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.type = (FW_DATA_TYPE)decoder.ReadEnumShortValue();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_32>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<Unnamed_32>(ref this.unnamed_1);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_MATCH_TYPE : int
	{
		FW_MATCH_TYPE_TRAFFIC_MATCH = 0,
		FW_MATCH_TYPE_EQUAL = 1,
		FW_MATCH_TYPE_MAX = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_QUERY_CONDITION : IRpcFixedStruct
	{
		public FW_MATCH_KEY matchKey;
		public FW_MATCH_TYPE matchType;
		public FW_MATCH_VALUE matchValue;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.matchKey);
			encoder.WriteEnumShortValue((short)this.matchType);
			encoder.WriteFixedStruct(this.matchValue, NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.matchKey = (FW_MATCH_KEY)decoder.ReadEnumShortValue();
			this.matchType = (FW_MATCH_TYPE)decoder.ReadEnumShortValue();
			this.matchValue = decoder.ReadFixedStruct<FW_MATCH_VALUE>(NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.matchValue);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FW_MATCH_VALUE>(ref this.matchValue);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_QUERY_CONDITIONS : IRpcFixedStruct
	{
		public uint dwNumEntries;
		public RpcPointer<FW_QUERY_CONDITION[]> AndedConditions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwNumEntries);
			encoder.WriteUniquePointer(this.AndedConditions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwNumEntries = decoder.ReadUInt32();
			this.AndedConditions = decoder.ReadUniquePointer<FW_QUERY_CONDITION[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.AndedConditions is not null)
			{
				encoder.WriteArrayHeader(this.AndedConditions.value);
				for (int i = 0; i < this.AndedConditions.value.Length; i++)
				{
					FW_QUERY_CONDITION elem_0 = this.AndedConditions.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.AndedConditions.value.Length; i++)
				{
					FW_QUERY_CONDITION elem_0 = this.AndedConditions.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.AndedConditions is not null)
			{
				this.AndedConditions.value = decoder.ReadArrayHeader<FW_QUERY_CONDITION>();
				for (int i = 0; i < this.AndedConditions.value.Length; i++)
				{
					FW_QUERY_CONDITION elem_0 = this.AndedConditions.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_QUERY_CONDITION>(NdrAlignment._8Byte);
					this.AndedConditions.value[i] = elem_0;
				}

				for (int i = 0; i < this.AndedConditions.value.Length; i++)
				{
					FW_QUERY_CONDITION elem_0 = this.AndedConditions.value[i];
					decoder.ReadStructDeferral<FW_QUERY_CONDITION>(ref elem_0);
					this.AndedConditions.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_QUERY : IRpcFixedStruct
	{
		public ushort wSchemaVersion;
		public uint dwNumEntries;
		public RpcPointer<FW_QUERY_CONDITIONS[]> ORConditions;
		public FW_RULE_STATUS Status;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wSchemaVersion);
			encoder.WriteValue(this.dwNumEntries);
			encoder.WriteUniquePointer(this.ORConditions);
			encoder.WriteValue((int)this.Status);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wSchemaVersion = decoder.ReadUInt16();
			this.dwNumEntries = decoder.ReadUInt32();
			this.ORConditions = decoder.ReadUniquePointer<FW_QUERY_CONDITIONS[]>();
			this.Status = (FW_RULE_STATUS)decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ORConditions is not null)
			{
				encoder.WriteArrayHeader(this.ORConditions.value);
				for (int i = 0; i < this.ORConditions.value.Length; i++)
				{
					FW_QUERY_CONDITIONS elem_0 = this.ORConditions.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.ORConditions.value.Length; i++)
				{
					FW_QUERY_CONDITIONS elem_0 = this.ORConditions.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ORConditions is not null)
			{
				this.ORConditions.value = decoder.ReadArrayHeader<FW_QUERY_CONDITIONS>();
				for (int i = 0; i < this.ORConditions.value.Length; i++)
				{
					FW_QUERY_CONDITIONS elem_0 = this.ORConditions.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_QUERY_CONDITIONS>(NdrAlignment.NativePtr);
					this.ORConditions.value[i] = elem_0;
				}

				for (int i = 0; i < this.ORConditions.value.Length; i++)
				{
					FW_QUERY_CONDITIONS elem_0 = this.ORConditions.value[i];
					decoder.ReadStructDeferral<FW_QUERY_CONDITIONS>(ref elem_0);
					this.ORConditions.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_HYPERV_VM_CREATOR0 : IRpcFixedStruct
	{
		public RpcPointer<FW_HYPERV_VM_CREATOR0> next;
		public ushort schemaVersion;
		public Guid id;
		public RpcPointer<string> friendlyName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.next);
			encoder.WriteValue(this.schemaVersion);
			encoder.WriteValue(this.id);
			encoder.WriteUniquePointer(this.friendlyName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.next = decoder.ReadUniquePointer<FW_HYPERV_VM_CREATOR0>();
			this.schemaVersion = decoder.ReadUInt16();
			this.id = decoder.ReadUuid();
			this.friendlyName = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.next is not null)
			{
				encoder.WriteFixedStruct(this.next.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.next.value);
			}

			if (this.friendlyName is not null)
			{
				encoder.WriteWideCharString(this.friendlyName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.next is not null)
			{
				this.next.value = decoder.ReadFixedStruct<FW_HYPERV_VM_CREATOR0>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_HYPERV_VM_CREATOR0>(ref this.next.value);
			}

			if (this.friendlyName is not null)
			{
				this.friendlyName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_HYPERV_PORT_FLAGS : int
	{
		FW_HYPERV_PORT_FLAGS_NONE = 0,
		FW_HYPERV_PORT_FLAGS_CONSTRAINED_INTERFACE = 1,
		FW_HYPERV_PORT_FLAGS_MAX = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_HYPERV_NETWORK_TYPE : int
	{
		FW_HYPERV_NETWORK_TYPE_INVALID = 0,
		FW_HYPERV_NETWORK_TYPE_FSE = 1,
		FW_HYPERV_NETWORK_TYPE_NAT = 2,
		FW_HYPERV_NETWORK_TYPE_MAX = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_HYPERV_PORT0 : IRpcFixedStruct
	{
		public RpcPointer<FW_HYPERV_PORT0> next;
		public RpcPointer<string> switchName;
		public RpcPointer<string> portName;
		public Guid vmCreatorId;
		public Guid interfaceGuid;
		public Guid partitionGuid;
		public uint flags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.next);
			encoder.WriteUniquePointer(this.switchName);
			encoder.WriteUniquePointer(this.portName);
			encoder.WriteValue(this.vmCreatorId);
			encoder.WriteValue(this.interfaceGuid);
			encoder.WriteValue(this.partitionGuid);
			encoder.WriteValue(this.flags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.next = decoder.ReadUniquePointer<FW_HYPERV_PORT0>();
			this.switchName = decoder.ReadUniquePointer<string>();
			this.portName = decoder.ReadUniquePointer<string>();
			this.vmCreatorId = decoder.ReadUuid();
			this.interfaceGuid = decoder.ReadUuid();
			this.partitionGuid = decoder.ReadUuid();
			this.flags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.next is not null)
			{
				encoder.WriteFixedStruct(this.next.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.next.value);
			}

			if (this.switchName is not null)
			{
				encoder.WriteWideCharString(this.switchName.value);
			}

			if (this.portName is not null)
			{
				encoder.WriteWideCharString(this.portName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.next is not null)
			{
				this.next.value = decoder.ReadFixedStruct<FW_HYPERV_PORT0>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_HYPERV_PORT0>(ref this.next.value);
			}

			if (this.switchName is not null)
			{
				this.switchName.value = decoder.ReadWideCharString();
			}

			if (this.portName is not null)
			{
				this.portName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_HYPERV_PORT1 : IRpcFixedStruct
	{
		public RpcPointer<FW_HYPERV_PORT1> next;
		public RpcPointer<string> switchName;
		public RpcPointer<string> portName;
		public Guid vmCreatorId;
		public Guid interfaceGuid;
		public Guid partitionGuid;
		public uint flags;
		public FW_PROFILE_TYPE profileType;
		public FW_HYPERV_NETWORK_TYPE networkType;
		public RpcPointer<string> constrainedInterfaceAlias;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.next);
			encoder.WriteUniquePointer(this.switchName);
			encoder.WriteUniquePointer(this.portName);
			encoder.WriteValue(this.vmCreatorId);
			encoder.WriteValue(this.interfaceGuid);
			encoder.WriteValue(this.partitionGuid);
			encoder.WriteValue(this.flags);
			encoder.WriteValue((int)this.profileType);
			encoder.WriteEnumShortValue((short)this.networkType);
			encoder.WriteUniquePointer(this.constrainedInterfaceAlias);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.next = decoder.ReadUniquePointer<FW_HYPERV_PORT1>();
			this.switchName = decoder.ReadUniquePointer<string>();
			this.portName = decoder.ReadUniquePointer<string>();
			this.vmCreatorId = decoder.ReadUuid();
			this.interfaceGuid = decoder.ReadUuid();
			this.partitionGuid = decoder.ReadUuid();
			this.flags = decoder.ReadUInt32();
			this.profileType = (FW_PROFILE_TYPE)decoder.ReadInt32();
			this.networkType = (FW_HYPERV_NETWORK_TYPE)decoder.ReadEnumShortValue();
			this.constrainedInterfaceAlias = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.next is not null)
			{
				encoder.WriteFixedStruct(this.next.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.next.value);
			}

			if (this.switchName is not null)
			{
				encoder.WriteWideCharString(this.switchName.value);
			}

			if (this.portName is not null)
			{
				encoder.WriteWideCharString(this.portName.value);
			}

			if (this.constrainedInterfaceAlias is not null)
			{
				encoder.WriteWideCharString(this.constrainedInterfaceAlias.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.next is not null)
			{
				this.next.value = decoder.ReadFixedStruct<FW_HYPERV_PORT1>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_HYPERV_PORT1>(ref this.next.value);
			}

			if (this.switchName is not null)
			{
				this.switchName.value = decoder.ReadWideCharString();
			}

			if (this.portName is not null)
			{
				this.portName.value = decoder.ReadWideCharString();
			}

			if (this.constrainedInterfaceAlias is not null)
			{
				this.constrainedInterfaceAlias.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_HYPERV_VM_CONFIG : int
	{
		FW_HYPERV_VM_CONFIG_INVALID = 0,
		FW_HYPERV_VM_CONFIG_LOOPBACK_ENABLED = 1,
		FW_HYPERV_VM_CONFIG_ALLOW_HOST_POLICY_MERGE = 2,
		FW_HYPERV_VM_CONFIG_MAX = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_HYPERV_PROFILE_CONFIG : int
	{
		FW_HYPERV_PROFILE_CONFIG_INVALID = 0,
		FW_HYPERV_PROFILE_CONFIG_ENABLED = 1,
		FW_HYPERV_PROFILE_CONFIG_ALLOW_LOCAL_POLICY_MERGE = 2,
		FW_HYPERV_PROFILE_CONFIG_MAX = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_HYPERV_VM_CONFIG_VALUE0 : IRpcFixedStruct
	{
		public RpcPointer<uint> pdwVal;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pdwVal);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pdwVal = decoder.ReadUniquePointer<uint>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pdwVal is not null)
			{
				encoder.WriteValue(this.pdwVal.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pdwVal is not null)
			{
				this.pdwVal.value = decoder.ReadUInt32();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE : int
	{
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_INVALID = 0,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_ENFORCED = 1,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_ERROR = 2,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_ENFORCED_EMPTY_RESOLUTION = 3,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_PORT_NOT_FOUND = 4,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_RULE_INACTIVE = 5,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_VM_CREATOR_NOT_APPLICABLE = 6,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_PROFILE_NOT_APPLICABLE = 7,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_PROFILE_DISABLED = 8,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_PROFILE_LOCAL_RULES_DISALLOWED = 9,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_CONSTRAINED_INTERFACE_NOT_APPLICABLE = 10,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_NAT_INBOUND_NOT_APPLICABLE = 11,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_NAT_LOCAL_ADDRESS_NOT_SUPPORTED = 12,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_HOST_POLICY_MERGE_DISABLED = 13,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_HOST_FIREWALL_PROFILE_DISABLED = 14,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_HOST_FIREWALL_DEFAULT_ACTION_CONFLICT = 15,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_HOST_FIREWALL_PROFILE_LOCAL_POLICY_MERGE_DISABLED = 16,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_HOST_FIREWALL_RULE_CATEGORY_DISABLED = 17,
		FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE_MAX = 18
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_HYPERV_RULE_STATUS : int
	{
		FW_HYPERV_RULE_STATUS_INVALID = 0,
		FW_HYPERV_RULE_STATUS_OK = 1,
		FW_HYPERV_RULE_STATUS_PARTIALLY_ENFORCED = 2,
		FW_HYPERV_RULE_STATUS_NO_APPLICABLE_PORTS = 3,
		FW_HYPERV_RULE_STATUS_PARSING_ERROR = 4,
		FW_HYPERV_RULE_STATUS_ERROR = 5,
		FW_HYPERV_RULE_STATUS_MAX = 6
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum FW_HYPERV_RULE_FLAGS : int
	{
		FW_HYPERV_RULE_FLAGS_NONE = 0,
		FW_HYPERV_RULE_FLAGS_ACTIVE = 1,
		FW_HYPERV_RULE_FLAGS_CONSTRAINED_INTERFACE = 2,
		FW_HYPERV_RULE_FLAGS_MAX_V2_32 = 4,
		FW_HYPERV_RULE_FLAGS_INTERNAL_MIN_PRIORITY = 4,
		FW_HYPERV_RULE_FLAGS_MAX_V2_33 = 8,
		FW_HYPERV_RULE_FLAGS_MAX = 8
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_HYPERV_RULE_METADATA : IRpcFixedStruct
	{
		public RpcPointer<string> switchName;
		public RpcPointer<string> portName;
		public FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE enforcementState;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.switchName);
			encoder.WriteUniquePointer(this.portName);
			encoder.WriteEnumShortValue((short)this.enforcementState);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.switchName = decoder.ReadUniquePointer<string>();
			this.portName = decoder.ReadUniquePointer<string>();
			this.enforcementState = (FW_HYPERV_RULE_PORT_ENFORCEMENT_STATE)decoder.ReadEnumShortValue();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.switchName is not null)
			{
				encoder.WriteWideCharString(this.switchName.value);
			}

			if (this.portName is not null)
			{
				encoder.WriteWideCharString(this.portName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.switchName is not null)
			{
				this.switchName.value = decoder.ReadWideCharString();
			}

			if (this.portName is not null)
			{
				this.portName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_HYPERV_RULE_METADATA_LIST : IRpcFixedStruct
	{
		public uint numEntries;
		public RpcPointer<FW_HYPERV_RULE_METADATA[]> list;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.numEntries);
			encoder.WriteUniquePointer(this.list);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.numEntries = decoder.ReadUInt32();
			this.list = decoder.ReadUniquePointer<FW_HYPERV_RULE_METADATA[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.list is not null)
			{
				encoder.WriteArrayHeader(this.list.value);
				for (int i = 0; i < this.list.value.Length; i++)
				{
					FW_HYPERV_RULE_METADATA elem_0 = this.list.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.list.value.Length; i++)
				{
					FW_HYPERV_RULE_METADATA elem_0 = this.list.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.list is not null)
			{
				this.list.value = decoder.ReadArrayHeader<FW_HYPERV_RULE_METADATA>();
				for (int i = 0; i < this.list.value.Length; i++)
				{
					FW_HYPERV_RULE_METADATA elem_0 = this.list.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_HYPERV_RULE_METADATA>(NdrAlignment.NativePtr);
					this.list.value[i] = elem_0;
				}

				for (int i = 0; i < this.list.value.Length; i++)
				{
					FW_HYPERV_RULE_METADATA elem_0 = this.list.value[i];
					decoder.ReadStructDeferral<FW_HYPERV_RULE_METADATA>(ref elem_0);
					this.list.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_HYPERV_RULE0 : IRpcFixedStruct
	{
		public RpcPointer<FW_HYPERV_RULE0> next;
		public ushort schemaVersion;
		public RpcPointer<string> ruleId;
		public RpcPointer<string> ruleName;
		public ushort priority;
		public FW_DIRECTION direction;
		public Guid vmCreatorId;
		public ushort protocol;
		public FW_ADDRESSES localAddresses;
		public FW_PORTS localPorts;
		public FW_ADDRESSES remoteAddresses;
		public FW_PORTS remotePorts;
		public FW_RULE_ACTION action;
		public ushort flags;
		public FW_HYPERV_RULE_STATUS status;
		public FW_RULE_ORIGIN_TYPE origin;
		public FW_HYPERV_RULE_METADATA_LIST metadataList;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.next);
			encoder.WriteValue(this.schemaVersion);
			encoder.WriteUniquePointer(this.ruleId);
			encoder.WriteUniquePointer(this.ruleName);
			encoder.WriteValue(this.priority);
			encoder.WriteEnumShortValue((short)this.direction);
			encoder.WriteValue(this.vmCreatorId);
			encoder.WriteValue(this.protocol);
			encoder.WriteFixedStruct(this.localAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.localPorts, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.remoteAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.remotePorts, NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.action);
			encoder.WriteValue(this.flags);
			encoder.WriteEnumShortValue((short)this.status);
			encoder.WriteEnumShortValue((short)this.origin);
			encoder.WriteFixedStruct(this.metadataList, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.next = decoder.ReadUniquePointer<FW_HYPERV_RULE0>();
			this.schemaVersion = decoder.ReadUInt16();
			this.ruleId = decoder.ReadUniquePointer<string>();
			this.ruleName = decoder.ReadUniquePointer<string>();
			this.priority = decoder.ReadUInt16();
			this.direction = (FW_DIRECTION)decoder.ReadEnumShortValue();
			this.vmCreatorId = decoder.ReadUuid();
			this.protocol = decoder.ReadUInt16();
			this.localAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.localPorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.remoteAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.remotePorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.action = (FW_RULE_ACTION)decoder.ReadEnumShortValue();
			this.flags = decoder.ReadUInt16();
			this.status = (FW_HYPERV_RULE_STATUS)decoder.ReadEnumShortValue();
			this.origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.metadataList = decoder.ReadFixedStruct<FW_HYPERV_RULE_METADATA_LIST>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.next is not null)
			{
				encoder.WriteFixedStruct(this.next.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.next.value);
			}

			if (this.ruleId is not null)
			{
				encoder.WriteWideCharString(this.ruleId.value);
			}

			if (this.ruleName is not null)
			{
				encoder.WriteWideCharString(this.ruleName.value);
			}

			encoder.WriteStructDeferral(this.localAddresses);
			encoder.WriteStructDeferral(this.localPorts);
			encoder.WriteStructDeferral(this.remoteAddresses);
			encoder.WriteStructDeferral(this.remotePorts);
			encoder.WriteStructDeferral(this.metadataList);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.next is not null)
			{
				this.next.value = decoder.ReadFixedStruct<FW_HYPERV_RULE0>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_HYPERV_RULE0>(ref this.next.value);
			}

			if (this.ruleId is not null)
			{
				this.ruleId.value = decoder.ReadWideCharString();
			}

			if (this.ruleName is not null)
			{
				this.ruleName.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.localAddresses);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.localPorts);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.remoteAddresses);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.remotePorts);
			decoder.ReadStructDeferral<FW_HYPERV_RULE_METADATA_LIST>(ref this.metadataList);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct FW_HYPERV_RULE1 : IRpcFixedStruct
	{
		public RpcPointer<FW_HYPERV_RULE1> next;
		public ushort schemaVersion;
		public RpcPointer<string> ruleId;
		public RpcPointer<string> ruleName;
		public ushort priority;
		public FW_DIRECTION direction;
		public Guid vmCreatorId;
		public ushort protocol;
		public FW_ADDRESSES localAddresses;
		public FW_PORTS localPorts;
		public FW_ADDRESSES remoteAddresses;
		public FW_PORTS remotePorts;
		public FW_RULE_ACTION action;
		public ushort flags;
		public FW_HYPERV_RULE_STATUS status;
		public FW_RULE_ORIGIN_TYPE origin;
		public FW_HYPERV_RULE_METADATA_LIST metadataList;
		public uint profileTypes;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.next);
			encoder.WriteValue(this.schemaVersion);
			encoder.WriteUniquePointer(this.ruleId);
			encoder.WriteUniquePointer(this.ruleName);
			encoder.WriteValue(this.priority);
			encoder.WriteEnumShortValue((short)this.direction);
			encoder.WriteValue(this.vmCreatorId);
			encoder.WriteValue(this.protocol);
			encoder.WriteFixedStruct(this.localAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.localPorts, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.remoteAddresses, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.remotePorts, NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.action);
			encoder.WriteValue(this.flags);
			encoder.WriteEnumShortValue((short)this.status);
			encoder.WriteEnumShortValue((short)this.origin);
			encoder.WriteFixedStruct(this.metadataList, NdrAlignment.NativePtr);
			encoder.WriteValue(this.profileTypes);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.next = decoder.ReadUniquePointer<FW_HYPERV_RULE1>();
			this.schemaVersion = decoder.ReadUInt16();
			this.ruleId = decoder.ReadUniquePointer<string>();
			this.ruleName = decoder.ReadUniquePointer<string>();
			this.priority = decoder.ReadUInt16();
			this.direction = (FW_DIRECTION)decoder.ReadEnumShortValue();
			this.vmCreatorId = decoder.ReadUuid();
			this.protocol = decoder.ReadUInt16();
			this.localAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.localPorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.remoteAddresses = decoder.ReadFixedStruct<FW_ADDRESSES>(NdrAlignment.NativePtr);
			this.remotePorts = decoder.ReadFixedStruct<FW_PORTS>(NdrAlignment.NativePtr);
			this.action = (FW_RULE_ACTION)decoder.ReadEnumShortValue();
			this.flags = decoder.ReadUInt16();
			this.status = (FW_HYPERV_RULE_STATUS)decoder.ReadEnumShortValue();
			this.origin = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			this.metadataList = decoder.ReadFixedStruct<FW_HYPERV_RULE_METADATA_LIST>(NdrAlignment.NativePtr);
			this.profileTypes = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.next is not null)
			{
				encoder.WriteFixedStruct(this.next.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.next.value);
			}

			if (this.ruleId is not null)
			{
				encoder.WriteWideCharString(this.ruleId.value);
			}

			if (this.ruleName is not null)
			{
				encoder.WriteWideCharString(this.ruleName.value);
			}

			encoder.WriteStructDeferral(this.localAddresses);
			encoder.WriteStructDeferral(this.localPorts);
			encoder.WriteStructDeferral(this.remoteAddresses);
			encoder.WriteStructDeferral(this.remotePorts);
			encoder.WriteStructDeferral(this.metadataList);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.next is not null)
			{
				this.next.value = decoder.ReadFixedStruct<FW_HYPERV_RULE1>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_HYPERV_RULE1>(ref this.next.value);
			}

			if (this.ruleId is not null)
			{
				this.ruleId.value = decoder.ReadWideCharString();
			}

			if (this.ruleName is not null)
			{
				this.ruleName.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.localAddresses);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.localPorts);
			decoder.ReadStructDeferral<FW_ADDRESSES>(ref this.remoteAddresses);
			decoder.ReadStructDeferral<FW_PORTS>(ref this.remotePorts);
			decoder.ReadStructDeferral<FW_HYPERV_RULE_METADATA_LIST>(ref this.metadataList);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11"), GuidAttribute("6b5bdd1e-528c-422c-af8c-a4079be4fe48"), RpcVersionAttribute(1, 0)]
	public partial interface RemoteFW
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWOpenPolicyStore(ushort BinaryVersion, FW_STORE_TYPE StoreType, FW_POLICY_ACCESS_RIGHT AccessRight, uint dwFlags, RpcPointer<RpcContextHandle> phPolicyStore, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWClosePolicyStore(RpcPointer<RpcContextHandle> phPolicyStore, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWRestoreDefaults(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWGetGlobalConfig(ushort BinaryVersion, FW_STORE_TYPE StoreType, FW_GLOBAL_CONFIG configID, uint dwFlags, RpcPointer<ArraySegment<byte>> pBuffer, uint cbData, RpcPointer<uint> pcbTransmittedLen, RpcPointer<uint> pcbRequired, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetGlobalConfig(ushort BinaryVersion, FW_STORE_TYPE StoreType, FW_GLOBAL_CONFIG configID, byte[] lpBuffer, uint dwBufSize, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddFirewallRule(RpcContextHandle hPolicyStore, FW_RULE2_0 pRule, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetFirewallRule(RpcContextHandle hPolicyStore, FW_RULE2_0 pRule, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWDeleteFirewallRule(RpcContextHandle hPolicyStore, string wszRuleID, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWDeleteAllFirewallRules(RpcContextHandle hPolicyStore, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumFirewallRules(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_0>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWGetConfig(RpcContextHandle hPolicyStore, FW_PROFILE_CONFIG configID, FW_PROFILE_TYPE Profile, uint dwFlags, RpcPointer<ArraySegment<byte>> pBuffer, uint cbData, RpcPointer<uint> pcbTransmittedLen, RpcPointer<uint> pcbRequired, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetConfig(RpcContextHandle hPolicyStore, FW_PROFILE_CONFIG configID, FW_PROFILE_TYPE Profile, FW_PROFILE_CONFIG_VALUE pConfig, uint dwBufSize, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddConnectionSecurityRule(RpcContextHandle hPolicyStore, FW_CS_RULE2_0 pRule, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetConnectionSecurityRule(RpcContextHandle hPolicyStore, FW_CS_RULE2_0 pRule, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWDeleteConnectionSecurityRule(RpcContextHandle hPolicyStore, string pRuleId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWDeleteAllConnectionSecurityRules(RpcContextHandle hPolicyStore, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumConnectionSecurityRules(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_CS_RULE2_0>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddAuthenticationSet(RpcContextHandle hPolicyStore, FW_AUTH_SET2_10 pAuth, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetAuthenticationSet(RpcContextHandle hPolicyStore, FW_AUTH_SET2_10 pAuth, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWDeleteAuthenticationSet(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, string wszSetId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWDeleteAllAuthenticationSets(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumAuthenticationSets(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, uint dwFilteredByStatus, ushort wFlags, RpcPointer<uint> pdwNumAuthSets, RpcPointer<RpcPointer<FW_AUTH_SET2_10>> ppAuth, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddCryptoSet(RpcContextHandle hPolicyStore, FW_CRYPTO_SET pCrypto, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetCryptoSet(RpcContextHandle hPolicyStore, FW_CRYPTO_SET pCrypto, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWDeleteCryptoSet(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, string wszSetId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWDeleteAllCryptoSets(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumCryptoSets(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, uint dwFilteredByStatus, ushort wFlags, RpcPointer<uint> pdwNumSets, RpcPointer<RpcPointer<FW_CRYPTO_SET>> ppCryptoSets, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumPhase1SAs(RpcContextHandle hPolicyStore, RpcPointer<FW_ENDPOINTS> pEndpoints, RpcPointer<uint> pdwNumSAs, RpcPointer<RpcPointer<FW_PHASE1_SA_DETAILS[]>> ppSAs, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumPhase2SAs(RpcContextHandle hPolicyStore, RpcPointer<FW_ENDPOINTS> pEndpoints, RpcPointer<uint> pdwNumSAs, RpcPointer<RpcPointer<FW_PHASE2_SA_DETAILS[]>> ppSAs, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWDeletePhase1SAs(RpcContextHandle hPolicyStore, RpcPointer<FW_ENDPOINTS> pEndpoints, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWDeletePhase2SAs(RpcContextHandle hPolicyStore, RpcPointer<FW_ENDPOINTS> pEndpoints, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumProducts(RpcContextHandle hPolicyStore, RpcPointer<uint> pdwNumProducts, RpcPointer<RpcPointer<FW_PRODUCT[]>> ppProducts, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddMainModeRule(RpcContextHandle hPolicyStore, FW_MM_RULE pMMRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetMainModeRule(RpcContextHandle hPolicyStore, FW_MM_RULE pMMRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWDeleteMainModeRule(RpcContextHandle hPolicyStore, string pRuleId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWDeleteAllMainModeRules(RpcContextHandle hPolicyStore, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumMainModeRules(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_MM_RULE>> ppMMRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryFirewallRules(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_10>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryConnectionSecurityRules2_10(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_CS_RULE2_10>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryMainModeRules(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_MM_RULE>> ppMMRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryAuthenticationSets(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IPsecPhase, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumSets, RpcPointer<RpcPointer<FW_AUTH_SET2_10>> ppAuthSets, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryCryptoSets(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IPsecPhase, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumSets, RpcPointer<RpcPointer<FW_CRYPTO_SET>> ppCryptoSets, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumNetworks(RpcContextHandle hPolicyStore, RpcPointer<uint> pdwNumNetworks, RpcPointer<RpcPointer<FW_NETWORK[]>> ppNetworks, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumAdapters(RpcContextHandle hPolicyStore, RpcPointer<uint> pdwNumAdapters, RpcPointer<RpcPointer<FW_ADAPTER[]>> ppAdapters, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWGetGlobalConfig2_10(ushort BinaryVersion, FW_STORE_TYPE StoreType, FW_GLOBAL_CONFIG configID, uint dwFlags, RpcPointer<ArraySegment<byte>> pBuffer, uint cbData, RpcPointer<uint> pcbTransmittedLen, RpcPointer<uint> pcbRequired, RpcPointer<FW_RULE_ORIGIN_TYPE> pOrigin, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWGetConfig2_10(RpcContextHandle hPolicyStore, FW_PROFILE_CONFIG configID, FW_PROFILE_TYPE Profile, uint dwFlags, RpcPointer<ArraySegment<byte>> pBuffer, uint cbData, RpcPointer<uint> pcbTransmittedLen, RpcPointer<uint> pcbRequired, RpcPointer<FW_RULE_ORIGIN_TYPE> pOrigin, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddFirewallRule2_10(RpcContextHandle hPolicyStore, FW_RULE2_10 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetFirewallRule2_10(RpcContextHandle hPolicyStore, FW_RULE2_10 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumFirewallRules2_10(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_10>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddConnectionSecurityRule2_10(RpcContextHandle hPolicyStore, FW_CS_RULE2_10 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetConnectionSecurityRule2_10(RpcContextHandle hPolicyStore, FW_CS_RULE2_10 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumConnectionSecurityRules2_10(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_CS_RULE2_10>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddAuthenticationSet2_10(RpcContextHandle hPolicyStore, FW_AUTH_SET2_10 pAuth, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetAuthenticationSet2_10(RpcContextHandle hPolicyStore, FW_AUTH_SET2_10 pAuth, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumAuthenticationSets2_10(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, uint dwFilteredByStatus, ushort wFlags, RpcPointer<uint> pdwNumAuthSets, RpcPointer<RpcPointer<FW_AUTH_SET2_10>> ppAuth, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddCryptoSet2_10(RpcContextHandle hPolicyStore, FW_CRYPTO_SET pCrypto, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetCryptoSet2_10(RpcContextHandle hPolicyStore, FW_CRYPTO_SET pCrypto, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumCryptoSets2_10(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, uint dwFilteredByStatus, ushort wFlags, RpcPointer<uint> pdwNumSets, RpcPointer<RpcPointer<FW_CRYPTO_SET>> ppCryptoSets, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddConnectionSecurityRule2_20(RpcContextHandle hPolicyStore, FW_CS_RULE pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetConnectionSecurityRule2_20(RpcContextHandle hPolicyStore, FW_CS_RULE pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumConnectionSecurityRules2_20(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_CS_RULE>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryConnectionSecurityRules2_20(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_CS_RULE>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddAuthenticationSet2_20(RpcContextHandle hPolicyStore, FW_AUTH_SET pAuth, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetAuthenticationSet2_20(RpcContextHandle hPolicyStore, FW_AUTH_SET pAuth, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumAuthenticationSets2_20(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, uint dwFilteredByStatus, ushort wFlags, RpcPointer<uint> pdwNumAuthSets, RpcPointer<RpcPointer<FW_AUTH_SET>> ppAuth, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryAuthenticationSets2_20(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IPsecPhase, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumSets, RpcPointer<RpcPointer<FW_AUTH_SET>> ppAuthSets, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddFirewallRule2_20(RpcContextHandle hPolicyStore, FW_RULE2_20 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetFirewallRule2_20(RpcContextHandle hPolicyStore, FW_RULE2_20 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumFirewallRules2_20(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_20>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryFirewallRules2_20(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_20>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddFirewallRule2_24(RpcContextHandle hPolicyStore, FW_RULE2_24 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetFirewallRule2_24(RpcContextHandle hPolicyStore, FW_RULE2_24 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumFirewallRules2_24(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_24>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryFirewallRules2_24(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_24>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddFirewallRule2_25(RpcContextHandle hPolicyStore, FW_RULE2_25 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetFirewallRule2_25(RpcContextHandle hPolicyStore, FW_RULE2_25 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumFirewallRules2_25(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_25>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryFirewallRules2_25(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_25>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddFirewallRule2_26(RpcContextHandle hPolicyStore, FW_RULE2_26 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetFirewallRule2_26(RpcContextHandle hPolicyStore, FW_RULE2_26 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumFirewallRules2_26(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_26>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryFirewallRules2_26(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_26>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddFirewallRule2_27(RpcContextHandle hPolicyStore, FW_RULE2_27 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetFirewallRule2_27(RpcContextHandle hPolicyStore, FW_RULE2_27 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumFirewallRules2_27(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_27>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryFirewallRules2_27(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_27>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddFirewallRule2_31(RpcContextHandle hPolicyStore, FW_RULE2_31 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetFirewallRule2_31(RpcContextHandle hPolicyStore, FW_RULE2_31 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumFirewallRules2_31(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_31>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryFirewallRules2_31(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_31>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWAddFirewallRule2_33(RpcContextHandle hPolicyStore, FW_RULE pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWSetFirewallRule2_33(RpcContextHandle hPolicyStore, FW_RULE pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWEnumFirewallRules2_33(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE>> ppRules, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> RRPC_FWQueryFirewallRules2_33(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE>> ppRules, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11"), IidAttribute("6b5bdd1e-528c-422c-af8c-a4079be4fe48")]
	public partial class RemoteFWClientProxy : Titanis.DceRpc.Client.RpcClientProxy, RemoteFW, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWOpenPolicyStore(ushort BinaryVersion, FW_STORE_TYPE StoreType, FW_POLICY_ACCESS_RIGHT AccessRight, uint dwFlags, RpcPointer<RpcContextHandle> phPolicyStore, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(BinaryVersion);
			encoder.WriteEnumShortValue((short)StoreType);
			encoder.WriteEnumShortValue((short)AccessRight);
			encoder.WriteValue(dwFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phPolicyStore.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWClosePolicyStore(RpcPointer<RpcContextHandle> phPolicyStore, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(phPolicyStore.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phPolicyStore.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWRestoreDefaults(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWGetGlobalConfig(ushort BinaryVersion, FW_STORE_TYPE StoreType, FW_GLOBAL_CONFIG configID, uint dwFlags, RpcPointer<ArraySegment<byte>> pBuffer, uint cbData, RpcPointer<uint> pcbTransmittedLen, RpcPointer<uint> pcbRequired, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(BinaryVersion);
			encoder.WriteEnumShortValue((short)StoreType);
			encoder.WriteEnumShortValue((short)configID);
			encoder.WriteValue(dwFlags);
			encoder.WriteUniquePointer(pBuffer);
			if (pBuffer is not null)
			{
				encoder.WriteArrayHeader(pBuffer.value, true);
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(cbData);
			encoder.WriteValue(pcbTransmittedLen.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pBuffer = decoder.ReadOutUniquePointer<ArraySegment<byte>>(pBuffer);
			if (pBuffer is not null)
			{
				pBuffer.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					elem_0 = decoder.ReadByte();
					pBuffer.value.Item(i) = elem_0;
				}
			}

			pcbTransmittedLen.value = decoder.ReadUInt32();
			pcbRequired.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetGlobalConfig(ushort BinaryVersion, FW_STORE_TYPE StoreType, FW_GLOBAL_CONFIG configID, byte[] lpBuffer, uint dwBufSize, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(BinaryVersion);
			encoder.WriteEnumShortValue((short)StoreType);
			encoder.WriteEnumShortValue((short)configID);
			encoder.WriteUniqueReferentId(lpBuffer is null);
			if (lpBuffer is not null)
			{
				encoder.WriteArrayHeader(lpBuffer);
				for (int i = 0; i < lpBuffer.Length; i++)
				{
					byte elem_0 = lpBuffer[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddFirewallRule(RpcContextHandle hPolicyStore, FW_RULE2_0 pRule, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetFirewallRule(RpcContextHandle hPolicyStore, FW_RULE2_0 pRule, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWDeleteFirewallRule(RpcContextHandle hPolicyStore, string wszRuleID, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteWideCharString(wszRuleID);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWDeleteAllFirewallRules(RpcContextHandle hPolicyStore, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumFirewallRules(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_0>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_0>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_0>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_0>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWGetConfig(RpcContextHandle hPolicyStore, FW_PROFILE_CONFIG configID, FW_PROFILE_TYPE Profile, uint dwFlags, RpcPointer<ArraySegment<byte>> pBuffer, uint cbData, RpcPointer<uint> pcbTransmittedLen, RpcPointer<uint> pcbRequired, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)configID);
			encoder.WriteValue((int)Profile);
			encoder.WriteValue(dwFlags);
			encoder.WriteUniquePointer(pBuffer);
			if (pBuffer is not null)
			{
				encoder.WriteArrayHeader(pBuffer.value, true);
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(cbData);
			encoder.WriteValue(pcbTransmittedLen.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pBuffer = decoder.ReadOutUniquePointer<ArraySegment<byte>>(pBuffer);
			if (pBuffer is not null)
			{
				pBuffer.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					elem_0 = decoder.ReadByte();
					pBuffer.value.Item(i) = elem_0;
				}
			}

			pcbTransmittedLen.value = decoder.ReadUInt32();
			pcbRequired.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetConfig(RpcContextHandle hPolicyStore, FW_PROFILE_CONFIG configID, FW_PROFILE_TYPE Profile, FW_PROFILE_CONFIG_VALUE pConfig, uint dwBufSize, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)configID);
			encoder.WriteValue((int)Profile);
			encoder.WriteUnion(pConfig);
			encoder.WriteStructDeferral(pConfig);
			encoder.WriteValue(dwBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddConnectionSecurityRule(RpcContextHandle hPolicyStore, FW_CS_RULE2_0 pRule, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetConnectionSecurityRule(RpcContextHandle hPolicyStore, FW_CS_RULE2_0 pRule, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWDeleteConnectionSecurityRule(RpcContextHandle hPolicyStore, string pRuleId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteWideCharString(pRuleId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWDeleteAllConnectionSecurityRules(RpcContextHandle hPolicyStore, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumConnectionSecurityRules(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_CS_RULE2_0>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_CS_RULE2_0>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_CS_RULE2_0>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CS_RULE2_0>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddAuthenticationSet(RpcContextHandle hPolicyStore, FW_AUTH_SET2_10 pAuth, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pAuth, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pAuth);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetAuthenticationSet(RpcContextHandle hPolicyStore, FW_AUTH_SET2_10 pAuth, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pAuth, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pAuth);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWDeleteAuthenticationSet(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, string wszSetId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)IpSecPhase);
			encoder.WriteWideCharString(wszSetId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWDeleteAllAuthenticationSets(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)IpSecPhase);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumAuthenticationSets(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, uint dwFilteredByStatus, ushort wFlags, RpcPointer<uint> pdwNumAuthSets, RpcPointer<RpcPointer<FW_AUTH_SET2_10>> ppAuth, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)IpSecPhase);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumAuthSets.value = decoder.ReadUInt32();
			ppAuth.value = decoder.ReadOutUniquePointer<FW_AUTH_SET2_10>(ppAuth.value);
			if (ppAuth.value is not null)
			{
				ppAuth.value.value = decoder.ReadFixedStruct<FW_AUTH_SET2_10>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_AUTH_SET2_10>(ref ppAuth.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddCryptoSet(RpcContextHandle hPolicyStore, FW_CRYPTO_SET pCrypto, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pCrypto, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pCrypto);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetCryptoSet(RpcContextHandle hPolicyStore, FW_CRYPTO_SET pCrypto, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pCrypto, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pCrypto);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWDeleteCryptoSet(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, string wszSetId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)IpSecPhase);
			encoder.WriteWideCharString(wszSetId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWDeleteAllCryptoSets(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)IpSecPhase);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumCryptoSets(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, uint dwFilteredByStatus, ushort wFlags, RpcPointer<uint> pdwNumSets, RpcPointer<RpcPointer<FW_CRYPTO_SET>> ppCryptoSets, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)IpSecPhase);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumSets.value = decoder.ReadUInt32();
			ppCryptoSets.value = decoder.ReadOutUniquePointer<FW_CRYPTO_SET>(ppCryptoSets.value);
			if (ppCryptoSets.value is not null)
			{
				ppCryptoSets.value.value = decoder.ReadFixedStruct<FW_CRYPTO_SET>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CRYPTO_SET>(ref ppCryptoSets.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumPhase1SAs(RpcContextHandle hPolicyStore, RpcPointer<FW_ENDPOINTS> pEndpoints, RpcPointer<uint> pdwNumSAs, RpcPointer<RpcPointer<FW_PHASE1_SA_DETAILS[]>> ppSAs, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteUniquePointer(pEndpoints);
			if (pEndpoints is not null)
			{
				encoder.WriteFixedStruct(pEndpoints.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pEndpoints.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumSAs.value = decoder.ReadUInt32();
			ppSAs.value = decoder.ReadOutUniquePointer<FW_PHASE1_SA_DETAILS[]>(ppSAs.value);
			if (ppSAs.value is not null)
			{
				ppSAs.value.value = decoder.ReadArrayHeader<FW_PHASE1_SA_DETAILS>();
				for (int i = 0; i < ppSAs.value.value.Length; i++)
				{
					FW_PHASE1_SA_DETAILS elem_0 = ppSAs.value.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_PHASE1_SA_DETAILS>(NdrAlignment._8Byte);
					ppSAs.value.value[i] = elem_0;
				}

				for (int i = 0; i < ppSAs.value.value.Length; i++)
				{
					FW_PHASE1_SA_DETAILS elem_0 = ppSAs.value.value[i];
					decoder.ReadStructDeferral<FW_PHASE1_SA_DETAILS>(ref elem_0);
					ppSAs.value.value[i] = elem_0;
				}
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumPhase2SAs(RpcContextHandle hPolicyStore, RpcPointer<FW_ENDPOINTS> pEndpoints, RpcPointer<uint> pdwNumSAs, RpcPointer<RpcPointer<FW_PHASE2_SA_DETAILS[]>> ppSAs, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteUniquePointer(pEndpoints);
			if (pEndpoints is not null)
			{
				encoder.WriteFixedStruct(pEndpoints.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pEndpoints.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumSAs.value = decoder.ReadUInt32();
			ppSAs.value = decoder.ReadOutUniquePointer<FW_PHASE2_SA_DETAILS[]>(ppSAs.value);
			if (ppSAs.value is not null)
			{
				ppSAs.value.value = decoder.ReadArrayHeader<FW_PHASE2_SA_DETAILS>();
				for (int i = 0; i < ppSAs.value.value.Length; i++)
				{
					FW_PHASE2_SA_DETAILS elem_0 = ppSAs.value.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_PHASE2_SA_DETAILS>(NdrAlignment._8Byte);
					ppSAs.value.value[i] = elem_0;
				}

				for (int i = 0; i < ppSAs.value.value.Length; i++)
				{
					FW_PHASE2_SA_DETAILS elem_0 = ppSAs.value.value[i];
					decoder.ReadStructDeferral<FW_PHASE2_SA_DETAILS>(ref elem_0);
					ppSAs.value.value[i] = elem_0;
				}
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWDeletePhase1SAs(RpcContextHandle hPolicyStore, RpcPointer<FW_ENDPOINTS> pEndpoints, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteUniquePointer(pEndpoints);
			if (pEndpoints is not null)
			{
				encoder.WriteFixedStruct(pEndpoints.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pEndpoints.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWDeletePhase2SAs(RpcContextHandle hPolicyStore, RpcPointer<FW_ENDPOINTS> pEndpoints, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteUniquePointer(pEndpoints);
			if (pEndpoints is not null)
			{
				encoder.WriteFixedStruct(pEndpoints.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pEndpoints.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumProducts(RpcContextHandle hPolicyStore, RpcPointer<uint> pdwNumProducts, RpcPointer<RpcPointer<FW_PRODUCT[]>> ppProducts, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumProducts.value = decoder.ReadUInt32();
			ppProducts.value = decoder.ReadOutUniquePointer<FW_PRODUCT[]>(ppProducts.value);
			if (ppProducts.value is not null)
			{
				ppProducts.value.value = decoder.ReadArrayHeader<FW_PRODUCT>();
				for (int i = 0; i < ppProducts.value.value.Length; i++)
				{
					FW_PRODUCT elem_0 = ppProducts.value.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_PRODUCT>(NdrAlignment.NativePtr);
					ppProducts.value.value[i] = elem_0;
				}

				for (int i = 0; i < ppProducts.value.value.Length; i++)
				{
					FW_PRODUCT elem_0 = ppProducts.value.value[i];
					decoder.ReadStructDeferral<FW_PRODUCT>(ref elem_0);
					ppProducts.value.value[i] = elem_0;
				}
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddMainModeRule(RpcContextHandle hPolicyStore, FW_MM_RULE pMMRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pMMRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pMMRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetMainModeRule(RpcContextHandle hPolicyStore, FW_MM_RULE pMMRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pMMRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pMMRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWDeleteMainModeRule(RpcContextHandle hPolicyStore, string pRuleId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteWideCharString(pRuleId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWDeleteAllMainModeRules(RpcContextHandle hPolicyStore, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumMainModeRules(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_MM_RULE>> ppMMRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(36);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppMMRules.value = decoder.ReadOutUniquePointer<FW_MM_RULE>(ppMMRules.value);
			if (ppMMRules.value is not null)
			{
				ppMMRules.value.value = decoder.ReadFixedStruct<FW_MM_RULE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_MM_RULE>(ref ppMMRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryFirewallRules(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_10>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(37);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_10>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_10>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_10>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryConnectionSecurityRules2_10(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_CS_RULE2_10>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(38);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_CS_RULE2_10>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_CS_RULE2_10>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CS_RULE2_10>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryMainModeRules(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_MM_RULE>> ppMMRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(39);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppMMRules.value = decoder.ReadOutUniquePointer<FW_MM_RULE>(ppMMRules.value);
			if (ppMMRules.value is not null)
			{
				ppMMRules.value.value = decoder.ReadFixedStruct<FW_MM_RULE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_MM_RULE>(ref ppMMRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryAuthenticationSets(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IPsecPhase, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumSets, RpcPointer<RpcPointer<FW_AUTH_SET2_10>> ppAuthSets, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(40);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)IPsecPhase);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumSets.value = decoder.ReadUInt32();
			ppAuthSets.value = decoder.ReadOutUniquePointer<FW_AUTH_SET2_10>(ppAuthSets.value);
			if (ppAuthSets.value is not null)
			{
				ppAuthSets.value.value = decoder.ReadFixedStruct<FW_AUTH_SET2_10>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_AUTH_SET2_10>(ref ppAuthSets.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryCryptoSets(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IPsecPhase, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumSets, RpcPointer<RpcPointer<FW_CRYPTO_SET>> ppCryptoSets, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(41);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)IPsecPhase);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumSets.value = decoder.ReadUInt32();
			ppCryptoSets.value = decoder.ReadOutUniquePointer<FW_CRYPTO_SET>(ppCryptoSets.value);
			if (ppCryptoSets.value is not null)
			{
				ppCryptoSets.value.value = decoder.ReadFixedStruct<FW_CRYPTO_SET>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CRYPTO_SET>(ref ppCryptoSets.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumNetworks(RpcContextHandle hPolicyStore, RpcPointer<uint> pdwNumNetworks, RpcPointer<RpcPointer<FW_NETWORK[]>> ppNetworks, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(42);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumNetworks.value = decoder.ReadUInt32();
			ppNetworks.value = decoder.ReadOutUniquePointer<FW_NETWORK[]>(ppNetworks.value);
			if (ppNetworks.value is not null)
			{
				ppNetworks.value.value = decoder.ReadArrayHeader<FW_NETWORK>();
				for (int i = 0; i < ppNetworks.value.value.Length; i++)
				{
					FW_NETWORK elem_0 = ppNetworks.value.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_NETWORK>(NdrAlignment.NativePtr);
					ppNetworks.value.value[i] = elem_0;
				}

				for (int i = 0; i < ppNetworks.value.value.Length; i++)
				{
					FW_NETWORK elem_0 = ppNetworks.value.value[i];
					decoder.ReadStructDeferral<FW_NETWORK>(ref elem_0);
					ppNetworks.value.value[i] = elem_0;
				}
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumAdapters(RpcContextHandle hPolicyStore, RpcPointer<uint> pdwNumAdapters, RpcPointer<RpcPointer<FW_ADAPTER[]>> ppAdapters, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(43);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumAdapters.value = decoder.ReadUInt32();
			ppAdapters.value = decoder.ReadOutUniquePointer<FW_ADAPTER[]>(ppAdapters.value);
			if (ppAdapters.value is not null)
			{
				ppAdapters.value.value = decoder.ReadArrayHeader<FW_ADAPTER>();
				for (int i = 0; i < ppAdapters.value.value.Length; i++)
				{
					FW_ADAPTER elem_0 = ppAdapters.value.value[i];
					elem_0 = decoder.ReadFixedStruct<FW_ADAPTER>(NdrAlignment.NativePtr);
					ppAdapters.value.value[i] = elem_0;
				}

				for (int i = 0; i < ppAdapters.value.value.Length; i++)
				{
					FW_ADAPTER elem_0 = ppAdapters.value.value[i];
					decoder.ReadStructDeferral<FW_ADAPTER>(ref elem_0);
					ppAdapters.value.value[i] = elem_0;
				}
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWGetGlobalConfig2_10(ushort BinaryVersion, FW_STORE_TYPE StoreType, FW_GLOBAL_CONFIG configID, uint dwFlags, RpcPointer<ArraySegment<byte>> pBuffer, uint cbData, RpcPointer<uint> pcbTransmittedLen, RpcPointer<uint> pcbRequired, RpcPointer<FW_RULE_ORIGIN_TYPE> pOrigin, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(44);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(BinaryVersion);
			encoder.WriteEnumShortValue((short)StoreType);
			encoder.WriteEnumShortValue((short)configID);
			encoder.WriteValue(dwFlags);
			encoder.WriteUniquePointer(pBuffer);
			if (pBuffer is not null)
			{
				encoder.WriteArrayHeader(pBuffer.value, true);
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(cbData);
			encoder.WriteValue(pcbTransmittedLen.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pBuffer = decoder.ReadOutUniquePointer<ArraySegment<byte>>(pBuffer);
			if (pBuffer is not null)
			{
				pBuffer.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					elem_0 = decoder.ReadByte();
					pBuffer.value.Item(i) = elem_0;
				}
			}

			pcbTransmittedLen.value = decoder.ReadUInt32();
			pcbRequired.value = decoder.ReadUInt32();
			pOrigin.value = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWGetConfig2_10(RpcContextHandle hPolicyStore, FW_PROFILE_CONFIG configID, FW_PROFILE_TYPE Profile, uint dwFlags, RpcPointer<ArraySegment<byte>> pBuffer, uint cbData, RpcPointer<uint> pcbTransmittedLen, RpcPointer<uint> pcbRequired, RpcPointer<FW_RULE_ORIGIN_TYPE> pOrigin, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(45);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)configID);
			encoder.WriteValue((int)Profile);
			encoder.WriteValue(dwFlags);
			encoder.WriteUniquePointer(pBuffer);
			if (pBuffer is not null)
			{
				encoder.WriteArrayHeader(pBuffer.value, true);
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(cbData);
			encoder.WriteValue(pcbTransmittedLen.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pBuffer = decoder.ReadOutUniquePointer<ArraySegment<byte>>(pBuffer);
			if (pBuffer is not null)
			{
				pBuffer.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					elem_0 = decoder.ReadByte();
					pBuffer.value.Item(i) = elem_0;
				}
			}

			pcbTransmittedLen.value = decoder.ReadUInt32();
			pcbRequired.value = decoder.ReadUInt32();
			pOrigin.value = (FW_RULE_ORIGIN_TYPE)decoder.ReadEnumShortValue();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddFirewallRule2_10(RpcContextHandle hPolicyStore, FW_RULE2_10 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(46);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetFirewallRule2_10(RpcContextHandle hPolicyStore, FW_RULE2_10 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(47);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumFirewallRules2_10(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_10>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(48);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_10>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_10>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_10>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddConnectionSecurityRule2_10(RpcContextHandle hPolicyStore, FW_CS_RULE2_10 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(49);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetConnectionSecurityRule2_10(RpcContextHandle hPolicyStore, FW_CS_RULE2_10 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(50);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumConnectionSecurityRules2_10(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_CS_RULE2_10>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(51);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_CS_RULE2_10>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_CS_RULE2_10>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CS_RULE2_10>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddAuthenticationSet2_10(RpcContextHandle hPolicyStore, FW_AUTH_SET2_10 pAuth, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(52);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pAuth, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pAuth);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetAuthenticationSet2_10(RpcContextHandle hPolicyStore, FW_AUTH_SET2_10 pAuth, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(53);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pAuth, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pAuth);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumAuthenticationSets2_10(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, uint dwFilteredByStatus, ushort wFlags, RpcPointer<uint> pdwNumAuthSets, RpcPointer<RpcPointer<FW_AUTH_SET2_10>> ppAuth, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(54);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)IpSecPhase);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumAuthSets.value = decoder.ReadUInt32();
			ppAuth.value = decoder.ReadOutUniquePointer<FW_AUTH_SET2_10>(ppAuth.value);
			if (ppAuth.value is not null)
			{
				ppAuth.value.value = decoder.ReadFixedStruct<FW_AUTH_SET2_10>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_AUTH_SET2_10>(ref ppAuth.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddCryptoSet2_10(RpcContextHandle hPolicyStore, FW_CRYPTO_SET pCrypto, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(55);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pCrypto, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pCrypto);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetCryptoSet2_10(RpcContextHandle hPolicyStore, FW_CRYPTO_SET pCrypto, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(56);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pCrypto, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pCrypto);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumCryptoSets2_10(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, uint dwFilteredByStatus, ushort wFlags, RpcPointer<uint> pdwNumSets, RpcPointer<RpcPointer<FW_CRYPTO_SET>> ppCryptoSets, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(57);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)IpSecPhase);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumSets.value = decoder.ReadUInt32();
			ppCryptoSets.value = decoder.ReadOutUniquePointer<FW_CRYPTO_SET>(ppCryptoSets.value);
			if (ppCryptoSets.value is not null)
			{
				ppCryptoSets.value.value = decoder.ReadFixedStruct<FW_CRYPTO_SET>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CRYPTO_SET>(ref ppCryptoSets.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddConnectionSecurityRule2_20(RpcContextHandle hPolicyStore, FW_CS_RULE pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(58);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetConnectionSecurityRule2_20(RpcContextHandle hPolicyStore, FW_CS_RULE pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(59);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumConnectionSecurityRules2_20(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_CS_RULE>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(60);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_CS_RULE>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_CS_RULE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CS_RULE>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryConnectionSecurityRules2_20(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_CS_RULE>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(61);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_CS_RULE>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_CS_RULE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_CS_RULE>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddAuthenticationSet2_20(RpcContextHandle hPolicyStore, FW_AUTH_SET pAuth, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(62);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pAuth, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pAuth);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetAuthenticationSet2_20(RpcContextHandle hPolicyStore, FW_AUTH_SET pAuth, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(63);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pAuth, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pAuth);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumAuthenticationSets2_20(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IpSecPhase, uint dwFilteredByStatus, ushort wFlags, RpcPointer<uint> pdwNumAuthSets, RpcPointer<RpcPointer<FW_AUTH_SET>> ppAuth, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(64);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)IpSecPhase);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumAuthSets.value = decoder.ReadUInt32();
			ppAuth.value = decoder.ReadOutUniquePointer<FW_AUTH_SET>(ppAuth.value);
			if (ppAuth.value is not null)
			{
				ppAuth.value.value = decoder.ReadFixedStruct<FW_AUTH_SET>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_AUTH_SET>(ref ppAuth.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryAuthenticationSets2_20(RpcContextHandle hPolicyStore, FW_IPSEC_PHASE IPsecPhase, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumSets, RpcPointer<RpcPointer<FW_AUTH_SET>> ppAuthSets, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(65);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteEnumShortValue((short)IPsecPhase);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumSets.value = decoder.ReadUInt32();
			ppAuthSets.value = decoder.ReadOutUniquePointer<FW_AUTH_SET>(ppAuthSets.value);
			if (ppAuthSets.value is not null)
			{
				ppAuthSets.value.value = decoder.ReadFixedStruct<FW_AUTH_SET>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_AUTH_SET>(ref ppAuthSets.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddFirewallRule2_20(RpcContextHandle hPolicyStore, FW_RULE2_20 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(66);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetFirewallRule2_20(RpcContextHandle hPolicyStore, FW_RULE2_20 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(67);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumFirewallRules2_20(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_20>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(68);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_20>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_20>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_20>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryFirewallRules2_20(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_20>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(69);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_20>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_20>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_20>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddFirewallRule2_24(RpcContextHandle hPolicyStore, FW_RULE2_24 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(70);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetFirewallRule2_24(RpcContextHandle hPolicyStore, FW_RULE2_24 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(71);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumFirewallRules2_24(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_24>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(72);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_24>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_24>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_24>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryFirewallRules2_24(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_24>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(73);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_24>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_24>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_24>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddFirewallRule2_25(RpcContextHandle hPolicyStore, FW_RULE2_25 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(74);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetFirewallRule2_25(RpcContextHandle hPolicyStore, FW_RULE2_25 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(75);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumFirewallRules2_25(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_25>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(76);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_25>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_25>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_25>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryFirewallRules2_25(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_25>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(77);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_25>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_25>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_25>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddFirewallRule2_26(RpcContextHandle hPolicyStore, FW_RULE2_26 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(78);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetFirewallRule2_26(RpcContextHandle hPolicyStore, FW_RULE2_26 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(79);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumFirewallRules2_26(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_26>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(80);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_26>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_26>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_26>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryFirewallRules2_26(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_26>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(81);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_26>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_26>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_26>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddFirewallRule2_27(RpcContextHandle hPolicyStore, FW_RULE2_27 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(82);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetFirewallRule2_27(RpcContextHandle hPolicyStore, FW_RULE2_27 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(83);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumFirewallRules2_27(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_27>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(84);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_27>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_27>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_27>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryFirewallRules2_27(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_27>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(85);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_27>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_27>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_27>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddFirewallRule2_31(RpcContextHandle hPolicyStore, FW_RULE2_31 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(86);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetFirewallRule2_31(RpcContextHandle hPolicyStore, FW_RULE2_31 pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(87);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumFirewallRules2_31(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_31>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(88);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_31>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_31>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_31>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryFirewallRules2_31(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE2_31>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(89);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE2_31>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE2_31>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE2_31>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWAddFirewallRule2_33(RpcContextHandle hPolicyStore, FW_RULE pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(90);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWSetFirewallRule2_33(RpcContextHandle hPolicyStore, FW_RULE pRule, RpcPointer<FW_RULE_STATUS> pStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(91);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pRule, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRule);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStatus.value = (FW_RULE_STATUS)decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWEnumFirewallRules2_33(RpcContextHandle hPolicyStore, uint dwFilteredByStatus, uint dwProfileFilter, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(92);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteValue(dwFilteredByStatus);
			encoder.WriteValue(dwProfileFilter);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> RRPC_FWQueryFirewallRules2_33(RpcContextHandle hPolicyStore, FW_QUERY pQuery, ushort wFlags, RpcPointer<uint> pdwNumRules, RpcPointer<RpcPointer<FW_RULE>> ppRules, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(93);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hPolicyStore);
			encoder.WriteFixedStruct(pQuery, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pQuery);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwNumRules.value = decoder.ReadUInt32();
			ppRules.value = decoder.ReadOutUniquePointer<FW_RULE>(ppRules.value);
			if (ppRules.value is not null)
			{
				ppRules.value.value = decoder.ReadFixedStruct<FW_RULE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FW_RULE>(ref ppRules.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(RemoteFW);
		private static Guid _interfaceUuid = new Guid("6b5bdd1e-528c-422c-af8c-a4079be4fe48");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial class RemoteFWStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWOpenPolicyStore(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ushort BinaryVersion;
			FW_STORE_TYPE StoreType;
			FW_POLICY_ACCESS_RIGHT AccessRight;
			uint dwFlags;
			RpcPointer<RpcContextHandle> phPolicyStore = new RpcPointer<RpcContextHandle>();
			BinaryVersion = decoder.ReadUInt16();
			StoreType = (FW_STORE_TYPE)decoder.ReadEnumShortValue();
			AccessRight = (FW_POLICY_ACCESS_RIGHT)decoder.ReadEnumShortValue();
			dwFlags = decoder.ReadUInt32();
			var invokeTask = this._obj.RRPC_FWOpenPolicyStore(BinaryVersion, StoreType, AccessRight, dwFlags, phPolicyStore, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phPolicyStore.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWClosePolicyStore(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> phPolicyStore;
			phPolicyStore = new RpcPointer<RpcContextHandle>();
			phPolicyStore.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.RRPC_FWClosePolicyStore(phPolicyStore, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phPolicyStore.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWRestoreDefaults(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.RRPC_FWRestoreDefaults(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWGetGlobalConfig(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ushort BinaryVersion;
			FW_STORE_TYPE StoreType;
			FW_GLOBAL_CONFIG configID;
			uint dwFlags;
			RpcPointer<ArraySegment<byte>> pBuffer;
			uint cbData;
			RpcPointer<uint> pcbTransmittedLen;
			RpcPointer<uint> pcbRequired = new RpcPointer<uint>();
			BinaryVersion = decoder.ReadUInt16();
			StoreType = (FW_STORE_TYPE)decoder.ReadEnumShortValue();
			configID = (FW_GLOBAL_CONFIG)decoder.ReadEnumShortValue();
			dwFlags = decoder.ReadUInt32();
			pBuffer = decoder.ReadUniquePointer<ArraySegment<byte>>();
			if (pBuffer is not null)
			{
				pBuffer.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					elem_0 = decoder.ReadByte();
					pBuffer.value.Item(i) = elem_0;
				}
			}

			cbData = decoder.ReadUInt32();
			pcbTransmittedLen = new RpcPointer<uint>();
			pcbTransmittedLen.value = decoder.ReadUInt32();
			var invokeTask = this._obj.RRPC_FWGetGlobalConfig(BinaryVersion, StoreType, configID, dwFlags, pBuffer, cbData, pcbTransmittedLen, pcbRequired, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pBuffer);
			if (pBuffer is not null)
			{
				encoder.WriteArrayHeader(pBuffer.value, true);
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(pcbTransmittedLen.value);
			encoder.WriteValue(pcbRequired.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetGlobalConfig(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ushort BinaryVersion;
			FW_STORE_TYPE StoreType;
			FW_GLOBAL_CONFIG configID;
			byte[] lpBuffer;
			uint dwBufSize;
			BinaryVersion = decoder.ReadUInt16();
			StoreType = (FW_STORE_TYPE)decoder.ReadEnumShortValue();
			configID = (FW_GLOBAL_CONFIG)decoder.ReadEnumShortValue();
			lpBuffer = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpBuffer.Length; i++)
			{
				byte elem_0 = lpBuffer[i];
				elem_0 = decoder.ReadByte();
				lpBuffer[i] = elem_0;
			}

			dwBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RRPC_FWSetGlobalConfig(BinaryVersion, StoreType, configID, lpBuffer, dwBufSize, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddFirewallRule(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_0 pRule;
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_0>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_0>(ref pRule);
			var invokeTask = this._obj.RRPC_FWAddFirewallRule(hPolicyStore, pRule, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetFirewallRule(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_0 pRule;
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_0>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_0>(ref pRule);
			var invokeTask = this._obj.RRPC_FWSetFirewallRule(hPolicyStore, pRule, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWDeleteFirewallRule(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			string wszRuleID;
			hPolicyStore = decoder.ReadContextHandle();
			wszRuleID = decoder.ReadWideCharString();
			var invokeTask = this._obj.RRPC_FWDeleteFirewallRule(hPolicyStore, wszRuleID, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWDeleteAllFirewallRules(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			hPolicyStore = decoder.ReadContextHandle();
			var invokeTask = this._obj.RRPC_FWDeleteAllFirewallRules(hPolicyStore, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumFirewallRules(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_0>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_0>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumFirewallRules(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWGetConfig(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_PROFILE_CONFIG configID;
			FW_PROFILE_TYPE Profile;
			uint dwFlags;
			RpcPointer<ArraySegment<byte>> pBuffer;
			uint cbData;
			RpcPointer<uint> pcbTransmittedLen;
			RpcPointer<uint> pcbRequired = new RpcPointer<uint>();
			hPolicyStore = decoder.ReadContextHandle();
			configID = (FW_PROFILE_CONFIG)decoder.ReadEnumShortValue();
			Profile = (FW_PROFILE_TYPE)decoder.ReadInt32();
			dwFlags = decoder.ReadUInt32();
			pBuffer = decoder.ReadUniquePointer<ArraySegment<byte>>();
			if (pBuffer is not null)
			{
				pBuffer.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					elem_0 = decoder.ReadByte();
					pBuffer.value.Item(i) = elem_0;
				}
			}

			cbData = decoder.ReadUInt32();
			pcbTransmittedLen = new RpcPointer<uint>();
			pcbTransmittedLen.value = decoder.ReadUInt32();
			var invokeTask = this._obj.RRPC_FWGetConfig(hPolicyStore, configID, Profile, dwFlags, pBuffer, cbData, pcbTransmittedLen, pcbRequired, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pBuffer);
			if (pBuffer is not null)
			{
				encoder.WriteArrayHeader(pBuffer.value, true);
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(pcbTransmittedLen.value);
			encoder.WriteValue(pcbRequired.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetConfig(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_PROFILE_CONFIG configID;
			FW_PROFILE_TYPE Profile;
			FW_PROFILE_CONFIG_VALUE pConfig;
			uint dwBufSize;
			hPolicyStore = decoder.ReadContextHandle();
			configID = (FW_PROFILE_CONFIG)decoder.ReadEnumShortValue();
			Profile = (FW_PROFILE_TYPE)decoder.ReadInt32();
			pConfig = decoder.ReadUnion<FW_PROFILE_CONFIG_VALUE>();
			decoder.ReadStructDeferral<FW_PROFILE_CONFIG_VALUE>(ref pConfig);
			dwBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RRPC_FWSetConfig(hPolicyStore, configID, Profile, pConfig, dwBufSize, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddConnectionSecurityRule(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_CS_RULE2_0 pRule;
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_CS_RULE2_0>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_CS_RULE2_0>(ref pRule);
			var invokeTask = this._obj.RRPC_FWAddConnectionSecurityRule(hPolicyStore, pRule, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetConnectionSecurityRule(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_CS_RULE2_0 pRule;
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_CS_RULE2_0>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_CS_RULE2_0>(ref pRule);
			var invokeTask = this._obj.RRPC_FWSetConnectionSecurityRule(hPolicyStore, pRule, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWDeleteConnectionSecurityRule(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			string pRuleId;
			hPolicyStore = decoder.ReadContextHandle();
			pRuleId = decoder.ReadWideCharString();
			var invokeTask = this._obj.RRPC_FWDeleteConnectionSecurityRule(hPolicyStore, pRuleId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWDeleteAllConnectionSecurityRules(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			hPolicyStore = decoder.ReadContextHandle();
			var invokeTask = this._obj.RRPC_FWDeleteAllConnectionSecurityRules(hPolicyStore, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumConnectionSecurityRules(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_CS_RULE2_0>> ppRules = new RpcPointer<RpcPointer<FW_CS_RULE2_0>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumConnectionSecurityRules(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddAuthenticationSet(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_AUTH_SET2_10 pAuth;
			hPolicyStore = decoder.ReadContextHandle();
			pAuth = decoder.ReadFixedStruct<FW_AUTH_SET2_10>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_AUTH_SET2_10>(ref pAuth);
			var invokeTask = this._obj.RRPC_FWAddAuthenticationSet(hPolicyStore, pAuth, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetAuthenticationSet(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_AUTH_SET2_10 pAuth;
			hPolicyStore = decoder.ReadContextHandle();
			pAuth = decoder.ReadFixedStruct<FW_AUTH_SET2_10>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_AUTH_SET2_10>(ref pAuth);
			var invokeTask = this._obj.RRPC_FWSetAuthenticationSet(hPolicyStore, pAuth, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWDeleteAuthenticationSet(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_IPSEC_PHASE IpSecPhase;
			string wszSetId;
			hPolicyStore = decoder.ReadContextHandle();
			IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			wszSetId = decoder.ReadWideCharString();
			var invokeTask = this._obj.RRPC_FWDeleteAuthenticationSet(hPolicyStore, IpSecPhase, wszSetId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWDeleteAllAuthenticationSets(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_IPSEC_PHASE IpSecPhase;
			hPolicyStore = decoder.ReadContextHandle();
			IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.RRPC_FWDeleteAllAuthenticationSets(hPolicyStore, IpSecPhase, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumAuthenticationSets(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_IPSEC_PHASE IpSecPhase;
			uint dwFilteredByStatus;
			ushort wFlags;
			RpcPointer<uint> pdwNumAuthSets = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_AUTH_SET2_10>> ppAuth = new RpcPointer<RpcPointer<FW_AUTH_SET2_10>>();
			hPolicyStore = decoder.ReadContextHandle();
			IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			dwFilteredByStatus = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumAuthenticationSets(hPolicyStore, IpSecPhase, dwFilteredByStatus, wFlags, pdwNumAuthSets, ppAuth, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumAuthSets.value);
			encoder.WriteUniquePointer(ppAuth.value);
			if (ppAuth.value is not null)
			{
				encoder.WriteFixedStruct(ppAuth.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppAuth.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddCryptoSet(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_CRYPTO_SET pCrypto;
			hPolicyStore = decoder.ReadContextHandle();
			pCrypto = decoder.ReadFixedStruct<FW_CRYPTO_SET>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_CRYPTO_SET>(ref pCrypto);
			var invokeTask = this._obj.RRPC_FWAddCryptoSet(hPolicyStore, pCrypto, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetCryptoSet(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_CRYPTO_SET pCrypto;
			hPolicyStore = decoder.ReadContextHandle();
			pCrypto = decoder.ReadFixedStruct<FW_CRYPTO_SET>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_CRYPTO_SET>(ref pCrypto);
			var invokeTask = this._obj.RRPC_FWSetCryptoSet(hPolicyStore, pCrypto, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWDeleteCryptoSet(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_IPSEC_PHASE IpSecPhase;
			string wszSetId;
			hPolicyStore = decoder.ReadContextHandle();
			IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			wszSetId = decoder.ReadWideCharString();
			var invokeTask = this._obj.RRPC_FWDeleteCryptoSet(hPolicyStore, IpSecPhase, wszSetId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWDeleteAllCryptoSets(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_IPSEC_PHASE IpSecPhase;
			hPolicyStore = decoder.ReadContextHandle();
			IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.RRPC_FWDeleteAllCryptoSets(hPolicyStore, IpSecPhase, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumCryptoSets(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_IPSEC_PHASE IpSecPhase;
			uint dwFilteredByStatus;
			ushort wFlags;
			RpcPointer<uint> pdwNumSets = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_CRYPTO_SET>> ppCryptoSets = new RpcPointer<RpcPointer<FW_CRYPTO_SET>>();
			hPolicyStore = decoder.ReadContextHandle();
			IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			dwFilteredByStatus = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumCryptoSets(hPolicyStore, IpSecPhase, dwFilteredByStatus, wFlags, pdwNumSets, ppCryptoSets, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumSets.value);
			encoder.WriteUniquePointer(ppCryptoSets.value);
			if (ppCryptoSets.value is not null)
			{
				encoder.WriteFixedStruct(ppCryptoSets.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppCryptoSets.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumPhase1SAs(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			RpcPointer<FW_ENDPOINTS> pEndpoints;
			RpcPointer<uint> pdwNumSAs = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_PHASE1_SA_DETAILS[]>> ppSAs = new RpcPointer<RpcPointer<FW_PHASE1_SA_DETAILS[]>>();
			hPolicyStore = decoder.ReadContextHandle();
			pEndpoints = decoder.ReadUniquePointer<FW_ENDPOINTS>();
			if (pEndpoints is not null)
			{
				pEndpoints.value = decoder.ReadFixedStruct<FW_ENDPOINTS>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FW_ENDPOINTS>(ref pEndpoints.value);
			}

			var invokeTask = this._obj.RRPC_FWEnumPhase1SAs(hPolicyStore, pEndpoints, pdwNumSAs, ppSAs, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumSAs.value);
			encoder.WriteUniquePointer(ppSAs.value);
			if (ppSAs.value is not null)
			{
				encoder.WriteArrayHeader(ppSAs.value.value);
				for (int i = 0; i < ppSAs.value.value.Length; i++)
				{
					FW_PHASE1_SA_DETAILS elem_0 = ppSAs.value.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < ppSAs.value.value.Length; i++)
				{
					FW_PHASE1_SA_DETAILS elem_0 = ppSAs.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumPhase2SAs(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			RpcPointer<FW_ENDPOINTS> pEndpoints;
			RpcPointer<uint> pdwNumSAs = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_PHASE2_SA_DETAILS[]>> ppSAs = new RpcPointer<RpcPointer<FW_PHASE2_SA_DETAILS[]>>();
			hPolicyStore = decoder.ReadContextHandle();
			pEndpoints = decoder.ReadUniquePointer<FW_ENDPOINTS>();
			if (pEndpoints is not null)
			{
				pEndpoints.value = decoder.ReadFixedStruct<FW_ENDPOINTS>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FW_ENDPOINTS>(ref pEndpoints.value);
			}

			var invokeTask = this._obj.RRPC_FWEnumPhase2SAs(hPolicyStore, pEndpoints, pdwNumSAs, ppSAs, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumSAs.value);
			encoder.WriteUniquePointer(ppSAs.value);
			if (ppSAs.value is not null)
			{
				encoder.WriteArrayHeader(ppSAs.value.value);
				for (int i = 0; i < ppSAs.value.value.Length; i++)
				{
					FW_PHASE2_SA_DETAILS elem_0 = ppSAs.value.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < ppSAs.value.value.Length; i++)
				{
					FW_PHASE2_SA_DETAILS elem_0 = ppSAs.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWDeletePhase1SAs(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			RpcPointer<FW_ENDPOINTS> pEndpoints;
			hPolicyStore = decoder.ReadContextHandle();
			pEndpoints = decoder.ReadUniquePointer<FW_ENDPOINTS>();
			if (pEndpoints is not null)
			{
				pEndpoints.value = decoder.ReadFixedStruct<FW_ENDPOINTS>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FW_ENDPOINTS>(ref pEndpoints.value);
			}

			var invokeTask = this._obj.RRPC_FWDeletePhase1SAs(hPolicyStore, pEndpoints, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWDeletePhase2SAs(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			RpcPointer<FW_ENDPOINTS> pEndpoints;
			hPolicyStore = decoder.ReadContextHandle();
			pEndpoints = decoder.ReadUniquePointer<FW_ENDPOINTS>();
			if (pEndpoints is not null)
			{
				pEndpoints.value = decoder.ReadFixedStruct<FW_ENDPOINTS>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FW_ENDPOINTS>(ref pEndpoints.value);
			}

			var invokeTask = this._obj.RRPC_FWDeletePhase2SAs(hPolicyStore, pEndpoints, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumProducts(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			RpcPointer<uint> pdwNumProducts = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_PRODUCT[]>> ppProducts = new RpcPointer<RpcPointer<FW_PRODUCT[]>>();
			hPolicyStore = decoder.ReadContextHandle();
			var invokeTask = this._obj.RRPC_FWEnumProducts(hPolicyStore, pdwNumProducts, ppProducts, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumProducts.value);
			encoder.WriteUniquePointer(ppProducts.value);
			if (ppProducts.value is not null)
			{
				encoder.WriteArrayHeader(ppProducts.value.value);
				for (int i = 0; i < ppProducts.value.value.Length; i++)
				{
					FW_PRODUCT elem_0 = ppProducts.value.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < ppProducts.value.value.Length; i++)
				{
					FW_PRODUCT elem_0 = ppProducts.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddMainModeRule(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_MM_RULE pMMRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pMMRule = decoder.ReadFixedStruct<FW_MM_RULE>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_MM_RULE>(ref pMMRule);
			var invokeTask = this._obj.RRPC_FWAddMainModeRule(hPolicyStore, pMMRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetMainModeRule(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_MM_RULE pMMRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pMMRule = decoder.ReadFixedStruct<FW_MM_RULE>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_MM_RULE>(ref pMMRule);
			var invokeTask = this._obj.RRPC_FWSetMainModeRule(hPolicyStore, pMMRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWDeleteMainModeRule(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			string pRuleId;
			hPolicyStore = decoder.ReadContextHandle();
			pRuleId = decoder.ReadWideCharString();
			var invokeTask = this._obj.RRPC_FWDeleteMainModeRule(hPolicyStore, pRuleId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWDeleteAllMainModeRules(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			hPolicyStore = decoder.ReadContextHandle();
			var invokeTask = this._obj.RRPC_FWDeleteAllMainModeRules(hPolicyStore, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumMainModeRules(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_MM_RULE>> ppMMRules = new RpcPointer<RpcPointer<FW_MM_RULE>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumMainModeRules(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppMMRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppMMRules.value);
			if (ppMMRules.value is not null)
			{
				encoder.WriteFixedStruct(ppMMRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppMMRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryFirewallRules(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_10>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_10>>();
			hPolicyStore = decoder.ReadContextHandle();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryFirewallRules(hPolicyStore, pQuery, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryConnectionSecurityRules2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_CS_RULE2_10>> ppRules = new RpcPointer<RpcPointer<FW_CS_RULE2_10>>();
			hPolicyStore = decoder.ReadContextHandle();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryConnectionSecurityRules2_10(hPolicyStore, pQuery, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryMainModeRules(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_MM_RULE>> ppMMRules = new RpcPointer<RpcPointer<FW_MM_RULE>>();
			hPolicyStore = decoder.ReadContextHandle();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryMainModeRules(hPolicyStore, pQuery, wFlags, pdwNumRules, ppMMRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppMMRules.value);
			if (ppMMRules.value is not null)
			{
				encoder.WriteFixedStruct(ppMMRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppMMRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryAuthenticationSets(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_IPSEC_PHASE IPsecPhase;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumSets = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_AUTH_SET2_10>> ppAuthSets = new RpcPointer<RpcPointer<FW_AUTH_SET2_10>>();
			hPolicyStore = decoder.ReadContextHandle();
			IPsecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryAuthenticationSets(hPolicyStore, IPsecPhase, pQuery, wFlags, pdwNumSets, ppAuthSets, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumSets.value);
			encoder.WriteUniquePointer(ppAuthSets.value);
			if (ppAuthSets.value is not null)
			{
				encoder.WriteFixedStruct(ppAuthSets.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppAuthSets.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryCryptoSets(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_IPSEC_PHASE IPsecPhase;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumSets = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_CRYPTO_SET>> ppCryptoSets = new RpcPointer<RpcPointer<FW_CRYPTO_SET>>();
			hPolicyStore = decoder.ReadContextHandle();
			IPsecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryCryptoSets(hPolicyStore, IPsecPhase, pQuery, wFlags, pdwNumSets, ppCryptoSets, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumSets.value);
			encoder.WriteUniquePointer(ppCryptoSets.value);
			if (ppCryptoSets.value is not null)
			{
				encoder.WriteFixedStruct(ppCryptoSets.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppCryptoSets.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumNetworks(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			RpcPointer<uint> pdwNumNetworks = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_NETWORK[]>> ppNetworks = new RpcPointer<RpcPointer<FW_NETWORK[]>>();
			hPolicyStore = decoder.ReadContextHandle();
			var invokeTask = this._obj.RRPC_FWEnumNetworks(hPolicyStore, pdwNumNetworks, ppNetworks, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumNetworks.value);
			encoder.WriteUniquePointer(ppNetworks.value);
			if (ppNetworks.value is not null)
			{
				encoder.WriteArrayHeader(ppNetworks.value.value);
				for (int i = 0; i < ppNetworks.value.value.Length; i++)
				{
					FW_NETWORK elem_0 = ppNetworks.value.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < ppNetworks.value.value.Length; i++)
				{
					FW_NETWORK elem_0 = ppNetworks.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumAdapters(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			RpcPointer<uint> pdwNumAdapters = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_ADAPTER[]>> ppAdapters = new RpcPointer<RpcPointer<FW_ADAPTER[]>>();
			hPolicyStore = decoder.ReadContextHandle();
			var invokeTask = this._obj.RRPC_FWEnumAdapters(hPolicyStore, pdwNumAdapters, ppAdapters, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumAdapters.value);
			encoder.WriteUniquePointer(ppAdapters.value);
			if (ppAdapters.value is not null)
			{
				encoder.WriteArrayHeader(ppAdapters.value.value);
				for (int i = 0; i < ppAdapters.value.value.Length; i++)
				{
					FW_ADAPTER elem_0 = ppAdapters.value.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < ppAdapters.value.value.Length; i++)
				{
					FW_ADAPTER elem_0 = ppAdapters.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWGetGlobalConfig2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ushort BinaryVersion;
			FW_STORE_TYPE StoreType;
			FW_GLOBAL_CONFIG configID;
			uint dwFlags;
			RpcPointer<ArraySegment<byte>> pBuffer;
			uint cbData;
			RpcPointer<uint> pcbTransmittedLen;
			RpcPointer<uint> pcbRequired = new RpcPointer<uint>();
			RpcPointer<FW_RULE_ORIGIN_TYPE> pOrigin = new RpcPointer<FW_RULE_ORIGIN_TYPE>();
			BinaryVersion = decoder.ReadUInt16();
			StoreType = (FW_STORE_TYPE)decoder.ReadEnumShortValue();
			configID = (FW_GLOBAL_CONFIG)decoder.ReadEnumShortValue();
			dwFlags = decoder.ReadUInt32();
			pBuffer = decoder.ReadUniquePointer<ArraySegment<byte>>();
			if (pBuffer is not null)
			{
				pBuffer.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					elem_0 = decoder.ReadByte();
					pBuffer.value.Item(i) = elem_0;
				}
			}

			cbData = decoder.ReadUInt32();
			pcbTransmittedLen = new RpcPointer<uint>();
			pcbTransmittedLen.value = decoder.ReadUInt32();
			var invokeTask = this._obj.RRPC_FWGetGlobalConfig2_10(BinaryVersion, StoreType, configID, dwFlags, pBuffer, cbData, pcbTransmittedLen, pcbRequired, pOrigin, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pBuffer);
			if (pBuffer is not null)
			{
				encoder.WriteArrayHeader(pBuffer.value, true);
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(pcbTransmittedLen.value);
			encoder.WriteValue(pcbRequired.value);
			encoder.WriteEnumShortValue((short)pOrigin.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWGetConfig2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_PROFILE_CONFIG configID;
			FW_PROFILE_TYPE Profile;
			uint dwFlags;
			RpcPointer<ArraySegment<byte>> pBuffer;
			uint cbData;
			RpcPointer<uint> pcbTransmittedLen;
			RpcPointer<uint> pcbRequired = new RpcPointer<uint>();
			RpcPointer<FW_RULE_ORIGIN_TYPE> pOrigin = new RpcPointer<FW_RULE_ORIGIN_TYPE>();
			hPolicyStore = decoder.ReadContextHandle();
			configID = (FW_PROFILE_CONFIG)decoder.ReadEnumShortValue();
			Profile = (FW_PROFILE_TYPE)decoder.ReadInt32();
			dwFlags = decoder.ReadUInt32();
			pBuffer = decoder.ReadUniquePointer<ArraySegment<byte>>();
			if (pBuffer is not null)
			{
				pBuffer.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					elem_0 = decoder.ReadByte();
					pBuffer.value.Item(i) = elem_0;
				}
			}

			cbData = decoder.ReadUInt32();
			pcbTransmittedLen = new RpcPointer<uint>();
			pcbTransmittedLen.value = decoder.ReadUInt32();
			var invokeTask = this._obj.RRPC_FWGetConfig2_10(hPolicyStore, configID, Profile, dwFlags, pBuffer, cbData, pcbTransmittedLen, pcbRequired, pOrigin, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pBuffer);
			if (pBuffer is not null)
			{
				encoder.WriteArrayHeader(pBuffer.value, true);
				for (int i = 0; i < pBuffer.value.Count; i++)
				{
					byte elem_0 = pBuffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(pcbTransmittedLen.value);
			encoder.WriteValue(pcbRequired.value);
			encoder.WriteEnumShortValue((short)pOrigin.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddFirewallRule2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_10 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_10>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_10>(ref pRule);
			var invokeTask = this._obj.RRPC_FWAddFirewallRule2_10(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetFirewallRule2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_10 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_10>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_10>(ref pRule);
			var invokeTask = this._obj.RRPC_FWSetFirewallRule2_10(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumFirewallRules2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_10>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_10>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumFirewallRules2_10(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddConnectionSecurityRule2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_CS_RULE2_10 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_CS_RULE2_10>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_CS_RULE2_10>(ref pRule);
			var invokeTask = this._obj.RRPC_FWAddConnectionSecurityRule2_10(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetConnectionSecurityRule2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_CS_RULE2_10 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_CS_RULE2_10>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_CS_RULE2_10>(ref pRule);
			var invokeTask = this._obj.RRPC_FWSetConnectionSecurityRule2_10(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumConnectionSecurityRules2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_CS_RULE2_10>> ppRules = new RpcPointer<RpcPointer<FW_CS_RULE2_10>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumConnectionSecurityRules2_10(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddAuthenticationSet2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_AUTH_SET2_10 pAuth;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pAuth = decoder.ReadFixedStruct<FW_AUTH_SET2_10>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_AUTH_SET2_10>(ref pAuth);
			var invokeTask = this._obj.RRPC_FWAddAuthenticationSet2_10(hPolicyStore, pAuth, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetAuthenticationSet2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_AUTH_SET2_10 pAuth;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pAuth = decoder.ReadFixedStruct<FW_AUTH_SET2_10>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_AUTH_SET2_10>(ref pAuth);
			var invokeTask = this._obj.RRPC_FWSetAuthenticationSet2_10(hPolicyStore, pAuth, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumAuthenticationSets2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_IPSEC_PHASE IpSecPhase;
			uint dwFilteredByStatus;
			ushort wFlags;
			RpcPointer<uint> pdwNumAuthSets = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_AUTH_SET2_10>> ppAuth = new RpcPointer<RpcPointer<FW_AUTH_SET2_10>>();
			hPolicyStore = decoder.ReadContextHandle();
			IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			dwFilteredByStatus = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumAuthenticationSets2_10(hPolicyStore, IpSecPhase, dwFilteredByStatus, wFlags, pdwNumAuthSets, ppAuth, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumAuthSets.value);
			encoder.WriteUniquePointer(ppAuth.value);
			if (ppAuth.value is not null)
			{
				encoder.WriteFixedStruct(ppAuth.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppAuth.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddCryptoSet2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_CRYPTO_SET pCrypto;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pCrypto = decoder.ReadFixedStruct<FW_CRYPTO_SET>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_CRYPTO_SET>(ref pCrypto);
			var invokeTask = this._obj.RRPC_FWAddCryptoSet2_10(hPolicyStore, pCrypto, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetCryptoSet2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_CRYPTO_SET pCrypto;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pCrypto = decoder.ReadFixedStruct<FW_CRYPTO_SET>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_CRYPTO_SET>(ref pCrypto);
			var invokeTask = this._obj.RRPC_FWSetCryptoSet2_10(hPolicyStore, pCrypto, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumCryptoSets2_10(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_IPSEC_PHASE IpSecPhase;
			uint dwFilteredByStatus;
			ushort wFlags;
			RpcPointer<uint> pdwNumSets = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_CRYPTO_SET>> ppCryptoSets = new RpcPointer<RpcPointer<FW_CRYPTO_SET>>();
			hPolicyStore = decoder.ReadContextHandle();
			IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			dwFilteredByStatus = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumCryptoSets2_10(hPolicyStore, IpSecPhase, dwFilteredByStatus, wFlags, pdwNumSets, ppCryptoSets, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumSets.value);
			encoder.WriteUniquePointer(ppCryptoSets.value);
			if (ppCryptoSets.value is not null)
			{
				encoder.WriteFixedStruct(ppCryptoSets.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppCryptoSets.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddConnectionSecurityRule2_20(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_CS_RULE pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_CS_RULE>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_CS_RULE>(ref pRule);
			var invokeTask = this._obj.RRPC_FWAddConnectionSecurityRule2_20(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetConnectionSecurityRule2_20(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_CS_RULE pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_CS_RULE>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_CS_RULE>(ref pRule);
			var invokeTask = this._obj.RRPC_FWSetConnectionSecurityRule2_20(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumConnectionSecurityRules2_20(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_CS_RULE>> ppRules = new RpcPointer<RpcPointer<FW_CS_RULE>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumConnectionSecurityRules2_20(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryConnectionSecurityRules2_20(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_CS_RULE>> ppRules = new RpcPointer<RpcPointer<FW_CS_RULE>>();
			hPolicyStore = decoder.ReadContextHandle();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryConnectionSecurityRules2_20(hPolicyStore, pQuery, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddAuthenticationSet2_20(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_AUTH_SET pAuth;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pAuth = decoder.ReadFixedStruct<FW_AUTH_SET>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_AUTH_SET>(ref pAuth);
			var invokeTask = this._obj.RRPC_FWAddAuthenticationSet2_20(hPolicyStore, pAuth, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetAuthenticationSet2_20(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_AUTH_SET pAuth;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pAuth = decoder.ReadFixedStruct<FW_AUTH_SET>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_AUTH_SET>(ref pAuth);
			var invokeTask = this._obj.RRPC_FWSetAuthenticationSet2_20(hPolicyStore, pAuth, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumAuthenticationSets2_20(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_IPSEC_PHASE IpSecPhase;
			uint dwFilteredByStatus;
			ushort wFlags;
			RpcPointer<uint> pdwNumAuthSets = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_AUTH_SET>> ppAuth = new RpcPointer<RpcPointer<FW_AUTH_SET>>();
			hPolicyStore = decoder.ReadContextHandle();
			IpSecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			dwFilteredByStatus = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumAuthenticationSets2_20(hPolicyStore, IpSecPhase, dwFilteredByStatus, wFlags, pdwNumAuthSets, ppAuth, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumAuthSets.value);
			encoder.WriteUniquePointer(ppAuth.value);
			if (ppAuth.value is not null)
			{
				encoder.WriteFixedStruct(ppAuth.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppAuth.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryAuthenticationSets2_20(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_IPSEC_PHASE IPsecPhase;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumSets = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_AUTH_SET>> ppAuthSets = new RpcPointer<RpcPointer<FW_AUTH_SET>>();
			hPolicyStore = decoder.ReadContextHandle();
			IPsecPhase = (FW_IPSEC_PHASE)decoder.ReadEnumShortValue();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryAuthenticationSets2_20(hPolicyStore, IPsecPhase, pQuery, wFlags, pdwNumSets, ppAuthSets, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumSets.value);
			encoder.WriteUniquePointer(ppAuthSets.value);
			if (ppAuthSets.value is not null)
			{
				encoder.WriteFixedStruct(ppAuthSets.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppAuthSets.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddFirewallRule2_20(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_20 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_20>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_20>(ref pRule);
			var invokeTask = this._obj.RRPC_FWAddFirewallRule2_20(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetFirewallRule2_20(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_20 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_20>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_20>(ref pRule);
			var invokeTask = this._obj.RRPC_FWSetFirewallRule2_20(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumFirewallRules2_20(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_20>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_20>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumFirewallRules2_20(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryFirewallRules2_20(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_20>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_20>>();
			hPolicyStore = decoder.ReadContextHandle();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryFirewallRules2_20(hPolicyStore, pQuery, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddFirewallRule2_24(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_24 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_24>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_24>(ref pRule);
			var invokeTask = this._obj.RRPC_FWAddFirewallRule2_24(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetFirewallRule2_24(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_24 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_24>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_24>(ref pRule);
			var invokeTask = this._obj.RRPC_FWSetFirewallRule2_24(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumFirewallRules2_24(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_24>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_24>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumFirewallRules2_24(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryFirewallRules2_24(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_24>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_24>>();
			hPolicyStore = decoder.ReadContextHandle();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryFirewallRules2_24(hPolicyStore, pQuery, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddFirewallRule2_25(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_25 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_25>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_25>(ref pRule);
			var invokeTask = this._obj.RRPC_FWAddFirewallRule2_25(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetFirewallRule2_25(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_25 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_25>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_25>(ref pRule);
			var invokeTask = this._obj.RRPC_FWSetFirewallRule2_25(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumFirewallRules2_25(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_25>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_25>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumFirewallRules2_25(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryFirewallRules2_25(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_25>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_25>>();
			hPolicyStore = decoder.ReadContextHandle();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryFirewallRules2_25(hPolicyStore, pQuery, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddFirewallRule2_26(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_26 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_26>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_26>(ref pRule);
			var invokeTask = this._obj.RRPC_FWAddFirewallRule2_26(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetFirewallRule2_26(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_26 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_26>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_26>(ref pRule);
			var invokeTask = this._obj.RRPC_FWSetFirewallRule2_26(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumFirewallRules2_26(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_26>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_26>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumFirewallRules2_26(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryFirewallRules2_26(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_26>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_26>>();
			hPolicyStore = decoder.ReadContextHandle();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryFirewallRules2_26(hPolicyStore, pQuery, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddFirewallRule2_27(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_27 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_27>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_27>(ref pRule);
			var invokeTask = this._obj.RRPC_FWAddFirewallRule2_27(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetFirewallRule2_27(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_27 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_27>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_27>(ref pRule);
			var invokeTask = this._obj.RRPC_FWSetFirewallRule2_27(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumFirewallRules2_27(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_27>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_27>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumFirewallRules2_27(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryFirewallRules2_27(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_27>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_27>>();
			hPolicyStore = decoder.ReadContextHandle();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryFirewallRules2_27(hPolicyStore, pQuery, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddFirewallRule2_31(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_31 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_31>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_31>(ref pRule);
			var invokeTask = this._obj.RRPC_FWAddFirewallRule2_31(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetFirewallRule2_31(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE2_31 pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE2_31>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE2_31>(ref pRule);
			var invokeTask = this._obj.RRPC_FWSetFirewallRule2_31(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumFirewallRules2_31(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_31>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_31>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumFirewallRules2_31(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryFirewallRules2_31(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE2_31>> ppRules = new RpcPointer<RpcPointer<FW_RULE2_31>>();
			hPolicyStore = decoder.ReadContextHandle();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryFirewallRules2_31(hPolicyStore, pQuery, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWAddFirewallRule2_33(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE>(ref pRule);
			var invokeTask = this._obj.RRPC_FWAddFirewallRule2_33(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWSetFirewallRule2_33(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_RULE pRule;
			RpcPointer<FW_RULE_STATUS> pStatus = new RpcPointer<FW_RULE_STATUS>();
			hPolicyStore = decoder.ReadContextHandle();
			pRule = decoder.ReadFixedStruct<FW_RULE>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_RULE>(ref pRule);
			var invokeTask = this._obj.RRPC_FWSetFirewallRule2_33(hPolicyStore, pRule, pStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWEnumFirewallRules2_33(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			uint dwFilteredByStatus;
			uint dwProfileFilter;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE>> ppRules = new RpcPointer<RpcPointer<FW_RULE>>();
			hPolicyStore = decoder.ReadContextHandle();
			dwFilteredByStatus = decoder.ReadUInt32();
			dwProfileFilter = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWEnumFirewallRules2_33(hPolicyStore, dwFilteredByStatus, dwProfileFilter, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_RRPC_FWQueryFirewallRules2_33(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hPolicyStore;
			FW_QUERY pQuery;
			ushort wFlags;
			RpcPointer<uint> pdwNumRules = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FW_RULE>> ppRules = new RpcPointer<RpcPointer<FW_RULE>>();
			hPolicyStore = decoder.ReadContextHandle();
			pQuery = decoder.ReadFixedStruct<FW_QUERY>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FW_QUERY>(ref pQuery);
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.RRPC_FWQueryFirewallRules2_33(hPolicyStore, pQuery, wFlags, pdwNumRules, ppRules, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwNumRules.value);
			encoder.WriteUniquePointer(ppRules.value);
			if (ppRules.value is not null)
			{
				encoder.WriteFixedStruct(ppRules.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppRules.value.value);
			}

			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("6b5bdd1e-528c-422c-af8c-a4079be4fe48");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private RemoteFW _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public RemoteFWStub(RemoteFW obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_RRPC_FWOpenPolicyStore, this.Invoke_RRPC_FWClosePolicyStore, this.Invoke_RRPC_FWRestoreDefaults, this.Invoke_RRPC_FWGetGlobalConfig, this.Invoke_RRPC_FWSetGlobalConfig, this.Invoke_RRPC_FWAddFirewallRule, this.Invoke_RRPC_FWSetFirewallRule, this.Invoke_RRPC_FWDeleteFirewallRule, this.Invoke_RRPC_FWDeleteAllFirewallRules, this.Invoke_RRPC_FWEnumFirewallRules, this.Invoke_RRPC_FWGetConfig, this.Invoke_RRPC_FWSetConfig, this.Invoke_RRPC_FWAddConnectionSecurityRule, this.Invoke_RRPC_FWSetConnectionSecurityRule, this.Invoke_RRPC_FWDeleteConnectionSecurityRule, this.Invoke_RRPC_FWDeleteAllConnectionSecurityRules, this.Invoke_RRPC_FWEnumConnectionSecurityRules, this.Invoke_RRPC_FWAddAuthenticationSet, this.Invoke_RRPC_FWSetAuthenticationSet, this.Invoke_RRPC_FWDeleteAuthenticationSet, this.Invoke_RRPC_FWDeleteAllAuthenticationSets, this.Invoke_RRPC_FWEnumAuthenticationSets, this.Invoke_RRPC_FWAddCryptoSet, this.Invoke_RRPC_FWSetCryptoSet, this.Invoke_RRPC_FWDeleteCryptoSet, this.Invoke_RRPC_FWDeleteAllCryptoSets, this.Invoke_RRPC_FWEnumCryptoSets, this.Invoke_RRPC_FWEnumPhase1SAs, this.Invoke_RRPC_FWEnumPhase2SAs, this.Invoke_RRPC_FWDeletePhase1SAs, this.Invoke_RRPC_FWDeletePhase2SAs, this.Invoke_RRPC_FWEnumProducts, this.Invoke_RRPC_FWAddMainModeRule, this.Invoke_RRPC_FWSetMainModeRule, this.Invoke_RRPC_FWDeleteMainModeRule, this.Invoke_RRPC_FWDeleteAllMainModeRules, this.Invoke_RRPC_FWEnumMainModeRules, this.Invoke_RRPC_FWQueryFirewallRules, this.Invoke_RRPC_FWQueryConnectionSecurityRules2_10, this.Invoke_RRPC_FWQueryMainModeRules, this.Invoke_RRPC_FWQueryAuthenticationSets, this.Invoke_RRPC_FWQueryCryptoSets, this.Invoke_RRPC_FWEnumNetworks, this.Invoke_RRPC_FWEnumAdapters, this.Invoke_RRPC_FWGetGlobalConfig2_10, this.Invoke_RRPC_FWGetConfig2_10, this.Invoke_RRPC_FWAddFirewallRule2_10, this.Invoke_RRPC_FWSetFirewallRule2_10, this.Invoke_RRPC_FWEnumFirewallRules2_10, this.Invoke_RRPC_FWAddConnectionSecurityRule2_10, this.Invoke_RRPC_FWSetConnectionSecurityRule2_10, this.Invoke_RRPC_FWEnumConnectionSecurityRules2_10, this.Invoke_RRPC_FWAddAuthenticationSet2_10, this.Invoke_RRPC_FWSetAuthenticationSet2_10, this.Invoke_RRPC_FWEnumAuthenticationSets2_10, this.Invoke_RRPC_FWAddCryptoSet2_10, this.Invoke_RRPC_FWSetCryptoSet2_10, this.Invoke_RRPC_FWEnumCryptoSets2_10, this.Invoke_RRPC_FWAddConnectionSecurityRule2_20, this.Invoke_RRPC_FWSetConnectionSecurityRule2_20, this.Invoke_RRPC_FWEnumConnectionSecurityRules2_20, this.Invoke_RRPC_FWQueryConnectionSecurityRules2_20, this.Invoke_RRPC_FWAddAuthenticationSet2_20, this.Invoke_RRPC_FWSetAuthenticationSet2_20, this.Invoke_RRPC_FWEnumAuthenticationSets2_20, this.Invoke_RRPC_FWQueryAuthenticationSets2_20, this.Invoke_RRPC_FWAddFirewallRule2_20, this.Invoke_RRPC_FWSetFirewallRule2_20, this.Invoke_RRPC_FWEnumFirewallRules2_20, this.Invoke_RRPC_FWQueryFirewallRules2_20, this.Invoke_RRPC_FWAddFirewallRule2_24, this.Invoke_RRPC_FWSetFirewallRule2_24, this.Invoke_RRPC_FWEnumFirewallRules2_24, this.Invoke_RRPC_FWQueryFirewallRules2_24, this.Invoke_RRPC_FWAddFirewallRule2_25, this.Invoke_RRPC_FWSetFirewallRule2_25, this.Invoke_RRPC_FWEnumFirewallRules2_25, this.Invoke_RRPC_FWQueryFirewallRules2_25, this.Invoke_RRPC_FWAddFirewallRule2_26, this.Invoke_RRPC_FWSetFirewallRule2_26, this.Invoke_RRPC_FWEnumFirewallRules2_26, this.Invoke_RRPC_FWQueryFirewallRules2_26, this.Invoke_RRPC_FWAddFirewallRule2_27, this.Invoke_RRPC_FWSetFirewallRule2_27, this.Invoke_RRPC_FWEnumFirewallRules2_27, this.Invoke_RRPC_FWQueryFirewallRules2_27, this.Invoke_RRPC_FWAddFirewallRule2_31, this.Invoke_RRPC_FWSetFirewallRule2_31, this.Invoke_RRPC_FWEnumFirewallRules2_31, this.Invoke_RRPC_FWQueryFirewallRules2_31, this.Invoke_RRPC_FWAddFirewallRule2_33, this.Invoke_RRPC_FWSetFirewallRule2_33, this.Invoke_RRPC_FWEnumFirewallRules2_33, this.Invoke_RRPC_FWQueryFirewallRules2_33};
		}
	}
}