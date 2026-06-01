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

		// [MS-DRSR] 5.138 NTSAPI_CLIENT_GUID
		private static readonly Guid NtdsapiClientGuid = new Guid("e24d201a-4fd6-11d1-a3da-0000f875ae0d");

		internal Task Unbind(RpcContextHandle hbind, CancellationToken cancellationToken)
		{
			return this._proxy.IDL_DRSUnbind(new RpcPointer<RpcContextHandle>(hbind), cancellationToken);
		}

		public async Task<DsBinding> Dsbind(CancellationToken cancellationToken)
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

			return new DsBinding(phDrs.value, this);
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
