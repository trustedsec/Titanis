namespace MS_EVEN6
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct RpcInfo : IRpcFixedStruct
	{
		public uint m_error;
		public uint m_subErr;
		public uint m_subErrParam;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.m_error);
			encoder.WriteValue(this.m_subErr);
			encoder.WriteValue(this.m_subErrParam);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.m_error = decoder.ReadUInt32();
			this.m_subErr = decoder.ReadUInt32();
			this.m_subErrParam = decoder.ReadUInt32();
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
	public partial struct BooleanArray : IRpcFixedStruct
	{
		public uint count;
		public RpcPointer<bool[]> ptr;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.count);
			encoder.WriteUniquePointer(this.ptr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.count = decoder.ReadUInt32();
			this.ptr = decoder.ReadUniquePointer<bool[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ptr is not null)
			{
				encoder.WriteArrayHeader(this.ptr.value);
				for (int i = 0; i < this.ptr.value.Length; i++)
				{
					bool elem_0 = this.ptr.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ptr is not null)
			{
				this.ptr.value = decoder.ReadArrayHeader<bool>();
				for (int i = 0; i < this.ptr.value.Length; i++)
				{
					bool elem_0 = this.ptr.value[i];
					elem_0 = decoder.ReadBoolean();
					this.ptr.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct UInt32Array : IRpcFixedStruct
	{
		public uint count;
		public RpcPointer<uint[]> ptr;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.count);
			encoder.WriteUniquePointer(this.ptr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.count = decoder.ReadUInt32();
			this.ptr = decoder.ReadUniquePointer<uint[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ptr is not null)
			{
				encoder.WriteArrayHeader(this.ptr.value);
				for (int i = 0; i < this.ptr.value.Length; i++)
				{
					uint elem_0 = this.ptr.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ptr is not null)
			{
				this.ptr.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < this.ptr.value.Length; i++)
				{
					uint elem_0 = this.ptr.value[i];
					elem_0 = decoder.ReadUInt32();
					this.ptr.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct UInt64Array : IRpcFixedStruct
	{
		public uint count;
		public RpcPointer<ulong[]> ptr;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.count);
			encoder.WriteUniquePointer(this.ptr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.count = decoder.ReadUInt32();
			this.ptr = decoder.ReadUniquePointer<ulong[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ptr is not null)
			{
				encoder.WriteArrayHeader(this.ptr.value);
				for (int i = 0; i < this.ptr.value.Length; i++)
				{
					ulong elem_0 = this.ptr.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ptr is not null)
			{
				this.ptr.value = decoder.ReadArrayHeader<ulong>();
				for (int i = 0; i < this.ptr.value.Length; i++)
				{
					ulong elem_0 = this.ptr.value[i];
					elem_0 = decoder.ReadUInt64();
					this.ptr.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct StringArray : IRpcFixedStruct
	{
		public uint count;
		public RpcPointer<RpcPointer<string>[]> ptr;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.count);
			encoder.WriteUniquePointer(this.ptr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.count = decoder.ReadUInt32();
			this.ptr = decoder.ReadUniquePointer<RpcPointer<string>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ptr is not null)
			{
				encoder.WriteArrayHeader(this.ptr.value);
				for (int i = 0; i < this.ptr.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.ptr.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.ptr.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.ptr.value[i];
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
			if (this.ptr is not null)
			{
				this.ptr.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < this.ptr.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.ptr.value[i];
					elem_0 = decoder.ReadUniquePointer<string>();
					this.ptr.value[i] = elem_0;
				}

				for (int i = 0; i < this.ptr.value.Length; i++)
				{
					RpcPointer<string> elem_0 = this.ptr.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadWideCharString();
					}

					this.ptr.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct GuidArray : IRpcFixedStruct
	{
		public uint count;
		public RpcPointer<Guid[]> ptr;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.count);
			encoder.WriteUniquePointer(this.ptr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.count = decoder.ReadUInt32();
			this.ptr = decoder.ReadUniquePointer<Guid[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ptr is not null)
			{
				encoder.WriteArrayHeader(this.ptr.value);
				for (int i = 0; i < this.ptr.value.Length; i++)
				{
					Guid elem_0 = this.ptr.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ptr is not null)
			{
				this.ptr.value = decoder.ReadArrayHeader<Guid>();
				for (int i = 0; i < this.ptr.value.Length; i++)
				{
					Guid elem_0 = this.ptr.value[i];
					elem_0 = decoder.ReadUuid();
					this.ptr.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum EvtRpcVariantType : int
	{
		EvtRpcVarTypeNull = 0,
		EvtRpcVarTypeBoolean = 1,
		EvtRpcVarTypeUInt32 = 2,
		EvtRpcVarTypeUInt64 = 3,
		EvtRpcVarTypeString = 4,
		EvtRpcVarTypeGuid = 5,
		EvtRpcVarTypeBooleanArray = 6,
		EvtRpcVarTypeUInt32Array = 7,
		EvtRpcVarTypeUInt64Array = 8,
		EvtRpcVarTypeStringArray = 9,
		EvtRpcVarTypeGuidArray = 10
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum EvtRpcAssertConfigFlags : int
	{
		EvtRpcChannelPath = 0,
		EvtRpcPublisherName = 1
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct Unnamed_1 : IRpcFixedStruct
	{
		public EvtRpcVariantType type;
		public int nullVal;
		public bool booleanVal;
		public uint uint32Val;
		public ulong uint64Val;
		public RpcPointer<string> stringVal;
		public RpcPointer<Guid> guidVal;
		public BooleanArray booleanArray;
		public UInt32Array uint32Array;
		public UInt64Array uint64Array;
		public StringArray stringArray;
		public GuidArray guidArray;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._8Byte);
			encoder.WriteValue((int)this.type);
			switch ((int)this.type)
			{
				case 0:
					encoder.WriteValue(this.nullVal);
					break;
				case 1:
					encoder.WriteValue(this.booleanVal);
					break;
				case 2:
					encoder.WriteValue(this.uint32Val);
					break;
				case 3:
					encoder.WriteValue(this.uint64Val);
					break;
				case 4:
					encoder.WriteUniquePointer(this.stringVal);
					break;
				case 5:
					encoder.WriteUniquePointer(this.guidVal);
					break;
				case 6:
					encoder.WriteFixedStruct(this.booleanArray, NdrAlignment.NativePtr);
					break;
				case 7:
					encoder.WriteFixedStruct(this.uint32Array, NdrAlignment.NativePtr);
					break;
				case 8:
					encoder.WriteFixedStruct(this.uint64Array, NdrAlignment.NativePtr);
					break;
				case 9:
					encoder.WriteFixedStruct(this.stringArray, NdrAlignment.NativePtr);
					break;
				case 10:
					encoder.WriteFixedStruct(this.guidArray, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._8Byte);
			this.type = (EvtRpcVariantType)decoder.ReadInt32();
			switch ((int)this.type)
			{
				case 0:
					this.nullVal = decoder.ReadInt32();
					break;
				case 1:
					this.booleanVal = decoder.ReadBoolean();
					break;
				case 2:
					this.uint32Val = decoder.ReadUInt32();
					break;
				case 3:
					this.uint64Val = decoder.ReadUInt64();
					break;
				case 4:
					this.stringVal = decoder.ReadUniquePointer<string>();
					break;
				case 5:
					this.guidVal = decoder.ReadUniquePointer<Guid>();
					break;
				case 6:
					this.booleanArray = decoder.ReadFixedStruct<BooleanArray>(NdrAlignment.NativePtr);
					break;
				case 7:
					this.uint32Array = decoder.ReadFixedStruct<UInt32Array>(NdrAlignment.NativePtr);
					break;
				case 8:
					this.uint64Array = decoder.ReadFixedStruct<UInt64Array>(NdrAlignment.NativePtr);
					break;
				case 9:
					this.stringArray = decoder.ReadFixedStruct<StringArray>(NdrAlignment.NativePtr);
					break;
				case 10:
					this.guidArray = decoder.ReadFixedStruct<GuidArray>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.type)
			{
				case 0:
					break;
				case 1:
					break;
				case 2:
					break;
				case 3:
					break;
				case 4:
					if (this.stringVal is not null)
					{
						encoder.WriteWideCharString(this.stringVal.value);
					}

					break;
				case 5:
					if (this.guidVal is not null)
					{
						encoder.WriteValue(this.guidVal.value);
					}

					break;
				case 6:
					encoder.WriteStructDeferral(this.booleanArray);
					break;
				case 7:
					encoder.WriteStructDeferral(this.uint32Array);
					break;
				case 8:
					encoder.WriteStructDeferral(this.uint64Array);
					break;
				case 9:
					encoder.WriteStructDeferral(this.stringArray);
					break;
				case 10:
					encoder.WriteStructDeferral(this.guidArray);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.type)
			{
				case 0:
					break;
				case 1:
					break;
				case 2:
					break;
				case 3:
					break;
				case 4:
					if (this.stringVal is not null)
					{
						this.stringVal.value = decoder.ReadWideCharString();
					}

					break;
				case 5:
					if (this.guidVal is not null)
					{
						this.guidVal.value = decoder.ReadUuid();
					}

					break;
				case 6:
					decoder.ReadStructDeferral<BooleanArray>(ref this.booleanArray);
					break;
				case 7:
					decoder.ReadStructDeferral<UInt32Array>(ref this.uint32Array);
					break;
				case 8:
					decoder.ReadStructDeferral<UInt64Array>(ref this.uint64Array);
					break;
				case 9:
					decoder.ReadStructDeferral<StringArray>(ref this.stringArray);
					break;
				case 10:
					decoder.ReadStructDeferral<GuidArray>(ref this.guidArray);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct EvtRpcVariant : IRpcFixedStruct
	{
		public EvtRpcVariantType type;
		public uint flags;
		public Unnamed_1 unnamed_1;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue((int)this.type);
			encoder.WriteValue(this.flags);
			encoder.WriteUnion(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.type = (EvtRpcVariantType)decoder.ReadInt32();
			this.flags = decoder.ReadUInt32();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_1>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<Unnamed_1>(ref this.unnamed_1);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct EvtRpcVariantList : IRpcFixedStruct
	{
		public uint count;
		public RpcPointer<EvtRpcVariant[]> props;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.count);
			encoder.WriteUniquePointer(this.props);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.count = decoder.ReadUInt32();
			this.props = decoder.ReadUniquePointer<EvtRpcVariant[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.props is not null)
			{
				encoder.WriteArrayHeader(this.props.value);
				for (int i = 0; i < this.props.value.Length; i++)
				{
					EvtRpcVariant elem_0 = this.props.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._8Byte);
				}

				for (int i = 0; i < this.props.value.Length; i++)
				{
					EvtRpcVariant elem_0 = this.props.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.props is not null)
			{
				this.props.value = decoder.ReadArrayHeader<EvtRpcVariant>();
				for (int i = 0; i < this.props.value.Length; i++)
				{
					EvtRpcVariant elem_0 = this.props.value[i];
					elem_0 = decoder.ReadFixedStruct<EvtRpcVariant>(NdrAlignment._8Byte);
					this.props.value[i] = elem_0;
				}

				for (int i = 0; i < this.props.value.Length; i++)
				{
					EvtRpcVariant elem_0 = this.props.value[i];
					decoder.ReadStructDeferral<EvtRpcVariant>(ref elem_0);
					this.props.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct EvtRpcQueryChannelInfo : IRpcFixedStruct
	{
		public RpcPointer<char> name;
		public uint status;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.name);
			encoder.WriteValue(this.status);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.name = decoder.ReadUniquePointer<char>();
			this.status = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.name is not null)
			{
				encoder.WriteValue(this.name.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.name is not null)
			{
				this.name.value = decoder.ReadWideChar();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("f6beaff7-1e19-4fbb-9f8f-b89e2018337c"), RpcVersionAttribute(1, 0)]
	public partial interface IEventService
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcRegisterRemoteSubscription(string channelPath, string query, string bookmarkXml, uint flags, RpcPointer<RpcContextHandle> handle, RpcPointer<RpcContextHandle> control, RpcPointer<uint> queryChannelInfoSize, RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>> queryChannelInfo, RpcPointer<RpcInfo> error, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcRemoteSubscriptionNextAsync(RpcContextHandle handle, uint numRequestedRecords, uint flags, RpcPointer<uint> numActualRecords, RpcPointer<RpcPointer<uint[]>> eventDataIndices, RpcPointer<RpcPointer<uint[]>> eventDataSizes, RpcPointer<uint> resultBufferSize, RpcPointer<RpcPointer<byte[]>> resultBuffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcRemoteSubscriptionNext(RpcContextHandle handle, uint numRequestedRecords, uint timeOut, uint flags, RpcPointer<uint> numActualRecords, RpcPointer<RpcPointer<uint[]>> eventDataIndices, RpcPointer<RpcPointer<uint[]>> eventDataSizes, RpcPointer<uint> resultBufferSize, RpcPointer<RpcPointer<byte[]>> resultBuffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcRemoteSubscriptionWaitAsync(RpcContextHandle handle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcRegisterControllableOperation(RpcPointer<RpcContextHandle> handle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcRegisterLogQuery(string path, string query, uint flags, RpcPointer<RpcContextHandle> handle, RpcPointer<RpcContextHandle> opControl, RpcPointer<uint> queryChannelInfoSize, RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>> queryChannelInfo, RpcPointer<RpcInfo> error, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcClearLog(RpcContextHandle control, string channelPath, string backupPath, uint flags, RpcPointer<RpcInfo> error, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcExportLog(RpcContextHandle control, string channelPath, string query, string backupPath, uint flags, RpcPointer<RpcInfo> error, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcLocalizeExportLog(RpcContextHandle control, string logFilePath, uint locale, uint flags, RpcPointer<RpcInfo> error, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcMessageRender(RpcContextHandle pubCfgObj, uint sizeEventId, byte[] eventId, uint messageId, EvtRpcVariantList values, uint flags, uint maxSizeString, RpcPointer<uint> actualSizeString, RpcPointer<uint> neededSizeString, RpcPointer<RpcPointer<byte[]>> @string, RpcPointer<RpcInfo> error, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcMessageRenderDefault(uint sizeEventId, byte[] eventId, uint messageId, EvtRpcVariantList values, uint flags, uint maxSizeString, RpcPointer<uint> actualSizeString, RpcPointer<uint> neededSizeString, RpcPointer<RpcPointer<byte[]>> @string, RpcPointer<RpcInfo> error, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcQueryNext(RpcContextHandle logQuery, uint numRequestedRecords, uint timeOutEnd, uint flags, RpcPointer<uint> numActualRecords, RpcPointer<RpcPointer<uint[]>> eventDataIndices, RpcPointer<RpcPointer<uint[]>> eventDataSizes, RpcPointer<uint> resultBufferSize, RpcPointer<RpcPointer<byte[]>> resultBuffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcQuerySeek(RpcContextHandle logQuery, long pos, string bookmarkXml, uint timeOut, uint flags, RpcPointer<RpcInfo> error, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcClose(RpcPointer<RpcContextHandle> handle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcCancel(RpcContextHandle handle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcAssertConfig(string path, uint flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcRetractConfig(string path, uint flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcOpenLogHandle(string channel, uint flags, RpcPointer<RpcContextHandle> handle, RpcPointer<RpcInfo> error, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcGetLogFileInfo(RpcContextHandle logHandle, uint propertyId, uint propertyValueBufferSize, RpcPointer<byte[]> propertyValueBuffer, RpcPointer<uint> propertyValueBufferLength, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcGetChannelList(uint flags, RpcPointer<uint> numChannelPaths, RpcPointer<RpcPointer<RpcPointer<string>[]>> channelPaths, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcGetChannelConfig(string channelPath, uint flags, RpcPointer<EvtRpcVariantList> props, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcPutChannelConfig(string channelPath, uint flags, EvtRpcVariantList props, RpcPointer<RpcInfo> error, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcGetPublisherList(uint flags, RpcPointer<uint> numPublisherIds, RpcPointer<RpcPointer<RpcPointer<string>[]>> publisherIds, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcGetPublisherListForChannel(char channelName, uint flags, RpcPointer<uint> numPublisherIds, RpcPointer<RpcPointer<RpcPointer<string>[]>> publisherIds, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcGetPublisherMetadata(string publisherId, string logFilePath, uint locale, uint flags, RpcPointer<EvtRpcVariantList> pubMetadataProps, RpcPointer<RpcContextHandle> pubMetadata, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcGetPublisherResourceMetadata(RpcContextHandle handle, uint propertyId, uint flags, RpcPointer<EvtRpcVariantList> pubMetadataProps, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcGetEventMetadataEnum(RpcContextHandle pubMetadata, uint flags, string reservedForFilter, RpcPointer<RpcContextHandle> eventMetaDataEnum, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcGetNextEventMetadata(RpcContextHandle eventMetaDataEnum, uint flags, uint numRequested, RpcPointer<uint> numReturned, RpcPointer<RpcPointer<EvtRpcVariantList[]>> eventMetadataInstances, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> EvtRpcGetClassicLogDisplayName(string logName, uint locale, uint flags, RpcPointer<RpcPointer<string>> displayName, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("f6beaff7-1e19-4fbb-9f8f-b89e2018337c")]
	public partial class IEventServiceClientProxy : Titanis.DceRpc.Client.RpcClientProxy, IEventService, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcRegisterRemoteSubscription(string channelPath, string query, string bookmarkXml, uint flags, RpcPointer<RpcContextHandle> handle, RpcPointer<RpcContextHandle> control, RpcPointer<uint> queryChannelInfoSize, RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>> queryChannelInfo, RpcPointer<RpcInfo> error, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(channelPath is null);
			if (channelPath is not null)
				encoder.WriteWideCharString(channelPath);
			encoder.WriteWideCharString(query);
			encoder.WriteUniqueReferentId(bookmarkXml is null);
			if (bookmarkXml is not null)
				encoder.WriteWideCharString(bookmarkXml);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			handle.value = decoder.ReadContextHandle();
			control.value = decoder.ReadContextHandle();
			queryChannelInfoSize.value = decoder.ReadUInt32();
			queryChannelInfo.value = decoder.ReadOutUniquePointer<EvtRpcQueryChannelInfo[]>(queryChannelInfo.value);
			if (queryChannelInfo.value is not null)
			{
				queryChannelInfo.value.value = decoder.ReadArrayHeader<EvtRpcQueryChannelInfo>();
				for (int i = 0; i < queryChannelInfo.value.value.Length; i++)
				{
					EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
					elem_0 = decoder.ReadFixedStruct<EvtRpcQueryChannelInfo>(NdrAlignment.NativePtr);
					queryChannelInfo.value.value[i] = elem_0;
				}

				for (int i = 0; i < queryChannelInfo.value.value.Length; i++)
				{
					EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
					decoder.ReadStructDeferral<EvtRpcQueryChannelInfo>(ref elem_0);
					queryChannelInfo.value.value[i] = elem_0;
				}
			}

			error.value = decoder.ReadFixedStruct<RpcInfo>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<RpcInfo>(ref error.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcRemoteSubscriptionNextAsync(RpcContextHandle handle, uint numRequestedRecords, uint flags, RpcPointer<uint> numActualRecords, RpcPointer<RpcPointer<uint[]>> eventDataIndices, RpcPointer<RpcPointer<uint[]>> eventDataSizes, RpcPointer<uint> resultBufferSize, RpcPointer<RpcPointer<byte[]>> resultBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(handle);
			encoder.WriteValue(numRequestedRecords);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			numActualRecords.value = decoder.ReadUInt32();
			eventDataIndices.value = decoder.ReadOutUniquePointer<uint[]>(eventDataIndices.value);
			if (eventDataIndices.value is not null)
			{
				eventDataIndices.value.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < eventDataIndices.value.value.Length; i++)
				{
					uint elem_0 = eventDataIndices.value.value[i];
					elem_0 = decoder.ReadUInt32();
					eventDataIndices.value.value[i] = elem_0;
				}
			}

			eventDataSizes.value = decoder.ReadOutUniquePointer<uint[]>(eventDataSizes.value);
			if (eventDataSizes.value is not null)
			{
				eventDataSizes.value.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < eventDataSizes.value.value.Length; i++)
				{
					uint elem_0 = eventDataSizes.value.value[i];
					elem_0 = decoder.ReadUInt32();
					eventDataSizes.value.value[i] = elem_0;
				}
			}

			resultBufferSize.value = decoder.ReadUInt32();
			resultBuffer.value = decoder.ReadOutUniquePointer<byte[]>(resultBuffer.value);
			if (resultBuffer.value is not null)
			{
				resultBuffer.value.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < resultBuffer.value.value.Length; i++)
				{
					byte elem_0 = resultBuffer.value.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					resultBuffer.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcRemoteSubscriptionNext(RpcContextHandle handle, uint numRequestedRecords, uint timeOut, uint flags, RpcPointer<uint> numActualRecords, RpcPointer<RpcPointer<uint[]>> eventDataIndices, RpcPointer<RpcPointer<uint[]>> eventDataSizes, RpcPointer<uint> resultBufferSize, RpcPointer<RpcPointer<byte[]>> resultBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(handle);
			encoder.WriteValue(numRequestedRecords);
			encoder.WriteValue(timeOut);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			numActualRecords.value = decoder.ReadUInt32();
			eventDataIndices.value = decoder.ReadOutUniquePointer<uint[]>(eventDataIndices.value);
			if (eventDataIndices.value is not null)
			{
				eventDataIndices.value.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < eventDataIndices.value.value.Length; i++)
				{
					uint elem_0 = eventDataIndices.value.value[i];
					elem_0 = decoder.ReadUInt32();
					eventDataIndices.value.value[i] = elem_0;
				}
			}

			eventDataSizes.value = decoder.ReadOutUniquePointer<uint[]>(eventDataSizes.value);
			if (eventDataSizes.value is not null)
			{
				eventDataSizes.value.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < eventDataSizes.value.value.Length; i++)
				{
					uint elem_0 = eventDataSizes.value.value[i];
					elem_0 = decoder.ReadUInt32();
					eventDataSizes.value.value[i] = elem_0;
				}
			}

			resultBufferSize.value = decoder.ReadUInt32();
			resultBuffer.value = decoder.ReadOutUniquePointer<byte[]>(resultBuffer.value);
			if (resultBuffer.value is not null)
			{
				resultBuffer.value.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < resultBuffer.value.value.Length; i++)
				{
					byte elem_0 = resultBuffer.value.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					resultBuffer.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcRemoteSubscriptionWaitAsync(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(handle);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcRegisterControllableOperation(RpcPointer<RpcContextHandle> handle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			handle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcRegisterLogQuery(string path, string query, uint flags, RpcPointer<RpcContextHandle> handle, RpcPointer<RpcContextHandle> opControl, RpcPointer<uint> queryChannelInfoSize, RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>> queryChannelInfo, RpcPointer<RpcInfo> error, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(path is null);
			if (path is not null)
				encoder.WriteWideCharString(path);
			encoder.WriteWideCharString(query);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			handle.value = decoder.ReadContextHandle();
			opControl.value = decoder.ReadContextHandle();
			queryChannelInfoSize.value = decoder.ReadUInt32();
			queryChannelInfo.value = decoder.ReadOutUniquePointer<EvtRpcQueryChannelInfo[]>(queryChannelInfo.value);
			if (queryChannelInfo.value is not null)
			{
				queryChannelInfo.value.value = decoder.ReadArrayHeader<EvtRpcQueryChannelInfo>();
				for (int i = 0; i < queryChannelInfo.value.value.Length; i++)
				{
					EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
					elem_0 = decoder.ReadFixedStruct<EvtRpcQueryChannelInfo>(NdrAlignment.NativePtr);
					queryChannelInfo.value.value[i] = elem_0;
				}

				for (int i = 0; i < queryChannelInfo.value.value.Length; i++)
				{
					EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
					decoder.ReadStructDeferral<EvtRpcQueryChannelInfo>(ref elem_0);
					queryChannelInfo.value.value[i] = elem_0;
				}
			}

			error.value = decoder.ReadFixedStruct<RpcInfo>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<RpcInfo>(ref error.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcClearLog(RpcContextHandle control, string channelPath, string backupPath, uint flags, RpcPointer<RpcInfo> error, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(control);
			encoder.WriteWideCharString(channelPath);
			encoder.WriteUniqueReferentId(backupPath is null);
			if (backupPath is not null)
				encoder.WriteWideCharString(backupPath);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			error.value = decoder.ReadFixedStruct<RpcInfo>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<RpcInfo>(ref error.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcExportLog(RpcContextHandle control, string channelPath, string query, string backupPath, uint flags, RpcPointer<RpcInfo> error, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(control);
			encoder.WriteUniqueReferentId(channelPath is null);
			if (channelPath is not null)
				encoder.WriteWideCharString(channelPath);
			encoder.WriteWideCharString(query);
			encoder.WriteWideCharString(backupPath);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			error.value = decoder.ReadFixedStruct<RpcInfo>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<RpcInfo>(ref error.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcLocalizeExportLog(RpcContextHandle control, string logFilePath, uint locale, uint flags, RpcPointer<RpcInfo> error, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(control);
			encoder.WriteWideCharString(logFilePath);
			encoder.WriteValue(locale);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			error.value = decoder.ReadFixedStruct<RpcInfo>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<RpcInfo>(ref error.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcMessageRender(RpcContextHandle pubCfgObj, uint sizeEventId, byte[] eventId, uint messageId, EvtRpcVariantList values, uint flags, uint maxSizeString, RpcPointer<uint> actualSizeString, RpcPointer<uint> neededSizeString, RpcPointer<RpcPointer<byte[]>> @string, RpcPointer<RpcInfo> error, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(pubCfgObj);
			encoder.WriteValue(sizeEventId);
			if (eventId is not null)
			{
				encoder.WriteArrayHeader(eventId);
				for (int i = 0; i < eventId.Length; i++)
				{
					byte elem_0 = eventId[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(messageId);
			encoder.WriteFixedStruct(values, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(values);
			encoder.WriteValue(flags);
			encoder.WriteValue(maxSizeString);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			actualSizeString.value = decoder.ReadUInt32();
			neededSizeString.value = decoder.ReadUInt32();
			@string.value = decoder.ReadOutUniquePointer<byte[]>(@string.value);
			if (@string.value is not null)
			{
				@string.value.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < @string.value.value.Length; i++)
				{
					byte elem_0 = @string.value.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					@string.value.value[i] = elem_0;
				}
			}

			error.value = decoder.ReadFixedStruct<RpcInfo>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<RpcInfo>(ref error.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcMessageRenderDefault(uint sizeEventId, byte[] eventId, uint messageId, EvtRpcVariantList values, uint flags, uint maxSizeString, RpcPointer<uint> actualSizeString, RpcPointer<uint> neededSizeString, RpcPointer<RpcPointer<byte[]>> @string, RpcPointer<RpcInfo> error, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(sizeEventId);
			if (eventId is not null)
			{
				encoder.WriteArrayHeader(eventId);
				for (int i = 0; i < eventId.Length; i++)
				{
					byte elem_0 = eventId[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(messageId);
			encoder.WriteFixedStruct(values, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(values);
			encoder.WriteValue(flags);
			encoder.WriteValue(maxSizeString);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			actualSizeString.value = decoder.ReadUInt32();
			neededSizeString.value = decoder.ReadUInt32();
			@string.value = decoder.ReadOutUniquePointer<byte[]>(@string.value);
			if (@string.value is not null)
			{
				@string.value.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < @string.value.value.Length; i++)
				{
					byte elem_0 = @string.value.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					@string.value.value[i] = elem_0;
				}
			}

			error.value = decoder.ReadFixedStruct<RpcInfo>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<RpcInfo>(ref error.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcQueryNext(RpcContextHandle logQuery, uint numRequestedRecords, uint timeOutEnd, uint flags, RpcPointer<uint> numActualRecords, RpcPointer<RpcPointer<uint[]>> eventDataIndices, RpcPointer<RpcPointer<uint[]>> eventDataSizes, RpcPointer<uint> resultBufferSize, RpcPointer<RpcPointer<byte[]>> resultBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(logQuery);
			encoder.WriteValue(numRequestedRecords);
			encoder.WriteValue(timeOutEnd);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			numActualRecords.value = decoder.ReadUInt32();
			eventDataIndices.value = decoder.ReadOutUniquePointer<uint[]>(eventDataIndices.value);
			if (eventDataIndices.value is not null)
			{
				eventDataIndices.value.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < eventDataIndices.value.value.Length; i++)
				{
					uint elem_0 = eventDataIndices.value.value[i];
					elem_0 = decoder.ReadUInt32();
					eventDataIndices.value.value[i] = elem_0;
				}
			}

			eventDataSizes.value = decoder.ReadOutUniquePointer<uint[]>(eventDataSizes.value);
			if (eventDataSizes.value is not null)
			{
				eventDataSizes.value.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < eventDataSizes.value.value.Length; i++)
				{
					uint elem_0 = eventDataSizes.value.value[i];
					elem_0 = decoder.ReadUInt32();
					eventDataSizes.value.value[i] = elem_0;
				}
			}

			resultBufferSize.value = decoder.ReadUInt32();
			resultBuffer.value = decoder.ReadOutUniquePointer<byte[]>(resultBuffer.value);
			if (resultBuffer.value is not null)
			{
				resultBuffer.value.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < resultBuffer.value.value.Length; i++)
				{
					byte elem_0 = resultBuffer.value.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					resultBuffer.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcQuerySeek(RpcContextHandle logQuery, long pos, string bookmarkXml, uint timeOut, uint flags, RpcPointer<RpcInfo> error, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(logQuery);
			encoder.WriteValue(pos);
			encoder.WriteUniqueReferentId(bookmarkXml is null);
			if (bookmarkXml is not null)
				encoder.WriteWideCharString(bookmarkXml);
			encoder.WriteValue(timeOut);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			error.value = decoder.ReadFixedStruct<RpcInfo>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<RpcInfo>(ref error.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcClose(RpcPointer<RpcContextHandle> handle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(handle.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			handle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcCancel(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(handle);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcAssertConfig(string path, uint flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcRetractConfig(string path, uint flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcOpenLogHandle(string channel, uint flags, RpcPointer<RpcContextHandle> handle, RpcPointer<RpcInfo> error, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(channel);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			handle.value = decoder.ReadContextHandle();
			error.value = decoder.ReadFixedStruct<RpcInfo>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<RpcInfo>(ref error.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcGetLogFileInfo(RpcContextHandle logHandle, uint propertyId, uint propertyValueBufferSize, RpcPointer<byte[]> propertyValueBuffer, RpcPointer<uint> propertyValueBufferLength, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(logHandle);
			encoder.WriteValue(propertyId);
			encoder.WriteValue(propertyValueBufferSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			propertyValueBuffer.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < propertyValueBuffer.value.Length; i++)
			{
				byte elem_0 = propertyValueBuffer.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				propertyValueBuffer.value[i] = elem_0;
			}

			propertyValueBufferLength.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcGetChannelList(uint flags, RpcPointer<uint> numChannelPaths, RpcPointer<RpcPointer<RpcPointer<string>[]>> channelPaths, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			numChannelPaths.value = decoder.ReadUInt32();
			channelPaths.value = decoder.ReadOutUniquePointer<RpcPointer<string>[]>(channelPaths.value);
			if (channelPaths.value is not null)
			{
				channelPaths.value.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < channelPaths.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = channelPaths.value.value[i];
					elem_0 = decoder.ReadUniquePointer<string>();
					channelPaths.value.value[i] = elem_0;
				}

				for (int i = 0; i < channelPaths.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = channelPaths.value.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadWideCharString();
					}

					channelPaths.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcGetChannelConfig(string channelPath, uint flags, RpcPointer<EvtRpcVariantList> props, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(channelPath);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			props.value = decoder.ReadFixedStruct<EvtRpcVariantList>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<EvtRpcVariantList>(ref props.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcPutChannelConfig(string channelPath, uint flags, EvtRpcVariantList props, RpcPointer<RpcInfo> error, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(channelPath);
			encoder.WriteValue(flags);
			encoder.WriteFixedStruct(props, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(props);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			error.value = decoder.ReadFixedStruct<RpcInfo>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<RpcInfo>(ref error.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcGetPublisherList(uint flags, RpcPointer<uint> numPublisherIds, RpcPointer<RpcPointer<RpcPointer<string>[]>> publisherIds, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			numPublisherIds.value = decoder.ReadUInt32();
			publisherIds.value = decoder.ReadOutUniquePointer<RpcPointer<string>[]>(publisherIds.value);
			if (publisherIds.value is not null)
			{
				publisherIds.value.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < publisherIds.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = publisherIds.value.value[i];
					elem_0 = decoder.ReadUniquePointer<string>();
					publisherIds.value.value[i] = elem_0;
				}

				for (int i = 0; i < publisherIds.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = publisherIds.value.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadWideCharString();
					}

					publisherIds.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcGetPublisherListForChannel(char channelName, uint flags, RpcPointer<uint> numPublisherIds, RpcPointer<RpcPointer<RpcPointer<string>[]>> publisherIds, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(channelName);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			numPublisherIds.value = decoder.ReadUInt32();
			publisherIds.value = decoder.ReadOutUniquePointer<RpcPointer<string>[]>(publisherIds.value);
			if (publisherIds.value is not null)
			{
				publisherIds.value.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < publisherIds.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = publisherIds.value.value[i];
					elem_0 = decoder.ReadUniquePointer<string>();
					publisherIds.value.value[i] = elem_0;
				}

				for (int i = 0; i < publisherIds.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = publisherIds.value.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadWideCharString();
					}

					publisherIds.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcGetPublisherMetadata(string publisherId, string logFilePath, uint locale, uint flags, RpcPointer<EvtRpcVariantList> pubMetadataProps, RpcPointer<RpcContextHandle> pubMetadata, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(publisherId is null);
			if (publisherId is not null)
				encoder.WriteWideCharString(publisherId);
			encoder.WriteUniqueReferentId(logFilePath is null);
			if (logFilePath is not null)
				encoder.WriteWideCharString(logFilePath);
			encoder.WriteValue(locale);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pubMetadataProps.value = decoder.ReadFixedStruct<EvtRpcVariantList>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<EvtRpcVariantList>(ref pubMetadataProps.value);
			pubMetadata.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcGetPublisherResourceMetadata(RpcContextHandle handle, uint propertyId, uint flags, RpcPointer<EvtRpcVariantList> pubMetadataProps, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(handle);
			encoder.WriteValue(propertyId);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pubMetadataProps.value = decoder.ReadFixedStruct<EvtRpcVariantList>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<EvtRpcVariantList>(ref pubMetadataProps.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcGetEventMetadataEnum(RpcContextHandle pubMetadata, uint flags, string reservedForFilter, RpcPointer<RpcContextHandle> eventMetaDataEnum, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(pubMetadata);
			encoder.WriteValue(flags);
			encoder.WriteUniqueReferentId(reservedForFilter is null);
			if (reservedForFilter is not null)
				encoder.WriteWideCharString(reservedForFilter);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			eventMetaDataEnum.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcGetNextEventMetadata(RpcContextHandle eventMetaDataEnum, uint flags, uint numRequested, RpcPointer<uint> numReturned, RpcPointer<RpcPointer<EvtRpcVariantList[]>> eventMetadataInstances, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(eventMetaDataEnum);
			encoder.WriteValue(flags);
			encoder.WriteValue(numRequested);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			numReturned.value = decoder.ReadUInt32();
			eventMetadataInstances.value = decoder.ReadOutUniquePointer<EvtRpcVariantList[]>(eventMetadataInstances.value);
			if (eventMetadataInstances.value is not null)
			{
				eventMetadataInstances.value.value = decoder.ReadArrayHeader<EvtRpcVariantList>();
				for (int i = 0; i < eventMetadataInstances.value.value.Length; i++)
				{
					EvtRpcVariantList elem_0 = eventMetadataInstances.value.value[i];
					elem_0 = decoder.ReadFixedStruct<EvtRpcVariantList>(NdrAlignment.NativePtr);
					eventMetadataInstances.value.value[i] = elem_0;
				}

				for (int i = 0; i < eventMetadataInstances.value.value.Length; i++)
				{
					EvtRpcVariantList elem_0 = eventMetadataInstances.value.value[i];
					decoder.ReadStructDeferral<EvtRpcVariantList>(ref elem_0);
					eventMetadataInstances.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> EvtRpcGetClassicLogDisplayName(string logName, uint locale, uint flags, RpcPointer<RpcPointer<string>> displayName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(logName);
			encoder.WriteValue(locale);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			displayName.value = decoder.ReadOutUniquePointer<string>(displayName.value);
			if (displayName.value is not null)
			{
				displayName.value.value = decoder.ReadWideCharString();
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IEventService);
		private static Guid _interfaceUuid = new Guid("f6beaff7-1e19-4fbb-9f8f-b89e2018337c");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class IEventServiceStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcRegisterRemoteSubscription(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string channelPath;
			string query;
			string bookmarkXml;
			uint flags;
			RpcPointer<RpcContextHandle> handle = new RpcPointer<RpcContextHandle>();
			RpcPointer<RpcContextHandle> control = new RpcPointer<RpcContextHandle>();
			RpcPointer<uint> queryChannelInfoSize = new RpcPointer<uint>();
			RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>> queryChannelInfo = new RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>>();
			RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
			if (decoder.ReadReferentId() == 0)
				channelPath = null;
			else
				channelPath = decoder.ReadWideCharString();
			query = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				bookmarkXml = null;
			else
				bookmarkXml = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcRegisterRemoteSubscription(channelPath, query, bookmarkXml, flags, handle, control, queryChannelInfoSize, queryChannelInfo, error, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(handle.value);
			encoder.WriteContextHandle(control.value);
			encoder.WriteValue(queryChannelInfoSize.value);
			encoder.WriteUniquePointer(queryChannelInfo.value);
			if (queryChannelInfo.value is not null)
			{
				encoder.WriteArrayHeader(queryChannelInfo.value.value);
				for (int i = 0; i < queryChannelInfo.value.value.Length; i++)
				{
					EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < queryChannelInfo.value.value.Length; i++)
				{
					EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteFixedStruct(error.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(error.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcRemoteSubscriptionNextAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle handle;
			uint numRequestedRecords;
			uint flags;
			RpcPointer<uint> numActualRecords = new RpcPointer<uint>();
			RpcPointer<RpcPointer<uint[]>> eventDataIndices = new RpcPointer<RpcPointer<uint[]>>();
			RpcPointer<RpcPointer<uint[]>> eventDataSizes = new RpcPointer<RpcPointer<uint[]>>();
			RpcPointer<uint> resultBufferSize = new RpcPointer<uint>();
			RpcPointer<RpcPointer<byte[]>> resultBuffer = new RpcPointer<RpcPointer<byte[]>>();
			handle = decoder.ReadContextHandle();
			numRequestedRecords = decoder.ReadUInt32();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcRemoteSubscriptionNextAsync(handle, numRequestedRecords, flags, numActualRecords, eventDataIndices, eventDataSizes, resultBufferSize, resultBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(numActualRecords.value);
			encoder.WriteUniquePointer(eventDataIndices.value);
			if (eventDataIndices.value is not null)
			{
				encoder.WriteArrayHeader(eventDataIndices.value.value);
				for (int i = 0; i < eventDataIndices.value.value.Length; i++)
				{
					uint elem_0 = eventDataIndices.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteUniquePointer(eventDataSizes.value);
			if (eventDataSizes.value is not null)
			{
				encoder.WriteArrayHeader(eventDataSizes.value.value);
				for (int i = 0; i < eventDataSizes.value.value.Length; i++)
				{
					uint elem_0 = eventDataSizes.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(resultBufferSize.value);
			encoder.WriteUniquePointer(resultBuffer.value);
			if (resultBuffer.value is not null)
			{
				encoder.WriteArrayHeader(resultBuffer.value.value);
				for (int i = 0; i < resultBuffer.value.value.Length; i++)
				{
					byte elem_0 = resultBuffer.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcRemoteSubscriptionNext(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle handle;
			uint numRequestedRecords;
			uint timeOut;
			uint flags;
			RpcPointer<uint> numActualRecords = new RpcPointer<uint>();
			RpcPointer<RpcPointer<uint[]>> eventDataIndices = new RpcPointer<RpcPointer<uint[]>>();
			RpcPointer<RpcPointer<uint[]>> eventDataSizes = new RpcPointer<RpcPointer<uint[]>>();
			RpcPointer<uint> resultBufferSize = new RpcPointer<uint>();
			RpcPointer<RpcPointer<byte[]>> resultBuffer = new RpcPointer<RpcPointer<byte[]>>();
			handle = decoder.ReadContextHandle();
			numRequestedRecords = decoder.ReadUInt32();
			timeOut = decoder.ReadUInt32();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcRemoteSubscriptionNext(handle, numRequestedRecords, timeOut, flags, numActualRecords, eventDataIndices, eventDataSizes, resultBufferSize, resultBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(numActualRecords.value);
			encoder.WriteUniquePointer(eventDataIndices.value);
			if (eventDataIndices.value is not null)
			{
				encoder.WriteArrayHeader(eventDataIndices.value.value);
				for (int i = 0; i < eventDataIndices.value.value.Length; i++)
				{
					uint elem_0 = eventDataIndices.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteUniquePointer(eventDataSizes.value);
			if (eventDataSizes.value is not null)
			{
				encoder.WriteArrayHeader(eventDataSizes.value.value);
				for (int i = 0; i < eventDataSizes.value.value.Length; i++)
				{
					uint elem_0 = eventDataSizes.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(resultBufferSize.value);
			encoder.WriteUniquePointer(resultBuffer.value);
			if (resultBuffer.value is not null)
			{
				encoder.WriteArrayHeader(resultBuffer.value.value);
				for (int i = 0; i < resultBuffer.value.value.Length; i++)
				{
					byte elem_0 = resultBuffer.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcRemoteSubscriptionWaitAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle handle;
			handle = decoder.ReadContextHandle();
			var invokeTask = this._obj.EvtRpcRemoteSubscriptionWaitAsync(handle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcRegisterControllableOperation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> handle = new RpcPointer<RpcContextHandle>();
			var invokeTask = this._obj.EvtRpcRegisterControllableOperation(handle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(handle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcRegisterLogQuery(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			string query;
			uint flags;
			RpcPointer<RpcContextHandle> handle = new RpcPointer<RpcContextHandle>();
			RpcPointer<RpcContextHandle> opControl = new RpcPointer<RpcContextHandle>();
			RpcPointer<uint> queryChannelInfoSize = new RpcPointer<uint>();
			RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>> queryChannelInfo = new RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>>();
			RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
			if (decoder.ReadReferentId() == 0)
				path = null;
			else
				path = decoder.ReadWideCharString();
			query = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcRegisterLogQuery(path, query, flags, handle, opControl, queryChannelInfoSize, queryChannelInfo, error, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(handle.value);
			encoder.WriteContextHandle(opControl.value);
			encoder.WriteValue(queryChannelInfoSize.value);
			encoder.WriteUniquePointer(queryChannelInfo.value);
			if (queryChannelInfo.value is not null)
			{
				encoder.WriteArrayHeader(queryChannelInfo.value.value);
				for (int i = 0; i < queryChannelInfo.value.value.Length; i++)
				{
					EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < queryChannelInfo.value.value.Length; i++)
				{
					EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteFixedStruct(error.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(error.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcClearLog(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle control;
			string channelPath;
			string backupPath;
			uint flags;
			RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
			control = decoder.ReadContextHandle();
			channelPath = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				backupPath = null;
			else
				backupPath = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcClearLog(control, channelPath, backupPath, flags, error, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(error.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(error.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcExportLog(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle control;
			string channelPath;
			string query;
			string backupPath;
			uint flags;
			RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
			control = decoder.ReadContextHandle();
			if (decoder.ReadReferentId() == 0)
				channelPath = null;
			else
				channelPath = decoder.ReadWideCharString();
			query = decoder.ReadWideCharString();
			backupPath = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcExportLog(control, channelPath, query, backupPath, flags, error, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(error.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(error.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcLocalizeExportLog(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle control;
			string logFilePath;
			uint locale;
			uint flags;
			RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
			control = decoder.ReadContextHandle();
			logFilePath = decoder.ReadWideCharString();
			locale = decoder.ReadUInt32();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcLocalizeExportLog(control, logFilePath, locale, flags, error, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(error.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(error.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcMessageRender(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle pubCfgObj;
			uint sizeEventId;
			byte[] eventId;
			uint messageId;
			EvtRpcVariantList values;
			uint flags;
			uint maxSizeString;
			RpcPointer<uint> actualSizeString = new RpcPointer<uint>();
			RpcPointer<uint> neededSizeString = new RpcPointer<uint>();
			RpcPointer<RpcPointer<byte[]>> @string = new RpcPointer<RpcPointer<byte[]>>();
			RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
			pubCfgObj = decoder.ReadContextHandle();
			sizeEventId = decoder.ReadUInt32();
			eventId = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < eventId.Length; i++)
			{
				byte elem_0 = eventId[i];
				elem_0 = decoder.ReadUnsignedChar();
				eventId[i] = elem_0;
			}

			messageId = decoder.ReadUInt32();
			values = decoder.ReadFixedStruct<EvtRpcVariantList>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<EvtRpcVariantList>(ref values);
			flags = decoder.ReadUInt32();
			maxSizeString = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcMessageRender(pubCfgObj, sizeEventId, eventId, messageId, values, flags, maxSizeString, actualSizeString, neededSizeString, @string, error, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(actualSizeString.value);
			encoder.WriteValue(neededSizeString.value);
			encoder.WriteUniquePointer(@string.value);
			if (@string.value is not null)
			{
				encoder.WriteArrayHeader(@string.value.value);
				for (int i = 0; i < @string.value.value.Length; i++)
				{
					byte elem_0 = @string.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteFixedStruct(error.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(error.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcMessageRenderDefault(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint sizeEventId;
			byte[] eventId;
			uint messageId;
			EvtRpcVariantList values;
			uint flags;
			uint maxSizeString;
			RpcPointer<uint> actualSizeString = new RpcPointer<uint>();
			RpcPointer<uint> neededSizeString = new RpcPointer<uint>();
			RpcPointer<RpcPointer<byte[]>> @string = new RpcPointer<RpcPointer<byte[]>>();
			RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
			sizeEventId = decoder.ReadUInt32();
			eventId = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < eventId.Length; i++)
			{
				byte elem_0 = eventId[i];
				elem_0 = decoder.ReadUnsignedChar();
				eventId[i] = elem_0;
			}

			messageId = decoder.ReadUInt32();
			values = decoder.ReadFixedStruct<EvtRpcVariantList>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<EvtRpcVariantList>(ref values);
			flags = decoder.ReadUInt32();
			maxSizeString = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcMessageRenderDefault(sizeEventId, eventId, messageId, values, flags, maxSizeString, actualSizeString, neededSizeString, @string, error, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(actualSizeString.value);
			encoder.WriteValue(neededSizeString.value);
			encoder.WriteUniquePointer(@string.value);
			if (@string.value is not null)
			{
				encoder.WriteArrayHeader(@string.value.value);
				for (int i = 0; i < @string.value.value.Length; i++)
				{
					byte elem_0 = @string.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteFixedStruct(error.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(error.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcQueryNext(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle logQuery;
			uint numRequestedRecords;
			uint timeOutEnd;
			uint flags;
			RpcPointer<uint> numActualRecords = new RpcPointer<uint>();
			RpcPointer<RpcPointer<uint[]>> eventDataIndices = new RpcPointer<RpcPointer<uint[]>>();
			RpcPointer<RpcPointer<uint[]>> eventDataSizes = new RpcPointer<RpcPointer<uint[]>>();
			RpcPointer<uint> resultBufferSize = new RpcPointer<uint>();
			RpcPointer<RpcPointer<byte[]>> resultBuffer = new RpcPointer<RpcPointer<byte[]>>();
			logQuery = decoder.ReadContextHandle();
			numRequestedRecords = decoder.ReadUInt32();
			timeOutEnd = decoder.ReadUInt32();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcQueryNext(logQuery, numRequestedRecords, timeOutEnd, flags, numActualRecords, eventDataIndices, eventDataSizes, resultBufferSize, resultBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(numActualRecords.value);
			encoder.WriteUniquePointer(eventDataIndices.value);
			if (eventDataIndices.value is not null)
			{
				encoder.WriteArrayHeader(eventDataIndices.value.value);
				for (int i = 0; i < eventDataIndices.value.value.Length; i++)
				{
					uint elem_0 = eventDataIndices.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteUniquePointer(eventDataSizes.value);
			if (eventDataSizes.value is not null)
			{
				encoder.WriteArrayHeader(eventDataSizes.value.value);
				for (int i = 0; i < eventDataSizes.value.value.Length; i++)
				{
					uint elem_0 = eventDataSizes.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(resultBufferSize.value);
			encoder.WriteUniquePointer(resultBuffer.value);
			if (resultBuffer.value is not null)
			{
				encoder.WriteArrayHeader(resultBuffer.value.value);
				for (int i = 0; i < resultBuffer.value.value.Length; i++)
				{
					byte elem_0 = resultBuffer.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcQuerySeek(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle logQuery;
			long pos;
			string bookmarkXml;
			uint timeOut;
			uint flags;
			RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
			logQuery = decoder.ReadContextHandle();
			pos = decoder.ReadInt64();
			if (decoder.ReadReferentId() == 0)
				bookmarkXml = null;
			else
				bookmarkXml = decoder.ReadWideCharString();
			timeOut = decoder.ReadUInt32();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcQuerySeek(logQuery, pos, bookmarkXml, timeOut, flags, error, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(error.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(error.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcClose(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> handle;
			handle = new RpcPointer<RpcContextHandle>();
			handle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.EvtRpcClose(handle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(handle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcCancel(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle handle;
			handle = decoder.ReadContextHandle();
			var invokeTask = this._obj.EvtRpcCancel(handle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcAssertConfig(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			uint flags;
			path = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcAssertConfig(path, flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcRetractConfig(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			uint flags;
			path = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcRetractConfig(path, flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcOpenLogHandle(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string channel;
			uint flags;
			RpcPointer<RpcContextHandle> handle = new RpcPointer<RpcContextHandle>();
			RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
			channel = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcOpenLogHandle(channel, flags, handle, error, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(handle.value);
			encoder.WriteFixedStruct(error.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(error.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcGetLogFileInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle logHandle;
			uint propertyId;
			uint propertyValueBufferSize;
			RpcPointer<byte[]> propertyValueBuffer = new RpcPointer<byte[]>();
			RpcPointer<uint> propertyValueBufferLength = new RpcPointer<uint>();
			logHandle = decoder.ReadContextHandle();
			propertyId = decoder.ReadUInt32();
			propertyValueBufferSize = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcGetLogFileInfo(logHandle, propertyId, propertyValueBufferSize, propertyValueBuffer, propertyValueBufferLength, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(propertyValueBuffer.value);
			for (int i = 0; i < propertyValueBuffer.value.Length; i++)
			{
				byte elem_0 = propertyValueBuffer.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(propertyValueBufferLength.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcGetChannelList(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint flags;
			RpcPointer<uint> numChannelPaths = new RpcPointer<uint>();
			RpcPointer<RpcPointer<RpcPointer<string>[]>> channelPaths = new RpcPointer<RpcPointer<RpcPointer<string>[]>>();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcGetChannelList(flags, numChannelPaths, channelPaths, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(numChannelPaths.value);
			encoder.WriteUniquePointer(channelPaths.value);
			if (channelPaths.value is not null)
			{
				encoder.WriteArrayHeader(channelPaths.value.value);
				for (int i = 0; i < channelPaths.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = channelPaths.value.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < channelPaths.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = channelPaths.value.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteWideCharString(elem_0.value);
					}
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcGetChannelConfig(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string channelPath;
			uint flags;
			RpcPointer<EvtRpcVariantList> props = new RpcPointer<EvtRpcVariantList>();
			channelPath = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcGetChannelConfig(channelPath, flags, props, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(props.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(props.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcPutChannelConfig(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string channelPath;
			uint flags;
			EvtRpcVariantList props;
			RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
			channelPath = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			props = decoder.ReadFixedStruct<EvtRpcVariantList>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<EvtRpcVariantList>(ref props);
			var invokeTask = this._obj.EvtRpcPutChannelConfig(channelPath, flags, props, error, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(error.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(error.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcGetPublisherList(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint flags;
			RpcPointer<uint> numPublisherIds = new RpcPointer<uint>();
			RpcPointer<RpcPointer<RpcPointer<string>[]>> publisherIds = new RpcPointer<RpcPointer<RpcPointer<string>[]>>();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcGetPublisherList(flags, numPublisherIds, publisherIds, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(numPublisherIds.value);
			encoder.WriteUniquePointer(publisherIds.value);
			if (publisherIds.value is not null)
			{
				encoder.WriteArrayHeader(publisherIds.value.value);
				for (int i = 0; i < publisherIds.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = publisherIds.value.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < publisherIds.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = publisherIds.value.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteWideCharString(elem_0.value);
					}
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcGetPublisherListForChannel(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			char channelName;
			uint flags;
			RpcPointer<uint> numPublisherIds = new RpcPointer<uint>();
			RpcPointer<RpcPointer<RpcPointer<string>[]>> publisherIds = new RpcPointer<RpcPointer<RpcPointer<string>[]>>();
			channelName = decoder.ReadWideChar();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcGetPublisherListForChannel(channelName, flags, numPublisherIds, publisherIds, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(numPublisherIds.value);
			encoder.WriteUniquePointer(publisherIds.value);
			if (publisherIds.value is not null)
			{
				encoder.WriteArrayHeader(publisherIds.value.value);
				for (int i = 0; i < publisherIds.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = publisherIds.value.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < publisherIds.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = publisherIds.value.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteWideCharString(elem_0.value);
					}
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcGetPublisherMetadata(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string publisherId;
			string logFilePath;
			uint locale;
			uint flags;
			RpcPointer<EvtRpcVariantList> pubMetadataProps = new RpcPointer<EvtRpcVariantList>();
			RpcPointer<RpcContextHandle> pubMetadata = new RpcPointer<RpcContextHandle>();
			if (decoder.ReadReferentId() == 0)
				publisherId = null;
			else
				publisherId = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				logFilePath = null;
			else
				logFilePath = decoder.ReadWideCharString();
			locale = decoder.ReadUInt32();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcGetPublisherMetadata(publisherId, logFilePath, locale, flags, pubMetadataProps, pubMetadata, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pubMetadataProps.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pubMetadataProps.value);
			encoder.WriteContextHandle(pubMetadata.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcGetPublisherResourceMetadata(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle handle;
			uint propertyId;
			uint flags;
			RpcPointer<EvtRpcVariantList> pubMetadataProps = new RpcPointer<EvtRpcVariantList>();
			handle = decoder.ReadContextHandle();
			propertyId = decoder.ReadUInt32();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcGetPublisherResourceMetadata(handle, propertyId, flags, pubMetadataProps, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pubMetadataProps.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pubMetadataProps.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcGetEventMetadataEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle pubMetadata;
			uint flags;
			string reservedForFilter;
			RpcPointer<RpcContextHandle> eventMetaDataEnum = new RpcPointer<RpcContextHandle>();
			pubMetadata = decoder.ReadContextHandle();
			flags = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				reservedForFilter = null;
			else
				reservedForFilter = decoder.ReadWideCharString();
			var invokeTask = this._obj.EvtRpcGetEventMetadataEnum(pubMetadata, flags, reservedForFilter, eventMetaDataEnum, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(eventMetaDataEnum.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcGetNextEventMetadata(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle eventMetaDataEnum;
			uint flags;
			uint numRequested;
			RpcPointer<uint> numReturned = new RpcPointer<uint>();
			RpcPointer<RpcPointer<EvtRpcVariantList[]>> eventMetadataInstances = new RpcPointer<RpcPointer<EvtRpcVariantList[]>>();
			eventMetaDataEnum = decoder.ReadContextHandle();
			flags = decoder.ReadUInt32();
			numRequested = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcGetNextEventMetadata(eventMetaDataEnum, flags, numRequested, numReturned, eventMetadataInstances, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(numReturned.value);
			encoder.WriteUniquePointer(eventMetadataInstances.value);
			if (eventMetadataInstances.value is not null)
			{
				encoder.WriteArrayHeader(eventMetadataInstances.value.value);
				for (int i = 0; i < eventMetadataInstances.value.value.Length; i++)
				{
					EvtRpcVariantList elem_0 = eventMetadataInstances.value.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < eventMetadataInstances.value.value.Length; i++)
				{
					EvtRpcVariantList elem_0 = eventMetadataInstances.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_EvtRpcGetClassicLogDisplayName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string logName;
			uint locale;
			uint flags;
			RpcPointer<RpcPointer<string>> displayName = new RpcPointer<RpcPointer<string>>();
			logName = decoder.ReadWideCharString();
			locale = decoder.ReadUInt32();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.EvtRpcGetClassicLogDisplayName(logName, locale, flags, displayName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(displayName.value);
			if (displayName.value is not null)
			{
				encoder.WriteWideCharString(displayName.value.value);
			}

			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("f6beaff7-1e19-4fbb-9f8f-b89e2018337c");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IEventService _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public IEventServiceStub(IEventService obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_EvtRpcRegisterRemoteSubscription, this.Invoke_EvtRpcRemoteSubscriptionNextAsync, this.Invoke_EvtRpcRemoteSubscriptionNext, this.Invoke_EvtRpcRemoteSubscriptionWaitAsync, this.Invoke_EvtRpcRegisterControllableOperation, this.Invoke_EvtRpcRegisterLogQuery, this.Invoke_EvtRpcClearLog, this.Invoke_EvtRpcExportLog, this.Invoke_EvtRpcLocalizeExportLog, this.Invoke_EvtRpcMessageRender, this.Invoke_EvtRpcMessageRenderDefault, this.Invoke_EvtRpcQueryNext, this.Invoke_EvtRpcQuerySeek, this.Invoke_EvtRpcClose, this.Invoke_EvtRpcCancel, this.Invoke_EvtRpcAssertConfig, this.Invoke_EvtRpcRetractConfig, this.Invoke_EvtRpcOpenLogHandle, this.Invoke_EvtRpcGetLogFileInfo, this.Invoke_EvtRpcGetChannelList, this.Invoke_EvtRpcGetChannelConfig, this.Invoke_EvtRpcPutChannelConfig, this.Invoke_EvtRpcGetPublisherList, this.Invoke_EvtRpcGetPublisherListForChannel, this.Invoke_EvtRpcGetPublisherMetadata, this.Invoke_EvtRpcGetPublisherResourceMetadata, this.Invoke_EvtRpcGetEventMetadataEnum, this.Invoke_EvtRpcGetNextEventMetadata, this.Invoke_EvtRpcGetClassicLogDisplayName};
		}
	}
}