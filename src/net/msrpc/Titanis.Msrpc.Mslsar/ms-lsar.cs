#pragma warning disable

namespace ms_lsar {
    using System;
    using System.Threading.Tasks;
    using Titanis;
	using Titanis.DceRpc;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct STRING : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Length);
            encoder.WriteValue(this.MaximumLength);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Length = decoder.ReadUInt16();
            this.MaximumLength = decoder.ReadUInt16();
            this.Buffer = decoder.ReadPointer<ArraySegment<byte>>();
        }
        public ushort Length;
        public ushort MaximumLength;
        public RpcPointer<ArraySegment<byte>> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value, true);
                for (int i = 0; (i < this.Buffer.value.Count); i++
                ) {
                    byte elem_0 = this.Buffer.value.Item(i);
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArraySegmentHeader<byte>();
                for (int i = 0; (i < this.Buffer.value.Count); i++
                ) {
                    byte elem_0 = this.Buffer.value.Item(i);
                    elem_0 = decoder.ReadUnsignedChar();
                    this.Buffer.value.Item(i) = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_ACL : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.AclRevision);
            encoder.WriteValue(this.Sbz1);
            encoder.WriteValue(this.AclSize);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.AclRevision = decoder.ReadUnsignedChar();
            this.Sbz1 = decoder.ReadUnsignedChar();
            this.AclSize = decoder.ReadUInt16();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.Dummy1);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Dummy1 = decoder.ReadArrayHeader<byte>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.Dummy1.Length); i++
            ) {
                byte elem_0 = this.Dummy1[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.Dummy1.Length); i++
            ) {
                byte elem_0 = this.Dummy1[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.Dummy1[i] = elem_0;
            }
        }
        public byte AclRevision;
        public byte Sbz1;
        public ushort AclSize;
        public byte[] Dummy1;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_SECURITY_DESCRIPTOR : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Revision);
            encoder.WriteValue(this.Sbz1);
            encoder.WriteValue(this.Control);
            encoder.WritePointer(this.Owner);
            encoder.WritePointer(this.Group);
            encoder.WritePointer(this.Sacl);
            encoder.WritePointer(this.Dacl);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Revision = decoder.ReadUnsignedChar();
            this.Sbz1 = decoder.ReadUnsignedChar();
            this.Control = decoder.ReadUInt16();
            this.Owner = decoder.ReadPointer<ms_dtyp.RPC_SID>();
            this.Group = decoder.ReadPointer<ms_dtyp.RPC_SID>();
            this.Sacl = decoder.ReadPointer<LSAPR_ACL>();
            this.Dacl = decoder.ReadPointer<LSAPR_ACL>();
        }
        public byte Revision;
        public byte Sbz1;
        public ushort Control;
        public RpcPointer<ms_dtyp.RPC_SID> Owner;
        public RpcPointer<ms_dtyp.RPC_SID> Group;
        public RpcPointer<LSAPR_ACL> Sacl;
        public RpcPointer<LSAPR_ACL> Dacl;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Owner)) {
                encoder.WriteConformantStruct(this.Owner.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.Owner.value);
            }
            if ((null != this.Group)) {
                encoder.WriteConformantStruct(this.Group.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.Group.value);
            }
            if ((null != this.Sacl)) {
                encoder.WriteConformantStruct(this.Sacl.value, Titanis.DceRpc.NdrAlignment._2Byte);
                encoder.WriteStructDeferral(this.Sacl.value);
            }
            if ((null != this.Dacl)) {
                encoder.WriteConformantStruct(this.Dacl.value, Titanis.DceRpc.NdrAlignment._2Byte);
                encoder.WriteStructDeferral(this.Dacl.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Owner)) {
                this.Owner.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Owner.value);
            }
            if ((null != this.Group)) {
                this.Group.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Group.value);
            }
            if ((null != this.Sacl)) {
                this.Sacl.value = decoder.ReadConformantStruct<LSAPR_ACL>(Titanis.DceRpc.NdrAlignment._2Byte);
                decoder.ReadStructDeferral<LSAPR_ACL>(ref this.Sacl.value);
            }
            if ((null != this.Dacl)) {
                this.Dacl.value = decoder.ReadConformantStruct<LSAPR_ACL>(Titanis.DceRpc.NdrAlignment._2Byte);
                decoder.ReadStructDeferral<LSAPR_ACL>(ref this.Dacl.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum SECURITY_IMPERSONATION_LEVEL : int {
        SecurityAnonymous = 0,
        SecurityIdentification = 1,
        SecurityImpersonation = 2,
        SecurityDelegation = 3,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SECURITY_QUALITY_OF_SERVICE : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Length);
            encoder.WriteValue(((short)(this.ImpersonationLevel)));
            encoder.WriteValue(this.ContextTrackingMode);
            encoder.WriteValue(this.EffectiveOnly);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Length = decoder.ReadUInt32();
            this.ImpersonationLevel = ((SECURITY_IMPERSONATION_LEVEL)(decoder.ReadInt16()));
            this.ContextTrackingMode = decoder.ReadUnsignedChar();
            this.EffectiveOnly = decoder.ReadUnsignedChar();
        }
        public uint Length;
        public SECURITY_IMPERSONATION_LEVEL ImpersonationLevel;
        public byte ContextTrackingMode;
        public byte EffectiveOnly;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_OBJECT_ATTRIBUTES : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Length);
            encoder.WritePointer(this.RootDirectory);
            encoder.WritePointer(this.ObjectName);
            encoder.WriteValue(this.Attributes);
            encoder.WritePointer(this.SecurityDescriptor);
            encoder.WritePointer(this.SecurityQualityOfService);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Length = decoder.ReadUInt32();
            this.RootDirectory = decoder.ReadPointer<byte>();
            this.ObjectName = decoder.ReadPointer<STRING>();
            this.Attributes = decoder.ReadUInt32();
            this.SecurityDescriptor = decoder.ReadPointer<LSAPR_SECURITY_DESCRIPTOR>();
            this.SecurityQualityOfService = decoder.ReadPointer<SECURITY_QUALITY_OF_SERVICE>();
        }
        public uint Length;
        public RpcPointer<byte> RootDirectory;
        public RpcPointer<STRING> ObjectName;
        public uint Attributes;
        public RpcPointer<LSAPR_SECURITY_DESCRIPTOR> SecurityDescriptor;
        public RpcPointer<SECURITY_QUALITY_OF_SERVICE> SecurityQualityOfService;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.RootDirectory)) {
                encoder.WriteValue(this.RootDirectory.value);
            }
            if ((null != this.ObjectName)) {
                encoder.WriteFixedStruct(this.ObjectName.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.ObjectName.value);
            }
            if ((null != this.SecurityDescriptor)) {
                encoder.WriteFixedStruct(this.SecurityDescriptor.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.SecurityDescriptor.value);
            }
            if ((null != this.SecurityQualityOfService)) {
                encoder.WriteFixedStruct(this.SecurityQualityOfService.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.SecurityQualityOfService.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.RootDirectory)) {
                this.RootDirectory.value = decoder.ReadUnsignedChar();
            }
            if ((null != this.ObjectName)) {
                this.ObjectName.value = decoder.ReadFixedStruct<STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<STRING>(ref this.ObjectName.value);
            }
            if ((null != this.SecurityDescriptor)) {
                this.SecurityDescriptor.value = decoder.ReadFixedStruct<LSAPR_SECURITY_DESCRIPTOR>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_SECURITY_DESCRIPTOR>(ref this.SecurityDescriptor.value);
            }
            if ((null != this.SecurityQualityOfService)) {
                this.SecurityQualityOfService.value = decoder.ReadFixedStruct<SECURITY_QUALITY_OF_SERVICE>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<SECURITY_QUALITY_OF_SERVICE>(ref this.SecurityQualityOfService.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUST_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WritePointer(this.Sid);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.Sid = decoder.ReadPointer<ms_dtyp.RPC_SID>();
        }
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public RpcPointer<ms_dtyp.RPC_SID> Sid;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
            if ((null != this.Sid)) {
                encoder.WriteConformantStruct(this.Sid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.Sid.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
            if ((null != this.Sid)) {
                this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum POLICY_INFORMATION_CLASS : int {
        PolicyAuditLogInformation = 1,
        PolicyAuditEventsInformation = 2,
        PolicyPrimaryDomainInformation = 3,
        PolicyPdAccountInformation = 4,
        PolicyAccountDomainInformation = 5,
        PolicyLsaServerRoleInformation = 6,
        PolicyReplicaSourceInformation = 7,
        PolicyInformationNotUsedOnWire = 8,
        PolicyModificationInformation = 9,
        PolicyAuditFullSetInformation = 10,
        PolicyAuditFullQueryInformation = 11,
        PolicyDnsDomainInformation = 12,
        PolicyDnsDomainInformationInt = 13,
        PolicyLocalAccountDomainInformation = 14,
        PolicyMachineAccountInformation = 15,
        PolicyLastEntry = 16,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum POLICY_AUDIT_EVENT_TYPE : int {
        AuditCategorySystem = 0,
        AuditCategoryLogon = 1,
        AuditCategoryObjectAccess = 2,
        AuditCategoryPrivilegeUse = 3,
        AuditCategoryDetailedTracking = 4,
        AuditCategoryPolicyChange = 5,
        AuditCategoryAccountManagement = 6,
        AuditCategoryDirectoryServiceAccess = 7,
        AuditCategoryAccountLogon = 8,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct POLICY_AUDIT_LOG_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.AuditLogPercentFull);
            encoder.WriteValue(this.MaximumLogSize);
            encoder.WriteFixedStruct(this.AuditRetentionPeriod, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteValue(this.AuditLogFullShutdownInProgress);
            encoder.WriteFixedStruct(this.TimeToShutdown, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteValue(this.NextAuditRecordId);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.AuditLogPercentFull = decoder.ReadUInt32();
            this.MaximumLogSize = decoder.ReadUInt32();
            this.AuditRetentionPeriod = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.AuditLogFullShutdownInProgress = decoder.ReadUnsignedChar();
            this.TimeToShutdown = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.NextAuditRecordId = decoder.ReadUInt32();
        }
        public uint AuditLogPercentFull;
        public uint MaximumLogSize;
        public ms_dtyp.LARGE_INTEGER AuditRetentionPeriod;
        public byte AuditLogFullShutdownInProgress;
        public ms_dtyp.LARGE_INTEGER TimeToShutdown;
        public uint NextAuditRecordId;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.AuditRetentionPeriod);
            encoder.WriteStructDeferral(this.TimeToShutdown);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.AuditRetentionPeriod);
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.TimeToShutdown);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum POLICY_LSA_SERVER_ROLE : int {
        PolicyServerRoleBackup = 2,
        PolicyServerRolePrimary = 3,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct POLICY_LSA_SERVER_ROLE_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.LsaServerRole)));
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.LsaServerRole = ((POLICY_LSA_SERVER_ROLE)(decoder.ReadInt16()));
        }
        public POLICY_LSA_SERVER_ROLE LsaServerRole;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct POLICY_MODIFICATION_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.ModifiedId, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteFixedStruct(this.DatabaseCreationTime, Titanis.DceRpc.NdrAlignment._8Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ModifiedId = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.DatabaseCreationTime = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
        }
        public ms_dtyp.LARGE_INTEGER ModifiedId;
        public ms_dtyp.LARGE_INTEGER DatabaseCreationTime;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.ModifiedId);
            encoder.WriteStructDeferral(this.DatabaseCreationTime);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.ModifiedId);
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.DatabaseCreationTime);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct POLICY_AUDIT_FULL_SET_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ShutDownOnFull);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ShutDownOnFull = decoder.ReadUnsignedChar();
        }
        public byte ShutDownOnFull;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct POLICY_AUDIT_FULL_QUERY_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ShutDownOnFull);
            encoder.WriteValue(this.LogIsFull);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ShutDownOnFull = decoder.ReadUnsignedChar();
            this.LogIsFull = decoder.ReadUnsignedChar();
        }
        public byte ShutDownOnFull;
        public byte LogIsFull;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum POLICY_DOMAIN_INFORMATION_CLASS : int {
        PolicyDomainQualityOfServiceInformation = 1,
        PolicyDomainEfsInformation = 2,
        PolicyDomainKerberosTicketInformation = 3,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct POLICY_DOMAIN_KERBEROS_TICKET_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.AuthenticationOptions);
            encoder.WriteFixedStruct(this.MaxServiceTicketAge, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteFixedStruct(this.MaxTicketAge, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteFixedStruct(this.MaxRenewAge, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteFixedStruct(this.MaxClockSkew, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteFixedStruct(this.Reserved, Titanis.DceRpc.NdrAlignment._8Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.AuthenticationOptions = decoder.ReadUInt32();
            this.MaxServiceTicketAge = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.MaxTicketAge = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.MaxRenewAge = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.MaxClockSkew = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.Reserved = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
        }
        public uint AuthenticationOptions;
        public ms_dtyp.LARGE_INTEGER MaxServiceTicketAge;
        public ms_dtyp.LARGE_INTEGER MaxTicketAge;
        public ms_dtyp.LARGE_INTEGER MaxRenewAge;
        public ms_dtyp.LARGE_INTEGER MaxClockSkew;
        public ms_dtyp.LARGE_INTEGER Reserved;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.MaxServiceTicketAge);
            encoder.WriteStructDeferral(this.MaxTicketAge);
            encoder.WriteStructDeferral(this.MaxRenewAge);
            encoder.WriteStructDeferral(this.MaxClockSkew);
            encoder.WriteStructDeferral(this.Reserved);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.MaxServiceTicketAge);
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.MaxTicketAge);
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.MaxRenewAge);
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.MaxClockSkew);
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.Reserved);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct TRUSTED_POSIX_OFFSET_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Offset);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Offset = decoder.ReadUInt32();
        }
        public uint Offset;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum TRUSTED_INFORMATION_CLASS : int {
        TrustedDomainNameInformation = 1,
        TrustedControllersInformation = 2,
        TrustedPosixOffsetInformation = 3,
        TrustedPasswordInformation = 4,
        TrustedDomainInformationBasic = 5,
        TrustedDomainInformationEx = 6,
        TrustedDomainAuthInformation = 7,
        TrustedDomainFullInformation = 8,
        TrustedDomainAuthInformationInternal = 9,
        TrustedDomainFullInformationInternal = 10,
        TrustedDomainInformationEx2Internal = 11,
        TrustedDomainFullInformation2Internal = 12,
        TrustedDomainSupportedEncryptionTypes = 13,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum LSA_FOREST_TRUST_RECORD_TYPE : int {
        ForestTrustTopLevelName = 0,
        ForestTrustTopLevelNameEx = 1,
        ForestTrustDomainInfo = 2,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSA_FOREST_TRUST_BINARY_DATA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Length);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Length = decoder.ReadUInt32();
            this.Buffer = decoder.ReadPointer<byte[]>();
        }
        public uint Length;
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
    public struct LSA_FOREST_TRUST_DOMAIN_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.Sid);
            encoder.WriteFixedStruct(this.DnsName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.NetbiosName, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Sid = decoder.ReadPointer<ms_dtyp.RPC_SID>();
            this.DnsName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.NetbiosName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public RpcPointer<ms_dtyp.RPC_SID> Sid;
        public ms_dtyp.RPC_UNICODE_STRING DnsName;
        public ms_dtyp.RPC_UNICODE_STRING NetbiosName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Sid)) {
                encoder.WriteConformantStruct(this.Sid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.Sid.value);
            }
            encoder.WriteStructDeferral(this.DnsName);
            encoder.WriteStructDeferral(this.NetbiosName);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Sid)) {
                this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
            }
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.DnsName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.NetbiosName);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct Unnamed_1 : Titanis.DceRpc.IRpcFixedStruct {
        public LSA_FOREST_TRUST_RECORD_TYPE ForestTrustType;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.ForestTrustType)));
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if (((((int)(this.ForestTrustType)) == 0) 
                        && (((int)(this.ForestTrustType)) == 1))) {
                encoder.WriteFixedStruct(this.TopLevelName, Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.ForestTrustType)) == 2)) {
                    encoder.WriteFixedStruct(this.DomainInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ForestTrustType = ((LSA_FOREST_TRUST_RECORD_TYPE)(decoder.ReadInt16()));
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if (((((int)(this.ForestTrustType)) == 0) 
                        && (((int)(this.ForestTrustType)) == 1))) {
                this.TopLevelName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.ForestTrustType)) == 2)) {
                    this.DomainInfo = decoder.ReadFixedStruct<LSA_FOREST_TRUST_DOMAIN_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if (((((int)(this.ForestTrustType)) == 0) 
                        && (((int)(this.ForestTrustType)) == 1))) {
                encoder.WriteStructDeferral(this.TopLevelName);
            }
            else {
                if ((((int)(this.ForestTrustType)) == 2)) {
                    encoder.WriteStructDeferral(this.DomainInfo);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if (((((int)(this.ForestTrustType)) == 0) 
                        && (((int)(this.ForestTrustType)) == 1))) {
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.TopLevelName);
            }
            else {
                if ((((int)(this.ForestTrustType)) == 2)) {
                    decoder.ReadStructDeferral<LSA_FOREST_TRUST_DOMAIN_INFO>(ref this.DomainInfo);
                }
            }
        }
        public ms_dtyp.RPC_UNICODE_STRING TopLevelName;
        public LSA_FOREST_TRUST_DOMAIN_INFO DomainInfo;
        public LSA_FOREST_TRUST_BINARY_DATA Data;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSA_FOREST_TRUST_RECORD : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Flags);
            encoder.WriteValue(((short)(this.ForestTrustType)));
            encoder.WriteFixedStruct(this.Time, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteUnion(this.ForestTrustData);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Flags = decoder.ReadUInt32();
            this.ForestTrustType = ((LSA_FOREST_TRUST_RECORD_TYPE)(decoder.ReadInt16()));
            this.Time = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.ForestTrustData = decoder.ReadUnion<Unnamed_1>();
        }
        public uint Flags;
        public LSA_FOREST_TRUST_RECORD_TYPE ForestTrustType;
        public ms_dtyp.LARGE_INTEGER Time;
        public Unnamed_1 ForestTrustData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Time);
            encoder.WriteStructDeferral(this.ForestTrustData);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.Time);
            decoder.ReadStructDeferral<Unnamed_1>(ref this.ForestTrustData);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSA_FOREST_TRUST_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.RecordCount);
            encoder.WritePointer(this.Entries);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.RecordCount = decoder.ReadUInt32();
            this.Entries = decoder.ReadPointer<RpcPointer<LSA_FOREST_TRUST_RECORD>[]>();
        }
        public uint RecordCount;
        public RpcPointer<RpcPointer<LSA_FOREST_TRUST_RECORD>[]> Entries;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Entries)) {
                encoder.WriteArrayHeader(this.Entries.value);
                for (int i = 0; (i < this.Entries.value.Length); i++
                ) {
                    RpcPointer<LSA_FOREST_TRUST_RECORD> elem_0 = this.Entries.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < this.Entries.value.Length); i++
                ) {
                    RpcPointer<LSA_FOREST_TRUST_RECORD> elem_0 = this.Entries.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._8Byte);
                        encoder.WriteStructDeferral(elem_0.value);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Entries)) {
                this.Entries.value = decoder.ReadArrayHeader<RpcPointer<LSA_FOREST_TRUST_RECORD>>();
                for (int i = 0; (i < this.Entries.value.Length); i++
                ) {
                    RpcPointer<LSA_FOREST_TRUST_RECORD> elem_0 = this.Entries.value[i];
                    elem_0 = decoder.ReadPointer<LSA_FOREST_TRUST_RECORD>();
                    this.Entries.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Entries.value.Length); i++
                ) {
                    RpcPointer<LSA_FOREST_TRUST_RECORD> elem_0 = this.Entries.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadFixedStruct<LSA_FOREST_TRUST_RECORD>(Titanis.DceRpc.NdrAlignment._8Byte);
                        decoder.ReadStructDeferral<LSA_FOREST_TRUST_RECORD>(ref elem_0.value);
                    }
                    this.Entries.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum LSA_FOREST_TRUST_COLLISION_RECORD_TYPE : int {
        CollisionTdo = 0,
        CollisionXref = 1,
        CollisionOther = 2,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSA_FOREST_TRUST_COLLISION_RECORD : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Index);
            encoder.WriteValue(((short)(this.Type)));
            encoder.WriteValue(this.Flags);
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Index = decoder.ReadUInt32();
            this.Type = ((LSA_FOREST_TRUST_COLLISION_RECORD_TYPE)(decoder.ReadInt16()));
            this.Flags = decoder.ReadUInt32();
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public uint Index;
        public LSA_FOREST_TRUST_COLLISION_RECORD_TYPE Type;
        public uint Flags;
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSA_FOREST_TRUST_COLLISION_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.RecordCount);
            encoder.WritePointer(this.Entries);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.RecordCount = decoder.ReadUInt32();
            this.Entries = decoder.ReadPointer<RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD>[]>();
        }
        public uint RecordCount;
        public RpcPointer<RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD>[]> Entries;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Entries)) {
                encoder.WriteArrayHeader(this.Entries.value);
                for (int i = 0; (i < this.Entries.value.Length); i++
                ) {
                    RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD> elem_0 = this.Entries.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < this.Entries.value.Length); i++
                ) {
                    RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD> elem_0 = this.Entries.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                        encoder.WriteStructDeferral(elem_0.value);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Entries)) {
                this.Entries.value = decoder.ReadArrayHeader<RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD>>();
                for (int i = 0; (i < this.Entries.value.Length); i++
                ) {
                    RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD> elem_0 = this.Entries.value[i];
                    elem_0 = decoder.ReadPointer<LSA_FOREST_TRUST_COLLISION_RECORD>();
                    this.Entries.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Entries.value.Length); i++
                ) {
                    RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD> elem_0 = this.Entries.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadFixedStruct<LSA_FOREST_TRUST_COLLISION_RECORD>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        decoder.ReadStructDeferral<LSA_FOREST_TRUST_COLLISION_RECORD>(ref elem_0.value);
                    }
                    this.Entries.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_ACCOUNT_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.Sid);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Sid = decoder.ReadPointer<ms_dtyp.RPC_SID>();
        }
        public RpcPointer<ms_dtyp.RPC_SID> Sid;
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_ACCOUNT_ENUM_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Information);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Information = decoder.ReadPointer<LSAPR_ACCOUNT_INFORMATION[]>();
        }
        public uint EntriesRead;
        public RpcPointer<LSAPR_ACCOUNT_INFORMATION[]> Information;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Information)) {
                encoder.WriteArrayHeader(this.Information.value);
                for (int i = 0; (i < this.Information.value.Length); i++
                ) {
                    LSAPR_ACCOUNT_INFORMATION elem_0 = this.Information.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Information.value.Length); i++
                ) {
                    LSAPR_ACCOUNT_INFORMATION elem_0 = this.Information.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Information)) {
                this.Information.value = decoder.ReadArrayHeader<LSAPR_ACCOUNT_INFORMATION>();
                for (int i = 0; (i < this.Information.value.Length); i++
                ) {
                    LSAPR_ACCOUNT_INFORMATION elem_0 = this.Information.value[i];
                    elem_0 = decoder.ReadFixedStruct<LSAPR_ACCOUNT_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Information.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Information.value.Length); i++
                ) {
                    LSAPR_ACCOUNT_INFORMATION elem_0 = this.Information.value[i];
                    decoder.ReadStructDeferral<LSAPR_ACCOUNT_INFORMATION>(ref elem_0);
                    this.Information.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_SR_SECURITY_DESCRIPTOR : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Length);
            encoder.WritePointer(this.SecurityDescriptor);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Length = decoder.ReadUInt32();
            this.SecurityDescriptor = decoder.ReadPointer<byte[]>();
        }
        public uint Length;
        public RpcPointer<byte[]> SecurityDescriptor;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.SecurityDescriptor)) {
                encoder.WriteArrayHeader(this.SecurityDescriptor.value);
                for (int i = 0; (i < this.SecurityDescriptor.value.Length); i++
                ) {
                    byte elem_0 = this.SecurityDescriptor.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.SecurityDescriptor)) {
                this.SecurityDescriptor.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.SecurityDescriptor.value.Length); i++
                ) {
                    byte elem_0 = this.SecurityDescriptor.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.SecurityDescriptor.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_LUID_AND_ATTRIBUTES : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Luid, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteValue(this.Attributes);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Luid = decoder.ReadFixedStruct<ms_dtyp.LUID>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.Attributes = decoder.ReadUInt32();
        }
        public ms_dtyp.LUID Luid;
        public uint Attributes;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Luid);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.LUID>(ref this.Luid);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_PRIVILEGE_SET : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.PrivilegeCount);
            encoder.WriteValue(this.Control);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.PrivilegeCount = decoder.ReadUInt32();
            this.Control = decoder.ReadUInt32();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.Privilege);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Privilege = decoder.ReadArrayHeader<LSAPR_LUID_AND_ATTRIBUTES>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.Privilege.Length); i++
            ) {
                LSAPR_LUID_AND_ATTRIBUTES elem_0 = this.Privilege[i];
                encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.Privilege.Length); i++
            ) {
                LSAPR_LUID_AND_ATTRIBUTES elem_0 = this.Privilege[i];
                elem_0 = decoder.ReadFixedStruct<LSAPR_LUID_AND_ATTRIBUTES>(Titanis.DceRpc.NdrAlignment._4Byte);
                this.Privilege[i] = elem_0;
            }
        }
        public uint PrivilegeCount;
        public uint Control;
        public LSAPR_LUID_AND_ATTRIBUTES[] Privilege;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.Privilege.Length); i++
            ) {
                LSAPR_LUID_AND_ATTRIBUTES elem_0 = this.Privilege[i];
                encoder.WriteStructDeferral(elem_0);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.Privilege.Length); i++
            ) {
                LSAPR_LUID_AND_ATTRIBUTES elem_0 = this.Privilege[i];
                decoder.ReadStructDeferral<LSAPR_LUID_AND_ATTRIBUTES>(ref elem_0);
                this.Privilege[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_POLICY_PRIVILEGE_DEF : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.LocalValue, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.LocalValue = decoder.ReadFixedStruct<ms_dtyp.LUID>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public ms_dtyp.LUID LocalValue;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
            encoder.WriteStructDeferral(this.LocalValue);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
            decoder.ReadStructDeferral<ms_dtyp.LUID>(ref this.LocalValue);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_PRIVILEGE_ENUM_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Entries);
            encoder.WritePointer(this.Privileges);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Entries = decoder.ReadUInt32();
            this.Privileges = decoder.ReadPointer<LSAPR_POLICY_PRIVILEGE_DEF[]>();
        }
        public uint Entries;
        public RpcPointer<LSAPR_POLICY_PRIVILEGE_DEF[]> Privileges;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Privileges)) {
                encoder.WriteArrayHeader(this.Privileges.value);
                for (int i = 0; (i < this.Privileges.value.Length); i++
                ) {
                    LSAPR_POLICY_PRIVILEGE_DEF elem_0 = this.Privileges.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Privileges.value.Length); i++
                ) {
                    LSAPR_POLICY_PRIVILEGE_DEF elem_0 = this.Privileges.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Privileges)) {
                this.Privileges.value = decoder.ReadArrayHeader<LSAPR_POLICY_PRIVILEGE_DEF>();
                for (int i = 0; (i < this.Privileges.value.Length); i++
                ) {
                    LSAPR_POLICY_PRIVILEGE_DEF elem_0 = this.Privileges.value[i];
                    elem_0 = decoder.ReadFixedStruct<LSAPR_POLICY_PRIVILEGE_DEF>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Privileges.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Privileges.value.Length); i++
                ) {
                    LSAPR_POLICY_PRIVILEGE_DEF elem_0 = this.Privileges.value[i];
                    decoder.ReadStructDeferral<LSAPR_POLICY_PRIVILEGE_DEF>(ref elem_0);
                    this.Privileges.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_CR_CIPHER_VALUE : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Length);
            encoder.WriteValue(this.MaximumLength);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Length = decoder.ReadUInt32();
            this.MaximumLength = decoder.ReadUInt32();
            this.Buffer = decoder.ReadPointer<ArraySegment<byte>>();
        }
        public uint Length;
        public uint MaximumLength;
        public RpcPointer<ArraySegment<byte>> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value, true);
                for (int i = 0; (i < this.Buffer.value.Count); i++
                ) {
                    byte elem_0 = this.Buffer.value.Item(i);
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArraySegmentHeader<byte>();
                for (int i = 0; (i < this.Buffer.value.Count); i++
                ) {
                    byte elem_0 = this.Buffer.value.Item(i);
                    elem_0 = decoder.ReadUnsignedChar();
                    this.Buffer.value.Item(i) = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_ENUM_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Information);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Information = decoder.ReadPointer<LSAPR_TRUST_INFORMATION[]>();
        }
        public uint EntriesRead;
        public RpcPointer<LSAPR_TRUST_INFORMATION[]> Information;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Information)) {
                encoder.WriteArrayHeader(this.Information.value);
                for (int i = 0; (i < this.Information.value.Length); i++
                ) {
                    LSAPR_TRUST_INFORMATION elem_0 = this.Information.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Information.value.Length); i++
                ) {
                    LSAPR_TRUST_INFORMATION elem_0 = this.Information.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Information)) {
                this.Information.value = decoder.ReadArrayHeader<LSAPR_TRUST_INFORMATION>();
                for (int i = 0; (i < this.Information.value.Length); i++
                ) {
                    LSAPR_TRUST_INFORMATION elem_0 = this.Information.value[i];
                    elem_0 = decoder.ReadFixedStruct<LSAPR_TRUST_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Information.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Information.value.Length); i++
                ) {
                    LSAPR_TRUST_INFORMATION elem_0 = this.Information.value[i];
                    decoder.ReadStructDeferral<LSAPR_TRUST_INFORMATION>(ref elem_0);
                    this.Information.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_POLICY_ACCOUNT_DOM_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.DomainName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WritePointer(this.DomainSid);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.DomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.DomainSid = decoder.ReadPointer<ms_dtyp.RPC_SID>();
        }
        public ms_dtyp.RPC_UNICODE_STRING DomainName;
        public RpcPointer<ms_dtyp.RPC_SID> DomainSid;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.DomainName);
            if ((null != this.DomainSid)) {
                encoder.WriteConformantStruct(this.DomainSid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.DomainSid.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.DomainName);
            if ((null != this.DomainSid)) {
                this.DomainSid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.DomainSid.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_POLICY_PRIMARY_DOM_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WritePointer(this.Sid);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.Sid = decoder.ReadPointer<ms_dtyp.RPC_SID>();
        }
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public RpcPointer<ms_dtyp.RPC_SID> Sid;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
            if ((null != this.Sid)) {
                encoder.WriteConformantStruct(this.Sid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.Sid.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
            if ((null != this.Sid)) {
                this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_POLICY_DNS_DOMAIN_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.DnsDomainName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.DnsForestName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.DomainGuid);
            encoder.WritePointer(this.Sid);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.DnsDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.DnsForestName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.DomainGuid = decoder.ReadUuid();
            this.Sid = decoder.ReadPointer<ms_dtyp.RPC_SID>();
        }
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public ms_dtyp.RPC_UNICODE_STRING DnsDomainName;
        public ms_dtyp.RPC_UNICODE_STRING DnsForestName;
        public System.Guid DomainGuid;
        public RpcPointer<ms_dtyp.RPC_SID> Sid;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
            encoder.WriteStructDeferral(this.DnsDomainName);
            encoder.WriteStructDeferral(this.DnsForestName);
            if ((null != this.Sid)) {
                encoder.WriteConformantStruct(this.Sid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.Sid.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.DnsDomainName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.DnsForestName);
            if ((null != this.Sid)) {
                this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_POLICY_PD_ACCOUNT_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_POLICY_REPLICA_SRCE_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.ReplicaSource, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.ReplicaAccountName, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ReplicaSource = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.ReplicaAccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING ReplicaSource;
        public ms_dtyp.RPC_UNICODE_STRING ReplicaAccountName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.ReplicaSource);
            encoder.WriteStructDeferral(this.ReplicaAccountName);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ReplicaSource);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ReplicaAccountName);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_POLICY_AUDIT_EVENTS_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.AuditingMode);
            encoder.WritePointer(this.EventAuditingOptions);
            encoder.WriteValue(this.MaximumAuditEventCount);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.AuditingMode = decoder.ReadUnsignedChar();
            this.EventAuditingOptions = decoder.ReadPointer<uint[]>();
            this.MaximumAuditEventCount = decoder.ReadUInt32();
        }
        public byte AuditingMode;
        public RpcPointer<uint[]> EventAuditingOptions;
        public uint MaximumAuditEventCount;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.EventAuditingOptions)) {
                encoder.WriteArrayHeader(this.EventAuditingOptions.value);
                for (int i = 0; (i < this.EventAuditingOptions.value.Length); i++
                ) {
                    uint elem_0 = this.EventAuditingOptions.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.EventAuditingOptions)) {
                this.EventAuditingOptions.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < this.EventAuditingOptions.value.Length); i++
                ) {
                    uint elem_0 = this.EventAuditingOptions.value[i];
                    elem_0 = decoder.ReadUInt32();
                    this.EventAuditingOptions.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_POLICY_MACHINE_ACCT_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Rid);
            encoder.WritePointer(this.Sid);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Rid = decoder.ReadUInt32();
            this.Sid = decoder.ReadPointer<ms_dtyp.RPC_SID>();
        }
        public uint Rid;
        public RpcPointer<ms_dtyp.RPC_SID> Sid;
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_POLICY_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public POLICY_INFORMATION_CLASS unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.unionSwitch)));
            encoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteFixedStruct(this.PolicyAuditLogInfo, Titanis.DceRpc.NdrAlignment._8Byte);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteFixedStruct(this.PolicyAuditEventsInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteFixedStruct(this.PolicyPrimaryDomainInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 5)) {
                            encoder.WriteFixedStruct(this.PolicyAccountDomainInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 4)) {
                                encoder.WriteFixedStruct(this.PolicyPdAccountInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    encoder.WriteFixedStruct(this.PolicyServerRoleInfo, Titanis.DceRpc.NdrAlignment._2Byte);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        encoder.WriteFixedStruct(this.PolicyReplicaSourceInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 9)) {
                                            encoder.WriteFixedStruct(this.PolicyModificationInfo, Titanis.DceRpc.NdrAlignment._8Byte);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 10)) {
                                                encoder.WriteFixedStruct(this.PolicyAuditFullSetInfo, Titanis.DceRpc.NdrAlignment._1Byte);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 11)) {
                                                    encoder.WriteFixedStruct(this.PolicyAuditFullQueryInfo, Titanis.DceRpc.NdrAlignment._1Byte);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 12)) {
                                                        encoder.WriteFixedStruct(this.PolicyDnsDomainInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 13)) {
                                                            encoder.WriteFixedStruct(this.PolicyDnsDomainInfoInt, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 14)) {
                                                                encoder.WriteFixedStruct(this.PolicyLocalAccountDomainInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                            }
                                                            else {
                                                                if ((((int)(this.unionSwitch)) == 15)) {
                                                                    encoder.WriteFixedStruct(this.PolicyMachineAccountInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
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
            this.unionSwitch = ((POLICY_INFORMATION_CLASS)(decoder.ReadInt16()));
            decoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.PolicyAuditLogInfo = decoder.ReadFixedStruct<POLICY_AUDIT_LOG_INFO>(Titanis.DceRpc.NdrAlignment._8Byte);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    this.PolicyAuditEventsInfo = decoder.ReadFixedStruct<LSAPR_POLICY_AUDIT_EVENTS_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        this.PolicyPrimaryDomainInfo = decoder.ReadFixedStruct<LSAPR_POLICY_PRIMARY_DOM_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 5)) {
                            this.PolicyAccountDomainInfo = decoder.ReadFixedStruct<LSAPR_POLICY_ACCOUNT_DOM_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 4)) {
                                this.PolicyPdAccountInfo = decoder.ReadFixedStruct<LSAPR_POLICY_PD_ACCOUNT_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    this.PolicyServerRoleInfo = decoder.ReadFixedStruct<POLICY_LSA_SERVER_ROLE_INFO>(Titanis.DceRpc.NdrAlignment._2Byte);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        this.PolicyReplicaSourceInfo = decoder.ReadFixedStruct<LSAPR_POLICY_REPLICA_SRCE_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 9)) {
                                            this.PolicyModificationInfo = decoder.ReadFixedStruct<POLICY_MODIFICATION_INFO>(Titanis.DceRpc.NdrAlignment._8Byte);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 10)) {
                                                this.PolicyAuditFullSetInfo = decoder.ReadFixedStruct<POLICY_AUDIT_FULL_SET_INFO>(Titanis.DceRpc.NdrAlignment._1Byte);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 11)) {
                                                    this.PolicyAuditFullQueryInfo = decoder.ReadFixedStruct<POLICY_AUDIT_FULL_QUERY_INFO>(Titanis.DceRpc.NdrAlignment._1Byte);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 12)) {
                                                        this.PolicyDnsDomainInfo = decoder.ReadFixedStruct<LSAPR_POLICY_DNS_DOMAIN_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 13)) {
                                                            this.PolicyDnsDomainInfoInt = decoder.ReadFixedStruct<LSAPR_POLICY_DNS_DOMAIN_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 14)) {
                                                                this.PolicyLocalAccountDomainInfo = decoder.ReadFixedStruct<LSAPR_POLICY_ACCOUNT_DOM_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                            }
                                                            else {
                                                                if ((((int)(this.unionSwitch)) == 15)) {
                                                                    this.PolicyMachineAccountInfo = decoder.ReadFixedStruct<LSAPR_POLICY_MACHINE_ACCT_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
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
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteStructDeferral(this.PolicyAuditLogInfo);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteStructDeferral(this.PolicyAuditEventsInfo);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteStructDeferral(this.PolicyPrimaryDomainInfo);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 5)) {
                            encoder.WriteStructDeferral(this.PolicyAccountDomainInfo);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 4)) {
                                encoder.WriteStructDeferral(this.PolicyPdAccountInfo);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    encoder.WriteStructDeferral(this.PolicyServerRoleInfo);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        encoder.WriteStructDeferral(this.PolicyReplicaSourceInfo);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 9)) {
                                            encoder.WriteStructDeferral(this.PolicyModificationInfo);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 10)) {
                                                encoder.WriteStructDeferral(this.PolicyAuditFullSetInfo);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 11)) {
                                                    encoder.WriteStructDeferral(this.PolicyAuditFullQueryInfo);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 12)) {
                                                        encoder.WriteStructDeferral(this.PolicyDnsDomainInfo);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 13)) {
                                                            encoder.WriteStructDeferral(this.PolicyDnsDomainInfoInt);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 14)) {
                                                                encoder.WriteStructDeferral(this.PolicyLocalAccountDomainInfo);
                                                            }
                                                            else {
                                                                if ((((int)(this.unionSwitch)) == 15)) {
                                                                    encoder.WriteStructDeferral(this.PolicyMachineAccountInfo);
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
            if ((((int)(this.unionSwitch)) == 1)) {
                decoder.ReadStructDeferral<POLICY_AUDIT_LOG_INFO>(ref this.PolicyAuditLogInfo);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    decoder.ReadStructDeferral<LSAPR_POLICY_AUDIT_EVENTS_INFO>(ref this.PolicyAuditEventsInfo);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        decoder.ReadStructDeferral<LSAPR_POLICY_PRIMARY_DOM_INFO>(ref this.PolicyPrimaryDomainInfo);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 5)) {
                            decoder.ReadStructDeferral<LSAPR_POLICY_ACCOUNT_DOM_INFO>(ref this.PolicyAccountDomainInfo);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 4)) {
                                decoder.ReadStructDeferral<LSAPR_POLICY_PD_ACCOUNT_INFO>(ref this.PolicyPdAccountInfo);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    decoder.ReadStructDeferral<POLICY_LSA_SERVER_ROLE_INFO>(ref this.PolicyServerRoleInfo);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        decoder.ReadStructDeferral<LSAPR_POLICY_REPLICA_SRCE_INFO>(ref this.PolicyReplicaSourceInfo);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 9)) {
                                            decoder.ReadStructDeferral<POLICY_MODIFICATION_INFO>(ref this.PolicyModificationInfo);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 10)) {
                                                decoder.ReadStructDeferral<POLICY_AUDIT_FULL_SET_INFO>(ref this.PolicyAuditFullSetInfo);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 11)) {
                                                    decoder.ReadStructDeferral<POLICY_AUDIT_FULL_QUERY_INFO>(ref this.PolicyAuditFullQueryInfo);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 12)) {
                                                        decoder.ReadStructDeferral<LSAPR_POLICY_DNS_DOMAIN_INFO>(ref this.PolicyDnsDomainInfo);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 13)) {
                                                            decoder.ReadStructDeferral<LSAPR_POLICY_DNS_DOMAIN_INFO>(ref this.PolicyDnsDomainInfoInt);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 14)) {
                                                                decoder.ReadStructDeferral<LSAPR_POLICY_ACCOUNT_DOM_INFO>(ref this.PolicyLocalAccountDomainInfo);
                                                            }
                                                            else {
                                                                if ((((int)(this.unionSwitch)) == 15)) {
                                                                    decoder.ReadStructDeferral<LSAPR_POLICY_MACHINE_ACCT_INFO>(ref this.PolicyMachineAccountInfo);
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
        public POLICY_AUDIT_LOG_INFO PolicyAuditLogInfo;
        public LSAPR_POLICY_AUDIT_EVENTS_INFO PolicyAuditEventsInfo;
        public LSAPR_POLICY_PRIMARY_DOM_INFO PolicyPrimaryDomainInfo;
        public LSAPR_POLICY_ACCOUNT_DOM_INFO PolicyAccountDomainInfo;
        public LSAPR_POLICY_PD_ACCOUNT_INFO PolicyPdAccountInfo;
        public POLICY_LSA_SERVER_ROLE_INFO PolicyServerRoleInfo;
        public LSAPR_POLICY_REPLICA_SRCE_INFO PolicyReplicaSourceInfo;
        public POLICY_MODIFICATION_INFO PolicyModificationInfo;
        public POLICY_AUDIT_FULL_SET_INFO PolicyAuditFullSetInfo;
        public POLICY_AUDIT_FULL_QUERY_INFO PolicyAuditFullQueryInfo;
        public LSAPR_POLICY_DNS_DOMAIN_INFO PolicyDnsDomainInfo;
        public LSAPR_POLICY_DNS_DOMAIN_INFO PolicyDnsDomainInfoInt;
        public LSAPR_POLICY_ACCOUNT_DOM_INFO PolicyLocalAccountDomainInfo;
        public LSAPR_POLICY_MACHINE_ACCT_INFO PolicyMachineAccountInfo;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct POLICY_DOMAIN_QUALITY_OF_SERVICE_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.QualityOfService);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.QualityOfService = decoder.ReadUInt32();
        }
        public uint QualityOfService;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_POLICY_DOMAIN_EFS_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.InfoLength);
            encoder.WritePointer(this.EfsBlob);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.InfoLength = decoder.ReadUInt32();
            this.EfsBlob = decoder.ReadPointer<byte[]>();
        }
        public uint InfoLength;
        public RpcPointer<byte[]> EfsBlob;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.EfsBlob)) {
                encoder.WriteArrayHeader(this.EfsBlob.value);
                for (int i = 0; (i < this.EfsBlob.value.Length); i++
                ) {
                    byte elem_0 = this.EfsBlob.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.EfsBlob)) {
                this.EfsBlob.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.EfsBlob.value.Length); i++
                ) {
                    byte elem_0 = this.EfsBlob.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.EfsBlob.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_POLICY_DOMAIN_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public POLICY_DOMAIN_INFORMATION_CLASS unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.unionSwitch)));
            encoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteFixedStruct(this.PolicyDomainQualityOfServiceInfo, Titanis.DceRpc.NdrAlignment._4Byte);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteFixedStruct(this.PolicyDomainEfsInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteFixedStruct(this.PolicyDomainKerbTicketInfo, Titanis.DceRpc.NdrAlignment._8Byte);
                    }
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.unionSwitch = ((POLICY_DOMAIN_INFORMATION_CLASS)(decoder.ReadInt16()));
            decoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.PolicyDomainQualityOfServiceInfo = decoder.ReadFixedStruct<POLICY_DOMAIN_QUALITY_OF_SERVICE_INFO>(Titanis.DceRpc.NdrAlignment._4Byte);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    this.PolicyDomainEfsInfo = decoder.ReadFixedStruct<LSAPR_POLICY_DOMAIN_EFS_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        this.PolicyDomainKerbTicketInfo = decoder.ReadFixedStruct<POLICY_DOMAIN_KERBEROS_TICKET_INFO>(Titanis.DceRpc.NdrAlignment._8Byte);
                    }
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteStructDeferral(this.PolicyDomainQualityOfServiceInfo);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteStructDeferral(this.PolicyDomainEfsInfo);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteStructDeferral(this.PolicyDomainKerbTicketInfo);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                decoder.ReadStructDeferral<POLICY_DOMAIN_QUALITY_OF_SERVICE_INFO>(ref this.PolicyDomainQualityOfServiceInfo);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    decoder.ReadStructDeferral<LSAPR_POLICY_DOMAIN_EFS_INFO>(ref this.PolicyDomainEfsInfo);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        decoder.ReadStructDeferral<POLICY_DOMAIN_KERBEROS_TICKET_INFO>(ref this.PolicyDomainKerbTicketInfo);
                    }
                }
            }
        }
        public POLICY_DOMAIN_QUALITY_OF_SERVICE_INFO PolicyDomainQualityOfServiceInfo;
        public LSAPR_POLICY_DOMAIN_EFS_INFO PolicyDomainEfsInfo;
        public POLICY_DOMAIN_KERBEROS_TICKET_INFO PolicyDomainKerbTicketInfo;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_DOMAIN_NAME_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_CONTROLLERS_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Entries);
            encoder.WritePointer(this.Names);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Entries = decoder.ReadUInt32();
            this.Names = decoder.ReadPointer<ms_dtyp.RPC_UNICODE_STRING[]>();
        }
        public uint Entries;
        public RpcPointer<ms_dtyp.RPC_UNICODE_STRING[]> Names;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Names)) {
                encoder.WriteArrayHeader(this.Names.value);
                for (int i = 0; (i < this.Names.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Names.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Names.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Names.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Names)) {
                this.Names.value = decoder.ReadArrayHeader<ms_dtyp.RPC_UNICODE_STRING>();
                for (int i = 0; (i < this.Names.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Names.value[i];
                    elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Names.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Names.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Names.value[i];
                    decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
                    this.Names.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_PASSWORD_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.Password);
            encoder.WritePointer(this.OldPassword);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Password = decoder.ReadPointer<LSAPR_CR_CIPHER_VALUE>();
            this.OldPassword = decoder.ReadPointer<LSAPR_CR_CIPHER_VALUE>();
        }
        public RpcPointer<LSAPR_CR_CIPHER_VALUE> Password;
        public RpcPointer<LSAPR_CR_CIPHER_VALUE> OldPassword;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Password)) {
                encoder.WriteFixedStruct(this.Password.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.Password.value);
            }
            if ((null != this.OldPassword)) {
                encoder.WriteFixedStruct(this.OldPassword.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.OldPassword.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Password)) {
                this.Password.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref this.Password.value);
            }
            if ((null != this.OldPassword)) {
                this.OldPassword.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref this.OldPassword.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_DOMAIN_INFORMATION_EX : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.FlatName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WritePointer(this.Sid);
            encoder.WriteValue(this.TrustDirection);
            encoder.WriteValue(this.TrustType);
            encoder.WriteValue(this.TrustAttributes);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.FlatName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.Sid = decoder.ReadPointer<ms_dtyp.RPC_SID>();
            this.TrustDirection = decoder.ReadUInt32();
            this.TrustType = decoder.ReadUInt32();
            this.TrustAttributes = decoder.ReadUInt32();
        }
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public ms_dtyp.RPC_UNICODE_STRING FlatName;
        public RpcPointer<ms_dtyp.RPC_SID> Sid;
        public uint TrustDirection;
        public uint TrustType;
        public uint TrustAttributes;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
            encoder.WriteStructDeferral(this.FlatName);
            if ((null != this.Sid)) {
                encoder.WriteConformantStruct(this.Sid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.Sid.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FlatName);
            if ((null != this.Sid)) {
                this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_AUTH_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.LastUpdateTime, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteValue(this.AuthType);
            encoder.WriteValue(this.AuthInfoLength);
            encoder.WritePointer(this.AuthInfo);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.LastUpdateTime = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.AuthType = decoder.ReadUInt32();
            this.AuthInfoLength = decoder.ReadUInt32();
            this.AuthInfo = decoder.ReadPointer<byte[]>();
        }
        public ms_dtyp.LARGE_INTEGER LastUpdateTime;
        public uint AuthType;
        public uint AuthInfoLength;
        public RpcPointer<byte[]> AuthInfo;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.LastUpdateTime);
            if ((null != this.AuthInfo)) {
                encoder.WriteArrayHeader(this.AuthInfo.value);
                for (int i = 0; (i < this.AuthInfo.value.Length); i++
                ) {
                    byte elem_0 = this.AuthInfo.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LastUpdateTime);
            if ((null != this.AuthInfo)) {
                this.AuthInfo.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.AuthInfo.value.Length); i++
                ) {
                    byte elem_0 = this.AuthInfo.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.AuthInfo.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.IncomingAuthInfos);
            encoder.WritePointer(this.IncomingAuthenticationInformation);
            encoder.WritePointer(this.IncomingPreviousAuthenticationInformation);
            encoder.WriteValue(this.OutgoingAuthInfos);
            encoder.WritePointer(this.OutgoingAuthenticationInformation);
            encoder.WritePointer(this.OutgoingPreviousAuthenticationInformation);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.IncomingAuthInfos = decoder.ReadUInt32();
            this.IncomingAuthenticationInformation = decoder.ReadPointer<LSAPR_AUTH_INFORMATION>();
            this.IncomingPreviousAuthenticationInformation = decoder.ReadPointer<LSAPR_AUTH_INFORMATION>();
            this.OutgoingAuthInfos = decoder.ReadUInt32();
            this.OutgoingAuthenticationInformation = decoder.ReadPointer<LSAPR_AUTH_INFORMATION>();
            this.OutgoingPreviousAuthenticationInformation = decoder.ReadPointer<LSAPR_AUTH_INFORMATION>();
        }
        public uint IncomingAuthInfos;
        public RpcPointer<LSAPR_AUTH_INFORMATION> IncomingAuthenticationInformation;
        public RpcPointer<LSAPR_AUTH_INFORMATION> IncomingPreviousAuthenticationInformation;
        public uint OutgoingAuthInfos;
        public RpcPointer<LSAPR_AUTH_INFORMATION> OutgoingAuthenticationInformation;
        public RpcPointer<LSAPR_AUTH_INFORMATION> OutgoingPreviousAuthenticationInformation;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.IncomingAuthenticationInformation)) {
                encoder.WriteFixedStruct(this.IncomingAuthenticationInformation.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(this.IncomingAuthenticationInformation.value);
            }
            if ((null != this.IncomingPreviousAuthenticationInformation)) {
                encoder.WriteFixedStruct(this.IncomingPreviousAuthenticationInformation.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(this.IncomingPreviousAuthenticationInformation.value);
            }
            if ((null != this.OutgoingAuthenticationInformation)) {
                encoder.WriteFixedStruct(this.OutgoingAuthenticationInformation.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(this.OutgoingAuthenticationInformation.value);
            }
            if ((null != this.OutgoingPreviousAuthenticationInformation)) {
                encoder.WriteFixedStruct(this.OutgoingPreviousAuthenticationInformation.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(this.OutgoingPreviousAuthenticationInformation.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.IncomingAuthenticationInformation)) {
                this.IncomingAuthenticationInformation.value = decoder.ReadFixedStruct<LSAPR_AUTH_INFORMATION>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<LSAPR_AUTH_INFORMATION>(ref this.IncomingAuthenticationInformation.value);
            }
            if ((null != this.IncomingPreviousAuthenticationInformation)) {
                this.IncomingPreviousAuthenticationInformation.value = decoder.ReadFixedStruct<LSAPR_AUTH_INFORMATION>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<LSAPR_AUTH_INFORMATION>(ref this.IncomingPreviousAuthenticationInformation.value);
            }
            if ((null != this.OutgoingAuthenticationInformation)) {
                this.OutgoingAuthenticationInformation.value = decoder.ReadFixedStruct<LSAPR_AUTH_INFORMATION>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<LSAPR_AUTH_INFORMATION>(ref this.OutgoingAuthenticationInformation.value);
            }
            if ((null != this.OutgoingPreviousAuthenticationInformation)) {
                this.OutgoingPreviousAuthenticationInformation.value = decoder.ReadFixedStruct<LSAPR_AUTH_INFORMATION>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<LSAPR_AUTH_INFORMATION>(ref this.OutgoingPreviousAuthenticationInformation.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Information, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.PosixOffset, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.AuthInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Information = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.PosixOffset = decoder.ReadFixedStruct<TRUSTED_POSIX_OFFSET_INFO>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.AuthInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public LSAPR_TRUSTED_DOMAIN_INFORMATION_EX Information;
        public TRUSTED_POSIX_OFFSET_INFO PosixOffset;
        public LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION AuthInformation;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Information);
            encoder.WriteStructDeferral(this.PosixOffset);
            encoder.WriteStructDeferral(this.AuthInformation);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(ref this.Information);
            decoder.ReadStructDeferral<TRUSTED_POSIX_OFFSET_INFO>(ref this.PosixOffset);
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(ref this.AuthInformation);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_DOMAIN_AUTH_BLOB : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.AuthSize);
            encoder.WritePointer(this.AuthBlob);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.AuthSize = decoder.ReadUInt32();
            this.AuthBlob = decoder.ReadPointer<byte[]>();
        }
        public uint AuthSize;
        public RpcPointer<byte[]> AuthBlob;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.AuthBlob)) {
                encoder.WriteArrayHeader(this.AuthBlob.value);
                for (int i = 0; (i < this.AuthBlob.value.Length); i++
                ) {
                    byte elem_0 = this.AuthBlob.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.AuthBlob)) {
                this.AuthBlob.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.AuthBlob.value.Length); i++
                ) {
                    byte elem_0 = this.AuthBlob.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.AuthBlob.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.AuthBlob, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.AuthBlob = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public LSAPR_TRUSTED_DOMAIN_AUTH_BLOB AuthBlob;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.AuthBlob);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_BLOB>(ref this.AuthBlob);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION_INTERNAL : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Information, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.PosixOffset, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.AuthInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Information = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.PosixOffset = decoder.ReadFixedStruct<TRUSTED_POSIX_OFFSET_INFO>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.AuthInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public LSAPR_TRUSTED_DOMAIN_INFORMATION_EX Information;
        public TRUSTED_POSIX_OFFSET_INFO PosixOffset;
        public LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL AuthInformation;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Information);
            encoder.WriteStructDeferral(this.PosixOffset);
            encoder.WriteStructDeferral(this.AuthInformation);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(ref this.Information);
            decoder.ReadStructDeferral<TRUSTED_POSIX_OFFSET_INFO>(ref this.PosixOffset);
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL>(ref this.AuthInformation);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.FlatName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WritePointer(this.Sid);
            encoder.WriteValue(this.TrustDirection);
            encoder.WriteValue(this.TrustType);
            encoder.WriteValue(this.TrustAttributes);
            encoder.WriteValue(this.ForestTrustLength);
            encoder.WritePointer(this.ForestTrustInfo);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.FlatName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.Sid = decoder.ReadPointer<ms_dtyp.RPC_SID>();
            this.TrustDirection = decoder.ReadUInt32();
            this.TrustType = decoder.ReadUInt32();
            this.TrustAttributes = decoder.ReadUInt32();
            this.ForestTrustLength = decoder.ReadUInt32();
            this.ForestTrustInfo = decoder.ReadPointer<byte[]>();
        }
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public ms_dtyp.RPC_UNICODE_STRING FlatName;
        public RpcPointer<ms_dtyp.RPC_SID> Sid;
        public uint TrustDirection;
        public uint TrustType;
        public uint TrustAttributes;
        public uint ForestTrustLength;
        public RpcPointer<byte[]> ForestTrustInfo;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
            encoder.WriteStructDeferral(this.FlatName);
            if ((null != this.Sid)) {
                encoder.WriteConformantStruct(this.Sid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.Sid.value);
            }
            if ((null != this.ForestTrustInfo)) {
                encoder.WriteArrayHeader(this.ForestTrustInfo.value);
                for (int i = 0; (i < this.ForestTrustInfo.value.Length); i++
                ) {
                    byte elem_0 = this.ForestTrustInfo.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FlatName);
            if ((null != this.Sid)) {
                this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
            }
            if ((null != this.ForestTrustInfo)) {
                this.ForestTrustInfo.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.ForestTrustInfo.value.Length); i++
                ) {
                    byte elem_0 = this.ForestTrustInfo.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.ForestTrustInfo.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION2 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Information, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.PosixOffset, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.AuthInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Information = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.PosixOffset = decoder.ReadFixedStruct<TRUSTED_POSIX_OFFSET_INFO>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.AuthInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2 Information;
        public TRUSTED_POSIX_OFFSET_INFO PosixOffset;
        public LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION AuthInformation;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Information);
            encoder.WriteStructDeferral(this.PosixOffset);
            encoder.WriteStructDeferral(this.AuthInformation);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2>(ref this.Information);
            decoder.ReadStructDeferral<TRUSTED_POSIX_OFFSET_INFO>(ref this.PosixOffset);
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(ref this.AuthInformation);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct TRUSTED_DOMAIN_SUPPORTED_ENCRYPTION_TYPES : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.SupportedEncryptionTypes);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.SupportedEncryptionTypes = decoder.ReadUInt32();
        }
        public uint SupportedEncryptionTypes;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_DOMAIN_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public TRUSTED_INFORMATION_CLASS unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.unionSwitch)));
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteFixedStruct(this.TrustedDomainNameInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteFixedStruct(this.TrustedControllersInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteFixedStruct(this.TrustedPosixOffsetInfo, Titanis.DceRpc.NdrAlignment._4Byte);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            encoder.WriteFixedStruct(this.TrustedPasswordInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                encoder.WriteFixedStruct(this.TrustedDomainInfoBasic, Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    encoder.WriteFixedStruct(this.TrustedDomainInfoEx, Titanis.DceRpc.NdrAlignment.NativePtr);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        encoder.WriteFixedStruct(this.TrustedAuthInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            encoder.WriteFixedStruct(this.TrustedFullInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                encoder.WriteFixedStruct(this.TrustedAuthInfoInternal, Titanis.DceRpc.NdrAlignment.NativePtr);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 10)) {
                                                    encoder.WriteFixedStruct(this.TrustedFullInfoInternal, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 11)) {
                                                        encoder.WriteFixedStruct(this.TrustedDomainInfoEx2, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 12)) {
                                                            encoder.WriteFixedStruct(this.TrustedFullInfo2, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 13)) {
                                                                encoder.WriteFixedStruct(this.TrustedDomainSETs, Titanis.DceRpc.NdrAlignment._4Byte);
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
            this.unionSwitch = ((TRUSTED_INFORMATION_CLASS)(decoder.ReadInt16()));
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.TrustedDomainNameInfo = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_NAME_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    this.TrustedControllersInfo = decoder.ReadFixedStruct<LSAPR_TRUSTED_CONTROLLERS_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        this.TrustedPosixOffsetInfo = decoder.ReadFixedStruct<TRUSTED_POSIX_OFFSET_INFO>(Titanis.DceRpc.NdrAlignment._4Byte);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            this.TrustedPasswordInfo = decoder.ReadFixedStruct<LSAPR_TRUSTED_PASSWORD_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                this.TrustedDomainInfoBasic = decoder.ReadFixedStruct<LSAPR_TRUST_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    this.TrustedDomainInfoEx = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        this.TrustedAuthInfo = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            this.TrustedFullInfo = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                this.TrustedAuthInfoInternal = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 10)) {
                                                    this.TrustedFullInfoInternal = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION_INTERNAL>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 11)) {
                                                        this.TrustedDomainInfoEx2 = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 12)) {
                                                            this.TrustedFullInfo2 = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION2>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 13)) {
                                                                this.TrustedDomainSETs = decoder.ReadFixedStruct<TRUSTED_DOMAIN_SUPPORTED_ENCRYPTION_TYPES>(Titanis.DceRpc.NdrAlignment._4Byte);
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
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteStructDeferral(this.TrustedDomainNameInfo);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteStructDeferral(this.TrustedControllersInfo);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteStructDeferral(this.TrustedPosixOffsetInfo);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            encoder.WriteStructDeferral(this.TrustedPasswordInfo);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                encoder.WriteStructDeferral(this.TrustedDomainInfoBasic);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    encoder.WriteStructDeferral(this.TrustedDomainInfoEx);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        encoder.WriteStructDeferral(this.TrustedAuthInfo);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            encoder.WriteStructDeferral(this.TrustedFullInfo);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                encoder.WriteStructDeferral(this.TrustedAuthInfoInternal);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 10)) {
                                                    encoder.WriteStructDeferral(this.TrustedFullInfoInternal);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 11)) {
                                                        encoder.WriteStructDeferral(this.TrustedDomainInfoEx2);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 12)) {
                                                            encoder.WriteStructDeferral(this.TrustedFullInfo2);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 13)) {
                                                                encoder.WriteStructDeferral(this.TrustedDomainSETs);
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
            if ((((int)(this.unionSwitch)) == 1)) {
                decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_NAME_INFO>(ref this.TrustedDomainNameInfo);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    decoder.ReadStructDeferral<LSAPR_TRUSTED_CONTROLLERS_INFO>(ref this.TrustedControllersInfo);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        decoder.ReadStructDeferral<TRUSTED_POSIX_OFFSET_INFO>(ref this.TrustedPosixOffsetInfo);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            decoder.ReadStructDeferral<LSAPR_TRUSTED_PASSWORD_INFO>(ref this.TrustedPasswordInfo);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                decoder.ReadStructDeferral<LSAPR_TRUST_INFORMATION>(ref this.TrustedDomainInfoBasic);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(ref this.TrustedDomainInfoEx);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(ref this.TrustedAuthInfo);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION>(ref this.TrustedFullInfo);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL>(ref this.TrustedAuthInfoInternal);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 10)) {
                                                    decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION_INTERNAL>(ref this.TrustedFullInfoInternal);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 11)) {
                                                        decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2>(ref this.TrustedDomainInfoEx2);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 12)) {
                                                            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION2>(ref this.TrustedFullInfo2);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 13)) {
                                                                decoder.ReadStructDeferral<TRUSTED_DOMAIN_SUPPORTED_ENCRYPTION_TYPES>(ref this.TrustedDomainSETs);
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
        public LSAPR_TRUSTED_DOMAIN_NAME_INFO TrustedDomainNameInfo;
        public LSAPR_TRUSTED_CONTROLLERS_INFO TrustedControllersInfo;
        public TRUSTED_POSIX_OFFSET_INFO TrustedPosixOffsetInfo;
        public LSAPR_TRUSTED_PASSWORD_INFO TrustedPasswordInfo;
        public LSAPR_TRUST_INFORMATION TrustedDomainInfoBasic;
        public LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInfoEx;
        public LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION TrustedAuthInfo;
        public LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION TrustedFullInfo;
        public LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL TrustedAuthInfoInternal;
        public LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION_INTERNAL TrustedFullInfoInternal;
        public LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2 TrustedDomainInfoEx2;
        public LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION2 TrustedFullInfo2;
        public TRUSTED_DOMAIN_SUPPORTED_ENCRYPTION_TYPES TrustedDomainSETs;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_USER_RIGHT_SET : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Entries);
            encoder.WritePointer(this.UserRights);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Entries = decoder.ReadUInt32();
            this.UserRights = decoder.ReadPointer<ms_dtyp.RPC_UNICODE_STRING[]>();
        }
        public uint Entries;
        public RpcPointer<ms_dtyp.RPC_UNICODE_STRING[]> UserRights;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.UserRights)) {
                encoder.WriteArrayHeader(this.UserRights.value);
                for (int i = 0; (i < this.UserRights.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.UserRights.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.UserRights.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.UserRights.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.UserRights)) {
                this.UserRights.value = decoder.ReadArrayHeader<ms_dtyp.RPC_UNICODE_STRING>();
                for (int i = 0; (i < this.UserRights.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.UserRights.value[i];
                    elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.UserRights.value[i] = elem_0;
                }
                for (int i = 0; (i < this.UserRights.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.UserRights.value[i];
                    decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
                    this.UserRights.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRUSTED_ENUM_BUFFER_EX : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.EnumerationBuffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.EnumerationBuffer = decoder.ReadPointer<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX[]>();
        }
        public uint EntriesRead;
        public RpcPointer<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX[]> EnumerationBuffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.EnumerationBuffer)) {
                encoder.WriteArrayHeader(this.EnumerationBuffer.value);
                for (int i = 0; (i < this.EnumerationBuffer.value.Length); i++
                ) {
                    LSAPR_TRUSTED_DOMAIN_INFORMATION_EX elem_0 = this.EnumerationBuffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.EnumerationBuffer.value.Length); i++
                ) {
                    LSAPR_TRUSTED_DOMAIN_INFORMATION_EX elem_0 = this.EnumerationBuffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.EnumerationBuffer)) {
                this.EnumerationBuffer.value = decoder.ReadArrayHeader<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>();
                for (int i = 0; (i < this.EnumerationBuffer.value.Length); i++
                ) {
                    LSAPR_TRUSTED_DOMAIN_INFORMATION_EX elem_0 = this.EnumerationBuffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.EnumerationBuffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.EnumerationBuffer.value.Length); i++
                ) {
                    LSAPR_TRUSTED_DOMAIN_INFORMATION_EX elem_0 = this.EnumerationBuffer.value[i];
                    decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(ref elem_0);
                    this.EnumerationBuffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_REFERENCED_DOMAIN_LIST : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Entries);
            encoder.WritePointer(this.Domains);
            encoder.WriteValue(this.MaxEntries);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Entries = decoder.ReadUInt32();
            this.Domains = decoder.ReadPointer<LSAPR_TRUST_INFORMATION[]>();
            this.MaxEntries = decoder.ReadUInt32();
        }
        public uint Entries;
        public RpcPointer<LSAPR_TRUST_INFORMATION[]> Domains;
        public uint MaxEntries;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Domains)) {
                encoder.WriteArrayHeader(this.Domains.value);
                for (int i = 0; (i < this.Domains.value.Length); i++
                ) {
                    LSAPR_TRUST_INFORMATION elem_0 = this.Domains.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Domains.value.Length); i++
                ) {
                    LSAPR_TRUST_INFORMATION elem_0 = this.Domains.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Domains)) {
                this.Domains.value = decoder.ReadArrayHeader<LSAPR_TRUST_INFORMATION>();
                for (int i = 0; (i < this.Domains.value.Length); i++
                ) {
                    LSAPR_TRUST_INFORMATION elem_0 = this.Domains.value[i];
                    elem_0 = decoder.ReadFixedStruct<LSAPR_TRUST_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Domains.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Domains.value.Length); i++
                ) {
                    LSAPR_TRUST_INFORMATION elem_0 = this.Domains.value[i];
                    decoder.ReadStructDeferral<LSAPR_TRUST_INFORMATION>(ref elem_0);
                    this.Domains.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum SID_NAME_USE : int {
        SidTypeUser = 1,
        SidTypeGroup = 2,
        SidTypeDomain = 3,
        SidTypeAlias = 4,
        SidTypeWellKnownGroup = 5,
        SidTypeDeletedAccount = 6,
        SidTypeInvalid = 7,
        SidTypeUnknown = 8,
        SidTypeComputer = 9,
        SidTypeLabel = 10,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSA_TRANSLATED_SID : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.Use)));
            encoder.WriteValue(this.RelativeId);
            encoder.WriteValue(this.DomainIndex);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Use = ((SID_NAME_USE)(decoder.ReadInt16()));
            this.RelativeId = decoder.ReadUInt32();
            this.DomainIndex = decoder.ReadInt32();
        }
        public SID_NAME_USE Use;
        public uint RelativeId;
        public int DomainIndex;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRANSLATED_SIDS : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Entries);
            encoder.WritePointer(this.Sids);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Entries = decoder.ReadUInt32();
            this.Sids = decoder.ReadPointer<LSA_TRANSLATED_SID[]>();
        }
        public uint Entries;
        public RpcPointer<LSA_TRANSLATED_SID[]> Sids;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Sids)) {
                encoder.WriteArrayHeader(this.Sids.value);
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    LSA_TRANSLATED_SID elem_0 = this.Sids.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
                }
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    LSA_TRANSLATED_SID elem_0 = this.Sids.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Sids)) {
                this.Sids.value = decoder.ReadArrayHeader<LSA_TRANSLATED_SID>();
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    LSA_TRANSLATED_SID elem_0 = this.Sids.value[i];
                    elem_0 = decoder.ReadFixedStruct<LSA_TRANSLATED_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                    this.Sids.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    LSA_TRANSLATED_SID elem_0 = this.Sids.value[i];
                    decoder.ReadStructDeferral<LSA_TRANSLATED_SID>(ref elem_0);
                    this.Sids.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum LSAP_LOOKUP_LEVEL : int {
        LsapLookupWksta = 1,
        LsapLookupPDC = 2,
        LsapLookupTDL = 3,
        LsapLookupGC = 4,
        LsapLookupXForestReferral = 5,
        LsapLookupXForestResolve = 6,
        LsapLookupRODCReferralToFullDC = 7,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_SID_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.Sid);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Sid = decoder.ReadPointer<ms_dtyp.RPC_SID>();
        }
        public RpcPointer<ms_dtyp.RPC_SID> Sid;
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_SID_ENUM_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Entries);
            encoder.WritePointer(this.SidInfo);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Entries = decoder.ReadUInt32();
            this.SidInfo = decoder.ReadPointer<LSAPR_SID_INFORMATION[]>();
        }
        public uint Entries;
        public RpcPointer<LSAPR_SID_INFORMATION[]> SidInfo;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.SidInfo)) {
                encoder.WriteArrayHeader(this.SidInfo.value);
                for (int i = 0; (i < this.SidInfo.value.Length); i++
                ) {
                    LSAPR_SID_INFORMATION elem_0 = this.SidInfo.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.SidInfo.value.Length); i++
                ) {
                    LSAPR_SID_INFORMATION elem_0 = this.SidInfo.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.SidInfo)) {
                this.SidInfo.value = decoder.ReadArrayHeader<LSAPR_SID_INFORMATION>();
                for (int i = 0; (i < this.SidInfo.value.Length); i++
                ) {
                    LSAPR_SID_INFORMATION elem_0 = this.SidInfo.value[i];
                    elem_0 = decoder.ReadFixedStruct<LSAPR_SID_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.SidInfo.value[i] = elem_0;
                }
                for (int i = 0; (i < this.SidInfo.value.Length); i++
                ) {
                    LSAPR_SID_INFORMATION elem_0 = this.SidInfo.value[i];
                    decoder.ReadStructDeferral<LSAPR_SID_INFORMATION>(ref elem_0);
                    this.SidInfo.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRANSLATED_NAME : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.Use)));
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.DomainIndex);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Use = ((SID_NAME_USE)(decoder.ReadInt16()));
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.DomainIndex = decoder.ReadInt32();
        }
        public SID_NAME_USE Use;
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public int DomainIndex;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRANSLATED_NAMES : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Entries);
            encoder.WritePointer(this.Names);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Entries = decoder.ReadUInt32();
            this.Names = decoder.ReadPointer<LSAPR_TRANSLATED_NAME[]>();
        }
        public uint Entries;
        public RpcPointer<LSAPR_TRANSLATED_NAME[]> Names;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Names)) {
                encoder.WriteArrayHeader(this.Names.value);
                for (int i = 0; (i < this.Names.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_NAME elem_0 = this.Names.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Names.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_NAME elem_0 = this.Names.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Names)) {
                this.Names.value = decoder.ReadArrayHeader<LSAPR_TRANSLATED_NAME>();
                for (int i = 0; (i < this.Names.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_NAME elem_0 = this.Names.value[i];
                    elem_0 = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAME>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Names.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Names.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_NAME elem_0 = this.Names.value[i];
                    decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAME>(ref elem_0);
                    this.Names.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRANSLATED_NAME_EX : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.Use)));
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.DomainIndex);
            encoder.WriteValue(this.Flags);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Use = ((SID_NAME_USE)(decoder.ReadInt16()));
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.DomainIndex = decoder.ReadInt32();
            this.Flags = decoder.ReadUInt32();
        }
        public SID_NAME_USE Use;
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public int DomainIndex;
        public uint Flags;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRANSLATED_NAMES_EX : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Entries);
            encoder.WritePointer(this.Names);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Entries = decoder.ReadUInt32();
            this.Names = decoder.ReadPointer<LSAPR_TRANSLATED_NAME_EX[]>();
        }
        public uint Entries;
        public RpcPointer<LSAPR_TRANSLATED_NAME_EX[]> Names;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Names)) {
                encoder.WriteArrayHeader(this.Names.value);
                for (int i = 0; (i < this.Names.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_NAME_EX elem_0 = this.Names.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Names.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_NAME_EX elem_0 = this.Names.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Names)) {
                this.Names.value = decoder.ReadArrayHeader<LSAPR_TRANSLATED_NAME_EX>();
                for (int i = 0; (i < this.Names.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_NAME_EX elem_0 = this.Names.value[i];
                    elem_0 = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAME_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Names.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Names.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_NAME_EX elem_0 = this.Names.value[i];
                    decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAME_EX>(ref elem_0);
                    this.Names.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRANSLATED_SID_EX : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.Use)));
            encoder.WriteValue(this.RelativeId);
            encoder.WriteValue(this.DomainIndex);
            encoder.WriteValue(this.Flags);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Use = ((SID_NAME_USE)(decoder.ReadInt16()));
            this.RelativeId = decoder.ReadUInt32();
            this.DomainIndex = decoder.ReadInt32();
            this.Flags = decoder.ReadUInt32();
        }
        public SID_NAME_USE Use;
        public uint RelativeId;
        public int DomainIndex;
        public uint Flags;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRANSLATED_SIDS_EX : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Entries);
            encoder.WritePointer(this.Sids);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Entries = decoder.ReadUInt32();
            this.Sids = decoder.ReadPointer<LSAPR_TRANSLATED_SID_EX[]>();
        }
        public uint Entries;
        public RpcPointer<LSAPR_TRANSLATED_SID_EX[]> Sids;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Sids)) {
                encoder.WriteArrayHeader(this.Sids.value);
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_SID_EX elem_0 = this.Sids.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
                }
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_SID_EX elem_0 = this.Sids.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Sids)) {
                this.Sids.value = decoder.ReadArrayHeader<LSAPR_TRANSLATED_SID_EX>();
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_SID_EX elem_0 = this.Sids.value[i];
                    elem_0 = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SID_EX>(Titanis.DceRpc.NdrAlignment._4Byte);
                    this.Sids.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_SID_EX elem_0 = this.Sids.value[i];
                    decoder.ReadStructDeferral<LSAPR_TRANSLATED_SID_EX>(ref elem_0);
                    this.Sids.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRANSLATED_SID_EX2 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.Use)));
            encoder.WritePointer(this.Sid);
            encoder.WriteValue(this.DomainIndex);
            encoder.WriteValue(this.Flags);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Use = ((SID_NAME_USE)(decoder.ReadInt16()));
            this.Sid = decoder.ReadPointer<ms_dtyp.RPC_SID>();
            this.DomainIndex = decoder.ReadInt32();
            this.Flags = decoder.ReadUInt32();
        }
        public SID_NAME_USE Use;
        public RpcPointer<ms_dtyp.RPC_SID> Sid;
        public int DomainIndex;
        public uint Flags;
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct LSAPR_TRANSLATED_SIDS_EX2 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Entries);
            encoder.WritePointer(this.Sids);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Entries = decoder.ReadUInt32();
            this.Sids = decoder.ReadPointer<LSAPR_TRANSLATED_SID_EX2[]>();
        }
        public uint Entries;
        public RpcPointer<LSAPR_TRANSLATED_SID_EX2[]> Sids;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Sids)) {
                encoder.WriteArrayHeader(this.Sids.value);
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_SID_EX2 elem_0 = this.Sids.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_SID_EX2 elem_0 = this.Sids.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Sids)) {
                this.Sids.value = decoder.ReadArrayHeader<LSAPR_TRANSLATED_SID_EX2>();
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_SID_EX2 elem_0 = this.Sids.value[i];
                    elem_0 = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SID_EX2>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Sids.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    LSAPR_TRANSLATED_SID_EX2 elem_0 = this.Sids.value[i];
                    decoder.ReadStructDeferral<LSAPR_TRANSLATED_SID_EX2>(ref elem_0);
                    this.Sids.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [System.Runtime.InteropServices.GuidAttribute("12345778-1234-abcd-ef00-0123456789ab")]
    [Titanis.DceRpc.RpcVersionAttribute(0, 0)]
    public interface lsarpc {
        Task<int> LsarClose(RpcPointer<Titanis.DceRpc.RpcContextHandle> ObjectHandle, System.Threading.CancellationToken cancellationToken);
        Task Opnum1NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> LsarEnumeratePrivileges(Titanis.DceRpc.RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_PRIVILEGE_ENUM_BUFFER> EnumerationBuffer, uint PreferedMaximumLength, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarQuerySecurityObject(Titanis.DceRpc.RpcContextHandle ObjectHandle, uint SecurityInformation, RpcPointer<RpcPointer<LSAPR_SR_SECURITY_DESCRIPTOR>> SecurityDescriptor, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarSetSecurityObject(Titanis.DceRpc.RpcContextHandle ObjectHandle, uint SecurityInformation, LSAPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor, System.Threading.CancellationToken cancellationToken);
        Task Opnum5NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> LsarOpenPolicy(RpcPointer<char> SystemName, LSAPR_OBJECT_ATTRIBUTES ObjectAttributes, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> PolicyHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarQueryInformationPolicy(Titanis.DceRpc.RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>> PolicyInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarSetInformationPolicy(Titanis.DceRpc.RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, LSAPR_POLICY_INFORMATION PolicyInformation, System.Threading.CancellationToken cancellationToken);
        Task Opnum9NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> LsarCreateAccount(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> AccountHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarEnumerateAccounts(Titanis.DceRpc.RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> EnumerationBuffer, uint PreferedMaximumLength, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarCreateTrustedDomain(Titanis.DceRpc.RpcContextHandle PolicyHandle, LSAPR_TRUST_INFORMATION TrustedDomainInformation, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarEnumerateTrustedDomains(Titanis.DceRpc.RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_TRUSTED_ENUM_BUFFER> EnumerationBuffer, uint PreferedMaximumLength, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarLookupNames(Titanis.DceRpc.RpcContextHandle PolicyHandle, uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarLookupSids(Titanis.DceRpc.RpcContextHandle PolicyHandle, LSAPR_SID_ENUM_BUFFER SidEnumBuffer, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_NAMES> TranslatedNames, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarCreateSecret(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING SecretName, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> SecretHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarOpenAccount(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> AccountHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarEnumeratePrivilegesAccount(Titanis.DceRpc.RpcContextHandle AccountHandle, RpcPointer<RpcPointer<LSAPR_PRIVILEGE_SET>> Privileges, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarAddPrivilegesToAccount(Titanis.DceRpc.RpcContextHandle AccountHandle, LSAPR_PRIVILEGE_SET Privileges, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarRemovePrivilegesFromAccount(Titanis.DceRpc.RpcContextHandle AccountHandle, byte AllPrivileges, RpcPointer<LSAPR_PRIVILEGE_SET> Privileges, System.Threading.CancellationToken cancellationToken);
        Task Opnum21NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum22NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> LsarGetSystemAccessAccount(Titanis.DceRpc.RpcContextHandle AccountHandle, RpcPointer<uint> SystemAccess, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarSetSystemAccessAccount(Titanis.DceRpc.RpcContextHandle AccountHandle, uint SystemAccess, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarOpenTrustedDomain(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarQueryInfoTrustedDomain(Titanis.DceRpc.RpcContextHandle TrustedDomainHandle, TRUSTED_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarSetInformationTrustedDomain(Titanis.DceRpc.RpcContextHandle TrustedDomainHandle, TRUSTED_INFORMATION_CLASS InformationClass, LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarOpenSecret(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING SecretName, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> SecretHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarSetSecret(Titanis.DceRpc.RpcContextHandle SecretHandle, RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedCurrentValue, RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedOldValue, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarQuerySecret(Titanis.DceRpc.RpcContextHandle SecretHandle, RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedCurrentValue, RpcPointer<ms_dtyp.LARGE_INTEGER> CurrentValueSetTime, RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedOldValue, RpcPointer<ms_dtyp.LARGE_INTEGER> OldValueSetTime, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarLookupPrivilegeValue(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING Name, RpcPointer<ms_dtyp.LUID> Value, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarLookupPrivilegeName(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.LUID Value, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> Name, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarLookupPrivilegeDisplayName(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING Name, short ClientLanguage, short ClientSystemDefaultLanguage, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> DisplayName, RpcPointer<ushort> LanguageReturned, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarDeleteObject(RpcPointer<Titanis.DceRpc.RpcContextHandle> ObjectHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarEnumerateAccountsWithUserRight(Titanis.DceRpc.RpcContextHandle PolicyHandle, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> UserRight, RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> EnumerationBuffer, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarEnumerateAccountRights(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, RpcPointer<LSAPR_USER_RIGHT_SET> UserRights, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarAddAccountRights(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, LSAPR_USER_RIGHT_SET UserRights, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarRemoveAccountRights(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, byte AllRights, LSAPR_USER_RIGHT_SET UserRights, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarQueryTrustedDomainInfo(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, TRUSTED_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarSetTrustedDomainInfo(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, TRUSTED_INFORMATION_CLASS InformationClass, LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarDeleteTrustedDomain(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarStorePrivateData(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING KeyName, RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedData, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarRetrievePrivateData(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING KeyName, RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedData, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarOpenPolicy2(string SystemName, LSAPR_OBJECT_ATTRIBUTES ObjectAttributes, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> PolicyHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarGetUserName(string SystemName, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> UserName, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> DomainName, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarQueryInformationPolicy2(Titanis.DceRpc.RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>> PolicyInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarSetInformationPolicy2(Titanis.DceRpc.RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, LSAPR_POLICY_INFORMATION PolicyInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarQueryTrustedDomainInfoByName(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, TRUSTED_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarSetTrustedDomainInfoByName(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, TRUSTED_INFORMATION_CLASS InformationClass, LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarEnumerateTrustedDomainsEx(Titanis.DceRpc.RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_TRUSTED_ENUM_BUFFER_EX> EnumerationBuffer, uint PreferedMaximumLength, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarCreateTrustedDomainEx(Titanis.DceRpc.RpcContextHandle PolicyHandle, LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInformation, LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION AuthenticationInformation, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle, System.Threading.CancellationToken cancellationToken);
        Task Opnum52NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> LsarQueryDomainInformationPolicy(Titanis.DceRpc.RpcContextHandle PolicyHandle, POLICY_DOMAIN_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION>> PolicyDomainInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarSetDomainInformationPolicy(Titanis.DceRpc.RpcContextHandle PolicyHandle, POLICY_DOMAIN_INFORMATION_CLASS InformationClass, RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION> PolicyDomainInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarOpenTrustedDomainByName(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle, System.Threading.CancellationToken cancellationToken);
        Task Opnum56NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> LsarLookupSids2(Titanis.DceRpc.RpcContextHandle PolicyHandle, LSAPR_SID_ENUM_BUFFER SidEnumBuffer, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_NAMES_EX> TranslatedNames, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarLookupNames2(Titanis.DceRpc.RpcContextHandle PolicyHandle, uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS_EX> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarCreateTrustedDomainEx2(Titanis.DceRpc.RpcContextHandle PolicyHandle, LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInformation, LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL AuthenticationInformation, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle, System.Threading.CancellationToken cancellationToken);
        Task Opnum60NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum61NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum62NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum63NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum64NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum65NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum66NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum67NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> LsarLookupNames3(Titanis.DceRpc.RpcContextHandle PolicyHandle, uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS_EX2> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, System.Threading.CancellationToken cancellationToken);
        Task Opnum69NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum70NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum71NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum72NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> LsarQueryForestTrustInformation(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, LSA_FOREST_TRUST_RECORD_TYPE HighestRecordType, RpcPointer<RpcPointer<LSA_FOREST_TRUST_INFORMATION>> ForestTrustInfo, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarSetForestTrustInformation(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, LSA_FOREST_TRUST_RECORD_TYPE HighestRecordType, LSA_FOREST_TRUST_INFORMATION ForestTrustInfo, byte CheckOnly, RpcPointer<RpcPointer<LSA_FOREST_TRUST_COLLISION_INFORMATION>> CollisionInfo, System.Threading.CancellationToken cancellationToken);
        Task Opnum75NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> LsarLookupSids3(LSAPR_SID_ENUM_BUFFER SidEnumBuffer, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_NAMES_EX> TranslatedNames, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, System.Threading.CancellationToken cancellationToken);
        Task<int> LsarLookupNames4(uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS_EX2> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [Titanis.DceRpc.IidAttribute("12345778-1234-abcd-ef00-0123456789ab")]
    public class lsarpcClientProxy : Titanis.DceRpc.Client.RpcClientProxy, lsarpc, Titanis.DceRpc.IRpcClientProxy {
        private static System.Guid _interfaceUuid = new System.Guid("12345778-1234-abcd-ef00-0123456789ab");
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
        public virtual async Task<int> LsarClose(RpcPointer<Titanis.DceRpc.RpcContextHandle> ObjectHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(ObjectHandle.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ObjectHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum1NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> LsarEnumeratePrivileges(Titanis.DceRpc.RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_PRIVILEGE_ENUM_BUFFER> EnumerationBuffer, uint PreferedMaximumLength, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(EnumerationContext.value);
            encoder.WriteValue(PreferedMaximumLength);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EnumerationContext.value = decoder.ReadUInt32();
            EnumerationBuffer.value = decoder.ReadFixedStruct<LSAPR_PRIVILEGE_ENUM_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_PRIVILEGE_ENUM_BUFFER>(ref EnumerationBuffer.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarQuerySecurityObject(Titanis.DceRpc.RpcContextHandle ObjectHandle, uint SecurityInformation, RpcPointer<RpcPointer<LSAPR_SR_SECURITY_DESCRIPTOR>> SecurityDescriptor, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(ObjectHandle);
            encoder.WriteValue(SecurityInformation);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            SecurityDescriptor.value = decoder.ReadOutPointer<LSAPR_SR_SECURITY_DESCRIPTOR>(SecurityDescriptor.value);
            if ((null != SecurityDescriptor.value)) {
                SecurityDescriptor.value.value = decoder.ReadFixedStruct<LSAPR_SR_SECURITY_DESCRIPTOR>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_SR_SECURITY_DESCRIPTOR>(ref SecurityDescriptor.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarSetSecurityObject(Titanis.DceRpc.RpcContextHandle ObjectHandle, uint SecurityInformation, LSAPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(ObjectHandle);
            encoder.WriteValue(SecurityInformation);
            encoder.WriteFixedStruct(SecurityDescriptor, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(SecurityDescriptor);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum5NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> LsarOpenPolicy(RpcPointer<char> SystemName, LSAPR_OBJECT_ATTRIBUTES ObjectAttributes, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> PolicyHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(SystemName);
            if ((null != SystemName)) {
                encoder.WriteValue(SystemName.value);
            }
            encoder.WriteFixedStruct(ObjectAttributes, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ObjectAttributes);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            PolicyHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarQueryInformationPolicy(Titanis.DceRpc.RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>> PolicyInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(((short)(InformationClass)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            PolicyInformation.value = decoder.ReadOutPointer<LSAPR_POLICY_INFORMATION>(PolicyInformation.value);
            if ((null != PolicyInformation.value)) {
                PolicyInformation.value.value = decoder.ReadUnion<LSAPR_POLICY_INFORMATION>();
                decoder.ReadStructDeferral<LSAPR_POLICY_INFORMATION>(ref PolicyInformation.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarSetInformationPolicy(Titanis.DceRpc.RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, LSAPR_POLICY_INFORMATION PolicyInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(((short)(InformationClass)));
            encoder.WriteUnion(PolicyInformation);
            encoder.WriteStructDeferral(PolicyInformation);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum9NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> LsarCreateAccount(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> AccountHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteConformantStruct(AccountSid, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(AccountSid);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            AccountHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarEnumerateAccounts(Titanis.DceRpc.RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> EnumerationBuffer, uint PreferedMaximumLength, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(EnumerationContext.value);
            encoder.WriteValue(PreferedMaximumLength);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EnumerationContext.value = decoder.ReadUInt32();
            EnumerationBuffer.value = decoder.ReadFixedStruct<LSAPR_ACCOUNT_ENUM_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_ACCOUNT_ENUM_BUFFER>(ref EnumerationBuffer.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarCreateTrustedDomain(Titanis.DceRpc.RpcContextHandle PolicyHandle, LSAPR_TRUST_INFORMATION TrustedDomainInformation, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(TrustedDomainInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TrustedDomainInformation);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            TrustedDomainHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarEnumerateTrustedDomains(Titanis.DceRpc.RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_TRUSTED_ENUM_BUFFER> EnumerationBuffer, uint PreferedMaximumLength, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(EnumerationContext.value);
            encoder.WriteValue(PreferedMaximumLength);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EnumerationContext.value = decoder.ReadUInt32();
            EnumerationBuffer.value = decoder.ReadFixedStruct<LSAPR_TRUSTED_ENUM_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRUSTED_ENUM_BUFFER>(ref EnumerationBuffer.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarLookupNames(Titanis.DceRpc.RpcContextHandle PolicyHandle, uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(Count);
            if ((Names != null)) {
                encoder.WriteArrayHeader(Names);
                for (int i = 0; (i < Names.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
            }
            for (int i = 0; (i < Names.Length); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                encoder.WriteStructDeferral(elem_0);
            }
            encoder.WriteFixedStruct(TranslatedSids.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedSids.value);
            encoder.WriteValue(((short)(LookupLevel)));
            encoder.WriteValue(MappedCount.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ReferencedDomains.value = decoder.ReadOutPointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
            }
            TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS>(ref TranslatedSids.value);
            MappedCount.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarLookupSids(Titanis.DceRpc.RpcContextHandle PolicyHandle, LSAPR_SID_ENUM_BUFFER SidEnumBuffer, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_NAMES> TranslatedNames, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(SidEnumBuffer, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(SidEnumBuffer);
            encoder.WriteFixedStruct(TranslatedNames.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedNames.value);
            encoder.WriteValue(((short)(LookupLevel)));
            encoder.WriteValue(MappedCount.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ReferencedDomains.value = decoder.ReadOutPointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
            }
            TranslatedNames.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAMES>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAMES>(ref TranslatedNames.value);
            MappedCount.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarCreateSecret(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING SecretName, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> SecretHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(SecretName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(SecretName);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            SecretHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarOpenAccount(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> AccountHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteConformantStruct(AccountSid, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(AccountSid);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            AccountHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarEnumeratePrivilegesAccount(Titanis.DceRpc.RpcContextHandle AccountHandle, RpcPointer<RpcPointer<LSAPR_PRIVILEGE_SET>> Privileges, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AccountHandle);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Privileges.value = decoder.ReadOutPointer<LSAPR_PRIVILEGE_SET>(Privileges.value);
            if ((null != Privileges.value)) {
                Privileges.value.value = decoder.ReadConformantStruct<LSAPR_PRIVILEGE_SET>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<LSAPR_PRIVILEGE_SET>(ref Privileges.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarAddPrivilegesToAccount(Titanis.DceRpc.RpcContextHandle AccountHandle, LSAPR_PRIVILEGE_SET Privileges, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AccountHandle);
            encoder.WriteConformantStruct(Privileges, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(Privileges);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarRemovePrivilegesFromAccount(Titanis.DceRpc.RpcContextHandle AccountHandle, byte AllPrivileges, RpcPointer<LSAPR_PRIVILEGE_SET> Privileges, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AccountHandle);
            encoder.WriteValue(AllPrivileges);
            encoder.WritePointer(Privileges);
            if ((null != Privileges)) {
                encoder.WriteConformantStruct(Privileges.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(Privileges.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum21NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum22NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> LsarGetSystemAccessAccount(Titanis.DceRpc.RpcContextHandle AccountHandle, RpcPointer<uint> SystemAccess, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AccountHandle);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            SystemAccess.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarSetSystemAccessAccount(Titanis.DceRpc.RpcContextHandle AccountHandle, uint SystemAccess, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AccountHandle);
            encoder.WriteValue(SystemAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarOpenTrustedDomain(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteConformantStruct(TrustedDomainSid, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(TrustedDomainSid);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            TrustedDomainHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarQueryInfoTrustedDomain(Titanis.DceRpc.RpcContextHandle TrustedDomainHandle, TRUSTED_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(TrustedDomainHandle);
            encoder.WriteValue(((short)(InformationClass)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            TrustedDomainInformation.value = decoder.ReadOutPointer<LSAPR_TRUSTED_DOMAIN_INFO>(TrustedDomainInformation.value);
            if ((null != TrustedDomainInformation.value)) {
                TrustedDomainInformation.value.value = decoder.ReadUnion<LSAPR_TRUSTED_DOMAIN_INFO>();
                decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFO>(ref TrustedDomainInformation.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarSetInformationTrustedDomain(Titanis.DceRpc.RpcContextHandle TrustedDomainHandle, TRUSTED_INFORMATION_CLASS InformationClass, LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(TrustedDomainHandle);
            encoder.WriteValue(((short)(InformationClass)));
            encoder.WriteUnion(TrustedDomainInformation);
            encoder.WriteStructDeferral(TrustedDomainInformation);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarOpenSecret(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING SecretName, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> SecretHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(SecretName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(SecretName);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            SecretHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarSetSecret(Titanis.DceRpc.RpcContextHandle SecretHandle, RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedCurrentValue, RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedOldValue, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(SecretHandle);
            encoder.WritePointer(EncryptedCurrentValue);
            if ((null != EncryptedCurrentValue)) {
                encoder.WriteFixedStruct(EncryptedCurrentValue.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(EncryptedCurrentValue.value);
            }
            encoder.WritePointer(EncryptedOldValue);
            if ((null != EncryptedOldValue)) {
                encoder.WriteFixedStruct(EncryptedOldValue.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(EncryptedOldValue.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarQuerySecret(Titanis.DceRpc.RpcContextHandle SecretHandle, RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedCurrentValue, RpcPointer<ms_dtyp.LARGE_INTEGER> CurrentValueSetTime, RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedOldValue, RpcPointer<ms_dtyp.LARGE_INTEGER> OldValueSetTime, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(SecretHandle);
            encoder.WritePointer(EncryptedCurrentValue);
            if ((null != EncryptedCurrentValue)) {
                encoder.WritePointer(EncryptedCurrentValue.value);
                if ((null != EncryptedCurrentValue.value)) {
                    encoder.WriteFixedStruct(EncryptedCurrentValue.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(EncryptedCurrentValue.value.value);
                }
            }
            encoder.WritePointer(CurrentValueSetTime);
            if ((null != CurrentValueSetTime)) {
                encoder.WriteFixedStruct(CurrentValueSetTime.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(CurrentValueSetTime.value);
            }
            encoder.WritePointer(EncryptedOldValue);
            if ((null != EncryptedOldValue)) {
                encoder.WritePointer(EncryptedOldValue.value);
                if ((null != EncryptedOldValue.value)) {
                    encoder.WriteFixedStruct(EncryptedOldValue.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(EncryptedOldValue.value.value);
                }
            }
            encoder.WritePointer(OldValueSetTime);
            if ((null != OldValueSetTime)) {
                encoder.WriteFixedStruct(OldValueSetTime.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(OldValueSetTime.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EncryptedCurrentValue = decoder.ReadOutPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>(EncryptedCurrentValue);
            if ((null != EncryptedCurrentValue)) {
                EncryptedCurrentValue.value = decoder.ReadPointer<LSAPR_CR_CIPHER_VALUE>();
                if ((null != EncryptedCurrentValue.value)) {
                    EncryptedCurrentValue.value.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedCurrentValue.value.value);
                }
            }
            CurrentValueSetTime = decoder.ReadOutPointer<ms_dtyp.LARGE_INTEGER>(CurrentValueSetTime);
            if ((null != CurrentValueSetTime)) {
                CurrentValueSetTime.value = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref CurrentValueSetTime.value);
            }
            EncryptedOldValue = decoder.ReadOutPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>(EncryptedOldValue);
            if ((null != EncryptedOldValue)) {
                EncryptedOldValue.value = decoder.ReadPointer<LSAPR_CR_CIPHER_VALUE>();
                if ((null != EncryptedOldValue.value)) {
                    EncryptedOldValue.value.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedOldValue.value.value);
                }
            }
            OldValueSetTime = decoder.ReadOutPointer<ms_dtyp.LARGE_INTEGER>(OldValueSetTime);
            if ((null != OldValueSetTime)) {
                OldValueSetTime.value = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref OldValueSetTime.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarLookupPrivilegeValue(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING Name, RpcPointer<ms_dtyp.LUID> Value, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Name);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Value.value = decoder.ReadFixedStruct<ms_dtyp.LUID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.LUID>(ref Value.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarLookupPrivilegeName(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.LUID Value, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> Name, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(Value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(Value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Name.value = decoder.ReadOutPointer<ms_dtyp.RPC_UNICODE_STRING>(Name.value);
            if ((null != Name.value)) {
                Name.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarLookupPrivilegeDisplayName(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING Name, short ClientLanguage, short ClientSystemDefaultLanguage, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> DisplayName, RpcPointer<ushort> LanguageReturned, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Name);
            encoder.WriteValue(ClientLanguage);
            encoder.WriteValue(ClientSystemDefaultLanguage);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            DisplayName.value = decoder.ReadOutPointer<ms_dtyp.RPC_UNICODE_STRING>(DisplayName.value);
            if ((null != DisplayName.value)) {
                DisplayName.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref DisplayName.value.value);
            }
            LanguageReturned.value = decoder.ReadUInt16();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarDeleteObject(RpcPointer<Titanis.DceRpc.RpcContextHandle> ObjectHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(ObjectHandle.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ObjectHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarEnumerateAccountsWithUserRight(Titanis.DceRpc.RpcContextHandle PolicyHandle, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> UserRight, RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> EnumerationBuffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WritePointer(UserRight);
            if ((null != UserRight)) {
                encoder.WriteFixedStruct(UserRight.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(UserRight.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EnumerationBuffer.value = decoder.ReadFixedStruct<LSAPR_ACCOUNT_ENUM_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_ACCOUNT_ENUM_BUFFER>(ref EnumerationBuffer.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarEnumerateAccountRights(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, RpcPointer<LSAPR_USER_RIGHT_SET> UserRights, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(36);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteConformantStruct(AccountSid, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(AccountSid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            UserRights.value = decoder.ReadFixedStruct<LSAPR_USER_RIGHT_SET>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_USER_RIGHT_SET>(ref UserRights.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarAddAccountRights(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, LSAPR_USER_RIGHT_SET UserRights, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(37);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteConformantStruct(AccountSid, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(AccountSid);
            encoder.WriteFixedStruct(UserRights, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(UserRights);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarRemoveAccountRights(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, byte AllRights, LSAPR_USER_RIGHT_SET UserRights, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(38);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteConformantStruct(AccountSid, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(AccountSid);
            encoder.WriteValue(AllRights);
            encoder.WriteFixedStruct(UserRights, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(UserRights);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarQueryTrustedDomainInfo(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, TRUSTED_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(39);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteConformantStruct(TrustedDomainSid, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(TrustedDomainSid);
            encoder.WriteValue(((short)(InformationClass)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            TrustedDomainInformation.value = decoder.ReadOutPointer<LSAPR_TRUSTED_DOMAIN_INFO>(TrustedDomainInformation.value);
            if ((null != TrustedDomainInformation.value)) {
                TrustedDomainInformation.value.value = decoder.ReadUnion<LSAPR_TRUSTED_DOMAIN_INFO>();
                decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFO>(ref TrustedDomainInformation.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarSetTrustedDomainInfo(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, TRUSTED_INFORMATION_CLASS InformationClass, LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(40);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteConformantStruct(TrustedDomainSid, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(TrustedDomainSid);
            encoder.WriteValue(((short)(InformationClass)));
            encoder.WriteUnion(TrustedDomainInformation);
            encoder.WriteStructDeferral(TrustedDomainInformation);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarDeleteTrustedDomain(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(41);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteConformantStruct(TrustedDomainSid, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(TrustedDomainSid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarStorePrivateData(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING KeyName, RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedData, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(42);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(KeyName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(KeyName);
            encoder.WritePointer(EncryptedData);
            if ((null != EncryptedData)) {
                encoder.WriteFixedStruct(EncryptedData.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(EncryptedData.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarRetrievePrivateData(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING KeyName, RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedData, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(43);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(KeyName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(KeyName);
            encoder.WritePointer(EncryptedData.value);
            if ((null != EncryptedData.value)) {
                encoder.WriteFixedStruct(EncryptedData.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(EncryptedData.value.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EncryptedData.value = decoder.ReadOutPointer<LSAPR_CR_CIPHER_VALUE>(EncryptedData.value);
            if ((null != EncryptedData.value)) {
                EncryptedData.value.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedData.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarOpenPolicy2(string SystemName, LSAPR_OBJECT_ATTRIBUTES ObjectAttributes, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> PolicyHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(44);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteUniqueReferentId((SystemName == null));
            if ((SystemName != null)) {
                encoder.WriteWideCharString(SystemName);
            }
            encoder.WriteFixedStruct(ObjectAttributes, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ObjectAttributes);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            PolicyHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarGetUserName(string SystemName, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> UserName, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> DomainName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(45);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteUniqueReferentId((SystemName == null));
            if ((SystemName != null)) {
                encoder.WriteWideCharString(SystemName);
            }
            encoder.WritePointer(UserName.value);
            if ((null != UserName.value)) {
                encoder.WriteFixedStruct(UserName.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(UserName.value.value);
            }
            encoder.WritePointer(DomainName);
            if ((null != DomainName)) {
                encoder.WritePointer(DomainName.value);
                if ((null != DomainName.value)) {
                    encoder.WriteFixedStruct(DomainName.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(DomainName.value.value);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            UserName.value = decoder.ReadOutPointer<ms_dtyp.RPC_UNICODE_STRING>(UserName.value);
            if ((null != UserName.value)) {
                UserName.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref UserName.value.value);
            }
            DomainName = decoder.ReadOutPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>(DomainName);
            if ((null != DomainName)) {
                DomainName.value = decoder.ReadPointer<ms_dtyp.RPC_UNICODE_STRING>();
                if ((null != DomainName.value)) {
                    DomainName.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref DomainName.value.value);
                }
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarQueryInformationPolicy2(Titanis.DceRpc.RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>> PolicyInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(46);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(((short)(InformationClass)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            PolicyInformation.value = decoder.ReadOutPointer<LSAPR_POLICY_INFORMATION>(PolicyInformation.value);
            if ((null != PolicyInformation.value)) {
                PolicyInformation.value.value = decoder.ReadUnion<LSAPR_POLICY_INFORMATION>();
                decoder.ReadStructDeferral<LSAPR_POLICY_INFORMATION>(ref PolicyInformation.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarSetInformationPolicy2(Titanis.DceRpc.RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, LSAPR_POLICY_INFORMATION PolicyInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(47);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(((short)(InformationClass)));
            encoder.WriteUnion(PolicyInformation);
            encoder.WriteStructDeferral(PolicyInformation);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarQueryTrustedDomainInfoByName(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, TRUSTED_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(48);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(TrustedDomainName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TrustedDomainName);
            encoder.WriteValue(((short)(InformationClass)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            TrustedDomainInformation.value = decoder.ReadOutPointer<LSAPR_TRUSTED_DOMAIN_INFO>(TrustedDomainInformation.value);
            if ((null != TrustedDomainInformation.value)) {
                TrustedDomainInformation.value.value = decoder.ReadUnion<LSAPR_TRUSTED_DOMAIN_INFO>();
                decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFO>(ref TrustedDomainInformation.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarSetTrustedDomainInfoByName(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, TRUSTED_INFORMATION_CLASS InformationClass, LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(49);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(TrustedDomainName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TrustedDomainName);
            encoder.WriteValue(((short)(InformationClass)));
            encoder.WriteUnion(TrustedDomainInformation);
            encoder.WriteStructDeferral(TrustedDomainInformation);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarEnumerateTrustedDomainsEx(Titanis.DceRpc.RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_TRUSTED_ENUM_BUFFER_EX> EnumerationBuffer, uint PreferedMaximumLength, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(50);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(EnumerationContext.value);
            encoder.WriteValue(PreferedMaximumLength);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EnumerationContext.value = decoder.ReadUInt32();
            EnumerationBuffer.value = decoder.ReadFixedStruct<LSAPR_TRUSTED_ENUM_BUFFER_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRUSTED_ENUM_BUFFER_EX>(ref EnumerationBuffer.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarCreateTrustedDomainEx(Titanis.DceRpc.RpcContextHandle PolicyHandle, LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInformation, LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION AuthenticationInformation, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(51);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(TrustedDomainInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TrustedDomainInformation);
            encoder.WriteFixedStruct(AuthenticationInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(AuthenticationInformation);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            TrustedDomainHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum52NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(52);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> LsarQueryDomainInformationPolicy(Titanis.DceRpc.RpcContextHandle PolicyHandle, POLICY_DOMAIN_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION>> PolicyDomainInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(53);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(((short)(InformationClass)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            PolicyDomainInformation.value = decoder.ReadOutPointer<LSAPR_POLICY_DOMAIN_INFORMATION>(PolicyDomainInformation.value);
            if ((null != PolicyDomainInformation.value)) {
                PolicyDomainInformation.value.value = decoder.ReadUnion<LSAPR_POLICY_DOMAIN_INFORMATION>();
                decoder.ReadStructDeferral<LSAPR_POLICY_DOMAIN_INFORMATION>(ref PolicyDomainInformation.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarSetDomainInformationPolicy(Titanis.DceRpc.RpcContextHandle PolicyHandle, POLICY_DOMAIN_INFORMATION_CLASS InformationClass, RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION> PolicyDomainInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(54);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(((short)(InformationClass)));
            encoder.WritePointer(PolicyDomainInformation);
            if ((null != PolicyDomainInformation)) {
                encoder.WriteUnion(PolicyDomainInformation.value);
                encoder.WriteStructDeferral(PolicyDomainInformation.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarOpenTrustedDomainByName(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(55);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(TrustedDomainName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TrustedDomainName);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            TrustedDomainHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum56NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(56);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> LsarLookupSids2(Titanis.DceRpc.RpcContextHandle PolicyHandle, LSAPR_SID_ENUM_BUFFER SidEnumBuffer, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_NAMES_EX> TranslatedNames, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(57);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(SidEnumBuffer, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(SidEnumBuffer);
            encoder.WriteFixedStruct(TranslatedNames.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedNames.value);
            encoder.WriteValue(((short)(LookupLevel)));
            encoder.WriteValue(MappedCount.value);
            encoder.WriteValue(LookupOptions);
            encoder.WriteValue(ClientRevision);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ReferencedDomains.value = decoder.ReadOutPointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
            }
            TranslatedNames.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAMES_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAMES_EX>(ref TranslatedNames.value);
            MappedCount.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarLookupNames2(Titanis.DceRpc.RpcContextHandle PolicyHandle, uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS_EX> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(58);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(Count);
            if ((Names != null)) {
                encoder.WriteArrayHeader(Names);
                for (int i = 0; (i < Names.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
            }
            for (int i = 0; (i < Names.Length); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                encoder.WriteStructDeferral(elem_0);
            }
            encoder.WriteFixedStruct(TranslatedSids.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedSids.value);
            encoder.WriteValue(((short)(LookupLevel)));
            encoder.WriteValue(MappedCount.value);
            encoder.WriteValue(LookupOptions);
            encoder.WriteValue(ClientRevision);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ReferencedDomains.value = decoder.ReadOutPointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
            }
            TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS_EX>(ref TranslatedSids.value);
            MappedCount.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarCreateTrustedDomainEx2(Titanis.DceRpc.RpcContextHandle PolicyHandle, LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInformation, LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL AuthenticationInformation, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(59);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(TrustedDomainInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TrustedDomainInformation);
            encoder.WriteFixedStruct(AuthenticationInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(AuthenticationInformation);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            TrustedDomainHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum60NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(60);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum61NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(61);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum62NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(62);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum63NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(63);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum64NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(64);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum65NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(65);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum66NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(66);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum67NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(67);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> LsarLookupNames3(Titanis.DceRpc.RpcContextHandle PolicyHandle, uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS_EX2> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(68);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteValue(Count);
            if ((Names != null)) {
                encoder.WriteArrayHeader(Names);
                for (int i = 0; (i < Names.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
            }
            for (int i = 0; (i < Names.Length); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                encoder.WriteStructDeferral(elem_0);
            }
            encoder.WriteFixedStruct(TranslatedSids.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedSids.value);
            encoder.WriteValue(((short)(LookupLevel)));
            encoder.WriteValue(MappedCount.value);
            encoder.WriteValue(LookupOptions);
            encoder.WriteValue(ClientRevision);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ReferencedDomains.value = decoder.ReadOutPointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
            }
            TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS_EX2>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS_EX2>(ref TranslatedSids.value);
            MappedCount.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum69NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(69);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum70NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(70);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum71NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(71);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum72NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(72);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> LsarQueryForestTrustInformation(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, LSA_FOREST_TRUST_RECORD_TYPE HighestRecordType, RpcPointer<RpcPointer<LSA_FOREST_TRUST_INFORMATION>> ForestTrustInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(73);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(TrustedDomainName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TrustedDomainName);
            encoder.WriteValue(((short)(HighestRecordType)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ForestTrustInfo.value = decoder.ReadOutPointer<LSA_FOREST_TRUST_INFORMATION>(ForestTrustInfo.value);
            if ((null != ForestTrustInfo.value)) {
                ForestTrustInfo.value.value = decoder.ReadFixedStruct<LSA_FOREST_TRUST_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSA_FOREST_TRUST_INFORMATION>(ref ForestTrustInfo.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarSetForestTrustInformation(Titanis.DceRpc.RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, LSA_FOREST_TRUST_RECORD_TYPE HighestRecordType, LSA_FOREST_TRUST_INFORMATION ForestTrustInfo, byte CheckOnly, RpcPointer<RpcPointer<LSA_FOREST_TRUST_COLLISION_INFORMATION>> CollisionInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(74);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(PolicyHandle);
            encoder.WriteFixedStruct(TrustedDomainName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TrustedDomainName);
            encoder.WriteValue(((short)(HighestRecordType)));
            encoder.WriteFixedStruct(ForestTrustInfo, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(ForestTrustInfo);
            encoder.WriteValue(CheckOnly);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            CollisionInfo.value = decoder.ReadOutPointer<LSA_FOREST_TRUST_COLLISION_INFORMATION>(CollisionInfo.value);
            if ((null != CollisionInfo.value)) {
                CollisionInfo.value.value = decoder.ReadFixedStruct<LSA_FOREST_TRUST_COLLISION_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSA_FOREST_TRUST_COLLISION_INFORMATION>(ref CollisionInfo.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum75NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(75);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> LsarLookupSids3(LSAPR_SID_ENUM_BUFFER SidEnumBuffer, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_NAMES_EX> TranslatedNames, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(76);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteFixedStruct(SidEnumBuffer, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(SidEnumBuffer);
            encoder.WriteFixedStruct(TranslatedNames.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedNames.value);
            encoder.WriteValue(((short)(LookupLevel)));
            encoder.WriteValue(MappedCount.value);
            encoder.WriteValue(LookupOptions);
            encoder.WriteValue(ClientRevision);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ReferencedDomains.value = decoder.ReadOutPointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
            }
            TranslatedNames.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAMES_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAMES_EX>(ref TranslatedNames.value);
            MappedCount.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> LsarLookupNames4(uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS_EX2> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(77);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(Count);
            if ((Names != null)) {
                encoder.WriteArrayHeader(Names);
                for (int i = 0; (i < Names.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
            }
            for (int i = 0; (i < Names.Length); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                encoder.WriteStructDeferral(elem_0);
            }
            encoder.WriteFixedStruct(TranslatedSids.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedSids.value);
            encoder.WriteValue(((short)(LookupLevel)));
            encoder.WriteValue(MappedCount.value);
            encoder.WriteValue(LookupOptions);
            encoder.WriteValue(ClientRevision);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ReferencedDomains.value = decoder.ReadOutPointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
            }
            TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS_EX2>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS_EX2>(ref TranslatedSids.value);
            MappedCount.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public class lsarpcStub : Titanis.DceRpc.Server.RpcServiceStub {
        private static System.Guid _interfaceUuid = new System.Guid("12345778-1234-abcd-ef00-0123456789ab");
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
        private lsarpc _obj;
        public lsarpcStub(lsarpc obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_LsarClose,
                    this.Invoke_Opnum1NotUsedOnWire,
                    this.Invoke_LsarEnumeratePrivileges,
                    this.Invoke_LsarQuerySecurityObject,
                    this.Invoke_LsarSetSecurityObject,
                    this.Invoke_Opnum5NotUsedOnWire,
                    this.Invoke_LsarOpenPolicy,
                    this.Invoke_LsarQueryInformationPolicy,
                    this.Invoke_LsarSetInformationPolicy,
                    this.Invoke_Opnum9NotUsedOnWire,
                    this.Invoke_LsarCreateAccount,
                    this.Invoke_LsarEnumerateAccounts,
                    this.Invoke_LsarCreateTrustedDomain,
                    this.Invoke_LsarEnumerateTrustedDomains,
                    this.Invoke_LsarLookupNames,
                    this.Invoke_LsarLookupSids,
                    this.Invoke_LsarCreateSecret,
                    this.Invoke_LsarOpenAccount,
                    this.Invoke_LsarEnumeratePrivilegesAccount,
                    this.Invoke_LsarAddPrivilegesToAccount,
                    this.Invoke_LsarRemovePrivilegesFromAccount,
                    this.Invoke_Opnum21NotUsedOnWire,
                    this.Invoke_Opnum22NotUsedOnWire,
                    this.Invoke_LsarGetSystemAccessAccount,
                    this.Invoke_LsarSetSystemAccessAccount,
                    this.Invoke_LsarOpenTrustedDomain,
                    this.Invoke_LsarQueryInfoTrustedDomain,
                    this.Invoke_LsarSetInformationTrustedDomain,
                    this.Invoke_LsarOpenSecret,
                    this.Invoke_LsarSetSecret,
                    this.Invoke_LsarQuerySecret,
                    this.Invoke_LsarLookupPrivilegeValue,
                    this.Invoke_LsarLookupPrivilegeName,
                    this.Invoke_LsarLookupPrivilegeDisplayName,
                    this.Invoke_LsarDeleteObject,
                    this.Invoke_LsarEnumerateAccountsWithUserRight,
                    this.Invoke_LsarEnumerateAccountRights,
                    this.Invoke_LsarAddAccountRights,
                    this.Invoke_LsarRemoveAccountRights,
                    this.Invoke_LsarQueryTrustedDomainInfo,
                    this.Invoke_LsarSetTrustedDomainInfo,
                    this.Invoke_LsarDeleteTrustedDomain,
                    this.Invoke_LsarStorePrivateData,
                    this.Invoke_LsarRetrievePrivateData,
                    this.Invoke_LsarOpenPolicy2,
                    this.Invoke_LsarGetUserName,
                    this.Invoke_LsarQueryInformationPolicy2,
                    this.Invoke_LsarSetInformationPolicy2,
                    this.Invoke_LsarQueryTrustedDomainInfoByName,
                    this.Invoke_LsarSetTrustedDomainInfoByName,
                    this.Invoke_LsarEnumerateTrustedDomainsEx,
                    this.Invoke_LsarCreateTrustedDomainEx,
                    this.Invoke_Opnum52NotUsedOnWire,
                    this.Invoke_LsarQueryDomainInformationPolicy,
                    this.Invoke_LsarSetDomainInformationPolicy,
                    this.Invoke_LsarOpenTrustedDomainByName,
                    this.Invoke_Opnum56NotUsedOnWire,
                    this.Invoke_LsarLookupSids2,
                    this.Invoke_LsarLookupNames2,
                    this.Invoke_LsarCreateTrustedDomainEx2,
                    this.Invoke_Opnum60NotUsedOnWire,
                    this.Invoke_Opnum61NotUsedOnWire,
                    this.Invoke_Opnum62NotUsedOnWire,
                    this.Invoke_Opnum63NotUsedOnWire,
                    this.Invoke_Opnum64NotUsedOnWire,
                    this.Invoke_Opnum65NotUsedOnWire,
                    this.Invoke_Opnum66NotUsedOnWire,
                    this.Invoke_Opnum67NotUsedOnWire,
                    this.Invoke_LsarLookupNames3,
                    this.Invoke_Opnum69NotUsedOnWire,
                    this.Invoke_Opnum70NotUsedOnWire,
                    this.Invoke_Opnum71NotUsedOnWire,
                    this.Invoke_Opnum72NotUsedOnWire,
                    this.Invoke_LsarQueryForestTrustInformation,
                    this.Invoke_LsarSetForestTrustInformation,
                    this.Invoke_Opnum75NotUsedOnWire,
                    this.Invoke_LsarLookupSids3,
                    this.Invoke_LsarLookupNames4};
        }
        private async Task Invoke_LsarClose(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> ObjectHandle;
            ObjectHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            ObjectHandle.value = decoder.ReadContextHandle();
            var invokeTask = this._obj.LsarClose(ObjectHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(ObjectHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_LsarEnumeratePrivileges(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            RpcPointer<uint> EnumerationContext;
            RpcPointer<LSAPR_PRIVILEGE_ENUM_BUFFER> EnumerationBuffer = new RpcPointer<LSAPR_PRIVILEGE_ENUM_BUFFER>();
            uint PreferedMaximumLength;
            PolicyHandle = decoder.ReadContextHandle();
            EnumerationContext = new RpcPointer<uint>();
            EnumerationContext.value = decoder.ReadUInt32();
            PreferedMaximumLength = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarEnumeratePrivileges(PolicyHandle, EnumerationContext, EnumerationBuffer, PreferedMaximumLength, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(EnumerationContext.value);
            encoder.WriteFixedStruct(EnumerationBuffer.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(EnumerationBuffer.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarQuerySecurityObject(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle ObjectHandle;
            uint SecurityInformation;
            RpcPointer<RpcPointer<LSAPR_SR_SECURITY_DESCRIPTOR>> SecurityDescriptor = new RpcPointer<RpcPointer<LSAPR_SR_SECURITY_DESCRIPTOR>>();
            ObjectHandle = decoder.ReadContextHandle();
            SecurityInformation = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarQuerySecurityObject(ObjectHandle, SecurityInformation, SecurityDescriptor, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(SecurityDescriptor.value);
            if ((null != SecurityDescriptor.value)) {
                encoder.WriteFixedStruct(SecurityDescriptor.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(SecurityDescriptor.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarSetSecurityObject(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle ObjectHandle;
            uint SecurityInformation;
            LSAPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor;
            ObjectHandle = decoder.ReadContextHandle();
            SecurityInformation = decoder.ReadUInt32();
            SecurityDescriptor = decoder.ReadFixedStruct<LSAPR_SR_SECURITY_DESCRIPTOR>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_SR_SECURITY_DESCRIPTOR>(ref SecurityDescriptor);
            var invokeTask = this._obj.LsarSetSecurityObject(ObjectHandle, SecurityInformation, SecurityDescriptor, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum5NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum5NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_LsarOpenPolicy(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<char> SystemName;
            LSAPR_OBJECT_ATTRIBUTES ObjectAttributes;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> PolicyHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            SystemName = decoder.ReadPointer<char>();
            if ((null != SystemName)) {
                SystemName.value = decoder.ReadWideChar();
            }
            ObjectAttributes = decoder.ReadFixedStruct<LSAPR_OBJECT_ATTRIBUTES>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_OBJECT_ATTRIBUTES>(ref ObjectAttributes);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarOpenPolicy(SystemName, ObjectAttributes, DesiredAccess, PolicyHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(PolicyHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarQueryInformationPolicy(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            POLICY_INFORMATION_CLASS InformationClass;
            RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>> PolicyInformation = new RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>>();
            PolicyHandle = decoder.ReadContextHandle();
            InformationClass = ((POLICY_INFORMATION_CLASS)(decoder.ReadInt16()));
            var invokeTask = this._obj.LsarQueryInformationPolicy(PolicyHandle, InformationClass, PolicyInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(PolicyInformation.value);
            if ((null != PolicyInformation.value)) {
                encoder.WriteUnion(PolicyInformation.value.value);
                encoder.WriteStructDeferral(PolicyInformation.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarSetInformationPolicy(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            POLICY_INFORMATION_CLASS InformationClass;
            LSAPR_POLICY_INFORMATION PolicyInformation;
            PolicyHandle = decoder.ReadContextHandle();
            InformationClass = ((POLICY_INFORMATION_CLASS)(decoder.ReadInt16()));
            PolicyInformation = decoder.ReadUnion<LSAPR_POLICY_INFORMATION>();
            decoder.ReadStructDeferral<LSAPR_POLICY_INFORMATION>(ref PolicyInformation);
            var invokeTask = this._obj.LsarSetInformationPolicy(PolicyHandle, InformationClass, PolicyInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum9NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum9NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_LsarCreateAccount(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_SID AccountSid;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> AccountHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            PolicyHandle = decoder.ReadContextHandle();
            AccountSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref AccountSid);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarCreateAccount(PolicyHandle, AccountSid, DesiredAccess, AccountHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(AccountHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarEnumerateAccounts(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            RpcPointer<uint> EnumerationContext;
            RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> EnumerationBuffer = new RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER>();
            uint PreferedMaximumLength;
            PolicyHandle = decoder.ReadContextHandle();
            EnumerationContext = new RpcPointer<uint>();
            EnumerationContext.value = decoder.ReadUInt32();
            PreferedMaximumLength = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarEnumerateAccounts(PolicyHandle, EnumerationContext, EnumerationBuffer, PreferedMaximumLength, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(EnumerationContext.value);
            encoder.WriteFixedStruct(EnumerationBuffer.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(EnumerationBuffer.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarCreateTrustedDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            LSAPR_TRUST_INFORMATION TrustedDomainInformation;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            PolicyHandle = decoder.ReadContextHandle();
            TrustedDomainInformation = decoder.ReadFixedStruct<LSAPR_TRUST_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRUST_INFORMATION>(ref TrustedDomainInformation);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarCreateTrustedDomain(PolicyHandle, TrustedDomainInformation, DesiredAccess, TrustedDomainHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(TrustedDomainHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarEnumerateTrustedDomains(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            RpcPointer<uint> EnumerationContext;
            RpcPointer<LSAPR_TRUSTED_ENUM_BUFFER> EnumerationBuffer = new RpcPointer<LSAPR_TRUSTED_ENUM_BUFFER>();
            uint PreferedMaximumLength;
            PolicyHandle = decoder.ReadContextHandle();
            EnumerationContext = new RpcPointer<uint>();
            EnumerationContext.value = decoder.ReadUInt32();
            PreferedMaximumLength = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarEnumerateTrustedDomains(PolicyHandle, EnumerationContext, EnumerationBuffer, PreferedMaximumLength, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(EnumerationContext.value);
            encoder.WriteFixedStruct(EnumerationBuffer.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(EnumerationBuffer.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarLookupNames(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            uint Count;
            ms_dtyp.RPC_UNICODE_STRING[] Names;
            RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
            RpcPointer<LSAPR_TRANSLATED_SIDS> TranslatedSids;
            LSAP_LOOKUP_LEVEL LookupLevel;
            RpcPointer<uint> MappedCount;
            PolicyHandle = decoder.ReadContextHandle();
            Count = decoder.ReadUInt32();
            Names = decoder.ReadArrayHeader<ms_dtyp.RPC_UNICODE_STRING>();
            for (int i = 0; (i < Names.Length); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                Names[i] = elem_0;
            }
            for (int i = 0; (i < Names.Length); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
                Names[i] = elem_0;
            }
            TranslatedSids = new RpcPointer<LSAPR_TRANSLATED_SIDS>();
            TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS>(ref TranslatedSids.value);
            LookupLevel = ((LSAP_LOOKUP_LEVEL)(decoder.ReadInt16()));
            MappedCount = new RpcPointer<uint>();
            MappedCount.value = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarLookupNames(PolicyHandle, Count, Names, ReferencedDomains, TranslatedSids, LookupLevel, MappedCount, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                encoder.WriteFixedStruct(ReferencedDomains.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ReferencedDomains.value.value);
            }
            encoder.WriteFixedStruct(TranslatedSids.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedSids.value);
            encoder.WriteValue(MappedCount.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarLookupSids(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            LSAPR_SID_ENUM_BUFFER SidEnumBuffer;
            RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
            RpcPointer<LSAPR_TRANSLATED_NAMES> TranslatedNames;
            LSAP_LOOKUP_LEVEL LookupLevel;
            RpcPointer<uint> MappedCount;
            PolicyHandle = decoder.ReadContextHandle();
            SidEnumBuffer = decoder.ReadFixedStruct<LSAPR_SID_ENUM_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_SID_ENUM_BUFFER>(ref SidEnumBuffer);
            TranslatedNames = new RpcPointer<LSAPR_TRANSLATED_NAMES>();
            TranslatedNames.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAMES>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAMES>(ref TranslatedNames.value);
            LookupLevel = ((LSAP_LOOKUP_LEVEL)(decoder.ReadInt16()));
            MappedCount = new RpcPointer<uint>();
            MappedCount.value = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarLookupSids(PolicyHandle, SidEnumBuffer, ReferencedDomains, TranslatedNames, LookupLevel, MappedCount, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                encoder.WriteFixedStruct(ReferencedDomains.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ReferencedDomains.value.value);
            }
            encoder.WriteFixedStruct(TranslatedNames.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedNames.value);
            encoder.WriteValue(MappedCount.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarCreateSecret(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_UNICODE_STRING SecretName;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> SecretHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            PolicyHandle = decoder.ReadContextHandle();
            SecretName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref SecretName);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarCreateSecret(PolicyHandle, SecretName, DesiredAccess, SecretHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(SecretHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarOpenAccount(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_SID AccountSid;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> AccountHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            PolicyHandle = decoder.ReadContextHandle();
            AccountSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref AccountSid);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarOpenAccount(PolicyHandle, AccountSid, DesiredAccess, AccountHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(AccountHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarEnumeratePrivilegesAccount(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle AccountHandle;
            RpcPointer<RpcPointer<LSAPR_PRIVILEGE_SET>> Privileges = new RpcPointer<RpcPointer<LSAPR_PRIVILEGE_SET>>();
            AccountHandle = decoder.ReadContextHandle();
            var invokeTask = this._obj.LsarEnumeratePrivilegesAccount(AccountHandle, Privileges, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(Privileges.value);
            if ((null != Privileges.value)) {
                encoder.WriteConformantStruct(Privileges.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(Privileges.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarAddPrivilegesToAccount(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle AccountHandle;
            LSAPR_PRIVILEGE_SET Privileges;
            AccountHandle = decoder.ReadContextHandle();
            Privileges = decoder.ReadConformantStruct<LSAPR_PRIVILEGE_SET>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<LSAPR_PRIVILEGE_SET>(ref Privileges);
            var invokeTask = this._obj.LsarAddPrivilegesToAccount(AccountHandle, Privileges, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarRemovePrivilegesFromAccount(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle AccountHandle;
            byte AllPrivileges;
            RpcPointer<LSAPR_PRIVILEGE_SET> Privileges;
            AccountHandle = decoder.ReadContextHandle();
            AllPrivileges = decoder.ReadUnsignedChar();
            Privileges = decoder.ReadPointer<LSAPR_PRIVILEGE_SET>();
            if ((null != Privileges)) {
                Privileges.value = decoder.ReadConformantStruct<LSAPR_PRIVILEGE_SET>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<LSAPR_PRIVILEGE_SET>(ref Privileges.value);
            }
            var invokeTask = this._obj.LsarRemovePrivilegesFromAccount(AccountHandle, AllPrivileges, Privileges, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum21NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum21NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum22NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum22NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_LsarGetSystemAccessAccount(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle AccountHandle;
            RpcPointer<uint> SystemAccess = new RpcPointer<uint>();
            AccountHandle = decoder.ReadContextHandle();
            var invokeTask = this._obj.LsarGetSystemAccessAccount(AccountHandle, SystemAccess, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(SystemAccess.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarSetSystemAccessAccount(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle AccountHandle;
            uint SystemAccess;
            AccountHandle = decoder.ReadContextHandle();
            SystemAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarSetSystemAccessAccount(AccountHandle, SystemAccess, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarOpenTrustedDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_SID TrustedDomainSid;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            PolicyHandle = decoder.ReadContextHandle();
            TrustedDomainSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref TrustedDomainSid);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarOpenTrustedDomain(PolicyHandle, TrustedDomainSid, DesiredAccess, TrustedDomainHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(TrustedDomainHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarQueryInfoTrustedDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle TrustedDomainHandle;
            TRUSTED_INFORMATION_CLASS InformationClass;
            RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation = new RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>>();
            TrustedDomainHandle = decoder.ReadContextHandle();
            InformationClass = ((TRUSTED_INFORMATION_CLASS)(decoder.ReadInt16()));
            var invokeTask = this._obj.LsarQueryInfoTrustedDomain(TrustedDomainHandle, InformationClass, TrustedDomainInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(TrustedDomainInformation.value);
            if ((null != TrustedDomainInformation.value)) {
                encoder.WriteUnion(TrustedDomainInformation.value.value);
                encoder.WriteStructDeferral(TrustedDomainInformation.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarSetInformationTrustedDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle TrustedDomainHandle;
            TRUSTED_INFORMATION_CLASS InformationClass;
            LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation;
            TrustedDomainHandle = decoder.ReadContextHandle();
            InformationClass = ((TRUSTED_INFORMATION_CLASS)(decoder.ReadInt16()));
            TrustedDomainInformation = decoder.ReadUnion<LSAPR_TRUSTED_DOMAIN_INFO>();
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFO>(ref TrustedDomainInformation);
            var invokeTask = this._obj.LsarSetInformationTrustedDomain(TrustedDomainHandle, InformationClass, TrustedDomainInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarOpenSecret(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_UNICODE_STRING SecretName;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> SecretHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            PolicyHandle = decoder.ReadContextHandle();
            SecretName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref SecretName);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarOpenSecret(PolicyHandle, SecretName, DesiredAccess, SecretHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(SecretHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarSetSecret(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle SecretHandle;
            RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedCurrentValue;
            RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedOldValue;
            SecretHandle = decoder.ReadContextHandle();
            EncryptedCurrentValue = decoder.ReadPointer<LSAPR_CR_CIPHER_VALUE>();
            if ((null != EncryptedCurrentValue)) {
                EncryptedCurrentValue.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedCurrentValue.value);
            }
            EncryptedOldValue = decoder.ReadPointer<LSAPR_CR_CIPHER_VALUE>();
            if ((null != EncryptedOldValue)) {
                EncryptedOldValue.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedOldValue.value);
            }
            var invokeTask = this._obj.LsarSetSecret(SecretHandle, EncryptedCurrentValue, EncryptedOldValue, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarQuerySecret(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle SecretHandle;
            RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedCurrentValue;
            RpcPointer<ms_dtyp.LARGE_INTEGER> CurrentValueSetTime;
            RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedOldValue;
            RpcPointer<ms_dtyp.LARGE_INTEGER> OldValueSetTime;
            SecretHandle = decoder.ReadContextHandle();
            EncryptedCurrentValue = decoder.ReadPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>();
            if ((null != EncryptedCurrentValue)) {
                EncryptedCurrentValue.value = decoder.ReadPointer<LSAPR_CR_CIPHER_VALUE>();
                if ((null != EncryptedCurrentValue.value)) {
                    EncryptedCurrentValue.value.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedCurrentValue.value.value);
                }
            }
            CurrentValueSetTime = decoder.ReadPointer<ms_dtyp.LARGE_INTEGER>();
            if ((null != CurrentValueSetTime)) {
                CurrentValueSetTime.value = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref CurrentValueSetTime.value);
            }
            EncryptedOldValue = decoder.ReadPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>();
            if ((null != EncryptedOldValue)) {
                EncryptedOldValue.value = decoder.ReadPointer<LSAPR_CR_CIPHER_VALUE>();
                if ((null != EncryptedOldValue.value)) {
                    EncryptedOldValue.value.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedOldValue.value.value);
                }
            }
            OldValueSetTime = decoder.ReadPointer<ms_dtyp.LARGE_INTEGER>();
            if ((null != OldValueSetTime)) {
                OldValueSetTime.value = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
                decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref OldValueSetTime.value);
            }
            var invokeTask = this._obj.LsarQuerySecret(SecretHandle, EncryptedCurrentValue, CurrentValueSetTime, EncryptedOldValue, OldValueSetTime, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(EncryptedCurrentValue);
            if ((null != EncryptedCurrentValue)) {
                encoder.WritePointer(EncryptedCurrentValue.value);
                if ((null != EncryptedCurrentValue.value)) {
                    encoder.WriteFixedStruct(EncryptedCurrentValue.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(EncryptedCurrentValue.value.value);
                }
            }
            encoder.WritePointer(CurrentValueSetTime);
            if ((null != CurrentValueSetTime)) {
                encoder.WriteFixedStruct(CurrentValueSetTime.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(CurrentValueSetTime.value);
            }
            encoder.WritePointer(EncryptedOldValue);
            if ((null != EncryptedOldValue)) {
                encoder.WritePointer(EncryptedOldValue.value);
                if ((null != EncryptedOldValue.value)) {
                    encoder.WriteFixedStruct(EncryptedOldValue.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(EncryptedOldValue.value.value);
                }
            }
            encoder.WritePointer(OldValueSetTime);
            if ((null != OldValueSetTime)) {
                encoder.WriteFixedStruct(OldValueSetTime.value, Titanis.DceRpc.NdrAlignment._8Byte);
                encoder.WriteStructDeferral(OldValueSetTime.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarLookupPrivilegeValue(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_UNICODE_STRING Name;
            RpcPointer<ms_dtyp.LUID> Value = new RpcPointer<ms_dtyp.LUID>();
            PolicyHandle = decoder.ReadContextHandle();
            Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name);
            var invokeTask = this._obj.LsarLookupPrivilegeValue(PolicyHandle, Name, Value, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(Value.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(Value.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarLookupPrivilegeName(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.LUID Value;
            RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> Name = new RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
            PolicyHandle = decoder.ReadContextHandle();
            Value = decoder.ReadFixedStruct<ms_dtyp.LUID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.LUID>(ref Value);
            var invokeTask = this._obj.LsarLookupPrivilegeName(PolicyHandle, Value, Name, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(Name.value);
            if ((null != Name.value)) {
                encoder.WriteFixedStruct(Name.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Name.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarLookupPrivilegeDisplayName(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_UNICODE_STRING Name;
            short ClientLanguage;
            short ClientSystemDefaultLanguage;
            RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> DisplayName = new RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
            RpcPointer<ushort> LanguageReturned = new RpcPointer<ushort>();
            PolicyHandle = decoder.ReadContextHandle();
            Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name);
            ClientLanguage = decoder.ReadInt16();
            ClientSystemDefaultLanguage = decoder.ReadInt16();
            var invokeTask = this._obj.LsarLookupPrivilegeDisplayName(PolicyHandle, Name, ClientLanguage, ClientSystemDefaultLanguage, DisplayName, LanguageReturned, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(DisplayName.value);
            if ((null != DisplayName.value)) {
                encoder.WriteFixedStruct(DisplayName.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(DisplayName.value.value);
            }
            encoder.WriteValue(LanguageReturned.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarDeleteObject(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> ObjectHandle;
            ObjectHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            ObjectHandle.value = decoder.ReadContextHandle();
            var invokeTask = this._obj.LsarDeleteObject(ObjectHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(ObjectHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarEnumerateAccountsWithUserRight(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            RpcPointer<ms_dtyp.RPC_UNICODE_STRING> UserRight;
            RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> EnumerationBuffer = new RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER>();
            PolicyHandle = decoder.ReadContextHandle();
            UserRight = decoder.ReadPointer<ms_dtyp.RPC_UNICODE_STRING>();
            if ((null != UserRight)) {
                UserRight.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref UserRight.value);
            }
            var invokeTask = this._obj.LsarEnumerateAccountsWithUserRight(PolicyHandle, UserRight, EnumerationBuffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(EnumerationBuffer.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(EnumerationBuffer.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarEnumerateAccountRights(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_SID AccountSid;
            RpcPointer<LSAPR_USER_RIGHT_SET> UserRights = new RpcPointer<LSAPR_USER_RIGHT_SET>();
            PolicyHandle = decoder.ReadContextHandle();
            AccountSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref AccountSid);
            var invokeTask = this._obj.LsarEnumerateAccountRights(PolicyHandle, AccountSid, UserRights, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(UserRights.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(UserRights.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarAddAccountRights(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_SID AccountSid;
            LSAPR_USER_RIGHT_SET UserRights;
            PolicyHandle = decoder.ReadContextHandle();
            AccountSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref AccountSid);
            UserRights = decoder.ReadFixedStruct<LSAPR_USER_RIGHT_SET>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_USER_RIGHT_SET>(ref UserRights);
            var invokeTask = this._obj.LsarAddAccountRights(PolicyHandle, AccountSid, UserRights, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarRemoveAccountRights(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_SID AccountSid;
            byte AllRights;
            LSAPR_USER_RIGHT_SET UserRights;
            PolicyHandle = decoder.ReadContextHandle();
            AccountSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref AccountSid);
            AllRights = decoder.ReadUnsignedChar();
            UserRights = decoder.ReadFixedStruct<LSAPR_USER_RIGHT_SET>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_USER_RIGHT_SET>(ref UserRights);
            var invokeTask = this._obj.LsarRemoveAccountRights(PolicyHandle, AccountSid, AllRights, UserRights, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarQueryTrustedDomainInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_SID TrustedDomainSid;
            TRUSTED_INFORMATION_CLASS InformationClass;
            RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation = new RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>>();
            PolicyHandle = decoder.ReadContextHandle();
            TrustedDomainSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref TrustedDomainSid);
            InformationClass = ((TRUSTED_INFORMATION_CLASS)(decoder.ReadInt16()));
            var invokeTask = this._obj.LsarQueryTrustedDomainInfo(PolicyHandle, TrustedDomainSid, InformationClass, TrustedDomainInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(TrustedDomainInformation.value);
            if ((null != TrustedDomainInformation.value)) {
                encoder.WriteUnion(TrustedDomainInformation.value.value);
                encoder.WriteStructDeferral(TrustedDomainInformation.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarSetTrustedDomainInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_SID TrustedDomainSid;
            TRUSTED_INFORMATION_CLASS InformationClass;
            LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation;
            PolicyHandle = decoder.ReadContextHandle();
            TrustedDomainSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref TrustedDomainSid);
            InformationClass = ((TRUSTED_INFORMATION_CLASS)(decoder.ReadInt16()));
            TrustedDomainInformation = decoder.ReadUnion<LSAPR_TRUSTED_DOMAIN_INFO>();
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFO>(ref TrustedDomainInformation);
            var invokeTask = this._obj.LsarSetTrustedDomainInfo(PolicyHandle, TrustedDomainSid, InformationClass, TrustedDomainInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarDeleteTrustedDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_SID TrustedDomainSid;
            PolicyHandle = decoder.ReadContextHandle();
            TrustedDomainSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref TrustedDomainSid);
            var invokeTask = this._obj.LsarDeleteTrustedDomain(PolicyHandle, TrustedDomainSid, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarStorePrivateData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_UNICODE_STRING KeyName;
            RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedData;
            PolicyHandle = decoder.ReadContextHandle();
            KeyName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref KeyName);
            EncryptedData = decoder.ReadPointer<LSAPR_CR_CIPHER_VALUE>();
            if ((null != EncryptedData)) {
                EncryptedData.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedData.value);
            }
            var invokeTask = this._obj.LsarStorePrivateData(PolicyHandle, KeyName, EncryptedData, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarRetrievePrivateData(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_UNICODE_STRING KeyName;
            RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedData;
            PolicyHandle = decoder.ReadContextHandle();
            KeyName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref KeyName);
            EncryptedData = new RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>();
            EncryptedData.value = decoder.ReadPointer<LSAPR_CR_CIPHER_VALUE>();
            if ((null != EncryptedData.value)) {
                EncryptedData.value.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedData.value.value);
            }
            var invokeTask = this._obj.LsarRetrievePrivateData(PolicyHandle, KeyName, EncryptedData, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(EncryptedData.value);
            if ((null != EncryptedData.value)) {
                encoder.WriteFixedStruct(EncryptedData.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(EncryptedData.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarOpenPolicy2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string SystemName;
            LSAPR_OBJECT_ATTRIBUTES ObjectAttributes;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> PolicyHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            if ((decoder.ReadReferentId() == 0)) {
                SystemName = null;
            }
            else {
                SystemName = decoder.ReadWideCharString();
            }
            ObjectAttributes = decoder.ReadFixedStruct<LSAPR_OBJECT_ATTRIBUTES>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_OBJECT_ATTRIBUTES>(ref ObjectAttributes);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarOpenPolicy2(SystemName, ObjectAttributes, DesiredAccess, PolicyHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(PolicyHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarGetUserName(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string SystemName;
            RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> UserName;
            RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> DomainName;
            if ((decoder.ReadReferentId() == 0)) {
                SystemName = null;
            }
            else {
                SystemName = decoder.ReadWideCharString();
            }
            UserName = new RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
            UserName.value = decoder.ReadPointer<ms_dtyp.RPC_UNICODE_STRING>();
            if ((null != UserName.value)) {
                UserName.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref UserName.value.value);
            }
            DomainName = decoder.ReadPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
            if ((null != DomainName)) {
                DomainName.value = decoder.ReadPointer<ms_dtyp.RPC_UNICODE_STRING>();
                if ((null != DomainName.value)) {
                    DomainName.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref DomainName.value.value);
                }
            }
            var invokeTask = this._obj.LsarGetUserName(SystemName, UserName, DomainName, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(UserName.value);
            if ((null != UserName.value)) {
                encoder.WriteFixedStruct(UserName.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(UserName.value.value);
            }
            encoder.WritePointer(DomainName);
            if ((null != DomainName)) {
                encoder.WritePointer(DomainName.value);
                if ((null != DomainName.value)) {
                    encoder.WriteFixedStruct(DomainName.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(DomainName.value.value);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarQueryInformationPolicy2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            POLICY_INFORMATION_CLASS InformationClass;
            RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>> PolicyInformation = new RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>>();
            PolicyHandle = decoder.ReadContextHandle();
            InformationClass = ((POLICY_INFORMATION_CLASS)(decoder.ReadInt16()));
            var invokeTask = this._obj.LsarQueryInformationPolicy2(PolicyHandle, InformationClass, PolicyInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(PolicyInformation.value);
            if ((null != PolicyInformation.value)) {
                encoder.WriteUnion(PolicyInformation.value.value);
                encoder.WriteStructDeferral(PolicyInformation.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarSetInformationPolicy2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            POLICY_INFORMATION_CLASS InformationClass;
            LSAPR_POLICY_INFORMATION PolicyInformation;
            PolicyHandle = decoder.ReadContextHandle();
            InformationClass = ((POLICY_INFORMATION_CLASS)(decoder.ReadInt16()));
            PolicyInformation = decoder.ReadUnion<LSAPR_POLICY_INFORMATION>();
            decoder.ReadStructDeferral<LSAPR_POLICY_INFORMATION>(ref PolicyInformation);
            var invokeTask = this._obj.LsarSetInformationPolicy2(PolicyHandle, InformationClass, PolicyInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarQueryTrustedDomainInfoByName(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_UNICODE_STRING TrustedDomainName;
            TRUSTED_INFORMATION_CLASS InformationClass;
            RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation = new RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>>();
            PolicyHandle = decoder.ReadContextHandle();
            TrustedDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref TrustedDomainName);
            InformationClass = ((TRUSTED_INFORMATION_CLASS)(decoder.ReadInt16()));
            var invokeTask = this._obj.LsarQueryTrustedDomainInfoByName(PolicyHandle, TrustedDomainName, InformationClass, TrustedDomainInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(TrustedDomainInformation.value);
            if ((null != TrustedDomainInformation.value)) {
                encoder.WriteUnion(TrustedDomainInformation.value.value);
                encoder.WriteStructDeferral(TrustedDomainInformation.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarSetTrustedDomainInfoByName(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_UNICODE_STRING TrustedDomainName;
            TRUSTED_INFORMATION_CLASS InformationClass;
            LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation;
            PolicyHandle = decoder.ReadContextHandle();
            TrustedDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref TrustedDomainName);
            InformationClass = ((TRUSTED_INFORMATION_CLASS)(decoder.ReadInt16()));
            TrustedDomainInformation = decoder.ReadUnion<LSAPR_TRUSTED_DOMAIN_INFO>();
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFO>(ref TrustedDomainInformation);
            var invokeTask = this._obj.LsarSetTrustedDomainInfoByName(PolicyHandle, TrustedDomainName, InformationClass, TrustedDomainInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarEnumerateTrustedDomainsEx(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            RpcPointer<uint> EnumerationContext;
            RpcPointer<LSAPR_TRUSTED_ENUM_BUFFER_EX> EnumerationBuffer = new RpcPointer<LSAPR_TRUSTED_ENUM_BUFFER_EX>();
            uint PreferedMaximumLength;
            PolicyHandle = decoder.ReadContextHandle();
            EnumerationContext = new RpcPointer<uint>();
            EnumerationContext.value = decoder.ReadUInt32();
            PreferedMaximumLength = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarEnumerateTrustedDomainsEx(PolicyHandle, EnumerationContext, EnumerationBuffer, PreferedMaximumLength, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(EnumerationContext.value);
            encoder.WriteFixedStruct(EnumerationBuffer.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(EnumerationBuffer.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarCreateTrustedDomainEx(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInformation;
            LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION AuthenticationInformation;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            PolicyHandle = decoder.ReadContextHandle();
            TrustedDomainInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(ref TrustedDomainInformation);
            AuthenticationInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(ref AuthenticationInformation);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarCreateTrustedDomainEx(PolicyHandle, TrustedDomainInformation, AuthenticationInformation, DesiredAccess, TrustedDomainHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(TrustedDomainHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum52NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum52NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_LsarQueryDomainInformationPolicy(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            POLICY_DOMAIN_INFORMATION_CLASS InformationClass;
            RpcPointer<RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION>> PolicyDomainInformation = new RpcPointer<RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION>>();
            PolicyHandle = decoder.ReadContextHandle();
            InformationClass = ((POLICY_DOMAIN_INFORMATION_CLASS)(decoder.ReadInt16()));
            var invokeTask = this._obj.LsarQueryDomainInformationPolicy(PolicyHandle, InformationClass, PolicyDomainInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(PolicyDomainInformation.value);
            if ((null != PolicyDomainInformation.value)) {
                encoder.WriteUnion(PolicyDomainInformation.value.value);
                encoder.WriteStructDeferral(PolicyDomainInformation.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarSetDomainInformationPolicy(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            POLICY_DOMAIN_INFORMATION_CLASS InformationClass;
            RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION> PolicyDomainInformation;
            PolicyHandle = decoder.ReadContextHandle();
            InformationClass = ((POLICY_DOMAIN_INFORMATION_CLASS)(decoder.ReadInt16()));
            PolicyDomainInformation = decoder.ReadPointer<LSAPR_POLICY_DOMAIN_INFORMATION>();
            if ((null != PolicyDomainInformation)) {
                PolicyDomainInformation.value = decoder.ReadUnion<LSAPR_POLICY_DOMAIN_INFORMATION>();
                decoder.ReadStructDeferral<LSAPR_POLICY_DOMAIN_INFORMATION>(ref PolicyDomainInformation.value);
            }
            var invokeTask = this._obj.LsarSetDomainInformationPolicy(PolicyHandle, InformationClass, PolicyDomainInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarOpenTrustedDomainByName(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_UNICODE_STRING TrustedDomainName;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            PolicyHandle = decoder.ReadContextHandle();
            TrustedDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref TrustedDomainName);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarOpenTrustedDomainByName(PolicyHandle, TrustedDomainName, DesiredAccess, TrustedDomainHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(TrustedDomainHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum56NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum56NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_LsarLookupSids2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            LSAPR_SID_ENUM_BUFFER SidEnumBuffer;
            RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
            RpcPointer<LSAPR_TRANSLATED_NAMES_EX> TranslatedNames;
            LSAP_LOOKUP_LEVEL LookupLevel;
            RpcPointer<uint> MappedCount;
            uint LookupOptions;
            uint ClientRevision;
            PolicyHandle = decoder.ReadContextHandle();
            SidEnumBuffer = decoder.ReadFixedStruct<LSAPR_SID_ENUM_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_SID_ENUM_BUFFER>(ref SidEnumBuffer);
            TranslatedNames = new RpcPointer<LSAPR_TRANSLATED_NAMES_EX>();
            TranslatedNames.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAMES_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAMES_EX>(ref TranslatedNames.value);
            LookupLevel = ((LSAP_LOOKUP_LEVEL)(decoder.ReadInt16()));
            MappedCount = new RpcPointer<uint>();
            MappedCount.value = decoder.ReadUInt32();
            LookupOptions = decoder.ReadUInt32();
            ClientRevision = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarLookupSids2(PolicyHandle, SidEnumBuffer, ReferencedDomains, TranslatedNames, LookupLevel, MappedCount, LookupOptions, ClientRevision, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                encoder.WriteFixedStruct(ReferencedDomains.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ReferencedDomains.value.value);
            }
            encoder.WriteFixedStruct(TranslatedNames.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedNames.value);
            encoder.WriteValue(MappedCount.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarLookupNames2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            uint Count;
            ms_dtyp.RPC_UNICODE_STRING[] Names;
            RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
            RpcPointer<LSAPR_TRANSLATED_SIDS_EX> TranslatedSids;
            LSAP_LOOKUP_LEVEL LookupLevel;
            RpcPointer<uint> MappedCount;
            uint LookupOptions;
            uint ClientRevision;
            PolicyHandle = decoder.ReadContextHandle();
            Count = decoder.ReadUInt32();
            Names = decoder.ReadArrayHeader<ms_dtyp.RPC_UNICODE_STRING>();
            for (int i = 0; (i < Names.Length); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                Names[i] = elem_0;
            }
            for (int i = 0; (i < Names.Length); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
                Names[i] = elem_0;
            }
            TranslatedSids = new RpcPointer<LSAPR_TRANSLATED_SIDS_EX>();
            TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS_EX>(ref TranslatedSids.value);
            LookupLevel = ((LSAP_LOOKUP_LEVEL)(decoder.ReadInt16()));
            MappedCount = new RpcPointer<uint>();
            MappedCount.value = decoder.ReadUInt32();
            LookupOptions = decoder.ReadUInt32();
            ClientRevision = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarLookupNames2(PolicyHandle, Count, Names, ReferencedDomains, TranslatedSids, LookupLevel, MappedCount, LookupOptions, ClientRevision, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                encoder.WriteFixedStruct(ReferencedDomains.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ReferencedDomains.value.value);
            }
            encoder.WriteFixedStruct(TranslatedSids.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedSids.value);
            encoder.WriteValue(MappedCount.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarCreateTrustedDomainEx2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInformation;
            LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL AuthenticationInformation;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> TrustedDomainHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            PolicyHandle = decoder.ReadContextHandle();
            TrustedDomainInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(ref TrustedDomainInformation);
            AuthenticationInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL>(ref AuthenticationInformation);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarCreateTrustedDomainEx2(PolicyHandle, TrustedDomainInformation, AuthenticationInformation, DesiredAccess, TrustedDomainHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(TrustedDomainHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum60NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum60NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum61NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum61NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum62NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum62NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum63NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum63NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum64NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum64NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum65NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum65NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum66NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum66NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum67NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum67NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_LsarLookupNames3(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            uint Count;
            ms_dtyp.RPC_UNICODE_STRING[] Names;
            RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
            RpcPointer<LSAPR_TRANSLATED_SIDS_EX2> TranslatedSids;
            LSAP_LOOKUP_LEVEL LookupLevel;
            RpcPointer<uint> MappedCount;
            uint LookupOptions;
            uint ClientRevision;
            PolicyHandle = decoder.ReadContextHandle();
            Count = decoder.ReadUInt32();
            Names = decoder.ReadArrayHeader<ms_dtyp.RPC_UNICODE_STRING>();
            for (int i = 0; (i < Names.Length); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                Names[i] = elem_0;
            }
            for (int i = 0; (i < Names.Length); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
                Names[i] = elem_0;
            }
            TranslatedSids = new RpcPointer<LSAPR_TRANSLATED_SIDS_EX2>();
            TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS_EX2>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS_EX2>(ref TranslatedSids.value);
            LookupLevel = ((LSAP_LOOKUP_LEVEL)(decoder.ReadInt16()));
            MappedCount = new RpcPointer<uint>();
            MappedCount.value = decoder.ReadUInt32();
            LookupOptions = decoder.ReadUInt32();
            ClientRevision = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarLookupNames3(PolicyHandle, Count, Names, ReferencedDomains, TranslatedSids, LookupLevel, MappedCount, LookupOptions, ClientRevision, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                encoder.WriteFixedStruct(ReferencedDomains.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ReferencedDomains.value.value);
            }
            encoder.WriteFixedStruct(TranslatedSids.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedSids.value);
            encoder.WriteValue(MappedCount.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum69NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum69NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum70NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum70NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum71NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum71NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum72NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum72NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_LsarQueryForestTrustInformation(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_UNICODE_STRING TrustedDomainName;
            LSA_FOREST_TRUST_RECORD_TYPE HighestRecordType;
            RpcPointer<RpcPointer<LSA_FOREST_TRUST_INFORMATION>> ForestTrustInfo = new RpcPointer<RpcPointer<LSA_FOREST_TRUST_INFORMATION>>();
            PolicyHandle = decoder.ReadContextHandle();
            TrustedDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref TrustedDomainName);
            HighestRecordType = ((LSA_FOREST_TRUST_RECORD_TYPE)(decoder.ReadInt16()));
            var invokeTask = this._obj.LsarQueryForestTrustInformation(PolicyHandle, TrustedDomainName, HighestRecordType, ForestTrustInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ForestTrustInfo.value);
            if ((null != ForestTrustInfo.value)) {
                encoder.WriteFixedStruct(ForestTrustInfo.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ForestTrustInfo.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarSetForestTrustInformation(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle PolicyHandle;
            ms_dtyp.RPC_UNICODE_STRING TrustedDomainName;
            LSA_FOREST_TRUST_RECORD_TYPE HighestRecordType;
            LSA_FOREST_TRUST_INFORMATION ForestTrustInfo;
            byte CheckOnly;
            RpcPointer<RpcPointer<LSA_FOREST_TRUST_COLLISION_INFORMATION>> CollisionInfo = new RpcPointer<RpcPointer<LSA_FOREST_TRUST_COLLISION_INFORMATION>>();
            PolicyHandle = decoder.ReadContextHandle();
            TrustedDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref TrustedDomainName);
            HighestRecordType = ((LSA_FOREST_TRUST_RECORD_TYPE)(decoder.ReadInt16()));
            ForestTrustInfo = decoder.ReadFixedStruct<LSA_FOREST_TRUST_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSA_FOREST_TRUST_INFORMATION>(ref ForestTrustInfo);
            CheckOnly = decoder.ReadUnsignedChar();
            var invokeTask = this._obj.LsarSetForestTrustInformation(PolicyHandle, TrustedDomainName, HighestRecordType, ForestTrustInfo, CheckOnly, CollisionInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(CollisionInfo.value);
            if ((null != CollisionInfo.value)) {
                encoder.WriteFixedStruct(CollisionInfo.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(CollisionInfo.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum75NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum75NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_LsarLookupSids3(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            LSAPR_SID_ENUM_BUFFER SidEnumBuffer;
            RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
            RpcPointer<LSAPR_TRANSLATED_NAMES_EX> TranslatedNames;
            LSAP_LOOKUP_LEVEL LookupLevel;
            RpcPointer<uint> MappedCount;
            uint LookupOptions;
            uint ClientRevision;
            SidEnumBuffer = decoder.ReadFixedStruct<LSAPR_SID_ENUM_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_SID_ENUM_BUFFER>(ref SidEnumBuffer);
            TranslatedNames = new RpcPointer<LSAPR_TRANSLATED_NAMES_EX>();
            TranslatedNames.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAMES_EX>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAMES_EX>(ref TranslatedNames.value);
            LookupLevel = ((LSAP_LOOKUP_LEVEL)(decoder.ReadInt16()));
            MappedCount = new RpcPointer<uint>();
            MappedCount.value = decoder.ReadUInt32();
            LookupOptions = decoder.ReadUInt32();
            ClientRevision = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarLookupSids3(SidEnumBuffer, ReferencedDomains, TranslatedNames, LookupLevel, MappedCount, LookupOptions, ClientRevision, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                encoder.WriteFixedStruct(ReferencedDomains.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ReferencedDomains.value.value);
            }
            encoder.WriteFixedStruct(TranslatedNames.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedNames.value);
            encoder.WriteValue(MappedCount.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_LsarLookupNames4(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint Count;
            ms_dtyp.RPC_UNICODE_STRING[] Names;
            RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
            RpcPointer<LSAPR_TRANSLATED_SIDS_EX2> TranslatedSids;
            LSAP_LOOKUP_LEVEL LookupLevel;
            RpcPointer<uint> MappedCount;
            uint LookupOptions;
            uint ClientRevision;
            Count = decoder.ReadUInt32();
            Names = decoder.ReadArrayHeader<ms_dtyp.RPC_UNICODE_STRING>();
            for (int i = 0; (i < Names.Length); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                Names[i] = elem_0;
            }
            for (int i = 0; (i < Names.Length); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
                Names[i] = elem_0;
            }
            TranslatedSids = new RpcPointer<LSAPR_TRANSLATED_SIDS_EX2>();
            TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS_EX2>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS_EX2>(ref TranslatedSids.value);
            LookupLevel = ((LSAP_LOOKUP_LEVEL)(decoder.ReadInt16()));
            MappedCount = new RpcPointer<uint>();
            MappedCount.value = decoder.ReadUInt32();
            LookupOptions = decoder.ReadUInt32();
            ClientRevision = decoder.ReadUInt32();
            var invokeTask = this._obj.LsarLookupNames4(Count, Names, ReferencedDomains, TranslatedSids, LookupLevel, MappedCount, LookupOptions, ClientRevision, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ReferencedDomains.value);
            if ((null != ReferencedDomains.value)) {
                encoder.WriteFixedStruct(ReferencedDomains.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ReferencedDomains.value.value);
            }
            encoder.WriteFixedStruct(TranslatedSids.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(TranslatedSids.value);
            encoder.WriteValue(MappedCount.value);
            encoder.WriteValue(retval);
        }
    }
}
