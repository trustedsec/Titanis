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
using Titanis.Ldap;
using Titanis.Winterop;
using Titanis.Winterop.Sam;
using Titanis.Winterop.SamServer;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msdrsr
{
	public enum DsbindScenario
	{
		Unspecified,
		Repnc
	}
	public class DirectoryReplicationClient : RpcServiceClient<ms_drsr.drsuapiClientProxy>
	{
		public DirectoryReplicationClient()
		{
		}

		internal drsuapiClientProxy proxy => this._proxy;

		public override bool RequiresEncryptionOverTcp => true;
		public override string? ServiceClass => "ldap";
		public override bool SupportsDynamicTcp => true;

		private const int DrsExtSize = (12 * 4 + 4);
		private const string SupplementalCredentialsOid = "1.2.840.113556.1.4.125";
		private const string ObjectSidOid = "1.2.840.113556.1.4.146";
		private const string UnicodePwdOid = "1.2.840.113556.1.4.90";
		private const string NtPwdHistoryOid = "1.2.840.113556.1.4.94";
		private const string LmPwdHistoryOid = "1.2.840.113556.1.4.160";
		private const string DbcsPwdOid = "1.2.840.113556.1.4.55";

		// [MS-DRSR] 5.138 NTSAPI_CLIENT_GUID [sic]
		public static readonly Guid NtdsapiClientGuid = new Guid("e24d201a-4fd6-11d1-a3da-0000f875ae0d");



		internal Task Unbind(RpcContextHandle hbind, CancellationToken cancellationToken)
		{
			return this._proxy.IDL_DRSUnbind(new RpcPointer<RpcContextHandle>(hbind), cancellationToken);
		}

		// Windows 2025 dcpromo
		public const DrsBindFlags Windows2025BindFlags = 0 // (DrsBindFlags)0x3ffffb7f;
			| DrsBindFlags.Base
			| DrsBindFlags.AsyncRepl
			| DrsBindFlags.RemoveApi
			| DrsBindFlags.MoveReqV2
			| DrsBindFlags.GetChgDeflate
			| DrsBindFlags.DcinfoV1
			| DrsBindFlags.RestoreUsnOptimization
			| DrsBindFlags.KccExecute
			| DrsBindFlags.AddEntryV2
			| DrsBindFlags.DcinfoV2
			| DrsBindFlags.InstanceTypeNotRequiredOnMod
			| DrsBindFlags.CryptoBind
			| DrsBindFlags.GetReplInfo
			| DrsBindFlags.StrongEncryption
			| DrsBindFlags.DcinfoVF
			| DrsBindFlags.TransitiveMembership
			| DrsBindFlags.AddSidHistory
			| DrsBindFlags.PostBeta3
			| DrsBindFlags.GetChgReqV5
			| DrsBindFlags.GetMemberships2
			| DrsBindFlags.GetChgReqV6
			| DrsBindFlags.NondomainNcs
			| DrsBindFlags.GetChgReqV8
			| DrsBindFlags.GetChgReplyV5
			| DrsBindFlags.GetChgReplyV6
			| DrsBindFlags.WhistlerBeta3
			| DrsBindFlags.W2K3Deflate
			| DrsBindFlags.GetChgReqV10
			;


		public Task<DsBinding> Dsbind(DsbindScenario scenario, CancellationToken cancellationToken)
			=> Dsbind(scenario, NtdsapiClientGuid, Guid.Empty, 1116, cancellationToken, Windows2025BindFlags);
		public async Task<DsBinding> Dsbind(DsbindScenario scenario, Guid clientGuid, Guid siteGuid, int pid, CancellationToken cancellationToken, DrsBindFlags flags = Windows2025BindFlags)
		{
			DceRpc.RpcPointer<DceRpc.RpcContextHandle> phDrs = new();

			// [MS-DRSR] § 5.39 DRS_EXTENSIONS_INT
			ByteWriter writer = new ByteWriter();
			writer.WritePduStruct(new DRS_EXTENSIONS_INT2
			{
				ext1 = new DRS_EXTENSIONS_INT
				{
					BindFlags = (scenario == DsbindScenario.Repnc) ? flags : 0,
					SiteObjGuid = siteGuid,
					Pid = pid,
					ReplEpoch = 0,
					MoreFlags = (scenario == DsbindScenario.Repnc) ? (DrsBindMoreFlags)0x0000080e : 0,
					ConfigObjGuid = default,
				},
				ExtCaps = (scenario == DsbindScenario.Repnc) ? 0x00001fff : 0
			});

			DceRpc.RpcPointer<ms_drsr.DRS_EXTENSIONS> pextClient = new(new ms_drsr.DRS_EXTENSIONS
			{
				cb = (uint)writer.Length,
				rgb = writer.GetData().ToArray()
			});
			DceRpc.RpcPointer<DceRpc.RpcPointer<ms_drsr.DRS_EXTENSIONS>> ppextServer = new();
			var res = (Win32ErrorCode)await this._proxy.IDL_DRSBind(
				new DceRpc.RpcPointer<Guid>(clientGuid),
				pextClient,
				ppextServer,
				phDrs,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			//if ((ppextServer.value?.value.cb ?? 0) >= DRS_EXTENSIONS_INT.PduStructSize)
			var reader = new ByteMemoryReader(ppextServer.value.value.rgb);
			DRS_EXTENSIONS_INT2 serverExt;
			if (reader.Length >= DRS_EXTENSIONS_INT2.PduStructSize)
			{
				serverExt = reader.ReadPduStruct<DRS_EXTENSIONS_INT2>();
			}
			else
			{
				serverExt = new DRS_EXTENSIONS_INT2
				{
					ext1 = reader.ReadPduStruct<DRS_EXTENSIONS_INT>(),
					ExtCaps = 0
				};
			}

			return new DsBinding(phDrs.value, this, serverExt);
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

		internal static string[] DecodePrefixTable(PrefixTableEntry[] prefixTableSrc)
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

		internal static DsAttribute[] AttrsFromBlock(in ATTRBLOCK attrBlock, uint userRid, string[] prefixTable, byte[] sessionKey)
		{
			var attrSrcs = attrBlock.pAttr.value;
			string[] oids = new string[attrSrcs.Length];
			var attrTypes = new AttributeTypeDescription?[attrSrcs.Length];
			for (int iAttr = 0; iAttr < attrSrcs.Length; iAttr++)
			{
				ATTR attrSrc = attrSrcs[iAttr];

				var prefixIndex = attrSrc.attrTyp >> 16;
				var rid = attrSrc.attrTyp & ushort.MaxValue;

				var oid = prefixTable[prefixIndex] + '.' + rid;
				oids[iAttr] = oid;
				attrTypes[iAttr] = LdapAttributeTypes.TryGetByNameOrOid(oid);
			}

			var attrs = new List<DsAttribute>(attrSrcs.Length);
			for (int iAttr = 0; iAttr < attrSrcs.Length; iAttr++)
			{
				var oid = oids[iAttr];
				ATTR attrSrc = attrSrcs[iAttr];

				ATTRVAL[]? pAttrVal = attrSrc.AttrVal.pAVal?.value;
				var values = (pAttrVal == null) ? null : Array.ConvertAll(pAttrVal, r => new DsAttributeValue(DecryptIfNeeded(oid, r.pVal.value, sessionKey, userRid)));

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
		private static byte[] DecryptIfNeeded(string attrOid, byte[] value, ReadOnlySpan<byte> sessionKey, uint userRid)
		{
			if (value != null && IsSecretAttribute(attrOid))
			{
				Span<byte> key = stackalloc byte[Md5Context.StaticDigestSizeBytes];
				ComputeEncryptionKey(sessionKey, value.Slice(0, 16), key);
				Rc4Context rc4 = new Rc4Context(key);
				rc4.Transform(value.Slice(16), value.Slice(16));

				// TODO: Verify CRC
				var decrypted = value.Slice(16 + 4).ToArray();

				if (attrOid is UnicodePwdOid or NtPwdHistoryOid or DbcsPwdOid or LmPwdHistoryOid)
				{
					var blocks = decrypted.Length / 16;
					for (int n = 0; n < blocks; n++)
					{
						SamStore.DecryptUserData(userRid, decrypted.Slice(n * 16, 16));
					}
				}

				return decrypted;
			}
			else
				return value;
		}

		// [MS-DRSR] § 4.1.10.3.11 - IsSecretAttribute
		private static readonly string[] SecretAttributeList = [
			"CURRENTVALUE", "1.2.840.113556.1.4.27",
			"DBCSPWD", DbcsPwdOid,
			"INITIALAUTHINCOMING", "1.2.840.113556.1.4.539",
			"INITIALAUTHOUTGOING", "1.2.840.113556.1.4.540",
			"LMPWDHISTORY", LmPwdHistoryOid,
			"NTPWDHISTORY", NtPwdHistoryOid,
			"PRIORVALUE", "1.2.840.113556.1.4.100",
			"SUPPLEMENTALCREDENTIALS", SupplementalCredentialsOid,
			"TRUSTAUTHINCOMING", "1.2.840.113556.1.4.129",
			"TRUSTAUTHOUTGOING", "1.2.840.113556.1.4.135",
			"UNICODEPWD", UnicodePwdOid,
			];
		// [MS-DRSR] § 4.1.10.3.11 - IsSecretAttribute
		public static bool IsSecretAttribute(string attributeNameOrOid)
			=> Array.IndexOf(SecretAttributeList, (attributeNameOrOid ?? throw new ArgumentNullException(nameof(attributeNameOrOid))).ToUpper()) >= 0;
	}
}
