#pragma warning disable

namespace ms_oaut {
	using ms_dcom;
    using System;
	using System.Text;
    using System.Threading.Tasks;
    using Titanis;
	using Titanis.DceRpc;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SAFEARRAYBOUND : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cElements);
            encoder.WriteValue(this.lLbound);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cElements = decoder.ReadUInt32();
            this.lLbound = decoder.ReadInt32();
        }
        public uint cElements;
        public int lLbound;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct FLAGGED_WORD_BLOB : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cBytes);
            encoder.WriteValue(this.clSize);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cBytes = decoder.ReadUInt32();
            this.clSize = decoder.ReadUInt32();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.asData);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.asData = decoder.ReadArrayHeader<ushort>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.asData.Length); i++
            ) {
                ushort elem_0 = this.asData[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.asData.Length); i++
            ) {
                ushort elem_0 = this.asData[i];
                elem_0 = decoder.ReadUInt16();
                this.asData[i] = elem_0;
            }
        }
        public uint cBytes;
        public uint clSize;
        public ushort[] asData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SAFEARR_BSTR : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Size);
            for (int i = 0; (i < this.aBstr.value.Length); i++
            ) {
                RpcPointer<FLAGGED_WORD_BLOB> elem_0 = this.aBstr.value[i];
                encoder.WritePointer(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Size = decoder.ReadUInt32();
            this.aBstr = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>[]>();
            for (int i = 0; (i < this.aBstr.value.Length); i++
            ) {
                RpcPointer<FLAGGED_WORD_BLOB> elem_0 = this.aBstr.value[i];
                elem_0 = decoder.ReadUniquePointer<FLAGGED_WORD_BLOB>();
                this.aBstr.value[i] = elem_0;
            }
        }
        public uint Size;
        public RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>[]> aBstr;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.aBstr.value.Length); i++
            ) {
                RpcPointer<FLAGGED_WORD_BLOB> elem_0 = this.aBstr.value[i];
                if ((null != elem_0)) {
                    encoder.WriteConformantStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._4Byte);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.aBstr.value.Length); i++
            ) {
                RpcPointer<FLAGGED_WORD_BLOB> elem_0 = this.aBstr.value[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                    decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref elem_0.value);
                }
                this.aBstr.value[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SAFEARR_UNKNOWN : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Size);
            for (int i = 0; (i < this.apUnknown.value.Length); i++
            ) {
                Titanis.DceRpc.TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
                encoder.WriteInterfacePointer(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Size = decoder.ReadUInt32();
            this.apUnknown = new RpcPointer<Titanis.DceRpc.TypedObjref<IUnknown>[]>();
            for (int i = 0; (i < this.apUnknown.value.Length); i++
            ) {
                Titanis.DceRpc.TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
                elem_0 = decoder.ReadInterfacePointer<IUnknown>();
                this.apUnknown.value[i] = elem_0;
            }
        }
        public uint Size;
        public RpcPointer<Titanis.DceRpc.TypedObjref<IUnknown>[]> apUnknown;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.apUnknown.value.Length); i++
            ) {
                Titanis.DceRpc.TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
                encoder.WriteInterfacePointerBody(elem_0);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.apUnknown.value.Length); i++
            ) {
                Titanis.DceRpc.TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
                decoder.ReadInterfacePointer(elem_0);
                this.apUnknown.value[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SAFEARR_DISPATCH : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Size);
            for (int i = 0; (i < this.apDispatch.value.Length); i++
            ) {
                Titanis.DceRpc.TypedObjref<IDispatch> elem_0 = this.apDispatch.value[i];
                encoder.WriteInterfacePointer(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Size = decoder.ReadUInt32();
            this.apDispatch = new RpcPointer<Titanis.DceRpc.TypedObjref<IDispatch>[]>();
            for (int i = 0; (i < this.apDispatch.value.Length); i++
            ) {
                Titanis.DceRpc.TypedObjref<IDispatch> elem_0 = this.apDispatch.value[i];
                elem_0 = decoder.ReadInterfacePointer<IDispatch>();
                this.apDispatch.value[i] = elem_0;
            }
        }
        public uint Size;
        public RpcPointer<Titanis.DceRpc.TypedObjref<IDispatch>[]> apDispatch;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.apDispatch.value.Length); i++
            ) {
                Titanis.DceRpc.TypedObjref<IDispatch> elem_0 = this.apDispatch.value[i];
                encoder.WriteInterfacePointerBody(elem_0);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.apDispatch.value.Length); i++
            ) {
                Titanis.DceRpc.TypedObjref<IDispatch> elem_0 = this.apDispatch.value[i];
                decoder.ReadInterfacePointer(elem_0);
                this.apDispatch.value[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct CURRENCY : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.int64);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.int64 = decoder.ReadInt64();
        }
        public long int64;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct wireBRECORDStr : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.fFlags);
            encoder.WriteValue(this.clSize);
            encoder.WritePointer(this.pRecInfo);
            encoder.WritePointer(this.pRecord);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.fFlags = decoder.ReadUInt32();
            this.clSize = decoder.ReadUInt32();
            this.pRecInfo = decoder.ReadUniquePointer<ms_dcom.MInterfacePointer>();
            this.pRecord = decoder.ReadUniquePointer<byte[]>();
        }
        public uint fFlags;
        public uint clSize;
        public RpcPointer<ms_dcom.MInterfacePointer> pRecInfo;
        public RpcPointer<byte[]> pRecord;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pRecInfo)) {
                encoder.WriteConformantStruct(this.pRecInfo.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.pRecInfo.value);
            }
            if ((null != this.pRecord)) {
                encoder.WriteArrayHeader(this.pRecord.value);
                for (int i = 0; (i < this.pRecord.value.Length); i++
                ) {
                    byte elem_0 = this.pRecord.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pRecInfo)) {
                this.pRecInfo.value = decoder.ReadConformantStruct<ms_dcom.MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dcom.MInterfacePointer>(ref this.pRecInfo.value);
            }
            if ((null != this.pRecord)) {
                this.pRecord.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.pRecord.value.Length); i++
                ) {
                    byte elem_0 = this.pRecord.value[i];
                    elem_0 = decoder.ReadByte();
                    this.pRecord.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DECIMAL : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.wReserved);
            encoder.WriteValue(this.scale);
            encoder.WriteValue(this.sign);
            encoder.WriteValue(this.Hi32);
            encoder.WriteValue(this.Lo64);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.wReserved = decoder.ReadUInt16();
            this.scale = decoder.ReadByte();
            this.sign = decoder.ReadByte();
            this.Hi32 = decoder.ReadUInt32();
            this.Lo64 = decoder.ReadUInt64();
        }
        public ushort wReserved;
        public byte scale;
        public byte sign;
        public uint Hi32;
        public ulong Lo64;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct Unnamed_1 : Titanis.DceRpc.IRpcFixedStruct {
        public uint vt;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.vt);
            //encoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.vt)) == 20)) {
                encoder.WriteValue(this.llVal);
            }
            else {
                if ((((int)(this.vt)) == 3)) {
                    encoder.WriteValue(this.lVal);
                }
                else {
                    if ((((int)(this.vt)) == 17)) {
                        encoder.WriteValue(this.bVal);
                    }
                    else {
                        if ((((int)(this.vt)) == 2)) {
                            encoder.WriteValue(this.iVal);
                        }
                        else {
                            if ((((int)(this.vt)) == 4)) {
                                encoder.WriteValue(this.fltVal);
                            }
                            else {
                                if ((((int)(this.vt)) == 5)) {
                                    encoder.WriteValue(this.dblVal);
                                }
                                else {
                                    if ((((int)(this.vt)) == 11)) {
                                        encoder.WriteValue(this.boolVal);
                                    }
                                    else {
                                        if ((((int)(this.vt)) == 10)) {
                                            encoder.WriteValue(this.scode);
                                        }
                                        else {
                                            if ((((int)(this.vt)) == 6)) {
                                                encoder.WriteFixedStruct(this.cyVal, Titanis.DceRpc.NdrAlignment._8Byte);
                                            }
                                            else {
                                                if ((((int)(this.vt)) == 7)) {
                                                    encoder.WriteValue(this.date);
                                                }
                                                else {
                                                    if ((((int)(this.vt)) == 8)) {
                                                        encoder.WritePointer(this.bstrVal);
                                                    }
                                                    else {
                                                        if ((((int)(this.vt)) == 13)) {
                                                            encoder.WriteInterfacePointer(this.punkVal);
                                                        }
                                                        else {
                                                            if ((((int)(this.vt)) == 9)) {
                                                                encoder.WriteInterfacePointer(this.pdispVal);
                                                            }
                                                            else {
                                                                if ((((int)(this.vt)) == 8192)) {
                                                                    encoder.WritePointer(this.parray);
                                                                }
                                                                else {
                                                                    if (((((int)(this.vt)) == 36) 
                                                                                && (((int)(this.vt)) == 16420))) {
                                                                        encoder.WritePointer(this.brecVal);
                                                                    }
                                                                    else {
                                                                        if ((((int)(this.vt)) == 16401)) {
                                                                            encoder.WritePointer(this.pbVal);
                                                                        }
                                                                        else {
                                                                            if ((((int)(this.vt)) == 16386)) {
                                                                                encoder.WritePointer(this.piVal);
                                                                            }
                                                                            else {
                                                                                if ((((int)(this.vt)) == 16387)) {
                                                                                    encoder.WritePointer(this.plVal);
                                                                                }
                                                                                else {
                                                                                    if ((((int)(this.vt)) == 16404)) {
                                                                                        encoder.WritePointer(this.pllVal);
                                                                                    }
                                                                                    else {
                                                                                        if ((((int)(this.vt)) == 16388)) {
                                                                                            encoder.WritePointer(this.pfltVal);
                                                                                        }
                                                                                        else {
                                                                                            if ((((int)(this.vt)) == 16389)) {
                                                                                                encoder.WritePointer(this.pdblVal);
                                                                                            }
                                                                                            else {
                                                                                                if ((((int)(this.vt)) == 16395)) {
                                                                                                    encoder.WritePointer(this.pboolVal);
                                                                                                }
                                                                                                else {
                                                                                                    if ((((int)(this.vt)) == 16394)) {
                                                                                                        encoder.WritePointer(this.pscode);
                                                                                                    }
                                                                                                    else {
                                                                                                        if ((((int)(this.vt)) == 16390)) {
                                                                                                            encoder.WritePointer(this.pcyVal);
                                                                                                        }
                                                                                                        else {
                                                                                                            if ((((int)(this.vt)) == 16391)) {
                                                                                                                encoder.WritePointer(this.pdate);
                                                                                                            }
                                                                                                            else {
                                                                                                                if ((((int)(this.vt)) == 16392)) {
                                                                                                                    encoder.WritePointer(this.pbstrVal);
                                                                                                                }
                                                                                                                else {
                                                                                                                    if ((((int)(this.vt)) == 16397)) {
                                                                                                                        encoder.WritePointer(this.ppunkVal);
                                                                                                                    }
                                                                                                                    else {
                                                                                                                        if ((((int)(this.vt)) == 16393)) {
                                                                                                                            encoder.WritePointer(this.ppdispVal);
                                                                                                                        }
                                                                                                                        else {
                                                                                                                            if ((((int)(this.vt)) == 24576)) {
                                                                                                                                encoder.WritePointer(this.pparray);
                                                                                                                            }
                                                                                                                            else {
                                                                                                                                if ((((int)(this.vt)) == 16396)) {
                                                                                                                                    encoder.WritePointer(this.pvarVal);
                                                                                                                                }
                                                                                                                                else {
                                                                                                                                    if ((((int)(this.vt)) == 16)) {
                                                                                                                                        encoder.WriteValue(this.cVal);
                                                                                                                                    }
                                                                                                                                    else {
                                                                                                                                        if ((((int)(this.vt)) == 18)) {
                                                                                                                                            encoder.WriteValue(this.uiVal);
                                                                                                                                        }
                                                                                                                                        else {
                                                                                                                                            if ((((int)(this.vt)) == 19)) {
                                                                                                                                                encoder.WriteValue(this.ulVal);
                                                                                                                                            }
                                                                                                                                            else {
                                                                                                                                                if ((((int)(this.vt)) == 21)) {
                                                                                                                                                    encoder.WriteValue(this.ullVal);
                                                                                                                                                }
                                                                                                                                                else {
                                                                                                                                                    if ((((int)(this.vt)) == 22)) {
                                                                                                                                                        encoder.WriteValue(this.intVal);
                                                                                                                                                    }
                                                                                                                                                    else {
                                                                                                                                                        if ((((int)(this.vt)) == 23)) {
                                                                                                                                                            encoder.WriteValue(this.uintVal);
                                                                                                                                                        }
                                                                                                                                                        else {
                                                                                                                                                            if ((((int)(this.vt)) == 14)) {
                                                                                                                                                                encoder.WriteFixedStruct(this.decVal, Titanis.DceRpc.NdrAlignment._8Byte);
                                                                                                                                                            }
                                                                                                                                                            else {
                                                                                                                                                                if ((((int)(this.vt)) == 16400)) {
                                                                                                                                                                    encoder.WritePointer(this.pcVal);
                                                                                                                                                                }
                                                                                                                                                                else {
                                                                                                                                                                    if ((((int)(this.vt)) == 16402)) {
                                                                                                                                                                        encoder.WritePointer(this.puiVal);
                                                                                                                                                                    }
                                                                                                                                                                    else {
                                                                                                                                                                        if ((((int)(this.vt)) == 16403)) {
                                                                                                                                                                            encoder.WritePointer(this.pulVal);
                                                                                                                                                                        }
                                                                                                                                                                        else {
                                                                                                                                                                            if ((((int)(this.vt)) == 16405)) {
                                                                                                                                                                                encoder.WritePointer(this.pullVal);
                                                                                                                                                                            }
                                                                                                                                                                            else {
                                                                                                                                                                                if ((((int)(this.vt)) == 16406)) {
                                                                                                                                                                                    encoder.WritePointer(this.pintVal);
                                                                                                                                                                                }
                                                                                                                                                                                else {
                                                                                                                                                                                    if ((((int)(this.vt)) == 16407)) {
                                                                                                                                                                                        encoder.WritePointer(this.puintVal);
                                                                                                                                                                                    }
                                                                                                                                                                                    else {
                                                                                                                                                                                        if ((((int)(this.vt)) == 16398)) {
                                                                                                                                                                                            encoder.WritePointer(this.pdecVal);
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
            this.vt = decoder.ReadUInt32();
            //decoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.vt)) == 20)) {
                this.llVal = decoder.ReadInt64();
            }
            else {
                if ((((int)(this.vt)) == 3)) {
                    this.lVal = decoder.ReadInt32();
                }
                else {
                    if ((((int)(this.vt)) == 17)) {
                        this.bVal = decoder.ReadByte();
                    }
                    else {
                        if ((((int)(this.vt)) == 2)) {
                            this.iVal = decoder.ReadInt16();
                        }
                        else {
                            if ((((int)(this.vt)) == 4)) {
                                this.fltVal = decoder.ReadFloat();
                            }
                            else {
                                if ((((int)(this.vt)) == 5)) {
                                    this.dblVal = decoder.ReadDouble();
                                }
                                else {
                                    if ((((int)(this.vt)) == 11)) {
                                        this.boolVal = decoder.ReadInt16();
                                    }
                                    else {
                                        if ((((int)(this.vt)) == 10)) {
                                            this.scode = decoder.ReadInt32();
                                        }
                                        else {
                                            if ((((int)(this.vt)) == 6)) {
                                                this.cyVal = decoder.ReadFixedStruct<CURRENCY>(Titanis.DceRpc.NdrAlignment._8Byte);
                                            }
                                            else {
                                                if ((((int)(this.vt)) == 7)) {
                                                    this.date = decoder.ReadDouble();
                                                }
                                                else {
                                                    if ((((int)(this.vt)) == 8)) {
                                                        this.bstrVal = decoder.ReadUniquePointer<FLAGGED_WORD_BLOB>();
                                                    }
                                                    else {
                                                        if ((((int)(this.vt)) == 13)) {
                                                            this.punkVal = decoder.ReadInterfacePointer<IUnknown>();
                                                        }
                                                        else {
                                                            if ((((int)(this.vt)) == 9)) {
                                                                this.pdispVal = decoder.ReadInterfacePointer<IDispatch>();
                                                            }
                                                            else {
                                                                if ((((int)(this.vt)) == 8192)) {
                                                                    this.parray = decoder.ReadUniquePointer<_wireSAFEARRAY>();
                                                                }
                                                                else {
                                                                    if (((((int)(this.vt)) == 36) 
                                                                                && (((int)(this.vt)) == 16420))) {
                                                                        this.brecVal = decoder.ReadUniquePointer<wireBRECORDStr>();
                                                                    }
                                                                    else {
                                                                        if ((((int)(this.vt)) == 16401)) {
                                                                            this.pbVal = decoder.ReadUniquePointer<byte>();
                                                                        }
                                                                        else {
                                                                            if ((((int)(this.vt)) == 16386)) {
                                                                                this.piVal = decoder.ReadUniquePointer<short>();
                                                                            }
                                                                            else {
                                                                                if ((((int)(this.vt)) == 16387)) {
                                                                                    this.plVal = decoder.ReadUniquePointer<int>();
                                                                                }
                                                                                else {
                                                                                    if ((((int)(this.vt)) == 16404)) {
                                                                                        this.pllVal = decoder.ReadUniquePointer<long>();
                                                                                    }
                                                                                    else {
                                                                                        if ((((int)(this.vt)) == 16388)) {
                                                                                            this.pfltVal = decoder.ReadUniquePointer<float>();
                                                                                        }
                                                                                        else {
                                                                                            if ((((int)(this.vt)) == 16389)) {
                                                                                                this.pdblVal = decoder.ReadUniquePointer<double>();
                                                                                            }
                                                                                            else {
                                                                                                if ((((int)(this.vt)) == 16395)) {
                                                                                                    this.pboolVal = decoder.ReadUniquePointer<short>();
                                                                                                }
                                                                                                else {
                                                                                                    if ((((int)(this.vt)) == 16394)) {
                                                                                                        this.pscode = decoder.ReadUniquePointer<int>();
                                                                                                    }
                                                                                                    else {
                                                                                                        if ((((int)(this.vt)) == 16390)) {
                                                                                                            this.pcyVal = decoder.ReadUniquePointer<CURRENCY>();
                                                                                                        }
                                                                                                        else {
                                                                                                            if ((((int)(this.vt)) == 16391)) {
                                                                                                                this.pdate = decoder.ReadUniquePointer<double>();
                                                                                                            }
                                                                                                            else {
                                                                                                                if ((((int)(this.vt)) == 16392)) {
                                                                                                                    this.pbstrVal = decoder.ReadUniquePointer<RpcPointer<FLAGGED_WORD_BLOB>>();
                                                                                                                }
                                                                                                                else {
                                                                                                                    if ((((int)(this.vt)) == 16397)) {
                                                                                                                        this.ppunkVal = decoder.ReadUniquePointer<Titanis.DceRpc.TypedObjref<IUnknown>>();
                                                                                                                    }
                                                                                                                    else {
                                                                                                                        if ((((int)(this.vt)) == 16393)) {
                                                                                                                            this.ppdispVal = decoder.ReadUniquePointer<Titanis.DceRpc.TypedObjref<IDispatch>>();
                                                                                                                        }
                                                                                                                        else {
                                                                                                                            if ((((int)(this.vt)) == 24576)) {
                                                                                                                                this.pparray = decoder.ReadUniquePointer<RpcPointer<_wireSAFEARRAY>>();
                                                                                                                            }
                                                                                                                            else {
                                                                                                                                if ((((int)(this.vt)) == 16396)) {
                                                                                                                                    this.pvarVal = decoder.ReadUniquePointer<RpcPointer<wireVARIANTStr>>();
                                                                                                                                }
                                                                                                                                else {
                                                                                                                                    if ((((int)(this.vt)) == 16)) {
                                                                                                                                        this.cVal = decoder.ReadUnsignedChar();
                                                                                                                                    }
                                                                                                                                    else {
                                                                                                                                        if ((((int)(this.vt)) == 18)) {
                                                                                                                                            this.uiVal = decoder.ReadUInt16();
                                                                                                                                        }
                                                                                                                                        else {
                                                                                                                                            if ((((int)(this.vt)) == 19)) {
                                                                                                                                                this.ulVal = decoder.ReadUInt32();
                                                                                                                                            }
                                                                                                                                            else {
                                                                                                                                                if ((((int)(this.vt)) == 21)) {
                                                                                                                                                    this.ullVal = decoder.ReadUInt64();
                                                                                                                                                }
                                                                                                                                                else {
                                                                                                                                                    if ((((int)(this.vt)) == 22)) {
                                                                                                                                                        this.intVal = decoder.ReadInt32();
                                                                                                                                                    }
                                                                                                                                                    else {
                                                                                                                                                        if ((((int)(this.vt)) == 23)) {
                                                                                                                                                            this.uintVal = decoder.ReadUInt32();
                                                                                                                                                        }
                                                                                                                                                        else {
                                                                                                                                                            if ((((int)(this.vt)) == 14)) {
                                                                                                                                                                this.decVal = decoder.ReadFixedStruct<DECIMAL>(Titanis.DceRpc.NdrAlignment._8Byte);
                                                                                                                                                            }
                                                                                                                                                            else {
                                                                                                                                                                if ((((int)(this.vt)) == 16400)) {
                                                                                                                                                                    this.pcVal = decoder.ReadUniquePointer<byte>();
                                                                                                                                                                }
                                                                                                                                                                else {
                                                                                                                                                                    if ((((int)(this.vt)) == 16402)) {
                                                                                                                                                                        this.puiVal = decoder.ReadUniquePointer<ushort>();
                                                                                                                                                                    }
                                                                                                                                                                    else {
                                                                                                                                                                        if ((((int)(this.vt)) == 16403)) {
                                                                                                                                                                            this.pulVal = decoder.ReadUniquePointer<uint>();
                                                                                                                                                                        }
                                                                                                                                                                        else {
                                                                                                                                                                            if ((((int)(this.vt)) == 16405)) {
                                                                                                                                                                                this.pullVal = decoder.ReadUniquePointer<ulong>();
                                                                                                                                                                            }
                                                                                                                                                                            else {
                                                                                                                                                                                if ((((int)(this.vt)) == 16406)) {
                                                                                                                                                                                    this.pintVal = decoder.ReadUniquePointer<int>();
                                                                                                                                                                                }
                                                                                                                                                                                else {
                                                                                                                                                                                    if ((((int)(this.vt)) == 16407)) {
                                                                                                                                                                                        this.puintVal = decoder.ReadUniquePointer<uint>();
                                                                                                                                                                                    }
                                                                                                                                                                                    else {
                                                                                                                                                                                        if ((((int)(this.vt)) == 16398)) {
                                                                                                                                                                                            this.pdecVal = decoder.ReadUniquePointer<DECIMAL>();
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
            if ((((int)(this.vt)) == 20)) {
            }
            else {
                if ((((int)(this.vt)) == 3)) {
                }
                else {
                    if ((((int)(this.vt)) == 17)) {
                    }
                    else {
                        if ((((int)(this.vt)) == 2)) {
                        }
                        else {
                            if ((((int)(this.vt)) == 4)) {
                            }
                            else {
                                if ((((int)(this.vt)) == 5)) {
                                }
                                else {
                                    if ((((int)(this.vt)) == 11)) {
                                    }
                                    else {
                                        if ((((int)(this.vt)) == 10)) {
                                        }
                                        else {
                                            if ((((int)(this.vt)) == 6)) {
                                                encoder.WriteStructDeferral(this.cyVal);
                                            }
                                            else {
                                                if ((((int)(this.vt)) == 7)) {
                                                }
                                                else {
                                                    if ((((int)(this.vt)) == 8)) {
                                                        if ((null != this.bstrVal)) {
                                                            encoder.WriteConformantStruct(this.bstrVal.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                                            encoder.WriteStructDeferral(this.bstrVal.value);
                                                        }
                                                    }
                                                    else {
                                                        if ((((int)(this.vt)) == 13)) {
                                                            encoder.WriteInterfacePointerBody(this.punkVal);
                                                        }
                                                        else {
                                                            if ((((int)(this.vt)) == 9)) {
                                                                encoder.WriteInterfacePointerBody(this.pdispVal);
                                                            }
                                                            else {
                                                                if ((((int)(this.vt)) == 8192)) {
                                                                    if ((null != this.parray)) {
                                                                        encoder.WriteConformantStruct(this.parray.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                        encoder.WriteStructDeferral(this.parray.value);
                                                                    }
                                                                }
                                                                else {
                                                                    if (((((int)(this.vt)) == 36) 
                                                                                && (((int)(this.vt)) == 16420))) {
                                                                        if ((null != this.brecVal)) {
                                                                            encoder.WriteFixedStruct(this.brecVal.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                            encoder.WriteStructDeferral(this.brecVal.value);
                                                                        }
                                                                    }
                                                                    else {
                                                                        if ((((int)(this.vt)) == 16401)) {
                                                                            if ((null != this.pbVal)) {
                                                                                encoder.WriteValue(this.pbVal.value);
                                                                            }
                                                                        }
                                                                        else {
                                                                            if ((((int)(this.vt)) == 16386)) {
                                                                                if ((null != this.piVal)) {
                                                                                    encoder.WriteValue(this.piVal.value);
                                                                                }
                                                                            }
                                                                            else {
                                                                                if ((((int)(this.vt)) == 16387)) {
                                                                                    if ((null != this.plVal)) {
                                                                                        encoder.WriteValue(this.plVal.value);
                                                                                    }
                                                                                }
                                                                                else {
                                                                                    if ((((int)(this.vt)) == 16404)) {
                                                                                        if ((null != this.pllVal)) {
                                                                                            encoder.WriteValue(this.pllVal.value);
                                                                                        }
                                                                                    }
                                                                                    else {
                                                                                        if ((((int)(this.vt)) == 16388)) {
                                                                                            if ((null != this.pfltVal)) {
                                                                                                encoder.WriteValue(this.pfltVal.value);
                                                                                            }
                                                                                        }
                                                                                        else {
                                                                                            if ((((int)(this.vt)) == 16389)) {
                                                                                                if ((null != this.pdblVal)) {
                                                                                                    encoder.WriteValue(this.pdblVal.value);
                                                                                                }
                                                                                            }
                                                                                            else {
                                                                                                if ((((int)(this.vt)) == 16395)) {
                                                                                                    if ((null != this.pboolVal)) {
                                                                                                        encoder.WriteValue(this.pboolVal.value);
                                                                                                    }
                                                                                                }
                                                                                                else {
                                                                                                    if ((((int)(this.vt)) == 16394)) {
                                                                                                        if ((null != this.pscode)) {
                                                                                                            encoder.WriteValue(this.pscode.value);
                                                                                                        }
                                                                                                    }
                                                                                                    else {
                                                                                                        if ((((int)(this.vt)) == 16390)) {
                                                                                                            if ((null != this.pcyVal)) {
                                                                                                                encoder.WriteFixedStruct(this.pcyVal.value, Titanis.DceRpc.NdrAlignment._8Byte);
                                                                                                                encoder.WriteStructDeferral(this.pcyVal.value);
                                                                                                            }
                                                                                                        }
                                                                                                        else {
                                                                                                            if ((((int)(this.vt)) == 16391)) {
                                                                                                                if ((null != this.pdate)) {
                                                                                                                    encoder.WriteValue(this.pdate.value);
                                                                                                                }
                                                                                                            }
                                                                                                            else {
                                                                                                                if ((((int)(this.vt)) == 16392)) {
                                                                                                                    if ((null != this.pbstrVal)) {
                                                                                                                        encoder.WritePointer(this.pbstrVal.value);
                                                                                                                        if ((null != this.pbstrVal.value)) {
                                                                                                                            encoder.WriteConformantStruct(this.pbstrVal.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                                                                                                            encoder.WriteStructDeferral(this.pbstrVal.value.value);
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                                else {
                                                                                                                    if ((((int)(this.vt)) == 16397)) {
                                                                                                                        if ((null != this.ppunkVal)) {
                                                                                                                            encoder.WriteInterfacePointer(this.ppunkVal.value);
                                                                                                                            encoder.WriteInterfacePointerBody(this.ppunkVal.value);
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else {
                                                                                                                        if ((((int)(this.vt)) == 16393)) {
                                                                                                                            if ((null != this.ppdispVal)) {
                                                                                                                                encoder.WriteInterfacePointer(this.ppdispVal.value);
                                                                                                                                encoder.WriteInterfacePointerBody(this.ppdispVal.value);
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else {
                                                                                                                            if ((((int)(this.vt)) == 24576)) {
                                                                                                                                if ((null != this.pparray)) {
                                                                                                                                    encoder.WritePointer(this.pparray.value);
                                                                                                                                    if ((null != this.pparray.value)) {
                                                                                                                                        encoder.WriteConformantStruct(this.pparray.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                                                                        encoder.WriteStructDeferral(this.pparray.value.value);
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                            else {
                                                                                                                                if ((((int)(this.vt)) == 16396)) {
                                                                                                                                    if ((null != this.pvarVal)) {
                                                                                                                                        encoder.WritePointer(this.pvarVal.value);
                                                                                                                                        if ((null != this.pvarVal.value)) {
                                                                                                                                            encoder.WriteFixedStruct(this.pvarVal.value.value, Titanis.DceRpc.NdrAlignment._8Byte);
                                                                                                                                            encoder.WriteStructDeferral(this.pvarVal.value.value);
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                else {
                                                                                                                                    if ((((int)(this.vt)) == 16)) {
                                                                                                                                    }
                                                                                                                                    else {
                                                                                                                                        if ((((int)(this.vt)) == 18)) {
                                                                                                                                        }
                                                                                                                                        else {
                                                                                                                                            if ((((int)(this.vt)) == 19)) {
                                                                                                                                            }
                                                                                                                                            else {
                                                                                                                                                if ((((int)(this.vt)) == 21)) {
                                                                                                                                                }
                                                                                                                                                else {
                                                                                                                                                    if ((((int)(this.vt)) == 22)) {
                                                                                                                                                    }
                                                                                                                                                    else {
                                                                                                                                                        if ((((int)(this.vt)) == 23)) {
                                                                                                                                                        }
                                                                                                                                                        else {
                                                                                                                                                            if ((((int)(this.vt)) == 14)) {
                                                                                                                                                                encoder.WriteStructDeferral(this.decVal);
                                                                                                                                                            }
                                                                                                                                                            else {
                                                                                                                                                                if ((((int)(this.vt)) == 16400)) {
                                                                                                                                                                    if ((null != this.pcVal)) {
                                                                                                                                                                        encoder.WriteValue(this.pcVal.value);
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                                else {
                                                                                                                                                                    if ((((int)(this.vt)) == 16402)) {
                                                                                                                                                                        if ((null != this.puiVal)) {
                                                                                                                                                                            encoder.WriteValue(this.puiVal.value);
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                    else {
                                                                                                                                                                        if ((((int)(this.vt)) == 16403)) {
                                                                                                                                                                            if ((null != this.pulVal)) {
                                                                                                                                                                                encoder.WriteValue(this.pulVal.value);
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                        else {
                                                                                                                                                                            if ((((int)(this.vt)) == 16405)) {
                                                                                                                                                                                if ((null != this.pullVal)) {
                                                                                                                                                                                    encoder.WriteValue(this.pullVal.value);
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            else {
                                                                                                                                                                                if ((((int)(this.vt)) == 16406)) {
                                                                                                                                                                                    if ((null != this.pintVal)) {
                                                                                                                                                                                        encoder.WriteValue(this.pintVal.value);
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                else {
                                                                                                                                                                                    if ((((int)(this.vt)) == 16407)) {
                                                                                                                                                                                        if ((null != this.puintVal)) {
                                                                                                                                                                                            encoder.WriteValue(this.puintVal.value);
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    else {
                                                                                                                                                                                        if ((((int)(this.vt)) == 16398)) {
                                                                                                                                                                                            if ((null != this.pdecVal)) {
                                                                                                                                                                                                encoder.WriteFixedStruct(this.pdecVal.value, Titanis.DceRpc.NdrAlignment._8Byte);
                                                                                                                                                                                                encoder.WriteStructDeferral(this.pdecVal.value);
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
            if ((((int)(this.vt)) == 20)) {
            }
            else {
                if ((((int)(this.vt)) == 3)) {
                }
                else {
                    if ((((int)(this.vt)) == 17)) {
                    }
                    else {
                        if ((((int)(this.vt)) == 2)) {
                        }
                        else {
                            if ((((int)(this.vt)) == 4)) {
                            }
                            else {
                                if ((((int)(this.vt)) == 5)) {
                                }
                                else {
                                    if ((((int)(this.vt)) == 11)) {
                                    }
                                    else {
                                        if ((((int)(this.vt)) == 10)) {
                                        }
                                        else {
                                            if ((((int)(this.vt)) == 6)) {
                                                decoder.ReadStructDeferral<CURRENCY>(ref this.cyVal);
                                            }
                                            else {
                                                if ((((int)(this.vt)) == 7)) {
                                                }
                                                else {
                                                    if ((((int)(this.vt)) == 8)) {
                                                        if ((null != this.bstrVal)) {
                                                            this.bstrVal.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                                                            decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref this.bstrVal.value);
                                                        }
                                                    }
                                                    else {
                                                        if ((((int)(this.vt)) == 13)) {
                                                            decoder.ReadInterfacePointer(this.punkVal);
                                                        }
                                                        else {
                                                            if ((((int)(this.vt)) == 9)) {
                                                                decoder.ReadInterfacePointer(this.pdispVal);
                                                            }
                                                            else {
                                                                if ((((int)(this.vt)) == 8192)) {
                                                                    if ((null != this.parray)) {
                                                                        this.parray.value = decoder.ReadConformantStruct<_wireSAFEARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                        decoder.ReadStructDeferral<_wireSAFEARRAY>(ref this.parray.value);
                                                                    }
                                                                }
                                                                else {
                                                                    if (((((int)(this.vt)) == 36) 
                                                                                && (((int)(this.vt)) == 16420))) {
                                                                        if ((null != this.brecVal)) {
                                                                            this.brecVal.value = decoder.ReadFixedStruct<wireBRECORDStr>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                            decoder.ReadStructDeferral<wireBRECORDStr>(ref this.brecVal.value);
                                                                        }
                                                                    }
                                                                    else {
                                                                        if ((((int)(this.vt)) == 16401)) {
                                                                            if ((null != this.pbVal)) {
                                                                                this.pbVal.value = decoder.ReadByte();
                                                                            }
                                                                        }
                                                                        else {
                                                                            if ((((int)(this.vt)) == 16386)) {
                                                                                if ((null != this.piVal)) {
                                                                                    this.piVal.value = decoder.ReadInt16();
                                                                                }
                                                                            }
                                                                            else {
                                                                                if ((((int)(this.vt)) == 16387)) {
                                                                                    if ((null != this.plVal)) {
                                                                                        this.plVal.value = decoder.ReadInt32();
                                                                                    }
                                                                                }
                                                                                else {
                                                                                    if ((((int)(this.vt)) == 16404)) {
                                                                                        if ((null != this.pllVal)) {
                                                                                            this.pllVal.value = decoder.ReadInt64();
                                                                                        }
                                                                                    }
                                                                                    else {
                                                                                        if ((((int)(this.vt)) == 16388)) {
                                                                                            if ((null != this.pfltVal)) {
                                                                                                this.pfltVal.value = decoder.ReadFloat();
                                                                                            }
                                                                                        }
                                                                                        else {
                                                                                            if ((((int)(this.vt)) == 16389)) {
                                                                                                if ((null != this.pdblVal)) {
                                                                                                    this.pdblVal.value = decoder.ReadDouble();
                                                                                                }
                                                                                            }
                                                                                            else {
                                                                                                if ((((int)(this.vt)) == 16395)) {
                                                                                                    if ((null != this.pboolVal)) {
                                                                                                        this.pboolVal.value = decoder.ReadInt16();
                                                                                                    }
                                                                                                }
                                                                                                else {
                                                                                                    if ((((int)(this.vt)) == 16394)) {
                                                                                                        if ((null != this.pscode)) {
                                                                                                            this.pscode.value = decoder.ReadInt32();
                                                                                                        }
                                                                                                    }
                                                                                                    else {
                                                                                                        if ((((int)(this.vt)) == 16390)) {
                                                                                                            if ((null != this.pcyVal)) {
                                                                                                                this.pcyVal.value = decoder.ReadFixedStruct<CURRENCY>(Titanis.DceRpc.NdrAlignment._8Byte);
                                                                                                                decoder.ReadStructDeferral<CURRENCY>(ref this.pcyVal.value);
                                                                                                            }
                                                                                                        }
                                                                                                        else {
                                                                                                            if ((((int)(this.vt)) == 16391)) {
                                                                                                                if ((null != this.pdate)) {
                                                                                                                    this.pdate.value = decoder.ReadDouble();
                                                                                                                }
                                                                                                            }
                                                                                                            else {
                                                                                                                if ((((int)(this.vt)) == 16392)) {
                                                                                                                    if ((null != this.pbstrVal)) {
                                                                                                                        this.pbstrVal.value = decoder.ReadUniquePointer<FLAGGED_WORD_BLOB>();
                                                                                                                        if ((null != this.pbstrVal.value)) {
                                                                                                                            this.pbstrVal.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                                                                                                                            decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref this.pbstrVal.value.value);
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                                else {
                                                                                                                    if ((((int)(this.vt)) == 16397)) {
                                                                                                                        if ((null != this.ppunkVal)) {
                                                                                                                            this.ppunkVal.value = decoder.ReadInterfacePointer<IUnknown>();
                                                                                                                            decoder.ReadInterfacePointer(this.ppunkVal.value);
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else {
                                                                                                                        if ((((int)(this.vt)) == 16393)) {
                                                                                                                            if ((null != this.ppdispVal)) {
                                                                                                                                this.ppdispVal.value = decoder.ReadInterfacePointer<IDispatch>();
                                                                                                                                decoder.ReadInterfacePointer(this.ppdispVal.value);
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else {
                                                                                                                            if ((((int)(this.vt)) == 24576)) {
                                                                                                                                if ((null != this.pparray)) {
                                                                                                                                    this.pparray.value = decoder.ReadUniquePointer<_wireSAFEARRAY>();
                                                                                                                                    if ((null != this.pparray.value)) {
                                                                                                                                        this.pparray.value.value = decoder.ReadConformantStruct<_wireSAFEARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                                                                        decoder.ReadStructDeferral<_wireSAFEARRAY>(ref this.pparray.value.value);
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                            else {
                                                                                                                                if ((((int)(this.vt)) == 16396)) {
                                                                                                                                    if ((null != this.pvarVal)) {
                                                                                                                                        this.pvarVal.value = decoder.ReadUniquePointer<wireVARIANTStr>();
                                                                                                                                        if ((null != this.pvarVal.value)) {
                                                                                                                                            this.pvarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                                                                                                                                            decoder.ReadStructDeferral<wireVARIANTStr>(ref this.pvarVal.value.value);
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                else {
                                                                                                                                    if ((((int)(this.vt)) == 16)) {
                                                                                                                                    }
                                                                                                                                    else {
                                                                                                                                        if ((((int)(this.vt)) == 18)) {
                                                                                                                                        }
                                                                                                                                        else {
                                                                                                                                            if ((((int)(this.vt)) == 19)) {
                                                                                                                                            }
                                                                                                                                            else {
                                                                                                                                                if ((((int)(this.vt)) == 21)) {
                                                                                                                                                }
                                                                                                                                                else {
                                                                                                                                                    if ((((int)(this.vt)) == 22)) {
                                                                                                                                                    }
                                                                                                                                                    else {
                                                                                                                                                        if ((((int)(this.vt)) == 23)) {
                                                                                                                                                        }
                                                                                                                                                        else {
                                                                                                                                                            if ((((int)(this.vt)) == 14)) {
                                                                                                                                                                decoder.ReadStructDeferral<DECIMAL>(ref this.decVal);
                                                                                                                                                            }
                                                                                                                                                            else {
                                                                                                                                                                if ((((int)(this.vt)) == 16400)) {
                                                                                                                                                                    if ((null != this.pcVal)) {
                                                                                                                                                                        this.pcVal.value = decoder.ReadUnsignedChar();
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                                else {
                                                                                                                                                                    if ((((int)(this.vt)) == 16402)) {
                                                                                                                                                                        if ((null != this.puiVal)) {
                                                                                                                                                                            this.puiVal.value = decoder.ReadUInt16();
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                    else {
                                                                                                                                                                        if ((((int)(this.vt)) == 16403)) {
                                                                                                                                                                            if ((null != this.pulVal)) {
                                                                                                                                                                                this.pulVal.value = decoder.ReadUInt32();
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                        else {
                                                                                                                                                                            if ((((int)(this.vt)) == 16405)) {
                                                                                                                                                                                if ((null != this.pullVal)) {
                                                                                                                                                                                    this.pullVal.value = decoder.ReadUInt64();
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            else {
                                                                                                                                                                                if ((((int)(this.vt)) == 16406)) {
                                                                                                                                                                                    if ((null != this.pintVal)) {
                                                                                                                                                                                        this.pintVal.value = decoder.ReadInt32();
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                else {
                                                                                                                                                                                    if ((((int)(this.vt)) == 16407)) {
                                                                                                                                                                                        if ((null != this.puintVal)) {
                                                                                                                                                                                            this.puintVal.value = decoder.ReadUInt32();
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    else {
                                                                                                                                                                                        if ((((int)(this.vt)) == 16398)) {
                                                                                                                                                                                            if ((null != this.pdecVal)) {
                                                                                                                                                                                                this.pdecVal.value = decoder.ReadFixedStruct<DECIMAL>(Titanis.DceRpc.NdrAlignment._8Byte);
                                                                                                                                                                                                decoder.ReadStructDeferral<DECIMAL>(ref this.pdecVal.value);
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
        public Titanis.DceRpc.TypedObjref<IUnknown> punkVal;
        public Titanis.DceRpc.TypedObjref<IDispatch> pdispVal;
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
        public RpcPointer<Titanis.DceRpc.TypedObjref<IUnknown>> ppunkVal;
        public RpcPointer<Titanis.DceRpc.TypedObjref<IDispatch>> ppdispVal;
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
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct wireVARIANTStr : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.clSize);
            encoder.WriteValue(this.rpcReserved);
            encoder.WriteValue(this.vt);
            encoder.WriteValue(this.wReserved1);
            encoder.WriteValue(this.wReserved2);
            encoder.WriteValue(this.wReserved3);
            encoder.WriteUnion(this._varUnion);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.clSize = decoder.ReadUInt32();
            this.rpcReserved = decoder.ReadUInt32();
            this.vt = decoder.ReadUInt16();
            this.wReserved1 = decoder.ReadUInt16();
            this.wReserved2 = decoder.ReadUInt16();
            this.wReserved3 = decoder.ReadUInt16();
            this._varUnion = decoder.ReadUnion<Unnamed_1>();
        }
        public uint clSize;
        public uint rpcReserved;
        public ushort vt;
        public ushort wReserved1;
        public ushort wReserved2;
        public ushort wReserved3;
        public Unnamed_1 _varUnion;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this._varUnion);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<Unnamed_1>(ref this._varUnion);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SAFEARR_VARIANT : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Size);
            for (int i = 0; (i < this.aVariant.value.Length); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = this.aVariant.value[i];
                encoder.WritePointer(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Size = decoder.ReadUInt32();
            this.aVariant = new RpcPointer<RpcPointer<wireVARIANTStr>[]>();
            for (int i = 0; (i < this.aVariant.value.Length); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = this.aVariant.value[i];
                elem_0 = decoder.ReadUniquePointer<wireVARIANTStr>();
                this.aVariant.value[i] = elem_0;
            }
        }
        public uint Size;
        public RpcPointer<RpcPointer<wireVARIANTStr>[]> aVariant;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.aVariant.value.Length); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = this.aVariant.value[i];
                if ((null != elem_0)) {
                    encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._8Byte);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.aVariant.value.Length); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = this.aVariant.value[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                    decoder.ReadStructDeferral<wireVARIANTStr>(ref elem_0.value);
                }
                this.aVariant.value[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SAFEARR_BRECORD : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Size);
            for (int i = 0; (i < this.aRecord.value.Length); i++
            ) {
                RpcPointer<wireBRECORDStr> elem_0 = this.aRecord.value[i];
                encoder.WritePointer(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Size = decoder.ReadUInt32();
            this.aRecord = new RpcPointer<RpcPointer<wireBRECORDStr>[]>();
            for (int i = 0; (i < this.aRecord.value.Length); i++
            ) {
                RpcPointer<wireBRECORDStr> elem_0 = this.aRecord.value[i];
                elem_0 = decoder.ReadUniquePointer<wireBRECORDStr>();
                this.aRecord.value[i] = elem_0;
            }
        }
        public uint Size;
        public RpcPointer<RpcPointer<wireBRECORDStr>[]> aRecord;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.aRecord.value.Length); i++
            ) {
                RpcPointer<wireBRECORDStr> elem_0 = this.aRecord.value[i];
                if ((null != elem_0)) {
                    encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.aRecord.value.Length); i++
            ) {
                RpcPointer<wireBRECORDStr> elem_0 = this.aRecord.value[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadFixedStruct<wireBRECORDStr>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<wireBRECORDStr>(ref elem_0.value);
                }
                this.aRecord.value[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SAFEARR_HAVEIID : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Size);
            for (int i = 0; (i < this.apUnknown.value.Length); i++
            ) {
                Titanis.DceRpc.TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
                encoder.WriteInterfacePointer(elem_0);
            }
            encoder.WriteValue(this.iid);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Size = decoder.ReadUInt32();
            this.apUnknown = new RpcPointer<Titanis.DceRpc.TypedObjref<IUnknown>[]>();
            for (int i = 0; (i < this.apUnknown.value.Length); i++
            ) {
                Titanis.DceRpc.TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
                elem_0 = decoder.ReadInterfacePointer<IUnknown>();
                this.apUnknown.value[i] = elem_0;
            }
            this.iid = decoder.ReadUuid();
        }
        public uint Size;
        public RpcPointer<Titanis.DceRpc.TypedObjref<IUnknown>[]> apUnknown;
        public System.Guid iid;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.apUnknown.value.Length); i++
            ) {
                Titanis.DceRpc.TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
                encoder.WriteInterfacePointerBody(elem_0);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.apUnknown.value.Length); i++
            ) {
                Titanis.DceRpc.TypedObjref<IUnknown> elem_0 = this.apUnknown.value[i];
                decoder.ReadInterfacePointer(elem_0);
                this.apUnknown.value[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct BYTE_SIZEDARR : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.clSize);
            encoder.WritePointer(this.pData);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.clSize = decoder.ReadUInt32();
            this.pData = decoder.ReadUniquePointer<byte[]>();
        }
        public uint clSize;
        public RpcPointer<byte[]> pData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pData)) {
                encoder.WriteArrayHeader(this.pData.value);
                for (int i = 0; (i < this.pData.value.Length); i++
                ) {
                    byte elem_0 = this.pData.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pData)) {
                this.pData.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.pData.value.Length); i++
                ) {
                    byte elem_0 = this.pData.value[i];
                    elem_0 = decoder.ReadByte();
                    this.pData.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct WORD_SIZEDARR : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.clSize);
            encoder.WritePointer(this.pData);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.clSize = decoder.ReadUInt32();
            this.pData = decoder.ReadUniquePointer<ushort[]>();
        }
        public uint clSize;
        public RpcPointer<ushort[]> pData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pData)) {
                encoder.WriteArrayHeader(this.pData.value);
                for (int i = 0; (i < this.pData.value.Length); i++
                ) {
                    ushort elem_0 = this.pData.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pData)) {
                this.pData.value = decoder.ReadArrayHeader<ushort>();
                for (int i = 0; (i < this.pData.value.Length); i++
                ) {
                    ushort elem_0 = this.pData.value[i];
                    elem_0 = decoder.ReadUInt16();
                    this.pData.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DWORD_SIZEDARR : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.clSize);
            encoder.WritePointer(this.pData);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.clSize = decoder.ReadUInt32();
            this.pData = decoder.ReadUniquePointer<uint[]>();
        }
        public uint clSize;
        public RpcPointer<uint[]> pData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pData)) {
                encoder.WriteArrayHeader(this.pData.value);
                for (int i = 0; (i < this.pData.value.Length); i++
                ) {
                    uint elem_0 = this.pData.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pData)) {
                this.pData.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < this.pData.value.Length); i++
                ) {
                    uint elem_0 = this.pData.value[i];
                    elem_0 = decoder.ReadUInt32();
                    this.pData.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct HYPER_SIZEDARR : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.clSize);
            encoder.WritePointer(this.pData);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.clSize = decoder.ReadUInt32();
            this.pData = decoder.ReadUniquePointer<long[]>();
        }
        public uint clSize;
        public RpcPointer<long[]> pData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pData)) {
                encoder.WriteArrayHeader(this.pData.value);
                for (int i = 0; (i < this.pData.value.Length); i++
                ) {
                    long elem_0 = this.pData.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pData)) {
                this.pData.value = decoder.ReadArrayHeader<long>();
                for (int i = 0; (i < this.pData.value.Length); i++
                ) {
                    long elem_0 = this.pData.value[i];
                    elem_0 = decoder.ReadInt64();
                    this.pData.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SAFEARRAYUNION : Titanis.DceRpc.IRpcFixedStruct {
        public uint sfType;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.sfType);
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.sfType)) == 8)) {
                encoder.WriteFixedStruct(this.BstrStr, Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.sfType)) == 13)) {
                    encoder.WriteFixedStruct(this.UnknownStr, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.sfType)) == 9)) {
                        encoder.WriteFixedStruct(this.DispatchStr, Titanis.DceRpc.NdrAlignment.NativePtr);
                    }
                    else {
                        if ((((int)(this.sfType)) == 12)) {
                            encoder.WriteFixedStruct(this.VariantStr, Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.sfType)) == 36)) {
                                encoder.WriteFixedStruct(this.RecordStr, Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                            else {
                                if ((((int)(this.sfType)) == 32781)) {
                                    encoder.WriteFixedStruct(this.HaveIidStr, Titanis.DceRpc.NdrAlignment.NativePtr);
                                }
                                else {
                                    if ((((int)(this.sfType)) == 16)) {
                                        encoder.WriteFixedStruct(this.ByteStr, Titanis.DceRpc.NdrAlignment.NativePtr);
                                    }
                                    else {
                                        if ((((int)(this.sfType)) == 2)) {
                                            encoder.WriteFixedStruct(this.WordStr, Titanis.DceRpc.NdrAlignment.NativePtr);
                                        }
                                        else {
                                            if ((((int)(this.sfType)) == 3)) {
                                                encoder.WriteFixedStruct(this.LongStr, Titanis.DceRpc.NdrAlignment.NativePtr);
                                            }
                                            else {
                                                if ((((int)(this.sfType)) == 20)) {
                                                    encoder.WriteFixedStruct(this.HyperStr, Titanis.DceRpc.NdrAlignment.NativePtr);
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
            this.sfType = decoder.ReadUInt32();
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.sfType)) == 8)) {
                this.BstrStr = decoder.ReadFixedStruct<SAFEARR_BSTR>(Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.sfType)) == 13)) {
                    this.UnknownStr = decoder.ReadFixedStruct<SAFEARR_UNKNOWN>(Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.sfType)) == 9)) {
                        this.DispatchStr = decoder.ReadFixedStruct<SAFEARR_DISPATCH>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    }
                    else {
                        if ((((int)(this.sfType)) == 12)) {
                            this.VariantStr = decoder.ReadFixedStruct<SAFEARR_VARIANT>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.sfType)) == 36)) {
                                this.RecordStr = decoder.ReadFixedStruct<SAFEARR_BRECORD>(Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                            else {
                                if ((((int)(this.sfType)) == 32781)) {
                                    this.HaveIidStr = decoder.ReadFixedStruct<SAFEARR_HAVEIID>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                }
                                else {
                                    if ((((int)(this.sfType)) == 16)) {
                                        this.ByteStr = decoder.ReadFixedStruct<BYTE_SIZEDARR>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                    }
                                    else {
                                        if ((((int)(this.sfType)) == 2)) {
                                            this.WordStr = decoder.ReadFixedStruct<WORD_SIZEDARR>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                        }
                                        else {
                                            if ((((int)(this.sfType)) == 3)) {
                                                this.LongStr = decoder.ReadFixedStruct<DWORD_SIZEDARR>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                            }
                                            else {
                                                if ((((int)(this.sfType)) == 20)) {
                                                    this.HyperStr = decoder.ReadFixedStruct<HYPER_SIZEDARR>(Titanis.DceRpc.NdrAlignment.NativePtr);
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
            if ((((int)(this.sfType)) == 8)) {
                encoder.WriteStructDeferral(this.BstrStr);
            }
            else {
                if ((((int)(this.sfType)) == 13)) {
                    encoder.WriteStructDeferral(this.UnknownStr);
                }
                else {
                    if ((((int)(this.sfType)) == 9)) {
                        encoder.WriteStructDeferral(this.DispatchStr);
                    }
                    else {
                        if ((((int)(this.sfType)) == 12)) {
                            encoder.WriteStructDeferral(this.VariantStr);
                        }
                        else {
                            if ((((int)(this.sfType)) == 36)) {
                                encoder.WriteStructDeferral(this.RecordStr);
                            }
                            else {
                                if ((((int)(this.sfType)) == 32781)) {
                                    encoder.WriteStructDeferral(this.HaveIidStr);
                                }
                                else {
                                    if ((((int)(this.sfType)) == 16)) {
                                        encoder.WriteStructDeferral(this.ByteStr);
                                    }
                                    else {
                                        if ((((int)(this.sfType)) == 2)) {
                                            encoder.WriteStructDeferral(this.WordStr);
                                        }
                                        else {
                                            if ((((int)(this.sfType)) == 3)) {
                                                encoder.WriteStructDeferral(this.LongStr);
                                            }
                                            else {
                                                if ((((int)(this.sfType)) == 20)) {
                                                    encoder.WriteStructDeferral(this.HyperStr);
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
            if ((((int)(this.sfType)) == 8)) {
                decoder.ReadStructDeferral<SAFEARR_BSTR>(ref this.BstrStr);
            }
            else {
                if ((((int)(this.sfType)) == 13)) {
                    decoder.ReadStructDeferral<SAFEARR_UNKNOWN>(ref this.UnknownStr);
                }
                else {
                    if ((((int)(this.sfType)) == 9)) {
                        decoder.ReadStructDeferral<SAFEARR_DISPATCH>(ref this.DispatchStr);
                    }
                    else {
                        if ((((int)(this.sfType)) == 12)) {
                            decoder.ReadStructDeferral<SAFEARR_VARIANT>(ref this.VariantStr);
                        }
                        else {
                            if ((((int)(this.sfType)) == 36)) {
                                decoder.ReadStructDeferral<SAFEARR_BRECORD>(ref this.RecordStr);
                            }
                            else {
                                if ((((int)(this.sfType)) == 32781)) {
                                    decoder.ReadStructDeferral<SAFEARR_HAVEIID>(ref this.HaveIidStr);
                                }
                                else {
                                    if ((((int)(this.sfType)) == 16)) {
                                        decoder.ReadStructDeferral<BYTE_SIZEDARR>(ref this.ByteStr);
                                    }
                                    else {
                                        if ((((int)(this.sfType)) == 2)) {
                                            decoder.ReadStructDeferral<WORD_SIZEDARR>(ref this.WordStr);
                                        }
                                        else {
                                            if ((((int)(this.sfType)) == 3)) {
                                                decoder.ReadStructDeferral<DWORD_SIZEDARR>(ref this.LongStr);
                                            }
                                            else {
                                                if ((((int)(this.sfType)) == 20)) {
                                                    decoder.ReadStructDeferral<HYPER_SIZEDARR>(ref this.HyperStr);
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
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct _wireSAFEARRAY : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cDims);
            encoder.WriteValue(this.fFeatures);
            encoder.WriteValue(this.cbElements);
            encoder.WriteValue(this.cLocks);
            encoder.WriteUnion(this.uArrayStructs);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cDims = decoder.ReadUInt16();
            this.fFeatures = decoder.ReadUInt16();
            this.cbElements = decoder.ReadUInt32();
            this.cLocks = decoder.ReadUInt32();
            this.uArrayStructs = decoder.ReadUnion<SAFEARRAYUNION>();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.rgsabound);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.rgsabound = decoder.ReadArrayHeader<SAFEARRAYBOUND>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.rgsabound.Length); i++
            ) {
                SAFEARRAYBOUND elem_0 = this.rgsabound[i];
                encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.rgsabound.Length); i++
            ) {
                SAFEARRAYBOUND elem_0 = this.rgsabound[i];
                elem_0 = decoder.ReadFixedStruct<SAFEARRAYBOUND>(Titanis.DceRpc.NdrAlignment._4Byte);
                this.rgsabound[i] = elem_0;
            }
        }
        public ushort cDims;
        public ushort fFeatures;
        public uint cbElements;
        public uint cLocks;
        public SAFEARRAYUNION uArrayStructs;
        public SAFEARRAYBOUND[] rgsabound;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.uArrayStructs);
            for (int i = 0; (i < this.rgsabound.Length); i++
            ) {
                SAFEARRAYBOUND elem_0 = this.rgsabound[i];
                encoder.WriteStructDeferral(elem_0);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SAFEARRAYUNION>(ref this.uArrayStructs);
            for (int i = 0; (i < this.rgsabound.Length); i++
            ) {
                SAFEARRAYBOUND elem_0 = this.rgsabound[i];
                decoder.ReadStructDeferral<SAFEARRAYBOUND>(ref elem_0);
                this.rgsabound[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum VARENUM : int {
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
        VT_BYREF = 16384,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum ADVFEATUREFLAGS : int {
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
        FADF_VARIANT = 2048,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum SF_TYPE : int {
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
        SF_HAVEIID = 32781,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum CALLCONV : int {
        CC_CDECL = 1,
        CC_PASCAL = 2,
        CC_STDCALL = 4,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum FUNCFLAGS : int {
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
        FUNCFLAG_FIMMEDIATEBIND = 4096,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum FUNCKIND : int {
        FUNC_PUREVIRTUAL = 1,
        FUNC_STATIC = 3,
        FUNC_DISPATCH = 4,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum IMPLTYPEFLAGS : int {
        IMPLTYPEFLAG_FDEFAULT = 1,
        IMPLTYPEFLAG_FSOURCE = 2,
        IMPLTYPEFLAG_FRESTRICTED = 4,
        IMPLTYPEFLAG_FDEFAULTVTABLE = 8,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum INVOKEKIND : int {
        INVOKE_FUNC = 1,
        INVOKE_PROPERTYGET = 2,
        INVOKE_PROPERTYPUT = 4,
        INVOKE_PROPERTYPUTREF = 8,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum PARAMFLAGS : int {
        PARAMFLAG_NONE = 0,
        PARAMFLAG_FIN = 1,
        PARAMFLAG_FOUT = 2,
        PARAMFLAG_FLCID = 4,
        PARAMFLAG_FRETVAL = 8,
        PARAMFLAG_FOPT = 16,
        PARAMFLAG_FHASDEFAULT = 32,
        PARAMFLAG_FHASCUSTDATA = 64,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum TYPEFLAGS : int {
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
        TYPEFLAG_FPROXY = 16384,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum TYPEKIND : int {
        TKIND_ENUM = 0,
        TKIND_RECORD = 1,
        TKIND_MODULE = 2,
        TKIND_INTERFACE = 3,
        TKIND_DISPATCH = 4,
        TKIND_COCLASS = 5,
        TKIND_ALIAS = 6,
        TKIND_UNION = 7,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum VARFLAGS : int {
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
        VARFLAG_FIMMEDIATEBIND = 4096,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum VARKIND : int {
        VAR_PERINSTANCE = 0,
        VAR_STATIC = 1,
        VAR_CONST = 2,
        VAR_DISPATCH = 3,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum LIBFLAGS : int {
        LIBFLAG_FRESTRICTED = 1,
        LIBFLAG_FCONTROL = 2,
        LIBFLAG_FHIDDEN = 4,
        LIBFLAG_FHASDISKIMAGE = 8,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum SYSKIND : int {
        SYS_WIN32 = 1,
        SYS_WIN64 = 3,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum DESCKIND : int {
        DESCKIND_NONE = 0,
        DESCKIND_FUNCDESC = 1,
        DESCKIND_VARDESC = 2,
        DESCKIND_TYPECOMP = 3,
        DESCKIND_IMPLICITAPPOBJ = 4,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct RecordInfo : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.libraryGuid);
            encoder.WriteValue(this.verMajor);
            encoder.WriteValue(this.recGuid);
            encoder.WriteValue(this.verMinor);
            encoder.WriteValue(this.Lcid);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.libraryGuid = decoder.ReadUuid();
            this.verMajor = decoder.ReadUInt32();
            this.recGuid = decoder.ReadUuid();
            this.verMinor = decoder.ReadUInt32();
            this.Lcid = decoder.ReadUInt32();
        }
        public System.Guid libraryGuid;
        public uint verMajor;
        public System.Guid recGuid;
        public uint verMinor;
        public uint Lcid;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DISPPARAMS : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.rgvarg);
            encoder.WritePointer(this.rgdispidNamedArgs);
            encoder.WriteValue(this.cArgs);
            encoder.WriteValue(this.cNamedArgs);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.rgvarg = decoder.ReadUniquePointer<RpcPointer<wireVARIANTStr>[]>();
            this.rgdispidNamedArgs = decoder.ReadUniquePointer<int[]>();
            this.cArgs = decoder.ReadUInt32();
            this.cNamedArgs = decoder.ReadUInt32();
        }
        public RpcPointer<RpcPointer<wireVARIANTStr>[]> rgvarg;
        public RpcPointer<int[]> rgdispidNamedArgs;
        public uint cArgs;
        public uint cNamedArgs;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.rgvarg)) {
                encoder.WriteArrayHeader(this.rgvarg.value);
                for (int i = 0; (i < this.rgvarg.value.Length); i++
                ) {
                    RpcPointer<wireVARIANTStr> elem_0 = this.rgvarg.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < this.rgvarg.value.Length); i++
                ) {
                    RpcPointer<wireVARIANTStr> elem_0 = this.rgvarg.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._8Byte);
                        encoder.WriteStructDeferral(elem_0.value);
                    }
                }
            }
            if ((null != this.rgdispidNamedArgs)) {
                encoder.WriteArrayHeader(this.rgdispidNamedArgs.value);
                for (int i = 0; (i < this.rgdispidNamedArgs.value.Length); i++
                ) {
                    int elem_0 = this.rgdispidNamedArgs.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.rgvarg)) {
                this.rgvarg.value = decoder.ReadArrayHeader<RpcPointer<wireVARIANTStr>>();
                for (int i = 0; (i < this.rgvarg.value.Length); i++
                ) {
                    RpcPointer<wireVARIANTStr> elem_0 = this.rgvarg.value[i];
                    elem_0 = decoder.ReadUniquePointer<wireVARIANTStr>();
                    this.rgvarg.value[i] = elem_0;
                }
                for (int i = 0; (i < this.rgvarg.value.Length); i++
                ) {
                    RpcPointer<wireVARIANTStr> elem_0 = this.rgvarg.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                        decoder.ReadStructDeferral<wireVARIANTStr>(ref elem_0.value);
                    }
                    this.rgvarg.value[i] = elem_0;
                }
            }
            if ((null != this.rgdispidNamedArgs)) {
                this.rgdispidNamedArgs.value = decoder.ReadArrayHeader<int>();
                for (int i = 0; (i < this.rgdispidNamedArgs.value.Length); i++
                ) {
                    int elem_0 = this.rgdispidNamedArgs.value[i];
                    elem_0 = decoder.ReadInt32();
                    this.rgdispidNamedArgs.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct EXCEPINFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.wCode);
            encoder.WriteValue(this.wReserved);
            encoder.WritePointer(this.bstrSource);
            encoder.WritePointer(this.bstrDescription);
            encoder.WritePointer(this.bstrHelpFile);
            encoder.WriteValue(this.dwHelpContext);
            encoder.WriteValue(this.pvReserved);
            encoder.WriteValue(this.pfnDeferredFillIn);
            encoder.WriteValue(this.scode);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
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
        public ushort wCode;
        public ushort wReserved;
        public RpcPointer<FLAGGED_WORD_BLOB> bstrSource;
        public RpcPointer<FLAGGED_WORD_BLOB> bstrDescription;
        public RpcPointer<FLAGGED_WORD_BLOB> bstrHelpFile;
        public uint dwHelpContext;
        public System.UIntPtr pvReserved;
        public System.UIntPtr pfnDeferredFillIn;
        public int scode;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.bstrSource)) {
                encoder.WriteConformantStruct(this.bstrSource.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.bstrSource.value);
            }
            if ((null != this.bstrDescription)) {
                encoder.WriteConformantStruct(this.bstrDescription.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.bstrDescription.value);
            }
            if ((null != this.bstrHelpFile)) {
                encoder.WriteConformantStruct(this.bstrHelpFile.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.bstrHelpFile.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.bstrSource)) {
                this.bstrSource.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref this.bstrSource.value);
            }
            if ((null != this.bstrDescription)) {
                this.bstrDescription.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref this.bstrDescription.value);
            }
            if ((null != this.bstrHelpFile)) {
                this.bstrHelpFile.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref this.bstrHelpFile.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ARRAYDESC : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.tdescElem, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.cDims);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.tdescElem = decoder.ReadFixedStruct<TYPEDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.cDims = decoder.ReadUInt16();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.rgbounds);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.rgbounds = decoder.ReadArrayHeader<SAFEARRAYBOUND>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.rgbounds.Length); i++
            ) {
                SAFEARRAYBOUND elem_0 = this.rgbounds[i];
                encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.rgbounds.Length); i++
            ) {
                SAFEARRAYBOUND elem_0 = this.rgbounds[i];
                elem_0 = decoder.ReadFixedStruct<SAFEARRAYBOUND>(Titanis.DceRpc.NdrAlignment._4Byte);
                this.rgbounds[i] = elem_0;
            }
        }
        public TYPEDESC tdescElem;
        public ushort cDims;
        public SAFEARRAYBOUND[] rgbounds;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.tdescElem);
            for (int i = 0; (i < this.rgbounds.Length); i++
            ) {
                SAFEARRAYBOUND elem_0 = this.rgbounds[i];
                encoder.WriteStructDeferral(elem_0);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<TYPEDESC>(ref this.tdescElem);
            for (int i = 0; (i < this.rgbounds.Length); i++
            ) {
                SAFEARRAYBOUND elem_0 = this.rgbounds[i];
                decoder.ReadStructDeferral<SAFEARRAYBOUND>(ref elem_0);
                this.rgbounds[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct Unnamed_2 : Titanis.DceRpc.IRpcFixedStruct {
        public ushort vt;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.vt);
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if (((((int)(this.vt)) == 26) 
                        && (((int)(this.vt)) == 27))) {
                encoder.WritePointer(this.lptdesc);
            }
            else {
                if ((((int)(this.vt)) == 28)) {
                    encoder.WritePointer(this.lpadesc);
                }
                else {
                    if ((((int)(this.vt)) == 29)) {
                        encoder.WriteValue(this.hreftype);
                    }
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.vt = decoder.ReadUInt16();
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if (((((int)(this.vt)) == 26) 
                        && (((int)(this.vt)) == 27))) {
                this.lptdesc = decoder.ReadUniquePointer<TYPEDESC>();
            }
            else {
                if ((((int)(this.vt)) == 28)) {
                    this.lpadesc = decoder.ReadUniquePointer<ARRAYDESC>();
                }
                else {
                    if ((((int)(this.vt)) == 29)) {
                        this.hreftype = decoder.ReadUInt32();
                    }
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if (((((int)(this.vt)) == 26) 
                        && (((int)(this.vt)) == 27))) {
                if ((null != this.lptdesc)) {
                    encoder.WriteFixedStruct(this.lptdesc.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(this.lptdesc.value);
                }
            }
            else {
                if ((((int)(this.vt)) == 28)) {
                    if ((null != this.lpadesc)) {
                        encoder.WriteConformantStruct(this.lpadesc.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                        encoder.WriteStructDeferral(this.lpadesc.value);
                    }
                }
                else {
                    if ((((int)(this.vt)) == 29)) {
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if (((((int)(this.vt)) == 26) 
                        && (((int)(this.vt)) == 27))) {
                if ((null != this.lptdesc)) {
                    this.lptdesc.value = decoder.ReadFixedStruct<TYPEDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<TYPEDESC>(ref this.lptdesc.value);
                }
            }
            else {
                if ((((int)(this.vt)) == 28)) {
                    if ((null != this.lpadesc)) {
                        this.lpadesc.value = decoder.ReadConformantStruct<ARRAYDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        decoder.ReadStructDeferral<ARRAYDESC>(ref this.lpadesc.value);
                    }
                }
                else {
                    if ((((int)(this.vt)) == 29)) {
                    }
                }
            }
        }
        public RpcPointer<TYPEDESC> lptdesc;
        public RpcPointer<ARRAYDESC> lpadesc;
        public uint hreftype;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct TYPEDESC : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteUnion(this._tdUnion);
            encoder.WriteValue(this.vt);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this._tdUnion = decoder.ReadUnion<Unnamed_2>();
            this.vt = decoder.ReadUInt16();
        }
        public Unnamed_2 _tdUnion;
        public ushort vt;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this._tdUnion);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<Unnamed_2>(ref this._tdUnion);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct PARAMDESCEX : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cBytes);
            encoder.WritePointer(this.varDefaultValue);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cBytes = decoder.ReadUInt32();
            this.varDefaultValue = decoder.ReadUniquePointer<wireVARIANTStr>();
        }
        public uint cBytes;
        public RpcPointer<wireVARIANTStr> varDefaultValue;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.varDefaultValue)) {
                encoder.WriteFixedStruct(this.varDefaultValue.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(this.varDefaultValue.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.varDefaultValue)) {
                this.varDefaultValue.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<wireVARIANTStr>(ref this.varDefaultValue.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct PARAMDESC : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.pparamdescex);
            encoder.WriteValue(this.wParamFlags);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.pparamdescex = decoder.ReadUniquePointer<PARAMDESCEX>();
            this.wParamFlags = decoder.ReadUInt16();
        }
        public RpcPointer<PARAMDESCEX> pparamdescex;
        public ushort wParamFlags;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pparamdescex)) {
                encoder.WriteFixedStruct(this.pparamdescex.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.pparamdescex.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pparamdescex)) {
                this.pparamdescex.value = decoder.ReadFixedStruct<PARAMDESCEX>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<PARAMDESCEX>(ref this.pparamdescex.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ELEMDESC : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.tdesc, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.paramdesc, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.tdesc = decoder.ReadFixedStruct<TYPEDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.paramdesc = decoder.ReadFixedStruct<PARAMDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public TYPEDESC tdesc;
        public PARAMDESC paramdesc;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.tdesc);
            encoder.WriteStructDeferral(this.paramdesc);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<TYPEDESC>(ref this.tdesc);
            decoder.ReadStructDeferral<PARAMDESC>(ref this.paramdesc);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct FUNCDESC : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.memid);
            encoder.WritePointer(this.lReserved1);
            encoder.WritePointer(this.lprgelemdescParam);
            encoder.WriteValue(((int)(this.funckind)));
            encoder.WriteValue(((int)(this.invkind)));
            encoder.WriteValue(((int)(this.callconv)));
            encoder.WriteValue(this.cParams);
            encoder.WriteValue(this.cParamsOpt);
            encoder.WriteValue(this.oVft);
            encoder.WriteValue(this.cReserved2);
            encoder.WriteFixedStruct(this.elemdescFunc, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.wFuncFlags);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.memid = decoder.ReadInt32();
            this.lReserved1 = decoder.ReadUniquePointer<int[]>();
            this.lprgelemdescParam = decoder.ReadUniquePointer<ELEMDESC[]>();
            this.funckind = ((FUNCKIND)(decoder.ReadInt32()));
            this.invkind = ((INVOKEKIND)(decoder.ReadInt32()));
            this.callconv = ((CALLCONV)(decoder.ReadInt32()));
            this.cParams = decoder.ReadInt16();
            this.cParamsOpt = decoder.ReadInt16();
            this.oVft = decoder.ReadInt16();
            this.cReserved2 = decoder.ReadInt16();
            this.elemdescFunc = decoder.ReadFixedStruct<ELEMDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.wFuncFlags = decoder.ReadUInt16();
        }
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
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lReserved1)) {
                encoder.WriteArrayHeader(this.lReserved1.value);
                for (int i = 0; (i < this.lReserved1.value.Length); i++
                ) {
                    int elem_0 = this.lReserved1.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            if ((null != this.lprgelemdescParam)) {
                encoder.WriteArrayHeader(this.lprgelemdescParam.value);
                for (int i = 0; (i < this.lprgelemdescParam.value.Length); i++
                ) {
                    ELEMDESC elem_0 = this.lprgelemdescParam.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.lprgelemdescParam.value.Length); i++
                ) {
                    ELEMDESC elem_0 = this.lprgelemdescParam.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
            encoder.WriteStructDeferral(this.elemdescFunc);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lReserved1)) {
                this.lReserved1.value = decoder.ReadArrayHeader<int>();
                for (int i = 0; (i < this.lReserved1.value.Length); i++
                ) {
                    int elem_0 = this.lReserved1.value[i];
                    elem_0 = decoder.ReadInt32();
                    this.lReserved1.value[i] = elem_0;
                }
            }
            if ((null != this.lprgelemdescParam)) {
                this.lprgelemdescParam.value = decoder.ReadArrayHeader<ELEMDESC>();
                for (int i = 0; (i < this.lprgelemdescParam.value.Length); i++
                ) {
                    ELEMDESC elem_0 = this.lprgelemdescParam.value[i];
                    elem_0 = decoder.ReadFixedStruct<ELEMDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.lprgelemdescParam.value[i] = elem_0;
                }
                for (int i = 0; (i < this.lprgelemdescParam.value.Length); i++
                ) {
                    ELEMDESC elem_0 = this.lprgelemdescParam.value[i];
                    decoder.ReadStructDeferral<ELEMDESC>(ref elem_0);
                    this.lprgelemdescParam.value[i] = elem_0;
                }
            }
            decoder.ReadStructDeferral<ELEMDESC>(ref this.elemdescFunc);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct Unnamed_3 : Titanis.DceRpc.IRpcFixedStruct {
        public VARKIND varkind;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((int)(this.varkind)));
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if (((((int)(this.varkind)) == 0) 
                        && ((((int)(this.varkind)) == 3) 
                        && (((int)(this.varkind)) == 1)))) {
                encoder.WriteValue(this.oInst);
            }
            else {
                if ((((int)(this.varkind)) == 2)) {
                    encoder.WritePointer(this.lpvarValue);
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.varkind = ((VARKIND)(decoder.ReadInt32()));
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if (((((int)(this.varkind)) == 0) 
                        && ((((int)(this.varkind)) == 3) 
                        && (((int)(this.varkind)) == 1)))) {
                this.oInst = decoder.ReadUInt32();
            }
            else {
                if ((((int)(this.varkind)) == 2)) {
                    this.lpvarValue = decoder.ReadUniquePointer<RpcPointer<wireVARIANTStr>>();
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if (((((int)(this.varkind)) == 0) 
                        && ((((int)(this.varkind)) == 3) 
                        && (((int)(this.varkind)) == 1)))) {
            }
            else {
                if ((((int)(this.varkind)) == 2)) {
                    if ((null != this.lpvarValue)) {
                        encoder.WritePointer(this.lpvarValue.value);
                        if ((null != this.lpvarValue.value)) {
                            encoder.WriteFixedStruct(this.lpvarValue.value.value, Titanis.DceRpc.NdrAlignment._8Byte);
                            encoder.WriteStructDeferral(this.lpvarValue.value.value);
                        }
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if (((((int)(this.varkind)) == 0) 
                        && ((((int)(this.varkind)) == 3) 
                        && (((int)(this.varkind)) == 1)))) {
            }
            else {
                if ((((int)(this.varkind)) == 2)) {
                    if ((null != this.lpvarValue)) {
                        this.lpvarValue.value = decoder.ReadUniquePointer<wireVARIANTStr>();
                        if ((null != this.lpvarValue.value)) {
                            this.lpvarValue.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                            decoder.ReadStructDeferral<wireVARIANTStr>(ref this.lpvarValue.value.value);
                        }
                    }
                }
            }
        }
        public uint oInst;
        public RpcPointer<RpcPointer<wireVARIANTStr>> lpvarValue;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct VARDESC : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.memid);
            encoder.WritePointer(this.lpstrReserved);
            encoder.WriteUnion(this._vdUnion);
            encoder.WriteFixedStruct(this.elemdescVar, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.wVarFlags);
            encoder.WriteValue(((int)(this.varkind)));
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.memid = decoder.ReadInt32();
            this.lpstrReserved = decoder.ReadUniquePointer<string>();
            this._vdUnion = decoder.ReadUnion<Unnamed_3>();
            this.elemdescVar = decoder.ReadFixedStruct<ELEMDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.wVarFlags = decoder.ReadUInt16();
            this.varkind = ((VARKIND)(decoder.ReadInt32()));
        }
        public int memid;
        public RpcPointer<string> lpstrReserved;
        public Unnamed_3 _vdUnion;
        public ELEMDESC elemdescVar;
        public ushort wVarFlags;
        public VARKIND varkind;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpstrReserved)) {
                encoder.WriteWideCharString(this.lpstrReserved.value);
            }
            encoder.WriteStructDeferral(this._vdUnion);
            encoder.WriteStructDeferral(this.elemdescVar);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpstrReserved)) {
                this.lpstrReserved.value = decoder.ReadWideCharString();
            }
            decoder.ReadStructDeferral<Unnamed_3>(ref this._vdUnion);
            decoder.ReadStructDeferral<ELEMDESC>(ref this.elemdescVar);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct TYPEATTR : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.guid);
            encoder.WriteValue(this.lcid);
            encoder.WriteValue(this.dwReserved1);
            encoder.WriteValue(this.dwReserved2);
            encoder.WriteValue(this.dwReserved3);
            encoder.WritePointer(this.lpstrReserved4);
            encoder.WriteValue(this.cbSizeInstance);
            encoder.WriteValue(((int)(this.typekind)));
            encoder.WriteValue(this.cFuncs);
            encoder.WriteValue(this.cVars);
            encoder.WriteValue(this.cImplTypes);
            encoder.WriteValue(this.cbSizeVft);
            encoder.WriteValue(this.cbAlignment);
            encoder.WriteValue(this.wTypeFlags);
            encoder.WriteValue(this.wMajorVerNum);
            encoder.WriteValue(this.wMinorVerNum);
            encoder.WriteFixedStruct(this.tdescAlias, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.dwReserved5);
            encoder.WriteValue(this.wReserved6);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.guid = decoder.ReadUuid();
            this.lcid = decoder.ReadUInt32();
            this.dwReserved1 = decoder.ReadUInt32();
            this.dwReserved2 = decoder.ReadUInt32();
            this.dwReserved3 = decoder.ReadUInt32();
            this.lpstrReserved4 = decoder.ReadUniquePointer<string>();
            this.cbSizeInstance = decoder.ReadUInt32();
            this.typekind = ((TYPEKIND)(decoder.ReadInt32()));
            this.cFuncs = decoder.ReadUInt16();
            this.cVars = decoder.ReadUInt16();
            this.cImplTypes = decoder.ReadUInt16();
            this.cbSizeVft = decoder.ReadUInt16();
            this.cbAlignment = decoder.ReadUInt16();
            this.wTypeFlags = decoder.ReadUInt16();
            this.wMajorVerNum = decoder.ReadUInt16();
            this.wMinorVerNum = decoder.ReadUInt16();
            this.tdescAlias = decoder.ReadFixedStruct<TYPEDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.dwReserved5 = decoder.ReadUInt32();
            this.wReserved6 = decoder.ReadUInt16();
        }
        public System.Guid guid;
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
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpstrReserved4)) {
                encoder.WriteWideCharString(this.lpstrReserved4.value);
            }
            encoder.WriteStructDeferral(this.tdescAlias);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpstrReserved4)) {
                this.lpstrReserved4.value = decoder.ReadWideCharString();
            }
            decoder.ReadStructDeferral<TYPEDESC>(ref this.tdescAlias);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct TLIBATTR : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.guid);
            encoder.WriteValue(this.lcid);
            encoder.WriteValue(((int)(this.syskind)));
            encoder.WriteValue(this.wMajorVerNum);
            encoder.WriteValue(this.wMinorVerNum);
            encoder.WriteValue(this.wLibFlags);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.guid = decoder.ReadUuid();
            this.lcid = decoder.ReadUInt32();
            this.syskind = ((SYSKIND)(decoder.ReadInt32()));
            this.wMajorVerNum = decoder.ReadUInt16();
            this.wMinorVerNum = decoder.ReadUInt16();
            this.wLibFlags = decoder.ReadUInt16();
        }
        public System.Guid guid;
        public uint lcid;
        public SYSKIND syskind;
        public ushort wMajorVerNum;
        public ushort wMinorVerNum;
        public ushort wLibFlags;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct CUSTDATAITEM : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.guid);
            encoder.WritePointer(this.varValue);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.guid = decoder.ReadUuid();
            this.varValue = decoder.ReadUniquePointer<wireVARIANTStr>();
        }
        public System.Guid guid;
        public RpcPointer<wireVARIANTStr> varValue;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.varValue)) {
                encoder.WriteFixedStruct(this.varValue.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(this.varValue.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.varValue)) {
                this.varValue.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<wireVARIANTStr>(ref this.varValue.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct CUSTDATA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cCustData);
            encoder.WritePointer(this.prgCustData);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cCustData = decoder.ReadUInt32();
            this.prgCustData = decoder.ReadUniquePointer<CUSTDATAITEM[]>();
        }
        public uint cCustData;
        public RpcPointer<CUSTDATAITEM[]> prgCustData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.prgCustData)) {
                encoder.WriteArrayHeader(this.prgCustData.value);
                for (int i = 0; (i < this.prgCustData.value.Length); i++
                ) {
                    CUSTDATAITEM elem_0 = this.prgCustData.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.prgCustData.value.Length); i++
                ) {
                    CUSTDATAITEM elem_0 = this.prgCustData.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.prgCustData)) {
                this.prgCustData.value = decoder.ReadArrayHeader<CUSTDATAITEM>();
                for (int i = 0; (i < this.prgCustData.value.Length); i++
                ) {
                    CUSTDATAITEM elem_0 = this.prgCustData.value[i];
                    elem_0 = decoder.ReadFixedStruct<CUSTDATAITEM>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.prgCustData.value[i] = elem_0;
                }
                for (int i = 0; (i < this.prgCustData.value.Length); i++
                ) {
                    CUSTDATAITEM elem_0 = this.prgCustData.value[i];
                    decoder.ReadStructDeferral<CUSTDATAITEM>(ref elem_0);
                    this.prgCustData.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [System.Runtime.InteropServices.GuidAttribute("00020400-0000-0000-c000-000000000046")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface IDispatch : IUnknown {
        Task<int> GetTypeInfoCount(RpcPointer<uint> pctinfo, System.Threading.CancellationToken cancellationToken);
        Task<int> GetTypeInfo(uint iTInfo, uint lcid, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo, System.Threading.CancellationToken cancellationToken);
        Task<int> GetIDsOfNames(System.Guid riid, RpcPointer<string>[] rgszNames, uint cNames, uint lcid, RpcPointer<int[]> rgDispId, System.Threading.CancellationToken cancellationToken);
        Task<int> Invoke(int dispIdMember, System.Guid riid, uint lcid, uint dwFlags, DISPPARAMS pDispParams, RpcPointer<RpcPointer<wireVARIANTStr>> pVarResult, RpcPointer<EXCEPINFO> pExcepInfo, RpcPointer<uint> pArgErr, uint cVarRef, uint[] rgVarRefIdx, RpcPointer<RpcPointer<wireVARIANTStr>[]> rgVarRef, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [Titanis.DceRpc.IidAttribute("00020400-0000-0000-c000-000000000046")]
    public class IDispatchClientProxy : IUnknownClientProxy, IDispatch {
        private static System.Guid _interfaceUuid = new System.Guid("00020400-0000-0000-c000-000000000046");
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
        public virtual async Task<int> GetTypeInfoCount(RpcPointer<uint> pctinfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pctinfo.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetTypeInfo(uint iTInfo, uint lcid, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(iTInfo);
            encoder.WriteValue(lcid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppTInfo.value = decoder.ReadInterfacePointer<ITypeInfo>();
            decoder.ReadInterfacePointer(ppTInfo.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetIDsOfNames(System.Guid riid, RpcPointer<string>[] rgszNames, uint cNames, uint lcid, RpcPointer<int[]> rgDispId, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(riid);
            if ((rgszNames != null)) {
                encoder.WriteArrayHeader(rgszNames);
                for (int i = 0; (i < rgszNames.Length); i++
                ) {
                    RpcPointer<string> elem_0 = rgszNames[i];
                    encoder.WritePointer(elem_0);
                }
            }
            for (int i = 0; (i < rgszNames.Length); i++
            ) {
                RpcPointer<string> elem_0 = rgszNames[i];
                if ((null != elem_0)) {
                    encoder.WriteWideCharString(elem_0.value);
                }
            }
            encoder.WriteValue(cNames);
            encoder.WriteValue(lcid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            rgDispId.value = decoder.ReadArrayHeader<int>();
            for (int i = 0; (i < rgDispId.value.Length); i++
            ) {
                int elem_0 = rgDispId.value[i];
                elem_0 = decoder.ReadInt32();
                rgDispId.value[i] = elem_0;
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Invoke(int dispIdMember, System.Guid riid, uint lcid, uint dwFlags, DISPPARAMS pDispParams, RpcPointer<RpcPointer<wireVARIANTStr>> pVarResult, RpcPointer<EXCEPINFO> pExcepInfo, RpcPointer<uint> pArgErr, uint cVarRef, uint[] rgVarRefIdx, RpcPointer<RpcPointer<wireVARIANTStr>[]> rgVarRef, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(dispIdMember);
            encoder.WriteValue(riid);
            encoder.WriteValue(lcid);
            encoder.WriteValue(dwFlags);
            encoder.WriteFixedStruct(pDispParams, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(pDispParams);
            encoder.WriteValue(cVarRef);
            if ((rgVarRefIdx != null)) {
                encoder.WriteArrayHeader(rgVarRefIdx);
                for (int i = 0; (i < rgVarRefIdx.Length); i++
                ) {
                    uint elem_0 = rgVarRefIdx[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteArrayHeader(rgVarRef.value);
            for (int i = 0; (i < rgVarRef.value.Length); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
                encoder.WritePointer(elem_0);
            }
            for (int i = 0; (i < rgVarRef.value.Length); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
                if ((null != elem_0)) {
                    encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._8Byte);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pVarResult.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarResult.value);
            if ((null != pVarResult.value)) {
                pVarResult.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarResult.value.value);
            }
            pExcepInfo.value = decoder.ReadFixedStruct<EXCEPINFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<EXCEPINFO>(ref pExcepInfo.value);
            pArgErr.value = decoder.ReadUInt32();
            rgVarRef.value = decoder.ReadArrayHeader<RpcPointer<wireVARIANTStr>>();
            for (int i = 0; (i < rgVarRef.value.Length); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
                elem_0 = decoder.ReadUniquePointer<wireVARIANTStr>();
                rgVarRef.value[i] = elem_0;
            }
            for (int i = 0; (i < rgVarRef.value.Length); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                    decoder.ReadStructDeferral<wireVARIANTStr>(ref elem_0.value);
                }
                rgVarRef.value[i] = elem_0;
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public class IDispatchStub : Titanis.DceRpc.Server.RpcObjectStub {
        private static System.Guid _interfaceUuid = new System.Guid("00020400-0000-0000-c000-000000000046");
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
        private IDispatch _obj;
        public IDispatchStub(IDispatch obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_Opnum0NotUsedOnWire,
                    this.Invoke_Opnum1NotUsedOnWire,
                    this.Invoke_Opnum2NotUsedOnWire,
                    this.Invoke_GetTypeInfoCount,
                    this.Invoke_GetTypeInfo,
                    this.Invoke_GetIDsOfNames,
                    this.Invoke_Invoke};
        }
        private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeInfoCount(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<uint> pctinfo = new RpcPointer<uint>();
            var invokeTask = this._obj.GetTypeInfoCount(pctinfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pctinfo.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint iTInfo;
            uint lcid;
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>>();
            iTInfo = decoder.ReadUInt32();
            lcid = decoder.ReadUInt32();
            var invokeTask = this._obj.GetTypeInfo(iTInfo, lcid, ppTInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTInfo.value);
            encoder.WriteInterfacePointerBody(ppTInfo.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetIDsOfNames(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            System.Guid riid;
            RpcPointer<string>[] rgszNames;
            uint cNames;
            uint lcid;
            RpcPointer<int[]> rgDispId = new RpcPointer<int[]>();
            riid = decoder.ReadUuid();
            rgszNames = decoder.ReadArrayHeader<RpcPointer<string>>();
            for (int i = 0; (i < rgszNames.Length); i++
            ) {
                RpcPointer<string> elem_0 = rgszNames[i];
                elem_0 = decoder.ReadUniquePointer<string>();
                rgszNames[i] = elem_0;
            }
            for (int i = 0; (i < rgszNames.Length); i++
            ) {
                RpcPointer<string> elem_0 = rgszNames[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadWideCharString();
                }
                rgszNames[i] = elem_0;
            }
            cNames = decoder.ReadUInt32();
            lcid = decoder.ReadUInt32();
            var invokeTask = this._obj.GetIDsOfNames(riid, rgszNames, cNames, lcid, rgDispId, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(rgDispId.value);
            for (int i = 0; (i < rgDispId.value.Length); i++
            ) {
                int elem_0 = rgDispId.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Invoke(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            int dispIdMember;
            System.Guid riid;
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
            pDispParams = decoder.ReadFixedStruct<DISPPARAMS>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<DISPPARAMS>(ref pDispParams);
            cVarRef = decoder.ReadUInt32();
            rgVarRefIdx = decoder.ReadArrayHeader<uint>();
            for (int i = 0; (i < rgVarRefIdx.Length); i++
            ) {
                uint elem_0 = rgVarRefIdx[i];
                elem_0 = decoder.ReadUInt32();
                rgVarRefIdx[i] = elem_0;
            }
            rgVarRef = new RpcPointer<RpcPointer<wireVARIANTStr>[]>();
            rgVarRef.value = decoder.ReadArrayHeader<RpcPointer<wireVARIANTStr>>();
            for (int i = 0; (i < rgVarRef.value.Length); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
                elem_0 = decoder.ReadUniquePointer<wireVARIANTStr>();
                rgVarRef.value[i] = elem_0;
            }
            for (int i = 0; (i < rgVarRef.value.Length); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                    decoder.ReadStructDeferral<wireVARIANTStr>(ref elem_0.value);
                }
                rgVarRef.value[i] = elem_0;
            }
            var invokeTask = this._obj.Invoke(dispIdMember, riid, lcid, dwFlags, pDispParams, pVarResult, pExcepInfo, pArgErr, cVarRef, rgVarRefIdx, rgVarRef, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(pVarResult.value);
            if ((null != pVarResult.value)) {
                encoder.WriteFixedStruct(pVarResult.value.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(pVarResult.value.value);
            }
            encoder.WriteFixedStruct(pExcepInfo.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(pExcepInfo.value);
            encoder.WriteValue(pArgErr.value);
            encoder.WriteArrayHeader(rgVarRef.value);
            for (int i = 0; (i < rgVarRef.value.Length); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
                encoder.WritePointer(elem_0);
            }
            for (int i = 0; (i < rgVarRef.value.Length); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = rgVarRef.value[i];
                if ((null != elem_0)) {
                    encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._8Byte);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
            encoder.WriteValue(retval);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [System.Runtime.InteropServices.GuidAttribute("00020404-0000-0000-c000-000000000046")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface IEnumVARIANT : IUnknown {
        Task<int> Next(uint celt, RpcPointer<ArraySegment<RpcPointer<wireVARIANTStr>>> rgVar, RpcPointer<uint> pCeltFetched, System.Threading.CancellationToken cancellationToken);
        Task<int> Skip(uint celt, System.Threading.CancellationToken cancellationToken);
        Task<int> Reset(System.Threading.CancellationToken cancellationToken);
        Task<int> Clone(RpcPointer<Titanis.DceRpc.TypedObjref<IEnumVARIANT>> ppEnum, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [Titanis.DceRpc.IidAttribute("00020404-0000-0000-c000-000000000046")]
    public class IEnumVARIANTClientProxy : IUnknownClientProxy, IEnumVARIANT {
        private static System.Guid _interfaceUuid = new System.Guid("00020404-0000-0000-c000-000000000046");
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
        public virtual async Task<int> Next(uint celt, RpcPointer<ArraySegment<RpcPointer<wireVARIANTStr>>> rgVar, RpcPointer<uint> pCeltFetched, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(celt);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            rgVar.value = decoder.ReadArraySegmentHeader<RpcPointer<wireVARIANTStr>>();
            for (int i = 0; (i < rgVar.value.Count); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = rgVar.value.Item(i);
                elem_0 = decoder.ReadUniquePointer<wireVARIANTStr>();
                rgVar.value.Item(i) = elem_0;
            }
            for (int i = 0; (i < rgVar.value.Count); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = rgVar.value.Item(i);
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                    decoder.ReadStructDeferral<wireVARIANTStr>(ref elem_0.value);
                }
                rgVar.value.Item(i) = elem_0;
            }
            pCeltFetched.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Skip(uint celt, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(celt);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Reset(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Clone(RpcPointer<Titanis.DceRpc.TypedObjref<IEnumVARIANT>> ppEnum, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppEnum.value = decoder.ReadInterfacePointer<IEnumVARIANT>();
            decoder.ReadInterfacePointer(ppEnum.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public class IEnumVARIANTStub : Titanis.DceRpc.Server.RpcObjectStub {
        private static System.Guid _interfaceUuid = new System.Guid("00020404-0000-0000-c000-000000000046");
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
        private IEnumVARIANT _obj;
        public IEnumVARIANTStub(IEnumVARIANT obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_Opnum0NotUsedOnWire,
                    this.Invoke_Opnum1NotUsedOnWire,
                    this.Invoke_Opnum2NotUsedOnWire,
                    this.Invoke_Next,
                    this.Invoke_Skip,
                    this.Invoke_Reset,
                    this.Invoke_Clone};
        }
        private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Next(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint celt;
            RpcPointer<ArraySegment<RpcPointer<wireVARIANTStr>>> rgVar = new RpcPointer<ArraySegment<RpcPointer<wireVARIANTStr>>>();
            RpcPointer<uint> pCeltFetched = new RpcPointer<uint>();
            celt = decoder.ReadUInt32();
            var invokeTask = this._obj.Next(celt, rgVar, pCeltFetched, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(rgVar.value, true);
            for (int i = 0; (i < rgVar.value.Count); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = rgVar.value.Item(i);
                encoder.WritePointer(elem_0);
            }
            for (int i = 0; (i < rgVar.value.Count); i++
            ) {
                RpcPointer<wireVARIANTStr> elem_0 = rgVar.value.Item(i);
                if ((null != elem_0)) {
                    encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._8Byte);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
            encoder.WriteValue(pCeltFetched.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Skip(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint celt;
            celt = decoder.ReadUInt32();
            var invokeTask = this._obj.Skip(celt, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Reset(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Reset(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Clone(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.TypedObjref<IEnumVARIANT>> ppEnum = new RpcPointer<Titanis.DceRpc.TypedObjref<IEnumVARIANT>>();
            var invokeTask = this._obj.Clone(ppEnum, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppEnum.value);
            encoder.WriteInterfacePointerBody(ppEnum.value);
            encoder.WriteValue(retval);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [System.Runtime.InteropServices.GuidAttribute("00020403-0000-0000-c000-000000000046")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface ITypeComp : IUnknown {
        Task<int> Bind(string szName, uint lHashVal, ushort wFlags, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo, RpcPointer<DESCKIND> pDescKind, RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc, RpcPointer<RpcPointer<VARDESC>> ppVarDesc, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>> ppTypeComp, RpcPointer<uint> pReserved, System.Threading.CancellationToken cancellationToken);
        Task<int> BindType(string szName, uint lHashVal, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [Titanis.DceRpc.IidAttribute("00020403-0000-0000-c000-000000000046")]
    public class ITypeCompClientProxy : IUnknownClientProxy, ITypeComp {
        private static System.Guid _interfaceUuid = new System.Guid("00020403-0000-0000-c000-000000000046");
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
        public virtual async Task<int> Bind(string szName, uint lHashVal, ushort wFlags, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo, RpcPointer<DESCKIND> pDescKind, RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc, RpcPointer<RpcPointer<VARDESC>> ppVarDesc, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>> ppTypeComp, RpcPointer<uint> pReserved, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(szName);
            encoder.WriteValue(lHashVal);
            encoder.WriteValue(wFlags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppTInfo.value = decoder.ReadInterfacePointer<ITypeInfo>();
            decoder.ReadInterfacePointer(ppTInfo.value);
            pDescKind.value = ((DESCKIND)(decoder.ReadInt32()));
            ppFuncDesc.value = decoder.ReadOutUniquePointer<FUNCDESC>(ppFuncDesc.value);
            if ((null != ppFuncDesc.value)) {
                ppFuncDesc.value.value = decoder.ReadFixedStruct<FUNCDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<FUNCDESC>(ref ppFuncDesc.value.value);
            }
            ppVarDesc.value = decoder.ReadOutUniquePointer<VARDESC>(ppVarDesc.value);
            if ((null != ppVarDesc.value)) {
                ppVarDesc.value.value = decoder.ReadFixedStruct<VARDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<VARDESC>(ref ppVarDesc.value.value);
            }
            ppTypeComp.value = decoder.ReadInterfacePointer<ITypeComp>();
            decoder.ReadInterfacePointer(ppTypeComp.value);
            pReserved.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> BindType(string szName, uint lHashVal, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(szName);
            encoder.WriteValue(lHashVal);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppTInfo.value = decoder.ReadInterfacePointer<ITypeInfo>();
            decoder.ReadInterfacePointer(ppTInfo.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public class ITypeCompStub : Titanis.DceRpc.Server.RpcObjectStub {
        private static System.Guid _interfaceUuid = new System.Guid("00020403-0000-0000-c000-000000000046");
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
        private ITypeComp _obj;
        public ITypeCompStub(ITypeComp obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_Opnum0NotUsedOnWire,
                    this.Invoke_Opnum1NotUsedOnWire,
                    this.Invoke_Opnum2NotUsedOnWire,
                    this.Invoke_Bind,
                    this.Invoke_BindType};
        }
        private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Bind(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string szName;
            uint lHashVal;
            ushort wFlags;
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>>();
            RpcPointer<DESCKIND> pDescKind = new RpcPointer<DESCKIND>();
            RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc = new RpcPointer<RpcPointer<FUNCDESC>>();
            RpcPointer<RpcPointer<VARDESC>> ppVarDesc = new RpcPointer<RpcPointer<VARDESC>>();
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>> ppTypeComp = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>>();
            RpcPointer<uint> pReserved = new RpcPointer<uint>();
            szName = decoder.ReadWideCharString();
            lHashVal = decoder.ReadUInt32();
            wFlags = decoder.ReadUInt16();
            var invokeTask = this._obj.Bind(szName, lHashVal, wFlags, ppTInfo, pDescKind, ppFuncDesc, ppVarDesc, ppTypeComp, pReserved, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTInfo.value);
            encoder.WriteInterfacePointerBody(ppTInfo.value);
            encoder.WriteValue(((int)(pDescKind.value)));
            encoder.WritePointer(ppFuncDesc.value);
            if ((null != ppFuncDesc.value)) {
                encoder.WriteFixedStruct(ppFuncDesc.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ppFuncDesc.value.value);
            }
            encoder.WritePointer(ppVarDesc.value);
            if ((null != ppVarDesc.value)) {
                encoder.WriteFixedStruct(ppVarDesc.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ppVarDesc.value.value);
            }
            encoder.WriteInterfacePointer(ppTypeComp.value);
            encoder.WriteInterfacePointerBody(ppTypeComp.value);
            encoder.WriteValue(pReserved.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_BindType(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string szName;
            uint lHashVal;
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>>();
            szName = decoder.ReadWideCharString();
            lHashVal = decoder.ReadUInt32();
            var invokeTask = this._obj.BindType(szName, lHashVal, ppTInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTInfo.value);
            encoder.WriteInterfacePointerBody(ppTInfo.value);
            encoder.WriteValue(retval);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [System.Runtime.InteropServices.GuidAttribute("00020401-0000-0000-c000-000000000046")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface ITypeInfo : IUnknown {
        Task<int> GetTypeAttr(RpcPointer<RpcPointer<TYPEATTR>> ppTypeAttr, RpcPointer<uint> pReserved, System.Threading.CancellationToken cancellationToken);
        Task<int> GetTypeComp(RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>> ppTComp, System.Threading.CancellationToken cancellationToken);
        Task<int> GetFuncDesc(uint index, RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc, RpcPointer<uint> pReserved, System.Threading.CancellationToken cancellationToken);
        Task<int> GetVarDesc(uint index, RpcPointer<RpcPointer<VARDESC>> ppVarDesc, RpcPointer<uint> pReserved, System.Threading.CancellationToken cancellationToken);
        Task<int> GetNames(int memid, RpcPointer<ArraySegment<RpcPointer<FLAGGED_WORD_BLOB>>> rgBstrNames, uint cMaxNames, RpcPointer<uint> pcNames, System.Threading.CancellationToken cancellationToken);
        Task<int> GetRefTypeOfImplType(uint index, RpcPointer<uint> pRefType, System.Threading.CancellationToken cancellationToken);
        Task<int> GetImplTypeFlags(uint index, RpcPointer<int> pImplTypeFlags, System.Threading.CancellationToken cancellationToken);
        Task<int> Opnum10NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> Opnum11NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> GetDocumentation(int memid, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDocString, RpcPointer<uint> pdwHelpContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrHelpFile, System.Threading.CancellationToken cancellationToken);
        Task<int> GetDllEntry(int memid, INVOKEKIND invKind, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDllName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName, RpcPointer<ushort> pwOrdinal, System.Threading.CancellationToken cancellationToken);
        Task<int> GetRefTypeInfo(uint hRefType, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo, System.Threading.CancellationToken cancellationToken);
        Task<int> Opnum15NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> CreateInstance(System.Guid riid, RpcPointer<Titanis.DceRpc.TypedObjref<IUnknown>> ppvObj, System.Threading.CancellationToken cancellationToken);
        Task<int> GetMops(int memid, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrMops, System.Threading.CancellationToken cancellationToken);
        Task<int> GetContainingTypeLib(RpcPointer<Titanis.DceRpc.TypedObjref<ITypeLib>> ppTLib, RpcPointer<uint> pIndex, System.Threading.CancellationToken cancellationToken);
        Task<int> Opnum19NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> Opnum20NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> Opnum21NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [Titanis.DceRpc.IidAttribute("00020401-0000-0000-c000-000000000046")]
    public class ITypeInfoClientProxy : IUnknownClientProxy, ITypeInfo {
        private static System.Guid _interfaceUuid = new System.Guid("00020401-0000-0000-c000-000000000046");
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
        public virtual async Task<int> GetTypeAttr(RpcPointer<RpcPointer<TYPEATTR>> ppTypeAttr, RpcPointer<uint> pReserved, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppTypeAttr.value = decoder.ReadOutUniquePointer<TYPEATTR>(ppTypeAttr.value);
            if ((null != ppTypeAttr.value)) {
                ppTypeAttr.value.value = decoder.ReadFixedStruct<TYPEATTR>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<TYPEATTR>(ref ppTypeAttr.value.value);
            }
            pReserved.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetTypeComp(RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>> ppTComp, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppTComp.value = decoder.ReadInterfacePointer<ITypeComp>();
            decoder.ReadInterfacePointer(ppTComp.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetFuncDesc(uint index, RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc, RpcPointer<uint> pReserved, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppFuncDesc.value = decoder.ReadOutUniquePointer<FUNCDESC>(ppFuncDesc.value);
            if ((null != ppFuncDesc.value)) {
                ppFuncDesc.value.value = decoder.ReadFixedStruct<FUNCDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<FUNCDESC>(ref ppFuncDesc.value.value);
            }
            pReserved.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetVarDesc(uint index, RpcPointer<RpcPointer<VARDESC>> ppVarDesc, RpcPointer<uint> pReserved, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppVarDesc.value = decoder.ReadOutUniquePointer<VARDESC>(ppVarDesc.value);
            if ((null != ppVarDesc.value)) {
                ppVarDesc.value.value = decoder.ReadFixedStruct<VARDESC>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<VARDESC>(ref ppVarDesc.value.value);
            }
            pReserved.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetNames(int memid, RpcPointer<ArraySegment<RpcPointer<FLAGGED_WORD_BLOB>>> rgBstrNames, uint cMaxNames, RpcPointer<uint> pcNames, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(memid);
            encoder.WriteValue(cMaxNames);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            rgBstrNames.value = decoder.ReadArraySegmentHeader<RpcPointer<FLAGGED_WORD_BLOB>>();
            for (int i = 0; (i < rgBstrNames.value.Count); i++
            ) {
                RpcPointer<FLAGGED_WORD_BLOB> elem_0 = rgBstrNames.value.Item(i);
                elem_0 = decoder.ReadUniquePointer<FLAGGED_WORD_BLOB>();
                rgBstrNames.value.Item(i) = elem_0;
            }
            for (int i = 0; (i < rgBstrNames.value.Count); i++
            ) {
                RpcPointer<FLAGGED_WORD_BLOB> elem_0 = rgBstrNames.value.Item(i);
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                    decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref elem_0.value);
                }
                rgBstrNames.value.Item(i) = elem_0;
            }
            pcNames.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetRefTypeOfImplType(uint index, RpcPointer<uint> pRefType, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pRefType.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetImplTypeFlags(uint index, RpcPointer<int> pImplTypeFlags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pImplTypeFlags.value = decoder.ReadInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Opnum10NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Opnum11NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetDocumentation(int memid, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDocString, RpcPointer<uint> pdwHelpContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrHelpFile, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(memid);
            encoder.WriteValue(refPtrFlags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pBstrName.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrName.value);
            if ((null != pBstrName.value)) {
                pBstrName.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrName.value.value);
            }
            pBstrDocString.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrDocString.value);
            if ((null != pBstrDocString.value)) {
                pBstrDocString.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrDocString.value.value);
            }
            pdwHelpContext.value = decoder.ReadUInt32();
            pBstrHelpFile.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrHelpFile.value);
            if ((null != pBstrHelpFile.value)) {
                pBstrHelpFile.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrHelpFile.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetDllEntry(int memid, INVOKEKIND invKind, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDllName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName, RpcPointer<ushort> pwOrdinal, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(memid);
            encoder.WriteValue(((int)(invKind)));
            encoder.WriteValue(refPtrFlags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pBstrDllName.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrDllName.value);
            if ((null != pBstrDllName.value)) {
                pBstrDllName.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrDllName.value.value);
            }
            pBstrName.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrName.value);
            if ((null != pBstrName.value)) {
                pBstrName.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrName.value.value);
            }
            pwOrdinal.value = decoder.ReadUInt16();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetRefTypeInfo(uint hRefType, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(hRefType);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppTInfo.value = decoder.ReadInterfacePointer<ITypeInfo>();
            decoder.ReadInterfacePointer(ppTInfo.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Opnum15NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> CreateInstance(System.Guid riid, RpcPointer<Titanis.DceRpc.TypedObjref<IUnknown>> ppvObj, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(riid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppvObj.value = decoder.ReadInterfacePointer<IUnknown>();
            decoder.ReadInterfacePointer(ppvObj.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetMops(int memid, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrMops, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(memid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pBstrMops.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrMops.value);
            if ((null != pBstrMops.value)) {
                pBstrMops.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrMops.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetContainingTypeLib(RpcPointer<Titanis.DceRpc.TypedObjref<ITypeLib>> ppTLib, RpcPointer<uint> pIndex, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppTLib.value = decoder.ReadInterfacePointer<ITypeLib>();
            decoder.ReadInterfacePointer(ppTLib.value);
            pIndex.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Opnum19NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Opnum20NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Opnum21NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public class ITypeInfoStub : Titanis.DceRpc.Server.RpcObjectStub {
        private static System.Guid _interfaceUuid = new System.Guid("00020401-0000-0000-c000-000000000046");
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
        private ITypeInfo _obj;
        public ITypeInfoStub(ITypeInfo obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_Opnum0NotUsedOnWire,
                    this.Invoke_Opnum1NotUsedOnWire,
                    this.Invoke_Opnum2NotUsedOnWire,
                    this.Invoke_GetTypeAttr,
                    this.Invoke_GetTypeComp,
                    this.Invoke_GetFuncDesc,
                    this.Invoke_GetVarDesc,
                    this.Invoke_GetNames,
                    this.Invoke_GetRefTypeOfImplType,
                    this.Invoke_GetImplTypeFlags,
                    this.Invoke_Opnum10NotUsedOnWire,
                    this.Invoke_Opnum11NotUsedOnWire,
                    this.Invoke_GetDocumentation,
                    this.Invoke_GetDllEntry,
                    this.Invoke_GetRefTypeInfo,
                    this.Invoke_Opnum15NotUsedOnWire,
                    this.Invoke_CreateInstance,
                    this.Invoke_GetMops,
                    this.Invoke_GetContainingTypeLib,
                    this.Invoke_Opnum19NotUsedOnWire,
                    this.Invoke_Opnum20NotUsedOnWire,
                    this.Invoke_Opnum21NotUsedOnWire};
        }
        private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeAttr(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<RpcPointer<TYPEATTR>> ppTypeAttr = new RpcPointer<RpcPointer<TYPEATTR>>();
            RpcPointer<uint> pReserved = new RpcPointer<uint>();
            var invokeTask = this._obj.GetTypeAttr(ppTypeAttr, pReserved, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppTypeAttr.value);
            if ((null != ppTypeAttr.value)) {
                encoder.WriteFixedStruct(ppTypeAttr.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ppTypeAttr.value.value);
            }
            encoder.WriteValue(pReserved.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeComp(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>> ppTComp = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>>();
            var invokeTask = this._obj.GetTypeComp(ppTComp, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTComp.value);
            encoder.WriteInterfacePointerBody(ppTComp.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetFuncDesc(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc = new RpcPointer<RpcPointer<FUNCDESC>>();
            RpcPointer<uint> pReserved = new RpcPointer<uint>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetFuncDesc(index, ppFuncDesc, pReserved, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppFuncDesc.value);
            if ((null != ppFuncDesc.value)) {
                encoder.WriteFixedStruct(ppFuncDesc.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ppFuncDesc.value.value);
            }
            encoder.WriteValue(pReserved.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetVarDesc(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<RpcPointer<VARDESC>> ppVarDesc = new RpcPointer<RpcPointer<VARDESC>>();
            RpcPointer<uint> pReserved = new RpcPointer<uint>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetVarDesc(index, ppVarDesc, pReserved, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppVarDesc.value);
            if ((null != ppVarDesc.value)) {
                encoder.WriteFixedStruct(ppVarDesc.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ppVarDesc.value.value);
            }
            encoder.WriteValue(pReserved.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetNames(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            int memid;
            RpcPointer<ArraySegment<RpcPointer<FLAGGED_WORD_BLOB>>> rgBstrNames = new RpcPointer<ArraySegment<RpcPointer<FLAGGED_WORD_BLOB>>>();
            uint cMaxNames;
            RpcPointer<uint> pcNames = new RpcPointer<uint>();
            memid = decoder.ReadInt32();
            cMaxNames = decoder.ReadUInt32();
            var invokeTask = this._obj.GetNames(memid, rgBstrNames, cMaxNames, pcNames, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(rgBstrNames.value, true);
            for (int i = 0; (i < rgBstrNames.value.Count); i++
            ) {
                RpcPointer<FLAGGED_WORD_BLOB> elem_0 = rgBstrNames.value.Item(i);
                encoder.WritePointer(elem_0);
            }
            for (int i = 0; (i < rgBstrNames.value.Count); i++
            ) {
                RpcPointer<FLAGGED_WORD_BLOB> elem_0 = rgBstrNames.value.Item(i);
                if ((null != elem_0)) {
                    encoder.WriteConformantStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._4Byte);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
            encoder.WriteValue(pcNames.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetRefTypeOfImplType(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<uint> pRefType = new RpcPointer<uint>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetRefTypeOfImplType(index, pRefType, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pRefType.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetImplTypeFlags(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<int> pImplTypeFlags = new RpcPointer<int>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetImplTypeFlags(index, pImplTypeFlags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pImplTypeFlags.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum10NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum10NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum11NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum11NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetDocumentation(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
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
            encoder.WritePointer(pBstrName.value);
            if ((null != pBstrName.value)) {
                encoder.WriteConformantStruct(pBstrName.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrName.value.value);
            }
            encoder.WritePointer(pBstrDocString.value);
            if ((null != pBstrDocString.value)) {
                encoder.WriteConformantStruct(pBstrDocString.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrDocString.value.value);
            }
            encoder.WriteValue(pdwHelpContext.value);
            encoder.WritePointer(pBstrHelpFile.value);
            if ((null != pBstrHelpFile.value)) {
                encoder.WriteConformantStruct(pBstrHelpFile.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrHelpFile.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetDllEntry(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            int memid;
            INVOKEKIND invKind;
            uint refPtrFlags;
            RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDllName = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
            RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
            RpcPointer<ushort> pwOrdinal = new RpcPointer<ushort>();
            memid = decoder.ReadInt32();
            invKind = ((INVOKEKIND)(decoder.ReadInt32()));
            refPtrFlags = decoder.ReadUInt32();
            var invokeTask = this._obj.GetDllEntry(memid, invKind, refPtrFlags, pBstrDllName, pBstrName, pwOrdinal, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(pBstrDllName.value);
            if ((null != pBstrDllName.value)) {
                encoder.WriteConformantStruct(pBstrDllName.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrDllName.value.value);
            }
            encoder.WritePointer(pBstrName.value);
            if ((null != pBstrName.value)) {
                encoder.WriteConformantStruct(pBstrName.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrName.value.value);
            }
            encoder.WriteValue(pwOrdinal.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetRefTypeInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint hRefType;
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>>();
            hRefType = decoder.ReadUInt32();
            var invokeTask = this._obj.GetRefTypeInfo(hRefType, ppTInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTInfo.value);
            encoder.WriteInterfacePointerBody(ppTInfo.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum15NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum15NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_CreateInstance(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            System.Guid riid;
            RpcPointer<Titanis.DceRpc.TypedObjref<IUnknown>> ppvObj = new RpcPointer<Titanis.DceRpc.TypedObjref<IUnknown>>();
            riid = decoder.ReadUuid();
            var invokeTask = this._obj.CreateInstance(riid, ppvObj, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppvObj.value);
            encoder.WriteInterfacePointerBody(ppvObj.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetMops(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            int memid;
            RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrMops = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
            memid = decoder.ReadInt32();
            var invokeTask = this._obj.GetMops(memid, pBstrMops, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(pBstrMops.value);
            if ((null != pBstrMops.value)) {
                encoder.WriteConformantStruct(pBstrMops.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrMops.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetContainingTypeLib(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeLib>> ppTLib = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeLib>>();
            RpcPointer<uint> pIndex = new RpcPointer<uint>();
            var invokeTask = this._obj.GetContainingTypeLib(ppTLib, pIndex, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTLib.value);
            encoder.WriteInterfacePointerBody(ppTLib.value);
            encoder.WriteValue(pIndex.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum19NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum19NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum20NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum20NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum21NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum21NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [System.Runtime.InteropServices.GuidAttribute("00020412-0000-0000-c000-000000000046")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface ITypeInfo2 : ITypeInfo {
        Task<int> GetTypeKind(RpcPointer<TYPEKIND> pTypeKind, System.Threading.CancellationToken cancellationToken);
        Task<int> GetTypeFlags(RpcPointer<uint> pTypeFlags, System.Threading.CancellationToken cancellationToken);
        Task<int> GetFuncIndexOfMemId(int memid, INVOKEKIND invKind, RpcPointer<uint> pFuncIndex, System.Threading.CancellationToken cancellationToken);
        Task<int> GetVarIndexOfMemId(int memid, RpcPointer<uint> pVarIndex, System.Threading.CancellationToken cancellationToken);
        Task<int> GetCustData(System.Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, System.Threading.CancellationToken cancellationToken);
        Task<int> GetFuncCustData(uint index, System.Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, System.Threading.CancellationToken cancellationToken);
        Task<int> GetParamCustData(uint indexFunc, uint indexParam, System.Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, System.Threading.CancellationToken cancellationToken);
        Task<int> GetVarCustData(uint index, System.Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, System.Threading.CancellationToken cancellationToken);
        Task<int> GetImplTypeCustData(uint index, System.Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, System.Threading.CancellationToken cancellationToken);
        Task<int> GetDocumentation2(int memid, uint lcid, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpString, RpcPointer<uint> pdwHelpStringContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpStringDll, System.Threading.CancellationToken cancellationToken);
        Task<int> GetAllCustData(RpcPointer<CUSTDATA> pCustData, System.Threading.CancellationToken cancellationToken);
        Task<int> GetAllFuncCustData(uint index, RpcPointer<CUSTDATA> pCustData, System.Threading.CancellationToken cancellationToken);
        Task<int> GetAllParamCustData(uint indexFunc, uint indexParam, RpcPointer<CUSTDATA> pCustData, System.Threading.CancellationToken cancellationToken);
        Task<int> GetAllVarCustData(uint index, RpcPointer<CUSTDATA> pCustData, System.Threading.CancellationToken cancellationToken);
        Task<int> GetAllImplTypeCustData(uint index, RpcPointer<CUSTDATA> pCustData, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [Titanis.DceRpc.IidAttribute("00020412-0000-0000-c000-000000000046")]
    public class ITypeInfo2ClientProxy : ITypeInfoClientProxy, ITypeInfo2 {
        private static System.Guid _interfaceUuid = new System.Guid("00020412-0000-0000-c000-000000000046");
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
        public virtual async Task<int> GetTypeKind(RpcPointer<TYPEKIND> pTypeKind, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pTypeKind.value = ((TYPEKIND)(decoder.ReadInt32()));
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetTypeFlags(RpcPointer<uint> pTypeFlags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pTypeFlags.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetFuncIndexOfMemId(int memid, INVOKEKIND invKind, RpcPointer<uint> pFuncIndex, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(memid);
            encoder.WriteValue(((int)(invKind)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pFuncIndex.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetVarIndexOfMemId(int memid, RpcPointer<uint> pVarIndex, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(memid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pVarIndex.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetCustData(System.Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(guid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pVarVal.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarVal.value);
            if ((null != pVarVal.value)) {
                pVarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarVal.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetFuncCustData(uint index, System.Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            encoder.WriteValue(guid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pVarVal.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarVal.value);
            if ((null != pVarVal.value)) {
                pVarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarVal.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetParamCustData(uint indexFunc, uint indexParam, System.Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(indexFunc);
            encoder.WriteValue(indexParam);
            encoder.WriteValue(guid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pVarVal.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarVal.value);
            if ((null != pVarVal.value)) {
                pVarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarVal.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetVarCustData(uint index, System.Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            encoder.WriteValue(guid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pVarVal.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarVal.value);
            if ((null != pVarVal.value)) {
                pVarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarVal.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetImplTypeCustData(uint index, System.Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            encoder.WriteValue(guid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pVarVal.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarVal.value);
            if ((null != pVarVal.value)) {
                pVarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarVal.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetDocumentation2(int memid, uint lcid, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpString, RpcPointer<uint> pdwHelpStringContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpStringDll, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(memid);
            encoder.WriteValue(lcid);
            encoder.WriteValue(refPtrFlags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pbstrHelpString.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pbstrHelpString.value);
            if ((null != pbstrHelpString.value)) {
                pbstrHelpString.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pbstrHelpString.value.value);
            }
            pdwHelpStringContext.value = decoder.ReadUInt32();
            pbstrHelpStringDll.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pbstrHelpStringDll.value);
            if ((null != pbstrHelpStringDll.value)) {
                pbstrHelpStringDll.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pbstrHelpStringDll.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetAllCustData(RpcPointer<CUSTDATA> pCustData, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pCustData.value = decoder.ReadFixedStruct<CUSTDATA>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<CUSTDATA>(ref pCustData.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetAllFuncCustData(uint index, RpcPointer<CUSTDATA> pCustData, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pCustData.value = decoder.ReadFixedStruct<CUSTDATA>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<CUSTDATA>(ref pCustData.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetAllParamCustData(uint indexFunc, uint indexParam, RpcPointer<CUSTDATA> pCustData, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(indexFunc);
            encoder.WriteValue(indexParam);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pCustData.value = decoder.ReadFixedStruct<CUSTDATA>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<CUSTDATA>(ref pCustData.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetAllVarCustData(uint index, RpcPointer<CUSTDATA> pCustData, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pCustData.value = decoder.ReadFixedStruct<CUSTDATA>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<CUSTDATA>(ref pCustData.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetAllImplTypeCustData(uint index, RpcPointer<CUSTDATA> pCustData, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(36);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pCustData.value = decoder.ReadFixedStruct<CUSTDATA>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<CUSTDATA>(ref pCustData.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public class ITypeInfo2Stub : Titanis.DceRpc.Server.RpcObjectStub {
        private static System.Guid _interfaceUuid = new System.Guid("00020412-0000-0000-c000-000000000046");
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
        private ITypeInfo2 _obj;
        public ITypeInfo2Stub(ITypeInfo2 obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_Opnum0NotUsedOnWire,
                    this.Invoke_Opnum1NotUsedOnWire,
                    this.Invoke_Opnum2NotUsedOnWire,
                    this.Invoke_GetTypeAttr,
                    this.Invoke_GetTypeComp,
                    this.Invoke_GetFuncDesc,
                    this.Invoke_GetVarDesc,
                    this.Invoke_GetNames,
                    this.Invoke_GetRefTypeOfImplType,
                    this.Invoke_GetImplTypeFlags,
                    this.Invoke_Opnum10NotUsedOnWire,
                    this.Invoke_Opnum11NotUsedOnWire,
                    this.Invoke_GetDocumentation,
                    this.Invoke_GetDllEntry,
                    this.Invoke_GetRefTypeInfo,
                    this.Invoke_Opnum15NotUsedOnWire,
                    this.Invoke_CreateInstance,
                    this.Invoke_GetMops,
                    this.Invoke_GetContainingTypeLib,
                    this.Invoke_Opnum19NotUsedOnWire,
                    this.Invoke_Opnum20NotUsedOnWire,
                    this.Invoke_Opnum21NotUsedOnWire,
                    this.Invoke_GetTypeKind,
                    this.Invoke_GetTypeFlags,
                    this.Invoke_GetFuncIndexOfMemId,
                    this.Invoke_GetVarIndexOfMemId,
                    this.Invoke_GetCustData,
                    this.Invoke_GetFuncCustData,
                    this.Invoke_GetParamCustData,
                    this.Invoke_GetVarCustData,
                    this.Invoke_GetImplTypeCustData,
                    this.Invoke_GetDocumentation2,
                    this.Invoke_GetAllCustData,
                    this.Invoke_GetAllFuncCustData,
                    this.Invoke_GetAllParamCustData,
                    this.Invoke_GetAllVarCustData,
                    this.Invoke_GetAllImplTypeCustData};
        }
        private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeAttr(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<RpcPointer<TYPEATTR>> ppTypeAttr = new RpcPointer<RpcPointer<TYPEATTR>>();
            RpcPointer<uint> pReserved = new RpcPointer<uint>();
            var invokeTask = this._obj.GetTypeAttr(ppTypeAttr, pReserved, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppTypeAttr.value);
            if ((null != ppTypeAttr.value)) {
                encoder.WriteFixedStruct(ppTypeAttr.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ppTypeAttr.value.value);
            }
            encoder.WriteValue(pReserved.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeComp(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>> ppTComp = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>>();
            var invokeTask = this._obj.GetTypeComp(ppTComp, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTComp.value);
            encoder.WriteInterfacePointerBody(ppTComp.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetFuncDesc(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<RpcPointer<FUNCDESC>> ppFuncDesc = new RpcPointer<RpcPointer<FUNCDESC>>();
            RpcPointer<uint> pReserved = new RpcPointer<uint>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetFuncDesc(index, ppFuncDesc, pReserved, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppFuncDesc.value);
            if ((null != ppFuncDesc.value)) {
                encoder.WriteFixedStruct(ppFuncDesc.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ppFuncDesc.value.value);
            }
            encoder.WriteValue(pReserved.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetVarDesc(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<RpcPointer<VARDESC>> ppVarDesc = new RpcPointer<RpcPointer<VARDESC>>();
            RpcPointer<uint> pReserved = new RpcPointer<uint>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetVarDesc(index, ppVarDesc, pReserved, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppVarDesc.value);
            if ((null != ppVarDesc.value)) {
                encoder.WriteFixedStruct(ppVarDesc.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ppVarDesc.value.value);
            }
            encoder.WriteValue(pReserved.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetNames(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            int memid;
            RpcPointer<ArraySegment<RpcPointer<FLAGGED_WORD_BLOB>>> rgBstrNames = new RpcPointer<ArraySegment<RpcPointer<FLAGGED_WORD_BLOB>>>();
            uint cMaxNames;
            RpcPointer<uint> pcNames = new RpcPointer<uint>();
            memid = decoder.ReadInt32();
            cMaxNames = decoder.ReadUInt32();
            var invokeTask = this._obj.GetNames(memid, rgBstrNames, cMaxNames, pcNames, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(rgBstrNames.value, true);
            for (int i = 0; (i < rgBstrNames.value.Count); i++
            ) {
                RpcPointer<FLAGGED_WORD_BLOB> elem_0 = rgBstrNames.value.Item(i);
                encoder.WritePointer(elem_0);
            }
            for (int i = 0; (i < rgBstrNames.value.Count); i++
            ) {
                RpcPointer<FLAGGED_WORD_BLOB> elem_0 = rgBstrNames.value.Item(i);
                if ((null != elem_0)) {
                    encoder.WriteConformantStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._4Byte);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
            encoder.WriteValue(pcNames.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetRefTypeOfImplType(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<uint> pRefType = new RpcPointer<uint>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetRefTypeOfImplType(index, pRefType, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pRefType.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetImplTypeFlags(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<int> pImplTypeFlags = new RpcPointer<int>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetImplTypeFlags(index, pImplTypeFlags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pImplTypeFlags.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum10NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum10NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum11NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum11NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetDocumentation(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
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
            encoder.WritePointer(pBstrName.value);
            if ((null != pBstrName.value)) {
                encoder.WriteConformantStruct(pBstrName.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrName.value.value);
            }
            encoder.WritePointer(pBstrDocString.value);
            if ((null != pBstrDocString.value)) {
                encoder.WriteConformantStruct(pBstrDocString.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrDocString.value.value);
            }
            encoder.WriteValue(pdwHelpContext.value);
            encoder.WritePointer(pBstrHelpFile.value);
            if ((null != pBstrHelpFile.value)) {
                encoder.WriteConformantStruct(pBstrHelpFile.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrHelpFile.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetDllEntry(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            int memid;
            INVOKEKIND invKind;
            uint refPtrFlags;
            RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDllName = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
            RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
            RpcPointer<ushort> pwOrdinal = new RpcPointer<ushort>();
            memid = decoder.ReadInt32();
            invKind = ((INVOKEKIND)(decoder.ReadInt32()));
            refPtrFlags = decoder.ReadUInt32();
            var invokeTask = this._obj.GetDllEntry(memid, invKind, refPtrFlags, pBstrDllName, pBstrName, pwOrdinal, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(pBstrDllName.value);
            if ((null != pBstrDllName.value)) {
                encoder.WriteConformantStruct(pBstrDllName.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrDllName.value.value);
            }
            encoder.WritePointer(pBstrName.value);
            if ((null != pBstrName.value)) {
                encoder.WriteConformantStruct(pBstrName.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrName.value.value);
            }
            encoder.WriteValue(pwOrdinal.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetRefTypeInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint hRefType;
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>>();
            hRefType = decoder.ReadUInt32();
            var invokeTask = this._obj.GetRefTypeInfo(hRefType, ppTInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTInfo.value);
            encoder.WriteInterfacePointerBody(ppTInfo.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum15NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum15NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_CreateInstance(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            System.Guid riid;
            RpcPointer<Titanis.DceRpc.TypedObjref<IUnknown>> ppvObj = new RpcPointer<Titanis.DceRpc.TypedObjref<IUnknown>>();
            riid = decoder.ReadUuid();
            var invokeTask = this._obj.CreateInstance(riid, ppvObj, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppvObj.value);
            encoder.WriteInterfacePointerBody(ppvObj.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetMops(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            int memid;
            RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrMops = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
            memid = decoder.ReadInt32();
            var invokeTask = this._obj.GetMops(memid, pBstrMops, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(pBstrMops.value);
            if ((null != pBstrMops.value)) {
                encoder.WriteConformantStruct(pBstrMops.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrMops.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetContainingTypeLib(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeLib>> ppTLib = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeLib>>();
            RpcPointer<uint> pIndex = new RpcPointer<uint>();
            var invokeTask = this._obj.GetContainingTypeLib(ppTLib, pIndex, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTLib.value);
            encoder.WriteInterfacePointerBody(ppTLib.value);
            encoder.WriteValue(pIndex.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum19NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum19NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum20NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum20NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum21NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum21NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeKind(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<TYPEKIND> pTypeKind = new RpcPointer<TYPEKIND>();
            var invokeTask = this._obj.GetTypeKind(pTypeKind, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(((int)(pTypeKind.value)));
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeFlags(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<uint> pTypeFlags = new RpcPointer<uint>();
            var invokeTask = this._obj.GetTypeFlags(pTypeFlags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pTypeFlags.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetFuncIndexOfMemId(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            int memid;
            INVOKEKIND invKind;
            RpcPointer<uint> pFuncIndex = new RpcPointer<uint>();
            memid = decoder.ReadInt32();
            invKind = ((INVOKEKIND)(decoder.ReadInt32()));
            var invokeTask = this._obj.GetFuncIndexOfMemId(memid, invKind, pFuncIndex, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pFuncIndex.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetVarIndexOfMemId(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            int memid;
            RpcPointer<uint> pVarIndex = new RpcPointer<uint>();
            memid = decoder.ReadInt32();
            var invokeTask = this._obj.GetVarIndexOfMemId(memid, pVarIndex, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pVarIndex.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetCustData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            System.Guid guid;
            RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal = new RpcPointer<RpcPointer<wireVARIANTStr>>();
            guid = decoder.ReadUuid();
            var invokeTask = this._obj.GetCustData(guid, pVarVal, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(pVarVal.value);
            if ((null != pVarVal.value)) {
                encoder.WriteFixedStruct(pVarVal.value.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(pVarVal.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetFuncCustData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            System.Guid guid;
            RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal = new RpcPointer<RpcPointer<wireVARIANTStr>>();
            index = decoder.ReadUInt32();
            guid = decoder.ReadUuid();
            var invokeTask = this._obj.GetFuncCustData(index, guid, pVarVal, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(pVarVal.value);
            if ((null != pVarVal.value)) {
                encoder.WriteFixedStruct(pVarVal.value.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(pVarVal.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetParamCustData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint indexFunc;
            uint indexParam;
            System.Guid guid;
            RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal = new RpcPointer<RpcPointer<wireVARIANTStr>>();
            indexFunc = decoder.ReadUInt32();
            indexParam = decoder.ReadUInt32();
            guid = decoder.ReadUuid();
            var invokeTask = this._obj.GetParamCustData(indexFunc, indexParam, guid, pVarVal, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(pVarVal.value);
            if ((null != pVarVal.value)) {
                encoder.WriteFixedStruct(pVarVal.value.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(pVarVal.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetVarCustData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            System.Guid guid;
            RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal = new RpcPointer<RpcPointer<wireVARIANTStr>>();
            index = decoder.ReadUInt32();
            guid = decoder.ReadUuid();
            var invokeTask = this._obj.GetVarCustData(index, guid, pVarVal, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(pVarVal.value);
            if ((null != pVarVal.value)) {
                encoder.WriteFixedStruct(pVarVal.value.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(pVarVal.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetImplTypeCustData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            System.Guid guid;
            RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal = new RpcPointer<RpcPointer<wireVARIANTStr>>();
            index = decoder.ReadUInt32();
            guid = decoder.ReadUuid();
            var invokeTask = this._obj.GetImplTypeCustData(index, guid, pVarVal, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(pVarVal.value);
            if ((null != pVarVal.value)) {
                encoder.WriteFixedStruct(pVarVal.value.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(pVarVal.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetDocumentation2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
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
            encoder.WritePointer(pbstrHelpString.value);
            if ((null != pbstrHelpString.value)) {
                encoder.WriteConformantStruct(pbstrHelpString.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pbstrHelpString.value.value);
            }
            encoder.WriteValue(pdwHelpStringContext.value);
            encoder.WritePointer(pbstrHelpStringDll.value);
            if ((null != pbstrHelpStringDll.value)) {
                encoder.WriteConformantStruct(pbstrHelpStringDll.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pbstrHelpStringDll.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetAllCustData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<CUSTDATA> pCustData = new RpcPointer<CUSTDATA>();
            var invokeTask = this._obj.GetAllCustData(pCustData, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(pCustData.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(pCustData.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetAllFuncCustData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<CUSTDATA> pCustData = new RpcPointer<CUSTDATA>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetAllFuncCustData(index, pCustData, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(pCustData.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(pCustData.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetAllParamCustData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint indexFunc;
            uint indexParam;
            RpcPointer<CUSTDATA> pCustData = new RpcPointer<CUSTDATA>();
            indexFunc = decoder.ReadUInt32();
            indexParam = decoder.ReadUInt32();
            var invokeTask = this._obj.GetAllParamCustData(indexFunc, indexParam, pCustData, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(pCustData.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(pCustData.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetAllVarCustData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<CUSTDATA> pCustData = new RpcPointer<CUSTDATA>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetAllVarCustData(index, pCustData, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(pCustData.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(pCustData.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetAllImplTypeCustData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<CUSTDATA> pCustData = new RpcPointer<CUSTDATA>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetAllImplTypeCustData(index, pCustData, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(pCustData.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(pCustData.value);
            encoder.WriteValue(retval);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [System.Runtime.InteropServices.GuidAttribute("00020402-0000-0000-c000-000000000046")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface ITypeLib : IUnknown {
        Task<int> GetTypeInfoCount(RpcPointer<uint> pcTInfo, System.Threading.CancellationToken cancellationToken);
        Task<int> GetTypeInfo(uint index, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo, System.Threading.CancellationToken cancellationToken);
        Task<int> GetTypeInfoType(uint index, RpcPointer<TYPEKIND> pTKind, System.Threading.CancellationToken cancellationToken);
        Task<int> GetTypeInfoOfGuid(System.Guid guid, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo, System.Threading.CancellationToken cancellationToken);
        Task<int> GetLibAttr(RpcPointer<RpcPointer<TLIBATTR>> ppTLibAttr, RpcPointer<uint> pReserved, System.Threading.CancellationToken cancellationToken);
        Task<int> GetTypeComp(RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>> ppTComp, System.Threading.CancellationToken cancellationToken);
        Task<int> GetDocumentation(int index, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDocString, RpcPointer<uint> pdwHelpContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrHelpFile, System.Threading.CancellationToken cancellationToken);
        Task<int> IsName(string szNameBuf, uint lHashVal, RpcPointer<int> pfName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary, System.Threading.CancellationToken cancellationToken);
        Task<int> FindName(string szNameBuf, uint lHashVal, RpcPointer<ArraySegment<Titanis.DceRpc.TypedObjref<ITypeInfo>>> ppTInfo, RpcPointer<ArraySegment<int>> rgMemId, RpcPointer<ushort> pcFound, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary, System.Threading.CancellationToken cancellationToken);
        Task<int> Opnum12NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [Titanis.DceRpc.IidAttribute("00020402-0000-0000-c000-000000000046")]
    public class ITypeLibClientProxy : IUnknownClientProxy, ITypeLib {
        private static System.Guid _interfaceUuid = new System.Guid("00020402-0000-0000-c000-000000000046");
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
        public virtual async Task<int> GetTypeInfoCount(RpcPointer<uint> pcTInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pcTInfo.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetTypeInfo(uint index, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppTInfo.value = decoder.ReadInterfacePointer<ITypeInfo>();
            decoder.ReadInterfacePointer(ppTInfo.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetTypeInfoType(uint index, RpcPointer<TYPEKIND> pTKind, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pTKind.value = ((TYPEKIND)(decoder.ReadInt32()));
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetTypeInfoOfGuid(System.Guid guid, RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(guid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppTInfo.value = decoder.ReadInterfacePointer<ITypeInfo>();
            decoder.ReadInterfacePointer(ppTInfo.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetLibAttr(RpcPointer<RpcPointer<TLIBATTR>> ppTLibAttr, RpcPointer<uint> pReserved, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppTLibAttr.value = decoder.ReadOutUniquePointer<TLIBATTR>(ppTLibAttr.value);
            if ((null != ppTLibAttr.value)) {
                ppTLibAttr.value.value = decoder.ReadFixedStruct<TLIBATTR>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<TLIBATTR>(ref ppTLibAttr.value.value);
            }
            pReserved.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetTypeComp(RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>> ppTComp, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppTComp.value = decoder.ReadInterfacePointer<ITypeComp>();
            decoder.ReadInterfacePointer(ppTComp.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetDocumentation(int index, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrDocString, RpcPointer<uint> pdwHelpContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrHelpFile, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            encoder.WriteValue(refPtrFlags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pBstrName.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrName.value);
            if ((null != pBstrName.value)) {
                pBstrName.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrName.value.value);
            }
            pBstrDocString.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrDocString.value);
            if ((null != pBstrDocString.value)) {
                pBstrDocString.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrDocString.value.value);
            }
            pdwHelpContext.value = decoder.ReadUInt32();
            pBstrHelpFile.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrHelpFile.value);
            if ((null != pBstrHelpFile.value)) {
                pBstrHelpFile.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrHelpFile.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> IsName(string szNameBuf, uint lHashVal, RpcPointer<int> pfName, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(szNameBuf);
            encoder.WriteValue(lHashVal);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pfName.value = decoder.ReadInt32();
            pBstrNameInLibrary.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrNameInLibrary.value);
            if ((null != pBstrNameInLibrary.value)) {
                pBstrNameInLibrary.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrNameInLibrary.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> FindName(string szNameBuf, uint lHashVal, RpcPointer<ArraySegment<Titanis.DceRpc.TypedObjref<ITypeInfo>>> ppTInfo, RpcPointer<ArraySegment<int>> rgMemId, RpcPointer<ushort> pcFound, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(szNameBuf);
            encoder.WriteValue(lHashVal);
            encoder.WriteValue(pcFound.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppTInfo.value = decoder.ReadArraySegmentHeader<Titanis.DceRpc.TypedObjref<ITypeInfo>>();
            for (int i = 0; (i < ppTInfo.value.Count); i++
            ) {
                Titanis.DceRpc.TypedObjref<ITypeInfo> elem_0 = ppTInfo.value.Item(i);
                elem_0 = decoder.ReadInterfacePointer<ITypeInfo>();
                ppTInfo.value.Item(i) = elem_0;
            }
            for (int i = 0; (i < ppTInfo.value.Count); i++
            ) {
                Titanis.DceRpc.TypedObjref<ITypeInfo> elem_0 = ppTInfo.value.Item(i);
                decoder.ReadInterfacePointer(elem_0);
                ppTInfo.value.Item(i) = elem_0;
            }
            rgMemId.value = decoder.ReadArraySegmentHeader<int>();
            for (int i = 0; (i < rgMemId.value.Count); i++
            ) {
                int elem_0 = rgMemId.value.Item(i);
                elem_0 = decoder.ReadInt32();
                rgMemId.value.Item(i) = elem_0;
            }
            pcFound.value = decoder.ReadUInt16();
            pBstrNameInLibrary.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pBstrNameInLibrary.value);
            if ((null != pBstrNameInLibrary.value)) {
                pBstrNameInLibrary.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pBstrNameInLibrary.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Opnum12NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public class ITypeLibStub : Titanis.DceRpc.Server.RpcObjectStub {
        private static System.Guid _interfaceUuid = new System.Guid("00020402-0000-0000-c000-000000000046");
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
        private ITypeLib _obj;
        public ITypeLibStub(ITypeLib obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_Opnum0NotUsedOnWire,
                    this.Invoke_Opnum1NotUsedOnWire,
                    this.Invoke_Opnum2NotUsedOnWire,
                    this.Invoke_GetTypeInfoCount,
                    this.Invoke_GetTypeInfo,
                    this.Invoke_GetTypeInfoType,
                    this.Invoke_GetTypeInfoOfGuid,
                    this.Invoke_GetLibAttr,
                    this.Invoke_GetTypeComp,
                    this.Invoke_GetDocumentation,
                    this.Invoke_IsName,
                    this.Invoke_FindName,
                    this.Invoke_Opnum12NotUsedOnWire};
        }
        private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeInfoCount(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<uint> pcTInfo = new RpcPointer<uint>();
            var invokeTask = this._obj.GetTypeInfoCount(pcTInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pcTInfo.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetTypeInfo(index, ppTInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTInfo.value);
            encoder.WriteInterfacePointerBody(ppTInfo.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeInfoType(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<TYPEKIND> pTKind = new RpcPointer<TYPEKIND>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetTypeInfoType(index, pTKind, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(((int)(pTKind.value)));
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeInfoOfGuid(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            System.Guid guid;
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>>();
            guid = decoder.ReadUuid();
            var invokeTask = this._obj.GetTypeInfoOfGuid(guid, ppTInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTInfo.value);
            encoder.WriteInterfacePointerBody(ppTInfo.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetLibAttr(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<RpcPointer<TLIBATTR>> ppTLibAttr = new RpcPointer<RpcPointer<TLIBATTR>>();
            RpcPointer<uint> pReserved = new RpcPointer<uint>();
            var invokeTask = this._obj.GetLibAttr(ppTLibAttr, pReserved, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppTLibAttr.value);
            if ((null != ppTLibAttr.value)) {
                encoder.WriteFixedStruct(ppTLibAttr.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(ppTLibAttr.value.value);
            }
            encoder.WriteValue(pReserved.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeComp(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>> ppTComp = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>>();
            var invokeTask = this._obj.GetTypeComp(ppTComp, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTComp.value);
            encoder.WriteInterfacePointerBody(ppTComp.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetDocumentation(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
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
            encoder.WritePointer(pBstrName.value);
            if ((null != pBstrName.value)) {
                encoder.WriteConformantStruct(pBstrName.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrName.value.value);
            }
            encoder.WritePointer(pBstrDocString.value);
            if ((null != pBstrDocString.value)) {
                encoder.WriteConformantStruct(pBstrDocString.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrDocString.value.value);
            }
            encoder.WriteValue(pdwHelpContext.value);
            encoder.WritePointer(pBstrHelpFile.value);
            if ((null != pBstrHelpFile.value)) {
                encoder.WriteConformantStruct(pBstrHelpFile.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrHelpFile.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_IsName(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string szNameBuf;
            uint lHashVal;
            RpcPointer<int> pfName = new RpcPointer<int>();
            RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
            szNameBuf = decoder.ReadWideCharString();
            lHashVal = decoder.ReadUInt32();
            var invokeTask = this._obj.IsName(szNameBuf, lHashVal, pfName, pBstrNameInLibrary, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pfName.value);
            encoder.WritePointer(pBstrNameInLibrary.value);
            if ((null != pBstrNameInLibrary.value)) {
                encoder.WriteConformantStruct(pBstrNameInLibrary.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrNameInLibrary.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_FindName(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string szNameBuf;
            uint lHashVal;
            RpcPointer<ArraySegment<Titanis.DceRpc.TypedObjref<ITypeInfo>>> ppTInfo = new RpcPointer<ArraySegment<Titanis.DceRpc.TypedObjref<ITypeInfo>>>();
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
            for (int i = 0; (i < ppTInfo.value.Count); i++
            ) {
                Titanis.DceRpc.TypedObjref<ITypeInfo> elem_0 = ppTInfo.value.Item(i);
                encoder.WriteInterfacePointer(elem_0);
            }
            for (int i = 0; (i < ppTInfo.value.Count); i++
            ) {
                Titanis.DceRpc.TypedObjref<ITypeInfo> elem_0 = ppTInfo.value.Item(i);
                encoder.WriteInterfacePointerBody(elem_0);
            }
            encoder.WriteArrayHeader(rgMemId.value, true);
            for (int i = 0; (i < rgMemId.value.Count); i++
            ) {
                int elem_0 = rgMemId.value.Item(i);
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcFound.value);
            encoder.WritePointer(pBstrNameInLibrary.value);
            if ((null != pBstrNameInLibrary.value)) {
                encoder.WriteConformantStruct(pBstrNameInLibrary.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrNameInLibrary.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum12NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum12NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [System.Runtime.InteropServices.GuidAttribute("00020411-0000-0000-c000-000000000046")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface ITypeLib2 : ITypeLib {
        Task<int> GetCustData(System.Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, System.Threading.CancellationToken cancellationToken);
        Task<int> GetLibStatistics(RpcPointer<uint> pcUniqueNames, RpcPointer<uint> pcchUniqueNames, System.Threading.CancellationToken cancellationToken);
        Task<int> GetDocumentation2(int index, uint lcid, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpString, RpcPointer<uint> pdwHelpStringContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpStringDll, System.Threading.CancellationToken cancellationToken);
        Task<int> GetAllCustData(RpcPointer<CUSTDATA> pCustData, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [Titanis.DceRpc.IidAttribute("00020411-0000-0000-c000-000000000046")]
    public class ITypeLib2ClientProxy : ITypeLibClientProxy, ITypeLib2 {
        private static System.Guid _interfaceUuid = new System.Guid("00020411-0000-0000-c000-000000000046");
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
        public virtual async Task<int> GetCustData(System.Guid guid, RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(guid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pVarVal.value = decoder.ReadOutUniquePointer<wireVARIANTStr>(pVarVal.value);
            if ((null != pVarVal.value)) {
                pVarVal.value.value = decoder.ReadFixedStruct<wireVARIANTStr>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<wireVARIANTStr>(ref pVarVal.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetLibStatistics(RpcPointer<uint> pcUniqueNames, RpcPointer<uint> pcchUniqueNames, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pcUniqueNames.value = decoder.ReadUInt32();
            pcchUniqueNames.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetDocumentation2(int index, uint lcid, uint refPtrFlags, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpString, RpcPointer<uint> pdwHelpStringContext, RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pbstrHelpStringDll, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(index);
            encoder.WriteValue(lcid);
            encoder.WriteValue(refPtrFlags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pbstrHelpString.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pbstrHelpString.value);
            if ((null != pbstrHelpString.value)) {
                pbstrHelpString.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pbstrHelpString.value.value);
            }
            pdwHelpStringContext.value = decoder.ReadUInt32();
            pbstrHelpStringDll.value = decoder.ReadOutUniquePointer<FLAGGED_WORD_BLOB>(pbstrHelpStringDll.value);
            if ((null != pbstrHelpStringDll.value)) {
                pbstrHelpStringDll.value.value = decoder.ReadConformantStruct<FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<FLAGGED_WORD_BLOB>(ref pbstrHelpStringDll.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> GetAllCustData(RpcPointer<CUSTDATA> pCustData, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pCustData.value = decoder.ReadFixedStruct<CUSTDATA>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<CUSTDATA>(ref pCustData.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public class ITypeLib2Stub : Titanis.DceRpc.Server.RpcObjectStub {
        private static System.Guid _interfaceUuid = new System.Guid("00020411-0000-0000-c000-000000000046");
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
        private ITypeLib2 _obj;
        public ITypeLib2Stub(ITypeLib2 obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_Opnum0NotUsedOnWire,
                    this.Invoke_Opnum1NotUsedOnWire,
                    this.Invoke_Opnum2NotUsedOnWire,
                    this.Invoke_GetTypeInfoCount,
                    this.Invoke_GetTypeInfo,
                    this.Invoke_GetTypeInfoType,
                    this.Invoke_GetTypeInfoOfGuid,
                    this.Invoke_GetLibAttr,
                    this.Invoke_GetTypeComp,
                    this.Invoke_GetDocumentation,
                    this.Invoke_IsName,
                    this.Invoke_FindName,
                    this.Invoke_Opnum12NotUsedOnWire,
                    this.Invoke_GetCustData,
                    this.Invoke_GetLibStatistics,
                    this.Invoke_GetDocumentation2,
                    this.Invoke_GetAllCustData};
        }
        private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeInfoCount(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<uint> pcTInfo = new RpcPointer<uint>();
            var invokeTask = this._obj.GetTypeInfoCount(pcTInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pcTInfo.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetTypeInfo(index, ppTInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTInfo.value);
            encoder.WriteInterfacePointerBody(ppTInfo.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeInfoType(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint index;
            RpcPointer<TYPEKIND> pTKind = new RpcPointer<TYPEKIND>();
            index = decoder.ReadUInt32();
            var invokeTask = this._obj.GetTypeInfoType(index, pTKind, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(((int)(pTKind.value)));
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeInfoOfGuid(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            System.Guid guid;
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>> ppTInfo = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeInfo>>();
            guid = decoder.ReadUuid();
            var invokeTask = this._obj.GetTypeInfoOfGuid(guid, ppTInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTInfo.value);
            encoder.WriteInterfacePointerBody(ppTInfo.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetLibAttr(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<RpcPointer<TLIBATTR>> ppTLibAttr = new RpcPointer<RpcPointer<TLIBATTR>>();
            RpcPointer<uint> pReserved = new RpcPointer<uint>();
            var invokeTask = this._obj.GetLibAttr(ppTLibAttr, pReserved, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppTLibAttr.value);
            if ((null != ppTLibAttr.value)) {
                encoder.WriteFixedStruct(ppTLibAttr.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(ppTLibAttr.value.value);
            }
            encoder.WriteValue(pReserved.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetTypeComp(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>> ppTComp = new RpcPointer<Titanis.DceRpc.TypedObjref<ITypeComp>>();
            var invokeTask = this._obj.GetTypeComp(ppTComp, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteInterfacePointer(ppTComp.value);
            encoder.WriteInterfacePointerBody(ppTComp.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetDocumentation(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
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
            encoder.WritePointer(pBstrName.value);
            if ((null != pBstrName.value)) {
                encoder.WriteConformantStruct(pBstrName.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrName.value.value);
            }
            encoder.WritePointer(pBstrDocString.value);
            if ((null != pBstrDocString.value)) {
                encoder.WriteConformantStruct(pBstrDocString.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrDocString.value.value);
            }
            encoder.WriteValue(pdwHelpContext.value);
            encoder.WritePointer(pBstrHelpFile.value);
            if ((null != pBstrHelpFile.value)) {
                encoder.WriteConformantStruct(pBstrHelpFile.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrHelpFile.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_IsName(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string szNameBuf;
            uint lHashVal;
            RpcPointer<int> pfName = new RpcPointer<int>();
            RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>> pBstrNameInLibrary = new RpcPointer<RpcPointer<FLAGGED_WORD_BLOB>>();
            szNameBuf = decoder.ReadWideCharString();
            lHashVal = decoder.ReadUInt32();
            var invokeTask = this._obj.IsName(szNameBuf, lHashVal, pfName, pBstrNameInLibrary, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pfName.value);
            encoder.WritePointer(pBstrNameInLibrary.value);
            if ((null != pBstrNameInLibrary.value)) {
                encoder.WriteConformantStruct(pBstrNameInLibrary.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrNameInLibrary.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_FindName(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string szNameBuf;
            uint lHashVal;
            RpcPointer<ArraySegment<Titanis.DceRpc.TypedObjref<ITypeInfo>>> ppTInfo = new RpcPointer<ArraySegment<Titanis.DceRpc.TypedObjref<ITypeInfo>>>();
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
            for (int i = 0; (i < ppTInfo.value.Count); i++
            ) {
                Titanis.DceRpc.TypedObjref<ITypeInfo> elem_0 = ppTInfo.value.Item(i);
                encoder.WriteInterfacePointer(elem_0);
            }
            for (int i = 0; (i < ppTInfo.value.Count); i++
            ) {
                Titanis.DceRpc.TypedObjref<ITypeInfo> elem_0 = ppTInfo.value.Item(i);
                encoder.WriteInterfacePointerBody(elem_0);
            }
            encoder.WriteArrayHeader(rgMemId.value, true);
            for (int i = 0; (i < rgMemId.value.Count); i++
            ) {
                int elem_0 = rgMemId.value.Item(i);
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcFound.value);
            encoder.WritePointer(pBstrNameInLibrary.value);
            if ((null != pBstrNameInLibrary.value)) {
                encoder.WriteConformantStruct(pBstrNameInLibrary.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pBstrNameInLibrary.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum12NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum12NotUsedOnWire(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetCustData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            System.Guid guid;
            RpcPointer<RpcPointer<wireVARIANTStr>> pVarVal = new RpcPointer<RpcPointer<wireVARIANTStr>>();
            guid = decoder.ReadUuid();
            var invokeTask = this._obj.GetCustData(guid, pVarVal, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(pVarVal.value);
            if ((null != pVarVal.value)) {
                encoder.WriteFixedStruct(pVarVal.value.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(pVarVal.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetLibStatistics(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<uint> pcUniqueNames = new RpcPointer<uint>();
            RpcPointer<uint> pcchUniqueNames = new RpcPointer<uint>();
            var invokeTask = this._obj.GetLibStatistics(pcUniqueNames, pcchUniqueNames, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pcUniqueNames.value);
            encoder.WriteValue(pcchUniqueNames.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetDocumentation2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
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
            encoder.WritePointer(pbstrHelpString.value);
            if ((null != pbstrHelpString.value)) {
                encoder.WriteConformantStruct(pbstrHelpString.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pbstrHelpString.value.value);
            }
            encoder.WriteValue(pdwHelpStringContext.value);
            encoder.WritePointer(pbstrHelpStringDll.value);
            if ((null != pbstrHelpStringDll.value)) {
                encoder.WriteConformantStruct(pbstrHelpStringDll.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pbstrHelpStringDll.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_GetAllCustData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<CUSTDATA> pCustData = new RpcPointer<CUSTDATA>();
            var invokeTask = this._obj.GetAllCustData(pCustData, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(pCustData.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(pCustData.value);
            encoder.WriteValue(retval);
        }
    }
}
