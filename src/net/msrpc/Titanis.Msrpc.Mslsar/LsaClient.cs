using ms_dtyp;
using ms_lsar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.IO;
using Titanis.Security;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mslsar
{
	public class LsaClient : RpcServiceClient<ms_lsar.lsarpcClientProxy>
	{
		public const string PipeName = "lsarpc";
		private const int MaxBufferSize = 1024;

		public async Task<LsaPolicy> OpenPolicy(LsaPolicyAccess access, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> phPolicy = new RpcPointer<RpcContextHandle>();
			var res = (Ntstatus)await this._proxy.LsarOpenPolicy(
				new RpcPointer<char>('.'),
				new LSAPR_OBJECT_ATTRIBUTES
				{

				},
				(uint)access,
				phPolicy,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new LsaPolicy(this, phPolicy.value);
		}

		internal async Task<LsaSecret> OpenSecret(RpcContextHandle handle, string name, LsaSecretAccess access, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> phSecret = new RpcPointer<RpcContextHandle>();
			var res = (Ntstatus)await this._proxy.LsarOpenSecret(
				handle,
				name.ToRpcUnicodeString(),
				(uint)access,
				phSecret,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new LsaSecret(this, phSecret.value);
		}

		internal async Task CloseAsync(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			await this._proxy.LsarClose(new RpcPointer<RpcContextHandle>(handle), cancellationToken).ConfigureAwait(false);
		}

		internal async Task<AuditLogInfo> QueryAuditLogInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<ms_lsar.LSAPR_POLICY_INFORMATION>> pInfo = new RpcPointer<RpcPointer<ms_lsar.LSAPR_POLICY_INFORMATION>>();
			var res = (Ntstatus)await this._proxy.LsarQueryInformationPolicy(
				handle,
				POLICY_INFORMATION_CLASS.PolicyAuditLogInformation,
				pInfo,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new AuditLogInfo(pInfo.value.value.PolicyAuditLogInfo);
		}

		internal async Task<LsaAccountMapping> LookupAccountName(RpcContextHandle handle, string accountName, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(accountName);
			return (await LookupAccountNames(handle, new string[] { accountName }, cancellationToken).ConfigureAwait(false))[0];
		}
		internal async Task<LsaAccountMapping[]> LookupAccountNames(RpcContextHandle handle, string[] accountNames, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> pRefDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
			RpcPointer<LSAPR_TRANSLATED_SIDS> pTranslated = new RpcPointer<LSAPR_TRANSLATED_SIDS>();
			RpcPointer<uint> mappedCount = new();
			var res = (Ntstatus)await this._proxy.LsarLookupNames(
				handle,
				(uint)accountNames.Length,
				Array.ConvertAll(accountNames, r => r.ToRpcUnicodeString()),
				pRefDomains,
				pTranslated,
				LSAP_LOOKUP_LEVEL.LsapLookupWksta,
				mappedCount,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			var domains = Array.ConvertAll(pRefDomains.value.value.Domains.value, r => new DomainInfo(r.Name.AsString(), r.Sid.value.ToSid()));

			LsaAccountMapping[] mappings = new LsaAccountMapping[pTranslated.value.Entries];
			bool failedLookup = (res == Ntstatus.STATUS_SOME_NOT_MAPPED);
			for (int i = 0; i < mappings.Length; i++)
			{
				var sidInfo = pTranslated.value.Sids.value[i];
				var mapping = new LsaAccountMapping
				{
					AccountName = accountNames[i],
					NameType = (LsaNameType)sidInfo.Use,
				};

				var domain = (sidInfo.DomainIndex != -1) ? domains[sidInfo.DomainIndex] : null;
				if (domain != null)
				{
					mapping.DomainName = domain.Name;
					mapping.DomainSid = domain.Sid;
				}
				if (sidInfo.Use != SID_NAME_USE.SidTypeUnknown)
				{
					mapping.AccountRid = sidInfo.RelativeId;
					mapping.AccountSid = domain!.Sid.Concat(sidInfo.RelativeId);
				}

				mappings[i] = mapping;
			}
			if (failedLookup)
				throw new LsaAccountMappingException(mappings);

			return mappings;
		}

		internal async Task<LsaAccountMapping> LookupSid(RpcContextHandle policyHandle, SecurityIdentifier sid, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(sid);
			return (await LookupSids(policyHandle, new SecurityIdentifier[] { sid }, cancellationToken).ConfigureAwait(false))[0]!;
		}
		internal async Task<LsaAccountMapping[]> LookupSids(RpcContextHandle policyHandle, SecurityIdentifier[] sids, CancellationToken cancellationToken)
		{
			if (sids.IsNullOrEmpty())
				throw new ArgumentNullException(nameof(sids));

			RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>> pRefDomains = new RpcPointer<RpcPointer<LSAPR_REFERENCED_DOMAIN_LIST>>();
			RpcPointer<LSAPR_TRANSLATED_NAMES> pTranslatedNames = new RpcPointer<LSAPR_TRANSLATED_NAMES>();
			var res = (Ntstatus)await this._proxy.LsarLookupSids(
				policyHandle,
				new LSAPR_SID_ENUM_BUFFER
				{
					Entries = (uint)sids.Length,
					SidInfo = new RpcPointer<LSAPR_SID_INFORMATION[]>(Array.ConvertAll(sids, r => new LSAPR_SID_INFORMATION
					{
						Sid = new RpcPointer<RPC_SID>(r.ToRpcSid())
					}
					))
				},
				pRefDomains,
				pTranslatedNames,
				LSAP_LOOKUP_LEVEL.LsapLookupWksta,
				new RpcPointer<uint>(),
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			var domains = Array.ConvertAll(pRefDomains.value.value.Domains.value, r => new DomainInfo(r.Name.AsString(), r.Sid.value.ToSid()));

			bool failedMapping = (res == Ntstatus.STATUS_SOME_NOT_MAPPED);
			LsaAccountMapping[] mappings = new LsaAccountMapping[pTranslatedNames.value.Entries];
			for (int i = 0; i < mappings.Length; i++)
			{
				var nameInfo = pTranslatedNames.value.Names.value[i];

				var sid = sids[i];

				var mapping = new LsaAccountMapping
				{
					AccountName = nameInfo.Name.AsString(),
					NameType = (LsaNameType)nameInfo.Use,
					AccountRid = sid.Rid,
					AccountSid = sid
				};

				var domain = (nameInfo.DomainIndex != -1) ? domains[nameInfo.DomainIndex] : null;
				if (domain != null)
				{
					mapping.DomainName = domain.Name;
					mapping.DomainSid = domain.Sid;
				}

				mappings[i] = mapping;
			}
			if (failedMapping)
				throw new LsaAccountMappingException(mappings);

			return mappings;
		}

		internal async Task<byte[]> RetrievePrivateData(RpcContextHandle handle, string keyName, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> ppCipherValue = new RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>();
			var res = (Ntstatus)await this._proxy.LsarRetrievePrivateData(
				handle,
				keyName.ToRpcUnicodeString(),
				ppCipherValue,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return ToBytes(ppCipherValue);
		}

		struct SecretHeader
		{
			internal int length;
			internal int version;
		}

		private unsafe static string EncryptSecret(string input, byte[] sessionKey)
		{
			throw new NotImplementedException();
			int blocklen = 8;
			int keyindex = 0;

			byte[] buffer = new byte[8];
			int version = 1;

			fixed (byte* pBuf = buffer)
			{
				*(int*)pBuf = input.Length;
				*(int*)(pBuf + 4) = version;


			}

		}

		internal async Task<SecretInfo> GetSecret(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> ppCurrentValue = new RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>();
			RpcPointer<LARGE_INTEGER> pCurrentValueSetTime = new RpcPointer<LARGE_INTEGER>();
			RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> encryptedOldValue = new RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>>();
			RpcPointer<LARGE_INTEGER> pOldValueSetTime = new RpcPointer<LARGE_INTEGER>();
			var res = (Ntstatus)await this._proxy.LsarQuerySecret(
				handle,
				ppCurrentValue,
				pCurrentValueSetTime,
				encryptedOldValue,
				pOldValueSetTime,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new SecretInfo
			{
				CurrentValue = ToBytes(ppCurrentValue),
				CurrentValueSetTime = pCurrentValueSetTime.AsDateTime(),
				OldValue = ToBytes(encryptedOldValue),
				OldValueSetTime = pOldValueSetTime.AsDateTime()
			};
		}

		public async Task<UserPrincipalName> WhoAmI(CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<RPC_UNICODE_STRING>> userName = new();
			RpcPointer<RpcPointer<RPC_UNICODE_STRING>> domain = new();
			var res = (Ntstatus)await _proxy.LsarGetUserName(".",
				userName,
				domain,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new UserPrincipalName(userName.value.value.AsString(), domain.value.value.AsString());
		}

		internal async Task<SecurityIdentifier[]> EnumAccounts(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			RpcPointer<uint> enumContext = new();
			RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> enumBuffer = new();

			List<SecurityIdentifier> sids = new List<SecurityIdentifier>();
			Ntstatus res;
			do
			{
				res = (Ntstatus)await _proxy.LsarEnumerateAccounts(
					handle,
					enumContext,
					enumBuffer,
					MaxBufferSize,
					cancellationToken).ConfigureAwait(false);
				if (res == Ntstatus.STATUS_NO_MORE_ENTRIES)
					break;
				res.CheckAndThrow();
				var entries = enumBuffer.value.Information.value;
				for (int i = 0; i < entries.Length; i++)
				{
					sids.Add(entries[i].Sid.value.ToSid());
				}
			} while (res == Ntstatus.STATUS_MORE_ENTRIES);

			return sids.ToArray();
		}

		private static byte[] ToBytes(RpcPointer<RpcPointer<LSAPR_CR_CIPHER_VALUE>> ppCurrentValue)
		{
			return ppCurrentValue.value?.value.Buffer?.value.ToArray();
		}

		internal async Task<LsaAccount> CreateAccount(RpcContextHandle handle, SecurityIdentifier sid, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> pUserAccount = new();
			var res = (Ntstatus)await _proxy.LsarCreateAccount(handle, sid.ToRpcSid(), (uint)LsaAccountAccess.View, pUserAccount, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return new LsaAccount(this, pUserAccount.value);
		}

		internal async Task<PrivilegeInfo[]> GetAccountPrivileges(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<LSAPR_PRIVILEGE_SET>> privileges = new();
			var res = (Ntstatus)await _proxy.LsarEnumeratePrivilegesAccount(handle, privileges, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return Array.ConvertAll(privileges.value.value.Privilege, r => new PrivilegeInfo(r.Luid.AsPrivilege(), (PrivilegeAttributes)r.Attributes));
		}

		internal async Task<UserRightInfo[]> GetAccountRights(RpcContextHandle handle, SecurityIdentifier sid, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(sid);

			RpcPointer<LSAPR_USER_RIGHT_SET> userRights = new();
			var res = (Ntstatus)await _proxy.LsarEnumerateAccountRights(handle, sid.ToRpcSid(), userRights, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return Array.ConvertAll(userRights.value.UserRights.value, r =>
			{
				string name = r.AsString();

				return new UserRightInfo()
				{
					Name = name
				};
			});
		}

		internal async Task<LsaAccount> OpenAccount(RpcContextHandle handle, SecurityIdentifier sid, LsaAccountAccess access, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(sid);
			RpcPointer<RpcContextHandle> accountHandle = new();
			var res = (Ntstatus)await _proxy.LsarOpenAccount(handle, sid.ToRpcSid(), (uint)access, accountHandle, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return new LsaAccount(this, accountHandle.value);
		}

		internal async Task<string> LookupPrivilege(RpcContextHandle handle, long luid, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<RPC_UNICODE_STRING>> name = new();
			var res = (Ntstatus)await _proxy.LsarLookupPrivilegeName(handle, luid.ToLuid(), name, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return name.value.value.AsString();
		}

		internal async Task<Privilege> LookupPrivilege(RpcContextHandle handle, string name, CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(name);

			RpcPointer<LUID> value = new();
			var res = (Ntstatus)await _proxy.LsarLookupPrivilegeValue(handle, name.AsRpcString(), value, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return value.value.AsPrivilege();
		}

		internal async Task<SecurityIdentifier[]> GetAccountsWithPrivilege(RpcContextHandle handle, string privilege, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(privilege);

			RpcPointer<LSAPR_ACCOUNT_ENUM_BUFFER> enumerationBuffer = new();
			var res = (Ntstatus)await _proxy.LsarEnumerateAccountsWithUserRight(
					handle,
					new RpcPointer<RPC_UNICODE_STRING>(privilege.AsRpcString()),
					enumerationBuffer,
					cancellationToken).ConfigureAwait(false);
			if (res == Ntstatus.STATUS_NO_MORE_ENTRIES)
				res = Ntstatus.STATUS_SUCCESS;

			res.CheckAndThrow();

			var entries = enumerationBuffer.value.Information.value;

			return Array.ConvertAll(entries, r => r.Sid.value.ToSid());
		}

		internal async Task AddPrivileges(RpcContextHandle handle, IList<PrivilegeInfo> privileges, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(privileges);

			var res = (Ntstatus)await _proxy.LsarAddPrivilegesToAccount(handle, new LSAPR_PRIVILEGE_SET
			{
				PrivilegeCount = (uint)privileges.Count,
				Privilege = privileges.Select(r => new LSAPR_LUID_AND_ATTRIBUTES { Luid = r.Privilege.ToLuid(), Attributes = (uint)r.Attributes }).ToArray()
			}, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		internal async Task RemovePrivileges(RpcContextHandle handle, IList<PrivilegeInfo> privileges, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(privileges);

			var res = (Ntstatus)await _proxy.LsarRemovePrivilegesFromAccount(handle, 0, new RpcPointer<LSAPR_PRIVILEGE_SET>(new LSAPR_PRIVILEGE_SET
			{
				PrivilegeCount = (uint)privileges.Count,
				Privilege = privileges.Select(r => new LSAPR_LUID_AND_ATTRIBUTES { Luid = r.Privilege.ToLuid(), Attributes = (uint)r.Attributes }).ToArray()
			}), cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		internal async Task RemoveAllPrivileges(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var res = (Ntstatus)await _proxy.LsarRemovePrivilegesFromAccount(handle, 1, null, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		internal async Task<SystemAccessRights> GetSystemAccess(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			RpcPointer<uint> systemAccess = new();
			var res = (Ntstatus)await _proxy.LsarGetSystemAccessAccount(handle, systemAccess, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return (SystemAccessRights)systemAccess.value;
		}

		internal async Task SetSystemAccess(RpcContextHandle handle, SystemAccessRights rights, CancellationToken cancellationToken)
		{
			var res = (Ntstatus)await _proxy.LsarSetSystemAccessAccount(handle, (uint)rights, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}
	}

	static class LuidHelper
	{
		public static LUID ToLuid(this Privilege priv)
			=> ToLuid((long)priv);
		public static LUID ToLuid(this long l)
		{
			return new LUID
			{
				HighPart = (int)(l >> 32),
				LowPart = (uint)l
			};
		}
		public static long AsLong(this LUID luid)
		{
			long n = luid.HighPart;
			n <<= 32;
			n |= luid.LowPart;
			return n;
		}
		public static Privilege AsPrivilege(this LUID luid)
			=> (Privilege)luid.AsLong();
	}
}
