namespace ms_oaut
{
	using ms_dcom;
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct SAFEARRAYBOUND : IRpcFixedStruct
	{
		public uint cElements;
		public int lLbound;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cElements);
			encoder.WriteValue(this.lLbound);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cElements = decoder.ReadUInt32();
			this.lLbound = decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct FLAGGED_WORD_BLOB : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.asData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.asData = decoder.ReadArrayHeader<ushort>();
		}

		public uint cBytes;
		public uint clSize;
		public ushort[] asData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.asData.Length; i++)
			{
				ushort elem_0 = this.asData[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.asData.Length; i++)
			{
				ushort elem_0 = this.asData[i];
				elem_0 = decoder.ReadUInt16();
				this.asData[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cBytes);
			encoder.WriteValue(this.clSize);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cBytes = decoder.ReadUInt32();
			this.clSize = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct SAFEARR_BSTR : IRpcFixedStruct
	{
		public uint Size;
		public RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>[]> aBstr;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Size);
			encoder.WriteUniquePointer(this.aBstr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Size = decoder.ReadUInt32();
			this.aBstr = decoder.ReadUniquePointer<RpcPointer<FLAGGED_WORD_BLOB>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.aBstr is not null)
			{
				encoder.WriteArrayHeader(this.aBstr.value);
				for (int i = 0; i < this.aBstr.value.Length; i++)
				{
					RpcPointer<FLAGGED_WORD_BLOB> elem_0 = this.aBstr.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.aBstr.value.Length; i++)
				{
					RpcPointer<FLAGGED_WORD_BLOB> elem_0 = this.aBstr.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteConformantStruct(elem_0.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.aBstr is not null)
			{
				this.aBstr.value = decoder.ReadArrayHeader<RpcPointer<FLAGGED_WORD_BLOB>>();
				for (int i = 0; i < this.aBstr.value.Length; i++)
				{
					RpcPointer<FLAGGED_WORD_BLOB> elem_0 = this.aBstr.value[i];
					elem_0 = decoder.ReadUniquePointer<FLAGGED_WORD_BLOB>();
					this.aBstr.value[i] = elem_0;
				}

				for (int i = 0; i < this.aBstr.value.Length; i++)
				{
					RpcPointer<FLAGGED_WORD_BLOB> elem_0 = this.aBstr.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref elem_0.value);
					}

					this.aBstr.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct SAFEARR_UNKNOWN : IRpcFixedStruct
	{
		public uint Size;
		public RpcPointer<TypedObjref<IUnknown>[]> apUnknown;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Size);
			encoder.WriteUniquePointer(this.apUnknown);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Size = decoder.ReadUInt32();
			this.apUnknown = decoder.ReadUniquePointer<TypedObjref<IUnknown>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.apUnknown is not null)
			{
				encoder.WriteArrayHeader(this.apUnknown.value);
				for (int i = 0; i < this.apUnknown.value.Length; i++)
				{
					TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
					encoder.WriteInterfacePointer(elem_0);
				}

				for (int i = 0; i < this.apUnknown.value.Length; i++)
				{
					TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
					encoder.WriteInterfacePointerBody(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.apUnknown is not null)
			{
				this.apUnknown.value = decoder.ReadArrayHeader<TypedObjref<IUnknown>>();
				for (int i = 0; i < this.apUnknown.value.Length; i++)
				{
					TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
					elem_0 = decoder.ReadInterfacePointer<IUnknown>();
					this.apUnknown.value[i] = elem_0;
				}

				for (int i = 0; i < this.apUnknown.value.Length; i++)
				{
					TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
					decoder.ReadInterfacePointer(elem_0);
					this.apUnknown.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct SAFEARR_DISPATCH : IRpcFixedStruct
	{
		public uint Size;
		public RpcPointer<TypedObjref<IDispatch>[]> apDispatch;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Size);
			encoder.WriteUniquePointer(this.apDispatch);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Size = decoder.ReadUInt32();
			this.apDispatch = decoder.ReadUniquePointer<TypedObjref<IDispatch>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.apDispatch is not null)
			{
				encoder.WriteArrayHeader(this.apDispatch.value);
				for (int i = 0; i < this.apDispatch.value.Length; i++)
				{
					TypedObjref<IDispatch> elem_0 = this.apDispatch.value[i];
					encoder.WriteInterfacePointer(elem_0);
				}

				for (int i = 0; i < this.apDispatch.value.Length; i++)
				{
					TypedObjref<IDispatch> elem_0 = this.apDispatch.value[i];
					encoder.WriteInterfacePointerBody(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.apDispatch is not null)
			{
				this.apDispatch.value = decoder.ReadArrayHeader<TypedObjref<IDispatch>>();
				for (int i = 0; i < this.apDispatch.value.Length; i++)
				{
					TypedObjref<IDispatch> elem_0 = this.apDispatch.value[i];
					elem_0 = decoder.ReadInterfacePointer<IDispatch>();
					this.apDispatch.value[i] = elem_0;
				}

				for (int i = 0; i < this.apDispatch.value.Length; i++)
				{
					TypedObjref<IDispatch> elem_0 = this.apDispatch.value[i];
					decoder.ReadInterfacePointer(elem_0);
					this.apDispatch.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct CURRENCY : IRpcFixedStruct
	{
		public long int64;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.int64);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.int64 = decoder.ReadInt64();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct wireBRECORDStr : IRpcFixedStruct
	{
		public uint fFlags;
		public uint clSize;
		public RpcPointer<ms_dcom.MInterfacePointer> pRecInfo;
		public RpcPointer<byte[]> pRecord;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.fFlags);
			encoder.WriteValue(this.clSize);
			encoder.WriteUniquePointer(this.pRecInfo);
			encoder.WriteUniquePointer(this.pRecord);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.fFlags = decoder.ReadUInt32();
			this.clSize = decoder.ReadUInt32();
			this.pRecInfo = decoder.ReadUniquePointer<ms_dcom.MInterfacePointer>();
			this.pRecord = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pRecInfo is not null)
			{
				encoder.WriteConformantStruct(this.pRecInfo.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.pRecInfo.value);
			}

			if (this.pRecord is not null)
			{
				encoder.WriteArrayHeader(this.pRecord.value);
				for (int i = 0; i < this.pRecord.value.Length; i++)
				{
					byte elem_0 = this.pRecord.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pRecInfo is not null)
			{
				this.pRecInfo.value = decoder.ReadConformantStruct<ms_dcom.MInterfacePointer>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dcom.MInterfacePointer>(ref this.pRecInfo.value);
			}

			if (this.pRecord is not null)
			{
				this.pRecord.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pRecord.value.Length; i++)
				{
					byte elem_0 = this.pRecord.value[i];
					elem_0 = decoder.ReadByte();
					this.pRecord.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct DECIMAL : IRpcFixedStruct
	{
		public ushort wReserved;
		public byte scale;
		public byte sign;
		public uint Hi32;
		public ulong Lo64;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wReserved);
			encoder.WriteValue(this.scale);
			encoder.WriteValue(this.sign);
			encoder.WriteValue(this.Hi32);
			encoder.WriteValue(this.Lo64);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wReserved = decoder.ReadUInt16();
			this.scale = decoder.ReadByte();
			this.sign = decoder.ReadByte();
			this.Hi32 = decoder.ReadUInt32();
			this.Lo64 = decoder.ReadUInt64();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct Unnamed_1 : IRpcFixedStruct
	{
		public uint vt;
		public long llVal;
		public int lVal;
		public byte bVal;
		public short iVal;
		public float fltVal;
		public double dblVal;
		public short boolVal;
		public int scode;
		public CURRENCY cyVal;
		public double date;
		public RpcPointer<FLAGGED_WORD_BLOB> bstrVal;
		public TypedObjref<IUnknown> punkVal;
		public TypedObjref<IDispatch> pdispVal;
		public RpcPointer<_wireSAFEARRAY> parray;
		public RpcPointer<wireBRECORDStr> brecVal;
		public RpcPointer<byte> pbVal;
		public RpcPointer<short> piVal;
		public RpcPointer<int> plVal;
		public RpcPointer<long> pllVal;
		public RpcPointer<float> pfltVal;
		public RpcPointer<double> pdblVal;
		public RpcPointer<short> pboolVal;
		public RpcPointer<int> pscode;
		public RpcPointer<CURRENCY> pcyVal;
		public RpcPointer<double> pdate;
		public RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrVal;
		public RpcPointer<TypedObjref<IUnknown>> ppunkVal;
		public RpcPointer<TypedObjref<IDispatch>> ppdispVal;
		public RpcPointer<RpcPointer<_wireSAFEARRAY>> pparray;
		public RpcPointer<RpcPointer<wireVARIANTStr>> pvarVal;
		public byte cVal;
		public ushort uiVal;
		public uint ulVal;
		public ulong ullVal;
		public int intVal;
		public uint uintVal;
		public DECIMAL decVal;
		public RpcPointer<byte> pcVal;
		public RpcPointer<ushort> puiVal;
		public RpcPointer<uint> pulVal;
		public RpcPointer<ulong> pullVal;
		public RpcPointer<int> pintVal;
		public RpcPointer<uint> puintVal;
		public RpcPointer<DECIMAL> pdecVal;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._8Byte);
			encoder.WriteValue(this.vt);
			encoder.AlignUnionArm(NdrAlignment._8Byte);
			switch ((uint)this.vt)
			{
				case 20U:
					encoder.WriteValue(this.llVal);
					break;
				case 3U:
					encoder.WriteValue(this.lVal);
					break;
				case 17U:
					encoder.WriteValue(this.bVal);
					break;
				case 2U:
					encoder.WriteValue(this.iVal);
					break;
				case 4U:
					encoder.WriteValue(this.fltVal);
					break;
				case 5U:
					encoder.WriteValue(this.dblVal);
					break;
				case 11U:
					encoder.WriteValue(this.boolVal);
					break;
				case 10U:
					encoder.WriteValue(this.scode);
					break;
				case 6U:
					encoder.WriteFixedStruct(this.cyVal, NdrAlignment._8Byte);
					break;
				case 7U:
					encoder.WriteValue(this.date);
					break;
				case 8U:
					encoder.WriteUniquePointer(this.bstrVal);
					break;
				case 13U:
					encoder.WriteInterfacePointer(this.punkVal);
					break;
				case 9U:
					encoder.WriteInterfacePointer(this.pdispVal);
					break;
				case 8192U:
					encoder.WriteUniquePointer(this.parray);
					break;
				case 36U:
				case 16420U:
					encoder.WriteUniquePointer(this.brecVal);
					break;
				case 16401U:
					encoder.WriteUniquePointer(this.pbVal);
					break;
				case 16386U:
					encoder.WriteUniquePointer(this.piVal);
					break;
				case 16387U:
					encoder.WriteUniquePointer(this.plVal);
					break;
				case 16404U:
					encoder.WriteUniquePointer(this.pllVal);
					break;
				case 16388U:
					encoder.WriteUniquePointer(this.pfltVal);
					break;
				case 16389U:
					encoder.WriteUniquePointer(this.pdblVal);
					break;
				case 16395U:
					encoder.WriteUniquePointer(this.pboolVal);
					break;
				case 16394U:
					encoder.WriteUniquePointer(this.pscode);
					break;
				case 16390U:
					encoder.WriteUniquePointer(this.pcyVal);
					break;
				case 16391U:
					encoder.WriteUniquePointer(this.pdate);
					break;
				case 16392U:
					encoder.WriteUniquePointer(this.pbstrVal);
					break;
				case 16397U:
					encoder.WriteUniquePointer(this.ppunkVal);
					break;
				case 16393U:
					encoder.WriteUniquePointer(this.ppdispVal);
					break;
				case 24576U:
					encoder.WriteUniquePointer(this.pparray);
					break;
				case 16396U:
					encoder.WriteUniquePointer(this.pvarVal);
					break;
				case 16U:
					encoder.WriteValue(this.cVal);
					break;
				case 18U:
					encoder.WriteValue(this.uiVal);
					break;
				case 19U:
					encoder.WriteValue(this.ulVal);
					break;
				case 21U:
					encoder.WriteValue(this.ullVal);
					break;
				case 22U:
					encoder.WriteValue(this.intVal);
					break;
				case 23U:
					encoder.WriteValue(this.uintVal);
					break;
				case 14U:
					encoder.WriteFixedStruct(this.decVal, NdrAlignment._8Byte);
					break;
				case 16400U:
					encoder.WriteUniquePointer(this.pcVal);
					break;
				case 16402U:
					encoder.WriteUniquePointer(this.puiVal);
					break;
				case 16403U:
					encoder.WriteUniquePointer(this.pulVal);
					break;
				case 16405U:
					encoder.WriteUniquePointer(this.pullVal);
					break;
				case 16406U:
					encoder.WriteUniquePointer(this.pintVal);
					break;
				case 16407U:
					encoder.WriteUniquePointer(this.puintVal);
					break;
				case 16398U:
					encoder.WriteUniquePointer(this.pdecVal);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._8Byte);
			this.vt = decoder.ReadUInt32();
			decoder.AlignUnionArm(NdrAlignment._8Byte);
			switch ((uint)this.vt)
			{
				case 20U:
					this.llVal = decoder.ReadInt64();
					break;
				case 3U:
					this.lVal = decoder.ReadInt32();
					break;
				case 17U:
					this.bVal = decoder.ReadByte();
					break;
				case 2U:
					this.iVal = decoder.ReadInt16();
					break;
				case 4U:
					this.fltVal = decoder.ReadFloat();
					break;
				case 5U:
					this.dblVal = decoder.ReadDouble();
					break;
				case 11U:
					this.boolVal = decoder.ReadInt16();
					break;
				case 10U:
					this.scode = decoder.ReadInt32();
					break;
				case 6U:
					this.cyVal = decoder.ReadFixedStruct<CURRENCY>(NdrAlignment._8Byte);
					break;
				case 7U:
					this.date = decoder.ReadDouble();
					break;
				case 8U:
					this.bstrVal = decoder.ReadUniquePointer<FLAGGED_WORD_BLOB>();
					break;
				case 13U:
					this.punkVal = decoder.ReadInterfacePointer<IUnknown>();
					break;
				case 9U:
					this.pdispVal = decoder.ReadInterfacePointer<IDispatch>();
					break;
				case 8192U:
					this.parray = decoder.ReadUniquePointer<_wireSAFEARRAY>();
					break;
				case 36U:
				case 16420U:
					this.brecVal = decoder.ReadUniquePointer<wireBRECORDStr>();
					break;
				case 16401U:
					this.pbVal = decoder.ReadUniquePointer<byte>();
					break;
				case 16386U:
					this.piVal = decoder.ReadUniquePointer<short>();
					break;
				case 16387U:
					this.plVal = decoder.ReadUniquePointer<int>();
					break;
				case 16404U:
					this.pllVal = decoder.ReadUniquePointer<long>();
					break;
				case 16388U:
					this.pfltVal = decoder.ReadUniquePointer<float>();
					break;
				case 16389U:
					this.pdblVal = decoder.ReadUniquePointer<double>();
					break;
				case 16395U:
					this.pboolVal = decoder.ReadUniquePointer<short>();
					break;
				case 16394U:
					this.pscode = decoder.ReadUniquePointer<int>();
					break;
				case 16390U:
					this.pcyVal = decoder.ReadUniquePointer<CURRENCY>();
					break;
				case 16391U:
					this.pdate = decoder.ReadUniquePointer<double>();
					break;
				case 16392U:
					this.pbstrVal = decoder.ReadUniquePointer<RpcPointer<FLAGGED_WORD_BLOB>>();
					break;
				case 16397U:
					this.ppunkVal = decoder.ReadUniquePointer<TypedObjref<IUnknown>>();
					break;
				case 16393U:
					this.ppdispVal = decoder.ReadUniquePointer<TypedObjref<IDispatch>>();
					break;
				case 24576U:
					this.pparray = decoder.ReadUniquePointer<RpcPointer<_wireSAFEARRAY>>();
					break;
				case 16396U:
					this.pvarVal = decoder.ReadUniquePointer<RpcPointer<wireVARIANTStr>>();
					break;
				case 16U:
					this.cVal = decoder.ReadUnsignedChar();
					break;
				case 18U:
					this.uiVal = decoder.ReadUInt16();
					break;
				case 19U:
					this.ulVal = decoder.ReadUInt32();
					break;
				case 21U:
					this.ullVal = decoder.ReadUInt64();
					break;
				case 22U:
					this.intVal = decoder.ReadInt32();
					break;
				case 23U:
					this.uintVal = decoder.ReadUInt32();
					break;
				case 14U:
					this.decVal = decoder.ReadFixedStruct<DECIMAL>(NdrAlignment._8Byte);
					break;
				case 16400U:
					this.pcVal = decoder.ReadUniquePointer<byte>();
					break;
				case 16402U:
					this.puiVal = decoder.ReadUniquePointer<ushort>();
					break;
				case 16403U:
					this.pulVal = decoder.ReadUniquePointer<uint>();
					break;
				case 16405U:
					this.pullVal = decoder.ReadUniquePointer<ulong>();
					break;
				case 16406U:
					this.pintVal = decoder.ReadUniquePointer<int>();
					break;
				case 16407U:
					this.puintVal = decoder.ReadUniquePointer<uint>();
					break;
				case 16398U:
					this.pdecVal = decoder.ReadUniquePointer<DECIMAL>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.vt)
			{
				case 20U:
					break;
				case 3U:
					break;
				case 17U:
					break;
				case 2U:
					break;
				case 4U:
					break;
				case 5U:
					break;
				case 11U:
					break;
				case 10U:
					break;
				case 6U:
					encoder.WriteStructDeferral(this.cyVal);
					break;
				case 7U:
					break;
				case 8U:
					if (this.bstrVal is not null)
					{
						encoder.WriteConformantStruct(this.bstrVal.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.bstrVal.value);
					}

					break;
				case 13U:
					encoder.WriteInterfacePointerBody(this.punkVal);
					break;
				case 9U:
					encoder.WriteInterfacePointerBody(this.pdispVal);
					break;
				case 8192U:
					if (this.parray is not null)
					{
						encoder.WriteConformantStruct(this.parray.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.parray.value);
					}

					break;
				case 36U:
				case 16420U:
					if (this.brecVal is not null)
					{
						encoder.WriteFixedStruct(this.brecVal.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.brecVal.value);
					}

					break;
				case 16401U:
					if (this.pbVal is not null)
					{
						encoder.WriteValue(this.pbVal.value);
					}

					break;
				case 16386U:
					if (this.piVal is not null)
					{
						encoder.WriteValue(this.piVal.value);
					}

					break;
				case 16387U:
					if (this.plVal is not null)
					{
						encoder.WriteValue(this.plVal.value);
					}

					break;
				case 16404U:
					if (this.pllVal is not null)
					{
						encoder.WriteValue(this.pllVal.value);
					}

					break;
				case 16388U:
					if (this.pfltVal is not null)
					{
						encoder.WriteValue(this.pfltVal.value);
					}

					break;
				case 16389U:
					if (this.pdblVal is not null)
					{
						encoder.WriteValue(this.pdblVal.value);
					}

					break;
				case 16395U:
					if (this.pboolVal is not null)
					{
						encoder.WriteValue(this.pboolVal.value);
					}

					break;
				case 16394U:
					if (this.pscode is not null)
					{
						encoder.WriteValue(this.pscode.value);
					}

					break;
				case 16390U:
					if (this.pcyVal is not null)
					{
						encoder.WriteFixedStruct(this.pcyVal.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pcyVal.value);
					}

					break;
				case 16391U:
					if (this.pdate is not null)
					{
						encoder.WriteValue(this.pdate.value);
					}

					break;
				case 16392U:
					if (this.pbstrVal is not null)
					{
						encoder.WriteUniquePointer(this.pbstrVal.value);
						if (this.pbstrVal.value is not null)
						{
							encoder.WriteConformantStruct(this.pbstrVal.value.value, NdrAlignment._4Byte);
							encoder.WriteStructDeferral(this.pbstrVal.value.value);
						}
					}

					break;
				case 16397U:
					if (this.ppunkVal is not null)
					{
						encoder.WriteInterfacePointer(this.ppunkVal.value);
						encoder.WriteInterfacePointerBody(this.ppunkVal.value);
					}

					break;
				case 16393U:
					if (this.ppdispVal is not null)
					{
						encoder.WriteInterfacePointer(this.ppdispVal.value);
						encoder.WriteInterfacePointerBody(this.ppdispVal.value);
					}

					break;
				case 24576U:
					if (this.pparray is not null)
					{
						encoder.WriteUniquePointer(this.pparray.value);
						if (this.pparray.value is not null)
						{
							encoder.WriteConformantStruct(this.pparray.value.value, NdrAlignment.NativePtr);
							encoder.WriteStructDeferral(this.pparray.value.value);
						}
					}

					break;
				case 16396U:
					if (this.pvarVal is not null)
					{
						encoder.WriteUniquePointer(this.pvarVal.value);
						if (this.pvarVal.value is not null)
						{
							encoder.WriteFixedStruct(this.pvarVal.value.value, NdrAlignment._8Byte);
							encoder.WriteStructDeferral(this.pvarVal.value.value);
						}
					}

					break;
				case 16U:
					break;
				case 18U:
					break;
				case 19U:
					break;
				case 21U:
					break;
				case 22U:
					break;
				case 23U:
					break;
				case 14U:
					encoder.WriteStructDeferral(this.decVal);
					break;
				case 16400U:
					if (this.pcVal is not null)
					{
						encoder.WriteValue(this.pcVal.value);
					}

					break;
				case 16402U:
					if (this.puiVal is not null)
					{
						encoder.WriteValue(this.puiVal.value);
					}

					break;
				case 16403U:
					if (this.pulVal is not null)
					{
						encoder.WriteValue(this.pulVal.value);
					}

					break;
				case 16405U:
					if (this.pullVal is not null)
					{
						encoder.WriteValue(this.pullVal.value);
					}

					break;
				case 16406U:
					if (this.pintVal is not null)
					{
						encoder.WriteValue(this.pintVal.value);
					}

					break;
				case 16407U:
					if (this.puintVal is not null)
					{
						encoder.WriteValue(this.puintVal.value);
					}

					break;
				case 16398U:
					if (this.pdecVal is not null)
					{
						encoder.WriteFixedStruct(this.pdecVal.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pdecVal.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.vt)
			{
				case 20U:
					break;
				case 3U:
					break;
				case 17U:
					break;
				case 2U:
					break;
				case 4U:
					break;
				case 5U:
					break;
				case 11U:
					break;
				case 10U:
					break;
				case 6U:
					decoder.ReadStructDeferral<CURRENCY>(ref this.cyVal);
					break;
				case 7U:
					break;
				case 8U:
					if (this.bstrVal is not null)
					{
						this.bstrVal.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref this.bstrVal.value);
					}

					break;
				case 13U:
					decoder.ReadInterfacePointer(this.punkVal);
					break;
				case 9U:
					decoder.ReadInterfacePointer(this.pdispVal);
					break;
				case 8192U:
					if (this.parray is not null)
					{
						this.parray.value = decoder.ReadConformantStruct<_wireSAFEARRAY>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<_wireSAFEARRAY>(ref this.parray.value);
					}

					break;
				case 36U:
				case 16420U:
					if (this.brecVal is not null)
					{
						this.brecVal.value = decoder.ReadFixedStruct<wireBRECORDStr>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<wireBRECORDStr>(ref this.brecVal.value);
					}

					break;
				case 16401U:
					if (this.pbVal is not null)
					{
						this.pbVal.value = decoder.ReadByte();
					}

					break;
				case 16386U:
					if (this.piVal is not null)
					{
						this.piVal.value = decoder.ReadInt16();
					}

					break;
				case 16387U:
					if (this.plVal is not null)
					{
						this.plVal.value = decoder.ReadInt32();
					}

					break;
				case 16404U:
					if (this.pllVal is not null)
					{
						this.pllVal.value = decoder.ReadInt64();
					}

					break;
				case 16388U:
					if (this.pfltVal is not null)
					{
						this.pfltVal.value = decoder.ReadFloat();
					}

					break;
				case 16389U:
					if (this.pdblVal is not null)
					{
						this.pdblVal.value = decoder.ReadDouble();
					}

					break;
				case 16395U:
					if (this.pboolVal is not null)
					{
						this.pboolVal.value = decoder.ReadInt16();
					}

					break;
				case 16394U:
					if (this.pscode is not null)
					{
						this.pscode.value = decoder.ReadInt32();
					}

					break;
				case 16390U:
					if (this.pcyVal is not null)
					{
						this.pcyVal.value = decoder.ReadFixedStruct<CURRENCY>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<CURRENCY>(ref this.pcyVal.value);
					}

					break;
				case 16391U:
					if (this.pdate is not null)
					{
						this.pdate.value = decoder.ReadDouble();
					}

					break;
				case 16392U:
					if (this.pbstrVal is not null)
					{
						this.pbstrVal.value = decoder.ReadUniquePointer<FLAGGED_WORD_BLOB>();
						if (this.pbstrVal.value is not null)
						{
							this.pbstrVal.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
							decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref this.pbstrVal.value.value);
						}
					}

					break;
				case 16397U:
					if (this.ppunkVal is not null)
					{
						this.ppunkVal.value = decoder.ReadInterfacePointer<IUnknown>();
						decoder.ReadInterfacePointer(this.ppunkVal.value);
					}

					break;
				case 16393U:
					if (this.ppdispVal is not null)
					{
						this.ppdispVal.value = decoder.ReadInterfacePointer<IDispatch>();
						decoder.ReadInterfacePointer(this.ppdispVal.value);
					}

					break;
				case 24576U:
					if (this.pparray is not null)
					{
						this.pparray.value = decoder.ReadUniquePointer<_wireSAFEARRAY>();
						if (this.pparray.value is not null)
						{
							this.pparray.value.value = decoder.ReadConformantStruct<_wireSAFEARRAY>(NdrAlignment.NativePtr);
							decoder.ReadStructDeferral<_wireSAFEARRAY>(ref this.pparray.value.value);
						}
					}

					break;
				case 16396U:
					if (this.pvarVal is not null)
					{
						this.pvarVal.value = decoder.ReadUniquePointer<wireVARIANTStr>();
						if (this.pvarVal.value is not null)
						{
							this.pvarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
							decoder.ReadStructDeferral<wireVARIANTStr>(ref this.pvarVal.value.value);
						}
					}

					break;
				case 16U:
					break;
				case 18U:
					break;
				case 19U:
					break;
				case 21U:
					break;
				case 22U:
					break;
				case 23U:
					break;
				case 14U:
					decoder.ReadStructDeferral<DECIMAL>(ref this.decVal);
					break;
				case 16400U:
					if (this.pcVal is not null)
					{
						this.pcVal.value = decoder.ReadUnsignedChar();
					}

					break;
				case 16402U:
					if (this.puiVal is not null)
					{
						this.puiVal.value = decoder.ReadUInt16();
					}

					break;
				case 16403U:
					if (this.pulVal is not null)
					{
						this.pulVal.value = decoder.ReadUInt32();
					}

					break;
				case 16405U:
					if (this.pullVal is not null)
					{
						this.pullVal.value = decoder.ReadUInt64();
					}

					break;
				case 16406U:
					if (this.pintVal is not null)
					{
						this.pintVal.value = decoder.ReadInt32();
					}

					break;
				case 16407U:
					if (this.puintVal is not null)
					{
						this.puintVal.value = decoder.ReadUInt32();
					}

					break;
				case 16398U:
					if (this.pdecVal is not null)
					{
						this.pdecVal.value = decoder.ReadFixedStruct<DECIMAL>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<DECIMAL>(ref this.pdecVal.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct wireVARIANTStr : IRpcFixedStruct
	{
		public uint clSize;
		public uint rpcReserved;
		public ushort vt;
		public ushort wReserved1;
		public ushort wReserved2;
		public ushort wReserved3;
		public Unnamed_1 _varUnion;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.clSize);
			encoder.WriteValue(this.rpcReserved);
			encoder.WriteValue(this.vt);
			encoder.WriteValue(this.wReserved1);
			encoder.WriteValue(this.wReserved2);
			encoder.WriteValue(this.wReserved3);
			encoder.WriteUnion(this._varUnion);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.clSize = decoder.ReadUInt32();
			this.rpcReserved = decoder.ReadUInt32();
			this.vt = decoder.ReadUInt16();
			this.wReserved1 = decoder.ReadUInt16();
			this.wReserved2 = decoder.ReadUInt16();
			this.wReserved3 = decoder.ReadUInt16();
			this._varUnion = decoder.ReadUnion<Unnamed_1>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this._varUnion);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<Unnamed_1>(ref this._varUnion);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct SAFEARR_VARIANT : IRpcFixedStruct
	{
		public uint Size;
		public RpcPointer<RpcPointer<wireVARIANTStr>[]> aVariant;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Size);
			encoder.WriteUniquePointer(this.aVariant);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Size = decoder.ReadUInt32();
			this.aVariant = decoder.ReadUniquePointer<RpcPointer<wireVARIANTStr>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.aVariant is not null)
			{
				encoder.WriteArrayHeader(this.aVariant.value);
				for (int i = 0; i < this.aVariant.value.Length; i++)
				{
					RpcPointer<wireVARIANTStr> elem_0 = this.aVariant.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.aVariant.value.Length; i++)
				{
					RpcPointer<wireVARIANTStr> elem_0 = this.aVariant.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteFixedStruct(elem_0.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.aVariant is not null)
			{
				this.aVariant.value = decoder.ReadArrayHeader<RpcPointer<wireVARIANTStr>>();
				for (int i = 0; i < this.aVariant.value.Length; i++)
				{
					RpcPointer<wireVARIANTStr> elem_0 = this.aVariant.value[i];
					elem_0 = decoder.ReadUniquePointer<wireVARIANTStr>();
					this.aVariant.value[i] = elem_0;
				}

				for (int i = 0; i < this.aVariant.value.Length; i++)
				{
					RpcPointer<wireVARIANTStr> elem_0 = this.aVariant.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<wireVARIANTStr>(ref elem_0.value);
					}

					this.aVariant.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct SAFEARR_BRECORD : IRpcFixedStruct
	{
		public uint Size;
		public RpcPointer<RpcPointer<wireBRECORDStr>[]> aRecord;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Size);
			encoder.WriteUniquePointer(this.aRecord);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Size = decoder.ReadUInt32();
			this.aRecord = decoder.ReadUniquePointer<RpcPointer<wireBRECORDStr>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.aRecord is not null)
			{
				encoder.WriteArrayHeader(this.aRecord.value);
				for (int i = 0; i < this.aRecord.value.Length; i++)
				{
					RpcPointer<wireBRECORDStr> elem_0 = this.aRecord.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.aRecord.value.Length; i++)
				{
					RpcPointer<wireBRECORDStr> elem_0 = this.aRecord.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteFixedStruct(elem_0.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.aRecord is not null)
			{
				this.aRecord.value = decoder.ReadArrayHeader<RpcPointer<wireBRECORDStr>>();
				for (int i = 0; i < this.aRecord.value.Length; i++)
				{
					RpcPointer<wireBRECORDStr> elem_0 = this.aRecord.value[i];
					elem_0 = decoder.ReadUniquePointer<wireBRECORDStr>();
					this.aRecord.value[i] = elem_0;
				}

				for (int i = 0; i < this.aRecord.value.Length; i++)
				{
					RpcPointer<wireBRECORDStr> elem_0 = this.aRecord.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadFixedStruct<wireBRECORDStr>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<wireBRECORDStr>(ref elem_0.value);
					}

					this.aRecord.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct SAFEARR_HAVEIID : IRpcFixedStruct
	{
		public uint Size;
		public RpcPointer<TypedObjref<IUnknown>[]> apUnknown;
		public Guid iid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Size);
			encoder.WriteUniquePointer(this.apUnknown);
			encoder.WriteValue(this.iid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Size = decoder.ReadUInt32();
			this.apUnknown = decoder.ReadUniquePointer<TypedObjref<IUnknown>[]>();
			this.iid = decoder.ReadUuid();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.apUnknown is not null)
			{
				encoder.WriteArrayHeader(this.apUnknown.value);
				for (int i = 0; i < this.apUnknown.value.Length; i++)
				{
					TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
					encoder.WriteInterfacePointer(elem_0);
				}

				for (int i = 0; i < this.apUnknown.value.Length; i++)
				{
					TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
					encoder.WriteInterfacePointerBody(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.apUnknown is not null)
			{
				this.apUnknown.value = decoder.ReadArrayHeader<TypedObjref<IUnknown>>();
				for (int i = 0; i < this.apUnknown.value.Length; i++)
				{
					TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
					elem_0 = decoder.ReadInterfacePointer<IUnknown>();
					this.apUnknown.value[i] = elem_0;
				}

				for (int i = 0; i < this.apUnknown.value.Length; i++)
				{
					TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
					decoder.ReadInterfacePointer(elem_0);
					this.apUnknown.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct BYTE_SIZEDARR : IRpcFixedStruct
	{
		public uint clSize;
		public RpcPointer<byte[]> pData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.clSize);
			encoder.WriteUniquePointer(this.pData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.clSize = decoder.ReadUInt32();
			this.pData = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pData is not null)
			{
				encoder.WriteArrayHeader(this.pData.value);
				for (int i = 0; i < this.pData.value.Length; i++)
				{
					byte elem_0 = this.pData.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pData is not null)
			{
				this.pData.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pData.value.Length; i++)
				{
					byte elem_0 = this.pData.value[i];
					elem_0 = decoder.ReadByte();
					this.pData.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct WORD_SIZEDARR : IRpcFixedStruct
	{
		public uint clSize;
		public RpcPointer<ushort[]> pData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.clSize);
			encoder.WriteUniquePointer(this.pData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.clSize = decoder.ReadUInt32();
			this.pData = decoder.ReadUniquePointer<ushort[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pData is not null)
			{
				encoder.WriteArrayHeader(this.pData.value);
				for (int i = 0; i < this.pData.value.Length; i++)
				{
					ushort elem_0 = this.pData.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pData is not null)
			{
				this.pData.value = decoder.ReadArrayHeader<ushort>();
				for (int i = 0; i < this.pData.value.Length; i++)
				{
					ushort elem_0 = this.pData.value[i];
					elem_0 = decoder.ReadUInt16();
					this.pData.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct DWORD_SIZEDARR : IRpcFixedStruct
	{
		public uint clSize;
		public RpcPointer<uint[]> pData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.clSize);
			encoder.WriteUniquePointer(this.pData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.clSize = decoder.ReadUInt32();
			this.pData = decoder.ReadUniquePointer<uint[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pData is not null)
			{
				encoder.WriteArrayHeader(this.pData.value);
				for (int i = 0; i < this.pData.value.Length; i++)
				{
					uint elem_0 = this.pData.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pData is not null)
			{
				this.pData.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < this.pData.value.Length; i++)
				{
					uint elem_0 = this.pData.value[i];
					elem_0 = decoder.ReadUInt32();
					this.pData.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct HYPER_SIZEDARR : IRpcFixedStruct
	{
		public uint clSize;
		public RpcPointer<long[]> pData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.clSize);
			encoder.WriteUniquePointer(this.pData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.clSize = decoder.ReadUInt32();
			this.pData = decoder.ReadUniquePointer<long[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pData is not null)
			{
				encoder.WriteArrayHeader(this.pData.value);
				for (int i = 0; i < this.pData.value.Length; i++)
				{
					long elem_0 = this.pData.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pData is not null)
			{
				this.pData.value = decoder.ReadArrayHeader<long>();
				for (int i = 0; i < this.pData.value.Length; i++)
				{
					long elem_0 = this.pData.value[i];
					elem_0 = decoder.ReadInt64();
					this.pData.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct SAFEARRAYUNION : IRpcFixedStruct
	{
		public uint sfType;
		public SAFEARR_BSTR BstrStr;
		public SAFEARR_UNKNOWN UnknownStr;
		public SAFEARR_DISPATCH DispatchStr;
		public SAFEARR_VARIANT VariantStr;
		public SAFEARR_BRECORD RecordStr;
		public SAFEARR_HAVEIID HaveIidStr;
		public BYTE_SIZEDARR ByteStr;
		public WORD_SIZEDARR WordStr;
		public DWORD_SIZEDARR LongStr;
		public HYPER_SIZEDARR HyperStr;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.sfType);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((uint)this.sfType)
			{
				case 8U:
					encoder.WriteFixedStruct(this.BstrStr, NdrAlignment.NativePtr);
					break;
				case 13U:
					encoder.WriteFixedStruct(this.UnknownStr, NdrAlignment.NativePtr);
					break;
				case 9U:
					encoder.WriteFixedStruct(this.DispatchStr, NdrAlignment.NativePtr);
					break;
				case 12U:
					encoder.WriteFixedStruct(this.VariantStr, NdrAlignment.NativePtr);
					break;
				case 36U:
					encoder.WriteFixedStruct(this.RecordStr, NdrAlignment.NativePtr);
					break;
				case 32781U:
					encoder.WriteFixedStruct(this.HaveIidStr, NdrAlignment.NativePtr);
					break;
				case 16U:
					encoder.WriteFixedStruct(this.ByteStr, NdrAlignment.NativePtr);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.WordStr, NdrAlignment.NativePtr);
					break;
				case 3U:
					encoder.WriteFixedStruct(this.LongStr, NdrAlignment.NativePtr);
					break;
				case 20U:
					encoder.WriteFixedStruct(this.HyperStr, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.sfType = decoder.ReadUInt32();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((uint)this.sfType)
			{
				case 8U:
					this.BstrStr = decoder.ReadFixedStruct<SAFEARR_BSTR>(NdrAlignment.NativePtr);
					break;
				case 13U:
					this.UnknownStr = decoder.ReadFixedStruct<SAFEARR_UNKNOWN>(NdrAlignment.NativePtr);
					break;
				case 9U:
					this.DispatchStr = decoder.ReadFixedStruct<SAFEARR_DISPATCH>(NdrAlignment.NativePtr);
					break;
				case 12U:
					this.VariantStr = decoder.ReadFixedStruct<SAFEARR_VARIANT>(NdrAlignment.NativePtr);
					break;
				case 36U:
					this.RecordStr = decoder.ReadFixedStruct<SAFEARR_BRECORD>(NdrAlignment.NativePtr);
					break;
				case 32781U:
					this.HaveIidStr = decoder.ReadFixedStruct<SAFEARR_HAVEIID>(NdrAlignment.NativePtr);
					break;
				case 16U:
					this.ByteStr = decoder.ReadFixedStruct<BYTE_SIZEDARR>(NdrAlignment.NativePtr);
					break;
				case 2U:
					this.WordStr = decoder.ReadFixedStruct<WORD_SIZEDARR>(NdrAlignment.NativePtr);
					break;
				case 3U:
					this.LongStr = decoder.ReadFixedStruct<DWORD_SIZEDARR>(NdrAlignment.NativePtr);
					break;
				case 20U:
					this.HyperStr = decoder.ReadFixedStruct<HYPER_SIZEDARR>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.sfType)
			{
				case 8U:
					encoder.WriteStructDeferral(this.BstrStr);
					break;
				case 13U:
					encoder.WriteStructDeferral(this.UnknownStr);
					break;
				case 9U:
					encoder.WriteStructDeferral(this.DispatchStr);
					break;
				case 12U:
					encoder.WriteStructDeferral(this.VariantStr);
					break;
				case 36U:
					encoder.WriteStructDeferral(this.RecordStr);
					break;
				case 32781U:
					encoder.WriteStructDeferral(this.HaveIidStr);
					break;
				case 16U:
					encoder.WriteStructDeferral(this.ByteStr);
					break;
				case 2U:
					encoder.WriteStructDeferral(this.WordStr);
					break;
				case 3U:
					encoder.WriteStructDeferral(this.LongStr);
					break;
				case 20U:
					encoder.WriteStructDeferral(this.HyperStr);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.sfType)
			{
				case 8U:
					decoder.ReadStructDeferral<SAFEARR_BSTR>(ref this.BstrStr);
					break;
				case 13U:
					decoder.ReadStructDeferral<SAFEARR_UNKNOWN>(ref this.UnknownStr);
					break;
				case 9U:
					decoder.ReadStructDeferral<SAFEARR_DISPATCH>(ref this.DispatchStr);
					break;
				case 12U:
					decoder.ReadStructDeferral<SAFEARR_VARIANT>(ref this.VariantStr);
					break;
				case 36U:
					decoder.ReadStructDeferral<SAFEARR_BRECORD>(ref this.RecordStr);
					break;
				case 32781U:
					decoder.ReadStructDeferral<SAFEARR_HAVEIID>(ref this.HaveIidStr);
					break;
				case 16U:
					decoder.ReadStructDeferral<BYTE_SIZEDARR>(ref this.ByteStr);
					break;
				case 2U:
					decoder.ReadStructDeferral<WORD_SIZEDARR>(ref this.WordStr);
					break;
				case 3U:
					decoder.ReadStructDeferral<DWORD_SIZEDARR>(ref this.LongStr);
					break;
				case 20U:
					decoder.ReadStructDeferral<HYPER_SIZEDARR>(ref this.HyperStr);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct _wireSAFEARRAY : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgsabound);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgsabound = decoder.ReadArrayHeader<SAFEARRAYBOUND>();
		}

		public ushort cDims;
		public ushort fFeatures;
		public uint cbElements;
		public uint cLocks;
		public SAFEARRAYUNION uArrayStructs;
		public SAFEARRAYBOUND[] rgsabound;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgsabound.Length; i++)
			{
				SAFEARRAYBOUND elem_0 = this.rgsabound[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgsabound.Length; i++)
			{
				SAFEARRAYBOUND elem_0 = this.rgsabound[i];
				elem_0 = decoder.ReadFixedStruct<SAFEARRAYBOUND>(NdrAlignment._4Byte);
				this.rgsabound[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cDims);
			encoder.WriteValue(this.fFeatures);
			encoder.WriteValue(this.cbElements);
			encoder.WriteValue(this.cLocks);
			encoder.WriteUnion(this.uArrayStructs);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cDims = decoder.ReadUInt16();
			this.fFeatures = decoder.ReadUInt16();
			this.cbElements = decoder.ReadUInt32();
			this.cLocks = decoder.ReadUInt32();
			this.uArrayStructs = decoder.ReadUnion<SAFEARRAYUNION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.uArrayStructs);
			for (int i = 0; i < this.rgsabound.Length; i++)
			{
				SAFEARRAYBOUND elem_0 = this.rgsabound[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SAFEARRAYUNION>(ref this.uArrayStructs);
			for (int i = 0; i < this.rgsabound.Length; i++)
			{
				SAFEARRAYBOUND elem_0 = this.rgsabound[i];
				decoder.ReadStructDeferral<SAFEARRAYBOUND>(ref elem_0);
				this.rgsabound[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum VARENUM : int
	{
		VT_EMPTY = 0,
		VT_NULL = 1,
		VT_I2 = 2,
		VT_I4 = 3,
		VT_R4 = 4,
		VT_R8 = 5,
		VT_CY = 6,
		VT_DATE = 7,
		VT_BSTR = 8,
		VT_DISPATCH = 9,
		VT_ERROR = 10,
		VT_BOOL = 11,
		VT_VARIANT = 12,
		VT_UNKNOWN = 13,
		VT_DECIMAL = 14,
		VT_I1 = 16,
		VT_UI1 = 17,
		VT_UI2 = 18,
		VT_UI4 = 19,
		VT_I8 = 20,
		VT_UI8 = 21,
		VT_INT = 22,
		VT_UINT = 23,
		VT_VOID = 24,
		VT_HRESULT = 25,
		VT_PTR = 26,
		VT_SAFEARRAY = 27,
		VT_CARRAY = 28,
		VT_USERDEFINED = 29,
		VT_LPSTR = 30,
		VT_LPWSTR = 31,
		VT_RECORD = 36,
		VT_INT_PTR = 37,
		VT_UINT_PTR = 38,
		VT_ARRAY = 8192,
		VT_BYREF = 16384
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum ADVFEATUREFLAGS : int
	{
		FADF_AUTO = 1,
		FADF_STATIC = 2,
		FADF_EMBEDDED = 4,
		FADF_FIXEDSIZE = 16,
		FADF_RECORD = 32,
		FADF_HAVEIID = 64,
		FADF_HAVEVARTYPE = 128,
		FADF_BSTR = 256,
		FADF_UNKNOWN = 512,
		FADF_DISPATCH = 1024,
		FADF_VARIANT = 2048
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum SF_TYPE : int
	{
		SF_ERROR = 10,
		SF_I1 = 16,
		SF_I2 = 2,
		SF_I4 = 3,
		SF_I8 = 20,
		SF_BSTR = 8,
		SF_UNKNOWN = 13,
		SF_DISPATCH = 9,
		SF_VARIANT = 12,
		SF_RECORD = 36,
		SF_HAVEIID = 32781
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum CALLCONV : int
	{
		CC_CDECL = 1,
		CC_PASCAL = 2,
		CC_STDCALL = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum FUNCFLAGS : int
	{
		FUNCFLAG_FRESTRICTED = 1,
		FUNCFLAG_FSOURCE = 2,
		FUNCFLAG_FBINDABLE = 4,
		FUNCFLAG_FREQUESTEDIT = 8,
		FUNCFLAG_FDISPLAYBIND = 16,
		FUNCFLAG_FDEFAULTBIND = 32,
		FUNCFLAG_FHIDDEN = 64,
		FUNCFLAG_FUSESGETLASTERROR = 128,
		FUNCFLAG_FDEFAULTCOLLELEM = 256,
		FUNCFLAG_FUIDEFAULT = 512,
		FUNCFLAG_FNONBROWSABLE = 1024,
		FUNCFLAG_FREPLACEABLE = 2048,
		FUNCFLAG_FIMMEDIATEBIND = 4096
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum FUNCKIND : int
	{
		FUNC_PUREVIRTUAL = 1,
		FUNC_STATIC = 3,
		FUNC_DISPATCH = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum IMPLTYPEFLAGS : int
	{
		IMPLTYPEFLAG_FDEFAULT = 1,
		IMPLTYPEFLAG_FSOURCE = 2,
		IMPLTYPEFLAG_FRESTRICTED = 4,
		IMPLTYPEFLAG_FDEFAULTVTABLE = 8
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum INVOKEKIND : int
	{
		INVOKE_FUNC = 1,
		INVOKE_PROPERTYGET = 2,
		INVOKE_PROPERTYPUT = 4,
		INVOKE_PROPERTYPUTREF = 8
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum PARAMFLAGS : int
	{
		PARAMFLAG_NONE = 0,
		PARAMFLAG_FIN = 1,
		PARAMFLAG_FOUT = 2,
		PARAMFLAG_FLCID = 4,
		PARAMFLAG_FRETVAL = 8,
		PARAMFLAG_FOPT = 16,
		PARAMFLAG_FHASDEFAULT = 32,
		PARAMFLAG_FHASCUSTDATA = 64
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum TYPEFLAGS : int
	{
		TYPEFLAG_FAPPOBJECT = 1,
		TYPEFLAG_FCANCREATE = 2,
		TYPEFLAG_FLICENSED = 4,
		TYPEFLAG_FPREDECLID = 8,
		TYPEFLAG_FHIDDEN = 16,
		TYPEFLAG_FCONTROL = 32,
		TYPEFLAG_FDUAL = 64,
		TYPEFLAG_FNONEXTENSIBLE = 128,
		TYPEFLAG_FOLEAUTOMATION = 256,
		TYPEFLAG_FRESTRICTED = 512,
		TYPEFLAG_FAGGREGATABLE = 1024,
		TYPEFLAG_FREPLACEABLE = 2048,
		TYPEFLAG_FDISPATCHABLE = 4096,
		TYPEFLAG_FPROXY = 16384
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum TYPEKIND : int
	{
		TKIND_ENUM = 0,
		TKIND_RECORD = 1,
		TKIND_MODULE = 2,
		TKIND_INTERFACE = 3,
		TKIND_DISPATCH = 4,
		TKIND_COCLASS = 5,
		TKIND_ALIAS = 6,
		TKIND_UNION = 7
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum VARFLAGS : int
	{
		VARFLAG_FREADONLY = 1,
		VARFLAG_FSOURCE = 2,
		VARFLAG_FBINDABLE = 4,
		VARFLAG_FREQUESTEDIT = 8,
		VARFLAG_FDISPLAYBIND = 16,
		VARFLAG_FDEFAULTBIND = 32,
		VARFLAG_FHIDDEN = 64,
		VARFLAG_FRESTRICTED = 128,
		VARFLAG_FDEFAULTCOLLELEM = 256,
		VARFLAG_FUIDEFAULT = 512,
		VARFLAG_FNONBROWSABLE = 1024,
		VARFLAG_FREPLACEABLE = 2048,
		VARFLAG_FIMMEDIATEBIND = 4096
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum VARKIND : int
	{
		VAR_PERINSTANCE = 0,
		VAR_STATIC = 1,
		VAR_CONST = 2,
		VAR_DISPATCH = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum LIBFLAGS : int
	{
		LIBFLAG_FRESTRICTED = 1,
		LIBFLAG_FCONTROL = 2,
		LIBFLAG_FHIDDEN = 4,
		LIBFLAG_FHASDISKIMAGE = 8
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum SYSKIND : int
	{
		SYS_WIN32 = 1,
		SYS_WIN64 = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum DESCKIND : int
	{
		DESCKIND_NONE = 0,
		DESCKIND_FUNCDESC = 1,
		DESCKIND_VARDESC = 2,
		DESCKIND_TYPECOMP = 3,
		DESCKIND_IMPLICITAPPOBJ = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct RecordInfo : IRpcFixedStruct
	{
		public Guid libraryGuid;
		public uint verMajor;
		public Guid recGuid;
		public uint verMinor;
		public uint Lcid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.libraryGuid);
			encoder.WriteValue(this.verMajor);
			encoder.WriteValue(this.recGuid);
			encoder.WriteValue(this.verMinor);
			encoder.WriteValue(this.Lcid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.libraryGuid = decoder.ReadUuid();
			this.verMajor = decoder.ReadUInt32();
			this.recGuid = decoder.ReadUuid();
			this.verMinor = decoder.ReadUInt32();
			this.Lcid = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct DISPPARAMS : IRpcFixedStruct
	{
		public RpcPointer<RpcPointer<wireVARIANTStr>[]> rgvarg;
		public RpcPointer<int[]> rgdispidNamedArgs;
		public uint cArgs;
		public uint cNamedArgs;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.rgvarg);
			encoder.WriteUniquePointer(this.rgdispidNamedArgs);
			encoder.WriteValue(this.cArgs);
			encoder.WriteValue(this.cNamedArgs);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.rgvarg = decoder.ReadUniquePointer<RpcPointer<wireVARIANTStr>[]>();
			this.rgdispidNamedArgs = decoder.ReadUniquePointer<int[]>();
			this.cArgs = decoder.ReadUInt32();
			this.cNamedArgs = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.rgvarg is not null)
			{
				encoder.WriteArrayHeader(this.rgvarg.value);
				for (int i = 0; i < this.rgvarg.value.Length; i++)
				{
					RpcPointer<wireVARIANTStr> elem_0 = this.rgvarg.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.rgvarg.value.Length; i++)
				{
					RpcPointer<wireVARIANTStr> elem_0 = this.rgvarg.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteFixedStruct(elem_0.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}

			if (this.rgdispidNamedArgs is not null)
			{
				encoder.WriteArrayHeader(this.rgdispidNamedArgs.value);
				for (int i = 0; i < this.rgdispidNamedArgs.value.Length; i++)
				{
					int elem_0 = this.rgdispidNamedArgs.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.rgvarg is not null)
			{
				this.rgvarg.value = decoder.ReadArrayHeader<RpcPointer<wireVARIANTStr>>();
				for (int i = 0; i < this.rgvarg.value.Length; i++)
				{
					RpcPointer<wireVARIANTStr> elem_0 = this.rgvarg.value[i];
					elem_0 = decoder.ReadUniquePointer<wireVARIANTStr>();
					this.rgvarg.value[i] = elem_0;
				}

				for (int i = 0; i < this.rgvarg.value.Length; i++)
				{
					RpcPointer<wireVARIANTStr> elem_0 = this.rgvarg.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<wireVARIANTStr>(ref elem_0.value);
					}

					this.rgvarg.value[i] = elem_0;
				}
			}

			if (this.rgdispidNamedArgs is not null)
			{
				this.rgdispidNamedArgs.value = decoder.ReadArrayHeader<int>();
				for (int i = 0; i < this.rgdispidNamedArgs.value.Length; i++)
				{
					int elem_0 = this.rgdispidNamedArgs.value[i];
					elem_0 = decoder.ReadInt32();
					this.rgdispidNamedArgs.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct EXCEPINFO : IRpcFixedStruct
	{
		public ushort wCode;
		public ushort wReserved;
		public RpcPointer<FLAGGED_WORD_BLOB> bstrSource;
		public RpcPointer<FLAGGED_WORD_BLOB> bstrDescription;
		public RpcPointer<FLAGGED_WORD_BLOB> bstrHelpFile;
		public uint dwHelpContext;
		public UIntPtr pvReserved;
		public UIntPtr pfnDeferredFillIn;
		public int scode;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wCode);
			encoder.WriteValue(this.wReserved);
			encoder.WriteUniquePointer(this.bstrSource);
			encoder.WriteUniquePointer(this.bstrDescription);
			encoder.WriteUniquePointer(this.bstrHelpFile);
			encoder.WriteValue(this.dwHelpContext);
			encoder.WriteValue(this.pvReserved);
			encoder.WriteValue(this.pfnDeferredFillIn);
			encoder.WriteValue(this.scode);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wCode = decoder.ReadUInt16();
			this.wReserved = decoder.ReadUInt16();
			this.bstrSource = decoder.ReadUniquePointer<FLAGGED_WORD_BLOB>();
			this.bstrDescription = decoder.ReadUniquePointer<FLAGGED_WORD_BLOB>();
			this.bstrHelpFile = decoder.ReadUniquePointer<FLAGGED_WORD_BLOB>();
			this.dwHelpContext = decoder.ReadUInt32();
			this.pvReserved = decoder.ReadUInt3264();
			this.pfnDeferredFillIn = decoder.ReadUInt3264();
			this.scode = decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.bstrSource is not null)
			{
				encoder.WriteConformantStruct(this.bstrSource.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.bstrSource.value);
			}

			if (this.bstrDescription is not null)
			{
				encoder.WriteConformantStruct(this.bstrDescription.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.bstrDescription.value);
			}

			if (this.bstrHelpFile is not null)
			{
				encoder.WriteConformantStruct(this.bstrHelpFile.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.bstrHelpFile.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.bstrSource is not null)
			{
				this.bstrSource.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref this.bstrSource.value);
			}

			if (this.bstrDescription is not null)
			{
				this.bstrDescription.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref this.bstrDescription.value);
			}

			if (this.bstrHelpFile is not null)
			{
				this.bstrHelpFile.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref this.bstrHelpFile.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct ARRAYDESC : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.rgbounds);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.rgbounds = decoder.ReadArrayHeader<SAFEARRAYBOUND>();
		}

		public TYPEDESC tdescElem;
		public ushort cDims;
		public SAFEARRAYBOUND[] rgbounds;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.rgbounds.Length; i++)
			{
				SAFEARRAYBOUND elem_0 = this.rgbounds[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.rgbounds.Length; i++)
			{
				SAFEARRAYBOUND elem_0 = this.rgbounds[i];
				elem_0 = decoder.ReadFixedStruct<SAFEARRAYBOUND>(NdrAlignment._4Byte);
				this.rgbounds[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.tdescElem, NdrAlignment.NativePtr);
			encoder.WriteValue(this.cDims);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.tdescElem = decoder.ReadFixedStruct<TYPEDESC>(NdrAlignment.NativePtr);
			this.cDims = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.tdescElem);
			for (int i = 0; i < this.rgbounds.Length; i++)
			{
				SAFEARRAYBOUND elem_0 = this.rgbounds[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<TYPEDESC>(ref this.tdescElem);
			for (int i = 0; i < this.rgbounds.Length; i++)
			{
				SAFEARRAYBOUND elem_0 = this.rgbounds[i];
				decoder.ReadStructDeferral<SAFEARRAYBOUND>(ref elem_0);
				this.rgbounds[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct Unnamed_2 : IRpcFixedStruct
	{
		public ushort vt;
		public RpcPointer<TYPEDESC> lptdesc;
		public RpcPointer<ARRAYDESC> lpadesc;
		public uint hreftype;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.vt);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.vt)
			{
				case 26:
				case 27:
					encoder.WriteUniquePointer(this.lptdesc);
					break;
				case 28:
					encoder.WriteUniquePointer(this.lpadesc);
					break;
				case 29:
					encoder.WriteValue(this.hreftype);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.vt = decoder.ReadUInt16();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.vt)
			{
				case 26:
				case 27:
					this.lptdesc = decoder.ReadUniquePointer<TYPEDESC>();
					break;
				case 28:
					this.lpadesc = decoder.ReadUniquePointer<ARRAYDESC>();
					break;
				case 29:
					this.hreftype = decoder.ReadUInt32();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.vt)
			{
				case 26:
				case 27:
					if (this.lptdesc is not null)
					{
						encoder.WriteFixedStruct(this.lptdesc.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.lptdesc.value);
					}

					break;
				case 28:
					if (this.lpadesc is not null)
					{
						encoder.WriteConformantStruct(this.lpadesc.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.lpadesc.value);
					}

					break;
				case 29:
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.vt)
			{
				case 26:
				case 27:
					if (this.lptdesc is not null)
					{
						this.lptdesc.value = decoder.ReadFixedStruct<TYPEDESC>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<TYPEDESC>(ref this.lptdesc.value);
					}

					break;
				case 28:
					if (this.lpadesc is not null)
					{
						this.lpadesc.value = decoder.ReadConformantStruct<ARRAYDESC>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<ARRAYDESC>(ref this.lpadesc.value);
					}

					break;
				case 29:
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct TYPEDESC : IRpcFixedStruct
	{
		public Unnamed_2 _tdUnion;
		public ushort vt;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUnion(this._tdUnion);
			encoder.WriteValue(this.vt);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this._tdUnion = decoder.ReadUnion<Unnamed_2>();
			this.vt = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this._tdUnion);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<Unnamed_2>(ref this._tdUnion);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct PARAMDESCEX : IRpcFixedStruct
	{
		public uint cBytes;
		public RpcPointer<wireVARIANTStr> varDefaultValue;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cBytes);
			encoder.WriteUniquePointer(this.varDefaultValue);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cBytes = decoder.ReadUInt32();
			this.varDefaultValue = decoder.ReadUniquePointer<wireVARIANTStr>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.varDefaultValue is not null)
			{
				encoder.WriteFixedStruct(this.varDefaultValue.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.varDefaultValue.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.varDefaultValue is not null)
			{
				this.varDefaultValue.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<wireVARIANTStr>(ref this.varDefaultValue.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct PARAMDESC : IRpcFixedStruct
	{
		public RpcPointer<PARAMDESCEX> pparamdescex;
		public ushort wParamFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.pparamdescex);
			encoder.WriteValue(this.wParamFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.pparamdescex = decoder.ReadUniquePointer<PARAMDESCEX>();
			this.wParamFlags = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pparamdescex is not null)
			{
				encoder.WriteFixedStruct(this.pparamdescex.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.pparamdescex.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pparamdescex is not null)
			{
				this.pparamdescex.value = decoder.ReadFixedStruct<PARAMDESCEX>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<PARAMDESCEX>(ref this.pparamdescex.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct ELEMDESC : IRpcFixedStruct
	{
		public TYPEDESC tdesc;
		public PARAMDESC paramdesc;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.tdesc, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.paramdesc, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.tdesc = decoder.ReadFixedStruct<TYPEDESC>(NdrAlignment.NativePtr);
			this.paramdesc = decoder.ReadFixedStruct<PARAMDESC>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.tdesc);
			encoder.WriteStructDeferral(this.paramdesc);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<TYPEDESC>(ref this.tdesc);
			decoder.ReadStructDeferral<PARAMDESC>(ref this.paramdesc);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct FUNCDESC : IRpcFixedStruct
	{
		public int memid;
		public RpcPointer<int[]> lReserved1;
		public RpcPointer<ELEMDESC[]> lprgelemdescParam;
		public FUNCKIND funckind;
		public INVOKEKIND invkind;
		public CALLCONV callconv;
		public short cParams;
		public short cParamsOpt;
		public short oVft;
		public short cReserved2;
		public ELEMDESC elemdescFunc;
		public ushort wFuncFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.memid);
			encoder.WriteUniquePointer(this.lReserved1);
			encoder.WriteUniquePointer(this.lprgelemdescParam);
			encoder.WriteValue((int)this.funckind);
			encoder.WriteValue((int)this.invkind);
			encoder.WriteValue((int)this.callconv);
			encoder.WriteValue(this.cParams);
			encoder.WriteValue(this.cParamsOpt);
			encoder.WriteValue(this.oVft);
			encoder.WriteValue(this.cReserved2);
			encoder.WriteFixedStruct(this.elemdescFunc, NdrAlignment.NativePtr);
			encoder.WriteValue(this.wFuncFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.memid = decoder.ReadInt32();
			this.lReserved1 = decoder.ReadUniquePointer<int[]>();
			this.lprgelemdescParam = decoder.ReadUniquePointer<ELEMDESC[]>();
			this.funckind = (FUNCKIND)decoder.ReadInt32();
			this.invkind = (INVOKEKIND)decoder.ReadInt32();
			this.callconv = (CALLCONV)decoder.ReadInt32();
			this.cParams = decoder.ReadInt16();
			this.cParamsOpt = decoder.ReadInt16();
			this.oVft = decoder.ReadInt16();
			this.cReserved2 = decoder.ReadInt16();
			this.elemdescFunc = decoder.ReadFixedStruct<ELEMDESC>(NdrAlignment.NativePtr);
			this.wFuncFlags = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lReserved1 is not null)
			{
				encoder.WriteArrayHeader(this.lReserved1.value);
				for (int i = 0; i < this.lReserved1.value.Length; i++)
				{
					int elem_0 = this.lReserved1.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.lprgelemdescParam is not null)
			{
				encoder.WriteArrayHeader(this.lprgelemdescParam.value);
				for (int i = 0; i < this.lprgelemdescParam.value.Length; i++)
				{
					ELEMDESC elem_0 = this.lprgelemdescParam.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.lprgelemdescParam.value.Length; i++)
				{
					ELEMDESC elem_0 = this.lprgelemdescParam.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteStructDeferral(this.elemdescFunc);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lReserved1 is not null)
			{
				this.lReserved1.value = decoder.ReadArrayHeader<int>();
				for (int i = 0; i < this.lReserved1.value.Length; i++)
				{
					int elem_0 = this.lReserved1.value[i];
					elem_0 = decoder.ReadInt32();
					this.lReserved1.value[i] = elem_0;
				}
			}

			if (this.lprgelemdescParam is not null)
			{
				this.lprgelemdescParam.value = decoder.ReadArrayHeader<ELEMDESC>();
				for (int i = 0; i < this.lprgelemdescParam.value.Length; i++)
				{
					ELEMDESC elem_0 = this.lprgelemdescParam.value[i];
					elem_0 = decoder.ReadFixedStruct<ELEMDESC>(NdrAlignment.NativePtr);
					this.lprgelemdescParam.value[i] = elem_0;
				}

				for (int i = 0; i < this.lprgelemdescParam.value.Length; i++)
				{
					ELEMDESC elem_0 = this.lprgelemdescParam.value[i];
					decoder.ReadStructDeferral<ELEMDESC>(ref elem_0);
					this.lprgelemdescParam.value[i] = elem_0;
				}
			}

			decoder.ReadStructDeferral<ELEMDESC>(ref this.elemdescFunc);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct Unnamed_3 : IRpcFixedStruct
	{
		public VARKIND varkind;
		public uint oInst;
		public RpcPointer<RpcPointer<wireVARIANTStr>> lpvarValue;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue((int)this.varkind);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.varkind)
			{
				case 0:
				case 3:
				case 1:
					encoder.WriteValue(this.oInst);
					break;
				case 2:
					encoder.WriteUniquePointer(this.lpvarValue);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.varkind = (VARKIND)decoder.ReadInt32();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.varkind)
			{
				case 0:
				case 3:
				case 1:
					this.oInst = decoder.ReadUInt32();
					break;
				case 2:
					this.lpvarValue = decoder.ReadUniquePointer<RpcPointer<wireVARIANTStr>>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.varkind)
			{
				case 0:
				case 3:
				case 1:
					break;
				case 2:
					if (this.lpvarValue is not null)
					{
						encoder.WriteUniquePointer(this.lpvarValue.value);
						if (this.lpvarValue.value is not null)
						{
							encoder.WriteFixedStruct(this.lpvarValue.value.value, NdrAlignment._8Byte);
							encoder.WriteStructDeferral(this.lpvarValue.value.value);
						}
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.varkind)
			{
				case 0:
				case 3:
				case 1:
					break;
				case 2:
					if (this.lpvarValue is not null)
					{
						this.lpvarValue.value = decoder.ReadUniquePointer<wireVARIANTStr>();
						if (this.lpvarValue.value is not null)
						{
							this.lpvarValue.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
							decoder.ReadStructDeferral<wireVARIANTStr>(ref this.lpvarValue.value.value);
						}
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct VARDESC : IRpcFixedStruct
	{
		public int memid;
		public RpcPointer<string> lpstrReserved;
		public Unnamed_3 _vdUnion;
		public ELEMDESC elemdescVar;
		public ushort wVarFlags;
		public VARKIND varkind;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.memid);
			encoder.WriteUniquePointer(this.lpstrReserved);
			encoder.WriteUnion(this._vdUnion);
			encoder.WriteFixedStruct(this.elemdescVar, NdrAlignment.NativePtr);
			encoder.WriteValue(this.wVarFlags);
			encoder.WriteValue((int)this.varkind);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.memid = decoder.ReadInt32();
			this.lpstrReserved = decoder.ReadUniquePointer<string>();
			this._vdUnion = decoder.ReadUnion<Unnamed_3>();
			this.elemdescVar = decoder.ReadFixedStruct<ELEMDESC>(NdrAlignment.NativePtr);
			this.wVarFlags = decoder.ReadUInt16();
			this.varkind = (VARKIND)decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpstrReserved is not null)
			{
				encoder.WriteWideCharString(this.lpstrReserved.value);
			}

			encoder.WriteStructDeferral(this._vdUnion);
			encoder.WriteStructDeferral(this.elemdescVar);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpstrReserved is not null)
			{
				this.lpstrReserved.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<Unnamed_3>(ref this._vdUnion);
			decoder.ReadStructDeferral<ELEMDESC>(ref this.elemdescVar);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct TYPEATTR : IRpcFixedStruct
	{
		public Guid guid;
		public uint lcid;
		public uint dwReserved1;
		public uint dwReserved2;
		public uint dwReserved3;
		public RpcPointer<string> lpstrReserved4;
		public uint cbSizeInstance;
		public TYPEKIND typekind;
		public ushort cFuncs;
		public ushort cVars;
		public ushort cImplTypes;
		public ushort cbSizeVft;
		public ushort cbAlignment;
		public ushort wTypeFlags;
		public ushort wMajorVerNum;
		public ushort wMinorVerNum;
		public TYPEDESC tdescAlias;
		public uint dwReserved5;
		public ushort wReserved6;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.guid);
			encoder.WriteValue(this.lcid);
			encoder.WriteValue(this.dwReserved1);
			encoder.WriteValue(this.dwReserved2);
			encoder.WriteValue(this.dwReserved3);
			encoder.WriteUniquePointer(this.lpstrReserved4);
			encoder.WriteValue(this.cbSizeInstance);
			encoder.WriteValue((int)this.typekind);
			encoder.WriteValue(this.cFuncs);
			encoder.WriteValue(this.cVars);
			encoder.WriteValue(this.cImplTypes);
			encoder.WriteValue(this.cbSizeVft);
			encoder.WriteValue(this.cbAlignment);
			encoder.WriteValue(this.wTypeFlags);
			encoder.WriteValue(this.wMajorVerNum);
			encoder.WriteValue(this.wMinorVerNum);
			encoder.WriteFixedStruct(this.tdescAlias, NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwReserved5);
			encoder.WriteValue(this.wReserved6);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.guid = decoder.ReadUuid();
			this.lcid = decoder.ReadUInt32();
			this.dwReserved1 = decoder.ReadUInt32();
			this.dwReserved2 = decoder.ReadUInt32();
			this.dwReserved3 = decoder.ReadUInt32();
			this.lpstrReserved4 = decoder.ReadUniquePointer<string>();
			this.cbSizeInstance = decoder.ReadUInt32();
			this.typekind = (TYPEKIND)decoder.ReadInt32();
			this.cFuncs = decoder.ReadUInt16();
			this.cVars = decoder.ReadUInt16();
			this.cImplTypes = decoder.ReadUInt16();
			this.cbSizeVft = decoder.ReadUInt16();
			this.cbAlignment = decoder.ReadUInt16();
			this.wTypeFlags = decoder.ReadUInt16();
			this.wMajorVerNum = decoder.ReadUInt16();
			this.wMinorVerNum = decoder.ReadUInt16();
			this.tdescAlias = decoder.ReadFixedStruct<TYPEDESC>(NdrAlignment.NativePtr);
			this.dwReserved5 = decoder.ReadUInt32();
			this.wReserved6 = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpstrReserved4 is not null)
			{
				encoder.WriteWideCharString(this.lpstrReserved4.value);
			}

			encoder.WriteStructDeferral(this.tdescAlias);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpstrReserved4 is not null)
			{
				this.lpstrReserved4.value = decoder.ReadWideCharString();
			}

			decoder.ReadStructDeferral<TYPEDESC>(ref this.tdescAlias);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct TLIBATTR : IRpcFixedStruct
	{
		public Guid guid;
		public uint lcid;
		public SYSKIND syskind;
		public ushort wMajorVerNum;
		public ushort wMinorVerNum;
		public ushort wLibFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.guid);
			encoder.WriteValue(this.lcid);
			encoder.WriteValue((int)this.syskind);
			encoder.WriteValue(this.wMajorVerNum);
			encoder.WriteValue(this.wMinorVerNum);
			encoder.WriteValue(this.wLibFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.guid = decoder.ReadUuid();
			this.lcid = decoder.ReadUInt32();
			this.syskind = (SYSKIND)decoder.ReadInt32();
			this.wMajorVerNum = decoder.ReadUInt16();
			this.wMinorVerNum = decoder.ReadUInt16();
			this.wLibFlags = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct CUSTDATAITEM : IRpcFixedStruct
	{
		public Guid guid;
		public RpcPointer<wireVARIANTStr> varValue;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.guid);
			encoder.WriteUniquePointer(this.varValue);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.guid = decoder.ReadUuid();
			this.varValue = decoder.ReadUniquePointer<wireVARIANTStr>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.varValue is not null)
			{
				encoder.WriteFixedStruct(this.varValue.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.varValue.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.varValue is not null)
			{
				this.varValue.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<wireVARIANTStr>(ref this.varValue.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct CUSTDATA : IRpcFixedStruct
	{
		public uint cCustData;
		public RpcPointer<CUSTDATAITEM[]> prgCustData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cCustData);
			encoder.WriteUniquePointer(this.prgCustData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cCustData = decoder.ReadUInt32();
			this.prgCustData = decoder.ReadUniquePointer<CUSTDATAITEM[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.prgCustData is not null)
			{
				encoder.WriteArrayHeader(this.prgCustData.value);
				for (int i = 0; i < this.prgCustData.value.Length; i++)
				{
					CUSTDATAITEM elem_0 = this.prgCustData.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.prgCustData.value.Length; i++)
				{
					CUSTDATAITEM elem_0 = this.prgCustData.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.prgCustData is not null)
			{
				this.prgCustData.value = decoder.ReadArrayHeader<CUSTDATAITEM>();
				for (int i = 0; i < this.prgCustData.value.Length; i++)
				{
					CUSTDATAITEM elem_0 = this.prgCustData.value[i];
					elem_0 = decoder.ReadFixedStruct<CUSTDATAITEM>(NdrAlignment.NativePtr);
					this.prgCustData.value[i] = elem_0;
				}

				for (int i = 0; i < this.prgCustData.value.Length; i++)
				{
					CUSTDATAITEM elem_0 = this.prgCustData.value[i];
					decoder.ReadStructDeferral<CUSTDATAITEM>(ref elem_0);
					this.prgCustData.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("00020400-0000-0000-c000-000000000046"), RpcVersionAttribute(0, 0)]
	public partial interface IDispatch : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetTypeInfoCount(RpcPointer<uint> pctinfo, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetTypeInfo(uint iTInfo, uint lcid, RpcPointer<TypedObjref<ITypeInfo>> ppTInfo, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetIDsOfNames(Guid riid, RpcPointer<string>[] rgszNames, uint cNames, uint lcid, RpcPointer<int[]> rgDispId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Invoke(int dispIdMember, Guid riid, uint lcid, uint dwFlags, DISPPARAMS pDispParams, RpcPointer<RpcPointer<wireVARIANTStr>> pVarResult, RpcPointer<EXCEPINFO> pExcepInfo, RpcPointer<uint> pArgErr, uint cVarRef, uint[] rgVarRefIdx, RpcPointer<RpcPointer<wireVARIANTStr>[]> rgVarRef, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("00020400-0000-0000-c000-000000000046")]
	public partial class IDispatchClientProxy : IUnknownClientProxy, IDispatch
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetTypeInfoCount(RpcPointer<uint> pctinfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pctinfo.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetTypeInfo(uint iTInfo, uint lcid, RpcPointer<TypedObjref<ITypeInfo>> ppTInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(iTInfo);
			encoder.WriteValue(lcid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppTInfo.value = decoder.ReadInterfacePointer<ITypeInfo>();
			decoder.ReadInterfacePointer(ppTInfo.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetIDsOfNames(Guid riid, RpcPointer<string>[] rgszNames, uint cNames, uint lcid, RpcPointer<int[]> rgDispId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(riid);
			if (rgszNames is not null)
			{
				encoder.WriteArrayHeader(rgszNames);
				for (int i = 0; i < rgszNames.Length; i++)
				{
					RpcPointer<string> elem_0 = rgszNames[i];
					encoder.WriteUniquePointer(elem_0);
				}
			}

			for (int i = 0; i < rgszNames.Length; i++)
			{
				RpcPointer<string> elem_0 = rgszNames[i];
				if (elem_0 is not null)
				{
					encoder.WriteWideCharString(elem_0.value);
				}
			}

			encoder.WriteValue(cNames);
			encoder.WriteValue(lcid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			rgDispId.value = decoder.ReadArrayHeader<int>();
			for (int i = 0; i < rgDispId.value.Length; i++)
			{
				int elem_0 = rgDispId.value[i];
				elem_0 = decoder.ReadInt32();
				rgDispId.value[i] = elem_0;
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Invoke(int dispIdMember, Guid riid, uint lcid, uint dwFlags, DISPPARAMS pDispParams, RpcPointer<RpcPointer<wireVARIANTStr>> pVarResult, RpcPointer<EXCEPINFO> pExcepInfo, RpcPointer<uint> pArgErr, uint cVarRef, uint[] rgVarRefIdx, RpcPointer<RpcPointer<wireVARIANTStr>[]> rgVarRef, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(dispIdMember);
			encoder.WriteValue(riid);
			encoder.WriteValue(lcid);
			encoder.WriteValue(dwFlags);
			encoder.WriteFixedStruct(pDispParams, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pDispParams);
			encoder.WriteValue(cVarRef);
			if (rgVarRefIdx is not null)
			{
				encoder.WriteArrayHeader(rgVarRefIdx);
				for (int i = 0; i < rgVarRefIdx.Length; i++)
				{
					uint elem_0 = rgVarRefIdx[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteArrayHeader(rgVarRef.value);
			for (int i = 0; i < rgVarRef.value.Length; i++)
			{
				RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
				encoder.WriteUniquePointer(elem_0);
			}

			for (int i = 0; i < rgVarRef.value.Length; i++)
			{
				RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
				if (elem_0 is not null)
				{
					encoder.WriteFixedStruct(elem_0.value, NdrAlignment._8Byte);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pVarResult.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarResult.value);
			if (pVarResult.value is not null)
			{
				pVarResult.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarResult.value.value);
			}

			pExcepInfo.value = decoder.ReadFixedStruct<EXCEPINFO>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<EXCEPINFO>(ref pExcepInfo.value);
			pArgErr.value = decoder.ReadUInt32();
			rgVarRef.value = decoder.ReadArrayHeader<RpcPointer<wireVARIANTStr>>();
			for (int i = 0; i < rgVarRef.value.Length; i++)
			{
				RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
				elem_0 = decoder.ReadUniquePointer<wireVARIANTStr>();
				rgVarRef.value[i] = elem_0;
			}

			for (int i = 0; i < rgVarRef.value.Length; i++)
			{
				RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
					decoder.ReadStructDeferral<wireVARIANTStr>(ref elem_0.value);
				}

				rgVarRef.value[i] = elem_0;
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IDispatch);
		private static Guid _interfaceUuid = new Guid("00020400-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IDispatchStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeInfoCount(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pctinfo = new RpcPointer<uint>();
			var invokeTask = this._obj.GetTypeInfoCount(pctinfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pctinfo.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint iTInfo;
			uint lcid;
			RpcPointer<TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<TypedObjref<ITypeInfo>>();
			iTInfo = decoder.ReadUInt32();
			lcid = decoder.ReadUInt32();
			var invokeTask = this._obj.GetTypeInfo(iTInfo, lcid, ppTInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTInfo.value);
			encoder.WriteInterfacePointerBody(ppTInfo.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetIDsOfNames(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid riid;
			RpcPointer<string>[] rgszNames;
			uint cNames;
			uint lcid;
			RpcPointer<int[]> rgDispId = new RpcPointer<int[]>();
			riid = decoder.ReadUuid();
			rgszNames = decoder.ReadArrayHeader<RpcPointer<string>>();
			for (int i = 0; i < rgszNames.Length; i++)
			{
				RpcPointer<string> elem_0 = rgszNames[i];
				elem_0 = decoder.ReadUniquePointer<string>();
				rgszNames[i] = elem_0;
			}

			for (int i = 0; i < rgszNames.Length; i++)
			{
				RpcPointer<string> elem_0 = rgszNames[i];
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadWideCharString();
				}

				rgszNames[i] = elem_0;
			}

			cNames = decoder.ReadUInt32();
			lcid = decoder.ReadUInt32();
			var invokeTask = this._obj.GetIDsOfNames(riid, rgszNames, cNames, lcid, rgDispId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(rgDispId.value);
			for (int i = 0; i < rgDispId.value.Length; i++)
			{
				int elem_0 = rgDispId.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Invoke(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int dispIdMember;
			Guid riid;
			uint lcid;
			uint dwFlags;
			DISPPARAMS pDispParams;
			RpcPointer<RpcPointer<wireVARIANTStr>> pVarResult = new RpcPointer<RpcPointer<wireVARIANTStr>>();
			RpcPointer<EXCEPINFO> pExcepInfo = new RpcPointer<EXCEPINFO>();
			RpcPointer<uint> pArgErr = new RpcPointer<uint>();
			uint cVarRef;
			uint[] rgVarRefIdx;
			RpcPointer<RpcPointer<wireVARIANTStr>[]> rgVarRef;
			dispIdMember = decoder.ReadInt32();
			riid = decoder.ReadUuid();
			lcid = decoder.ReadUInt32();
			dwFlags = decoder.ReadUInt32();
			pDispParams = decoder.ReadFixedStruct<DISPPARAMS>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<DISPPARAMS>(ref pDispParams);
			cVarRef = decoder.ReadUInt32();
			rgVarRefIdx = decoder.ReadArrayHeader<uint>();
			for (int i = 0; i < rgVarRefIdx.Length; i++)
			{
				uint elem_0 = rgVarRefIdx[i];
				elem_0 = decoder.ReadUInt32();
				rgVarRefIdx[i] = elem_0;
			}

			rgVarRef = new RpcPointer<RpcPointer<wireVARIANTStr>[]>();
			rgVarRef.value = decoder.ReadArrayHeader<RpcPointer<wireVARIANTStr>>();
			for (int i = 0; i < rgVarRef.value.Length; i++)
			{
				RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
				elem_0 = decoder.ReadUniquePointer<wireVARIANTStr>();
				rgVarRef.value[i] = elem_0;
			}

			for (int i = 0; i < rgVarRef.value.Length; i++)
			{
				RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
					decoder.ReadStructDeferral<wireVARIANTStr>(ref elem_0.value);
				}

				rgVarRef.value[i] = elem_0;
			}

			var invokeTask = this._obj.Invoke(dispIdMember, riid, lcid, dwFlags, pDispParams, pVarResult, pExcepInfo, pArgErr, cVarRef, rgVarRefIdx, rgVarRef, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pVarResult.value);
			if (pVarResult.value is not null)
			{
				encoder.WriteFixedStruct(pVarResult.value.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(pVarResult.value.value);
			}

			encoder.WriteFixedStruct(pExcepInfo.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pExcepInfo.value);
			encoder.WriteValue(pArgErr.value);
			encoder.WriteArrayHeader(rgVarRef.value);
			for (int i = 0; i < rgVarRef.value.Length; i++)
			{
				RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
				encoder.WriteUniquePointer(elem_0);
			}

			for (int i = 0; i < rgVarRef.value.Length; i++)
			{
				RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
				if (elem_0 is not null)
				{
					encoder.WriteFixedStruct(elem_0.value, NdrAlignment._8Byte);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}

			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("00020400-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IDispatch _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IDispatchStub(IDispatch obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_GetTypeInfoCount, this.Invoke_GetTypeInfo, this.Invoke_GetIDsOfNames, this.Invoke_Invoke};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("00020404-0000-0000-c000-000000000046"), RpcVersionAttribute(0, 0)]
	public partial interface IEnumVARIANT : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Next(uint celt, RpcPointer<ArraySegment<RpcPointer<wireVARIANTStr>>> rgVar, RpcPointer<uint> pCeltFetched, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Skip(uint celt, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Reset(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Clone(RpcPointer<TypedObjref<IEnumVARIANT>> ppEnum, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("00020404-0000-0000-c000-000000000046")]
	public partial class IEnumVARIANTClientProxy : IUnknownClientProxy, IEnumVARIANT
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Next(uint celt, RpcPointer<ArraySegment<RpcPointer<wireVARIANTStr>>> rgVar, RpcPointer<uint> pCeltFetched, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(celt);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			rgVar.value = decoder.ReadArraySegmentHeader<RpcPointer<wireVARIANTStr>>();
			for (int i = 0; i < rgVar.value.Count; i++)
			{
				RpcPointer<wireVARIANTStr> elem_0 = rgVar.value.Item(i);
				elem_0 = decoder.ReadUniquePointer<wireVARIANTStr>();
				rgVar.value.Item(i) = elem_0;
			}

			for (int i = 0; i < rgVar.value.Count; i++)
			{
				RpcPointer<wireVARIANTStr> elem_0 = rgVar.value.Item(i);
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
					decoder.ReadStructDeferral<wireVARIANTStr>(ref elem_0.value);
				}

				rgVar.value.Item(i) = elem_0;
			}

			pCeltFetched.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Skip(uint celt, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(celt);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Reset(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Clone(RpcPointer<TypedObjref<IEnumVARIANT>> ppEnum, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppEnum.value = decoder.ReadInterfacePointer<IEnumVARIANT>();
			decoder.ReadInterfacePointer(ppEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IEnumVARIANT);
		private static Guid _interfaceUuid = new Guid("00020404-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IEnumVARIANTStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Next(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint celt;
			RpcPointer<ArraySegment<RpcPointer<wireVARIANTStr>>> rgVar = new RpcPointer<ArraySegment<RpcPointer<wireVARIANTStr>>>();
			RpcPointer<uint> pCeltFetched = new RpcPointer<uint>();
			celt = decoder.ReadUInt32();
			var invokeTask = this._obj.Next(celt, rgVar, pCeltFetched, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(rgVar.value, true);
			for (int i = 0; i < rgVar.value.Count; i++)
			{
				RpcPointer<wireVARIANTStr> elem_0 = rgVar.value.Item(i);
				encoder.WriteUniquePointer(elem_0);
			}

			for (int i = 0; i < rgVar.value.Count; i++)
			{
				RpcPointer<wireVARIANTStr> elem_0 = rgVar.value.Item(i);
				if (elem_0 is not null)
				{
					encoder.WriteFixedStruct(elem_0.value, NdrAlignment._8Byte);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}

			encoder.WriteValue(pCeltFetched.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Skip(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint celt;
			celt = decoder.ReadUInt32();
			var invokeTask = this._obj.Skip(celt, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Reset(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Reset(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Clone(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<TypedObjref<IEnumVARIANT>> ppEnum = new RpcPointer<TypedObjref<IEnumVARIANT>>();
			var invokeTask = this._obj.Clone(ppEnum, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppEnum.value);
			encoder.WriteInterfacePointerBody(ppEnum.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("00020404-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IEnumVARIANT _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IEnumVARIANTStub(IEnumVARIANT obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_Next, this.Invoke_Skip, this.Invoke_Reset, this.Invoke_Clone};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("00020403-0000-0000-c000-000000000046"), RpcVersionAttribute(0, 0)]
	public partial interface ITypeComp : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Bind(string szName, uint lHashVal, ushort wFlags, RpcPointer<TypedObjref<ITypeInfo>> ppTInfo, RpcPointer<DESCKIND> pDescKind, RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc, RpcPointer<RpcPointer<VARDESC>> ppVarDesc, RpcPointer<TypedObjref<ITypeComp>> ppTypeComp, RpcPointer<uint> pReserved, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> BindType(string szName, uint lHashVal, RpcPointer<TypedObjref<ITypeInfo>> ppTInfo, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("00020403-0000-0000-c000-000000000046")]
	public partial class ITypeCompClientProxy : IUnknownClientProxy, ITypeComp
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Bind(string szName, uint lHashVal, ushort wFlags, RpcPointer<TypedObjref<ITypeInfo>> ppTInfo, RpcPointer<DESCKIND> pDescKind, RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc, RpcPointer<RpcPointer<VARDESC>> ppVarDesc, RpcPointer<TypedObjref<ITypeComp>> ppTypeComp, RpcPointer<uint> pReserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(szName);
			encoder.WriteValue(lHashVal);
			encoder.WriteValue(wFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppTInfo.value = decoder.ReadInterfacePointer<ITypeInfo>();
			decoder.ReadInterfacePointer(ppTInfo.value);
			pDescKind.value = (DESCKIND)decoder.ReadInt32();
			ppFuncDesc.value = decoder.ReadOutUniquePointer<FUNCDESC>(ppFuncDesc.value);
			if (ppFuncDesc.value is not null)
			{
				ppFuncDesc.value.value = decoder.ReadFixedStruct<FUNCDESC>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FUNCDESC>(ref ppFuncDesc.value.value);
			}

			ppVarDesc.value = decoder.ReadOutUniquePointer<VARDESC>(ppVarDesc.value);
			if (ppVarDesc.value is not null)
			{
				ppVarDesc.value.value = decoder.ReadFixedStruct<VARDESC>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<VARDESC>(ref ppVarDesc.value.value);
			}

			ppTypeComp.value = decoder.ReadInterfacePointer<ITypeComp>();
			decoder.ReadInterfacePointer(ppTypeComp.value);
			pReserved.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> BindType(string szName, uint lHashVal, RpcPointer<TypedObjref<ITypeInfo>> ppTInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(szName);
			encoder.WriteValue(lHashVal);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppTInfo.value = decoder.ReadInterfacePointer<ITypeInfo>();
			decoder.ReadInterfacePointer(ppTInfo.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(ITypeComp);
		private static Guid _interfaceUuid = new Guid("00020403-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class ITypeCompStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Bind(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string szName;
			uint lHashVal;
			ushort wFlags;
			RpcPointer<TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<TypedObjref<ITypeInfo>>();
			RpcPointer<DESCKIND> pDescKind = new RpcPointer<DESCKIND>();
			RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc = new RpcPointer<RpcPointer<FUNCDESC>>();
			RpcPointer<RpcPointer<VARDESC>> ppVarDesc = new RpcPointer<RpcPointer<VARDESC>>();
			RpcPointer<TypedObjref<ITypeComp>> ppTypeComp = new RpcPointer<TypedObjref<ITypeComp>>();
			RpcPointer<uint> pReserved = new RpcPointer<uint>();
			szName = decoder.ReadWideCharString();
			lHashVal = decoder.ReadUInt32();
			wFlags = decoder.ReadUInt16();
			var invokeTask = this._obj.Bind(szName, lHashVal, wFlags, ppTInfo, pDescKind, ppFuncDesc, ppVarDesc, ppTypeComp, pReserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTInfo.value);
			encoder.WriteInterfacePointerBody(ppTInfo.value);
			encoder.WriteValue((int)pDescKind.value);
			encoder.WriteUniquePointer(ppFuncDesc.value);
			if (ppFuncDesc.value is not null)
			{
				encoder.WriteFixedStruct(ppFuncDesc.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppFuncDesc.value.value);
			}

			encoder.WriteUniquePointer(ppVarDesc.value);
			if (ppVarDesc.value is not null)
			{
				encoder.WriteFixedStruct(ppVarDesc.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppVarDesc.value.value);
			}

			encoder.WriteInterfacePointer(ppTypeComp.value);
			encoder.WriteInterfacePointerBody(ppTypeComp.value);
			encoder.WriteValue(pReserved.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_BindType(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string szName;
			uint lHashVal;
			RpcPointer<TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<TypedObjref<ITypeInfo>>();
			szName = decoder.ReadWideCharString();
			lHashVal = decoder.ReadUInt32();
			var invokeTask = this._obj.BindType(szName, lHashVal, ppTInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTInfo.value);
			encoder.WriteInterfacePointerBody(ppTInfo.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("00020403-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private ITypeComp _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public ITypeCompStub(ITypeComp obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_Bind, this.Invoke_BindType};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("00020401-0000-0000-c000-000000000046"), RpcVersionAttribute(0, 0)]
	public partial interface ITypeInfo : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetTypeAttr(RpcPointer<RpcPointer<TYPEATTR>> ppTypeAttr, RpcPointer<uint> pReserved, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetTypeComp(RpcPointer<TypedObjref<ITypeComp>> ppTComp, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetFuncDesc(uint index, RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc, RpcPointer<uint> pReserved, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetVarDesc(uint index, RpcPointer<RpcPointer<VARDESC>> ppVarDesc, RpcPointer<uint> pReserved, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetNames(int memid, RpcPointer<ArraySegment<RpcPointer<FLAGGED_WORD_BLOB>>> rgBstrNames, uint cMaxNames, RpcPointer<uint> pcNames, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetRefTypeOfImplType(uint index, RpcPointer<uint> pRefType, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetImplTypeFlags(uint index, RpcPointer<int> pImplTypeFlags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Opnum10NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Opnum11NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetDocumentation(int memid, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDocString, RpcPointer<uint> pdwHelpContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrHelpFile, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetDllEntry(int memid, INVOKEKIND invKind, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDllName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName, RpcPointer<ushort> pwOrdinal, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetRefTypeInfo(uint hRefType, RpcPointer<TypedObjref<ITypeInfo>> ppTInfo, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Opnum15NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> CreateInstance(Guid riid, RpcPointer<TypedObjref<IUnknown>> ppvObj, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetMops(int memid, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrMops, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetContainingTypeLib(RpcPointer<TypedObjref<ITypeLib>> ppTLib, RpcPointer<uint> pIndex, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Opnum19NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Opnum20NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Opnum21NotUsedOnWire(CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("00020401-0000-0000-c000-000000000046")]
	public partial class ITypeInfoClientProxy : IUnknownClientProxy, ITypeInfo
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetTypeAttr(RpcPointer<RpcPointer<TYPEATTR>> ppTypeAttr, RpcPointer<uint> pReserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppTypeAttr.value = decoder.ReadOutUniquePointer<TYPEATTR>(ppTypeAttr.value);
			if (ppTypeAttr.value is not null)
			{
				ppTypeAttr.value.value = decoder.ReadFixedStruct<TYPEATTR>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<TYPEATTR>(ref ppTypeAttr.value.value);
			}

			pReserved.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetTypeComp(RpcPointer<TypedObjref<ITypeComp>> ppTComp, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppTComp.value = decoder.ReadInterfacePointer<ITypeComp>();
			decoder.ReadInterfacePointer(ppTComp.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetFuncDesc(uint index, RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc, RpcPointer<uint> pReserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppFuncDesc.value = decoder.ReadOutUniquePointer<FUNCDESC>(ppFuncDesc.value);
			if (ppFuncDesc.value is not null)
			{
				ppFuncDesc.value.value = decoder.ReadFixedStruct<FUNCDESC>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<FUNCDESC>(ref ppFuncDesc.value.value);
			}

			pReserved.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetVarDesc(uint index, RpcPointer<RpcPointer<VARDESC>> ppVarDesc, RpcPointer<uint> pReserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppVarDesc.value = decoder.ReadOutUniquePointer<VARDESC>(ppVarDesc.value);
			if (ppVarDesc.value is not null)
			{
				ppVarDesc.value.value = decoder.ReadFixedStruct<VARDESC>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<VARDESC>(ref ppVarDesc.value.value);
			}

			pReserved.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetNames(int memid, RpcPointer<ArraySegment<RpcPointer<FLAGGED_WORD_BLOB>>> rgBstrNames, uint cMaxNames, RpcPointer<uint> pcNames, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(memid);
			encoder.WriteValue(cMaxNames);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			rgBstrNames.value = decoder.ReadArraySegmentHeader<RpcPointer<FLAGGED_WORD_BLOB>>();
			for (int i = 0; i < rgBstrNames.value.Count; i++)
			{
				RpcPointer<FLAGGED_WORD_BLOB> elem_0 = rgBstrNames.value.Item(i);
				elem_0 = decoder.ReadUniquePointer<FLAGGED_WORD_BLOB>();
				rgBstrNames.value.Item(i) = elem_0;
			}

			for (int i = 0; i < rgBstrNames.value.Count; i++)
			{
				RpcPointer<FLAGGED_WORD_BLOB> elem_0 = rgBstrNames.value.Item(i);
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
					decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref elem_0.value);
				}

				rgBstrNames.value.Item(i) = elem_0;
			}

			pcNames.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetRefTypeOfImplType(uint index, RpcPointer<uint> pRefType, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pRefType.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetImplTypeFlags(uint index, RpcPointer<int> pImplTypeFlags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pImplTypeFlags.value = decoder.ReadInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Opnum10NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Opnum11NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetDocumentation(int memid, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDocString, RpcPointer<uint> pdwHelpContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrHelpFile, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(memid);
			encoder.WriteValue(refPtrFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pBstrName.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrName.value);
			if (pBstrName.value is not null)
			{
				pBstrName.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrName.value.value);
			}

			pBstrDocString.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrDocString.value);
			if (pBstrDocString.value is not null)
			{
				pBstrDocString.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrDocString.value.value);
			}

			pdwHelpContext.value = decoder.ReadUInt32();
			pBstrHelpFile.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrHelpFile.value);
			if (pBstrHelpFile.value is not null)
			{
				pBstrHelpFile.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrHelpFile.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetDllEntry(int memid, INVOKEKIND invKind, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDllName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName, RpcPointer<ushort> pwOrdinal, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(memid);
			encoder.WriteValue((int)invKind);
			encoder.WriteValue(refPtrFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pBstrDllName.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrDllName.value);
			if (pBstrDllName.value is not null)
			{
				pBstrDllName.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrDllName.value.value);
			}

			pBstrName.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrName.value);
			if (pBstrName.value is not null)
			{
				pBstrName.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrName.value.value);
			}

			pwOrdinal.value = decoder.ReadUInt16();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetRefTypeInfo(uint hRefType, RpcPointer<TypedObjref<ITypeInfo>> ppTInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(hRefType);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppTInfo.value = decoder.ReadInterfacePointer<ITypeInfo>();
			decoder.ReadInterfacePointer(ppTInfo.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Opnum15NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> CreateInstance(Guid riid, RpcPointer<TypedObjref<IUnknown>> ppvObj, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(riid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppvObj.value = decoder.ReadInterfacePointer<IUnknown>();
			decoder.ReadInterfacePointer(ppvObj.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetMops(int memid, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrMops, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(memid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pBstrMops.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrMops.value);
			if (pBstrMops.value is not null)
			{
				pBstrMops.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrMops.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetContainingTypeLib(RpcPointer<TypedObjref<ITypeLib>> ppTLib, RpcPointer<uint> pIndex, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppTLib.value = decoder.ReadInterfacePointer<ITypeLib>();
			decoder.ReadInterfacePointer(ppTLib.value);
			pIndex.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Opnum19NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Opnum20NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Opnum21NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public override Type InterfaceType => typeof(ITypeInfo);
		private static Guid _interfaceUuid = new Guid("00020401-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class ITypeInfoStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeAttr(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<TYPEATTR>> ppTypeAttr = new RpcPointer<RpcPointer<TYPEATTR>>();
			RpcPointer<uint> pReserved = new RpcPointer<uint>();
			var invokeTask = this._obj.GetTypeAttr(ppTypeAttr, pReserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppTypeAttr.value);
			if (ppTypeAttr.value is not null)
			{
				encoder.WriteFixedStruct(ppTypeAttr.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppTypeAttr.value.value);
			}

			encoder.WriteValue(pReserved.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeComp(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<TypedObjref<ITypeComp>> ppTComp = new RpcPointer<TypedObjref<ITypeComp>>();
			var invokeTask = this._obj.GetTypeComp(ppTComp, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTComp.value);
			encoder.WriteInterfacePointerBody(ppTComp.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetFuncDesc(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc = new RpcPointer<RpcPointer<FUNCDESC>>();
			RpcPointer<uint> pReserved = new RpcPointer<uint>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetFuncDesc(index, ppFuncDesc, pReserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppFuncDesc.value);
			if (ppFuncDesc.value is not null)
			{
				encoder.WriteFixedStruct(ppFuncDesc.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppFuncDesc.value.value);
			}

			encoder.WriteValue(pReserved.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetVarDesc(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<RpcPointer<VARDESC>> ppVarDesc = new RpcPointer<RpcPointer<VARDESC>>();
			RpcPointer<uint> pReserved = new RpcPointer<uint>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetVarDesc(index, ppVarDesc, pReserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppVarDesc.value);
			if (ppVarDesc.value is not null)
			{
				encoder.WriteFixedStruct(ppVarDesc.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppVarDesc.value.value);
			}

			encoder.WriteValue(pReserved.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetNames(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int memid;
			RpcPointer<ArraySegment<RpcPointer<FLAGGED_WORD_BLOB>>> rgBstrNames = new RpcPointer<ArraySegment<RpcPointer<FLAGGED_WORD_BLOB>>>();
			uint cMaxNames;
			RpcPointer<uint> pcNames = new RpcPointer<uint>();
			memid = decoder.ReadInt32();
			cMaxNames = decoder.ReadUInt32();
			var invokeTask = this._obj.GetNames(memid, rgBstrNames, cMaxNames, pcNames, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(rgBstrNames.value, true);
			for (int i = 0; i < rgBstrNames.value.Count; i++)
			{
				RpcPointer<FLAGGED_WORD_BLOB> elem_0 = rgBstrNames.value.Item(i);
				encoder.WriteUniquePointer(elem_0);
			}

			for (int i = 0; i < rgBstrNames.value.Count; i++)
			{
				RpcPointer<FLAGGED_WORD_BLOB> elem_0 = rgBstrNames.value.Item(i);
				if (elem_0 is not null)
				{
					encoder.WriteConformantStruct(elem_0.value, NdrAlignment._4Byte);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}

			encoder.WriteValue(pcNames.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetRefTypeOfImplType(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<uint> pRefType = new RpcPointer<uint>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetRefTypeOfImplType(index, pRefType, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pRefType.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetImplTypeFlags(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<int> pImplTypeFlags = new RpcPointer<int>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetImplTypeFlags(index, pImplTypeFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pImplTypeFlags.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum10NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum10NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum11NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum11NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetDocumentation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int memid;
			uint refPtrFlags;
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDocString = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<uint> pdwHelpContext = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrHelpFile = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			memid = decoder.ReadInt32();
			refPtrFlags = decoder.ReadUInt32();
			var invokeTask = this._obj.GetDocumentation(memid, refPtrFlags, pBstrName, pBstrDocString, pdwHelpContext, pBstrHelpFile, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pBstrName.value);
			if (pBstrName.value is not null)
			{
				encoder.WriteConformantStruct(pBstrName.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrName.value.value);
			}

			encoder.WriteUniquePointer(pBstrDocString.value);
			if (pBstrDocString.value is not null)
			{
				encoder.WriteConformantStruct(pBstrDocString.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrDocString.value.value);
			}

			encoder.WriteValue(pdwHelpContext.value);
			encoder.WriteUniquePointer(pBstrHelpFile.value);
			if (pBstrHelpFile.value is not null)
			{
				encoder.WriteConformantStruct(pBstrHelpFile.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrHelpFile.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetDllEntry(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int memid;
			INVOKEKIND invKind;
			uint refPtrFlags;
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDllName = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<ushort> pwOrdinal = new RpcPointer<ushort>();
			memid = decoder.ReadInt32();
			invKind = (INVOKEKIND)decoder.ReadInt32();
			refPtrFlags = decoder.ReadUInt32();
			var invokeTask = this._obj.GetDllEntry(memid, invKind, refPtrFlags, pBstrDllName, pBstrName, pwOrdinal, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pBstrDllName.value);
			if (pBstrDllName.value is not null)
			{
				encoder.WriteConformantStruct(pBstrDllName.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrDllName.value.value);
			}

			encoder.WriteUniquePointer(pBstrName.value);
			if (pBstrName.value is not null)
			{
				encoder.WriteConformantStruct(pBstrName.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrName.value.value);
			}

			encoder.WriteValue(pwOrdinal.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetRefTypeInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint hRefType;
			RpcPointer<TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<TypedObjref<ITypeInfo>>();
			hRefType = decoder.ReadUInt32();
			var invokeTask = this._obj.GetRefTypeInfo(hRefType, ppTInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTInfo.value);
			encoder.WriteInterfacePointerBody(ppTInfo.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum15NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum15NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_CreateInstance(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid riid;
			RpcPointer<TypedObjref<IUnknown>> ppvObj = new RpcPointer<TypedObjref<IUnknown>>();
			riid = decoder.ReadUuid();
			var invokeTask = this._obj.CreateInstance(riid, ppvObj, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppvObj.value);
			encoder.WriteInterfacePointerBody(ppvObj.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetMops(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int memid;
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrMops = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			memid = decoder.ReadInt32();
			var invokeTask = this._obj.GetMops(memid, pBstrMops, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pBstrMops.value);
			if (pBstrMops.value is not null)
			{
				encoder.WriteConformantStruct(pBstrMops.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrMops.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetContainingTypeLib(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<TypedObjref<ITypeLib>> ppTLib = new RpcPointer<TypedObjref<ITypeLib>>();
			RpcPointer<uint> pIndex = new RpcPointer<uint>();
			var invokeTask = this._obj.GetContainingTypeLib(ppTLib, pIndex, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTLib.value);
			encoder.WriteInterfacePointerBody(ppTLib.value);
			encoder.WriteValue(pIndex.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum19NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum19NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum20NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum20NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum21NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum21NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("00020401-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private ITypeInfo _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public ITypeInfoStub(ITypeInfo obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_GetTypeAttr, this.Invoke_GetTypeComp, this.Invoke_GetFuncDesc, this.Invoke_GetVarDesc, this.Invoke_GetNames, this.Invoke_GetRefTypeOfImplType, this.Invoke_GetImplTypeFlags, this.Invoke_Opnum10NotUsedOnWire, this.Invoke_Opnum11NotUsedOnWire, this.Invoke_GetDocumentation, this.Invoke_GetDllEntry, this.Invoke_GetRefTypeInfo, this.Invoke_Opnum15NotUsedOnWire, this.Invoke_CreateInstance, this.Invoke_GetMops, this.Invoke_GetContainingTypeLib, this.Invoke_Opnum19NotUsedOnWire, this.Invoke_Opnum20NotUsedOnWire, this.Invoke_Opnum21NotUsedOnWire};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("00020412-0000-0000-c000-000000000046"), RpcVersionAttribute(0, 0)]
	public partial interface ITypeInfo2 : ITypeInfo
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetTypeKind(RpcPointer<TYPEKIND> pTypeKind, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetTypeFlags(RpcPointer<uint> pTypeFlags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetFuncIndexOfMemId(int memid, INVOKEKIND invKind, RpcPointer<uint> pFuncIndex, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetVarIndexOfMemId(int memid, RpcPointer<uint> pVarIndex, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetCustData(Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetFuncCustData(uint index, Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetParamCustData(uint indexFunc, uint indexParam, Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetVarCustData(uint index, Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetImplTypeCustData(uint index, Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetDocumentation2(int memid, uint lcid, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpString, RpcPointer<uint> pdwHelpStringContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpStringDll, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetAllCustData(RpcPointer<CUSTDATA> pCustData, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetAllFuncCustData(uint index, RpcPointer<CUSTDATA> pCustData, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetAllParamCustData(uint indexFunc, uint indexParam, RpcPointer<CUSTDATA> pCustData, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetAllVarCustData(uint index, RpcPointer<CUSTDATA> pCustData, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetAllImplTypeCustData(uint index, RpcPointer<CUSTDATA> pCustData, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("00020412-0000-0000-c000-000000000046")]
	public partial class ITypeInfo2ClientProxy : ITypeInfoClientProxy, ITypeInfo2
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetTypeKind(RpcPointer<TYPEKIND> pTypeKind, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pTypeKind.value = (TYPEKIND)decoder.ReadInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetTypeFlags(RpcPointer<uint> pTypeFlags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pTypeFlags.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetFuncIndexOfMemId(int memid, INVOKEKIND invKind, RpcPointer<uint> pFuncIndex, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(memid);
			encoder.WriteValue((int)invKind);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pFuncIndex.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetVarIndexOfMemId(int memid, RpcPointer<uint> pVarIndex, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(memid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pVarIndex.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetCustData(Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(guid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pVarVal.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarVal.value);
			if (pVarVal.value is not null)
			{
				pVarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarVal.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetFuncCustData(uint index, Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			encoder.WriteValue(guid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pVarVal.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarVal.value);
			if (pVarVal.value is not null)
			{
				pVarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarVal.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetParamCustData(uint indexFunc, uint indexParam, Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(indexFunc);
			encoder.WriteValue(indexParam);
			encoder.WriteValue(guid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pVarVal.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarVal.value);
			if (pVarVal.value is not null)
			{
				pVarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarVal.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetVarCustData(uint index, Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			encoder.WriteValue(guid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pVarVal.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarVal.value);
			if (pVarVal.value is not null)
			{
				pVarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarVal.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetImplTypeCustData(uint index, Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			encoder.WriteValue(guid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pVarVal.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarVal.value);
			if (pVarVal.value is not null)
			{
				pVarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarVal.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetDocumentation2(int memid, uint lcid, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpString, RpcPointer<uint> pdwHelpStringContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpStringDll, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(memid);
			encoder.WriteValue(lcid);
			encoder.WriteValue(refPtrFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pbstrHelpString.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pbstrHelpString.value);
			if (pbstrHelpString.value is not null)
			{
				pbstrHelpString.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pbstrHelpString.value.value);
			}

			pdwHelpStringContext.value = decoder.ReadUInt32();
			pbstrHelpStringDll.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pbstrHelpStringDll.value);
			if (pbstrHelpStringDll.value is not null)
			{
				pbstrHelpStringDll.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pbstrHelpStringDll.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetAllCustData(RpcPointer<CUSTDATA> pCustData, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pCustData.value = decoder.ReadFixedStruct<CUSTDATA>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<CUSTDATA>(ref pCustData.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetAllFuncCustData(uint index, RpcPointer<CUSTDATA> pCustData, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pCustData.value = decoder.ReadFixedStruct<CUSTDATA>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<CUSTDATA>(ref pCustData.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetAllParamCustData(uint indexFunc, uint indexParam, RpcPointer<CUSTDATA> pCustData, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(indexFunc);
			encoder.WriteValue(indexParam);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pCustData.value = decoder.ReadFixedStruct<CUSTDATA>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<CUSTDATA>(ref pCustData.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetAllVarCustData(uint index, RpcPointer<CUSTDATA> pCustData, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pCustData.value = decoder.ReadFixedStruct<CUSTDATA>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<CUSTDATA>(ref pCustData.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetAllImplTypeCustData(uint index, RpcPointer<CUSTDATA> pCustData, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(36);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pCustData.value = decoder.ReadFixedStruct<CUSTDATA>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<CUSTDATA>(ref pCustData.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(ITypeInfo2);
		private static Guid _interfaceUuid = new Guid("00020412-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class ITypeInfo2Stub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeAttr(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<TYPEATTR>> ppTypeAttr = new RpcPointer<RpcPointer<TYPEATTR>>();
			RpcPointer<uint> pReserved = new RpcPointer<uint>();
			var invokeTask = this._obj.GetTypeAttr(ppTypeAttr, pReserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppTypeAttr.value);
			if (ppTypeAttr.value is not null)
			{
				encoder.WriteFixedStruct(ppTypeAttr.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppTypeAttr.value.value);
			}

			encoder.WriteValue(pReserved.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeComp(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<TypedObjref<ITypeComp>> ppTComp = new RpcPointer<TypedObjref<ITypeComp>>();
			var invokeTask = this._obj.GetTypeComp(ppTComp, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTComp.value);
			encoder.WriteInterfacePointerBody(ppTComp.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetFuncDesc(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc = new RpcPointer<RpcPointer<FUNCDESC>>();
			RpcPointer<uint> pReserved = new RpcPointer<uint>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetFuncDesc(index, ppFuncDesc, pReserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppFuncDesc.value);
			if (ppFuncDesc.value is not null)
			{
				encoder.WriteFixedStruct(ppFuncDesc.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppFuncDesc.value.value);
			}

			encoder.WriteValue(pReserved.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetVarDesc(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<RpcPointer<VARDESC>> ppVarDesc = new RpcPointer<RpcPointer<VARDESC>>();
			RpcPointer<uint> pReserved = new RpcPointer<uint>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetVarDesc(index, ppVarDesc, pReserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppVarDesc.value);
			if (ppVarDesc.value is not null)
			{
				encoder.WriteFixedStruct(ppVarDesc.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppVarDesc.value.value);
			}

			encoder.WriteValue(pReserved.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetNames(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int memid;
			RpcPointer<ArraySegment<RpcPointer<FLAGGED_WORD_BLOB>>> rgBstrNames = new RpcPointer<ArraySegment<RpcPointer<FLAGGED_WORD_BLOB>>>();
			uint cMaxNames;
			RpcPointer<uint> pcNames = new RpcPointer<uint>();
			memid = decoder.ReadInt32();
			cMaxNames = decoder.ReadUInt32();
			var invokeTask = this._obj.GetNames(memid, rgBstrNames, cMaxNames, pcNames, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(rgBstrNames.value, true);
			for (int i = 0; i < rgBstrNames.value.Count; i++)
			{
				RpcPointer<FLAGGED_WORD_BLOB> elem_0 = rgBstrNames.value.Item(i);
				encoder.WriteUniquePointer(elem_0);
			}

			for (int i = 0; i < rgBstrNames.value.Count; i++)
			{
				RpcPointer<FLAGGED_WORD_BLOB> elem_0 = rgBstrNames.value.Item(i);
				if (elem_0 is not null)
				{
					encoder.WriteConformantStruct(elem_0.value, NdrAlignment._4Byte);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}

			encoder.WriteValue(pcNames.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetRefTypeOfImplType(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<uint> pRefType = new RpcPointer<uint>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetRefTypeOfImplType(index, pRefType, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pRefType.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetImplTypeFlags(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<int> pImplTypeFlags = new RpcPointer<int>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetImplTypeFlags(index, pImplTypeFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pImplTypeFlags.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum10NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum10NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum11NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum11NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetDocumentation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int memid;
			uint refPtrFlags;
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDocString = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<uint> pdwHelpContext = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrHelpFile = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			memid = decoder.ReadInt32();
			refPtrFlags = decoder.ReadUInt32();
			var invokeTask = this._obj.GetDocumentation(memid, refPtrFlags, pBstrName, pBstrDocString, pdwHelpContext, pBstrHelpFile, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pBstrName.value);
			if (pBstrName.value is not null)
			{
				encoder.WriteConformantStruct(pBstrName.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrName.value.value);
			}

			encoder.WriteUniquePointer(pBstrDocString.value);
			if (pBstrDocString.value is not null)
			{
				encoder.WriteConformantStruct(pBstrDocString.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrDocString.value.value);
			}

			encoder.WriteValue(pdwHelpContext.value);
			encoder.WriteUniquePointer(pBstrHelpFile.value);
			if (pBstrHelpFile.value is not null)
			{
				encoder.WriteConformantStruct(pBstrHelpFile.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrHelpFile.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetDllEntry(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int memid;
			INVOKEKIND invKind;
			uint refPtrFlags;
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDllName = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<ushort> pwOrdinal = new RpcPointer<ushort>();
			memid = decoder.ReadInt32();
			invKind = (INVOKEKIND)decoder.ReadInt32();
			refPtrFlags = decoder.ReadUInt32();
			var invokeTask = this._obj.GetDllEntry(memid, invKind, refPtrFlags, pBstrDllName, pBstrName, pwOrdinal, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pBstrDllName.value);
			if (pBstrDllName.value is not null)
			{
				encoder.WriteConformantStruct(pBstrDllName.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrDllName.value.value);
			}

			encoder.WriteUniquePointer(pBstrName.value);
			if (pBstrName.value is not null)
			{
				encoder.WriteConformantStruct(pBstrName.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrName.value.value);
			}

			encoder.WriteValue(pwOrdinal.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetRefTypeInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint hRefType;
			RpcPointer<TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<TypedObjref<ITypeInfo>>();
			hRefType = decoder.ReadUInt32();
			var invokeTask = this._obj.GetRefTypeInfo(hRefType, ppTInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTInfo.value);
			encoder.WriteInterfacePointerBody(ppTInfo.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum15NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum15NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_CreateInstance(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid riid;
			RpcPointer<TypedObjref<IUnknown>> ppvObj = new RpcPointer<TypedObjref<IUnknown>>();
			riid = decoder.ReadUuid();
			var invokeTask = this._obj.CreateInstance(riid, ppvObj, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppvObj.value);
			encoder.WriteInterfacePointerBody(ppvObj.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetMops(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int memid;
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrMops = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			memid = decoder.ReadInt32();
			var invokeTask = this._obj.GetMops(memid, pBstrMops, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pBstrMops.value);
			if (pBstrMops.value is not null)
			{
				encoder.WriteConformantStruct(pBstrMops.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrMops.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetContainingTypeLib(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<TypedObjref<ITypeLib>> ppTLib = new RpcPointer<TypedObjref<ITypeLib>>();
			RpcPointer<uint> pIndex = new RpcPointer<uint>();
			var invokeTask = this._obj.GetContainingTypeLib(ppTLib, pIndex, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTLib.value);
			encoder.WriteInterfacePointerBody(ppTLib.value);
			encoder.WriteValue(pIndex.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum19NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum19NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum20NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum20NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum21NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum21NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeKind(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<TYPEKIND> pTypeKind = new RpcPointer<TYPEKIND>();
			var invokeTask = this._obj.GetTypeKind(pTypeKind, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pTypeKind.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeFlags(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pTypeFlags = new RpcPointer<uint>();
			var invokeTask = this._obj.GetTypeFlags(pTypeFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pTypeFlags.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetFuncIndexOfMemId(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int memid;
			INVOKEKIND invKind;
			RpcPointer<uint> pFuncIndex = new RpcPointer<uint>();
			memid = decoder.ReadInt32();
			invKind = (INVOKEKIND)decoder.ReadInt32();
			var invokeTask = this._obj.GetFuncIndexOfMemId(memid, invKind, pFuncIndex, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pFuncIndex.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetVarIndexOfMemId(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int memid;
			RpcPointer<uint> pVarIndex = new RpcPointer<uint>();
			memid = decoder.ReadInt32();
			var invokeTask = this._obj.GetVarIndexOfMemId(memid, pVarIndex, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pVarIndex.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetCustData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid guid;
			RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal = new RpcPointer<RpcPointer<wireVARIANTStr>>();
			guid = decoder.ReadUuid();
			var invokeTask = this._obj.GetCustData(guid, pVarVal, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pVarVal.value);
			if (pVarVal.value is not null)
			{
				encoder.WriteFixedStruct(pVarVal.value.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(pVarVal.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetFuncCustData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			Guid guid;
			RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal = new RpcPointer<RpcPointer<wireVARIANTStr>>();
			index = decoder.ReadUInt32();
			guid = decoder.ReadUuid();
			var invokeTask = this._obj.GetFuncCustData(index, guid, pVarVal, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pVarVal.value);
			if (pVarVal.value is not null)
			{
				encoder.WriteFixedStruct(pVarVal.value.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(pVarVal.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetParamCustData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint indexFunc;
			uint indexParam;
			Guid guid;
			RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal = new RpcPointer<RpcPointer<wireVARIANTStr>>();
			indexFunc = decoder.ReadUInt32();
			indexParam = decoder.ReadUInt32();
			guid = decoder.ReadUuid();
			var invokeTask = this._obj.GetParamCustData(indexFunc, indexParam, guid, pVarVal, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pVarVal.value);
			if (pVarVal.value is not null)
			{
				encoder.WriteFixedStruct(pVarVal.value.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(pVarVal.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetVarCustData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			Guid guid;
			RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal = new RpcPointer<RpcPointer<wireVARIANTStr>>();
			index = decoder.ReadUInt32();
			guid = decoder.ReadUuid();
			var invokeTask = this._obj.GetVarCustData(index, guid, pVarVal, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pVarVal.value);
			if (pVarVal.value is not null)
			{
				encoder.WriteFixedStruct(pVarVal.value.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(pVarVal.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetImplTypeCustData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			Guid guid;
			RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal = new RpcPointer<RpcPointer<wireVARIANTStr>>();
			index = decoder.ReadUInt32();
			guid = decoder.ReadUuid();
			var invokeTask = this._obj.GetImplTypeCustData(index, guid, pVarVal, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pVarVal.value);
			if (pVarVal.value is not null)
			{
				encoder.WriteFixedStruct(pVarVal.value.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(pVarVal.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetDocumentation2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int memid;
			uint lcid;
			uint refPtrFlags;
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpString = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<uint> pdwHelpStringContext = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpStringDll = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			memid = decoder.ReadInt32();
			lcid = decoder.ReadUInt32();
			refPtrFlags = decoder.ReadUInt32();
			var invokeTask = this._obj.GetDocumentation2(memid, lcid, refPtrFlags, pbstrHelpString, pdwHelpStringContext, pbstrHelpStringDll, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pbstrHelpString.value);
			if (pbstrHelpString.value is not null)
			{
				encoder.WriteConformantStruct(pbstrHelpString.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pbstrHelpString.value.value);
			}

			encoder.WriteValue(pdwHelpStringContext.value);
			encoder.WriteUniquePointer(pbstrHelpStringDll.value);
			if (pbstrHelpStringDll.value is not null)
			{
				encoder.WriteConformantStruct(pbstrHelpStringDll.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pbstrHelpStringDll.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetAllCustData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<CUSTDATA> pCustData = new RpcPointer<CUSTDATA>();
			var invokeTask = this._obj.GetAllCustData(pCustData, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pCustData.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pCustData.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetAllFuncCustData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<CUSTDATA> pCustData = new RpcPointer<CUSTDATA>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetAllFuncCustData(index, pCustData, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pCustData.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pCustData.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetAllParamCustData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint indexFunc;
			uint indexParam;
			RpcPointer<CUSTDATA> pCustData = new RpcPointer<CUSTDATA>();
			indexFunc = decoder.ReadUInt32();
			indexParam = decoder.ReadUInt32();
			var invokeTask = this._obj.GetAllParamCustData(indexFunc, indexParam, pCustData, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pCustData.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pCustData.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetAllVarCustData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<CUSTDATA> pCustData = new RpcPointer<CUSTDATA>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetAllVarCustData(index, pCustData, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pCustData.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pCustData.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetAllImplTypeCustData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<CUSTDATA> pCustData = new RpcPointer<CUSTDATA>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetAllImplTypeCustData(index, pCustData, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pCustData.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pCustData.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("00020412-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private ITypeInfo2 _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public ITypeInfo2Stub(ITypeInfo2 obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_GetTypeAttr, this.Invoke_GetTypeComp, this.Invoke_GetFuncDesc, this.Invoke_GetVarDesc, this.Invoke_GetNames, this.Invoke_GetRefTypeOfImplType, this.Invoke_GetImplTypeFlags, this.Invoke_Opnum10NotUsedOnWire, this.Invoke_Opnum11NotUsedOnWire, this.Invoke_GetDocumentation, this.Invoke_GetDllEntry, this.Invoke_GetRefTypeInfo, this.Invoke_Opnum15NotUsedOnWire, this.Invoke_CreateInstance, this.Invoke_GetMops, this.Invoke_GetContainingTypeLib, this.Invoke_Opnum19NotUsedOnWire, this.Invoke_Opnum20NotUsedOnWire, this.Invoke_Opnum21NotUsedOnWire, this.Invoke_GetTypeKind, this.Invoke_GetTypeFlags, this.Invoke_GetFuncIndexOfMemId, this.Invoke_GetVarIndexOfMemId, this.Invoke_GetCustData, this.Invoke_GetFuncCustData, this.Invoke_GetParamCustData, this.Invoke_GetVarCustData, this.Invoke_GetImplTypeCustData, this.Invoke_GetDocumentation2, this.Invoke_GetAllCustData, this.Invoke_GetAllFuncCustData, this.Invoke_GetAllParamCustData, this.Invoke_GetAllVarCustData, this.Invoke_GetAllImplTypeCustData};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("00020402-0000-0000-c000-000000000046"), RpcVersionAttribute(0, 0)]
	public partial interface ITypeLib : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetTypeInfoCount(RpcPointer<uint> pcTInfo, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetTypeInfo(uint index, RpcPointer<TypedObjref<ITypeInfo>> ppTInfo, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetTypeInfoType(uint index, RpcPointer<TYPEKIND> pTKind, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetTypeInfoOfGuid(Guid guid, RpcPointer<TypedObjref<ITypeInfo>> ppTInfo, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetLibAttr(RpcPointer<RpcPointer<TLIBATTR>> ppTLibAttr, RpcPointer<uint> pReserved, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetTypeComp(RpcPointer<TypedObjref<ITypeComp>> ppTComp, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetDocumentation(int index, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDocString, RpcPointer<uint> pdwHelpContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrHelpFile, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> IsName(string szNameBuf, uint lHashVal, RpcPointer<int> pfName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> FindName(string szNameBuf, uint lHashVal, RpcPointer<ArraySegment<TypedObjref<ITypeInfo>>> ppTInfo, RpcPointer<ArraySegment<int>> rgMemId, RpcPointer<ushort> pcFound, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Opnum12NotUsedOnWire(CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("00020402-0000-0000-c000-000000000046")]
	public partial class ITypeLibClientProxy : IUnknownClientProxy, ITypeLib
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetTypeInfoCount(RpcPointer<uint> pcTInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pcTInfo.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetTypeInfo(uint index, RpcPointer<TypedObjref<ITypeInfo>> ppTInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppTInfo.value = decoder.ReadInterfacePointer<ITypeInfo>();
			decoder.ReadInterfacePointer(ppTInfo.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetTypeInfoType(uint index, RpcPointer<TYPEKIND> pTKind, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pTKind.value = (TYPEKIND)decoder.ReadInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetTypeInfoOfGuid(Guid guid, RpcPointer<TypedObjref<ITypeInfo>> ppTInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(guid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppTInfo.value = decoder.ReadInterfacePointer<ITypeInfo>();
			decoder.ReadInterfacePointer(ppTInfo.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetLibAttr(RpcPointer<RpcPointer<TLIBATTR>> ppTLibAttr, RpcPointer<uint> pReserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppTLibAttr.value = decoder.ReadOutUniquePointer<TLIBATTR>(ppTLibAttr.value);
			if (ppTLibAttr.value is not null)
			{
				ppTLibAttr.value.value = decoder.ReadFixedStruct<TLIBATTR>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<TLIBATTR>(ref ppTLibAttr.value.value);
			}

			pReserved.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetTypeComp(RpcPointer<TypedObjref<ITypeComp>> ppTComp, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppTComp.value = decoder.ReadInterfacePointer<ITypeComp>();
			decoder.ReadInterfacePointer(ppTComp.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetDocumentation(int index, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDocString, RpcPointer<uint> pdwHelpContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrHelpFile, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			encoder.WriteValue(refPtrFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pBstrName.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrName.value);
			if (pBstrName.value is not null)
			{
				pBstrName.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrName.value.value);
			}

			pBstrDocString.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrDocString.value);
			if (pBstrDocString.value is not null)
			{
				pBstrDocString.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrDocString.value.value);
			}

			pdwHelpContext.value = decoder.ReadUInt32();
			pBstrHelpFile.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrHelpFile.value);
			if (pBstrHelpFile.value is not null)
			{
				pBstrHelpFile.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrHelpFile.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> IsName(string szNameBuf, uint lHashVal, RpcPointer<int> pfName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(szNameBuf);
			encoder.WriteValue(lHashVal);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pfName.value = decoder.ReadInt32();
			pBstrNameInLibrary.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrNameInLibrary.value);
			if (pBstrNameInLibrary.value is not null)
			{
				pBstrNameInLibrary.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrNameInLibrary.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> FindName(string szNameBuf, uint lHashVal, RpcPointer<ArraySegment<TypedObjref<ITypeInfo>>> ppTInfo, RpcPointer<ArraySegment<int>> rgMemId, RpcPointer<ushort> pcFound, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(szNameBuf);
			encoder.WriteValue(lHashVal);
			encoder.WriteValue(pcFound.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppTInfo.value = decoder.ReadArraySegmentHeader<TypedObjref<ITypeInfo>>();
			for (int i = 0; i < ppTInfo.value.Count; i++)
			{
				TypedObjref<ITypeInfo> elem_0 = ppTInfo.value.Item(i);
				elem_0 = decoder.ReadInterfacePointer<ITypeInfo>();
				ppTInfo.value.Item(i) = elem_0;
			}

			for (int i = 0; i < ppTInfo.value.Count; i++)
			{
				TypedObjref<ITypeInfo> elem_0 = ppTInfo.value.Item(i);
				decoder.ReadInterfacePointer(elem_0);
				ppTInfo.value.Item(i) = elem_0;
			}

			rgMemId.value = decoder.ReadArraySegmentHeader<int>();
			for (int i = 0; i < rgMemId.value.Count; i++)
			{
				int elem_0 = rgMemId.value.Item(i);
				elem_0 = decoder.ReadInt32();
				rgMemId.value.Item(i) = elem_0;
			}

			pcFound.value = decoder.ReadUInt16();
			pBstrNameInLibrary.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrNameInLibrary.value);
			if (pBstrNameInLibrary.value is not null)
			{
				pBstrNameInLibrary.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrNameInLibrary.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Opnum12NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public override Type InterfaceType => typeof(ITypeLib);
		private static Guid _interfaceUuid = new Guid("00020402-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class ITypeLibStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeInfoCount(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pcTInfo = new RpcPointer<uint>();
			var invokeTask = this._obj.GetTypeInfoCount(pcTInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pcTInfo.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<TypedObjref<ITypeInfo>>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetTypeInfo(index, ppTInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTInfo.value);
			encoder.WriteInterfacePointerBody(ppTInfo.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeInfoType(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<TYPEKIND> pTKind = new RpcPointer<TYPEKIND>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetTypeInfoType(index, pTKind, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pTKind.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeInfoOfGuid(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid guid;
			RpcPointer<TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<TypedObjref<ITypeInfo>>();
			guid = decoder.ReadUuid();
			var invokeTask = this._obj.GetTypeInfoOfGuid(guid, ppTInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTInfo.value);
			encoder.WriteInterfacePointerBody(ppTInfo.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetLibAttr(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<TLIBATTR>> ppTLibAttr = new RpcPointer<RpcPointer<TLIBATTR>>();
			RpcPointer<uint> pReserved = new RpcPointer<uint>();
			var invokeTask = this._obj.GetLibAttr(ppTLibAttr, pReserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppTLibAttr.value);
			if (ppTLibAttr.value is not null)
			{
				encoder.WriteFixedStruct(ppTLibAttr.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(ppTLibAttr.value.value);
			}

			encoder.WriteValue(pReserved.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeComp(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<TypedObjref<ITypeComp>> ppTComp = new RpcPointer<TypedObjref<ITypeComp>>();
			var invokeTask = this._obj.GetTypeComp(ppTComp, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTComp.value);
			encoder.WriteInterfacePointerBody(ppTComp.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetDocumentation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int index;
			uint refPtrFlags;
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDocString = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<uint> pdwHelpContext = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrHelpFile = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			index = decoder.ReadInt32();
			refPtrFlags = decoder.ReadUInt32();
			var invokeTask = this._obj.GetDocumentation(index, refPtrFlags, pBstrName, pBstrDocString, pdwHelpContext, pBstrHelpFile, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pBstrName.value);
			if (pBstrName.value is not null)
			{
				encoder.WriteConformantStruct(pBstrName.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrName.value.value);
			}

			encoder.WriteUniquePointer(pBstrDocString.value);
			if (pBstrDocString.value is not null)
			{
				encoder.WriteConformantStruct(pBstrDocString.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrDocString.value.value);
			}

			encoder.WriteValue(pdwHelpContext.value);
			encoder.WriteUniquePointer(pBstrHelpFile.value);
			if (pBstrHelpFile.value is not null)
			{
				encoder.WriteConformantStruct(pBstrHelpFile.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrHelpFile.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_IsName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string szNameBuf;
			uint lHashVal;
			RpcPointer<int> pfName = new RpcPointer<int>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			szNameBuf = decoder.ReadWideCharString();
			lHashVal = decoder.ReadUInt32();
			var invokeTask = this._obj.IsName(szNameBuf, lHashVal, pfName, pBstrNameInLibrary, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pfName.value);
			encoder.WriteUniquePointer(pBstrNameInLibrary.value);
			if (pBstrNameInLibrary.value is not null)
			{
				encoder.WriteConformantStruct(pBstrNameInLibrary.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrNameInLibrary.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_FindName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string szNameBuf;
			uint lHashVal;
			RpcPointer<ArraySegment<TypedObjref<ITypeInfo>>> ppTInfo = new RpcPointer<ArraySegment<TypedObjref<ITypeInfo>>>();
			RpcPointer<ArraySegment<int>> rgMemId = new RpcPointer<ArraySegment<int>>();
			RpcPointer<ushort> pcFound;
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			szNameBuf = decoder.ReadWideCharString();
			lHashVal = decoder.ReadUInt32();
			pcFound = new RpcPointer<ushort>();
			pcFound.value = decoder.ReadUInt16();
			var invokeTask = this._obj.FindName(szNameBuf, lHashVal, ppTInfo, rgMemId, pcFound, pBstrNameInLibrary, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(ppTInfo.value, true);
			for (int i = 0; i < ppTInfo.value.Count; i++)
			{
				TypedObjref<ITypeInfo> elem_0 = ppTInfo.value.Item(i);
				encoder.WriteInterfacePointer(elem_0);
			}

			for (int i = 0; i < ppTInfo.value.Count; i++)
			{
				TypedObjref<ITypeInfo> elem_0 = ppTInfo.value.Item(i);
				encoder.WriteInterfacePointerBody(elem_0);
			}

			encoder.WriteArrayHeader(rgMemId.value, true);
			for (int i = 0; i < rgMemId.value.Count; i++)
			{
				int elem_0 = rgMemId.value.Item(i);
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcFound.value);
			encoder.WriteUniquePointer(pBstrNameInLibrary.value);
			if (pBstrNameInLibrary.value is not null)
			{
				encoder.WriteConformantStruct(pBstrNameInLibrary.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrNameInLibrary.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum12NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum12NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("00020402-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private ITypeLib _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public ITypeLibStub(ITypeLib obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_GetTypeInfoCount, this.Invoke_GetTypeInfo, this.Invoke_GetTypeInfoType, this.Invoke_GetTypeInfoOfGuid, this.Invoke_GetLibAttr, this.Invoke_GetTypeComp, this.Invoke_GetDocumentation, this.Invoke_IsName, this.Invoke_FindName, this.Invoke_Opnum12NotUsedOnWire};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("00020411-0000-0000-c000-000000000046"), RpcVersionAttribute(0, 0)]
	public partial interface ITypeLib2 : ITypeLib
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetCustData(Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetLibStatistics(RpcPointer<uint> pcUniqueNames, RpcPointer<uint> pcchUniqueNames, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetDocumentation2(int index, uint lcid, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpString, RpcPointer<uint> pdwHelpStringContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpStringDll, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetAllCustData(RpcPointer<CUSTDATA> pCustData, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("00020411-0000-0000-c000-000000000046")]
	public partial class ITypeLib2ClientProxy : ITypeLibClientProxy, ITypeLib2
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetCustData(Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(guid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pVarVal.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarVal.value);
			if (pVarVal.value is not null)
			{
				pVarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarVal.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetLibStatistics(RpcPointer<uint> pcUniqueNames, RpcPointer<uint> pcchUniqueNames, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pcUniqueNames.value = decoder.ReadUInt32();
			pcchUniqueNames.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetDocumentation2(int index, uint lcid, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpString, RpcPointer<uint> pdwHelpStringContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpStringDll, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(index);
			encoder.WriteValue(lcid);
			encoder.WriteValue(refPtrFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pbstrHelpString.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pbstrHelpString.value);
			if (pbstrHelpString.value is not null)
			{
				pbstrHelpString.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pbstrHelpString.value.value);
			}

			pdwHelpStringContext.value = decoder.ReadUInt32();
			pbstrHelpStringDll.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pbstrHelpStringDll.value);
			if (pbstrHelpStringDll.value is not null)
			{
				pbstrHelpStringDll.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pbstrHelpStringDll.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetAllCustData(RpcPointer<CUSTDATA> pCustData, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pCustData.value = decoder.ReadFixedStruct<CUSTDATA>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<CUSTDATA>(ref pCustData.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(ITypeLib2);
		private static Guid _interfaceUuid = new Guid("00020411-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class ITypeLib2Stub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeInfoCount(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pcTInfo = new RpcPointer<uint>();
			var invokeTask = this._obj.GetTypeInfoCount(pcTInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pcTInfo.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<TypedObjref<ITypeInfo>>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetTypeInfo(index, ppTInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTInfo.value);
			encoder.WriteInterfacePointerBody(ppTInfo.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeInfoType(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint index;
			RpcPointer<TYPEKIND> pTKind = new RpcPointer<TYPEKIND>();
			index = decoder.ReadUInt32();
			var invokeTask = this._obj.GetTypeInfoType(index, pTKind, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue((int)pTKind.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeInfoOfGuid(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid guid;
			RpcPointer<TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<TypedObjref<ITypeInfo>>();
			guid = decoder.ReadUuid();
			var invokeTask = this._obj.GetTypeInfoOfGuid(guid, ppTInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTInfo.value);
			encoder.WriteInterfacePointerBody(ppTInfo.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetLibAttr(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<TLIBATTR>> ppTLibAttr = new RpcPointer<RpcPointer<TLIBATTR>>();
			RpcPointer<uint> pReserved = new RpcPointer<uint>();
			var invokeTask = this._obj.GetLibAttr(ppTLibAttr, pReserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppTLibAttr.value);
			if (ppTLibAttr.value is not null)
			{
				encoder.WriteFixedStruct(ppTLibAttr.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(ppTLibAttr.value.value);
			}

			encoder.WriteValue(pReserved.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetTypeComp(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<TypedObjref<ITypeComp>> ppTComp = new RpcPointer<TypedObjref<ITypeComp>>();
			var invokeTask = this._obj.GetTypeComp(ppTComp, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppTComp.value);
			encoder.WriteInterfacePointerBody(ppTComp.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetDocumentation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int index;
			uint refPtrFlags;
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDocString = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<uint> pdwHelpContext = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrHelpFile = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			index = decoder.ReadInt32();
			refPtrFlags = decoder.ReadUInt32();
			var invokeTask = this._obj.GetDocumentation(index, refPtrFlags, pBstrName, pBstrDocString, pdwHelpContext, pBstrHelpFile, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pBstrName.value);
			if (pBstrName.value is not null)
			{
				encoder.WriteConformantStruct(pBstrName.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrName.value.value);
			}

			encoder.WriteUniquePointer(pBstrDocString.value);
			if (pBstrDocString.value is not null)
			{
				encoder.WriteConformantStruct(pBstrDocString.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrDocString.value.value);
			}

			encoder.WriteValue(pdwHelpContext.value);
			encoder.WriteUniquePointer(pBstrHelpFile.value);
			if (pBstrHelpFile.value is not null)
			{
				encoder.WriteConformantStruct(pBstrHelpFile.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrHelpFile.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_IsName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string szNameBuf;
			uint lHashVal;
			RpcPointer<int> pfName = new RpcPointer<int>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			szNameBuf = decoder.ReadWideCharString();
			lHashVal = decoder.ReadUInt32();
			var invokeTask = this._obj.IsName(szNameBuf, lHashVal, pfName, pBstrNameInLibrary, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pfName.value);
			encoder.WriteUniquePointer(pBstrNameInLibrary.value);
			if (pBstrNameInLibrary.value is not null)
			{
				encoder.WriteConformantStruct(pBstrNameInLibrary.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrNameInLibrary.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_FindName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string szNameBuf;
			uint lHashVal;
			RpcPointer<ArraySegment<TypedObjref<ITypeInfo>>> ppTInfo = new RpcPointer<ArraySegment<TypedObjref<ITypeInfo>>>();
			RpcPointer<ArraySegment<int>> rgMemId = new RpcPointer<ArraySegment<int>>();
			RpcPointer<ushort> pcFound;
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			szNameBuf = decoder.ReadWideCharString();
			lHashVal = decoder.ReadUInt32();
			pcFound = new RpcPointer<ushort>();
			pcFound.value = decoder.ReadUInt16();
			var invokeTask = this._obj.FindName(szNameBuf, lHashVal, ppTInfo, rgMemId, pcFound, pBstrNameInLibrary, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(ppTInfo.value, true);
			for (int i = 0; i < ppTInfo.value.Count; i++)
			{
				TypedObjref<ITypeInfo> elem_0 = ppTInfo.value.Item(i);
				encoder.WriteInterfacePointer(elem_0);
			}

			for (int i = 0; i < ppTInfo.value.Count; i++)
			{
				TypedObjref<ITypeInfo> elem_0 = ppTInfo.value.Item(i);
				encoder.WriteInterfacePointerBody(elem_0);
			}

			encoder.WriteArrayHeader(rgMemId.value, true);
			for (int i = 0; i < rgMemId.value.Count; i++)
			{
				int elem_0 = rgMemId.value.Item(i);
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcFound.value);
			encoder.WriteUniquePointer(pBstrNameInLibrary.value);
			if (pBstrNameInLibrary.value is not null)
			{
				encoder.WriteConformantStruct(pBstrNameInLibrary.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pBstrNameInLibrary.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum12NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum12NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetCustData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid guid;
			RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal = new RpcPointer<RpcPointer<wireVARIANTStr>>();
			guid = decoder.ReadUuid();
			var invokeTask = this._obj.GetCustData(guid, pVarVal, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pVarVal.value);
			if (pVarVal.value is not null)
			{
				encoder.WriteFixedStruct(pVarVal.value.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(pVarVal.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetLibStatistics(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pcUniqueNames = new RpcPointer<uint>();
			RpcPointer<uint> pcchUniqueNames = new RpcPointer<uint>();
			var invokeTask = this._obj.GetLibStatistics(pcUniqueNames, pcchUniqueNames, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pcUniqueNames.value);
			encoder.WriteValue(pcchUniqueNames.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetDocumentation2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int index;
			uint lcid;
			uint refPtrFlags;
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpString = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			RpcPointer<uint> pdwHelpStringContext = new RpcPointer<uint>();
			RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpStringDll = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
			index = decoder.ReadInt32();
			lcid = decoder.ReadUInt32();
			refPtrFlags = decoder.ReadUInt32();
			var invokeTask = this._obj.GetDocumentation2(index, lcid, refPtrFlags, pbstrHelpString, pdwHelpStringContext, pbstrHelpStringDll, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pbstrHelpString.value);
			if (pbstrHelpString.value is not null)
			{
				encoder.WriteConformantStruct(pbstrHelpString.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pbstrHelpString.value.value);
			}

			encoder.WriteValue(pdwHelpStringContext.value);
			encoder.WriteUniquePointer(pbstrHelpStringDll.value);
			if (pbstrHelpStringDll.value is not null)
			{
				encoder.WriteConformantStruct(pbstrHelpStringDll.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pbstrHelpStringDll.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetAllCustData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<CUSTDATA> pCustData = new RpcPointer<CUSTDATA>();
			var invokeTask = this._obj.GetAllCustData(pCustData, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pCustData.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pCustData.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("00020411-0000-0000-c000-000000000046");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private ITypeLib2 _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public ITypeLib2Stub(ITypeLib2 obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_GetTypeInfoCount, this.Invoke_GetTypeInfo, this.Invoke_GetTypeInfoType, this.Invoke_GetTypeInfoOfGuid, this.Invoke_GetLibAttr, this.Invoke_GetTypeComp, this.Invoke_GetDocumentation, this.Invoke_IsName, this.Invoke_FindName, this.Invoke_Opnum12NotUsedOnWire, this.Invoke_GetCustData, this.Invoke_GetLibStatistics, this.Invoke_GetDocumentation2, this.Invoke_GetAllCustData};
		}
	}
}