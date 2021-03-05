#pragma warning disable

namespace ms_dcom {
    using System;
    using System.Threading.Tasks;
    using Titanis;
    using Titanis.DceRpc;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct ORPC_EXTENT : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.id);
            encoder.WriteValue(this.size);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.id = decoder.ReadUuid();
            this.size = decoder.ReadUInt32();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.data);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.data = decoder.ReadArrayHeader<byte>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.data.Length); i++
            ) {
                byte elem_0 = this.data[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.data.Length); i++
            ) {
                byte elem_0 = this.data[i];
                elem_0 = decoder.ReadByte();
                this.data[i] = elem_0;
            }
        }
        public System.Guid id;
        public uint size;
        public byte[] data;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct ORPC_EXTENT_ARRAY : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.size);
            encoder.WriteValue(this.reserved);
            encoder.WritePointer(this.extent);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.size = decoder.ReadUInt32();
            this.reserved = decoder.ReadUInt32();
            this.extent = decoder.ReadPointer<RpcPointer<ORPC_EXTENT>[]>();
        }
        public uint size;
        public uint reserved;
        public RpcPointer<RpcPointer<ORPC_EXTENT>[]> extent;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.extent)) {
                encoder.WriteArrayHeader(this.extent.value);
                for (int i = 0; (i < this.extent.value.Length); i++
                ) {
                    RpcPointer<ORPC_EXTENT> elem_0 = this.extent.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < this.extent.value.Length); i++
                ) {
                    RpcPointer<ORPC_EXTENT> elem_0 = this.extent.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteConformantStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._4Byte);
                        encoder.WriteStructDeferral(elem_0.value);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.extent)) {
                this.extent.value = decoder.ReadArrayHeader<RpcPointer<ORPC_EXTENT>>();
                for (int i = 0; (i < this.extent.value.Length); i++
                ) {
                    RpcPointer<ORPC_EXTENT> elem_0 = this.extent.value[i];
                    elem_0 = decoder.ReadPointer<ORPC_EXTENT>();
                    this.extent.value[i] = elem_0;
                }
                for (int i = 0; (i < this.extent.value.Length); i++
                ) {
                    RpcPointer<ORPC_EXTENT> elem_0 = this.extent.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadConformantStruct<ORPC_EXTENT>(Titanis.DceRpc.NdrAlignment._4Byte);
                        decoder.ReadStructDeferral<ORPC_EXTENT>(ref elem_0.value);
                    }
                    this.extent.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct ORPCTHIS : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.version, Titanis.DceRpc.NdrAlignment._2Byte);
            encoder.WriteValue(this.flags);
            encoder.WriteValue(this.reserved1);
            encoder.WriteValue(this.cid);
            encoder.WritePointer(this.extensions);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.version = decoder.ReadFixedStruct<COMVERSION>(Titanis.DceRpc.NdrAlignment._2Byte);
            this.flags = decoder.ReadUInt32();
            this.reserved1 = decoder.ReadUInt32();
            this.cid = decoder.ReadUuid();
            this.extensions = decoder.ReadPointer<ORPC_EXTENT_ARRAY>();
        }
        public COMVERSION version;
        public uint flags;
        public uint reserved1;
        public System.Guid cid;
        public RpcPointer<ORPC_EXTENT_ARRAY> extensions;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.version);
            if ((null != this.extensions)) {
                encoder.WriteFixedStruct(this.extensions.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.extensions.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<COMVERSION>(ref this.version);
            if ((null != this.extensions)) {
                this.extensions.value = decoder.ReadFixedStruct<ORPC_EXTENT_ARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ORPC_EXTENT_ARRAY>(ref this.extensions.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct ORPCTHAT : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.flags);
            encoder.WritePointer(this.extensions);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.flags = decoder.ReadUInt32();
            this.extensions = decoder.ReadPointer<ORPC_EXTENT_ARRAY>();
        }
        public uint flags;
        public RpcPointer<ORPC_EXTENT_ARRAY> extensions;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.extensions)) {
                encoder.WriteFixedStruct(this.extensions.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.extensions.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.extensions)) {
                this.extensions.value = decoder.ReadFixedStruct<ORPC_EXTENT_ARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ORPC_EXTENT_ARRAY>(ref this.extensions.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct DUALSTRINGARRAY : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.wNumEntries);
            encoder.WriteValue(this.wSecurityOffset);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.wNumEntries = decoder.ReadUInt16();
            this.wSecurityOffset = decoder.ReadUInt16();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.aStringArray);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.aStringArray = decoder.ReadArrayHeader<ushort>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.aStringArray.Length); i++
            ) {
                ushort elem_0 = this.aStringArray[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.aStringArray.Length); i++
            ) {
                ushort elem_0 = this.aStringArray[i];
                elem_0 = decoder.ReadUInt16();
                this.aStringArray[i] = elem_0;
            }
        }
        public ushort wNumEntries;
        public ushort wSecurityOffset;
        public ushort[] aStringArray;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public enum tagCPFLAGS : int {
        CPFLAG_PROPAGATE = 1,
        CPFLAG_EXPOSE = 2,
        CPFLAG_ENVOY = 4,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct MInterfacePointer : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ulCntData);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ulCntData = decoder.ReadUInt32();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.abData);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.abData = decoder.ReadArrayHeader<byte>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.abData.Length); i++
            ) {
                byte elem_0 = this.abData[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.abData.Length); i++
            ) {
                byte elem_0 = this.abData[i];
                elem_0 = decoder.ReadByte();
                this.abData[i] = elem_0;
            }
        }
        public uint ulCntData;
        public byte[] abData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct ErrorObjectData : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwVersion);
            encoder.WriteValue(this.dwHelpContext);
            encoder.WriteValue(this.iid);
            encoder.WritePointer(this.pszSource);
            encoder.WritePointer(this.pszDescription);
            encoder.WritePointer(this.pszHelpFile);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwVersion = decoder.ReadUInt32();
            this.dwHelpContext = decoder.ReadUInt32();
            this.iid = decoder.ReadUuid();
            this.pszSource = decoder.ReadPointer<string>();
            this.pszDescription = decoder.ReadPointer<string>();
            this.pszHelpFile = decoder.ReadPointer<string>();
        }
        public uint dwVersion;
        public uint dwHelpContext;
        public System.Guid iid;
        public RpcPointer<string> pszSource;
        public RpcPointer<string> pszDescription;
        public RpcPointer<string> pszHelpFile;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pszSource)) {
                encoder.WriteWideCharString(this.pszSource.value);
            }
            if ((null != this.pszDescription)) {
                encoder.WriteWideCharString(this.pszDescription.value);
            }
            if ((null != this.pszHelpFile)) {
                encoder.WriteWideCharString(this.pszHelpFile.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pszSource)) {
                this.pszSource.value = decoder.ReadWideCharString();
            }
            if ((null != this.pszDescription)) {
                this.pszDescription.value = decoder.ReadWideCharString();
            }
            if ((null != this.pszHelpFile)) {
                this.pszHelpFile.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct STDOBJREF : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.flags);
            encoder.WriteValue(this.cPublicRefs);
            encoder.WriteValue(this.oxid);
            encoder.WriteValue(this.oid);
            encoder.WriteValue(this.ipid);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.flags = decoder.ReadUInt32();
            this.cPublicRefs = decoder.ReadUInt32();
            this.oxid = decoder.ReadUInt64();
            this.oid = decoder.ReadUInt64();
            this.ipid = decoder.ReadUuid();
        }
        public uint flags;
        public uint cPublicRefs;
        public ulong oxid;
        public ulong oid;
        public System.Guid ipid;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct REMQIRESULT : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.hResult);
            encoder.WriteFixedStruct(this.std, Titanis.DceRpc.NdrAlignment._8Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.hResult = decoder.ReadInt32();
            this.std = decoder.ReadFixedStruct<STDOBJREF>(Titanis.DceRpc.NdrAlignment._8Byte);
        }
        public int hResult;
        public STDOBJREF std;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.std);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<STDOBJREF>(ref this.std);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct REMINTERFACEREF : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ipid);
            encoder.WriteValue(this.cPublicRefs);
            encoder.WriteValue(this.cPrivateRefs);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ipid = decoder.ReadUuid();
            this.cPublicRefs = decoder.ReadUInt32();
            this.cPrivateRefs = decoder.ReadUInt32();
        }
        public System.Guid ipid;
        public uint cPublicRefs;
        public uint cPrivateRefs;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct COSERVERINFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwReserved1);
            encoder.WritePointer(this.pwszName);
            encoder.WritePointer(this.pdwReserved);
            encoder.WriteValue(this.dwReserved2);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwReserved1 = decoder.ReadUInt32();
            this.pwszName = decoder.ReadPointer<string>();
            this.pdwReserved = decoder.ReadPointer<uint>();
            this.dwReserved2 = decoder.ReadUInt32();
        }
        public uint dwReserved1;
        public RpcPointer<string> pwszName;
        public RpcPointer<uint> pdwReserved;
        public uint dwReserved2;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pwszName)) {
                encoder.WriteWideCharString(this.pwszName.value);
            }
            if ((null != this.pdwReserved)) {
                encoder.WriteValue(this.pdwReserved.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pwszName)) {
                this.pwszName.value = decoder.ReadWideCharString();
            }
            if ((null != this.pdwReserved)) {
                this.pdwReserved.value = decoder.ReadUInt32();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct customREMOTE_REQUEST_SCM_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ClientImpLevel);
            encoder.WriteValue(this.cRequestedProtseqs);
            encoder.WritePointer(this.pRequestedProtseqs);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ClientImpLevel = decoder.ReadUInt32();
            this.cRequestedProtseqs = decoder.ReadUInt16();
            this.pRequestedProtseqs = decoder.ReadPointer<ushort[]>();
        }
        public uint ClientImpLevel;
        public ushort cRequestedProtseqs;
        public RpcPointer<ushort[]> pRequestedProtseqs;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pRequestedProtseqs)) {
                encoder.WriteArrayHeader(this.pRequestedProtseqs.value);
                for (int i = 0; (i < this.pRequestedProtseqs.value.Length); i++
                ) {
                    ushort elem_0 = this.pRequestedProtseqs.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pRequestedProtseqs)) {
                this.pRequestedProtseqs.value = decoder.ReadArrayHeader<ushort>();
                for (int i = 0; (i < this.pRequestedProtseqs.value.Length); i++
                ) {
                    ushort elem_0 = this.pRequestedProtseqs.value[i];
                    elem_0 = decoder.ReadUInt16();
                    this.pRequestedProtseqs.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct customREMOTE_REPLY_SCM_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Oxid);
            encoder.WritePointer(this.pdsaOxidBindings);
            encoder.WriteValue(this.ipidRemUnknown);
            encoder.WriteValue(this.authnHint);
            encoder.WriteFixedStruct(this.serverVersion, Titanis.DceRpc.NdrAlignment._2Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Oxid = decoder.ReadUInt64();
            this.pdsaOxidBindings = decoder.ReadPointer<DUALSTRINGARRAY>();
            this.ipidRemUnknown = decoder.ReadUuid();
            this.authnHint = decoder.ReadUInt32();
            this.serverVersion = decoder.ReadFixedStruct<COMVERSION>(Titanis.DceRpc.NdrAlignment._2Byte);
        }
        public ulong Oxid;
        public RpcPointer<DUALSTRINGARRAY> pdsaOxidBindings;
        public System.Guid ipidRemUnknown;
        public uint authnHint;
        public COMVERSION serverVersion;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pdsaOxidBindings)) {
                encoder.WriteConformantStruct(this.pdsaOxidBindings.value, Titanis.DceRpc.NdrAlignment._2Byte);
                encoder.WriteStructDeferral(this.pdsaOxidBindings.value);
            }
            encoder.WriteStructDeferral(this.serverVersion);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pdsaOxidBindings)) {
                this.pdsaOxidBindings.value = decoder.ReadConformantStruct<DUALSTRINGARRAY>(Titanis.DceRpc.NdrAlignment._2Byte);
                decoder.ReadStructDeferral<DUALSTRINGARRAY>(ref this.pdsaOxidBindings.value);
            }
            decoder.ReadStructDeferral<COMVERSION>(ref this.serverVersion);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct InstantiationInfoData : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.classId);
            encoder.WriteValue(this.classCtx);
            encoder.WriteValue(this.actvflags);
            encoder.WriteValue(this.fIsSurrogate);
            encoder.WriteValue(this.cIID);
            encoder.WriteValue(this.instFlag);
            encoder.WritePointer(this.pIID);
            encoder.WriteValue(this.thisSize);
            encoder.WriteFixedStruct(this.clientCOMVersion, Titanis.DceRpc.NdrAlignment._2Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.classId = decoder.ReadUuid();
            this.classCtx = decoder.ReadUInt32();
            this.actvflags = decoder.ReadUInt32();
            this.fIsSurrogate = decoder.ReadInt32();
            this.cIID = decoder.ReadUInt32();
            this.instFlag = decoder.ReadUInt32();
            this.pIID = decoder.ReadPointer<System.Guid[]>();
            this.thisSize = decoder.ReadUInt32();
            this.clientCOMVersion = decoder.ReadFixedStruct<COMVERSION>(Titanis.DceRpc.NdrAlignment._2Byte);
        }
        public System.Guid classId;
        public uint classCtx;
        public uint actvflags;
        public int fIsSurrogate;
        public uint cIID;
        public uint instFlag;
        public RpcPointer<System.Guid[]> pIID;
        public uint thisSize;
        public COMVERSION clientCOMVersion;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pIID)) {
                encoder.WriteArrayHeader(this.pIID.value);
                for (int i = 0; (i < this.pIID.value.Length); i++
                ) {
                    System.Guid elem_0 = this.pIID.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteStructDeferral(this.clientCOMVersion);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pIID)) {
                this.pIID.value = decoder.ReadArrayHeader<System.Guid>();
                for (int i = 0; (i < this.pIID.value.Length); i++
                ) {
                    System.Guid elem_0 = this.pIID.value[i];
                    elem_0 = decoder.ReadUuid();
                    this.pIID.value[i] = elem_0;
                }
            }
            decoder.ReadStructDeferral<COMVERSION>(ref this.clientCOMVersion);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct LocationInfoData : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.machineName);
            encoder.WriteValue(this.processId);
            encoder.WriteValue(this.apartmentId);
            encoder.WriteValue(this.contextId);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.machineName = decoder.ReadPointer<string>();
            this.processId = decoder.ReadUInt32();
            this.apartmentId = decoder.ReadUInt32();
            this.contextId = decoder.ReadUInt32();
        }
        public RpcPointer<string> machineName;
        public uint processId;
        public uint apartmentId;
        public uint contextId;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.machineName)) {
                encoder.WriteWideCharString(this.machineName.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.machineName)) {
                this.machineName.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct ActivationContextInfoData : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.clientOK);
            encoder.WriteValue(this.bReserved1);
            encoder.WriteValue(this.dwReserved1);
            encoder.WriteValue(this.dwReserved2);
            encoder.WritePointer(this.pIFDClientCtx);
            encoder.WritePointer(this.pIFDPrototypeCtx);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.clientOK = decoder.ReadInt32();
            this.bReserved1 = decoder.ReadInt32();
            this.dwReserved1 = decoder.ReadUInt32();
            this.dwReserved2 = decoder.ReadUInt32();
            this.pIFDClientCtx = decoder.ReadPointer<MInterfacePointer>();
            this.pIFDPrototypeCtx = decoder.ReadPointer<MInterfacePointer>();
        }
        public int clientOK;
        public int bReserved1;
        public uint dwReserved1;
        public uint dwReserved2;
        public RpcPointer<MInterfacePointer> pIFDClientCtx;
        public RpcPointer<MInterfacePointer> pIFDPrototypeCtx;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pIFDClientCtx)) {
                encoder.WriteConformantStruct(this.pIFDClientCtx.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.pIFDClientCtx.value);
            }
            if ((null != this.pIFDPrototypeCtx)) {
                encoder.WriteConformantStruct(this.pIFDPrototypeCtx.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.pIFDPrototypeCtx.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pIFDClientCtx)) {
                this.pIFDClientCtx.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<MInterfacePointer>(ref this.pIFDClientCtx.value);
            }
            if ((null != this.pIFDPrototypeCtx)) {
                this.pIFDPrototypeCtx.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<MInterfacePointer>(ref this.pIFDPrototypeCtx.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct CustomHeader : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.totalSize);
            encoder.WriteValue(this.headerSize);
            encoder.WriteValue(this.dwReserved);
            encoder.WriteValue(this.destCtx);
            encoder.WriteValue(this.cIfs);
            encoder.WriteValue(this.classInfoClsid);
            encoder.WritePointer(this.pclsid);
            encoder.WritePointer(this.pSizes);
            encoder.WritePointer(this.pdwReserved);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.totalSize = decoder.ReadUInt32();
            this.headerSize = decoder.ReadUInt32();
            this.dwReserved = decoder.ReadUInt32();
            this.destCtx = decoder.ReadUInt32();
            this.cIfs = decoder.ReadUInt32();
            this.classInfoClsid = decoder.ReadUuid();
            this.pclsid = decoder.ReadPointer<System.Guid[]>();
            this.pSizes = decoder.ReadPointer<uint[]>();
            this.pdwReserved = decoder.ReadPointer<uint>();
        }
        public uint totalSize;
        public uint headerSize;
        public uint dwReserved;
        public uint destCtx;
        public uint cIfs;
        public System.Guid classInfoClsid;
        public RpcPointer<System.Guid[]> pclsid;
        public RpcPointer<uint[]> pSizes;
        public RpcPointer<uint> pdwReserved;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pclsid)) {
                encoder.WriteArrayHeader(this.pclsid.value);
                for (int i = 0; (i < this.pclsid.value.Length); i++
                ) {
                    System.Guid elem_0 = this.pclsid.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            if ((null != this.pSizes)) {
                encoder.WriteArrayHeader(this.pSizes.value);
                for (int i = 0; (i < this.pSizes.value.Length); i++
                ) {
                    uint elem_0 = this.pSizes.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            if ((null != this.pdwReserved)) {
                encoder.WriteValue(this.pdwReserved.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pclsid)) {
                this.pclsid.value = decoder.ReadArrayHeader<System.Guid>();
                for (int i = 0; (i < this.pclsid.value.Length); i++
                ) {
                    System.Guid elem_0 = this.pclsid.value[i];
                    elem_0 = decoder.ReadUuid();
                    this.pclsid.value[i] = elem_0;
                }
            }
            if ((null != this.pSizes)) {
                this.pSizes.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < this.pSizes.value.Length); i++
                ) {
                    uint elem_0 = this.pSizes.value[i];
                    elem_0 = decoder.ReadUInt32();
                    this.pSizes.value[i] = elem_0;
                }
            }
            if ((null != this.pdwReserved)) {
                this.pdwReserved.value = decoder.ReadUInt32();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct PropsOutInfo : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cIfs);
            encoder.WritePointer(this.piid);
            encoder.WritePointer(this.phresults);
            encoder.WritePointer(this.ppIntfData);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cIfs = decoder.ReadUInt32();
            this.piid = decoder.ReadPointer<System.Guid[]>();
            this.phresults = decoder.ReadPointer<int[]>();
            this.ppIntfData = decoder.ReadPointer<RpcPointer<MInterfacePointer>[]>();
        }
        public uint cIfs;
        public RpcPointer<System.Guid[]> piid;
        public RpcPointer<int[]> phresults;
        public RpcPointer<RpcPointer<MInterfacePointer>[]> ppIntfData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.piid)) {
                encoder.WriteArrayHeader(this.piid.value);
                for (int i = 0; (i < this.piid.value.Length); i++
                ) {
                    System.Guid elem_0 = this.piid.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            if ((null != this.phresults)) {
                encoder.WriteArrayHeader(this.phresults.value);
                for (int i = 0; (i < this.phresults.value.Length); i++
                ) {
                    int elem_0 = this.phresults.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            if ((null != this.ppIntfData)) {
                encoder.WriteArrayHeader(this.ppIntfData.value);
                for (int i = 0; (i < this.ppIntfData.value.Length); i++
                ) {
                    RpcPointer<MInterfacePointer> elem_0 = this.ppIntfData.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < this.ppIntfData.value.Length); i++
                ) {
                    RpcPointer<MInterfacePointer> elem_0 = this.ppIntfData.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteConformantStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._4Byte);
                        encoder.WriteStructDeferral(elem_0.value);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.piid)) {
                this.piid.value = decoder.ReadArrayHeader<System.Guid>();
                for (int i = 0; (i < this.piid.value.Length); i++
                ) {
                    System.Guid elem_0 = this.piid.value[i];
                    elem_0 = decoder.ReadUuid();
                    this.piid.value[i] = elem_0;
                }
            }
            if ((null != this.phresults)) {
                this.phresults.value = decoder.ReadArrayHeader<int>();
                for (int i = 0; (i < this.phresults.value.Length); i++
                ) {
                    int elem_0 = this.phresults.value[i];
                    elem_0 = decoder.ReadInt32();
                    this.phresults.value[i] = elem_0;
                }
            }
            if ((null != this.ppIntfData)) {
                this.ppIntfData.value = decoder.ReadArrayHeader<RpcPointer<MInterfacePointer>>();
                for (int i = 0; (i < this.ppIntfData.value.Length); i++
                ) {
                    RpcPointer<MInterfacePointer> elem_0 = this.ppIntfData.value[i];
                    elem_0 = decoder.ReadPointer<MInterfacePointer>();
                    this.ppIntfData.value[i] = elem_0;
                }
                for (int i = 0; (i < this.ppIntfData.value.Length); i++
                ) {
                    RpcPointer<MInterfacePointer> elem_0 = this.ppIntfData.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                        decoder.ReadStructDeferral<MInterfacePointer>(ref elem_0.value);
                    }
                    this.ppIntfData.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct SecurityInfoData : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwAuthnFlags);
            encoder.WritePointer(this.pServerInfo);
            encoder.WritePointer(this.pdwReserved);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwAuthnFlags = decoder.ReadUInt32();
            this.pServerInfo = decoder.ReadPointer<COSERVERINFO>();
            this.pdwReserved = decoder.ReadPointer<uint>();
        }
        public uint dwAuthnFlags;
        public RpcPointer<COSERVERINFO> pServerInfo;
        public RpcPointer<uint> pdwReserved;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pServerInfo)) {
                encoder.WriteFixedStruct(this.pServerInfo.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.pServerInfo.value);
            }
            if ((null != this.pdwReserved)) {
                encoder.WriteValue(this.pdwReserved.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pServerInfo)) {
                this.pServerInfo.value = decoder.ReadFixedStruct<COSERVERINFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<COSERVERINFO>(ref this.pServerInfo.value);
            }
            if ((null != this.pdwReserved)) {
                this.pdwReserved.value = decoder.ReadUInt32();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct ScmRequestInfoData : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.pdwReserved);
            encoder.WritePointer(this.remoteRequest);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.pdwReserved = decoder.ReadPointer<uint>();
            this.remoteRequest = decoder.ReadPointer<customREMOTE_REQUEST_SCM_INFO>();
        }
        public RpcPointer<uint> pdwReserved;
        public RpcPointer<customREMOTE_REQUEST_SCM_INFO> remoteRequest;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pdwReserved)) {
                encoder.WriteValue(this.pdwReserved.value);
            }
            if ((null != this.remoteRequest)) {
                encoder.WriteFixedStruct(this.remoteRequest.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.remoteRequest.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pdwReserved)) {
                this.pdwReserved.value = decoder.ReadUInt32();
            }
            if ((null != this.remoteRequest)) {
                this.remoteRequest.value = decoder.ReadFixedStruct<customREMOTE_REQUEST_SCM_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<customREMOTE_REQUEST_SCM_INFO>(ref this.remoteRequest.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct ScmReplyInfoData : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.pdwReserved);
            encoder.WritePointer(this.remoteReply);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.pdwReserved = decoder.ReadPointer<uint>();
            this.remoteReply = decoder.ReadPointer<customREMOTE_REPLY_SCM_INFO>();
        }
        public RpcPointer<uint> pdwReserved;
        public RpcPointer<customREMOTE_REPLY_SCM_INFO> remoteReply;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pdwReserved)) {
                encoder.WriteValue(this.pdwReserved.value);
            }
            if ((null != this.remoteReply)) {
                encoder.WriteFixedStruct(this.remoteReply.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(this.remoteReply.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pdwReserved)) {
                this.pdwReserved.value = decoder.ReadUInt32();
            }
            if ((null != this.remoteReply)) {
                this.remoteReply.value = decoder.ReadFixedStruct<customREMOTE_REPLY_SCM_INFO>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<customREMOTE_REPLY_SCM_INFO>(ref this.remoteReply.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct InstanceInfoData : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.fileName);
            encoder.WriteValue(this.mode);
            encoder.WritePointer(this.ifdROT);
            encoder.WritePointer(this.ifdStg);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.fileName = decoder.ReadPointer<string>();
            this.mode = decoder.ReadUInt32();
            this.ifdROT = decoder.ReadPointer<MInterfacePointer>();
            this.ifdStg = decoder.ReadPointer<MInterfacePointer>();
        }
        public RpcPointer<string> fileName;
        public uint mode;
        public RpcPointer<MInterfacePointer> ifdROT;
        public RpcPointer<MInterfacePointer> ifdStg;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.fileName)) {
                encoder.WriteWideCharString(this.fileName.value);
            }
            if ((null != this.ifdROT)) {
                encoder.WriteConformantStruct(this.ifdROT.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.ifdROT.value);
            }
            if ((null != this.ifdStg)) {
                encoder.WriteConformantStruct(this.ifdStg.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.ifdStg.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.fileName)) {
                this.fileName.value = decoder.ReadWideCharString();
            }
            if ((null != this.ifdROT)) {
                this.ifdROT.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<MInterfacePointer>(ref this.ifdROT.value);
            }
            if ((null != this.ifdStg)) {
                this.ifdStg.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<MInterfacePointer>(ref this.ifdStg.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public enum SPD_FLAGS : int {
        SPD_FLAG_USE_CONSOLE_SESSION = 1,
        SPD_FLAG_USE_DEFAULT_AUTHN_LVL = 2,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct SpecialPropertiesData : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
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
            if ((this.Reserved3 == null)) {
                this.Reserved3 = new uint[5];
            }
            for (int i = 0; (i < 5); i++
            ) {
                uint elem_0 = this.Reserved3[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
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
            if ((this.Reserved3 == null)) {
                this.Reserved3 = new uint[5];
            }
            for (int i = 0; (i < 5); i++
            ) {
                uint elem_0 = this.Reserved3[i];
                elem_0 = decoder.ReadUInt32();
                this.Reserved3[i] = elem_0;
            }
        }
        public uint dwSessionId;
        public int fRemoteThisSessionId;
        public int fClientImpersonating;
        public int fPartitionIDPresent;
        public uint dwDefaultAuthnLvl;
        public System.Guid guidPartition;
        public uint dwPRTFlags;
        public uint dwOrigClsctx;
        public uint dwFlags;
        public uint Reserved1;
        public ulong Reserved2;
        public uint[] Reserved3;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct SpecialPropertiesData_Alternate : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwSessionId);
            encoder.WriteValue(this.fRemoteThisSessionId);
            encoder.WriteValue(this.fClientImpersonating);
            encoder.WriteValue(this.fPartitionIDPresent);
            encoder.WriteValue(this.dwDefaultAuthnLvl);
            encoder.WriteValue(this.guidPartition);
            encoder.WriteValue(this.dwPRTFlags);
            encoder.WriteValue(this.dwOrigClsctx);
            encoder.WriteValue(this.dwFlags);
            if ((this.Reserved3 == null)) {
                this.Reserved3 = new uint[8];
            }
            for (int i = 0; (i < 8); i++
            ) {
                uint elem_0 = this.Reserved3[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwSessionId = decoder.ReadUInt32();
            this.fRemoteThisSessionId = decoder.ReadInt32();
            this.fClientImpersonating = decoder.ReadInt32();
            this.fPartitionIDPresent = decoder.ReadInt32();
            this.dwDefaultAuthnLvl = decoder.ReadUInt32();
            this.guidPartition = decoder.ReadUuid();
            this.dwPRTFlags = decoder.ReadUInt32();
            this.dwOrigClsctx = decoder.ReadUInt32();
            this.dwFlags = decoder.ReadUInt32();
            if ((this.Reserved3 == null)) {
                this.Reserved3 = new uint[8];
            }
            for (int i = 0; (i < 8); i++
            ) {
                uint elem_0 = this.Reserved3[i];
                elem_0 = decoder.ReadUInt32();
                this.Reserved3[i] = elem_0;
            }
        }
        public uint dwSessionId;
        public int fRemoteThisSessionId;
        public int fClientImpersonating;
        public int fPartitionIDPresent;
        public uint dwDefaultAuthnLvl;
        public System.Guid guidPartition;
        public uint dwPRTFlags;
        public uint dwOrigClsctx;
        public uint dwFlags;
        public uint[] Reserved3;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    [System.Runtime.InteropServices.GuidAttribute("4d9f4ab8-7d1c-11cf-861e-0020af6e7c57")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface IActivation {
        Task<int> RemoteActivation(
                    RpcPointer<ORPCTHIS> ORPCthis, 
                    RpcPointer<ORPCTHAT> ORPCthat, 
                    RpcPointer<System.Guid> Clsid, 
                    string pwszObjectName, 
                    RpcPointer<MInterfacePointer> pObjectStorage, 
                    uint ClientImpLevel, 
                    uint Mode, 
                    uint Interfaces, 
                    System.Guid[] pIIDs, 
                    ushort cRequestedProtseqs, 
                    ushort[] aRequestedProtseqs, 
                    RpcPointer<ulong> pOxid, 
                    RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings, 
                    RpcPointer<System.Guid> pipidRemUnknown, 
                    RpcPointer<uint> pAuthnHint, 
                    RpcPointer<COMVERSION> pServerVersion, 
                    RpcPointer<int> phr, 
                    RpcPointer<RpcPointer<MInterfacePointer>[]> ppInterfaceData, 
                    RpcPointer<int[]> pResults, 
                    System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    [Titanis.DceRpc.IidAttribute("4d9f4ab8-7d1c-11cf-861e-0020af6e7c57")]
    public class IActivationClientProxy : Titanis.DceRpc.Client.RpcClientProxy, IActivation, Titanis.DceRpc.IRpcClientProxy {
        private static System.Guid _interfaceUuid = new System.Guid("4d9f4ab8-7d1c-11cf-861e-0020af6e7c57");
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
        public virtual async Task<int> RemoteActivation(
                    RpcPointer<ORPCTHIS> ORPCthis, 
                    RpcPointer<ORPCTHAT> ORPCthat, 
                    RpcPointer<System.Guid> Clsid, 
                    string pwszObjectName, 
                    RpcPointer<MInterfacePointer> pObjectStorage, 
                    uint ClientImpLevel, 
                    uint Mode, 
                    uint Interfaces, 
                    System.Guid[] pIIDs, 
                    ushort cRequestedProtseqs, 
                    ushort[] aRequestedProtseqs, 
                    RpcPointer<ulong> pOxid, 
                    RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings, 
                    RpcPointer<System.Guid> pipidRemUnknown, 
                    RpcPointer<uint> pAuthnHint, 
                    RpcPointer<COMVERSION> pServerVersion, 
                    RpcPointer<int> phr, 
                    RpcPointer<RpcPointer<MInterfacePointer>[]> ppInterfaceData, 
                    RpcPointer<int[]> pResults, 
                    System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteFixedStruct(ORPCthis.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ORPCthis.value);
            encoder.WriteValue(Clsid.value);
            encoder.WriteUniqueReferentId((pwszObjectName == null));
            if ((pwszObjectName != null)) {
                encoder.WriteWideCharString(pwszObjectName);
            }
            encoder.WritePointer(pObjectStorage);
            if ((null != pObjectStorage)) {
                encoder.WriteConformantStruct(pObjectStorage.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pObjectStorage.value);
            }
            encoder.WriteValue(ClientImpLevel);
            encoder.WriteValue(Mode);
            encoder.WriteValue(Interfaces);
            encoder.WriteUniqueReferentId((pIIDs == null));
            if ((pIIDs != null)) {
                encoder.WriteArrayHeader(pIIDs);
                for (int i = 0; (i < pIIDs.Length); i++
                ) {
                    System.Guid elem_0 = pIIDs[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(cRequestedProtseqs);
            if ((aRequestedProtseqs != null)) {
                encoder.WriteArrayHeader(aRequestedProtseqs);
                for (int i = 0; (i < aRequestedProtseqs.Length); i++
                ) {
                    ushort elem_0 = aRequestedProtseqs[i];
                    encoder.WriteValue(elem_0);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ORPCthat.value = decoder.ReadFixedStruct<ORPCTHAT>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ORPCTHAT>(ref ORPCthat.value);
            pOxid.value = decoder.ReadUInt64();
            ppdsaOxidBindings.value = decoder.ReadOutPointer<DUALSTRINGARRAY>(ppdsaOxidBindings.value);
            if ((null != ppdsaOxidBindings.value)) {
                ppdsaOxidBindings.value.value = decoder.ReadConformantStruct<DUALSTRINGARRAY>(Titanis.DceRpc.NdrAlignment._2Byte);
                decoder.ReadStructDeferral<DUALSTRINGARRAY>(ref ppdsaOxidBindings.value.value);
            }
            pipidRemUnknown.value = decoder.ReadUuid();
            pAuthnHint.value = decoder.ReadUInt32();
            pServerVersion.value = decoder.ReadFixedStruct<COMVERSION>(Titanis.DceRpc.NdrAlignment._2Byte);
            decoder.ReadStructDeferral<COMVERSION>(ref pServerVersion.value);
            phr.value = decoder.ReadInt32();
            ppInterfaceData.value = decoder.ReadArrayHeader<RpcPointer<MInterfacePointer>>();
            for (int i = 0; (i < ppInterfaceData.value.Length); i++
            ) {
                RpcPointer<MInterfacePointer> elem_0 = ppInterfaceData.value[i];
                elem_0 = decoder.ReadPointer<MInterfacePointer>();
                ppInterfaceData.value[i] = elem_0;
            }
            for (int i = 0; (i < ppInterfaceData.value.Length); i++
            ) {
                RpcPointer<MInterfacePointer> elem_0 = ppInterfaceData.value[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                    decoder.ReadStructDeferral<MInterfacePointer>(ref elem_0.value);
                }
                ppInterfaceData.value[i] = elem_0;
            }
            pResults.value = decoder.ReadArrayHeader<int>();
            for (int i = 0; (i < pResults.value.Length); i++
            ) {
                int elem_0 = pResults.value[i];
                elem_0 = decoder.ReadInt32();
                pResults.value[i] = elem_0;
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public class IActivationStub : Titanis.DceRpc.Server.RpcServiceStub {
        private static System.Guid _interfaceUuid = new System.Guid("4d9f4ab8-7d1c-11cf-861e-0020af6e7c57");
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
        private IActivation _obj;
        public IActivationStub(IActivation obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_RemoteActivation};
        }
        private async Task Invoke_RemoteActivation(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<ORPCTHIS> ORPCthis;
            RpcPointer<ORPCTHAT> ORPCthat = new RpcPointer<ORPCTHAT>();
            RpcPointer<System.Guid> Clsid;
            string pwszObjectName;
            RpcPointer<MInterfacePointer> pObjectStorage;
            uint ClientImpLevel;
            uint Mode;
            uint Interfaces;
            System.Guid[] pIIDs;
            ushort cRequestedProtseqs;
            ushort[] aRequestedProtseqs;
            RpcPointer<ulong> pOxid = new RpcPointer<ulong>();
            RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings = new RpcPointer<RpcPointer<DUALSTRINGARRAY>>();
            RpcPointer<System.Guid> pipidRemUnknown = new RpcPointer<System.Guid>();
            RpcPointer<uint> pAuthnHint = new RpcPointer<uint>();
            RpcPointer<COMVERSION> pServerVersion = new RpcPointer<COMVERSION>();
            RpcPointer<int> phr = new RpcPointer<int>();
            RpcPointer<RpcPointer<MInterfacePointer>[]> ppInterfaceData = new RpcPointer<RpcPointer<MInterfacePointer>[]>();
            RpcPointer<int[]> pResults = new RpcPointer<int[]>();
            ORPCthis = new RpcPointer<ORPCTHIS>();
            ORPCthis.value = decoder.ReadFixedStruct<ORPCTHIS>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ORPCTHIS>(ref ORPCthis.value);
            Clsid = new RpcPointer<System.Guid>();
            Clsid.value = decoder.ReadUuid();
            if ((decoder.ReadReferentId() == 0)) {
                pwszObjectName = null;
            }
            else {
                pwszObjectName = decoder.ReadWideCharString();
            }
            pObjectStorage = decoder.ReadPointer<MInterfacePointer>();
            if ((null != pObjectStorage)) {
                pObjectStorage.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<MInterfacePointer>(ref pObjectStorage.value);
            }
            ClientImpLevel = decoder.ReadUInt32();
            Mode = decoder.ReadUInt32();
            Interfaces = decoder.ReadUInt32();
            pIIDs = decoder.ReadArrayHeader<System.Guid>();
            for (int i = 0; (i < pIIDs.Length); i++
            ) {
                System.Guid elem_0 = pIIDs[i];
                elem_0 = decoder.ReadUuid();
                pIIDs[i] = elem_0;
            }
            cRequestedProtseqs = decoder.ReadUInt16();
            aRequestedProtseqs = decoder.ReadArrayHeader<ushort>();
            for (int i = 0; (i < aRequestedProtseqs.Length); i++
            ) {
                ushort elem_0 = aRequestedProtseqs[i];
                elem_0 = decoder.ReadUInt16();
                aRequestedProtseqs[i] = elem_0;
            }
            var invokeTask = this._obj.RemoteActivation(ORPCthis, ORPCthat, Clsid, pwszObjectName, pObjectStorage, ClientImpLevel, Mode, Interfaces, pIIDs, cRequestedProtseqs, aRequestedProtseqs, pOxid, ppdsaOxidBindings, pipidRemUnknown, pAuthnHint, pServerVersion, phr, ppInterfaceData, pResults, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(ORPCthat.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ORPCthat.value);
            encoder.WriteValue(pOxid.value);
            encoder.WritePointer(ppdsaOxidBindings.value);
            if ((null != ppdsaOxidBindings.value)) {
                encoder.WriteConformantStruct(ppdsaOxidBindings.value.value, Titanis.DceRpc.NdrAlignment._2Byte);
                encoder.WriteStructDeferral(ppdsaOxidBindings.value.value);
            }
            encoder.WriteValue(pipidRemUnknown.value);
            encoder.WriteValue(pAuthnHint.value);
            encoder.WriteFixedStruct(pServerVersion.value, Titanis.DceRpc.NdrAlignment._2Byte);
            encoder.WriteStructDeferral(pServerVersion.value);
            encoder.WriteValue(phr.value);
            encoder.WriteArrayHeader(ppInterfaceData.value);
            for (int i = 0; (i < ppInterfaceData.value.Length); i++
            ) {
                RpcPointer<MInterfacePointer> elem_0 = ppInterfaceData.value[i];
                encoder.WritePointer(elem_0);
            }
            for (int i = 0; (i < ppInterfaceData.value.Length); i++
            ) {
                RpcPointer<MInterfacePointer> elem_0 = ppInterfaceData.value[i];
                if ((null != elem_0)) {
                    encoder.WriteConformantStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._4Byte);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
            encoder.WriteArrayHeader(pResults.value);
            for (int i = 0; (i < pResults.value.Length); i++
            ) {
                int elem_0 = pResults.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(retval);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    [System.Runtime.InteropServices.GuidAttribute("000001a0-0000-0000-c000-000000000046")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface IRemoteSCMActivator {
        Task Opnum0NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum1NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum2NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> RemoteGetClassObject(RpcPointer<ORPCTHIS> orpcthis, RpcPointer<ORPCTHAT> orpcthat, RpcPointer<MInterfacePointer> pActProperties, RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties, System.Threading.CancellationToken cancellationToken);
        Task<int> RemoteCreateInstance(RpcPointer<ORPCTHIS> orpcthis, RpcPointer<ORPCTHAT> orpcthat, RpcPointer<MInterfacePointer> pUnkOuter, RpcPointer<MInterfacePointer> pActProperties, RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    [Titanis.DceRpc.IidAttribute("000001a0-0000-0000-c000-000000000046")]
    public class IRemoteSCMActivatorClientProxy : Titanis.DceRpc.Client.RpcClientProxy, IRemoteSCMActivator, Titanis.DceRpc.IRpcClientProxy {
        private static System.Guid _interfaceUuid = new System.Guid("000001a0-0000-0000-c000-000000000046");
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
        public virtual async Task Opnum0NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum1NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum2NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> RemoteGetClassObject(RpcPointer<ORPCTHIS> orpcthis, RpcPointer<ORPCTHAT> orpcthat, RpcPointer<MInterfacePointer> pActProperties, RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteFixedStruct(orpcthis.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(orpcthis.value);
            encoder.WritePointer(pActProperties);
            if ((null != pActProperties)) {
                encoder.WriteConformantStruct(pActProperties.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pActProperties.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            orpcthat.value = decoder.ReadFixedStruct<ORPCTHAT>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ORPCTHAT>(ref orpcthat.value);
            ppActProperties.value = decoder.ReadOutPointer<MInterfacePointer>(ppActProperties.value);
            if ((null != ppActProperties.value)) {
                ppActProperties.value.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<MInterfacePointer>(ref ppActProperties.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> RemoteCreateInstance(RpcPointer<ORPCTHIS> orpcthis, RpcPointer<ORPCTHAT> orpcthat, RpcPointer<MInterfacePointer> pUnkOuter, RpcPointer<MInterfacePointer> pActProperties, RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteFixedStruct(orpcthis.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(orpcthis.value);
            encoder.WritePointer(pUnkOuter);
            if ((null != pUnkOuter)) {
                encoder.WriteConformantStruct(pUnkOuter.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pUnkOuter.value);
            }
            encoder.WritePointer(pActProperties);
            if ((null != pActProperties)) {
                encoder.WriteConformantStruct(pActProperties.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(pActProperties.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            orpcthat.value = decoder.ReadFixedStruct<ORPCTHAT>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ORPCTHAT>(ref orpcthat.value);
            ppActProperties.value = decoder.ReadOutPointer<MInterfacePointer>(ppActProperties.value);
            if ((null != ppActProperties.value)) {
                ppActProperties.value.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<MInterfacePointer>(ref ppActProperties.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public class IRemoteSCMActivatorStub : Titanis.DceRpc.Server.RpcServiceStub {
        private static System.Guid _interfaceUuid = new System.Guid("000001a0-0000-0000-c000-000000000046");
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
        private IRemoteSCMActivator _obj;
        public IRemoteSCMActivatorStub(IRemoteSCMActivator obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_Opnum0NotUsedOnWire,
                    this.Invoke_Opnum1NotUsedOnWire,
                    this.Invoke_Opnum2NotUsedOnWire,
                    this.Invoke_RemoteGetClassObject,
                    this.Invoke_RemoteCreateInstance};
        }
        private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_RemoteGetClassObject(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<ORPCTHIS> orpcthis;
            RpcPointer<ORPCTHAT> orpcthat = new RpcPointer<ORPCTHAT>();
            RpcPointer<MInterfacePointer> pActProperties;
            RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties = new RpcPointer<RpcPointer<MInterfacePointer>>();
            orpcthis = new RpcPointer<ORPCTHIS>();
            orpcthis.value = decoder.ReadFixedStruct<ORPCTHIS>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ORPCTHIS>(ref orpcthis.value);
            pActProperties = decoder.ReadPointer<MInterfacePointer>();
            if ((null != pActProperties)) {
                pActProperties.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<MInterfacePointer>(ref pActProperties.value);
            }
            var invokeTask = this._obj.RemoteGetClassObject(orpcthis, orpcthat, pActProperties, ppActProperties, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(orpcthat.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(orpcthat.value);
            encoder.WritePointer(ppActProperties.value);
            if ((null != ppActProperties.value)) {
                encoder.WriteConformantStruct(ppActProperties.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(ppActProperties.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RemoteCreateInstance(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<ORPCTHIS> orpcthis;
            RpcPointer<ORPCTHAT> orpcthat = new RpcPointer<ORPCTHAT>();
            RpcPointer<MInterfacePointer> pUnkOuter;
            RpcPointer<MInterfacePointer> pActProperties;
            RpcPointer<RpcPointer<MInterfacePointer>> ppActProperties = new RpcPointer<RpcPointer<MInterfacePointer>>();
            orpcthis = new RpcPointer<ORPCTHIS>();
            orpcthis.value = decoder.ReadFixedStruct<ORPCTHIS>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ORPCTHIS>(ref orpcthis.value);
            pUnkOuter = decoder.ReadPointer<MInterfacePointer>();
            if ((null != pUnkOuter)) {
                pUnkOuter.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<MInterfacePointer>(ref pUnkOuter.value);
            }
            pActProperties = decoder.ReadPointer<MInterfacePointer>();
            if ((null != pActProperties)) {
                pActProperties.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<MInterfacePointer>(ref pActProperties.value);
            }
            var invokeTask = this._obj.RemoteCreateInstance(orpcthis, orpcthat, pUnkOuter, pActProperties, ppActProperties, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(orpcthat.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(orpcthat.value);
            encoder.WritePointer(ppActProperties.value);
            if ((null != ppActProperties.value)) {
                encoder.WriteConformantStruct(ppActProperties.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(ppActProperties.value.value);
            }
            encoder.WriteValue(retval);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    [System.Runtime.InteropServices.GuidAttribute("99fcfec4-5260-101b-bbcb-00aa0021347a")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface IObjectExporter {
        [Titanis.DceRpc.IdempotentAttribute()]
        Task<int> ResolveOxid(RpcPointer<ulong> pOxid, ushort cRequestedProtseqs, ushort[] arRequestedProtseqs, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings, RpcPointer<System.Guid> pipidRemUnknown, RpcPointer<uint> pAuthnHint, System.Threading.CancellationToken cancellationToken);
        [Titanis.DceRpc.IdempotentAttribute()]
        Task<int> SimplePing(RpcPointer<ulong> pSetId, System.Threading.CancellationToken cancellationToken);
        [Titanis.DceRpc.IdempotentAttribute()]
        Task<int> ComplexPing(RpcPointer<ulong> pSetId, ushort SequenceNum, ushort cAddToSet, ushort cDelFromSet, ulong[] AddToSet, ulong[] DelFromSet, RpcPointer<ushort> pPingBackoffFactor, System.Threading.CancellationToken cancellationToken);
        [Titanis.DceRpc.IdempotentAttribute()]
        Task<int> ServerAlive(System.Threading.CancellationToken cancellationToken);
        [Titanis.DceRpc.IdempotentAttribute()]
        Task<int> ResolveOxid2(RpcPointer<ulong> pOxid, ushort cRequestedProtseqs, ushort[] arRequestedProtseqs, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings, RpcPointer<System.Guid> pipidRemUnknown, RpcPointer<uint> pAuthnHint, RpcPointer<COMVERSION> pComVersion, System.Threading.CancellationToken cancellationToken);
        [Titanis.DceRpc.IdempotentAttribute()]
        Task<int> ServerAlive2(RpcPointer<COMVERSION> pComVersion, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOrBindings, RpcPointer<uint> pReserved, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    [Titanis.DceRpc.IidAttribute("99fcfec4-5260-101b-bbcb-00aa0021347a")]
    public class IObjectExporterClientProxy : Titanis.DceRpc.Client.RpcClientProxy, IObjectExporter, Titanis.DceRpc.IRpcClientProxy {
        private static System.Guid _interfaceUuid = new System.Guid("99fcfec4-5260-101b-bbcb-00aa0021347a");
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
        [Titanis.DceRpc.IdempotentAttribute()]
        public virtual async Task<int> ResolveOxid(RpcPointer<ulong> pOxid, ushort cRequestedProtseqs, ushort[] arRequestedProtseqs, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings, RpcPointer<System.Guid> pipidRemUnknown, RpcPointer<uint> pAuthnHint, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(pOxid.value);
            encoder.WriteValue(cRequestedProtseqs);
            if ((arRequestedProtseqs != null)) {
                encoder.WriteArrayHeader(arRequestedProtseqs);
                for (int i = 0; (i < arRequestedProtseqs.Length); i++
                ) {
                    ushort elem_0 = arRequestedProtseqs[i];
                    encoder.WriteValue(elem_0);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppdsaOxidBindings.value = decoder.ReadOutPointer<DUALSTRINGARRAY>(ppdsaOxidBindings.value);
            if ((null != ppdsaOxidBindings.value)) {
                ppdsaOxidBindings.value.value = decoder.ReadConformantStruct<DUALSTRINGARRAY>(Titanis.DceRpc.NdrAlignment._2Byte);
                decoder.ReadStructDeferral<DUALSTRINGARRAY>(ref ppdsaOxidBindings.value.value);
            }
            pipidRemUnknown.value = decoder.ReadUuid();
            pAuthnHint.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        [Titanis.DceRpc.IdempotentAttribute()]
        public virtual async Task<int> SimplePing(RpcPointer<ulong> pSetId, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(pSetId.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        [Titanis.DceRpc.IdempotentAttribute()]
        public virtual async Task<int> ComplexPing(RpcPointer<ulong> pSetId, ushort SequenceNum, ushort cAddToSet, ushort cDelFromSet, ulong[] AddToSet, ulong[] DelFromSet, RpcPointer<ushort> pPingBackoffFactor, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(pSetId.value);
            encoder.WriteValue(SequenceNum);
            encoder.WriteValue(cAddToSet);
            encoder.WriteValue(cDelFromSet);
            encoder.WriteUniqueReferentId((AddToSet == null));
            if ((AddToSet != null)) {
                encoder.WriteArrayHeader(AddToSet);
                for (int i = 0; (i < AddToSet.Length); i++
                ) {
                    ulong elem_0 = AddToSet[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteUniqueReferentId((DelFromSet == null));
            if ((DelFromSet != null)) {
                encoder.WriteArrayHeader(DelFromSet);
                for (int i = 0; (i < DelFromSet.Length); i++
                ) {
                    ulong elem_0 = DelFromSet[i];
                    encoder.WriteValue(elem_0);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pSetId.value = decoder.ReadUInt64();
            pPingBackoffFactor.value = decoder.ReadUInt16();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        [Titanis.DceRpc.IdempotentAttribute()]
        public virtual async Task<int> ServerAlive(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        [Titanis.DceRpc.IdempotentAttribute()]
        public virtual async Task<int> ResolveOxid2(RpcPointer<ulong> pOxid, ushort cRequestedProtseqs, ushort[] arRequestedProtseqs, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings, RpcPointer<System.Guid> pipidRemUnknown, RpcPointer<uint> pAuthnHint, RpcPointer<COMVERSION> pComVersion, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(pOxid.value);
            encoder.WriteValue(cRequestedProtseqs);
            if ((arRequestedProtseqs != null)) {
                encoder.WriteArrayHeader(arRequestedProtseqs);
                for (int i = 0; (i < arRequestedProtseqs.Length); i++
                ) {
                    ushort elem_0 = arRequestedProtseqs[i];
                    encoder.WriteValue(elem_0);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppdsaOxidBindings.value = decoder.ReadOutPointer<DUALSTRINGARRAY>(ppdsaOxidBindings.value);
            if ((null != ppdsaOxidBindings.value)) {
                ppdsaOxidBindings.value.value = decoder.ReadConformantStruct<DUALSTRINGARRAY>(Titanis.DceRpc.NdrAlignment._2Byte);
                decoder.ReadStructDeferral<DUALSTRINGARRAY>(ref ppdsaOxidBindings.value.value);
            }
            pipidRemUnknown.value = decoder.ReadUuid();
            pAuthnHint.value = decoder.ReadUInt32();
            pComVersion.value = decoder.ReadFixedStruct<COMVERSION>(Titanis.DceRpc.NdrAlignment._2Byte);
            decoder.ReadStructDeferral<COMVERSION>(ref pComVersion.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        [Titanis.DceRpc.IdempotentAttribute()]
        public virtual async Task<int> ServerAlive2(RpcPointer<COMVERSION> pComVersion, RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOrBindings, RpcPointer<uint> pReserved, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pComVersion.value = decoder.ReadFixedStruct<COMVERSION>(Titanis.DceRpc.NdrAlignment._2Byte);
            decoder.ReadStructDeferral<COMVERSION>(ref pComVersion.value);
            ppdsaOrBindings.value = decoder.ReadOutPointer<DUALSTRINGARRAY>(ppdsaOrBindings.value);
            if ((null != ppdsaOrBindings.value)) {
                ppdsaOrBindings.value.value = decoder.ReadConformantStruct<DUALSTRINGARRAY>(Titanis.DceRpc.NdrAlignment._2Byte);
                decoder.ReadStructDeferral<DUALSTRINGARRAY>(ref ppdsaOrBindings.value.value);
            }
            pReserved.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public class IObjectExporterStub : Titanis.DceRpc.Server.RpcServiceStub {
        private static System.Guid _interfaceUuid = new System.Guid("99fcfec4-5260-101b-bbcb-00aa0021347a");
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
        private IObjectExporter _obj;
        public IObjectExporterStub(IObjectExporter obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_ResolveOxid,
                    this.Invoke_SimplePing,
                    this.Invoke_ComplexPing,
                    this.Invoke_ServerAlive,
                    this.Invoke_ResolveOxid2,
                    this.Invoke_ServerAlive2};
        }
        private async Task Invoke_ResolveOxid(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<ulong> pOxid;
            ushort cRequestedProtseqs;
            ushort[] arRequestedProtseqs;
            RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings = new RpcPointer<RpcPointer<DUALSTRINGARRAY>>();
            RpcPointer<System.Guid> pipidRemUnknown = new RpcPointer<System.Guid>();
            RpcPointer<uint> pAuthnHint = new RpcPointer<uint>();
            pOxid = new RpcPointer<ulong>();
            pOxid.value = decoder.ReadUInt64();
            cRequestedProtseqs = decoder.ReadUInt16();
            arRequestedProtseqs = decoder.ReadArrayHeader<ushort>();
            for (int i = 0; (i < arRequestedProtseqs.Length); i++
            ) {
                ushort elem_0 = arRequestedProtseqs[i];
                elem_0 = decoder.ReadUInt16();
                arRequestedProtseqs[i] = elem_0;
            }
            var invokeTask = this._obj.ResolveOxid(pOxid, cRequestedProtseqs, arRequestedProtseqs, ppdsaOxidBindings, pipidRemUnknown, pAuthnHint, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppdsaOxidBindings.value);
            if ((null != ppdsaOxidBindings.value)) {
                encoder.WriteConformantStruct(ppdsaOxidBindings.value.value, Titanis.DceRpc.NdrAlignment._2Byte);
                encoder.WriteStructDeferral(ppdsaOxidBindings.value.value);
            }
            encoder.WriteValue(pipidRemUnknown.value);
            encoder.WriteValue(pAuthnHint.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SimplePing(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<ulong> pSetId;
            pSetId = new RpcPointer<ulong>();
            pSetId.value = decoder.ReadUInt64();
            var invokeTask = this._obj.SimplePing(pSetId, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ComplexPing(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
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
            for (int i = 0; (i < AddToSet.Length); i++
            ) {
                ulong elem_0 = AddToSet[i];
                elem_0 = decoder.ReadUInt64();
                AddToSet[i] = elem_0;
            }
            DelFromSet = decoder.ReadArrayHeader<ulong>();
            for (int i = 0; (i < DelFromSet.Length); i++
            ) {
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
        private async Task Invoke_ServerAlive(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.ServerAlive(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ResolveOxid2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<ulong> pOxid;
            ushort cRequestedProtseqs;
            ushort[] arRequestedProtseqs;
            RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOxidBindings = new RpcPointer<RpcPointer<DUALSTRINGARRAY>>();
            RpcPointer<System.Guid> pipidRemUnknown = new RpcPointer<System.Guid>();
            RpcPointer<uint> pAuthnHint = new RpcPointer<uint>();
            RpcPointer<COMVERSION> pComVersion = new RpcPointer<COMVERSION>();
            pOxid = new RpcPointer<ulong>();
            pOxid.value = decoder.ReadUInt64();
            cRequestedProtseqs = decoder.ReadUInt16();
            arRequestedProtseqs = decoder.ReadArrayHeader<ushort>();
            for (int i = 0; (i < arRequestedProtseqs.Length); i++
            ) {
                ushort elem_0 = arRequestedProtseqs[i];
                elem_0 = decoder.ReadUInt16();
                arRequestedProtseqs[i] = elem_0;
            }
            var invokeTask = this._obj.ResolveOxid2(pOxid, cRequestedProtseqs, arRequestedProtseqs, ppdsaOxidBindings, pipidRemUnknown, pAuthnHint, pComVersion, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppdsaOxidBindings.value);
            if ((null != ppdsaOxidBindings.value)) {
                encoder.WriteConformantStruct(ppdsaOxidBindings.value.value, Titanis.DceRpc.NdrAlignment._2Byte);
                encoder.WriteStructDeferral(ppdsaOxidBindings.value.value);
            }
            encoder.WriteValue(pipidRemUnknown.value);
            encoder.WriteValue(pAuthnHint.value);
            encoder.WriteFixedStruct(pComVersion.value, Titanis.DceRpc.NdrAlignment._2Byte);
            encoder.WriteStructDeferral(pComVersion.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ServerAlive2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<COMVERSION> pComVersion = new RpcPointer<COMVERSION>();
            RpcPointer<RpcPointer<DUALSTRINGARRAY>> ppdsaOrBindings = new RpcPointer<RpcPointer<DUALSTRINGARRAY>>();
            RpcPointer<uint> pReserved = new RpcPointer<uint>();
            var invokeTask = this._obj.ServerAlive2(pComVersion, ppdsaOrBindings, pReserved, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(pComVersion.value, Titanis.DceRpc.NdrAlignment._2Byte);
            encoder.WriteStructDeferral(pComVersion.value);
            encoder.WritePointer(ppdsaOrBindings.value);
            if ((null != ppdsaOrBindings.value)) {
                encoder.WriteConformantStruct(ppdsaOrBindings.value.value, Titanis.DceRpc.NdrAlignment._2Byte);
                encoder.WriteStructDeferral(ppdsaOrBindings.value.value);
            }
            encoder.WriteValue(pReserved.value);
            encoder.WriteValue(retval);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    [System.Runtime.InteropServices.GuidAttribute("00000000-0000-0000-c000-000000000046")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface IUnknown : IRpcObject {
        Task<int> Opnum0NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> Opnum1NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> Opnum2NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    [Titanis.DceRpc.IidAttribute("00000000-0000-0000-c000-000000000046")]
    public class IUnknownClientProxy : Titanis.DceRpc.Client.RpcObjectProxy, IUnknown, Titanis.DceRpc.IRpcObjectProxy {
        private static System.Guid _interfaceUuid = new System.Guid("00000000-0000-0000-c000-000000000046");
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
        public virtual async Task<int> Opnum0NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Opnum1NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> Opnum2NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public class IUnknownStub : Titanis.DceRpc.Server.RpcObjectStub {
        private static System.Guid _interfaceUuid = new System.Guid("00000000-0000-0000-c000-000000000046");
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
        private IUnknown _obj;
        public IUnknownStub(IUnknown obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_Opnum0NotUsedOnWire,
                    this.Invoke_Opnum1NotUsedOnWire,
                    this.Invoke_Opnum2NotUsedOnWire};
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
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    [System.Runtime.InteropServices.GuidAttribute("00000131-0000-0000-c000-000000000046")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface IRemUnknown : IUnknown {
        Task<int> RemQueryInterface(RpcPointer<System.Guid> ripid, uint cRefs, ushort cIids, System.Guid[] iids, RpcPointer<RpcPointer<REMQIRESULT[]>> ppQIResults, System.Threading.CancellationToken cancellationToken);
        Task<int> RemAddRef(ushort cInterfaceRefs, REMINTERFACEREF[] InterfaceRefs, RpcPointer<int[]> pResults, System.Threading.CancellationToken cancellationToken);
        Task<int> RemRelease(ushort cInterfaceRefs, REMINTERFACEREF[] InterfaceRefs, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    [Titanis.DceRpc.IidAttribute("00000131-0000-0000-c000-000000000046")]
    public class IRemUnknownClientProxy : IUnknownClientProxy, IRemUnknown {
        private static System.Guid _interfaceUuid = new System.Guid("00000131-0000-0000-c000-000000000046");
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
        public virtual async Task<int> RemQueryInterface(RpcPointer<System.Guid> ripid, uint cRefs, ushort cIids, System.Guid[] iids, RpcPointer<RpcPointer<REMQIRESULT[]>> ppQIResults, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(ripid.value);
            encoder.WriteValue(cRefs);
            encoder.WriteValue(cIids);
            if ((iids != null)) {
                encoder.WriteArrayHeader(iids);
                for (int i = 0; (i < iids.Length); i++
                ) {
                    System.Guid elem_0 = iids[i];
                    encoder.WriteValue(elem_0);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppQIResults.value = decoder.ReadOutPointer<REMQIRESULT[]>(ppQIResults.value);
            if ((null != ppQIResults.value)) {
                ppQIResults.value.value = decoder.ReadArrayHeader<REMQIRESULT>();
                for (int i = 0; (i < ppQIResults.value.value.Length); i++
                ) {
                    REMQIRESULT elem_0 = ppQIResults.value.value[i];
                    elem_0 = decoder.ReadFixedStruct<REMQIRESULT>(Titanis.DceRpc.NdrAlignment._8Byte);
                    ppQIResults.value.value[i] = elem_0;
                }
                for (int i = 0; (i < ppQIResults.value.value.Length); i++
                ) {
                    REMQIRESULT elem_0 = ppQIResults.value.value[i];
                    decoder.ReadStructDeferral<REMQIRESULT>(ref elem_0);
                    ppQIResults.value.value[i] = elem_0;
                }
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> RemAddRef(ushort cInterfaceRefs, REMINTERFACEREF[] InterfaceRefs, RpcPointer<int[]> pResults, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(cInterfaceRefs);
            if ((InterfaceRefs != null)) {
                encoder.WriteArrayHeader(InterfaceRefs);
                for (int i = 0; (i < InterfaceRefs.Length); i++
                ) {
                    REMINTERFACEREF elem_0 = InterfaceRefs[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
                }
            }
            for (int i = 0; (i < InterfaceRefs.Length); i++
            ) {
                REMINTERFACEREF elem_0 = InterfaceRefs[i];
                encoder.WriteStructDeferral(elem_0);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pResults.value = decoder.ReadArrayHeader<int>();
            for (int i = 0; (i < pResults.value.Length); i++
            ) {
                int elem_0 = pResults.value[i];
                elem_0 = decoder.ReadInt32();
                pResults.value[i] = elem_0;
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> RemRelease(ushort cInterfaceRefs, REMINTERFACEREF[] InterfaceRefs, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(cInterfaceRefs);
            if ((InterfaceRefs != null)) {
                encoder.WriteArrayHeader(InterfaceRefs);
                for (int i = 0; (i < InterfaceRefs.Length); i++
                ) {
                    REMINTERFACEREF elem_0 = InterfaceRefs[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
                }
            }
            for (int i = 0; (i < InterfaceRefs.Length); i++
            ) {
                REMINTERFACEREF elem_0 = InterfaceRefs[i];
                encoder.WriteStructDeferral(elem_0);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public class IRemUnknownStub : Titanis.DceRpc.Server.RpcObjectStub {
        private static System.Guid _interfaceUuid = new System.Guid("00000131-0000-0000-c000-000000000046");
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
        private IRemUnknown _obj;
        public IRemUnknownStub(IRemUnknown obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_Opnum0NotUsedOnWire,
                    this.Invoke_Opnum1NotUsedOnWire,
                    this.Invoke_Opnum2NotUsedOnWire,
                    this.Invoke_RemQueryInterface,
                    this.Invoke_RemAddRef,
                    this.Invoke_RemRelease};
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
        private async Task Invoke_RemQueryInterface(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<System.Guid> ripid;
            uint cRefs;
            ushort cIids;
            System.Guid[] iids;
            RpcPointer<RpcPointer<REMQIRESULT[]>> ppQIResults = new RpcPointer<RpcPointer<REMQIRESULT[]>>();
            ripid = new RpcPointer<System.Guid>();
            ripid.value = decoder.ReadUuid();
            cRefs = decoder.ReadUInt32();
            cIids = decoder.ReadUInt16();
            iids = decoder.ReadArrayHeader<System.Guid>();
            for (int i = 0; (i < iids.Length); i++
            ) {
                System.Guid elem_0 = iids[i];
                elem_0 = decoder.ReadUuid();
                iids[i] = elem_0;
            }
            var invokeTask = this._obj.RemQueryInterface(ripid, cRefs, cIids, iids, ppQIResults, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppQIResults.value);
            if ((null != ppQIResults.value)) {
                encoder.WriteArrayHeader(ppQIResults.value.value);
                for (int i = 0; (i < ppQIResults.value.value.Length); i++
                ) {
                    REMQIRESULT elem_0 = ppQIResults.value.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._8Byte);
                }
                for (int i = 0; (i < ppQIResults.value.value.Length); i++
                ) {
                    REMQIRESULT elem_0 = ppQIResults.value.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RemAddRef(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            ushort cInterfaceRefs;
            REMINTERFACEREF[] InterfaceRefs;
            RpcPointer<int[]> pResults = new RpcPointer<int[]>();
            cInterfaceRefs = decoder.ReadUInt16();
            InterfaceRefs = decoder.ReadArrayHeader<REMINTERFACEREF>();
            for (int i = 0; (i < InterfaceRefs.Length); i++
            ) {
                REMINTERFACEREF elem_0 = InterfaceRefs[i];
                elem_0 = decoder.ReadFixedStruct<REMINTERFACEREF>(Titanis.DceRpc.NdrAlignment._4Byte);
                InterfaceRefs[i] = elem_0;
            }
            for (int i = 0; (i < InterfaceRefs.Length); i++
            ) {
                REMINTERFACEREF elem_0 = InterfaceRefs[i];
                decoder.ReadStructDeferral<REMINTERFACEREF>(ref elem_0);
                InterfaceRefs[i] = elem_0;
            }
            var invokeTask = this._obj.RemAddRef(cInterfaceRefs, InterfaceRefs, pResults, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(pResults.value);
            for (int i = 0; (i < pResults.value.Length); i++
            ) {
                int elem_0 = pResults.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RemRelease(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            ushort cInterfaceRefs;
            REMINTERFACEREF[] InterfaceRefs;
            cInterfaceRefs = decoder.ReadUInt16();
            InterfaceRefs = decoder.ReadArrayHeader<REMINTERFACEREF>();
            for (int i = 0; (i < InterfaceRefs.Length); i++
            ) {
                REMINTERFACEREF elem_0 = InterfaceRefs[i];
                elem_0 = decoder.ReadFixedStruct<REMINTERFACEREF>(Titanis.DceRpc.NdrAlignment._4Byte);
                InterfaceRefs[i] = elem_0;
            }
            for (int i = 0; (i < InterfaceRefs.Length); i++
            ) {
                REMINTERFACEREF elem_0 = InterfaceRefs[i];
                decoder.ReadStructDeferral<REMINTERFACEREF>(ref elem_0);
                InterfaceRefs[i] = elem_0;
            }
            var invokeTask = this._obj.RemRelease(cInterfaceRefs, InterfaceRefs, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    [System.Runtime.InteropServices.GuidAttribute("00000143-0000-0000-c000-000000000046")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface IRemUnknown2 : IRemUnknown {
        Task<int> RemQueryInterface2(RpcPointer<System.Guid> ripid, ushort cIids, System.Guid[] iids, RpcPointer<int[]> phr, RpcPointer<RpcPointer<MInterfacePointer>[]> ppMIF, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    [Titanis.DceRpc.IidAttribute("00000143-0000-0000-c000-000000000046")]
    public class IRemUnknown2ClientProxy : IRemUnknownClientProxy, IRemUnknown2 {
        private static System.Guid _interfaceUuid = new System.Guid("00000143-0000-0000-c000-000000000046");
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
        public virtual async Task<int> RemQueryInterface2(RpcPointer<System.Guid> ripid, ushort cIids, System.Guid[] iids, RpcPointer<int[]> phr, RpcPointer<RpcPointer<MInterfacePointer>[]> ppMIF, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(ripid.value);
            encoder.WriteValue(cIids);
            if ((iids != null)) {
                encoder.WriteArrayHeader(iids);
                for (int i = 0; (i < iids.Length); i++
                ) {
                    System.Guid elem_0 = iids[i];
                    encoder.WriteValue(elem_0);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            phr.value = decoder.ReadArrayHeader<int>();
            for (int i = 0; (i < phr.value.Length); i++
            ) {
                int elem_0 = phr.value[i];
                elem_0 = decoder.ReadInt32();
                phr.value[i] = elem_0;
            }
            ppMIF.value = decoder.ReadArrayHeader<RpcPointer<MInterfacePointer>>();
            for (int i = 0; (i < ppMIF.value.Length); i++
            ) {
                RpcPointer<MInterfacePointer> elem_0 = ppMIF.value[i];
                elem_0 = decoder.ReadPointer<MInterfacePointer>();
                ppMIF.value[i] = elem_0;
            }
            for (int i = 0; (i < ppMIF.value.Length); i++
            ) {
                RpcPointer<MInterfacePointer> elem_0 = ppMIF.value[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadConformantStruct<MInterfacePointer>(Titanis.DceRpc.NdrAlignment._4Byte);
                    decoder.ReadStructDeferral<MInterfacePointer>(ref elem_0.value);
                }
                ppMIF.value[i] = elem_0;
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public class IRemUnknown2Stub : Titanis.DceRpc.Server.RpcObjectStub {
        private static System.Guid _interfaceUuid = new System.Guid("00000143-0000-0000-c000-000000000046");
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
        private IRemUnknown2 _obj;
        public IRemUnknown2Stub(IRemUnknown2 obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_Opnum0NotUsedOnWire,
                    this.Invoke_Opnum1NotUsedOnWire,
                    this.Invoke_Opnum2NotUsedOnWire,
                    this.Invoke_RemQueryInterface,
                    this.Invoke_RemAddRef,
                    this.Invoke_RemRelease,
                    this.Invoke_RemQueryInterface2};
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
        private async Task Invoke_RemQueryInterface(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<System.Guid> ripid;
            uint cRefs;
            ushort cIids;
            System.Guid[] iids;
            RpcPointer<RpcPointer<REMQIRESULT[]>> ppQIResults = new RpcPointer<RpcPointer<REMQIRESULT[]>>();
            ripid = new RpcPointer<System.Guid>();
            ripid.value = decoder.ReadUuid();
            cRefs = decoder.ReadUInt32();
            cIids = decoder.ReadUInt16();
            iids = decoder.ReadArrayHeader<System.Guid>();
            for (int i = 0; (i < iids.Length); i++
            ) {
                System.Guid elem_0 = iids[i];
                elem_0 = decoder.ReadUuid();
                iids[i] = elem_0;
            }
            var invokeTask = this._obj.RemQueryInterface(ripid, cRefs, cIids, iids, ppQIResults, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppQIResults.value);
            if ((null != ppQIResults.value)) {
                encoder.WriteArrayHeader(ppQIResults.value.value);
                for (int i = 0; (i < ppQIResults.value.value.Length); i++
                ) {
                    REMQIRESULT elem_0 = ppQIResults.value.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._8Byte);
                }
                for (int i = 0; (i < ppQIResults.value.value.Length); i++
                ) {
                    REMQIRESULT elem_0 = ppQIResults.value.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RemAddRef(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            ushort cInterfaceRefs;
            REMINTERFACEREF[] InterfaceRefs;
            RpcPointer<int[]> pResults = new RpcPointer<int[]>();
            cInterfaceRefs = decoder.ReadUInt16();
            InterfaceRefs = decoder.ReadArrayHeader<REMINTERFACEREF>();
            for (int i = 0; (i < InterfaceRefs.Length); i++
            ) {
                REMINTERFACEREF elem_0 = InterfaceRefs[i];
                elem_0 = decoder.ReadFixedStruct<REMINTERFACEREF>(Titanis.DceRpc.NdrAlignment._4Byte);
                InterfaceRefs[i] = elem_0;
            }
            for (int i = 0; (i < InterfaceRefs.Length); i++
            ) {
                REMINTERFACEREF elem_0 = InterfaceRefs[i];
                decoder.ReadStructDeferral<REMINTERFACEREF>(ref elem_0);
                InterfaceRefs[i] = elem_0;
            }
            var invokeTask = this._obj.RemAddRef(cInterfaceRefs, InterfaceRefs, pResults, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(pResults.value);
            for (int i = 0; (i < pResults.value.Length); i++
            ) {
                int elem_0 = pResults.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RemRelease(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            ushort cInterfaceRefs;
            REMINTERFACEREF[] InterfaceRefs;
            cInterfaceRefs = decoder.ReadUInt16();
            InterfaceRefs = decoder.ReadArrayHeader<REMINTERFACEREF>();
            for (int i = 0; (i < InterfaceRefs.Length); i++
            ) {
                REMINTERFACEREF elem_0 = InterfaceRefs[i];
                elem_0 = decoder.ReadFixedStruct<REMINTERFACEREF>(Titanis.DceRpc.NdrAlignment._4Byte);
                InterfaceRefs[i] = elem_0;
            }
            for (int i = 0; (i < InterfaceRefs.Length); i++
            ) {
                REMINTERFACEREF elem_0 = InterfaceRefs[i];
                decoder.ReadStructDeferral<REMINTERFACEREF>(ref elem_0);
                InterfaceRefs[i] = elem_0;
            }
            var invokeTask = this._obj.RemRelease(cInterfaceRefs, InterfaceRefs, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RemQueryInterface2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<System.Guid> ripid;
            ushort cIids;
            System.Guid[] iids;
            RpcPointer<int[]> phr = new RpcPointer<int[]>();
            RpcPointer<RpcPointer<MInterfacePointer>[]> ppMIF = new RpcPointer<RpcPointer<MInterfacePointer>[]>();
            ripid = new RpcPointer<System.Guid>();
            ripid.value = decoder.ReadUuid();
            cIids = decoder.ReadUInt16();
            iids = decoder.ReadArrayHeader<System.Guid>();
            for (int i = 0; (i < iids.Length); i++
            ) {
                System.Guid elem_0 = iids[i];
                elem_0 = decoder.ReadUuid();
                iids[i] = elem_0;
            }
            var invokeTask = this._obj.RemQueryInterface2(ripid, cIids, iids, phr, ppMIF, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(phr.value);
            for (int i = 0; (i < phr.value.Length); i++
            ) {
                int elem_0 = phr.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteArrayHeader(ppMIF.value);
            for (int i = 0; (i < ppMIF.value.Length); i++
            ) {
                RpcPointer<MInterfacePointer> elem_0 = ppMIF.value[i];
                encoder.WritePointer(elem_0);
            }
            for (int i = 0; (i < ppMIF.value.Length); i++
            ) {
                RpcPointer<MInterfacePointer> elem_0 = ppMIF.value[i];
                if ((null != elem_0)) {
                    encoder.WriteConformantStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._4Byte);
                    encoder.WriteStructDeferral(elem_0.value);
                }
            }
            encoder.WriteValue(retval);
        }
    }
}
