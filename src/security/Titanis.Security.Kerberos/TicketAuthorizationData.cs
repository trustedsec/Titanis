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

namespace Titanis.Security.Kerberos
{
	public class TicketAuthorizationData
	{

		internal enum AdType
		{
			IfRelevant = 1,
			Pac = 128,
		}

		public TicketAuthorizationData()
		{
		}

		internal void Process(EncTicketPart encPart, byte[]? asrepKey, KerberosClient krb)
		{
			var authData = encPart?.Value?.authorization_data;
			if (authData == null)
				return;

			Process(authData, false, asrepKey, krb);
		}

		private void Process(IList<AuthorizationData_Element> authData, bool optional, byte[]? asrepKey, KerberosClient krb)
		{
			foreach (var adRec in authData)
			{
				this.Process(adRec, false, asrepKey, krb);
			}
		}

		internal void Process(AuthorizationData_Element adRec, bool optional, byte[]? asrepKey, KerberosClient krb)
		{
			switch ((AdType)adRec.ad_type)
			{
				case AdType.IfRelevant:
					{
						var inner = Asn1DerDecoder.DecodeTlv<Asn1SequenceOf<AuthorizationData_Element>>(adRec.ad_data);
						this.Process(inner.Values, true, asrepKey, krb);
					}
					break;
				case AdType.Pac:
					this.ProcessPac(adRec.ad_data, asrepKey, krb);
					break;
				default:
					break;
			}
		}

		private void ProcessPac(byte[] authData, byte[]? asrepKey, KerberosClient krb)
		{
			var decoder = RpcEncoding.MsrpcNdr.CreateDecoder(new ByteMemoryReader(authData), new RpcCallContext(null));
			// PAC_TYPE
			var bufferCount = decoder.ReadInt32();
			var version = decoder.ReadInt32();
			for (int i = 0; i < bufferCount; i++)
			{
				var bufferInfo = decoder.ReadFixedStruct<PAC_INFO_BUFFER>(NdrAlignment._8Byte);
				int offBuffer = (int)bufferInfo.Offset;
				PacBufferType pacType = (PacBufferType)bufferInfo.ulType;

				switch (pacType)
				{
					case PacBufferType.ServerChecksum:
						this._offServerCheckvsum = offBuffer;
						break;
					case PacBufferType.ExtendedKdcChecksum:
						this._offExtendedKdcCheckvsum = offBuffer;
						break;
					case PacBufferType.KdcChecksum:
						this._offKdcCheckvsum = offBuffer;
						break;
					case PacBufferType.TicketChecksum:
						this._offTicketChecksum = offBuffer;
						break;
					default:
						{
							var buffer = authData.AsMemory(offBuffer, (int)bufferInfo.cbBufferSize);
							var bufferDecoder = RpcEncoding.MsrpcNdr.CreateDecoder(new ByteMemoryReader(buffer), new RpcCallContext(null));

							switch (pacType)
							{
								case PacBufferType.LogonInfo:
									// [MS-PAC] Š 2.5 KERB_VALIDATION_INFO
									ProcessLogonInfo(bufferDecoder);
									break;
								case PacBufferType.ClientNameInfo:
									// [MS-PAC] § 2.7 PAC_CLIENT_INFO
									ProcessClientNameInfo(bufferDecoder);
									break;
								case PacBufferType.UserPrincipalName:
									// [MS-PAC] § 2.10 UPN_DNS_INFO
									ProcessUpn(bufferDecoder);
									break;
								case PacBufferType.PacAttributes:
									break;
								case PacBufferType.PacRequestorSid:
									break;
								case PacBufferType.CredentialInfo:
									// [MS-PAC] Š 2.6 PAC Credentials
									if (asrepKey != null)
										ProcessCredentialInfo(bufferDecoder, asrepKey, krb);
									break;
								default:
									break;
							}
						}
						break;
				}
			}
		}

		// [MS-PAC] § 2.10 - UPN_DNS_INFO
		private void ProcessUpn(RpcDecoder bufferDecoder)
		{
			ByteMemoryReader buf = bufferDecoder.GetStubData();

			var upnInfo = new UPN_DNS_INFO();
			upnInfo.Decode(bufferDecoder);

			string? upn = null;
			if (upnInfo.UpnLength > 0)
			{
				upn = Encoding.Unicode.GetString(buf.GetBytesReadOnly(upnInfo.UpnOffset, upnInfo.UpnLength));
			}
			string? dnsName = null;
			if (upnInfo.DnsDomainNameLength > 0)
			{
				dnsName = Encoding.Unicode.GetString(buf.GetBytesReadOnly(upnInfo.DnsDomainNameOffset, upnInfo.DnsDomainNameLength));
			}

			string? samName = null;
			if (0 != ((UpnDnsInfoFlags)upnInfo.Flags & UpnDnsInfoFlags.HasSidInfo))
			{
				var samNameLength = bufferDecoder.ReadUInt16();
				var samNameOffset = bufferDecoder.ReadUInt16();
				if (samNameLength > 0)
				{
					upn = Encoding.Unicode.GetString(buf.GetBytesReadOnly(samNameOffset, samNameLength));
				}



				var sidLength = bufferDecoder.ReadUInt16();
				var sidOffset = bufferDecoder.ReadUInt16();
				if (samNameLength > 0)
				{
					ReadOnlySpan<byte> sidBytes = buf.GetBytesReadOnly(samNameOffset, samNameLength);
					upn = Encoding.Unicode.GetString(sidBytes);
				}
			}
		}

		// [MS-PAC] § 2.7 PAC_CLIENT_INFO
		private void ProcessClientNameInfo(RpcDecoder bufferDecoder)
		{
			var clientNameInfo = new PAC_CLIENT_INFO();
			clientNameInfo.Decode(bufferDecoder);
			var nameBytes = bufferDecoder.GetStubData().Consume(clientNameInfo.NameLength);
			string name = Encoding.Unicode.GetString(nameBytes);
			this.ClientName = name;
		}

		// [MS-PAC] Š 2.6 PAC Credentials
		private void ProcessCredentialInfo(RpcDecoder bufferDecoder, byte[] asrepKey, KerberosClient krb)
		{
			PAC_CREDENTIAL_INFO credInfo = new PAC_CREDENTIAL_INFO();
			credInfo.Decode(bufferDecoder);

			var etype = (EType)credInfo.EncryptionType;
			var encProf = krb.GetEncProfile(etype);
			var key = encProf.CreateSessionKey(asrepKey);
			var encCredData = bufferDecoder.GetStubData().Remaining.ToArray();
			var credDataBytes = key.Decrypt(KeyUsage.NonKerbSalt, encCredData);

			bufferDecoder = RpcEncoding.MsrpcNdr.CreateDecoder(new ByteMemoryReader(credDataBytes), new RpcCallContext(null));
			var credData = bufferDecoder.DeserializeType1(d =>
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

		private int _offServerCheckvsum;
		private int _offKdcCheckvsum;
		private int _offExtendedKdcCheckvsum;
		private int _offTicketChecksum;

		public LogonInfo? LogonInfo { get; private set; }
		public string ClientName { get; private set; }

		// [MS-PAC] Š 2.5 KERB_VALIDATION_INFO
		private void ProcessLogonInfo(RpcDecoder decoder)
		{
			decoder.GetStubData().Consume(16);
			var ptr = decoder.ReadReferentId();
			if (ptr != 0)
			{
				var logonInfo = new LogonInfo();
				logonInfo.info.Decode(decoder);
				logonInfo.info.DecodeDeferrals(decoder);
				this.LogonInfo = logonInfo;
			}
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
		public int LogonCount => info.LogonCount;
		public int BadPasswordCount => info.BadPasswordCount;
		public uint UserId => info.UserId;
		public uint PrimaryGroupId => info.PrimaryGroupId;

		public UserLogonFlags UserFlags => (UserLogonFlags)info.UserFlags;
		public NtlmSessionKey? UserSessionKey => info.UserSessionKey.ToNtlmSessionKey();

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
		public SecurityIdentifier? LogonDomainSid => (this._logonDomainId ??= info.LogonDomainId.ToSid());

		private SecurityIdentifier? _userSid;
		public SecurityIdentifier UserSid => (this._userSid ??= (this.LogonDomainSid.Concat(this.UserId)));

		public SamUserAccountFlags UserAccountControl => (SamUserAccountFlags)info.UserAccountControl;

		#region ExtraSids
		private List<SidWithAttributes>? _extraSids;
		public IList<SidWithAttributes> ExtraSids => (this._extraSids ??= this.info.ExtraSids.ToList(r => new SidWithAttributes(r.Sid.ToSid(), (SidAttributes)r.Attributes)));
		#endregion

		#region ResourceGroupDomainSid
		public SecurityIdentifier? _resourceGroupSid;
		public SecurityIdentifier? ResourceGroupDomainSid => this._resourceGroupSid ??= this.info.ResourceGroupDomainSid.ToSid();
		#endregion

		#region ResourceGroupIds
		private List<RidWithAttributes>? _resGroupIds;
		public IList<RidWithAttributes> ResourceGroupIds => (this._resGroupIds ??= this.info.ResourceGroupIds.ToList(r => new RidWithAttributes(r.RelativeId, (SidAttributes)r.Attributes)));
		#endregion

		#region GroupIds
		private List<RidWithAttributes>? _groupIds;
		public IList<RidWithAttributes> GroupIds => (this._groupIds ??= this.info.GroupIds.ToList(r => new RidWithAttributes(r.RelativeId, (SidAttributes)r.Attributes)));
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
			=> $"{this.Sid} : {this.Attributes}";
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
		public NtlmSessionKey(byte[] part1, byte[] part2)
		{
			ArgumentNullException.ThrowIfNull(part1);
			if (part1.Length != 8 || part2.Length != 8)
				throw new ArgumentException("Both parts of the key must be 8-byte arrays.");
			ArgumentNullException.ThrowIfNull(part2);

			byte[] key = new byte[16];
			part1.CopyTo(key, 0);
			part2.CopyTo(key, 8);

			this.Key = key;
		}

		public byte[] Key { get; }
	}
}