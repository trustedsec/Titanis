using KerberosV5Spec2;
using ms_dtyp;
using ms_pac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.DceRpc;
using Titanis.IO;
using KerberosV5Spec2;
using Titanis.Winterop.Security;
using Microsoft.Win32.SafeHandles;
using System.Buffers.Binary;
using System.Linq;
using Titanis.Ldap;
using Titanis.PduStruct;
using System.Security;

namespace Titanis.Security.Kerberos
{
	// [MS-PAC] § 2.14 PAC_ATTRIBUTES_INFO
	public enum PacAttributeFlags
	{
		None = 0,
		WasRequested = 1,
		WasGivenImplicitly = 2,
	}

	internal enum AdType
	{
		IfRelevant = 1,
		Pac = 128,
	}

	public class TicketAuthorizationData
	{

		public TicketAuthorizationData()
		{
		}

		internal void Process(
			IList<AuthorizationData_Element> authData,
			SessionKey? serverKey,
			SessionKey? asrepKey,
			bool optional = false)
		{
			foreach (var adRec in authData)
			{
				this.Process(adRec, serverKey, asrepKey, optional);
			}
		}

		internal void Process(AuthorizationData_Element adRec, SessionKey? serverKey, SessionKey? asrepKey, bool optional)
		{
			switch ((AdType)adRec.ad_type)
			{
				case AdType.IfRelevant:
					{
						var inner = Asn1DerDecoder.DecodeTlv<Asn1SequenceOf<AuthorizationData_Element>>(adRec.ad_data);
						this.Process(inner.Values, serverKey, asrepKey, true);
						adRec.ad_data = Asn1DerEncoder.EncodeTlv(inner).ToArray();
					}
					break;
				case AdType.Pac:
					this.ProcessPac(adRec.ad_data, asrepKey, serverKey);
					break;
				default:
					break;
			}
		}

		public static byte[] BuildPac(
			// matches EncRepPart.authTime, not LOGON_INFO
			DateTime authTime,
			LogonInfo logonInfo,
			UpnDnsInfo? upnDnsInfo,
			SessionKey serverKey,
			SessionKey? kdcKey,
			EncChecksumType ticketChecksumType,
			byte[]? ticketChecksum
			)
		{
			int bufferCount = 6;
			if (ticketChecksum != null)
				bufferCount++;
			if (kdcKey != null)
				bufferCount += 2;

			PacBuilder builder = new PacBuilder(bufferCount);
			builder.WriteLogonInfo(logonInfo);
			// Server checksum
			var offServerChecksum = builder.WriteServerChecksum(PacBufferType.ServerChecksum, serverKey);
			// KDC checksum
			var offKdcChecksum = (kdcKey != null) ? builder.WriteServerChecksum(PacBufferType.KdcChecksum, serverKey)
				: 0;
			// Client info
			builder.WriteClientInfo(authTime, logonInfo.EffectiveName);
			if (upnDnsInfo != null)
				builder.WriteUpnDnsInfo(upnDnsInfo);

			// TODO: Claims
			//builder.WriteClientClaims();
			if (ticketChecksum != null)
				builder.WriteTicketChecksum(ticketChecksumType, ticketChecksum);

			var offExtKdcChecksum = (kdcKey != null) ? builder.WriteServerChecksum(PacBufferType.ExtendedKdcChecksum, kdcKey)
				: 0;

			var bytes = builder.GetBytes();
			Checksum checksum;
			// Extended KDC
			if (kdcKey != null)
			{
				checksum = kdcKey.Checksum(KeyUsage.NonKerbChecksumSalt, bytes);
				checksum.checksum.CopyTo(bytes.Slice(offExtKdcChecksum));
			}
			// Server
			checksum = serverKey.Checksum(KeyUsage.NonKerbChecksumSalt, bytes);
			checksum.checksum.CopyTo(bytes.Slice(offServerChecksum));
			if (kdcKey != null)
			{
				// KDC
				checksum = serverKey.Checksum(KeyUsage.NonKerbChecksumSalt, bytes);
				checksum.checksum.CopyTo(bytes.Slice(offKdcChecksum));
			}

			return bytes;
		}

#if DEBUG
		private PAC_TYPE _pacBuffers;
#endif

		private void ProcessPac(byte[] authData, SessionKey? asrepKey, SessionKey? serverKey)
		{
			var authDataToSign = (byte[])authData.Clone();
			ByteMemoryReader reader = new(authData);
			var pacBuffers = reader.ReadPduStruct<PAC_TYPE>();
#if DEBUG
			this._pacBuffers = pacBuffers;
#endif

			foreach (var bufferInfo in pacBuffers.Buffers)
			{
				if (bufferInfo.cbBufferSize == 0)
					continue;
				int offBuffer = (int)bufferInfo.Offset;
				reader.Position = offBuffer;

				switch (bufferInfo.ulType)
				{
					case PacBufferType.ServerChecksum:
						ExtractSig(authDataToSign, reader, offBuffer, out this._serverSig, out this._serverSigOffset, true);
						break;
					case PacBufferType.ExtendedKdcChecksum:
						ExtractSig(authDataToSign, reader, offBuffer, out this._extKdcSig, out this._extkdcSigOffset, false);
						break;
					case PacBufferType.KdcChecksum:
						ExtractSig(authDataToSign, reader, offBuffer, out this._kdcSig, out this._kdcSigOffset, true);
						break;
					case PacBufferType.TicketChecksum:
						ExtractSig(authDataToSign, reader, offBuffer, out this._ticketSig, out this._ticketSigOffset, false);
						break;
					case PacBufferType.ClientNameInfo:
						// [MS-PAC] § 2.7 PAC_CLIENT_INFO
						ProcessClientNameInfo(reader);
						break;
					case PacBufferType.UserPrincipalName:
						// [MS-PAC] § 2.10 UPN_DNS_INFO
						ProcessUpn(reader);
						break;
					case PacBufferType.PacAttributes:
						if (bufferInfo.cbBufferSize == 8)
						{
							var cbFlags = reader.ReadInt32LE();
							this.PacAttributeFlags = (PacAttributeFlags)reader.ReadInt32();
						}
						break;
					case PacBufferType.PacRequestorSid:
						this.RequestorSid = new SecurityIdentifier(reader.Consume(bufferInfo.cbBufferSize));
						break;
					case PacBufferType.CredentialInfo:
						// [MS-PAC] Š 2.6 PAC Credentials
						if (asrepKey != null)
							ProcessCredentialInfo(reader, bufferInfo.cbBufferSize, asrepKey);
						break;
					default:
						{
							var buffer = authData.AsMemory(offBuffer, (int)bufferInfo.cbBufferSize);
							var bufferDecoder = RpcEncoding.MsrpcNdr.CreateDecoder(new ByteMemoryReader(buffer), new RpcCallContext(null));

							switch (bufferInfo.ulType)
							{
								case PacBufferType.LogonInfo:
									// [MS-PAC] Š 2.5 KERB_VALIDATION_INFO
									ProcessLogonInfo(bufferDecoder);
									break;
								case PacBufferType.ConstrainedDelegationInfo:
									ProcessConstrainedDelegationInfo(bufferDecoder);
									break;
								default:
									break;
							}
						}
						break;
				}
			}

#if DEBUG
			var serverSig = this._serverSig;
			var serverChecksum = serverKey.Checksum(KeyUsage.NonKerbChecksumSalt, authDataToSign);
			if (!serverChecksum.checksum.SequenceEqual(serverSig.Signature))
				throw new SecurityException("The server checksum does not match");
#endif
		}

		private void ExtractSig(byte[] authData, ByteMemoryReader reader, int offBuffer, out PAC_SIGNATURE_DATA sig, out int sigOffset, bool clear)
		{
			sig = reader.ReadPduStruct<PAC_SIGNATURE_DATA>();
			sigOffset = offBuffer + 4;

			if (clear)
				//authData.Slice(offBuffer, sig.Signature.Length + 4).Clear();
				authData.Slice(sigOffset, sig.Signature.Length).Clear();
		}

		private void ProcessConstrainedDelegationInfo(RpcDecoder bufferDecoder)
		{
			var delgInfo = bufferDecoder.DeserializeType1(static d =>
			{
				var refid = d.ReadReferentId();
				if (refid != 0)
				{
					var delgInfo = new S4U_DELEGATION_INFO();
					delgInfo.Decode(d);
					delgInfo.DecodeDeferrals(d);
					return delgInfo;
				}

				return default;
			});

			this.S4uProxyTarget = delgInfo.S4U2proxyTarget.AsString();
			this.S4uTransitedList = delgInfo.S4UTransitedServices?.ToList(r => r.AsString());
		}

		public UpnDnsInfo? UpnDnsInfo { get; private set; }

		// [MS-PAC] § 2.10 - UPN_DNS_INFO
		private void ProcessUpn(ByteMemoryReader reader)
		{
			UpnDnsInfo info = new UpnDnsInfo
			{
				dnsInfo = reader.ReadPduStruct<UPN_DNS_INFO>()
			};
			this.UpnDnsInfo = info;
		}

		// [MS-PAC] § 2.7 PAC_CLIENT_INFO
		private void ProcessClientNameInfo(ByteMemoryReader reader)
		{
			var clientNameInfo = reader.ReadPduStruct<PAC_CLIENT_INFO>();
			this.ClientName = clientNameInfo.Name;
			this.ClientAuthTime = clientNameInfo.GetLogonTime();
		}

		// [MS-PAC] Š 2.6 PAC Credentials
		private void ProcessCredentialInfo(IByteSource reader, int length, SessionKey? asrepKey)
		{
			int end = (int)reader.Position + length;
			PAC_CREDENTIAL_INFO credInfo = reader.ReadPduStruct<PAC_CREDENTIAL_INFO>();
			var encCredData = reader.Consume(end - (int)reader.Position).ToArray();
			var credDataBytes = asrepKey.Decrypt(KeyUsage.NonKerbSalt, encCredData);

			var decoder = RpcEncoding.MsrpcNdr.CreateDecoder(new ByteMemoryReader(credDataBytes), new RpcCallContext(null));
			var credData = decoder.DeserializeType1(d =>
			{
				var refId = d.ReadReferentId();
				var credData = new PAC_CREDENTIAL_DATA();
				if (refId != 0)
				{
					credData.DecodeHeader(d);
					credData.Decode(d);
					credData.Credentials = new SECPKG_SUPPLEMENTAL_CRED[credData.CredentialCount];
					credData.DecodeConformantArrayField(d);
					credData.DecodeDeferrals(d);

				}
				return credData;
			});

			if (credData.Credentials != null)
			{
				foreach (var cred in credData.Credentials)
				{
					var packageName = cred.PackageName.AsString();
					var credBytes = cred.Credentials.value;
					if (packageName == "NTLM")
					{
						var version = BinaryPrimitives.ReadInt32LittleEndian(credBytes);
						if (version == 0)
						{
							NtlmCredFlags flags = (NtlmCredFlags)BinaryPrimitives.ReadInt32LittleEndian(credBytes.AsSpan(4, 4));
							if (0 != (flags & NtlmCredFlags.LmHashPresent))
								this.LmHash = credBytes.Slice(8, 16).ToArray();
							if (0 != (flags & NtlmCredFlags.NtlmHashPresent))
								this.NtlmHash = credBytes.Slice(8 + 16, 16).ToArray();
						}
					}
				}
			}
		}

		[Flags]
		enum NtlmCredFlags
		{
			None = 0,
			LmHashPresent = 1,
			NtlmHashPresent = 2,
		}
		public byte[]? LmHash { get; set; }
		public byte[]? NtlmHash { get; set; }

		private PAC_SIGNATURE_DATA _serverSig;
		private int _serverSigOffset;

		private PAC_SIGNATURE_DATA _kdcSig;
		private int _kdcSigOffset;

		private PAC_SIGNATURE_DATA _ticketSig;
		private int _ticketSigOffset;
		public EncChecksumType TicketSignatureType { get => this._ticketSig.SignatureType; set => this._ticketSig.SignatureType = value; }
		public byte[] TicketSignature { get => this._ticketSig.Signature; set => this._ticketSig.Signature = value; }

		private PAC_SIGNATURE_DATA _extKdcSig;
		private int _extkdcSigOffset;

		private int _offKdcCheckvsum;
		private int _offTicketChecksum;

		public LogonInfo? LogonInfo { get; private set; }
		public string ClientName { get; private set; }
		public DateTime ClientAuthTime { get; private set; }
		public string? S4uProxyTarget { get; private set; }
		public List<string?>? S4uTransitedList { get; private set; }
		public PacAttributeFlags PacAttributeFlags { get; private set; }
		public SecurityIdentifier? RequestorSid { get; private set; }

		// [MS-PAC] Š 2.5 KERB_VALIDATION_INFO
		private void ProcessLogonInfo(RpcDecoder decoder)
		{
			this.LogonInfo = decoder.DeserializeType1(static d =>
			{
				var ptr = d.ReadReferentId();
				if (ptr != 0)
				{
					var logonInfo = new LogonInfo();
					logonInfo.info.Decode(d);
					logonInfo.info.DecodeDeferrals(d);
					return logonInfo;
				}
				return default;
			});
		}

		public IReadOnlyList<SidWithAttributes> GetSecurityGroups()
		{
			List<SidWithAttributes> sids = [];

			foreach (var groupRid in this.LogonInfo.GroupIds)
			{
				var groupSid = this.LogonInfo.LogonDomainSid.Concat(groupRid.Rid);
				sids.Add(new SidWithAttributes(groupSid, groupRid.Attributes));
			}

			foreach (var groupRid in this.LogonInfo.ResourceGroupIds)
			{
				var groupSid = this.LogonInfo.ResourceGroupDomainSid.Concat(groupRid.Rid);
				sids.Add(new SidWithAttributes(groupSid, groupRid.Attributes));
			}

			var extraSids = this.LogonInfo?.ExtraSids;
			if (extraSids != null)
			{
				sids.AddRange(extraSids);
			}

			return sids;
		}
	}

	public class LogonInfo
	{
		internal KERB_VALIDATION_INFO info;

		public DateTime LogonTime
		{
			get => info.LogonTime.ToDateTime();
			set => info.LogonTime = RpcExtensions.ToFileTime(value);
		}
		public DateTime? LogoffTime
		{
			get => info.LogoffTime.ToDateTimeOrNull();
			set => info.LogoffTime = value.ToFileTime(FileTimeOptions.NullAsForever);
		}
		public DateTime? KickOffTime
		{
			get => info.KickOffTime.ToDateTimeOrNull();
			set => info.KickOffTime = value.ToFileTime(FileTimeOptions.NullAsForever);
		}
		public DateTime? LastSuccessfulLogon
		{
			get => info.LastSuccessfulLogon.ToDateTimeOrNull();
			set => info.LastSuccessfulLogon = value.ToFileTime(FileTimeOptions.NullAsForever);
		}
		public DateTime? LastFailedLogon
		{
			get => info.LastFailedLogon.ToDateTimeOrNull();
			set => info.LastFailedLogon = value.ToFileTime(FileTimeOptions.NullAsForever);
		}
		public DateTime? PasswordLastSet
		{
			get => info.PasswordLastSet.ToDateTimeOrNull();
			set => info.PasswordLastSet = value.ToFileTime(FileTimeOptions.NullAsNever);
		}
		public DateTime? PasswordCanChange
		{
			get => info.PasswordCanChange.ToDateTimeOrNull();
			set => info.PasswordCanChange = value.ToFileTime(FileTimeOptions.NullAsNever);
		}
		public DateTime? PasswordMustChange
		{
			get => info.PasswordMustChange.ToDateTimeOrNull();
			set => info.PasswordMustChange = value.ToFileTime(FileTimeOptions.NullAsForever);
		}

		private string? _effectiveName;
		public string? EffectiveName
		{
			get => (this._effectiveName ??= info.EffectiveName.AsString());
			set => info.EffectiveName = (this._effectiveName = value).ToRpcUnicodeString();
		}
		private string? _FullName;
		public string? FullName
		{
			get => (this._FullName ??= info.FullName.AsString());
			set => info.FullName = (this._FullName = value).ToRpcUnicodeString();
		}
		private string? _LogonScript;
		public string? LogonScript
		{
			get => (this._LogonScript ??= info.LogonScript.AsString());
			set => info.LogonScript = (this._LogonScript = value).ToRpcUnicodeString();
		}
		private string? _ProfilePath;
		public string? ProfilePath
		{
			get => (this._ProfilePath ??= info.ProfilePath.AsString());
			set => info.ProfilePath = (this._ProfilePath = value).ToRpcUnicodeString();
		}
		private string? _HomeDirectory;
		public string? HomeDirectory
		{
			get => (this._HomeDirectory ??= info.HomeDirectory.AsString());
			set => info.HomeDirectory = (this._HomeDirectory = value).ToRpcUnicodeString();
		}
		private string? _HomeDirectoryDrive;
		public string? HomeDirectoryDrive
		{
			get => (this._HomeDirectoryDrive ??= info.HomeDirectoryDrive.AsString());
			set => info.HomeDirectoryDrive = (this._HomeDirectoryDrive = value).ToRpcUnicodeString();
		}
		public int LogonCount { get => info.LogonCount; set => this.info.LogonCount = (ushort)value; }
		public int BadPasswordCount { get => info.BadPasswordCount; set => this.info.BadPasswordCount = (ushort)value; }
		public uint UserId { get => info.UserId; set => this.info.UserId = value; }
		public uint PrimaryGroupId { get => info.PrimaryGroupId; set => this.info.PrimaryGroupId = value; }

		public UserLogonFlags UserFlags { get => (UserLogonFlags)info.UserFlags; set => this.info.UserFlags = (uint)value; }

		private NtlmSessionKey? _userSessionKey;
		public NtlmSessionKey? UserSessionKey
		{
			get => (this._userSessionKey ??= info.UserSessionKey.ToNtlmSessionKey());
			set
			{
				if (value is null)
					value = new NtlmSessionKey();

				this._userSessionKey = value;
				this.info.UserSessionKey = new USER_SESSION_KEY
				{
					data = [
						new CYPHER_BLOCK(){ data=value.Part1 },
						new CYPHER_BLOCK(){ data=value.Part2 },
						]
				};
			}
		}

		private string? _LogonServer;
		public string? LogonServer
		{
			get => (this._LogonServer ??= info.LogonServer.AsString());
			set => info.LogonServer = (this._LogonServer = value).ToRpcUnicodeString();
		}
		private string? _LogonDomainName;
		public string? LogonDomainName
		{
			get => (this._LogonDomainName ??= info.LogonDomainName.AsString());
			set => info.LogonDomainName = (this._LogonDomainName = value).ToRpcUnicodeString();
		}

		private SecurityIdentifier? _logonDomainId;
		public SecurityIdentifier? LogonDomainSid
		{
			get => (this._logonDomainId ??= this.info.LogonDomainId?.ToSid());
			set
			{
				if (value is null)
				{
					this._logonDomainId = null;
				}
				else
				{
					this._logonDomainId = value;
					this.info.LogonDomainId = new RpcPointer<RPC_SID>(value.ToRpcSid());
				}
			}
		}

		private SecurityIdentifier? _userSid;
		public SecurityIdentifier UserSid => (this._userSid ??= (this.LogonDomainSid.Concat(this.UserId)));

		public SamUserAccountFlags UserAccountControl
		{
			get => (SamUserAccountFlags)info.UserAccountControl;
			set => this.info.UserAccountControl = (uint)value;
		}

		#region ExtraSids
		private List<SidWithAttributes>? _extraSids;
		public IReadOnlyList<SidWithAttributes> ExtraSids => (this._extraSids ??= this.info.ExtraSids.ToList(r => new SidWithAttributes(r.Sid.ToSid(), (SidAttributes)r.Attributes)));
		public void SetExtraSids(IEnumerable<SidWithAttributes> extraSids)
		{
			if (SetSids(extraSids, ref this._extraSids, ref this.info.SidCount, ref this.info.ExtraSids))
				this.UserFlags |= UserLogonFlags.HasExtraSids;
			else
				this.UserFlags &= ~UserLogonFlags.HasExtraSids;
		}

		private static bool SetSids(
			IEnumerable<SidWithAttributes> extraSids,
			ref List<SidWithAttributes> sidListField,
			ref uint sidCountField,
			ref RpcPointer<KERB_SID_AND_ATTRIBUTES[]>? rpcListField
			)
		{
			sidListField = extraSids.ToList();
			sidCountField = (uint)sidListField.Count;
			if (sidListField.Count > 0)
			{
				rpcListField = new RpcPointer<KERB_SID_AND_ATTRIBUTES[]>(extraSids.Select(r => new KERB_SID_AND_ATTRIBUTES() { Sid = new RpcPointer<RPC_SID>(r.Sid.ToRpcSid()), Attributes = (uint)r.Attributes }).ToArray());
				return true;
			}
			else
			{
				rpcListField = null;
				return false;
			}
		}
		#endregion

		#region ResourceGroupDomainSid
		public SecurityIdentifier? _resourceGroupSid;
		public SecurityIdentifier? ResourceGroupDomainSid
		{
			get => this._resourceGroupSid ??= this.info.ResourceGroupDomainSid.ToSid();
			set
			{
				if (value is null)
				{
					this._resourceGroupSid = null;
				}
				else
				{
					this._resourceGroupSid = value;
					this.info.ResourceGroupDomainSid = new RpcPointer<RPC_SID>(value.ToRpcSid());
				}
			}
		}
		#endregion

		#region ResourceGroupIds
		private List<RidWithAttributes>? _resGroupIds;
		public IList<RidWithAttributes> ResourceGroupIds => (this._resGroupIds ??= this.info.ResourceGroupIds.ToList(r => new RidWithAttributes(r.RelativeId, (SidAttributes)r.Attributes)));
		public void SetResourceGroupIds(IEnumerable<RidWithAttributes> groups)
		{
			this._resGroupIds = groups.ToList();
			this.UserFlags |= UserLogonFlags.HasResourceGroupIds;
			this.info.ResourceGroupCount = (uint)this._resGroupIds.Count;
			this.info.ResourceGroupIds = new RpcPointer<GROUP_MEMBERSHIP[]>(groups.Select(r => new GROUP_MEMBERSHIP() { Attributes = (uint)r.Attributes, RelativeId = r.Rid }).ToArray());
		}
		#endregion

		#region GroupIds
		private List<RidWithAttributes>? _groupIds;
		public IList<RidWithAttributes> GroupIds => (this._groupIds ??= this.info.GroupIds.ToList(r => new RidWithAttributes(r.RelativeId, (SidAttributes)r.Attributes)));
		public void SetGroupIds(IEnumerable<RidWithAttributes> groups)
		{
			this._groupIds = groups.ToList();
			this.info.GroupCount = (uint)this._groupIds.Count;
			this.info.GroupIds = new RpcPointer<GROUP_MEMBERSHIP[]>(groups.Select(r => new GROUP_MEMBERSHIP() { Attributes = (uint)r.Attributes, RelativeId = r.Rid }).ToArray());
		}
		#endregion
	}

	public class SidWithAttributes
	{
		public SidWithAttributes(SecurityIdentifier sid, SidAttributes attributes)
		{
			ArgumentNullException.ThrowIfNull(sid);
			Sid = sid;
			Attributes = attributes;
		}

		public SecurityIdentifier Sid { get; }
		public SidAttributes Attributes { get; }

		public override string ToString()
		{
			var wks = this.Sid.AsWellKnownSid();
			return ($"{this.Sid} {((wks != WellKnownSid.Unknown) ? $"({wks}) " : null)}: {this.Attributes}");
		}
	}

	public class RidWithAttributes
	{
		public RidWithAttributes(uint rid, SidAttributes attributes)
		{
			Rid = rid;
			Attributes = attributes;
		}

		public uint Rid { get; }
		public SidAttributes Attributes { get; }

		public override string ToString()
			=> $"{this.Rid} : {this.Attributes}";
	}

	[Flags]
	public enum UserLogonFlags : uint
	{
		None = 0,

		Guest = (1 << 0), // A - 31
		NoEncryption = (1 << 1), // B - 30
								 // 0 - 29
		LanmanKeyUsed = (1 << 3), // C - 28
								  // 0 - 27
		HasExtraSids = (1 << 5), // D - 26
		SubauthUsed = (1 << 6), // E - 25
		MachineAccount = (1 << 7), // F - 24
		DomainControllAcceptsNtlmV2 = (1 << 8), // G - 23
		HasResourceGroupIds = (1 << 9), // H - 22
		HasProfilePath = (1 << 10), // I - 21
		NtChallengeResponseUsed = (1 << 11), // J - 20
		LmChallengeResponseUsed = (1 << 12), // K - 19
		LmAndNtChallengeResponseUsed = (1 << 13), // L - 18
	}

	enum FileTimeOptions
	{
		NullAsNever = 0,
		NullAsForever
	}

	static class RpcExtensions
	{

		public static FILETIME Never => new FILETIME();
		public static FILETIME Forever => new FILETIME() { dwLowDateTime = uint.MaxValue, dwHighDateTime = int.MaxValue };

		public static List<TResult> ToList<T, TResult>(this RpcPointer<T[]>? ptr, Converter<T, TResult> converter)
		{
			if (ptr == null) return new List<TResult>();
			else
			{
				var arr = Array.ConvertAll(ptr.value, converter);
				return new List<TResult>(arr);
			}
		}

		public static FILETIME ToFileTime(this DateTime dt)
		{
			var ftvalue = (ulong)dt.ToFileTimeUtc();
			return new FILETIME()
			{
				dwLowDateTime = (uint)(ftvalue & uint.MaxValue),
				dwHighDateTime = (uint)(ftvalue >> 32)
			};
		}
		public static FILETIME ToFileTime(this DateTime? dt, FileTimeOptions options)
			=> dt.HasValue ? ToFileTime(dt.Value)
			: (options == FileTimeOptions.NullAsNever) ? Never
			: Forever;

		public static NtlmSessionKey ToNtlmSessionKey(this USER_SESSION_KEY key)
			=> new NtlmSessionKey(key.data[0].data, key.data[1].data);
	}

	public class NtlmSessionKey
	{
		public NtlmSessionKey()
		{
			this.Part1 = new byte[8];
			this.Part2 = new byte[8];
			this.Key = new byte[16];
		}
		public NtlmSessionKey(byte[] part1, byte[] part2)
		{
			ArgumentNullException.ThrowIfNull(part1);
			if (part1.Length != 8 || part2.Length != 8)
				throw new ArgumentException("Both parts of the key must be 8-byte arrays.");
			ArgumentNullException.ThrowIfNull(part2);

			this.Part1 = part1;
			this.Part2 = part2;

			byte[] key = new byte[16];
			part1.CopyTo(key, 0);
			part2.CopyTo(key, 8);

			this.Key = key;
		}

		public byte[] Key { get; }
		public byte[] Part1 { get; }
		public byte[] Part2 { get; }
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct PAC_CLIENT_INFO
	{
		public int clientId_low;
		public int clientId_hi;
		public ushort NameLength;

		[PduString(System.Runtime.InteropServices.CharSet.Unicode, nameof(NameLength))]
		public string Name;

		public DateTime GetLogonTime()
		{
			long n = (((long)this.clientId_hi) << 32) | (uint)this.clientId_low;
			return DateTime.FromFileTimeUtc(n);
		}
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct PAC_TYPE
	{
		private int bufferCount;
		internal int version;

		[PduArraySize(nameof(bufferCount))]
		private PAC_INFO_BUFFER[] _buffers;

		internal PAC_INFO_BUFFER[] Buffers
		{
			get => this._buffers;
			set
			{
				this._buffers = value;
				this.bufferCount = value?.Length ?? 0;
			}
		}
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct PAC_INFO_BUFFER
	{
		public PacBufferType ulType;
		public int cbBufferSize;
		public ulong Offset;
	}

	[PduStruct]
	partial struct UpnInfoExtension
	{
		internal ushort samNameLength;
		internal ushort samNameOffset;
		internal ushort sidLength;
		internal ushort sidOffset;
	}

	[PduStruct]
	partial struct PAC_SIGNATURE_DATA
	{
		public EncChecksumType SignatureType;

		private int SignatureSize => this.SignatureType switch
		{
			EncChecksumType.HmacMd5String => 128 / 8,
			EncChecksumType.HmacSha1_96_Aes128 => 96 / 8,
			EncChecksumType.HmacSha1_96_Aes256 => 96 / 8,
			_ => 0
		};

		[PduArraySize(nameof(SignatureSize))]
		public byte[] Signature;
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct UPN_DNS_INFO
	{
		[PduPosition]
		private long offStart;

		private ushort UpnLength;
		private ushort UpnOffset;
		private ushort DnsDomainNameLength;
		private ushort DnsDomainNameOffset;
		private UpnDnsInfoFlags Flags;
		private bool HasExtension => 0 != (this.Flags & UpnDnsInfoFlags.HasSidInfo);

		[PduIgnore]
		private string? _upn;
		public string? Upn { get => this._upn; }

		[PduIgnore]
		private string? _dnsDomainName;
		public string? DnsDomainName { get => this._dnsDomainName; set => this._dnsDomainName = value; }

		[PduConditional(nameof(HasExtension))]
		private UpnInfoExtension? _ext;

		[field: PduIgnore]
		public string? SamName { get; set; }
		[field: PduIgnore]
		public SecurityIdentifier? Sid { get; set; }

		partial void OnBeforeWritePdu(ByteWriter writer)
		{
			int startOffset = 10;
			bool hasExt = (this.Sid != null || this.SamName != null);
			this.Flags = hasExt ? UpnDnsInfoFlags.HasSidInfo : UpnDnsInfoFlags.None;
			if (hasExt)
				startOffset += 8;

			startOffset = BinaryHelper.Align(startOffset, 8);

			this.UpnOffset = (ushort)startOffset;
			this.UpnLength = (ushort)((this.Upn != null) ? Encoding.Unicode.GetByteCount(this.Upn) : 0);
			startOffset += this.UpnLength;
			startOffset = BinaryHelper.Align(startOffset, 8);
			this.DnsDomainNameOffset = (ushort)startOffset;
			this.DnsDomainNameLength = (ushort)((this.DnsDomainName != null) ? Encoding.Unicode.GetByteCount(this.DnsDomainName) : 0);
			startOffset += this.DnsDomainNameLength;
			startOffset = BinaryHelper.Align(startOffset, 8);

			if (hasExt)
			{
				var ext = new UpnInfoExtension();

				ext.samNameOffset = (ushort)startOffset;
				if (this.SamName != null)
				{
					ext.samNameLength = (ushort)Encoding.Unicode.GetByteCount(this.SamName);
					startOffset += ext.samNameLength;
					startOffset = BinaryHelper.Align(startOffset, 8);
				}

				ext.sidOffset = (ushort)startOffset;
				if (this.Sid != null)
				{
					ext.sidLength = (ushort)this.Sid.BinaryLength;
					startOffset += ext.sidLength;
					startOffset = BinaryHelper.Align(startOffset, 8);
				}

				this._ext = ext;
			}
		}

		private string? ReadStringFrom(IByteSource reader, int offset, int length)
		{
			if (length > 0)
			{
				reader.Position = this.offStart + offset;
				return reader.ReadStringUni(length);
			}
			return null;

		}
		partial void OnAfterReadPdu<TSource>(TSource reader) where TSource : class, IByteSource
		{
			this._upn = this.ReadStringFrom(reader, this.UpnOffset, this.UpnLength);
			this._dnsDomainName = this.ReadStringFrom(reader, this.DnsDomainNameOffset, this.DnsDomainNameLength);

			if (this.HasExtension)
			{
				var ext = this._ext.Value;
				this.SamName = this.ReadStringFrom(reader, ext.samNameOffset, ext.samNameLength);
				if (ext.sidLength > 0)
				{
					reader.Position = this.offStart + ext.sidOffset;
					this.Sid = new SecurityIdentifier(reader.Consume(ext.sidLength));
				}
			}
		}

		private void WriteStringTo(ByteWriter writer, int offset, string? str)
		{
			if (!string.IsNullOrEmpty(str))
			{
				writer.SetPosition((int)this.offStart + offset);
				writer.WriteStringUni(str);
			}

		}

		partial void OnAfterWritePdu(ByteWriter writer)
		{
			this.WriteStringTo(writer, this.UpnOffset, this.Upn);
			this.WriteStringTo(writer, this.DnsDomainNameOffset, this.DnsDomainName);
			if (this._ext.HasValue)
			{
				var ext = this._ext.Value;
				this.WriteStringTo(writer, ext.samNameOffset, this.SamName);

				if (this.Sid != null)
				{
					writer.SetPosition((int)this.offStart + ext.sidOffset);
					this.Sid.GetBytes(writer.Consume(ext.sidLength));
				}
			}
			writer.Align(8);
		}
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct PAC_CREDENTIAL_INFO
	{
		internal int version;
		internal EType encryptionType;
	}
}