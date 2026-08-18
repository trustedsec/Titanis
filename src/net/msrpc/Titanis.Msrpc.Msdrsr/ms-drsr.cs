namespace ms_drsr
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct NT4SID : IRpcFixedStruct
	{
		public byte[] Data;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			if (this.Data == null)
				this.Data = new byte[28];
			for (int i = 0; i < 28; i++)
			{
				byte elem_0 = this.Data[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			if (this.Data == null)
				this.Data = new byte[28];
			for (int i = 0; i < 28; i++)
			{
				byte elem_0 = this.Data[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.Data[i] = elem_0;
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
	public partial struct DSNAME : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.StringName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.StringName = decoder.ReadArrayHeader<char>();
		}

		public uint structLen;
		public uint SidLen;
		public Guid Guid;
		public NT4SID Sid;
		public uint NameLen;
		public char[] StringName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.StringName.Length; i++)
			{
				char elem_0 = this.StringName[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.StringName.Length; i++)
			{
				char elem_0 = this.StringName[i];
				elem_0 = decoder.ReadWideChar();
				this.StringName[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.structLen);
			encoder.WriteValue(this.SidLen);
			encoder.WriteValue(this.Guid);
			encoder.WriteFixedStruct(this.Sid, NdrAlignment._1Byte);
			encoder.WriteValue(this.NameLen);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.structLen = decoder.ReadUInt32();
			this.SidLen = decoder.ReadUInt32();
			this.Guid = decoder.ReadUuid();
			this.Sid = decoder.ReadFixedStruct<NT4SID>(NdrAlignment._1Byte);
			this.NameLen = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Sid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<NT4SID>(ref this.Sid);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct USN_VECTOR : IRpcFixedStruct
	{
		public long usnHighObjUpdate;
		public long usnReserved;
		public long usnHighPropUpdate;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.usnHighObjUpdate);
			encoder.WriteValue(this.usnReserved);
			encoder.WriteValue(this.usnHighPropUpdate);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.usnHighObjUpdate = decoder.ReadInt64();
			this.usnReserved = decoder.ReadInt64();
			this.usnHighPropUpdate = decoder.ReadInt64();
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
	public partial struct UPTODATE_CURSOR_V1 : IRpcFixedStruct
	{
		public Guid uuidDsa;
		public long usnHighPropUpdate;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidDsa);
			encoder.WriteValue(this.usnHighPropUpdate);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidDsa = decoder.ReadUuid();
			this.usnHighPropUpdate = decoder.ReadInt64();
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
	public partial struct UPTODATE_VECTOR_V1_EXT : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgCursors);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgCursors = decoder.ReadArrayHeader<UPTODATE_CURSOR_V1>();
		}

		public uint dwVersion;
		public uint dwReserved1;
		public uint cNumCursors;
		public uint dwReserved2;
		public UPTODATE_CURSOR_V1[] rgCursors;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgCursors.Length; i++)
			{
				UPTODATE_CURSOR_V1 elem_0 = this.rgCursors[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgCursors.Length; i++)
			{
				UPTODATE_CURSOR_V1 elem_0 = this.rgCursors[i];
				elem_0 = decoder.ReadFixedStruct<UPTODATE_CURSOR_V1>(NdrAlignment._8Byte);
				this.rgCursors[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwVersion);
			encoder.WriteValue(this.dwReserved1);
			encoder.WriteValue(this.cNumCursors);
			encoder.WriteValue(this.dwReserved2);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwVersion = decoder.ReadUInt32();
			this.dwReserved1 = decoder.ReadUInt32();
			this.cNumCursors = decoder.ReadUInt32();
			this.dwReserved2 = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgCursors.Length; i++)
			{
				UPTODATE_CURSOR_V1 elem_0 = this.rgCursors[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgCursors.Length; i++)
			{
				UPTODATE_CURSOR_V1 elem_0 = this.rgCursors[i];
				decoder.ReadStructDeferral<UPTODATE_CURSOR_V1>(ref elem_0);
				this.rgCursors[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct OID_t : IRpcFixedStruct
	{
		public uint length;
		public RpcPointer<byte[]> elements;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.length);
			encoder.WriteUniquePointer(this.elements);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.length = decoder.ReadUInt32();
			this.elements = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.elements is not null)
			{
				encoder.WriteArrayHeader(this.elements.value);
				for (int i = 0; i < this.elements.value.Length; i++)
				{
					byte elem_0 = this.elements.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.elements is not null)
			{
				this.elements.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.elements.value.Length; i++)
				{
					byte elem_0 = this.elements.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.elements.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct PrefixTableEntry : IRpcFixedStruct
	{
		public uint ndx;
		public OID_t prefix;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ndx);
			encoder.WriteFixedStruct(this.prefix, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ndx = decoder.ReadUInt32();
			this.prefix = decoder.ReadFixedStruct<OID_t>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.prefix);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<OID_t>(ref this.prefix);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SCHEMA_PREFIX_TABLE : IRpcFixedStruct
	{
		public uint PrefixCount;
		public RpcPointer<PrefixTableEntry[]> pPrefixEntry;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.PrefixCount);
			encoder.WriteUniquePointer(this.pPrefixEntry);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.PrefixCount = decoder.ReadUInt32();
			this.pPrefixEntry = decoder.ReadUniquePointer<PrefixTableEntry[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pPrefixEntry is not null)
			{
				encoder.WriteArrayHeader(this.pPrefixEntry.value);
				for (int i = 0; i < this.pPrefixEntry.value.Length; i++)
				{
					PrefixTableEntry elem_0 = this.pPrefixEntry.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.pPrefixEntry.value.Length; i++)
				{
					PrefixTableEntry elem_0 = this.pPrefixEntry.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pPrefixEntry is not null)
			{
				this.pPrefixEntry.value = decoder.ReadArrayHeader<PrefixTableEntry>();
				for (int i = 0; i < this.pPrefixEntry.value.Length; i++)
				{
					PrefixTableEntry elem_0 = this.pPrefixEntry.value[i];
					elem_0 = decoder.ReadFixedStruct<PrefixTableEntry>(NdrAlignment.NativePtr);
					this.pPrefixEntry.value[i] = elem_0;
				}

				for (int i = 0; i < this.pPrefixEntry.value.Length; i++)
				{
					PrefixTableEntry elem_0 = this.pPrefixEntry.value[i];
					decoder.ReadStructDeferral<PrefixTableEntry>(ref elem_0);
					this.pPrefixEntry.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct PARTIAL_ATTR_VECTOR_V1_EXT : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgPartialAttr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgPartialAttr = decoder.ReadArrayHeader<uint>();
		}

		public uint dwVersion;
		public uint dwReserved1;
		public uint cAttrs;
		public uint[] rgPartialAttr;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgPartialAttr.Length; i++)
			{
				uint elem_0 = this.rgPartialAttr[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgPartialAttr.Length; i++)
			{
				uint elem_0 = this.rgPartialAttr[i];
				elem_0 = decoder.ReadUInt32();
				this.rgPartialAttr[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwVersion);
			encoder.WriteValue(this.dwReserved1);
			encoder.WriteValue(this.cAttrs);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwVersion = decoder.ReadUInt32();
			this.dwReserved1 = decoder.ReadUInt32();
			this.cAttrs = decoder.ReadUInt32();
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
	public partial struct MTX_ADDR : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.mtx_name);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.mtx_name = decoder.ReadArrayHeader<byte>();
		}

		public uint mtx_namelen;
		public byte[] mtx_name;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.mtx_name.Length; i++)
			{
				byte elem_0 = this.mtx_name[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.mtx_name.Length; i++)
			{
				byte elem_0 = this.mtx_name[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.mtx_name[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.mtx_namelen);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.mtx_namelen = decoder.ReadUInt32();
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
	public partial struct ATTRVAL : IRpcFixedStruct
	{
		public uint valLen;
		public RpcPointer<byte[]> pVal;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.valLen);
			encoder.WriteUniquePointer(this.pVal);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.valLen = decoder.ReadUInt32();
			this.pVal = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pVal is not null)
			{
				encoder.WriteArrayHeader(this.pVal.value);
				for (int i = 0; i < this.pVal.value.Length; i++)
				{
					byte elem_0 = this.pVal.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pVal is not null)
			{
				this.pVal.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pVal.value.Length; i++)
				{
					byte elem_0 = this.pVal.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pVal.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ATTRVALBLOCK : IRpcFixedStruct
	{
		public uint valCount;
		public RpcPointer<ATTRVAL[]> pAVal;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.valCount);
			encoder.WriteUniquePointer(this.pAVal);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.valCount = decoder.ReadUInt32();
			this.pAVal = decoder.ReadUniquePointer<ATTRVAL[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pAVal is not null)
			{
				encoder.WriteArrayHeader(this.pAVal.value);
				for (int i = 0; i < this.pAVal.value.Length; i++)
				{
					ATTRVAL elem_0 = this.pAVal.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.pAVal.value.Length; i++)
				{
					ATTRVAL elem_0 = this.pAVal.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pAVal is not null)
			{
				this.pAVal.value = decoder.ReadArrayHeader<ATTRVAL>();
				for (int i = 0; i < this.pAVal.value.Length; i++)
				{
					ATTRVAL elem_0 = this.pAVal.value[i];
					elem_0 = decoder.ReadFixedStruct<ATTRVAL>(NdrAlignment.NativePtr);
					this.pAVal.value[i] = elem_0;
				}

				for (int i = 0; i < this.pAVal.value.Length; i++)
				{
					ATTRVAL elem_0 = this.pAVal.value[i];
					decoder.ReadStructDeferral<ATTRVAL>(ref elem_0);
					this.pAVal.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ATTR : IRpcFixedStruct
	{
		public uint attrTyp;
		public ATTRVALBLOCK AttrVal;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.attrTyp);
			encoder.WriteFixedStruct(this.AttrVal, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.attrTyp = decoder.ReadUInt32();
			this.AttrVal = decoder.ReadFixedStruct<ATTRVALBLOCK>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.AttrVal);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ATTRVALBLOCK>(ref this.AttrVal);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ATTRBLOCK : IRpcFixedStruct
	{
		public uint attrCount;
		public RpcPointer<ATTR[]> pAttr;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.attrCount);
			encoder.WriteUniquePointer(this.pAttr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.attrCount = decoder.ReadUInt32();
			this.pAttr = decoder.ReadUniquePointer<ATTR[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pAttr is not null)
			{
				encoder.WriteArrayHeader(this.pAttr.value);
				for (int i = 0; i < this.pAttr.value.Length; i++)
				{
					ATTR elem_0 = this.pAttr.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.pAttr.value.Length; i++)
				{
					ATTR elem_0 = this.pAttr.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pAttr is not null)
			{
				this.pAttr.value = decoder.ReadArrayHeader<ATTR>();
				for (int i = 0; i < this.pAttr.value.Length; i++)
				{
					ATTR elem_0 = this.pAttr.value[i];
					elem_0 = decoder.ReadFixedStruct<ATTR>(NdrAlignment.NativePtr);
					this.pAttr.value[i] = elem_0;
				}

				for (int i = 0; i < this.pAttr.value.Length; i++)
				{
					ATTR elem_0 = this.pAttr.value[i];
					decoder.ReadStructDeferral<ATTR>(ref elem_0);
					this.pAttr.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ENTINF : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pName;
		public uint ulFlags;
		public ATTRBLOCK AttrBlock;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pName);
			encoder.WriteValue(this.ulFlags);
			encoder.WriteFixedStruct(this.AttrBlock, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pName = decoder.ReadUniquePointer<DSNAME>();
			this.ulFlags = decoder.ReadUInt32();
			this.AttrBlock = decoder.ReadFixedStruct<ATTRBLOCK>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pName is not null)
			{
				encoder.WriteConformantStruct(this.pName.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pName.value);
			}

			encoder.WriteStructDeferral(this.AttrBlock);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pName is not null)
			{
				this.pName.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pName.value);
			}

			decoder.ReadStructDeferral<ATTRBLOCK>(ref this.AttrBlock);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct PROPERTY_META_DATA_EXT : IRpcFixedStruct
	{
		public uint dwVersion;
		public long timeChanged;
		public Guid uuidDsaOriginating;
		public long usnOriginating;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwVersion);
			encoder.WriteValue(this.timeChanged);
			encoder.WriteValue(this.uuidDsaOriginating);
			encoder.WriteValue(this.usnOriginating);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwVersion = decoder.ReadUInt32();
			this.timeChanged = decoder.ReadInt64();
			this.uuidDsaOriginating = decoder.ReadUuid();
			this.usnOriginating = decoder.ReadInt64();
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
	public partial struct PROPERTY_META_DATA_EXT_VECTOR : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgMetaData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgMetaData = decoder.ReadArrayHeader<PROPERTY_META_DATA_EXT>();
		}

		public uint cNumProps;
		public PROPERTY_META_DATA_EXT[] rgMetaData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				PROPERTY_META_DATA_EXT elem_0 = this.rgMetaData[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				PROPERTY_META_DATA_EXT elem_0 = this.rgMetaData[i];
				elem_0 = decoder.ReadFixedStruct<PROPERTY_META_DATA_EXT>(NdrAlignment._8Byte);
				this.rgMetaData[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cNumProps);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cNumProps = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				PROPERTY_META_DATA_EXT elem_0 = this.rgMetaData[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				PROPERTY_META_DATA_EXT elem_0 = this.rgMetaData[i];
				decoder.ReadStructDeferral<PROPERTY_META_DATA_EXT>(ref elem_0);
				this.rgMetaData[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct REPLENTINFLIST : IRpcFixedStruct
	{
		public RpcPointer<REPLENTINFLIST> pNextEntInf;
		public ENTINF Entinf;
		public int fIsNCPrefix;
		public RpcPointer<Guid> pParentGuid;
		public RpcPointer<PROPERTY_META_DATA_EXT_VECTOR> pMetaDataExt;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNextEntInf);
			encoder.WriteFixedStruct(this.Entinf, NdrAlignment.NativePtr);
			encoder.WriteValue(this.fIsNCPrefix);
			encoder.WriteUniquePointer(this.pParentGuid);
			encoder.WriteUniquePointer(this.pMetaDataExt);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNextEntInf = decoder.ReadUniquePointer<REPLENTINFLIST>();
			this.Entinf = decoder.ReadFixedStruct<ENTINF>(NdrAlignment.NativePtr);
			this.fIsNCPrefix = decoder.ReadInt32();
			this.pParentGuid = decoder.ReadUniquePointer<Guid>();
			this.pMetaDataExt = decoder.ReadUniquePointer<PROPERTY_META_DATA_EXT_VECTOR>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNextEntInf is not null)
			{
				encoder.WriteFixedStruct(this.pNextEntInf.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNextEntInf.value);
			}

			encoder.WriteStructDeferral(this.Entinf);
			if (this.pParentGuid is not null)
			{
				encoder.WriteValue(this.pParentGuid.value);
			}

			if (this.pMetaDataExt is not null)
			{
				encoder.WriteConformantStruct(this.pMetaDataExt.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pMetaDataExt.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNextEntInf is not null)
			{
				this.pNextEntInf.value = decoder.ReadFixedStruct<REPLENTINFLIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<REPLENTINFLIST>(ref this.pNextEntInf.value);
			}

			decoder.ReadStructDeferral<ENTINF>(ref this.Entinf);
			if (this.pParentGuid is not null)
			{
				this.pParentGuid.value = decoder.ReadUuid();
			}

			if (this.pMetaDataExt is not null)
			{
				this.pMetaDataExt.value = decoder.ReadConformantStruct<PROPERTY_META_DATA_EXT_VECTOR>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<PROPERTY_META_DATA_EXT_VECTOR>(ref this.pMetaDataExt.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct UPTODATE_CURSOR_V2 : IRpcFixedStruct
	{
		public Guid uuidDsa;
		public long usnHighPropUpdate;
		public long timeLastSyncSuccess;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidDsa);
			encoder.WriteValue(this.usnHighPropUpdate);
			encoder.WriteValue(this.timeLastSyncSuccess);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidDsa = decoder.ReadUuid();
			this.usnHighPropUpdate = decoder.ReadInt64();
			this.timeLastSyncSuccess = decoder.ReadInt64();
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
	public partial struct UPTODATE_VECTOR_V2_EXT : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgCursors);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgCursors = decoder.ReadArrayHeader<UPTODATE_CURSOR_V2>();
		}

		public uint dwVersion;
		public uint dwReserved1;
		public uint cNumCursors;
		public uint dwReserved2;
		public UPTODATE_CURSOR_V2[] rgCursors;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgCursors.Length; i++)
			{
				UPTODATE_CURSOR_V2 elem_0 = this.rgCursors[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgCursors.Length; i++)
			{
				UPTODATE_CURSOR_V2 elem_0 = this.rgCursors[i];
				elem_0 = decoder.ReadFixedStruct<UPTODATE_CURSOR_V2>(NdrAlignment._8Byte);
				this.rgCursors[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwVersion);
			encoder.WriteValue(this.dwReserved1);
			encoder.WriteValue(this.cNumCursors);
			encoder.WriteValue(this.dwReserved2);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwVersion = decoder.ReadUInt32();
			this.dwReserved1 = decoder.ReadUInt32();
			this.cNumCursors = decoder.ReadUInt32();
			this.dwReserved2 = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgCursors.Length; i++)
			{
				UPTODATE_CURSOR_V2 elem_0 = this.rgCursors[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgCursors.Length; i++)
			{
				UPTODATE_CURSOR_V2 elem_0 = this.rgCursors[i];
				decoder.ReadStructDeferral<UPTODATE_CURSOR_V2>(ref elem_0);
				this.rgCursors[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct VALUE_META_DATA_EXT_V1 : IRpcFixedStruct
	{
		public long timeCreated;
		public PROPERTY_META_DATA_EXT MetaData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.timeCreated);
			encoder.WriteFixedStruct(this.MetaData, NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.timeCreated = decoder.ReadInt64();
			this.MetaData = decoder.ReadFixedStruct<PROPERTY_META_DATA_EXT>(NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.MetaData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<PROPERTY_META_DATA_EXT>(ref this.MetaData);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct VALUE_META_DATA_EXT_V3 : IRpcFixedStruct
	{
		public long timeCreated;
		public PROPERTY_META_DATA_EXT MetaData;
		public uint unused1;
		public uint unused2;
		public uint unused3;
		public long timeExpired;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.timeCreated);
			encoder.WriteFixedStruct(this.MetaData, NdrAlignment._8Byte);
			encoder.WriteValue(this.unused1);
			encoder.WriteValue(this.unused2);
			encoder.WriteValue(this.unused3);
			encoder.WriteValue(this.timeExpired);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.timeCreated = decoder.ReadInt64();
			this.MetaData = decoder.ReadFixedStruct<PROPERTY_META_DATA_EXT>(NdrAlignment._8Byte);
			this.unused1 = decoder.ReadUInt32();
			this.unused2 = decoder.ReadUInt32();
			this.unused3 = decoder.ReadUInt32();
			this.timeExpired = decoder.ReadInt64();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.MetaData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<PROPERTY_META_DATA_EXT>(ref this.MetaData);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct REPLVALINF_V1 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pObject;
		public uint attrTyp;
		public ATTRVAL Aval;
		public int fIsPresent;
		public VALUE_META_DATA_EXT_V1 MetaData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pObject);
			encoder.WriteValue(this.attrTyp);
			encoder.WriteFixedStruct(this.Aval, NdrAlignment.NativePtr);
			encoder.WriteValue(this.fIsPresent);
			encoder.WriteFixedStruct(this.MetaData, NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pObject = decoder.ReadUniquePointer<DSNAME>();
			this.attrTyp = decoder.ReadUInt32();
			this.Aval = decoder.ReadFixedStruct<ATTRVAL>(NdrAlignment.NativePtr);
			this.fIsPresent = decoder.ReadInt32();
			this.MetaData = decoder.ReadFixedStruct<VALUE_META_DATA_EXT_V1>(NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pObject is not null)
			{
				encoder.WriteConformantStruct(this.pObject.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pObject.value);
			}

			encoder.WriteStructDeferral(this.Aval);
			encoder.WriteStructDeferral(this.MetaData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pObject is not null)
			{
				this.pObject.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pObject.value);
			}

			decoder.ReadStructDeferral<ATTRVAL>(ref this.Aval);
			decoder.ReadStructDeferral<VALUE_META_DATA_EXT_V1>(ref this.MetaData);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct REPLVALINF_V3 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pObject;
		public uint attrTyp;
		public ATTRVAL Aval;
		public int fIsPresent;
		public VALUE_META_DATA_EXT_V3 MetaData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pObject);
			encoder.WriteValue(this.attrTyp);
			encoder.WriteFixedStruct(this.Aval, NdrAlignment.NativePtr);
			encoder.WriteValue(this.fIsPresent);
			encoder.WriteFixedStruct(this.MetaData, NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pObject = decoder.ReadUniquePointer<DSNAME>();
			this.attrTyp = decoder.ReadUInt32();
			this.Aval = decoder.ReadFixedStruct<ATTRVAL>(NdrAlignment.NativePtr);
			this.fIsPresent = decoder.ReadInt32();
			this.MetaData = decoder.ReadFixedStruct<VALUE_META_DATA_EXT_V3>(NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pObject is not null)
			{
				encoder.WriteConformantStruct(this.pObject.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pObject.value);
			}

			encoder.WriteStructDeferral(this.Aval);
			encoder.WriteStructDeferral(this.MetaData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pObject is not null)
			{
				this.pObject.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pObject.value);
			}

			decoder.ReadStructDeferral<ATTRVAL>(ref this.Aval);
			decoder.ReadStructDeferral<VALUE_META_DATA_EXT_V3>(ref this.MetaData);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct REPLTIMES : IRpcFixedStruct
	{
		public byte[] rgTimes;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			if (this.rgTimes == null)
				this.rgTimes = new byte[84];
			for (int i = 0; i < 84; i++)
			{
				byte elem_0 = this.rgTimes[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			if (this.rgTimes == null)
				this.rgTimes = new byte[84];
			for (int i = 0; i < 84; i++)
			{
				byte elem_0 = this.rgTimes[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.rgTimes[i] = elem_0;
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
	public partial struct DS_NAME_RESULT_ITEMW : IRpcFixedStruct
	{
		public uint status;
		public RpcPointer<string> pDomain;
		public RpcPointer<string> pName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.status);
			encoder.WriteUniquePointer(this.pDomain);
			encoder.WriteUniquePointer(this.pName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.status = decoder.ReadUInt32();
			this.pDomain = decoder.ReadUniquePointer<string>();
			this.pName = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pDomain is not null)
			{
				encoder.WriteWideCharString(this.pDomain.value);
			}

			if (this.pName is not null)
			{
				encoder.WriteWideCharString(this.pName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pDomain is not null)
			{
				this.pDomain.value = decoder.ReadWideCharString();
			}

			if (this.pName is not null)
			{
				this.pName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_NAME_RESULTW : IRpcFixedStruct
	{
		public uint cItems;
		public RpcPointer<DS_NAME_RESULT_ITEMW[]> rItems;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cItems);
			encoder.WriteUniquePointer(this.rItems);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cItems = decoder.ReadUInt32();
			this.rItems = decoder.ReadUniquePointer<DS_NAME_RESULT_ITEMW[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.rItems is not null)
			{
				encoder.WriteArrayHeader(this.rItems.value);
				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_NAME_RESULT_ITEMW elem_0 = this.rItems.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_NAME_RESULT_ITEMW elem_0 = this.rItems.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.rItems is not null)
			{
				this.rItems.value = decoder.ReadArrayHeader<DS_NAME_RESULT_ITEMW>();
				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_NAME_RESULT_ITEMW elem_0 = this.rItems.value[i];
					elem_0 = decoder.ReadFixedStruct<DS_NAME_RESULT_ITEMW>(NdrAlignment.NativePtr);
					this.rItems.value[i] = elem_0;
				}

				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_NAME_RESULT_ITEMW elem_0 = this.rItems.value[i];
					decoder.ReadStructDeferral<DS_NAME_RESULT_ITEMW>(ref elem_0);
					this.rItems.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_DOMAIN_CONTROLLER_INFO_1W : IRpcFixedStruct
	{
		public RpcPointer<string> NetbiosName;
		public RpcPointer<string> DnsHostName;
		public RpcPointer<string> SiteName;
		public RpcPointer<string> ComputerObjectName;
		public RpcPointer<string> ServerObjectName;
		public int fIsPdc;
		public int fDsEnabled;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.NetbiosName);
			encoder.WriteUniquePointer(this.DnsHostName);
			encoder.WriteUniquePointer(this.SiteName);
			encoder.WriteUniquePointer(this.ComputerObjectName);
			encoder.WriteUniquePointer(this.ServerObjectName);
			encoder.WriteValue(this.fIsPdc);
			encoder.WriteValue(this.fDsEnabled);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.NetbiosName = decoder.ReadUniquePointer<string>();
			this.DnsHostName = decoder.ReadUniquePointer<string>();
			this.SiteName = decoder.ReadUniquePointer<string>();
			this.ComputerObjectName = decoder.ReadUniquePointer<string>();
			this.ServerObjectName = decoder.ReadUniquePointer<string>();
			this.fIsPdc = decoder.ReadInt32();
			this.fDsEnabled = decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.NetbiosName is not null)
			{
				encoder.WriteWideCharString(this.NetbiosName.value);
			}

			if (this.DnsHostName is not null)
			{
				encoder.WriteWideCharString(this.DnsHostName.value);
			}

			if (this.SiteName is not null)
			{
				encoder.WriteWideCharString(this.SiteName.value);
			}

			if (this.ComputerObjectName is not null)
			{
				encoder.WriteWideCharString(this.ComputerObjectName.value);
			}

			if (this.ServerObjectName is not null)
			{
				encoder.WriteWideCharString(this.ServerObjectName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.NetbiosName is not null)
			{
				this.NetbiosName.value = decoder.ReadWideCharString();
			}

			if (this.DnsHostName is not null)
			{
				this.DnsHostName.value = decoder.ReadWideCharString();
			}

			if (this.SiteName is not null)
			{
				this.SiteName.value = decoder.ReadWideCharString();
			}

			if (this.ComputerObjectName is not null)
			{
				this.ComputerObjectName.value = decoder.ReadWideCharString();
			}

			if (this.ServerObjectName is not null)
			{
				this.ServerObjectName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_DOMAIN_CONTROLLER_INFO_2W : IRpcFixedStruct
	{
		public RpcPointer<string> NetbiosName;
		public RpcPointer<string> DnsHostName;
		public RpcPointer<string> SiteName;
		public RpcPointer<string> SiteObjectName;
		public RpcPointer<string> ComputerObjectName;
		public RpcPointer<string> ServerObjectName;
		public RpcPointer<string> NtdsDsaObjectName;
		public int fIsPdc;
		public int fDsEnabled;
		public int fIsGc;
		public Guid SiteObjectGuid;
		public Guid ComputerObjectGuid;
		public Guid ServerObjectGuid;
		public Guid NtdsDsaObjectGuid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.NetbiosName);
			encoder.WriteUniquePointer(this.DnsHostName);
			encoder.WriteUniquePointer(this.SiteName);
			encoder.WriteUniquePointer(this.SiteObjectName);
			encoder.WriteUniquePointer(this.ComputerObjectName);
			encoder.WriteUniquePointer(this.ServerObjectName);
			encoder.WriteUniquePointer(this.NtdsDsaObjectName);
			encoder.WriteValue(this.fIsPdc);
			encoder.WriteValue(this.fDsEnabled);
			encoder.WriteValue(this.fIsGc);
			encoder.WriteValue(this.SiteObjectGuid);
			encoder.WriteValue(this.ComputerObjectGuid);
			encoder.WriteValue(this.ServerObjectGuid);
			encoder.WriteValue(this.NtdsDsaObjectGuid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.NetbiosName = decoder.ReadUniquePointer<string>();
			this.DnsHostName = decoder.ReadUniquePointer<string>();
			this.SiteName = decoder.ReadUniquePointer<string>();
			this.SiteObjectName = decoder.ReadUniquePointer<string>();
			this.ComputerObjectName = decoder.ReadUniquePointer<string>();
			this.ServerObjectName = decoder.ReadUniquePointer<string>();
			this.NtdsDsaObjectName = decoder.ReadUniquePointer<string>();
			this.fIsPdc = decoder.ReadInt32();
			this.fDsEnabled = decoder.ReadInt32();
			this.fIsGc = decoder.ReadInt32();
			this.SiteObjectGuid = decoder.ReadUuid();
			this.ComputerObjectGuid = decoder.ReadUuid();
			this.ServerObjectGuid = decoder.ReadUuid();
			this.NtdsDsaObjectGuid = decoder.ReadUuid();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.NetbiosName is not null)
			{
				encoder.WriteWideCharString(this.NetbiosName.value);
			}

			if (this.DnsHostName is not null)
			{
				encoder.WriteWideCharString(this.DnsHostName.value);
			}

			if (this.SiteName is not null)
			{
				encoder.WriteWideCharString(this.SiteName.value);
			}

			if (this.SiteObjectName is not null)
			{
				encoder.WriteWideCharString(this.SiteObjectName.value);
			}

			if (this.ComputerObjectName is not null)
			{
				encoder.WriteWideCharString(this.ComputerObjectName.value);
			}

			if (this.ServerObjectName is not null)
			{
				encoder.WriteWideCharString(this.ServerObjectName.value);
			}

			if (this.NtdsDsaObjectName is not null)
			{
				encoder.WriteWideCharString(this.NtdsDsaObjectName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.NetbiosName is not null)
			{
				this.NetbiosName.value = decoder.ReadWideCharString();
			}

			if (this.DnsHostName is not null)
			{
				this.DnsHostName.value = decoder.ReadWideCharString();
			}

			if (this.SiteName is not null)
			{
				this.SiteName.value = decoder.ReadWideCharString();
			}

			if (this.SiteObjectName is not null)
			{
				this.SiteObjectName.value = decoder.ReadWideCharString();
			}

			if (this.ComputerObjectName is not null)
			{
				this.ComputerObjectName.value = decoder.ReadWideCharString();
			}

			if (this.ServerObjectName is not null)
			{
				this.ServerObjectName.value = decoder.ReadWideCharString();
			}

			if (this.NtdsDsaObjectName is not null)
			{
				this.NtdsDsaObjectName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_DOMAIN_CONTROLLER_INFO_3W : IRpcFixedStruct
	{
		public RpcPointer<string> NetbiosName;
		public RpcPointer<string> DnsHostName;
		public RpcPointer<string> SiteName;
		public RpcPointer<string> SiteObjectName;
		public RpcPointer<string> ComputerObjectName;
		public RpcPointer<string> ServerObjectName;
		public RpcPointer<string> NtdsDsaObjectName;
		public int fIsPdc;
		public int fDsEnabled;
		public int fIsGc;
		public int fIsRodc;
		public Guid SiteObjectGuid;
		public Guid ComputerObjectGuid;
		public Guid ServerObjectGuid;
		public Guid NtdsDsaObjectGuid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.NetbiosName);
			encoder.WriteUniquePointer(this.DnsHostName);
			encoder.WriteUniquePointer(this.SiteName);
			encoder.WriteUniquePointer(this.SiteObjectName);
			encoder.WriteUniquePointer(this.ComputerObjectName);
			encoder.WriteUniquePointer(this.ServerObjectName);
			encoder.WriteUniquePointer(this.NtdsDsaObjectName);
			encoder.WriteValue(this.fIsPdc);
			encoder.WriteValue(this.fDsEnabled);
			encoder.WriteValue(this.fIsGc);
			encoder.WriteValue(this.fIsRodc);
			encoder.WriteValue(this.SiteObjectGuid);
			encoder.WriteValue(this.ComputerObjectGuid);
			encoder.WriteValue(this.ServerObjectGuid);
			encoder.WriteValue(this.NtdsDsaObjectGuid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.NetbiosName = decoder.ReadUniquePointer<string>();
			this.DnsHostName = decoder.ReadUniquePointer<string>();
			this.SiteName = decoder.ReadUniquePointer<string>();
			this.SiteObjectName = decoder.ReadUniquePointer<string>();
			this.ComputerObjectName = decoder.ReadUniquePointer<string>();
			this.ServerObjectName = decoder.ReadUniquePointer<string>();
			this.NtdsDsaObjectName = decoder.ReadUniquePointer<string>();
			this.fIsPdc = decoder.ReadInt32();
			this.fDsEnabled = decoder.ReadInt32();
			this.fIsGc = decoder.ReadInt32();
			this.fIsRodc = decoder.ReadInt32();
			this.SiteObjectGuid = decoder.ReadUuid();
			this.ComputerObjectGuid = decoder.ReadUuid();
			this.ServerObjectGuid = decoder.ReadUuid();
			this.NtdsDsaObjectGuid = decoder.ReadUuid();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.NetbiosName is not null)
			{
				encoder.WriteWideCharString(this.NetbiosName.value);
			}

			if (this.DnsHostName is not null)
			{
				encoder.WriteWideCharString(this.DnsHostName.value);
			}

			if (this.SiteName is not null)
			{
				encoder.WriteWideCharString(this.SiteName.value);
			}

			if (this.SiteObjectName is not null)
			{
				encoder.WriteWideCharString(this.SiteObjectName.value);
			}

			if (this.ComputerObjectName is not null)
			{
				encoder.WriteWideCharString(this.ComputerObjectName.value);
			}

			if (this.ServerObjectName is not null)
			{
				encoder.WriteWideCharString(this.ServerObjectName.value);
			}

			if (this.NtdsDsaObjectName is not null)
			{
				encoder.WriteWideCharString(this.NtdsDsaObjectName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.NetbiosName is not null)
			{
				this.NetbiosName.value = decoder.ReadWideCharString();
			}

			if (this.DnsHostName is not null)
			{
				this.DnsHostName.value = decoder.ReadWideCharString();
			}

			if (this.SiteName is not null)
			{
				this.SiteName.value = decoder.ReadWideCharString();
			}

			if (this.SiteObjectName is not null)
			{
				this.SiteObjectName.value = decoder.ReadWideCharString();
			}

			if (this.ComputerObjectName is not null)
			{
				this.ComputerObjectName.value = decoder.ReadWideCharString();
			}

			if (this.ServerObjectName is not null)
			{
				this.ServerObjectName.value = decoder.ReadWideCharString();
			}

			if (this.NtdsDsaObjectName is not null)
			{
				this.NtdsDsaObjectName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_DOMAIN_CONTROLLER_INFO_FFFFFFFFW : IRpcFixedStruct
	{
		public uint IPAddress;
		public uint NotificationCount;
		public uint secTimeConnected;
		public uint Flags;
		public uint TotalRequests;
		public uint Reserved1;
		public RpcPointer<string> UserName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.IPAddress);
			encoder.WriteValue(this.NotificationCount);
			encoder.WriteValue(this.secTimeConnected);
			encoder.WriteValue(this.Flags);
			encoder.WriteValue(this.TotalRequests);
			encoder.WriteValue(this.Reserved1);
			encoder.WriteUniquePointer(this.UserName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.IPAddress = decoder.ReadUInt32();
			this.NotificationCount = decoder.ReadUInt32();
			this.secTimeConnected = decoder.ReadUInt32();
			this.Flags = decoder.ReadUInt32();
			this.TotalRequests = decoder.ReadUInt32();
			this.Reserved1 = decoder.ReadUInt32();
			this.UserName = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.UserName is not null)
			{
				encoder.WriteWideCharString(this.UserName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.UserName is not null)
			{
				this.UserName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ENTINFLIST : IRpcFixedStruct
	{
		public RpcPointer<ENTINFLIST> pNextEntInf;
		public ENTINF Entinf;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNextEntInf);
			encoder.WriteFixedStruct(this.Entinf, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNextEntInf = decoder.ReadUniquePointer<ENTINFLIST>();
			this.Entinf = decoder.ReadFixedStruct<ENTINF>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNextEntInf is not null)
			{
				encoder.WriteFixedStruct(this.pNextEntInf.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNextEntInf.value);
			}

			encoder.WriteStructDeferral(this.Entinf);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNextEntInf is not null)
			{
				this.pNextEntInf.value = decoder.ReadFixedStruct<ENTINFLIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ENTINFLIST>(ref this.pNextEntInf.value);
			}

			decoder.ReadStructDeferral<ENTINF>(ref this.Entinf);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct INTFORMPROB_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public uint dsid;
		public uint extendedErr;
		public uint extendedData;
		public ushort problem;
		public uint type;
		public int valReturned;
		public ATTRVAL Val;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dsid);
			encoder.WriteValue(this.extendedErr);
			encoder.WriteValue(this.extendedData);
			encoder.WriteValue(this.problem);
			encoder.WriteValue(this.type);
			encoder.WriteValue(this.valReturned);
			encoder.WriteFixedStruct(this.Val, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dsid = decoder.ReadUInt32();
			this.extendedErr = decoder.ReadUInt32();
			this.extendedData = decoder.ReadUInt32();
			this.problem = decoder.ReadUInt16();
			this.type = decoder.ReadUInt32();
			this.valReturned = decoder.ReadInt32();
			this.Val = decoder.ReadFixedStruct<ATTRVAL>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Val);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ATTRVAL>(ref this.Val);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct PROBLEMLIST_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public RpcPointer<PROBLEMLIST_DRS_WIRE_V1> pNextProblem;
		public INTFORMPROB_DRS_WIRE_V1 intprob;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNextProblem);
			encoder.WriteFixedStruct(this.intprob, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNextProblem = decoder.ReadUniquePointer<PROBLEMLIST_DRS_WIRE_V1>();
			this.intprob = decoder.ReadFixedStruct<INTFORMPROB_DRS_WIRE_V1>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNextProblem is not null)
			{
				encoder.WriteFixedStruct(this.pNextProblem.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNextProblem.value);
			}

			encoder.WriteStructDeferral(this.intprob);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNextProblem is not null)
			{
				this.pNextProblem.value = decoder.ReadFixedStruct<PROBLEMLIST_DRS_WIRE_V1>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<PROBLEMLIST_DRS_WIRE_V1>(ref this.pNextProblem.value);
			}

			decoder.ReadStructDeferral<INTFORMPROB_DRS_WIRE_V1>(ref this.intprob);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ATRERR_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pObject;
		public uint count;
		public PROBLEMLIST_DRS_WIRE_V1 FirstProblem;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pObject);
			encoder.WriteValue(this.count);
			encoder.WriteFixedStruct(this.FirstProblem, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pObject = decoder.ReadUniquePointer<DSNAME>();
			this.count = decoder.ReadUInt32();
			this.FirstProblem = decoder.ReadFixedStruct<PROBLEMLIST_DRS_WIRE_V1>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pObject is not null)
			{
				encoder.WriteConformantStruct(this.pObject.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pObject.value);
			}

			encoder.WriteStructDeferral(this.FirstProblem);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pObject is not null)
			{
				this.pObject.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pObject.value);
			}

			decoder.ReadStructDeferral<PROBLEMLIST_DRS_WIRE_V1>(ref this.FirstProblem);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct NAMERR_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public uint dsid;
		public uint extendedErr;
		public uint extendedData;
		public ushort problem;
		public RpcPointer<DSNAME> pMatched;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dsid);
			encoder.WriteValue(this.extendedErr);
			encoder.WriteValue(this.extendedData);
			encoder.WriteValue(this.problem);
			encoder.WriteUniquePointer(this.pMatched);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dsid = decoder.ReadUInt32();
			this.extendedErr = decoder.ReadUInt32();
			this.extendedData = decoder.ReadUInt32();
			this.problem = decoder.ReadUInt16();
			this.pMatched = decoder.ReadUniquePointer<DSNAME>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pMatched is not null)
			{
				encoder.WriteConformantStruct(this.pMatched.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pMatched.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pMatched is not null)
			{
				this.pMatched.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pMatched.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct NAMERESOP_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public byte nameRes;
		public byte unusedPad;
		public ushort nextRDN;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.nameRes);
			encoder.WriteValue(this.unusedPad);
			encoder.WriteValue(this.nextRDN);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.nameRes = decoder.ReadUnsignedChar();
			this.unusedPad = decoder.ReadUnsignedChar();
			this.nextRDN = decoder.ReadUInt16();
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
	public partial struct DSA_ADDRESS_LIST_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public RpcPointer<DSA_ADDRESS_LIST_DRS_WIRE_V1> pNextAddress;
		public RpcPointer<ms_dtyp.RPC_UNICODE_STRING> pAddress;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNextAddress);
			encoder.WriteUniquePointer(this.pAddress);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNextAddress = decoder.ReadUniquePointer<DSA_ADDRESS_LIST_DRS_WIRE_V1>();
			this.pAddress = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNextAddress is not null)
			{
				encoder.WriteFixedStruct(this.pNextAddress.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNextAddress.value);
			}

			if (this.pAddress is not null)
			{
				encoder.WriteFixedStruct(this.pAddress.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pAddress.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNextAddress is not null)
			{
				this.pNextAddress.value = decoder.ReadFixedStruct<DSA_ADDRESS_LIST_DRS_WIRE_V1>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<DSA_ADDRESS_LIST_DRS_WIRE_V1>(ref this.pNextAddress.value);
			}

			if (this.pAddress is not null)
			{
				this.pAddress.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.pAddress.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct CONTREF_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pTarget;
		public NAMERESOP_DRS_WIRE_V1 OpState;
		public ushort aliasRDN;
		public ushort RDNsInternal;
		public ushort refType;
		public ushort count;
		public RpcPointer<DSA_ADDRESS_LIST_DRS_WIRE_V1> pDAL;
		public RpcPointer<CONTREF_DRS_WIRE_V1> pNextContRef;
		public int bNewChoice;
		public byte choice;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pTarget);
			encoder.WriteFixedStruct(this.OpState, NdrAlignment._2Byte);
			encoder.WriteValue(this.aliasRDN);
			encoder.WriteValue(this.RDNsInternal);
			encoder.WriteValue(this.refType);
			encoder.WriteValue(this.count);
			encoder.WriteUniquePointer(this.pDAL);
			encoder.WriteUniquePointer(this.pNextContRef);
			encoder.WriteValue(this.bNewChoice);
			encoder.WriteValue(this.choice);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pTarget = decoder.ReadUniquePointer<DSNAME>();
			this.OpState = decoder.ReadFixedStruct<NAMERESOP_DRS_WIRE_V1>(NdrAlignment._2Byte);
			this.aliasRDN = decoder.ReadUInt16();
			this.RDNsInternal = decoder.ReadUInt16();
			this.refType = decoder.ReadUInt16();
			this.count = decoder.ReadUInt16();
			this.pDAL = decoder.ReadUniquePointer<DSA_ADDRESS_LIST_DRS_WIRE_V1>();
			this.pNextContRef = decoder.ReadUniquePointer<CONTREF_DRS_WIRE_V1>();
			this.bNewChoice = decoder.ReadInt32();
			this.choice = decoder.ReadUnsignedChar();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pTarget is not null)
			{
				encoder.WriteConformantStruct(this.pTarget.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pTarget.value);
			}

			encoder.WriteStructDeferral(this.OpState);
			if (this.pDAL is not null)
			{
				encoder.WriteFixedStruct(this.pDAL.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pDAL.value);
			}

			if (this.pNextContRef is not null)
			{
				encoder.WriteFixedStruct(this.pNextContRef.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pNextContRef.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pTarget is not null)
			{
				this.pTarget.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pTarget.value);
			}

			decoder.ReadStructDeferral<NAMERESOP_DRS_WIRE_V1>(ref this.OpState);
			if (this.pDAL is not null)
			{
				this.pDAL.value = decoder.ReadFixedStruct<DSA_ADDRESS_LIST_DRS_WIRE_V1>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<DSA_ADDRESS_LIST_DRS_WIRE_V1>(ref this.pDAL.value);
			}

			if (this.pNextContRef is not null)
			{
				this.pNextContRef.value = decoder.ReadFixedStruct<CONTREF_DRS_WIRE_V1>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<CONTREF_DRS_WIRE_V1>(ref this.pNextContRef.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct REFERR_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public uint dsid;
		public uint extendedErr;
		public uint extendedData;
		public CONTREF_DRS_WIRE_V1 Refer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dsid);
			encoder.WriteValue(this.extendedErr);
			encoder.WriteValue(this.extendedData);
			encoder.WriteFixedStruct(this.Refer, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dsid = decoder.ReadUInt32();
			this.extendedErr = decoder.ReadUInt32();
			this.extendedData = decoder.ReadUInt32();
			this.Refer = decoder.ReadFixedStruct<CONTREF_DRS_WIRE_V1>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Refer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<CONTREF_DRS_WIRE_V1>(ref this.Refer);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SECERR_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public uint dsid;
		public uint extendedErr;
		public uint extendedData;
		public ushort problem;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dsid);
			encoder.WriteValue(this.extendedErr);
			encoder.WriteValue(this.extendedData);
			encoder.WriteValue(this.problem);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dsid = decoder.ReadUInt32();
			this.extendedErr = decoder.ReadUInt32();
			this.extendedData = decoder.ReadUInt32();
			this.problem = decoder.ReadUInt16();
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
	public partial struct SVCERR_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public uint dsid;
		public uint extendedErr;
		public uint extendedData;
		public ushort problem;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dsid);
			encoder.WriteValue(this.extendedErr);
			encoder.WriteValue(this.extendedData);
			encoder.WriteValue(this.problem);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dsid = decoder.ReadUInt32();
			this.extendedErr = decoder.ReadUInt32();
			this.extendedData = decoder.ReadUInt32();
			this.problem = decoder.ReadUInt16();
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
	public partial struct UPDERR_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public uint dsid;
		public uint extendedErr;
		public uint extendedData;
		public ushort problem;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dsid);
			encoder.WriteValue(this.extendedErr);
			encoder.WriteValue(this.extendedData);
			encoder.WriteValue(this.problem);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dsid = decoder.ReadUInt32();
			this.extendedErr = decoder.ReadUInt32();
			this.extendedData = decoder.ReadUInt32();
			this.problem = decoder.ReadUInt16();
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
	public partial struct SYSERR_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public uint dsid;
		public uint extendedErr;
		public uint extendedData;
		public ushort problem;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dsid);
			encoder.WriteValue(this.extendedErr);
			encoder.WriteValue(this.extendedData);
			encoder.WriteValue(this.problem);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dsid = decoder.ReadUInt32();
			this.extendedErr = decoder.ReadUInt32();
			this.extendedData = decoder.ReadUInt32();
			this.problem = decoder.ReadUInt16();
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
	public partial struct DIRERR_DRS_WIRE_V1 : IRpcFixedStruct
	{
		public uint unionSwitch;
		public ATRERR_DRS_WIRE_V1 AtrErr;
		public NAMERR_DRS_WIRE_V1 NamErr;
		public REFERR_DRS_WIRE_V1 RefErr;
		public SECERR_DRS_WIRE_V1 SecErr;
		public SVCERR_DRS_WIRE_V1 SvcErr;
		public UPDERR_DRS_WIRE_V1 UpdErr;
		public SYSERR_DRS_WIRE_V1 SysErr;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.AtrErr, NdrAlignment.NativePtr);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.NamErr, NdrAlignment.NativePtr);
					break;
				case 3U:
					encoder.WriteFixedStruct(this.RefErr, NdrAlignment.NativePtr);
					break;
				case 4U:
					encoder.WriteFixedStruct(this.SecErr, NdrAlignment._4Byte);
					break;
				case 5U:
					encoder.WriteFixedStruct(this.SvcErr, NdrAlignment._4Byte);
					break;
				case 6U:
					encoder.WriteFixedStruct(this.UpdErr, NdrAlignment._4Byte);
					break;
				case 7U:
					encoder.WriteFixedStruct(this.SysErr, NdrAlignment._4Byte);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.AtrErr = decoder.ReadFixedStruct<ATRERR_DRS_WIRE_V1>(NdrAlignment.NativePtr);
					break;
				case 2U:
					this.NamErr = decoder.ReadFixedStruct<NAMERR_DRS_WIRE_V1>(NdrAlignment.NativePtr);
					break;
				case 3U:
					this.RefErr = decoder.ReadFixedStruct<REFERR_DRS_WIRE_V1>(NdrAlignment.NativePtr);
					break;
				case 4U:
					this.SecErr = decoder.ReadFixedStruct<SECERR_DRS_WIRE_V1>(NdrAlignment._4Byte);
					break;
				case 5U:
					this.SvcErr = decoder.ReadFixedStruct<SVCERR_DRS_WIRE_V1>(NdrAlignment._4Byte);
					break;
				case 6U:
					this.UpdErr = decoder.ReadFixedStruct<UPDERR_DRS_WIRE_V1>(NdrAlignment._4Byte);
					break;
				case 7U:
					this.SysErr = decoder.ReadFixedStruct<SYSERR_DRS_WIRE_V1>(NdrAlignment._4Byte);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteStructDeferral(this.AtrErr);
					break;
				case 2U:
					encoder.WriteStructDeferral(this.NamErr);
					break;
				case 3U:
					encoder.WriteStructDeferral(this.RefErr);
					break;
				case 4U:
					encoder.WriteStructDeferral(this.SecErr);
					break;
				case 5U:
					encoder.WriteStructDeferral(this.SvcErr);
					break;
				case 6U:
					encoder.WriteStructDeferral(this.UpdErr);
					break;
				case 7U:
					encoder.WriteStructDeferral(this.SysErr);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					decoder.ReadStructDeferral<ATRERR_DRS_WIRE_V1>(ref this.AtrErr);
					break;
				case 2U:
					decoder.ReadStructDeferral<NAMERR_DRS_WIRE_V1>(ref this.NamErr);
					break;
				case 3U:
					decoder.ReadStructDeferral<REFERR_DRS_WIRE_V1>(ref this.RefErr);
					break;
				case 4U:
					decoder.ReadStructDeferral<SECERR_DRS_WIRE_V1>(ref this.SecErr);
					break;
				case 5U:
					decoder.ReadStructDeferral<SVCERR_DRS_WIRE_V1>(ref this.SvcErr);
					break;
				case 6U:
					decoder.ReadStructDeferral<UPDERR_DRS_WIRE_V1>(ref this.UpdErr);
					break;
				case 7U:
					decoder.ReadStructDeferral<SYSERR_DRS_WIRE_V1>(ref this.SysErr);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_NEIGHBORW : IRpcFixedStruct
	{
		public RpcPointer<string> pszNamingContext;
		public RpcPointer<string> pszSourceDsaDN;
		public RpcPointer<string> pszSourceDsaAddress;
		public RpcPointer<string> pszAsyncIntersiteTransportDN;
		public uint dwReplicaFlags;
		public uint dwReserved;
		public Guid uuidNamingContextObjGuid;
		public Guid uuidSourceDsaObjGuid;
		public Guid uuidSourceDsaInvocationID;
		public Guid uuidAsyncIntersiteTransportObjGuid;
		public long usnLastObjChangeSynced;
		public long usnAttributeFilter;
		public ms_dtyp.FILETIME ftimeLastSyncSuccess;
		public ms_dtyp.FILETIME ftimeLastSyncAttempt;
		public uint dwLastSyncResult;
		public uint cNumConsecutiveSyncFailures;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pszNamingContext);
			encoder.WriteUniquePointer(this.pszSourceDsaDN);
			encoder.WriteUniquePointer(this.pszSourceDsaAddress);
			encoder.WriteUniquePointer(this.pszAsyncIntersiteTransportDN);
			encoder.WriteValue(this.dwReplicaFlags);
			encoder.WriteValue(this.dwReserved);
			encoder.WriteValue(this.uuidNamingContextObjGuid);
			encoder.WriteValue(this.uuidSourceDsaObjGuid);
			encoder.WriteValue(this.uuidSourceDsaInvocationID);
			encoder.WriteValue(this.uuidAsyncIntersiteTransportObjGuid);
			encoder.WriteValue(this.usnLastObjChangeSynced);
			encoder.WriteValue(this.usnAttributeFilter);
			encoder.WriteFixedStruct(this.ftimeLastSyncSuccess, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.ftimeLastSyncAttempt, NdrAlignment._4Byte);
			encoder.WriteValue(this.dwLastSyncResult);
			encoder.WriteValue(this.cNumConsecutiveSyncFailures);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pszNamingContext = decoder.ReadUniquePointer<string>();
			this.pszSourceDsaDN = decoder.ReadUniquePointer<string>();
			this.pszSourceDsaAddress = decoder.ReadUniquePointer<string>();
			this.pszAsyncIntersiteTransportDN = decoder.ReadUniquePointer<string>();
			this.dwReplicaFlags = decoder.ReadUInt32();
			this.dwReserved = decoder.ReadUInt32();
			this.uuidNamingContextObjGuid = decoder.ReadUuid();
			this.uuidSourceDsaObjGuid = decoder.ReadUuid();
			this.uuidSourceDsaInvocationID = decoder.ReadUuid();
			this.uuidAsyncIntersiteTransportObjGuid = decoder.ReadUuid();
			this.usnLastObjChangeSynced = decoder.ReadInt64();
			this.usnAttributeFilter = decoder.ReadInt64();
			this.ftimeLastSyncSuccess = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.ftimeLastSyncAttempt = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.dwLastSyncResult = decoder.ReadUInt32();
			this.cNumConsecutiveSyncFailures = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszNamingContext is not null)
			{
				encoder.WriteWideCharString(this.pszNamingContext.value);
			}

			if (this.pszSourceDsaDN is not null)
			{
				encoder.WriteWideCharString(this.pszSourceDsaDN.value);
			}

			if (this.pszSourceDsaAddress is not null)
			{
				encoder.WriteWideCharString(this.pszSourceDsaAddress.value);
			}

			if (this.pszAsyncIntersiteTransportDN is not null)
			{
				encoder.WriteWideCharString(this.pszAsyncIntersiteTransportDN.value);
			}

			encoder.WriteStructDeferral(this.ftimeLastSyncSuccess);
			encoder.WriteStructDeferral(this.ftimeLastSyncAttempt);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszNamingContext is not null)
			{
				this.pszNamingContext.value = decoder.ReadWideCharString();
			}

			if (this.pszSourceDsaDN is not null)
			{
				this.pszSourceDsaDN.value = decoder.ReadWideCharString();
			}

			if (this.pszSourceDsaAddress is not null)
			{
				this.pszSourceDsaAddress.value = decoder.ReadWideCharString();
			}

			if (this.pszAsyncIntersiteTransportDN is not null)
			{
				this.pszAsyncIntersiteTransportDN.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeLastSyncSuccess);
			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeLastSyncAttempt);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_NEIGHBORSW : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgNeighbor);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgNeighbor = decoder.ReadArrayHeader<DS_REPL_NEIGHBORW>();
		}

		public uint cNumNeighbors;
		public uint dwReserved;
		public DS_REPL_NEIGHBORW[] rgNeighbor;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgNeighbor.Length; i++)
			{
				DS_REPL_NEIGHBORW elem_0 = this.rgNeighbor[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgNeighbor.Length; i++)
			{
				DS_REPL_NEIGHBORW elem_0 = this.rgNeighbor[i];
				elem_0 = decoder.ReadFixedStruct<DS_REPL_NEIGHBORW>(NdrAlignment._8Byte);
				this.rgNeighbor[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cNumNeighbors);
			encoder.WriteValue(this.dwReserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cNumNeighbors = decoder.ReadUInt32();
			this.dwReserved = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgNeighbor.Length; i++)
			{
				DS_REPL_NEIGHBORW elem_0 = this.rgNeighbor[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgNeighbor.Length; i++)
			{
				DS_REPL_NEIGHBORW elem_0 = this.rgNeighbor[i];
				decoder.ReadStructDeferral<DS_REPL_NEIGHBORW>(ref elem_0);
				this.rgNeighbor[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_CURSOR : IRpcFixedStruct
	{
		public Guid uuidSourceDsaInvocationID;
		public long usnAttributeFilter;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidSourceDsaInvocationID);
			encoder.WriteValue(this.usnAttributeFilter);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidSourceDsaInvocationID = decoder.ReadUuid();
			this.usnAttributeFilter = decoder.ReadInt64();
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
	public partial struct DS_REPL_CURSORS : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgCursor);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgCursor = decoder.ReadArrayHeader<DS_REPL_CURSOR>();
		}

		public uint cNumCursors;
		public uint dwReserved;
		public DS_REPL_CURSOR[] rgCursor;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgCursor.Length; i++)
			{
				DS_REPL_CURSOR elem_0 = this.rgCursor[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgCursor.Length; i++)
			{
				DS_REPL_CURSOR elem_0 = this.rgCursor[i];
				elem_0 = decoder.ReadFixedStruct<DS_REPL_CURSOR>(NdrAlignment._8Byte);
				this.rgCursor[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cNumCursors);
			encoder.WriteValue(this.dwReserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cNumCursors = decoder.ReadUInt32();
			this.dwReserved = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgCursor.Length; i++)
			{
				DS_REPL_CURSOR elem_0 = this.rgCursor[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgCursor.Length; i++)
			{
				DS_REPL_CURSOR elem_0 = this.rgCursor[i];
				decoder.ReadStructDeferral<DS_REPL_CURSOR>(ref elem_0);
				this.rgCursor[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_ATTR_META_DATA : IRpcFixedStruct
	{
		public RpcPointer<string> pszAttributeName;
		public uint dwVersion;
		public ms_dtyp.FILETIME ftimeLastOriginatingChange;
		public Guid uuidLastOriginatingDsaInvocationID;
		public long usnOriginatingChange;
		public long usnLocalChange;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pszAttributeName);
			encoder.WriteValue(this.dwVersion);
			encoder.WriteFixedStruct(this.ftimeLastOriginatingChange, NdrAlignment._4Byte);
			encoder.WriteValue(this.uuidLastOriginatingDsaInvocationID);
			encoder.WriteValue(this.usnOriginatingChange);
			encoder.WriteValue(this.usnLocalChange);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pszAttributeName = decoder.ReadUniquePointer<string>();
			this.dwVersion = decoder.ReadUInt32();
			this.ftimeLastOriginatingChange = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.uuidLastOriginatingDsaInvocationID = decoder.ReadUuid();
			this.usnOriginatingChange = decoder.ReadInt64();
			this.usnLocalChange = decoder.ReadInt64();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszAttributeName is not null)
			{
				encoder.WriteWideCharString(this.pszAttributeName.value);
			}

			encoder.WriteStructDeferral(this.ftimeLastOriginatingChange);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszAttributeName is not null)
			{
				this.pszAttributeName.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeLastOriginatingChange);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_KCC_DSA_FAILUREW : IRpcFixedStruct
	{
		public RpcPointer<string> pszDsaDN;
		public Guid uuidDsaObjGuid;
		public ms_dtyp.FILETIME ftimeFirstFailure;
		public uint cNumFailures;
		public uint dwLastResult;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pszDsaDN);
			encoder.WriteValue(this.uuidDsaObjGuid);
			encoder.WriteFixedStruct(this.ftimeFirstFailure, NdrAlignment._4Byte);
			encoder.WriteValue(this.cNumFailures);
			encoder.WriteValue(this.dwLastResult);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pszDsaDN = decoder.ReadUniquePointer<string>();
			this.uuidDsaObjGuid = decoder.ReadUuid();
			this.ftimeFirstFailure = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.cNumFailures = decoder.ReadUInt32();
			this.dwLastResult = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszDsaDN is not null)
			{
				encoder.WriteWideCharString(this.pszDsaDN.value);
			}

			encoder.WriteStructDeferral(this.ftimeFirstFailure);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszDsaDN is not null)
			{
				this.pszDsaDN.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeFirstFailure);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_KCC_DSA_FAILURESW : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgDsaFailure);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgDsaFailure = decoder.ReadArrayHeader<DS_REPL_KCC_DSA_FAILUREW>();
		}

		public uint cNumEntries;
		public uint dwReserved;
		public DS_REPL_KCC_DSA_FAILUREW[] rgDsaFailure;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgDsaFailure.Length; i++)
			{
				DS_REPL_KCC_DSA_FAILUREW elem_0 = this.rgDsaFailure[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgDsaFailure.Length; i++)
			{
				DS_REPL_KCC_DSA_FAILUREW elem_0 = this.rgDsaFailure[i];
				elem_0 = decoder.ReadFixedStruct<DS_REPL_KCC_DSA_FAILUREW>(NdrAlignment.NativePtr);
				this.rgDsaFailure[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cNumEntries);
			encoder.WriteValue(this.dwReserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cNumEntries = decoder.ReadUInt32();
			this.dwReserved = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgDsaFailure.Length; i++)
			{
				DS_REPL_KCC_DSA_FAILUREW elem_0 = this.rgDsaFailure[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgDsaFailure.Length; i++)
			{
				DS_REPL_KCC_DSA_FAILUREW elem_0 = this.rgDsaFailure[i];
				decoder.ReadStructDeferral<DS_REPL_KCC_DSA_FAILUREW>(ref elem_0);
				this.rgDsaFailure[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_OBJ_META_DATA : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgMetaData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgMetaData = decoder.ReadArrayHeader<DS_REPL_ATTR_META_DATA>();
		}

		public uint cNumEntries;
		public uint dwReserved;
		public DS_REPL_ATTR_META_DATA[] rgMetaData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_ATTR_META_DATA elem_0 = this.rgMetaData[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_ATTR_META_DATA elem_0 = this.rgMetaData[i];
				elem_0 = decoder.ReadFixedStruct<DS_REPL_ATTR_META_DATA>(NdrAlignment._8Byte);
				this.rgMetaData[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cNumEntries);
			encoder.WriteValue(this.dwReserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cNumEntries = decoder.ReadUInt32();
			this.dwReserved = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_ATTR_META_DATA elem_0 = this.rgMetaData[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_ATTR_META_DATA elem_0 = this.rgMetaData[i];
				decoder.ReadStructDeferral<DS_REPL_ATTR_META_DATA>(ref elem_0);
				this.rgMetaData[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum DS_REPL_OP_TYPE : int
	{
		DS_REPL_OP_TYPE_SYNC = 0,
		DS_REPL_OP_TYPE_ADD = 1,
		DS_REPL_OP_TYPE_DELETE = 2,
		DS_REPL_OP_TYPE_MODIFY = 3,
		DS_REPL_OP_TYPE_UPDATE_REFS = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_OPW : IRpcFixedStruct
	{
		public ms_dtyp.FILETIME ftimeEnqueued;
		public uint ulSerialNumber;
		public uint ulPriority;
		public DS_REPL_OP_TYPE OpType;
		public uint ulOptions;
		public RpcPointer<string> pszNamingContext;
		public RpcPointer<string> pszDsaDN;
		public RpcPointer<string> pszDsaAddress;
		public Guid uuidNamingContextObjGuid;
		public Guid uuidDsaObjGuid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ftimeEnqueued, NdrAlignment._4Byte);
			encoder.WriteValue(this.ulSerialNumber);
			encoder.WriteValue(this.ulPriority);
			encoder.WriteEnumShortValue((short)this.OpType);
			encoder.WriteValue(this.ulOptions);
			encoder.WriteUniquePointer(this.pszNamingContext);
			encoder.WriteUniquePointer(this.pszDsaDN);
			encoder.WriteUniquePointer(this.pszDsaAddress);
			encoder.WriteValue(this.uuidNamingContextObjGuid);
			encoder.WriteValue(this.uuidDsaObjGuid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ftimeEnqueued = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.ulSerialNumber = decoder.ReadUInt32();
			this.ulPriority = decoder.ReadUInt32();
			this.OpType = (DS_REPL_OP_TYPE)decoder.ReadEnumShortValue();
			this.ulOptions = decoder.ReadUInt32();
			this.pszNamingContext = decoder.ReadUniquePointer<string>();
			this.pszDsaDN = decoder.ReadUniquePointer<string>();
			this.pszDsaAddress = decoder.ReadUniquePointer<string>();
			this.uuidNamingContextObjGuid = decoder.ReadUuid();
			this.uuidDsaObjGuid = decoder.ReadUuid();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ftimeEnqueued);
			if (this.pszNamingContext is not null)
			{
				encoder.WriteWideCharString(this.pszNamingContext.value);
			}

			if (this.pszDsaDN is not null)
			{
				encoder.WriteWideCharString(this.pszDsaDN.value);
			}

			if (this.pszDsaAddress is not null)
			{
				encoder.WriteWideCharString(this.pszDsaAddress.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeEnqueued);
			if (this.pszNamingContext is not null)
			{
				this.pszNamingContext.value = decoder.ReadWideCharString();
			}

			if (this.pszDsaDN is not null)
			{
				this.pszDsaDN.value = decoder.ReadWideCharString();
			}

			if (this.pszDsaAddress is not null)
			{
				this.pszDsaAddress.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_PENDING_OPSW : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgPendingOp);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgPendingOp = decoder.ReadArrayHeader<DS_REPL_OPW>();
		}

		public ms_dtyp.FILETIME ftimeCurrentOpStarted;
		public uint cNumPendingOps;
		public DS_REPL_OPW[] rgPendingOp;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgPendingOp.Length; i++)
			{
				DS_REPL_OPW elem_0 = this.rgPendingOp[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgPendingOp.Length; i++)
			{
				DS_REPL_OPW elem_0 = this.rgPendingOp[i];
				elem_0 = decoder.ReadFixedStruct<DS_REPL_OPW>(NdrAlignment.NativePtr);
				this.rgPendingOp[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ftimeCurrentOpStarted, NdrAlignment._4Byte);
			encoder.WriteValue(this.cNumPendingOps);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ftimeCurrentOpStarted = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.cNumPendingOps = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ftimeCurrentOpStarted);
			for (int i = 0; i < this.rgPendingOp.Length; i++)
			{
				DS_REPL_OPW elem_0 = this.rgPendingOp[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeCurrentOpStarted);
			for (int i = 0; i < this.rgPendingOp.Length; i++)
			{
				DS_REPL_OPW elem_0 = this.rgPendingOp[i];
				decoder.ReadStructDeferral<DS_REPL_OPW>(ref elem_0);
				this.rgPendingOp[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_VALUE_META_DATA : IRpcFixedStruct
	{
		public RpcPointer<string> pszAttributeName;
		public RpcPointer<string> pszObjectDn;
		public uint cbData;
		public RpcPointer<byte[]>? pbData;
		public ms_dtyp.FILETIME ftimeDeleted;
		public ms_dtyp.FILETIME ftimeCreated;
		public uint dwVersion;
		public ms_dtyp.FILETIME ftimeLastOriginatingChange;
		public Guid uuidLastOriginatingDsaInvocationID;
		public long usnOriginatingChange;
		public long usnLocalChange;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pszAttributeName);
			encoder.WriteUniquePointer(this.pszObjectDn);
			encoder.WriteValue(this.cbData);
			encoder.WriteFullPointer(this.pbData);
			encoder.WriteFixedStruct(this.ftimeDeleted, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.ftimeCreated, NdrAlignment._4Byte);
			encoder.WriteValue(this.dwVersion);
			encoder.WriteFixedStruct(this.ftimeLastOriginatingChange, NdrAlignment._4Byte);
			encoder.WriteValue(this.uuidLastOriginatingDsaInvocationID);
			encoder.WriteValue(this.usnOriginatingChange);
			encoder.WriteValue(this.usnLocalChange);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pszAttributeName = decoder.ReadUniquePointer<string>();
			this.pszObjectDn = decoder.ReadUniquePointer<string>();
			this.cbData = decoder.ReadUInt32();
			this.pbData = decoder.ReadFullPointer<byte[]>();
			this.ftimeDeleted = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.ftimeCreated = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.dwVersion = decoder.ReadUInt32();
			this.ftimeLastOriginatingChange = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.uuidLastOriginatingDsaInvocationID = decoder.ReadUuid();
			this.usnOriginatingChange = decoder.ReadInt64();
			this.usnLocalChange = decoder.ReadInt64();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszAttributeName is not null)
			{
				encoder.WriteWideCharString(this.pszAttributeName.value);
			}

			if (this.pszObjectDn is not null)
			{
				encoder.WriteWideCharString(this.pszObjectDn.value);
			}

			if (this.pbData is not null)
			{
				encoder.WriteArrayHeader(this.pbData.value);
				for (int i = 0; i < this.pbData.value.Length; i++)
				{
					byte elem_0 = this.pbData.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteStructDeferral(this.ftimeDeleted);
			encoder.WriteStructDeferral(this.ftimeCreated);
			encoder.WriteStructDeferral(this.ftimeLastOriginatingChange);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszAttributeName is not null)
			{
				this.pszAttributeName.value = decoder.ReadWideCharString();
			}

			if (this.pszObjectDn is not null)
			{
				this.pszObjectDn.value = decoder.ReadWideCharString();
			}

			if (this.pbData is not null)
			{
				this.pbData.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pbData.value.Length; i++)
				{
					byte elem_0 = this.pbData.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pbData.value[i] = elem_0;
				}
			}

			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeDeleted);
			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeCreated);
			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeLastOriginatingChange);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_ATTR_VALUE_META_DATA : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgMetaData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgMetaData = decoder.ReadArrayHeader<DS_REPL_VALUE_META_DATA>();
		}

		public uint cNumEntries;
		public uint dwEnumerationContext;
		public DS_REPL_VALUE_META_DATA[] rgMetaData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_VALUE_META_DATA elem_0 = this.rgMetaData[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_VALUE_META_DATA elem_0 = this.rgMetaData[i];
				elem_0 = decoder.ReadFixedStruct<DS_REPL_VALUE_META_DATA>(NdrAlignment._8Byte);
				this.rgMetaData[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cNumEntries);
			encoder.WriteValue(this.dwEnumerationContext);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cNumEntries = decoder.ReadUInt32();
			this.dwEnumerationContext = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_VALUE_META_DATA elem_0 = this.rgMetaData[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_VALUE_META_DATA elem_0 = this.rgMetaData[i];
				decoder.ReadStructDeferral<DS_REPL_VALUE_META_DATA>(ref elem_0);
				this.rgMetaData[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_CURSOR_2 : IRpcFixedStruct
	{
		public Guid uuidSourceDsaInvocationID;
		public long usnAttributeFilter;
		public ms_dtyp.FILETIME ftimeLastSyncSuccess;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidSourceDsaInvocationID);
			encoder.WriteValue(this.usnAttributeFilter);
			encoder.WriteFixedStruct(this.ftimeLastSyncSuccess, NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidSourceDsaInvocationID = decoder.ReadUuid();
			this.usnAttributeFilter = decoder.ReadInt64();
			this.ftimeLastSyncSuccess = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ftimeLastSyncSuccess);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeLastSyncSuccess);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_CURSORS_2 : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgCursor);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgCursor = decoder.ReadArrayHeader<DS_REPL_CURSOR_2>();
		}

		public uint cNumCursors;
		public uint dwEnumerationContext;
		public DS_REPL_CURSOR_2[] rgCursor;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgCursor.Length; i++)
			{
				DS_REPL_CURSOR_2 elem_0 = this.rgCursor[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgCursor.Length; i++)
			{
				DS_REPL_CURSOR_2 elem_0 = this.rgCursor[i];
				elem_0 = decoder.ReadFixedStruct<DS_REPL_CURSOR_2>(NdrAlignment._8Byte);
				this.rgCursor[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cNumCursors);
			encoder.WriteValue(this.dwEnumerationContext);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cNumCursors = decoder.ReadUInt32();
			this.dwEnumerationContext = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgCursor.Length; i++)
			{
				DS_REPL_CURSOR_2 elem_0 = this.rgCursor[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgCursor.Length; i++)
			{
				DS_REPL_CURSOR_2 elem_0 = this.rgCursor[i];
				decoder.ReadStructDeferral<DS_REPL_CURSOR_2>(ref elem_0);
				this.rgCursor[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_CURSOR_3W : IRpcFixedStruct
	{
		public Guid uuidSourceDsaInvocationID;
		public long usnAttributeFilter;
		public ms_dtyp.FILETIME ftimeLastSyncSuccess;
		public RpcPointer<string> pszSourceDsaDN;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidSourceDsaInvocationID);
			encoder.WriteValue(this.usnAttributeFilter);
			encoder.WriteFixedStruct(this.ftimeLastSyncSuccess, NdrAlignment._4Byte);
			encoder.WriteUniquePointer(this.pszSourceDsaDN);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidSourceDsaInvocationID = decoder.ReadUuid();
			this.usnAttributeFilter = decoder.ReadInt64();
			this.ftimeLastSyncSuccess = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.pszSourceDsaDN = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ftimeLastSyncSuccess);
			if (this.pszSourceDsaDN is not null)
			{
				encoder.WriteWideCharString(this.pszSourceDsaDN.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeLastSyncSuccess);
			if (this.pszSourceDsaDN is not null)
			{
				this.pszSourceDsaDN.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_CURSORS_3W : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgCursor);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgCursor = decoder.ReadArrayHeader<DS_REPL_CURSOR_3W>();
		}

		public uint cNumCursors;
		public uint dwEnumerationContext;
		public DS_REPL_CURSOR_3W[] rgCursor;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgCursor.Length; i++)
			{
				DS_REPL_CURSOR_3W elem_0 = this.rgCursor[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgCursor.Length; i++)
			{
				DS_REPL_CURSOR_3W elem_0 = this.rgCursor[i];
				elem_0 = decoder.ReadFixedStruct<DS_REPL_CURSOR_3W>(NdrAlignment._8Byte);
				this.rgCursor[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cNumCursors);
			encoder.WriteValue(this.dwEnumerationContext);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cNumCursors = decoder.ReadUInt32();
			this.dwEnumerationContext = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgCursor.Length; i++)
			{
				DS_REPL_CURSOR_3W elem_0 = this.rgCursor[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgCursor.Length; i++)
			{
				DS_REPL_CURSOR_3W elem_0 = this.rgCursor[i];
				decoder.ReadStructDeferral<DS_REPL_CURSOR_3W>(ref elem_0);
				this.rgCursor[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_ATTR_META_DATA_2 : IRpcFixedStruct
	{
		public RpcPointer<string> pszAttributeName;
		public uint dwVersion;
		public ms_dtyp.FILETIME ftimeLastOriginatingChange;
		public Guid uuidLastOriginatingDsaInvocationID;
		public long usnOriginatingChange;
		public long usnLocalChange;
		public RpcPointer<string> pszLastOriginatingDsaDN;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pszAttributeName);
			encoder.WriteValue(this.dwVersion);
			encoder.WriteFixedStruct(this.ftimeLastOriginatingChange, NdrAlignment._4Byte);
			encoder.WriteValue(this.uuidLastOriginatingDsaInvocationID);
			encoder.WriteValue(this.usnOriginatingChange);
			encoder.WriteValue(this.usnLocalChange);
			encoder.WriteUniquePointer(this.pszLastOriginatingDsaDN);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pszAttributeName = decoder.ReadUniquePointer<string>();
			this.dwVersion = decoder.ReadUInt32();
			this.ftimeLastOriginatingChange = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.uuidLastOriginatingDsaInvocationID = decoder.ReadUuid();
			this.usnOriginatingChange = decoder.ReadInt64();
			this.usnLocalChange = decoder.ReadInt64();
			this.pszLastOriginatingDsaDN = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszAttributeName is not null)
			{
				encoder.WriteWideCharString(this.pszAttributeName.value);
			}

			encoder.WriteStructDeferral(this.ftimeLastOriginatingChange);
			if (this.pszLastOriginatingDsaDN is not null)
			{
				encoder.WriteWideCharString(this.pszLastOriginatingDsaDN.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszAttributeName is not null)
			{
				this.pszAttributeName.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeLastOriginatingChange);
			if (this.pszLastOriginatingDsaDN is not null)
			{
				this.pszLastOriginatingDsaDN.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_OBJ_META_DATA_2 : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgMetaData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgMetaData = decoder.ReadArrayHeader<DS_REPL_ATTR_META_DATA_2>();
		}

		public uint cNumEntries;
		public uint dwReserved;
		public DS_REPL_ATTR_META_DATA_2[] rgMetaData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_ATTR_META_DATA_2 elem_0 = this.rgMetaData[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_ATTR_META_DATA_2 elem_0 = this.rgMetaData[i];
				elem_0 = decoder.ReadFixedStruct<DS_REPL_ATTR_META_DATA_2>(NdrAlignment._8Byte);
				this.rgMetaData[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cNumEntries);
			encoder.WriteValue(this.dwReserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cNumEntries = decoder.ReadUInt32();
			this.dwReserved = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_ATTR_META_DATA_2 elem_0 = this.rgMetaData[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_ATTR_META_DATA_2 elem_0 = this.rgMetaData[i];
				decoder.ReadStructDeferral<DS_REPL_ATTR_META_DATA_2>(ref elem_0);
				this.rgMetaData[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_VALUE_META_DATA_2 : IRpcFixedStruct
	{
		public RpcPointer<string> pszAttributeName;
		public RpcPointer<string> pszObjectDn;
		public uint cbData;
		public RpcPointer<byte[]>? pbData;
		public ms_dtyp.FILETIME ftimeDeleted;
		public ms_dtyp.FILETIME ftimeCreated;
		public uint dwVersion;
		public ms_dtyp.FILETIME ftimeLastOriginatingChange;
		public Guid uuidLastOriginatingDsaInvocationID;
		public long usnOriginatingChange;
		public long usnLocalChange;
		public RpcPointer<string> pszLastOriginatingDsaDN;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pszAttributeName);
			encoder.WriteUniquePointer(this.pszObjectDn);
			encoder.WriteValue(this.cbData);
			encoder.WriteFullPointer(this.pbData);
			encoder.WriteFixedStruct(this.ftimeDeleted, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.ftimeCreated, NdrAlignment._4Byte);
			encoder.WriteValue(this.dwVersion);
			encoder.WriteFixedStruct(this.ftimeLastOriginatingChange, NdrAlignment._4Byte);
			encoder.WriteValue(this.uuidLastOriginatingDsaInvocationID);
			encoder.WriteValue(this.usnOriginatingChange);
			encoder.WriteValue(this.usnLocalChange);
			encoder.WriteUniquePointer(this.pszLastOriginatingDsaDN);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pszAttributeName = decoder.ReadUniquePointer<string>();
			this.pszObjectDn = decoder.ReadUniquePointer<string>();
			this.cbData = decoder.ReadUInt32();
			this.pbData = decoder.ReadFullPointer<byte[]>();
			this.ftimeDeleted = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.ftimeCreated = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.dwVersion = decoder.ReadUInt32();
			this.ftimeLastOriginatingChange = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			this.uuidLastOriginatingDsaInvocationID = decoder.ReadUuid();
			this.usnOriginatingChange = decoder.ReadInt64();
			this.usnLocalChange = decoder.ReadInt64();
			this.pszLastOriginatingDsaDN = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszAttributeName is not null)
			{
				encoder.WriteWideCharString(this.pszAttributeName.value);
			}

			if (this.pszObjectDn is not null)
			{
				encoder.WriteWideCharString(this.pszObjectDn.value);
			}

			if (this.pbData is not null)
			{
				encoder.WriteArrayHeader(this.pbData.value);
				for (int i = 0; i < this.pbData.value.Length; i++)
				{
					byte elem_0 = this.pbData.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteStructDeferral(this.ftimeDeleted);
			encoder.WriteStructDeferral(this.ftimeCreated);
			encoder.WriteStructDeferral(this.ftimeLastOriginatingChange);
			if (this.pszLastOriginatingDsaDN is not null)
			{
				encoder.WriteWideCharString(this.pszLastOriginatingDsaDN.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszAttributeName is not null)
			{
				this.pszAttributeName.value = decoder.ReadWideCharString();
			}

			if (this.pszObjectDn is not null)
			{
				this.pszObjectDn.value = decoder.ReadWideCharString();
			}

			if (this.pbData is not null)
			{
				this.pbData.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pbData.value.Length; i++)
				{
					byte elem_0 = this.pbData.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pbData.value[i] = elem_0;
				}
			}

			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeDeleted);
			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeCreated);
			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ftimeLastOriginatingChange);
			if (this.pszLastOriginatingDsaDN is not null)
			{
				this.pszLastOriginatingDsaDN.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_ATTR_VALUE_META_DATA_2 : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgMetaData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgMetaData = decoder.ReadArrayHeader<DS_REPL_VALUE_META_DATA_2>();
		}

		public uint cNumEntries;
		public uint dwEnumerationContext;
		public DS_REPL_VALUE_META_DATA_2[] rgMetaData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_VALUE_META_DATA_2 elem_0 = this.rgMetaData[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_VALUE_META_DATA_2 elem_0 = this.rgMetaData[i];
				elem_0 = decoder.ReadFixedStruct<DS_REPL_VALUE_META_DATA_2>(NdrAlignment._8Byte);
				this.rgMetaData[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cNumEntries);
			encoder.WriteValue(this.dwEnumerationContext);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cNumEntries = decoder.ReadUInt32();
			this.dwEnumerationContext = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_VALUE_META_DATA_2 elem_0 = this.rgMetaData[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgMetaData.Length; i++)
			{
				DS_REPL_VALUE_META_DATA_2 elem_0 = this.rgMetaData[i];
				decoder.ReadStructDeferral<DS_REPL_VALUE_META_DATA_2>(ref elem_0);
				this.rgMetaData[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_EXTENSIONS : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgb);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgb = decoder.ReadArrayHeader<byte>();
		}

		public uint cb;
		public byte[] rgb;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgb.Length; i++)
			{
				byte elem_0 = this.rgb[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgb.Length; i++)
			{
				byte elem_0 = this.rgb[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.rgb[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cb);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cb = decoder.ReadUInt32();
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
	public partial struct DRS_MSG_GETCHGREQ_V3 : IRpcFixedStruct
	{
		public Guid uuidDsaObjDest;
		public Guid uuidInvocIdSrc;
		public RpcPointer<DSNAME> pNC;
		public USN_VECTOR usnvecFrom;
		public RpcPointer<UPTODATE_VECTOR_V1_EXT> pUpToDateVecDestV1;
		public RpcPointer<PARTIAL_ATTR_VECTOR_V1_EXT> pPartialAttrVecDestV1;
		public SCHEMA_PREFIX_TABLE PrefixTableDest;
		public uint ulFlags;
		public uint cMaxObjects;
		public uint cMaxBytes;
		public uint ulExtendedOp;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidDsaObjDest);
			encoder.WriteValue(this.uuidInvocIdSrc);
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteFixedStruct(this.usnvecFrom, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pUpToDateVecDestV1);
			encoder.WriteUniquePointer(this.pPartialAttrVecDestV1);
			encoder.WriteFixedStruct(this.PrefixTableDest, NdrAlignment.NativePtr);
			encoder.WriteValue(this.ulFlags);
			encoder.WriteValue(this.cMaxObjects);
			encoder.WriteValue(this.cMaxBytes);
			encoder.WriteValue(this.ulExtendedOp);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidDsaObjDest = decoder.ReadUuid();
			this.uuidInvocIdSrc = decoder.ReadUuid();
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.usnvecFrom = decoder.ReadFixedStruct<USN_VECTOR>(NdrAlignment._8Byte);
			this.pUpToDateVecDestV1 = decoder.ReadUniquePointer<UPTODATE_VECTOR_V1_EXT>();
			this.pPartialAttrVecDestV1 = decoder.ReadUniquePointer<PARTIAL_ATTR_VECTOR_V1_EXT>();
			this.PrefixTableDest = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
			this.ulFlags = decoder.ReadUInt32();
			this.cMaxObjects = decoder.ReadUInt32();
			this.cMaxBytes = decoder.ReadUInt32();
			this.ulExtendedOp = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			encoder.WriteStructDeferral(this.usnvecFrom);
			if (this.pUpToDateVecDestV1 is not null)
			{
				encoder.WriteConformantStruct(this.pUpToDateVecDestV1.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pUpToDateVecDestV1.value);
			}

			if (this.pPartialAttrVecDestV1 is not null)
			{
				encoder.WriteConformantStruct(this.pPartialAttrVecDestV1.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pPartialAttrVecDestV1.value);
			}

			encoder.WriteStructDeferral(this.PrefixTableDest);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			decoder.ReadStructDeferral<USN_VECTOR>(ref this.usnvecFrom);
			if (this.pUpToDateVecDestV1 is not null)
			{
				this.pUpToDateVecDestV1.value = decoder.ReadConformantStruct<UPTODATE_VECTOR_V1_EXT>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<UPTODATE_VECTOR_V1_EXT>(ref this.pUpToDateVecDestV1.value);
			}

			if (this.pPartialAttrVecDestV1 is not null)
			{
				this.pPartialAttrVecDestV1.value = decoder.ReadConformantStruct<PARTIAL_ATTR_VECTOR_V1_EXT>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<PARTIAL_ATTR_VECTOR_V1_EXT>(ref this.pPartialAttrVecDestV1.value);
			}

			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTableDest);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETCHGREQ_V4 : IRpcFixedStruct
	{
		public Guid uuidTransportObj;
		public RpcPointer<MTX_ADDR> pmtxReturnAddress;
		public DRS_MSG_GETCHGREQ_V3 V3;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidTransportObj);
			encoder.WriteUniquePointer(this.pmtxReturnAddress);
			encoder.WriteFixedStruct(this.V3, NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidTransportObj = decoder.ReadUuid();
			this.pmtxReturnAddress = decoder.ReadUniquePointer<MTX_ADDR>();
			this.V3 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREQ_V3>(NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pmtxReturnAddress is not null)
			{
				encoder.WriteConformantStruct(this.pmtxReturnAddress.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pmtxReturnAddress.value);
			}

			encoder.WriteStructDeferral(this.V3);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pmtxReturnAddress is not null)
			{
				this.pmtxReturnAddress.value = decoder.ReadConformantStruct<MTX_ADDR>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<MTX_ADDR>(ref this.pmtxReturnAddress.value);
			}

			decoder.ReadStructDeferral<DRS_MSG_GETCHGREQ_V3>(ref this.V3);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETCHGREQ_V7 : IRpcFixedStruct
	{
		public Guid uuidTransportObj;
		public RpcPointer<MTX_ADDR> pmtxReturnAddress;
		public DRS_MSG_GETCHGREQ_V3 V3;
		public RpcPointer<PARTIAL_ATTR_VECTOR_V1_EXT> pPartialAttrSet;
		public RpcPointer<PARTIAL_ATTR_VECTOR_V1_EXT> pPartialAttrSetEx;
		public SCHEMA_PREFIX_TABLE PrefixTableDest;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidTransportObj);
			encoder.WriteUniquePointer(this.pmtxReturnAddress);
			encoder.WriteFixedStruct(this.V3, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pPartialAttrSet);
			encoder.WriteUniquePointer(this.pPartialAttrSetEx);
			encoder.WriteFixedStruct(this.PrefixTableDest, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidTransportObj = decoder.ReadUuid();
			this.pmtxReturnAddress = decoder.ReadUniquePointer<MTX_ADDR>();
			this.V3 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREQ_V3>(NdrAlignment._8Byte);
			this.pPartialAttrSet = decoder.ReadUniquePointer<PARTIAL_ATTR_VECTOR_V1_EXT>();
			this.pPartialAttrSetEx = decoder.ReadUniquePointer<PARTIAL_ATTR_VECTOR_V1_EXT>();
			this.PrefixTableDest = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pmtxReturnAddress is not null)
			{
				encoder.WriteConformantStruct(this.pmtxReturnAddress.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pmtxReturnAddress.value);
			}

			encoder.WriteStructDeferral(this.V3);
			if (this.pPartialAttrSet is not null)
			{
				encoder.WriteConformantStruct(this.pPartialAttrSet.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pPartialAttrSet.value);
			}

			if (this.pPartialAttrSetEx is not null)
			{
				encoder.WriteConformantStruct(this.pPartialAttrSetEx.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pPartialAttrSetEx.value);
			}

			encoder.WriteStructDeferral(this.PrefixTableDest);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pmtxReturnAddress is not null)
			{
				this.pmtxReturnAddress.value = decoder.ReadConformantStruct<MTX_ADDR>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<MTX_ADDR>(ref this.pmtxReturnAddress.value);
			}

			decoder.ReadStructDeferral<DRS_MSG_GETCHGREQ_V3>(ref this.V3);
			if (this.pPartialAttrSet is not null)
			{
				this.pPartialAttrSet.value = decoder.ReadConformantStruct<PARTIAL_ATTR_VECTOR_V1_EXT>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<PARTIAL_ATTR_VECTOR_V1_EXT>(ref this.pPartialAttrSet.value);
			}

			if (this.pPartialAttrSetEx is not null)
			{
				this.pPartialAttrSetEx.value = decoder.ReadConformantStruct<PARTIAL_ATTR_VECTOR_V1_EXT>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<PARTIAL_ATTR_VECTOR_V1_EXT>(ref this.pPartialAttrSetEx.value);
			}

			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTableDest);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETCHGREPLY_V1 : IRpcFixedStruct
	{
		public Guid uuidDsaObjSrc;
		public Guid uuidInvocIdSrc;
		public RpcPointer<DSNAME> pNC;
		public USN_VECTOR usnvecFrom;
		public USN_VECTOR usnvecTo;
		public RpcPointer<UPTODATE_VECTOR_V1_EXT> pUpToDateVecSrcV1;
		public SCHEMA_PREFIX_TABLE PrefixTableSrc;
		public uint ulExtendedRet;
		public uint cNumObjects;
		public uint cNumBytes;
		public RpcPointer<REPLENTINFLIST> pObjects;
		public int fMoreData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidDsaObjSrc);
			encoder.WriteValue(this.uuidInvocIdSrc);
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteFixedStruct(this.usnvecFrom, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.usnvecTo, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pUpToDateVecSrcV1);
			encoder.WriteFixedStruct(this.PrefixTableSrc, NdrAlignment.NativePtr);
			encoder.WriteValue(this.ulExtendedRet);
			encoder.WriteValue(this.cNumObjects);
			encoder.WriteValue(this.cNumBytes);
			encoder.WriteUniquePointer(this.pObjects);
			encoder.WriteValue(this.fMoreData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidDsaObjSrc = decoder.ReadUuid();
			this.uuidInvocIdSrc = decoder.ReadUuid();
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.usnvecFrom = decoder.ReadFixedStruct<USN_VECTOR>(NdrAlignment._8Byte);
			this.usnvecTo = decoder.ReadFixedStruct<USN_VECTOR>(NdrAlignment._8Byte);
			this.pUpToDateVecSrcV1 = decoder.ReadUniquePointer<UPTODATE_VECTOR_V1_EXT>();
			this.PrefixTableSrc = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
			this.ulExtendedRet = decoder.ReadUInt32();
			this.cNumObjects = decoder.ReadUInt32();
			this.cNumBytes = decoder.ReadUInt32();
			this.pObjects = decoder.ReadUniquePointer<REPLENTINFLIST>();
			this.fMoreData = decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			encoder.WriteStructDeferral(this.usnvecFrom);
			encoder.WriteStructDeferral(this.usnvecTo);
			if (this.pUpToDateVecSrcV1 is not null)
			{
				encoder.WriteConformantStruct(this.pUpToDateVecSrcV1.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pUpToDateVecSrcV1.value);
			}

			encoder.WriteStructDeferral(this.PrefixTableSrc);
			if (this.pObjects is not null)
			{
				encoder.WriteFixedStruct(this.pObjects.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pObjects.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			decoder.ReadStructDeferral<USN_VECTOR>(ref this.usnvecFrom);
			decoder.ReadStructDeferral<USN_VECTOR>(ref this.usnvecTo);
			if (this.pUpToDateVecSrcV1 is not null)
			{
				this.pUpToDateVecSrcV1.value = decoder.ReadConformantStruct<UPTODATE_VECTOR_V1_EXT>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<UPTODATE_VECTOR_V1_EXT>(ref this.pUpToDateVecSrcV1.value);
			}

			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTableSrc);
			if (this.pObjects is not null)
			{
				this.pObjects.value = decoder.ReadFixedStruct<REPLENTINFLIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<REPLENTINFLIST>(ref this.pObjects.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETCHGREPLY_V6 : IRpcFixedStruct
	{
		public Guid uuidDsaObjSrc;
		public Guid uuidInvocIdSrc;
		public RpcPointer<DSNAME> pNC;
		public USN_VECTOR usnvecFrom;
		public USN_VECTOR usnvecTo;
		public RpcPointer<UPTODATE_VECTOR_V2_EXT> pUpToDateVecSrc;
		public SCHEMA_PREFIX_TABLE PrefixTableSrc;
		public uint ulExtendedRet;
		public uint cNumObjects;
		public uint cNumBytes;
		public RpcPointer<REPLENTINFLIST> pObjects;
		public int fMoreData;
		public uint cNumNcSizeObjects;
		public uint cNumNcSizeValues;
		public uint cNumValues;
		public RpcPointer<REPLVALINF_V1[]> rgValues;
		public uint dwDRSError;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidDsaObjSrc);
			encoder.WriteValue(this.uuidInvocIdSrc);
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteFixedStruct(this.usnvecFrom, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.usnvecTo, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pUpToDateVecSrc);
			encoder.WriteFixedStruct(this.PrefixTableSrc, NdrAlignment.NativePtr);
			encoder.WriteValue(this.ulExtendedRet);
			encoder.WriteValue(this.cNumObjects);
			encoder.WriteValue(this.cNumBytes);
			encoder.WriteUniquePointer(this.pObjects);
			encoder.WriteValue(this.fMoreData);
			encoder.WriteValue(this.cNumNcSizeObjects);
			encoder.WriteValue(this.cNumNcSizeValues);
			encoder.WriteValue(this.cNumValues);
			encoder.WriteUniquePointer(this.rgValues);
			encoder.WriteValue(this.dwDRSError);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidDsaObjSrc = decoder.ReadUuid();
			this.uuidInvocIdSrc = decoder.ReadUuid();
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.usnvecFrom = decoder.ReadFixedStruct<USN_VECTOR>(NdrAlignment._8Byte);
			this.usnvecTo = decoder.ReadFixedStruct<USN_VECTOR>(NdrAlignment._8Byte);
			this.pUpToDateVecSrc = decoder.ReadUniquePointer<UPTODATE_VECTOR_V2_EXT>();
			this.PrefixTableSrc = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
			this.ulExtendedRet = decoder.ReadUInt32();
			this.cNumObjects = decoder.ReadUInt32();
			this.cNumBytes = decoder.ReadUInt32();
			this.pObjects = decoder.ReadUniquePointer<REPLENTINFLIST>();
			this.fMoreData = decoder.ReadInt32();
			this.cNumNcSizeObjects = decoder.ReadUInt32();
			this.cNumNcSizeValues = decoder.ReadUInt32();
			this.cNumValues = decoder.ReadUInt32();
			this.rgValues = decoder.ReadUniquePointer<REPLVALINF_V1[]>();
			this.dwDRSError = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			encoder.WriteStructDeferral(this.usnvecFrom);
			encoder.WriteStructDeferral(this.usnvecTo);
			if (this.pUpToDateVecSrc is not null)
			{
				encoder.WriteConformantStruct(this.pUpToDateVecSrc.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pUpToDateVecSrc.value);
			}

			encoder.WriteStructDeferral(this.PrefixTableSrc);
			if (this.pObjects is not null)
			{
				encoder.WriteFixedStruct(this.pObjects.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pObjects.value);
			}

			if (this.rgValues is not null)
			{
				encoder.WriteArrayHeader(this.rgValues.value);
				for (int i = 0; i < this.rgValues.value.Length; i++)
				{
					REPLVALINF_V1 elem_0 = this.rgValues.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.rgValues.value.Length; i++)
				{
					REPLVALINF_V1 elem_0 = this.rgValues.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			decoder.ReadStructDeferral<USN_VECTOR>(ref this.usnvecFrom);
			decoder.ReadStructDeferral<USN_VECTOR>(ref this.usnvecTo);
			if (this.pUpToDateVecSrc is not null)
			{
				this.pUpToDateVecSrc.value = decoder.ReadConformantStruct<UPTODATE_VECTOR_V2_EXT>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<UPTODATE_VECTOR_V2_EXT>(ref this.pUpToDateVecSrc.value);
			}

			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTableSrc);
			if (this.pObjects is not null)
			{
				this.pObjects.value = decoder.ReadFixedStruct<REPLENTINFLIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<REPLENTINFLIST>(ref this.pObjects.value);
			}

			if (this.rgValues is not null)
			{
				this.rgValues.value = decoder.ReadArrayHeader<REPLVALINF_V1>();
				for (int i = 0; i < this.rgValues.value.Length; i++)
				{
					REPLVALINF_V1 elem_0 = this.rgValues.value[i];
					elem_0 = decoder.ReadFixedStruct<REPLVALINF_V1>(NdrAlignment._8Byte);
					this.rgValues.value[i] = elem_0;
				}

				for (int i = 0; i < this.rgValues.value.Length; i++)
				{
					REPLVALINF_V1 elem_0 = this.rgValues.value[i];
					decoder.ReadStructDeferral<REPLVALINF_V1>(ref elem_0);
					this.rgValues.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETCHGREPLY_V9 : IRpcFixedStruct
	{
		public Guid uuidDsaObjSrc;
		public Guid uuidInvocIdSrc;
		public RpcPointer<DSNAME> pNC;
		public USN_VECTOR usnvecFrom;
		public USN_VECTOR usnvecTo;
		public RpcPointer<UPTODATE_VECTOR_V2_EXT> pUpToDateVecSrc;
		public SCHEMA_PREFIX_TABLE PrefixTableSrc;
		public uint ulExtendedRet;
		public uint cNumObjects;
		public uint cNumBytes;
		public RpcPointer<REPLENTINFLIST> pObjects;
		public int fMoreData;
		public uint cNumNcSizeObjects;
		public uint cNumNcSizeValues;
		public uint cNumValues;
		public RpcPointer<REPLVALINF_V3[]> rgValues;
		public uint dwDRSError;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidDsaObjSrc);
			encoder.WriteValue(this.uuidInvocIdSrc);
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteFixedStruct(this.usnvecFrom, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.usnvecTo, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pUpToDateVecSrc);
			encoder.WriteFixedStruct(this.PrefixTableSrc, NdrAlignment.NativePtr);
			encoder.WriteValue(this.ulExtendedRet);
			encoder.WriteValue(this.cNumObjects);
			encoder.WriteValue(this.cNumBytes);
			encoder.WriteUniquePointer(this.pObjects);
			encoder.WriteValue(this.fMoreData);
			encoder.WriteValue(this.cNumNcSizeObjects);
			encoder.WriteValue(this.cNumNcSizeValues);
			encoder.WriteValue(this.cNumValues);
			encoder.WriteUniquePointer(this.rgValues);
			encoder.WriteValue(this.dwDRSError);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidDsaObjSrc = decoder.ReadUuid();
			this.uuidInvocIdSrc = decoder.ReadUuid();
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.usnvecFrom = decoder.ReadFixedStruct<USN_VECTOR>(NdrAlignment._8Byte);
			this.usnvecTo = decoder.ReadFixedStruct<USN_VECTOR>(NdrAlignment._8Byte);
			this.pUpToDateVecSrc = decoder.ReadUniquePointer<UPTODATE_VECTOR_V2_EXT>();
			this.PrefixTableSrc = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
			this.ulExtendedRet = decoder.ReadUInt32();
			this.cNumObjects = decoder.ReadUInt32();
			this.cNumBytes = decoder.ReadUInt32();
			this.pObjects = decoder.ReadUniquePointer<REPLENTINFLIST>();
			this.fMoreData = decoder.ReadInt32();
			this.cNumNcSizeObjects = decoder.ReadUInt32();
			this.cNumNcSizeValues = decoder.ReadUInt32();
			this.cNumValues = decoder.ReadUInt32();
			this.rgValues = decoder.ReadUniquePointer<REPLVALINF_V3[]>();
			this.dwDRSError = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			encoder.WriteStructDeferral(this.usnvecFrom);
			encoder.WriteStructDeferral(this.usnvecTo);
			if (this.pUpToDateVecSrc is not null)
			{
				encoder.WriteConformantStruct(this.pUpToDateVecSrc.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pUpToDateVecSrc.value);
			}

			encoder.WriteStructDeferral(this.PrefixTableSrc);
			if (this.pObjects is not null)
			{
				encoder.WriteFixedStruct(this.pObjects.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pObjects.value);
			}

			if (this.rgValues is not null)
			{
				encoder.WriteArrayHeader(this.rgValues.value);
				for (int i = 0; i < this.rgValues.value.Length; i++)
				{
					REPLVALINF_V3 elem_0 = this.rgValues.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.rgValues.value.Length; i++)
				{
					REPLVALINF_V3 elem_0 = this.rgValues.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			decoder.ReadStructDeferral<USN_VECTOR>(ref this.usnvecFrom);
			decoder.ReadStructDeferral<USN_VECTOR>(ref this.usnvecTo);
			if (this.pUpToDateVecSrc is not null)
			{
				this.pUpToDateVecSrc.value = decoder.ReadConformantStruct<UPTODATE_VECTOR_V2_EXT>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<UPTODATE_VECTOR_V2_EXT>(ref this.pUpToDateVecSrc.value);
			}

			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTableSrc);
			if (this.pObjects is not null)
			{
				this.pObjects.value = decoder.ReadFixedStruct<REPLENTINFLIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<REPLENTINFLIST>(ref this.pObjects.value);
			}

			if (this.rgValues is not null)
			{
				this.rgValues.value = decoder.ReadArrayHeader<REPLVALINF_V3>();
				for (int i = 0; i < this.rgValues.value.Length; i++)
				{
					REPLVALINF_V3 elem_0 = this.rgValues.value[i];
					elem_0 = decoder.ReadFixedStruct<REPLVALINF_V3>(NdrAlignment._8Byte);
					this.rgValues.value[i] = elem_0;
				}

				for (int i = 0; i < this.rgValues.value.Length; i++)
				{
					REPLVALINF_V3 elem_0 = this.rgValues.value[i];
					decoder.ReadStructDeferral<REPLVALINF_V3>(ref elem_0);
					this.rgValues.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_COMPRESSED_BLOB : IRpcFixedStruct
	{
		public uint cbUncompressedSize;
		public uint cbCompressedSize;
		public RpcPointer<byte[]> pbCompressedData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cbUncompressedSize);
			encoder.WriteValue(this.cbCompressedSize);
			encoder.WriteUniquePointer(this.pbCompressedData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cbUncompressedSize = decoder.ReadUInt32();
			this.cbCompressedSize = decoder.ReadUInt32();
			this.pbCompressedData = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pbCompressedData is not null)
			{
				encoder.WriteArrayHeader(this.pbCompressedData.value);
				for (int i = 0; i < this.pbCompressedData.value.Length; i++)
				{
					byte elem_0 = this.pbCompressedData.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pbCompressedData is not null)
			{
				this.pbCompressedData.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pbCompressedData.value.Length; i++)
				{
					byte elem_0 = this.pbCompressedData.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pbCompressedData.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETCHGREQ_V5 : IRpcFixedStruct
	{
		public Guid uuidDsaObjDest;
		public Guid uuidInvocIdSrc;
		public RpcPointer<DSNAME> pNC;
		public USN_VECTOR usnvecFrom;
		public RpcPointer<UPTODATE_VECTOR_V1_EXT> pUpToDateVecDestV1;
		public uint ulFlags;
		public uint cMaxObjects;
		public uint cMaxBytes;
		public uint ulExtendedOp;
		public ms_dtyp.ULARGE_INTEGER liFsmoInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidDsaObjDest);
			encoder.WriteValue(this.uuidInvocIdSrc);
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteFixedStruct(this.usnvecFrom, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pUpToDateVecDestV1);
			encoder.WriteValue(this.ulFlags);
			encoder.WriteValue(this.cMaxObjects);
			encoder.WriteValue(this.cMaxBytes);
			encoder.WriteValue(this.ulExtendedOp);
			encoder.WriteFixedStruct(this.liFsmoInfo, NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidDsaObjDest = decoder.ReadUuid();
			this.uuidInvocIdSrc = decoder.ReadUuid();
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.usnvecFrom = decoder.ReadFixedStruct<USN_VECTOR>(NdrAlignment._8Byte);
			this.pUpToDateVecDestV1 = decoder.ReadUniquePointer<UPTODATE_VECTOR_V1_EXT>();
			this.ulFlags = decoder.ReadUInt32();
			this.cMaxObjects = decoder.ReadUInt32();
			this.cMaxBytes = decoder.ReadUInt32();
			this.ulExtendedOp = decoder.ReadUInt32();
			this.liFsmoInfo = decoder.ReadFixedStruct<ms_dtyp.ULARGE_INTEGER>(NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			encoder.WriteStructDeferral(this.usnvecFrom);
			if (this.pUpToDateVecDestV1 is not null)
			{
				encoder.WriteConformantStruct(this.pUpToDateVecDestV1.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pUpToDateVecDestV1.value);
			}

			encoder.WriteStructDeferral(this.liFsmoInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			decoder.ReadStructDeferral<USN_VECTOR>(ref this.usnvecFrom);
			if (this.pUpToDateVecDestV1 is not null)
			{
				this.pUpToDateVecDestV1.value = decoder.ReadConformantStruct<UPTODATE_VECTOR_V1_EXT>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<UPTODATE_VECTOR_V1_EXT>(ref this.pUpToDateVecDestV1.value);
			}

			decoder.ReadStructDeferral<ms_dtyp.ULARGE_INTEGER>(ref this.liFsmoInfo);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETCHGREQ_V8 : IRpcFixedStruct
	{
		public Guid uuidDsaObjDest;
		public Guid uuidInvocIdSrc;
		public RpcPointer<DSNAME> pNC;
		public USN_VECTOR usnvecFrom;
		public RpcPointer<UPTODATE_VECTOR_V1_EXT> pUpToDateVecDest;
		public uint ulFlags;
		public uint cMaxObjects;
		public uint cMaxBytes;
		public uint ulExtendedOp;
		public ms_dtyp.ULARGE_INTEGER liFsmoInfo;
		public RpcPointer<PARTIAL_ATTR_VECTOR_V1_EXT> pPartialAttrSet;
		public RpcPointer<PARTIAL_ATTR_VECTOR_V1_EXT> pPartialAttrSetEx;
		public SCHEMA_PREFIX_TABLE PrefixTableDest;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidDsaObjDest);
			encoder.WriteValue(this.uuidInvocIdSrc);
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteFixedStruct(this.usnvecFrom, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pUpToDateVecDest);
			encoder.WriteValue(this.ulFlags);
			encoder.WriteValue(this.cMaxObjects);
			encoder.WriteValue(this.cMaxBytes);
			encoder.WriteValue(this.ulExtendedOp);
			encoder.WriteFixedStruct(this.liFsmoInfo, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pPartialAttrSet);
			encoder.WriteUniquePointer(this.pPartialAttrSetEx);
			encoder.WriteFixedStruct(this.PrefixTableDest, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidDsaObjDest = decoder.ReadUuid();
			this.uuidInvocIdSrc = decoder.ReadUuid();
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.usnvecFrom = decoder.ReadFixedStruct<USN_VECTOR>(NdrAlignment._8Byte);
			this.pUpToDateVecDest = decoder.ReadUniquePointer<UPTODATE_VECTOR_V1_EXT>();
			this.ulFlags = decoder.ReadUInt32();
			this.cMaxObjects = decoder.ReadUInt32();
			this.cMaxBytes = decoder.ReadUInt32();
			this.ulExtendedOp = decoder.ReadUInt32();
			this.liFsmoInfo = decoder.ReadFixedStruct<ms_dtyp.ULARGE_INTEGER>(NdrAlignment._8Byte);
			this.pPartialAttrSet = decoder.ReadUniquePointer<PARTIAL_ATTR_VECTOR_V1_EXT>();
			this.pPartialAttrSetEx = decoder.ReadUniquePointer<PARTIAL_ATTR_VECTOR_V1_EXT>();
			this.PrefixTableDest = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			encoder.WriteStructDeferral(this.usnvecFrom);
			if (this.pUpToDateVecDest is not null)
			{
				encoder.WriteConformantStruct(this.pUpToDateVecDest.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pUpToDateVecDest.value);
			}

			encoder.WriteStructDeferral(this.liFsmoInfo);
			if (this.pPartialAttrSet is not null)
			{
				encoder.WriteConformantStruct(this.pPartialAttrSet.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pPartialAttrSet.value);
			}

			if (this.pPartialAttrSetEx is not null)
			{
				encoder.WriteConformantStruct(this.pPartialAttrSetEx.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pPartialAttrSetEx.value);
			}

			encoder.WriteStructDeferral(this.PrefixTableDest);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			decoder.ReadStructDeferral<USN_VECTOR>(ref this.usnvecFrom);
			if (this.pUpToDateVecDest is not null)
			{
				this.pUpToDateVecDest.value = decoder.ReadConformantStruct<UPTODATE_VECTOR_V1_EXT>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<UPTODATE_VECTOR_V1_EXT>(ref this.pUpToDateVecDest.value);
			}

			decoder.ReadStructDeferral<ms_dtyp.ULARGE_INTEGER>(ref this.liFsmoInfo);
			if (this.pPartialAttrSet is not null)
			{
				this.pPartialAttrSet.value = decoder.ReadConformantStruct<PARTIAL_ATTR_VECTOR_V1_EXT>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<PARTIAL_ATTR_VECTOR_V1_EXT>(ref this.pPartialAttrSet.value);
			}

			if (this.pPartialAttrSetEx is not null)
			{
				this.pPartialAttrSetEx.value = decoder.ReadConformantStruct<PARTIAL_ATTR_VECTOR_V1_EXT>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<PARTIAL_ATTR_VECTOR_V1_EXT>(ref this.pPartialAttrSetEx.value);
			}

			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTableDest);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETCHGREQ_V10 : IRpcFixedStruct
	{
		public Guid uuidDsaObjDest;
		public Guid uuidInvocIdSrc;
		public RpcPointer<DSNAME> pNC;
		public USN_VECTOR usnvecFrom;
		public RpcPointer<UPTODATE_VECTOR_V1_EXT> pUpToDateVecDest;
		public uint ulFlags;
		public uint cMaxObjects;
		public uint cMaxBytes;
		public uint ulExtendedOp;
		public ms_dtyp.ULARGE_INTEGER liFsmoInfo;
		public RpcPointer<PARTIAL_ATTR_VECTOR_V1_EXT> pPartialAttrSet;
		public RpcPointer<PARTIAL_ATTR_VECTOR_V1_EXT> pPartialAttrSetEx;
		public SCHEMA_PREFIX_TABLE PrefixTableDest;
		public uint ulMoreFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidDsaObjDest);
			encoder.WriteValue(this.uuidInvocIdSrc);
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteFixedStruct(this.usnvecFrom, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pUpToDateVecDest);
			encoder.WriteValue(this.ulFlags);
			encoder.WriteValue(this.cMaxObjects);
			encoder.WriteValue(this.cMaxBytes);
			encoder.WriteValue(this.ulExtendedOp);
			encoder.WriteFixedStruct(this.liFsmoInfo, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pPartialAttrSet);
			encoder.WriteUniquePointer(this.pPartialAttrSetEx);
			encoder.WriteFixedStruct(this.PrefixTableDest, NdrAlignment.NativePtr);
			encoder.WriteValue(this.ulMoreFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidDsaObjDest = decoder.ReadUuid();
			this.uuidInvocIdSrc = decoder.ReadUuid();
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.usnvecFrom = decoder.ReadFixedStruct<USN_VECTOR>(NdrAlignment._8Byte);
			this.pUpToDateVecDest = decoder.ReadUniquePointer<UPTODATE_VECTOR_V1_EXT>();
			this.ulFlags = decoder.ReadUInt32();
			this.cMaxObjects = decoder.ReadUInt32();
			this.cMaxBytes = decoder.ReadUInt32();
			this.ulExtendedOp = decoder.ReadUInt32();
			this.liFsmoInfo = decoder.ReadFixedStruct<ms_dtyp.ULARGE_INTEGER>(NdrAlignment._8Byte);
			this.pPartialAttrSet = decoder.ReadUniquePointer<PARTIAL_ATTR_VECTOR_V1_EXT>();
			this.pPartialAttrSetEx = decoder.ReadUniquePointer<PARTIAL_ATTR_VECTOR_V1_EXT>();
			this.PrefixTableDest = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
			this.ulMoreFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			encoder.WriteStructDeferral(this.usnvecFrom);
			if (this.pUpToDateVecDest is not null)
			{
				encoder.WriteConformantStruct(this.pUpToDateVecDest.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pUpToDateVecDest.value);
			}

			encoder.WriteStructDeferral(this.liFsmoInfo);
			if (this.pPartialAttrSet is not null)
			{
				encoder.WriteConformantStruct(this.pPartialAttrSet.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pPartialAttrSet.value);
			}

			if (this.pPartialAttrSetEx is not null)
			{
				encoder.WriteConformantStruct(this.pPartialAttrSetEx.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pPartialAttrSetEx.value);
			}

			encoder.WriteStructDeferral(this.PrefixTableDest);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			decoder.ReadStructDeferral<USN_VECTOR>(ref this.usnvecFrom);
			if (this.pUpToDateVecDest is not null)
			{
				this.pUpToDateVecDest.value = decoder.ReadConformantStruct<UPTODATE_VECTOR_V1_EXT>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<UPTODATE_VECTOR_V1_EXT>(ref this.pUpToDateVecDest.value);
			}

			decoder.ReadStructDeferral<ms_dtyp.ULARGE_INTEGER>(ref this.liFsmoInfo);
			if (this.pPartialAttrSet is not null)
			{
				this.pPartialAttrSet.value = decoder.ReadConformantStruct<PARTIAL_ATTR_VECTOR_V1_EXT>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<PARTIAL_ATTR_VECTOR_V1_EXT>(ref this.pPartialAttrSet.value);
			}

			if (this.pPartialAttrSetEx is not null)
			{
				this.pPartialAttrSetEx.value = decoder.ReadConformantStruct<PARTIAL_ATTR_VECTOR_V1_EXT>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<PARTIAL_ATTR_VECTOR_V1_EXT>(ref this.pPartialAttrSetEx.value);
			}

			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTableDest);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct VAR_SIZE_BUFFER_WITH_VERSION : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgbBuffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgbBuffer = decoder.ReadArrayHeader<byte>();
		}

		public uint ulVersion;
		public uint cbByteBuffer;
		public ulong ullPadding;
		public byte[] rgbBuffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgbBuffer.Length; i++)
			{
				byte elem_0 = this.rgbBuffer[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgbBuffer.Length; i++)
			{
				byte elem_0 = this.rgbBuffer[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.rgbBuffer[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ulVersion);
			encoder.WriteValue(this.cbByteBuffer);
			encoder.WriteValue(this.ullPadding);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ulVersion = decoder.ReadUInt32();
			this.cbByteBuffer = decoder.ReadUInt32();
			this.ullPadding = decoder.ReadUInt64();
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
	public partial struct DRS_MSG_GETCHGREQ_V11 : IRpcFixedStruct
	{
		public Guid uuidDsaObjDest;
		public Guid uuidInvocIdSrc;
		public RpcPointer<DSNAME> pNC;
		public USN_VECTOR usnvecFrom;
		public RpcPointer<UPTODATE_VECTOR_V1_EXT> pUpToDateVecDest;
		public uint ulFlags;
		public uint cMaxObjects;
		public uint cMaxBytes;
		public uint ulExtendedOp;
		public ms_dtyp.ULARGE_INTEGER liFsmoInfo;
		public RpcPointer<PARTIAL_ATTR_VECTOR_V1_EXT> pPartialAttrSet;
		public RpcPointer<PARTIAL_ATTR_VECTOR_V1_EXT> pPartialAttrSetEx;
		public SCHEMA_PREFIX_TABLE PrefixTableDest;
		public uint ulMoreFlags;
		public Guid correlationID;
		public RpcPointer<VAR_SIZE_BUFFER_WITH_VERSION> pReservedBuffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.uuidDsaObjDest);
			encoder.WriteValue(this.uuidInvocIdSrc);
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteFixedStruct(this.usnvecFrom, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pUpToDateVecDest);
			encoder.WriteValue(this.ulFlags);
			encoder.WriteValue(this.cMaxObjects);
			encoder.WriteValue(this.cMaxBytes);
			encoder.WriteValue(this.ulExtendedOp);
			encoder.WriteFixedStruct(this.liFsmoInfo, NdrAlignment._8Byte);
			encoder.WriteUniquePointer(this.pPartialAttrSet);
			encoder.WriteUniquePointer(this.pPartialAttrSetEx);
			encoder.WriteFixedStruct(this.PrefixTableDest, NdrAlignment.NativePtr);
			encoder.WriteValue(this.ulMoreFlags);
			encoder.WriteValue(this.correlationID);
			encoder.WriteUniquePointer(this.pReservedBuffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.uuidDsaObjDest = decoder.ReadUuid();
			this.uuidInvocIdSrc = decoder.ReadUuid();
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.usnvecFrom = decoder.ReadFixedStruct<USN_VECTOR>(NdrAlignment._8Byte);
			this.pUpToDateVecDest = decoder.ReadUniquePointer<UPTODATE_VECTOR_V1_EXT>();
			this.ulFlags = decoder.ReadUInt32();
			this.cMaxObjects = decoder.ReadUInt32();
			this.cMaxBytes = decoder.ReadUInt32();
			this.ulExtendedOp = decoder.ReadUInt32();
			this.liFsmoInfo = decoder.ReadFixedStruct<ms_dtyp.ULARGE_INTEGER>(NdrAlignment._8Byte);
			this.pPartialAttrSet = decoder.ReadUniquePointer<PARTIAL_ATTR_VECTOR_V1_EXT>();
			this.pPartialAttrSetEx = decoder.ReadUniquePointer<PARTIAL_ATTR_VECTOR_V1_EXT>();
			this.PrefixTableDest = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
			this.ulMoreFlags = decoder.ReadUInt32();
			this.correlationID = decoder.ReadUuid();
			this.pReservedBuffer = decoder.ReadUniquePointer<VAR_SIZE_BUFFER_WITH_VERSION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			encoder.WriteStructDeferral(this.usnvecFrom);
			if (this.pUpToDateVecDest is not null)
			{
				encoder.WriteConformantStruct(this.pUpToDateVecDest.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pUpToDateVecDest.value);
			}

			encoder.WriteStructDeferral(this.liFsmoInfo);
			if (this.pPartialAttrSet is not null)
			{
				encoder.WriteConformantStruct(this.pPartialAttrSet.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pPartialAttrSet.value);
			}

			if (this.pPartialAttrSetEx is not null)
			{
				encoder.WriteConformantStruct(this.pPartialAttrSetEx.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pPartialAttrSetEx.value);
			}

			encoder.WriteStructDeferral(this.PrefixTableDest);
			if (this.pReservedBuffer is not null)
			{
				encoder.WriteConformantStruct(this.pReservedBuffer.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pReservedBuffer.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			decoder.ReadStructDeferral<USN_VECTOR>(ref this.usnvecFrom);
			if (this.pUpToDateVecDest is not null)
			{
				this.pUpToDateVecDest.value = decoder.ReadConformantStruct<UPTODATE_VECTOR_V1_EXT>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<UPTODATE_VECTOR_V1_EXT>(ref this.pUpToDateVecDest.value);
			}

			decoder.ReadStructDeferral<ms_dtyp.ULARGE_INTEGER>(ref this.liFsmoInfo);
			if (this.pPartialAttrSet is not null)
			{
				this.pPartialAttrSet.value = decoder.ReadConformantStruct<PARTIAL_ATTR_VECTOR_V1_EXT>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<PARTIAL_ATTR_VECTOR_V1_EXT>(ref this.pPartialAttrSet.value);
			}

			if (this.pPartialAttrSetEx is not null)
			{
				this.pPartialAttrSetEx.value = decoder.ReadConformantStruct<PARTIAL_ATTR_VECTOR_V1_EXT>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<PARTIAL_ATTR_VECTOR_V1_EXT>(ref this.pPartialAttrSetEx.value);
			}

			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTableDest);
			if (this.pReservedBuffer is not null)
			{
				this.pReservedBuffer.value = decoder.ReadConformantStruct<VAR_SIZE_BUFFER_WITH_VERSION>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<VAR_SIZE_BUFFER_WITH_VERSION>(ref this.pReservedBuffer.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETCHGREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_GETCHGREQ_V4 V4;
		public DRS_MSG_GETCHGREQ_V5 V5;
		public DRS_MSG_GETCHGREQ_V7 V7;
		public DRS_MSG_GETCHGREQ_V8 V8;
		public DRS_MSG_GETCHGREQ_V10 V10;
		public DRS_MSG_GETCHGREQ_V11 V11;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._8Byte);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 4U:
					encoder.WriteFixedStruct(this.V4, NdrAlignment._8Byte);
					break;
				case 5U:
					encoder.WriteFixedStruct(this.V5, NdrAlignment._8Byte);
					break;
				case 7U:
					encoder.WriteFixedStruct(this.V7, NdrAlignment._8Byte);
					break;
				case 8U:
					encoder.WriteFixedStruct(this.V8, NdrAlignment._8Byte);
					break;
				case 10U:
					encoder.WriteFixedStruct(this.V10, NdrAlignment._8Byte);
					break;
				case 11U:
					encoder.WriteFixedStruct(this.V11, NdrAlignment._8Byte);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._8Byte);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 4U:
					this.V4 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREQ_V4>(NdrAlignment._8Byte);
					break;
				case 5U:
					this.V5 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREQ_V5>(NdrAlignment._8Byte);
					break;
				case 7U:
					this.V7 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREQ_V7>(NdrAlignment._8Byte);
					break;
				case 8U:
					this.V8 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREQ_V8>(NdrAlignment._8Byte);
					break;
				case 10U:
					this.V10 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREQ_V10>(NdrAlignment._8Byte);
					break;
				case 11U:
					this.V11 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREQ_V11>(NdrAlignment._8Byte);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 4U:
					encoder.WriteStructDeferral(this.V4);
					break;
				case 5U:
					encoder.WriteStructDeferral(this.V5);
					break;
				case 7U:
					encoder.WriteStructDeferral(this.V7);
					break;
				case 8U:
					encoder.WriteStructDeferral(this.V8);
					break;
				case 10U:
					encoder.WriteStructDeferral(this.V10);
					break;
				case 11U:
					encoder.WriteStructDeferral(this.V11);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 4U:
					decoder.ReadStructDeferral<DRS_MSG_GETCHGREQ_V4>(ref this.V4);
					break;
				case 5U:
					decoder.ReadStructDeferral<DRS_MSG_GETCHGREQ_V5>(ref this.V5);
					break;
				case 7U:
					decoder.ReadStructDeferral<DRS_MSG_GETCHGREQ_V7>(ref this.V7);
					break;
				case 8U:
					decoder.ReadStructDeferral<DRS_MSG_GETCHGREQ_V8>(ref this.V8);
					break;
				case 10U:
					decoder.ReadStructDeferral<DRS_MSG_GETCHGREQ_V10>(ref this.V10);
					break;
				case 11U:
					decoder.ReadStructDeferral<DRS_MSG_GETCHGREQ_V11>(ref this.V11);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETCHGREPLY_V2 : IRpcFixedStruct
	{
		public DRS_COMPRESSED_BLOB CompressedV1;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.CompressedV1, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.CompressedV1 = decoder.ReadFixedStruct<DRS_COMPRESSED_BLOB>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.CompressedV1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<DRS_COMPRESSED_BLOB>(ref this.CompressedV1);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum DRS_COMP_ALG_TYPE : int
	{
		DRS_COMP_ALG_NONE = 0,
		DRS_COMP_ALG_UNUSED = 1,
		DRS_COMP_ALG_MSZIP = 2,
		DRS_COMP_ALG_WIN2K3 = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETCHGREPLY_V7 : IRpcFixedStruct
	{
		public uint dwCompressedVersion;
		public DRS_COMP_ALG_TYPE CompressionAlg;
		public DRS_COMPRESSED_BLOB CompressedAny;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwCompressedVersion);
			encoder.WriteEnumShortValue((short)this.CompressionAlg);
			encoder.WriteFixedStruct(this.CompressedAny, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwCompressedVersion = decoder.ReadUInt32();
			this.CompressionAlg = (DRS_COMP_ALG_TYPE)decoder.ReadEnumShortValue();
			this.CompressedAny = decoder.ReadFixedStruct<DRS_COMPRESSED_BLOB>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.CompressedAny);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<DRS_COMPRESSED_BLOB>(ref this.CompressedAny);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETCHGREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_GETCHGREPLY_V1 V1;
		public DRS_MSG_GETCHGREPLY_V2 V2;
		public DRS_MSG_GETCHGREPLY_V6 V6;
		public DRS_MSG_GETCHGREPLY_V7 V7;
		public DRS_MSG_GETCHGREPLY_V9 V9;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._8Byte);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment._8Byte);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.V2, NdrAlignment.NativePtr);
					break;
				case 6U:
					encoder.WriteFixedStruct(this.V6, NdrAlignment._8Byte);
					break;
				case 7U:
					encoder.WriteFixedStruct(this.V7, NdrAlignment.NativePtr);
					break;
				case 9U:
					encoder.WriteFixedStruct(this.V9, NdrAlignment._8Byte);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._8Byte);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREPLY_V1>(NdrAlignment._8Byte);
					break;
				case 2U:
					this.V2 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREPLY_V2>(NdrAlignment.NativePtr);
					break;
				case 6U:
					this.V6 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREPLY_V6>(NdrAlignment._8Byte);
					break;
				case 7U:
					this.V7 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREPLY_V7>(NdrAlignment.NativePtr);
					break;
				case 9U:
					this.V9 = decoder.ReadFixedStruct<DRS_MSG_GETCHGREPLY_V9>(NdrAlignment._8Byte);
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
				case 2U:
					encoder.WriteStructDeferral(this.V2);
					break;
				case 6U:
					encoder.WriteStructDeferral(this.V6);
					break;
				case 7U:
					encoder.WriteStructDeferral(this.V7);
					break;
				case 9U:
					encoder.WriteStructDeferral(this.V9);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					decoder.ReadStructDeferral<DRS_MSG_GETCHGREPLY_V1>(ref this.V1);
					break;
				case 2U:
					decoder.ReadStructDeferral<DRS_MSG_GETCHGREPLY_V2>(ref this.V2);
					break;
				case 6U:
					decoder.ReadStructDeferral<DRS_MSG_GETCHGREPLY_V6>(ref this.V6);
					break;
				case 7U:
					decoder.ReadStructDeferral<DRS_MSG_GETCHGREPLY_V7>(ref this.V7);
					break;
				case 9U:
					decoder.ReadStructDeferral<DRS_MSG_GETCHGREPLY_V9>(ref this.V9);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPSYNC_V1 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pNC;
		public Guid uuidDsaSrc;
		public RpcPointer<string> pszDsaSrc;
		public uint ulOptions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteValue(this.uuidDsaSrc);
			encoder.WriteUniquePointer(this.pszDsaSrc);
			encoder.WriteValue(this.ulOptions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.uuidDsaSrc = decoder.ReadUuid();
			this.pszDsaSrc = decoder.ReadUniquePointer<string>();
			this.ulOptions = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			if (this.pszDsaSrc is not null)
			{
				encoder.WriteUnsignedCharString(this.pszDsaSrc.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			if (this.pszDsaSrc is not null)
			{
				this.pszDsaSrc.value = decoder.ReadUnsignedCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPSYNC_V2 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pNC;
		public Guid uuidDsaSrc;
		public RpcPointer<string> pszDsaSrc;
		public uint ulOptions;
		public Guid correlationID;
		public RpcPointer<VAR_SIZE_BUFFER_WITH_VERSION> pReservedBuffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteValue(this.uuidDsaSrc);
			encoder.WriteUniquePointer(this.pszDsaSrc);
			encoder.WriteValue(this.ulOptions);
			encoder.WriteValue(this.correlationID);
			encoder.WriteUniquePointer(this.pReservedBuffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.uuidDsaSrc = decoder.ReadUuid();
			this.pszDsaSrc = decoder.ReadUniquePointer<string>();
			this.ulOptions = decoder.ReadUInt32();
			this.correlationID = decoder.ReadUuid();
			this.pReservedBuffer = decoder.ReadUniquePointer<VAR_SIZE_BUFFER_WITH_VERSION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			if (this.pszDsaSrc is not null)
			{
				encoder.WriteUnsignedCharString(this.pszDsaSrc.value);
			}

			if (this.pReservedBuffer is not null)
			{
				encoder.WriteConformantStruct(this.pReservedBuffer.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pReservedBuffer.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			if (this.pszDsaSrc is not null)
			{
				this.pszDsaSrc.value = decoder.ReadUnsignedCharString();
			}

			if (this.pReservedBuffer is not null)
			{
				this.pReservedBuffer.value = decoder.ReadConformantStruct<VAR_SIZE_BUFFER_WITH_VERSION>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<VAR_SIZE_BUFFER_WITH_VERSION>(ref this.pReservedBuffer.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPSYNC : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_REPSYNC_V1 V1;
		public DRS_MSG_REPSYNC_V2 V2;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.V2, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_REPSYNC_V1>(NdrAlignment.NativePtr);
					break;
				case 2U:
					this.V2 = decoder.ReadFixedStruct<DRS_MSG_REPSYNC_V2>(NdrAlignment.NativePtr);
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
				case 2U:
					encoder.WriteStructDeferral(this.V2);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					decoder.ReadStructDeferral<DRS_MSG_REPSYNC_V1>(ref this.V1);
					break;
				case 2U:
					decoder.ReadStructDeferral<DRS_MSG_REPSYNC_V2>(ref this.V2);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_UPDREFS_V1 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pNC;
		public RpcPointer<string> pszDsaDest;
		public Guid uuidDsaObjDest;
		public uint ulOptions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteUniquePointer(this.pszDsaDest);
			encoder.WriteValue(this.uuidDsaObjDest);
			encoder.WriteValue(this.ulOptions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.pszDsaDest = decoder.ReadUniquePointer<string>();
			this.uuidDsaObjDest = decoder.ReadUuid();
			this.ulOptions = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			if (this.pszDsaDest is not null)
			{
				encoder.WriteUnsignedCharString(this.pszDsaDest.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			if (this.pszDsaDest is not null)
			{
				this.pszDsaDest.value = decoder.ReadUnsignedCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_UPDREFS_V2 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pNC;
		public RpcPointer<string> pszDsaDest;
		public Guid uuidDsaObjDest;
		public uint ulOptions;
		public Guid correlationID;
		public RpcPointer<VAR_SIZE_BUFFER_WITH_VERSION> pReservedBuffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteUniquePointer(this.pszDsaDest);
			encoder.WriteValue(this.uuidDsaObjDest);
			encoder.WriteValue(this.ulOptions);
			encoder.WriteValue(this.correlationID);
			encoder.WriteUniquePointer(this.pReservedBuffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.pszDsaDest = decoder.ReadUniquePointer<string>();
			this.uuidDsaObjDest = decoder.ReadUuid();
			this.ulOptions = decoder.ReadUInt32();
			this.correlationID = decoder.ReadUuid();
			this.pReservedBuffer = decoder.ReadUniquePointer<VAR_SIZE_BUFFER_WITH_VERSION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			if (this.pszDsaDest is not null)
			{
				encoder.WriteUnsignedCharString(this.pszDsaDest.value);
			}

			if (this.pReservedBuffer is not null)
			{
				encoder.WriteConformantStruct(this.pReservedBuffer.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pReservedBuffer.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			if (this.pszDsaDest is not null)
			{
				this.pszDsaDest.value = decoder.ReadUnsignedCharString();
			}

			if (this.pReservedBuffer is not null)
			{
				this.pReservedBuffer.value = decoder.ReadConformantStruct<VAR_SIZE_BUFFER_WITH_VERSION>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<VAR_SIZE_BUFFER_WITH_VERSION>(ref this.pReservedBuffer.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_UPDREFS : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_UPDREFS_V1 V1;
		public DRS_MSG_UPDREFS_V2 V2;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.V2, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_UPDREFS_V1>(NdrAlignment.NativePtr);
					break;
				case 2U:
					this.V2 = decoder.ReadFixedStruct<DRS_MSG_UPDREFS_V2>(NdrAlignment.NativePtr);
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
				case 2U:
					encoder.WriteStructDeferral(this.V2);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					decoder.ReadStructDeferral<DRS_MSG_UPDREFS_V1>(ref this.V1);
					break;
				case 2U:
					decoder.ReadStructDeferral<DRS_MSG_UPDREFS_V2>(ref this.V2);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPADD_V1 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pNC;
		public RpcPointer<string> pszDsaSrc;
		public REPLTIMES rtSchedule;
		public uint ulOptions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteUniquePointer(this.pszDsaSrc);
			encoder.WriteFixedStruct(this.rtSchedule, NdrAlignment._1Byte);
			encoder.WriteValue(this.ulOptions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.pszDsaSrc = decoder.ReadUniquePointer<string>();
			this.rtSchedule = decoder.ReadFixedStruct<REPLTIMES>(NdrAlignment._1Byte);
			this.ulOptions = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			if (this.pszDsaSrc is not null)
			{
				encoder.WriteUnsignedCharString(this.pszDsaSrc.value);
			}

			encoder.WriteStructDeferral(this.rtSchedule);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			if (this.pszDsaSrc is not null)
			{
				this.pszDsaSrc.value = decoder.ReadUnsignedCharString();
			}

			decoder.ReadStructDeferral<REPLTIMES>(ref this.rtSchedule);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPADD_V2 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pNC;
		public RpcPointer<DSNAME> pSourceDsaDN;
		public RpcPointer<DSNAME> pTransportDN;
		public RpcPointer<string> pszSourceDsaAddress;
		public REPLTIMES rtSchedule;
		public uint ulOptions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteUniquePointer(this.pSourceDsaDN);
			encoder.WriteUniquePointer(this.pTransportDN);
			encoder.WriteUniquePointer(this.pszSourceDsaAddress);
			encoder.WriteFixedStruct(this.rtSchedule, NdrAlignment._1Byte);
			encoder.WriteValue(this.ulOptions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.pSourceDsaDN = decoder.ReadUniquePointer<DSNAME>();
			this.pTransportDN = decoder.ReadUniquePointer<DSNAME>();
			this.pszSourceDsaAddress = decoder.ReadUniquePointer<string>();
			this.rtSchedule = decoder.ReadFixedStruct<REPLTIMES>(NdrAlignment._1Byte);
			this.ulOptions = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			if (this.pSourceDsaDN is not null)
			{
				encoder.WriteConformantStruct(this.pSourceDsaDN.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pSourceDsaDN.value);
			}

			if (this.pTransportDN is not null)
			{
				encoder.WriteConformantStruct(this.pTransportDN.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pTransportDN.value);
			}

			if (this.pszSourceDsaAddress is not null)
			{
				encoder.WriteUnsignedCharString(this.pszSourceDsaAddress.value);
			}

			encoder.WriteStructDeferral(this.rtSchedule);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			if (this.pSourceDsaDN is not null)
			{
				this.pSourceDsaDN.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pSourceDsaDN.value);
			}

			if (this.pTransportDN is not null)
			{
				this.pTransportDN.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pTransportDN.value);
			}

			if (this.pszSourceDsaAddress is not null)
			{
				this.pszSourceDsaAddress.value = decoder.ReadUnsignedCharString();
			}

			decoder.ReadStructDeferral<REPLTIMES>(ref this.rtSchedule);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPADD_V3 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pNC;
		public RpcPointer<DSNAME> pSourceDsaDN;
		public RpcPointer<DSNAME> pTransportDN;
		public RpcPointer<string> pszSourceDsaAddress;
		public REPLTIMES rtSchedule;
		public uint ulOptions;
		public Guid correlationID;
		public RpcPointer<VAR_SIZE_BUFFER_WITH_VERSION> pReservedBuffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteUniquePointer(this.pSourceDsaDN);
			encoder.WriteUniquePointer(this.pTransportDN);
			encoder.WriteUniquePointer(this.pszSourceDsaAddress);
			encoder.WriteFixedStruct(this.rtSchedule, NdrAlignment._1Byte);
			encoder.WriteValue(this.ulOptions);
			encoder.WriteValue(this.correlationID);
			encoder.WriteUniquePointer(this.pReservedBuffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.pSourceDsaDN = decoder.ReadUniquePointer<DSNAME>();
			this.pTransportDN = decoder.ReadUniquePointer<DSNAME>();
			this.pszSourceDsaAddress = decoder.ReadUniquePointer<string>();
			this.rtSchedule = decoder.ReadFixedStruct<REPLTIMES>(NdrAlignment._1Byte);
			this.ulOptions = decoder.ReadUInt32();
			this.correlationID = decoder.ReadUuid();
			this.pReservedBuffer = decoder.ReadUniquePointer<VAR_SIZE_BUFFER_WITH_VERSION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			if (this.pSourceDsaDN is not null)
			{
				encoder.WriteConformantStruct(this.pSourceDsaDN.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pSourceDsaDN.value);
			}

			if (this.pTransportDN is not null)
			{
				encoder.WriteConformantStruct(this.pTransportDN.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pTransportDN.value);
			}

			if (this.pszSourceDsaAddress is not null)
			{
				encoder.WriteUnsignedCharString(this.pszSourceDsaAddress.value);
			}

			encoder.WriteStructDeferral(this.rtSchedule);
			if (this.pReservedBuffer is not null)
			{
				encoder.WriteConformantStruct(this.pReservedBuffer.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pReservedBuffer.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			if (this.pSourceDsaDN is not null)
			{
				this.pSourceDsaDN.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pSourceDsaDN.value);
			}

			if (this.pTransportDN is not null)
			{
				this.pTransportDN.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pTransportDN.value);
			}

			if (this.pszSourceDsaAddress is not null)
			{
				this.pszSourceDsaAddress.value = decoder.ReadUnsignedCharString();
			}

			decoder.ReadStructDeferral<REPLTIMES>(ref this.rtSchedule);
			if (this.pReservedBuffer is not null)
			{
				this.pReservedBuffer.value = decoder.ReadConformantStruct<VAR_SIZE_BUFFER_WITH_VERSION>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<VAR_SIZE_BUFFER_WITH_VERSION>(ref this.pReservedBuffer.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPADD : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_REPADD_V1 V1;
		public DRS_MSG_REPADD_V2 V2;
		public DRS_MSG_REPADD_V3 V3;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.V2, NdrAlignment.NativePtr);
					break;
				case 3U:
					encoder.WriteFixedStruct(this.V3, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_REPADD_V1>(NdrAlignment.NativePtr);
					break;
				case 2U:
					this.V2 = decoder.ReadFixedStruct<DRS_MSG_REPADD_V2>(NdrAlignment.NativePtr);
					break;
				case 3U:
					this.V3 = decoder.ReadFixedStruct<DRS_MSG_REPADD_V3>(NdrAlignment.NativePtr);
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
				case 2U:
					encoder.WriteStructDeferral(this.V2);
					break;
				case 3U:
					encoder.WriteStructDeferral(this.V3);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					decoder.ReadStructDeferral<DRS_MSG_REPADD_V1>(ref this.V1);
					break;
				case 2U:
					decoder.ReadStructDeferral<DRS_MSG_REPADD_V2>(ref this.V2);
					break;
				case 3U:
					decoder.ReadStructDeferral<DRS_MSG_REPADD_V3>(ref this.V3);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPDEL_V1 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pNC;
		public RpcPointer<string> pszDsaSrc;
		public uint ulOptions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteUniquePointer(this.pszDsaSrc);
			encoder.WriteValue(this.ulOptions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.pszDsaSrc = decoder.ReadUniquePointer<string>();
			this.ulOptions = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			if (this.pszDsaSrc is not null)
			{
				encoder.WriteUnsignedCharString(this.pszDsaSrc.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			if (this.pszDsaSrc is not null)
			{
				this.pszDsaSrc.value = decoder.ReadUnsignedCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPDEL : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_REPDEL_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_REPDEL_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_REPDEL_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPMOD_V1 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pNC;
		public Guid uuidSourceDRA;
		public RpcPointer<string> pszSourceDRA;
		public REPLTIMES rtSchedule;
		public uint ulReplicaFlags;
		public uint ulModifyFields;
		public uint ulOptions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteValue(this.uuidSourceDRA);
			encoder.WriteUniquePointer(this.pszSourceDRA);
			encoder.WriteFixedStruct(this.rtSchedule, NdrAlignment._1Byte);
			encoder.WriteValue(this.ulReplicaFlags);
			encoder.WriteValue(this.ulModifyFields);
			encoder.WriteValue(this.ulOptions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.uuidSourceDRA = decoder.ReadUuid();
			this.pszSourceDRA = decoder.ReadUniquePointer<string>();
			this.rtSchedule = decoder.ReadFixedStruct<REPLTIMES>(NdrAlignment._1Byte);
			this.ulReplicaFlags = decoder.ReadUInt32();
			this.ulModifyFields = decoder.ReadUInt32();
			this.ulOptions = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			if (this.pszSourceDRA is not null)
			{
				encoder.WriteUnsignedCharString(this.pszSourceDRA.value);
			}

			encoder.WriteStructDeferral(this.rtSchedule);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			if (this.pszSourceDRA is not null)
			{
				this.pszSourceDRA.value = decoder.ReadUnsignedCharString();
			}

			decoder.ReadStructDeferral<REPLTIMES>(ref this.rtSchedule);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPMOD : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_REPMOD_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_REPMOD_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_REPMOD_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_VERIFYREQ_V1 : IRpcFixedStruct
	{
		public uint dwFlags;
		public uint cNames;
		public RpcPointer<RpcPointer<DSNAME>[]> rpNames;
		public ATTRBLOCK RequiredAttrs;
		public SCHEMA_PREFIX_TABLE PrefixTable;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwFlags);
			encoder.WriteValue(this.cNames);
			encoder.WriteUniquePointer(this.rpNames);
			encoder.WriteFixedStruct(this.RequiredAttrs, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.PrefixTable, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwFlags = decoder.ReadUInt32();
			this.cNames = decoder.ReadUInt32();
			this.rpNames = decoder.ReadUniquePointer<RpcPointer<DSNAME>[]>();
			this.RequiredAttrs = decoder.ReadFixedStruct<ATTRBLOCK>(NdrAlignment.NativePtr);
			this.PrefixTable = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.rpNames is not null)
			{
				encoder.WriteArrayHeader(this.rpNames.value);
				for (int i = 0; i < this.rpNames.value.Length; i++)
				{
					RpcPointer<DSNAME> elem_0 = this.rpNames.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.rpNames.value.Length; i++)
				{
					RpcPointer<DSNAME> elem_0 = this.rpNames.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteConformantStruct(elem_0.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}

			encoder.WriteStructDeferral(this.RequiredAttrs);
			encoder.WriteStructDeferral(this.PrefixTable);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.rpNames is not null)
			{
				this.rpNames.value = decoder.ReadArrayHeader<RpcPointer<DSNAME>>();
				for (int i = 0; i < this.rpNames.value.Length; i++)
				{
					RpcPointer<DSNAME> elem_0 = this.rpNames.value[i];
					elem_0 = decoder.ReadUniquePointer<DSNAME>();
					this.rpNames.value[i] = elem_0;
				}

				for (int i = 0; i < this.rpNames.value.Length; i++)
				{
					RpcPointer<DSNAME> elem_0 = this.rpNames.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<DSNAME>(ref elem_0.value);
					}

					this.rpNames.value[i] = elem_0;
				}
			}

			decoder.ReadStructDeferral<ATTRBLOCK>(ref this.RequiredAttrs);
			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTable);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_VERIFYREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_VERIFYREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_VERIFYREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_VERIFYREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_VERIFYREPLY_V1 : IRpcFixedStruct
	{
		public uint error;
		public uint cNames;
		public RpcPointer<ENTINF[]> rpEntInf;
		public SCHEMA_PREFIX_TABLE PrefixTable;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.error);
			encoder.WriteValue(this.cNames);
			encoder.WriteUniquePointer(this.rpEntInf);
			encoder.WriteFixedStruct(this.PrefixTable, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.error = decoder.ReadUInt32();
			this.cNames = decoder.ReadUInt32();
			this.rpEntInf = decoder.ReadUniquePointer<ENTINF[]>();
			this.PrefixTable = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.rpEntInf is not null)
			{
				encoder.WriteArrayHeader(this.rpEntInf.value);
				for (int i = 0; i < this.rpEntInf.value.Length; i++)
				{
					ENTINF elem_0 = this.rpEntInf.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.rpEntInf.value.Length; i++)
				{
					ENTINF elem_0 = this.rpEntInf.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteStructDeferral(this.PrefixTable);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.rpEntInf is not null)
			{
				this.rpEntInf.value = decoder.ReadArrayHeader<ENTINF>();
				for (int i = 0; i < this.rpEntInf.value.Length; i++)
				{
					ENTINF elem_0 = this.rpEntInf.value[i];
					elem_0 = decoder.ReadFixedStruct<ENTINF>(NdrAlignment.NativePtr);
					this.rpEntInf.value[i] = elem_0;
				}

				for (int i = 0; i < this.rpEntInf.value.Length; i++)
				{
					ENTINF elem_0 = this.rpEntInf.value[i];
					decoder.ReadStructDeferral<ENTINF>(ref elem_0);
					this.rpEntInf.value[i] = elem_0;
				}
			}

			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTable);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_VERIFYREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_VERIFYREPLY_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_VERIFYREPLY_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_VERIFYREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum REVERSE_MEMBERSHIP_OPERATION_TYPE : int
	{
		RevMembGetGroupsForUser = 1,
		RevMembGetAliasMembership = 2,
		RevMembGetAccountGroups = 3,
		RevMembGetResourceGroups = 4,
		RevMembGetUniversalGroups = 5,
		GroupMembersTransitive = 6,
		RevMembGlobalGroupsNonTransitive = 7
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REVMEMB_REQ_V1 : IRpcFixedStruct
	{
		public uint cDsNames;
		public RpcPointer<RpcPointer<DSNAME>[]> ppDsNames;
		public uint dwFlags;
		public REVERSE_MEMBERSHIP_OPERATION_TYPE OperationType;
		public RpcPointer<DSNAME> pLimitingDomain;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cDsNames);
			encoder.WriteUniquePointer(this.ppDsNames);
			encoder.WriteValue(this.dwFlags);
			encoder.WriteEnumShortValue((short)this.OperationType);
			encoder.WriteUniquePointer(this.pLimitingDomain);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cDsNames = decoder.ReadUInt32();
			this.ppDsNames = decoder.ReadUniquePointer<RpcPointer<DSNAME>[]>();
			this.dwFlags = decoder.ReadUInt32();
			this.OperationType = (REVERSE_MEMBERSHIP_OPERATION_TYPE)decoder.ReadEnumShortValue();
			this.pLimitingDomain = decoder.ReadUniquePointer<DSNAME>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ppDsNames is not null)
			{
				encoder.WriteArrayHeader(this.ppDsNames.value);
				for (int i = 0; i < this.ppDsNames.value.Length; i++)
				{
					RpcPointer<DSNAME> elem_0 = this.ppDsNames.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.ppDsNames.value.Length; i++)
				{
					RpcPointer<DSNAME> elem_0 = this.ppDsNames.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteConformantStruct(elem_0.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}

			if (this.pLimitingDomain is not null)
			{
				encoder.WriteConformantStruct(this.pLimitingDomain.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pLimitingDomain.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ppDsNames is not null)
			{
				this.ppDsNames.value = decoder.ReadArrayHeader<RpcPointer<DSNAME>>();
				for (int i = 0; i < this.ppDsNames.value.Length; i++)
				{
					RpcPointer<DSNAME> elem_0 = this.ppDsNames.value[i];
					elem_0 = decoder.ReadUniquePointer<DSNAME>();
					this.ppDsNames.value[i] = elem_0;
				}

				for (int i = 0; i < this.ppDsNames.value.Length; i++)
				{
					RpcPointer<DSNAME> elem_0 = this.ppDsNames.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<DSNAME>(ref elem_0.value);
					}

					this.ppDsNames.value[i] = elem_0;
				}
			}

			if (this.pLimitingDomain is not null)
			{
				this.pLimitingDomain.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pLimitingDomain.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REVMEMB_REQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_REVMEMB_REQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_REVMEMB_REQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_REVMEMB_REQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REVMEMB_REPLY_V1 : IRpcFixedStruct
	{
		public uint errCode;
		public uint cDsNames;
		public uint cSidHistory;
		public RpcPointer<RpcPointer<DSNAME>[]> ppDsNames;
		public RpcPointer<uint[]> pAttributes;
		public RpcPointer<RpcPointer<NT4SID>[]> ppSidHistory;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.errCode);
			encoder.WriteValue(this.cDsNames);
			encoder.WriteValue(this.cSidHistory);
			encoder.WriteUniquePointer(this.ppDsNames);
			encoder.WriteUniquePointer(this.pAttributes);
			encoder.WriteUniquePointer(this.ppSidHistory);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.errCode = decoder.ReadUInt32();
			this.cDsNames = decoder.ReadUInt32();
			this.cSidHistory = decoder.ReadUInt32();
			this.ppDsNames = decoder.ReadUniquePointer<RpcPointer<DSNAME>[]>();
			this.pAttributes = decoder.ReadUniquePointer<uint[]>();
			this.ppSidHistory = decoder.ReadUniquePointer<RpcPointer<NT4SID>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ppDsNames is not null)
			{
				encoder.WriteArrayHeader(this.ppDsNames.value);
				for (int i = 0; i < this.ppDsNames.value.Length; i++)
				{
					RpcPointer<DSNAME> elem_0 = this.ppDsNames.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.ppDsNames.value.Length; i++)
				{
					RpcPointer<DSNAME> elem_0 = this.ppDsNames.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteConformantStruct(elem_0.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}

			if (this.pAttributes is not null)
			{
				encoder.WriteArrayHeader(this.pAttributes.value);
				for (int i = 0; i < this.pAttributes.value.Length; i++)
				{
					uint elem_0 = this.pAttributes.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.ppSidHistory is not null)
			{
				encoder.WriteArrayHeader(this.ppSidHistory.value);
				for (int i = 0; i < this.ppSidHistory.value.Length; i++)
				{
					RpcPointer<NT4SID> elem_0 = this.ppSidHistory.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.ppSidHistory.value.Length; i++)
				{
					RpcPointer<NT4SID> elem_0 = this.ppSidHistory.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteFixedStruct(elem_0.value, NdrAlignment._1Byte);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ppDsNames is not null)
			{
				this.ppDsNames.value = decoder.ReadArrayHeader<RpcPointer<DSNAME>>();
				for (int i = 0; i < this.ppDsNames.value.Length; i++)
				{
					RpcPointer<DSNAME> elem_0 = this.ppDsNames.value[i];
					elem_0 = decoder.ReadUniquePointer<DSNAME>();
					this.ppDsNames.value[i] = elem_0;
				}

				for (int i = 0; i < this.ppDsNames.value.Length; i++)
				{
					RpcPointer<DSNAME> elem_0 = this.ppDsNames.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<DSNAME>(ref elem_0.value);
					}

					this.ppDsNames.value[i] = elem_0;
				}
			}

			if (this.pAttributes is not null)
			{
				this.pAttributes.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < this.pAttributes.value.Length; i++)
				{
					uint elem_0 = this.pAttributes.value[i];
					elem_0 = decoder.ReadUInt32();
					this.pAttributes.value[i] = elem_0;
				}
			}

			if (this.ppSidHistory is not null)
			{
				this.ppSidHistory.value = decoder.ReadArrayHeader<RpcPointer<NT4SID>>();
				for (int i = 0; i < this.ppSidHistory.value.Length; i++)
				{
					RpcPointer<NT4SID> elem_0 = this.ppSidHistory.value[i];
					elem_0 = decoder.ReadUniquePointer<NT4SID>();
					this.ppSidHistory.value[i] = elem_0;
				}

				for (int i = 0; i < this.ppSidHistory.value.Length; i++)
				{
					RpcPointer<NT4SID> elem_0 = this.ppSidHistory.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadFixedStruct<NT4SID>(NdrAlignment._1Byte);
						decoder.ReadStructDeferral<NT4SID>(ref elem_0.value);
					}

					this.ppSidHistory.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REVMEMB_REPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_REVMEMB_REPLY_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_REVMEMB_REPLY_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_REVMEMB_REPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_MOVEREQ_V1 : IRpcFixedStruct
	{
		public RpcPointer<byte> pSourceDSA;
		public RpcPointer<ENTINF> pObject;
		public RpcPointer<Guid> pParentUUID;
		public SCHEMA_PREFIX_TABLE PrefixTable;
		public uint ulFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pSourceDSA);
			encoder.WriteUniquePointer(this.pObject);
			encoder.WriteUniquePointer(this.pParentUUID);
			encoder.WriteFixedStruct(this.PrefixTable, NdrAlignment.NativePtr);
			encoder.WriteValue(this.ulFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pSourceDSA = decoder.ReadUniquePointer<byte>();
			this.pObject = decoder.ReadUniquePointer<ENTINF>();
			this.pParentUUID = decoder.ReadUniquePointer<Guid>();
			this.PrefixTable = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
			this.ulFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pSourceDSA is not null)
			{
				encoder.WriteValue(this.pSourceDSA.value);
			}

			if (this.pObject is not null)
			{
				encoder.WriteFixedStruct(this.pObject.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pObject.value);
			}

			if (this.pParentUUID is not null)
			{
				encoder.WriteValue(this.pParentUUID.value);
			}

			encoder.WriteStructDeferral(this.PrefixTable);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pSourceDSA is not null)
			{
				this.pSourceDSA.value = decoder.ReadUnsignedChar();
			}

			if (this.pObject is not null)
			{
				this.pObject.value = decoder.ReadFixedStruct<ENTINF>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ENTINF>(ref this.pObject.value);
			}

			if (this.pParentUUID is not null)
			{
				this.pParentUUID.value = decoder.ReadUuid();
			}

			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTable);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_SecBuffer : IRpcFixedStruct
	{
		public uint cbBuffer;
		public uint BufferType;
		public RpcPointer<byte[]> pvBuffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cbBuffer);
			encoder.WriteValue(this.BufferType);
			encoder.WriteUniquePointer(this.pvBuffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cbBuffer = decoder.ReadUInt32();
			this.BufferType = decoder.ReadUInt32();
			this.pvBuffer = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pvBuffer is not null)
			{
				encoder.WriteArrayHeader(this.pvBuffer.value);
				for (int i = 0; i < this.pvBuffer.value.Length; i++)
				{
					byte elem_0 = this.pvBuffer.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pvBuffer is not null)
			{
				this.pvBuffer.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pvBuffer.value.Length; i++)
				{
					byte elem_0 = this.pvBuffer.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pvBuffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_SecBufferDesc : IRpcFixedStruct
	{
		public uint ulVersion;
		public uint cBuffers;
		public RpcPointer<DRS_SecBuffer[]> Buffers;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ulVersion);
			encoder.WriteValue(this.cBuffers);
			encoder.WriteUniquePointer(this.Buffers);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ulVersion = decoder.ReadUInt32();
			this.cBuffers = decoder.ReadUInt32();
			this.Buffers = decoder.ReadUniquePointer<DRS_SecBuffer[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffers is not null)
			{
				encoder.WriteArrayHeader(this.Buffers.value);
				for (int i = 0; i < this.Buffers.value.Length; i++)
				{
					DRS_SecBuffer elem_0 = this.Buffers.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffers.value.Length; i++)
				{
					DRS_SecBuffer elem_0 = this.Buffers.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffers is not null)
			{
				this.Buffers.value = decoder.ReadArrayHeader<DRS_SecBuffer>();
				for (int i = 0; i < this.Buffers.value.Length; i++)
				{
					DRS_SecBuffer elem_0 = this.Buffers.value[i];
					elem_0 = decoder.ReadFixedStruct<DRS_SecBuffer>(NdrAlignment.NativePtr);
					this.Buffers.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffers.value.Length; i++)
				{
					DRS_SecBuffer elem_0 = this.Buffers.value[i];
					decoder.ReadStructDeferral<DRS_SecBuffer>(ref elem_0);
					this.Buffers.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_MOVEREQ_V2 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pSrcDSA;
		public RpcPointer<ENTINF> pSrcObject;
		public RpcPointer<DSNAME> pDstName;
		public RpcPointer<DSNAME> pExpectedTargetNC;
		public RpcPointer<DRS_SecBufferDesc> pClientCreds;
		public SCHEMA_PREFIX_TABLE PrefixTable;
		public uint ulFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pSrcDSA);
			encoder.WriteUniquePointer(this.pSrcObject);
			encoder.WriteUniquePointer(this.pDstName);
			encoder.WriteUniquePointer(this.pExpectedTargetNC);
			encoder.WriteUniquePointer(this.pClientCreds);
			encoder.WriteFixedStruct(this.PrefixTable, NdrAlignment.NativePtr);
			encoder.WriteValue(this.ulFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pSrcDSA = decoder.ReadUniquePointer<DSNAME>();
			this.pSrcObject = decoder.ReadUniquePointer<ENTINF>();
			this.pDstName = decoder.ReadUniquePointer<DSNAME>();
			this.pExpectedTargetNC = decoder.ReadUniquePointer<DSNAME>();
			this.pClientCreds = decoder.ReadUniquePointer<DRS_SecBufferDesc>();
			this.PrefixTable = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
			this.ulFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pSrcDSA is not null)
			{
				encoder.WriteConformantStruct(this.pSrcDSA.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pSrcDSA.value);
			}

			if (this.pSrcObject is not null)
			{
				encoder.WriteFixedStruct(this.pSrcObject.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pSrcObject.value);
			}

			if (this.pDstName is not null)
			{
				encoder.WriteConformantStruct(this.pDstName.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pDstName.value);
			}

			if (this.pExpectedTargetNC is not null)
			{
				encoder.WriteConformantStruct(this.pExpectedTargetNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pExpectedTargetNC.value);
			}

			if (this.pClientCreds is not null)
			{
				encoder.WriteFixedStruct(this.pClientCreds.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pClientCreds.value);
			}

			encoder.WriteStructDeferral(this.PrefixTable);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pSrcDSA is not null)
			{
				this.pSrcDSA.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pSrcDSA.value);
			}

			if (this.pSrcObject is not null)
			{
				this.pSrcObject.value = decoder.ReadFixedStruct<ENTINF>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ENTINF>(ref this.pSrcObject.value);
			}

			if (this.pDstName is not null)
			{
				this.pDstName.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pDstName.value);
			}

			if (this.pExpectedTargetNC is not null)
			{
				this.pExpectedTargetNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pExpectedTargetNC.value);
			}

			if (this.pClientCreds is not null)
			{
				this.pClientCreds.value = decoder.ReadFixedStruct<DRS_SecBufferDesc>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<DRS_SecBufferDesc>(ref this.pClientCreds.value);
			}

			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTable);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_MOVEREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_MOVEREQ_V1 V1;
		public DRS_MSG_MOVEREQ_V2 V2;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.V2, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_MOVEREQ_V1>(NdrAlignment.NativePtr);
					break;
				case 2U:
					this.V2 = decoder.ReadFixedStruct<DRS_MSG_MOVEREQ_V2>(NdrAlignment.NativePtr);
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
				case 2U:
					encoder.WriteStructDeferral(this.V2);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					decoder.ReadStructDeferral<DRS_MSG_MOVEREQ_V1>(ref this.V1);
					break;
				case 2U:
					decoder.ReadStructDeferral<DRS_MSG_MOVEREQ_V2>(ref this.V2);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_MOVEREPLY_V1 : IRpcFixedStruct
	{
		public RpcPointer<RpcPointer<ENTINF>> ppResult;
		public SCHEMA_PREFIX_TABLE PrefixTable;
		public RpcPointer<uint> pError;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.ppResult);
			encoder.WriteFixedStruct(this.PrefixTable, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.pError);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ppResult = decoder.ReadUniquePointer<RpcPointer<ENTINF>>();
			this.PrefixTable = decoder.ReadFixedStruct<SCHEMA_PREFIX_TABLE>(NdrAlignment.NativePtr);
			this.pError = decoder.ReadUniquePointer<uint>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ppResult is not null)
			{
				encoder.WriteUniquePointer(this.ppResult.value);
				if (this.ppResult.value is not null)
				{
					encoder.WriteFixedStruct(this.ppResult.value.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(this.ppResult.value.value);
				}
			}

			encoder.WriteStructDeferral(this.PrefixTable);
			if (this.pError is not null)
			{
				encoder.WriteValue(this.pError.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ppResult is not null)
			{
				this.ppResult.value = decoder.ReadUniquePointer<ENTINF>();
				if (this.ppResult.value is not null)
				{
					this.ppResult.value.value = decoder.ReadFixedStruct<ENTINF>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<ENTINF>(ref this.ppResult.value.value);
				}
			}

			decoder.ReadStructDeferral<SCHEMA_PREFIX_TABLE>(ref this.PrefixTable);
			if (this.pError is not null)
			{
				this.pError.value = decoder.ReadUInt32();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_MOVEREPLY_V2 : IRpcFixedStruct
	{
		public uint win32Error;
		public RpcPointer<DSNAME> pAddedName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.win32Error);
			encoder.WriteUniquePointer(this.pAddedName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.win32Error = decoder.ReadUInt32();
			this.pAddedName = decoder.ReadUniquePointer<DSNAME>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pAddedName is not null)
			{
				encoder.WriteConformantStruct(this.pAddedName.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pAddedName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pAddedName is not null)
			{
				this.pAddedName.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pAddedName.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_MOVEREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_MOVEREPLY_V1 V1;
		public DRS_MSG_MOVEREPLY_V2 V2;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.V2, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_MOVEREPLY_V1>(NdrAlignment.NativePtr);
					break;
				case 2U:
					this.V2 = decoder.ReadFixedStruct<DRS_MSG_MOVEREPLY_V2>(NdrAlignment.NativePtr);
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
				case 2U:
					encoder.WriteStructDeferral(this.V2);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					decoder.ReadStructDeferral<DRS_MSG_MOVEREPLY_V1>(ref this.V1);
					break;
				case 2U:
					decoder.ReadStructDeferral<DRS_MSG_MOVEREPLY_V2>(ref this.V2);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_CRACKREQ_V1 : IRpcFixedStruct
	{
		public uint CodePage;
		public uint LocaleId;
		public uint dwFlags;
		public uint formatOffered;
		public uint formatDesired;
		public uint cNames;
		public RpcPointer<RpcPointer<string>[]> rpNames;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.CodePage);
			encoder.WriteValue(this.LocaleId);
			encoder.WriteValue(this.dwFlags);
			encoder.WriteValue(this.formatOffered);
			encoder.WriteValue(this.formatDesired);
			encoder.WriteValue(this.cNames);
			encoder.WriteUniquePointer(this.rpNames);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.CodePage = decoder.ReadUInt32();
			this.LocaleId = decoder.ReadUInt32();
			this.dwFlags = decoder.ReadUInt32();
			this.formatOffered = decoder.ReadUInt32();
			this.formatDesired = decoder.ReadUInt32();
			this.cNames = decoder.ReadUInt32();
			this.rpNames = decoder.ReadUniquePointer<RpcPointer<string>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.rpNames is not null)
			{
				encoder.WriteArrayHeader(this.rpNames.value);
				for (int i = 0; i < this.rpNames.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.rpNames.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.rpNames.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.rpNames.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteWideCharString(elem_0.value);
					}
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.rpNames is not null)
			{
				this.rpNames.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < this.rpNames.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.rpNames.value[i];
					elem_0 = decoder.ReadUniquePointer<string>();
					this.rpNames.value[i] = elem_0;
				}

				for (int i = 0; i < this.rpNames.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.rpNames.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadWideCharString();
					}

					this.rpNames.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_CRACKREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_CRACKREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_CRACKREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_CRACKREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_CRACKREPLY_V1 : IRpcFixedStruct
	{
		public RpcPointer<DS_NAME_RESULTW> pResult;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pResult);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pResult = decoder.ReadUniquePointer<DS_NAME_RESULTW>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pResult is not null)
			{
				encoder.WriteFixedStruct(this.pResult.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pResult.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pResult is not null)
			{
				this.pResult.value = decoder.ReadFixedStruct<DS_NAME_RESULTW>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<DS_NAME_RESULTW>(ref this.pResult.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_CRACKREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_CRACKREPLY_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_CRACKREPLY_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_CRACKREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_NT4_CHGLOG_REQ_V1 : IRpcFixedStruct
	{
		public uint dwFlags;
		public uint PreferredMaximumLength;
		public uint cbRestart;
		public RpcPointer<byte[]> pRestart;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwFlags);
			encoder.WriteValue(this.PreferredMaximumLength);
			encoder.WriteValue(this.cbRestart);
			encoder.WriteUniquePointer(this.pRestart);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwFlags = decoder.ReadUInt32();
			this.PreferredMaximumLength = decoder.ReadUInt32();
			this.cbRestart = decoder.ReadUInt32();
			this.pRestart = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pRestart is not null)
			{
				encoder.WriteArrayHeader(this.pRestart.value);
				for (int i = 0; i < this.pRestart.value.Length; i++)
				{
					byte elem_0 = this.pRestart.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pRestart is not null)
			{
				this.pRestart.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pRestart.value.Length; i++)
				{
					byte elem_0 = this.pRestart.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pRestart.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_NT4_CHGLOG_REQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_NT4_CHGLOG_REQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_NT4_CHGLOG_REQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_NT4_CHGLOG_REQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct NT4_REPLICATION_STATE : IRpcFixedStruct
	{
		public ms_dtyp.LARGE_INTEGER SamSerialNumber;
		public ms_dtyp.LARGE_INTEGER SamCreationTime;
		public ms_dtyp.LARGE_INTEGER BuiltinSerialNumber;
		public ms_dtyp.LARGE_INTEGER BuiltinCreationTime;
		public ms_dtyp.LARGE_INTEGER LsaSerialNumber;
		public ms_dtyp.LARGE_INTEGER LsaCreationTime;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.SamSerialNumber, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.SamCreationTime, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.BuiltinSerialNumber, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.BuiltinCreationTime, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.LsaSerialNumber, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.LsaCreationTime, NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.SamSerialNumber = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.SamCreationTime = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.BuiltinSerialNumber = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.BuiltinCreationTime = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.LsaSerialNumber = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.LsaCreationTime = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.SamSerialNumber);
			encoder.WriteStructDeferral(this.SamCreationTime);
			encoder.WriteStructDeferral(this.BuiltinSerialNumber);
			encoder.WriteStructDeferral(this.BuiltinCreationTime);
			encoder.WriteStructDeferral(this.LsaSerialNumber);
			encoder.WriteStructDeferral(this.LsaCreationTime);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.SamSerialNumber);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.SamCreationTime);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.BuiltinSerialNumber);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.BuiltinCreationTime);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LsaSerialNumber);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LsaCreationTime);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_NT4_CHGLOG_REPLY_V1 : IRpcFixedStruct
	{
		public uint cbRestart;
		public uint cbLog;
		public NT4_REPLICATION_STATE ReplicationState;
		public uint ActualNtStatus;
		public RpcPointer<byte[]> pRestart;
		public RpcPointer<byte[]> pLog;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cbRestart);
			encoder.WriteValue(this.cbLog);
			encoder.WriteFixedStruct(this.ReplicationState, NdrAlignment._8Byte);
			encoder.WriteValue(this.ActualNtStatus);
			encoder.WriteUniquePointer(this.pRestart);
			encoder.WriteUniquePointer(this.pLog);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cbRestart = decoder.ReadUInt32();
			this.cbLog = decoder.ReadUInt32();
			this.ReplicationState = decoder.ReadFixedStruct<NT4_REPLICATION_STATE>(NdrAlignment._8Byte);
			this.ActualNtStatus = decoder.ReadUInt32();
			this.pRestart = decoder.ReadUniquePointer<byte[]>();
			this.pLog = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ReplicationState);
			if (this.pRestart is not null)
			{
				encoder.WriteArrayHeader(this.pRestart.value);
				for (int i = 0; i < this.pRestart.value.Length; i++)
				{
					byte elem_0 = this.pRestart.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.pLog is not null)
			{
				encoder.WriteArrayHeader(this.pLog.value);
				for (int i = 0; i < this.pLog.value.Length; i++)
				{
					byte elem_0 = this.pLog.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<NT4_REPLICATION_STATE>(ref this.ReplicationState);
			if (this.pRestart is not null)
			{
				this.pRestart.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pRestart.value.Length; i++)
				{
					byte elem_0 = this.pRestart.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pRestart.value[i] = elem_0;
				}
			}

			if (this.pLog is not null)
			{
				this.pLog.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pLog.value.Length; i++)
				{
					byte elem_0 = this.pLog.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pLog.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_NT4_CHGLOG_REPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_NT4_CHGLOG_REPLY_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._8Byte);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment._8Byte);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._8Byte);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_NT4_CHGLOG_REPLY_V1>(NdrAlignment._8Byte);
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
					decoder.ReadStructDeferral<DRS_MSG_NT4_CHGLOG_REPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_SPNREQ_V1 : IRpcFixedStruct
	{
		public uint operation;
		public uint flags;
		public RpcPointer<string> pwszAccount;
		public uint cSPN;
		public RpcPointer<RpcPointer<string>[]> rpwszSPN;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.operation);
			encoder.WriteValue(this.flags);
			encoder.WriteUniquePointer(this.pwszAccount);
			encoder.WriteValue(this.cSPN);
			encoder.WriteUniquePointer(this.rpwszSPN);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.operation = decoder.ReadUInt32();
			this.flags = decoder.ReadUInt32();
			this.pwszAccount = decoder.ReadUniquePointer<string>();
			this.cSPN = decoder.ReadUInt32();
			this.rpwszSPN = decoder.ReadUniquePointer<RpcPointer<string>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pwszAccount is not null)
			{
				encoder.WriteWideCharString(this.pwszAccount.value);
			}

			if (this.rpwszSPN is not null)
			{
				encoder.WriteArrayHeader(this.rpwszSPN.value);
				for (int i = 0; i < this.rpwszSPN.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.rpwszSPN.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.rpwszSPN.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.rpwszSPN.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteWideCharString(elem_0.value);
					}
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pwszAccount is not null)
			{
				this.pwszAccount.value = decoder.ReadWideCharString();
			}

			if (this.rpwszSPN is not null)
			{
				this.rpwszSPN.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < this.rpwszSPN.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.rpwszSPN.value[i];
					elem_0 = decoder.ReadUniquePointer<string>();
					this.rpwszSPN.value[i] = elem_0;
				}

				for (int i = 0; i < this.rpwszSPN.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.rpwszSPN.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadWideCharString();
					}

					this.rpwszSPN.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_SPNREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_SPNREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_SPNREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_SPNREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_SPNREPLY_V1 : IRpcFixedStruct
	{
		public uint retVal;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.retVal);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.retVal = decoder.ReadUInt32();
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
	public partial struct DRS_MSG_SPNREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_SPNREPLY_V1 V1;
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
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_SPNREPLY_V1>(NdrAlignment._4Byte);
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
					decoder.ReadStructDeferral<DRS_MSG_SPNREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_RMSVRREQ_V1 : IRpcFixedStruct
	{
		public RpcPointer<string> ServerDN;
		public RpcPointer<string> DomainDN;
		public int fCommit;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.ServerDN);
			encoder.WriteUniquePointer(this.DomainDN);
			encoder.WriteValue(this.fCommit);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ServerDN = decoder.ReadUniquePointer<string>();
			this.DomainDN = decoder.ReadUniquePointer<string>();
			this.fCommit = decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ServerDN is not null)
			{
				encoder.WriteWideCharString(this.ServerDN.value);
			}

			if (this.DomainDN is not null)
			{
				encoder.WriteWideCharString(this.DomainDN.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ServerDN is not null)
			{
				this.ServerDN.value = decoder.ReadWideCharString();
			}

			if (this.DomainDN is not null)
			{
				this.DomainDN.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_RMSVRREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_RMSVRREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_RMSVRREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_RMSVRREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_RMSVRREPLY_V1 : IRpcFixedStruct
	{
		public int fLastDcInDomain;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.fLastDcInDomain);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.fLastDcInDomain = decoder.ReadInt32();
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
	public partial struct DRS_MSG_RMSVRREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_RMSVRREPLY_V1 V1;
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
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_RMSVRREPLY_V1>(NdrAlignment._4Byte);
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
					decoder.ReadStructDeferral<DRS_MSG_RMSVRREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_RMDMNREQ_V1 : IRpcFixedStruct
	{
		public RpcPointer<string> DomainDN;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.DomainDN);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.DomainDN = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.DomainDN is not null)
			{
				encoder.WriteWideCharString(this.DomainDN.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.DomainDN is not null)
			{
				this.DomainDN.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_RMDMNREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_RMDMNREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_RMDMNREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_RMDMNREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_RMDMNREPLY_V1 : IRpcFixedStruct
	{
		public uint Reserved;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Reserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Reserved = decoder.ReadUInt32();
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
	public partial struct DRS_MSG_RMDMNREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_RMDMNREPLY_V1 V1;
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
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_RMDMNREPLY_V1>(NdrAlignment._4Byte);
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
					decoder.ReadStructDeferral<DRS_MSG_RMDMNREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_DCINFOREQ_V1 : IRpcFixedStruct
	{
		public RpcPointer<string> Domain;
		public uint InfoLevel;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.Domain);
			encoder.WriteValue(this.InfoLevel);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Domain = decoder.ReadUniquePointer<string>();
			this.InfoLevel = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Domain is not null)
			{
				encoder.WriteWideCharString(this.Domain.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Domain is not null)
			{
				this.Domain.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_DCINFOREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_DCINFOREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_DCINFOREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_DCINFOREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_DCINFOREPLY_V1 : IRpcFixedStruct
	{
		public uint cItems;
		public RpcPointer<DS_DOMAIN_CONTROLLER_INFO_1W[]> rItems;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cItems);
			encoder.WriteUniquePointer(this.rItems);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cItems = decoder.ReadUInt32();
			this.rItems = decoder.ReadUniquePointer<DS_DOMAIN_CONTROLLER_INFO_1W[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.rItems is not null)
			{
				encoder.WriteArrayHeader(this.rItems.value);
				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_1W elem_0 = this.rItems.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_1W elem_0 = this.rItems.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.rItems is not null)
			{
				this.rItems.value = decoder.ReadArrayHeader<DS_DOMAIN_CONTROLLER_INFO_1W>();
				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_1W elem_0 = this.rItems.value[i];
					elem_0 = decoder.ReadFixedStruct<DS_DOMAIN_CONTROLLER_INFO_1W>(NdrAlignment.NativePtr);
					this.rItems.value[i] = elem_0;
				}

				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_1W elem_0 = this.rItems.value[i];
					decoder.ReadStructDeferral<DS_DOMAIN_CONTROLLER_INFO_1W>(ref elem_0);
					this.rItems.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_DCINFOREPLY_V2 : IRpcFixedStruct
	{
		public uint cItems;
		public RpcPointer<DS_DOMAIN_CONTROLLER_INFO_2W[]> rItems;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cItems);
			encoder.WriteUniquePointer(this.rItems);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cItems = decoder.ReadUInt32();
			this.rItems = decoder.ReadUniquePointer<DS_DOMAIN_CONTROLLER_INFO_2W[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.rItems is not null)
			{
				encoder.WriteArrayHeader(this.rItems.value);
				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_2W elem_0 = this.rItems.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_2W elem_0 = this.rItems.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.rItems is not null)
			{
				this.rItems.value = decoder.ReadArrayHeader<DS_DOMAIN_CONTROLLER_INFO_2W>();
				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_2W elem_0 = this.rItems.value[i];
					elem_0 = decoder.ReadFixedStruct<DS_DOMAIN_CONTROLLER_INFO_2W>(NdrAlignment.NativePtr);
					this.rItems.value[i] = elem_0;
				}

				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_2W elem_0 = this.rItems.value[i];
					decoder.ReadStructDeferral<DS_DOMAIN_CONTROLLER_INFO_2W>(ref elem_0);
					this.rItems.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_DCINFOREPLY_V3 : IRpcFixedStruct
	{
		public uint cItems;
		public RpcPointer<DS_DOMAIN_CONTROLLER_INFO_3W[]> rItems;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cItems);
			encoder.WriteUniquePointer(this.rItems);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cItems = decoder.ReadUInt32();
			this.rItems = decoder.ReadUniquePointer<DS_DOMAIN_CONTROLLER_INFO_3W[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.rItems is not null)
			{
				encoder.WriteArrayHeader(this.rItems.value);
				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_3W elem_0 = this.rItems.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_3W elem_0 = this.rItems.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.rItems is not null)
			{
				this.rItems.value = decoder.ReadArrayHeader<DS_DOMAIN_CONTROLLER_INFO_3W>();
				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_3W elem_0 = this.rItems.value[i];
					elem_0 = decoder.ReadFixedStruct<DS_DOMAIN_CONTROLLER_INFO_3W>(NdrAlignment.NativePtr);
					this.rItems.value[i] = elem_0;
				}

				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_3W elem_0 = this.rItems.value[i];
					decoder.ReadStructDeferral<DS_DOMAIN_CONTROLLER_INFO_3W>(ref elem_0);
					this.rItems.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_DCINFOREPLY_VFFFFFFFF : IRpcFixedStruct
	{
		public uint cItems;
		public RpcPointer<DS_DOMAIN_CONTROLLER_INFO_FFFFFFFFW[]> rItems;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cItems);
			encoder.WriteUniquePointer(this.rItems);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cItems = decoder.ReadUInt32();
			this.rItems = decoder.ReadUniquePointer<DS_DOMAIN_CONTROLLER_INFO_FFFFFFFFW[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.rItems is not null)
			{
				encoder.WriteArrayHeader(this.rItems.value);
				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_FFFFFFFFW elem_0 = this.rItems.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_FFFFFFFFW elem_0 = this.rItems.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.rItems is not null)
			{
				this.rItems.value = decoder.ReadArrayHeader<DS_DOMAIN_CONTROLLER_INFO_FFFFFFFFW>();
				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_FFFFFFFFW elem_0 = this.rItems.value[i];
					elem_0 = decoder.ReadFixedStruct<DS_DOMAIN_CONTROLLER_INFO_FFFFFFFFW>(NdrAlignment.NativePtr);
					this.rItems.value[i] = elem_0;
				}

				for (int i = 0; i < this.rItems.value.Length; i++)
				{
					DS_DOMAIN_CONTROLLER_INFO_FFFFFFFFW elem_0 = this.rItems.value[i];
					decoder.ReadStructDeferral<DS_DOMAIN_CONTROLLER_INFO_FFFFFFFFW>(ref elem_0);
					this.rItems.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_DCINFOREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_DCINFOREPLY_V1 V1;
		public DRS_MSG_DCINFOREPLY_V2 V2;
		public DRS_MSG_DCINFOREPLY_V3 V3;
		public DRS_MSG_DCINFOREPLY_VFFFFFFFF VFFFFFFFF;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.V2, NdrAlignment.NativePtr);
					break;
				case 3U:
					encoder.WriteFixedStruct(this.V3, NdrAlignment.NativePtr);
					break;
				case 0xFFFFFFFF:
					encoder.WriteFixedStruct(this.VFFFFFFFF, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_DCINFOREPLY_V1>(NdrAlignment.NativePtr);
					break;
				case 2U:
					this.V2 = decoder.ReadFixedStruct<DRS_MSG_DCINFOREPLY_V2>(NdrAlignment.NativePtr);
					break;
				case 3U:
					this.V3 = decoder.ReadFixedStruct<DRS_MSG_DCINFOREPLY_V3>(NdrAlignment.NativePtr);
					break;
				case 0xFFFFFFFF:
					this.VFFFFFFFF = decoder.ReadFixedStruct<DRS_MSG_DCINFOREPLY_VFFFFFFFF>(NdrAlignment.NativePtr);
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
				case 2U:
					encoder.WriteStructDeferral(this.V2);
					break;
				case 3U:
					encoder.WriteStructDeferral(this.V3);
					break;
				case 0xFFFFFFFF:
					encoder.WriteStructDeferral(this.VFFFFFFFF);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					decoder.ReadStructDeferral<DRS_MSG_DCINFOREPLY_V1>(ref this.V1);
					break;
				case 2U:
					decoder.ReadStructDeferral<DRS_MSG_DCINFOREPLY_V2>(ref this.V2);
					break;
				case 3U:
					decoder.ReadStructDeferral<DRS_MSG_DCINFOREPLY_V3>(ref this.V3);
					break;
				case 0xFFFFFFFF:
					decoder.ReadStructDeferral<DRS_MSG_DCINFOREPLY_VFFFFFFFF>(ref this.VFFFFFFFF);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDENTRYREQ_V1 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pObject;
		public ATTRBLOCK AttrBlock;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pObject);
			encoder.WriteFixedStruct(this.AttrBlock, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pObject = decoder.ReadUniquePointer<DSNAME>();
			this.AttrBlock = decoder.ReadFixedStruct<ATTRBLOCK>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pObject is not null)
			{
				encoder.WriteConformantStruct(this.pObject.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pObject.value);
			}

			encoder.WriteStructDeferral(this.AttrBlock);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pObject is not null)
			{
				this.pObject.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pObject.value);
			}

			decoder.ReadStructDeferral<ATTRBLOCK>(ref this.AttrBlock);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDENTRYREQ_V2 : IRpcFixedStruct
	{
		public ENTINFLIST EntInfList;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.EntInfList, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntInfList = decoder.ReadFixedStruct<ENTINFLIST>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.EntInfList);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ENTINFLIST>(ref this.EntInfList);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDENTRYREQ_V3 : IRpcFixedStruct
	{
		public ENTINFLIST EntInfList;
		public RpcPointer<DRS_SecBufferDesc> pClientCreds;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.EntInfList, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.pClientCreds);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntInfList = decoder.ReadFixedStruct<ENTINFLIST>(NdrAlignment.NativePtr);
			this.pClientCreds = decoder.ReadUniquePointer<DRS_SecBufferDesc>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.EntInfList);
			if (this.pClientCreds is not null)
			{
				encoder.WriteFixedStruct(this.pClientCreds.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pClientCreds.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ENTINFLIST>(ref this.EntInfList);
			if (this.pClientCreds is not null)
			{
				this.pClientCreds.value = decoder.ReadFixedStruct<DRS_SecBufferDesc>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<DRS_SecBufferDesc>(ref this.pClientCreds.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDENTRYREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_ADDENTRYREQ_V1 V1;
		public DRS_MSG_ADDENTRYREQ_V2 V2;
		public DRS_MSG_ADDENTRYREQ_V3 V3;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.V2, NdrAlignment.NativePtr);
					break;
				case 3U:
					encoder.WriteFixedStruct(this.V3, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_ADDENTRYREQ_V1>(NdrAlignment.NativePtr);
					break;
				case 2U:
					this.V2 = decoder.ReadFixedStruct<DRS_MSG_ADDENTRYREQ_V2>(NdrAlignment.NativePtr);
					break;
				case 3U:
					this.V3 = decoder.ReadFixedStruct<DRS_MSG_ADDENTRYREQ_V3>(NdrAlignment.NativePtr);
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
				case 2U:
					encoder.WriteStructDeferral(this.V2);
					break;
				case 3U:
					encoder.WriteStructDeferral(this.V3);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					decoder.ReadStructDeferral<DRS_MSG_ADDENTRYREQ_V1>(ref this.V1);
					break;
				case 2U:
					decoder.ReadStructDeferral<DRS_MSG_ADDENTRYREQ_V2>(ref this.V2);
					break;
				case 3U:
					decoder.ReadStructDeferral<DRS_MSG_ADDENTRYREQ_V3>(ref this.V3);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDENTRYREPLY_V1 : IRpcFixedStruct
	{
		public Guid Guid;
		public NT4SID Sid;
		public uint errCode;
		public uint dsid;
		public uint extendedErr;
		public uint extendedData;
		public ushort problem;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Guid);
			encoder.WriteFixedStruct(this.Sid, NdrAlignment._1Byte);
			encoder.WriteValue(this.errCode);
			encoder.WriteValue(this.dsid);
			encoder.WriteValue(this.extendedErr);
			encoder.WriteValue(this.extendedData);
			encoder.WriteValue(this.problem);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Guid = decoder.ReadUuid();
			this.Sid = decoder.ReadFixedStruct<NT4SID>(NdrAlignment._1Byte);
			this.errCode = decoder.ReadUInt32();
			this.dsid = decoder.ReadUInt32();
			this.extendedErr = decoder.ReadUInt32();
			this.extendedData = decoder.ReadUInt32();
			this.problem = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Sid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<NT4SID>(ref this.Sid);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ADDENTRY_REPLY_INFO : IRpcFixedStruct
	{
		public Guid objGuid;
		public NT4SID objSid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.objGuid);
			encoder.WriteFixedStruct(this.objSid, NdrAlignment._1Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.objGuid = decoder.ReadUuid();
			this.objSid = decoder.ReadFixedStruct<NT4SID>(NdrAlignment._1Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.objSid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<NT4SID>(ref this.objSid);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDENTRYREPLY_V2 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pErrorObject;
		public uint errCode;
		public uint dsid;
		public uint extendedErr;
		public uint extendedData;
		public ushort problem;
		public uint cObjectsAdded;
		public RpcPointer<ADDENTRY_REPLY_INFO[]> infoList;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pErrorObject);
			encoder.WriteValue(this.errCode);
			encoder.WriteValue(this.dsid);
			encoder.WriteValue(this.extendedErr);
			encoder.WriteValue(this.extendedData);
			encoder.WriteValue(this.problem);
			encoder.WriteValue(this.cObjectsAdded);
			encoder.WriteUniquePointer(this.infoList);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pErrorObject = decoder.ReadUniquePointer<DSNAME>();
			this.errCode = decoder.ReadUInt32();
			this.dsid = decoder.ReadUInt32();
			this.extendedErr = decoder.ReadUInt32();
			this.extendedData = decoder.ReadUInt32();
			this.problem = decoder.ReadUInt16();
			this.cObjectsAdded = decoder.ReadUInt32();
			this.infoList = decoder.ReadUniquePointer<ADDENTRY_REPLY_INFO[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pErrorObject is not null)
			{
				encoder.WriteConformantStruct(this.pErrorObject.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pErrorObject.value);
			}

			if (this.infoList is not null)
			{
				encoder.WriteArrayHeader(this.infoList.value);
				for (int i = 0; i < this.infoList.value.Length; i++)
				{
					ADDENTRY_REPLY_INFO elem_0 = this.infoList.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.infoList.value.Length; i++)
				{
					ADDENTRY_REPLY_INFO elem_0 = this.infoList.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pErrorObject is not null)
			{
				this.pErrorObject.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pErrorObject.value);
			}

			if (this.infoList is not null)
			{
				this.infoList.value = decoder.ReadArrayHeader<ADDENTRY_REPLY_INFO>();
				for (int i = 0; i < this.infoList.value.Length; i++)
				{
					ADDENTRY_REPLY_INFO elem_0 = this.infoList.value[i];
					elem_0 = decoder.ReadFixedStruct<ADDENTRY_REPLY_INFO>(NdrAlignment._4Byte);
					this.infoList.value[i] = elem_0;
				}

				for (int i = 0; i < this.infoList.value.Length; i++)
				{
					ADDENTRY_REPLY_INFO elem_0 = this.infoList.value[i];
					decoder.ReadStructDeferral<ADDENTRY_REPLY_INFO>(ref elem_0);
					this.infoList.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_ERROR_DATA_V1 : IRpcFixedStruct
	{
		public uint dwRepError;
		public uint errCode;
		public RpcPointer<DIRERR_DRS_WIRE_V1> pErrInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwRepError);
			encoder.WriteValue(this.errCode);
			encoder.WriteUniquePointer(this.pErrInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwRepError = decoder.ReadUInt32();
			this.errCode = decoder.ReadUInt32();
			this.pErrInfo = decoder.ReadUniquePointer<DIRERR_DRS_WIRE_V1>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pErrInfo is not null)
			{
				encoder.WriteUnion(this.pErrInfo.value);
				encoder.WriteStructDeferral(this.pErrInfo.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pErrInfo is not null)
			{
				this.pErrInfo.value = decoder.ReadUnion<DIRERR_DRS_WIRE_V1>();
				decoder.ReadStructDeferral<DIRERR_DRS_WIRE_V1>(ref this.pErrInfo.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_ERROR_DATA : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_ERROR_DATA_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_ERROR_DATA_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_ERROR_DATA_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDENTRYREPLY_V3 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pdsErrObject;
		public uint dwErrVer;
		public RpcPointer<DRS_ERROR_DATA> pErrData;
		public uint cObjectsAdded;
		public RpcPointer<ADDENTRY_REPLY_INFO[]> infoList;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pdsErrObject);
			encoder.WriteValue(this.dwErrVer);
			encoder.WriteUniquePointer(this.pErrData);
			encoder.WriteValue(this.cObjectsAdded);
			encoder.WriteUniquePointer(this.infoList);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pdsErrObject = decoder.ReadUniquePointer<DSNAME>();
			this.dwErrVer = decoder.ReadUInt32();
			this.pErrData = decoder.ReadUniquePointer<DRS_ERROR_DATA>();
			this.cObjectsAdded = decoder.ReadUInt32();
			this.infoList = decoder.ReadUniquePointer<ADDENTRY_REPLY_INFO[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pdsErrObject is not null)
			{
				encoder.WriteConformantStruct(this.pdsErrObject.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pdsErrObject.value);
			}

			if (this.pErrData is not null)
			{
				encoder.WriteUnion(this.pErrData.value);
				encoder.WriteStructDeferral(this.pErrData.value);
			}

			if (this.infoList is not null)
			{
				encoder.WriteArrayHeader(this.infoList.value);
				for (int i = 0; i < this.infoList.value.Length; i++)
				{
					ADDENTRY_REPLY_INFO elem_0 = this.infoList.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.infoList.value.Length; i++)
				{
					ADDENTRY_REPLY_INFO elem_0 = this.infoList.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pdsErrObject is not null)
			{
				this.pdsErrObject.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pdsErrObject.value);
			}

			if (this.pErrData is not null)
			{
				this.pErrData.value = decoder.ReadUnion<DRS_ERROR_DATA>();
				decoder.ReadStructDeferral<DRS_ERROR_DATA>(ref this.pErrData.value);
			}

			if (this.infoList is not null)
			{
				this.infoList.value = decoder.ReadArrayHeader<ADDENTRY_REPLY_INFO>();
				for (int i = 0; i < this.infoList.value.Length; i++)
				{
					ADDENTRY_REPLY_INFO elem_0 = this.infoList.value[i];
					elem_0 = decoder.ReadFixedStruct<ADDENTRY_REPLY_INFO>(NdrAlignment._4Byte);
					this.infoList.value[i] = elem_0;
				}

				for (int i = 0; i < this.infoList.value.Length; i++)
				{
					ADDENTRY_REPLY_INFO elem_0 = this.infoList.value[i];
					decoder.ReadStructDeferral<ADDENTRY_REPLY_INFO>(ref elem_0);
					this.infoList.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDENTRYREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_ADDENTRYREPLY_V1 V1;
		public DRS_MSG_ADDENTRYREPLY_V2 V2;
		public DRS_MSG_ADDENTRYREPLY_V3 V3;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment._4Byte);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.V2, NdrAlignment.NativePtr);
					break;
				case 3U:
					encoder.WriteFixedStruct(this.V3, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_ADDENTRYREPLY_V1>(NdrAlignment._4Byte);
					break;
				case 2U:
					this.V2 = decoder.ReadFixedStruct<DRS_MSG_ADDENTRYREPLY_V2>(NdrAlignment.NativePtr);
					break;
				case 3U:
					this.V3 = decoder.ReadFixedStruct<DRS_MSG_ADDENTRYREPLY_V3>(NdrAlignment.NativePtr);
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
				case 2U:
					encoder.WriteStructDeferral(this.V2);
					break;
				case 3U:
					encoder.WriteStructDeferral(this.V3);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					decoder.ReadStructDeferral<DRS_MSG_ADDENTRYREPLY_V1>(ref this.V1);
					break;
				case 2U:
					decoder.ReadStructDeferral<DRS_MSG_ADDENTRYREPLY_V2>(ref this.V2);
					break;
				case 3U:
					decoder.ReadStructDeferral<DRS_MSG_ADDENTRYREPLY_V3>(ref this.V3);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_KCC_EXECUTE_V1 : IRpcFixedStruct
	{
		public uint dwTaskID;
		public uint dwFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwTaskID);
			encoder.WriteValue(this.dwFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwTaskID = decoder.ReadUInt32();
			this.dwFlags = decoder.ReadUInt32();
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
	public partial struct DRS_MSG_KCC_EXECUTE : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_KCC_EXECUTE_V1 V1;
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
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_KCC_EXECUTE_V1>(NdrAlignment._4Byte);
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
					decoder.ReadStructDeferral<DRS_MSG_KCC_EXECUTE_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_CLIENT_CONTEXT : IRpcFixedStruct
	{
		public ulong hCtx;
		public int lReferenceCount;
		public int fIsBound;
		public Guid uuidClient;
		public long timeLastUsed;
		public uint IPAddr;
		public int pid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.hCtx);
			encoder.WriteValue(this.lReferenceCount);
			encoder.WriteValue(this.fIsBound);
			encoder.WriteValue(this.uuidClient);
			encoder.WriteValue(this.timeLastUsed);
			encoder.WriteValue(this.IPAddr);
			encoder.WriteValue(this.pid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.hCtx = decoder.ReadUInt64();
			this.lReferenceCount = decoder.ReadInt32();
			this.fIsBound = decoder.ReadInt32();
			this.uuidClient = decoder.ReadUuid();
			this.timeLastUsed = decoder.ReadInt64();
			this.IPAddr = decoder.ReadUInt32();
			this.pid = decoder.ReadInt32();
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
	public partial struct DS_REPL_CLIENT_CONTEXTS : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgContext);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgContext = decoder.ReadArrayHeader<DS_REPL_CLIENT_CONTEXT>();
		}

		public uint cNumContexts;
		public uint dwReserved;
		public DS_REPL_CLIENT_CONTEXT[] rgContext;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgContext.Length; i++)
			{
				DS_REPL_CLIENT_CONTEXT elem_0 = this.rgContext[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgContext.Length; i++)
			{
				DS_REPL_CLIENT_CONTEXT elem_0 = this.rgContext[i];
				elem_0 = decoder.ReadFixedStruct<DS_REPL_CLIENT_CONTEXT>(NdrAlignment._8Byte);
				this.rgContext[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cNumContexts);
			encoder.WriteValue(this.dwReserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cNumContexts = decoder.ReadUInt32();
			this.dwReserved = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgContext.Length; i++)
			{
				DS_REPL_CLIENT_CONTEXT elem_0 = this.rgContext[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgContext.Length; i++)
			{
				DS_REPL_CLIENT_CONTEXT elem_0 = this.rgContext[i];
				decoder.ReadStructDeferral<DS_REPL_CLIENT_CONTEXT>(ref elem_0);
				this.rgContext[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_SERVER_OUTGOING_CALL : IRpcFixedStruct
	{
		public RpcPointer<string> pszServerName;
		public int fIsHandleBound;
		public int fIsHandleFromCache;
		public int fIsHandleInCache;
		public uint dwThreadId;
		public uint dwBindingTimeoutMins;
		public long dstimeCreated;
		public uint dwCallType;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pszServerName);
			encoder.WriteValue(this.fIsHandleBound);
			encoder.WriteValue(this.fIsHandleFromCache);
			encoder.WriteValue(this.fIsHandleInCache);
			encoder.WriteValue(this.dwThreadId);
			encoder.WriteValue(this.dwBindingTimeoutMins);
			encoder.WriteValue(this.dstimeCreated);
			encoder.WriteValue(this.dwCallType);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pszServerName = decoder.ReadUniquePointer<string>();
			this.fIsHandleBound = decoder.ReadInt32();
			this.fIsHandleFromCache = decoder.ReadInt32();
			this.fIsHandleInCache = decoder.ReadInt32();
			this.dwThreadId = decoder.ReadUInt32();
			this.dwBindingTimeoutMins = decoder.ReadUInt32();
			this.dstimeCreated = decoder.ReadInt64();
			this.dwCallType = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszServerName is not null)
			{
				encoder.WriteWideCharString(this.pszServerName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszServerName is not null)
			{
				this.pszServerName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DS_REPL_SERVER_OUTGOING_CALLS : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgCall);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgCall = decoder.ReadArrayHeader<DS_REPL_SERVER_OUTGOING_CALL>();
		}

		public uint cNumCalls;
		public uint dwReserved;
		public DS_REPL_SERVER_OUTGOING_CALL[] rgCall;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgCall.Length; i++)
			{
				DS_REPL_SERVER_OUTGOING_CALL elem_0 = this.rgCall[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgCall.Length; i++)
			{
				DS_REPL_SERVER_OUTGOING_CALL elem_0 = this.rgCall[i];
				elem_0 = decoder.ReadFixedStruct<DS_REPL_SERVER_OUTGOING_CALL>(NdrAlignment._8Byte);
				this.rgCall[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cNumCalls);
			encoder.WriteValue(this.dwReserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cNumCalls = decoder.ReadUInt32();
			this.dwReserved = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgCall.Length; i++)
			{
				DS_REPL_SERVER_OUTGOING_CALL elem_0 = this.rgCall[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgCall.Length; i++)
			{
				DS_REPL_SERVER_OUTGOING_CALL elem_0 = this.rgCall[i];
				decoder.ReadStructDeferral<DS_REPL_SERVER_OUTGOING_CALL>(ref elem_0);
				this.rgCall[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETREPLINFO_REQ_V1 : IRpcFixedStruct
	{
		public uint InfoType;
		public RpcPointer<string> pszObjectDN;
		public Guid uuidSourceDsaObjGuid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.InfoType);
			encoder.WriteUniquePointer(this.pszObjectDN);
			encoder.WriteValue(this.uuidSourceDsaObjGuid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.InfoType = decoder.ReadUInt32();
			this.pszObjectDN = decoder.ReadUniquePointer<string>();
			this.uuidSourceDsaObjGuid = decoder.ReadUuid();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszObjectDN is not null)
			{
				encoder.WriteWideCharString(this.pszObjectDN.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszObjectDN is not null)
			{
				this.pszObjectDN.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETREPLINFO_REQ_V2 : IRpcFixedStruct
	{
		public uint InfoType;
		public RpcPointer<string> pszObjectDN;
		public Guid uuidSourceDsaObjGuid;
		public uint ulFlags;
		public RpcPointer<string> pszAttributeName;
		public RpcPointer<string> pszValueDN;
		public uint dwEnumerationContext;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.InfoType);
			encoder.WriteUniquePointer(this.pszObjectDN);
			encoder.WriteValue(this.uuidSourceDsaObjGuid);
			encoder.WriteValue(this.ulFlags);
			encoder.WriteUniquePointer(this.pszAttributeName);
			encoder.WriteUniquePointer(this.pszValueDN);
			encoder.WriteValue(this.dwEnumerationContext);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.InfoType = decoder.ReadUInt32();
			this.pszObjectDN = decoder.ReadUniquePointer<string>();
			this.uuidSourceDsaObjGuid = decoder.ReadUuid();
			this.ulFlags = decoder.ReadUInt32();
			this.pszAttributeName = decoder.ReadUniquePointer<string>();
			this.pszValueDN = decoder.ReadUniquePointer<string>();
			this.dwEnumerationContext = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszObjectDN is not null)
			{
				encoder.WriteWideCharString(this.pszObjectDN.value);
			}

			if (this.pszAttributeName is not null)
			{
				encoder.WriteWideCharString(this.pszAttributeName.value);
			}

			if (this.pszValueDN is not null)
			{
				encoder.WriteWideCharString(this.pszValueDN.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszObjectDN is not null)
			{
				this.pszObjectDN.value = decoder.ReadWideCharString();
			}

			if (this.pszAttributeName is not null)
			{
				this.pszAttributeName.value = decoder.ReadWideCharString();
			}

			if (this.pszValueDN is not null)
			{
				this.pszValueDN.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETREPLINFO_REQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_GETREPLINFO_REQ_V1 V1;
		public DRS_MSG_GETREPLINFO_REQ_V2 V2;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.V2, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_GETREPLINFO_REQ_V1>(NdrAlignment.NativePtr);
					break;
				case 2U:
					this.V2 = decoder.ReadFixedStruct<DRS_MSG_GETREPLINFO_REQ_V2>(NdrAlignment.NativePtr);
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
				case 2U:
					encoder.WriteStructDeferral(this.V2);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					decoder.ReadStructDeferral<DRS_MSG_GETREPLINFO_REQ_V1>(ref this.V1);
					break;
				case 2U:
					decoder.ReadStructDeferral<DRS_MSG_GETREPLINFO_REQ_V2>(ref this.V2);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETREPLINFO_REPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public RpcPointer<DS_REPL_NEIGHBORSW> pNeighbors;
		public RpcPointer<DS_REPL_CURSORS> pCursors;
		public RpcPointer<DS_REPL_OBJ_META_DATA> pObjMetaData;
		public RpcPointer<DS_REPL_KCC_DSA_FAILURESW> pConnectFailures;
		public RpcPointer<DS_REPL_KCC_DSA_FAILURESW> pLinkFailures;
		public RpcPointer<DS_REPL_PENDING_OPSW> pPendingOps;
		public RpcPointer<DS_REPL_ATTR_VALUE_META_DATA> pAttrValueMetaData;
		public RpcPointer<DS_REPL_CURSORS_2> pCursors2;
		public RpcPointer<DS_REPL_CURSORS_3W> pCursors3;
		public RpcPointer<DS_REPL_OBJ_META_DATA_2> pObjMetaData2;
		public RpcPointer<DS_REPL_ATTR_VALUE_META_DATA_2> pAttrValueMetaData2;
		public RpcPointer<DS_REPL_SERVER_OUTGOING_CALLS> pServerOutgoingCalls;
		public RpcPointer<UPTODATE_VECTOR_V1_EXT> pUpToDateVec;
		public RpcPointer<DS_REPL_CLIENT_CONTEXTS> pClientContexts;
		public RpcPointer<DS_REPL_NEIGHBORSW> pRepsTo;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					encoder.WriteUniquePointer(this.pNeighbors);
					break;
				case 1U:
					encoder.WriteUniquePointer(this.pCursors);
					break;
				case 2U:
					encoder.WriteUniquePointer(this.pObjMetaData);
					break;
				case 3U:
					encoder.WriteUniquePointer(this.pConnectFailures);
					break;
				case 4U:
					encoder.WriteUniquePointer(this.pLinkFailures);
					break;
				case 5U:
					encoder.WriteUniquePointer(this.pPendingOps);
					break;
				case 6U:
					encoder.WriteUniquePointer(this.pAttrValueMetaData);
					break;
				case 7U:
					encoder.WriteUniquePointer(this.pCursors2);
					break;
				case 8U:
					encoder.WriteUniquePointer(this.pCursors3);
					break;
				case 9U:
					encoder.WriteUniquePointer(this.pObjMetaData2);
					break;
				case 10U:
					encoder.WriteUniquePointer(this.pAttrValueMetaData2);
					break;
				case 0xFFFFFFFA:
					encoder.WriteUniquePointer(this.pServerOutgoingCalls);
					break;
				case 0xFFFFFFFB:
					encoder.WriteUniquePointer(this.pUpToDateVec);
					break;
				case 0xFFFFFFFC:
					encoder.WriteUniquePointer(this.pClientContexts);
					break;
				case 0xFFFFFFFE:
					encoder.WriteUniquePointer(this.pRepsTo);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					this.pNeighbors = decoder.ReadUniquePointer<DS_REPL_NEIGHBORSW>();
					break;
				case 1U:
					this.pCursors = decoder.ReadUniquePointer<DS_REPL_CURSORS>();
					break;
				case 2U:
					this.pObjMetaData = decoder.ReadUniquePointer<DS_REPL_OBJ_META_DATA>();
					break;
				case 3U:
					this.pConnectFailures = decoder.ReadUniquePointer<DS_REPL_KCC_DSA_FAILURESW>();
					break;
				case 4U:
					this.pLinkFailures = decoder.ReadUniquePointer<DS_REPL_KCC_DSA_FAILURESW>();
					break;
				case 5U:
					this.pPendingOps = decoder.ReadUniquePointer<DS_REPL_PENDING_OPSW>();
					break;
				case 6U:
					this.pAttrValueMetaData = decoder.ReadUniquePointer<DS_REPL_ATTR_VALUE_META_DATA>();
					break;
				case 7U:
					this.pCursors2 = decoder.ReadUniquePointer<DS_REPL_CURSORS_2>();
					break;
				case 8U:
					this.pCursors3 = decoder.ReadUniquePointer<DS_REPL_CURSORS_3W>();
					break;
				case 9U:
					this.pObjMetaData2 = decoder.ReadUniquePointer<DS_REPL_OBJ_META_DATA_2>();
					break;
				case 10U:
					this.pAttrValueMetaData2 = decoder.ReadUniquePointer<DS_REPL_ATTR_VALUE_META_DATA_2>();
					break;
				case 0xFFFFFFFA:
					this.pServerOutgoingCalls = decoder.ReadUniquePointer<DS_REPL_SERVER_OUTGOING_CALLS>();
					break;
				case 0xFFFFFFFB:
					this.pUpToDateVec = decoder.ReadUniquePointer<UPTODATE_VECTOR_V1_EXT>();
					break;
				case 0xFFFFFFFC:
					this.pClientContexts = decoder.ReadUniquePointer<DS_REPL_CLIENT_CONTEXTS>();
					break;
				case 0xFFFFFFFE:
					this.pRepsTo = decoder.ReadUniquePointer<DS_REPL_NEIGHBORSW>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					if (this.pNeighbors is not null)
					{
						encoder.WriteConformantStruct(this.pNeighbors.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pNeighbors.value);
					}

					break;
				case 1U:
					if (this.pCursors is not null)
					{
						encoder.WriteConformantStruct(this.pCursors.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pCursors.value);
					}

					break;
				case 2U:
					if (this.pObjMetaData is not null)
					{
						encoder.WriteConformantStruct(this.pObjMetaData.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pObjMetaData.value);
					}

					break;
				case 3U:
					if (this.pConnectFailures is not null)
					{
						encoder.WriteConformantStruct(this.pConnectFailures.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.pConnectFailures.value);
					}

					break;
				case 4U:
					if (this.pLinkFailures is not null)
					{
						encoder.WriteConformantStruct(this.pLinkFailures.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.pLinkFailures.value);
					}

					break;
				case 5U:
					if (this.pPendingOps is not null)
					{
						encoder.WriteConformantStruct(this.pPendingOps.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.pPendingOps.value);
					}

					break;
				case 6U:
					if (this.pAttrValueMetaData is not null)
					{
						encoder.WriteConformantStruct(this.pAttrValueMetaData.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pAttrValueMetaData.value);
					}

					break;
				case 7U:
					if (this.pCursors2 is not null)
					{
						encoder.WriteConformantStruct(this.pCursors2.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pCursors2.value);
					}

					break;
				case 8U:
					if (this.pCursors3 is not null)
					{
						encoder.WriteConformantStruct(this.pCursors3.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pCursors3.value);
					}

					break;
				case 9U:
					if (this.pObjMetaData2 is not null)
					{
						encoder.WriteConformantStruct(this.pObjMetaData2.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pObjMetaData2.value);
					}

					break;
				case 10U:
					if (this.pAttrValueMetaData2 is not null)
					{
						encoder.WriteConformantStruct(this.pAttrValueMetaData2.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pAttrValueMetaData2.value);
					}

					break;
				case 0xFFFFFFFA:
					if (this.pServerOutgoingCalls is not null)
					{
						encoder.WriteConformantStruct(this.pServerOutgoingCalls.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pServerOutgoingCalls.value);
					}

					break;
				case 0xFFFFFFFB:
					if (this.pUpToDateVec is not null)
					{
						encoder.WriteConformantStruct(this.pUpToDateVec.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pUpToDateVec.value);
					}

					break;
				case 0xFFFFFFFC:
					if (this.pClientContexts is not null)
					{
						encoder.WriteConformantStruct(this.pClientContexts.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pClientContexts.value);
					}

					break;
				case 0xFFFFFFFE:
					if (this.pRepsTo is not null)
					{
						encoder.WriteConformantStruct(this.pRepsTo.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pRepsTo.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					if (this.pNeighbors is not null)
					{
						this.pNeighbors.value = decoder.ReadConformantStruct<DS_REPL_NEIGHBORSW>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<DS_REPL_NEIGHBORSW>(ref this.pNeighbors.value);
					}

					break;
				case 1U:
					if (this.pCursors is not null)
					{
						this.pCursors.value = decoder.ReadConformantStruct<DS_REPL_CURSORS>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<DS_REPL_CURSORS>(ref this.pCursors.value);
					}

					break;
				case 2U:
					if (this.pObjMetaData is not null)
					{
						this.pObjMetaData.value = decoder.ReadConformantStruct<DS_REPL_OBJ_META_DATA>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<DS_REPL_OBJ_META_DATA>(ref this.pObjMetaData.value);
					}

					break;
				case 3U:
					if (this.pConnectFailures is not null)
					{
						this.pConnectFailures.value = decoder.ReadConformantStruct<DS_REPL_KCC_DSA_FAILURESW>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<DS_REPL_KCC_DSA_FAILURESW>(ref this.pConnectFailures.value);
					}

					break;
				case 4U:
					if (this.pLinkFailures is not null)
					{
						this.pLinkFailures.value = decoder.ReadConformantStruct<DS_REPL_KCC_DSA_FAILURESW>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<DS_REPL_KCC_DSA_FAILURESW>(ref this.pLinkFailures.value);
					}

					break;
				case 5U:
					if (this.pPendingOps is not null)
					{
						this.pPendingOps.value = decoder.ReadConformantStruct<DS_REPL_PENDING_OPSW>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<DS_REPL_PENDING_OPSW>(ref this.pPendingOps.value);
					}

					break;
				case 6U:
					if (this.pAttrValueMetaData is not null)
					{
						this.pAttrValueMetaData.value = decoder.ReadConformantStruct<DS_REPL_ATTR_VALUE_META_DATA>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<DS_REPL_ATTR_VALUE_META_DATA>(ref this.pAttrValueMetaData.value);
					}

					break;
				case 7U:
					if (this.pCursors2 is not null)
					{
						this.pCursors2.value = decoder.ReadConformantStruct<DS_REPL_CURSORS_2>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<DS_REPL_CURSORS_2>(ref this.pCursors2.value);
					}

					break;
				case 8U:
					if (this.pCursors3 is not null)
					{
						this.pCursors3.value = decoder.ReadConformantStruct<DS_REPL_CURSORS_3W>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<DS_REPL_CURSORS_3W>(ref this.pCursors3.value);
					}

					break;
				case 9U:
					if (this.pObjMetaData2 is not null)
					{
						this.pObjMetaData2.value = decoder.ReadConformantStruct<DS_REPL_OBJ_META_DATA_2>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<DS_REPL_OBJ_META_DATA_2>(ref this.pObjMetaData2.value);
					}

					break;
				case 10U:
					if (this.pAttrValueMetaData2 is not null)
					{
						this.pAttrValueMetaData2.value = decoder.ReadConformantStruct<DS_REPL_ATTR_VALUE_META_DATA_2>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<DS_REPL_ATTR_VALUE_META_DATA_2>(ref this.pAttrValueMetaData2.value);
					}

					break;
				case 0xFFFFFFFA:
					if (this.pServerOutgoingCalls is not null)
					{
						this.pServerOutgoingCalls.value = decoder.ReadConformantStruct<DS_REPL_SERVER_OUTGOING_CALLS>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<DS_REPL_SERVER_OUTGOING_CALLS>(ref this.pServerOutgoingCalls.value);
					}

					break;
				case 0xFFFFFFFB:
					if (this.pUpToDateVec is not null)
					{
						this.pUpToDateVec.value = decoder.ReadConformantStruct<UPTODATE_VECTOR_V1_EXT>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<UPTODATE_VECTOR_V1_EXT>(ref this.pUpToDateVec.value);
					}

					break;
				case 0xFFFFFFFC:
					if (this.pClientContexts is not null)
					{
						this.pClientContexts.value = decoder.ReadConformantStruct<DS_REPL_CLIENT_CONTEXTS>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<DS_REPL_CLIENT_CONTEXTS>(ref this.pClientContexts.value);
					}

					break;
				case 0xFFFFFFFE:
					if (this.pRepsTo is not null)
					{
						this.pRepsTo.value = decoder.ReadConformantStruct<DS_REPL_NEIGHBORSW>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<DS_REPL_NEIGHBORSW>(ref this.pRepsTo.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDSIDREQ_V1 : IRpcFixedStruct
	{
		public uint Flags;
		public RpcPointer<string> SrcDomain;
		public RpcPointer<string> SrcPrincipal;
		public RpcPointer<string> SrcDomainController;
		public uint SrcCredsUserLength;
		public RpcPointer<char[]> SrcCredsUser;
		public uint SrcCredsDomainLength;
		public RpcPointer<char[]> SrcCredsDomain;
		public uint SrcCredsPasswordLength;
		public RpcPointer<char[]> SrcCredsPassword;
		public RpcPointer<string> DstDomain;
		public RpcPointer<string> DstPrincipal;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Flags);
			encoder.WriteUniquePointer(this.SrcDomain);
			encoder.WriteUniquePointer(this.SrcPrincipal);
			encoder.WriteFullPointer(this.SrcDomainController);
			encoder.WriteValue(this.SrcCredsUserLength);
			encoder.WriteUniquePointer(this.SrcCredsUser);
			encoder.WriteValue(this.SrcCredsDomainLength);
			encoder.WriteUniquePointer(this.SrcCredsDomain);
			encoder.WriteValue(this.SrcCredsPasswordLength);
			encoder.WriteUniquePointer(this.SrcCredsPassword);
			encoder.WriteUniquePointer(this.DstDomain);
			encoder.WriteUniquePointer(this.DstPrincipal);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Flags = decoder.ReadUInt32();
			this.SrcDomain = decoder.ReadUniquePointer<string>();
			this.SrcPrincipal = decoder.ReadUniquePointer<string>();
			this.SrcDomainController = decoder.ReadFullPointer<string>();
			this.SrcCredsUserLength = decoder.ReadUInt32();
			this.SrcCredsUser = decoder.ReadUniquePointer<char[]>();
			this.SrcCredsDomainLength = decoder.ReadUInt32();
			this.SrcCredsDomain = decoder.ReadUniquePointer<char[]>();
			this.SrcCredsPasswordLength = decoder.ReadUInt32();
			this.SrcCredsPassword = decoder.ReadUniquePointer<char[]>();
			this.DstDomain = decoder.ReadUniquePointer<string>();
			this.DstPrincipal = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.SrcDomain is not null)
			{
				encoder.WriteWideCharString(this.SrcDomain.value);
			}

			if (this.SrcPrincipal is not null)
			{
				encoder.WriteWideCharString(this.SrcPrincipal.value);
			}

			if (this.SrcDomainController is not null)
			{
				encoder.WriteWideCharString(this.SrcDomainController.value);
			}

			if (this.SrcCredsUser is not null)
			{
				encoder.WriteArrayHeader(this.SrcCredsUser.value);
				for (int i = 0; i < this.SrcCredsUser.value.Length; i++)
				{
					char elem_0 = this.SrcCredsUser.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.SrcCredsDomain is not null)
			{
				encoder.WriteArrayHeader(this.SrcCredsDomain.value);
				for (int i = 0; i < this.SrcCredsDomain.value.Length; i++)
				{
					char elem_0 = this.SrcCredsDomain.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.SrcCredsPassword is not null)
			{
				encoder.WriteArrayHeader(this.SrcCredsPassword.value);
				for (int i = 0; i < this.SrcCredsPassword.value.Length; i++)
				{
					char elem_0 = this.SrcCredsPassword.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.DstDomain is not null)
			{
				encoder.WriteWideCharString(this.DstDomain.value);
			}

			if (this.DstPrincipal is not null)
			{
				encoder.WriteWideCharString(this.DstPrincipal.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.SrcDomain is not null)
			{
				this.SrcDomain.value = decoder.ReadWideCharString();
			}

			if (this.SrcPrincipal is not null)
			{
				this.SrcPrincipal.value = decoder.ReadWideCharString();
			}

			if (this.SrcDomainController is not null)
			{
				this.SrcDomainController.value = decoder.ReadWideCharString();
			}

			if (this.SrcCredsUser is not null)
			{
				this.SrcCredsUser.value = decoder.ReadArrayHeader<char>();
				for (int i = 0; i < this.SrcCredsUser.value.Length; i++)
				{
					char elem_0 = this.SrcCredsUser.value[i];
					elem_0 = decoder.ReadWideChar();
					this.SrcCredsUser.value[i] = elem_0;
				}
			}

			if (this.SrcCredsDomain is not null)
			{
				this.SrcCredsDomain.value = decoder.ReadArrayHeader<char>();
				for (int i = 0; i < this.SrcCredsDomain.value.Length; i++)
				{
					char elem_0 = this.SrcCredsDomain.value[i];
					elem_0 = decoder.ReadWideChar();
					this.SrcCredsDomain.value[i] = elem_0;
				}
			}

			if (this.SrcCredsPassword is not null)
			{
				this.SrcCredsPassword.value = decoder.ReadArrayHeader<char>();
				for (int i = 0; i < this.SrcCredsPassword.value.Length; i++)
				{
					char elem_0 = this.SrcCredsPassword.value[i];
					elem_0 = decoder.ReadWideChar();
					this.SrcCredsPassword.value[i] = elem_0;
				}
			}

			if (this.DstDomain is not null)
			{
				this.DstDomain.value = decoder.ReadWideCharString();
			}

			if (this.DstPrincipal is not null)
			{
				this.DstPrincipal.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDSIDREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_ADDSIDREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_ADDSIDREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_ADDSIDREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDSIDREPLY_V1 : IRpcFixedStruct
	{
		public uint dwWin32Error;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwWin32Error);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwWin32Error = decoder.ReadUInt32();
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
	public partial struct DRS_MSG_ADDSIDREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_ADDSIDREPLY_V1 V1;
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
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_ADDSIDREPLY_V1>(NdrAlignment._4Byte);
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
					decoder.ReadStructDeferral<DRS_MSG_ADDSIDREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETMEMBERSHIPS2_REQ_V1 : IRpcFixedStruct
	{
		public uint Count;
		public RpcPointer<DRS_MSG_REVMEMB_REQ_V1[]> Requests;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Count);
			encoder.WriteUniquePointer(this.Requests);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Count = decoder.ReadUInt32();
			this.Requests = decoder.ReadUniquePointer<DRS_MSG_REVMEMB_REQ_V1[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Requests is not null)
			{
				encoder.WriteArrayHeader(this.Requests.value);
				for (int i = 0; i < this.Requests.value.Length; i++)
				{
					DRS_MSG_REVMEMB_REQ_V1 elem_0 = this.Requests.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Requests.value.Length; i++)
				{
					DRS_MSG_REVMEMB_REQ_V1 elem_0 = this.Requests.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Requests is not null)
			{
				this.Requests.value = decoder.ReadArrayHeader<DRS_MSG_REVMEMB_REQ_V1>();
				for (int i = 0; i < this.Requests.value.Length; i++)
				{
					DRS_MSG_REVMEMB_REQ_V1 elem_0 = this.Requests.value[i];
					elem_0 = decoder.ReadFixedStruct<DRS_MSG_REVMEMB_REQ_V1>(NdrAlignment.NativePtr);
					this.Requests.value[i] = elem_0;
				}

				for (int i = 0; i < this.Requests.value.Length; i++)
				{
					DRS_MSG_REVMEMB_REQ_V1 elem_0 = this.Requests.value[i];
					decoder.ReadStructDeferral<DRS_MSG_REVMEMB_REQ_V1>(ref elem_0);
					this.Requests.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETMEMBERSHIPS2_REQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_GETMEMBERSHIPS2_REQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_GETMEMBERSHIPS2_REQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_GETMEMBERSHIPS2_REQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETMEMBERSHIPS2_REPLY_V1 : IRpcFixedStruct
	{
		public uint Count;
		public RpcPointer<DRS_MSG_REVMEMB_REPLY_V1[]> Replies;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Count);
			encoder.WriteUniquePointer(this.Replies);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Count = decoder.ReadUInt32();
			this.Replies = decoder.ReadUniquePointer<DRS_MSG_REVMEMB_REPLY_V1[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Replies is not null)
			{
				encoder.WriteArrayHeader(this.Replies.value);
				for (int i = 0; i < this.Replies.value.Length; i++)
				{
					DRS_MSG_REVMEMB_REPLY_V1 elem_0 = this.Replies.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Replies.value.Length; i++)
				{
					DRS_MSG_REVMEMB_REPLY_V1 elem_0 = this.Replies.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Replies is not null)
			{
				this.Replies.value = decoder.ReadArrayHeader<DRS_MSG_REVMEMB_REPLY_V1>();
				for (int i = 0; i < this.Replies.value.Length; i++)
				{
					DRS_MSG_REVMEMB_REPLY_V1 elem_0 = this.Replies.value[i];
					elem_0 = decoder.ReadFixedStruct<DRS_MSG_REVMEMB_REPLY_V1>(NdrAlignment.NativePtr);
					this.Replies.value[i] = elem_0;
				}

				for (int i = 0; i < this.Replies.value.Length; i++)
				{
					DRS_MSG_REVMEMB_REPLY_V1 elem_0 = this.Replies.value[i];
					decoder.ReadStructDeferral<DRS_MSG_REVMEMB_REPLY_V1>(ref elem_0);
					this.Replies.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_GETMEMBERSHIPS2_REPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_GETMEMBERSHIPS2_REPLY_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_GETMEMBERSHIPS2_REPLY_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_GETMEMBERSHIPS2_REPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPVERIFYOBJ_V1 : IRpcFixedStruct
	{
		public RpcPointer<DSNAME> pNC;
		public Guid uuidDsaSrc;
		public uint ulOptions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteValue(this.uuidDsaSrc);
			encoder.WriteValue(this.ulOptions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.uuidDsaSrc = decoder.ReadUuid();
			this.ulOptions = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPVERIFYOBJ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_REPVERIFYOBJ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_REPVERIFYOBJ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_REPVERIFYOBJ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_EXISTREQ_V1 : IRpcFixedStruct
	{
		public Guid guidStart;
		public uint cGuids;
		public RpcPointer<DSNAME> pNC;
		public RpcPointer<UPTODATE_VECTOR_V1_EXT> pUpToDateVecCommonV1;
		public byte[] Md5Digest;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.guidStart);
			encoder.WriteValue(this.cGuids);
			encoder.WriteUniquePointer(this.pNC);
			encoder.WriteUniquePointer(this.pUpToDateVecCommonV1);
			if (this.Md5Digest == null)
				this.Md5Digest = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.Md5Digest[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.guidStart = decoder.ReadUuid();
			this.cGuids = decoder.ReadUInt32();
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
			this.pUpToDateVecCommonV1 = decoder.ReadUniquePointer<UPTODATE_VECTOR_V1_EXT>();
			if (this.Md5Digest == null)
				this.Md5Digest = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.Md5Digest[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.Md5Digest[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}

			if (this.pUpToDateVecCommonV1 is not null)
			{
				encoder.WriteConformantStruct(this.pUpToDateVecCommonV1.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.pUpToDateVecCommonV1.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}

			if (this.pUpToDateVecCommonV1 is not null)
			{
				this.pUpToDateVecCommonV1.value = decoder.ReadConformantStruct<UPTODATE_VECTOR_V1_EXT>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<UPTODATE_VECTOR_V1_EXT>(ref this.pUpToDateVecCommonV1.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_EXISTREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_EXISTREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_EXISTREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_EXISTREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_EXISTREPLY_V1 : IRpcFixedStruct
	{
		public uint dwStatusFlags;
		public uint cNumGuids;
		public RpcPointer<Guid[]> rgGuids;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwStatusFlags);
			encoder.WriteValue(this.cNumGuids);
			encoder.WriteUniquePointer(this.rgGuids);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwStatusFlags = decoder.ReadUInt32();
			this.cNumGuids = decoder.ReadUInt32();
			this.rgGuids = decoder.ReadUniquePointer<Guid[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.rgGuids is not null)
			{
				encoder.WriteArrayHeader(this.rgGuids.value);
				for (int i = 0; i < this.rgGuids.value.Length; i++)
				{
					Guid elem_0 = this.rgGuids.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.rgGuids is not null)
			{
				this.rgGuids.value = decoder.ReadArrayHeader<Guid>();
				for (int i = 0; i < this.rgGuids.value.Length; i++)
				{
					Guid elem_0 = this.rgGuids.value[i];
					elem_0 = decoder.ReadUuid();
					this.rgGuids.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_EXISTREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_EXISTREPLY_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_EXISTREPLY_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_EXISTREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_QUERYSITESREQ_V1 : IRpcFixedStruct
	{
		public RpcPointer<string> pwszFromSite;
		public uint cToSites;
		public RpcPointer<RpcPointer<string>[]> rgszToSites;
		public uint dwFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pwszFromSite);
			encoder.WriteValue(this.cToSites);
			encoder.WriteUniquePointer(this.rgszToSites);
			encoder.WriteValue(this.dwFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pwszFromSite = decoder.ReadUniquePointer<string>();
			this.cToSites = decoder.ReadUInt32();
			this.rgszToSites = decoder.ReadUniquePointer<RpcPointer<string>[]>();
			this.dwFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pwszFromSite is not null)
			{
				encoder.WriteWideCharString(this.pwszFromSite.value);
			}

			if (this.rgszToSites is not null)
			{
				encoder.WriteArrayHeader(this.rgszToSites.value);
				for (int i = 0; i < this.rgszToSites.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.rgszToSites.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.rgszToSites.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.rgszToSites.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteWideCharString(elem_0.value);
					}
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pwszFromSite is not null)
			{
				this.pwszFromSite.value = decoder.ReadWideCharString();
			}

			if (this.rgszToSites is not null)
			{
				this.rgszToSites.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < this.rgszToSites.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.rgszToSites.value[i];
					elem_0 = decoder.ReadUniquePointer<string>();
					this.rgszToSites.value[i] = elem_0;
				}

				for (int i = 0; i < this.rgszToSites.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.rgszToSites.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadWideCharString();
					}

					this.rgszToSites.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_QUERYSITESREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_QUERYSITESREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_QUERYSITESREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_QUERYSITESREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_QUERYSITESREPLYELEMENT_V1 : IRpcFixedStruct
	{
		public uint dwErrorCode;
		public uint dwCost;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwErrorCode);
			encoder.WriteValue(this.dwCost);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwErrorCode = decoder.ReadUInt32();
			this.dwCost = decoder.ReadUInt32();
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
	public partial struct DRS_MSG_QUERYSITESREPLY_V1 : IRpcFixedStruct
	{
		public uint cToSites;
		public RpcPointer<DRS_MSG_QUERYSITESREPLYELEMENT_V1[]> rgCostInfo;
		public uint dwFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cToSites);
			encoder.WriteUniquePointer(this.rgCostInfo);
			encoder.WriteValue(this.dwFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cToSites = decoder.ReadUInt32();
			this.rgCostInfo = decoder.ReadUniquePointer<DRS_MSG_QUERYSITESREPLYELEMENT_V1[]>();
			this.dwFlags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.rgCostInfo is not null)
			{
				encoder.WriteArrayHeader(this.rgCostInfo.value);
				for (int i = 0; i < this.rgCostInfo.value.Length; i++)
				{
					DRS_MSG_QUERYSITESREPLYELEMENT_V1 elem_0 = this.rgCostInfo.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.rgCostInfo.value.Length; i++)
				{
					DRS_MSG_QUERYSITESREPLYELEMENT_V1 elem_0 = this.rgCostInfo.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.rgCostInfo is not null)
			{
				this.rgCostInfo.value = decoder.ReadArrayHeader<DRS_MSG_QUERYSITESREPLYELEMENT_V1>();
				for (int i = 0; i < this.rgCostInfo.value.Length; i++)
				{
					DRS_MSG_QUERYSITESREPLYELEMENT_V1 elem_0 = this.rgCostInfo.value[i];
					elem_0 = decoder.ReadFixedStruct<DRS_MSG_QUERYSITESREPLYELEMENT_V1>(NdrAlignment._4Byte);
					this.rgCostInfo.value[i] = elem_0;
				}

				for (int i = 0; i < this.rgCostInfo.value.Length; i++)
				{
					DRS_MSG_QUERYSITESREPLYELEMENT_V1 elem_0 = this.rgCostInfo.value[i];
					decoder.ReadStructDeferral<DRS_MSG_QUERYSITESREPLYELEMENT_V1>(ref elem_0);
					this.rgCostInfo.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_QUERYSITESREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_QUERYSITESREPLY_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_QUERYSITESREPLY_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_QUERYSITESREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_INIT_DEMOTIONREQ_V1 : IRpcFixedStruct
	{
		public uint dwReserved;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwReserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwReserved = decoder.ReadUInt32();
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
	public partial struct DRS_MSG_INIT_DEMOTIONREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_INIT_DEMOTIONREQ_V1 V1;
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
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_INIT_DEMOTIONREQ_V1>(NdrAlignment._4Byte);
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
					decoder.ReadStructDeferral<DRS_MSG_INIT_DEMOTIONREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_INIT_DEMOTIONREPLY_V1 : IRpcFixedStruct
	{
		public uint dwOpError;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwOpError);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwOpError = decoder.ReadUInt32();
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
	public partial struct DRS_MSG_INIT_DEMOTIONREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_INIT_DEMOTIONREPLY_V1 V1;
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
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_INIT_DEMOTIONREPLY_V1>(NdrAlignment._4Byte);
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
					decoder.ReadStructDeferral<DRS_MSG_INIT_DEMOTIONREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPLICA_DEMOTIONREQ_V1 : IRpcFixedStruct
	{
		public uint dwFlags;
		public Guid uuidHelperDest;
		public RpcPointer<DSNAME> pNC;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwFlags);
			encoder.WriteValue(this.uuidHelperDest);
			encoder.WriteUniquePointer(this.pNC);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwFlags = decoder.ReadUInt32();
			this.uuidHelperDest = decoder.ReadUuid();
			this.pNC = decoder.ReadUniquePointer<DSNAME>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNC is not null)
			{
				encoder.WriteConformantStruct(this.pNC.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pNC.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNC is not null)
			{
				this.pNC.value = decoder.ReadConformantStruct<DSNAME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DSNAME>(ref this.pNC.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPLICA_DEMOTIONREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_REPLICA_DEMOTIONREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_REPLICA_DEMOTIONREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_REPLICA_DEMOTIONREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_REPLICA_DEMOTIONREPLY_V1 : IRpcFixedStruct
	{
		public uint dwOpError;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwOpError);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwOpError = decoder.ReadUInt32();
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
	public partial struct DRS_MSG_REPLICA_DEMOTIONREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_REPLICA_DEMOTIONREPLY_V1 V1;
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
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_REPLICA_DEMOTIONREPLY_V1>(NdrAlignment._4Byte);
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
					decoder.ReadStructDeferral<DRS_MSG_REPLICA_DEMOTIONREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_FINISH_DEMOTIONREQ_V1 : IRpcFixedStruct
	{
		public uint dwOperations;
		public Guid uuidHelperDest;
		public RpcPointer<string> szScriptBase;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwOperations);
			encoder.WriteValue(this.uuidHelperDest);
			encoder.WriteUniquePointer(this.szScriptBase);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwOperations = decoder.ReadUInt32();
			this.uuidHelperDest = decoder.ReadUuid();
			this.szScriptBase = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.szScriptBase is not null)
			{
				encoder.WriteWideCharString(this.szScriptBase.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.szScriptBase is not null)
			{
				this.szScriptBase.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_FINISH_DEMOTIONREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_FINISH_DEMOTIONREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_FINISH_DEMOTIONREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_FINISH_DEMOTIONREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_FINISH_DEMOTIONREPLY_V1 : IRpcFixedStruct
	{
		public uint dwOperationsDone;
		public uint dwOpFailed;
		public uint dwOpError;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwOperationsDone);
			encoder.WriteValue(this.dwOpFailed);
			encoder.WriteValue(this.dwOpError);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwOperationsDone = decoder.ReadUInt32();
			this.dwOpFailed = decoder.ReadUInt32();
			this.dwOpError = decoder.ReadUInt32();
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
	public partial struct DRS_MSG_FINISH_DEMOTIONREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_FINISH_DEMOTIONREPLY_V1 V1;
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
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_FINISH_DEMOTIONREPLY_V1>(NdrAlignment._4Byte);
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
					decoder.ReadStructDeferral<DRS_MSG_FINISH_DEMOTIONREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDCLONEDCREQ_V1 : IRpcFixedStruct
	{
		public RpcPointer<string> pwszCloneDCName;
		public RpcPointer<string> pwszSite;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pwszCloneDCName);
			encoder.WriteUniquePointer(this.pwszSite);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pwszCloneDCName = decoder.ReadUniquePointer<string>();
			this.pwszSite = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pwszCloneDCName is not null)
			{
				encoder.WriteWideCharString(this.pwszCloneDCName.value);
			}

			if (this.pwszSite is not null)
			{
				encoder.WriteWideCharString(this.pwszSite.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pwszCloneDCName is not null)
			{
				this.pwszCloneDCName.value = decoder.ReadWideCharString();
			}

			if (this.pwszSite is not null)
			{
				this.pwszSite.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDCLONEDCREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_ADDCLONEDCREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_ADDCLONEDCREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_ADDCLONEDCREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDCLONEDCREPLY_V1 : IRpcFixedStruct
	{
		public RpcPointer<string> pwszCloneDCName;
		public RpcPointer<string> pwszSite;
		public uint cPasswordLength;
		public RpcPointer<char[]> pwsNewDCAccountPassword;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pwszCloneDCName);
			encoder.WriteUniquePointer(this.pwszSite);
			encoder.WriteValue(this.cPasswordLength);
			encoder.WriteUniquePointer(this.pwsNewDCAccountPassword);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pwszCloneDCName = decoder.ReadUniquePointer<string>();
			this.pwszSite = decoder.ReadUniquePointer<string>();
			this.cPasswordLength = decoder.ReadUInt32();
			this.pwsNewDCAccountPassword = decoder.ReadUniquePointer<char[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pwszCloneDCName is not null)
			{
				encoder.WriteWideCharString(this.pwszCloneDCName.value);
			}

			if (this.pwszSite is not null)
			{
				encoder.WriteWideCharString(this.pwszSite.value);
			}

			if (this.pwsNewDCAccountPassword is not null)
			{
				encoder.WriteArrayHeader(this.pwsNewDCAccountPassword.value);
				for (int i = 0; i < this.pwsNewDCAccountPassword.value.Length; i++)
				{
					char elem_0 = this.pwsNewDCAccountPassword.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pwszCloneDCName is not null)
			{
				this.pwszCloneDCName.value = decoder.ReadWideCharString();
			}

			if (this.pwszSite is not null)
			{
				this.pwszSite.value = decoder.ReadWideCharString();
			}

			if (this.pwsNewDCAccountPassword is not null)
			{
				this.pwsNewDCAccountPassword.value = decoder.ReadArrayHeader<char>();
				for (int i = 0; i < this.pwsNewDCAccountPassword.value.Length; i++)
				{
					char elem_0 = this.pwsNewDCAccountPassword.value[i];
					elem_0 = decoder.ReadWideChar();
					this.pwsNewDCAccountPassword.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_ADDCLONEDCREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_ADDCLONEDCREPLY_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_ADDCLONEDCREPLY_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_ADDCLONEDCREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_WRITENGCKEYREQ_V1 : IRpcFixedStruct
	{
		public RpcPointer<string> pwszAccount;
		public uint cNgcKey;
		public RpcPointer<byte[]> pNgcKey;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pwszAccount);
			encoder.WriteValue(this.cNgcKey);
			encoder.WriteUniquePointer(this.pNgcKey);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pwszAccount = decoder.ReadUniquePointer<string>();
			this.cNgcKey = decoder.ReadUInt32();
			this.pNgcKey = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pwszAccount is not null)
			{
				encoder.WriteWideCharString(this.pwszAccount.value);
			}

			if (this.pNgcKey is not null)
			{
				encoder.WriteArrayHeader(this.pNgcKey.value);
				for (int i = 0; i < this.pNgcKey.value.Length; i++)
				{
					byte elem_0 = this.pNgcKey.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pwszAccount is not null)
			{
				this.pwszAccount.value = decoder.ReadWideCharString();
			}

			if (this.pNgcKey is not null)
			{
				this.pNgcKey.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pNgcKey.value.Length; i++)
				{
					byte elem_0 = this.pNgcKey.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pNgcKey.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_WRITENGCKEYREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_WRITENGCKEYREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_WRITENGCKEYREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_WRITENGCKEYREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_WRITENGCKEYREPLY_V1 : IRpcFixedStruct
	{
		public uint retVal;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.retVal);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.retVal = decoder.ReadUInt32();
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
	public partial struct DRS_MSG_WRITENGCKEYREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_WRITENGCKEYREPLY_V1 V1;
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
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_WRITENGCKEYREPLY_V1>(NdrAlignment._4Byte);
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
					decoder.ReadStructDeferral<DRS_MSG_WRITENGCKEYREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_READNGCKEYREQ_V1 : IRpcFixedStruct
	{
		public RpcPointer<string> pwszAccount;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pwszAccount);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pwszAccount = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pwszAccount is not null)
			{
				encoder.WriteWideCharString(this.pwszAccount.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pwszAccount is not null)
			{
				this.pwszAccount.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_READNGCKEYREQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_READNGCKEYREQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_READNGCKEYREQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_READNGCKEYREQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_READNGCKEYREPLY_V1 : IRpcFixedStruct
	{
		public uint retVal;
		public uint cNgcKey;
		public RpcPointer<byte[]> pNgcKey;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.retVal);
			encoder.WriteValue(this.cNgcKey);
			encoder.WriteUniquePointer(this.pNgcKey);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.retVal = decoder.ReadUInt32();
			this.cNgcKey = decoder.ReadUInt32();
			this.pNgcKey = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pNgcKey is not null)
			{
				encoder.WriteArrayHeader(this.pNgcKey.value);
				for (int i = 0; i < this.pNgcKey.value.Length; i++)
				{
					byte elem_0 = this.pNgcKey.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pNgcKey is not null)
			{
				this.pNgcKey.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pNgcKey.value.Length; i++)
				{
					byte elem_0 = this.pNgcKey.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pNgcKey.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DRS_MSG_READNGCKEYREPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DRS_MSG_READNGCKEYREPLY_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DRS_MSG_READNGCKEYREPLY_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DRS_MSG_READNGCKEYREPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DSA_MSG_EXECUTE_SCRIPT_REQ_V1 : IRpcFixedStruct
	{
		public uint Flags;
		public uint cbPassword;
		public RpcPointer<byte[]> pbPassword;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Flags);
			encoder.WriteValue(this.cbPassword);
			encoder.WriteUniquePointer(this.pbPassword);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Flags = decoder.ReadUInt32();
			this.cbPassword = decoder.ReadUInt32();
			this.pbPassword = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pbPassword is not null)
			{
				encoder.WriteArrayHeader(this.pbPassword.value);
				for (int i = 0; i < this.pbPassword.value.Length; i++)
				{
					byte elem_0 = this.pbPassword.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pbPassword is not null)
			{
				this.pbPassword.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pbPassword.value.Length; i++)
				{
					byte elem_0 = this.pbPassword.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pbPassword.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DSA_MSG_EXECUTE_SCRIPT_REQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DSA_MSG_EXECUTE_SCRIPT_REQ_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DSA_MSG_EXECUTE_SCRIPT_REQ_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DSA_MSG_EXECUTE_SCRIPT_REQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DSA_MSG_EXECUTE_SCRIPT_REPLY_V1 : IRpcFixedStruct
	{
		public uint dwOperationStatus;
		public RpcPointer<string> pwErrMessage;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwOperationStatus);
			encoder.WriteUniquePointer(this.pwErrMessage);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwOperationStatus = decoder.ReadUInt32();
			this.pwErrMessage = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pwErrMessage is not null)
			{
				encoder.WriteWideCharString(this.pwErrMessage.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pwErrMessage is not null)
			{
				this.pwErrMessage.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DSA_MSG_EXECUTE_SCRIPT_REPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DSA_MSG_EXECUTE_SCRIPT_REPLY_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DSA_MSG_EXECUTE_SCRIPT_REPLY_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DSA_MSG_EXECUTE_SCRIPT_REPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DSA_MSG_PREPARE_SCRIPT_REQ_V1 : IRpcFixedStruct
	{
		public uint Reserved;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Reserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Reserved = decoder.ReadUInt32();
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
	public partial struct DSA_MSG_PREPARE_SCRIPT_REQ : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DSA_MSG_PREPARE_SCRIPT_REQ_V1 V1;
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
					this.V1 = decoder.ReadFixedStruct<DSA_MSG_PREPARE_SCRIPT_REQ_V1>(NdrAlignment._4Byte);
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
					decoder.ReadStructDeferral<DSA_MSG_PREPARE_SCRIPT_REQ_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DSA_MSG_PREPARE_SCRIPT_REPLY_V1 : IRpcFixedStruct
	{
		public uint dwOperationStatus;
		public RpcPointer<string> pwErrMessage;
		public uint cbPassword;
		public RpcPointer<byte[]> pbPassword;
		public uint cbHashBody;
		public RpcPointer<byte[]> pbHashBody;
		public uint cbHashSignature;
		public RpcPointer<byte[]> pbHashSignature;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwOperationStatus);
			encoder.WriteUniquePointer(this.pwErrMessage);
			encoder.WriteValue(this.cbPassword);
			encoder.WriteUniquePointer(this.pbPassword);
			encoder.WriteValue(this.cbHashBody);
			encoder.WriteUniquePointer(this.pbHashBody);
			encoder.WriteValue(this.cbHashSignature);
			encoder.WriteUniquePointer(this.pbHashSignature);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwOperationStatus = decoder.ReadUInt32();
			this.pwErrMessage = decoder.ReadUniquePointer<string>();
			this.cbPassword = decoder.ReadUInt32();
			this.pbPassword = decoder.ReadUniquePointer<byte[]>();
			this.cbHashBody = decoder.ReadUInt32();
			this.pbHashBody = decoder.ReadUniquePointer<byte[]>();
			this.cbHashSignature = decoder.ReadUInt32();
			this.pbHashSignature = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pwErrMessage is not null)
			{
				encoder.WriteWideCharString(this.pwErrMessage.value);
			}

			if (this.pbPassword is not null)
			{
				encoder.WriteArrayHeader(this.pbPassword.value);
				for (int i = 0; i < this.pbPassword.value.Length; i++)
				{
					byte elem_0 = this.pbPassword.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.pbHashBody is not null)
			{
				encoder.WriteArrayHeader(this.pbHashBody.value);
				for (int i = 0; i < this.pbHashBody.value.Length; i++)
				{
					byte elem_0 = this.pbHashBody.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.pbHashSignature is not null)
			{
				encoder.WriteArrayHeader(this.pbHashSignature.value);
				for (int i = 0; i < this.pbHashSignature.value.Length; i++)
				{
					byte elem_0 = this.pbHashSignature.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pwErrMessage is not null)
			{
				this.pwErrMessage.value = decoder.ReadWideCharString();
			}

			if (this.pbPassword is not null)
			{
				this.pbPassword.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pbPassword.value.Length; i++)
				{
					byte elem_0 = this.pbPassword.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pbPassword.value[i] = elem_0;
				}
			}

			if (this.pbHashBody is not null)
			{
				this.pbHashBody.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pbHashBody.value.Length; i++)
				{
					byte elem_0 = this.pbHashBody.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pbHashBody.value[i] = elem_0;
				}
			}

			if (this.pbHashSignature is not null)
			{
				this.pbHashSignature.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pbHashSignature.value.Length; i++)
				{
					byte elem_0 = this.pbHashSignature.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pbHashSignature.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DSA_MSG_PREPARE_SCRIPT_REPLY : IRpcFixedStruct
	{
		public uint unionSwitch;
		public DSA_MSG_PREPARE_SCRIPT_REPLY_V1 V1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteFixedStruct(this.V1, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.V1 = decoder.ReadFixedStruct<DSA_MSG_PREPARE_SCRIPT_REPLY_V1>(NdrAlignment.NativePtr);
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
					decoder.ReadStructDeferral<DSA_MSG_PREPARE_SCRIPT_REPLY_V1>(ref this.V1);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("e3514235-4b06-11d1-ab04-00c04fc2dcd2"), RpcVersionAttribute(4, 0)]
	public partial interface drsuapi
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSBind(RpcPointer<Guid> puuidClientDsa, RpcPointer<DRS_EXTENSIONS> pextClient, RpcPointer<RpcPointer<DRS_EXTENSIONS>> ppextServer, RpcPointer<RpcContextHandle> phDrs, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSUnbind(RpcPointer<RpcContextHandle> phDrs, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSReplicaSync(RpcContextHandle hDrs, uint dwVersion, DRS_MSG_REPSYNC pmsgSync, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSGetNCChanges(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_GETCHGREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_GETCHGREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSUpdateRefs(RpcContextHandle hDrs, uint dwVersion, DRS_MSG_UPDREFS pmsgUpdRefs, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSReplicaAdd(RpcContextHandle hDrs, uint dwVersion, DRS_MSG_REPADD pmsgAdd, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSReplicaDel(RpcContextHandle hDrs, uint dwVersion, DRS_MSG_REPDEL pmsgDel, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSReplicaModify(RpcContextHandle hDrs, uint dwVersion, DRS_MSG_REPMOD pmsgMod, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSVerifyNames(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_VERIFYREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_VERIFYREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSGetMemberships(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_REVMEMB_REQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_REVMEMB_REPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSInterDomainMove(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_MOVEREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_MOVEREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSGetNT4ChangeLog(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_NT4_CHGLOG_REQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_NT4_CHGLOG_REPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSCrackNames(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_CRACKREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_CRACKREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSWriteSPN(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_SPNREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_SPNREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSRemoveDsServer(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_RMSVRREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_RMSVRREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSRemoveDsDomain(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_RMDMNREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_RMDMNREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSDomainControllerInfo(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_DCINFOREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_DCINFOREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSAddEntry(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_ADDENTRYREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_ADDENTRYREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSExecuteKCC(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_KCC_EXECUTE pmsgIn, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSGetReplInfo(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_GETREPLINFO_REQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_GETREPLINFO_REPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSAddSidHistory(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_ADDSIDREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_ADDSIDREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSGetMemberships2(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_GETMEMBERSHIPS2_REQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_GETMEMBERSHIPS2_REPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSReplicaVerifyObjects(RpcContextHandle hDrs, uint dwVersion, DRS_MSG_REPVERIFYOBJ pmsgVerify, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSGetObjectExistence(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_EXISTREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_EXISTREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSQuerySitesByCost(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_QUERYSITESREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_QUERYSITESREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSInitDemotion(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_INIT_DEMOTIONREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_INIT_DEMOTIONREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSReplicaDemotion(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_REPLICA_DEMOTIONREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_REPLICA_DEMOTIONREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSFinishDemotion(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_FINISH_DEMOTIONREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_FINISH_DEMOTIONREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSAddCloneDC(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_ADDCLONEDCREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_ADDCLONEDCREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSWriteNgcKey(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_WRITENGCKEYREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_WRITENGCKEYREPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DRSReadNgcKey(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_READNGCKEYREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_READNGCKEYREPLY> pmsgOut, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("e3514235-4b06-11d1-ab04-00c04fc2dcd2")]
	public partial class drsuapiClientProxy : Titanis.DceRpc.Client.RpcClientProxy, drsuapi, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSBind(RpcPointer<Guid> puuidClientDsa, RpcPointer<DRS_EXTENSIONS> pextClient, RpcPointer<RpcPointer<DRS_EXTENSIONS>> ppextServer, RpcPointer<RpcContextHandle> phDrs, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(puuidClientDsa);
			if (puuidClientDsa is not null)
			{
				encoder.WriteValue(puuidClientDsa.value);
			}

			encoder.WriteUniquePointer(pextClient);
			if (pextClient is not null)
			{
				encoder.WriteConformantStruct(pextClient.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pextClient.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppextServer.value = decoder.ReadOutUniquePointer<DRS_EXTENSIONS>(ppextServer.value);
			if (ppextServer.value is not null)
			{
				ppextServer.value.value = decoder.ReadConformantStruct<DRS_EXTENSIONS>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DRS_EXTENSIONS>(ref ppextServer.value.value);
			}

			phDrs.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSUnbind(RpcPointer<RpcContextHandle> phDrs, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(phDrs.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phDrs.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSReplicaSync(RpcContextHandle hDrs, uint dwVersion, DRS_MSG_REPSYNC pmsgSync, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwVersion);
			encoder.WriteUnion(pmsgSync);
			encoder.WriteStructDeferral(pmsgSync);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSGetNCChanges(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_GETCHGREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_GETCHGREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_GETCHGREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_GETCHGREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSUpdateRefs(RpcContextHandle hDrs, uint dwVersion, DRS_MSG_UPDREFS pmsgUpdRefs, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwVersion);
			encoder.WriteUnion(pmsgUpdRefs);
			encoder.WriteStructDeferral(pmsgUpdRefs);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSReplicaAdd(RpcContextHandle hDrs, uint dwVersion, DRS_MSG_REPADD pmsgAdd, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwVersion);
			encoder.WriteUnion(pmsgAdd);
			encoder.WriteStructDeferral(pmsgAdd);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSReplicaDel(RpcContextHandle hDrs, uint dwVersion, DRS_MSG_REPDEL pmsgDel, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwVersion);
			encoder.WriteUnion(pmsgDel);
			encoder.WriteStructDeferral(pmsgDel);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSReplicaModify(RpcContextHandle hDrs, uint dwVersion, DRS_MSG_REPMOD pmsgMod, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwVersion);
			encoder.WriteUnion(pmsgMod);
			encoder.WriteStructDeferral(pmsgMod);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSVerifyNames(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_VERIFYREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_VERIFYREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_VERIFYREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_VERIFYREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSGetMemberships(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_REVMEMB_REQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_REVMEMB_REPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_REVMEMB_REPLY>();
			decoder.ReadStructDeferral<DRS_MSG_REVMEMB_REPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSInterDomainMove(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_MOVEREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_MOVEREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_MOVEREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_MOVEREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSGetNT4ChangeLog(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_NT4_CHGLOG_REQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_NT4_CHGLOG_REPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_NT4_CHGLOG_REPLY>();
			decoder.ReadStructDeferral<DRS_MSG_NT4_CHGLOG_REPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSCrackNames(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_CRACKREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_CRACKREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_CRACKREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_CRACKREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSWriteSPN(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_SPNREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_SPNREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_SPNREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_SPNREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSRemoveDsServer(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_RMSVRREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_RMSVRREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_RMSVRREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_RMSVRREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSRemoveDsDomain(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_RMDMNREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_RMDMNREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_RMDMNREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_RMDMNREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSDomainControllerInfo(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_DCINFOREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_DCINFOREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_DCINFOREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_DCINFOREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSAddEntry(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_ADDENTRYREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_ADDENTRYREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_ADDENTRYREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_ADDENTRYREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSExecuteKCC(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_KCC_EXECUTE pmsgIn, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSGetReplInfo(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_GETREPLINFO_REQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_GETREPLINFO_REPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_GETREPLINFO_REPLY>();
			decoder.ReadStructDeferral<DRS_MSG_GETREPLINFO_REPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSAddSidHistory(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_ADDSIDREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_ADDSIDREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_ADDSIDREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_ADDSIDREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSGetMemberships2(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_GETMEMBERSHIPS2_REQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_GETMEMBERSHIPS2_REPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_GETMEMBERSHIPS2_REPLY>();
			decoder.ReadStructDeferral<DRS_MSG_GETMEMBERSHIPS2_REPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSReplicaVerifyObjects(RpcContextHandle hDrs, uint dwVersion, DRS_MSG_REPVERIFYOBJ pmsgVerify, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwVersion);
			encoder.WriteUnion(pmsgVerify);
			encoder.WriteStructDeferral(pmsgVerify);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSGetObjectExistence(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_EXISTREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_EXISTREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_EXISTREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_EXISTREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSQuerySitesByCost(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_QUERYSITESREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_QUERYSITESREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_QUERYSITESREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_QUERYSITESREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSInitDemotion(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_INIT_DEMOTIONREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_INIT_DEMOTIONREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_INIT_DEMOTIONREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_INIT_DEMOTIONREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSReplicaDemotion(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_REPLICA_DEMOTIONREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_REPLICA_DEMOTIONREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_REPLICA_DEMOTIONREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_REPLICA_DEMOTIONREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSFinishDemotion(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_FINISH_DEMOTIONREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_FINISH_DEMOTIONREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_FINISH_DEMOTIONREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_FINISH_DEMOTIONREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSAddCloneDC(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_ADDCLONEDCREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_ADDCLONEDCREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_ADDCLONEDCREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_ADDCLONEDCREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSWriteNgcKey(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_WRITENGCKEYREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_WRITENGCKEYREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_WRITENGCKEYREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_WRITENGCKEYREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DRSReadNgcKey(RpcContextHandle hDrs, uint dwInVersion, DRS_MSG_READNGCKEYREQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DRS_MSG_READNGCKEYREPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hDrs);
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DRS_MSG_READNGCKEYREPLY>();
			decoder.ReadStructDeferral<DRS_MSG_READNGCKEYREPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(drsuapi);
		private static Guid _interfaceUuid = new Guid("e3514235-4b06-11d1-ab04-00c04fc2dcd2");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(4, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class drsuapiStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSBind(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<Guid> puuidClientDsa;
			RpcPointer<DRS_EXTENSIONS> pextClient;
			RpcPointer<RpcPointer<DRS_EXTENSIONS>> ppextServer = new RpcPointer<RpcPointer<DRS_EXTENSIONS>>();
			RpcPointer<RpcContextHandle> phDrs = new RpcPointer<RpcContextHandle>();
			puuidClientDsa = decoder.ReadUniquePointer<Guid>();
			if (puuidClientDsa is not null)
			{
				puuidClientDsa.value = decoder.ReadUuid();
			}

			pextClient = decoder.ReadUniquePointer<DRS_EXTENSIONS>();
			if (pextClient is not null)
			{
				pextClient.value = decoder.ReadConformantStruct<DRS_EXTENSIONS>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<DRS_EXTENSIONS>(ref pextClient.value);
			}

			var invokeTask = this._obj.IDL_DRSBind(puuidClientDsa, pextClient, ppextServer, phDrs, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppextServer.value);
			if (ppextServer.value is not null)
			{
				encoder.WriteConformantStruct(ppextServer.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(ppextServer.value.value);
			}

			encoder.WriteContextHandle(phDrs.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSUnbind(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> phDrs;
			phDrs = new RpcPointer<RpcContextHandle>();
			phDrs.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.IDL_DRSUnbind(phDrs, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phDrs.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSReplicaSync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwVersion;
			DRS_MSG_REPSYNC pmsgSync;
			hDrs = decoder.ReadContextHandle();
			dwVersion = decoder.ReadUInt32();
			pmsgSync = decoder.ReadUnion<DRS_MSG_REPSYNC>();
			decoder.ReadStructDeferral<DRS_MSG_REPSYNC>(ref pmsgSync);
			var invokeTask = this._obj.IDL_DRSReplicaSync(hDrs, dwVersion, pmsgSync, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSGetNCChanges(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_GETCHGREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_GETCHGREPLY> pmsgOut = new RpcPointer<DRS_MSG_GETCHGREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_GETCHGREQ>();
			decoder.ReadStructDeferral<DRS_MSG_GETCHGREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSGetNCChanges(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSUpdateRefs(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwVersion;
			DRS_MSG_UPDREFS pmsgUpdRefs;
			hDrs = decoder.ReadContextHandle();
			dwVersion = decoder.ReadUInt32();
			pmsgUpdRefs = decoder.ReadUnion<DRS_MSG_UPDREFS>();
			decoder.ReadStructDeferral<DRS_MSG_UPDREFS>(ref pmsgUpdRefs);
			var invokeTask = this._obj.IDL_DRSUpdateRefs(hDrs, dwVersion, pmsgUpdRefs, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSReplicaAdd(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwVersion;
			DRS_MSG_REPADD pmsgAdd;
			hDrs = decoder.ReadContextHandle();
			dwVersion = decoder.ReadUInt32();
			pmsgAdd = decoder.ReadUnion<DRS_MSG_REPADD>();
			decoder.ReadStructDeferral<DRS_MSG_REPADD>(ref pmsgAdd);
			var invokeTask = this._obj.IDL_DRSReplicaAdd(hDrs, dwVersion, pmsgAdd, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSReplicaDel(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwVersion;
			DRS_MSG_REPDEL pmsgDel;
			hDrs = decoder.ReadContextHandle();
			dwVersion = decoder.ReadUInt32();
			pmsgDel = decoder.ReadUnion<DRS_MSG_REPDEL>();
			decoder.ReadStructDeferral<DRS_MSG_REPDEL>(ref pmsgDel);
			var invokeTask = this._obj.IDL_DRSReplicaDel(hDrs, dwVersion, pmsgDel, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSReplicaModify(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwVersion;
			DRS_MSG_REPMOD pmsgMod;
			hDrs = decoder.ReadContextHandle();
			dwVersion = decoder.ReadUInt32();
			pmsgMod = decoder.ReadUnion<DRS_MSG_REPMOD>();
			decoder.ReadStructDeferral<DRS_MSG_REPMOD>(ref pmsgMod);
			var invokeTask = this._obj.IDL_DRSReplicaModify(hDrs, dwVersion, pmsgMod, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSVerifyNames(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_VERIFYREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_VERIFYREPLY> pmsgOut = new RpcPointer<DRS_MSG_VERIFYREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_VERIFYREQ>();
			decoder.ReadStructDeferral<DRS_MSG_VERIFYREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSVerifyNames(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSGetMemberships(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_REVMEMB_REQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_REVMEMB_REPLY> pmsgOut = new RpcPointer<DRS_MSG_REVMEMB_REPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_REVMEMB_REQ>();
			decoder.ReadStructDeferral<DRS_MSG_REVMEMB_REQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSGetMemberships(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSInterDomainMove(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_MOVEREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_MOVEREPLY> pmsgOut = new RpcPointer<DRS_MSG_MOVEREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_MOVEREQ>();
			decoder.ReadStructDeferral<DRS_MSG_MOVEREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSInterDomainMove(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSGetNT4ChangeLog(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_NT4_CHGLOG_REQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_NT4_CHGLOG_REPLY> pmsgOut = new RpcPointer<DRS_MSG_NT4_CHGLOG_REPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_NT4_CHGLOG_REQ>();
			decoder.ReadStructDeferral<DRS_MSG_NT4_CHGLOG_REQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSGetNT4ChangeLog(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSCrackNames(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_CRACKREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_CRACKREPLY> pmsgOut = new RpcPointer<DRS_MSG_CRACKREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_CRACKREQ>();
			decoder.ReadStructDeferral<DRS_MSG_CRACKREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSCrackNames(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSWriteSPN(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_SPNREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_SPNREPLY> pmsgOut = new RpcPointer<DRS_MSG_SPNREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_SPNREQ>();
			decoder.ReadStructDeferral<DRS_MSG_SPNREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSWriteSPN(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSRemoveDsServer(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_RMSVRREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_RMSVRREPLY> pmsgOut = new RpcPointer<DRS_MSG_RMSVRREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_RMSVRREQ>();
			decoder.ReadStructDeferral<DRS_MSG_RMSVRREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSRemoveDsServer(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSRemoveDsDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_RMDMNREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_RMDMNREPLY> pmsgOut = new RpcPointer<DRS_MSG_RMDMNREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_RMDMNREQ>();
			decoder.ReadStructDeferral<DRS_MSG_RMDMNREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSRemoveDsDomain(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSDomainControllerInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_DCINFOREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_DCINFOREPLY> pmsgOut = new RpcPointer<DRS_MSG_DCINFOREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_DCINFOREQ>();
			decoder.ReadStructDeferral<DRS_MSG_DCINFOREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSDomainControllerInfo(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSAddEntry(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_ADDENTRYREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_ADDENTRYREPLY> pmsgOut = new RpcPointer<DRS_MSG_ADDENTRYREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_ADDENTRYREQ>();
			decoder.ReadStructDeferral<DRS_MSG_ADDENTRYREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSAddEntry(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSExecuteKCC(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_KCC_EXECUTE pmsgIn;
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_KCC_EXECUTE>();
			decoder.ReadStructDeferral<DRS_MSG_KCC_EXECUTE>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSExecuteKCC(hDrs, dwInVersion, pmsgIn, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSGetReplInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_GETREPLINFO_REQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_GETREPLINFO_REPLY> pmsgOut = new RpcPointer<DRS_MSG_GETREPLINFO_REPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_GETREPLINFO_REQ>();
			decoder.ReadStructDeferral<DRS_MSG_GETREPLINFO_REQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSGetReplInfo(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSAddSidHistory(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_ADDSIDREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_ADDSIDREPLY> pmsgOut = new RpcPointer<DRS_MSG_ADDSIDREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_ADDSIDREQ>();
			decoder.ReadStructDeferral<DRS_MSG_ADDSIDREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSAddSidHistory(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSGetMemberships2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_GETMEMBERSHIPS2_REQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_GETMEMBERSHIPS2_REPLY> pmsgOut = new RpcPointer<DRS_MSG_GETMEMBERSHIPS2_REPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_GETMEMBERSHIPS2_REQ>();
			decoder.ReadStructDeferral<DRS_MSG_GETMEMBERSHIPS2_REQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSGetMemberships2(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSReplicaVerifyObjects(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwVersion;
			DRS_MSG_REPVERIFYOBJ pmsgVerify;
			hDrs = decoder.ReadContextHandle();
			dwVersion = decoder.ReadUInt32();
			pmsgVerify = decoder.ReadUnion<DRS_MSG_REPVERIFYOBJ>();
			decoder.ReadStructDeferral<DRS_MSG_REPVERIFYOBJ>(ref pmsgVerify);
			var invokeTask = this._obj.IDL_DRSReplicaVerifyObjects(hDrs, dwVersion, pmsgVerify, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSGetObjectExistence(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_EXISTREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_EXISTREPLY> pmsgOut = new RpcPointer<DRS_MSG_EXISTREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_EXISTREQ>();
			decoder.ReadStructDeferral<DRS_MSG_EXISTREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSGetObjectExistence(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSQuerySitesByCost(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_QUERYSITESREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_QUERYSITESREPLY> pmsgOut = new RpcPointer<DRS_MSG_QUERYSITESREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_QUERYSITESREQ>();
			decoder.ReadStructDeferral<DRS_MSG_QUERYSITESREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSQuerySitesByCost(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSInitDemotion(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_INIT_DEMOTIONREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_INIT_DEMOTIONREPLY> pmsgOut = new RpcPointer<DRS_MSG_INIT_DEMOTIONREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_INIT_DEMOTIONREQ>();
			decoder.ReadStructDeferral<DRS_MSG_INIT_DEMOTIONREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSInitDemotion(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSReplicaDemotion(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_REPLICA_DEMOTIONREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_REPLICA_DEMOTIONREPLY> pmsgOut = new RpcPointer<DRS_MSG_REPLICA_DEMOTIONREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_REPLICA_DEMOTIONREQ>();
			decoder.ReadStructDeferral<DRS_MSG_REPLICA_DEMOTIONREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSReplicaDemotion(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSFinishDemotion(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_FINISH_DEMOTIONREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_FINISH_DEMOTIONREPLY> pmsgOut = new RpcPointer<DRS_MSG_FINISH_DEMOTIONREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_FINISH_DEMOTIONREQ>();
			decoder.ReadStructDeferral<DRS_MSG_FINISH_DEMOTIONREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSFinishDemotion(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSAddCloneDC(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_ADDCLONEDCREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_ADDCLONEDCREPLY> pmsgOut = new RpcPointer<DRS_MSG_ADDCLONEDCREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_ADDCLONEDCREQ>();
			decoder.ReadStructDeferral<DRS_MSG_ADDCLONEDCREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSAddCloneDC(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSWriteNgcKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_WRITENGCKEYREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_WRITENGCKEYREPLY> pmsgOut = new RpcPointer<DRS_MSG_WRITENGCKEYREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_WRITENGCKEYREQ>();
			decoder.ReadStructDeferral<DRS_MSG_WRITENGCKEYREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSWriteNgcKey(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DRSReadNgcKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hDrs;
			uint dwInVersion;
			DRS_MSG_READNGCKEYREQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DRS_MSG_READNGCKEYREPLY> pmsgOut = new RpcPointer<DRS_MSG_READNGCKEYREPLY>();
			hDrs = decoder.ReadContextHandle();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DRS_MSG_READNGCKEYREQ>();
			decoder.ReadStructDeferral<DRS_MSG_READNGCKEYREQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DRSReadNgcKey(hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("e3514235-4b06-11d1-ab04-00c04fc2dcd2");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(4, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private drsuapi _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public drsuapiStub(drsuapi obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] { this.Invoke_IDL_DRSBind, this.Invoke_IDL_DRSUnbind, this.Invoke_IDL_DRSReplicaSync, this.Invoke_IDL_DRSGetNCChanges, this.Invoke_IDL_DRSUpdateRefs, this.Invoke_IDL_DRSReplicaAdd, this.Invoke_IDL_DRSReplicaDel, this.Invoke_IDL_DRSReplicaModify, this.Invoke_IDL_DRSVerifyNames, this.Invoke_IDL_DRSGetMemberships, this.Invoke_IDL_DRSInterDomainMove, this.Invoke_IDL_DRSGetNT4ChangeLog, this.Invoke_IDL_DRSCrackNames, this.Invoke_IDL_DRSWriteSPN, this.Invoke_IDL_DRSRemoveDsServer, this.Invoke_IDL_DRSRemoveDsDomain, this.Invoke_IDL_DRSDomainControllerInfo, this.Invoke_IDL_DRSAddEntry, this.Invoke_IDL_DRSExecuteKCC, this.Invoke_IDL_DRSGetReplInfo, this.Invoke_IDL_DRSAddSidHistory, this.Invoke_IDL_DRSGetMemberships2, this.Invoke_IDL_DRSReplicaVerifyObjects, this.Invoke_IDL_DRSGetObjectExistence, this.Invoke_IDL_DRSQuerySitesByCost, this.Invoke_IDL_DRSInitDemotion, this.Invoke_IDL_DRSReplicaDemotion, this.Invoke_IDL_DRSFinishDemotion, this.Invoke_IDL_DRSAddCloneDC, this.Invoke_IDL_DRSWriteNgcKey, this.Invoke_IDL_DRSReadNgcKey };
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("7c44d7d4-31d5-424c-bd5e-2b3e1f323d22"), RpcVersionAttribute(1, 0)]
	public partial interface dsaop
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DSAPrepareScript(uint dwInVersion, DSA_MSG_PREPARE_SCRIPT_REQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DSA_MSG_PREPARE_SCRIPT_REPLY> pmsgOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> IDL_DSAExecuteScript(uint dwInVersion, DSA_MSG_EXECUTE_SCRIPT_REQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DSA_MSG_EXECUTE_SCRIPT_REPLY> pmsgOut, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("7c44d7d4-31d5-424c-bd5e-2b3e1f323d22")]
	public partial class dsaopClientProxy : Titanis.DceRpc.Client.RpcClientProxy, dsaop, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DSAPrepareScript(uint dwInVersion, DSA_MSG_PREPARE_SCRIPT_REQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DSA_MSG_PREPARE_SCRIPT_REPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DSA_MSG_PREPARE_SCRIPT_REPLY>();
			decoder.ReadStructDeferral<DSA_MSG_PREPARE_SCRIPT_REPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> IDL_DSAExecuteScript(uint dwInVersion, DSA_MSG_EXECUTE_SCRIPT_REQ pmsgIn, RpcPointer<uint> pdwOutVersion, RpcPointer<DSA_MSG_EXECUTE_SCRIPT_REPLY> pmsgOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(dwInVersion);
			encoder.WriteUnion(pmsgIn);
			encoder.WriteStructDeferral(pmsgIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwOutVersion.value = decoder.ReadUInt32();
			pmsgOut.value = decoder.ReadUnion<DSA_MSG_EXECUTE_SCRIPT_REPLY>();
			decoder.ReadStructDeferral<DSA_MSG_EXECUTE_SCRIPT_REPLY>(ref pmsgOut.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(dsaop);
		private static Guid _interfaceUuid = new Guid("7c44d7d4-31d5-424c-bd5e-2b3e1f323d22");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class dsaopStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DSAPrepareScript(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint dwInVersion;
			DSA_MSG_PREPARE_SCRIPT_REQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DSA_MSG_PREPARE_SCRIPT_REPLY> pmsgOut = new RpcPointer<DSA_MSG_PREPARE_SCRIPT_REPLY>();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DSA_MSG_PREPARE_SCRIPT_REQ>();
			decoder.ReadStructDeferral<DSA_MSG_PREPARE_SCRIPT_REQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DSAPrepareScript(dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_IDL_DSAExecuteScript(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint dwInVersion;
			DSA_MSG_EXECUTE_SCRIPT_REQ pmsgIn;
			RpcPointer<uint> pdwOutVersion = new RpcPointer<uint>();
			RpcPointer<DSA_MSG_EXECUTE_SCRIPT_REPLY> pmsgOut = new RpcPointer<DSA_MSG_EXECUTE_SCRIPT_REPLY>();
			dwInVersion = decoder.ReadUInt32();
			pmsgIn = decoder.ReadUnion<DSA_MSG_EXECUTE_SCRIPT_REQ>();
			decoder.ReadStructDeferral<DSA_MSG_EXECUTE_SCRIPT_REQ>(ref pmsgIn);
			var invokeTask = this._obj.IDL_DSAExecuteScript(dwInVersion, pmsgIn, pdwOutVersion, pmsgOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwOutVersion.value);
			encoder.WriteUnion(pmsgOut.value);
			encoder.WriteStructDeferral(pmsgOut.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("7c44d7d4-31d5-424c-bd5e-2b3e1f323d22");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private dsaop _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public dsaopStub(dsaop obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] { this.Invoke_IDL_DSAPrepareScript, this.Invoke_IDL_DSAExecuteScript };
		}
	}
}