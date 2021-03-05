#pragma warning disable

namespace ms_even {
    using System;
    using System.Threading.Tasks;
    using Titanis;
    using Titanis.DceRpc;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct RPC_STRING : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Length);
            encoder.WriteValue(this.MaximumLength);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Length = decoder.ReadUInt16();
            this.MaximumLength = decoder.ReadUInt16();
            this.Buffer = decoder.ReadUniquePointer<byte[]>();
        }
        public ushort Length;
        public ushort MaximumLength;
        public RpcPointer<byte[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    byte elem_0 = this.Buffer.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    byte elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct RPC_CLIENT_ID : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.UniqueProcess);
            encoder.WriteValue(this.UniqueThread);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UniqueProcess = decoder.ReadUInt32();
            this.UniqueThread = decoder.ReadUInt32();
        }
        public uint UniqueProcess;
        public uint UniqueThread;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [System.Runtime.InteropServices.GuidAttribute("82273fdc-e32a-18c3-3f78-827929dc23ea")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface eventlog {
        Task<int> ElfrClearELFW(Titanis.DceRpc.RpcContextHandle LogHandle, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> BackupFileName, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrBackupELFW(Titanis.DceRpc.RpcContextHandle LogHandle, ms_dtyp.RPC_UNICODE_STRING BackupFileName, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrCloseEL(RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrDeregisterEventSource(RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrNumberOfRecords(Titanis.DceRpc.RpcContextHandle LogHandle, RpcPointer<uint> NumberOfRecords, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrOldestRecord(Titanis.DceRpc.RpcContextHandle LogHandle, RpcPointer<uint> OldestRecordNumber, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrChangeNotify(Titanis.DceRpc.RpcContextHandle LogHandle, RPC_CLIENT_ID ClientId, uint Event, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrOpenELW(RpcPointer<char> UNCServerName, ms_dtyp.RPC_UNICODE_STRING ModuleName, ms_dtyp.RPC_UNICODE_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrRegisterEventSourceW(RpcPointer<char> UNCServerName, ms_dtyp.RPC_UNICODE_STRING ModuleName, ms_dtyp.RPC_UNICODE_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrOpenBELW(RpcPointer<char> UNCServerName, ms_dtyp.RPC_UNICODE_STRING BackupFileName, uint MajorVersion, uint MinorVersion, RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrReadELW(Titanis.DceRpc.RpcContextHandle LogHandle, uint ReadFlags, uint RecordOffset, uint NumberOfBytesToRead, RpcPointer<byte[]> Buffer, RpcPointer<uint> NumberOfBytesRead, RpcPointer<uint> MinNumberOfBytesNeeded, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrReportEventW(Titanis.DceRpc.RpcContextHandle LogHandle, uint Time, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, ms_dtyp.RPC_UNICODE_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, RpcPointer<uint> TimeWritten, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrClearELFA(Titanis.DceRpc.RpcContextHandle LogHandle, RpcPointer<RPC_STRING> BackupFileName, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrBackupELFA(Titanis.DceRpc.RpcContextHandle LogHandle, RPC_STRING BackupFileName, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrOpenELA(RpcPointer<byte> UNCServerName, RPC_STRING ModuleName, RPC_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrRegisterEventSourceA(RpcPointer<byte> UNCServerName, RPC_STRING ModuleName, RPC_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrOpenBELA(RpcPointer<byte> UNCServerName, RPC_STRING BackupFileName, uint MajorVersion, uint MinorVersion, RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrReadELA(Titanis.DceRpc.RpcContextHandle LogHandle, uint ReadFlags, uint RecordOffset, uint NumberOfBytesToRead, RpcPointer<byte[]> Buffer, RpcPointer<uint> NumberOfBytesRead, RpcPointer<uint> MinNumberOfBytesNeeded, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrReportEventA(Titanis.DceRpc.RpcContextHandle LogHandle, uint Time, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, RPC_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<RPC_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, RpcPointer<uint> TimeWritten, System.Threading.CancellationToken cancellationToken);
        Task Opnum19NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum20NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum21NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrGetLogInformation(Titanis.DceRpc.RpcContextHandle LogHandle, uint InfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken);
        Task Opnum23NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrReportEventAndSourceW(
                    Titanis.DceRpc.RpcContextHandle LogHandle, 
                    uint Time, 
                    ushort EventType, 
                    ushort EventCategory, 
                    uint EventID, 
                    ms_dtyp.RPC_UNICODE_STRING SourceName, 
                    ushort NumStrings, 
                    uint DataSize, 
                    ms_dtyp.RPC_UNICODE_STRING ComputerName, 
                    RpcPointer<ms_dtyp.RPC_SID> UserSID, 
                    RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings, 
                    byte[] Data, 
                    ushort Flags, 
                    RpcPointer<uint> RecordNumber, 
                    RpcPointer<uint> TimeWritten, 
                    System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrReportEventExW(Titanis.DceRpc.RpcContextHandle LogHandle, ms_dtyp.FILETIME TimeGenerated, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, ms_dtyp.RPC_UNICODE_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, System.Threading.CancellationToken cancellationToken);
        Task<int> ElfrReportEventExA(Titanis.DceRpc.RpcContextHandle LogHandle, ms_dtyp.FILETIME TimeGenerated, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, RPC_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<RPC_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [Titanis.DceRpc.IidAttribute("82273fdc-e32a-18c3-3f78-827929dc23ea")]
    public class eventlogClientProxy : Titanis.DceRpc.Client.RpcClientProxy, eventlog, Titanis.DceRpc.IRpcClientProxy {
        private static System.Guid _interfaceUuid = new System.Guid("82273fdc-e32a-18c3-3f78-827929dc23ea");
        public override System.Guid InterfaceUuid {
            get {
                return _interfaceUuid;
            }
        }
        public override Titanis.DceRpc.RpcVersion InterfaceVersion {
            get {
                return new Titanis.DceRpc.RpcVersion(0, 0);
            }
        }
        public virtual async Task<int> ElfrClearELFW(Titanis.DceRpc.RpcContextHandle LogHandle, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> BackupFileName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WritePointer(BackupFileName);
            if ((null != BackupFileName)) {
                encoder.WriteFixedStruct(BackupFileName.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(BackupFileName.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrBackupELFW(Titanis.DceRpc.RpcContextHandle LogHandle, ms_dtyp.RPC_UNICODE_STRING BackupFileName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WriteFixedStruct(BackupFileName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(BackupFileName);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrCloseEL(RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            LogHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrDeregisterEventSource(RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            LogHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrNumberOfRecords(Titanis.DceRpc.RpcContextHandle LogHandle, RpcPointer<uint> NumberOfRecords, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            NumberOfRecords.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrOldestRecord(Titanis.DceRpc.RpcContextHandle LogHandle, RpcPointer<uint> OldestRecordNumber, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            OldestRecordNumber.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrChangeNotify(Titanis.DceRpc.RpcContextHandle LogHandle, RPC_CLIENT_ID ClientId, uint Event, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WriteFixedStruct(ClientId, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(ClientId);
            encoder.WriteValue(Event);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrOpenELW(RpcPointer<char> UNCServerName, ms_dtyp.RPC_UNICODE_STRING ModuleName, ms_dtyp.RPC_UNICODE_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(UNCServerName);
            if ((null != UNCServerName)) {
                encoder.WriteValue(UNCServerName.value);
            }
            encoder.WriteFixedStruct(ModuleName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ModuleName);
            encoder.WriteFixedStruct(RegModuleName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(RegModuleName);
            encoder.WriteValue(MajorVersion);
            encoder.WriteValue(MinorVersion);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            LogHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrRegisterEventSourceW(RpcPointer<char> UNCServerName, ms_dtyp.RPC_UNICODE_STRING ModuleName, ms_dtyp.RPC_UNICODE_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(UNCServerName);
            if ((null != UNCServerName)) {
                encoder.WriteValue(UNCServerName.value);
            }
            encoder.WriteFixedStruct(ModuleName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ModuleName);
            encoder.WriteFixedStruct(RegModuleName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(RegModuleName);
            encoder.WriteValue(MajorVersion);
            encoder.WriteValue(MinorVersion);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            LogHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrOpenBELW(RpcPointer<char> UNCServerName, ms_dtyp.RPC_UNICODE_STRING BackupFileName, uint MajorVersion, uint MinorVersion, RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(UNCServerName);
            if ((null != UNCServerName)) {
                encoder.WriteValue(UNCServerName.value);
            }
            encoder.WriteFixedStruct(BackupFileName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(BackupFileName);
            encoder.WriteValue(MajorVersion);
            encoder.WriteValue(MinorVersion);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            LogHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrReadELW(Titanis.DceRpc.RpcContextHandle LogHandle, uint ReadFlags, uint RecordOffset, uint NumberOfBytesToRead, RpcPointer<byte[]> Buffer, RpcPointer<uint> NumberOfBytesRead, RpcPointer<uint> MinNumberOfBytesNeeded, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WriteValue(ReadFlags);
            encoder.WriteValue(RecordOffset);
            encoder.WriteValue(NumberOfBytesToRead);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Buffer.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < Buffer.value.Length); i++
            ) {
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
        public virtual async Task<int> ElfrReportEventW(Titanis.DceRpc.RpcContextHandle LogHandle, uint Time, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, ms_dtyp.RPC_UNICODE_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, RpcPointer<uint> TimeWritten, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WriteValue(Time);
            encoder.WriteValue(EventType);
            encoder.WriteValue(EventCategory);
            encoder.WriteValue(EventID);
            encoder.WriteValue(NumStrings);
            encoder.WriteValue(DataSize);
            encoder.WriteFixedStruct(ComputerName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ComputerName);
            encoder.WritePointer(UserSID);
            if ((null != UserSID)) {
                encoder.WriteConformantStruct(UserSID.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(UserSID.value);
            }
            encoder.WriteArrayHeader(Strings);
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
                encoder.WritePointer(elem_0);
            }
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
                if ((null != elem_0)) {
                    encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
            encoder.WriteUniqueReferentId((Data == null));
            if ((Data != null)) {
                encoder.WriteArrayHeader(Data);
                for (int i = 0; (i < Data.Length); i++
                ) {
                    byte elem_0 = Data[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(Flags);
            encoder.WritePointer(RecordNumber);
            if ((null != RecordNumber)) {
                encoder.WriteValue(RecordNumber.value);
            }
            encoder.WritePointer(TimeWritten);
            if ((null != TimeWritten)) {
                encoder.WriteValue(TimeWritten.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            RecordNumber = decoder.ReadOutUniquePointer<uint>(RecordNumber);
            if ((null != RecordNumber)) {
                RecordNumber.value = decoder.ReadUInt32();
            }
            TimeWritten = decoder.ReadOutUniquePointer<uint>(TimeWritten);
            if ((null != TimeWritten)) {
                TimeWritten.value = decoder.ReadUInt32();
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrClearELFA(Titanis.DceRpc.RpcContextHandle LogHandle, RpcPointer<RPC_STRING> BackupFileName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WritePointer(BackupFileName);
            if ((null != BackupFileName)) {
                encoder.WriteFixedStruct(BackupFileName.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(BackupFileName.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrBackupELFA(Titanis.DceRpc.RpcContextHandle LogHandle, RPC_STRING BackupFileName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WriteFixedStruct(BackupFileName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(BackupFileName);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrOpenELA(RpcPointer<byte> UNCServerName, RPC_STRING ModuleName, RPC_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(UNCServerName);
            if ((null != UNCServerName)) {
                encoder.WriteValue(UNCServerName.value);
            }
            encoder.WriteFixedStruct(ModuleName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ModuleName);
            encoder.WriteFixedStruct(RegModuleName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(RegModuleName);
            encoder.WriteValue(MajorVersion);
            encoder.WriteValue(MinorVersion);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            LogHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrRegisterEventSourceA(RpcPointer<byte> UNCServerName, RPC_STRING ModuleName, RPC_STRING RegModuleName, uint MajorVersion, uint MinorVersion, RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(UNCServerName);
            if ((null != UNCServerName)) {
                encoder.WriteValue(UNCServerName.value);
            }
            encoder.WriteFixedStruct(ModuleName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ModuleName);
            encoder.WriteFixedStruct(RegModuleName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(RegModuleName);
            encoder.WriteValue(MajorVersion);
            encoder.WriteValue(MinorVersion);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            LogHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrOpenBELA(RpcPointer<byte> UNCServerName, RPC_STRING BackupFileName, uint MajorVersion, uint MinorVersion, RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(UNCServerName);
            if ((null != UNCServerName)) {
                encoder.WriteValue(UNCServerName.value);
            }
            encoder.WriteFixedStruct(BackupFileName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(BackupFileName);
            encoder.WriteValue(MajorVersion);
            encoder.WriteValue(MinorVersion);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            LogHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrReadELA(Titanis.DceRpc.RpcContextHandle LogHandle, uint ReadFlags, uint RecordOffset, uint NumberOfBytesToRead, RpcPointer<byte[]> Buffer, RpcPointer<uint> NumberOfBytesRead, RpcPointer<uint> MinNumberOfBytesNeeded, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WriteValue(ReadFlags);
            encoder.WriteValue(RecordOffset);
            encoder.WriteValue(NumberOfBytesToRead);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Buffer.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < Buffer.value.Length); i++
            ) {
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
        public virtual async Task<int> ElfrReportEventA(Titanis.DceRpc.RpcContextHandle LogHandle, uint Time, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, RPC_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<RPC_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, RpcPointer<uint> TimeWritten, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WriteValue(Time);
            encoder.WriteValue(EventType);
            encoder.WriteValue(EventCategory);
            encoder.WriteValue(EventID);
            encoder.WriteValue(NumStrings);
            encoder.WriteValue(DataSize);
            encoder.WriteFixedStruct(ComputerName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ComputerName);
            encoder.WritePointer(UserSID);
            if ((null != UserSID)) {
                encoder.WriteConformantStruct(UserSID.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(UserSID.value);
            }
            encoder.WriteArrayHeader(Strings);
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<RPC_STRING> elem_0 = Strings[i];
                encoder.WritePointer(elem_0);
            }
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<RPC_STRING> elem_0 = Strings[i];
                if ((null != elem_0)) {
                    encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
            encoder.WriteUniqueReferentId((Data == null));
            if ((Data != null)) {
                encoder.WriteArrayHeader(Data);
                for (int i = 0; (i < Data.Length); i++
                ) {
                    byte elem_0 = Data[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(Flags);
            encoder.WritePointer(RecordNumber);
            if ((null != RecordNumber)) {
                encoder.WriteValue(RecordNumber.value);
            }
            encoder.WritePointer(TimeWritten);
            if ((null != TimeWritten)) {
                encoder.WriteValue(TimeWritten.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            RecordNumber = decoder.ReadOutUniquePointer<uint>(RecordNumber);
            if ((null != RecordNumber)) {
                RecordNumber.value = decoder.ReadUInt32();
            }
            TimeWritten = decoder.ReadOutUniquePointer<uint>(TimeWritten);
            if ((null != TimeWritten)) {
                TimeWritten.value = decoder.ReadUInt32();
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum19NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum20NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum21NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> ElfrGetLogInformation(Titanis.DceRpc.RpcContextHandle LogHandle, uint InfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WriteValue(InfoLevel);
            encoder.WriteValue(cbBufSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpBuffer.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpBuffer.value[i] = elem_0;
            }
            pcbBytesNeeded.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum23NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> ElfrReportEventAndSourceW(
                    Titanis.DceRpc.RpcContextHandle LogHandle, 
                    uint Time, 
                    ushort EventType, 
                    ushort EventCategory, 
                    uint EventID, 
                    ms_dtyp.RPC_UNICODE_STRING SourceName, 
                    ushort NumStrings, 
                    uint DataSize, 
                    ms_dtyp.RPC_UNICODE_STRING ComputerName, 
                    RpcPointer<ms_dtyp.RPC_SID> UserSID, 
                    RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings, 
                    byte[] Data, 
                    ushort Flags, 
                    RpcPointer<uint> RecordNumber, 
                    RpcPointer<uint> TimeWritten, 
                    System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WriteValue(Time);
            encoder.WriteValue(EventType);
            encoder.WriteValue(EventCategory);
            encoder.WriteValue(EventID);
            encoder.WriteFixedStruct(SourceName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(SourceName);
            encoder.WriteValue(NumStrings);
            encoder.WriteValue(DataSize);
            encoder.WriteFixedStruct(ComputerName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ComputerName);
            encoder.WritePointer(UserSID);
            if ((null != UserSID)) {
                encoder.WriteConformantStruct(UserSID.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(UserSID.value);
            }
            encoder.WriteArrayHeader(Strings);
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
                encoder.WritePointer(elem_0);
            }
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
                if ((null != elem_0)) {
                    encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
            encoder.WriteUniqueReferentId((Data == null));
            if ((Data != null)) {
                encoder.WriteArrayHeader(Data);
                for (int i = 0; (i < Data.Length); i++
                ) {
                    byte elem_0 = Data[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(Flags);
            encoder.WritePointer(RecordNumber);
            if ((null != RecordNumber)) {
                encoder.WriteValue(RecordNumber.value);
            }
            encoder.WritePointer(TimeWritten);
            if ((null != TimeWritten)) {
                encoder.WriteValue(TimeWritten.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            RecordNumber = decoder.ReadOutUniquePointer<uint>(RecordNumber);
            if ((null != RecordNumber)) {
                RecordNumber.value = decoder.ReadUInt32();
            }
            TimeWritten = decoder.ReadOutUniquePointer<uint>(TimeWritten);
            if ((null != TimeWritten)) {
                TimeWritten.value = decoder.ReadUInt32();
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrReportEventExW(Titanis.DceRpc.RpcContextHandle LogHandle, ms_dtyp.FILETIME TimeGenerated, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, ms_dtyp.RPC_UNICODE_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<ms_dtyp.RPC_UNICODE_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WriteFixedStruct(TimeGenerated, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(TimeGenerated);
            encoder.WriteValue(EventType);
            encoder.WriteValue(EventCategory);
            encoder.WriteValue(EventID);
            encoder.WriteValue(NumStrings);
            encoder.WriteValue(DataSize);
            encoder.WriteFixedStruct(ComputerName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ComputerName);
            encoder.WritePointer(UserSID);
            if ((null != UserSID)) {
                encoder.WriteConformantStruct(UserSID.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(UserSID.value);
            }
            encoder.WriteArrayHeader(Strings);
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
                encoder.WritePointer(elem_0);
            }
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
                if ((null != elem_0)) {
                    encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
            encoder.WriteUniqueReferentId((Data == null));
            if ((Data != null)) {
                encoder.WriteArrayHeader(Data);
                for (int i = 0; (i < Data.Length); i++
                ) {
                    byte elem_0 = Data[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(Flags);
            encoder.WritePointer(RecordNumber);
            if ((null != RecordNumber)) {
                encoder.WriteValue(RecordNumber.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            RecordNumber = decoder.ReadOutUniquePointer<uint>(RecordNumber);
            if ((null != RecordNumber)) {
                RecordNumber.value = decoder.ReadUInt32();
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> ElfrReportEventExA(Titanis.DceRpc.RpcContextHandle LogHandle, ms_dtyp.FILETIME TimeGenerated, ushort EventType, ushort EventCategory, uint EventID, ushort NumStrings, uint DataSize, RPC_STRING ComputerName, RpcPointer<ms_dtyp.RPC_SID> UserSID, RpcPointer<RPC_STRING>[] Strings, byte[] Data, ushort Flags, RpcPointer<uint> RecordNumber, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(LogHandle);
            encoder.WriteFixedStruct(TimeGenerated, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(TimeGenerated);
            encoder.WriteValue(EventType);
            encoder.WriteValue(EventCategory);
            encoder.WriteValue(EventID);
            encoder.WriteValue(NumStrings);
            encoder.WriteValue(DataSize);
            encoder.WriteFixedStruct(ComputerName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ComputerName);
            encoder.WritePointer(UserSID);
            if ((null != UserSID)) {
                encoder.WriteConformantStruct(UserSID.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(UserSID.value);
            }
            encoder.WriteArrayHeader(Strings);
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<RPC_STRING> elem_0 = Strings[i];
                encoder.WritePointer(elem_0);
            }
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<RPC_STRING> elem_0 = Strings[i];
                if ((null != elem_0)) {
                    encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
            encoder.WriteUniqueReferentId((Data == null));
            if ((Data != null)) {
                encoder.WriteArrayHeader(Data);
                for (int i = 0; (i < Data.Length); i++
                ) {
                    byte elem_0 = Data[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(Flags);
            encoder.WritePointer(RecordNumber);
            if ((null != RecordNumber)) {
                encoder.WriteValue(RecordNumber.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            RecordNumber = decoder.ReadOutUniquePointer<uint>(RecordNumber);
            if ((null != RecordNumber)) {
                RecordNumber.value = decoder.ReadUInt32();
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public class eventlogStub : Titanis.DceRpc.Server.RpcServiceStub {
        private static System.Guid _interfaceUuid = new System.Guid("82273fdc-e32a-18c3-3f78-827929dc23ea");
        public override System.Guid InterfaceUuid {
            get {
                return _interfaceUuid;
            }
        }
        public override Titanis.DceRpc.RpcVersion InterfaceVersion {
            get {
                return new Titanis.DceRpc.RpcVersion(0, 0);
            }
        }
        private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
        public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable {
            get {
                return this._dispatchTable;
            }
        }
        private eventlog _obj;
        public eventlogStub(eventlog obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_ElfrClearELFW,
                    this.Invoke_ElfrBackupELFW,
                    this.Invoke_ElfrCloseEL,
                    this.Invoke_ElfrDeregisterEventSource,
                    this.Invoke_ElfrNumberOfRecords,
                    this.Invoke_ElfrOldestRecord,
                    this.Invoke_ElfrChangeNotify,
                    this.Invoke_ElfrOpenELW,
                    this.Invoke_ElfrRegisterEventSourceW,
                    this.Invoke_ElfrOpenBELW,
                    this.Invoke_ElfrReadELW,
                    this.Invoke_ElfrReportEventW,
                    this.Invoke_ElfrClearELFA,
                    this.Invoke_ElfrBackupELFA,
                    this.Invoke_ElfrOpenELA,
                    this.Invoke_ElfrRegisterEventSourceA,
                    this.Invoke_ElfrOpenBELA,
                    this.Invoke_ElfrReadELA,
                    this.Invoke_ElfrReportEventA,
                    this.Invoke_Opnum19NotUsedOnWire,
                    this.Invoke_Opnum20NotUsedOnWire,
                    this.Invoke_Opnum21NotUsedOnWire,
                    this.Invoke_ElfrGetLogInformation,
                    this.Invoke_Opnum23NotUsedOnWire,
                    this.Invoke_ElfrReportEventAndSourceW,
                    this.Invoke_ElfrReportEventExW,
                    this.Invoke_ElfrReportEventExA};
        }
        private async Task Invoke_ElfrClearELFW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
            RpcPointer<ms_dtyp.RPC_UNICODE_STRING> BackupFileName;
            LogHandle = decoder.ReadContextHandle();
            BackupFileName = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
            if ((null != BackupFileName)) {
                BackupFileName.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref BackupFileName.value);
            }
            var invokeTask = this._obj.ElfrClearELFW(LogHandle, BackupFileName, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrBackupELFW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
            ms_dtyp.RPC_UNICODE_STRING BackupFileName;
            LogHandle = decoder.ReadContextHandle();
            BackupFileName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref BackupFileName);
            var invokeTask = this._obj.ElfrBackupELFW(LogHandle, BackupFileName, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrCloseEL(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle;
            LogHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            LogHandle.value = decoder.ReadContextHandle();
            var invokeTask = this._obj.ElfrCloseEL(LogHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(LogHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrDeregisterEventSource(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle;
            LogHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            LogHandle.value = decoder.ReadContextHandle();
            var invokeTask = this._obj.ElfrDeregisterEventSource(LogHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(LogHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrNumberOfRecords(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
            RpcPointer<uint> NumberOfRecords = new RpcPointer<uint>();
            LogHandle = decoder.ReadContextHandle();
            var invokeTask = this._obj.ElfrNumberOfRecords(LogHandle, NumberOfRecords, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(NumberOfRecords.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrOldestRecord(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
            RpcPointer<uint> OldestRecordNumber = new RpcPointer<uint>();
            LogHandle = decoder.ReadContextHandle();
            var invokeTask = this._obj.ElfrOldestRecord(LogHandle, OldestRecordNumber, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(OldestRecordNumber.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrChangeNotify(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
            RPC_CLIENT_ID ClientId;
            uint Event;
            LogHandle = decoder.ReadContextHandle();
            ClientId = decoder.ReadFixedStruct<RPC_CLIENT_ID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<RPC_CLIENT_ID>(ref ClientId);
            Event = decoder.ReadUInt32();
            var invokeTask = this._obj.ElfrChangeNotify(LogHandle, ClientId, Event, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrOpenELW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<char> UNCServerName;
            ms_dtyp.RPC_UNICODE_STRING ModuleName;
            ms_dtyp.RPC_UNICODE_STRING RegModuleName;
            uint MajorVersion;
            uint MinorVersion;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            UNCServerName = decoder.ReadUniquePointer<char>();
            if ((null != UNCServerName)) {
                UNCServerName.value = decoder.ReadWideChar();
            }
            ModuleName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref ModuleName);
            RegModuleName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref RegModuleName);
            MajorVersion = decoder.ReadUInt32();
            MinorVersion = decoder.ReadUInt32();
            var invokeTask = this._obj.ElfrOpenELW(UNCServerName, ModuleName, RegModuleName, MajorVersion, MinorVersion, LogHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(LogHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrRegisterEventSourceW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<char> UNCServerName;
            ms_dtyp.RPC_UNICODE_STRING ModuleName;
            ms_dtyp.RPC_UNICODE_STRING RegModuleName;
            uint MajorVersion;
            uint MinorVersion;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            UNCServerName = decoder.ReadUniquePointer<char>();
            if ((null != UNCServerName)) {
                UNCServerName.value = decoder.ReadWideChar();
            }
            ModuleName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref ModuleName);
            RegModuleName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref RegModuleName);
            MajorVersion = decoder.ReadUInt32();
            MinorVersion = decoder.ReadUInt32();
            var invokeTask = this._obj.ElfrRegisterEventSourceW(UNCServerName, ModuleName, RegModuleName, MajorVersion, MinorVersion, LogHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(LogHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrOpenBELW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<char> UNCServerName;
            ms_dtyp.RPC_UNICODE_STRING BackupFileName;
            uint MajorVersion;
            uint MinorVersion;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            UNCServerName = decoder.ReadUniquePointer<char>();
            if ((null != UNCServerName)) {
                UNCServerName.value = decoder.ReadWideChar();
            }
            BackupFileName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref BackupFileName);
            MajorVersion = decoder.ReadUInt32();
            MinorVersion = decoder.ReadUInt32();
            var invokeTask = this._obj.ElfrOpenBELW(UNCServerName, BackupFileName, MajorVersion, MinorVersion, LogHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(LogHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrReadELW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
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
            for (int i = 0; (i < Buffer.value.Length); i++
            ) {
                byte elem_0 = Buffer.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(NumberOfBytesRead.value);
            encoder.WriteValue(MinNumberOfBytesNeeded.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrReportEventW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
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
            ComputerName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref ComputerName);
            UserSID = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            if ((null != UserSID)) {
                UserSID.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref UserSID.value);
            }
            Strings = decoder.ReadArrayHeader<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
                elem_0 = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
                Strings[i] = elem_0;
            }
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0.value);
                }
                Strings[i] = elem_0;
            }
            Data = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < Data.Length); i++
            ) {
                byte elem_0 = Data[i];
                elem_0 = decoder.ReadUnsignedChar();
                Data[i] = elem_0;
            }
            Flags = decoder.ReadUInt16();
            RecordNumber = decoder.ReadUniquePointer<uint>();
            if ((null != RecordNumber)) {
                RecordNumber.value = decoder.ReadUInt32();
            }
            TimeWritten = decoder.ReadUniquePointer<uint>();
            if ((null != TimeWritten)) {
                TimeWritten.value = decoder.ReadUInt32();
            }
            var invokeTask = this._obj.ElfrReportEventW(LogHandle, Time, EventType, EventCategory, EventID, NumStrings, DataSize, ComputerName, UserSID, Strings, Data, Flags, RecordNumber, TimeWritten, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(RecordNumber);
            if ((null != RecordNumber)) {
                encoder.WriteValue(RecordNumber.value);
            }
            encoder.WritePointer(TimeWritten);
            if ((null != TimeWritten)) {
                encoder.WriteValue(TimeWritten.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrClearELFA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
            RpcPointer<RPC_STRING> BackupFileName;
            LogHandle = decoder.ReadContextHandle();
            BackupFileName = decoder.ReadUniquePointer<RPC_STRING>();
            if ((null != BackupFileName)) {
                BackupFileName.value = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<RPC_STRING>(ref BackupFileName.value);
            }
            var invokeTask = this._obj.ElfrClearELFA(LogHandle, BackupFileName, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrBackupELFA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
            RPC_STRING BackupFileName;
            LogHandle = decoder.ReadContextHandle();
            BackupFileName = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<RPC_STRING>(ref BackupFileName);
            var invokeTask = this._obj.ElfrBackupELFA(LogHandle, BackupFileName, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrOpenELA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<byte> UNCServerName;
            RPC_STRING ModuleName;
            RPC_STRING RegModuleName;
            uint MajorVersion;
            uint MinorVersion;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            UNCServerName = decoder.ReadUniquePointer<byte>();
            if ((null != UNCServerName)) {
                UNCServerName.value = decoder.ReadUnsignedChar();
            }
            ModuleName = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<RPC_STRING>(ref ModuleName);
            RegModuleName = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<RPC_STRING>(ref RegModuleName);
            MajorVersion = decoder.ReadUInt32();
            MinorVersion = decoder.ReadUInt32();
            var invokeTask = this._obj.ElfrOpenELA(UNCServerName, ModuleName, RegModuleName, MajorVersion, MinorVersion, LogHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(LogHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrRegisterEventSourceA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<byte> UNCServerName;
            RPC_STRING ModuleName;
            RPC_STRING RegModuleName;
            uint MajorVersion;
            uint MinorVersion;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            UNCServerName = decoder.ReadUniquePointer<byte>();
            if ((null != UNCServerName)) {
                UNCServerName.value = decoder.ReadUnsignedChar();
            }
            ModuleName = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<RPC_STRING>(ref ModuleName);
            RegModuleName = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<RPC_STRING>(ref RegModuleName);
            MajorVersion = decoder.ReadUInt32();
            MinorVersion = decoder.ReadUInt32();
            var invokeTask = this._obj.ElfrRegisterEventSourceA(UNCServerName, ModuleName, RegModuleName, MajorVersion, MinorVersion, LogHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(LogHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrOpenBELA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<byte> UNCServerName;
            RPC_STRING BackupFileName;
            uint MajorVersion;
            uint MinorVersion;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> LogHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            UNCServerName = decoder.ReadUniquePointer<byte>();
            if ((null != UNCServerName)) {
                UNCServerName.value = decoder.ReadUnsignedChar();
            }
            BackupFileName = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<RPC_STRING>(ref BackupFileName);
            MajorVersion = decoder.ReadUInt32();
            MinorVersion = decoder.ReadUInt32();
            var invokeTask = this._obj.ElfrOpenBELA(UNCServerName, BackupFileName, MajorVersion, MinorVersion, LogHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(LogHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrReadELA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
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
            for (int i = 0; (i < Buffer.value.Length); i++
            ) {
                byte elem_0 = Buffer.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(NumberOfBytesRead.value);
            encoder.WriteValue(MinNumberOfBytesNeeded.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrReportEventA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
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
            ComputerName = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<RPC_STRING>(ref ComputerName);
            UserSID = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            if ((null != UserSID)) {
                UserSID.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref UserSID.value);
            }
            Strings = decoder.ReadArrayHeader<RpcPointer<RPC_STRING>>();
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<RPC_STRING> elem_0 = Strings[i];
                elem_0 = decoder.ReadUniquePointer<RPC_STRING>();
                Strings[i] = elem_0;
            }
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<RPC_STRING> elem_0 = Strings[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<RPC_STRING>(ref elem_0.value);
                }
                Strings[i] = elem_0;
            }
            Data = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < Data.Length); i++
            ) {
                byte elem_0 = Data[i];
                elem_0 = decoder.ReadUnsignedChar();
                Data[i] = elem_0;
            }
            Flags = decoder.ReadUInt16();
            RecordNumber = decoder.ReadUniquePointer<uint>();
            if ((null != RecordNumber)) {
                RecordNumber.value = decoder.ReadUInt32();
            }
            TimeWritten = decoder.ReadUniquePointer<uint>();
            if ((null != TimeWritten)) {
                TimeWritten.value = decoder.ReadUInt32();
            }
            var invokeTask = this._obj.ElfrReportEventA(LogHandle, Time, EventType, EventCategory, EventID, NumStrings, DataSize, ComputerName, UserSID, Strings, Data, Flags, RecordNumber, TimeWritten, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(RecordNumber);
            if ((null != RecordNumber)) {
                encoder.WriteValue(RecordNumber.value);
            }
            encoder.WritePointer(TimeWritten);
            if ((null != TimeWritten)) {
                encoder.WriteValue(TimeWritten.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum19NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum19NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum20NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum20NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum21NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum21NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_ElfrGetLogInformation(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
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
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum23NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum23NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_ElfrReportEventAndSourceW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
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
            SourceName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref SourceName);
            NumStrings = decoder.ReadUInt16();
            DataSize = decoder.ReadUInt32();
            ComputerName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref ComputerName);
            UserSID = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            if ((null != UserSID)) {
                UserSID.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref UserSID.value);
            }
            Strings = decoder.ReadArrayHeader<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
                elem_0 = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
                Strings[i] = elem_0;
            }
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0.value);
                }
                Strings[i] = elem_0;
            }
            Data = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < Data.Length); i++
            ) {
                byte elem_0 = Data[i];
                elem_0 = decoder.ReadUnsignedChar();
                Data[i] = elem_0;
            }
            Flags = decoder.ReadUInt16();
            RecordNumber = decoder.ReadUniquePointer<uint>();
            if ((null != RecordNumber)) {
                RecordNumber.value = decoder.ReadUInt32();
            }
            TimeWritten = decoder.ReadUniquePointer<uint>();
            if ((null != TimeWritten)) {
                TimeWritten.value = decoder.ReadUInt32();
            }
            var invokeTask = this._obj.ElfrReportEventAndSourceW(LogHandle, Time, EventType, EventCategory, EventID, SourceName, NumStrings, DataSize, ComputerName, UserSID, Strings, Data, Flags, RecordNumber, TimeWritten, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(RecordNumber);
            if ((null != RecordNumber)) {
                encoder.WriteValue(RecordNumber.value);
            }
            encoder.WritePointer(TimeWritten);
            if ((null != TimeWritten)) {
                encoder.WriteValue(TimeWritten.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrReportEventExW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
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
            TimeGenerated = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref TimeGenerated);
            EventType = decoder.ReadUInt16();
            EventCategory = decoder.ReadUInt16();
            EventID = decoder.ReadUInt32();
            NumStrings = decoder.ReadUInt16();
            DataSize = decoder.ReadUInt32();
            ComputerName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref ComputerName);
            UserSID = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            if ((null != UserSID)) {
                UserSID.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref UserSID.value);
            }
            Strings = decoder.ReadArrayHeader<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
                elem_0 = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
                Strings[i] = elem_0;
            }
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<ms_dtyp.RPC_UNICODE_STRING> elem_0 = Strings[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0.value);
                }
                Strings[i] = elem_0;
            }
            Data = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < Data.Length); i++
            ) {
                byte elem_0 = Data[i];
                elem_0 = decoder.ReadUnsignedChar();
                Data[i] = elem_0;
            }
            Flags = decoder.ReadUInt16();
            RecordNumber = decoder.ReadUniquePointer<uint>();
            if ((null != RecordNumber)) {
                RecordNumber.value = decoder.ReadUInt32();
            }
            var invokeTask = this._obj.ElfrReportEventExW(LogHandle, TimeGenerated, EventType, EventCategory, EventID, NumStrings, DataSize, ComputerName, UserSID, Strings, Data, Flags, RecordNumber, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(RecordNumber);
            if ((null != RecordNumber)) {
                encoder.WriteValue(RecordNumber.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ElfrReportEventExA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle LogHandle;
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
            TimeGenerated = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref TimeGenerated);
            EventType = decoder.ReadUInt16();
            EventCategory = decoder.ReadUInt16();
            EventID = decoder.ReadUInt32();
            NumStrings = decoder.ReadUInt16();
            DataSize = decoder.ReadUInt32();
            ComputerName = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<RPC_STRING>(ref ComputerName);
            UserSID = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            if ((null != UserSID)) {
                UserSID.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref UserSID.value);
            }
            Strings = decoder.ReadArrayHeader<RpcPointer<RPC_STRING>>();
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<RPC_STRING> elem_0 = Strings[i];
                elem_0 = decoder.ReadUniquePointer<RPC_STRING>();
                Strings[i] = elem_0;
            }
            for (int i = 0; (i < Strings.Length); i++
            ) {
                RpcPointer<RPC_STRING> elem_0 = Strings[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<RPC_STRING>(ref elem_0.value);
                }
                Strings[i] = elem_0;
            }
            Data = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < Data.Length); i++
            ) {
                byte elem_0 = Data[i];
                elem_0 = decoder.ReadUnsignedChar();
                Data[i] = elem_0;
            }
            Flags = decoder.ReadUInt16();
            RecordNumber = decoder.ReadUniquePointer<uint>();
            if ((null != RecordNumber)) {
                RecordNumber.value = decoder.ReadUInt32();
            }
            var invokeTask = this._obj.ElfrReportEventExA(LogHandle, TimeGenerated, EventType, EventCategory, EventID, NumStrings, DataSize, ComputerName, UserSID, Strings, Data, Flags, RecordNumber, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(RecordNumber);
            if ((null != RecordNumber)) {
                encoder.WriteValue(RecordNumber.value);
            }
            encoder.WriteValue(retval);
        }
    }
}
