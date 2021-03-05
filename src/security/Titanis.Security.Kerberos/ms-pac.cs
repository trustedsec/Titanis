namespace ms_pac {
    using System;
    using System.Threading.Tasks;
    using Titanis;
    using Titanis.DceRpc;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct KERB_SID_AND_ATTRIBUTES : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.Sid);
            encoder.WriteValue(this.Attributes);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Sid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            this.Attributes = decoder.ReadUInt32();
        }
        public RpcPointer<ms_dtyp.RPC_SID> Sid;
        public uint Attributes;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Sid)) {
                encoder.WriteConformantStruct(this.Sid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.Sid.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Sid)) {
                this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct GROUP_MEMBERSHIP : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.RelativeId);
            encoder.WriteValue(this.Attributes);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.RelativeId = decoder.ReadUInt32();
            this.Attributes = decoder.ReadUInt32();
        }
        public uint RelativeId;
        public uint Attributes;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct DOMAIN_GROUP_MEMBERSHIP : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.DomainId);
            encoder.WriteValue(this.GroupCount);
            encoder.WritePointer(this.GroupIds);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.DomainId = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            this.GroupCount = decoder.ReadUInt32();
            this.GroupIds = decoder.ReadUniquePointer<GROUP_MEMBERSHIP[]>();
        }
        public RpcPointer<ms_dtyp.RPC_SID> DomainId;
        public uint GroupCount;
        public RpcPointer<GROUP_MEMBERSHIP[]> GroupIds;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.DomainId)) {
                encoder.WriteConformantStruct(this.DomainId.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.DomainId.value);
            }
            if ((null != this.GroupIds)) {
                encoder.WriteArrayHeader(this.GroupIds.value);
                for (int i = 0; (i < this.GroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.GroupIds.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
                }
                for (int i = 0; (i < this.GroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.GroupIds.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.DomainId)) {
                this.DomainId.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.DomainId.value);
            }
            if ((null != this.GroupIds)) {
                this.GroupIds.value = decoder.ReadArrayHeader<GROUP_MEMBERSHIP>();
                for (int i = 0; (i < this.GroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.GroupIds.value[i];
                    elem_0 = decoder.ReadFixedStruct<GROUP_MEMBERSHIP>(Titanis.DceRpc.NdrAlignment._4Byte);
                    this.GroupIds.value[i] = elem_0;
                }
                for (int i = 0; (i < this.GroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.GroupIds.value[i];
                    decoder.ReadStructDeferral<GROUP_MEMBERSHIP>(ref elem_0);
                    this.GroupIds.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct PAC_INFO_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ulType);
            encoder.WriteValue(this.cbBufferSize);
            encoder.WriteValue(this.Offset);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ulType = decoder.ReadUInt32();
            this.cbBufferSize = decoder.ReadUInt32();
            this.Offset = decoder.ReadUInt64();
        }
        public uint ulType;
        public uint cbBufferSize;
        public ulong Offset;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct PACTYPE : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cBuffers);
            encoder.WriteValue(this.Version);
            if ((this.Buffers == null)) {
                this.Buffers = new PAC_INFO_BUFFER[1];
            }
            for (int i = 0; (i < 1); i++
            ) {
                PAC_INFO_BUFFER elem_0 = this.Buffers[i];
                encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._8Byte);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cBuffers = decoder.ReadUInt32();
            this.Version = decoder.ReadUInt32();
            if ((this.Buffers == null)) {
                this.Buffers = new PAC_INFO_BUFFER[1];
            }
            for (int i = 0; (i < 1); i++
            ) {
                PAC_INFO_BUFFER elem_0 = this.Buffers[i];
                elem_0 = decoder.ReadFixedStruct<PAC_INFO_BUFFER>(Titanis.DceRpc.NdrAlignment._8Byte);
                this.Buffers[i] = elem_0;
            }
        }
        public uint cBuffers;
        public uint Version;
        public PAC_INFO_BUFFER[] Buffers;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < 1); i++
            ) {
                PAC_INFO_BUFFER elem_0 = this.Buffers[i];
                encoder.WriteStructDeferral(elem_0);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < 1); i++
            ) {
                PAC_INFO_BUFFER elem_0 = this.Buffers[i];
                decoder.ReadStructDeferral<PAC_INFO_BUFFER>(ref elem_0);
                this.Buffers[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct CYPHER_BLOCK : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((this.data == null)) {
                this.data = new byte[8];
            }
            for (int i = 0; (i < 8); i++
            ) {
                byte elem_0 = this.data[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((this.data == null)) {
                this.data = new byte[8];
            }
            for (int i = 0; (i < 8); i++
            ) {
                byte elem_0 = this.data[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.data[i] = elem_0;
            }
        }
        public byte[] data;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct USER_SESSION_KEY : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((this.data == null)) {
                this.data = new CYPHER_BLOCK[2];
            }
            for (int i = 0; (i < 2); i++
            ) {
                CYPHER_BLOCK elem_0 = this.data[i];
                encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._1Byte);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((this.data == null)) {
                this.data = new CYPHER_BLOCK[2];
            }
            for (int i = 0; (i < 2); i++
            ) {
                CYPHER_BLOCK elem_0 = this.data[i];
                elem_0 = decoder.ReadFixedStruct<CYPHER_BLOCK>(Titanis.DceRpc.NdrAlignment._1Byte);
                this.data[i] = elem_0;
            }
        }
        public CYPHER_BLOCK[] data;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < 2); i++
            ) {
                CYPHER_BLOCK elem_0 = this.data[i];
                encoder.WriteStructDeferral(elem_0);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < 2); i++
            ) {
                CYPHER_BLOCK elem_0 = this.data[i];
                decoder.ReadStructDeferral<CYPHER_BLOCK>(ref elem_0);
                this.data[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct KERB_VALIDATION_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.LogonTime, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.LogoffTime, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.KickOffTime, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.PasswordLastSet, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.PasswordCanChange, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.PasswordMustChange, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.EffectiveName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.FullName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.LogonScript, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.ProfilePath, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.HomeDirectory, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.HomeDirectoryDrive, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.LogonCount);
            encoder.WriteValue(this.BadPasswordCount);
            encoder.WriteValue(this.UserId);
            encoder.WriteValue(this.PrimaryGroupId);
            encoder.WriteValue(this.GroupCount);
            encoder.WritePointer(this.GroupIds);
            encoder.WriteValue(this.UserFlags);
            encoder.WriteFixedStruct(this.UserSessionKey, Titanis.DceRpc.NdrAlignment._1Byte);
            encoder.WriteFixedStruct(this.LogonServer, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.LogonDomainName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WritePointer(this.LogonDomainId);
            if ((this.Reserved1 == null)) {
                this.Reserved1 = new uint[2];
            }
            for (int i = 0; (i < 2); i++
            ) {
                uint elem_0 = this.Reserved1[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(this.UserAccountControl);
            if ((this.Reserved3 == null)) {
                this.Reserved3 = new uint[7];
            }
            for (int i = 0; (i < 7); i++
            ) {
                uint elem_0 = this.Reserved3[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(this.SidCount);
            encoder.WritePointer(this.ExtraSids);
            encoder.WritePointer(this.ResourceGroupDomainSid);
            encoder.WriteValue(this.ResourceGroupCount);
            encoder.WritePointer(this.ResourceGroupIds);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.LogonTime = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.LogoffTime = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.KickOffTime = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.PasswordLastSet = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.PasswordCanChange = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.PasswordMustChange = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.EffectiveName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.LogonScript = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.ProfilePath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.HomeDirectory = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.HomeDirectoryDrive = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.LogonCount = decoder.ReadUInt16();
            this.BadPasswordCount = decoder.ReadUInt16();
            this.UserId = decoder.ReadUInt32();
            this.PrimaryGroupId = decoder.ReadUInt32();
            this.GroupCount = decoder.ReadUInt32();
            this.GroupIds = decoder.ReadUniquePointer<GROUP_MEMBERSHIP[]>();
            this.UserFlags = decoder.ReadUInt32();
            this.UserSessionKey = decoder.ReadFixedStruct<USER_SESSION_KEY>(Titanis.DceRpc.NdrAlignment._1Byte);
            this.LogonServer = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.LogonDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.LogonDomainId = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            if ((this.Reserved1 == null)) {
                this.Reserved1 = new uint[2];
            }
            for (int i = 0; (i < 2); i++
            ) {
                uint elem_0 = this.Reserved1[i];
                elem_0 = decoder.ReadUInt32();
                this.Reserved1[i] = elem_0;
            }
            this.UserAccountControl = decoder.ReadUInt32();
            if ((this.Reserved3 == null)) {
                this.Reserved3 = new uint[7];
            }
            for (int i = 0; (i < 7); i++
            ) {
                uint elem_0 = this.Reserved3[i];
                elem_0 = decoder.ReadUInt32();
                this.Reserved3[i] = elem_0;
            }
            this.SidCount = decoder.ReadUInt32();
            this.ExtraSids = decoder.ReadUniquePointer<KERB_SID_AND_ATTRIBUTES[]>();
            this.ResourceGroupDomainSid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            this.ResourceGroupCount = decoder.ReadUInt32();
            this.ResourceGroupIds = decoder.ReadUniquePointer<GROUP_MEMBERSHIP[]>();
        }
        public ms_dtyp.FILETIME LogonTime;
        public ms_dtyp.FILETIME LogoffTime;
        public ms_dtyp.FILETIME KickOffTime;
        public ms_dtyp.FILETIME PasswordLastSet;
        public ms_dtyp.FILETIME PasswordCanChange;
        public ms_dtyp.FILETIME PasswordMustChange;
        public ms_dtyp.RPC_UNICODE_STRING EffectiveName;
        public ms_dtyp.RPC_UNICODE_STRING FullName;
        public ms_dtyp.RPC_UNICODE_STRING LogonScript;
        public ms_dtyp.RPC_UNICODE_STRING ProfilePath;
        public ms_dtyp.RPC_UNICODE_STRING HomeDirectory;
        public ms_dtyp.RPC_UNICODE_STRING HomeDirectoryDrive;
        public ushort LogonCount;
        public ushort BadPasswordCount;
        public uint UserId;
        public uint PrimaryGroupId;
        public uint GroupCount;
        public RpcPointer<GROUP_MEMBERSHIP[]> GroupIds;
        public uint UserFlags;
        public USER_SESSION_KEY UserSessionKey;
        public ms_dtyp.RPC_UNICODE_STRING LogonServer;
        public ms_dtyp.RPC_UNICODE_STRING LogonDomainName;
        public RpcPointer<ms_dtyp.RPC_SID> LogonDomainId;
        public uint[] Reserved1;
        public uint UserAccountControl;
        public uint[] Reserved3;
        public uint SidCount;
        public RpcPointer<KERB_SID_AND_ATTRIBUTES[]> ExtraSids;
        public RpcPointer<ms_dtyp.RPC_SID> ResourceGroupDomainSid;
        public uint ResourceGroupCount;
        public RpcPointer<GROUP_MEMBERSHIP[]> ResourceGroupIds;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.LogonTime);
            encoder.WriteStructDeferral(this.LogoffTime);
            encoder.WriteStructDeferral(this.KickOffTime);
            encoder.WriteStructDeferral(this.PasswordLastSet);
            encoder.WriteStructDeferral(this.PasswordCanChange);
            encoder.WriteStructDeferral(this.PasswordMustChange);
            encoder.WriteStructDeferral(this.EffectiveName);
            encoder.WriteStructDeferral(this.FullName);
            encoder.WriteStructDeferral(this.LogonScript);
            encoder.WriteStructDeferral(this.ProfilePath);
            encoder.WriteStructDeferral(this.HomeDirectory);
            encoder.WriteStructDeferral(this.HomeDirectoryDrive);
            if ((null != this.GroupIds)) {
                encoder.WriteArrayHeader(this.GroupIds.value);
                for (int i = 0; (i < this.GroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.GroupIds.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
                }
                for (int i = 0; (i < this.GroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.GroupIds.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
            encoder.WriteStructDeferral(this.UserSessionKey);
            encoder.WriteStructDeferral(this.LogonServer);
            encoder.WriteStructDeferral(this.LogonDomainName);
            if ((null != this.LogonDomainId)) {
                encoder.WriteConformantStruct(this.LogonDomainId.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.LogonDomainId.value);
            }
            if ((null != this.ExtraSids)) {
                encoder.WriteArrayHeader(this.ExtraSids.value);
                for (int i = 0; (i < this.ExtraSids.value.Length); i++
                ) {
                    KERB_SID_AND_ATTRIBUTES elem_0 = this.ExtraSids.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.ExtraSids.value.Length); i++
                ) {
                    KERB_SID_AND_ATTRIBUTES elem_0 = this.ExtraSids.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
            if ((null != this.ResourceGroupDomainSid)) {
                encoder.WriteConformantStruct(this.ResourceGroupDomainSid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.ResourceGroupDomainSid.value);
            }
            if ((null != this.ResourceGroupIds)) {
                encoder.WriteArrayHeader(this.ResourceGroupIds.value);
                for (int i = 0; (i < this.ResourceGroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.ResourceGroupIds.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
                }
                for (int i = 0; (i < this.ResourceGroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.ResourceGroupIds.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.LogonTime);
            decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.LogoffTime);
            decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.KickOffTime);
            decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.PasswordLastSet);
            decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.PasswordCanChange);
            decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.PasswordMustChange);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.EffectiveName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.LogonScript);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ProfilePath);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectory);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectoryDrive);
            if ((null != this.GroupIds)) {
                this.GroupIds.value = decoder.ReadArrayHeader<GROUP_MEMBERSHIP>();
                for (int i = 0; (i < this.GroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.GroupIds.value[i];
                    elem_0 = decoder.ReadFixedStruct<GROUP_MEMBERSHIP>(Titanis.DceRpc.NdrAlignment._4Byte);
                    this.GroupIds.value[i] = elem_0;
                }
                for (int i = 0; (i < this.GroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.GroupIds.value[i];
                    decoder.ReadStructDeferral<GROUP_MEMBERSHIP>(ref elem_0);
                    this.GroupIds.value[i] = elem_0;
                }
            }
            decoder.ReadStructDeferral<USER_SESSION_KEY>(ref this.UserSessionKey);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.LogonServer);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.LogonDomainName);
            if ((null != this.LogonDomainId)) {
                this.LogonDomainId.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.LogonDomainId.value);
            }
            if ((null != this.ExtraSids)) {
                this.ExtraSids.value = decoder.ReadArrayHeader<KERB_SID_AND_ATTRIBUTES>();
                for (int i = 0; (i < this.ExtraSids.value.Length); i++
                ) {
                    KERB_SID_AND_ATTRIBUTES elem_0 = this.ExtraSids.value[i];
                    elem_0 = decoder.ReadFixedStruct<KERB_SID_AND_ATTRIBUTES>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.ExtraSids.value[i] = elem_0;
                }
                for (int i = 0; (i < this.ExtraSids.value.Length); i++
                ) {
                    KERB_SID_AND_ATTRIBUTES elem_0 = this.ExtraSids.value[i];
                    decoder.ReadStructDeferral<KERB_SID_AND_ATTRIBUTES>(ref elem_0);
                    this.ExtraSids.value[i] = elem_0;
                }
            }
            if ((null != this.ResourceGroupDomainSid)) {
                this.ResourceGroupDomainSid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.ResourceGroupDomainSid.value);
            }
            if ((null != this.ResourceGroupIds)) {
                this.ResourceGroupIds.value = decoder.ReadArrayHeader<GROUP_MEMBERSHIP>();
                for (int i = 0; (i < this.ResourceGroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.ResourceGroupIds.value[i];
                    elem_0 = decoder.ReadFixedStruct<GROUP_MEMBERSHIP>(Titanis.DceRpc.NdrAlignment._4Byte);
                    this.ResourceGroupIds.value[i] = elem_0;
                }
                for (int i = 0; (i < this.ResourceGroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.ResourceGroupIds.value[i];
                    decoder.ReadStructDeferral<GROUP_MEMBERSHIP>(ref elem_0);
                    this.ResourceGroupIds.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct PAC_CREDENTIAL_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Version);
            encoder.WriteValue(this.EncryptionType);
            if ((this.SerializedData == null)) {
                this.SerializedData = new byte[1];
            }
            for (int i = 0; (i < 1); i++
            ) {
                byte elem_0 = this.SerializedData[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Version = decoder.ReadUInt32();
            this.EncryptionType = decoder.ReadUInt32();
            if ((this.SerializedData == null)) {
                this.SerializedData = new byte[1];
            }
            for (int i = 0; (i < 1); i++
            ) {
                byte elem_0 = this.SerializedData[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.SerializedData[i] = elem_0;
            }
        }
        public uint Version;
        public uint EncryptionType;
        public byte[] SerializedData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct SECPKG_SUPPLEMENTAL_CRED : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.PackageName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.CredentialSize);
            encoder.WritePointer(this.Credentials);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.PackageName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.CredentialSize = decoder.ReadUInt32();
            this.Credentials = decoder.ReadUniquePointer<byte[]>();
        }
        public ms_dtyp.RPC_UNICODE_STRING PackageName;
        public uint CredentialSize;
        public RpcPointer<byte[]> Credentials;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.PackageName);
            if ((null != this.Credentials)) {
                encoder.WriteArrayHeader(this.Credentials.value);
                for (int i = 0; (i < this.Credentials.value.Length); i++
                ) {
                    byte elem_0 = this.Credentials.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.PackageName);
            if ((null != this.Credentials)) {
                this.Credentials.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.Credentials.value.Length); i++
                ) {
                    byte elem_0 = this.Credentials.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.Credentials.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct PAC_CREDENTIAL_DATA : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.CredentialCount);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.CredentialCount = decoder.ReadUInt32();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.Credentials);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Credentials = decoder.ReadArrayHeader<SECPKG_SUPPLEMENTAL_CRED>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.Credentials.Length); i++
            ) {
                SECPKG_SUPPLEMENTAL_CRED elem_0 = this.Credentials[i];
                encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.Credentials.Length); i++
            ) {
                SECPKG_SUPPLEMENTAL_CRED elem_0 = this.Credentials[i];
                elem_0 = decoder.ReadFixedStruct<SECPKG_SUPPLEMENTAL_CRED>(Titanis.DceRpc.NdrAlignment.NativePtr);
                this.Credentials[i] = elem_0;
            }
        }
        public uint CredentialCount;
        public SECPKG_SUPPLEMENTAL_CRED[] Credentials;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.Credentials.Length); i++
            ) {
                SECPKG_SUPPLEMENTAL_CRED elem_0 = this.Credentials[i];
                encoder.WriteStructDeferral(elem_0);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.Credentials.Length); i++
            ) {
                SECPKG_SUPPLEMENTAL_CRED elem_0 = this.Credentials[i];
                decoder.ReadStructDeferral<SECPKG_SUPPLEMENTAL_CRED>(ref elem_0);
                this.Credentials[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct NTLM_SUPPLEMENTAL_CREDENTIAL : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Version);
            encoder.WriteValue(this.Flags);
            if ((this.LmPassword == null)) {
                this.LmPassword = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.LmPassword[i];
                encoder.WriteValue(elem_0);
            }
            if ((this.NtPassword == null)) {
                this.NtPassword = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.NtPassword[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Version = decoder.ReadUInt32();
            this.Flags = decoder.ReadUInt32();
            if ((this.LmPassword == null)) {
                this.LmPassword = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.LmPassword[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.LmPassword[i] = elem_0;
            }
            if ((this.NtPassword == null)) {
                this.NtPassword = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.NtPassword[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.NtPassword[i] = elem_0;
            }
        }
        public uint Version;
        public uint Flags;
        public byte[] LmPassword;
        public byte[] NtPassword;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct PAC_CLIENT_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.ClientId, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteValue(this.NameLength);
            if ((this.Name == null)) {
                this.Name = new char[1];
            }
            for (int i = 0; (i < 1); i++
            ) {
                char elem_0 = this.Name[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ClientId = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.NameLength = decoder.ReadUInt16();
            if ((this.Name == null)) {
                this.Name = new char[1];
            }
            for (int i = 0; (i < 1); i++
            ) {
                char elem_0 = this.Name[i];
                elem_0 = decoder.ReadWideChar();
                this.Name[i] = elem_0;
            }
        }
        public ms_dtyp.FILETIME ClientId;
        public ushort NameLength;
        public char[] Name;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.ClientId);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref this.ClientId);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct PAC_SIGNATURE_DATA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.SignatureType);
            if ((this.Signature == null)) {
                this.Signature = new byte[1];
            }
            for (int i = 0; (i < 1); i++
            ) {
                byte elem_0 = this.Signature[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.SignatureType = decoder.ReadUInt32();
            if ((this.Signature == null)) {
                this.Signature = new byte[1];
            }
            for (int i = 0; (i < 1); i++
            ) {
                byte elem_0 = this.Signature[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.Signature[i] = elem_0;
            }
        }
        public uint SignatureType;
        public byte[] Signature;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct S4U_DELEGATION_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.S4U2proxyTarget, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.TransitedListSize);
            encoder.WritePointer(this.S4UTransitedServices);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.S4U2proxyTarget = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.TransitedListSize = decoder.ReadUInt32();
            this.S4UTransitedServices = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING[]>();
        }
        public ms_dtyp.RPC_UNICODE_STRING S4U2proxyTarget;
        public uint TransitedListSize;
        public RpcPointer<ms_dtyp.RPC_UNICODE_STRING[]> S4UTransitedServices;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.S4U2proxyTarget);
            if ((null != this.S4UTransitedServices)) {
                encoder.WriteArrayHeader(this.S4UTransitedServices.value);
                for (int i = 0; (i < this.S4UTransitedServices.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.S4UTransitedServices.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.S4UTransitedServices.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.S4UTransitedServices.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.S4U2proxyTarget);
            if ((null != this.S4UTransitedServices)) {
                this.S4UTransitedServices.value = decoder.ReadArrayHeader<ms_dtyp.RPC_UNICODE_STRING>();
                for (int i = 0; (i < this.S4UTransitedServices.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.S4UTransitedServices.value[i];
                    elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.S4UTransitedServices.value[i] = elem_0;
                }
                for (int i = 0; (i < this.S4UTransitedServices.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.S4UTransitedServices.value[i];
                    decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
                    this.S4UTransitedServices.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct UPN_DNS_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.UpnLength);
            encoder.WriteValue(this.UpnOffset);
            encoder.WriteValue(this.DnsDomainNameLength);
            encoder.WriteValue(this.DnsDomainNameOffset);
            encoder.WriteValue(this.Flags);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UpnLength = decoder.ReadUInt16();
            this.UpnOffset = decoder.ReadUInt16();
            this.DnsDomainNameLength = decoder.ReadUInt16();
            this.DnsDomainNameOffset = decoder.ReadUInt16();
            this.Flags = decoder.ReadUInt32();
        }
        public ushort UpnLength;
        public ushort UpnOffset;
        public ushort DnsDomainNameLength;
        public ushort DnsDomainNameOffset;
        public uint Flags;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct PAC_DEVICE_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.UserId);
            encoder.WriteValue(this.PrimaryGroupId);
            encoder.WritePointer(this.AccountDomainId);
            encoder.WriteValue(this.AccountGroupCount);
            encoder.WritePointer(this.AccountGroupIds);
            encoder.WriteValue(this.SidCount);
            encoder.WritePointer(this.ExtraSids);
            encoder.WriteValue(this.DomainGroupCount);
            encoder.WritePointer(this.DomainGroup);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UserId = decoder.ReadUInt32();
            this.PrimaryGroupId = decoder.ReadUInt32();
            this.AccountDomainId = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            this.AccountGroupCount = decoder.ReadUInt32();
            this.AccountGroupIds = decoder.ReadUniquePointer<GROUP_MEMBERSHIP[]>();
            this.SidCount = decoder.ReadUInt32();
            this.ExtraSids = decoder.ReadUniquePointer<KERB_SID_AND_ATTRIBUTES[]>();
            this.DomainGroupCount = decoder.ReadUInt32();
            this.DomainGroup = decoder.ReadUniquePointer<DOMAIN_GROUP_MEMBERSHIP[]>();
        }
        public uint UserId;
        public uint PrimaryGroupId;
        public RpcPointer<ms_dtyp.RPC_SID> AccountDomainId;
        public uint AccountGroupCount;
        public RpcPointer<GROUP_MEMBERSHIP[]> AccountGroupIds;
        public uint SidCount;
        public RpcPointer<KERB_SID_AND_ATTRIBUTES[]> ExtraSids;
        public uint DomainGroupCount;
        public RpcPointer<DOMAIN_GROUP_MEMBERSHIP[]> DomainGroup;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.AccountDomainId)) {
                encoder.WriteConformantStruct(this.AccountDomainId.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.AccountDomainId.value);
            }
            if ((null != this.AccountGroupIds)) {
                encoder.WriteArrayHeader(this.AccountGroupIds.value);
                for (int i = 0; (i < this.AccountGroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.AccountGroupIds.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
                }
                for (int i = 0; (i < this.AccountGroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.AccountGroupIds.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
            if ((null != this.ExtraSids)) {
                encoder.WriteArrayHeader(this.ExtraSids.value);
                for (int i = 0; (i < this.ExtraSids.value.Length); i++
                ) {
                    KERB_SID_AND_ATTRIBUTES elem_0 = this.ExtraSids.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.ExtraSids.value.Length); i++
                ) {
                    KERB_SID_AND_ATTRIBUTES elem_0 = this.ExtraSids.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
            if ((null != this.DomainGroup)) {
                encoder.WriteArrayHeader(this.DomainGroup.value);
                for (int i = 0; (i < this.DomainGroup.value.Length); i++
                ) {
                    DOMAIN_GROUP_MEMBERSHIP elem_0 = this.DomainGroup.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.DomainGroup.value.Length); i++
                ) {
                    DOMAIN_GROUP_MEMBERSHIP elem_0 = this.DomainGroup.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.AccountDomainId)) {
                this.AccountDomainId.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.AccountDomainId.value);
            }
            if ((null != this.AccountGroupIds)) {
                this.AccountGroupIds.value = decoder.ReadArrayHeader<GROUP_MEMBERSHIP>();
                for (int i = 0; (i < this.AccountGroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.AccountGroupIds.value[i];
                    elem_0 = decoder.ReadFixedStruct<GROUP_MEMBERSHIP>(Titanis.DceRpc.NdrAlignment._4Byte);
                    this.AccountGroupIds.value[i] = elem_0;
                }
                for (int i = 0; (i < this.AccountGroupIds.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.AccountGroupIds.value[i];
                    decoder.ReadStructDeferral<GROUP_MEMBERSHIP>(ref elem_0);
                    this.AccountGroupIds.value[i] = elem_0;
                }
            }
            if ((null != this.ExtraSids)) {
                this.ExtraSids.value = decoder.ReadArrayHeader<KERB_SID_AND_ATTRIBUTES>();
                for (int i = 0; (i < this.ExtraSids.value.Length); i++
                ) {
                    KERB_SID_AND_ATTRIBUTES elem_0 = this.ExtraSids.value[i];
                    elem_0 = decoder.ReadFixedStruct<KERB_SID_AND_ATTRIBUTES>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.ExtraSids.value[i] = elem_0;
                }
                for (int i = 0; (i < this.ExtraSids.value.Length); i++
                ) {
                    KERB_SID_AND_ATTRIBUTES elem_0 = this.ExtraSids.value[i];
                    decoder.ReadStructDeferral<KERB_SID_AND_ATTRIBUTES>(ref elem_0);
                    this.ExtraSids.value[i] = elem_0;
                }
            }
            if ((null != this.DomainGroup)) {
                this.DomainGroup.value = decoder.ReadArrayHeader<DOMAIN_GROUP_MEMBERSHIP>();
                for (int i = 0; (i < this.DomainGroup.value.Length); i++
                ) {
                    DOMAIN_GROUP_MEMBERSHIP elem_0 = this.DomainGroup.value[i];
                    elem_0 = decoder.ReadFixedStruct<DOMAIN_GROUP_MEMBERSHIP>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.DomainGroup.value[i] = elem_0;
                }
                for (int i = 0; (i < this.DomainGroup.value.Length); i++
                ) {
                    DOMAIN_GROUP_MEMBERSHIP elem_0 = this.DomainGroup.value[i];
                    decoder.ReadStructDeferral<DOMAIN_GROUP_MEMBERSHIP>(ref elem_0);
                    this.DomainGroup.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.7")]
    public struct PAC_ATTRIBUTES_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.FlagsLength);
            if ((this.Flags == null)) {
                this.Flags = new uint[1];
            }
            for (int i = 0; (i < 1); i++
            ) {
                uint elem_0 = this.Flags[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.FlagsLength = decoder.ReadUInt32();
            if ((this.Flags == null)) {
                this.Flags = new uint[1];
            }
            for (int i = 0; (i < 1); i++
            ) {
                uint elem_0 = this.Flags[i];
                elem_0 = decoder.ReadUInt32();
                this.Flags[i] = elem_0;
            }
        }
        public uint FlagsLength;
        public uint[] Flags;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
}
