using ms_dtyp;
using ms_samr;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Crypto;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Security;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mssamr
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct UnsaltedEncryptedBuffer
	{
		internal const int DataSize = 256;

		internal unsafe fixed char data[DataSize];
		internal int length;

		internal unsafe Span<byte> AsSpan()
		{
			fixed (char* pBuf = this.data)
			{
				return new Span<byte>((byte*)pBuf, sizeof(UnsaltedEncryptedBuffer));
			}
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct EncryptedBuffer
	{
		internal static unsafe int StructSize = sizeof(EncryptedBuffer);
		internal const int DataSize = 256;
		internal const int SaltSize = 16;

		internal unsafe fixed char data[DataSize];
		internal int length;
		internal unsafe fixed byte salt[SaltSize];

		internal unsafe Span<byte> GetDataSpan()
		{
			fixed (char* pBuf = this.data)
			{
				return new Span<byte>((byte*)pBuf, 512 + 4);
			}
		}
		internal unsafe Span<byte> AsSpan()
		{
			fixed (char* pBuf = this.data)
			{
				return new Span<byte>((byte*)pBuf, StructSize);
			}
		}

	}

	/// <summary>
	/// Implements a client for [MS-SAMR].
	/// </summary>
	public partial class SamClient : RpcServiceClient<samrClientProxy>
	{
		/// <summary>
		/// Initializes a new <see cref="SamClient"/>.
		/// </summary>
		public SamClient()
		{
		}

		#region Connection parameters
		// Just a guess
		/// <inheritdoc/>
		public sealed override string ServiceClass => ServiceClassNames.HostU;
		// [MS-SAMR] § 2.1
		/// <inheritdoc/>
		public sealed override string? WellKnownPipeName => PipeName;
		// [MS-SAMR] § 2.1
		/// <inheritdoc/>
		public sealed override bool SupportsDynamicTcp => true;
		// [MS-SAMR] § 2.1
		/// <inheritdoc/>
		public sealed override bool SupportsNdr64 => true;
		// [MS-SAMR] § 2.1
		/// <inheritdoc/>
		public sealed override bool SupportsReauthOverNamedPipes => false;
		// [MS-SAMR] § 2.1
		/// <inheritdoc/>
		public sealed override bool RequiresEncryptionOverTcp => true;
		#endregion

		/// <summary>
		/// Name of the pipe for RPC
		/// </summary>
		public const string PipeName = "samr";

		/// <summary>
		/// Encrypts a password for transmission over an insecure channel, such as pipes.
		/// </summary>
		/// <param name="password">Password</param>
		/// <returns>A byte array representing the encrypted password.</returns>
		/// <exception cref="InvalidOperationException">The underlying channel does not have a session key</exception>
		public byte[] EncryptPassword(string password)
		{
			ArgumentNullException.ThrowIfNull(password);
			if (this._proxy.SecureChannel is null || !this._proxy.SecureChannel.HasSessionKey)
				throw new NotSupportedException(Messages.NoSessionKey);

			byte[]? sessionKey = this._proxy.SecureChannel.GetSessionKey();
			if (sessionKey is null)
				throw new NotSupportedException(Messages.NoSessionKey);

			return EncryptPassword(sessionKey, password);
		}

		/// <summary>
		/// Encrypts a password for transmission over an insecure channel, such as pipes.
		/// </summary>
		/// <param name="key">Session key</param>
		/// <param name="password">Password</param>
		/// <returns>A byte array representing the encrypted password.</returns>
		public static unsafe byte[] UnsaltedEncryptPassword(byte[] key, string password)
		{
			if (key is null) throw new ArgumentNullException(nameof(key));
			if (password is null) throw new ArgumentNullException(nameof(password));
			if (password.Length > UnsaltedEncryptedBuffer.DataSize)
				throw new ArgumentException(Messages.SamClient_PasswordTooLong, nameof(password));

			UnsaltedEncryptedBuffer buffer = new UnsaltedEncryptedBuffer();
			int startOffset = UnsaltedEncryptedBuffer.DataSize - password.Length;
			for (int i = 0; i < password.Length; i++)
			{
				buffer.data[i + startOffset] = password[i];
			}

			buffer.length = password.Length * 2;
			Rc4Context rc4 = new Rc4Context();
			rc4.Initialize(key);
			rc4.Transform(buffer.AsSpan(), buffer.AsSpan());
			return buffer.AsSpan().ToArray();

		}

		/// <summary>
		/// Encrypts a password for transmission over an insecure channel, such as pipes.
		/// </summary>
		/// <param name="sessionKey">Session key</param>
		/// <param name="password">Password</param>
		/// <returns>A byte array representing the encrypted password.</returns>
		public static unsafe byte[] EncryptPassword(byte[] sessionKey, string password)
		{
			if (sessionKey is null) throw new ArgumentNullException(nameof(sessionKey));
			if (password is null) throw new ArgumentNullException(nameof(password));
			if (password.Length > UnsaltedEncryptedBuffer.DataSize)
				throw new ArgumentException(Messages.SamClient_PasswordTooLong, nameof(password));

			EncryptedBuffer buf = new EncryptedBuffer();
			int startOffset = EncryptedBuffer.DataSize - password.Length;
			for (int i = 0; i < password.Length; i++)
			{
				buf.data[i + startOffset] = password[i];
			}
			buf.length = password.Length * 2;

			RandomNumberGenerator rng = RandomNumberGenerator.Create();
			if (startOffset > 0)
			{
				byte[] padding = new byte[startOffset * 2];
				rng.GetBytes(padding);
				for (int i = 0; i < startOffset; i++)
				{
					ushort n = padding[i * 2];
					n <<= 8;
					n |= padding[i * 2 + 1];
					buf.data[i] = (char)n;
				}
			}
			byte[] salt = new byte[16];
			rng.GetBytes(salt);
			for (int i = 0; i < 16; i++)
			{
				buf.salt[i] = salt[i];
			}


			Md5Context ctx = new Md5Context();
			ctx.Initialize();
			ctx.HashData(salt);
			ctx.HashData(sessionKey);

			Span<byte> rc4Key = stackalloc byte[16];
			ctx.HashFinal(rc4Key);

			Rc4Context rc4 = new Rc4Context();
			rc4.Initialize(rc4Key);
			rc4.Transform(buf.GetDataSpan(), buf.GetDataSpan());

			return buf.AsSpan().ToArray();
		}

		/// <summary>
		/// Connects to the SAM.
		/// </summary>
		/// <param name="access">Requested access</param>
		/// <param name="server">Name of server</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="Sam"/> object representing the SAM.</returns>
		/// <exception cref="NtstatusException">The call failed</exception>
		public async Task<Sam> Connect(
			SamServerAccess access,
			string server,
			CancellationToken cancellationToken)
		{
			var pRevInfo = new SAMPR_REVISION_INFO
			{
				unionSwitch = 1,
				V1 = new SAMPR_REVISION_INFO_V1
				{
					Revision = 3,
					SupportedFeatures = 1,
				}
			};
			var pOutVersion = new RpcPointer<uint>();
			var pOutRevInfo = new RpcPointer<SAMPR_REVISION_INFO>(new SAMPR_REVISION_INFO
			{
				//unionSwitch = 1
			});
			var phServer = new RpcPointer<RpcContextHandle>();
			// TODO: Inspect actual SAMR packets to see actual values
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrConnect5(
				(server.StartsWith("\\\\") ? server : "\\\\" + server),
				(uint)access,
				1,
				pRevInfo,
				pOutVersion,
				pOutRevInfo,
				phServer,
				cancellationToken
				).ConfigureAwait(false));

			return new Sam(this, phServer.value);
		}

		internal async Task<SamDomain> OpenDomain(
			RpcContextHandle phServer,
			RPC_SID pDomainSid,
			SamDomainAccess access,
			CancellationToken cancellationToken
			)
		{
			var phDomain = new RpcPointer<RpcContextHandle>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrOpenDomain(
				phServer,
				(uint)access,
				pDomainSid,
				phDomain,
				cancellationToken
				).ConfigureAwait(false));

			return new SamDomain(this, phDomain.value);
		}

		internal async Task<SamGroup> OpenGroup(
			RpcContextHandle phDomain,
			uint groupId,
			SamGroupAccess access,
			CancellationToken cancellationToken
			)
		{
			var phGroup = new RpcPointer<RpcContextHandle>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrOpenGroup(
				phDomain,
				(uint)access,
				groupId,
				phGroup,
				cancellationToken
				).ConfigureAwait(false));

			return new SamGroup(this, phGroup.value);
		}

		internal async Task<SamAlias> OpenAlias(
			RpcContextHandle phDomain,
			uint aliasId,
			SamAliasAccess access,
			CancellationToken cancellationToken
			)
		{
			var phAlias = new RpcPointer<RpcContextHandle>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrOpenAlias(
				phDomain,
				(uint)access,
				aliasId,
				phAlias,
				cancellationToken
				).ConfigureAwait(false));

			return new SamAlias(this, phAlias.value);
		}

		internal async Task<SamUser> OpenUser(
			RpcContextHandle phDomain,
			uint aliasId,
			SamUserAccess access,
			CancellationToken cancellationToken
			)
		{
			var phUser = new RpcPointer<RpcContextHandle>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrOpenUser(
				phDomain,
				(uint)access,
				aliasId,
				phUser,
				cancellationToken
				).ConfigureAwait(false));

			return new SamUser(this, phUser.value);
		}

		private delegate Task<int> EnumFunc(
			Titanis.DceRpc.RpcContextHandle ServerHandle,
			RpcPointer<uint> EnumerationContext,
			RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer,
			uint PreferedMaximumLength,
			RpcPointer<uint> CountReturned,
			CancellationToken cancellationToken);

		internal async Task<List<SamEntry>> EnumDomains(
			RpcContextHandle phServer,
			CancellationToken cancellationToken)
		{
			return await EnumSamEntries(
				phServer,
				SamEntryType.Domain,
				this._proxy.SamrEnumerateDomainsInSamServer,
				cancellationToken).ConfigureAwait(false);
		}

		private static async Task<List<SamEntry>> EnumSamEntries(
			RpcContextHandle phServer,
			SamEntryType entryType,
			EnumFunc enumFunc,
			CancellationToken cancellationToken)
		{
			RpcPointer<uint> pContext = new RpcPointer<uint>();
			RpcPointer<uint> pcReturned = new RpcPointer<uint>();
			var pBuf = new RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>>(null);

			var domains = new List<SamEntry>();
			Ntstatus status;
			do
			{
				pBuf.value = null;
				status = (Ntstatus)(await enumFunc(
					phServer,
					pContext,
					pBuf,
					8192,
					pcReturned,
					cancellationToken).ConfigureAwait(false));
				if (
					(status == Ntstatus.STATUS_NO_MORE_ENTRIES)
					|| (status == Ntstatus.STATUS_SUCCESS)
					)
				{
					var enumBuf = pBuf.value?.value.Buffer?.value;
					if (enumBuf != null)
					{
						foreach (var entry in enumBuf)
						{
							domains.Add(new SamEntry(
								entry.RelativeId,
								new string(entry.Name.Buffer.value.Array, entry.Name.Buffer.value.Offset, entry.Name.Buffer.value.Count),
								entryType
							));
						}
					}
				}
				else
				{
					throw new NtstatusException(status);
				}
			} while (status == Ntstatus.STATUS_NO_MORE_ENTRIES);

			return domains;
		}

		internal async Task<List<SamEntry>> EnumGroupsInDomains(RpcContextHandle phDomain, CancellationToken cancellationToken)
		{
			return await EnumSamEntries(phDomain, SamEntryType.Group, this._proxy.SamrEnumerateGroupsInDomain, cancellationToken).ConfigureAwait(false);
		}

		internal async Task<List<SamEntry>> EnumAliasesInDomains(RpcContextHandle phDomain, CancellationToken cancellationToken)
		{
			return await EnumSamEntries(phDomain, SamEntryType.Alias, this._proxy.SamrEnumerateAliasesInDomain, cancellationToken).ConfigureAwait(false);
		}

		internal async Task<List<SamEntry>> EnumUsersInDomains(RpcContextHandle phDomain, CancellationToken cancellationToken)
		{
			return await EnumSamEntries(phDomain, SamEntryType.User, (RpcContextHandle ServerHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, CancellationToken cancellationToken) =>
			{
				return this._proxy.SamrEnumerateUsersInDomain(
					ServerHandle,
					EnumerationContext,
					0, Buffer,
					PreferedMaximumLength,
					CountReturned,
					cancellationToken);
			},
			cancellationToken).ConfigureAwait(false);
		}

		internal async Task<RpcPointer<RPC_SID>> LookupDomain(RpcContextHandle phServer, string name, CancellationToken cancellationToken)
		{
			var pSid = new RpcPointer<RpcPointer<RPC_SID>>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrLookupDomainInSamServer(
				phServer,
				CreateRpcStringFrom(name),
				pSid,
				cancellationToken
				).ConfigureAwait(false));

			return pSid.value;
		}

		internal async Task<SamEntry[]> LookupNames(
			RpcContextHandle phDomain,
			string[] names,
			CancellationToken cancellationToken)
		{
			if (names is null)
				throw new ArgumentNullException(nameof(names));
			if (names.Length > 1000)
				throw new ArgumentOutOfRangeException(nameof(names));

			RPC_UNICODE_STRING[] nameStrs = new RPC_UNICODE_STRING[1000];
			for (int i = 0; i < names.Length; i++)
			{
				nameStrs[i] = CreateRpcStringFrom(names[i]);
			}

			var ridBuf = new RpcPointer<SAMPR_ULONG_ARRAY>();
			var nameTypeBuf = new RpcPointer<SAMPR_ULONG_ARRAY>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrLookupNamesInDomain(
				phDomain,
				(uint)names.Length,
				new ArraySegment<RPC_UNICODE_STRING>(nameStrs, 0, names.Length),
				ridBuf,
				nameTypeBuf,
				cancellationToken
				).ConfigureAwait(false));

			var rids = ridBuf.value.Element.value;
			var nameTypes = nameTypeBuf.value.Element.value;
			SamEntry[] entries = new SamEntry[names.Length];
			for (int i = 0; i < entries.Length; i++)
			{
				entries[i] = new SamEntry(rids[i], names[i], (SamEntryType)nameTypes[i]);
			}

			return entries;
		}

		internal async Task<SamEntry[]> LookupIDs(
			RpcContextHandle phDomain,
			uint[] rids,
			CancellationToken cancellationToken)
		{
			if (rids is null)
				throw new ArgumentNullException(nameof(rids));
			if (rids.Length > 1000)
				throw new ArgumentOutOfRangeException(nameof(rids));

			int idCount = rids.Length;
			if (idCount != 1000)
			{
				uint[] idList = new uint[1000];
				Buffer.BlockCopy(rids, 0, idList, 0, 4 * idCount);
				rids = idList;
			}

			var nameBuf = new RpcPointer<SAMPR_RETURNED_USTRING_ARRAY>();
			var nameTypeBuf = new RpcPointer<SAMPR_ULONG_ARRAY>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrLookupIdsInDomain(
				phDomain,
				(uint)idCount,
				new ArraySegment<uint>(rids, 0, idCount),
				nameBuf,
				nameTypeBuf,
				cancellationToken
				).ConfigureAwait(false));

			var names = nameBuf.value.Element.value;
			var nameTypes = nameTypeBuf.value.Element.value;
			SamEntry[] entries = new SamEntry[idCount];
			for (int i = 0; i < entries.Length; i++)
			{
				entries[i] = new SamEntry(rids[i], names[i].AsString(), (SamEntryType)nameTypes[i]);
			}

			return entries;
		}

		private static RpcPointer<RPC_UNICODE_STRING> CreateRpcStringPtrFrom(string str)
		{
			RPC_UNICODE_STRING rpcstr = CreateRpcStringFrom(str);
			return new RpcPointer<RPC_UNICODE_STRING>(rpcstr);
		}

		private static RPC_UNICODE_STRING CreateRpcStringFrom(string str)
		{
			var chars = (str + '\0').ToCharArray();
			RPC_UNICODE_STRING rpcstr = new RPC_UNICODE_STRING
			{
				Buffer = new RpcPointer<ArraySegment<char>>(new ArraySegment<char>(chars, 0, str.Length)),
				Length = (ushort)(str.Length * 2),
				MaximumLength = (ushort)((str.Length + 1) * 2)
			};
			return rpcstr;
		}

		private delegate Task<int> EnumMembersFunc(
			Titanis.DceRpc.RpcContextHandle GroupHandle,
			RpcPointer<RpcPointer<SAMPR_GET_MEMBERS_BUFFER>> Members
			);

		internal async Task<List<SamMemberInfo>> EnumGroupMembers(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var pMemberBuf = new RpcPointer<RpcPointer<SAMPR_GET_MEMBERS_BUFFER>>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrGetMembersInGroup(
				handle,
				pMemberBuf,
				cancellationToken
				).ConfigureAwait(false));

			var enumBuf = pMemberBuf.value;
			List<SamMemberInfo> list = new List<SamMemberInfo>();

			if (
				(enumBuf != null)
				)
			{
				var members = enumBuf.value.Members?.value;
				var attrs = enumBuf.value.Attributes?.value;
				if (members != null && attrs != null)
				{
					int count = Math.Min(members.Length, attrs.Length);
					count = Math.Min(count, (int)enumBuf.value.MemberCount);
					for (int i = 0; i < count; i++)
					{
						list.Add(new SamMemberInfo(members[i], attrs[i]));
					}
				}
			}
			return list;
		}

		internal async Task<List<SamSid>> EnumAliasMembers(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var pMemberBuf = new RpcPointer<SAMPR_PSID_ARRAY_OUT>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrGetMembersInAlias(
				handle,
				pMemberBuf,
				cancellationToken
				).ConfigureAwait(false));

			var enumBuf = pMemberBuf.value.Sids?.value;
			List<SamSid> list = new List<SamSid>();

			if (
				(enumBuf != null)
				)
			{
				foreach (var entry in enumBuf)
				{
					if (entry.SidPointer != null)
						list.Add(new SamSid(entry.SidPointer.value));
				}
			}
			return list;
		}

		internal async Task<SamGroup> CreateGroup(
			RpcContextHandle phDomain,
			string name,
			SamGroupAccess access,
			CancellationToken cancellationToken)
		{
			var pRelativeId = new RpcPointer<uint>();
			var phGroup = new RpcPointer<RpcContextHandle>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrCreateGroupInDomain(
				phDomain,
				CreateRpcStringFrom(name),
				(uint)access,
				phGroup,
				pRelativeId,
				cancellationToken
				).ConfigureAwait(false));

			return new SamGroup(this, phGroup.value);
		}

		internal async Task<SamAlias> CreateAlias(
			RpcContextHandle phDomain,
			string name,
			SamAliasAccess access,
			CancellationToken cancellationToken)
		{
			var pRelativeId = new RpcPointer<uint>();
			var phAlias = new RpcPointer<RpcContextHandle>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrCreateAliasInDomain(
				phDomain,
				CreateRpcStringFrom(name),
				(uint)access,
				phAlias,
				pRelativeId,
				cancellationToken).ConfigureAwait(false));

			return new SamAlias(this, phAlias.value);
		}

		internal async Task<SamUser> CreateUser(
			RpcContextHandle phDomain,
			string name,
			SamUserAccountFlags accountType,
			SamUserAccess access,
			CancellationToken cancellationToken)
		{
			var pRelativeId = new RpcPointer<uint>();
			var phUser = new RpcPointer<RpcContextHandle>();
			var pGrantedAccess = new RpcPointer<uint>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrCreateUser2InDomain(
				phDomain,
				CreateRpcStringFrom(name),
				(uint)accountType,
				(uint)access,
				phUser,
				pGrantedAccess,
				pRelativeId,
				cancellationToken).ConfigureAwait(false));

			return new SamUser(this, phUser.value);
		}

		#region Query domain info
		internal async Task<SamDomainGeneralInfo> QueryDomainGeneralInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			SAMPR_DOMAIN_INFO_BUFFER info = await QueryDomainInfo(handle, DOMAIN_INFORMATION_CLASS.DomainGeneralInformation, cancellationToken).ConfigureAwait(false);
			return new SamDomainGeneralInfo(info.General);
		}

		internal async Task<SamDomainGeneralInfo2> QueryDomainGeneralInfo2(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			SAMPR_DOMAIN_INFO_BUFFER info = await QueryDomainInfo(handle, DOMAIN_INFORMATION_CLASS.DomainGeneralInformation2, cancellationToken).ConfigureAwait(false);
			return new SamDomainGeneralInfo2(info.General2);
		}

		internal async Task<SamDomainPasswordInfo> QueryDomainPasswordInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			SAMPR_DOMAIN_INFO_BUFFER info = await QueryDomainInfo(handle, DOMAIN_INFORMATION_CLASS.DomainPasswordInformation, cancellationToken).ConfigureAwait(false);
			return new SamDomainPasswordInfo(info.Password);
		}

		internal async Task<SamDomainLogoffInfo> QueryDomainLogoffInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			SAMPR_DOMAIN_INFO_BUFFER info = await QueryDomainInfo(handle, DOMAIN_INFORMATION_CLASS.DomainLogoffInformation, cancellationToken).ConfigureAwait(false);
			return new SamDomainLogoffInfo(info.Logoff);
		}

		internal async Task<SamDomainModifiedInfo> QueryDomainModifiedInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			SAMPR_DOMAIN_INFO_BUFFER info = await QueryDomainInfo(handle, DOMAIN_INFORMATION_CLASS.DomainModifiedInformation, cancellationToken).ConfigureAwait(false);
			return new SamDomainModifiedInfo(info.Modified);
		}

		internal async Task<SamDomainModifiedInfo2> QueryDomainModifiedInfo2(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			SAMPR_DOMAIN_INFO_BUFFER info = await QueryDomainInfo(handle, DOMAIN_INFORMATION_CLASS.DomainModifiedInformation2, cancellationToken).ConfigureAwait(false);
			return new SamDomainModifiedInfo2(info.Modified2);
		}

		internal async Task<string> QueryDomainOemInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			SAMPR_DOMAIN_INFO_BUFFER info = await QueryDomainInfo(handle, DOMAIN_INFORMATION_CLASS.DomainOemInformation, cancellationToken).ConfigureAwait(false);
			return info.Oem.OemInformation.AsString();
		}

		internal async Task<string> QueryDomainNameInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			SAMPR_DOMAIN_INFO_BUFFER info = await QueryDomainInfo(handle, DOMAIN_INFORMATION_CLASS.DomainNameInformation, cancellationToken).ConfigureAwait(false);
			return info.Name.DomainName.AsString();
		}

		internal async Task<DomainServerRole> QueryDomainServerRole(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			SAMPR_DOMAIN_INFO_BUFFER info = await QueryDomainInfo(handle, DOMAIN_INFORMATION_CLASS.DomainServerRoleInformation, cancellationToken).ConfigureAwait(false);
			return (DomainServerRole)info.Role.DomainServerRole;
		}

		internal async Task<DomainServerEnableState> QueryDomainServerEnabledState(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			SAMPR_DOMAIN_INFO_BUFFER info = await QueryDomainInfo(handle, DOMAIN_INFORMATION_CLASS.DomainStateInformation, cancellationToken).ConfigureAwait(false);
			return (DomainServerEnableState)info.State.DomainServerState;
		}

		internal async Task<string> QueryDomainReplicaInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			SAMPR_DOMAIN_INFO_BUFFER info = await QueryDomainInfo(handle, DOMAIN_INFORMATION_CLASS.DomainReplicationInformation, cancellationToken).ConfigureAwait(false);
			return info.Replication.ReplicaSourceNodeName.AsString();
		}

		private async Task<SAMPR_DOMAIN_INFO_BUFFER> QueryDomainInfo(
			RpcContextHandle handle,
			DOMAIN_INFORMATION_CLASS infoClass,
			CancellationToken cancellationToken)
		{
			var pInfoBuf = new RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrQueryInformationDomain(
				handle,
				infoClass,
				pInfoBuf,
				cancellationToken
				).ConfigureAwait(false));

			var info = pInfoBuf.value.value;
			return info;
		}
		#endregion

		#region Query group info

		internal async Task<SamGroupGeneralInfo> QueryGroupGeneralInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryGroupInfo(handle, GROUP_INFORMATION_CLASS.GroupGeneralInformation, cancellationToken).ConfigureAwait(false);
			return new SamGroupGeneralInfo(info.General);
		}

		internal async Task<SamGroupGeneralInfo> QueryGroupReplicaInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryGroupInfo(handle, GROUP_INFORMATION_CLASS.GroupReplicationInformation, cancellationToken).ConfigureAwait(false);
			return new SamGroupGeneralInfo(info.General);
		}

		internal async Task<string> QueryGroupNameInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryGroupInfo(handle, GROUP_INFORMATION_CLASS.GroupNameInformation, cancellationToken).ConfigureAwait(false);
			return info.Name.Name.AsString();
		}

		internal async Task<SamGroupAttributes> QueryGroupAttrInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryGroupInfo(handle, GROUP_INFORMATION_CLASS.GroupAttributeInformation, cancellationToken).ConfigureAwait(false);
			return (SamGroupAttributes)info.Attribute.Attributes;
		}

		internal async Task<string> QueryGroupAdminComment(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryGroupInfo(handle, GROUP_INFORMATION_CLASS.GroupAdminCommentInformation, cancellationToken).ConfigureAwait(false);
			return info.AdminComment.AdminComment.AsString();
		}

		private async Task<SAMPR_GROUP_INFO_BUFFER> QueryGroupInfo(RpcContextHandle handle, GROUP_INFORMATION_CLASS infoClass, CancellationToken cancellationToken)
		{
			var pInfoBuf = new RpcPointer<RpcPointer<SAMPR_GROUP_INFO_BUFFER>>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrQueryInformationGroup(
				handle,
				infoClass,
				pInfoBuf,
				cancellationToken
				).ConfigureAwait(false));

			var info = pInfoBuf.value.value;
			return info;
		}
		#endregion

		#region Set group info

		internal async Task SetGroupName(RpcContextHandle handle, string name, CancellationToken cancellationToken)
		{
			await this.SetGroupInfo(
				handle,
				new SAMPR_GROUP_INFO_BUFFER
				{
					unionSwitch = GROUP_INFORMATION_CLASS.GroupNameInformation,
					Name = new SAMPR_GROUP_NAME_INFORMATION
					{
						Name = name.AsRpcString()
					}
				}, cancellationToken).ConfigureAwait(false);
		}

		internal async Task SetGroupComment(RpcContextHandle handle, string comment, CancellationToken cancellationToken)
		{
			await this.SetGroupInfo(
				handle,
				new SAMPR_GROUP_INFO_BUFFER
				{
					unionSwitch = GROUP_INFORMATION_CLASS.GroupAdminCommentInformation,
					AdminComment = new SAMPR_GROUP_ADM_COMMENT_INFORMATION
					{
						AdminComment = comment.AsRpcString()
					}
				}, cancellationToken).ConfigureAwait(false);
		}

		private async Task SetGroupInfo(RpcContextHandle handle, SAMPR_GROUP_INFO_BUFFER groupInfo, CancellationToken cancellationToken)
		{
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrSetInformationGroup(
				handle,
				groupInfo.unionSwitch,
				groupInfo,
				cancellationToken
				).ConfigureAwait(false));
		}
		#endregion




		#region Query group info

		internal async Task<SamAliasGeneralInfo> QueryAliasGeneralInfo(
			RpcContextHandle handle,
			CancellationToken cancellationToken
			)
		{
			var info = await QueryAliasInfo(handle, ALIAS_INFORMATION_CLASS.AliasGeneralInformation, cancellationToken).ConfigureAwait(false);
			return new SamAliasGeneralInfo(info.General);
		}

		internal async Task<string> QueryAliasNameInfo(
			RpcContextHandle handle,
			CancellationToken cancellationToken)
		{
			var info = await QueryAliasInfo(handle, ALIAS_INFORMATION_CLASS.AliasNameInformation, cancellationToken).ConfigureAwait(false);
			return info.Name.Name.AsString();
		}

		internal async Task<string> QueryAliasAdminComment(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryAliasInfo(handle, ALIAS_INFORMATION_CLASS.AliasAdminCommentInformation, cancellationToken).ConfigureAwait(false);
			return info.AdminComment.AdminComment.AsString();
		}

		private async Task<SAMPR_ALIAS_INFO_BUFFER> QueryAliasInfo(RpcContextHandle handle, ALIAS_INFORMATION_CLASS infoClass, CancellationToken cancellationToken)
		{
			var pInfoBuf = new RpcPointer<RpcPointer<SAMPR_ALIAS_INFO_BUFFER>>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrQueryInformationAlias(
				handle,
				infoClass,
				pInfoBuf,
				cancellationToken
				).ConfigureAwait(false));

			var info = pInfoBuf.value.value;
			return info;
		}
		#endregion

		#region Set group info

		internal async Task SetAliasName(RpcContextHandle handle, string name, CancellationToken cancellationToken)
		{
			await this.SetAliasInfo(
				handle,
				new SAMPR_ALIAS_INFO_BUFFER
				{
					unionSwitch = ALIAS_INFORMATION_CLASS.AliasNameInformation,
					Name = new SAMPR_ALIAS_NAME_INFORMATION
					{
						Name = name.AsRpcString()
					}
				}, cancellationToken).ConfigureAwait(false);
		}

		internal async Task SetAliasComment(RpcContextHandle handle, string comment, CancellationToken cancellationToken)
		{
			await this.SetAliasInfo(
				handle,
				new SAMPR_ALIAS_INFO_BUFFER
				{
					unionSwitch = ALIAS_INFORMATION_CLASS.AliasAdminCommentInformation,
					AdminComment = new SAMPR_ALIAS_ADM_COMMENT_INFORMATION
					{
						AdminComment = comment.AsRpcString()
					}
				}, cancellationToken).ConfigureAwait(false);
		}

		private async Task SetAliasInfo(RpcContextHandle handle, SAMPR_ALIAS_INFO_BUFFER groupInfo, CancellationToken cancellationToken)
		{
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrSetInformationAlias(
				handle,
				groupInfo.unionSwitch,
				groupInfo,
				cancellationToken
				).ConfigureAwait(false));
		}
		#endregion

		#region Query user info

		internal async Task<SamUserGeneralInfo> QueryUserGeneralInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserGeneralInformation, cancellationToken).ConfigureAwait(false);
			return new SamUserGeneralInfo(info.General);
		}

		internal async Task<SamUserPreferencesInfo> QueryUserPreferencesInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserPreferencesInformation, cancellationToken).ConfigureAwait(false);
			return new SamUserPreferencesInfo(info.Preferences);
		}

		internal async Task<SamUserLogonInfo> QueryUserLogonInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserLogonInformation, cancellationToken).ConfigureAwait(false);
			return new SamUserLogonInfo(info.Logon);
		}

		//internal async Task<SamUserLogonHoursInfo> QueryUserLogonHoursInfo(RpcContextHandle handle)
		//{
		//	var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserLogonHoursInformation);
		//	return new SamUserLogonHoursInfo(info.LogonHours);
		//}

		internal async Task<SamUserAccountInfo> QueryUserAccountInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserAccountInformation, cancellationToken).ConfigureAwait(false);
			return new SamUserAccountInfo(info.Account);
		}

		internal async Task<string> QueryUserAccountNameInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserAccountNameInformation, cancellationToken).ConfigureAwait(false);
			return info.AccountName.UserName.AsString();
		}

		internal async Task<string> QueryUserFullNameInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserFullNameInformation, cancellationToken).ConfigureAwait(false);
			return info.FullName.FullName.AsString();
		}

		internal async Task<uint> QueryUserPrimaryGroup(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserPrimaryGroupInformation, cancellationToken).ConfigureAwait(false);
			return info.PrimaryGroup.PrimaryGroupId;
		}

		internal async Task<SamUserHomeInfo> QueryUserHomeInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserHomeInformation, cancellationToken).ConfigureAwait(false);
			return new SamUserHomeInfo(info.Home);
		}

		internal async Task<string> QueryUserScriptInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserScriptInformation, cancellationToken).ConfigureAwait(false);
			return info.Script.ScriptPath.AsString();
		}

		internal async Task<string> QueryUserProfileInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserProfileInformation, cancellationToken).ConfigureAwait(false);
			return info.Profile.ProfilePath.AsString();
		}

		internal async Task<string> QueryUserAdminComment(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserAdminCommentInformation, cancellationToken).ConfigureAwait(false);
			return info.AdminComment.AdminComment.AsString();
		}

		internal async Task<string> QueryUserWorkstations(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserWorkStationsInformation, cancellationToken).ConfigureAwait(false);
			return info.WorkStations.WorkStations.AsString();
		}

		internal async Task<SamUserAccountFlags> QueryUserControlInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserControlInformation, cancellationToken).ConfigureAwait(false);
			return (SamUserAccountFlags)info.Control.UserAccountControl;
		}

		internal async Task<SamUserAllInfo> QueryUserAllInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var info = await QueryUserInfo(handle, USER_INFORMATION_CLASS.UserAllInformation, cancellationToken).ConfigureAwait(false);
			return new SamUserAllInfo(info.All);
		}

		private async Task<SAMPR_USER_INFO_BUFFER> QueryUserInfo(
			RpcContextHandle handle,
			USER_INFORMATION_CLASS infoClass,
			CancellationToken cancellationToken)
		{
			var pInfoBuf = new RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>>();
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrQueryInformationUser2(
				handle,
				infoClass,
				pInfoBuf,
				cancellationToken
				).ConfigureAwait(false));

			var info = pInfoBuf.value.value;
			return info;
		}

		#endregion

		#region Set user info
		internal async Task SetUserPassword(RpcContextHandle handle, byte[] encryptedPassword, CancellationToken cancellationToken)
		{
			await this.SetUserInfo(handle, new SAMPR_USER_INFO_BUFFER
			{
				unionSwitch = USER_INFORMATION_CLASS.UserInternal5InformationNew,
				Internal5New = new SAMPR_USER_INTERNAL5_INFORMATION_NEW
				{
					UserPassword = new SAMPR_ENCRYPTED_USER_PASSWORD_NEW
					{
						Buffer = encryptedPassword
					},
					PasswordExpired = 0
				}
			}, cancellationToken).ConfigureAwait(false);
		}

		public async Task UpdateUserPassword(string user, string oldPassword, string newPassword, CancellationToken cancellationToken)
		{

			byte[] oldNtlm = oldPassword.AsNtlm().ToArray();
			byte[] newNtlm = newPassword.AsNtlm().ToArray();
			var junk = new UnsaltedEncryptedBuffer().AsSpan().ToArray();
			SAMPR_ENCRYPTED_USER_PASSWORD lmPass = new SAMPR_ENCRYPTED_USER_PASSWORD { Buffer = junk };
			ENCRYPTED_LM_OWF_PASSWORD oldLmPass = new ENCRYPTED_LM_OWF_PASSWORD { data = new byte[16] };
			ENCRYPTED_LM_OWF_PASSWORD oldNtPass = new ENCRYPTED_LM_OWF_PASSWORD { data = oldNtlm.EncryptHashWithHash(newNtlm) };
			SAMPR_ENCRYPTED_USER_PASSWORD newNtPass = new SAMPR_ENCRYPTED_USER_PASSWORD { Buffer = UnsaltedEncryptPassword(oldNtlm, newPassword) };


			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrUnicodeChangePasswordUser2(
				new RpcPointer<RPC_UNICODE_STRING>("".AsRpcString()),
				user.AsRpcString(),
				new RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD>(newNtPass),
				new RpcPointer<ENCRYPTED_LM_OWF_PASSWORD>(oldNtPass),
				0,
				new RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD>(lmPass),
				new RpcPointer<ENCRYPTED_LM_OWF_PASSWORD>(oldLmPass),
				cancellationToken
			).ConfigureAwait(false));
		}

		internal async Task SetUserControlFlags(RpcContextHandle handle, SamUserAccountFlags flags, CancellationToken cancellationToken)
		{
			await this.SetUserInfo(handle, new SAMPR_USER_INFO_BUFFER
			{
				unionSwitch = USER_INFORMATION_CLASS.UserControlInformation,
				Control = new USER_CONTROL_INFORMATION
				{
					UserAccountControl = (uint)flags
				}
			}, cancellationToken).ConfigureAwait(false);
		}

		private async Task SetUserInfo(RpcContextHandle handle, SAMPR_USER_INFO_BUFFER userInfo, CancellationToken cancellationToken)
		{
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrSetInformationUser2(
				handle,
				userInfo.unionSwitch,
				userInfo,
				cancellationToken
				).ConfigureAwait(false));
		}

		internal async Task CloseHandle(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			await this._proxy.SamrCloseHandle(new RpcPointer<RpcContextHandle>(handle), cancellationToken).ConfigureAwait(false);
		}
		#endregion

		#region Delete pattern
		internal async Task DeleteUser(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrDeleteUser(new RpcPointer<RpcContextHandle>(handle), cancellationToken).ConfigureAwait(false));
		}
		internal async Task DeleteGroup(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrDeleteGroup(new RpcPointer<RpcContextHandle>(handle), cancellationToken).ConfigureAwait(false));
		}
		internal async Task DeleteAlias(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			NtstatusException.CheckAndThrow((Ntstatus)await this._proxy.SamrDeleteAlias(new RpcPointer<RpcContextHandle>(handle), cancellationToken).ConfigureAwait(false));
		}
		#endregion
	}
}
