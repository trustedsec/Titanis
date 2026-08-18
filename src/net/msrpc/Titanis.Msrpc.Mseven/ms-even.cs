namespace ms_even
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
		public RpcPointer<byte[]> Buffer;
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
			this.Buffer = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					byte elem_0 = this.Buffer.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					byte elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct RPC_CLIENT_ID : IRpcFixedStruct
	{
		public uint UniqueProcess;
		public uint UniqueThread;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.UniqueProcess);
			encoder.WriteValue(this.UniqueThread);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.UniqueProcess = decoder.ReadUInt32();
			this.UniqueThread = decoder.ReadUInt32();
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("82273fdc-e32a-18c3-3f78-827929dc23ea"), RpcVersionAttribute(0, 0)]
	public partial interface eventlog
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrClearELFW(RpcContextHandle LogHandle, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> BackupFileName, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrBackupELFW(RpcContextHandle LogHandle, ms_dtyp.RPC_UNICODE_STRING BackupFileName, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrCloseEL(RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrDeregisterEventSource(RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrNumberOfRecords(RpcContextHandle LogHandle, RpcPointer<uint> NumberOfRecords, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrOldestRecord(RpcContextHandle LogHandle, RpcPointer<uint> OldestRecordNumber, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrChangeNotify(RpcContextHandle LogHandle, RPC_CLIENT_ID ClientId, uint Event, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrOpenELW(RpcPointer<char> UNCServerName, ms_dtyp.RPC_UNICODE_STRING ModuleName, ms_dtyp.RPC_UNICODE_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrRegisterEventSourceW(RpcPointer<char> UNCServerName, ms_dtyp.RPC_UNICODE_STRING ModuleName, ms_dtyp.RPC_UNICODE_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrOpenBELW(RpcPointer<char> UNCServerName, ms_dtyp.RPC_UNICODE_STRING BackupFileName, uint MajorVersion, uint MinorVersion, RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrReadELW(RpcContextHandle LogHandle, uint ReadFlags, uint RecordOffset, uint NumberOfBytesToRead, RpcPointer<byte[]> Buffer, RpcPointer<uint> NumberOfBytesRead, RpcPointer<uint> MinNumberOfBytesNeeded, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrReportEventW(RpcContextHandle LogHandle, uint Time, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, ms_dtyp.RPC_UNICODE_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, RpcPointer<uint> TimeWritten, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrClearELFA(RpcContextHandle LogHandle, RpcPointer<RPC_STRING> BackupFileName, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrBackupELFA(RpcContextHandle LogHandle, RPC_STRING BackupFileName, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrOpenELA(RpcPointer<byte> UNCServerName, RPC_STRING ModuleName, RPC_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrRegisterEventSourceA(RpcPointer<byte> UNCServerName, RPC_STRING ModuleName, RPC_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrOpenBELA(RpcPointer<byte> UNCServerName, RPC_STRING BackupFileName, uint MajorVersion, uint MinorVersion, RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrReadELA(RpcContextHandle LogHandle, uint ReadFlags, uint RecordOffset, uint NumberOfBytesToRead, RpcPointer<byte[]> Buffer, RpcPointer<uint> NumberOfBytesRead, RpcPointer<uint> MinNumberOfBytesNeeded, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrReportEventA(RpcContextHandle LogHandle, uint Time, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, RPC_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<RPC_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, RpcPointer<uint> TimeWritten, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum19NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum20NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum21NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrGetLogInformation(RpcContextHandle LogHandle, uint InfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum23NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrReportEventAndSourceW(RpcContextHandle LogHandle, uint Time, ushort EventType, ushort EventCategory, uint EventID, ms_dtyp.RPC_UNICODE_STRING SourceName, ushort NumStrings, uint DataSize, ms_dtyp.RPC_UNICODE_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, RpcPointer<uint> TimeWritten, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrReportEventExW(RpcContextHandle LogHandle, ms_dtyp.FILETIME TimeGenerated, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, ms_dtyp.RPC_UNICODE_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> ElfrReportEventExA(RpcContextHandle LogHandle, ms_dtyp.FILETIME TimeGenerated, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, RPC_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<RPC_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("82273fdc-e32a-18c3-3f78-827929dc23ea")]
	public partial class eventlogClientProxy : Titanis.DceRpc.Client.RpcClientProxy, eventlog, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrClearELFW(RpcContextHandle LogHandle, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> BackupFileName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteUniquePointer(BackupFileName);
			if (BackupFileName is not null)
			{
				encoder.WriteFixedStruct(BackupFileName.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(BackupFileName.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrBackupELFW(RpcContextHandle LogHandle, ms_dtyp.RPC_UNICODE_STRING BackupFileName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteFixedStruct(BackupFileName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(BackupFileName);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrCloseEL(RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			LogHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrDeregisterEventSource(RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			LogHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrNumberOfRecords(RpcContextHandle LogHandle, RpcPointer<uint> NumberOfRecords, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			NumberOfRecords.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrOldestRecord(RpcContextHandle LogHandle, RpcPointer<uint> OldestRecordNumber, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			OldestRecordNumber.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrChangeNotify(RpcContextHandle LogHandle, RPC_CLIENT_ID ClientId, uint Event, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteFixedStruct(ClientId, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(ClientId);
			encoder.WriteValue(Event);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrOpenELW(RpcPointer<char> UNCServerName, ms_dtyp.RPC_UNICODE_STRING ModuleName, ms_dtyp.RPC_UNICODE_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(UNCServerName);
			if (UNCServerName is not null)
			{
				encoder.WriteValue(UNCServerName.value);
			}

			encoder.WriteFixedStruct(ModuleName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ModuleName);
			encoder.WriteFixedStruct(RegModuleName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(RegModuleName);
			encoder.WriteValue(MajorVersion);
			encoder.WriteValue(MinorVersion);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			LogHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrRegisterEventSourceW(RpcPointer<char> UNCServerName, ms_dtyp.RPC_UNICODE_STRING ModuleName, ms_dtyp.RPC_UNICODE_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(UNCServerName);
			if (UNCServerName is not null)
			{
				encoder.WriteValue(UNCServerName.value);
			}

			encoder.WriteFixedStruct(ModuleName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ModuleName);
			encoder.WriteFixedStruct(RegModuleName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(RegModuleName);
			encoder.WriteValue(MajorVersion);
			encoder.WriteValue(MinorVersion);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			LogHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrOpenBELW(RpcPointer<char> UNCServerName, ms_dtyp.RPC_UNICODE_STRING BackupFileName, uint MajorVersion, uint MinorVersion, RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(UNCServerName);
			if (UNCServerName is not null)
			{
				encoder.WriteValue(UNCServerName.value);
			}

			encoder.WriteFixedStruct(BackupFileName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(BackupFileName);
			encoder.WriteValue(MajorVersion);
			encoder.WriteValue(MinorVersion);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			LogHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrReadELW(RpcContextHandle LogHandle, uint ReadFlags, uint RecordOffset, uint NumberOfBytesToRead, RpcPointer<byte[]> Buffer, RpcPointer<uint> NumberOfBytesRead, RpcPointer<uint> MinNumberOfBytesNeeded, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteValue(ReadFlags);
			encoder.WriteValue(RecordOffset);
			encoder.WriteValue(NumberOfBytesToRead);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Buffer.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < Buffer.value.Length; i++)
			{
				byte elem_0 = Buffer.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				Buffer.value[i] = elem_0;
			}

			NumberOfBytesRead.value = decoder.ReadUInt32();
			MinNumberOfBytesNeeded.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrReportEventW(RpcContextHandle LogHandle, uint Time, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, ms_dtyp.RPC_UNICODE_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, RpcPointer<uint> TimeWritten, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteValue(Time);
			encoder.WriteValue(EventType);
			encoder.WriteValue(EventCategory);
			encoder.WriteValue(EventID);
			encoder.WriteValue(NumStrings);
			encoder.WriteValue(DataSize);
			encoder.WriteFixedStruct(ComputerName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ComputerName);
			encoder.WriteUniquePointer(UserSID);
			if (UserSID is not null)
			{
				encoder.WriteConformantStruct(UserSID.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(UserSID.value);
			}

			encoder.WriteArrayHeader(Strings);
			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
				encoder.WriteUniquePointer(elem_0);
			}

			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
				if (elem_0 is not null)
				{
					encoder.WriteFixedStruct(elem_0.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}

			encoder.WriteUniqueReferentId(Data is null);
			if (Data is not null)
			{
				encoder.WriteArrayHeader(Data);
				for (int i = 0; i < Data.Length; i++)
				{
					byte elem_0 = Data[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(Flags);
			encoder.WriteUniquePointer(RecordNumber);
			if (RecordNumber is not null)
			{
				encoder.WriteValue(RecordNumber.value);
			}

			encoder.WriteUniquePointer(TimeWritten);
			if (TimeWritten is not null)
			{
				encoder.WriteValue(TimeWritten.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			RecordNumber = decoder.ReadOutUniquePointer<uint>(RecordNumber);
			if (RecordNumber is not null)
			{
				RecordNumber.value = decoder.ReadUInt32();
			}

			TimeWritten = decoder.ReadOutUniquePointer<uint>(TimeWritten);
			if (TimeWritten is not null)
			{
				TimeWritten.value = decoder.ReadUInt32();
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrClearELFA(RpcContextHandle LogHandle, RpcPointer<RPC_STRING> BackupFileName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteUniquePointer(BackupFileName);
			if (BackupFileName is not null)
			{
				encoder.WriteFixedStruct(BackupFileName.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(BackupFileName.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrBackupELFA(RpcContextHandle LogHandle, RPC_STRING BackupFileName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteFixedStruct(BackupFileName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(BackupFileName);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrOpenELA(RpcPointer<byte> UNCServerName, RPC_STRING ModuleName, RPC_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(UNCServerName);
			if (UNCServerName is not null)
			{
				encoder.WriteValue(UNCServerName.value);
			}

			encoder.WriteFixedStruct(ModuleName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ModuleName);
			encoder.WriteFixedStruct(RegModuleName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(RegModuleName);
			encoder.WriteValue(MajorVersion);
			encoder.WriteValue(MinorVersion);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			LogHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrRegisterEventSourceA(RpcPointer<byte> UNCServerName, RPC_STRING ModuleName, RPC_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(UNCServerName);
			if (UNCServerName is not null)
			{
				encoder.WriteValue(UNCServerName.value);
			}

			encoder.WriteFixedStruct(ModuleName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ModuleName);
			encoder.WriteFixedStruct(RegModuleName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(RegModuleName);
			encoder.WriteValue(MajorVersion);
			encoder.WriteValue(MinorVersion);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			LogHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrOpenBELA(RpcPointer<byte> UNCServerName, RPC_STRING BackupFileName, uint MajorVersion, uint MinorVersion, RpcPointer<RpcContextHandle> LogHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(UNCServerName);
			if (UNCServerName is not null)
			{
				encoder.WriteValue(UNCServerName.value);
			}

			encoder.WriteFixedStruct(BackupFileName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(BackupFileName);
			encoder.WriteValue(MajorVersion);
			encoder.WriteValue(MinorVersion);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			LogHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrReadELA(RpcContextHandle LogHandle, uint ReadFlags, uint RecordOffset, uint NumberOfBytesToRead, RpcPointer<byte[]> Buffer, RpcPointer<uint> NumberOfBytesRead, RpcPointer<uint> MinNumberOfBytesNeeded, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteValue(ReadFlags);
			encoder.WriteValue(RecordOffset);
			encoder.WriteValue(NumberOfBytesToRead);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Buffer.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < Buffer.value.Length; i++)
			{
				byte elem_0 = Buffer.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				Buffer.value[i] = elem_0;
			}

			NumberOfBytesRead.value = decoder.ReadUInt32();
			MinNumberOfBytesNeeded.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrReportEventA(RpcContextHandle LogHandle, uint Time, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, RPC_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<RPC_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, RpcPointer<uint> TimeWritten, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteValue(Time);
			encoder.WriteValue(EventType);
			encoder.WriteValue(EventCategory);
			encoder.WriteValue(EventID);
			encoder.WriteValue(NumStrings);
			encoder.WriteValue(DataSize);
			encoder.WriteFixedStruct(ComputerName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ComputerName);
			encoder.WriteUniquePointer(UserSID);
			if (UserSID is not null)
			{
				encoder.WriteConformantStruct(UserSID.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(UserSID.value);
			}

			encoder.WriteArrayHeader(Strings);
			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<RPC_STRING> elem_0 = Strings[i];
				encoder.WriteUniquePointer(elem_0);
			}

			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<RPC_STRING> elem_0 = Strings[i];
				if (elem_0 is not null)
				{
					encoder.WriteFixedStruct(elem_0.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}

			encoder.WriteUniqueReferentId(Data is null);
			if (Data is not null)
			{
				encoder.WriteArrayHeader(Data);
				for (int i = 0; i < Data.Length; i++)
				{
					byte elem_0 = Data[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(Flags);
			encoder.WriteUniquePointer(RecordNumber);
			if (RecordNumber is not null)
			{
				encoder.WriteValue(RecordNumber.value);
			}

			encoder.WriteUniquePointer(TimeWritten);
			if (TimeWritten is not null)
			{
				encoder.WriteValue(TimeWritten.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			RecordNumber = decoder.ReadOutUniquePointer<uint>(RecordNumber);
			if (RecordNumber is not null)
			{
				RecordNumber.value = decoder.ReadUInt32();
			}

			TimeWritten = decoder.ReadOutUniquePointer<uint>(TimeWritten);
			if (TimeWritten is not null)
			{
				TimeWritten.value = decoder.ReadUInt32();
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum19NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum20NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum21NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrGetLogInformation(RpcContextHandle LogHandle, uint InfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteValue(InfoLevel);
			encoder.WriteValue(cbBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpBuffer.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpBuffer.value[i] = elem_0;
			}

			pcbBytesNeeded.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum23NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrReportEventAndSourceW(RpcContextHandle LogHandle, uint Time, ushort EventType, ushort EventCategory, uint EventID, ms_dtyp.RPC_UNICODE_STRING SourceName, ushort NumStrings, uint DataSize, ms_dtyp.RPC_UNICODE_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, RpcPointer<uint> TimeWritten, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteValue(Time);
			encoder.WriteValue(EventType);
			encoder.WriteValue(EventCategory);
			encoder.WriteValue(EventID);
			encoder.WriteFixedStruct(SourceName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(SourceName);
			encoder.WriteValue(NumStrings);
			encoder.WriteValue(DataSize);
			encoder.WriteFixedStruct(ComputerName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ComputerName);
			encoder.WriteUniquePointer(UserSID);
			if (UserSID is not null)
			{
				encoder.WriteConformantStruct(UserSID.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(UserSID.value);
			}

			encoder.WriteArrayHeader(Strings);
			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
				encoder.WriteUniquePointer(elem_0);
			}

			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
				if (elem_0 is not null)
				{
					encoder.WriteFixedStruct(elem_0.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}

			encoder.WriteUniqueReferentId(Data is null);
			if (Data is not null)
			{
				encoder.WriteArrayHeader(Data);
				for (int i = 0; i < Data.Length; i++)
				{
					byte elem_0 = Data[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(Flags);
			encoder.WriteUniquePointer(RecordNumber);
			if (RecordNumber is not null)
			{
				encoder.WriteValue(RecordNumber.value);
			}

			encoder.WriteUniquePointer(TimeWritten);
			if (TimeWritten is not null)
			{
				encoder.WriteValue(TimeWritten.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			RecordNumber = decoder.ReadOutUniquePointer<uint>(RecordNumber);
			if (RecordNumber is not null)
			{
				RecordNumber.value = decoder.ReadUInt32();
			}

			TimeWritten = decoder.ReadOutUniquePointer<uint>(TimeWritten);
			if (TimeWritten is not null)
			{
				TimeWritten.value = decoder.ReadUInt32();
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrReportEventExW(RpcContextHandle LogHandle, ms_dtyp.FILETIME TimeGenerated, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, ms_dtyp.RPC_UNICODE_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteFixedStruct(TimeGenerated, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(TimeGenerated);
			encoder.WriteValue(EventType);
			encoder.WriteValue(EventCategory);
			encoder.WriteValue(EventID);
			encoder.WriteValue(NumStrings);
			encoder.WriteValue(DataSize);
			encoder.WriteFixedStruct(ComputerName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ComputerName);
			encoder.WriteUniquePointer(UserSID);
			if (UserSID is not null)
			{
				encoder.WriteConformantStruct(UserSID.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(UserSID.value);
			}

			encoder.WriteArrayHeader(Strings);
			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
				encoder.WriteUniquePointer(elem_0);
			}

			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
				if (elem_0 is not null)
				{
					encoder.WriteFixedStruct(elem_0.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}

			encoder.WriteUniqueReferentId(Data is null);
			if (Data is not null)
			{
				encoder.WriteArrayHeader(Data);
				for (int i = 0; i < Data.Length; i++)
				{
					byte elem_0 = Data[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(Flags);
			encoder.WriteUniquePointer(RecordNumber);
			if (RecordNumber is not null)
			{
				encoder.WriteValue(RecordNumber.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			RecordNumber = decoder.ReadOutUniquePointer<uint>(RecordNumber);
			if (RecordNumber is not null)
			{
				RecordNumber.value = decoder.ReadUInt32();
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> ElfrReportEventExA(RpcContextHandle LogHandle, ms_dtyp.FILETIME TimeGenerated, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, RPC_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<RPC_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(LogHandle);
			encoder.WriteFixedStruct(TimeGenerated, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(TimeGenerated);
			encoder.WriteValue(EventType);
			encoder.WriteValue(EventCategory);
			encoder.WriteValue(EventID);
			encoder.WriteValue(NumStrings);
			encoder.WriteValue(DataSize);
			encoder.WriteFixedStruct(ComputerName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ComputerName);
			encoder.WriteUniquePointer(UserSID);
			if (UserSID is not null)
			{
				encoder.WriteConformantStruct(UserSID.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(UserSID.value);
			}

			encoder.WriteArrayHeader(Strings);
			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<RPC_STRING> elem_0 = Strings[i];
				encoder.WriteUniquePointer(elem_0);
			}

			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<RPC_STRING> elem_0 = Strings[i];
				if (elem_0 is not null)
				{
					encoder.WriteFixedStruct(elem_0.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}

			encoder.WriteUniqueReferentId(Data is null);
			if (Data is not null)
			{
				encoder.WriteArrayHeader(Data);
				for (int i = 0; i < Data.Length; i++)
				{
					byte elem_0 = Data[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(Flags);
			encoder.WriteUniquePointer(RecordNumber);
			if (RecordNumber is not null)
			{
				encoder.WriteValue(RecordNumber.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			RecordNumber = decoder.ReadOutUniquePointer<uint>(RecordNumber);
			if (RecordNumber is not null)
			{
				RecordNumber.value = decoder.ReadUInt32();
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(eventlog);
		private static Guid _interfaceUuid = new Guid("82273fdc-e32a-18c3-3f78-827929dc23ea");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class eventlogStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrClearELFW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> BackupFileName;
			LogHandle = decoder.ReadContextHandle();
			BackupFileName = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
			if (BackupFileName is not null)
			{
				BackupFileName.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref BackupFileName.value);
			}

			var invokeTask = this._obj.ElfrClearELFW(LogHandle, BackupFileName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrBackupELFW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			ms_dtyp.RPC_UNICODE_STRING BackupFileName;
			LogHandle = decoder.ReadContextHandle();
			BackupFileName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref BackupFileName);
			var invokeTask = this._obj.ElfrBackupELFW(LogHandle, BackupFileName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrCloseEL(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> LogHandle;
			LogHandle = new RpcPointer<RpcContextHandle>();
			LogHandle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.ElfrCloseEL(LogHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(LogHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrDeregisterEventSource(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> LogHandle;
			LogHandle = new RpcPointer<RpcContextHandle>();
			LogHandle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.ElfrDeregisterEventSource(LogHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(LogHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrNumberOfRecords(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			RpcPointer<uint> NumberOfRecords = new RpcPointer<uint>();
			LogHandle = decoder.ReadContextHandle();
			var invokeTask = this._obj.ElfrNumberOfRecords(LogHandle, NumberOfRecords, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(NumberOfRecords.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrOldestRecord(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			RpcPointer<uint> OldestRecordNumber = new RpcPointer<uint>();
			LogHandle = decoder.ReadContextHandle();
			var invokeTask = this._obj.ElfrOldestRecord(LogHandle, OldestRecordNumber, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(OldestRecordNumber.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrChangeNotify(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			RPC_CLIENT_ID ClientId;
			uint Event;
			LogHandle = decoder.ReadContextHandle();
			ClientId = decoder.ReadFixedStruct<RPC_CLIENT_ID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<RPC_CLIENT_ID>(ref ClientId);
			Event = decoder.ReadUInt32();
			var invokeTask = this._obj.ElfrChangeNotify(LogHandle, ClientId, Event, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrOpenELW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> UNCServerName;
			ms_dtyp.RPC_UNICODE_STRING ModuleName;
			ms_dtyp.RPC_UNICODE_STRING RegModuleName;
			uint MajorVersion;
			uint MinorVersion;
			RpcPointer<RpcContextHandle> LogHandle = new RpcPointer<RpcContextHandle>();
			UNCServerName = decoder.ReadUniquePointer<char>();
			if (UNCServerName is not null)
			{
				UNCServerName.value = decoder.ReadWideChar();
			}

			ModuleName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref ModuleName);
			RegModuleName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref RegModuleName);
			MajorVersion = decoder.ReadUInt32();
			MinorVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.ElfrOpenELW(UNCServerName, ModuleName, RegModuleName, MajorVersion, MinorVersion, LogHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(LogHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrRegisterEventSourceW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> UNCServerName;
			ms_dtyp.RPC_UNICODE_STRING ModuleName;
			ms_dtyp.RPC_UNICODE_STRING RegModuleName;
			uint MajorVersion;
			uint MinorVersion;
			RpcPointer<RpcContextHandle> LogHandle = new RpcPointer<RpcContextHandle>();
			UNCServerName = decoder.ReadUniquePointer<char>();
			if (UNCServerName is not null)
			{
				UNCServerName.value = decoder.ReadWideChar();
			}

			ModuleName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref ModuleName);
			RegModuleName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref RegModuleName);
			MajorVersion = decoder.ReadUInt32();
			MinorVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.ElfrRegisterEventSourceW(UNCServerName, ModuleName, RegModuleName, MajorVersion, MinorVersion, LogHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(LogHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrOpenBELW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> UNCServerName;
			ms_dtyp.RPC_UNICODE_STRING BackupFileName;
			uint MajorVersion;
			uint MinorVersion;
			RpcPointer<RpcContextHandle> LogHandle = new RpcPointer<RpcContextHandle>();
			UNCServerName = decoder.ReadUniquePointer<char>();
			if (UNCServerName is not null)
			{
				UNCServerName.value = decoder.ReadWideChar();
			}

			BackupFileName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref BackupFileName);
			MajorVersion = decoder.ReadUInt32();
			MinorVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.ElfrOpenBELW(UNCServerName, BackupFileName, MajorVersion, MinorVersion, LogHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(LogHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrReadELW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			uint ReadFlags;
			uint RecordOffset;
			uint NumberOfBytesToRead;
			RpcPointer<byte[]> Buffer = new RpcPointer<byte[]>();
			RpcPointer<uint> NumberOfBytesRead = new RpcPointer<uint>();
			RpcPointer<uint> MinNumberOfBytesNeeded = new RpcPointer<uint>();
			LogHandle = decoder.ReadContextHandle();
			ReadFlags = decoder.ReadUInt32();
			RecordOffset = decoder.ReadUInt32();
			NumberOfBytesToRead = decoder.ReadUInt32();
			var invokeTask = this._obj.ElfrReadELW(LogHandle, ReadFlags, RecordOffset, NumberOfBytesToRead, Buffer, NumberOfBytesRead, MinNumberOfBytesNeeded, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(Buffer.value);
			for (int i = 0; i < Buffer.value.Length; i++)
			{
				byte elem_0 = Buffer.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(NumberOfBytesRead.value);
			encoder.WriteValue(MinNumberOfBytesNeeded.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrReportEventW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			uint Time;
			ushort EventType;
			ushort EventCategory;
			uint EventID;
			ushort NumStrings;
			uint DataSize;
			ms_dtyp.RPC_UNICODE_STRING ComputerName;
			RpcPointer<ms_dtyp.RPC_SID> UserSID;
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings;
			byte[] Data;
			ushort Flags;
			RpcPointer<uint> RecordNumber;
			RpcPointer<uint> TimeWritten;
			LogHandle = decoder.ReadContextHandle();
			Time = decoder.ReadUInt32();
			EventType = decoder.ReadUInt16();
			EventCategory = decoder.ReadUInt16();
			EventID = decoder.ReadUInt32();
			NumStrings = decoder.ReadUInt16();
			DataSize = decoder.ReadUInt32();
			ComputerName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref ComputerName);
			UserSID = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			if (UserSID is not null)
			{
				UserSID.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref UserSID.value);
			}

			Strings = decoder.ReadArrayHeader<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
				elem_0 = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
				Strings[i] = elem_0;
			}

			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0.value);
				}

				Strings[i] = elem_0;
			}

			Data = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < Data.Length; i++)
			{
				byte elem_0 = Data[i];
				elem_0 = decoder.ReadUnsignedChar();
				Data[i] = elem_0;
			}

			Flags = decoder.ReadUInt16();
			RecordNumber = decoder.ReadUniquePointer<uint>();
			if (RecordNumber is not null)
			{
				RecordNumber.value = decoder.ReadUInt32();
			}

			TimeWritten = decoder.ReadUniquePointer<uint>();
			if (TimeWritten is not null)
			{
				TimeWritten.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.ElfrReportEventW(LogHandle, Time, EventType, EventCategory, EventID, NumStrings, DataSize, ComputerName, UserSID, Strings, Data, Flags, RecordNumber, TimeWritten, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(RecordNumber);
			if (RecordNumber is not null)
			{
				encoder.WriteValue(RecordNumber.value);
			}

			encoder.WriteUniquePointer(TimeWritten);
			if (TimeWritten is not null)
			{
				encoder.WriteValue(TimeWritten.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrClearELFA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			RpcPointer<RPC_STRING> BackupFileName;
			LogHandle = decoder.ReadContextHandle();
			BackupFileName = decoder.ReadUniquePointer<RPC_STRING>();
			if (BackupFileName is not null)
			{
				BackupFileName.value = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<RPC_STRING>(ref BackupFileName.value);
			}

			var invokeTask = this._obj.ElfrClearELFA(LogHandle, BackupFileName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrBackupELFA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			RPC_STRING BackupFileName;
			LogHandle = decoder.ReadContextHandle();
			BackupFileName = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<RPC_STRING>(ref BackupFileName);
			var invokeTask = this._obj.ElfrBackupELFA(LogHandle, BackupFileName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrOpenELA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<byte> UNCServerName;
			RPC_STRING ModuleName;
			RPC_STRING RegModuleName;
			uint MajorVersion;
			uint MinorVersion;
			RpcPointer<RpcContextHandle> LogHandle = new RpcPointer<RpcContextHandle>();
			UNCServerName = decoder.ReadUniquePointer<byte>();
			if (UNCServerName is not null)
			{
				UNCServerName.value = decoder.ReadUnsignedChar();
			}

			ModuleName = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<RPC_STRING>(ref ModuleName);
			RegModuleName = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<RPC_STRING>(ref RegModuleName);
			MajorVersion = decoder.ReadUInt32();
			MinorVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.ElfrOpenELA(UNCServerName, ModuleName, RegModuleName, MajorVersion, MinorVersion, LogHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(LogHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrRegisterEventSourceA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<byte> UNCServerName;
			RPC_STRING ModuleName;
			RPC_STRING RegModuleName;
			uint MajorVersion;
			uint MinorVersion;
			RpcPointer<RpcContextHandle> LogHandle = new RpcPointer<RpcContextHandle>();
			UNCServerName = decoder.ReadUniquePointer<byte>();
			if (UNCServerName is not null)
			{
				UNCServerName.value = decoder.ReadUnsignedChar();
			}

			ModuleName = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<RPC_STRING>(ref ModuleName);
			RegModuleName = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<RPC_STRING>(ref RegModuleName);
			MajorVersion = decoder.ReadUInt32();
			MinorVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.ElfrRegisterEventSourceA(UNCServerName, ModuleName, RegModuleName, MajorVersion, MinorVersion, LogHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(LogHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrOpenBELA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<byte> UNCServerName;
			RPC_STRING BackupFileName;
			uint MajorVersion;
			uint MinorVersion;
			RpcPointer<RpcContextHandle> LogHandle = new RpcPointer<RpcContextHandle>();
			UNCServerName = decoder.ReadUniquePointer<byte>();
			if (UNCServerName is not null)
			{
				UNCServerName.value = decoder.ReadUnsignedChar();
			}

			BackupFileName = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<RPC_STRING>(ref BackupFileName);
			MajorVersion = decoder.ReadUInt32();
			MinorVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.ElfrOpenBELA(UNCServerName, BackupFileName, MajorVersion, MinorVersion, LogHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(LogHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrReadELA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			uint ReadFlags;
			uint RecordOffset;
			uint NumberOfBytesToRead;
			RpcPointer<byte[]> Buffer = new RpcPointer<byte[]>();
			RpcPointer<uint> NumberOfBytesRead = new RpcPointer<uint>();
			RpcPointer<uint> MinNumberOfBytesNeeded = new RpcPointer<uint>();
			LogHandle = decoder.ReadContextHandle();
			ReadFlags = decoder.ReadUInt32();
			RecordOffset = decoder.ReadUInt32();
			NumberOfBytesToRead = decoder.ReadUInt32();
			var invokeTask = this._obj.ElfrReadELA(LogHandle, ReadFlags, RecordOffset, NumberOfBytesToRead, Buffer, NumberOfBytesRead, MinNumberOfBytesNeeded, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(Buffer.value);
			for (int i = 0; i < Buffer.value.Length; i++)
			{
				byte elem_0 = Buffer.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(NumberOfBytesRead.value);
			encoder.WriteValue(MinNumberOfBytesNeeded.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrReportEventA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			uint Time;
			ushort EventType;
			ushort EventCategory;
			uint EventID;
			ushort NumStrings;
			uint DataSize;
			RPC_STRING ComputerName;
			RpcPointer<ms_dtyp.RPC_SID> UserSID;
			RpcPointer<RPC_STRING>[] Strings;
			byte[] Data;
			ushort Flags;
			RpcPointer<uint> RecordNumber;
			RpcPointer<uint> TimeWritten;
			LogHandle = decoder.ReadContextHandle();
			Time = decoder.ReadUInt32();
			EventType = decoder.ReadUInt16();
			EventCategory = decoder.ReadUInt16();
			EventID = decoder.ReadUInt32();
			NumStrings = decoder.ReadUInt16();
			DataSize = decoder.ReadUInt32();
			ComputerName = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<RPC_STRING>(ref ComputerName);
			UserSID = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			if (UserSID is not null)
			{
				UserSID.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref UserSID.value);
			}

			Strings = decoder.ReadArrayHeader<RpcPointer<RPC_STRING>>();
			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<RPC_STRING> elem_0 = Strings[i];
				elem_0 = decoder.ReadUniquePointer<RPC_STRING>();
				Strings[i] = elem_0;
			}

			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<RPC_STRING> elem_0 = Strings[i];
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<RPC_STRING>(ref elem_0.value);
				}

				Strings[i] = elem_0;
			}

			Data = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < Data.Length; i++)
			{
				byte elem_0 = Data[i];
				elem_0 = decoder.ReadUnsignedChar();
				Data[i] = elem_0;
			}

			Flags = decoder.ReadUInt16();
			RecordNumber = decoder.ReadUniquePointer<uint>();
			if (RecordNumber is not null)
			{
				RecordNumber.value = decoder.ReadUInt32();
			}

			TimeWritten = decoder.ReadUniquePointer<uint>();
			if (TimeWritten is not null)
			{
				TimeWritten.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.ElfrReportEventA(LogHandle, Time, EventType, EventCategory, EventID, NumStrings, DataSize, ComputerName, UserSID, Strings, Data, Flags, RecordNumber, TimeWritten, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(RecordNumber);
			if (RecordNumber is not null)
			{
				encoder.WriteValue(RecordNumber.value);
			}

			encoder.WriteUniquePointer(TimeWritten);
			if (TimeWritten is not null)
			{
				encoder.WriteValue(TimeWritten.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum19NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum19NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum20NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum20NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum21NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum21NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrGetLogInformation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			uint InfoLevel;
			RpcPointer<byte[]> lpBuffer = new RpcPointer<byte[]>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			LogHandle = decoder.ReadContextHandle();
			InfoLevel = decoder.ReadUInt32();
			cbBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.ElfrGetLogInformation(LogHandle, InfoLevel, lpBuffer, cbBufSize, pcbBytesNeeded, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(lpBuffer.value);
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum23NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum23NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrReportEventAndSourceW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			uint Time;
			ushort EventType;
			ushort EventCategory;
			uint EventID;
			ms_dtyp.RPC_UNICODE_STRING SourceName;
			ushort NumStrings;
			uint DataSize;
			ms_dtyp.RPC_UNICODE_STRING ComputerName;
			RpcPointer<ms_dtyp.RPC_SID> UserSID;
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings;
			byte[] Data;
			ushort Flags;
			RpcPointer<uint> RecordNumber;
			RpcPointer<uint> TimeWritten;
			LogHandle = decoder.ReadContextHandle();
			Time = decoder.ReadUInt32();
			EventType = decoder.ReadUInt16();
			EventCategory = decoder.ReadUInt16();
			EventID = decoder.ReadUInt32();
			SourceName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref SourceName);
			NumStrings = decoder.ReadUInt16();
			DataSize = decoder.ReadUInt32();
			ComputerName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref ComputerName);
			UserSID = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			if (UserSID is not null)
			{
				UserSID.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref UserSID.value);
			}

			Strings = decoder.ReadArrayHeader<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
				elem_0 = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
				Strings[i] = elem_0;
			}

			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0.value);
				}

				Strings[i] = elem_0;
			}

			Data = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < Data.Length; i++)
			{
				byte elem_0 = Data[i];
				elem_0 = decoder.ReadUnsignedChar();
				Data[i] = elem_0;
			}

			Flags = decoder.ReadUInt16();
			RecordNumber = decoder.ReadUniquePointer<uint>();
			if (RecordNumber is not null)
			{
				RecordNumber.value = decoder.ReadUInt32();
			}

			TimeWritten = decoder.ReadUniquePointer<uint>();
			if (TimeWritten is not null)
			{
				TimeWritten.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.ElfrReportEventAndSourceW(LogHandle, Time, EventType, EventCategory, EventID, SourceName, NumStrings, DataSize, ComputerName, UserSID, Strings, Data, Flags, RecordNumber, TimeWritten, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(RecordNumber);
			if (RecordNumber is not null)
			{
				encoder.WriteValue(RecordNumber.value);
			}

			encoder.WriteUniquePointer(TimeWritten);
			if (TimeWritten is not null)
			{
				encoder.WriteValue(TimeWritten.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrReportEventExW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			ms_dtyp.FILETIME TimeGenerated;
			ushort EventType;
			ushort EventCategory;
			uint EventID;
			ushort NumStrings;
			uint DataSize;
			ms_dtyp.RPC_UNICODE_STRING ComputerName;
			RpcPointer<ms_dtyp.RPC_SID> UserSID;
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings;
			byte[] Data;
			ushort Flags;
			RpcPointer<uint> RecordNumber;
			LogHandle = decoder.ReadContextHandle();
			TimeGenerated = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref TimeGenerated);
			EventType = decoder.ReadUInt16();
			EventCategory = decoder.ReadUInt16();
			EventID = decoder.ReadUInt32();
			NumStrings = decoder.ReadUInt16();
			DataSize = decoder.ReadUInt32();
			ComputerName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref ComputerName);
			UserSID = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			if (UserSID is not null)
			{
				UserSID.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref UserSID.value);
			}

			Strings = decoder.ReadArrayHeader<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
				elem_0 = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
				Strings[i] = elem_0;
			}

			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0.value);
				}

				Strings[i] = elem_0;
			}

			Data = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < Data.Length; i++)
			{
				byte elem_0 = Data[i];
				elem_0 = decoder.ReadUnsignedChar();
				Data[i] = elem_0;
			}

			Flags = decoder.ReadUInt16();
			RecordNumber = decoder.ReadUniquePointer<uint>();
			if (RecordNumber is not null)
			{
				RecordNumber.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.ElfrReportEventExW(LogHandle, TimeGenerated, EventType, EventCategory, EventID, NumStrings, DataSize, ComputerName, UserSID, Strings, Data, Flags, RecordNumber, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(RecordNumber);
			if (RecordNumber is not null)
			{
				encoder.WriteValue(RecordNumber.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ElfrReportEventExA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle LogHandle;
			ms_dtyp.FILETIME TimeGenerated;
			ushort EventType;
			ushort EventCategory;
			uint EventID;
			ushort NumStrings;
			uint DataSize;
			RPC_STRING ComputerName;
			RpcPointer<ms_dtyp.RPC_SID> UserSID;
			RpcPointer<RPC_STRING>[] Strings;
			byte[] Data;
			ushort Flags;
			RpcPointer<uint> RecordNumber;
			LogHandle = decoder.ReadContextHandle();
			TimeGenerated = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref TimeGenerated);
			EventType = decoder.ReadUInt16();
			EventCategory = decoder.ReadUInt16();
			EventID = decoder.ReadUInt32();
			NumStrings = decoder.ReadUInt16();
			DataSize = decoder.ReadUInt32();
			ComputerName = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<RPC_STRING>(ref ComputerName);
			UserSID = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			if (UserSID is not null)
			{
				UserSID.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref UserSID.value);
			}

			Strings = decoder.ReadArrayHeader<RpcPointer<RPC_STRING>>();
			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<RPC_STRING> elem_0 = Strings[i];
				elem_0 = decoder.ReadUniquePointer<RPC_STRING>();
				Strings[i] = elem_0;
			}

			for (int i = 0; i < Strings.Length; i++)
			{
				RpcPointer<RPC_STRING> elem_0 = Strings[i];
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadFixedStruct<RPC_STRING>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<RPC_STRING>(ref elem_0.value);
				}

				Strings[i] = elem_0;
			}

			Data = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < Data.Length; i++)
			{
				byte elem_0 = Data[i];
				elem_0 = decoder.ReadUnsignedChar();
				Data[i] = elem_0;
			}

			Flags = decoder.ReadUInt16();
			RecordNumber = decoder.ReadUniquePointer<uint>();
			if (RecordNumber is not null)
			{
				RecordNumber.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.ElfrReportEventExA(LogHandle, TimeGenerated, EventType, EventCategory, EventID, NumStrings, DataSize, ComputerName, UserSID, Strings, Data, Flags, RecordNumber, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(RecordNumber);
			if (RecordNumber is not null)
			{
				encoder.WriteValue(RecordNumber.value);
			}

			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("82273fdc-e32a-18c3-3f78-827929dc23ea");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private eventlog _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public eventlogStub(eventlog obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_ElfrClearELFW, this.Invoke_ElfrBackupELFW, this.Invoke_ElfrCloseEL, this.Invoke_ElfrDeregisterEventSource, this.Invoke_ElfrNumberOfRecords, this.Invoke_ElfrOldestRecord, this.Invoke_ElfrChangeNotify, this.Invoke_ElfrOpenELW, this.Invoke_ElfrRegisterEventSourceW, this.Invoke_ElfrOpenBELW, this.Invoke_ElfrReadELW, this.Invoke_ElfrReportEventW, this.Invoke_ElfrClearELFA, this.Invoke_ElfrBackupELFA, this.Invoke_ElfrOpenELA, this.Invoke_ElfrRegisterEventSourceA, this.Invoke_ElfrOpenBELA, this.Invoke_ElfrReadELA, this.Invoke_ElfrReportEventA, this.Invoke_Opnum19NotUsedOnWire, this.Invoke_Opnum20NotUsedOnWire, this.Invoke_Opnum21NotUsedOnWire, this.Invoke_ElfrGetLogInformation, this.Invoke_Opnum23NotUsedOnWire, this.Invoke_ElfrReportEventAndSourceW, this.Invoke_ElfrReportEventExW, this.Invoke_ElfrReportEventExA};
		}
	}
}