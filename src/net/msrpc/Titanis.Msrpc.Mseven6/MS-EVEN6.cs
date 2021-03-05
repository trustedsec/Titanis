#pragma warning disable

namespace MS_EVEN6 {
    using System;
    using System.Threading.Tasks;
    using Titanis;
    using Titanis.DceRpc;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public struct RpcInfo : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.m_error);
            encoder.WriteValue(this.m_subErr);
            encoder.WriteValue(this.m_subErrParam);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.m_error = decoder.ReadUInt32();
            this.m_subErr = decoder.ReadUInt32();
            this.m_subErrParam = decoder.ReadUInt32();
        }
        public uint m_error;
        public uint m_subErr;
        public uint m_subErrParam;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public struct BooleanArray : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.count);
            encoder.WritePointer(this.ptr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.count = decoder.ReadUInt32();
            this.ptr = decoder.ReadUniquePointer<bool[]>();
        }
        public uint count;
        public RpcPointer<bool[]> ptr;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.ptr)) {
                encoder.WriteArrayHeader(this.ptr.value);
                for (int i = 0; (i < this.ptr.value.Length); i++
                ) {
                    bool elem_0 = this.ptr.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.ptr)) {
                this.ptr.value = decoder.ReadArrayHeader<bool>();
                for (int i = 0; (i < this.ptr.value.Length); i++
                ) {
                    bool elem_0 = this.ptr.value[i];
                    elem_0 = decoder.ReadBoolean();
                    this.ptr.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public struct UInt32Array : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.count);
            encoder.WritePointer(this.ptr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.count = decoder.ReadUInt32();
            this.ptr = decoder.ReadUniquePointer<uint[]>();
        }
        public uint count;
        public RpcPointer<uint[]> ptr;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.ptr)) {
                encoder.WriteArrayHeader(this.ptr.value);
                for (int i = 0; (i < this.ptr.value.Length); i++
                ) {
                    uint elem_0 = this.ptr.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.ptr)) {
                this.ptr.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < this.ptr.value.Length); i++
                ) {
                    uint elem_0 = this.ptr.value[i];
                    elem_0 = decoder.ReadUInt32();
                    this.ptr.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public struct UInt64Array : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.count);
            encoder.WritePointer(this.ptr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.count = decoder.ReadUInt32();
            this.ptr = decoder.ReadUniquePointer<ulong[]>();
        }
        public uint count;
        public RpcPointer<ulong[]> ptr;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.ptr)) {
                encoder.WriteArrayHeader(this.ptr.value);
                for (int i = 0; (i < this.ptr.value.Length); i++
                ) {
                    ulong elem_0 = this.ptr.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.ptr)) {
                this.ptr.value = decoder.ReadArrayHeader<ulong>();
                for (int i = 0; (i < this.ptr.value.Length); i++
                ) {
                    ulong elem_0 = this.ptr.value[i];
                    elem_0 = decoder.ReadUInt64();
                    this.ptr.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public struct StringArray : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.count);
            encoder.WritePointer(this.ptr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.count = decoder.ReadUInt32();
            this.ptr = decoder.ReadUniquePointer<RpcPointer<string>[]>();
        }
        public uint count;
        public RpcPointer<RpcPointer<string>[]> ptr;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.ptr)) {
                encoder.WriteArrayHeader(this.ptr.value);
                for (int i = 0; (i < this.ptr.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = this.ptr.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < this.ptr.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = this.ptr.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteWideCharString(elem_0.value);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.ptr)) {
                this.ptr.value = decoder.ReadArrayHeader<RpcPointer<string>>();
                for (int i = 0; (i < this.ptr.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = this.ptr.value[i];
                    elem_0 = decoder.ReadUniquePointer<string>();
                    this.ptr.value[i] = elem_0;
                }
                for (int i = 0; (i < this.ptr.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = this.ptr.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadWideCharString();
                    }
                    this.ptr.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public struct GuidArray : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.count);
            encoder.WritePointer(this.ptr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.count = decoder.ReadUInt32();
            this.ptr = decoder.ReadUniquePointer<System.Guid[]>();
        }
        public uint count;
        public RpcPointer<System.Guid[]> ptr;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.ptr)) {
                encoder.WriteArrayHeader(this.ptr.value);
                for (int i = 0; (i < this.ptr.value.Length); i++
                ) {
                    System.Guid elem_0 = this.ptr.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.ptr)) {
                this.ptr.value = decoder.ReadArrayHeader<System.Guid>();
                for (int i = 0; (i < this.ptr.value.Length); i++
                ) {
                    System.Guid elem_0 = this.ptr.value[i];
                    elem_0 = decoder.ReadUuid();
                    this.ptr.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public enum EvtRpcVariantType : int {
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
        EvtRpcVarTypeGuidArray = 10,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public enum EvtRpcAssertConfigFlags : int {
        EvtRpcChannelPath = 0,
        EvtRpcPublisherName = 1,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public struct Unnamed_1 : Titanis.DceRpc.IRpcFixedStruct {
        public EvtRpcVariantType type;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((int)(this.type)));
            encoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.type)) == 0)) {
                encoder.WriteValue(this.nullVal);
            }
            else {
                if ((((int)(this.type)) == 1)) {
                    encoder.WriteValue(this.booleanVal);
                }
                else {
                    if ((((int)(this.type)) == 2)) {
                        encoder.WriteValue(this.uint32Val);
                    }
                    else {
                        if ((((int)(this.type)) == 3)) {
                            encoder.WriteValue(this.uint64Val);
                        }
                        else {
                            if ((((int)(this.type)) == 4)) {
                                encoder.WritePointer(this.stringVal);
                            }
                            else {
                                if ((((int)(this.type)) == 5)) {
                                    encoder.WritePointer(this.guidVal);
                                }
                                else {
                                    if ((((int)(this.type)) == 6)) {
                                        encoder.WriteFixedStruct(this.booleanArray, Titanis.DceRpc.NdrAlignment.NativePtr);
                                    }
                                    else {
                                        if ((((int)(this.type)) == 7)) {
                                            encoder.WriteFixedStruct(this.uint32Array, Titanis.DceRpc.NdrAlignment.NativePtr);
                                        }
                                        else {
                                            if ((((int)(this.type)) == 8)) {
                                                encoder.WriteFixedStruct(this.uint64Array, Titanis.DceRpc.NdrAlignment.NativePtr);
                                            }
                                            else {
                                                if ((((int)(this.type)) == 9)) {
                                                    encoder.WriteFixedStruct(this.stringArray, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                }
                                                else {
                                                    if ((((int)(this.type)) == 10)) {
                                                        encoder.WriteFixedStruct(this.guidArray, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.type = ((EvtRpcVariantType)(decoder.ReadInt32()));
            decoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.type)) == 0)) {
                this.nullVal = decoder.ReadInt32();
            }
            else {
                if ((((int)(this.type)) == 1)) {
                    this.booleanVal = decoder.ReadBoolean();
                }
                else {
                    if ((((int)(this.type)) == 2)) {
                        this.uint32Val = decoder.ReadUInt32();
                    }
                    else {
                        if ((((int)(this.type)) == 3)) {
                            this.uint64Val = decoder.ReadUInt64();
                        }
                        else {
                            if ((((int)(this.type)) == 4)) {
                                this.stringVal = decoder.ReadUniquePointer<string>();
                            }
                            else {
                                if ((((int)(this.type)) == 5)) {
                                    this.guidVal = decoder.ReadUniquePointer<System.Guid>();
                                }
                                else {
                                    if ((((int)(this.type)) == 6)) {
                                        this.booleanArray = decoder.ReadFixedStruct<BooleanArray>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                    }
                                    else {
                                        if ((((int)(this.type)) == 7)) {
                                            this.uint32Array = decoder.ReadFixedStruct<UInt32Array>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                        }
                                        else {
                                            if ((((int)(this.type)) == 8)) {
                                                this.uint64Array = decoder.ReadFixedStruct<UInt64Array>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                            }
                                            else {
                                                if ((((int)(this.type)) == 9)) {
                                                    this.stringArray = decoder.ReadFixedStruct<StringArray>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                }
                                                else {
                                                    if ((((int)(this.type)) == 10)) {
                                                        this.guidArray = decoder.ReadFixedStruct<GuidArray>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.type)) == 0)) {
            }
            else {
                if ((((int)(this.type)) == 1)) {
                }
                else {
                    if ((((int)(this.type)) == 2)) {
                    }
                    else {
                        if ((((int)(this.type)) == 3)) {
                        }
                        else {
                            if ((((int)(this.type)) == 4)) {
                                if ((null != this.stringVal)) {
                                    encoder.WriteWideCharString(this.stringVal.value);
                                }
                            }
                            else {
                                if ((((int)(this.type)) == 5)) {
                                    if ((null != this.guidVal)) {
                                        encoder.WriteValue(this.guidVal.value);
                                    }
                                }
                                else {
                                    if ((((int)(this.type)) == 6)) {
                                        encoder.WriteStructDeferral(this.booleanArray);
                                    }
                                    else {
                                        if ((((int)(this.type)) == 7)) {
                                            encoder.WriteStructDeferral(this.uint32Array);
                                        }
                                        else {
                                            if ((((int)(this.type)) == 8)) {
                                                encoder.WriteStructDeferral(this.uint64Array);
                                            }
                                            else {
                                                if ((((int)(this.type)) == 9)) {
                                                    encoder.WriteStructDeferral(this.stringArray);
                                                }
                                                else {
                                                    if ((((int)(this.type)) == 10)) {
                                                        encoder.WriteStructDeferral(this.guidArray);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.type)) == 0)) {
            }
            else {
                if ((((int)(this.type)) == 1)) {
                }
                else {
                    if ((((int)(this.type)) == 2)) {
                    }
                    else {
                        if ((((int)(this.type)) == 3)) {
                        }
                        else {
                            if ((((int)(this.type)) == 4)) {
                                if ((null != this.stringVal)) {
                                    this.stringVal.value = decoder.ReadWideCharString();
                                }
                            }
                            else {
                                if ((((int)(this.type)) == 5)) {
                                    if ((null != this.guidVal)) {
                                        this.guidVal.value = decoder.ReadUuid();
                                    }
                                }
                                else {
                                    if ((((int)(this.type)) == 6)) {
                                        decoder.ReadStructDeferral<BooleanArray>(ref this.booleanArray);
                                    }
                                    else {
                                        if ((((int)(this.type)) == 7)) {
                                            decoder.ReadStructDeferral<UInt32Array>(ref this.uint32Array);
                                        }
                                        else {
                                            if ((((int)(this.type)) == 8)) {
                                                decoder.ReadStructDeferral<UInt64Array>(ref this.uint64Array);
                                            }
                                            else {
                                                if ((((int)(this.type)) == 9)) {
                                                    decoder.ReadStructDeferral<StringArray>(ref this.stringArray);
                                                }
                                                else {
                                                    if ((((int)(this.type)) == 10)) {
                                                        decoder.ReadStructDeferral<GuidArray>(ref this.guidArray);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public int nullVal;
        public bool booleanVal;
        public uint uint32Val;
        public ulong uint64Val;
        public RpcPointer<string> stringVal;
        public RpcPointer<System.Guid> guidVal;
        public BooleanArray booleanArray;
        public UInt32Array uint32Array;
        public UInt64Array uint64Array;
        public StringArray stringArray;
        public GuidArray guidArray;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public struct EvtRpcVariant : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((int)(this.type)));
            encoder.WriteValue(this.flags);
            encoder.WriteUnion(this.unnamed_1);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.type = ((EvtRpcVariantType)(decoder.ReadInt32()));
            this.flags = decoder.ReadUInt32();
            this.unnamed_1 = decoder.ReadUnion<Unnamed_1>();
        }
        public EvtRpcVariantType type;
        public uint flags;
        public Unnamed_1 unnamed_1;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.unnamed_1);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<Unnamed_1>(ref this.unnamed_1);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public struct EvtRpcVariantList : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.count);
            encoder.WritePointer(this.props);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.count = decoder.ReadUInt32();
            this.props = decoder.ReadUniquePointer<EvtRpcVariant[]>();
        }
        public uint count;
        public RpcPointer<EvtRpcVariant[]> props;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.props)) {
                encoder.WriteArrayHeader(this.props.value);
                for (int i = 0; (i < this.props.value.Length); i++
                ) {
                    EvtRpcVariant elem_0 = this.props.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._8Byte);
                }
                for (int i = 0; (i < this.props.value.Length); i++
                ) {
                    EvtRpcVariant elem_0 = this.props.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.props)) {
                this.props.value = decoder.ReadArrayHeader<EvtRpcVariant>();
                for (int i = 0; (i < this.props.value.Length); i++
                ) {
                    EvtRpcVariant elem_0 = this.props.value[i];
                    elem_0 = decoder.ReadFixedStruct<EvtRpcVariant>(Titanis.DceRpc.NdrAlignment._8Byte);
                    this.props.value[i] = elem_0;
                }
                for (int i = 0; (i < this.props.value.Length); i++
                ) {
                    EvtRpcVariant elem_0 = this.props.value[i];
                    decoder.ReadStructDeferral<EvtRpcVariant>(ref elem_0);
                    this.props.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public struct EvtRpcQueryChannelInfo : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.name);
            encoder.WriteValue(this.status);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.name = decoder.ReadUniquePointer<char>();
            this.status = decoder.ReadUInt32();
        }
        public RpcPointer<char> name;
        public uint status;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.name)) {
                encoder.WriteValue(this.name.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.name)) {
                this.name.value = decoder.ReadWideChar();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    [System.Runtime.InteropServices.GuidAttribute("f6beaff7-1e19-4fbb-9f8f-b89e2018337c")]
    [Titanis.DceRpc.RpcVersionAttribute(1, 0)]
    public interface IEventService {
        Task<int> EvtRpcRegisterRemoteSubscription(string channelPath, string query, string bookmarkXml, uint flags, Titanis.DceRpc.RpcContextHandle handle, Titanis.DceRpc.RpcContextHandle control, RpcPointer<uint> queryChannelInfoSize, RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>> queryChannelInfo, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcRemoteSubscriptionNextAsync(Titanis.DceRpc.RpcContextHandle handle, uint numRequestedRecords, uint flags, RpcPointer<uint> numActualRecords, RpcPointer<RpcPointer<uint[]>> eventDataIndices, RpcPointer<RpcPointer<uint[]>> eventDataSizes, RpcPointer<uint> resultBufferSize, RpcPointer<RpcPointer<byte[]>> resultBuffer, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcRemoteSubscriptionNext(Titanis.DceRpc.RpcContextHandle handle, uint numRequestedRecords, uint timeOut, uint flags, RpcPointer<uint> numActualRecords, RpcPointer<RpcPointer<uint[]>> eventDataIndices, RpcPointer<RpcPointer<uint[]>> eventDataSizes, RpcPointer<uint> resultBufferSize, RpcPointer<RpcPointer<byte[]>> resultBuffer, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcRemoteSubscriptionWaitAsync(Titanis.DceRpc.RpcContextHandle handle, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcRegisterControllableOperation(Titanis.DceRpc.RpcContextHandle handle, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcRegisterLogQuery(string path, string query, uint flags, Titanis.DceRpc.RpcContextHandle handle, Titanis.DceRpc.RpcContextHandle opControl, RpcPointer<uint> queryChannelInfoSize, RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>> queryChannelInfo, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcClearLog(Titanis.DceRpc.RpcContextHandle control, string channelPath, string backupPath, uint flags, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcExportLog(Titanis.DceRpc.RpcContextHandle control, string channelPath, string query, string backupPath, uint flags, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcLocalizeExportLog(Titanis.DceRpc.RpcContextHandle control, string logFilePath, uint locale, uint flags, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcMessageRender(Titanis.DceRpc.RpcContextHandle pubCfgObj, uint sizeEventId, byte[] eventId, uint messageId, EvtRpcVariantList values, uint flags, uint maxSizeString, RpcPointer<uint> actualSizeString, RpcPointer<uint> neededSizeString, RpcPointer<RpcPointer<byte[]>> @string, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcMessageRenderDefault(uint sizeEventId, byte[] eventId, uint messageId, EvtRpcVariantList values, uint flags, uint maxSizeString, RpcPointer<uint> actualSizeString, RpcPointer<uint> neededSizeString, RpcPointer<RpcPointer<byte[]>> @string, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcQueryNext(Titanis.DceRpc.RpcContextHandle logQuery, uint numRequestedRecords, uint timeOutEnd, uint flags, RpcPointer<uint> numActualRecords, RpcPointer<RpcPointer<uint[]>> eventDataIndices, RpcPointer<RpcPointer<uint[]>> eventDataSizes, RpcPointer<uint> resultBufferSize, RpcPointer<RpcPointer<byte[]>> resultBuffer, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcQuerySeek(Titanis.DceRpc.RpcContextHandle logQuery, long pos, string bookmarkXml, uint timeOut, uint flags, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcClose(Titanis.DceRpc.RpcContextHandle handle, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcCancel(Titanis.DceRpc.RpcContextHandle handle, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcAssertConfig(string path, uint flags, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcRetractConfig(string path, uint flags, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcOpenLogHandle(string channel, uint flags, Titanis.DceRpc.RpcContextHandle handle, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcGetLogFileInfo(Titanis.DceRpc.RpcContextHandle logHandle, uint propertyId, uint propertyValueBufferSize, RpcPointer<byte[]> propertyValueBuffer, RpcPointer<uint> propertyValueBufferLength, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcGetChannelList(uint flags, RpcPointer<uint> numChannelPaths, RpcPointer<RpcPointer<RpcPointer<string>[]>> channelPaths, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcGetChannelConfig(string channelPath, uint flags, RpcPointer<EvtRpcVariantList> props, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcPutChannelConfig(string channelPath, uint flags, EvtRpcVariantList props, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcGetPublisherList(uint flags, RpcPointer<uint> numPublisherIds, RpcPointer<RpcPointer<RpcPointer<string>[]>> publisherIds, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcGetPublisherListForChannel(char channelName, uint flags, RpcPointer<uint> numPublisherIds, RpcPointer<RpcPointer<RpcPointer<string>[]>> publisherIds, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcGetPublisherMetadata(string publisherId, string logFilePath, uint locale, uint flags, RpcPointer<EvtRpcVariantList> pubMetadataProps, Titanis.DceRpc.RpcContextHandle pubMetadata, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcGetPublisherResourceMetadata(Titanis.DceRpc.RpcContextHandle handle, uint propertyId, uint flags, RpcPointer<EvtRpcVariantList> pubMetadataProps, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcGetEventMetadataEnum(Titanis.DceRpc.RpcContextHandle pubMetadata, uint flags, string reservedForFilter, Titanis.DceRpc.RpcContextHandle eventMetaDataEnum, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcGetNextEventMetadata(Titanis.DceRpc.RpcContextHandle eventMetaDataEnum, uint flags, uint numRequested, RpcPointer<uint> numReturned, RpcPointer<RpcPointer<EvtRpcVariantList[]>> eventMetadataInstances, System.Threading.CancellationToken cancellationToken);
        Task<int> EvtRpcGetClassicLogDisplayName(string logName, uint locale, uint flags, RpcPointer<RpcPointer<char>> displayName, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    [Titanis.DceRpc.IidAttribute("f6beaff7-1e19-4fbb-9f8f-b89e2018337c")]
    public class IEventServiceClientProxy : Titanis.DceRpc.Client.RpcClientProxy, IEventService, Titanis.DceRpc.IRpcClientProxy {
        private static System.Guid _interfaceUuid = new System.Guid("f6beaff7-1e19-4fbb-9f8f-b89e2018337c");
        public override System.Guid InterfaceUuid {
            get {
                return _interfaceUuid;
            }
        }
        public override Titanis.DceRpc.RpcVersion InterfaceVersion {
            get {
                return new Titanis.DceRpc.RpcVersion(1, 0);
            }
        }
        public virtual async Task<int> EvtRpcRegisterRemoteSubscription(string channelPath, string query, string bookmarkXml, uint flags, Titanis.DceRpc.RpcContextHandle handle, Titanis.DceRpc.RpcContextHandle control, RpcPointer<uint> queryChannelInfoSize, RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>> queryChannelInfo, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteUniqueReferentId((channelPath == null));
            if ((channelPath != null)) {
                encoder.WriteWideCharString(channelPath);
            }
            encoder.WriteWideCharString(query);
            encoder.WriteUniqueReferentId((bookmarkXml == null));
            if ((bookmarkXml != null)) {
                encoder.WriteWideCharString(bookmarkXml);
            }
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            handle = decoder.ReadContextHandle();
            control = decoder.ReadContextHandle();
            queryChannelInfoSize.value = decoder.ReadUInt32();
            queryChannelInfo.value = decoder.ReadOutUniquePointer<EvtRpcQueryChannelInfo[]>(queryChannelInfo.value);
            if ((null != queryChannelInfo.value)) {
                queryChannelInfo.value.value = decoder.ReadArrayHeader<EvtRpcQueryChannelInfo>();
                for (int i = 0; (i < queryChannelInfo.value.value.Length); i++
                ) {
                    EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
                    elem_0 = decoder.ReadFixedStruct<EvtRpcQueryChannelInfo>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    queryChannelInfo.value.value[i] = elem_0;
                }
                for (int i = 0; (i < queryChannelInfo.value.value.Length); i++
                ) {
                    EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
                    decoder.ReadStructDeferral<EvtRpcQueryChannelInfo>(ref elem_0);
                    queryChannelInfo.value.value[i] = elem_0;
                }
            }
            error.value = decoder.ReadFixedStruct<RpcInfo>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<RpcInfo>(ref error.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcRemoteSubscriptionNextAsync(Titanis.DceRpc.RpcContextHandle handle, uint numRequestedRecords, uint flags, RpcPointer<uint> numActualRecords, RpcPointer<RpcPointer<uint[]>> eventDataIndices, RpcPointer<RpcPointer<uint[]>> eventDataSizes, RpcPointer<uint> resultBufferSize, RpcPointer<RpcPointer<byte[]>> resultBuffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(handle);
            encoder.WriteValue(numRequestedRecords);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            numActualRecords.value = decoder.ReadUInt32();
            eventDataIndices.value = decoder.ReadOutUniquePointer<uint[]>(eventDataIndices.value);
            if ((null != eventDataIndices.value)) {
                eventDataIndices.value.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < eventDataIndices.value.value.Length); i++
                ) {
                    uint elem_0 = eventDataIndices.value.value[i];
                    elem_0 = decoder.ReadUInt32();
                    eventDataIndices.value.value[i] = elem_0;
                }
            }
            eventDataSizes.value = decoder.ReadOutUniquePointer<uint[]>(eventDataSizes.value);
            if ((null != eventDataSizes.value)) {
                eventDataSizes.value.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < eventDataSizes.value.value.Length); i++
                ) {
                    uint elem_0 = eventDataSizes.value.value[i];
                    elem_0 = decoder.ReadUInt32();
                    eventDataSizes.value.value[i] = elem_0;
                }
            }
            resultBufferSize.value = decoder.ReadUInt32();
            resultBuffer.value = decoder.ReadOutUniquePointer<byte[]>(resultBuffer.value);
            if ((null != resultBuffer.value)) {
                resultBuffer.value.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < resultBuffer.value.value.Length); i++
                ) {
                    byte elem_0 = resultBuffer.value.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    resultBuffer.value.value[i] = elem_0;
                }
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcRemoteSubscriptionNext(Titanis.DceRpc.RpcContextHandle handle, uint numRequestedRecords, uint timeOut, uint flags, RpcPointer<uint> numActualRecords, RpcPointer<RpcPointer<uint[]>> eventDataIndices, RpcPointer<RpcPointer<uint[]>> eventDataSizes, RpcPointer<uint> resultBufferSize, RpcPointer<RpcPointer<byte[]>> resultBuffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(handle);
            encoder.WriteValue(numRequestedRecords);
            encoder.WriteValue(timeOut);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            numActualRecords.value = decoder.ReadUInt32();
            eventDataIndices.value = decoder.ReadOutUniquePointer<uint[]>(eventDataIndices.value);
            if ((null != eventDataIndices.value)) {
                eventDataIndices.value.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < eventDataIndices.value.value.Length); i++
                ) {
                    uint elem_0 = eventDataIndices.value.value[i];
                    elem_0 = decoder.ReadUInt32();
                    eventDataIndices.value.value[i] = elem_0;
                }
            }
            eventDataSizes.value = decoder.ReadOutUniquePointer<uint[]>(eventDataSizes.value);
            if ((null != eventDataSizes.value)) {
                eventDataSizes.value.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < eventDataSizes.value.value.Length); i++
                ) {
                    uint elem_0 = eventDataSizes.value.value[i];
                    elem_0 = decoder.ReadUInt32();
                    eventDataSizes.value.value[i] = elem_0;
                }
            }
            resultBufferSize.value = decoder.ReadUInt32();
            resultBuffer.value = decoder.ReadOutUniquePointer<byte[]>(resultBuffer.value);
            if ((null != resultBuffer.value)) {
                resultBuffer.value.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < resultBuffer.value.value.Length); i++
                ) {
                    byte elem_0 = resultBuffer.value.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    resultBuffer.value.value[i] = elem_0;
                }
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcRemoteSubscriptionWaitAsync(Titanis.DceRpc.RpcContextHandle handle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(handle);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcRegisterControllableOperation(Titanis.DceRpc.RpcContextHandle handle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            handle = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcRegisterLogQuery(string path, string query, uint flags, Titanis.DceRpc.RpcContextHandle handle, Titanis.DceRpc.RpcContextHandle opControl, RpcPointer<uint> queryChannelInfoSize, RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>> queryChannelInfo, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteUniqueReferentId((path == null));
            if ((path != null)) {
                encoder.WriteWideCharString(path);
            }
            encoder.WriteWideCharString(query);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            handle = decoder.ReadContextHandle();
            opControl = decoder.ReadContextHandle();
            queryChannelInfoSize.value = decoder.ReadUInt32();
            queryChannelInfo.value = decoder.ReadOutUniquePointer<EvtRpcQueryChannelInfo[]>(queryChannelInfo.value);
            if ((null != queryChannelInfo.value)) {
                queryChannelInfo.value.value = decoder.ReadArrayHeader<EvtRpcQueryChannelInfo>();
                for (int i = 0; (i < queryChannelInfo.value.value.Length); i++
                ) {
                    EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
                    elem_0 = decoder.ReadFixedStruct<EvtRpcQueryChannelInfo>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    queryChannelInfo.value.value[i] = elem_0;
                }
                for (int i = 0; (i < queryChannelInfo.value.value.Length); i++
                ) {
                    EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
                    decoder.ReadStructDeferral<EvtRpcQueryChannelInfo>(ref elem_0);
                    queryChannelInfo.value.value[i] = elem_0;
                }
            }
            error.value = decoder.ReadFixedStruct<RpcInfo>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<RpcInfo>(ref error.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcClearLog(Titanis.DceRpc.RpcContextHandle control, string channelPath, string backupPath, uint flags, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(control);
            encoder.WriteWideCharString(channelPath);
            encoder.WriteUniqueReferentId((backupPath == null));
            if ((backupPath != null)) {
                encoder.WriteWideCharString(backupPath);
            }
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            error.value = decoder.ReadFixedStruct<RpcInfo>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<RpcInfo>(ref error.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcExportLog(Titanis.DceRpc.RpcContextHandle control, string channelPath, string query, string backupPath, uint flags, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(control);
            encoder.WriteUniqueReferentId((channelPath == null));
            if ((channelPath != null)) {
                encoder.WriteWideCharString(channelPath);
            }
            encoder.WriteWideCharString(query);
            encoder.WriteWideCharString(backupPath);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            error.value = decoder.ReadFixedStruct<RpcInfo>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<RpcInfo>(ref error.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcLocalizeExportLog(Titanis.DceRpc.RpcContextHandle control, string logFilePath, uint locale, uint flags, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(control);
            encoder.WriteWideCharString(logFilePath);
            encoder.WriteValue(locale);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            error.value = decoder.ReadFixedStruct<RpcInfo>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<RpcInfo>(ref error.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcMessageRender(Titanis.DceRpc.RpcContextHandle pubCfgObj, uint sizeEventId, byte[] eventId, uint messageId, EvtRpcVariantList values, uint flags, uint maxSizeString, RpcPointer<uint> actualSizeString, RpcPointer<uint> neededSizeString, RpcPointer<RpcPointer<byte[]>> @string, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(pubCfgObj);
            encoder.WriteValue(sizeEventId);
            if ((eventId != null)) {
                encoder.WriteArrayHeader(eventId);
                for (int i = 0; (i < eventId.Length); i++
                ) {
                    byte elem_0 = eventId[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(messageId);
            encoder.WriteFixedStruct(values, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(values);
            encoder.WriteValue(flags);
            encoder.WriteValue(maxSizeString);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            actualSizeString.value = decoder.ReadUInt32();
            neededSizeString.value = decoder.ReadUInt32();
            @string.value = decoder.ReadOutUniquePointer<byte[]>(@string.value);
            if ((null != @string.value)) {
                @string.value.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < @string.value.value.Length); i++
                ) {
                    byte elem_0 = @string.value.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    @string.value.value[i] = elem_0;
                }
            }
            error.value = decoder.ReadFixedStruct<RpcInfo>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<RpcInfo>(ref error.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcMessageRenderDefault(uint sizeEventId, byte[] eventId, uint messageId, EvtRpcVariantList values, uint flags, uint maxSizeString, RpcPointer<uint> actualSizeString, RpcPointer<uint> neededSizeString, RpcPointer<RpcPointer<byte[]>> @string, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(sizeEventId);
            if ((eventId != null)) {
                encoder.WriteArrayHeader(eventId);
                for (int i = 0; (i < eventId.Length); i++
                ) {
                    byte elem_0 = eventId[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(messageId);
            encoder.WriteFixedStruct(values, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(values);
            encoder.WriteValue(flags);
            encoder.WriteValue(maxSizeString);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            actualSizeString.value = decoder.ReadUInt32();
            neededSizeString.value = decoder.ReadUInt32();
            @string.value = decoder.ReadOutUniquePointer<byte[]>(@string.value);
            if ((null != @string.value)) {
                @string.value.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < @string.value.value.Length); i++
                ) {
                    byte elem_0 = @string.value.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    @string.value.value[i] = elem_0;
                }
            }
            error.value = decoder.ReadFixedStruct<RpcInfo>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<RpcInfo>(ref error.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcQueryNext(Titanis.DceRpc.RpcContextHandle logQuery, uint numRequestedRecords, uint timeOutEnd, uint flags, RpcPointer<uint> numActualRecords, RpcPointer<RpcPointer<uint[]>> eventDataIndices, RpcPointer<RpcPointer<uint[]>> eventDataSizes, RpcPointer<uint> resultBufferSize, RpcPointer<RpcPointer<byte[]>> resultBuffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(logQuery);
            encoder.WriteValue(numRequestedRecords);
            encoder.WriteValue(timeOutEnd);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            numActualRecords.value = decoder.ReadUInt32();
            eventDataIndices.value = decoder.ReadOutUniquePointer<uint[]>(eventDataIndices.value);
            if ((null != eventDataIndices.value)) {
                eventDataIndices.value.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < eventDataIndices.value.value.Length); i++
                ) {
                    uint elem_0 = eventDataIndices.value.value[i];
                    elem_0 = decoder.ReadUInt32();
                    eventDataIndices.value.value[i] = elem_0;
                }
            }
            eventDataSizes.value = decoder.ReadOutUniquePointer<uint[]>(eventDataSizes.value);
            if ((null != eventDataSizes.value)) {
                eventDataSizes.value.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < eventDataSizes.value.value.Length); i++
                ) {
                    uint elem_0 = eventDataSizes.value.value[i];
                    elem_0 = decoder.ReadUInt32();
                    eventDataSizes.value.value[i] = elem_0;
                }
            }
            resultBufferSize.value = decoder.ReadUInt32();
            resultBuffer.value = decoder.ReadOutUniquePointer<byte[]>(resultBuffer.value);
            if ((null != resultBuffer.value)) {
                resultBuffer.value.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < resultBuffer.value.value.Length); i++
                ) {
                    byte elem_0 = resultBuffer.value.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    resultBuffer.value.value[i] = elem_0;
                }
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcQuerySeek(Titanis.DceRpc.RpcContextHandle logQuery, long pos, string bookmarkXml, uint timeOut, uint flags, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(logQuery);
            encoder.WriteValue(pos);
            encoder.WriteUniqueReferentId((bookmarkXml == null));
            if ((bookmarkXml != null)) {
                encoder.WriteWideCharString(bookmarkXml);
            }
            encoder.WriteValue(timeOut);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            error.value = decoder.ReadFixedStruct<RpcInfo>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<RpcInfo>(ref error.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcClose(Titanis.DceRpc.RpcContextHandle handle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(handle);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            handle = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcCancel(Titanis.DceRpc.RpcContextHandle handle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(handle);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcAssertConfig(string path, uint flags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(path);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcRetractConfig(string path, uint flags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(path);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcOpenLogHandle(string channel, uint flags, Titanis.DceRpc.RpcContextHandle handle, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(channel);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            handle = decoder.ReadContextHandle();
            error.value = decoder.ReadFixedStruct<RpcInfo>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<RpcInfo>(ref error.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcGetLogFileInfo(Titanis.DceRpc.RpcContextHandle logHandle, uint propertyId, uint propertyValueBufferSize, RpcPointer<byte[]> propertyValueBuffer, RpcPointer<uint> propertyValueBufferLength, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(logHandle);
            encoder.WriteValue(propertyId);
            encoder.WriteValue(propertyValueBufferSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            propertyValueBuffer.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < propertyValueBuffer.value.Length); i++
            ) {
                byte elem_0 = propertyValueBuffer.value[i];
                elem_0 = decoder.ReadUnsignedChar();
                propertyValueBuffer.value[i] = elem_0;
            }
            propertyValueBufferLength.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcGetChannelList(uint flags, RpcPointer<uint> numChannelPaths, RpcPointer<RpcPointer<RpcPointer<string>[]>> channelPaths, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            numChannelPaths.value = decoder.ReadUInt32();
            channelPaths.value = decoder.ReadOutUniquePointer<RpcPointer<string>[]>(channelPaths.value);
            if ((null != channelPaths.value)) {
                channelPaths.value.value = decoder.ReadArrayHeader<RpcPointer<string>>();
                for (int i = 0; (i < channelPaths.value.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = channelPaths.value.value[i];
                    elem_0 = decoder.ReadUniquePointer<string>();
                    channelPaths.value.value[i] = elem_0;
                }
                for (int i = 0; (i < channelPaths.value.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = channelPaths.value.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadWideCharString();
                    }
                    channelPaths.value.value[i] = elem_0;
                }
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcGetChannelConfig(string channelPath, uint flags, RpcPointer<EvtRpcVariantList> props, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(channelPath);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            props.value = decoder.ReadFixedStruct<EvtRpcVariantList>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<EvtRpcVariantList>(ref props.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcPutChannelConfig(string channelPath, uint flags, EvtRpcVariantList props, RpcPointer<RpcInfo> error, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(channelPath);
            encoder.WriteValue(flags);
            encoder.WriteFixedStruct(props, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(props);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            error.value = decoder.ReadFixedStruct<RpcInfo>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<RpcInfo>(ref error.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcGetPublisherList(uint flags, RpcPointer<uint> numPublisherIds, RpcPointer<RpcPointer<RpcPointer<string>[]>> publisherIds, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            numPublisherIds.value = decoder.ReadUInt32();
            publisherIds.value = decoder.ReadOutUniquePointer<RpcPointer<string>[]>(publisherIds.value);
            if ((null != publisherIds.value)) {
                publisherIds.value.value = decoder.ReadArrayHeader<RpcPointer<string>>();
                for (int i = 0; (i < publisherIds.value.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = publisherIds.value.value[i];
                    elem_0 = decoder.ReadUniquePointer<string>();
                    publisherIds.value.value[i] = elem_0;
                }
                for (int i = 0; (i < publisherIds.value.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = publisherIds.value.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadWideCharString();
                    }
                    publisherIds.value.value[i] = elem_0;
                }
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcGetPublisherListForChannel(char channelName, uint flags, RpcPointer<uint> numPublisherIds, RpcPointer<RpcPointer<RpcPointer<string>[]>> publisherIds, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(channelName);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            numPublisherIds.value = decoder.ReadUInt32();
            publisherIds.value = decoder.ReadOutUniquePointer<RpcPointer<string>[]>(publisherIds.value);
            if ((null != publisherIds.value)) {
                publisherIds.value.value = decoder.ReadArrayHeader<RpcPointer<string>>();
                for (int i = 0; (i < publisherIds.value.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = publisherIds.value.value[i];
                    elem_0 = decoder.ReadUniquePointer<string>();
                    publisherIds.value.value[i] = elem_0;
                }
                for (int i = 0; (i < publisherIds.value.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = publisherIds.value.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadWideCharString();
                    }
                    publisherIds.value.value[i] = elem_0;
                }
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcGetPublisherMetadata(string publisherId, string logFilePath, uint locale, uint flags, RpcPointer<EvtRpcVariantList> pubMetadataProps, Titanis.DceRpc.RpcContextHandle pubMetadata, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteUniqueReferentId((publisherId == null));
            if ((publisherId != null)) {
                encoder.WriteWideCharString(publisherId);
            }
            encoder.WriteUniqueReferentId((logFilePath == null));
            if ((logFilePath != null)) {
                encoder.WriteWideCharString(logFilePath);
            }
            encoder.WriteValue(locale);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pubMetadataProps.value = decoder.ReadFixedStruct<EvtRpcVariantList>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<EvtRpcVariantList>(ref pubMetadataProps.value);
            pubMetadata = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcGetPublisherResourceMetadata(Titanis.DceRpc.RpcContextHandle handle, uint propertyId, uint flags, RpcPointer<EvtRpcVariantList> pubMetadataProps, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(handle);
            encoder.WriteValue(propertyId);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pubMetadataProps.value = decoder.ReadFixedStruct<EvtRpcVariantList>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<EvtRpcVariantList>(ref pubMetadataProps.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcGetEventMetadataEnum(Titanis.DceRpc.RpcContextHandle pubMetadata, uint flags, string reservedForFilter, Titanis.DceRpc.RpcContextHandle eventMetaDataEnum, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(pubMetadata);
            encoder.WriteValue(flags);
            encoder.WriteUniqueReferentId((reservedForFilter == null));
            if ((reservedForFilter != null)) {
                encoder.WriteWideCharString(reservedForFilter);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            eventMetaDataEnum = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcGetNextEventMetadata(Titanis.DceRpc.RpcContextHandle eventMetaDataEnum, uint flags, uint numRequested, RpcPointer<uint> numReturned, RpcPointer<RpcPointer<EvtRpcVariantList[]>> eventMetadataInstances, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(eventMetaDataEnum);
            encoder.WriteValue(flags);
            encoder.WriteValue(numRequested);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            numReturned.value = decoder.ReadUInt32();
            eventMetadataInstances.value = decoder.ReadOutUniquePointer<EvtRpcVariantList[]>(eventMetadataInstances.value);
            if ((null != eventMetadataInstances.value)) {
                eventMetadataInstances.value.value = decoder.ReadArrayHeader<EvtRpcVariantList>();
                for (int i = 0; (i < eventMetadataInstances.value.value.Length); i++
                ) {
                    EvtRpcVariantList elem_0 = eventMetadataInstances.value.value[i];
                    elem_0 = decoder.ReadFixedStruct<EvtRpcVariantList>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    eventMetadataInstances.value.value[i] = elem_0;
                }
                for (int i = 0; (i < eventMetadataInstances.value.value.Length); i++
                ) {
                    EvtRpcVariantList elem_0 = eventMetadataInstances.value.value[i];
                    decoder.ReadStructDeferral<EvtRpcVariantList>(ref elem_0);
                    eventMetadataInstances.value.value[i] = elem_0;
                }
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EvtRpcGetClassicLogDisplayName(string logName, uint locale, uint flags, RpcPointer<RpcPointer<char>> displayName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(logName);
            encoder.WriteValue(locale);
            encoder.WriteValue(flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            displayName.value = decoder.ReadOutUniquePointer<char>(displayName.value);
            if ((null != displayName.value)) {
                displayName.value.value = decoder.ReadWideChar();
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.5")]
    public class IEventServiceStub : Titanis.DceRpc.Server.RpcServiceStub {
        private static System.Guid _interfaceUuid = new System.Guid("f6beaff7-1e19-4fbb-9f8f-b89e2018337c");
        public override System.Guid InterfaceUuid {
            get {
                return _interfaceUuid;
            }
        }
        public override Titanis.DceRpc.RpcVersion InterfaceVersion {
            get {
                return new Titanis.DceRpc.RpcVersion(1, 0);
            }
        }
        private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
        public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable {
            get {
                return this._dispatchTable;
            }
        }
        private IEventService _obj;
        public IEventServiceStub(IEventService obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_EvtRpcRegisterRemoteSubscription,
                    this.Invoke_EvtRpcRemoteSubscriptionNextAsync,
                    this.Invoke_EvtRpcRemoteSubscriptionNext,
                    this.Invoke_EvtRpcRemoteSubscriptionWaitAsync,
                    this.Invoke_EvtRpcRegisterControllableOperation,
                    this.Invoke_EvtRpcRegisterLogQuery,
                    this.Invoke_EvtRpcClearLog,
                    this.Invoke_EvtRpcExportLog,
                    this.Invoke_EvtRpcLocalizeExportLog,
                    this.Invoke_EvtRpcMessageRender,
                    this.Invoke_EvtRpcMessageRenderDefault,
                    this.Invoke_EvtRpcQueryNext,
                    this.Invoke_EvtRpcQuerySeek,
                    this.Invoke_EvtRpcClose,
                    this.Invoke_EvtRpcCancel,
                    this.Invoke_EvtRpcAssertConfig,
                    this.Invoke_EvtRpcRetractConfig,
                    this.Invoke_EvtRpcOpenLogHandle,
                    this.Invoke_EvtRpcGetLogFileInfo,
                    this.Invoke_EvtRpcGetChannelList,
                    this.Invoke_EvtRpcGetChannelConfig,
                    this.Invoke_EvtRpcPutChannelConfig,
                    this.Invoke_EvtRpcGetPublisherList,
                    this.Invoke_EvtRpcGetPublisherListForChannel,
                    this.Invoke_EvtRpcGetPublisherMetadata,
                    this.Invoke_EvtRpcGetPublisherResourceMetadata,
                    this.Invoke_EvtRpcGetEventMetadataEnum,
                    this.Invoke_EvtRpcGetNextEventMetadata,
                    this.Invoke_EvtRpcGetClassicLogDisplayName};
        }
        private async Task Invoke_EvtRpcRegisterRemoteSubscription(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string channelPath;
            string query;
            string bookmarkXml;
            uint flags;
            Titanis.DceRpc.RpcContextHandle handle = new Titanis.DceRpc.RpcContextHandle();
            Titanis.DceRpc.RpcContextHandle control = new Titanis.DceRpc.RpcContextHandle();
            RpcPointer<uint> queryChannelInfoSize = new RpcPointer<uint>();
            RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>> queryChannelInfo = new RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>>();
            RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
            if ((decoder.ReadReferentId() == 0)) {
                channelPath = null;
            }
            else {
                channelPath = decoder.ReadWideCharString();
            }
            query = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                bookmarkXml = null;
            }
            else {
                bookmarkXml = decoder.ReadWideCharString();
            }
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcRegisterRemoteSubscription(channelPath, query, bookmarkXml, flags, handle, control, queryChannelInfoSize, queryChannelInfo, error, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(handle);
            encoder.WriteContextHandle(control);
            encoder.WriteValue(queryChannelInfoSize.value);
            encoder.WritePointer(queryChannelInfo.value);
            if ((null != queryChannelInfo.value)) {
                encoder.WriteArrayHeader(queryChannelInfo.value.value);
                for (int i = 0; (i < queryChannelInfo.value.value.Length); i++
                ) {
                    EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < queryChannelInfo.value.value.Length); i++
                ) {
                    EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
            encoder.WriteFixedStruct(error.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(error.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcRemoteSubscriptionNextAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle handle;
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
            encoder.WritePointer(eventDataIndices.value);
            if ((null != eventDataIndices.value)) {
                encoder.WriteArrayHeader(eventDataIndices.value.value);
                for (int i = 0; (i < eventDataIndices.value.value.Length); i++
                ) {
                    uint elem_0 = eventDataIndices.value.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WritePointer(eventDataSizes.value);
            if ((null != eventDataSizes.value)) {
                encoder.WriteArrayHeader(eventDataSizes.value.value);
                for (int i = 0; (i < eventDataSizes.value.value.Length); i++
                ) {
                    uint elem_0 = eventDataSizes.value.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(resultBufferSize.value);
            encoder.WritePointer(resultBuffer.value);
            if ((null != resultBuffer.value)) {
                encoder.WriteArrayHeader(resultBuffer.value.value);
                for (int i = 0; (i < resultBuffer.value.value.Length); i++
                ) {
                    byte elem_0 = resultBuffer.value.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcRemoteSubscriptionNext(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle handle;
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
            encoder.WritePointer(eventDataIndices.value);
            if ((null != eventDataIndices.value)) {
                encoder.WriteArrayHeader(eventDataIndices.value.value);
                for (int i = 0; (i < eventDataIndices.value.value.Length); i++
                ) {
                    uint elem_0 = eventDataIndices.value.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WritePointer(eventDataSizes.value);
            if ((null != eventDataSizes.value)) {
                encoder.WriteArrayHeader(eventDataSizes.value.value);
                for (int i = 0; (i < eventDataSizes.value.value.Length); i++
                ) {
                    uint elem_0 = eventDataSizes.value.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(resultBufferSize.value);
            encoder.WritePointer(resultBuffer.value);
            if ((null != resultBuffer.value)) {
                encoder.WriteArrayHeader(resultBuffer.value.value);
                for (int i = 0; (i < resultBuffer.value.value.Length); i++
                ) {
                    byte elem_0 = resultBuffer.value.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcRemoteSubscriptionWaitAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle handle;
            handle = decoder.ReadContextHandle();
            var invokeTask = this._obj.EvtRpcRemoteSubscriptionWaitAsync(handle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcRegisterControllableOperation(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle handle = new Titanis.DceRpc.RpcContextHandle();
            var invokeTask = this._obj.EvtRpcRegisterControllableOperation(handle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(handle);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcRegisterLogQuery(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string path;
            string query;
            uint flags;
            Titanis.DceRpc.RpcContextHandle handle = new Titanis.DceRpc.RpcContextHandle();
            Titanis.DceRpc.RpcContextHandle opControl = new Titanis.DceRpc.RpcContextHandle();
            RpcPointer<uint> queryChannelInfoSize = new RpcPointer<uint>();
            RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>> queryChannelInfo = new RpcPointer<RpcPointer<EvtRpcQueryChannelInfo[]>>();
            RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
            if ((decoder.ReadReferentId() == 0)) {
                path = null;
            }
            else {
                path = decoder.ReadWideCharString();
            }
            query = decoder.ReadWideCharString();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcRegisterLogQuery(path, query, flags, handle, opControl, queryChannelInfoSize, queryChannelInfo, error, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(handle);
            encoder.WriteContextHandle(opControl);
            encoder.WriteValue(queryChannelInfoSize.value);
            encoder.WritePointer(queryChannelInfo.value);
            if ((null != queryChannelInfo.value)) {
                encoder.WriteArrayHeader(queryChannelInfo.value.value);
                for (int i = 0; (i < queryChannelInfo.value.value.Length); i++
                ) {
                    EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < queryChannelInfo.value.value.Length); i++
                ) {
                    EvtRpcQueryChannelInfo elem_0 = queryChannelInfo.value.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
            encoder.WriteFixedStruct(error.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(error.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcClearLog(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle control;
            string channelPath;
            string backupPath;
            uint flags;
            RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
            control = decoder.ReadContextHandle();
            channelPath = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                backupPath = null;
            }
            else {
                backupPath = decoder.ReadWideCharString();
            }
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcClearLog(control, channelPath, backupPath, flags, error, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(error.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(error.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcExportLog(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle control;
            string channelPath;
            string query;
            string backupPath;
            uint flags;
            RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
            control = decoder.ReadContextHandle();
            if ((decoder.ReadReferentId() == 0)) {
                channelPath = null;
            }
            else {
                channelPath = decoder.ReadWideCharString();
            }
            query = decoder.ReadWideCharString();
            backupPath = decoder.ReadWideCharString();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcExportLog(control, channelPath, query, backupPath, flags, error, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(error.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(error.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcLocalizeExportLog(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle control;
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
            encoder.WriteFixedStruct(error.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(error.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcMessageRender(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle pubCfgObj;
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
            for (int i = 0; (i < eventId.Length); i++
            ) {
                byte elem_0 = eventId[i];
                elem_0 = decoder.ReadUnsignedChar();
                eventId[i] = elem_0;
            }
            messageId = decoder.ReadUInt32();
            values = decoder.ReadFixedStruct<EvtRpcVariantList>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<EvtRpcVariantList>(ref values);
            flags = decoder.ReadUInt32();
            maxSizeString = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcMessageRender(pubCfgObj, sizeEventId, eventId, messageId, values, flags, maxSizeString, actualSizeString, neededSizeString, @string, error, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(actualSizeString.value);
            encoder.WriteValue(neededSizeString.value);
            encoder.WritePointer(@string.value);
            if ((null != @string.value)) {
                encoder.WriteArrayHeader(@string.value.value);
                for (int i = 0; (i < @string.value.value.Length); i++
                ) {
                    byte elem_0 = @string.value.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteFixedStruct(error.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(error.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcMessageRenderDefault(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
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
            for (int i = 0; (i < eventId.Length); i++
            ) {
                byte elem_0 = eventId[i];
                elem_0 = decoder.ReadUnsignedChar();
                eventId[i] = elem_0;
            }
            messageId = decoder.ReadUInt32();
            values = decoder.ReadFixedStruct<EvtRpcVariantList>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<EvtRpcVariantList>(ref values);
            flags = decoder.ReadUInt32();
            maxSizeString = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcMessageRenderDefault(sizeEventId, eventId, messageId, values, flags, maxSizeString, actualSizeString, neededSizeString, @string, error, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(actualSizeString.value);
            encoder.WriteValue(neededSizeString.value);
            encoder.WritePointer(@string.value);
            if ((null != @string.value)) {
                encoder.WriteArrayHeader(@string.value.value);
                for (int i = 0; (i < @string.value.value.Length); i++
                ) {
                    byte elem_0 = @string.value.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteFixedStruct(error.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(error.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcQueryNext(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle logQuery;
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
            encoder.WritePointer(eventDataIndices.value);
            if ((null != eventDataIndices.value)) {
                encoder.WriteArrayHeader(eventDataIndices.value.value);
                for (int i = 0; (i < eventDataIndices.value.value.Length); i++
                ) {
                    uint elem_0 = eventDataIndices.value.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WritePointer(eventDataSizes.value);
            if ((null != eventDataSizes.value)) {
                encoder.WriteArrayHeader(eventDataSizes.value.value);
                for (int i = 0; (i < eventDataSizes.value.value.Length); i++
                ) {
                    uint elem_0 = eventDataSizes.value.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(resultBufferSize.value);
            encoder.WritePointer(resultBuffer.value);
            if ((null != resultBuffer.value)) {
                encoder.WriteArrayHeader(resultBuffer.value.value);
                for (int i = 0; (i < resultBuffer.value.value.Length); i++
                ) {
                    byte elem_0 = resultBuffer.value.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcQuerySeek(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle logQuery;
            long pos;
            string bookmarkXml;
            uint timeOut;
            uint flags;
            RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
            logQuery = decoder.ReadContextHandle();
            pos = decoder.ReadInt64();
            if ((decoder.ReadReferentId() == 0)) {
                bookmarkXml = null;
            }
            else {
                bookmarkXml = decoder.ReadWideCharString();
            }
            timeOut = decoder.ReadUInt32();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcQuerySeek(logQuery, pos, bookmarkXml, timeOut, flags, error, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(error.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(error.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcClose(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle handle;
            handle = decoder.ReadContextHandle();
            var invokeTask = this._obj.EvtRpcClose(handle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(handle);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcCancel(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle handle;
            handle = decoder.ReadContextHandle();
            var invokeTask = this._obj.EvtRpcCancel(handle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcAssertConfig(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string path;
            uint flags;
            path = decoder.ReadWideCharString();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcAssertConfig(path, flags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcRetractConfig(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string path;
            uint flags;
            path = decoder.ReadWideCharString();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcRetractConfig(path, flags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcOpenLogHandle(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string channel;
            uint flags;
            Titanis.DceRpc.RpcContextHandle handle = new Titanis.DceRpc.RpcContextHandle();
            RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
            channel = decoder.ReadWideCharString();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcOpenLogHandle(channel, flags, handle, error, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(handle);
            encoder.WriteFixedStruct(error.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(error.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcGetLogFileInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle logHandle;
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
            for (int i = 0; (i < propertyValueBuffer.value.Length); i++
            ) {
                byte elem_0 = propertyValueBuffer.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(propertyValueBufferLength.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcGetChannelList(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint flags;
            RpcPointer<uint> numChannelPaths = new RpcPointer<uint>();
            RpcPointer<RpcPointer<RpcPointer<string>[]>> channelPaths = new RpcPointer<RpcPointer<RpcPointer<string>[]>>();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcGetChannelList(flags, numChannelPaths, channelPaths, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(numChannelPaths.value);
            encoder.WritePointer(channelPaths.value);
            if ((null != channelPaths.value)) {
                encoder.WriteArrayHeader(channelPaths.value.value);
                for (int i = 0; (i < channelPaths.value.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = channelPaths.value.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < channelPaths.value.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = channelPaths.value.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteWideCharString(elem_0.value);
                    }
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcGetChannelConfig(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string channelPath;
            uint flags;
            RpcPointer<EvtRpcVariantList> props = new RpcPointer<EvtRpcVariantList>();
            channelPath = decoder.ReadWideCharString();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcGetChannelConfig(channelPath, flags, props, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(props.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(props.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcPutChannelConfig(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string channelPath;
            uint flags;
            EvtRpcVariantList props;
            RpcPointer<RpcInfo> error = new RpcPointer<RpcInfo>();
            channelPath = decoder.ReadWideCharString();
            flags = decoder.ReadUInt32();
            props = decoder.ReadFixedStruct<EvtRpcVariantList>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<EvtRpcVariantList>(ref props);
            var invokeTask = this._obj.EvtRpcPutChannelConfig(channelPath, flags, props, error, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(error.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(error.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcGetPublisherList(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint flags;
            RpcPointer<uint> numPublisherIds = new RpcPointer<uint>();
            RpcPointer<RpcPointer<RpcPointer<string>[]>> publisherIds = new RpcPointer<RpcPointer<RpcPointer<string>[]>>();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcGetPublisherList(flags, numPublisherIds, publisherIds, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(numPublisherIds.value);
            encoder.WritePointer(publisherIds.value);
            if ((null != publisherIds.value)) {
                encoder.WriteArrayHeader(publisherIds.value.value);
                for (int i = 0; (i < publisherIds.value.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = publisherIds.value.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < publisherIds.value.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = publisherIds.value.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteWideCharString(elem_0.value);
                    }
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcGetPublisherListForChannel(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            char channelName;
            uint flags;
            RpcPointer<uint> numPublisherIds = new RpcPointer<uint>();
            RpcPointer<RpcPointer<RpcPointer<string>[]>> publisherIds = new RpcPointer<RpcPointer<RpcPointer<string>[]>>();
            channelName = decoder.ReadWideChar();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcGetPublisherListForChannel(channelName, flags, numPublisherIds, publisherIds, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(numPublisherIds.value);
            encoder.WritePointer(publisherIds.value);
            if ((null != publisherIds.value)) {
                encoder.WriteArrayHeader(publisherIds.value.value);
                for (int i = 0; (i < publisherIds.value.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = publisherIds.value.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < publisherIds.value.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = publisherIds.value.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteWideCharString(elem_0.value);
                    }
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcGetPublisherMetadata(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string publisherId;
            string logFilePath;
            uint locale;
            uint flags;
            RpcPointer<EvtRpcVariantList> pubMetadataProps = new RpcPointer<EvtRpcVariantList>();
            Titanis.DceRpc.RpcContextHandle pubMetadata = new Titanis.DceRpc.RpcContextHandle();
            if ((decoder.ReadReferentId() == 0)) {
                publisherId = null;
            }
            else {
                publisherId = decoder.ReadWideCharString();
            }
            if ((decoder.ReadReferentId() == 0)) {
                logFilePath = null;
            }
            else {
                logFilePath = decoder.ReadWideCharString();
            }
            locale = decoder.ReadUInt32();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcGetPublisherMetadata(publisherId, logFilePath, locale, flags, pubMetadataProps, pubMetadata, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(pubMetadataProps.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(pubMetadataProps.value);
            encoder.WriteContextHandle(pubMetadata);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcGetPublisherResourceMetadata(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle handle;
            uint propertyId;
            uint flags;
            RpcPointer<EvtRpcVariantList> pubMetadataProps = new RpcPointer<EvtRpcVariantList>();
            handle = decoder.ReadContextHandle();
            propertyId = decoder.ReadUInt32();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcGetPublisherResourceMetadata(handle, propertyId, flags, pubMetadataProps, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(pubMetadataProps.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(pubMetadataProps.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcGetEventMetadataEnum(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle pubMetadata;
            uint flags;
            string reservedForFilter;
            Titanis.DceRpc.RpcContextHandle eventMetaDataEnum = new Titanis.DceRpc.RpcContextHandle();
            pubMetadata = decoder.ReadContextHandle();
            flags = decoder.ReadUInt32();
            if ((decoder.ReadReferentId() == 0)) {
                reservedForFilter = null;
            }
            else {
                reservedForFilter = decoder.ReadWideCharString();
            }
            var invokeTask = this._obj.EvtRpcGetEventMetadataEnum(pubMetadata, flags, reservedForFilter, eventMetaDataEnum, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(eventMetaDataEnum);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcGetNextEventMetadata(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle eventMetaDataEnum;
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
            encoder.WritePointer(eventMetadataInstances.value);
            if ((null != eventMetadataInstances.value)) {
                encoder.WriteArrayHeader(eventMetadataInstances.value.value);
                for (int i = 0; (i < eventMetadataInstances.value.value.Length); i++
                ) {
                    EvtRpcVariantList elem_0 = eventMetadataInstances.value.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < eventMetadataInstances.value.value.Length); i++
                ) {
                    EvtRpcVariantList elem_0 = eventMetadataInstances.value.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EvtRpcGetClassicLogDisplayName(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string logName;
            uint locale;
            uint flags;
            RpcPointer<RpcPointer<char>> displayName = new RpcPointer<RpcPointer<char>>();
            logName = decoder.ReadWideCharString();
            locale = decoder.ReadUInt32();
            flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EvtRpcGetClassicLogDisplayName(logName, locale, flags, displayName, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(displayName.value);
            if ((null != displayName.value)) {
                encoder.WriteValue(displayName.value.value);
            }
            encoder.WriteValue(retval);
        }
    }
}
