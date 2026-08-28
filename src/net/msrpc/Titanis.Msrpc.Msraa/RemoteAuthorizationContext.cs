using ms_raa;
using Titanis.DceRpc;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msraa
{
	// [MS-RAA] § 2.2.3.5 AUTHZR_SECURITY_ATTRIBUTE_V1
	public enum ClaimAttributeOptions : uint
	{
		None = 0,
		NonInheritable = 1,
		CaseSensitive = 2,
	}

	public record struct ClaimsAttribute(string Name, object?[] Values, ClaimAttributeOptions Options);

	public class AccessCheckResult
	{
		public uint GrantedAccessMask { get; set; }
		public Win32ErrorCode ErrorCode { get; set; }
	}

	public partial class RemoteAuthorizationContext
	{
		private readonly RemoteAuthorizationClient owner;
		private readonly RpcPointer<RpcContextHandle> handle;

		internal RemoteAuthorizationContext(RemoteAuthorizationClient owner, RpcPointer<RpcContextHandle> handle)
		{
			this.owner = owner;
			this.handle = handle;
		}

		public async Task<SecurityIdentifier> GetUserSid(CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<ms_raa.AUTHZR_CONTEXT_INFORMATION>?> contextInfo = new();
			var res = (Win32ErrorCode)await owner.ClientProxy.AuthzGetInformationFromContext(
				handle.value,
				ms_raa.AUTHZ_CONTEXT_INFORMATION_CLASS.AuthzContextInfoUserSid,
				contextInfo,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return contextInfo.value.value.ContextInfoUnion.pTokenUser.value.User.Sid.ToSid();
		}

		public Task<SecurityIdentifier[]> GetGroups(CancellationToken cancellationToken)
		{
			return GetGroups(ms_raa.AUTHZ_CONTEXT_INFORMATION_CLASS.AuthzContextInfoGroupsSids, cancellationToken);
		}

		public Task<SecurityIdentifier[]> GetRestrictedGroups(CancellationToken cancellationToken)
		{
			return GetGroups(ms_raa.AUTHZ_CONTEXT_INFORMATION_CLASS.AuthzContextInfoRestrictedSids, cancellationToken);
		}

		public Task<SecurityIdentifier[]> GetDeviceSids(CancellationToken cancellationToken)
		{
			return GetGroups(ms_raa.AUTHZ_CONTEXT_INFORMATION_CLASS.AuthzContextInfoDeviceSids, cancellationToken);
		}

		private async Task<SecurityIdentifier[]> GetGroups(ms_raa.AUTHZ_CONTEXT_INFORMATION_CLASS infoClass, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<ms_raa.AUTHZR_CONTEXT_INFORMATION>?> contextInfo = new();
			var res = (Win32ErrorCode)await owner.ClientProxy.AuthzGetInformationFromContext(
				handle.value,
				infoClass,
				contextInfo,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return Array.ConvertAll(contextInfo.value.value.ContextInfoUnion.pTokenGroups.value.Groups, r => r.Sid.ToSid());
		}

		public Task<ClaimsAttribute[]> GetUserClaims(CancellationToken cancellationToken) =>
			this.GetClaims(AUTHZ_CONTEXT_INFORMATION_CLASS.AuthzContextInfoUserClaims, cancellationToken);
		public Task<ClaimsAttribute[]> GetDeviceClaims(CancellationToken cancellationToken) =>
			this.GetClaims(AUTHZ_CONTEXT_INFORMATION_CLASS.AuthzContextInfoDeviceClaims, cancellationToken);
		private async Task<ClaimsAttribute[]> GetClaims(ms_raa.AUTHZ_CONTEXT_INFORMATION_CLASS infoClass, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<ms_raa.AUTHZR_CONTEXT_INFORMATION>?> contextInfo = new();
			var res = (Win32ErrorCode)await owner.ClientProxy.AuthzGetInformationFromContext(
				handle.value,
				infoClass,
				contextInfo,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			AUTHZR_SECURITY_ATTRIBUTE_V1[]? attrs = contextInfo.value.value.ContextInfoUnion.pTokenClaims.value.Attributes?.value;
			return (attrs is null) ? [] : Array.ConvertAll<ms_raa.AUTHZR_SECURITY_ATTRIBUTE_V1, ClaimsAttribute>(attrs, r => CreateClaimAttribute(r));
		}

		private ClaimsAttribute CreateClaimAttribute(AUTHZR_SECURITY_ATTRIBUTE_V1 r)
		{
			return new ClaimsAttribute(r.Value.value, Array.ConvertAll(r.Values.value, r => CreateAttrValue(r)), (ClaimAttributeOptions)r.Flags);
		}

		// [MS-RAA] § 2.2.3.6 AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE
		enum AttrValueType
		{
			Int64 = 1,
			UInt64 = 2,
			// TODO: I'm guessing this is some other AD type that is represented as a bigint, like a datetime...
			UInt64_2 = 6,
			String = 3,
		}

		private object? CreateAttrValue(AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE r)
		{
			// [MS-RAA] § 2.2.3.6 AUTHZR_SECURITY_ATTRIBUTE_V1_VALUE
			return (AttrValueType)r.ValueType switch
			{
				AttrValueType.Int64 => r.AttributeUnion.Int64,
				AttrValueType.UInt64 => r.AttributeUnion.Uint64,
				AttrValueType.UInt64_2 => r.AttributeUnion.Uint64,
				AttrValueType.String => r.AttributeUnion.String.Value.value,
				_ => null
			};
		}

		public async Task<AccessCheckResult> AccessCheck(
			SecurityDescriptor sd,
			uint access,
			SecurityIdentifier? principalSelf,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(sd);

			var bytes = sd.ToByteArray();
			RpcPointer<AUTHZR_ACCESS_REPLY> pReply = new(new AUTHZR_ACCESS_REPLY
			{
				ResultListLength = 1,
				GrantedAccessMask = new RpcPointer<uint[]>([0]),
				Error = new RpcPointer<uint[]>([0]),
			});
			var res = (Win32ErrorCode)await owner.ClientProxy.AuthzrAccessCheck(
				handle.value,
				0,
				new AUTHZR_ACCESS_REQUEST
				{
					DesiredAccess = access,
					PrincipalSelfSid = (principalSelf is null) ? null : new RpcPointer<ms_dtyp.RPC_SID>(principalSelf.ToRpcSid()),
					ObjectTypeListLength = 0,
					ObjectTypeList = null,
				},
				1,
				[new SR_SD { dwLength = (uint)bytes.Length, pSrSd = new RpcPointer<byte[]>(bytes) }],
				pReply,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return new AccessCheckResult()
			{
				GrantedAccessMask = pReply.value.GrantedAccessMask.value[0],
				ErrorCode = (Win32ErrorCode)pReply.value.Error.value[0]
			};
		}
	}

	partial class RemoteAuthorizationContext : IDisposable, IAsyncDisposable
	{
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					this.owner.ClientProxy.AuthzrFreeContext(this.handle, CancellationToken.None).Wait();
				}

				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~RemoteAuthorizationContext()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public async ValueTask DisposeAsync()
		{
			await owner.ClientProxy.AuthzrFreeContext(handle, CancellationToken.None).ConfigureAwait(false);
			this.disposedValue = true;
		}
	}
}