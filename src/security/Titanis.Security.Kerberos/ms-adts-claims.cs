namespace ms_adtsclaims {
    using System;
    using System.Threading.Tasks;
    using Titanis;
    using Titanis.DceRpc;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public enum CLAIM_TYPE : int {
        CLAIM_TYPE_INT64 = 1,
        CLAIM_TYPE_UINT64 = 2,
        CLAIM_TYPE_STRING = 3,
        CLAIM_TYPE_BOOLEAN = 6,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public enum CLAIMS_SOURCE_TYPE : int {
        CLAIMS_SOURCE_TYPE_AD = 1,
        CLAIMS_SOURCE_TYPE_CERTIFICATE = 2,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public enum CLAIMS_COMPRESSION_FORMAT : int {
        COMPRESSION_FORMAT_NONE = 0,
        COMPRESSION_FORMAT_LZNT1 = 2,
        COMPRESSION_FORMAT_XPRESS = 3,
        COMPRESSION_FORMAT_XPRESS_HUFF = 4,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct Unnamed_2 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ValueCount);
            encoder.WritePointer(this.Int64Values);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ValueCount = decoder.ReadUInt32();
            this.Int64Values = decoder.ReadUniquePointer<long[]>();
        }
        public uint ValueCount;
        public RpcPointer<long[]> Int64Values;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Int64Values)) {
                encoder.WriteArrayHeader(this.Int64Values.value);
                for (int i = 0; (i < this.Int64Values.value.Length); i++
                ) {
                    long elem_0 = this.Int64Values.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Int64Values)) {
                this.Int64Values.value = decoder.ReadArrayHeader<long>();
                for (int i = 0; (i < this.Int64Values.value.Length); i++
                ) {
                    long elem_0 = this.Int64Values.value[i];
                    elem_0 = decoder.ReadInt64();
                    this.Int64Values.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct Unnamed_3 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ValueCount);
            encoder.WritePointer(this.Uint64Values);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ValueCount = decoder.ReadUInt32();
            this.Uint64Values = decoder.ReadUniquePointer<ulong[]>();
        }
        public uint ValueCount;
        public RpcPointer<ulong[]> Uint64Values;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Uint64Values)) {
                encoder.WriteArrayHeader(this.Uint64Values.value);
                for (int i = 0; (i < this.Uint64Values.value.Length); i++
                ) {
                    ulong elem_0 = this.Uint64Values.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Uint64Values)) {
                this.Uint64Values.value = decoder.ReadArrayHeader<ulong>();
                for (int i = 0; (i < this.Uint64Values.value.Length); i++
                ) {
                    ulong elem_0 = this.Uint64Values.value[i];
                    elem_0 = decoder.ReadUInt64();
                    this.Uint64Values.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct Unnamed_4 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ValueCount);
            encoder.WritePointer(this.StringValues);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ValueCount = decoder.ReadUInt32();
            this.StringValues = decoder.ReadUniquePointer<RpcPointer<string>[]>();
        }
        public uint ValueCount;
        public RpcPointer<RpcPointer<string>[]> StringValues;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.StringValues)) {
                encoder.WriteArrayHeader(this.StringValues.value);
                for (int i = 0; (i < this.StringValues.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = this.StringValues.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < this.StringValues.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = this.StringValues.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteWideCharString(elem_0.value);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.StringValues)) {
                this.StringValues.value = decoder.ReadArrayHeader<RpcPointer<string>>();
                for (int i = 0; (i < this.StringValues.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = this.StringValues.value[i];
                    elem_0 = decoder.ReadUniquePointer<string>();
                    this.StringValues.value[i] = elem_0;
                }
                for (int i = 0; (i < this.StringValues.value.Length); i++
                ) {
                    RpcPointer<string> elem_0 = this.StringValues.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadWideCharString();
                    }
                    this.StringValues.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct Unnamed_5 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ValueCount);
            encoder.WritePointer(this.BooleanValues);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ValueCount = decoder.ReadUInt32();
            this.BooleanValues = decoder.ReadUniquePointer<ulong[]>();
        }
        public uint ValueCount;
        public RpcPointer<ulong[]> BooleanValues;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.BooleanValues)) {
                encoder.WriteArrayHeader(this.BooleanValues.value);
                for (int i = 0; (i < this.BooleanValues.value.Length); i++
                ) {
                    ulong elem_0 = this.BooleanValues.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.BooleanValues)) {
                this.BooleanValues.value = decoder.ReadArrayHeader<ulong>();
                for (int i = 0; (i < this.BooleanValues.value.Length); i++
                ) {
                    ulong elem_0 = this.BooleanValues.value[i];
                    elem_0 = decoder.ReadUInt64();
                    this.BooleanValues.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct Unnamed_1 : Titanis.DceRpc.IRpcFixedStruct {
        public CLAIM_TYPE Type;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.Type)));
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.Type)) == 1)) {
                encoder.WriteFixedStruct(this.@__unnamed_0, Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.Type)) == 2)) {
                    encoder.WriteFixedStruct(this.@__unnamed_1, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.Type)) == 3)) {
                        encoder.WriteFixedStruct(this.@__unnamed_2, Titanis.DceRpc.NdrAlignment.NativePtr);
                    }
                    else {
                        if ((((int)(this.Type)) == 6)) {
                            encoder.WriteFixedStruct(this.@__unnamed_3, Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                    }
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Type = ((CLAIM_TYPE)(decoder.ReadInt16()));
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.Type)) == 1)) {
                this.@__unnamed_0 = decoder.ReadFixedStruct<Unnamed_2>(Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.Type)) == 2)) {
                    this.@__unnamed_1 = decoder.ReadFixedStruct<Unnamed_3>(Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.Type)) == 3)) {
                        this.@__unnamed_2 = decoder.ReadFixedStruct<Unnamed_4>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    }
                    else {
                        if ((((int)(this.Type)) == 6)) {
                            this.@__unnamed_3 = decoder.ReadFixedStruct<Unnamed_5>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                    }
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.Type)) == 1)) {
                encoder.WriteStructDeferral(this.@__unnamed_0);
            }
            else {
                if ((((int)(this.Type)) == 2)) {
                    encoder.WriteStructDeferral(this.@__unnamed_1);
                }
                else {
                    if ((((int)(this.Type)) == 3)) {
                        encoder.WriteStructDeferral(this.@__unnamed_2);
                    }
                    else {
                        if ((((int)(this.Type)) == 6)) {
                            encoder.WriteStructDeferral(this.@__unnamed_3);
                        }
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.Type)) == 1)) {
                decoder.ReadStructDeferral<Unnamed_2>(ref this.@__unnamed_0);
            }
            else {
                if ((((int)(this.Type)) == 2)) {
                    decoder.ReadStructDeferral<Unnamed_3>(ref this.@__unnamed_1);
                }
                else {
                    if ((((int)(this.Type)) == 3)) {
                        decoder.ReadStructDeferral<Unnamed_4>(ref this.@__unnamed_2);
                    }
                    else {
                        if ((((int)(this.Type)) == 6)) {
                            decoder.ReadStructDeferral<Unnamed_5>(ref this.@__unnamed_3);
                        }
                    }
                }
            }
        }
        public Unnamed_2 @__unnamed_0;
        public Unnamed_3 @__unnamed_1;
        public Unnamed_4 @__unnamed_2;
        public Unnamed_5 @__unnamed_3;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct CLAIM_ENTRY : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.Id);
            encoder.WriteValue(((short)(this.Type)));
            encoder.WriteUnion(this.Values);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Id = decoder.ReadUniquePointer<string>();
            this.Type = ((CLAIM_TYPE)(decoder.ReadInt16()));
            this.Values = decoder.ReadUnion<Unnamed_1>();
        }
        public RpcPointer<string> Id;
        public CLAIM_TYPE Type;
        public Unnamed_1 Values;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Id)) {
                encoder.WriteWideCharString(this.Id.value);
            }
            encoder.WriteStructDeferral(this.Values);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Id)) {
                this.Id.value = decoder.ReadWideCharString();
            }
            decoder.ReadStructDeferral<Unnamed_1>(ref this.Values);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct CLAIMS_ARRAY : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.usClaimsSourceType)));
            encoder.WriteValue(this.ulClaimsCount);
            encoder.WritePointer(this.ClaimEntries);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.usClaimsSourceType = ((CLAIMS_SOURCE_TYPE)(decoder.ReadInt16()));
            this.ulClaimsCount = decoder.ReadUInt32();
            this.ClaimEntries = decoder.ReadUniquePointer<CLAIM_ENTRY[]>();
        }
        public CLAIMS_SOURCE_TYPE usClaimsSourceType;
        public uint ulClaimsCount;
        public RpcPointer<CLAIM_ENTRY[]> ClaimEntries;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.ClaimEntries)) {
                encoder.WriteArrayHeader(this.ClaimEntries.value);
                for (int i = 0; (i < this.ClaimEntries.value.Length); i++
                ) {
                    CLAIM_ENTRY elem_0 = this.ClaimEntries.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.ClaimEntries.value.Length); i++
                ) {
                    CLAIM_ENTRY elem_0 = this.ClaimEntries.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.ClaimEntries)) {
                this.ClaimEntries.value = decoder.ReadArrayHeader<CLAIM_ENTRY>();
                for (int i = 0; (i < this.ClaimEntries.value.Length); i++
                ) {
                    CLAIM_ENTRY elem_0 = this.ClaimEntries.value[i];
                    elem_0 = decoder.ReadFixedStruct<CLAIM_ENTRY>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.ClaimEntries.value[i] = elem_0;
                }
                for (int i = 0; (i < this.ClaimEntries.value.Length); i++
                ) {
                    CLAIM_ENTRY elem_0 = this.ClaimEntries.value[i];
                    decoder.ReadStructDeferral<CLAIM_ENTRY>(ref elem_0);
                    this.ClaimEntries.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct CLAIMS_SET : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ulClaimsArrayCount);
            encoder.WritePointer(this.ClaimsArrays);
            encoder.WriteValue(this.usReservedType);
            encoder.WriteValue(this.ulReservedFieldSize);
            encoder.WritePointer(this.ReservedField);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ulClaimsArrayCount = decoder.ReadUInt32();
            this.ClaimsArrays = decoder.ReadUniquePointer<CLAIMS_ARRAY[]>();
            this.usReservedType = decoder.ReadUInt16();
            this.ulReservedFieldSize = decoder.ReadUInt32();
            this.ReservedField = decoder.ReadUniquePointer<byte[]>();
        }
        public uint ulClaimsArrayCount;
        public RpcPointer<CLAIMS_ARRAY[]> ClaimsArrays;
        public ushort usReservedType;
        public uint ulReservedFieldSize;
        public RpcPointer<byte[]> ReservedField;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.ClaimsArrays)) {
                encoder.WriteArrayHeader(this.ClaimsArrays.value);
                for (int i = 0; (i < this.ClaimsArrays.value.Length); i++
                ) {
                    CLAIMS_ARRAY elem_0 = this.ClaimsArrays.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.ClaimsArrays.value.Length); i++
                ) {
                    CLAIMS_ARRAY elem_0 = this.ClaimsArrays.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
            if ((null != this.ReservedField)) {
                encoder.WriteArrayHeader(this.ReservedField.value);
                for (int i = 0; (i < this.ReservedField.value.Length); i++
                ) {
                    byte elem_0 = this.ReservedField.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.ClaimsArrays)) {
                this.ClaimsArrays.value = decoder.ReadArrayHeader<CLAIMS_ARRAY>();
                for (int i = 0; (i < this.ClaimsArrays.value.Length); i++
                ) {
                    CLAIMS_ARRAY elem_0 = this.ClaimsArrays.value[i];
                    elem_0 = decoder.ReadFixedStruct<CLAIMS_ARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.ClaimsArrays.value[i] = elem_0;
                }
                for (int i = 0; (i < this.ClaimsArrays.value.Length); i++
                ) {
                    CLAIMS_ARRAY elem_0 = this.ClaimsArrays.value[i];
                    decoder.ReadStructDeferral<CLAIMS_ARRAY>(ref elem_0);
                    this.ClaimsArrays.value[i] = elem_0;
                }
            }
            if ((null != this.ReservedField)) {
                this.ReservedField.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.ReservedField.value.Length); i++
                ) {
                    byte elem_0 = this.ReservedField.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.ReservedField.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct CLAIMS_SET_METADATA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ulClaimsSetSize);
            encoder.WritePointer(this.ClaimsSet);
            encoder.WriteValue(((short)(this.usCompressionFormat)));
            encoder.WriteValue(this.ulUncompressedClaimsSetSize);
            encoder.WriteValue(this.usReservedType);
            encoder.WriteValue(this.ulReservedFieldSize);
            encoder.WritePointer(this.ReservedField);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ulClaimsSetSize = decoder.ReadUInt32();
            this.ClaimsSet = decoder.ReadUniquePointer<byte[]>();
            this.usCompressionFormat = ((CLAIMS_COMPRESSION_FORMAT)(decoder.ReadInt16()));
            this.ulUncompressedClaimsSetSize = decoder.ReadUInt32();
            this.usReservedType = decoder.ReadUInt16();
            this.ulReservedFieldSize = decoder.ReadUInt32();
            this.ReservedField = decoder.ReadUniquePointer<byte[]>();
        }
        public uint ulClaimsSetSize;
        public RpcPointer<byte[]> ClaimsSet;
        public CLAIMS_COMPRESSION_FORMAT usCompressionFormat;
        public uint ulUncompressedClaimsSetSize;
        public ushort usReservedType;
        public uint ulReservedFieldSize;
        public RpcPointer<byte[]> ReservedField;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.ClaimsSet)) {
                encoder.WriteArrayHeader(this.ClaimsSet.value);
                for (int i = 0; (i < this.ClaimsSet.value.Length); i++
                ) {
                    byte elem_0 = this.ClaimsSet.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            if ((null != this.ReservedField)) {
                encoder.WriteArrayHeader(this.ReservedField.value);
                for (int i = 0; (i < this.ReservedField.value.Length); i++
                ) {
                    byte elem_0 = this.ReservedField.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.ClaimsSet)) {
                this.ClaimsSet.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.ClaimsSet.value.Length); i++
                ) {
                    byte elem_0 = this.ClaimsSet.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.ClaimsSet.value[i] = elem_0;
                }
            }
            if ((null != this.ReservedField)) {
                this.ReservedField.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.ReservedField.value.Length); i++
                ) {
                    byte elem_0 = this.ReservedField.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.ReservedField.value[i] = elem_0;
                }
            }
        }
    }
}
