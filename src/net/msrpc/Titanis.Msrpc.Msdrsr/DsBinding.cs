using ms_drsr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.DceRpc;
using Titanis.Winterop;

namespace Titanis.Msrpc.Msdrsr
{
	public interface IDrsChangeCallback
	{
		Task OnObjectReplicated(DsObject obj);
		Task OnError(DsName objectName, Exception exception);
	}

	public class DsBinding : IDisposable, IAsyncDisposable
	{
		internal DsBinding(RpcContextHandle hbind, DirectoryReplicationClient owner)
		{
			this.hbind = hbind;
			this.owner = owner;
		}

		private readonly RpcContextHandle hbind;
		private readonly DirectoryReplicationClient owner;

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
				new RpcPointer<ms_drsr.DRS_MSG_DCINFOREQ>(new ms_drsr.DRS_MSG_DCINFOREQ
				{
					unionSwitch = 1,
					V1 = new ms_drsr.DRS_MSG_DCINFOREQ_V1
					{
						Domain = new RpcPointer<string>(domainName),
						InfoLevel = 2,
					},
				}),
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

		public async Task GetNcChanges(
			DomainControllerInfo dcInfo,
			IAsyncEnumerable<DsName> objectNames,
			string[] attributeOids,
			int count,
			IDrsChangeCallback callback,
			int parallelDegree,
			CancellationToken cancellationToken)
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
					if (attrOid is null)
						continue;

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
							attrTagsList.Add(tag);
						}
						catch
						{
							isValid = false;
						}
					}
					else
						isValid = true;

					if (!isValid)
						throw new ArgumentException($"Attribute OID '{attrOid}' is invalid.", nameof(attributeOids));
				}

				attrTags = attrTagsList.ToArray();
				prefixes = prefixList.ToArray();
			}

			ArgumentNullException.ThrowIfNull(dcInfo);
			ArgumentNullException.ThrowIfNull(objectNames);

			//return this.WithBind(GetNcChanges, new ReplicateReq(
			//	dcInfo,
			//	objectName,
			//	count,
			//	prefixes.ToArray(),
			//	attrTags.ToArray()
			//), cancellationToken);

			var sessionKey = this.owner.proxy.BoundAuthContext.AuthContext.GetSessionKey().ToArray();

			//await foreach(var objectName in objectNames.WithCancellation(cancellationToken))
			await Parallel.ForEachAsync(objectNames, new ParallelOptions() { CancellationToken = cancellationToken, MaxDegreeOfParallelism = Math.Max(1, parallelDegree) }, async (objectName, cancellationToken) =>
			{
				RpcPointer<DRS_MSG_GETCHGREPLY> pmsgOut = new();
				RpcPointer<uint> pdwOutVersion = new();
				var res = (Win32ErrorCode)await owner.proxy.IDL_DRSGetNCChanges(
					hbind,
					8,
					new DRS_MSG_GETCHGREQ()
					{
						unionSwitch = 8,
						V8 = new DRS_MSG_GETCHGREQ_V8()
						{
							uuidDsaObjDest = dcInfo.NtdsDsaObjectGuid,
							uuidInvocIdSrc = dcInfo.NtdsDsaObjectGuid,
							pNC = objectName.ToRpcDsName(),
							usnvecFrom = default,
							pUpToDateVecDest = null,
							ulFlags = (uint)(DrsOptions.InitSync | DrsOptions.WriteRep),
							cMaxObjects = (uint)count,
							cMaxBytes = 0,
							ulExtendedOp = (uint)ExtendedOpRequest.ReplObject,
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
							}
						}
					},
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

				DsObject obj;
				switch (pdwOutVersion.value)
				{
					case 6:
						{
							var rep6 = pmsgOut.value.V6;
							var prefixTable = DirectoryReplicationClient.DecodePrefixTable(rep6.PrefixTableSrc.pPrefixEntry.value);

							var pObj = rep6.pObjects;
							while (pObj != null)
							{
								var name = new DsName(pObj.value.Entinf.pName.value);
								var attrs = DirectoryReplicationClient.AttrsFromBlock(in pObj.value.Entinf.AttrBlock, prefixTable, sessionKey);

								obj = new DsObject(name, attrs);

								await callback.OnObjectReplicated(obj).ConfigureAwait(false);

								pObj = pObj.value.pNextEntInf;
							}
						}
						break;
					default:
						await callback.OnError(objectName, new NotSupportedException($"Server responded with unsupported message version {pdwOutVersion.value}.")).ConfigureAwait(false);
						break;
				}
			}).ConfigureAwait(false);
		}

		// [MS-DRSR] § 5.41 DRS_OPTIONS
		[Flags]
		enum DrsOptions : uint
		{
			None,
			Async = 1,
			GetChgCheck = 2,
			UpdateNotification = 2,
			AddRef = 4,
			SyncAll = 8,
			DelRef = 8,
			WriteRep = 0x10,
			InitSync = 0x20,
			PeriodicSync = 0x40,
			MailRep = 0x80,
			AsyncRep = 0x100,
			IgnoreErrors = 0x100,
			TwoWaySync = 0x200,
			CriticalOnly = 0x400,
			GetAncestors = 0x800,
			GetNcSize = 0x1000,
			LocalOnly = 0x1000,
			NongcReadOnlyReplica = 0x2000,
			SyncByName = 0x4000,
			RefOk = 0x4000,
			FullSyncNow = 0x8000,
			NoSource = 0x8000,
			FullSyncInProgress = 0x1_0000,
			FullSyncPacket = 0x2_0000,
			SyncRequeue = 0x4_0000,
			Urgent = 0x8_0000,
			GcSpn = 0x10_0000,
			NoDiscard = 0x10_0000,
			NeverSynced = 0x20_0000,
			SpecialSecretProcessing = 0x40_0000,
			InitSyncNow = 0x80_0000,
			Preempted = 0x100_0000,
			SyncForced = 0x200_0000,
			DisableAutoSync = 0x400_0000,
			DisablePeriodicSync = 0x800_0000,
			UseCompression = 0x1000_0000,
			NeverNotify = 0x2000_0000,
			SyncPartial = 0x4000_0000,
			GetAllGroupMembership = 0x8000_0000,
		}

		// [MS-DRSR] § 4.1.10.2.22 EXOP_REQ Codes
		enum ExtendedOpRequest
		{
			FsmoReqRole = 1,
			FsmoReqRidAlloc = 2,
			FsmoRidReqRole = 3,
			FsmoReqPdf = 4,
			FsmoAbandonRole = 5,
			ReplObject = 6,
			ReplSecrets = 7,
		}
	}
}
