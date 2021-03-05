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
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;
using Titanis.Winterop.Security;

namespace Titanis.Security.Kerberos
{
	public class TicketAuthorizationData
	{

		internal enum AdType
		{
			IfRelevant = 1,
			Pac = 128,
		}

		public TicketAuthorizationData(KerberosClient krb, SessionKey key)
		{
			this._krb = krb;
			this._key = key;
		}

		private readonly KerberosClient _krb;
		private readonly SessionKey _key;

		internal void Process(Ticket_EncPart encPart)
		{
			var authData = encPart?.Value?.authorization_data;
			if (authData == null)
				return;

			Process(authData, false);
		}

		private void Process(IList<Unnamed_0> authData, bool optional)
		{
			foreach (var adRec in authData)
			{
				this.Process(adRec, false);
			}
		}

		private void Process(Unnamed_0 adRec, bool optional)
		{
			switch ((AdType)adRec.ad_type)
			{
				case AdType.IfRelevant:
					{
						var inner = Asn1DerDecoder.Decode<Asn1SequenceOf<Unnamed_0>>(adRec.ad_data);
						this.Process(inner.Values, true);
					}
					break;
				case AdType.Pac:
					this.ProcessPac(adRec.ad_data);
					break;
				default:
					break;
			}
		}

		private void ProcessPac(byte[] authData)
		{
			var decoder = RpcEncoding.MsrpcNdr.CreateDecoder(new ByteMemoryReader(authData), new RpcCallContext(null));
			// PAC_TYPE
			var bufferCount = decoder.ReadInt32();
			var version = decoder.ReadInt32();
			for (int i = 0; i < bufferCount; i++)
			{
				var bufferInfo = decoder.ReadFixedStruct<PAC_INFO_BUFFER>(NdrAlignment._8Byte);
				int offBuffer = (int)bufferInfo.Offset + 16;

				switch ((PacBufferType)bufferInfo.ulType)
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
					default:
						{
							var buffer = authData.AsMemory(offBuffer, (int)bufferInfo.cbBufferSize);
							var bufferDecoder = RpcEncoding.MsrpcNdr.CreateDecoder(new ByteMemoryReader(buffer), new RpcCallContext(null));

							switch ((PacBufferType)bufferInfo.ulType)
							{
								case PacBufferType.LogonInfo:
									ProcessLogonInfo(bufferDecoder);
									break;
								case PacBufferType.ClientNameInfo:
									break;
								case PacBufferType.UserPrincipalName:
									break;
								case PacBufferType.PacAttributes:
									break;
								case PacBufferType.PacRequestorSid:
									break;
								default:
									break;
							}
						}
						break;
				}
			}
		}

		private int _offServerCheckvsum;
		private int _offKdcCheckvsum;
		private int _offExtendedKdcCheckvsum;

		public LogonInfo? LogonInfo { get; private set; }
		private void ProcessLogonInfo(RpcDecoder decoder)
		{
			var ptr = decoder.ReadReferentId();
			if (ptr != 0)
			{
				var logonInfo = new LogonInfo();
				logonInfo.info.Decode(decoder);
				logonInfo.info.DecodeDeferrals(decoder);
				this.LogonInfo = logonInfo;
			}
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
			set => info.EffectiveName = (this._effectiveName = value).ToRpcUniString();
		}
		private string? _FullName;
		public string? FullName
		{
			get => (this._FullName ??= info.FullName.AsString());
			set => info.FullName = (this._FullName = value).ToRpcUniString();
		}
		private string? _LogonScript;
		public string? LogonScript
		{
			get => (this._LogonScript ??= info.LogonScript.AsString());
			set => info.LogonScript = (this._LogonScript = value).ToRpcUniString();
		}
		private string? _ProfilePath;
		public string? ProfilePath
		{
			get => (this._ProfilePath ??= info.ProfilePath.AsString());
			set => info.ProfilePath = (this._ProfilePath = value).ToRpcUniString();
		}
		private string? _HomeDirectory;
		public string? HomeDirectory
		{
			get => (this._HomeDirectory ??= info.HomeDirectory.AsString());
			set => info.HomeDirectory = (this._HomeDirectory = value).ToRpcUniString();
		}
		private string? _HomeDirectoryDrive;
		public string? HomeDirectoryDrive
		{
			get => (this._HomeDirectoryDrive ??= info.HomeDirectoryDrive.AsString());
			set => info.HomeDirectoryDrive = (this._HomeDirectoryDrive = value).ToRpcUniString();
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
			set => info.LogonServer = (this._LogonServer = value).ToRpcUniString();
		}
		private string? _LogonDomainName;
		public string? LogonDomainName
		{
			get => (this._LogonDomainName ??= info.LogonDomainName.AsString());
			set => info.LogonDomainName = (this._LogonDomainName = value).ToRpcUniString();
		}

		public SecurityIdentifier? LogonDomainId => info.LogonDomainId.ToSid();
		public SamUserAccountFlags UserAccountControl => (SamUserAccountFlags)info.UserAccountControl;

		private List<SidWithAttributes>? _extraSids;
		public IList<SidWithAttributes> ExtraSids => (this._extraSids ??= this.info.ExtraSids.ToList(r => new SidWithAttributes(r.Sid.ToSid(), (SidAttributes)r.Attributes)));

		public SecurityIdentifier? _resourceGroupSid;
		public SecurityIdentifier? ResourceGroupDomainSid => this._resourceGroupSid ??= this.info.ResourceGroupDomainSid.ToSid();

		private List<RidWithAttributes>? _resGroupIds;
		public IList<RidWithAttributes> ResourceGroupIds => (this._resGroupIds ??= this.info.ResourceGroupIds.ToList(r => new RidWithAttributes(r.RelativeId, (SidAttributes)r.Attributes)));
	}

	// [MS-PAC] § 2.2.1 - KERB_SID_AND_ATTRIBUTES
	[Flags]
	public enum SidAttributes
	{
		None = 0,

		Mandatory = 1,
		EnabledByDefault = 2,
		Enabled = 4,
		Owner = 8,
		Resource = (1 << 29),
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

		public static DateTime? ToDateTimeOrNull(this FILETIME ft)
			=> (
				((ft.dwLowDateTime == uint.MaxValue) && (ft.dwHighDateTime == int.MaxValue))
				|| ((ft.dwLowDateTime == 0) && (ft.dwHighDateTime == 0))
			) ? null
			: DateTime.FromFileTimeUtc((((long)ft.dwHighDateTime) << 32) | ft.dwLowDateTime);

		public static DateTime ToDateTime(this FILETIME ft)
			=> DateTime.FromFileTimeUtc((((long)ft.dwHighDateTime) << 32) | ft.dwLowDateTime);

		public static string? AsString(this RPC_UNICODE_STRING rpcUniString)
			=> (rpcUniString.Buffer == null) ? null : new string(rpcUniString.Buffer.value.AsSpan());

		public static RPC_UNICODE_STRING ToRpcUniString(this string? str)
		{
			if (str == null)
			{
				return new RPC_UNICODE_STRING
				{
					Buffer = new RpcPointer<ArraySegment<char>>(Array.Empty<char>())
				};
			}
			else
			{
				return new RPC_UNICODE_STRING
				{
					Length = (ushort)(str.Length * 2),
					MaximumLength = (ushort)(str.Length * 2),
					Buffer = new RpcPointer<ArraySegment<char>>(str.ToCharArray()),
				};
			}
		}

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