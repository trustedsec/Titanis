namespace ms_lsar
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct STRING : IRpcFixedStruct
	{
		public ushort Length;
		public ushort MaximumLength;
		public RpcPointer<ArraySegment<byte>> Buffer;
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
			this.Buffer = decoder.ReadUniquePointer<ArraySegment<byte>>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value, true);
				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					byte elem_0 = this.Buffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					byte elem_0 = this.Buffer.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					this.Buffer.value.Item(i) = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_ACL : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.Dummy1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.Dummy1 = decoder.ReadArrayHeader<byte>();
		}

		public byte AclRevision;
		public byte Sbz1;
		public ushort AclSize;
		public byte[] Dummy1;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.Dummy1.Length; i++)
			{
				byte elem_0 = this.Dummy1[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.Dummy1.Length; i++)
			{
				byte elem_0 = this.Dummy1[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.Dummy1[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.AclRevision);
			encoder.WriteValue(this.Sbz1);
			encoder.WriteValue(this.AclSize);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.AclRevision = decoder.ReadUnsignedChar();
			this.Sbz1 = decoder.ReadUnsignedChar();
			this.AclSize = decoder.ReadUInt16();
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
	public partial struct LSAPR_SECURITY_DESCRIPTOR : IRpcFixedStruct
	{
		public byte Revision;
		public byte Sbz1;
		public ushort Control;
		public RpcPointer<ms_dtyp.RPC_SID> Owner;
		public RpcPointer<ms_dtyp.RPC_SID> Group;
		public RpcPointer<LSAPR_ACL> Sacl;
		public RpcPointer<LSAPR_ACL> Dacl;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Revision);
			encoder.WriteValue(this.Sbz1);
			encoder.WriteValue(this.Control);
			encoder.WriteUniquePointer(this.Owner);
			encoder.WriteUniquePointer(this.Group);
			encoder.WriteUniquePointer(this.Sacl);
			encoder.WriteUniquePointer(this.Dacl);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Revision = decoder.ReadUnsignedChar();
			this.Sbz1 = decoder.ReadUnsignedChar();
			this.Control = decoder.ReadUInt16();
			this.Owner = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			this.Group = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			this.Sacl = decoder.ReadUniquePointer<LSAPR_ACL>();
			this.Dacl = decoder.ReadUniquePointer<LSAPR_ACL>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Owner is not null)
			{
				encoder.WriteConformantStruct(this.Owner.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Owner.value);
			}

			if (this.Group is not null)
			{
				encoder.WriteConformantStruct(this.Group.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Group.value);
			}

			if (this.Sacl is not null)
			{
				encoder.WriteConformantStruct(this.Sacl.value, NdrAlignment._2Byte);
				encoder.WriteStructDeferral(this.Sacl.value);
			}

			if (this.Dacl is not null)
			{
				encoder.WriteConformantStruct(this.Dacl.value, NdrAlignment._2Byte);
				encoder.WriteStructDeferral(this.Dacl.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Owner is not null)
			{
				this.Owner.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Owner.value);
			}

			if (this.Group is not null)
			{
				this.Group.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Group.value);
			}

			if (this.Sacl is not null)
			{
				this.Sacl.value = decoder.ReadConformantStruct<LSAPR_ACL>(NdrAlignment._2Byte);
				decoder.ReadStructDeferral<LSAPR_ACL>(ref this.Sacl.value);
			}

			if (this.Dacl is not null)
			{
				this.Dacl.value = decoder.ReadConformantStruct<LSAPR_ACL>(NdrAlignment._2Byte);
				decoder.ReadStructDeferral<LSAPR_ACL>(ref this.Dacl.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum SECURITY_IMPERSONATION_LEVEL : int
	{
		SecurityAnonymous = 0,
		SecurityIdentification = 1,
		SecurityImpersonation = 2,
		SecurityDelegation = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SECURITY_QUALITY_OF_SERVICE : IRpcFixedStruct
	{
		public uint Length;
		public SECURITY_IMPERSONATION_LEVEL ImpersonationLevel;
		public byte ContextTrackingMode;
		public byte EffectiveOnly;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteEnumShortValue((short)this.ImpersonationLevel);
			encoder.WriteValue(this.ContextTrackingMode);
			encoder.WriteValue(this.EffectiveOnly);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt32();
			this.ImpersonationLevel = (SECURITY_IMPERSONATION_LEVEL)decoder.ReadEnumShortValue();
			this.ContextTrackingMode = decoder.ReadUnsignedChar();
			this.EffectiveOnly = decoder.ReadUnsignedChar();
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
	public partial struct LSAPR_OBJECT_ATTRIBUTES : IRpcFixedStruct
	{
		public uint Length;
		public RpcPointer<byte> RootDirectory;
		public RpcPointer<STRING> ObjectName;
		public uint Attributes;
		public RpcPointer<LSAPR_SECURITY_DESCRIPTOR> SecurityDescriptor;
		public RpcPointer<SECURITY_QUALITY_OF_SERVICE> SecurityQualityOfService;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteUniquePointer(this.RootDirectory);
			encoder.WriteUniquePointer(this.ObjectName);
			encoder.WriteValue(this.Attributes);
			encoder.WriteUniquePointer(this.SecurityDescriptor);
			encoder.WriteUniquePointer(this.SecurityQualityOfService);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt32();
			this.RootDirectory = decoder.ReadUniquePointer<byte>();
			this.ObjectName = decoder.ReadUniquePointer<STRING>();
			this.Attributes = decoder.ReadUInt32();
			this.SecurityDescriptor = decoder.ReadUniquePointer<LSAPR_SECURITY_DESCRIPTOR>();
			this.SecurityQualityOfService = decoder.ReadUniquePointer<SECURITY_QUALITY_OF_SERVICE>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.RootDirectory is not null)
			{
				encoder.WriteValue(this.RootDirectory.value);
			}

			if (this.ObjectName is not null)
			{
				encoder.WriteFixedStruct(this.ObjectName.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.ObjectName.value);
			}

			if (this.SecurityDescriptor is not null)
			{
				encoder.WriteFixedStruct(this.SecurityDescriptor.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.SecurityDescriptor.value);
			}

			if (this.SecurityQualityOfService is not null)
			{
				encoder.WriteFixedStruct(this.SecurityQualityOfService.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.SecurityQualityOfService.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.RootDirectory is not null)
			{
				this.RootDirectory.value = decoder.ReadUnsignedChar();
			}

			if (this.ObjectName is not null)
			{
				this.ObjectName.value = decoder.ReadFixedStruct<STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<STRING>(ref this.ObjectName.value);
			}

			if (this.SecurityDescriptor is not null)
			{
				this.SecurityDescriptor.value = decoder.ReadFixedStruct<LSAPR_SECURITY_DESCRIPTOR>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_SECURITY_DESCRIPTOR>(ref this.SecurityDescriptor.value);
			}

			if (this.SecurityQualityOfService is not null)
			{
				this.SecurityQualityOfService.value = decoder.ReadFixedStruct<SECURITY_QUALITY_OF_SERVICE>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<SECURITY_QUALITY_OF_SERVICE>(ref this.SecurityQualityOfService.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUST_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Name;
		public RpcPointer<ms_dtyp.RPC_SID> Sid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.Sid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.Sid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
			if (this.Sid is not null)
			{
				encoder.WriteConformantStruct(this.Sid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Sid.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
			if (this.Sid is not null)
			{
				this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum POLICY_INFORMATION_CLASS : int
	{
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
		PolicyLastEntry = 16
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum POLICY_AUDIT_EVENT_TYPE : int
	{
		AuditCategorySystem = 0,
		AuditCategoryLogon = 1,
		AuditCategoryObjectAccess = 2,
		AuditCategoryPrivilegeUse = 3,
		AuditCategoryDetailedTracking = 4,
		AuditCategoryPolicyChange = 5,
		AuditCategoryAccountManagement = 6,
		AuditCategoryDirectoryServiceAccess = 7,
		AuditCategoryAccountLogon = 8
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct POLICY_AUDIT_LOG_INFO : IRpcFixedStruct
	{
		public uint AuditLogPercentFull;
		public uint MaximumLogSize;
		public ms_dtyp.LARGE_INTEGER AuditRetentionPeriod;
		public byte AuditLogFullShutdownInProgress;
		public ms_dtyp.LARGE_INTEGER TimeToShutdown;
		public uint NextAuditRecordId;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.AuditLogPercentFull);
			encoder.WriteValue(this.MaximumLogSize);
			encoder.WriteFixedStruct(this.AuditRetentionPeriod, NdrAlignment._8Byte);
			encoder.WriteValue(this.AuditLogFullShutdownInProgress);
			encoder.WriteFixedStruct(this.TimeToShutdown, NdrAlignment._8Byte);
			encoder.WriteValue(this.NextAuditRecordId);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.AuditLogPercentFull = decoder.ReadUInt32();
			this.MaximumLogSize = decoder.ReadUInt32();
			this.AuditRetentionPeriod = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.AuditLogFullShutdownInProgress = decoder.ReadUnsignedChar();
			this.TimeToShutdown = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.NextAuditRecordId = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.AuditRetentionPeriod);
			encoder.WriteStructDeferral(this.TimeToShutdown);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.AuditRetentionPeriod);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.TimeToShutdown);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum POLICY_LSA_SERVER_ROLE : int
	{
		PolicyServerRoleBackup = 2,
		PolicyServerRolePrimary = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct POLICY_LSA_SERVER_ROLE_INFO : IRpcFixedStruct
	{
		public POLICY_LSA_SERVER_ROLE LsaServerRole;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.LsaServerRole);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LsaServerRole = (POLICY_LSA_SERVER_ROLE)decoder.ReadEnumShortValue();
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
	public partial struct POLICY_MODIFICATION_INFO : IRpcFixedStruct
	{
		public ms_dtyp.LARGE_INTEGER ModifiedId;
		public ms_dtyp.LARGE_INTEGER DatabaseCreationTime;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ModifiedId, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.DatabaseCreationTime, NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ModifiedId = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.DatabaseCreationTime = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ModifiedId);
			encoder.WriteStructDeferral(this.DatabaseCreationTime);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.ModifiedId);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.DatabaseCreationTime);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct POLICY_AUDIT_FULL_SET_INFO : IRpcFixedStruct
	{
		public byte ShutDownOnFull;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ShutDownOnFull);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ShutDownOnFull = decoder.ReadUnsignedChar();
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
	public partial struct POLICY_AUDIT_FULL_QUERY_INFO : IRpcFixedStruct
	{
		public byte ShutDownOnFull;
		public byte LogIsFull;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ShutDownOnFull);
			encoder.WriteValue(this.LogIsFull);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ShutDownOnFull = decoder.ReadUnsignedChar();
			this.LogIsFull = decoder.ReadUnsignedChar();
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
	public enum POLICY_DOMAIN_INFORMATION_CLASS : int
	{
		PolicyDomainQualityOfServiceInformation = 1,
		PolicyDomainEfsInformation = 2,
		PolicyDomainKerberosTicketInformation = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct POLICY_DOMAIN_KERBEROS_TICKET_INFO : IRpcFixedStruct
	{
		public uint AuthenticationOptions;
		public ms_dtyp.LARGE_INTEGER MaxServiceTicketAge;
		public ms_dtyp.LARGE_INTEGER MaxTicketAge;
		public ms_dtyp.LARGE_INTEGER MaxRenewAge;
		public ms_dtyp.LARGE_INTEGER MaxClockSkew;
		public ms_dtyp.LARGE_INTEGER Reserved;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.AuthenticationOptions);
			encoder.WriteFixedStruct(this.MaxServiceTicketAge, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.MaxTicketAge, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.MaxRenewAge, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.MaxClockSkew, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.Reserved, NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.AuthenticationOptions = decoder.ReadUInt32();
			this.MaxServiceTicketAge = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.MaxTicketAge = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.MaxRenewAge = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.MaxClockSkew = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.Reserved = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.MaxServiceTicketAge);
			encoder.WriteStructDeferral(this.MaxTicketAge);
			encoder.WriteStructDeferral(this.MaxRenewAge);
			encoder.WriteStructDeferral(this.MaxClockSkew);
			encoder.WriteStructDeferral(this.Reserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.MaxServiceTicketAge);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.MaxTicketAge);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.MaxRenewAge);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.MaxClockSkew);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.Reserved);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct TRUSTED_POSIX_OFFSET_INFO : IRpcFixedStruct
	{
		public uint Offset;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Offset);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Offset = decoder.ReadUInt32();
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
	public enum TRUSTED_INFORMATION_CLASS : int
	{
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
		TrustedDomainSupportedEncryptionTypes = 13
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum LSA_FOREST_TRUST_RECORD_TYPE : int
	{
		ForestTrustTopLevelName = 0,
		ForestTrustTopLevelNameEx = 1,
		ForestTrustDomainInfo = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSA_FOREST_TRUST_BINARY_DATA : IRpcFixedStruct
	{
		public uint Length;
		public RpcPointer<byte[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt32();
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
	public partial struct LSA_FOREST_TRUST_DOMAIN_INFO : IRpcFixedStruct
	{
		public RpcPointer<ms_dtyp.RPC_SID> Sid;
		public ms_dtyp.RPC_UNICODE_STRING DnsName;
		public ms_dtyp.RPC_UNICODE_STRING NetbiosName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.Sid);
			encoder.WriteFixedStruct(this.DnsName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.NetbiosName, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Sid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			this.DnsName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.NetbiosName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Sid is not null)
			{
				encoder.WriteConformantStruct(this.Sid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Sid.value);
			}

			encoder.WriteStructDeferral(this.DnsName);
			encoder.WriteStructDeferral(this.NetbiosName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Sid is not null)
			{
				this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
			}

			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.DnsName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.NetbiosName);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct Unnamed_1 : IRpcFixedStruct
	{
		public LSA_FOREST_TRUST_RECORD_TYPE ForestTrustType;
		public ms_dtyp.RPC_UNICODE_STRING TopLevelName;
		public LSA_FOREST_TRUST_DOMAIN_INFO DomainInfo;
		public LSA_FOREST_TRUST_BINARY_DATA Data;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.ForestTrustType);
			switch ((int)this.ForestTrustType)
			{
				case 0:
				case 1:
					encoder.WriteFixedStruct(this.TopLevelName, NdrAlignment.NativePtr);
					break;
				case 2:
					encoder.WriteFixedStruct(this.DomainInfo, NdrAlignment.NativePtr);
					break;
				default:
					encoder.WriteFixedStruct(this.Data, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.ForestTrustType = (LSA_FOREST_TRUST_RECORD_TYPE)decoder.ReadEnumShortValue();
			switch ((int)this.ForestTrustType)
			{
				case 0:
				case 1:
					this.TopLevelName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
					break;
				case 2:
					this.DomainInfo = decoder.ReadFixedStruct<LSA_FOREST_TRUST_DOMAIN_INFO>(NdrAlignment.NativePtr);
					break;
				default:
					this.Data = decoder.ReadFixedStruct<LSA_FOREST_TRUST_BINARY_DATA>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.ForestTrustType)
			{
				case 0:
				case 1:
					encoder.WriteStructDeferral(this.TopLevelName);
					break;
				case 2:
					encoder.WriteStructDeferral(this.DomainInfo);
					break;
				default:
					encoder.WriteStructDeferral(this.Data);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.ForestTrustType)
			{
				case 0:
				case 1:
					decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.TopLevelName);
					break;
				case 2:
					decoder.ReadStructDeferral<LSA_FOREST_TRUST_DOMAIN_INFO>(ref this.DomainInfo);
					break;
				default:
					decoder.ReadStructDeferral<LSA_FOREST_TRUST_BINARY_DATA>(ref this.Data);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSA_FOREST_TRUST_RECORD : IRpcFixedStruct
	{
		public uint Flags;
		public LSA_FOREST_TRUST_RECORD_TYPE ForestTrustType;
		public ms_dtyp.LARGE_INTEGER Time;
		public Unnamed_1 ForestTrustData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Flags);
			encoder.WriteEnumShortValue((short)this.ForestTrustType);
			encoder.WriteFixedStruct(this.Time, NdrAlignment._8Byte);
			encoder.WriteUnion(this.ForestTrustData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Flags = decoder.ReadUInt32();
			this.ForestTrustType = (LSA_FOREST_TRUST_RECORD_TYPE)decoder.ReadEnumShortValue();
			this.Time = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.ForestTrustData = decoder.ReadUnion<Unnamed_1>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Time);
			encoder.WriteStructDeferral(this.ForestTrustData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.Time);
			decoder.ReadStructDeferral<Unnamed_1>(ref this.ForestTrustData);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSA_FOREST_TRUST_INFORMATION : IRpcFixedStruct
	{
		public uint RecordCount;
		public RpcPointer<RpcPointer<LSA_FOREST_TRUST_RECORD>[]> Entries;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.RecordCount);
			encoder.WriteUniquePointer(this.Entries);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.RecordCount = decoder.ReadUInt32();
			this.Entries = decoder.ReadUniquePointer<RpcPointer<LSA_FOREST_TRUST_RECORD>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Entries is not null)
			{
				encoder.WriteArrayHeader(this.Entries.value);
				for (int i = 0; i < this.Entries.value.Length; i++)
				{
					RpcPointer<LSA_FOREST_TRUST_RECORD> elem_0 = this.Entries.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.Entries.value.Length; i++)
				{
					RpcPointer<LSA_FOREST_TRUST_RECORD> elem_0 = this.Entries.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteFixedStruct(elem_0.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Entries is not null)
			{
				this.Entries.value = decoder.ReadArrayHeader<RpcPointer<LSA_FOREST_TRUST_RECORD>>();
				for (int i = 0; i < this.Entries.value.Length; i++)
				{
					RpcPointer<LSA_FOREST_TRUST_RECORD> elem_0 = this.Entries.value[i];
					elem_0 = decoder.ReadUniquePointer<LSA_FOREST_TRUST_RECORD>();
					this.Entries.value[i] = elem_0;
				}

				for (int i = 0; i < this.Entries.value.Length; i++)
				{
					RpcPointer<LSA_FOREST_TRUST_RECORD> elem_0 = this.Entries.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadFixedStruct<LSA_FOREST_TRUST_RECORD>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<LSA_FOREST_TRUST_RECORD>(ref elem_0.value);
					}

					this.Entries.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum LSA_FOREST_TRUST_COLLISION_RECORD_TYPE : int
	{
		CollisionTdo = 0,
		CollisionXref = 1,
		CollisionOther = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSA_FOREST_TRUST_COLLISION_RECORD : IRpcFixedStruct
	{
		public uint Index;
		public LSA_FOREST_TRUST_COLLISION_RECORD_TYPE Type;
		public uint Flags;
		public ms_dtyp.RPC_UNICODE_STRING Name;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Index);
			encoder.WriteEnumShortValue((short)this.Type);
			encoder.WriteValue(this.Flags);
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Index = decoder.ReadUInt32();
			this.Type = (LSA_FOREST_TRUST_COLLISION_RECORD_TYPE)decoder.ReadEnumShortValue();
			this.Flags = decoder.ReadUInt32();
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSA_FOREST_TRUST_COLLISION_INFORMATION : IRpcFixedStruct
	{
		public uint RecordCount;
		public RpcPointer<RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD>[]> Entries;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.RecordCount);
			encoder.WriteUniquePointer(this.Entries);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.RecordCount = decoder.ReadUInt32();
			this.Entries = decoder.ReadUniquePointer<RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD>[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Entries is not null)
			{
				encoder.WriteArrayHeader(this.Entries.value);
				for (int i = 0; i < this.Entries.value.Length; i++)
				{
					RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD> elem_0 = this.Entries.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < this.Entries.value.Length; i++)
				{
					RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD> elem_0 = this.Entries.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteFixedStruct(elem_0.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Entries is not null)
			{
				this.Entries.value = decoder.ReadArrayHeader<RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD>>();
				for (int i = 0; i < this.Entries.value.Length; i++)
				{
					RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD> elem_0 = this.Entries.value[i];
					elem_0 = decoder.ReadUniquePointer<LSA_FOREST_TRUST_COLLISION_RECORD>();
					this.Entries.value[i] = elem_0;
				}

				for (int i = 0; i < this.Entries.value.Length; i++)
				{
					RpcPointer<LSA_FOREST_TRUST_COLLISION_RECORD> elem_0 = this.Entries.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadFixedStruct<LSA_FOREST_TRUST_COLLISION_RECORD>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<LSA_FOREST_TRUST_COLLISION_RECORD>(ref elem_0.value);
					}

					this.Entries.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_ACCOUNT_INFORMATION : IRpcFixedStruct
	{
		public RpcPointer<ms_dtyp.RPC_SID> Sid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.Sid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Sid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Sid is not null)
			{
				encoder.WriteConformantStruct(this.Sid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Sid.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Sid is not null)
			{
				this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_ACCOUNT_ENUM_BUFFER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<LSAPR_ACCOUNT_INFORMATION[]> Information;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Information);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Information = decoder.ReadUniquePointer<LSAPR_ACCOUNT_INFORMATION[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Information is not null)
			{
				encoder.WriteArrayHeader(this.Information.value);
				for (int i = 0; i < this.Information.value.Length; i++)
				{
					LSAPR_ACCOUNT_INFORMATION elem_0 = this.Information.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Information.value.Length; i++)
				{
					LSAPR_ACCOUNT_INFORMATION elem_0 = this.Information.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Information is not null)
			{
				this.Information.value = decoder.ReadArrayHeader<LSAPR_ACCOUNT_INFORMATION>();
				for (int i = 0; i < this.Information.value.Length; i++)
				{
					LSAPR_ACCOUNT_INFORMATION elem_0 = this.Information.value[i];
					elem_0 = decoder.ReadFixedStruct<LSAPR_ACCOUNT_INFORMATION>(NdrAlignment.NativePtr);
					this.Information.value[i] = elem_0;
				}

				for (int i = 0; i < this.Information.value.Length; i++)
				{
					LSAPR_ACCOUNT_INFORMATION elem_0 = this.Information.value[i];
					decoder.ReadStructDeferral<LSAPR_ACCOUNT_INFORMATION>(ref elem_0);
					this.Information.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_SR_SECURITY_DESCRIPTOR : IRpcFixedStruct
	{
		public uint Length;
		public RpcPointer<byte[]> SecurityDescriptor;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteUniquePointer(this.SecurityDescriptor);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt32();
			this.SecurityDescriptor = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.SecurityDescriptor is not null)
			{
				encoder.WriteArrayHeader(this.SecurityDescriptor.value);
				for (int i = 0; i < this.SecurityDescriptor.value.Length; i++)
				{
					byte elem_0 = this.SecurityDescriptor.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.SecurityDescriptor is not null)
			{
				this.SecurityDescriptor.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.SecurityDescriptor.value.Length; i++)
				{
					byte elem_0 = this.SecurityDescriptor.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.SecurityDescriptor.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_LUID_AND_ATTRIBUTES : IRpcFixedStruct
	{
		public ms_dtyp.LUID Luid;
		public uint Attributes;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Luid, NdrAlignment._4Byte);
			encoder.WriteValue(this.Attributes);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Luid = decoder.ReadFixedStruct<ms_dtyp.LUID>(NdrAlignment._4Byte);
			this.Attributes = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Luid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.LUID>(ref this.Luid);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_PRIVILEGE_SET : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.Privilege);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.Privilege = decoder.ReadArrayHeader<LSAPR_LUID_AND_ATTRIBUTES>();
		}

		public uint PrivilegeCount;
		public uint Control;
		public LSAPR_LUID_AND_ATTRIBUTES[] Privilege;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.Privilege.Length; i++)
			{
				LSAPR_LUID_AND_ATTRIBUTES elem_0 = this.Privilege[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.Privilege.Length; i++)
			{
				LSAPR_LUID_AND_ATTRIBUTES elem_0 = this.Privilege[i];
				elem_0 = decoder.ReadFixedStruct<LSAPR_LUID_AND_ATTRIBUTES>(NdrAlignment._4Byte);
				this.Privilege[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.PrivilegeCount);
			encoder.WriteValue(this.Control);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.PrivilegeCount = decoder.ReadUInt32();
			this.Control = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.Privilege.Length; i++)
			{
				LSAPR_LUID_AND_ATTRIBUTES elem_0 = this.Privilege[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.Privilege.Length; i++)
			{
				LSAPR_LUID_AND_ATTRIBUTES elem_0 = this.Privilege[i];
				decoder.ReadStructDeferral<LSAPR_LUID_AND_ATTRIBUTES>(ref elem_0);
				this.Privilege[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_POLICY_PRIVILEGE_DEF : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Name;
		public ms_dtyp.LUID LocalValue;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.LocalValue, NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.LocalValue = decoder.ReadFixedStruct<ms_dtyp.LUID>(NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
			encoder.WriteStructDeferral(this.LocalValue);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
			decoder.ReadStructDeferral<ms_dtyp.LUID>(ref this.LocalValue);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_PRIVILEGE_ENUM_BUFFER : IRpcFixedStruct
	{
		public uint Entries;
		public RpcPointer<LSAPR_POLICY_PRIVILEGE_DEF[]> Privileges;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Entries);
			encoder.WriteUniquePointer(this.Privileges);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Entries = decoder.ReadUInt32();
			this.Privileges = decoder.ReadUniquePointer<LSAPR_POLICY_PRIVILEGE_DEF[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Privileges is not null)
			{
				encoder.WriteArrayHeader(this.Privileges.value);
				for (int i = 0; i < this.Privileges.value.Length; i++)
				{
					LSAPR_POLICY_PRIVILEGE_DEF elem_0 = this.Privileges.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Privileges.value.Length; i++)
				{
					LSAPR_POLICY_PRIVILEGE_DEF elem_0 = this.Privileges.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Privileges is not null)
			{
				this.Privileges.value = decoder.ReadArrayHeader<LSAPR_POLICY_PRIVILEGE_DEF>();
				for (int i = 0; i < this.Privileges.value.Length; i++)
				{
					LSAPR_POLICY_PRIVILEGE_DEF elem_0 = this.Privileges.value[i];
					elem_0 = decoder.ReadFixedStruct<LSAPR_POLICY_PRIVILEGE_DEF>(NdrAlignment.NativePtr);
					this.Privileges.value[i] = elem_0;
				}

				for (int i = 0; i < this.Privileges.value.Length; i++)
				{
					LSAPR_POLICY_PRIVILEGE_DEF elem_0 = this.Privileges.value[i];
					decoder.ReadStructDeferral<LSAPR_POLICY_PRIVILEGE_DEF>(ref elem_0);
					this.Privileges.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_CR_CIPHER_VALUE : IRpcFixedStruct
	{
		public uint Length;
		public uint MaximumLength;
		public RpcPointer<ArraySegment<byte>> Buffer;
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
			this.Length = decoder.ReadUInt32();
			this.MaximumLength = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<ArraySegment<byte>>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value, true);
				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					byte elem_0 = this.Buffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					byte elem_0 = this.Buffer.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					this.Buffer.value.Item(i) = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_ENUM_BUFFER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<LSAPR_TRUST_INFORMATION[]> Information;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Information);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Information = decoder.ReadUniquePointer<LSAPR_TRUST_INFORMATION[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Information is not null)
			{
				encoder.WriteArrayHeader(this.Information.value);
				for (int i = 0; i < this.Information.value.Length; i++)
				{
					LSAPR_TRUST_INFORMATION elem_0 = this.Information.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Information.value.Length; i++)
				{
					LSAPR_TRUST_INFORMATION elem_0 = this.Information.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Information is not null)
			{
				this.Information.value = decoder.ReadArrayHeader<LSAPR_TRUST_INFORMATION>();
				for (int i = 0; i < this.Information.value.Length; i++)
				{
					LSAPR_TRUST_INFORMATION elem_0 = this.Information.value[i];
					elem_0 = decoder.ReadFixedStruct<LSAPR_TRUST_INFORMATION>(NdrAlignment.NativePtr);
					this.Information.value[i] = elem_0;
				}

				for (int i = 0; i < this.Information.value.Length; i++)
				{
					LSAPR_TRUST_INFORMATION elem_0 = this.Information.value[i];
					decoder.ReadStructDeferral<LSAPR_TRUST_INFORMATION>(ref elem_0);
					this.Information.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_POLICY_ACCOUNT_DOM_INFO : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING DomainName;
		public RpcPointer<ms_dtyp.RPC_SID> DomainSid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.DomainName, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.DomainSid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.DomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.DomainSid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.DomainName);
			if (this.DomainSid is not null)
			{
				encoder.WriteConformantStruct(this.DomainSid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.DomainSid.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.DomainName);
			if (this.DomainSid is not null)
			{
				this.DomainSid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.DomainSid.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_POLICY_PRIMARY_DOM_INFO : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Name;
		public RpcPointer<ms_dtyp.RPC_SID> Sid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.Sid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.Sid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
			if (this.Sid is not null)
			{
				encoder.WriteConformantStruct(this.Sid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Sid.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
			if (this.Sid is not null)
			{
				this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_POLICY_DNS_DOMAIN_INFO : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Name;
		public ms_dtyp.RPC_UNICODE_STRING DnsDomainName;
		public ms_dtyp.RPC_UNICODE_STRING DnsForestName;
		public Guid DomainGuid;
		public RpcPointer<ms_dtyp.RPC_SID> Sid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.DnsDomainName, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.DnsForestName, NdrAlignment.NativePtr);
			encoder.WriteValue(this.DomainGuid);
			encoder.WriteUniquePointer(this.Sid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.DnsDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.DnsForestName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.DomainGuid = decoder.ReadUuid();
			this.Sid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
			encoder.WriteStructDeferral(this.DnsDomainName);
			encoder.WriteStructDeferral(this.DnsForestName);
			if (this.Sid is not null)
			{
				encoder.WriteConformantStruct(this.Sid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Sid.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.DnsDomainName);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.DnsForestName);
			if (this.Sid is not null)
			{
				this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_POLICY_PD_ACCOUNT_INFO : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Name;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_POLICY_REPLICA_SRCE_INFO : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING ReplicaSource;
		public ms_dtyp.RPC_UNICODE_STRING ReplicaAccountName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ReplicaSource, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.ReplicaAccountName, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ReplicaSource = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.ReplicaAccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ReplicaSource);
			encoder.WriteStructDeferral(this.ReplicaAccountName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ReplicaSource);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ReplicaAccountName);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_POLICY_AUDIT_EVENTS_INFO : IRpcFixedStruct
	{
		public byte AuditingMode;
		public RpcPointer<uint[]> EventAuditingOptions;
		public uint MaximumAuditEventCount;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.AuditingMode);
			encoder.WriteUniquePointer(this.EventAuditingOptions);
			encoder.WriteValue(this.MaximumAuditEventCount);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.AuditingMode = decoder.ReadUnsignedChar();
			this.EventAuditingOptions = decoder.ReadUniquePointer<uint[]>();
			this.MaximumAuditEventCount = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.EventAuditingOptions is not null)
			{
				encoder.WriteArrayHeader(this.EventAuditingOptions.value);
				for (int i = 0; i < this.EventAuditingOptions.value.Length; i++)
				{
					uint elem_0 = this.EventAuditingOptions.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.EventAuditingOptions is not null)
			{
				this.EventAuditingOptions.value = decoder.ReadArrayHeader<uint>();
				for (int i = 0; i < this.EventAuditingOptions.value.Length; i++)
				{
					uint elem_0 = this.EventAuditingOptions.value[i];
					elem_0 = decoder.ReadUInt32();
					this.EventAuditingOptions.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_POLICY_MACHINE_ACCT_INFO : IRpcFixedStruct
	{
		public uint Rid;
		public RpcPointer<ms_dtyp.RPC_SID> Sid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Rid);
			encoder.WriteUniquePointer(this.Sid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Rid = decoder.ReadUInt32();
			this.Sid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Sid is not null)
			{
				encoder.WriteConformantStruct(this.Sid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Sid.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Sid is not null)
			{
				this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_POLICY_INFORMATION : IRpcFixedStruct
	{
		public POLICY_INFORMATION_CLASS unionSwitch;
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
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._8Byte);
			encoder.WriteEnumShortValue((short)this.unionSwitch);
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteFixedStruct(this.PolicyAuditLogInfo, NdrAlignment._8Byte);
					break;
				case 2:
					encoder.WriteFixedStruct(this.PolicyAuditEventsInfo, NdrAlignment.NativePtr);
					break;
				case 3:
					encoder.WriteFixedStruct(this.PolicyPrimaryDomainInfo, NdrAlignment.NativePtr);
					break;
				case 5:
					encoder.WriteFixedStruct(this.PolicyAccountDomainInfo, NdrAlignment.NativePtr);
					break;
				case 4:
					encoder.WriteFixedStruct(this.PolicyPdAccountInfo, NdrAlignment.NativePtr);
					break;
				case 6:
					encoder.WriteFixedStruct(this.PolicyServerRoleInfo, NdrAlignment.ShortEnum);
					break;
				case 7:
					encoder.WriteFixedStruct(this.PolicyReplicaSourceInfo, NdrAlignment.NativePtr);
					break;
				case 9:
					encoder.WriteFixedStruct(this.PolicyModificationInfo, NdrAlignment._8Byte);
					break;
				case 10:
					encoder.WriteFixedStruct(this.PolicyAuditFullSetInfo, NdrAlignment._1Byte);
					break;
				case 11:
					encoder.WriteFixedStruct(this.PolicyAuditFullQueryInfo, NdrAlignment._1Byte);
					break;
				case 12:
					encoder.WriteFixedStruct(this.PolicyDnsDomainInfo, NdrAlignment.NativePtr);
					break;
				case 13:
					encoder.WriteFixedStruct(this.PolicyDnsDomainInfoInt, NdrAlignment.NativePtr);
					break;
				case 14:
					encoder.WriteFixedStruct(this.PolicyLocalAccountDomainInfo, NdrAlignment.NativePtr);
					break;
				case 15:
					encoder.WriteFixedStruct(this.PolicyMachineAccountInfo, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._8Byte);
			this.unionSwitch = (POLICY_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			switch ((int)this.unionSwitch)
			{
				case 1:
					this.PolicyAuditLogInfo = decoder.ReadFixedStruct<POLICY_AUDIT_LOG_INFO>(NdrAlignment._8Byte);
					break;
				case 2:
					this.PolicyAuditEventsInfo = decoder.ReadFixedStruct<LSAPR_POLICY_AUDIT_EVENTS_INFO>(NdrAlignment.NativePtr);
					break;
				case 3:
					this.PolicyPrimaryDomainInfo = decoder.ReadFixedStruct<LSAPR_POLICY_PRIMARY_DOM_INFO>(NdrAlignment.NativePtr);
					break;
				case 5:
					this.PolicyAccountDomainInfo = decoder.ReadFixedStruct<LSAPR_POLICY_ACCOUNT_DOM_INFO>(NdrAlignment.NativePtr);
					break;
				case 4:
					this.PolicyPdAccountInfo = decoder.ReadFixedStruct<LSAPR_POLICY_PD_ACCOUNT_INFO>(NdrAlignment.NativePtr);
					break;
				case 6:
					this.PolicyServerRoleInfo = decoder.ReadFixedStruct<POLICY_LSA_SERVER_ROLE_INFO>(NdrAlignment.ShortEnum);
					break;
				case 7:
					this.PolicyReplicaSourceInfo = decoder.ReadFixedStruct<LSAPR_POLICY_REPLICA_SRCE_INFO>(NdrAlignment.NativePtr);
					break;
				case 9:
					this.PolicyModificationInfo = decoder.ReadFixedStruct<POLICY_MODIFICATION_INFO>(NdrAlignment._8Byte);
					break;
				case 10:
					this.PolicyAuditFullSetInfo = decoder.ReadFixedStruct<POLICY_AUDIT_FULL_SET_INFO>(NdrAlignment._1Byte);
					break;
				case 11:
					this.PolicyAuditFullQueryInfo = decoder.ReadFixedStruct<POLICY_AUDIT_FULL_QUERY_INFO>(NdrAlignment._1Byte);
					break;
				case 12:
					this.PolicyDnsDomainInfo = decoder.ReadFixedStruct<LSAPR_POLICY_DNS_DOMAIN_INFO>(NdrAlignment.NativePtr);
					break;
				case 13:
					this.PolicyDnsDomainInfoInt = decoder.ReadFixedStruct<LSAPR_POLICY_DNS_DOMAIN_INFO>(NdrAlignment.NativePtr);
					break;
				case 14:
					this.PolicyLocalAccountDomainInfo = decoder.ReadFixedStruct<LSAPR_POLICY_ACCOUNT_DOM_INFO>(NdrAlignment.NativePtr);
					break;
				case 15:
					this.PolicyMachineAccountInfo = decoder.ReadFixedStruct<LSAPR_POLICY_MACHINE_ACCT_INFO>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteStructDeferral(this.PolicyAuditLogInfo);
					break;
				case 2:
					encoder.WriteStructDeferral(this.PolicyAuditEventsInfo);
					break;
				case 3:
					encoder.WriteStructDeferral(this.PolicyPrimaryDomainInfo);
					break;
				case 5:
					encoder.WriteStructDeferral(this.PolicyAccountDomainInfo);
					break;
				case 4:
					encoder.WriteStructDeferral(this.PolicyPdAccountInfo);
					break;
				case 6:
					encoder.WriteStructDeferral(this.PolicyServerRoleInfo);
					break;
				case 7:
					encoder.WriteStructDeferral(this.PolicyReplicaSourceInfo);
					break;
				case 9:
					encoder.WriteStructDeferral(this.PolicyModificationInfo);
					break;
				case 10:
					encoder.WriteStructDeferral(this.PolicyAuditFullSetInfo);
					break;
				case 11:
					encoder.WriteStructDeferral(this.PolicyAuditFullQueryInfo);
					break;
				case 12:
					encoder.WriteStructDeferral(this.PolicyDnsDomainInfo);
					break;
				case 13:
					encoder.WriteStructDeferral(this.PolicyDnsDomainInfoInt);
					break;
				case 14:
					encoder.WriteStructDeferral(this.PolicyLocalAccountDomainInfo);
					break;
				case 15:
					encoder.WriteStructDeferral(this.PolicyMachineAccountInfo);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					decoder.ReadStructDeferral<POLICY_AUDIT_LOG_INFO>(ref this.PolicyAuditLogInfo);
					break;
				case 2:
					decoder.ReadStructDeferral<LSAPR_POLICY_AUDIT_EVENTS_INFO>(ref this.PolicyAuditEventsInfo);
					break;
				case 3:
					decoder.ReadStructDeferral<LSAPR_POLICY_PRIMARY_DOM_INFO>(ref this.PolicyPrimaryDomainInfo);
					break;
				case 5:
					decoder.ReadStructDeferral<LSAPR_POLICY_ACCOUNT_DOM_INFO>(ref this.PolicyAccountDomainInfo);
					break;
				case 4:
					decoder.ReadStructDeferral<LSAPR_POLICY_PD_ACCOUNT_INFO>(ref this.PolicyPdAccountInfo);
					break;
				case 6:
					decoder.ReadStructDeferral<POLICY_LSA_SERVER_ROLE_INFO>(ref this.PolicyServerRoleInfo);
					break;
				case 7:
					decoder.ReadStructDeferral<LSAPR_POLICY_REPLICA_SRCE_INFO>(ref this.PolicyReplicaSourceInfo);
					break;
				case 9:
					decoder.ReadStructDeferral<POLICY_MODIFICATION_INFO>(ref this.PolicyModificationInfo);
					break;
				case 10:
					decoder.ReadStructDeferral<POLICY_AUDIT_FULL_SET_INFO>(ref this.PolicyAuditFullSetInfo);
					break;
				case 11:
					decoder.ReadStructDeferral<POLICY_AUDIT_FULL_QUERY_INFO>(ref this.PolicyAuditFullQueryInfo);
					break;
				case 12:
					decoder.ReadStructDeferral<LSAPR_POLICY_DNS_DOMAIN_INFO>(ref this.PolicyDnsDomainInfo);
					break;
				case 13:
					decoder.ReadStructDeferral<LSAPR_POLICY_DNS_DOMAIN_INFO>(ref this.PolicyDnsDomainInfoInt);
					break;
				case 14:
					decoder.ReadStructDeferral<LSAPR_POLICY_ACCOUNT_DOM_INFO>(ref this.PolicyLocalAccountDomainInfo);
					break;
				case 15:
					decoder.ReadStructDeferral<LSAPR_POLICY_MACHINE_ACCT_INFO>(ref this.PolicyMachineAccountInfo);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct POLICY_DOMAIN_QUALITY_OF_SERVICE_INFO : IRpcFixedStruct
	{
		public uint QualityOfService;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.QualityOfService);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.QualityOfService = decoder.ReadUInt32();
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
	public partial struct LSAPR_POLICY_DOMAIN_EFS_INFO : IRpcFixedStruct
	{
		public uint InfoLength;
		public RpcPointer<byte[]> EfsBlob;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.InfoLength);
			encoder.WriteUniquePointer(this.EfsBlob);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.InfoLength = decoder.ReadUInt32();
			this.EfsBlob = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.EfsBlob is not null)
			{
				encoder.WriteArrayHeader(this.EfsBlob.value);
				for (int i = 0; i < this.EfsBlob.value.Length; i++)
				{
					byte elem_0 = this.EfsBlob.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.EfsBlob is not null)
			{
				this.EfsBlob.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.EfsBlob.value.Length; i++)
				{
					byte elem_0 = this.EfsBlob.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.EfsBlob.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_POLICY_DOMAIN_INFORMATION : IRpcFixedStruct
	{
		public POLICY_DOMAIN_INFORMATION_CLASS unionSwitch;
		public POLICY_DOMAIN_QUALITY_OF_SERVICE_INFO PolicyDomainQualityOfServiceInfo;
		public LSAPR_POLICY_DOMAIN_EFS_INFO PolicyDomainEfsInfo;
		public POLICY_DOMAIN_KERBEROS_TICKET_INFO PolicyDomainKerbTicketInfo;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment._8Byte);
			encoder.WriteEnumShortValue((short)this.unionSwitch);
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteFixedStruct(this.PolicyDomainQualityOfServiceInfo, NdrAlignment._4Byte);
					break;
				case 2:
					encoder.WriteFixedStruct(this.PolicyDomainEfsInfo, NdrAlignment.NativePtr);
					break;
				case 3:
					encoder.WriteFixedStruct(this.PolicyDomainKerbTicketInfo, NdrAlignment._8Byte);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment._8Byte);
			this.unionSwitch = (POLICY_DOMAIN_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			switch ((int)this.unionSwitch)
			{
				case 1:
					this.PolicyDomainQualityOfServiceInfo = decoder.ReadFixedStruct<POLICY_DOMAIN_QUALITY_OF_SERVICE_INFO>(NdrAlignment._4Byte);
					break;
				case 2:
					this.PolicyDomainEfsInfo = decoder.ReadFixedStruct<LSAPR_POLICY_DOMAIN_EFS_INFO>(NdrAlignment.NativePtr);
					break;
				case 3:
					this.PolicyDomainKerbTicketInfo = decoder.ReadFixedStruct<POLICY_DOMAIN_KERBEROS_TICKET_INFO>(NdrAlignment._8Byte);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteStructDeferral(this.PolicyDomainQualityOfServiceInfo);
					break;
				case 2:
					encoder.WriteStructDeferral(this.PolicyDomainEfsInfo);
					break;
				case 3:
					encoder.WriteStructDeferral(this.PolicyDomainKerbTicketInfo);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					decoder.ReadStructDeferral<POLICY_DOMAIN_QUALITY_OF_SERVICE_INFO>(ref this.PolicyDomainQualityOfServiceInfo);
					break;
				case 2:
					decoder.ReadStructDeferral<LSAPR_POLICY_DOMAIN_EFS_INFO>(ref this.PolicyDomainEfsInfo);
					break;
				case 3:
					decoder.ReadStructDeferral<POLICY_DOMAIN_KERBEROS_TICKET_INFO>(ref this.PolicyDomainKerbTicketInfo);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_DOMAIN_NAME_INFO : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Name;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_CONTROLLERS_INFO : IRpcFixedStruct
	{
		public uint Entries;
		public RpcPointer<ms_dtyp.RPC_UNICODE_STRING[]> Names;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Entries);
			encoder.WriteUniquePointer(this.Names);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Entries = decoder.ReadUInt32();
			this.Names = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Names is not null)
			{
				encoder.WriteArrayHeader(this.Names.value);
				for (int i = 0; i < this.Names.value.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Names.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Names.value.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Names.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Names is not null)
			{
				this.Names.value = decoder.ReadArrayHeader<ms_dtyp.RPC_UNICODE_STRING>();
				for (int i = 0; i < this.Names.value.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Names.value[i];
					elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
					this.Names.value[i] = elem_0;
				}

				for (int i = 0; i < this.Names.value.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Names.value[i];
					decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
					this.Names.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_PASSWORD_INFO : IRpcFixedStruct
	{
		public RpcPointer<LSAPR_CR_CIPHER_VALUE> Password;
		public RpcPointer<LSAPR_CR_CIPHER_VALUE> OldPassword;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.Password);
			encoder.WriteUniquePointer(this.OldPassword);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Password = decoder.ReadUniquePointer<LSAPR_CR_CIPHER_VALUE>();
			this.OldPassword = decoder.ReadUniquePointer<LSAPR_CR_CIPHER_VALUE>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Password is not null)
			{
				encoder.WriteFixedStruct(this.Password.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.Password.value);
			}

			if (this.OldPassword is not null)
			{
				encoder.WriteFixedStruct(this.OldPassword.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.OldPassword.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Password is not null)
			{
				this.Password.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref this.Password.value);
			}

			if (this.OldPassword is not null)
			{
				this.OldPassword.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref this.OldPassword.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_DOMAIN_INFORMATION_EX : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Name;
		public ms_dtyp.RPC_UNICODE_STRING FlatName;
		public RpcPointer<ms_dtyp.RPC_SID> Sid;
		public uint TrustDirection;
		public uint TrustType;
		public uint TrustAttributes;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.FlatName, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.Sid);
			encoder.WriteValue(this.TrustDirection);
			encoder.WriteValue(this.TrustType);
			encoder.WriteValue(this.TrustAttributes);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.FlatName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.Sid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			this.TrustDirection = decoder.ReadUInt32();
			this.TrustType = decoder.ReadUInt32();
			this.TrustAttributes = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
			encoder.WriteStructDeferral(this.FlatName);
			if (this.Sid is not null)
			{
				encoder.WriteConformantStruct(this.Sid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Sid.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FlatName);
			if (this.Sid is not null)
			{
				this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_AUTH_INFORMATION : IRpcFixedStruct
	{
		public ms_dtyp.LARGE_INTEGER LastUpdateTime;
		public uint AuthType;
		public uint AuthInfoLength;
		public RpcPointer<byte[]> AuthInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.LastUpdateTime, NdrAlignment._8Byte);
			encoder.WriteValue(this.AuthType);
			encoder.WriteValue(this.AuthInfoLength);
			encoder.WriteUniquePointer(this.AuthInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.LastUpdateTime = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.AuthType = decoder.ReadUInt32();
			this.AuthInfoLength = decoder.ReadUInt32();
			this.AuthInfo = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.LastUpdateTime);
			if (this.AuthInfo is not null)
			{
				encoder.WriteArrayHeader(this.AuthInfo.value);
				for (int i = 0; i < this.AuthInfo.value.Length; i++)
				{
					byte elem_0 = this.AuthInfo.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LastUpdateTime);
			if (this.AuthInfo is not null)
			{
				this.AuthInfo.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.AuthInfo.value.Length; i++)
				{
					byte elem_0 = this.AuthInfo.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.AuthInfo.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION : IRpcFixedStruct
	{
		public uint IncomingAuthInfos;
		public RpcPointer<LSAPR_AUTH_INFORMATION> IncomingAuthenticationInformation;
		public RpcPointer<LSAPR_AUTH_INFORMATION> IncomingPreviousAuthenticationInformation;
		public uint OutgoingAuthInfos;
		public RpcPointer<LSAPR_AUTH_INFORMATION> OutgoingAuthenticationInformation;
		public RpcPointer<LSAPR_AUTH_INFORMATION> OutgoingPreviousAuthenticationInformation;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.IncomingAuthInfos);
			encoder.WriteUniquePointer(this.IncomingAuthenticationInformation);
			encoder.WriteUniquePointer(this.IncomingPreviousAuthenticationInformation);
			encoder.WriteValue(this.OutgoingAuthInfos);
			encoder.WriteUniquePointer(this.OutgoingAuthenticationInformation);
			encoder.WriteUniquePointer(this.OutgoingPreviousAuthenticationInformation);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.IncomingAuthInfos = decoder.ReadUInt32();
			this.IncomingAuthenticationInformation = decoder.ReadUniquePointer<LSAPR_AUTH_INFORMATION>();
			this.IncomingPreviousAuthenticationInformation = decoder.ReadUniquePointer<LSAPR_AUTH_INFORMATION>();
			this.OutgoingAuthInfos = decoder.ReadUInt32();
			this.OutgoingAuthenticationInformation = decoder.ReadUniquePointer<LSAPR_AUTH_INFORMATION>();
			this.OutgoingPreviousAuthenticationInformation = decoder.ReadUniquePointer<LSAPR_AUTH_INFORMATION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.IncomingAuthenticationInformation is not null)
			{
				encoder.WriteFixedStruct(this.IncomingAuthenticationInformation.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.IncomingAuthenticationInformation.value);
			}

			if (this.IncomingPreviousAuthenticationInformation is not null)
			{
				encoder.WriteFixedStruct(this.IncomingPreviousAuthenticationInformation.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.IncomingPreviousAuthenticationInformation.value);
			}

			if (this.OutgoingAuthenticationInformation is not null)
			{
				encoder.WriteFixedStruct(this.OutgoingAuthenticationInformation.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.OutgoingAuthenticationInformation.value);
			}

			if (this.OutgoingPreviousAuthenticationInformation is not null)
			{
				encoder.WriteFixedStruct(this.OutgoingPreviousAuthenticationInformation.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(this.OutgoingPreviousAuthenticationInformation.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.IncomingAuthenticationInformation is not null)
			{
				this.IncomingAuthenticationInformation.value = decoder.ReadFixedStruct<LSAPR_AUTH_INFORMATION>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<LSAPR_AUTH_INFORMATION>(ref this.IncomingAuthenticationInformation.value);
			}

			if (this.IncomingPreviousAuthenticationInformation is not null)
			{
				this.IncomingPreviousAuthenticationInformation.value = decoder.ReadFixedStruct<LSAPR_AUTH_INFORMATION>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<LSAPR_AUTH_INFORMATION>(ref this.IncomingPreviousAuthenticationInformation.value);
			}

			if (this.OutgoingAuthenticationInformation is not null)
			{
				this.OutgoingAuthenticationInformation.value = decoder.ReadFixedStruct<LSAPR_AUTH_INFORMATION>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<LSAPR_AUTH_INFORMATION>(ref this.OutgoingAuthenticationInformation.value);
			}

			if (this.OutgoingPreviousAuthenticationInformation is not null)
			{
				this.OutgoingPreviousAuthenticationInformation.value = decoder.ReadFixedStruct<LSAPR_AUTH_INFORMATION>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<LSAPR_AUTH_INFORMATION>(ref this.OutgoingPreviousAuthenticationInformation.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION : IRpcFixedStruct
	{
		public LSAPR_TRUSTED_DOMAIN_INFORMATION_EX Information;
		public TRUSTED_POSIX_OFFSET_INFO PosixOffset;
		public LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION AuthInformation;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Information, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.PosixOffset, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.AuthInformation, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Information = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(NdrAlignment.NativePtr);
			this.PosixOffset = decoder.ReadFixedStruct<TRUSTED_POSIX_OFFSET_INFO>(NdrAlignment._4Byte);
			this.AuthInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Information);
			encoder.WriteStructDeferral(this.PosixOffset);
			encoder.WriteStructDeferral(this.AuthInformation);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(ref this.Information);
			decoder.ReadStructDeferral<TRUSTED_POSIX_OFFSET_INFO>(ref this.PosixOffset);
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(ref this.AuthInformation);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_DOMAIN_AUTH_BLOB : IRpcFixedStruct
	{
		public uint AuthSize;
		public RpcPointer<byte[]> AuthBlob;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.AuthSize);
			encoder.WriteUniquePointer(this.AuthBlob);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.AuthSize = decoder.ReadUInt32();
			this.AuthBlob = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.AuthBlob is not null)
			{
				encoder.WriteArrayHeader(this.AuthBlob.value);
				for (int i = 0; i < this.AuthBlob.value.Length; i++)
				{
					byte elem_0 = this.AuthBlob.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.AuthBlob is not null)
			{
				this.AuthBlob.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.AuthBlob.value.Length; i++)
				{
					byte elem_0 = this.AuthBlob.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.AuthBlob.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL : IRpcFixedStruct
	{
		public LSAPR_TRUSTED_DOMAIN_AUTH_BLOB AuthBlob;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.AuthBlob, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.AuthBlob = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_BLOB>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.AuthBlob);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_BLOB>(ref this.AuthBlob);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION_INTERNAL : IRpcFixedStruct
	{
		public LSAPR_TRUSTED_DOMAIN_INFORMATION_EX Information;
		public TRUSTED_POSIX_OFFSET_INFO PosixOffset;
		public LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL AuthInformation;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Information, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.PosixOffset, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.AuthInformation, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Information = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(NdrAlignment.NativePtr);
			this.PosixOffset = decoder.ReadFixedStruct<TRUSTED_POSIX_OFFSET_INFO>(NdrAlignment._4Byte);
			this.AuthInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Information);
			encoder.WriteStructDeferral(this.PosixOffset);
			encoder.WriteStructDeferral(this.AuthInformation);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(ref this.Information);
			decoder.ReadStructDeferral<TRUSTED_POSIX_OFFSET_INFO>(ref this.PosixOffset);
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL>(ref this.AuthInformation);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2 : IRpcFixedStruct
	{
		public ms_dtyp.RPC_UNICODE_STRING Name;
		public ms_dtyp.RPC_UNICODE_STRING FlatName;
		public RpcPointer<ms_dtyp.RPC_SID> Sid;
		public uint TrustDirection;
		public uint TrustType;
		public uint TrustAttributes;
		public uint ForestTrustLength;
		public RpcPointer<byte[]> ForestTrustInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.FlatName, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.Sid);
			encoder.WriteValue(this.TrustDirection);
			encoder.WriteValue(this.TrustType);
			encoder.WriteValue(this.TrustAttributes);
			encoder.WriteValue(this.ForestTrustLength);
			encoder.WriteUniquePointer(this.ForestTrustInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.FlatName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.Sid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			this.TrustDirection = decoder.ReadUInt32();
			this.TrustType = decoder.ReadUInt32();
			this.TrustAttributes = decoder.ReadUInt32();
			this.ForestTrustLength = decoder.ReadUInt32();
			this.ForestTrustInfo = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
			encoder.WriteStructDeferral(this.FlatName);
			if (this.Sid is not null)
			{
				encoder.WriteConformantStruct(this.Sid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Sid.value);
			}

			if (this.ForestTrustInfo is not null)
			{
				encoder.WriteArrayHeader(this.ForestTrustInfo.value);
				for (int i = 0; i < this.ForestTrustInfo.value.Length; i++)
				{
					byte elem_0 = this.ForestTrustInfo.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FlatName);
			if (this.Sid is not null)
			{
				this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
			}

			if (this.ForestTrustInfo is not null)
			{
				this.ForestTrustInfo.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.ForestTrustInfo.value.Length; i++)
				{
					byte elem_0 = this.ForestTrustInfo.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.ForestTrustInfo.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION2 : IRpcFixedStruct
	{
		public LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2 Information;
		public TRUSTED_POSIX_OFFSET_INFO PosixOffset;
		public LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION AuthInformation;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.Information, NdrAlignment.NativePtr);
			encoder.WriteFixedStruct(this.PosixOffset, NdrAlignment._4Byte);
			encoder.WriteFixedStruct(this.AuthInformation, NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Information = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2>(NdrAlignment.NativePtr);
			this.PosixOffset = decoder.ReadFixedStruct<TRUSTED_POSIX_OFFSET_INFO>(NdrAlignment._4Byte);
			this.AuthInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(NdrAlignment.NativePtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Information);
			encoder.WriteStructDeferral(this.PosixOffset);
			encoder.WriteStructDeferral(this.AuthInformation);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2>(ref this.Information);
			decoder.ReadStructDeferral<TRUSTED_POSIX_OFFSET_INFO>(ref this.PosixOffset);
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(ref this.AuthInformation);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct TRUSTED_DOMAIN_SUPPORTED_ENCRYPTION_TYPES : IRpcFixedStruct
	{
		public uint SupportedEncryptionTypes;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.SupportedEncryptionTypes);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.SupportedEncryptionTypes = decoder.ReadUInt32();
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
	public partial struct LSAPR_TRUSTED_DOMAIN_INFO : IRpcFixedStruct
	{
		public TRUSTED_INFORMATION_CLASS unionSwitch;
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
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteEnumShortValue((short)this.unionSwitch);
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteFixedStruct(this.TrustedDomainNameInfo, NdrAlignment.NativePtr);
					break;
				case 2:
					encoder.WriteFixedStruct(this.TrustedControllersInfo, NdrAlignment.NativePtr);
					break;
				case 3:
					encoder.WriteFixedStruct(this.TrustedPosixOffsetInfo, NdrAlignment._4Byte);
					break;
				case 4:
					encoder.WriteFixedStruct(this.TrustedPasswordInfo, NdrAlignment.NativePtr);
					break;
				case 5:
					encoder.WriteFixedStruct(this.TrustedDomainInfoBasic, NdrAlignment.NativePtr);
					break;
				case 6:
					encoder.WriteFixedStruct(this.TrustedDomainInfoEx, NdrAlignment.NativePtr);
					break;
				case 7:
					encoder.WriteFixedStruct(this.TrustedAuthInfo, NdrAlignment.NativePtr);
					break;
				case 8:
					encoder.WriteFixedStruct(this.TrustedFullInfo, NdrAlignment.NativePtr);
					break;
				case 9:
					encoder.WriteFixedStruct(this.TrustedAuthInfoInternal, NdrAlignment.NativePtr);
					break;
				case 10:
					encoder.WriteFixedStruct(this.TrustedFullInfoInternal, NdrAlignment.NativePtr);
					break;
				case 11:
					encoder.WriteFixedStruct(this.TrustedDomainInfoEx2, NdrAlignment.NativePtr);
					break;
				case 12:
					encoder.WriteFixedStruct(this.TrustedFullInfo2, NdrAlignment.NativePtr);
					break;
				case 13:
					encoder.WriteFixedStruct(this.TrustedDomainSETs, NdrAlignment._4Byte);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = (TRUSTED_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			switch ((int)this.unionSwitch)
			{
				case 1:
					this.TrustedDomainNameInfo = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_NAME_INFO>(NdrAlignment.NativePtr);
					break;
				case 2:
					this.TrustedControllersInfo = decoder.ReadFixedStruct<LSAPR_TRUSTED_CONTROLLERS_INFO>(NdrAlignment.NativePtr);
					break;
				case 3:
					this.TrustedPosixOffsetInfo = decoder.ReadFixedStruct<TRUSTED_POSIX_OFFSET_INFO>(NdrAlignment._4Byte);
					break;
				case 4:
					this.TrustedPasswordInfo = decoder.ReadFixedStruct<LSAPR_TRUSTED_PASSWORD_INFO>(NdrAlignment.NativePtr);
					break;
				case 5:
					this.TrustedDomainInfoBasic = decoder.ReadFixedStruct<LSAPR_TRUST_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 6:
					this.TrustedDomainInfoEx = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(NdrAlignment.NativePtr);
					break;
				case 7:
					this.TrustedAuthInfo = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 8:
					this.TrustedFullInfo = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION>(NdrAlignment.NativePtr);
					break;
				case 9:
					this.TrustedAuthInfoInternal = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL>(NdrAlignment.NativePtr);
					break;
				case 10:
					this.TrustedFullInfoInternal = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION_INTERNAL>(NdrAlignment.NativePtr);
					break;
				case 11:
					this.TrustedDomainInfoEx2 = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2>(NdrAlignment.NativePtr);
					break;
				case 12:
					this.TrustedFullInfo2 = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION2>(NdrAlignment.NativePtr);
					break;
				case 13:
					this.TrustedDomainSETs = decoder.ReadFixedStruct<TRUSTED_DOMAIN_SUPPORTED_ENCRYPTION_TYPES>(NdrAlignment._4Byte);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					encoder.WriteStructDeferral(this.TrustedDomainNameInfo);
					break;
				case 2:
					encoder.WriteStructDeferral(this.TrustedControllersInfo);
					break;
				case 3:
					encoder.WriteStructDeferral(this.TrustedPosixOffsetInfo);
					break;
				case 4:
					encoder.WriteStructDeferral(this.TrustedPasswordInfo);
					break;
				case 5:
					encoder.WriteStructDeferral(this.TrustedDomainInfoBasic);
					break;
				case 6:
					encoder.WriteStructDeferral(this.TrustedDomainInfoEx);
					break;
				case 7:
					encoder.WriteStructDeferral(this.TrustedAuthInfo);
					break;
				case 8:
					encoder.WriteStructDeferral(this.TrustedFullInfo);
					break;
				case 9:
					encoder.WriteStructDeferral(this.TrustedAuthInfoInternal);
					break;
				case 10:
					encoder.WriteStructDeferral(this.TrustedFullInfoInternal);
					break;
				case 11:
					encoder.WriteStructDeferral(this.TrustedDomainInfoEx2);
					break;
				case 12:
					encoder.WriteStructDeferral(this.TrustedFullInfo2);
					break;
				case 13:
					encoder.WriteStructDeferral(this.TrustedDomainSETs);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.unionSwitch)
			{
				case 1:
					decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_NAME_INFO>(ref this.TrustedDomainNameInfo);
					break;
				case 2:
					decoder.ReadStructDeferral<LSAPR_TRUSTED_CONTROLLERS_INFO>(ref this.TrustedControllersInfo);
					break;
				case 3:
					decoder.ReadStructDeferral<TRUSTED_POSIX_OFFSET_INFO>(ref this.TrustedPosixOffsetInfo);
					break;
				case 4:
					decoder.ReadStructDeferral<LSAPR_TRUSTED_PASSWORD_INFO>(ref this.TrustedPasswordInfo);
					break;
				case 5:
					decoder.ReadStructDeferral<LSAPR_TRUST_INFORMATION>(ref this.TrustedDomainInfoBasic);
					break;
				case 6:
					decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(ref this.TrustedDomainInfoEx);
					break;
				case 7:
					decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(ref this.TrustedAuthInfo);
					break;
				case 8:
					decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION>(ref this.TrustedFullInfo);
					break;
				case 9:
					decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL>(ref this.TrustedAuthInfoInternal);
					break;
				case 10:
					decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION_INTERNAL>(ref this.TrustedFullInfoInternal);
					break;
				case 11:
					decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX2>(ref this.TrustedDomainInfoEx2);
					break;
				case 12:
					decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_FULL_INFORMATION2>(ref this.TrustedFullInfo2);
					break;
				case 13:
					decoder.ReadStructDeferral<TRUSTED_DOMAIN_SUPPORTED_ENCRYPTION_TYPES>(ref this.TrustedDomainSETs);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_USER_RIGHT_SET : IRpcFixedStruct
	{
		public uint Entries;
		public RpcPointer<ms_dtyp.RPC_UNICODE_STRING[]> UserRights;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Entries);
			encoder.WriteUniquePointer(this.UserRights);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Entries = decoder.ReadUInt32();
			this.UserRights = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.UserRights is not null)
			{
				encoder.WriteArrayHeader(this.UserRights.value);
				for (int i = 0; i < this.UserRights.value.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = this.UserRights.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.UserRights.value.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = this.UserRights.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.UserRights is not null)
			{
				this.UserRights.value = decoder.ReadArrayHeader<ms_dtyp.RPC_UNICODE_STRING>();
				for (int i = 0; i < this.UserRights.value.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = this.UserRights.value[i];
					elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
					this.UserRights.value[i] = elem_0;
				}

				for (int i = 0; i < this.UserRights.value.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = this.UserRights.value[i];
					decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
					this.UserRights.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRUSTED_ENUM_BUFFER_EX : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX[]> EnumerationBuffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.EnumerationBuffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.EnumerationBuffer = decoder.ReadUniquePointer<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.EnumerationBuffer is not null)
			{
				encoder.WriteArrayHeader(this.EnumerationBuffer.value);
				for (int i = 0; i < this.EnumerationBuffer.value.Length; i++)
				{
					LSAPR_TRUSTED_DOMAIN_INFORMATION_EX elem_0 = this.EnumerationBuffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.EnumerationBuffer.value.Length; i++)
				{
					LSAPR_TRUSTED_DOMAIN_INFORMATION_EX elem_0 = this.EnumerationBuffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.EnumerationBuffer is not null)
			{
				this.EnumerationBuffer.value = decoder.ReadArrayHeader<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>();
				for (int i = 0; i < this.EnumerationBuffer.value.Length; i++)
				{
					LSAPR_TRUSTED_DOMAIN_INFORMATION_EX elem_0 = this.EnumerationBuffer.value[i];
					elem_0 = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(NdrAlignment.NativePtr);
					this.EnumerationBuffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.EnumerationBuffer.value.Length; i++)
				{
					LSAPR_TRUSTED_DOMAIN_INFORMATION_EX elem_0 = this.EnumerationBuffer.value[i];
					decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(ref elem_0);
					this.EnumerationBuffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_REFERENCED_DOMAIN_LIST : IRpcFixedStruct
	{
		public uint Entries;
		public RpcPointer<LSAPR_TRUST_INFORMATION[]> Domains;
		public uint MaxEntries;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Entries);
			encoder.WriteUniquePointer(this.Domains);
			encoder.WriteValue(this.MaxEntries);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Entries = decoder.ReadUInt32();
			this.Domains = decoder.ReadUniquePointer<LSAPR_TRUST_INFORMATION[]>();
			this.MaxEntries = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Domains is not null)
			{
				encoder.WriteArrayHeader(this.Domains.value);
				for (int i = 0; i < this.Domains.value.Length; i++)
				{
					LSAPR_TRUST_INFORMATION elem_0 = this.Domains.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Domains.value.Length; i++)
				{
					LSAPR_TRUST_INFORMATION elem_0 = this.Domains.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Domains is not null)
			{
				this.Domains.value = decoder.ReadArrayHeader<LSAPR_TRUST_INFORMATION>();
				for (int i = 0; i < this.Domains.value.Length; i++)
				{
					LSAPR_TRUST_INFORMATION elem_0 = this.Domains.value[i];
					elem_0 = decoder.ReadFixedStruct<LSAPR_TRUST_INFORMATION>(NdrAlignment.NativePtr);
					this.Domains.value[i] = elem_0;
				}

				for (int i = 0; i < this.Domains.value.Length; i++)
				{
					LSAPR_TRUST_INFORMATION elem_0 = this.Domains.value[i];
					decoder.ReadStructDeferral<LSAPR_TRUST_INFORMATION>(ref elem_0);
					this.Domains.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum SID_NAME_USE : int
	{
		SidTypeUser = 1,
		SidTypeGroup = 2,
		SidTypeDomain = 3,
		SidTypeAlias = 4,
		SidTypeWellKnownGroup = 5,
		SidTypeDeletedAccount = 6,
		SidTypeInvalid = 7,
		SidTypeUnknown = 8,
		SidTypeComputer = 9,
		SidTypeLabel = 10
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSA_TRANSLATED_SID : IRpcFixedStruct
	{
		public SID_NAME_USE Use;
		public uint RelativeId;
		public int DomainIndex;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.Use);
			encoder.WriteValue(this.RelativeId);
			encoder.WriteValue(this.DomainIndex);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Use = (SID_NAME_USE)decoder.ReadEnumShortValue();
			this.RelativeId = decoder.ReadUInt32();
			this.DomainIndex = decoder.ReadInt32();
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
	public partial struct LSAPR_TRANSLATED_SIDS : IRpcFixedStruct
	{
		public uint Entries;
		public RpcPointer<LSA_TRANSLATED_SID[]> Sids;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Entries);
			encoder.WriteUniquePointer(this.Sids);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Entries = decoder.ReadUInt32();
			this.Sids = decoder.ReadUniquePointer<LSA_TRANSLATED_SID[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Sids is not null)
			{
				encoder.WriteArrayHeader(this.Sids.value);
				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					LSA_TRANSLATED_SID elem_0 = this.Sids.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					LSA_TRANSLATED_SID elem_0 = this.Sids.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Sids is not null)
			{
				this.Sids.value = decoder.ReadArrayHeader<LSA_TRANSLATED_SID>();
				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					LSA_TRANSLATED_SID elem_0 = this.Sids.value[i];
					elem_0 = decoder.ReadFixedStruct<LSA_TRANSLATED_SID>(NdrAlignment._4Byte);
					this.Sids.value[i] = elem_0;
				}

				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					LSA_TRANSLATED_SID elem_0 = this.Sids.value[i];
					decoder.ReadStructDeferral<LSA_TRANSLATED_SID>(ref elem_0);
					this.Sids.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum LSAP_LOOKUP_LEVEL : int
	{
		LsapLookupWksta = 1,
		LsapLookupPDC = 2,
		LsapLookupTDL = 3,
		LsapLookupGC = 4,
		LsapLookupXForestReferral = 5,
		LsapLookupXForestResolve = 6,
		LsapLookupRODCReferralToFullDC = 7
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_SID_INFORMATION : IRpcFixedStruct
	{
		public RpcPointer<ms_dtyp.RPC_SID> Sid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.Sid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Sid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Sid is not null)
			{
				encoder.WriteConformantStruct(this.Sid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Sid.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Sid is not null)
			{
				this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_SID_ENUM_BUFFER : IRpcFixedStruct
	{
		public uint Entries;
		public RpcPointer<LSAPR_SID_INFORMATION[]> SidInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Entries);
			encoder.WriteUniquePointer(this.SidInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Entries = decoder.ReadUInt32();
			this.SidInfo = decoder.ReadUniquePointer<LSAPR_SID_INFORMATION[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.SidInfo is not null)
			{
				encoder.WriteArrayHeader(this.SidInfo.value);
				for (int i = 0; i < this.SidInfo.value.Length; i++)
				{
					LSAPR_SID_INFORMATION elem_0 = this.SidInfo.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.SidInfo.value.Length; i++)
				{
					LSAPR_SID_INFORMATION elem_0 = this.SidInfo.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.SidInfo is not null)
			{
				this.SidInfo.value = decoder.ReadArrayHeader<LSAPR_SID_INFORMATION>();
				for (int i = 0; i < this.SidInfo.value.Length; i++)
				{
					LSAPR_SID_INFORMATION elem_0 = this.SidInfo.value[i];
					elem_0 = decoder.ReadFixedStruct<LSAPR_SID_INFORMATION>(NdrAlignment.NativePtr);
					this.SidInfo.value[i] = elem_0;
				}

				for (int i = 0; i < this.SidInfo.value.Length; i++)
				{
					LSAPR_SID_INFORMATION elem_0 = this.SidInfo.value[i];
					decoder.ReadStructDeferral<LSAPR_SID_INFORMATION>(ref elem_0);
					this.SidInfo.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRANSLATED_NAME : IRpcFixedStruct
	{
		public SID_NAME_USE Use;
		public ms_dtyp.RPC_UNICODE_STRING Name;
		public int DomainIndex;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.Use);
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
			encoder.WriteValue(this.DomainIndex);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Use = (SID_NAME_USE)decoder.ReadEnumShortValue();
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.DomainIndex = decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRANSLATED_NAMES : IRpcFixedStruct
	{
		public uint Entries;
		public RpcPointer<LSAPR_TRANSLATED_NAME[]> Names;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Entries);
			encoder.WriteUniquePointer(this.Names);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Entries = decoder.ReadUInt32();
			this.Names = decoder.ReadUniquePointer<LSAPR_TRANSLATED_NAME[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Names is not null)
			{
				encoder.WriteArrayHeader(this.Names.value);
				for (int i = 0; i < this.Names.value.Length; i++)
				{
					LSAPR_TRANSLATED_NAME elem_0 = this.Names.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Names.value.Length; i++)
				{
					LSAPR_TRANSLATED_NAME elem_0 = this.Names.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Names is not null)
			{
				this.Names.value = decoder.ReadArrayHeader<LSAPR_TRANSLATED_NAME>();
				for (int i = 0; i < this.Names.value.Length; i++)
				{
					LSAPR_TRANSLATED_NAME elem_0 = this.Names.value[i];
					elem_0 = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAME>(NdrAlignment.NativePtr);
					this.Names.value[i] = elem_0;
				}

				for (int i = 0; i < this.Names.value.Length; i++)
				{
					LSAPR_TRANSLATED_NAME elem_0 = this.Names.value[i];
					decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAME>(ref elem_0);
					this.Names.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRANSLATED_NAME_EX : IRpcFixedStruct
	{
		public SID_NAME_USE Use;
		public ms_dtyp.RPC_UNICODE_STRING Name;
		public int DomainIndex;
		public uint Flags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.Use);
			encoder.WriteFixedStruct(this.Name, NdrAlignment.NativePtr);
			encoder.WriteValue(this.DomainIndex);
			encoder.WriteValue(this.Flags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Use = (SID_NAME_USE)decoder.ReadEnumShortValue();
			this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			this.DomainIndex = decoder.ReadInt32();
			this.Flags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.Name);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRANSLATED_NAMES_EX : IRpcFixedStruct
	{
		public uint Entries;
		public RpcPointer<LSAPR_TRANSLATED_NAME_EX[]> Names;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Entries);
			encoder.WriteUniquePointer(this.Names);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Entries = decoder.ReadUInt32();
			this.Names = decoder.ReadUniquePointer<LSAPR_TRANSLATED_NAME_EX[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Names is not null)
			{
				encoder.WriteArrayHeader(this.Names.value);
				for (int i = 0; i < this.Names.value.Length; i++)
				{
					LSAPR_TRANSLATED_NAME_EX elem_0 = this.Names.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Names.value.Length; i++)
				{
					LSAPR_TRANSLATED_NAME_EX elem_0 = this.Names.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Names is not null)
			{
				this.Names.value = decoder.ReadArrayHeader<LSAPR_TRANSLATED_NAME_EX>();
				for (int i = 0; i < this.Names.value.Length; i++)
				{
					LSAPR_TRANSLATED_NAME_EX elem_0 = this.Names.value[i];
					elem_0 = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAME_EX>(NdrAlignment.NativePtr);
					this.Names.value[i] = elem_0;
				}

				for (int i = 0; i < this.Names.value.Length; i++)
				{
					LSAPR_TRANSLATED_NAME_EX elem_0 = this.Names.value[i];
					decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAME_EX>(ref elem_0);
					this.Names.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRANSLATED_SID_EX : IRpcFixedStruct
	{
		public SID_NAME_USE Use;
		public uint RelativeId;
		public int DomainIndex;
		public uint Flags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.Use);
			encoder.WriteValue(this.RelativeId);
			encoder.WriteValue(this.DomainIndex);
			encoder.WriteValue(this.Flags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Use = (SID_NAME_USE)decoder.ReadEnumShortValue();
			this.RelativeId = decoder.ReadUInt32();
			this.DomainIndex = decoder.ReadInt32();
			this.Flags = decoder.ReadUInt32();
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
	public partial struct LSAPR_TRANSLATED_SIDS_EX : IRpcFixedStruct
	{
		public uint Entries;
		public RpcPointer<LSAPR_TRANSLATED_SID_EX[]> Sids;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Entries);
			encoder.WriteUniquePointer(this.Sids);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Entries = decoder.ReadUInt32();
			this.Sids = decoder.ReadUniquePointer<LSAPR_TRANSLATED_SID_EX[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Sids is not null)
			{
				encoder.WriteArrayHeader(this.Sids.value);
				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					LSAPR_TRANSLATED_SID_EX elem_0 = this.Sids.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					LSAPR_TRANSLATED_SID_EX elem_0 = this.Sids.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Sids is not null)
			{
				this.Sids.value = decoder.ReadArrayHeader<LSAPR_TRANSLATED_SID_EX>();
				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					LSAPR_TRANSLATED_SID_EX elem_0 = this.Sids.value[i];
					elem_0 = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SID_EX>(NdrAlignment._4Byte);
					this.Sids.value[i] = elem_0;
				}

				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					LSAPR_TRANSLATED_SID_EX elem_0 = this.Sids.value[i];
					decoder.ReadStructDeferral<LSAPR_TRANSLATED_SID_EX>(ref elem_0);
					this.Sids.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRANSLATED_SID_EX2 : IRpcFixedStruct
	{
		public SID_NAME_USE Use;
		public RpcPointer<ms_dtyp.RPC_SID> Sid;
		public int DomainIndex;
		public uint Flags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteEnumShortValue((short)this.Use);
			encoder.WriteUniquePointer(this.Sid);
			encoder.WriteValue(this.DomainIndex);
			encoder.WriteValue(this.Flags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Use = (SID_NAME_USE)decoder.ReadEnumShortValue();
			this.Sid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
			this.DomainIndex = decoder.ReadInt32();
			this.Flags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Sid is not null)
			{
				encoder.WriteConformantStruct(this.Sid.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.Sid.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Sid is not null)
			{
				this.Sid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.Sid.value);
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct LSAPR_TRANSLATED_SIDS_EX2 : IRpcFixedStruct
	{
		public uint Entries;
		public RpcPointer<LSAPR_TRANSLATED_SID_EX2[]> Sids;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Entries);
			encoder.WriteUniquePointer(this.Sids);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Entries = decoder.ReadUInt32();
			this.Sids = decoder.ReadUniquePointer<LSAPR_TRANSLATED_SID_EX2[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Sids is not null)
			{
				encoder.WriteArrayHeader(this.Sids.value);
				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					LSAPR_TRANSLATED_SID_EX2 elem_0 = this.Sids.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					LSAPR_TRANSLATED_SID_EX2 elem_0 = this.Sids.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Sids is not null)
			{
				this.Sids.value = decoder.ReadArrayHeader<LSAPR_TRANSLATED_SID_EX2>();
				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					LSAPR_TRANSLATED_SID_EX2 elem_0 = this.Sids.value[i];
					elem_0 = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SID_EX2>(NdrAlignment.NativePtr);
					this.Sids.value[i] = elem_0;
				}

				for (int i = 0; i < this.Sids.value.Length; i++)
				{
					LSAPR_TRANSLATED_SID_EX2 elem_0 = this.Sids.value[i];
					decoder.ReadStructDeferral<LSAPR_TRANSLATED_SID_EX2>(ref elem_0);
					this.Sids.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("12345778-1234-abcd-ef00-0123456789ab"), RpcVersionAttribute(0, 0)]
	public partial interface lsarpc
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarClose(RpcPointer<RpcContextHandle> ObjectHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum1NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarEnumeratePrivileges(RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_PRIVILEGE_ENUM_BUFFER> EnumerationBuffer, uint PreferedMaximumLength, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarQuerySecurityObject(RpcContextHandle ObjectHandle, uint SecurityInformation, RpcPointer<RpcPointer<LSAPR_SR_SECURITY_DESCRIPTOR>> SecurityDescriptor, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarSetSecurityObject(RpcContextHandle ObjectHandle, uint SecurityInformation, LSAPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum5NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarOpenPolicy(RpcPointer<char> SystemName, LSAPR_OBJECT_ATTRIBUTES ObjectAttributes, uint DesiredAccess, RpcPointer<RpcContextHandle> PolicyHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarQueryInformationPolicy(RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>> PolicyInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarSetInformationPolicy(RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, LSAPR_POLICY_INFORMATION PolicyInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum9NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarCreateAccount(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, uint DesiredAccess, RpcPointer<RpcContextHandle> AccountHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarEnumerateAccounts(RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> EnumerationBuffer, uint PreferedMaximumLength, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarCreateTrustedDomain(RpcContextHandle PolicyHandle, LSAPR_TRUST_INFORMATION TrustedDomainInformation, uint DesiredAccess, RpcPointer<RpcContextHandle> TrustedDomainHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarEnumerateTrustedDomains(RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_TRUSTED_ENUM_BUFFER> EnumerationBuffer, uint PreferedMaximumLength, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarLookupNames(RpcContextHandle PolicyHandle, uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarLookupSids(RpcContextHandle PolicyHandle, LSAPR_SID_ENUM_BUFFER SidEnumBuffer, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_NAMES> TranslatedNames, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarCreateSecret(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING SecretName, uint DesiredAccess, RpcPointer<RpcContextHandle> SecretHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarOpenAccount(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, uint DesiredAccess, RpcPointer<RpcContextHandle> AccountHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarEnumeratePrivilegesAccount(RpcContextHandle AccountHandle, RpcPointer<RpcPointer<LSAPR_PRIVILEGE_SET>> Privileges, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarAddPrivilegesToAccount(RpcContextHandle AccountHandle, LSAPR_PRIVILEGE_SET Privileges, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarRemovePrivilegesFromAccount(RpcContextHandle AccountHandle, byte AllPrivileges, RpcPointer<LSAPR_PRIVILEGE_SET> Privileges, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum21NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum22NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarGetSystemAccessAccount(RpcContextHandle AccountHandle, RpcPointer<uint> SystemAccess, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarSetSystemAccessAccount(RpcContextHandle AccountHandle, uint SystemAccess, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarOpenTrustedDomain(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, uint DesiredAccess, RpcPointer<RpcContextHandle> TrustedDomainHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarQueryInfoTrustedDomain(RpcContextHandle TrustedDomainHandle, TRUSTED_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarSetInformationTrustedDomain(RpcContextHandle TrustedDomainHandle, TRUSTED_INFORMATION_CLASS InformationClass, LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarOpenSecret(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING SecretName, uint DesiredAccess, RpcPointer<RpcContextHandle> SecretHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarSetSecret(RpcContextHandle SecretHandle, RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedCurrentValue, RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedOldValue, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarQuerySecret(RpcContextHandle SecretHandle, RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedCurrentValue, RpcPointer<ms_dtyp.LARGE_INTEGER> CurrentValueSetTime, RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedOldValue, RpcPointer<ms_dtyp.LARGE_INTEGER> OldValueSetTime, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarLookupPrivilegeValue(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING Name, RpcPointer<ms_dtyp.LUID> Value, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarLookupPrivilegeName(RpcContextHandle PolicyHandle, ms_dtyp.LUID Value, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> Name, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarLookupPrivilegeDisplayName(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING Name, short ClientLanguage, short ClientSystemDefaultLanguage, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> DisplayName, RpcPointer<ushort> LanguageReturned, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarDeleteObject(RpcPointer<RpcContextHandle> ObjectHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarEnumerateAccountsWithUserRight(RpcContextHandle PolicyHandle, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> UserRight, RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> EnumerationBuffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarEnumerateAccountRights(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, RpcPointer<LSAPR_USER_RIGHT_SET> UserRights, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarAddAccountRights(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, LSAPR_USER_RIGHT_SET UserRights, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarRemoveAccountRights(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, byte AllRights, LSAPR_USER_RIGHT_SET UserRights, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarQueryTrustedDomainInfo(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, TRUSTED_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarSetTrustedDomainInfo(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, TRUSTED_INFORMATION_CLASS InformationClass, LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarDeleteTrustedDomain(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarStorePrivateData(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING KeyName, RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedData, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarRetrievePrivateData(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING KeyName, RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedData, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarOpenPolicy2(string SystemName, LSAPR_OBJECT_ATTRIBUTES ObjectAttributes, uint DesiredAccess, RpcPointer<RpcContextHandle> PolicyHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarGetUserName(string SystemName, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> UserName, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> DomainName, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarQueryInformationPolicy2(RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>> PolicyInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarSetInformationPolicy2(RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, LSAPR_POLICY_INFORMATION PolicyInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarQueryTrustedDomainInfoByName(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, TRUSTED_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarSetTrustedDomainInfoByName(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, TRUSTED_INFORMATION_CLASS InformationClass, LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarEnumerateTrustedDomainsEx(RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_TRUSTED_ENUM_BUFFER_EX> EnumerationBuffer, uint PreferedMaximumLength, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarCreateTrustedDomainEx(RpcContextHandle PolicyHandle, LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInformation, LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION AuthenticationInformation, uint DesiredAccess, RpcPointer<RpcContextHandle> TrustedDomainHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum52NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarQueryDomainInformationPolicy(RpcContextHandle PolicyHandle, POLICY_DOMAIN_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION>> PolicyDomainInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarSetDomainInformationPolicy(RpcContextHandle PolicyHandle, POLICY_DOMAIN_INFORMATION_CLASS InformationClass, RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION> PolicyDomainInformation, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarOpenTrustedDomainByName(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, uint DesiredAccess, RpcPointer<RpcContextHandle> TrustedDomainHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum56NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarLookupSids2(RpcContextHandle PolicyHandle, LSAPR_SID_ENUM_BUFFER SidEnumBuffer, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_NAMES_EX> TranslatedNames, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarLookupNames2(RpcContextHandle PolicyHandle, uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS_EX> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarCreateTrustedDomainEx2(RpcContextHandle PolicyHandle, LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInformation, LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL AuthenticationInformation, uint DesiredAccess, RpcPointer<RpcContextHandle> TrustedDomainHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum60NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum61NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum62NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum63NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum64NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum65NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum66NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum67NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarLookupNames3(RpcContextHandle PolicyHandle, uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS_EX2> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum69NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum70NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum71NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum72NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarQueryForestTrustInformation(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, LSA_FOREST_TRUST_RECORD_TYPE HighestRecordType, RpcPointer<RpcPointer<LSA_FOREST_TRUST_INFORMATION>> ForestTrustInfo, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarSetForestTrustInformation(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, LSA_FOREST_TRUST_RECORD_TYPE HighestRecordType, LSA_FOREST_TRUST_INFORMATION ForestTrustInfo, byte CheckOnly, RpcPointer<RpcPointer<LSA_FOREST_TRUST_COLLISION_INFORMATION>> CollisionInfo, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum75NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarLookupSids3(LSAPR_SID_ENUM_BUFFER SidEnumBuffer, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_NAMES_EX> TranslatedNames, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> LsarLookupNames4(uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS_EX2> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("12345778-1234-abcd-ef00-0123456789ab")]
	public partial class lsarpcClientProxy : Titanis.DceRpc.Client.RpcClientProxy, lsarpc, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarClose(RpcPointer<RpcContextHandle> ObjectHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ObjectHandle.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ObjectHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum1NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarEnumeratePrivileges(RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_PRIVILEGE_ENUM_BUFFER> EnumerationBuffer, uint PreferedMaximumLength, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteValue(EnumerationContext.value);
			encoder.WriteValue(PreferedMaximumLength);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			EnumerationContext.value = decoder.ReadUInt32();
			EnumerationBuffer.value = decoder.ReadFixedStruct<LSAPR_PRIVILEGE_ENUM_BUFFER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_PRIVILEGE_ENUM_BUFFER>(ref EnumerationBuffer.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarQuerySecurityObject(RpcContextHandle ObjectHandle, uint SecurityInformation, RpcPointer<RpcPointer<LSAPR_SR_SECURITY_DESCRIPTOR>> SecurityDescriptor, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ObjectHandle);
			encoder.WriteValue(SecurityInformation);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			SecurityDescriptor.value = decoder.ReadOutUniquePointer<LSAPR_SR_SECURITY_DESCRIPTOR>(SecurityDescriptor.value);
			if (SecurityDescriptor.value is not null)
			{
				SecurityDescriptor.value.value = decoder.ReadFixedStruct<LSAPR_SR_SECURITY_DESCRIPTOR>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_SR_SECURITY_DESCRIPTOR>(ref SecurityDescriptor.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarSetSecurityObject(RpcContextHandle ObjectHandle, uint SecurityInformation, LSAPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ObjectHandle);
			encoder.WriteValue(SecurityInformation);
			encoder.WriteFixedStruct(SecurityDescriptor, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(SecurityDescriptor);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum5NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarOpenPolicy(RpcPointer<char> SystemName, LSAPR_OBJECT_ATTRIBUTES ObjectAttributes, uint DesiredAccess, RpcPointer<RpcContextHandle> PolicyHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(SystemName);
			if (SystemName is not null)
			{
				encoder.WriteValue(SystemName.value);
			}

			encoder.WriteFixedStruct(ObjectAttributes, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ObjectAttributes);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			PolicyHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarQueryInformationPolicy(RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>> PolicyInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteEnumShortValue((short)InformationClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			PolicyInformation.value = decoder.ReadOutUniquePointer<LSAPR_POLICY_INFORMATION>(PolicyInformation.value);
			if (PolicyInformation.value is not null)
			{
				PolicyInformation.value.value = decoder.ReadUnion<LSAPR_POLICY_INFORMATION>();
				decoder.ReadStructDeferral<LSAPR_POLICY_INFORMATION>(ref PolicyInformation.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarSetInformationPolicy(RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, LSAPR_POLICY_INFORMATION PolicyInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteEnumShortValue((short)InformationClass);
			encoder.WriteUnion(PolicyInformation);
			encoder.WriteStructDeferral(PolicyInformation);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum9NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarCreateAccount(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, uint DesiredAccess, RpcPointer<RpcContextHandle> AccountHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteConformantStruct(AccountSid, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(AccountSid);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			AccountHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarEnumerateAccounts(RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> EnumerationBuffer, uint PreferedMaximumLength, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteValue(EnumerationContext.value);
			encoder.WriteValue(PreferedMaximumLength);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			EnumerationContext.value = decoder.ReadUInt32();
			EnumerationBuffer.value = decoder.ReadFixedStruct<LSAPR_ACCOUNT_ENUM_BUFFER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_ACCOUNT_ENUM_BUFFER>(ref EnumerationBuffer.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarCreateTrustedDomain(RpcContextHandle PolicyHandle, LSAPR_TRUST_INFORMATION TrustedDomainInformation, uint DesiredAccess, RpcPointer<RpcContextHandle> TrustedDomainHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(TrustedDomainInformation, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TrustedDomainInformation);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			TrustedDomainHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarEnumerateTrustedDomains(RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_TRUSTED_ENUM_BUFFER> EnumerationBuffer, uint PreferedMaximumLength, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteValue(EnumerationContext.value);
			encoder.WriteValue(PreferedMaximumLength);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			EnumerationContext.value = decoder.ReadUInt32();
			EnumerationBuffer.value = decoder.ReadFixedStruct<LSAPR_TRUSTED_ENUM_BUFFER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRUSTED_ENUM_BUFFER>(ref EnumerationBuffer.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarLookupNames(RpcContextHandle PolicyHandle, uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteValue(Count);
			if (Names is not null)
			{
				encoder.WriteArrayHeader(Names);
				for (int i = 0; i < Names.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}
			}

			for (int i = 0; i < Names.Length; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
				encoder.WriteStructDeferral(elem_0);
			}

			encoder.WriteFixedStruct(TranslatedSids.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedSids.value);
			encoder.WriteEnumShortValue((short)LookupLevel);
			encoder.WriteValue(MappedCount.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ReferencedDomains.value = decoder.ReadOutUniquePointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
			}

			TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS>(ref TranslatedSids.value);
			MappedCount.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarLookupSids(RpcContextHandle PolicyHandle, LSAPR_SID_ENUM_BUFFER SidEnumBuffer, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_NAMES> TranslatedNames, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(SidEnumBuffer, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(SidEnumBuffer);
			encoder.WriteFixedStruct(TranslatedNames.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedNames.value);
			encoder.WriteEnumShortValue((short)LookupLevel);
			encoder.WriteValue(MappedCount.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ReferencedDomains.value = decoder.ReadOutUniquePointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
			}

			TranslatedNames.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAMES>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAMES>(ref TranslatedNames.value);
			MappedCount.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarCreateSecret(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING SecretName, uint DesiredAccess, RpcPointer<RpcContextHandle> SecretHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(SecretName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(SecretName);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			SecretHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarOpenAccount(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, uint DesiredAccess, RpcPointer<RpcContextHandle> AccountHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteConformantStruct(AccountSid, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(AccountSid);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			AccountHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarEnumeratePrivilegesAccount(RpcContextHandle AccountHandle, RpcPointer<RpcPointer<LSAPR_PRIVILEGE_SET>> Privileges, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AccountHandle);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Privileges.value = decoder.ReadOutUniquePointer<LSAPR_PRIVILEGE_SET>(Privileges.value);
			if (Privileges.value is not null)
			{
				Privileges.value.value = decoder.ReadConformantStruct<LSAPR_PRIVILEGE_SET>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<LSAPR_PRIVILEGE_SET>(ref Privileges.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarAddPrivilegesToAccount(RpcContextHandle AccountHandle, LSAPR_PRIVILEGE_SET Privileges, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AccountHandle);
			encoder.WriteConformantStruct(Privileges, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(Privileges);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarRemovePrivilegesFromAccount(RpcContextHandle AccountHandle, byte AllPrivileges, RpcPointer<LSAPR_PRIVILEGE_SET> Privileges, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AccountHandle);
			encoder.WriteValue(AllPrivileges);
			encoder.WriteUniquePointer(Privileges);
			if (Privileges is not null)
			{
				encoder.WriteConformantStruct(Privileges.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(Privileges.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum21NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum22NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarGetSystemAccessAccount(RpcContextHandle AccountHandle, RpcPointer<uint> SystemAccess, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AccountHandle);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			SystemAccess.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarSetSystemAccessAccount(RpcContextHandle AccountHandle, uint SystemAccess, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(AccountHandle);
			encoder.WriteValue(SystemAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarOpenTrustedDomain(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, uint DesiredAccess, RpcPointer<RpcContextHandle> TrustedDomainHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteConformantStruct(TrustedDomainSid, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(TrustedDomainSid);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			TrustedDomainHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarQueryInfoTrustedDomain(RpcContextHandle TrustedDomainHandle, TRUSTED_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(TrustedDomainHandle);
			encoder.WriteEnumShortValue((short)InformationClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			TrustedDomainInformation.value = decoder.ReadOutUniquePointer<LSAPR_TRUSTED_DOMAIN_INFO>(TrustedDomainInformation.value);
			if (TrustedDomainInformation.value is not null)
			{
				TrustedDomainInformation.value.value = decoder.ReadUnion<LSAPR_TRUSTED_DOMAIN_INFO>();
				decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFO>(ref TrustedDomainInformation.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarSetInformationTrustedDomain(RpcContextHandle TrustedDomainHandle, TRUSTED_INFORMATION_CLASS InformationClass, LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(TrustedDomainHandle);
			encoder.WriteEnumShortValue((short)InformationClass);
			encoder.WriteUnion(TrustedDomainInformation);
			encoder.WriteStructDeferral(TrustedDomainInformation);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarOpenSecret(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING SecretName, uint DesiredAccess, RpcPointer<RpcContextHandle> SecretHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(SecretName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(SecretName);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			SecretHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarSetSecret(RpcContextHandle SecretHandle, RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedCurrentValue, RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedOldValue, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(SecretHandle);
			encoder.WriteUniquePointer(EncryptedCurrentValue);
			if (EncryptedCurrentValue is not null)
			{
				encoder.WriteFixedStruct(EncryptedCurrentValue.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(EncryptedCurrentValue.value);
			}

			encoder.WriteUniquePointer(EncryptedOldValue);
			if (EncryptedOldValue is not null)
			{
				encoder.WriteFixedStruct(EncryptedOldValue.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(EncryptedOldValue.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarQuerySecret(RpcContextHandle SecretHandle, RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedCurrentValue, RpcPointer<ms_dtyp.LARGE_INTEGER> CurrentValueSetTime, RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedOldValue, RpcPointer<ms_dtyp.LARGE_INTEGER> OldValueSetTime, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(SecretHandle);
			encoder.WriteUniquePointer(EncryptedCurrentValue);
			if (EncryptedCurrentValue is not null)
			{
				encoder.WriteUniquePointer(EncryptedCurrentValue.value);
				if (EncryptedCurrentValue.value is not null)
				{
					encoder.WriteFixedStruct(EncryptedCurrentValue.value.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(EncryptedCurrentValue.value.value);
				}
			}

			encoder.WriteUniquePointer(CurrentValueSetTime);
			if (CurrentValueSetTime is not null)
			{
				encoder.WriteFixedStruct(CurrentValueSetTime.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(CurrentValueSetTime.value);
			}

			encoder.WriteUniquePointer(EncryptedOldValue);
			if (EncryptedOldValue is not null)
			{
				encoder.WriteUniquePointer(EncryptedOldValue.value);
				if (EncryptedOldValue.value is not null)
				{
					encoder.WriteFixedStruct(EncryptedOldValue.value.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(EncryptedOldValue.value.value);
				}
			}

			encoder.WriteUniquePointer(OldValueSetTime);
			if (OldValueSetTime is not null)
			{
				encoder.WriteFixedStruct(OldValueSetTime.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(OldValueSetTime.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			EncryptedCurrentValue = decoder.ReadOutUniquePointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>(EncryptedCurrentValue);
			if (EncryptedCurrentValue is not null)
			{
				EncryptedCurrentValue.value = decoder.ReadUniquePointer<LSAPR_CR_CIPHER_VALUE>();
				if (EncryptedCurrentValue.value is not null)
				{
					EncryptedCurrentValue.value.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedCurrentValue.value.value);
				}
			}

			CurrentValueSetTime = decoder.ReadOutUniquePointer<ms_dtyp.LARGE_INTEGER>(CurrentValueSetTime);
			if (CurrentValueSetTime is not null)
			{
				CurrentValueSetTime.value = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref CurrentValueSetTime.value);
			}

			EncryptedOldValue = decoder.ReadOutUniquePointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>(EncryptedOldValue);
			if (EncryptedOldValue is not null)
			{
				EncryptedOldValue.value = decoder.ReadUniquePointer<LSAPR_CR_CIPHER_VALUE>();
				if (EncryptedOldValue.value is not null)
				{
					EncryptedOldValue.value.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedOldValue.value.value);
				}
			}

			OldValueSetTime = decoder.ReadOutUniquePointer<ms_dtyp.LARGE_INTEGER>(OldValueSetTime);
			if (OldValueSetTime is not null)
			{
				OldValueSetTime.value = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref OldValueSetTime.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarLookupPrivilegeValue(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING Name, RpcPointer<ms_dtyp.LUID> Value, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(Name, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Name);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Value.value = decoder.ReadFixedStruct<ms_dtyp.LUID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.LUID>(ref Value.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarLookupPrivilegeName(RpcContextHandle PolicyHandle, ms_dtyp.LUID Value, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> Name, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(Value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(Value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Name.value = decoder.ReadOutUniquePointer<ms_dtyp.RPC_UNICODE_STRING>(Name.value);
			if (Name.value is not null)
			{
				Name.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarLookupPrivilegeDisplayName(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING Name, short ClientLanguage, short ClientSystemDefaultLanguage, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> DisplayName, RpcPointer<ushort> LanguageReturned, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(Name, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Name);
			encoder.WriteValue(ClientLanguage);
			encoder.WriteValue(ClientSystemDefaultLanguage);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			DisplayName.value = decoder.ReadOutUniquePointer<ms_dtyp.RPC_UNICODE_STRING>(DisplayName.value);
			if (DisplayName.value is not null)
			{
				DisplayName.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref DisplayName.value.value);
			}

			LanguageReturned.value = decoder.ReadUInt16();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarDeleteObject(RpcPointer<RpcContextHandle> ObjectHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ObjectHandle.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ObjectHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarEnumerateAccountsWithUserRight(RpcContextHandle PolicyHandle, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> UserRight, RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> EnumerationBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteUniquePointer(UserRight);
			if (UserRight is not null)
			{
				encoder.WriteFixedStruct(UserRight.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(UserRight.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			EnumerationBuffer.value = decoder.ReadFixedStruct<LSAPR_ACCOUNT_ENUM_BUFFER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_ACCOUNT_ENUM_BUFFER>(ref EnumerationBuffer.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarEnumerateAccountRights(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, RpcPointer<LSAPR_USER_RIGHT_SET> UserRights, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(36);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteConformantStruct(AccountSid, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(AccountSid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			UserRights.value = decoder.ReadFixedStruct<LSAPR_USER_RIGHT_SET>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_USER_RIGHT_SET>(ref UserRights.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarAddAccountRights(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, LSAPR_USER_RIGHT_SET UserRights, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(37);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteConformantStruct(AccountSid, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(AccountSid);
			encoder.WriteFixedStruct(UserRights, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(UserRights);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarRemoveAccountRights(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID AccountSid, byte AllRights, LSAPR_USER_RIGHT_SET UserRights, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(38);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteConformantStruct(AccountSid, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(AccountSid);
			encoder.WriteValue(AllRights);
			encoder.WriteFixedStruct(UserRights, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(UserRights);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarQueryTrustedDomainInfo(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, TRUSTED_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(39);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteConformantStruct(TrustedDomainSid, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(TrustedDomainSid);
			encoder.WriteEnumShortValue((short)InformationClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			TrustedDomainInformation.value = decoder.ReadOutUniquePointer<LSAPR_TRUSTED_DOMAIN_INFO>(TrustedDomainInformation.value);
			if (TrustedDomainInformation.value is not null)
			{
				TrustedDomainInformation.value.value = decoder.ReadUnion<LSAPR_TRUSTED_DOMAIN_INFO>();
				decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFO>(ref TrustedDomainInformation.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarSetTrustedDomainInfo(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, TRUSTED_INFORMATION_CLASS InformationClass, LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(40);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteConformantStruct(TrustedDomainSid, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(TrustedDomainSid);
			encoder.WriteEnumShortValue((short)InformationClass);
			encoder.WriteUnion(TrustedDomainInformation);
			encoder.WriteStructDeferral(TrustedDomainInformation);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarDeleteTrustedDomain(RpcContextHandle PolicyHandle, ms_dtyp.RPC_SID TrustedDomainSid, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(41);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteConformantStruct(TrustedDomainSid, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(TrustedDomainSid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarStorePrivateData(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING KeyName, RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedData, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(42);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(KeyName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(KeyName);
			encoder.WriteUniquePointer(EncryptedData);
			if (EncryptedData is not null)
			{
				encoder.WriteFixedStruct(EncryptedData.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(EncryptedData.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarRetrievePrivateData(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING KeyName, RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedData, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(43);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(KeyName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(KeyName);
			encoder.WriteUniquePointer(EncryptedData.value);
			if (EncryptedData.value is not null)
			{
				encoder.WriteFixedStruct(EncryptedData.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(EncryptedData.value.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			EncryptedData.value = decoder.ReadOutUniquePointer<LSAPR_CR_CIPHER_VALUE>(EncryptedData.value);
			if (EncryptedData.value is not null)
			{
				EncryptedData.value.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedData.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarOpenPolicy2(string SystemName, LSAPR_OBJECT_ATTRIBUTES ObjectAttributes, uint DesiredAccess, RpcPointer<RpcContextHandle> PolicyHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(44);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(SystemName is null);
			if (SystemName is not null)
				encoder.WriteWideCharString(SystemName);
			encoder.WriteFixedStruct(ObjectAttributes, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ObjectAttributes);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			PolicyHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarGetUserName(string SystemName, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> UserName, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> DomainName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(45);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(SystemName is null);
			if (SystemName is not null)
				encoder.WriteWideCharString(SystemName);
			encoder.WriteUniquePointer(UserName.value);
			if (UserName.value is not null)
			{
				encoder.WriteFixedStruct(UserName.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(UserName.value.value);
			}

			encoder.WriteUniquePointer(DomainName);
			if (DomainName is not null)
			{
				encoder.WriteUniquePointer(DomainName.value);
				if (DomainName.value is not null)
				{
					encoder.WriteFixedStruct(DomainName.value.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(DomainName.value.value);
				}
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			UserName.value = decoder.ReadOutUniquePointer<ms_dtyp.RPC_UNICODE_STRING>(UserName.value);
			if (UserName.value is not null)
			{
				UserName.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref UserName.value.value);
			}

			DomainName = decoder.ReadOutUniquePointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>(DomainName);
			if (DomainName is not null)
			{
				DomainName.value = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
				if (DomainName.value is not null)
				{
					DomainName.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref DomainName.value.value);
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarQueryInformationPolicy2(RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>> PolicyInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(46);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteEnumShortValue((short)InformationClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			PolicyInformation.value = decoder.ReadOutUniquePointer<LSAPR_POLICY_INFORMATION>(PolicyInformation.value);
			if (PolicyInformation.value is not null)
			{
				PolicyInformation.value.value = decoder.ReadUnion<LSAPR_POLICY_INFORMATION>();
				decoder.ReadStructDeferral<LSAPR_POLICY_INFORMATION>(ref PolicyInformation.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarSetInformationPolicy2(RpcContextHandle PolicyHandle, POLICY_INFORMATION_CLASS InformationClass, LSAPR_POLICY_INFORMATION PolicyInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(47);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteEnumShortValue((short)InformationClass);
			encoder.WriteUnion(PolicyInformation);
			encoder.WriteStructDeferral(PolicyInformation);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarQueryTrustedDomainInfoByName(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, TRUSTED_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(48);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(TrustedDomainName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TrustedDomainName);
			encoder.WriteEnumShortValue((short)InformationClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			TrustedDomainInformation.value = decoder.ReadOutUniquePointer<LSAPR_TRUSTED_DOMAIN_INFO>(TrustedDomainInformation.value);
			if (TrustedDomainInformation.value is not null)
			{
				TrustedDomainInformation.value.value = decoder.ReadUnion<LSAPR_TRUSTED_DOMAIN_INFO>();
				decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFO>(ref TrustedDomainInformation.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarSetTrustedDomainInfoByName(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, TRUSTED_INFORMATION_CLASS InformationClass, LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(49);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(TrustedDomainName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TrustedDomainName);
			encoder.WriteEnumShortValue((short)InformationClass);
			encoder.WriteUnion(TrustedDomainInformation);
			encoder.WriteStructDeferral(TrustedDomainInformation);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarEnumerateTrustedDomainsEx(RpcContextHandle PolicyHandle, RpcPointer<uint> EnumerationContext, RpcPointer<LSAPR_TRUSTED_ENUM_BUFFER_EX> EnumerationBuffer, uint PreferedMaximumLength, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(50);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteValue(EnumerationContext.value);
			encoder.WriteValue(PreferedMaximumLength);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			EnumerationContext.value = decoder.ReadUInt32();
			EnumerationBuffer.value = decoder.ReadFixedStruct<LSAPR_TRUSTED_ENUM_BUFFER_EX>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRUSTED_ENUM_BUFFER_EX>(ref EnumerationBuffer.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarCreateTrustedDomainEx(RpcContextHandle PolicyHandle, LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInformation, LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION AuthenticationInformation, uint DesiredAccess, RpcPointer<RpcContextHandle> TrustedDomainHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(51);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(TrustedDomainInformation, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TrustedDomainInformation);
			encoder.WriteFixedStruct(AuthenticationInformation, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(AuthenticationInformation);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			TrustedDomainHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum52NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(52);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarQueryDomainInformationPolicy(RpcContextHandle PolicyHandle, POLICY_DOMAIN_INFORMATION_CLASS InformationClass, RpcPointer<RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION>> PolicyDomainInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(53);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteEnumShortValue((short)InformationClass);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			PolicyDomainInformation.value = decoder.ReadOutUniquePointer<LSAPR_POLICY_DOMAIN_INFORMATION>(PolicyDomainInformation.value);
			if (PolicyDomainInformation.value is not null)
			{
				PolicyDomainInformation.value.value = decoder.ReadUnion<LSAPR_POLICY_DOMAIN_INFORMATION>();
				decoder.ReadStructDeferral<LSAPR_POLICY_DOMAIN_INFORMATION>(ref PolicyDomainInformation.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarSetDomainInformationPolicy(RpcContextHandle PolicyHandle, POLICY_DOMAIN_INFORMATION_CLASS InformationClass, RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION> PolicyDomainInformation, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(54);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteEnumShortValue((short)InformationClass);
			encoder.WriteUniquePointer(PolicyDomainInformation);
			if (PolicyDomainInformation is not null)
			{
				encoder.WriteUnion(PolicyDomainInformation.value);
				encoder.WriteStructDeferral(PolicyDomainInformation.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarOpenTrustedDomainByName(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, uint DesiredAccess, RpcPointer<RpcContextHandle> TrustedDomainHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(55);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(TrustedDomainName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TrustedDomainName);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			TrustedDomainHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum56NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(56);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarLookupSids2(RpcContextHandle PolicyHandle, LSAPR_SID_ENUM_BUFFER SidEnumBuffer, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_NAMES_EX> TranslatedNames, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(57);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(SidEnumBuffer, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(SidEnumBuffer);
			encoder.WriteFixedStruct(TranslatedNames.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedNames.value);
			encoder.WriteEnumShortValue((short)LookupLevel);
			encoder.WriteValue(MappedCount.value);
			encoder.WriteValue(LookupOptions);
			encoder.WriteValue(ClientRevision);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ReferencedDomains.value = decoder.ReadOutUniquePointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
			}

			TranslatedNames.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAMES_EX>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAMES_EX>(ref TranslatedNames.value);
			MappedCount.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarLookupNames2(RpcContextHandle PolicyHandle, uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS_EX> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(58);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteValue(Count);
			if (Names is not null)
			{
				encoder.WriteArrayHeader(Names);
				for (int i = 0; i < Names.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}
			}

			for (int i = 0; i < Names.Length; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
				encoder.WriteStructDeferral(elem_0);
			}

			encoder.WriteFixedStruct(TranslatedSids.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedSids.value);
			encoder.WriteEnumShortValue((short)LookupLevel);
			encoder.WriteValue(MappedCount.value);
			encoder.WriteValue(LookupOptions);
			encoder.WriteValue(ClientRevision);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ReferencedDomains.value = decoder.ReadOutUniquePointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
			}

			TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS_EX>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS_EX>(ref TranslatedSids.value);
			MappedCount.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarCreateTrustedDomainEx2(RpcContextHandle PolicyHandle, LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInformation, LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL AuthenticationInformation, uint DesiredAccess, RpcPointer<RpcContextHandle> TrustedDomainHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(59);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(TrustedDomainInformation, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TrustedDomainInformation);
			encoder.WriteFixedStruct(AuthenticationInformation, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(AuthenticationInformation);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			TrustedDomainHandle.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum60NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(60);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum61NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(61);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum62NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(62);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum63NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(63);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum64NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(64);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum65NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(65);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum66NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(66);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum67NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(67);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarLookupNames3(RpcContextHandle PolicyHandle, uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS_EX2> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(68);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteValue(Count);
			if (Names is not null)
			{
				encoder.WriteArrayHeader(Names);
				for (int i = 0; i < Names.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}
			}

			for (int i = 0; i < Names.Length; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
				encoder.WriteStructDeferral(elem_0);
			}

			encoder.WriteFixedStruct(TranslatedSids.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedSids.value);
			encoder.WriteEnumShortValue((short)LookupLevel);
			encoder.WriteValue(MappedCount.value);
			encoder.WriteValue(LookupOptions);
			encoder.WriteValue(ClientRevision);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ReferencedDomains.value = decoder.ReadOutUniquePointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
			}

			TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS_EX2>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS_EX2>(ref TranslatedSids.value);
			MappedCount.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum69NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(69);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum70NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(70);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum71NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(71);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum72NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(72);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarQueryForestTrustInformation(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, LSA_FOREST_TRUST_RECORD_TYPE HighestRecordType, RpcPointer<RpcPointer<LSA_FOREST_TRUST_INFORMATION>> ForestTrustInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(73);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(TrustedDomainName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TrustedDomainName);
			encoder.WriteEnumShortValue((short)HighestRecordType);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ForestTrustInfo.value = decoder.ReadOutUniquePointer<LSA_FOREST_TRUST_INFORMATION>(ForestTrustInfo.value);
			if (ForestTrustInfo.value is not null)
			{
				ForestTrustInfo.value.value = decoder.ReadFixedStruct<LSA_FOREST_TRUST_INFORMATION>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSA_FOREST_TRUST_INFORMATION>(ref ForestTrustInfo.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarSetForestTrustInformation(RpcContextHandle PolicyHandle, ms_dtyp.RPC_UNICODE_STRING TrustedDomainName, LSA_FOREST_TRUST_RECORD_TYPE HighestRecordType, LSA_FOREST_TRUST_INFORMATION ForestTrustInfo, byte CheckOnly, RpcPointer<RpcPointer<LSA_FOREST_TRUST_COLLISION_INFORMATION>> CollisionInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(74);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(PolicyHandle);
			encoder.WriteFixedStruct(TrustedDomainName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TrustedDomainName);
			encoder.WriteEnumShortValue((short)HighestRecordType);
			encoder.WriteFixedStruct(ForestTrustInfo, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(ForestTrustInfo);
			encoder.WriteValue(CheckOnly);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			CollisionInfo.value = decoder.ReadOutUniquePointer<LSA_FOREST_TRUST_COLLISION_INFORMATION>(CollisionInfo.value);
			if (CollisionInfo.value is not null)
			{
				CollisionInfo.value.value = decoder.ReadFixedStruct<LSA_FOREST_TRUST_COLLISION_INFORMATION>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSA_FOREST_TRUST_COLLISION_INFORMATION>(ref CollisionInfo.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum75NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(75);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarLookupSids3(LSAPR_SID_ENUM_BUFFER SidEnumBuffer, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_NAMES_EX> TranslatedNames, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(76);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(SidEnumBuffer, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(SidEnumBuffer);
			encoder.WriteFixedStruct(TranslatedNames.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedNames.value);
			encoder.WriteEnumShortValue((short)LookupLevel);
			encoder.WriteValue(MappedCount.value);
			encoder.WriteValue(LookupOptions);
			encoder.WriteValue(ClientRevision);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ReferencedDomains.value = decoder.ReadOutUniquePointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
			}

			TranslatedNames.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAMES_EX>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAMES_EX>(ref TranslatedNames.value);
			MappedCount.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> LsarLookupNames4(uint Count, ms_dtyp.RPC_UNICODE_STRING[] Names, RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains, RpcPointer<LSAPR_TRANSLATED_SIDS_EX2> TranslatedSids, LSAP_LOOKUP_LEVEL LookupLevel, RpcPointer<uint> MappedCount, uint LookupOptions, uint ClientRevision, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(77);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(Count);
			if (Names is not null)
			{
				encoder.WriteArrayHeader(Names);
				for (int i = 0; i < Names.Length; i++)
				{
					ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}
			}

			for (int i = 0; i < Names.Length; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
				encoder.WriteStructDeferral(elem_0);
			}

			encoder.WriteFixedStruct(TranslatedSids.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedSids.value);
			encoder.WriteEnumShortValue((short)LookupLevel);
			encoder.WriteValue(MappedCount.value);
			encoder.WriteValue(LookupOptions);
			encoder.WriteValue(ClientRevision);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ReferencedDomains.value = decoder.ReadOutUniquePointer<LSAPR_REFERENCED_DOMAIN_LIST>(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				ReferencedDomains.value.value = decoder.ReadFixedStruct<LSAPR_REFERENCED_DOMAIN_LIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_REFERENCED_DOMAIN_LIST>(ref ReferencedDomains.value.value);
			}

			TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS_EX2>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS_EX2>(ref TranslatedSids.value);
			MappedCount.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(lsarpc);
		private static Guid _interfaceUuid = new Guid("12345778-1234-abcd-ef00-0123456789ab");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class lsarpcStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarClose(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> ObjectHandle;
			ObjectHandle = new RpcPointer<RpcContextHandle>();
			ObjectHandle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.LsarClose(ObjectHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(ObjectHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarEnumeratePrivileges(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
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
			encoder.WriteFixedStruct(EnumerationBuffer.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(EnumerationBuffer.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarQuerySecurityObject(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle ObjectHandle;
			uint SecurityInformation;
			RpcPointer<RpcPointer<LSAPR_SR_SECURITY_DESCRIPTOR>> SecurityDescriptor = new RpcPointer<RpcPointer<LSAPR_SR_SECURITY_DESCRIPTOR>>();
			ObjectHandle = decoder.ReadContextHandle();
			SecurityInformation = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarQuerySecurityObject(ObjectHandle, SecurityInformation, SecurityDescriptor, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(SecurityDescriptor.value);
			if (SecurityDescriptor.value is not null)
			{
				encoder.WriteFixedStruct(SecurityDescriptor.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(SecurityDescriptor.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarSetSecurityObject(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle ObjectHandle;
			uint SecurityInformation;
			LSAPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor;
			ObjectHandle = decoder.ReadContextHandle();
			SecurityInformation = decoder.ReadUInt32();
			SecurityDescriptor = decoder.ReadFixedStruct<LSAPR_SR_SECURITY_DESCRIPTOR>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_SR_SECURITY_DESCRIPTOR>(ref SecurityDescriptor);
			var invokeTask = this._obj.LsarSetSecurityObject(ObjectHandle, SecurityInformation, SecurityDescriptor, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum5NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum5NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarOpenPolicy(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> SystemName;
			LSAPR_OBJECT_ATTRIBUTES ObjectAttributes;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> PolicyHandle = new RpcPointer<RpcContextHandle>();
			SystemName = decoder.ReadUniquePointer<char>();
			if (SystemName is not null)
			{
				SystemName.value = decoder.ReadWideChar();
			}

			ObjectAttributes = decoder.ReadFixedStruct<LSAPR_OBJECT_ATTRIBUTES>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_OBJECT_ATTRIBUTES>(ref ObjectAttributes);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarOpenPolicy(SystemName, ObjectAttributes, DesiredAccess, PolicyHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(PolicyHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarQueryInformationPolicy(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			POLICY_INFORMATION_CLASS InformationClass;
			RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>> PolicyInformation = new RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>>();
			PolicyHandle = decoder.ReadContextHandle();
			InformationClass = (POLICY_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.LsarQueryInformationPolicy(PolicyHandle, InformationClass, PolicyInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(PolicyInformation.value);
			if (PolicyInformation.value is not null)
			{
				encoder.WriteUnion(PolicyInformation.value.value);
				encoder.WriteStructDeferral(PolicyInformation.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarSetInformationPolicy(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			POLICY_INFORMATION_CLASS InformationClass;
			LSAPR_POLICY_INFORMATION PolicyInformation;
			PolicyHandle = decoder.ReadContextHandle();
			InformationClass = (POLICY_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			PolicyInformation = decoder.ReadUnion<LSAPR_POLICY_INFORMATION>();
			decoder.ReadStructDeferral<LSAPR_POLICY_INFORMATION>(ref PolicyInformation);
			var invokeTask = this._obj.LsarSetInformationPolicy(PolicyHandle, InformationClass, PolicyInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum9NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum9NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarCreateAccount(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_SID AccountSid;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> AccountHandle = new RpcPointer<RpcContextHandle>();
			PolicyHandle = decoder.ReadContextHandle();
			AccountSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref AccountSid);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarCreateAccount(PolicyHandle, AccountSid, DesiredAccess, AccountHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(AccountHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarEnumerateAccounts(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
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
			encoder.WriteFixedStruct(EnumerationBuffer.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(EnumerationBuffer.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarCreateTrustedDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			LSAPR_TRUST_INFORMATION TrustedDomainInformation;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> TrustedDomainHandle = new RpcPointer<RpcContextHandle>();
			PolicyHandle = decoder.ReadContextHandle();
			TrustedDomainInformation = decoder.ReadFixedStruct<LSAPR_TRUST_INFORMATION>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRUST_INFORMATION>(ref TrustedDomainInformation);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarCreateTrustedDomain(PolicyHandle, TrustedDomainInformation, DesiredAccess, TrustedDomainHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(TrustedDomainHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarEnumerateTrustedDomains(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
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
			encoder.WriteFixedStruct(EnumerationBuffer.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(EnumerationBuffer.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarLookupNames(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			uint Count;
			ms_dtyp.RPC_UNICODE_STRING[] Names;
			RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
			RpcPointer<LSAPR_TRANSLATED_SIDS> TranslatedSids;
			LSAP_LOOKUP_LEVEL LookupLevel;
			RpcPointer<uint> MappedCount;
			PolicyHandle = decoder.ReadContextHandle();
			Count = decoder.ReadUInt32();
			Names = decoder.ReadArrayHeader<ms_dtyp.RPC_UNICODE_STRING>();
			for (int i = 0; i < Names.Length; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
				elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				Names[i] = elem_0;
			}

			for (int i = 0; i < Names.Length; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
				Names[i] = elem_0;
			}

			TranslatedSids = new RpcPointer<LSAPR_TRANSLATED_SIDS>();
			TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS>(ref TranslatedSids.value);
			LookupLevel = (LSAP_LOOKUP_LEVEL)decoder.ReadEnumShortValue();
			MappedCount = new RpcPointer<uint>();
			MappedCount.value = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarLookupNames(PolicyHandle, Count, Names, ReferencedDomains, TranslatedSids, LookupLevel, MappedCount, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				encoder.WriteFixedStruct(ReferencedDomains.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ReferencedDomains.value.value);
			}

			encoder.WriteFixedStruct(TranslatedSids.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedSids.value);
			encoder.WriteValue(MappedCount.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarLookupSids(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			LSAPR_SID_ENUM_BUFFER SidEnumBuffer;
			RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
			RpcPointer<LSAPR_TRANSLATED_NAMES> TranslatedNames;
			LSAP_LOOKUP_LEVEL LookupLevel;
			RpcPointer<uint> MappedCount;
			PolicyHandle = decoder.ReadContextHandle();
			SidEnumBuffer = decoder.ReadFixedStruct<LSAPR_SID_ENUM_BUFFER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_SID_ENUM_BUFFER>(ref SidEnumBuffer);
			TranslatedNames = new RpcPointer<LSAPR_TRANSLATED_NAMES>();
			TranslatedNames.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAMES>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAMES>(ref TranslatedNames.value);
			LookupLevel = (LSAP_LOOKUP_LEVEL)decoder.ReadEnumShortValue();
			MappedCount = new RpcPointer<uint>();
			MappedCount.value = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarLookupSids(PolicyHandle, SidEnumBuffer, ReferencedDomains, TranslatedNames, LookupLevel, MappedCount, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				encoder.WriteFixedStruct(ReferencedDomains.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ReferencedDomains.value.value);
			}

			encoder.WriteFixedStruct(TranslatedNames.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedNames.value);
			encoder.WriteValue(MappedCount.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarCreateSecret(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_UNICODE_STRING SecretName;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> SecretHandle = new RpcPointer<RpcContextHandle>();
			PolicyHandle = decoder.ReadContextHandle();
			SecretName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref SecretName);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarCreateSecret(PolicyHandle, SecretName, DesiredAccess, SecretHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(SecretHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarOpenAccount(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_SID AccountSid;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> AccountHandle = new RpcPointer<RpcContextHandle>();
			PolicyHandle = decoder.ReadContextHandle();
			AccountSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref AccountSid);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarOpenAccount(PolicyHandle, AccountSid, DesiredAccess, AccountHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(AccountHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarEnumeratePrivilegesAccount(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle AccountHandle;
			RpcPointer<RpcPointer<LSAPR_PRIVILEGE_SET>> Privileges = new RpcPointer<RpcPointer<LSAPR_PRIVILEGE_SET>>();
			AccountHandle = decoder.ReadContextHandle();
			var invokeTask = this._obj.LsarEnumeratePrivilegesAccount(AccountHandle, Privileges, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(Privileges.value);
			if (Privileges.value is not null)
			{
				encoder.WriteConformantStruct(Privileges.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(Privileges.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarAddPrivilegesToAccount(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle AccountHandle;
			LSAPR_PRIVILEGE_SET Privileges;
			AccountHandle = decoder.ReadContextHandle();
			Privileges = decoder.ReadConformantStruct<LSAPR_PRIVILEGE_SET>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<LSAPR_PRIVILEGE_SET>(ref Privileges);
			var invokeTask = this._obj.LsarAddPrivilegesToAccount(AccountHandle, Privileges, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarRemovePrivilegesFromAccount(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle AccountHandle;
			byte AllPrivileges;
			RpcPointer<LSAPR_PRIVILEGE_SET> Privileges;
			AccountHandle = decoder.ReadContextHandle();
			AllPrivileges = decoder.ReadUnsignedChar();
			Privileges = decoder.ReadUniquePointer<LSAPR_PRIVILEGE_SET>();
			if (Privileges is not null)
			{
				Privileges.value = decoder.ReadConformantStruct<LSAPR_PRIVILEGE_SET>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<LSAPR_PRIVILEGE_SET>(ref Privileges.value);
			}

			var invokeTask = this._obj.LsarRemovePrivilegesFromAccount(AccountHandle, AllPrivileges, Privileges, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum21NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum21NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum22NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum22NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarGetSystemAccessAccount(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle AccountHandle;
			RpcPointer<uint> SystemAccess = new RpcPointer<uint>();
			AccountHandle = decoder.ReadContextHandle();
			var invokeTask = this._obj.LsarGetSystemAccessAccount(AccountHandle, SystemAccess, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(SystemAccess.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarSetSystemAccessAccount(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle AccountHandle;
			uint SystemAccess;
			AccountHandle = decoder.ReadContextHandle();
			SystemAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarSetSystemAccessAccount(AccountHandle, SystemAccess, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarOpenTrustedDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_SID TrustedDomainSid;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> TrustedDomainHandle = new RpcPointer<RpcContextHandle>();
			PolicyHandle = decoder.ReadContextHandle();
			TrustedDomainSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref TrustedDomainSid);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarOpenTrustedDomain(PolicyHandle, TrustedDomainSid, DesiredAccess, TrustedDomainHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(TrustedDomainHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarQueryInfoTrustedDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle TrustedDomainHandle;
			TRUSTED_INFORMATION_CLASS InformationClass;
			RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation = new RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>>();
			TrustedDomainHandle = decoder.ReadContextHandle();
			InformationClass = (TRUSTED_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.LsarQueryInfoTrustedDomain(TrustedDomainHandle, InformationClass, TrustedDomainInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(TrustedDomainInformation.value);
			if (TrustedDomainInformation.value is not null)
			{
				encoder.WriteUnion(TrustedDomainInformation.value.value);
				encoder.WriteStructDeferral(TrustedDomainInformation.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarSetInformationTrustedDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle TrustedDomainHandle;
			TRUSTED_INFORMATION_CLASS InformationClass;
			LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation;
			TrustedDomainHandle = decoder.ReadContextHandle();
			InformationClass = (TRUSTED_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			TrustedDomainInformation = decoder.ReadUnion<LSAPR_TRUSTED_DOMAIN_INFO>();
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFO>(ref TrustedDomainInformation);
			var invokeTask = this._obj.LsarSetInformationTrustedDomain(TrustedDomainHandle, InformationClass, TrustedDomainInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarOpenSecret(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_UNICODE_STRING SecretName;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> SecretHandle = new RpcPointer<RpcContextHandle>();
			PolicyHandle = decoder.ReadContextHandle();
			SecretName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref SecretName);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarOpenSecret(PolicyHandle, SecretName, DesiredAccess, SecretHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(SecretHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarSetSecret(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle SecretHandle;
			RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedCurrentValue;
			RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedOldValue;
			SecretHandle = decoder.ReadContextHandle();
			EncryptedCurrentValue = decoder.ReadUniquePointer<LSAPR_CR_CIPHER_VALUE>();
			if (EncryptedCurrentValue is not null)
			{
				EncryptedCurrentValue.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedCurrentValue.value);
			}

			EncryptedOldValue = decoder.ReadUniquePointer<LSAPR_CR_CIPHER_VALUE>();
			if (EncryptedOldValue is not null)
			{
				EncryptedOldValue.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedOldValue.value);
			}

			var invokeTask = this._obj.LsarSetSecret(SecretHandle, EncryptedCurrentValue, EncryptedOldValue, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarQuerySecret(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle SecretHandle;
			RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedCurrentValue;
			RpcPointer<ms_dtyp.LARGE_INTEGER> CurrentValueSetTime;
			RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedOldValue;
			RpcPointer<ms_dtyp.LARGE_INTEGER> OldValueSetTime;
			SecretHandle = decoder.ReadContextHandle();
			EncryptedCurrentValue = decoder.ReadUniquePointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>();
			if (EncryptedCurrentValue is not null)
			{
				EncryptedCurrentValue.value = decoder.ReadUniquePointer<LSAPR_CR_CIPHER_VALUE>();
				if (EncryptedCurrentValue.value is not null)
				{
					EncryptedCurrentValue.value.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedCurrentValue.value.value);
				}
			}

			CurrentValueSetTime = decoder.ReadUniquePointer<ms_dtyp.LARGE_INTEGER>();
			if (CurrentValueSetTime is not null)
			{
				CurrentValueSetTime.value = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref CurrentValueSetTime.value);
			}

			EncryptedOldValue = decoder.ReadUniquePointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>();
			if (EncryptedOldValue is not null)
			{
				EncryptedOldValue.value = decoder.ReadUniquePointer<LSAPR_CR_CIPHER_VALUE>();
				if (EncryptedOldValue.value is not null)
				{
					EncryptedOldValue.value.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedOldValue.value.value);
				}
			}

			OldValueSetTime = decoder.ReadUniquePointer<ms_dtyp.LARGE_INTEGER>();
			if (OldValueSetTime is not null)
			{
				OldValueSetTime.value = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref OldValueSetTime.value);
			}

			var invokeTask = this._obj.LsarQuerySecret(SecretHandle, EncryptedCurrentValue, CurrentValueSetTime, EncryptedOldValue, OldValueSetTime, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(EncryptedCurrentValue);
			if (EncryptedCurrentValue is not null)
			{
				encoder.WriteUniquePointer(EncryptedCurrentValue.value);
				if (EncryptedCurrentValue.value is not null)
				{
					encoder.WriteFixedStruct(EncryptedCurrentValue.value.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(EncryptedCurrentValue.value.value);
				}
			}

			encoder.WriteUniquePointer(CurrentValueSetTime);
			if (CurrentValueSetTime is not null)
			{
				encoder.WriteFixedStruct(CurrentValueSetTime.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(CurrentValueSetTime.value);
			}

			encoder.WriteUniquePointer(EncryptedOldValue);
			if (EncryptedOldValue is not null)
			{
				encoder.WriteUniquePointer(EncryptedOldValue.value);
				if (EncryptedOldValue.value is not null)
				{
					encoder.WriteFixedStruct(EncryptedOldValue.value.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(EncryptedOldValue.value.value);
				}
			}

			encoder.WriteUniquePointer(OldValueSetTime);
			if (OldValueSetTime is not null)
			{
				encoder.WriteFixedStruct(OldValueSetTime.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(OldValueSetTime.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarLookupPrivilegeValue(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_UNICODE_STRING Name;
			RpcPointer<ms_dtyp.LUID> Value = new RpcPointer<ms_dtyp.LUID>();
			PolicyHandle = decoder.ReadContextHandle();
			Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name);
			var invokeTask = this._obj.LsarLookupPrivilegeValue(PolicyHandle, Name, Value, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(Value.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(Value.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarLookupPrivilegeName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.LUID Value;
			RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> Name = new RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
			PolicyHandle = decoder.ReadContextHandle();
			Value = decoder.ReadFixedStruct<ms_dtyp.LUID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.LUID>(ref Value);
			var invokeTask = this._obj.LsarLookupPrivilegeName(PolicyHandle, Value, Name, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(Name.value);
			if (Name.value is not null)
			{
				encoder.WriteFixedStruct(Name.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(Name.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarLookupPrivilegeDisplayName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_UNICODE_STRING Name;
			short ClientLanguage;
			short ClientSystemDefaultLanguage;
			RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> DisplayName = new RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
			RpcPointer<ushort> LanguageReturned = new RpcPointer<ushort>();
			PolicyHandle = decoder.ReadContextHandle();
			Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name);
			ClientLanguage = decoder.ReadInt16();
			ClientSystemDefaultLanguage = decoder.ReadInt16();
			var invokeTask = this._obj.LsarLookupPrivilegeDisplayName(PolicyHandle, Name, ClientLanguage, ClientSystemDefaultLanguage, DisplayName, LanguageReturned, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(DisplayName.value);
			if (DisplayName.value is not null)
			{
				encoder.WriteFixedStruct(DisplayName.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(DisplayName.value.value);
			}

			encoder.WriteValue(LanguageReturned.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarDeleteObject(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> ObjectHandle;
			ObjectHandle = new RpcPointer<RpcContextHandle>();
			ObjectHandle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.LsarDeleteObject(ObjectHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(ObjectHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarEnumerateAccountsWithUserRight(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> UserRight;
			RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> EnumerationBuffer = new RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER>();
			PolicyHandle = decoder.ReadContextHandle();
			UserRight = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
			if (UserRight is not null)
			{
				UserRight.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref UserRight.value);
			}

			var invokeTask = this._obj.LsarEnumerateAccountsWithUserRight(PolicyHandle, UserRight, EnumerationBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(EnumerationBuffer.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(EnumerationBuffer.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarEnumerateAccountRights(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_SID AccountSid;
			RpcPointer<LSAPR_USER_RIGHT_SET> UserRights = new RpcPointer<LSAPR_USER_RIGHT_SET>();
			PolicyHandle = decoder.ReadContextHandle();
			AccountSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref AccountSid);
			var invokeTask = this._obj.LsarEnumerateAccountRights(PolicyHandle, AccountSid, UserRights, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(UserRights.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(UserRights.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarAddAccountRights(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_SID AccountSid;
			LSAPR_USER_RIGHT_SET UserRights;
			PolicyHandle = decoder.ReadContextHandle();
			AccountSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref AccountSid);
			UserRights = decoder.ReadFixedStruct<LSAPR_USER_RIGHT_SET>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_USER_RIGHT_SET>(ref UserRights);
			var invokeTask = this._obj.LsarAddAccountRights(PolicyHandle, AccountSid, UserRights, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarRemoveAccountRights(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_SID AccountSid;
			byte AllRights;
			LSAPR_USER_RIGHT_SET UserRights;
			PolicyHandle = decoder.ReadContextHandle();
			AccountSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref AccountSid);
			AllRights = decoder.ReadUnsignedChar();
			UserRights = decoder.ReadFixedStruct<LSAPR_USER_RIGHT_SET>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_USER_RIGHT_SET>(ref UserRights);
			var invokeTask = this._obj.LsarRemoveAccountRights(PolicyHandle, AccountSid, AllRights, UserRights, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarQueryTrustedDomainInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_SID TrustedDomainSid;
			TRUSTED_INFORMATION_CLASS InformationClass;
			RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation = new RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>>();
			PolicyHandle = decoder.ReadContextHandle();
			TrustedDomainSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref TrustedDomainSid);
			InformationClass = (TRUSTED_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.LsarQueryTrustedDomainInfo(PolicyHandle, TrustedDomainSid, InformationClass, TrustedDomainInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(TrustedDomainInformation.value);
			if (TrustedDomainInformation.value is not null)
			{
				encoder.WriteUnion(TrustedDomainInformation.value.value);
				encoder.WriteStructDeferral(TrustedDomainInformation.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarSetTrustedDomainInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_SID TrustedDomainSid;
			TRUSTED_INFORMATION_CLASS InformationClass;
			LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation;
			PolicyHandle = decoder.ReadContextHandle();
			TrustedDomainSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref TrustedDomainSid);
			InformationClass = (TRUSTED_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			TrustedDomainInformation = decoder.ReadUnion<LSAPR_TRUSTED_DOMAIN_INFO>();
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFO>(ref TrustedDomainInformation);
			var invokeTask = this._obj.LsarSetTrustedDomainInfo(PolicyHandle, TrustedDomainSid, InformationClass, TrustedDomainInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarDeleteTrustedDomain(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_SID TrustedDomainSid;
			PolicyHandle = decoder.ReadContextHandle();
			TrustedDomainSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref TrustedDomainSid);
			var invokeTask = this._obj.LsarDeleteTrustedDomain(PolicyHandle, TrustedDomainSid, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarStorePrivateData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_UNICODE_STRING KeyName;
			RpcPointer<LSAPR_CR_CIPHER_VALUE> EncryptedData;
			PolicyHandle = decoder.ReadContextHandle();
			KeyName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref KeyName);
			EncryptedData = decoder.ReadUniquePointer<LSAPR_CR_CIPHER_VALUE>();
			if (EncryptedData is not null)
			{
				EncryptedData.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedData.value);
			}

			var invokeTask = this._obj.LsarStorePrivateData(PolicyHandle, KeyName, EncryptedData, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarRetrievePrivateData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_UNICODE_STRING KeyName;
			RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> EncryptedData;
			PolicyHandle = decoder.ReadContextHandle();
			KeyName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref KeyName);
			EncryptedData = new RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>();
			EncryptedData.value = decoder.ReadUniquePointer<LSAPR_CR_CIPHER_VALUE>();
			if (EncryptedData.value is not null)
			{
				EncryptedData.value.value = decoder.ReadFixedStruct<LSAPR_CR_CIPHER_VALUE>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<LSAPR_CR_CIPHER_VALUE>(ref EncryptedData.value.value);
			}

			var invokeTask = this._obj.LsarRetrievePrivateData(PolicyHandle, KeyName, EncryptedData, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(EncryptedData.value);
			if (EncryptedData.value is not null)
			{
				encoder.WriteFixedStruct(EncryptedData.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(EncryptedData.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarOpenPolicy2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string SystemName;
			LSAPR_OBJECT_ATTRIBUTES ObjectAttributes;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> PolicyHandle = new RpcPointer<RpcContextHandle>();
			if (decoder.ReadReferentId() == 0)
				SystemName = null;
			else
				SystemName = decoder.ReadWideCharString();
			ObjectAttributes = decoder.ReadFixedStruct<LSAPR_OBJECT_ATTRIBUTES>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_OBJECT_ATTRIBUTES>(ref ObjectAttributes);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarOpenPolicy2(SystemName, ObjectAttributes, DesiredAccess, PolicyHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(PolicyHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarGetUserName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string SystemName;
			RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> UserName;
			RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> DomainName;
			if (decoder.ReadReferentId() == 0)
				SystemName = null;
			else
				SystemName = decoder.ReadWideCharString();
			UserName = new RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
			UserName.value = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
			if (UserName.value is not null)
			{
				UserName.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref UserName.value.value);
			}

			DomainName = decoder.ReadUniquePointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
			if (DomainName is not null)
			{
				DomainName.value = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
				if (DomainName.value is not null)
				{
					DomainName.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref DomainName.value.value);
				}
			}

			var invokeTask = this._obj.LsarGetUserName(SystemName, UserName, DomainName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(UserName.value);
			if (UserName.value is not null)
			{
				encoder.WriteFixedStruct(UserName.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(UserName.value.value);
			}

			encoder.WriteUniquePointer(DomainName);
			if (DomainName is not null)
			{
				encoder.WriteUniquePointer(DomainName.value);
				if (DomainName.value is not null)
				{
					encoder.WriteFixedStruct(DomainName.value.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(DomainName.value.value);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarQueryInformationPolicy2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			POLICY_INFORMATION_CLASS InformationClass;
			RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>> PolicyInformation = new RpcPointer<RpcPointer<LSAPR_POLICY_INFORMATION>>();
			PolicyHandle = decoder.ReadContextHandle();
			InformationClass = (POLICY_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.LsarQueryInformationPolicy2(PolicyHandle, InformationClass, PolicyInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(PolicyInformation.value);
			if (PolicyInformation.value is not null)
			{
				encoder.WriteUnion(PolicyInformation.value.value);
				encoder.WriteStructDeferral(PolicyInformation.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarSetInformationPolicy2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			POLICY_INFORMATION_CLASS InformationClass;
			LSAPR_POLICY_INFORMATION PolicyInformation;
			PolicyHandle = decoder.ReadContextHandle();
			InformationClass = (POLICY_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			PolicyInformation = decoder.ReadUnion<LSAPR_POLICY_INFORMATION>();
			decoder.ReadStructDeferral<LSAPR_POLICY_INFORMATION>(ref PolicyInformation);
			var invokeTask = this._obj.LsarSetInformationPolicy2(PolicyHandle, InformationClass, PolicyInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarQueryTrustedDomainInfoByName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_UNICODE_STRING TrustedDomainName;
			TRUSTED_INFORMATION_CLASS InformationClass;
			RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>> TrustedDomainInformation = new RpcPointer<RpcPointer<LSAPR_TRUSTED_DOMAIN_INFO>>();
			PolicyHandle = decoder.ReadContextHandle();
			TrustedDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref TrustedDomainName);
			InformationClass = (TRUSTED_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.LsarQueryTrustedDomainInfoByName(PolicyHandle, TrustedDomainName, InformationClass, TrustedDomainInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(TrustedDomainInformation.value);
			if (TrustedDomainInformation.value is not null)
			{
				encoder.WriteUnion(TrustedDomainInformation.value.value);
				encoder.WriteStructDeferral(TrustedDomainInformation.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarSetTrustedDomainInfoByName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_UNICODE_STRING TrustedDomainName;
			TRUSTED_INFORMATION_CLASS InformationClass;
			LSAPR_TRUSTED_DOMAIN_INFO TrustedDomainInformation;
			PolicyHandle = decoder.ReadContextHandle();
			TrustedDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref TrustedDomainName);
			InformationClass = (TRUSTED_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			TrustedDomainInformation = decoder.ReadUnion<LSAPR_TRUSTED_DOMAIN_INFO>();
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFO>(ref TrustedDomainInformation);
			var invokeTask = this._obj.LsarSetTrustedDomainInfoByName(PolicyHandle, TrustedDomainName, InformationClass, TrustedDomainInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarEnumerateTrustedDomainsEx(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
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
			encoder.WriteFixedStruct(EnumerationBuffer.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(EnumerationBuffer.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarCreateTrustedDomainEx(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInformation;
			LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION AuthenticationInformation;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> TrustedDomainHandle = new RpcPointer<RpcContextHandle>();
			PolicyHandle = decoder.ReadContextHandle();
			TrustedDomainInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(ref TrustedDomainInformation);
			AuthenticationInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION>(ref AuthenticationInformation);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarCreateTrustedDomainEx(PolicyHandle, TrustedDomainInformation, AuthenticationInformation, DesiredAccess, TrustedDomainHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(TrustedDomainHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum52NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum52NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarQueryDomainInformationPolicy(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			POLICY_DOMAIN_INFORMATION_CLASS InformationClass;
			RpcPointer<RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION>> PolicyDomainInformation = new RpcPointer<RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION>>();
			PolicyHandle = decoder.ReadContextHandle();
			InformationClass = (POLICY_DOMAIN_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.LsarQueryDomainInformationPolicy(PolicyHandle, InformationClass, PolicyDomainInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(PolicyDomainInformation.value);
			if (PolicyDomainInformation.value is not null)
			{
				encoder.WriteUnion(PolicyDomainInformation.value.value);
				encoder.WriteStructDeferral(PolicyDomainInformation.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarSetDomainInformationPolicy(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			POLICY_DOMAIN_INFORMATION_CLASS InformationClass;
			RpcPointer<LSAPR_POLICY_DOMAIN_INFORMATION> PolicyDomainInformation;
			PolicyHandle = decoder.ReadContextHandle();
			InformationClass = (POLICY_DOMAIN_INFORMATION_CLASS)decoder.ReadEnumShortValue();
			PolicyDomainInformation = decoder.ReadUniquePointer<LSAPR_POLICY_DOMAIN_INFORMATION>();
			if (PolicyDomainInformation is not null)
			{
				PolicyDomainInformation.value = decoder.ReadUnion<LSAPR_POLICY_DOMAIN_INFORMATION>();
				decoder.ReadStructDeferral<LSAPR_POLICY_DOMAIN_INFORMATION>(ref PolicyDomainInformation.value);
			}

			var invokeTask = this._obj.LsarSetDomainInformationPolicy(PolicyHandle, InformationClass, PolicyDomainInformation, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarOpenTrustedDomainByName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_UNICODE_STRING TrustedDomainName;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> TrustedDomainHandle = new RpcPointer<RpcContextHandle>();
			PolicyHandle = decoder.ReadContextHandle();
			TrustedDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref TrustedDomainName);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarOpenTrustedDomainByName(PolicyHandle, TrustedDomainName, DesiredAccess, TrustedDomainHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(TrustedDomainHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum56NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum56NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarLookupSids2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			LSAPR_SID_ENUM_BUFFER SidEnumBuffer;
			RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
			RpcPointer<LSAPR_TRANSLATED_NAMES_EX> TranslatedNames;
			LSAP_LOOKUP_LEVEL LookupLevel;
			RpcPointer<uint> MappedCount;
			uint LookupOptions;
			uint ClientRevision;
			PolicyHandle = decoder.ReadContextHandle();
			SidEnumBuffer = decoder.ReadFixedStruct<LSAPR_SID_ENUM_BUFFER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_SID_ENUM_BUFFER>(ref SidEnumBuffer);
			TranslatedNames = new RpcPointer<LSAPR_TRANSLATED_NAMES_EX>();
			TranslatedNames.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAMES_EX>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAMES_EX>(ref TranslatedNames.value);
			LookupLevel = (LSAP_LOOKUP_LEVEL)decoder.ReadEnumShortValue();
			MappedCount = new RpcPointer<uint>();
			MappedCount.value = decoder.ReadUInt32();
			LookupOptions = decoder.ReadUInt32();
			ClientRevision = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarLookupSids2(PolicyHandle, SidEnumBuffer, ReferencedDomains, TranslatedNames, LookupLevel, MappedCount, LookupOptions, ClientRevision, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				encoder.WriteFixedStruct(ReferencedDomains.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ReferencedDomains.value.value);
			}

			encoder.WriteFixedStruct(TranslatedNames.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedNames.value);
			encoder.WriteValue(MappedCount.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarLookupNames2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
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
			for (int i = 0; i < Names.Length; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
				elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				Names[i] = elem_0;
			}

			for (int i = 0; i < Names.Length; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
				Names[i] = elem_0;
			}

			TranslatedSids = new RpcPointer<LSAPR_TRANSLATED_SIDS_EX>();
			TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS_EX>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS_EX>(ref TranslatedSids.value);
			LookupLevel = (LSAP_LOOKUP_LEVEL)decoder.ReadEnumShortValue();
			MappedCount = new RpcPointer<uint>();
			MappedCount.value = decoder.ReadUInt32();
			LookupOptions = decoder.ReadUInt32();
			ClientRevision = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarLookupNames2(PolicyHandle, Count, Names, ReferencedDomains, TranslatedSids, LookupLevel, MappedCount, LookupOptions, ClientRevision, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				encoder.WriteFixedStruct(ReferencedDomains.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ReferencedDomains.value.value);
			}

			encoder.WriteFixedStruct(TranslatedSids.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedSids.value);
			encoder.WriteValue(MappedCount.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarCreateTrustedDomainEx2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			LSAPR_TRUSTED_DOMAIN_INFORMATION_EX TrustedDomainInformation;
			LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL AuthenticationInformation;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> TrustedDomainHandle = new RpcPointer<RpcContextHandle>();
			PolicyHandle = decoder.ReadContextHandle();
			TrustedDomainInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_INFORMATION_EX>(ref TrustedDomainInformation);
			AuthenticationInformation = decoder.ReadFixedStruct<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRUSTED_DOMAIN_AUTH_INFORMATION_INTERNAL>(ref AuthenticationInformation);
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarCreateTrustedDomainEx2(PolicyHandle, TrustedDomainInformation, AuthenticationInformation, DesiredAccess, TrustedDomainHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(TrustedDomainHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum60NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum60NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum61NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum61NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum62NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum62NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum63NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum63NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum64NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum64NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum65NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum65NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum66NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum66NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum67NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum67NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarLookupNames3(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
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
			for (int i = 0; i < Names.Length; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
				elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				Names[i] = elem_0;
			}

			for (int i = 0; i < Names.Length; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
				Names[i] = elem_0;
			}

			TranslatedSids = new RpcPointer<LSAPR_TRANSLATED_SIDS_EX2>();
			TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS_EX2>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS_EX2>(ref TranslatedSids.value);
			LookupLevel = (LSAP_LOOKUP_LEVEL)decoder.ReadEnumShortValue();
			MappedCount = new RpcPointer<uint>();
			MappedCount.value = decoder.ReadUInt32();
			LookupOptions = decoder.ReadUInt32();
			ClientRevision = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarLookupNames3(PolicyHandle, Count, Names, ReferencedDomains, TranslatedSids, LookupLevel, MappedCount, LookupOptions, ClientRevision, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				encoder.WriteFixedStruct(ReferencedDomains.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ReferencedDomains.value.value);
			}

			encoder.WriteFixedStruct(TranslatedSids.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedSids.value);
			encoder.WriteValue(MappedCount.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum69NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum69NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum70NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum70NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum71NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum71NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum72NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum72NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarQueryForestTrustInformation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_UNICODE_STRING TrustedDomainName;
			LSA_FOREST_TRUST_RECORD_TYPE HighestRecordType;
			RpcPointer<RpcPointer<LSA_FOREST_TRUST_INFORMATION>> ForestTrustInfo = new RpcPointer<RpcPointer<LSA_FOREST_TRUST_INFORMATION>>();
			PolicyHandle = decoder.ReadContextHandle();
			TrustedDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref TrustedDomainName);
			HighestRecordType = (LSA_FOREST_TRUST_RECORD_TYPE)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.LsarQueryForestTrustInformation(PolicyHandle, TrustedDomainName, HighestRecordType, ForestTrustInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ForestTrustInfo.value);
			if (ForestTrustInfo.value is not null)
			{
				encoder.WriteFixedStruct(ForestTrustInfo.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ForestTrustInfo.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarSetForestTrustInformation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle PolicyHandle;
			ms_dtyp.RPC_UNICODE_STRING TrustedDomainName;
			LSA_FOREST_TRUST_RECORD_TYPE HighestRecordType;
			LSA_FOREST_TRUST_INFORMATION ForestTrustInfo;
			byte CheckOnly;
			RpcPointer<RpcPointer<LSA_FOREST_TRUST_COLLISION_INFORMATION>> CollisionInfo = new RpcPointer<RpcPointer<LSA_FOREST_TRUST_COLLISION_INFORMATION>>();
			PolicyHandle = decoder.ReadContextHandle();
			TrustedDomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref TrustedDomainName);
			HighestRecordType = (LSA_FOREST_TRUST_RECORD_TYPE)decoder.ReadEnumShortValue();
			ForestTrustInfo = decoder.ReadFixedStruct<LSA_FOREST_TRUST_INFORMATION>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSA_FOREST_TRUST_INFORMATION>(ref ForestTrustInfo);
			CheckOnly = decoder.ReadUnsignedChar();
			var invokeTask = this._obj.LsarSetForestTrustInformation(PolicyHandle, TrustedDomainName, HighestRecordType, ForestTrustInfo, CheckOnly, CollisionInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(CollisionInfo.value);
			if (CollisionInfo.value is not null)
			{
				encoder.WriteFixedStruct(CollisionInfo.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(CollisionInfo.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum75NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum75NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarLookupSids3(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			LSAPR_SID_ENUM_BUFFER SidEnumBuffer;
			RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> ReferencedDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
			RpcPointer<LSAPR_TRANSLATED_NAMES_EX> TranslatedNames;
			LSAP_LOOKUP_LEVEL LookupLevel;
			RpcPointer<uint> MappedCount;
			uint LookupOptions;
			uint ClientRevision;
			SidEnumBuffer = decoder.ReadFixedStruct<LSAPR_SID_ENUM_BUFFER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_SID_ENUM_BUFFER>(ref SidEnumBuffer);
			TranslatedNames = new RpcPointer<LSAPR_TRANSLATED_NAMES_EX>();
			TranslatedNames.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_NAMES_EX>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_NAMES_EX>(ref TranslatedNames.value);
			LookupLevel = (LSAP_LOOKUP_LEVEL)decoder.ReadEnumShortValue();
			MappedCount = new RpcPointer<uint>();
			MappedCount.value = decoder.ReadUInt32();
			LookupOptions = decoder.ReadUInt32();
			ClientRevision = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarLookupSids3(SidEnumBuffer, ReferencedDomains, TranslatedNames, LookupLevel, MappedCount, LookupOptions, ClientRevision, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				encoder.WriteFixedStruct(ReferencedDomains.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ReferencedDomains.value.value);
			}

			encoder.WriteFixedStruct(TranslatedNames.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedNames.value);
			encoder.WriteValue(MappedCount.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_LsarLookupNames4(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
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
			for (int i = 0; i < Names.Length; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
				elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				Names[i] = elem_0;
			}

			for (int i = 0; i < Names.Length; i++)
			{
				ms_dtyp.RPC_UNICODE_STRING elem_0 = Names[i];
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
				Names[i] = elem_0;
			}

			TranslatedSids = new RpcPointer<LSAPR_TRANSLATED_SIDS_EX2>();
			TranslatedSids.value = decoder.ReadFixedStruct<LSAPR_TRANSLATED_SIDS_EX2>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<LSAPR_TRANSLATED_SIDS_EX2>(ref TranslatedSids.value);
			LookupLevel = (LSAP_LOOKUP_LEVEL)decoder.ReadEnumShortValue();
			MappedCount = new RpcPointer<uint>();
			MappedCount.value = decoder.ReadUInt32();
			LookupOptions = decoder.ReadUInt32();
			ClientRevision = decoder.ReadUInt32();
			var invokeTask = this._obj.LsarLookupNames4(Count, Names, ReferencedDomains, TranslatedSids, LookupLevel, MappedCount, LookupOptions, ClientRevision, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ReferencedDomains.value);
			if (ReferencedDomains.value is not null)
			{
				encoder.WriteFixedStruct(ReferencedDomains.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ReferencedDomains.value.value);
			}

			encoder.WriteFixedStruct(TranslatedSids.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TranslatedSids.value);
			encoder.WriteValue(MappedCount.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("12345778-1234-abcd-ef00-0123456789ab");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private lsarpc _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public lsarpcStub(lsarpc obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_LsarClose, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_LsarEnumeratePrivileges, this.Invoke_LsarQuerySecurityObject, this.Invoke_LsarSetSecurityObject, this.Invoke_Opnum5NotUsedOnWire, this.Invoke_LsarOpenPolicy, this.Invoke_LsarQueryInformationPolicy, this.Invoke_LsarSetInformationPolicy, this.Invoke_Opnum9NotUsedOnWire, this.Invoke_LsarCreateAccount, this.Invoke_LsarEnumerateAccounts, this.Invoke_LsarCreateTrustedDomain, this.Invoke_LsarEnumerateTrustedDomains, this.Invoke_LsarLookupNames, this.Invoke_LsarLookupSids, this.Invoke_LsarCreateSecret, this.Invoke_LsarOpenAccount, this.Invoke_LsarEnumeratePrivilegesAccount, this.Invoke_LsarAddPrivilegesToAccount, this.Invoke_LsarRemovePrivilegesFromAccount, this.Invoke_Opnum21NotUsedOnWire, this.Invoke_Opnum22NotUsedOnWire, this.Invoke_LsarGetSystemAccessAccount, this.Invoke_LsarSetSystemAccessAccount, this.Invoke_LsarOpenTrustedDomain, this.Invoke_LsarQueryInfoTrustedDomain, this.Invoke_LsarSetInformationTrustedDomain, this.Invoke_LsarOpenSecret, this.Invoke_LsarSetSecret, this.Invoke_LsarQuerySecret, this.Invoke_LsarLookupPrivilegeValue, this.Invoke_LsarLookupPrivilegeName, this.Invoke_LsarLookupPrivilegeDisplayName, this.Invoke_LsarDeleteObject, this.Invoke_LsarEnumerateAccountsWithUserRight, this.Invoke_LsarEnumerateAccountRights, this.Invoke_LsarAddAccountRights, this.Invoke_LsarRemoveAccountRights, this.Invoke_LsarQueryTrustedDomainInfo, this.Invoke_LsarSetTrustedDomainInfo, this.Invoke_LsarDeleteTrustedDomain, this.Invoke_LsarStorePrivateData, this.Invoke_LsarRetrievePrivateData, this.Invoke_LsarOpenPolicy2, this.Invoke_LsarGetUserName, this.Invoke_LsarQueryInformationPolicy2, this.Invoke_LsarSetInformationPolicy2, this.Invoke_LsarQueryTrustedDomainInfoByName, this.Invoke_LsarSetTrustedDomainInfoByName, this.Invoke_LsarEnumerateTrustedDomainsEx, this.Invoke_LsarCreateTrustedDomainEx, this.Invoke_Opnum52NotUsedOnWire, this.Invoke_LsarQueryDomainInformationPolicy, this.Invoke_LsarSetDomainInformationPolicy, this.Invoke_LsarOpenTrustedDomainByName, this.Invoke_Opnum56NotUsedOnWire, this.Invoke_LsarLookupSids2, this.Invoke_LsarLookupNames2, this.Invoke_LsarCreateTrustedDomainEx2, this.Invoke_Opnum60NotUsedOnWire, this.Invoke_Opnum61NotUsedOnWire, this.Invoke_Opnum62NotUsedOnWire, this.Invoke_Opnum63NotUsedOnWire, this.Invoke_Opnum64NotUsedOnWire, this.Invoke_Opnum65NotUsedOnWire, this.Invoke_Opnum66NotUsedOnWire, this.Invoke_Opnum67NotUsedOnWire, this.Invoke_LsarLookupNames3, this.Invoke_Opnum69NotUsedOnWire, this.Invoke_Opnum70NotUsedOnWire, this.Invoke_Opnum71NotUsedOnWire, this.Invoke_Opnum72NotUsedOnWire, this.Invoke_LsarQueryForestTrustInformation, this.Invoke_LsarSetForestTrustInformation, this.Invoke_Opnum75NotUsedOnWire, this.Invoke_LsarLookupSids3, this.Invoke_LsarLookupNames4};
		}
	}
}