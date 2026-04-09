using ms_drsr;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Crypto;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.IO;
using Titanis.Winterop;
using Titanis.Winterop.SamServer;

namespace Titanis.Msrpc.Msdrsr
{
	// [MS-DRSR] § 5.39 DRS_EXTENSIONS_INT
	[Flags]
	enum DrsBindFlags : uint
	{
		None = 0,

		Base = 1,
		AsyncRepl = 2,
		RemoveApi = 4,
		MoveReqV2 = 8,
		GetChgDeflate = 0x10,
		DcinfoV1 = 0x20,
		RestoreUsnOptimization = 0x40,
		AddEntry = 0x80,
		KccExecute = 0x100,
		AddEntryV2 = 0x200,
		LinkedValueReplication = 0x400,
		DcinfoV2 = 0x800,
		InstanceTypeNotRequiredOnMod = 0x1000,
		CryptoBind = 0x2000,
		GetReplInfo = 0x4000,
		StrongEncryption = 0x8000,
		DcinfoVF = 0x1_0000,
		TransitiveMembership = 0x2_0000,
		AddSidHistory = 0x4_0000,
		PostBeta3 = 0x8_0000,
		GetChgReqV5 = 0x10_0000,
		GetMemberships2 = 0x20_0000,
		GetChgReqV6 = 0x40_0000,
		NondomainNcs = 0x80_0000,
		GetChgReqV8 = 0x100_0000,
		GetChgReplyV5 = 0x200_0000,
		GetChgReplyV6 = 0x400_0000,
		WhistlerBeta3 = 0x800_0000,
		W2K3Deflate = 0x1000_0000,
		GetChgReqV10 = 0x2000_0000,
		Res1 = 0x4000_0000,
		Res2 = 0x8000_0000,
	}

	// [MS-DRSR] § 5.39 DRS_EXTENSIONS_INT
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct DRS_EXTENSIONS_INT
	{
		public DrsBindFlags BindFlags { get; set; }
		public Guid SiteObjGuid { get; set; }
		public int Pid { get; set; }
		public int ReplEpoch { get; set; }
		public uint MoreFlags { get; set; }
		public Guid ConfigObjGuid { get; set; }
		public int ExtCaps { get; set; }
	}

	public class DirectoryReplicationClient : RpcServiceClient<ms_drsr.drsuapiClientProxy>
	{
		public DirectoryReplicationClient()
		{
		}

		public override bool RequiresEncryptionOverTcp => true;
		public override string? ServiceClass => "ldap";
		public override bool SupportsDynamicTcp => true;

		private const int DrsExtSize = (12 * 4 + 4);
		private const string SupplementalCredentialsOid = "1.2.840.113556.1.4.125";

		// [MS-DRSR] 5.138 NTSAPI_CLIENT_GUID
		private static readonly Guid NtdsapiClientGuid = new Guid("e24d201a-4fd6-11d1-a3da-0000f875ae0d");

		private async Task<TReturn> WithBind<TArg, TReturn>(Func<RpcContextHandle, TArg, CancellationToken, Task<TReturn>> func, TArg arg, CancellationToken cancellationToken)
		{
			DceRpc.RpcPointer<DceRpc.RpcContextHandle> phDrs = new();
			Guid dsaGuid = NtdsapiClientGuid;
			Guid siteGuid = new Guid();

			DrsBindFlags flags = 0
				// Required
				| DrsBindFlags.Base
				| DrsBindFlags.RestoreUsnOptimization
				| DrsBindFlags.InstanceTypeNotRequiredOnMod
				| DrsBindFlags.PostBeta3
				| DrsBindFlags.GetChgReplyV5

				| DrsBindFlags.CryptoBind

				| DrsBindFlags.GetChgReqV6
				| DrsBindFlags.GetChgReplyV6
				| DrsBindFlags.GetChgReqV8
				| DrsBindFlags.StrongEncryption
				| DrsBindFlags.NondomainNcs;
			flags = (DrsBindFlags)0x05C08000;

			// [MS-DRSR] § 5.39 DRS_EXTENSIONS_INT
			ByteWriter writer = new ByteWriter();
			writer.WritePduStruct(new DRS_EXTENSIONS_INT
			{
				BindFlags = flags,
				SiteObjGuid = siteGuid,
				ExtCaps = 0x7
			});

			DceRpc.RpcPointer<ms_drsr.DRS_EXTENSIONS> pextClient = new(new ms_drsr.DRS_EXTENSIONS
			{
				cb = (uint)writer.Length,
				rgb = writer.GetData().ToArray()
			});
			pextClient = new DceRpc.RpcPointer<ms_drsr.DRS_EXTENSIONS>(new ms_drsr.DRS_EXTENSIONS()
			{
				cb = (uint)writer.Length,
				rgb = writer.GetData().ToArray()
			});
			dsaGuid = NtdsapiClientGuid;
			DceRpc.RpcPointer<DceRpc.RpcPointer<ms_drsr.DRS_EXTENSIONS>> ppextServer = new();
			var res = (Win32ErrorCode)await this._proxy.IDL_DRSBind(
				new DceRpc.RpcPointer<Guid>(dsaGuid),
				pextClient,
				ppextServer,
				phDrs,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			//if ((ppextServer.value?.value.cb ?? 0) >= DRS_EXTENSIONS_INT.PduStructSize)
			var serverExt = new ByteMemoryReader(ppextServer.value.value.rgb).ReadPduStruct<DRS_EXTENSIONS_INT>();

			try
			{
				return await func(phDrs.value, arg, cancellationToken).ConfigureAwait(false);
			}
			finally
			{
				await _proxy.IDL_DRSUnbind(phDrs, cancellationToken).ConfigureAwait(false);
			}

		}
		public Task<DomainControllerInfo[]> GetDcInfo(string domainName, CancellationToken cancellationToken)
		{
			return this.WithBind(GetDcInfoWithBind, domainName, cancellationToken);
		}

		private async Task<DomainControllerInfo[]> GetDcInfoWithBind(RpcContextHandle hbind, string domainName, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pdwOutVersion = new();
			RpcPointer<ms_drsr.DRS_MSG_DCINFOREPLY> pmsgOut = new();
			var res = (Win32ErrorCode)await _proxy.IDL_DRSDomainControllerInfo(
				hbind,
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

		record class ReplicateReq(
			DomainControllerInfo dcInfo,
			DsName objectName,
			int count,
			PrefixTableEntry[] prefixes,
			uint[] attrTags
			)
		{
		}

		public Task<DsObject[]> GetNcChanges(
			DomainControllerInfo dcInfo,
			DsName objectName,
			string[] attributeOids,
			int count,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(attributeOids);

			Dictionary<string, int> prefixLookup = new Dictionary<string, int>();
			List<PrefixTableEntry> prefixes = new(attributeOids.Length);
			List<uint> attrTags = new List<uint>(attributeOids.Length);
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
							prefixIndex = prefixes.Count;
							prefixes.Add(new PrefixTableEntry { ndx = (uint)prefixIndex, prefix = new OID_t { length = (uint)bytes.Length, elements = new RpcPointer<byte[]>(bytes) } });
							prefixLookup.Add(prefix, prefixIndex);

							isValid = true;
						}
						else
							isValid = true;

						var tag = (uint)(prefixIndex << 16) | last;
						attrTags.Add(tag);
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

			ArgumentNullException.ThrowIfNull(dcInfo);
			ArgumentNullException.ThrowIfNull(objectName);
			return this.WithBind(GetNcChanges, new ReplicateReq(
				dcInfo,
				objectName,
				count,
				prefixes.ToArray(),
				attrTags.ToArray()
			), cancellationToken);
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

		private async Task<DsObject[]> GetNcChanges(RpcContextHandle handle, ReplicateReq arg, CancellationToken cancellationToken)
		{
			var dcInfo = arg.dcInfo;

			var sessionKey = this._proxy.BoundAuthContext.AuthContext.GetSessionKey().ToArray();

			RpcPointer<DRS_MSG_GETCHGREPLY> pmsgOut = new();
			RpcPointer<uint> pdwOutVersion = new();
			var res = (Win32ErrorCode)await this._proxy.IDL_DRSGetNCChanges(
				handle,
				8,
				new ms_drsr.DRS_MSG_GETCHGREQ()
				{
					unionSwitch = 8,
					V8 = new ms_drsr.DRS_MSG_GETCHGREQ_V8()
					{
						uuidDsaObjDest = dcInfo.NtdsDsaObjectGuid,
						uuidInvocIdSrc = dcInfo.NtdsDsaObjectGuid,
						pNC = arg.objectName.ToRpcDsName(),
						usnvecFrom = default,
						pUpToDateVecDest = null,
						ulFlags = (uint)(DrsOptions.InitSync | DrsOptions.WriteRep),
						cMaxObjects = (uint)arg.count,
						cMaxBytes = 0,
						ulExtendedOp = (uint)ExtendedOpRequest.ReplObject,
						pPartialAttrSet = new RpcPointer<ms_drsr.PARTIAL_ATTR_VECTOR_V1_EXT>(new ms_drsr.PARTIAL_ATTR_VECTOR_V1_EXT()
						{
							dwVersion = 1,
							cAttrs = (uint)arg.attrTags.Length,
							rgPartialAttr = arg.attrTags,
						}),
						PrefixTableDest = new ms_drsr.SCHEMA_PREFIX_TABLE
						{
							PrefixCount = (uint)arg.prefixes.Length,
							pPrefixEntry = new RpcPointer<ms_drsr.PrefixTableEntry[]>(arg.prefixes)
						}
					}
				},
				pdwOutVersion,
				pmsgOut,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			DsObject obj;
			switch (pdwOutVersion.value)
			{
				case 6:
					{
						var rep6 = pmsgOut.value.V6;
						var prefixTable = DecodePrefixTable(rep6.PrefixTableSrc.pPrefixEntry.value);

						var pObj = rep6.pObjects;
						List<DsObject> objs = new List<DsObject>((int)pmsgOut.value.V6.cNumObjects);
						while (pObj != null)
						{
							var name = new DsName(pObj.value.Entinf.pName.value);
							var attrs = AttrsFromBlock(in pObj.value.Entinf.AttrBlock, prefixTable, sessionKey);

							obj = new DsObject(name, attrs);
							objs.Add(obj);

							pObj = pObj.value.pNextEntInf;
						}
						return objs.ToArray();
					}
				default:
					throw new NotSupportedException($"Server responded with unsupported message version {pdwOutVersion.value}.");
			}
		}

		private static string[] DecodePrefixTable(PrefixTableEntry[] prefixTableSrc)
		{
			string[] prefixes = new string[prefixTableSrc.Length];
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < prefixTableSrc.Length; i++)
			{
				PrefixTableEntry prefix = prefixTableSrc[i];

				var bytes = prefix.prefix.elements.value;
				var o0 = bytes[0];
				// ASN.1 DER
				sb.Append((o0 / 40))
					.Append('.')
					.Append(o0 % 40);

				int value = 0;
				for (int j = 1; j < bytes.Length; j++)
				{
					byte o = bytes[j];

					value <<= 7;
					value |= (o & 0x7F);
					if (o < 0x80)
					{
						sb.Append('.').Append(value);
						value = 0;
					}
				}

				prefixes[i] = sb.ToString();
				sb.Clear();
			}

			return prefixes;
		}

		private static DsAttribute[] AttrsFromBlock(in ATTRBLOCK attrBlock, string[] prefixTable, byte[] sessionKey)
		{
			var attrSrcs = attrBlock.pAttr.value;
			var attrs = new List<DsAttribute>(attrSrcs.Length);
			for (int iAttr = 0; iAttr < attrSrcs.Length; iAttr++)
			{
				ATTR attrSrc = attrSrcs[iAttr];

				var prefixIndex = attrSrc.attrTyp >> 16;
				var rid = attrSrc.attrTyp & ushort.MaxValue;

				var oid = prefixTable[prefixIndex] + '.' + rid;

				ATTRVAL[]? pAttrVal = attrSrc.AttrVal.pAVal?.value;
				var values = (pAttrVal == null) ? null : Array.ConvertAll(pAttrVal, r => new DsAttributeValue(DecryptIfNeeded(oid, r.pVal.value, sessionKey)));

				DsAttribute dsattr = new DsAttribute(oid, values ?? []);
				attrs.Add(dsattr);
			}

			return attrs.ToArray();
		}

		// [MS-DRSR] § 4.1.10.5.11 - EncryptValuesIfNecessary
		private static void ComputeEncryptionKey(ReadOnlySpan<byte> sessionKey, ReadOnlySpan<byte> salt, Span<byte> keyBuffer)
		{
			Debug.Assert(salt.Length == 16);
			Debug.Assert(keyBuffer.Length == Md5Context.StaticDigestSizeBytes);

			Md5Context ctx = new Md5Context();
			ctx.Initialize();
			ctx.HashData(sessionKey);
			ctx.HashData(salt);
			ctx.HashFinal(keyBuffer);
		}

		// [MS-DRSR] § 4.1.10.5.11 - EncryptValuesIfNecessary
		private static byte[] DecryptIfNeeded(string attrOid, byte[] value, ReadOnlySpan<byte> sessionKey)
		{
			if (value != null && IsSecretAttribute(attrOid))
			{
				Span<byte> key = stackalloc byte[Md5Context.StaticDigestSizeBytes];
				ComputeEncryptionKey(sessionKey, value.Slice(0, 16), key);
				Rc4Context rc4 = new Rc4Context(key);
				rc4.Transform(value.Slice(16), value.Slice(16));

				// TODO: Verify CRC
				return value.Slice(16 + 4).ToArray();
			}
			else
				return value;
		}

		// [MS-DRSR] § 4.1.10.3.11 - IsSecretAttribute
		private static readonly string[] SecretAttributeList = [
			"CURRENTVALUE", "1.2.840.113556.1.4.27",
			"DBCSPWD", "1.2.840.113556.1.4.55",
			"INITIALAUTHINCOMING", "1.2.840.113556.1.4.539",
			"INITIALAUTHOUTGOING", "1.2.840.113556.1.4.540",
			"LMPWDHISTORY", "1.2.840.113556.1.4.160",
			"NTPWDHISTORY", "1.2.840.113556.1.4.94",
			"PRIORVALUE", "1.2.840.113556.1.4.100",
			"SUPPLEMENTALCREDENTIALS", SupplementalCredentialsOid,
			"TRUSTAUTHINCOMING", "1.2.840.113556.1.4.129",
			"TRUSTAUTHOUTGOING", "1.2.840.113556.1.4.135",
			"UNICODEPWD", "1.2.840.113556.1.4.90",
			];
		// [MS-DRSR] § 4.1.10.3.11 - IsSecretAttribute
		public static bool IsSecretAttribute(string attributeNameOrOid)
			=> Array.IndexOf(SecretAttributeList, (attributeNameOrOid ?? throw new ArgumentNullException(nameof(attributeNameOrOid))).ToUpper()) >= 0;
	}
}
