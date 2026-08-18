namespace ms_dcom
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ORPC_EXTENT : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.data);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.data = decoder.ReadArrayHeader<byte>();
		}

		public Guid id;
		public uint size;
		public byte[] data;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				byte elem_0 = this.data[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				byte elem_0 = this.data[i];
				elem_0 = decoder.ReadByte();
				this.data[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.id);
			encoder.WriteValue(this.size);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.id = decoder.ReadUuid();
			this.size = decoder.ReadUInt32();
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
	public partial struct ORPC_EXTENT_ARRAY : IRpcFixedStruct
	{
		public uint size;
		public uint reserved;
		public RpcPointer<RpcPointer<ORPC_EXTENT>[]> extent;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.size);
			encoder.WriteValue(this.reserved);
			encoder.WriteUniquePointer(this.extent);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.size = decoder.ReadUInt32();
			this.reserved = decoder.ReadUInt32();
			this.extent = decoder.ReadUniquePointer<RpcPointer<ORPC_EXTENT>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.extent is not null)
			{
				encoder.WriteArrayHeader(this.extent.value);
				for (int i = 0; i < this.extent.value.Length; i++)
				{
					RpcPointer<ORPC_EXTENT> elem_0 = this.extent.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.extent.value.Length; i++)
				{
					RpcPointer<ORPC_EXTENT> elem_0 = this.extent.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteConformantStruct(elem_0.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.extent is not null)
			{
				this.extent.value = decoder.ReadArrayHeader<RpcPointer<ORPC_EXTENT>>();
				for (int i = 0; i < this.extent.value.Length; i++)
				{
					RpcPointer<ORPC_EXTENT> elem_0 = this.extent.value[i];
					elem_0 = decoder.ReadUniquePointer<ORPC_EXTENT>();
					this.extent.value[i] = elem_0;
				}

				for (int i = 0; i < this.extent.value.Length; i++)
				{
					RpcPointer<ORPC_EXTENT> elem_0 = this.extent.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadConformantStruct<ORPC_EXTENT>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<ORPC_EXTENT>(ref elem_0.value);
					}

					this.extent.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ORPCTHIS : IRpcFixedStruct
	{
		public COMVERSION version;
		public uint flags;
		public uint reserved1;
		public Guid cid;
		public RpcPointer<ORPC_EXTENT_ARRAY> extensions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.version, NdrAlignment._2Byte);
			encoder.WriteValue(this.flags);
			encoder.WriteValue(this.reserved1);
			encoder.WriteValue(this.cid);
			encoder.WriteUniquePointer(this.extensions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.version = decoder.ReadFixedStruct<COMVERSION>(NdrAlignment._2Byte);
			this.flags = decoder.ReadUInt32();
			this.reserved1 = decoder.ReadUInt32();
			this.cid = decoder.ReadUuid();
			this.extensions = decoder.ReadUniquePointer<ORPC_EXTENT_ARRAY>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.version);
			if (this.extensions is not null)
			{
				encoder.WriteFixedStruct(this.extensions.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.extensions.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<COMVERSION>(ref this.version);
			if (this.extensions is not null)
			{
				this.extensions.value = decoder.ReadFixedStruct<ORPC_EXTENT_ARRAY>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ORPC_EXTENT_ARRAY>(ref this.extensions.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ORPCTHAT : IRpcFixedStruct
	{
		public uint flags;
		public RpcPointer<ORPC_EXTENT_ARRAY> extensions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.flags);
			encoder.WriteUniquePointer(this.extensions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.flags = decoder.ReadUInt32();
			this.extensions = decoder.ReadUniquePointer<ORPC_EXTENT_ARRAY>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.extensions is not null)
			{
				encoder.WriteFixedStruct(this.extensions.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.extensions.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.extensions is not null)
			{
				this.extensions.value = decoder.ReadFixedStruct<ORPC_EXTENT_ARRAY>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ORPC_EXTENT_ARRAY>(ref this.extensions.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DUALSTRINGARRAY : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.aStringArray);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.aStringArray = decoder.ReadArrayHeader<ushort>();
		}

		public ushort wNumEntries;
		public ushort wSecurityOffset;
		public ushort[] aStringArray;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.aStringArray.Length; i++)
			{
				ushort elem_0 = this.aStringArray[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.aStringArray.Length; i++)
			{
				ushort elem_0 = this.aStringArray[i];
				elem_0 = decoder.ReadUInt16();
				this.aStringArray[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wNumEntries);
			encoder.WriteValue(this.wSecurityOffset);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wNumEntries = decoder.ReadUInt16();
			this.wSecurityOffset = decoder.ReadUInt16();
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
	public enum tagCPFLAGS : int
	{
		CPFLAG_PROPAGATE = 1,
		CPFLAG_EXPOSE = 2,
		CPFLAG_ENVOY = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct MInterfacePointer : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.abData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.abData = decoder.ReadArrayHeader<byte>();
		}

		public uint ulCntData;
		public byte[] abData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.abData.Length; i++)
			{
				byte elem_0 = this.abData[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.abData.Length; i++)
			{
				byte elem_0 = this.abData[i];
				elem_0 = decoder.ReadByte();
				this.abData[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ulCntData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ulCntData = decoder.ReadUInt32();
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
	public partial struct ErrorObjectData : IRpcFixedStruct
	{
		public uint dwVersion;
		public uint dwHelpContext;
		public Guid iid;
		public RpcPointer<string> pszSource;
		public RpcPointer<string> pszDescription;
		public RpcPointer<string> pszHelpFile;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwVersion);
			encoder.WriteValue(this.dwHelpContext);
			encoder.WriteValue(this.iid);
			encoder.WriteUniquePointer(this.pszSource);
			encoder.WriteUniquePointer(this.pszDescription);
			encoder.WriteUniquePointer(this.pszHelpFile);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwVersion = decoder.ReadUInt32();
			this.dwHelpContext = decoder.ReadUInt32();
			this.iid = decoder.ReadUuid();
			this.pszSource = decoder.ReadUniquePointer<string>();
			this.pszDescription = decoder.ReadUniquePointer<string>();
			this.pszHelpFile = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszSource is not null)
			{
				encoder.WriteWideCharString(this.pszSource.value);
			}

			if (this.pszDescription is not null)
			{
				encoder.WriteWideCharString(this.pszDescription.value);
			}

			if (this.pszHelpFile is not null)
			{
				encoder.WriteWideCharString(this.pszHelpFile.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszSource is not null)
			{
				this.pszSource.value = decoder.ReadWideCharString();
			}

			if (this.pszDescription is not null)
			{
				this.pszDescription.value = decoder.ReadWideCharString();
			}

			if (this.pszHelpFile is not null)
			{
				this.pszHelpFile.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct STDOBJREF : IRpcFixedStruct
	{
		public uint flags;
		public uint cPublicRefs;
		public ulong oxid;
		public ulong oid;
		public Guid ipid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.flags);
			encoder.WriteValue(this.cPublicRefs);
			encoder.WriteValue(this.oxid);
			encoder.WriteValue(this.oid);
			encoder.WriteValue(this.ipid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.flags = decoder.ReadUInt32();
			this.cPublicRefs = decoder.ReadUInt32();
			this.oxid = decoder.ReadUInt64();
			this.oid = decoder.ReadUInt64();
			this.ipid = decoder.ReadUuid();
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
	public partial struct REMQIRESULT : IRpcFixedStruct
	{
		public int hResult;
		public STDOBJREF std;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.hResult);
			encoder.WriteFixedStruct(this.std, NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.hResult = decoder.ReadInt32();
			this.std = decoder.ReadFixedStruct<STDOBJREF>(NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.std);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<STDOBJREF>(ref this.std);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct REMINTERFACEREF : IRpcFixedStruct
	{
		public Guid ipid;
		public uint cPublicRefs;
		public uint cPrivateRefs;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ipid);
			encoder.WriteValue(this.cPublicRefs);
			encoder.WriteValue(this.cPrivateRefs);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ipid = decoder.ReadUuid();
			this.cPublicRefs = decoder.ReadUInt32();
			this.cPrivateRefs = decoder.ReadUInt32();
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
	public partial struct COSERVERINFO : IRpcFixedStruct
	{
		public uint dwReserved1;
		public RpcPointer<string> pwszName;
		public RpcPointer<uint> pdwReserved;
		public uint dwReserved2;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwReserved1);
			encoder.WriteUniquePointer(this.pwszName);
			encoder.WriteUniquePointer(this.pdwReserved);
			encoder.WriteValue(this.dwReserved2);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwReserved1 = decoder.ReadUInt32();
			this.pwszName = decoder.ReadUniquePointer<string>();
			this.pdwReserved = decoder.ReadUniquePointer<uint>();
			this.dwReserved2 = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pwszName is not null)
			{
				encoder.WriteWideCharString(this.pwszName.value);
			}

			if (this.pdwReserved is not null)
			{
				encoder.WriteValue(this.pdwReserved.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pwszName is not null)
			{
				this.pwszName.value = decoder.ReadWideCharString();
			}

			if (this.pdwReserved is not null)
			{
				this.pdwReserved.value = decoder.ReadUInt32();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct customREMOTE_REQUEST_SCM_INFO : IRpcFixedStruct
	{
		public uint ClientImpLevel;
		public ushort cRequestedProtseqs;
		public RpcPointer<ushort[]> pRequestedProtseqs;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ClientImpLevel);
			encoder.WriteValue(this.cRequestedProtseqs);
			encoder.WriteUniquePointer(this.pRequestedProtseqs);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ClientImpLevel = decoder.ReadUInt32();
			this.cRequestedProtseqs = decoder.ReadUInt16();
			this.pRequestedProtseqs = decoder.ReadUniquePointer<ushort[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pRequestedProtseqs is not null)
			{
				encoder.WriteArrayHeader(this.pRequestedProtseqs.value);
				for (int i = 0; i < this.pRequestedProtseqs.value.Length; i++)
				{
					ushort elem_0 = this.pRequestedProtseqs.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pRequestedProtseqs is not null)
			{
				this.pRequestedProtseqs.value = decoder.ReadArrayHeader<ushort>();
				for (int i = 0; i < this.pRequestedProtseqs.value.Length; i++)
				{
					ushort elem_0 = this.pRequestedProtseqs.value[i];
					elem_0 = decoder.ReadUInt16();
					this.pRequestedProtseqs.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct customREMOTE_REPLY_SCM_INFO : IRpcFixedStruct
	{
		public ulong Oxid;
		public RpcPointer<DUALSTRINGARRAY> pdsaOxidBindings;
		public Guid ipidRemUnknown;
		public uint authnHint;
		public COMVERSION serverVersion;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Oxid);
			encoder.WriteUniquePointer(this.pdsaOxidBindings);
			encoder.WriteValue(this.ipidRemUnknown);
			encoder.WriteValue(this.authnHint);
			encoder.WriteFixedStruct(this.serverVersion, NdrAlignment._2Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Oxid = decoder.ReadUInt64();
			this.pdsaOxidBindings = decoder.ReadUniquePointer<DUALSTRINGARRAY>();
			this.ipidRemUnknown = decoder.ReadUuid();
			this.authnHint = decoder.ReadUInt32();
			this.serverVersion = decoder.ReadFixedStruct<COMVERSION>(NdrAlignment._2Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pdsaOxidBindings is not null)
			{
				encoder.WriteConformantStruct(this.pdsaOxidBindings.value, NdrAlignment._2Byte);
				encoder.WriteStructDeferral(this.pdsaOxidBindings.value);
			}

			encoder.WriteStructDeferral(this.serverVersion);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pdsaOxidBindings is not null)
			{
				this.pdsaOxidBindings.value = decoder.ReadConformantStruct<DUALSTRINGARRAY>(NdrAlignment._2Byte);
				decoder.ReadStructDeferral<DUALSTRINGARRAY>(ref this.pdsaOxidBindings.value);
			}

			decoder.ReadStructDeferral<COMVERSION>(ref this.serverVersion);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct InstantiationInfoData : IRpcFixedStruct
	{
		public Guid classId;
		public uint classCtx;
		public uint actvflags;
		public int fIsSurrogate;
		public uint cIID;
		public uint instFlag;
		public RpcPointer<Guid[]> pIID;
		public uint thisSize;
		public COMVERSION clientCOMVersion;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.classId);
			encoder.WriteValue(this.classCtx);
			encoder.WriteValue(this.actvflags);
			encoder.WriteValue(this.fIsSurrogate);
			encoder.WriteValue(this.cIID);
			encoder.WriteValue(this.instFlag);
			encoder.WriteUniquePointer(this.pIID);
			encoder.WriteValue(this.thisSize);
			encoder.WriteFixedStruct(this.clientCOMVersion, NdrAlignment._2Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.classId = decoder.ReadUuid();
			this.classCtx = decoder.ReadUInt32();
			this.actvflags = decoder.ReadUInt32();
			this.fIsSurrogate = decoder.ReadInt32();
			this.cIID = decoder.ReadUInt32();
			this.instFlag = decoder.ReadUInt32();
			this.pIID = decoder.ReadUniquePointer<Guid[]>();
			this.thisSize = decoder.ReadUInt32();
			this.clientCOMVersion = decoder.ReadFixedStruct<COMVERSION>(NdrAlignment._2Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pIID is not null)
			{
				encoder.WriteArrayHeader(this.pIID.value);
				for (int i = 0; i < this.pIID.value.Length; i++)
				{
					Guid elem_0 = this.pIID.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteStructDeferral(this.clientCOMVersion);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pIID is not null)
			{
				this.pIID.value = decoder.ReadArrayHeader<Guid>();
				for (int i = 0; i < this.pIID.value.Length; i++)
				{
					Guid elem_0 = this.pIID.value[i];
					elem_0 = decoder.ReadUuid();
					this.pIID.value[i] = elem_0;
				}
			}

			decoder.ReadStructDeferral<COMVERSION>(ref this.clientCOMVersion);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LocationInfoData : IRpcFixedStruct
	{
		public RpcPointer<string> machineName;
		public uint processId;
		public uint apartmentId;
		public uint contextId;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.machineName);
			encoder.WriteValue(this.processId);
			encoder.WriteValue(this.apartmentId);
			encoder.WriteValue(this.contextId);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.machineName = decoder.ReadUniquePointer<string>();
			this.processId = decoder.ReadUInt32();
			this.apartmentId = decoder.ReadUInt32();
			this.contextId = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.machineName is not null)
			{
				encoder.WriteWideCharString(this.machineName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.machineName is not null)
			{
				this.machineName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ActivationContextInfoData : IRpcFixedStruct
	{
		public int clientOK;
		public int bReserved1;
		public uint dwReserved1;
		public uint dwReserved2;
		public RpcPointer<MInterfacePointer> pIFDClientCtx;
		public RpcPointer<MInterfacePointer> pIFDPrototypeCtx;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.clientOK);
			encoder.WriteValue(this.bReserved1);
			encoder.WriteValue(this.dwReserved1);
			encoder.WriteValue(this.dwReserved2);
			encoder.WriteUniquePointer(this.pIFDClientCtx);
			encoder.WriteUniquePointer(this.pIFDPrototypeCtx);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.clientOK = decoder.ReadInt32();
			this.bReserved1 = decoder.ReadInt32();
			this.dwReserved1 = decoder.ReadUInt32();
			this.dwReserved2 = decoder.ReadUInt32();
			this.pIFDClientCtx = decoder.ReadUniquePointer<MInterfacePointer>();
			this.pIFDPrototypeCtx = decoder.ReadUniquePointer<MInterfacePointer>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pIFDClientCtx is not null)
			{
				encoder.WriteConformantStruct(this.pIFDClientCtx.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pIFDClientCtx.value);
			}

			if (this.pIFDPrototypeCtx is not null)
			{
				encoder.WriteConformantStruct(this.pIFDPrototypeCtx.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pIFDPrototypeCtx.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pIFDClientCtx is not null)
			{
				this.pIFDClientCtx.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<MInterfacePointer>(ref this.pIFDClientCtx.value);
			}

			if (this.pIFDPrototypeCtx is not null)
			{
				this.pIFDPrototypeCtx.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<MInterfacePointer>(ref this.pIFDPrototypeCtx.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct CustomHeader : IRpcFixedStruct
	{
		public uint totalSize;
		public uint headerSize;
		public uint dwReserved;
		public uint destCtx;
		public uint cIfs;
		public Guid classInfoClsid;
		public RpcPointer<Guid[]> pclsid;
		public RpcPointer<uint[]> pSizes;
		public RpcPointer<uint> pdwReserved;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.totalSize);
			encoder.WriteValue(this.headerSize);
			encoder.WriteValue(this.dwReserved);
			encoder.WriteValue(this.destCtx);
			encoder.WriteValue(this.cIfs);
			encoder.WriteValue(this.classInfoClsid);
			encoder.WriteUniquePointer(this.pclsid);
			encoder.WriteUniquePointer(this.pSizes);
			encoder.WriteUniquePointer(this.pdwReserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.totalSize = decoder.ReadUInt32();
			this.headerSize = decoder.ReadUInt32();
			this.dwReserved = decoder.ReadUInt32();
			this.destCtx = decoder.ReadUInt32();
			this.cIfs = decoder.ReadUInt32();
			this.classInfoClsid = decoder.ReadUuid();
			this.pclsid = decoder.ReadUniquePointer<Guid[]>();
			this.pSizes = decoder.ReadUniquePointer<uint[]>();
			this.pdwReserved = decoder.ReadUniquePointer<uint>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pclsid is not null)
			{
				encoder.WriteArrayHeader(this.pclsid.value);
				for (int i = 0; i < this.pclsid.value.Length; i++)
				{
					Guid elem_0 = this.pclsid.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.pSizes is not null)
			{
				encoder.WriteArrayHeader(this.pSizes.value);
				for (int i = 0; i < this.pSizes.value.Length; i++)
				{
					uint elem_0 = this.pSizes.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.pdwReserved is not null)
			{
				encoder.WriteValue(this.pdwReserved.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pclsid is not null)
			{
				this.pclsid.value = decoder.ReadArrayHeader<Guid>();
				for (int i = 0; i < this.pclsid.value.Length; i++)
				{
					Guid elem_0 = this.pclsid.value[i];
					elem_0 = decoder.ReadUuid();
					this.pclsid.value[i] = elem_0;
				}
			}

			if (this.pSizes is not null)
			{
				this.pSizes.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < this.pSizes.value.Length; i++)
				{
					uint elem_0 = this.pSizes.value[i];
					elem_0 = decoder.ReadUInt32();
					this.pSizes.value[i] = elem_0;
				}
			}

			if (this.pdwReserved is not null)
			{
				this.pdwReserved.value = decoder.ReadUInt32();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct PropsOutInfo : IRpcFixedStruct
	{
		public uint cIfs;
		public RpcPointer<Guid[]> piid;
		public RpcPointer<int[]> phresults;
		public RpcPointer<RpcPointer<MInterfacePointer>[]> ppIntfData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cIfs);
			encoder.WriteUniquePointer(this.piid);
			encoder.WriteUniquePointer(this.phresults);
			encoder.WriteUniquePointer(this.ppIntfData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cIfs = decoder.ReadUInt32();
			this.piid = decoder.ReadUniquePointer<Guid[]>();
			this.phresults = decoder.ReadUniquePointer<int[]>();
			this.ppIntfData = decoder.ReadUniquePointer<RpcPointer<MInterfacePointer>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.piid is not null)
			{
				encoder.WriteArrayHeader(this.piid.value);
				for (int i = 0; i < this.piid.value.Length; i++)
				{
					Guid elem_0 = this.piid.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.phresults is not null)
			{
				encoder.WriteArrayHeader(this.phresults.value);
				for (int i = 0; i < this.phresults.value.Length; i++)
				{
					int elem_0 = this.phresults.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.ppIntfData is not null)
			{
				encoder.WriteArrayHeader(this.ppIntfData.value);
				for (int i = 0; i < this.ppIntfData.value.Length; i++)
				{
					RpcPointer<MInterfacePointer> elem_0 = this.ppIntfData.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.ppIntfData.value.Length; i++)
				{
					RpcPointer<MInterfacePointer> elem_0 = this.ppIntfData.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteConformantStruct(elem_0.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.piid is not null)
			{
				this.piid.value = decoder.ReadArrayHeader<Guid>();
				for (int i = 0; i < this.piid.value.Length; i++)
				{
					Guid elem_0 = this.piid.value[i];
					elem_0 = decoder.ReadUuid();
					this.piid.value[i] = elem_0;
				}
			}

			if (this.phresults is not null)
			{
				this.phresults.value = decoder.ReadArrayHeader<int>();
				for (int i = 0; i < this.phresults.value.Length; i++)
				{
					int elem_0 = this.phresults.value[i];
					elem_0 = decoder.ReadInt32();
					this.phresults.value[i] = elem_0;
				}
			}

			if (this.ppIntfData is not null)
			{
				this.ppIntfData.value = decoder.ReadArrayHeader<RpcPointer<MInterfacePointer>>();
				for (int i = 0; i < this.ppIntfData.value.Length; i++)
				{
					RpcPointer<MInterfacePointer> elem_0 = this.ppIntfData.value[i];
					elem_0 = decoder.ReadUniquePointer<MInterfacePointer>();
					this.ppIntfData.value[i] = elem_0;
				}

				for (int i = 0; i < this.ppIntfData.value.Length; i++)
				{
					RpcPointer<MInterfacePointer> elem_0 = this.ppIntfData.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<MInterfacePointer>(ref elem_0.value);
					}

					this.ppIntfData.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SecurityInfoData : IRpcFixedStruct
	{
		public uint dwAuthnFlags;
		public RpcPointer<COSERVERINFO> pServerInfo;
		public RpcPointer<uint> pdwReserved;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwAuthnFlags);
			encoder.WriteUniquePointer(this.pServerInfo);
			encoder.WriteUniquePointer(this.pdwReserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwAuthnFlags = decoder.ReadUInt32();
			this.pServerInfo = decoder.ReadUniquePointer<COSERVERINFO>();
			this.pdwReserved = decoder.ReadUniquePointer<uint>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pServerInfo is not null)
			{
				encoder.WriteFixedStruct(this.pServerInfo.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pServerInfo.value);
			}

			if (this.pdwReserved is not null)
			{
				encoder.WriteValue(this.pdwReserved.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pServerInfo is not null)
			{
				this.pServerInfo.value = decoder.ReadFixedStruct<COSERVERINFO>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<COSERVERINFO>(ref this.pServerInfo.value);
			}

			if (this.pdwReserved is not null)
			{
				this.pdwReserved.value = decoder.ReadUInt32();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ScmRequestInfoData : IRpcFixedStruct
	{
		public RpcPointer<uint> pdwReserved;
		public RpcPointer<customREMOTE_REQUEST_SCM_INFO> remoteRequest;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pdwReserved);
			encoder.WriteUniquePointer(this.remoteRequest);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pdwReserved = decoder.ReadUniquePointer<uint>();
			this.remoteRequest = decoder.ReadUniquePointer<customREMOTE_REQUEST_SCM_INFO>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pdwReserved is not null)
			{
				encoder.WriteValue(this.pdwReserved.value);
			}

			if (this.remoteRequest is not null)
			{
				encoder.WriteFixedStruct(this.remoteRequest.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.remoteRequest.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pdwReserved is not null)
			{
				this.pdwReserved.value = decoder.ReadUInt32();
			}

			if (this.remoteRequest is not null)
			{
				this.remoteRequest.value = decoder.ReadFixedStruct<customREMOTE_REQUEST_SCM_INFO>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<customREMOTE_REQUEST_SCM_INFO>(ref this.remoteRequest.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ScmReplyInfoData : IRpcFixedStruct
	{
		public RpcPointer<uint> pdwReserved;
		public RpcPointer<customREMOTE_REPLY_SCM_INFO> remoteReply;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pdwReserved);
			encoder.WriteUniquePointer(this.remoteReply);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pdwReserved = decoder.ReadUniquePointer<uint>();
			this.remoteReply = decoder.ReadUniquePointer<customREMOTE_REPLY_SCM_INFO>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pdwReserved is not null)
			{
				encoder.WriteValue(this.pdwReserved.value);
			}

			if (this.remoteReply is not null)
			{
				encoder.WriteFixedStruct(this.remoteReply.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.remoteReply.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pdwReserved is not null)
			{
				this.pdwReserved.value = decoder.ReadUInt32();
			}

			if (this.remoteReply is not null)
			{
				this.remoteReply.value = decoder.ReadFixedStruct<customREMOTE_REPLY_SCM_INFO>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<customREMOTE_REPLY_SCM_INFO>(ref this.remoteReply.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct InstanceInfoData : IRpcFixedStruct
	{
		public RpcPointer<string> fileName;
		public uint mode;
		public RpcPointer<MInterfacePointer> ifdROT;
		public RpcPointer<MInterfacePointer> ifdStg;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.fileName);
			encoder.WriteValue(this.mode);
			encoder.WriteUniquePointer(this.ifdROT);
			encoder.WriteUniquePointer(this.ifdStg);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.fileName = decoder.ReadUniquePointer<string>();
			this.mode = decoder.ReadUInt32();
			this.ifdROT = decoder.ReadUniquePointer<MInterfacePointer>();
			this.ifdStg = decoder.ReadUniquePointer<MInterfacePointer>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.fileName is not null)
			{
				encoder.WriteWideCharString(this.fileName.value);
			}

			if (this.ifdROT is not null)
			{
				encoder.WriteConformantStruct(this.ifdROT.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.ifdROT.value);
			}

			if (this.ifdStg is not null)
			{
				encoder.WriteConformantStruct(this.ifdStg.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.ifdStg.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.fileName is not null)
			{
				this.fileName.value = decoder.ReadWideCharString();
			}

			if (this.ifdROT is not null)
			{
				this.ifdROT.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<MInterfacePointer>(ref this.ifdROT.value);
			}

			if (this.ifdStg is not null)
			{
				this.ifdStg.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<MInterfacePointer>(ref this.ifdStg.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum SPD_FLAGS : int
	{
		SPD_FLAG_USE_CONSOLE_SESSION = 1,
		SPD_FLAG_USE_DEFAULT_AUTHN_LVL = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SpecialPropertiesData : IRpcFixedStruct
	{
		public uint dwSessionId;
		public int fRemoteThisSessionId;
		public int fClientImpersonating;
		public int fPartitionIDPresent;
		public uint dwDefaultAuthnLvl;
		public Guid guidPartition;
		public uint dwPRTFlags;
		public uint dwOrigClsctx;
		public uint dwFlags;
		public uint Reserved1;
		public ulong Reserved2;
		public uint[] Reserved3;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwSessionId);
			encoder.WriteValue(this.fRemoteThisSessionId);
			encoder.WriteValue(this.fClientImpersonating);
			encoder.WriteValue(this.fPartitionIDPresent);
			encoder.WriteValue(this.dwDefaultAuthnLvl);
			encoder.WriteValue(this.guidPartition);
			encoder.WriteValue(this.dwPRTFlags);
			encoder.WriteValue(this.dwOrigClsctx);
			encoder.WriteValue(this.dwFlags);
			encoder.WriteValue(this.Reserved1);
			encoder.WriteValue(this.Reserved2);
			if (this.Reserved3 == null)
				this.Reserved3 = new uint[5];
			for (int i = 0; i < 5; i++)
			{
				uint elem_0 = this.Reserved3[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwSessionId = decoder.ReadUInt32();
			this.fRemoteThisSessionId = decoder.ReadInt32();
			this.fClientImpersonating = decoder.ReadInt32();
			this.fPartitionIDPresent = decoder.ReadInt32();
			this.dwDefaultAuthnLvl = decoder.ReadUInt32();
			this.guidPartition = decoder.ReadUuid();
			this.dwPRTFlags = decoder.ReadUInt32();
			this.dwOrigClsctx = decoder.ReadUInt32();
			this.dwFlags = decoder.ReadUInt32();
			this.Reserved1 = decoder.ReadUInt32();
			this.Reserved2 = decoder.ReadUInt64();
			if (this.Reserved3 == null)
				this.Reserved3 = new uint[5];
			for (int i = 0; i < 5; i++)
			{
				uint elem_0 = this.Reserved3[i];
				elem_0 = decoder.ReadUInt32();
				this.Reserved3[i] = elem_0;
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
	public partial struct SpecialPropertiesData_Alternate : IRpcFixedStruct
	{
		public uint dwSessionId;
		public int fRemoteThisSessionId;
		public int fClientImpersonating;
		public int fPartitionIDPresent;
		public uint dwDefaultAuthnLvl;
		public Guid guidPartition;
		public uint dwPRTFlags;
		public uint dwOrigClsctx;
		public uint dwFlags;
		public uint[] Reserved3;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwSessionId);
			encoder.WriteValue(this.fRemoteThisSessionId);
			encoder.WriteValue(this.fClientImpersonating);
			encoder.WriteValue(this.fPartitionIDPresent);
			encoder.WriteValue(this.dwDefaultAuthnLvl);
			encoder.WriteValue(this.guidPartition);
			encoder.WriteValue(this.dwPRTFlags);
			encoder.WriteValue(this.dwOrigClsctx);
			encoder.WriteValue(this.dwFlags);
			if (this.Reserved3 == null)
				this.Reserved3 = new uint[8];
			for (int i = 0; i < 8; i++)
			{
				uint elem_0 = this.Reserved3[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwSessionId = decoder.ReadUInt32();
			this.fRemoteThisSessionId = decoder.ReadInt32();
			this.fClientImpersonating = decoder.ReadInt32();
			this.fPartitionIDPresent = decoder.ReadInt32();
			this.dwDefaultAuthnLvl = decoder.ReadUInt32();
			this.guidPartition = decoder.ReadUuid();
			this.dwPRTFlags = decoder.ReadUInt32();
			this.dwOrigClsctx = decoder.ReadUInt32();
			this.dwFlags = decoder.ReadUInt32();
			if (this.Reserved3 == null)
				this.Reserved3 = new uint[8];
			for (int i = 0; i < 8; i++)
			{
				uint elem_0 = this.Reserved3[i];
				elem_0 = decoder.ReadUInt32();
				this.Reserved3[i] = elem_0;
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("4d9f4ab8-7d1c-11cf-861e-0020af6e7c57"), RpcVersionAttribute(0, 0)]
	public partial interface IActivation
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> RemoteActivation(ORPCTHIS ORPCthis, RpcPointer<ORPCTHAT> ORPCthat, Guid Clsid, string pwszObjectName, RpcPointer<MInterfacePointer> pObjectStorage, uint ClientImpLevel, uint Mode, uint Interfaces, Guid[] pIIDs, ushort cRequestedProtseqs, ushort[] aRequestedProtseqs, RpcPointer<ulong> pOxid, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings, RpcPointer<Guid> pipidRemUnknown, RpcPointer<uint> pAuthnHint, RpcPointer<COMVERSION> pServerVersion, RpcPointer<int> phr, RpcPointer<RpcPointer<MInterfacePointer>[]> ppInterfaceData, RpcPointer<int[]> pResults, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("4d9f4ab8-7d1c-11cf-861e-0020af6e7c57")]
	public partial class IActivationClientProxy : Titanis.DceRpc.Client.RpcClientProxy, IActivation, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> RemoteActivation(ORPCTHIS ORPCthis, RpcPointer<ORPCTHAT> ORPCthat, Guid Clsid, string pwszObjectName, RpcPointer<MInterfacePointer> pObjectStorage, uint ClientImpLevel, uint Mode, uint Interfaces, Guid[] pIIDs, ushort cRequestedProtseqs, ushort[] aRequestedProtseqs, RpcPointer<ulong> pOxid, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings, RpcPointer<Guid> pipidRemUnknown, RpcPointer<uint> pAuthnHint, RpcPointer<COMVERSION> pServerVersion, RpcPointer<int> phr, RpcPointer<RpcPointer<MInterfacePointer>[]> ppInterfaceData, RpcPointer<int[]> pResults, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(ORPCthis, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ORPCthis);
			encoder.WriteValue(Clsid);
			encoder.WriteUniqueReferentId(pwszObjectName is null);
			if (pwszObjectName is not null)
				encoder.WriteWideCharString(pwszObjectName);
			encoder.WriteUniquePointer(pObjectStorage);
			if (pObjectStorage is not null)
			{
				encoder.WriteConformantStruct(pObjectStorage.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pObjectStorage.value);
			}

			encoder.WriteValue(ClientImpLevel);
			encoder.WriteValue(Mode);
			encoder.WriteValue(Interfaces);
			encoder.WriteUniqueReferentId(pIIDs is null);
			if (pIIDs is not null)
			{
				encoder.WriteArrayHeader(pIIDs);
				for (int i = 0; i < pIIDs.Length; i++)
				{
					Guid elem_0 = pIIDs[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(cRequestedProtseqs);
			encoder.WriteArrayHeader(aRequestedProtseqs);
			for (int i = 0; i < aRequestedProtseqs.Length; i++)
			{
				ushort elem_0 = aRequestedProtseqs[i];
				encoder.WriteValue(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ORPCthat.value = decoder.ReadFixedStruct<ORPCTHAT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ORPCTHAT>(ref ORPCthat.value);
			pOxid.value = decoder.ReadUInt64();
			ppdsaOxidBindings.value = decoder.ReadOutUniquePointer<DUALSTRINGARRAY>(ppdsaOxidBindings.value);
			if (ppdsaOxidBindings.value is not null)
			{
				ppdsaOxidBindings.value.value = decoder.ReadConformantStruct<DUALSTRINGARRAY>(NdrAlignment._2Byte);
				decoder.ReadStructDeferral<DUALSTRINGARRAY>(ref ppdsaOxidBindings.value.value);
			}

			pipidRemUnknown.value = decoder.ReadUuid();
			pAuthnHint.value = decoder.ReadUInt32();
			pServerVersion.value = decoder.ReadFixedStruct<COMVERSION>(NdrAlignment._2Byte);
			decoder.ReadStructDeferral<COMVERSION>(ref pServerVersion.value);
			phr.value = decoder.ReadInt32();
			ppInterfaceData.value = decoder.ReadArrayHeader<RpcPointer<MInterfacePointer>>();
			for (int i = 0; i < ppInterfaceData.value.Length; i++)
			{
				RpcPointer<MInterfacePointer> elem_0 = ppInterfaceData.value[i];
				elem_0 = decoder.ReadUniquePointer<MInterfacePointer>();
				ppInterfaceData.value[i] = elem_0;
			}

			for (int i = 0; i < ppInterfaceData.value.Length; i++)
			{
				RpcPointer<MInterfacePointer> elem_0 = ppInterfaceData.value[i];
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
					decoder.ReadStructDeferral<MInterfacePointer>(ref elem_0.value);
				}

				ppInterfaceData.value[i] = elem_0;
			}

			pResults.value = decoder.ReadArrayHeader<int>();
			for (int i = 0; i < pResults.value.Length; i++)
			{
				int elem_0 = pResults.value[i];
				elem_0 = decoder.ReadInt32();
				pResults.value[i] = elem_0;
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IActivation);
		private static Guid _interfaceUuid = new Guid("4d9f4ab8-7d1c-11cf-861e-0020af6e7c57");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class IActivationStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RemoteActivation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ORPCTHIS ORPCthis;
			RpcPointer<ORPCTHAT> ORPCthat = new RpcPointer<ORPCTHAT>();
			Guid Clsid;
			string pwszObjectName;
			RpcPointer<MInterfacePointer> pObjectStorage;
			uint ClientImpLevel;
			uint Mode;
			uint Interfaces;
			Guid[] pIIDs;
			ushort cRequestedProtseqs;
			ushort[] aRequestedProtseqs;
			RpcPointer<ulong> pOxid = new RpcPointer<ulong>();
			RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings = new RpcPointer<RpcPointer<DUALSTRINGARRAY>>();
			RpcPointer<Guid> pipidRemUnknown = new RpcPointer<Guid>();
			RpcPointer<uint> pAuthnHint = new RpcPointer<uint>();
			RpcPointer<COMVERSION> pServerVersion = new RpcPointer<COMVERSION>();
			RpcPointer<int> phr = new RpcPointer<int>();
			RpcPointer<RpcPointer<MInterfacePointer>[]> ppInterfaceData = new RpcPointer<RpcPointer<MInterfacePointer>[]>();
			RpcPointer<int[]> pResults = new RpcPointer<int[]>();
			ORPCthis = decoder.ReadFixedStruct<ORPCTHIS>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ORPCTHIS>(ref ORPCthis);
			Clsid = decoder.ReadUuid();
			if (decoder.ReadReferentId() == 0)
				pwszObjectName = null;
			else
				pwszObjectName = decoder.ReadWideCharString();
			pObjectStorage = decoder.ReadUniquePointer<MInterfacePointer>();
			if (pObjectStorage is not null)
			{
				pObjectStorage.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<MInterfacePointer>(ref pObjectStorage.value);
			}

			ClientImpLevel = decoder.ReadUInt32();
			Mode = decoder.ReadUInt32();
			Interfaces = decoder.ReadUInt32();
			pIIDs = decoder.ReadArrayHeader<Guid>();
			for (int i = 0; i < pIIDs.Length; i++)
			{
				Guid elem_0 = pIIDs[i];
				elem_0 = decoder.ReadUuid();
				pIIDs[i] = elem_0;
			}

			cRequestedProtseqs = decoder.ReadUInt16();
			aRequestedProtseqs = decoder.ReadArrayHeader<ushort>();
			for (int i = 0; i < aRequestedProtseqs.Length; i++)
			{
				ushort elem_0 = aRequestedProtseqs[i];
				elem_0 = decoder.ReadUInt16();
				aRequestedProtseqs[i] = elem_0;
			}

			var invokeTask = this._obj.RemoteActivation(ORPCthis, ORPCthat, Clsid, pwszObjectName, pObjectStorage, ClientImpLevel, Mode, Interfaces, pIIDs, cRequestedProtseqs, aRequestedProtseqs, pOxid, ppdsaOxidBindings, pipidRemUnknown, pAuthnHint, pServerVersion, phr, ppInterfaceData, pResults, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(ORPCthat.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ORPCthat.value);
			encoder.WriteValue(pOxid.value);
			encoder.WriteUniquePointer(ppdsaOxidBindings.value);
			if (ppdsaOxidBindings.value is not null)
			{
				encoder.WriteConformantStruct(ppdsaOxidBindings.value.value, NdrAlignment._2Byte);
				encoder.WriteStructDeferral(ppdsaOxidBindings.value.value);
			}

			encoder.WriteValue(pipidRemUnknown.value);
			encoder.WriteValue(pAuthnHint.value);
			encoder.WriteFixedStruct(pServerVersion.value, NdrAlignment._2Byte);
			encoder.WriteStructDeferral(pServerVersion.value);
			encoder.WriteValue(phr.value);
			encoder.WriteArrayHeader(ppInterfaceData.value);
			for (int i = 0; i < ppInterfaceData.value.Length; i++)
			{
				RpcPointer<MInterfacePointer> elem_0 = ppInterfaceData.value[i];
				encoder.WriteUniquePointer(elem_0);
			}

			for (int i = 0; i < ppInterfaceData.value.Length; i++)
			{
				RpcPointer<MInterfacePointer> elem_0 = ppInterfaceData.value[i];
				if (elem_0 is not null)
				{
					encoder.WriteConformantStruct(elem_0.value, NdrAlignment._4Byte);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}

			encoder.WriteArrayHeader(pResults.value);
			for (int i = 0; i < pResults.value.Length; i++)
			{
				int elem_0 = pResults.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("4d9f4ab8-7d1c-11cf-861e-0020af6e7c57");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IActivation _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public IActivationStub(IActivation obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_RemoteActivation};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("000001a0-0000-0000-c000-000000000046"), RpcVersionAttribute(0, 0)]
	public partial interface IRemoteSCMActivator
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum0NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum1NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum2NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> RemoteGetClassObject(ORPCTHIS orpcthis, RpcPointer<ORPCTHAT> orpcthat, RpcPointer<MInterfacePointer> pActProperties, RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> RemoteCreateInstance(ORPCTHIS orpcthis, RpcPointer<ORPCTHAT> orpcthat, RpcPointer<MInterfacePointer> pUnkOuter, RpcPointer<MInterfacePointer> pActProperties, RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("000001a0-0000-0000-c000-000000000046")]
	public partial class IRemoteSCMActivatorClientProxy : Titanis.DceRpc.Client.RpcClientProxy, IRemoteSCMActivator, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum0NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum1NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum2NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> RemoteGetClassObject(ORPCTHIS orpcthis, RpcPointer<ORPCTHAT> orpcthat, RpcPointer<MInterfacePointer> pActProperties, RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(orpcthis, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(orpcthis);
			encoder.WriteUniquePointer(pActProperties);
			if (pActProperties is not null)
			{
				encoder.WriteConformantStruct(pActProperties.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pActProperties.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			orpcthat.value = decoder.ReadFixedStruct<ORPCTHAT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ORPCTHAT>(ref orpcthat.value);
			ppActProperties.value = decoder.ReadOutUniquePointer<MInterfacePointer>(ppActProperties.value);
			if (ppActProperties.value is not null)
			{
				ppActProperties.value.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<MInterfacePointer>(ref ppActProperties.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> RemoteCreateInstance(ORPCTHIS orpcthis, RpcPointer<ORPCTHAT> orpcthat, RpcPointer<MInterfacePointer> pUnkOuter, RpcPointer<MInterfacePointer> pActProperties, RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(orpcthis, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(orpcthis);
			encoder.WriteUniquePointer(pUnkOuter);
			if (pUnkOuter is not null)
			{
				encoder.WriteConformantStruct(pUnkOuter.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pUnkOuter.value);
			}

			encoder.WriteUniquePointer(pActProperties);
			if (pActProperties is not null)
			{
				encoder.WriteConformantStruct(pActProperties.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pActProperties.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			orpcthat.value = decoder.ReadFixedStruct<ORPCTHAT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ORPCTHAT>(ref orpcthat.value);
			ppActProperties.value = decoder.ReadOutUniquePointer<MInterfacePointer>(ppActProperties.value);
			if (ppActProperties.value is not null)
			{
				ppActProperties.value.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<MInterfacePointer>(ref ppActProperties.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IRemoteSCMActivator);
		private static Guid _interfaceUuid = new Guid("000001a0-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class IRemoteSCMActivatorStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RemoteGetClassObject(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ORPCTHIS orpcthis;
			RpcPointer<ORPCTHAT> orpcthat = new RpcPointer<ORPCTHAT>();
			RpcPointer<MInterfacePointer> pActProperties;
			RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties = new RpcPointer<RpcPointer<MInterfacePointer>>();
			orpcthis = decoder.ReadFixedStruct<ORPCTHIS>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ORPCTHIS>(ref orpcthis);
			pActProperties = decoder.ReadUniquePointer<MInterfacePointer>();
			if (pActProperties is not null)
			{
				pActProperties.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<MInterfacePointer>(ref pActProperties.value);
			}

			var invokeTask = this._obj.RemoteGetClassObject(orpcthis, orpcthat, pActProperties, ppActProperties, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(orpcthat.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(orpcthat.value);
			encoder.WriteUniquePointer(ppActProperties.value);
			if (ppActProperties.value is not null)
			{
				encoder.WriteConformantStruct(ppActProperties.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(ppActProperties.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RemoteCreateInstance(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ORPCTHIS orpcthis;
			RpcPointer<ORPCTHAT> orpcthat = new RpcPointer<ORPCTHAT>();
			RpcPointer<MInterfacePointer> pUnkOuter;
			RpcPointer<MInterfacePointer> pActProperties;
			RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties = new RpcPointer<RpcPointer<MInterfacePointer>>();
			orpcthis = decoder.ReadFixedStruct<ORPCTHIS>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ORPCTHIS>(ref orpcthis);
			pUnkOuter = decoder.ReadUniquePointer<MInterfacePointer>();
			if (pUnkOuter is not null)
			{
				pUnkOuter.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<MInterfacePointer>(ref pUnkOuter.value);
			}

			pActProperties = decoder.ReadUniquePointer<MInterfacePointer>();
			if (pActProperties is not null)
			{
				pActProperties.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<MInterfacePointer>(ref pActProperties.value);
			}

			var invokeTask = this._obj.RemoteCreateInstance(orpcthis, orpcthat, pUnkOuter, pActProperties, ppActProperties, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(orpcthat.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(orpcthat.value);
			encoder.WriteUniquePointer(ppActProperties.value);
			if (ppActProperties.value is not null)
			{
				encoder.WriteConformantStruct(ppActProperties.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(ppActProperties.value.value);
			}

			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("000001a0-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IRemoteSCMActivator _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public IRemoteSCMActivatorStub(IRemoteSCMActivator obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_RemoteGetClassObject, this.Invoke_RemoteCreateInstance};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("99fcfec4-5260-101b-bbcb-00aa0021347a"), RpcVersionAttribute(0, 0)]
	public partial interface IObjectExporter
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), Titanis.DceRpc.IdempotentAttribute()]
		Task<int> ResolveOxid(ulong pOxid, ushort cRequestedProtseqs, ushort[] arRequestedProtseqs, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings, RpcPointer<Guid> pipidRemUnknown, RpcPointer<uint> pAuthnHint, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), Titanis.DceRpc.IdempotentAttribute()]
		Task<int> SimplePing(ulong pSetId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), Titanis.DceRpc.IdempotentAttribute()]
		Task<int> ComplexPing(RpcPointer<ulong> pSetId, ushort SequenceNum, ushort cAddToSet, ushort cDelFromSet, ulong[] AddToSet, ulong[] DelFromSet, RpcPointer<ushort> pPingBackoffFactor, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), Titanis.DceRpc.IdempotentAttribute()]
		Task<int> ServerAlive(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), Titanis.DceRpc.IdempotentAttribute()]
		Task<int> ResolveOxid2(ulong pOxid, ushort cRequestedProtseqs, ushort[] arRequestedProtseqs, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings, RpcPointer<Guid> pipidRemUnknown, RpcPointer<uint> pAuthnHint, RpcPointer<COMVERSION> pComVersion, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), Titanis.DceRpc.IdempotentAttribute()]
		Task<int> ServerAlive2(RpcPointer<COMVERSION> pComVersion, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOrBindings, RpcPointer<uint> pReserved, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("99fcfec4-5260-101b-bbcb-00aa0021347a")]
	public partial class IObjectExporterClientProxy : Titanis.DceRpc.Client.RpcClientProxy, IObjectExporter, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), Titanis.DceRpc.IdempotentAttribute()]
		public async Task<int> ResolveOxid(ulong pOxid, ushort cRequestedProtseqs, ushort[] arRequestedProtseqs, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings, RpcPointer<Guid> pipidRemUnknown, RpcPointer<uint> pAuthnHint, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(pOxid);
			encoder.WriteValue(cRequestedProtseqs);
			encoder.WriteArrayHeader(arRequestedProtseqs);
			for (int i = 0; i < arRequestedProtseqs.Length; i++)
			{
				ushort elem_0 = arRequestedProtseqs[i];
				encoder.WriteValue(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppdsaOxidBindings.value = decoder.ReadOutUniquePointer<DUALSTRINGARRAY>(ppdsaOxidBindings.value);
			if (ppdsaOxidBindings.value is not null)
			{
				ppdsaOxidBindings.value.value = decoder.ReadConformantStruct<DUALSTRINGARRAY>(NdrAlignment._2Byte);
				decoder.ReadStructDeferral<DUALSTRINGARRAY>(ref ppdsaOxidBindings.value.value);
			}

			pipidRemUnknown.value = decoder.ReadUuid();
			pAuthnHint.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), Titanis.DceRpc.IdempotentAttribute()]
		public async Task<int> SimplePing(ulong pSetId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(pSetId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), Titanis.DceRpc.IdempotentAttribute()]
		public async Task<int> ComplexPing(RpcPointer<ulong> pSetId, ushort SequenceNum, ushort cAddToSet, ushort cDelFromSet, ulong[] AddToSet, ulong[] DelFromSet, RpcPointer<ushort> pPingBackoffFactor, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(pSetId.value);
			encoder.WriteValue(SequenceNum);
			encoder.WriteValue(cAddToSet);
			encoder.WriteValue(cDelFromSet);
			encoder.WriteArrayHeader(AddToSet);
			for (int i = 0; i < AddToSet.Length; i++)
			{
				ulong elem_0 = AddToSet[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteArrayHeader(DelFromSet);
			for (int i = 0; i < DelFromSet.Length; i++)
			{
				ulong elem_0 = DelFromSet[i];
				encoder.WriteValue(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pSetId.value = decoder.ReadUInt64();
			pPingBackoffFactor.value = decoder.ReadUInt16();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), Titanis.DceRpc.IdempotentAttribute()]
		public async Task<int> ServerAlive(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), Titanis.DceRpc.IdempotentAttribute()]
		public async Task<int> ResolveOxid2(ulong pOxid, ushort cRequestedProtseqs, ushort[] arRequestedProtseqs, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings, RpcPointer<Guid> pipidRemUnknown, RpcPointer<uint> pAuthnHint, RpcPointer<COMVERSION> pComVersion, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(pOxid);
			encoder.WriteValue(cRequestedProtseqs);
			encoder.WriteArrayHeader(arRequestedProtseqs);
			for (int i = 0; i < arRequestedProtseqs.Length; i++)
			{
				ushort elem_0 = arRequestedProtseqs[i];
				encoder.WriteValue(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppdsaOxidBindings.value = decoder.ReadOutUniquePointer<DUALSTRINGARRAY>(ppdsaOxidBindings.value);
			if (ppdsaOxidBindings.value is not null)
			{
				ppdsaOxidBindings.value.value = decoder.ReadConformantStruct<DUALSTRINGARRAY>(NdrAlignment._2Byte);
				decoder.ReadStructDeferral<DUALSTRINGARRAY>(ref ppdsaOxidBindings.value.value);
			}

			pipidRemUnknown.value = decoder.ReadUuid();
			pAuthnHint.value = decoder.ReadUInt32();
			pComVersion.value = decoder.ReadFixedStruct<COMVERSION>(NdrAlignment._2Byte);
			decoder.ReadStructDeferral<COMVERSION>(ref pComVersion.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), Titanis.DceRpc.IdempotentAttribute()]
		public async Task<int> ServerAlive2(RpcPointer<COMVERSION> pComVersion, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOrBindings, RpcPointer<uint> pReserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pComVersion.value = decoder.ReadFixedStruct<COMVERSION>(NdrAlignment._2Byte);
			decoder.ReadStructDeferral<COMVERSION>(ref pComVersion.value);
			ppdsaOrBindings.value = decoder.ReadOutUniquePointer<DUALSTRINGARRAY>(ppdsaOrBindings.value);
			if (ppdsaOrBindings.value is not null)
			{
				ppdsaOrBindings.value.value = decoder.ReadConformantStruct<DUALSTRINGARRAY>(NdrAlignment._2Byte);
				decoder.ReadStructDeferral<DUALSTRINGARRAY>(ref ppdsaOrBindings.value.value);
			}

			pReserved.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IObjectExporter);
		private static Guid _interfaceUuid = new Guid("99fcfec4-5260-101b-bbcb-00aa0021347a");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class IObjectExporterStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ResolveOxid(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ulong pOxid;
			ushort cRequestedProtseqs;
			ushort[] arRequestedProtseqs;
			RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings = new RpcPointer<RpcPointer<DUALSTRINGARRAY>>();
			RpcPointer<Guid> pipidRemUnknown = new RpcPointer<Guid>();
			RpcPointer<uint> pAuthnHint = new RpcPointer<uint>();
			pOxid = decoder.ReadUInt64();
			cRequestedProtseqs = decoder.ReadUInt16();
			arRequestedProtseqs = decoder.ReadArrayHeader<ushort>();
			for (int i = 0; i < arRequestedProtseqs.Length; i++)
			{
				ushort elem_0 = arRequestedProtseqs[i];
				elem_0 = decoder.ReadUInt16();
				arRequestedProtseqs[i] = elem_0;
			}

			var invokeTask = this._obj.ResolveOxid(pOxid, cRequestedProtseqs, arRequestedProtseqs, ppdsaOxidBindings, pipidRemUnknown, pAuthnHint, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppdsaOxidBindings.value);
			if (ppdsaOxidBindings.value is not null)
			{
				encoder.WriteConformantStruct(ppdsaOxidBindings.value.value, NdrAlignment._2Byte);
				encoder.WriteStructDeferral(ppdsaOxidBindings.value.value);
			}

			encoder.WriteValue(pipidRemUnknown.value);
			encoder.WriteValue(pAuthnHint.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_SimplePing(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ulong pSetId;
			pSetId = decoder.ReadUInt64();
			var invokeTask = this._obj.SimplePing(pSetId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ComplexPing(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ulong> pSetId;
			ushort SequenceNum;
			ushort cAddToSet;
			ushort cDelFromSet;
			ulong[] AddToSet;
			ulong[] DelFromSet;
			RpcPointer<ushort> pPingBackoffFactor = new RpcPointer<ushort>();
			pSetId = new RpcPointer<ulong>();
			pSetId.value = decoder.ReadUInt64();
			SequenceNum = decoder.ReadUInt16();
			cAddToSet = decoder.ReadUInt16();
			cDelFromSet = decoder.ReadUInt16();
			AddToSet = decoder.ReadArrayHeader<ulong>();
			for (int i = 0; i < AddToSet.Length; i++)
			{
				ulong elem_0 = AddToSet[i];
				elem_0 = decoder.ReadUInt64();
				AddToSet[i] = elem_0;
			}

			DelFromSet = decoder.ReadArrayHeader<ulong>();
			for (int i = 0; i < DelFromSet.Length; i++)
			{
				ulong elem_0 = DelFromSet[i];
				elem_0 = decoder.ReadUInt64();
				DelFromSet[i] = elem_0;
			}

			var invokeTask = this._obj.ComplexPing(pSetId, SequenceNum, cAddToSet, cDelFromSet, AddToSet, DelFromSet, pPingBackoffFactor, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pSetId.value);
			encoder.WriteValue(pPingBackoffFactor.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ServerAlive(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.ServerAlive(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ResolveOxid2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ulong pOxid;
			ushort cRequestedProtseqs;
			ushort[] arRequestedProtseqs;
			RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings = new RpcPointer<RpcPointer<DUALSTRINGARRAY>>();
			RpcPointer<Guid> pipidRemUnknown = new RpcPointer<Guid>();
			RpcPointer<uint> pAuthnHint = new RpcPointer<uint>();
			RpcPointer<COMVERSION> pComVersion = new RpcPointer<COMVERSION>();
			pOxid = decoder.ReadUInt64();
			cRequestedProtseqs = decoder.ReadUInt16();
			arRequestedProtseqs = decoder.ReadArrayHeader<ushort>();
			for (int i = 0; i < arRequestedProtseqs.Length; i++)
			{
				ushort elem_0 = arRequestedProtseqs[i];
				elem_0 = decoder.ReadUInt16();
				arRequestedProtseqs[i] = elem_0;
			}

			var invokeTask = this._obj.ResolveOxid2(pOxid, cRequestedProtseqs, arRequestedProtseqs, ppdsaOxidBindings, pipidRemUnknown, pAuthnHint, pComVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppdsaOxidBindings.value);
			if (ppdsaOxidBindings.value is not null)
			{
				encoder.WriteConformantStruct(ppdsaOxidBindings.value.value, NdrAlignment._2Byte);
				encoder.WriteStructDeferral(ppdsaOxidBindings.value.value);
			}

			encoder.WriteValue(pipidRemUnknown.value);
			encoder.WriteValue(pAuthnHint.value);
			encoder.WriteFixedStruct(pComVersion.value, NdrAlignment._2Byte);
			encoder.WriteStructDeferral(pComVersion.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ServerAlive2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<COMVERSION> pComVersion = new RpcPointer<COMVERSION>();
			RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOrBindings = new RpcPointer<RpcPointer<DUALSTRINGARRAY>>();
			RpcPointer<uint> pReserved = new RpcPointer<uint>();
			var invokeTask = this._obj.ServerAlive2(pComVersion, ppdsaOrBindings, pReserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pComVersion.value, NdrAlignment._2Byte);
			encoder.WriteStructDeferral(pComVersion.value);
			encoder.WriteUniquePointer(ppdsaOrBindings.value);
			if (ppdsaOrBindings.value is not null)
			{
				encoder.WriteConformantStruct(ppdsaOrBindings.value.value, NdrAlignment._2Byte);
				encoder.WriteStructDeferral(ppdsaOrBindings.value.value);
			}

			encoder.WriteValue(pReserved.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("99fcfec4-5260-101b-bbcb-00aa0021347a");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IObjectExporter _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public IObjectExporterStub(IObjectExporter obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_ResolveOxid, this.Invoke_SimplePing, this.Invoke_ComplexPing, this.Invoke_ServerAlive, this.Invoke_ResolveOxid2, this.Invoke_ServerAlive2};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("00000000-0000-0000-c000-000000000046"), RpcVersionAttribute(0, 0)]
	public partial interface IUnknown : Titanis.DceRpc.IRpcObject
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> Opnum0NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> Opnum1NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> Opnum2NotUsedOnWire(CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("00000000-0000-0000-c000-000000000046")]
	public partial class IUnknownClientProxy : Titanis.DceRpc.Client.RpcObjectProxy, IUnknown, Titanis.DceRpc.IRpcObjectProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> Opnum0NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> Opnum1NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> Opnum2NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public override Type InterfaceType => typeof(IUnknown);
		private static Guid _interfaceUuid = new Guid("00000000-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class IUnknownStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("00000000-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IUnknown _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public IUnknownStub(IUnknown obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("00000131-0000-0000-c000-000000000046"), RpcVersionAttribute(0, 0)]
	public partial interface IRemUnknown : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> RemQueryInterface(Guid ripid, uint cRefs, ushort cIids, Guid[] iids, RpcPointer<RpcPointer<REMQIRESULT[]>> ppQIResults, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> RemAddRef(ushort cInterfaceRefs, REMINTERFACEREF[] InterfaceRefs, RpcPointer<int[]> pResults, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> RemRelease(ushort cInterfaceRefs, REMINTERFACEREF[] InterfaceRefs, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("00000131-0000-0000-c000-000000000046")]
	public partial class IRemUnknownClientProxy : IUnknownClientProxy, IRemUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> RemQueryInterface(Guid ripid, uint cRefs, ushort cIids, Guid[] iids, RpcPointer<RpcPointer<REMQIRESULT[]>> ppQIResults, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(ripid);
			encoder.WriteValue(cRefs);
			encoder.WriteValue(cIids);
			if (iids is not null)
			{
				encoder.WriteArrayHeader(iids);
				for (int i = 0; i < iids.Length; i++)
				{
					Guid elem_0 = iids[i];
					encoder.WriteValue(elem_0);
				}
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppQIResults.value = decoder.ReadOutUniquePointer<REMQIRESULT[]>(ppQIResults.value);
			if (ppQIResults.value is not null)
			{
				ppQIResults.value.value = decoder.ReadArrayHeader<REMQIRESULT>();
				for (int i = 0; i < ppQIResults.value.value.Length; i++)
				{
					REMQIRESULT elem_0 = ppQIResults.value.value[i];
					elem_0 = decoder.ReadFixedStruct<REMQIRESULT>(NdrAlignment._8Byte);
					ppQIResults.value.value[i] = elem_0;
				}

				for (int i = 0; i < ppQIResults.value.value.Length; i++)
				{
					REMQIRESULT elem_0 = ppQIResults.value.value[i];
					decoder.ReadStructDeferral<REMQIRESULT>(ref elem_0);
					ppQIResults.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> RemAddRef(ushort cInterfaceRefs, REMINTERFACEREF[] InterfaceRefs, RpcPointer<int[]> pResults, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(cInterfaceRefs);
			encoder.WriteArrayHeader(InterfaceRefs);
			for (int i = 0; i < InterfaceRefs.Length; i++)
			{
				REMINTERFACEREF elem_0 = InterfaceRefs[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
			}

			for (int i = 0; i < InterfaceRefs.Length; i++)
			{
				REMINTERFACEREF elem_0 = InterfaceRefs[i];
				encoder.WriteStructDeferral(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pResults.value = decoder.ReadArrayHeader<int>();
			for (int i = 0; i < pResults.value.Length; i++)
			{
				int elem_0 = pResults.value[i];
				elem_0 = decoder.ReadInt32();
				pResults.value[i] = elem_0;
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> RemRelease(ushort cInterfaceRefs, REMINTERFACEREF[] InterfaceRefs, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(cInterfaceRefs);
			encoder.WriteArrayHeader(InterfaceRefs);
			for (int i = 0; i < InterfaceRefs.Length; i++)
			{
				REMINTERFACEREF elem_0 = InterfaceRefs[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
			}

			for (int i = 0; i < InterfaceRefs.Length; i++)
			{
				REMINTERFACEREF elem_0 = InterfaceRefs[i];
				encoder.WriteStructDeferral(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public override Type InterfaceType => typeof(IRemUnknown);
		private static Guid _interfaceUuid = new Guid("00000131-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class IRemUnknownStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RemQueryInterface(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid ripid;
			uint cRefs;
			ushort cIids;
			Guid[] iids;
			RpcPointer<RpcPointer<REMQIRESULT[]>> ppQIResults = new RpcPointer<RpcPointer<REMQIRESULT[]>>();
			ripid = decoder.ReadUuid();
			cRefs = decoder.ReadUInt32();
			cIids = decoder.ReadUInt16();
			iids = decoder.ReadArrayHeader<Guid>();
			for (int i = 0; i < iids.Length; i++)
			{
				Guid elem_0 = iids[i];
				elem_0 = decoder.ReadUuid();
				iids[i] = elem_0;
			}

			var invokeTask = this._obj.RemQueryInterface(ripid, cRefs, cIids, iids, ppQIResults, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppQIResults.value);
			if (ppQIResults.value is not null)
			{
				encoder.WriteArrayHeader(ppQIResults.value.value);
				for (int i = 0; i < ppQIResults.value.value.Length; i++)
				{
					REMQIRESULT elem_0 = ppQIResults.value.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < ppQIResults.value.value.Length; i++)
				{
					REMQIRESULT elem_0 = ppQIResults.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RemAddRef(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ushort cInterfaceRefs;
			REMINTERFACEREF[] InterfaceRefs;
			RpcPointer<int[]> pResults = new RpcPointer<int[]>();
			cInterfaceRefs = decoder.ReadUInt16();
			InterfaceRefs = decoder.ReadArrayHeader<REMINTERFACEREF>();
			for (int i = 0; i < InterfaceRefs.Length; i++)
			{
				REMINTERFACEREF elem_0 = InterfaceRefs[i];
				elem_0 = decoder.ReadFixedStruct<REMINTERFACEREF>(NdrAlignment._4Byte);
				InterfaceRefs[i] = elem_0;
			}

			for (int i = 0; i < InterfaceRefs.Length; i++)
			{
				REMINTERFACEREF elem_0 = InterfaceRefs[i];
				decoder.ReadStructDeferral<REMINTERFACEREF>(ref elem_0);
				InterfaceRefs[i] = elem_0;
			}

			var invokeTask = this._obj.RemAddRef(cInterfaceRefs, InterfaceRefs, pResults, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(pResults.value);
			for (int i = 0; i < pResults.value.Length; i++)
			{
				int elem_0 = pResults.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RemRelease(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ushort cInterfaceRefs;
			REMINTERFACEREF[] InterfaceRefs;
			cInterfaceRefs = decoder.ReadUInt16();
			InterfaceRefs = decoder.ReadArrayHeader<REMINTERFACEREF>();
			for (int i = 0; i < InterfaceRefs.Length; i++)
			{
				REMINTERFACEREF elem_0 = InterfaceRefs[i];
				elem_0 = decoder.ReadFixedStruct<REMINTERFACEREF>(NdrAlignment._4Byte);
				InterfaceRefs[i] = elem_0;
			}

			for (int i = 0; i < InterfaceRefs.Length; i++)
			{
				REMINTERFACEREF elem_0 = InterfaceRefs[i];
				decoder.ReadStructDeferral<REMINTERFACEREF>(ref elem_0);
				InterfaceRefs[i] = elem_0;
			}

			var invokeTask = this._obj.RemRelease(cInterfaceRefs, InterfaceRefs, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("00000131-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IRemUnknown _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public IRemUnknownStub(IRemUnknown obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_RemQueryInterface, this.Invoke_RemAddRef, this.Invoke_RemRelease};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("00000143-0000-0000-c000-000000000046"), RpcVersionAttribute(0, 0)]
	public partial interface IRemUnknown2 : IRemUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> RemQueryInterface2(Guid ripid, ushort cIids, Guid[] iids, RpcPointer<int[]> phr, RpcPointer<RpcPointer<MInterfacePointer>[]> ppMIF, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("00000143-0000-0000-c000-000000000046")]
	public partial class IRemUnknown2ClientProxy : IRemUnknownClientProxy, IRemUnknown2
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> RemQueryInterface2(Guid ripid, ushort cIids, Guid[] iids, RpcPointer<int[]> phr, RpcPointer<RpcPointer<MInterfacePointer>[]> ppMIF, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(ripid);
			encoder.WriteValue(cIids);
			if (iids is not null)
			{
				encoder.WriteArrayHeader(iids);
				for (int i = 0; i < iids.Length; i++)
				{
					Guid elem_0 = iids[i];
					encoder.WriteValue(elem_0);
				}
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phr.value = decoder.ReadArrayHeader<int>();
			for (int i = 0; i < phr.value.Length; i++)
			{
				int elem_0 = phr.value[i];
				elem_0 = decoder.ReadInt32();
				phr.value[i] = elem_0;
			}

			ppMIF.value = decoder.ReadArrayHeader<RpcPointer<MInterfacePointer>>();
			for (int i = 0; i < ppMIF.value.Length; i++)
			{
				RpcPointer<MInterfacePointer> elem_0 = ppMIF.value[i];
				elem_0 = decoder.ReadUniquePointer<MInterfacePointer>();
				ppMIF.value[i] = elem_0;
			}

			for (int i = 0; i < ppMIF.value.Length; i++)
			{
				RpcPointer<MInterfacePointer> elem_0 = ppMIF.value[i];
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
					decoder.ReadStructDeferral<MInterfacePointer>(ref elem_0.value);
				}

				ppMIF.value[i] = elem_0;
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IRemUnknown2);
		private static Guid _interfaceUuid = new Guid("00000143-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class IRemUnknown2Stub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RemQueryInterface(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid ripid;
			uint cRefs;
			ushort cIids;
			Guid[] iids;
			RpcPointer<RpcPointer<REMQIRESULT[]>> ppQIResults = new RpcPointer<RpcPointer<REMQIRESULT[]>>();
			ripid = decoder.ReadUuid();
			cRefs = decoder.ReadUInt32();
			cIids = decoder.ReadUInt16();
			iids = decoder.ReadArrayHeader<Guid>();
			for (int i = 0; i < iids.Length; i++)
			{
				Guid elem_0 = iids[i];
				elem_0 = decoder.ReadUuid();
				iids[i] = elem_0;
			}

			var invokeTask = this._obj.RemQueryInterface(ripid, cRefs, cIids, iids, ppQIResults, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppQIResults.value);
			if (ppQIResults.value is not null)
			{
				encoder.WriteArrayHeader(ppQIResults.value.value);
				for (int i = 0; i < ppQIResults.value.value.Length; i++)
				{
					REMQIRESULT elem_0 = ppQIResults.value.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < ppQIResults.value.value.Length; i++)
				{
					REMQIRESULT elem_0 = ppQIResults.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RemAddRef(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ushort cInterfaceRefs;
			REMINTERFACEREF[] InterfaceRefs;
			RpcPointer<int[]> pResults = new RpcPointer<int[]>();
			cInterfaceRefs = decoder.ReadUInt16();
			InterfaceRefs = decoder.ReadArrayHeader<REMINTERFACEREF>();
			for (int i = 0; i < InterfaceRefs.Length; i++)
			{
				REMINTERFACEREF elem_0 = InterfaceRefs[i];
				elem_0 = decoder.ReadFixedStruct<REMINTERFACEREF>(NdrAlignment._4Byte);
				InterfaceRefs[i] = elem_0;
			}

			for (int i = 0; i < InterfaceRefs.Length; i++)
			{
				REMINTERFACEREF elem_0 = InterfaceRefs[i];
				decoder.ReadStructDeferral<REMINTERFACEREF>(ref elem_0);
				InterfaceRefs[i] = elem_0;
			}

			var invokeTask = this._obj.RemAddRef(cInterfaceRefs, InterfaceRefs, pResults, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(pResults.value);
			for (int i = 0; i < pResults.value.Length; i++)
			{
				int elem_0 = pResults.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RemRelease(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			ushort cInterfaceRefs;
			REMINTERFACEREF[] InterfaceRefs;
			cInterfaceRefs = decoder.ReadUInt16();
			InterfaceRefs = decoder.ReadArrayHeader<REMINTERFACEREF>();
			for (int i = 0; i < InterfaceRefs.Length; i++)
			{
				REMINTERFACEREF elem_0 = InterfaceRefs[i];
				elem_0 = decoder.ReadFixedStruct<REMINTERFACEREF>(NdrAlignment._4Byte);
				InterfaceRefs[i] = elem_0;
			}

			for (int i = 0; i < InterfaceRefs.Length; i++)
			{
				REMINTERFACEREF elem_0 = InterfaceRefs[i];
				decoder.ReadStructDeferral<REMINTERFACEREF>(ref elem_0);
				InterfaceRefs[i] = elem_0;
			}

			var invokeTask = this._obj.RemRelease(cInterfaceRefs, InterfaceRefs, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RemQueryInterface2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid ripid;
			ushort cIids;
			Guid[] iids;
			RpcPointer<int[]> phr = new RpcPointer<int[]>();
			RpcPointer<RpcPointer<MInterfacePointer>[]> ppMIF = new RpcPointer<RpcPointer<MInterfacePointer>[]>();
			ripid = decoder.ReadUuid();
			cIids = decoder.ReadUInt16();
			iids = decoder.ReadArrayHeader<Guid>();
			for (int i = 0; i < iids.Length; i++)
			{
				Guid elem_0 = iids[i];
				elem_0 = decoder.ReadUuid();
				iids[i] = elem_0;
			}

			var invokeTask = this._obj.RemQueryInterface2(ripid, cIids, iids, phr, ppMIF, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(phr.value);
			for (int i = 0; i < phr.value.Length; i++)
			{
				int elem_0 = phr.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteArrayHeader(ppMIF.value);
			for (int i = 0; i < ppMIF.value.Length; i++)
			{
				RpcPointer<MInterfacePointer> elem_0 = ppMIF.value[i];
				encoder.WriteUniquePointer(elem_0);
			}

			for (int i = 0; i < ppMIF.value.Length; i++)
			{
				RpcPointer<MInterfacePointer> elem_0 = ppMIF.value[i];
				if (elem_0 is not null)
				{
					encoder.WriteConformantStruct(elem_0.value, NdrAlignment._4Byte);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}

			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("00000143-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IRemUnknown2 _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public IRemUnknown2Stub(IRemUnknown2 obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_RemQueryInterface, this.Invoke_RemAddRef, this.Invoke_RemRelease, this.Invoke_RemQueryInterface2};
		}
	}
}