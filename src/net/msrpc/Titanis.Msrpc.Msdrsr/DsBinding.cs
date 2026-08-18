using ms_drsr;
using System.Buffers.Binary;
using System.Diagnostics;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Compression;
using Titanis.DceRpc;
using Titanis.Ldap;
using Titanis.Winterop;

namespace Titanis.Msrpc.Msdrsr
{
	public interface IDrsChangeCallback
	{
		Task OnObjectReplicated(DsObject obj);
		Task OnError(DsName objectName, Exception exception);
	}

	// [MS-DRSR] § 4.1.4.1.8 DS_NAME_ERROR
	public enum DsrepNameError : uint
	{
		Success = 0,
		Resolving = 1,
		NotFound = 2,
		NotUnique = 3,
		NoMapping = 4,
		DomainOnly = 5,
		TrustReferral = 7,
		SidHistoryUnknown = 0xFFFFFFF2,
		SidHistoryAlias = 0xFFFFFFF3,
		SidHistoryGroup = 0xFFFFFFF4,
		SidHistoryUser = 0xFFFFFFF5,
		SidUnknown = 0xFFFFFFF6,
		SidAlias = 0xFFFFFFF7,
		SidGroup = 0xFFFFFFF8,
		SidUser = 0xFFFFFFF9,
		SchemaGuidControlRight = 0xFFFFFFFA,
		SchemaGuidClass = 0xFFFFFFFB,
		SchemaGuidAttributeSet = 0xFFFFFFFC,
		SchemaGuidAttribute = 0xFFFFFFFD,
		SchemaGuidNotFound = 0xFFFFFFFE,
		ForeignPrincipalObject = 0xFFFFFFFF,
	}

	public class DsrepCrackedName
	{
		public string? OfferedName { get; set; }
		public DsCrackNameFormat OfferedFormat { get; set; }
		public string? CrackedDomain { get; set; }
		public string? CrackedName { get; set; }
		public DsCrackNameResultFormat ResultFormat { get; set; }
		public DsrepNameError Status { get; set; }
	}

	public partial class DsBinding : IDisposable, IAsyncDisposable
	{
		internal DsBinding(RpcContextHandle hbind, DirectoryReplicationClient owner, DRS_EXTENSIONS_INT2 serverExt)
		{
			this.hbind = hbind;
			this.owner = owner;
			this.serverExt = serverExt;
		}

		private readonly RpcContextHandle hbind;
		private readonly DirectoryReplicationClient owner;
		private readonly DRS_EXTENSIONS_INT2 serverExt;
		private bool _isDisposed;

		protected virtual void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				if (disposing)
				{
					this.owner.Unbind(this.hbind, CancellationToken.None).Wait();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				_isDisposed = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public async ValueTask DisposeAsync()
		{
			if (!this._isDisposed)
			{
				await this.owner.Unbind(this.hbind, CancellationToken.None).ConfigureAwait(false);
				this._isDisposed = true;
				GC.SuppressFinalize(this);
			}
		}

		public async Task<DomainControllerInfo[]> GetDcInfo(string domainName, CancellationToken cancellationToken)
		{
			var drsr = this.owner;

			RpcPointer<uint> pdwOutVersion = new();
			RpcPointer<ms_drsr.DRS_MSG_DCINFOREPLY> pmsgOut = new();
			var res = (Win32ErrorCode)await drsr.proxy.IDL_DRSDomainControllerInfo(
				this.hbind,
				1,
				new ms_drsr.DRS_MSG_DCINFOREQ
				{
					unionSwitch = 1,
					V1 = new ms_drsr.DRS_MSG_DCINFOREQ_V1
					{
						Domain = new RpcPointer<string>(domainName),
						InfoLevel = 2,
					},
				},
				pdwOutVersion,
				pmsgOut,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			var dcInfos = Array.ConvertAll(pmsgOut.value.V2.rItems.value, r => new DomainControllerInfo(
				r.NetbiosName?.value,
				r.DnsHostName?.value,
				r.SiteName?.value,
				r.SiteObjectName?.value,
				r.ComputerObjectName?.value,
				r.ServerObjectName?.value,
				r.NtdsDsaObjectName?.value,
				r.fIsPdc != 0,
				r.fDsEnabled != 0,
				r.fIsGc != 0,
				r.SiteObjectGuid,
				r.ComputerObjectGuid,
				r.ServerObjectGuid,
				r.NtdsDsaObjectGuid
				));
			return dcInfos;
		}


		#region Topology

		public Task<string[]> GetSites(DsCrackNameResultFormat format, CancellationToken cancellationToken) =>
			this.CrackName(DsCrackNameFormat.ListSites, format, cancellationToken);
		public Task<string[]> GetRoles(DsCrackNameResultFormat format, CancellationToken cancellationToken)
			=> this.CrackName(DsCrackNameFormat.ListRoles, format, cancellationToken);
		public Task<string[]> GetDomainsInSite(string site, DsCrackNameResultFormat format, CancellationToken cancellationToken) =>
			this.CrackName(site, DsCrackNameFormat.ListDomainsInSite, format, cancellationToken);
		public Task<string[]> GetDomains(DsCrackNameResultFormat format, CancellationToken cancellationToken) =>
			this.CrackName(DsCrackNameFormat.ListDomains, format, cancellationToken);
		public Task<string[]> GetPartitions(DsCrackNameResultFormat format, CancellationToken cancellationToken) =>
			this.CrackName(DsCrackNameFormat.ListPartitions, format, cancellationToken);
		public Task<string[]> GetGlobalCatalogServers(DsCrackNameResultFormat format, CancellationToken cancellationToken) =>
			this.CrackName(DsCrackNameFormat.ListGlobalCatalogServers, format, cancellationToken);

		private async Task<string[]> CrackName(DsCrackNameFormat offeredFormat, DsCrackNameResultFormat desiredFormat, CancellationToken cancellationToken)
		{
			var names = await CrackNames(["."], offeredFormat, desiredFormat, cancellationToken).ConfigureAwait(false);
			return Array.ConvertAll(names, r => r.CrackedName);
		}

		public async Task<string[]> CrackName(string name, DsCrackNameFormat offeredFormat, DsCrackNameResultFormat desiredFormat, CancellationToken cancellationToken)
		{
			var names=await CrackNames([name], offeredFormat, desiredFormat, cancellationToken).ConfigureAwait(false);
			return Array.ConvertAll(names, r => r.CrackedName);
		}

		public async Task<DsrepCrackedName[]> CrackNames(string[] names, DsCrackNameFormat offeredFormat, DsCrackNameResultFormat desiredFormat, CancellationToken cancellationToken)
		{
			if (names.IsNullOrEmpty())
				throw new ArgumentNullException(nameof(names));

			const int CrackRequestVersion = 1;
			const int CrackResponseVersion = 1;
			RpcPointer<uint> pdwOutVersion = new();
			RpcPointer<DRS_MSG_CRACKREPLY> pmsgOut = new();
			var res = (Win32ErrorCode)await owner.proxy.IDL_DRSCrackNames(
				hbind,
				CrackRequestVersion,
				new DRS_MSG_CRACKREQ()
				{
					unionSwitch = CrackRequestVersion,
					V1 = new DRS_MSG_CRACKREQ_V1
					{
						CodePage = 1252,
						LocaleId = 1033,
						dwFlags = 0,
						formatOffered = (uint)offeredFormat,
						formatDesired = (uint)desiredFormat,
						cNames = (uint)names.Length,
						rpNames = new RpcPointer<RpcPointer<string>[]>(Array.ConvertAll(names, r => new RpcPointer<string>(r)))
					}
				},
				pdwOutVersion,
				pmsgOut,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			if (pmsgOut.value.unionSwitch == CrackResponseVersion)
			{
				DS_NAME_RESULT_ITEMW[]? results = pmsgOut.value.V1.pResult.value.rItems.value;
				if (results != null)
				{
					DsrepCrackedName[] cracked = new DsrepCrackedName[results.Length];
					for (int i = 0; i < results.Length; i++)
					{
						DS_NAME_RESULT_ITEMW item = results[i];

						cracked[i] = new DsrepCrackedName
						{
							OfferedName = (i < names.Length) ? names[i] : null,
							OfferedFormat = offeredFormat,
							ResultFormat = desiredFormat,
							CrackedDomain = item.pDomain?.value,
							CrackedName = item.pName?.value,
							Status = (DsrepNameError)item.status
						};
					}

					return cracked;
				}

				return [];
			}
			else
			{
				throw new NotSupportedException($"The server replied with an unsupported version number: {pmsgOut.value.unionSwitch}");
			}
		}
		#endregion

		#region Keys
		public async Task WriteNgcKey(
			LdapDistinguishedName accountDn,
			byte[] key,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(accountDn);
			ArgumentNullException.ThrowIfNull(key);

			RpcPointer<uint> pdwOutVersion = new();
			RpcPointer<DRS_MSG_WRITENGCKEYREPLY> pmsgOut = new();
			var res = (Win32ErrorCode)await owner.proxy.IDL_DRSWriteNgcKey(
				hbind,
				1,
				new DRS_MSG_WRITENGCKEYREQ
				{
					unionSwitch = 1,
					V1 = new DRS_MSG_WRITENGCKEYREQ_V1
					{
						pwszAccount = new RpcPointer<string>(accountDn.ToString()),
						cNgcKey = (uint)key.Length,
						pNgcKey = new RpcPointer<byte[]>(key)
					}
				},
				pdwOutVersion,
				pmsgOut,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			if (pmsgOut.value.unionSwitch == 1)
			{
				((Win32ErrorCode)pmsgOut.value.V1.retVal).CheckAndThrow();
			}
			else
			{
				throw new NotSupportedException($"Server returned with unsupported version: {pmsgOut.value.unionSwitch}");
			}
		}
		public async Task<byte[]> ReadNgcKey(LdapDistinguishedName accountDn, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(accountDn);
			RpcPointer<uint> pdwOutVersion = new();
			RpcPointer<DRS_MSG_READNGCKEYREPLY> pmsgOut = new();
			var res = (Win32ErrorCode)(await owner.proxy.IDL_DRSReadNgcKey(
				hbind,
				1,
				new DRS_MSG_READNGCKEYREQ
				{
					unionSwitch = 1,
					V1 = new DRS_MSG_READNGCKEYREQ_V1
					{
						pwszAccount = new RpcPointer<string>(accountDn.ToString())
					}
				},
				pdwOutVersion,
				pmsgOut,
				cancellationToken
				).ConfigureAwait(false));

			if (pmsgOut.value.unionSwitch == 1)
			{
				((Win32ErrorCode)pmsgOut.value.V1.retVal).CheckAndThrow();
				return pmsgOut.value.V1.pNgcKey.value;
			}
			else
			{
				throw new NotSupportedException($"Server returned with unsupported version: {pmsgOut.value.unionSwitch}");
			}
		}
		#endregion

		public async Task AddSidHistory(
			DsrepAddSidHistoryOptions options,
			string sourceDomain,
			string sourcePrincipal,
			string? sourceDc,
			string? sourceCredsUser,
			string? sourceCredsDomain,
			string? sourceCredsPassword,
			string destDomain,
			string destPrincipal,
			CancellationToken cancellationToken
			)
		{
			RpcPointer<uint> pdwOutVersion = new();
			RpcPointer<DRS_MSG_ADDSIDREPLY> pmsgOut = new();
			var res = (Win32ErrorCode)await owner.proxy.IDL_DRSAddSidHistory(
				hbind,
				1,
				new DRS_MSG_ADDSIDREQ
				{
					unionSwitch = 1,
					V1 = new DRS_MSG_ADDSIDREQ_V1
					{
						Flags = (uint)options,
						SrcDomain = new RpcPointer<string>(sourceDomain),
						SrcPrincipal = new RpcPointer<string>(sourcePrincipal),
						SrcDomainController = sourceDc.ToRpcPointerOrNull(),
						SrcCredsUserLength = (uint)(sourceCredsUser?.Length ?? 0),
						SrcCredsUser = (sourceCredsUser != null) ? new RpcPointer<char[]>(sourceCredsUser.ToCharArray()) : null,
						SrcCredsDomainLength = (uint)(sourceCredsDomain?.Length ?? 0),
						SrcCredsDomain = (sourceCredsDomain != null) ? new RpcPointer<char[]>(sourceCredsDomain.ToCharArray()) : null,
						SrcCredsPasswordLength = (uint)(sourceCredsPassword?.Length ?? 0),
						SrcCredsPassword = (sourceCredsPassword != null) ? new RpcPointer<char[]>(sourceCredsPassword.ToCharArray()) : null,
						DstDomain = destDomain.ToRpcPointerOrNull(),
						DstPrincipal = destPrincipal.ToRpcPointerOrNull()
					}
				},
				pdwOutVersion,
				pmsgOut,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			if (pmsgOut.value.unionSwitch == 1)
			{
				((Win32ErrorCode)pmsgOut.value.V1.dwWin32Error).CheckAndThrow();
			}
		}

		#region Replication
		// [MS-DRSR] § 4.1.13.1.5 DRS_MSG_GETREPLINFO_REPLY
		enum ReplInfoReplySwitch : uint
		{
			Neighbors = 0,
			Cursors = 1,
			ObjectMetadata = 2,
			KccDsaConnectFailures = 3,
			KccDsaLinkFailures = 4,
			PendingOps = 5,
			AttributeValueMetadata = 6,
			Cursors2 = 7,
			Cursors3 = 8,
			ObjectMetadata2 = 9,
			AttributeValueMetadata2 = 10,
			ServerOutgoingCalls = 0xFFFFFFFA,
			UpToDateVectorV1 = 0xFFFFFFFB,
			ClientContexts = 0xFFFFFFFC,
			RepsTo = 0xFFFFFFFE
		}

		public async Task<DsrepVector[]> GetUptodateVectors(LdapDistinguishedName? objectDn, CancellationToken cancellationToken)
		{
			var reply = await GetReplicationInfo(ReplicationInfoKind.UpToDateVectorV1, objectDn, cancellationToken).ConfigureAwait(false);

			if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.UpToDateVectorV1)
			{
				return Array.ConvertAll(reply.value.pUpToDateVec.value.rgCursors, ToVector);
			}
			else
			{
				throw new NotSupportedException($"The server returned an unsupported reply: {(ReplInfoReplySwitch)reply.value.unionSwitch}");
			}
		}

		private DsrepVector ToVector(UPTODATE_CURSOR_V1 r)
		{
			return new DsrepVector(
				r.uuidDsa,
				r.usnHighPropUpdate
				);
		}

		public async Task<DsrepNeighbor[]> GetRepsFromNeighbors(LdapDistinguishedName? objectDn, CancellationToken cancellationToken)
		{
			var reply = await GetReplicationInfo(ReplicationInfoKind.Neighbors, objectDn, cancellationToken).ConfigureAwait(false);

			if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.Neighbors)
			{
				return Array.ConvertAll(reply.value.pNeighbors.value.rgNeighbor, ToNeighbor);
			}
			else
			{
				throw new NotSupportedException($"The server returned an unsupported reply: {(ReplInfoReplySwitch)reply.value.unionSwitch}");
			}
		}

		public async Task<DsrepNeighbor[]> GetRepsToNeighbors(LdapDistinguishedName? objectDN, CancellationToken cancellationToken)
		{
			var reply = await GetReplicationInfo(ReplicationInfoKind.RepsTo, objectDN, cancellationToken).ConfigureAwait(false);

			if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.RepsTo)
			{
				return Array.ConvertAll(reply.value.pRepsTo.value.rgNeighbor, ToNeighbor);
			}
			else
			{
				throw new NotSupportedException($"The server returned an unsupported reply: {(ReplInfoReplySwitch)reply.value.unionSwitch}");
			}
		}

		private static DsrepNeighbor ToNeighbor(DS_REPL_NEIGHBORW r)
		{
			return new DsrepNeighbor(
				LdapDistinguishedName.Parse(r.pszNamingContext.value),
				LdapDistinguishedName.Parse(r.pszSourceDsaDN.value),
				(r.pszSourceDsaAddress.value != null) ? LdapDistinguishedName.Parse(r.pszSourceDsaAddress.value) : null,
				LdapDistinguishedName.Parse(r.pszAsyncIntersiteTransportDN?.value),
				(DrsOptions)r.dwReplicaFlags,
				r.uuidNamingContextObjGuid,
				r.uuidSourceDsaObjGuid,
				r.uuidSourceDsaInvocationID,
				r.uuidAsyncIntersiteTransportObjGuid,
				r.usnLastObjChangeSynced,
				r.usnAttributeFilter,
				r.ftimeLastSyncSuccess.ToDateTime(),
				r.ftimeLastSyncAttempt.ToDateTime(),
				(Win32ErrorCode)r.dwLastSyncResult
				);
		}


		public async Task<DsrepCursor[]> GetReplicationCursors(LdapDistinguishedName objectDN, DsrepCursorLevel level, CancellationToken cancellationToken)
		{
			switch (level)
			{
				case DsrepCursorLevel.Cursor:
				case DsrepCursorLevel.Cursor2:
				case DsrepCursorLevel.Cursor3:
					{
						var reply = await GetReplicationInfo((ReplicationInfoKind)level, objectDN, cancellationToken).ConfigureAwait(false);

						if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.Cursors)
						{
							return Array.ConvertAll(reply.value.pCursors.value.rgCursor, ToCursor);
						}
						else if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.Cursors2)
						{
							return Array.ConvertAll(reply.value.pCursors2.value.rgCursor, ToCursor);
						}
						else if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.Cursors3)
						{
							return Array.ConvertAll(reply.value.pCursors3.value.rgCursor, ToCursor);
						}
						else
						{
							throw new NotSupportedException($"The server returned an unsupported reply: {(ReplInfoReplySwitch)reply.value.unionSwitch}");
						}

					}
				default:
					throw new ArgumentException($"Invalid cursor level: {level}.", nameof(level));
			}
		}

		private DsrepCursor ToCursor(DS_REPL_CURSOR input)
		{
			return new DsrepCursor(
				input.uuidSourceDsaInvocationID,
				input.usnAttributeFilter
				);
		}

		private DsrepCursor ToCursor(DS_REPL_CURSOR_2 input)
		{
			return new DsrepCursor(
				input.uuidSourceDsaInvocationID,
				input.usnAttributeFilter,
				input.ftimeLastSyncSuccess.ToDateTime()
				);
		}

		private DsrepCursor ToCursor(DS_REPL_CURSOR_3W input)
		{
			return new DsrepCursor(
				input.uuidSourceDsaInvocationID,
				input.usnAttributeFilter,
				input.ftimeLastSyncSuccess.ToDateTime(),
				(input.pszSourceDsaDN != null) ? LdapDistinguishedName.Parse(input.pszSourceDsaDN.value) : null
				);
		}




		public async Task<DsrepObjectMetadata[]> GetObjectMetadata(LdapDistinguishedName objectDN, DsrepObjectMetadataLevel level, CancellationToken cancellationToken)
		{
			switch (level)
			{
				case DsrepObjectMetadataLevel.Metadata:
				case DsrepObjectMetadataLevel.Metadata2:
					{
						var reply = await GetReplicationInfo((ReplicationInfoKind)level, objectDN, cancellationToken).ConfigureAwait(false);

						if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.ObjectMetadata)
						{
							return Array.ConvertAll(reply.value.pObjMetaData.value.rgMetaData, ToObjMetadata);
						}
						else if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.ObjectMetadata2)
						{
							return Array.ConvertAll(reply.value.pObjMetaData2.value.rgMetaData, ToObjMetadata);
						}
						else
						{
							throw new NotSupportedException($"The server returned an unsupported reply: {(ReplInfoReplySwitch)reply.value.unionSwitch}");
						}

					}
				default:
					throw new ArgumentException($"Invalid cursor level: {level}.", nameof(level));
			}
		}

		private DsrepObjectMetadata ToObjMetadata(DS_REPL_ATTR_META_DATA r)
		{
			return new DsrepObjectMetadata(
				r.pszAttributeName.value,
				(int)r.dwVersion,
				r.ftimeLastOriginatingChange.ToDateTime(),
				r.uuidLastOriginatingDsaInvocationID,
				r.usnOriginatingChange,
				r.usnLocalChange
				);
		}

		private DsrepObjectMetadata ToObjMetadata(DS_REPL_ATTR_META_DATA_2 r)
		{
			return new DsrepObjectMetadata(
				r.pszAttributeName.value,
				(int)r.dwVersion,
				r.ftimeLastOriginatingChange.ToDateTime(),
				r.uuidLastOriginatingDsaInvocationID,
				r.usnOriginatingChange,
				r.usnLocalChange,
				(r.pszLastOriginatingDsaDN != null) ? LdapDistinguishedName.Parse(r.pszLastOriginatingDsaDN.value) : null
				);
		}




		public async Task<DsrepAttributeMetadataResult?> GetAttributeMetadata(LdapDistinguishedName objectDN, string? attributeName, string? value, CancellationToken cancellationToken)
		{
			// TODO: Add levels

			var reply = await GetReplicationInfo(ReplicationInfoKind.Metadata2ForAttributeValue, objectDN, attributeName, value, cancellationToken).ConfigureAwait(false);

			if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.AttributeValueMetadata)
			{
				if (reply.value.pAttrValueMetaData != null)
				{
					return new DsrepAttributeMetadataResult(reply.value.pAttrValueMetaData.value.dwEnumerationContext, Array.ConvertAll(reply.value.pAttrValueMetaData.value.rgMetaData, ToValueMetaedata));
				}
				return null;
			}
			else if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.AttributeValueMetadata2)
			{
				if (reply.value.pAttrValueMetaData2 != null)
				{
					return new DsrepAttributeMetadataResult(reply.value.pAttrValueMetaData2.value.dwEnumerationContext, Array.ConvertAll(reply.value.pAttrValueMetaData2.value.rgMetaData, ToValueMetaedata));
				}
				return null;
			}
			else
			{
				throw new NotSupportedException($"The server returned an unsupported reply: {(ReplInfoReplySwitch)reply.value.unionSwitch}");
			}
		}

		private DsrepAttributeValueMetadata ToValueMetaedata(DS_REPL_VALUE_META_DATA r)
		{
			return new DsrepAttributeValueMetadata(
				r.pszAttributeName.value,
				LdapDistinguishedName.Parse(r.pszObjectDn.value),
				r.pbData?.value,
				r.ftimeDeleted.ToDateTimeOrNull(),
				r.ftimeCreated.ToDateTime(),
				(int)r.dwVersion,
				r.ftimeLastOriginatingChange.ToDateTime(),
				r.uuidLastOriginatingDsaInvocationID,
				r.usnOriginatingChange,
				r.usnLocalChange
				);
		}

		private DsrepAttributeValueMetadata ToValueMetaedata(DS_REPL_VALUE_META_DATA_2 r)
		{
			return new DsrepAttributeValueMetadata(
				r.pszAttributeName.value,
				LdapDistinguishedName.Parse(r.pszObjectDn.value),
				r.pbData?.value,
				r.ftimeDeleted.ToDateTimeOrNull(),
				r.ftimeCreated.ToDateTime(),
				(int)r.dwVersion,
				r.ftimeLastOriginatingChange.ToDateTime(),
				r.uuidLastOriginatingDsaInvocationID,
				r.usnOriginatingChange,
				r.usnLocalChange,
				LdapDistinguishedName.Parse(r.pszLastOriginatingDsaDN.value)
				);
		}



		public async Task<DsrepPendingOp[]> GetPendingOps(CancellationToken cancellationToken)
		{
			var reply = await GetReplicationInfo(ReplicationInfoKind.PendingOps, null, cancellationToken).ConfigureAwait(false);

			if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.PendingOps)
			{
				return Array.ConvertAll(reply.value.pPendingOps.value.rgPendingOp, ToPendingOp);
			}
			else
			{
				throw new NotSupportedException($"The server returned an unsupported reply: {(ReplInfoReplySwitch)reply.value.unionSwitch}");
			}
		}

		private DsrepPendingOp ToPendingOp(DS_REPL_OPW r)
		{
			return new DsrepPendingOp(
				r.ftimeEnqueued.ToDateTime(),
				r.ulSerialNumber,
				r.ulPriority,
				r.OpType,
				r.ulOptions,
				LdapDistinguishedName.Parse(r.pszNamingContext.value),
				LdapDistinguishedName.Parse(r.pszDsaDN.value),
				r.pszDsaAddress.value,
				r.uuidNamingContextObjGuid,
				r.uuidDsaObjGuid
				);
		}



		public async Task<DsrepKccFailure[]> GetKccFailures(LdapDistinguishedName? objectDn, DsrepKccFailureKind kind, CancellationToken cancellationToken)
		{
			switch (kind)
			{
				case DsrepKccFailureKind.Connect:
				case DsrepKccFailureKind.Link:
					{
						var reply = await GetReplicationInfo((ReplicationInfoKind)kind, objectDn, cancellationToken).ConfigureAwait(false);

						if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.KccDsaConnectFailures)
						{
							return Array.ConvertAll(reply.value.pConnectFailures.value.rgDsaFailure, ToKccFailure);
						}
						else if ((ReplInfoReplySwitch)reply.value.unionSwitch == ReplInfoReplySwitch.KccDsaLinkFailures)
						{
							return Array.ConvertAll(reply.value.pLinkFailures.value.rgDsaFailure, ToKccFailure);
						}
						else
						{
							throw new NotSupportedException($"The server returned an unsupported reply: {(ReplInfoReplySwitch)reply.value.unionSwitch}");
						}
					}
				default:
					throw new ArgumentException($"Invalid KCC failure kind: {kind}.", nameof(kind));
			}
		}

		private DsrepKccFailure ToKccFailure(DS_REPL_KCC_DSA_FAILUREW r)
		{
			return new DsrepKccFailure(
				LdapDistinguishedName.Parse(r.pszDsaDN.value),
				r.uuidDsaObjGuid,
				r.ftimeFirstFailure.ToDateTime(),
				r.cNumFailures,
				(Win32ErrorCode)r.dwLastResult
				);
		}



		private Task<RpcPointer<DRS_MSG_GETREPLINFO_REPLY>> GetReplicationInfo(ReplicationInfoKind kind, LdapDistinguishedName? objectDn, CancellationToken cancellationToken) => GetReplicationInfo(kind, objectDn, null, null, cancellationToken);
		private async Task<RpcPointer<DRS_MSG_GETREPLINFO_REPLY>> GetReplicationInfo(ReplicationInfoKind kind, LdapDistinguishedName? objectDn, string? attribute, string? value, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pdwOutVersion = new();
			RpcPointer<DRS_MSG_GETREPLINFO_REPLY> pmsgOut = new();
			var res = (Win32ErrorCode)await owner.proxy.IDL_DRSGetReplInfo(
				hbind,
				2,
				new DRS_MSG_GETREPLINFO_REQ
				{
					unionSwitch = 2,
					V2 = new DRS_MSG_GETREPLINFO_REQ_V2
					{
						InfoType = (uint)kind,
						pszObjectDN = (objectDn == null) ? null : new RpcPointer<string>(objectDn.ToString()),
						uuidSourceDsaObjGuid = default,
						ulFlags = 0,
						pszAttributeName = string.IsNullOrEmpty(attribute) ? null : new RpcPointer<string>(attribute),
						pszValueDN = value.ToRpcPointerOrNull(),
						dwEnumerationContext = 0,
					}
				},
				pdwOutVersion,
				pmsgOut,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return pmsgOut;
		}
		#endregion

		// [MS-DRSR] § 5.16.4 ATTRTYP-to-OID Conversion
		private static readonly Dictionary<string, int> defaultPrefixLookup = new Dictionary<string, int>()
		{
			{ "2.5.4", 0 },
			{ "2.5.6", 1 },
			{ "1.2.840.113556.1.2", 2 },
			{ "1.2.840.113556.1.3", 3 },
			{ "2.16.840.1.101.2.2.1", 4 },
			{ "2.16.840.1.101.2.2.3", 5 },
			{ "2.16.840.1.101.2.1.5", 6 },
			{ "2.16.840.1.101.2.1.4", 7 },
			{ "2.5.5", 8 },
			{ "1.2.840.113556.1.4", 9 },
			{ "1.2.840.113556.1.5", 10 },
			{ "0.9.2342.19200300.100", 19 },
			{ "2.16.840.1.113730.3", 20 },
			{ "0.9.2342.19200300.100.1", 21 },
			{ "2.16.840.1.113730.3.1", 22 },
			{ "1.2.840.113556.1.5.7000", 23 },
			{ "2.5.21", 24 },
			{ "2.5.18", 25 },
			{ "2.5.20", 26 },

		};
		public async Task Add(DsName name, IReadOnlyList<LdapAttribute> attributes, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pdwOutVersion = new();
			RpcPointer<DRS_MSG_ADDENTRYREPLY> pmsgOut = new();
			var res = await owner.proxy.IDL_DRSAddEntry(
				hbind,
				2,
				new DRS_MSG_ADDENTRYREQ
				{
					unionSwitch = 2,
					V2 = new DRS_MSG_ADDENTRYREQ_V2
					{
						EntInfList = new ENTINFLIST
						{
							Entinf = new ENTINF
							{
								pName = name.ToRpcDsName(),
								ulFlags = 0,
								AttrBlock = CreateAttrBlock(attributes, defaultPrefixLookup, null)
							}
						}
					}
				},
				pdwOutVersion,
				pmsgOut,
				cancellationToken
				).ConfigureAwait(false);
		}

		private static ATTRBLOCK CreateAttrBlock(
			IReadOnlyList<LdapAttribute> attributes,
			Dictionary<string, int> prefixLookup,
			List<PrefixTableEntry>? prefixList
			)
		{
			ATTR[] attrs = new ATTR[attributes.Count];
			for (int i = 0; i < attributes.Count; i++)
			{
				LdapAttribute? attribute = attributes[i];

				ATTR attr = new ATTR
				{
					attrTyp = OidToAttrtyp(attribute.AttributeType.Oid, prefixLookup, prefixList),
					AttrVal = new ATTRVALBLOCK
					{
						valCount = (uint)attribute.Values.Length,
						pAVal = new RpcPointer<ATTRVAL[]>(Array.ConvertAll(attribute.Values, r =>
						{
							var bytes = attribute.AttributeType.Syntax.EncodeDsrep(r);
							return new ATTRVAL { valLen = (uint)bytes.Length, pVal = new RpcPointer<byte[]>(bytes) };
						}))
					}
				};
				attrs[i] = attr;
			}
			return new ATTRBLOCK { attrCount = (uint)attrs.Length, pAttr = new RpcPointer<ATTR[]>(attrs) };
		}

		private static uint OidToAttrtyp(
			string attrOid,
			Dictionary<string, int> prefixLookup,
			List<PrefixTableEntry>? prefixList
			)
		{
			if (attrOid is null)
				return 0;

			//var m = rgxOid.Match(attrOid);

			int isep = attrOid.LastIndexOf('.');
			bool isValid;
			int prefixIndex;
			if (isep > 0)
			{
				try
				{
					ushort last = ushort.Parse(attrOid.Substring(isep + 1));
					string prefix = attrOid.Substring(0, isep);
					if (!prefixLookup.TryGetValue(prefix, out prefixIndex))
					{
						var oid = new Asn1Oid(prefix);
						var bytes = Asn1DerEncoder.EncodeValue(oid).ToArray();
						prefixIndex = prefixList.Count;
						prefixList.Add(new PrefixTableEntry { ndx = (uint)prefixIndex, prefix = new OID_t { length = (uint)bytes.Length, elements = new RpcPointer<byte[]>(bytes) } });
						prefixLookup.Add(prefix, prefixIndex);

						isValid = true;
					}
					else
						isValid = true;

					var tag = (uint)(prefixIndex << 16) | last;
					return tag;
				}
				catch
				{
					isValid = false;
				}
			}

			throw new ArgumentException($"Attribute OID '{attrOid}' is invalid.", nameof(attrOid));
		}

		public async Task<UsnVector> GetNcChanges(
			DomainControllerInfo dcInfo,
			IAsyncEnumerable<DsName> objectNames,
			string[] attributeOids,
			int maxObjCount,
			int maxByteCount,
			UsnVector usnvecFrom_,
			IDrsChangeCallback callback,
			int parallelDegree,
			ExtendedOpRequest exop,
			CancellationToken cancellationToken
			)
		{
			ArgumentNullException.ThrowIfNull(attributeOids);
			ArgumentNullException.ThrowIfNull(callback);

			Dictionary<string, int> prefixLookup = new Dictionary<string, int>();
			uint[] attrTags;
			PrefixTableEntry[] prefixes;
			{
				List<PrefixTableEntry> prefixList = new(attributeOids.Length);
				List<uint> attrTagsList = new List<uint>(attributeOids.Length);
				foreach (var attrOid in attributeOids)
				{
					var tag = OidToAttrtyp(attrOid, prefixLookup, prefixList);
					if (tag == 0)
						continue;

					attrTagsList.Add(tag);
				}

				attrTags = attrTagsList.ToArray();
				prefixes = prefixList.ToArray();
			}

			ArgumentNullException.ThrowIfNull(dcInfo);
			ArgumentNullException.ThrowIfNull(objectNames);

			// Replication parameters
			Guid clientGuid = dcInfo.NtdsDsaObjectGuid;
			Guid invocIdSrc = dcInfo.NtdsDsaObjectGuid;
			DrsOptions options = DrsOptions.WriteRep | DrsOptions.InitSync | DrsOptions.PeriodicSync | DrsOptions.GetNcSize | DrsOptions.NeverSynced | DrsOptions.UseCompression | DrsOptions.GetAllGroupMembership;

			var sessionKey = this.owner.proxy.BoundAuthContext.AuthContext.GetSessionKey().ToArray();

			await Parallel.ForEachAsync(objectNames, new ParallelOptions() { CancellationToken = cancellationToken, MaxDegreeOfParallelism = Math.Max(1, parallelDegree) }, async (objectName, cancellationToken) =>
			{
				bool more;
				USN_VECTOR usnvecFrom = usnvecFrom_.vec;
				Task? prevOutputTask = null;

				do
				{
					RpcPointer<DRS_MSG_GETCHGREPLY> pmsgOut = new();
					RpcPointer<uint> pdwOutVersion = new();
					DRS_MSG_GETCHGREQ getchgreq;

					//// V11
					//if (0 != (this.serverExt.ext1.MoreFlags & DrsBindMoreFlags.RpcCorrelationId1))
					//{
					//	throw new NotImplementedException();
					//}
					// V10
					if (0 != (this.serverExt.ext1.BindFlags & DrsBindFlags.GetChgReqV10))
					{
						getchgreq = new()
						{
							unionSwitch = 10,
							V10 = new DRS_MSG_GETCHGREQ_V10()
							{
								uuidDsaObjDest = clientGuid,
								uuidInvocIdSrc = invocIdSrc,
								pNC = objectName.ToRpcDsName(),
								usnvecFrom = usnvecFrom,
								//pUpToDateVecDest = new RpcPointer<UPTODATE_VECTOR_V1_EXT>(new UPTODATE_VECTOR_V1_EXT
								//{
								//	cNumCursors = 1,
								//	dwVersion = 1,
								//	rgCursors = new UPTODATE_CURSOR_V1[1]
								//	{
								//		new UPTODATE_CURSOR_V1
								//		{
								//			uuidDsa=invocIdSrc
								//		}
								//	}
								//}),
								ulFlags = (uint)options,
								cMaxObjects = (uint)maxObjCount,
								cMaxBytes = (uint)maxByteCount,
								ulExtendedOp = (uint)exop,
								liFsmoInfo = default,
								pPartialAttrSet = new RpcPointer<PARTIAL_ATTR_VECTOR_V1_EXT>(new PARTIAL_ATTR_VECTOR_V1_EXT()
								{
									dwVersion = 1,
									cAttrs = (uint)attrTags.Length,
									rgPartialAttr = attrTags,
								}),
								PrefixTableDest = new SCHEMA_PREFIX_TABLE
								{
									PrefixCount = (uint)prefixes.Length,
									pPrefixEntry = new RpcPointer<PrefixTableEntry[]>(prefixes)
								},
								// TODO: More options
								ulMoreFlags = 0
							}
						};
					}
					// V8
					else if (0 != (this.serverExt.ext1.BindFlags & DrsBindFlags.GetChgReqV8))
					{
						getchgreq = new()
						{
							unionSwitch = 8,
							V8 = new DRS_MSG_GETCHGREQ_V8()
							{
								uuidDsaObjDest = clientGuid,
								uuidInvocIdSrc = invocIdSrc,
								pNC = objectName.ToRpcDsName(),
								usnvecFrom = usnvecFrom,
								pUpToDateVecDest = null,
								ulFlags = (uint)options,
								cMaxObjects = (uint)maxObjCount,
								cMaxBytes = (uint)maxByteCount,
								ulExtendedOp = (uint)exop,
								liFsmoInfo = default,
								pPartialAttrSet = new RpcPointer<PARTIAL_ATTR_VECTOR_V1_EXT>(new PARTIAL_ATTR_VECTOR_V1_EXT()
								{
									dwVersion = 1,
									cAttrs = (uint)attrTags.Length,
									rgPartialAttr = attrTags,
								}),
								pPartialAttrSetEx = null,
								PrefixTableDest = new SCHEMA_PREFIX_TABLE
								{
									PrefixCount = (uint)prefixes.Length,
									pPrefixEntry = new RpcPointer<PrefixTableEntry[]>(prefixes)
								}
							}
						};
					}
					// V7 - Not used
					// V6 - Not used
					// V5
					else if (0 != (this.serverExt.ext1.BindFlags & DrsBindFlags.GetChgReqV5))
					{
						getchgreq = new()
						{
							unionSwitch = 5,
							V5 = new DRS_MSG_GETCHGREQ_V5()
							{
								uuidDsaObjDest = clientGuid,
								uuidInvocIdSrc = invocIdSrc,
								pNC = objectName.ToRpcDsName(),
								usnvecFrom = usnvecFrom,
								pUpToDateVecDestV1 = null,
								ulFlags = (uint)options,
								cMaxObjects = (uint)maxObjCount,
								cMaxBytes = (uint)maxByteCount,
								ulExtendedOp = (uint)exop,
								liFsmoInfo = default,
							}
						};
					}
					else
					{
						getchgreq = new DRS_MSG_GETCHGREQ()
						{
							unionSwitch = 4,
							V4 = new DRS_MSG_GETCHGREQ_V4()
							{
								uuidTransportObj = default,
								pmtxReturnAddress = new RpcPointer<MTX_ADDR>(new MTX_ADDR { mtx_name = [], mtx_namelen = 0 }),
								V3 = new DRS_MSG_GETCHGREQ_V3
								{
									uuidDsaObjDest = clientGuid,
									uuidInvocIdSrc = invocIdSrc,
									pNC = objectName.ToRpcDsName(),
									usnvecFrom = usnvecFrom,
									pUpToDateVecDestV1 = null,
									ulExtendedOp = (uint)exop,
								}
							}
						};
					}

					var res = (Win32ErrorCode)await owner.proxy.IDL_DRSGetNCChanges(
						hbind,
						getchgreq.unionSwitch,
						getchgreq,
						pdwOutVersion,
						pmsgOut,
						cancellationToken).ConfigureAwait(false);
					try
					{
						res.CheckAndThrow();
					}
					catch (Exception ex)
					{
						await callback.OnError(objectName, ex).ConfigureAwait(false);
						return;
					}

					if (prevOutputTask != null)
						await prevOutputTask.ConfigureAwait(false);

					ReplicateResult repres;
					switch (pdwOutVersion.value)
					{
						case 7:
							repres = HandleV7Reply(callback, sessionKey, pmsgOut.value.V7);
							break;
						case 6:
							repres = HandleV6Reply(callback, sessionKey, pmsgOut.value.V6);
							break;
						default:
							await callback.OnError(objectName, new NotSupportedException($"Server responded with unsupported message version {pdwOutVersion.value}.")).ConfigureAwait(false);
							return;
					}

					prevOutputTask = repres.outputTask;
					more = repres.more;
					usnvecFrom = repres.usnvec;
					usnvecFrom_ = new UsnVector(usnvecFrom);

#if DEBUG
					Console.Out.WriteLine($"*** NEW USN vector: {usnvecFrom_.ToBytes().ToHexString()}");
#endif
				} while (more);

				if (prevOutputTask != null)
					await prevOutputTask.ConfigureAwait(false);
			}).ConfigureAwait(false);

			return usnvecFrom_;
		}

		record struct ReplicateResult(bool more, USN_VECTOR usnvec, Task outputTask);

		private static ReplicateResult HandleV7Reply(IDrsChangeCallback callback, byte[] sessionKey, DRS_MSG_GETCHGREPLY_V7 v7)
		{
			var compData = v7.CompressedAny.pbCompressedData.value;

			byte[] message = new byte[v7.CompressedAny.cbUncompressedSize];
			DecompressMessage((DRS_COMP_ALG_TYPE)v7.CompressionAlg, compData, message, 0);

			var decoder = MsrpcNdrEncoding.MsrpcNdr.CreateDecoder(new IO.ByteMemoryReader(message), new RpcCallContext(null));
			var v6 = decoder.DeserializeType1<DRS_MSG_GETCHGREPLY_V6>(d =>
			{
				var v6 = d.ReadFixedStruct<DRS_MSG_GETCHGREPLY_V6>(NdrAlignment.NativePtr);
				v6.DecodeDeferrals(d);
				return v6;
			});
			return HandleV6Reply(callback, sessionKey, v6);
		}

		private static ReplicateResult HandleV6Reply(IDrsChangeCallback callback, byte[] sessionKey, DRS_MSG_GETCHGREPLY_V6 rep6)
		{
			var prefixTable = DirectoryReplicationClient.DecodePrefixTable(rep6.PrefixTableSrc.pPrefixEntry.value);

			var pObj = rep6.pObjects;
			Task outputTask = Task.Factory.StartNew(async () =>
			{
				while (pObj != null)
				{
					var name = new DsName(pObj.value.Entinf.pName.value);
					var attrs = DirectoryReplicationClient.AttrsFromBlock(in pObj.value.Entinf.AttrBlock, name.Sid?.Rid ?? 0, prefixTable, sessionKey);

					var obj = new DsObject(name, attrs);
					await callback.OnObjectReplicated(obj).ConfigureAwait(false);

					pObj = pObj.value.pNextEntInf;
				}
			}).Unwrap();

#if DEBUG
			// TODO: Add to log
			Console.Out.WriteLine($"*** cNumNcSizeObjects: {rep6.cNumNcSizeObjects}; cNumNcSizeValues: {rep6.cNumNcSizeValues}");
#endif

			return new ReplicateResult(rep6.fMoreData != 0, rep6.usnvecTo, outputTask);
		}

		// [MS-DRSR] § 4.1.10.6.19 DecompressMessage
		public static void DecompressMessage(
			DRS_COMP_ALG_TYPE algorithm,
			ReadOnlySpan<byte> compressed,
			Span<byte> uncompressed,
			int uncompPosition
			)
		{
			if (compressed.Length == uncompressed.Length)
			{
				compressed.CopyTo(uncompressed);
				return;
			}

			int cbInputProcessed = 0;
			int cbDecomp = 0;
			while (cbInputProcessed < compressed.Length)
			{
				var cbChunkDecompSize = BinaryPrimitives.ReadInt32LittleEndian(compressed.Slice(cbInputProcessed, 4));
				cbInputProcessed += 4;
				var cbChunkCompSize = BinaryPrimitives.ReadInt32LittleEndian(compressed.Slice(cbInputProcessed, 4));
				cbInputProcessed += 4;

				var compChunk = compressed.Slice(cbInputProcessed, cbChunkCompSize);

				if (cbChunkDecompSize == cbChunkCompSize)
				{
					compChunk.CopyTo(uncompressed.Slice(cbDecomp, cbChunkDecompSize));
				}
				else
				{
					switch (algorithm)
					{
						case DRS_COMP_ALG_TYPE.DRS_COMP_ALG_WIN2K3:
							Lz77.Decompress(compChunk, uncompressed.Slice(cbDecomp, cbChunkDecompSize), 0);
							break;
						case DRS_COMP_ALG_TYPE.DRS_COMP_ALG_MSZIP:
							{
								int cbDecomped = Mszip.Decompress(compChunk, uncompressed.Slice(0, cbDecomp + cbChunkDecompSize), cbDecomp);
								Debug.Assert(cbDecomped == (cbDecomp + cbChunkDecompSize));
							}
							break;
						case DRS_COMP_ALG_TYPE.DRS_COMP_ALG_NONE:
						case DRS_COMP_ALG_TYPE.DRS_COMP_ALG_UNUSED:
						default:
							throw new NotImplementedException($"Unsupported compression algorithm: {algorithm}");
							break;
					}

					cbInputProcessed += compChunk.Length;
					cbDecomp += cbChunkDecompSize;
				}

				// Round up
				cbInputProcessed = (cbInputProcessed + 3) & ~3;
			}

			Debug.Assert(cbInputProcessed == ((compressed.Length + 3) & ~3));
			Debug.Assert(cbDecomp == uncompressed.Length);
		}
	}
}
