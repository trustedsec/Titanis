using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{

	public class KdcEncryptionTypeInfo
	{
		public EType EType { get; }
		internal readonly EncProfile encProfile;
		[Browsable(false)]
		public byte[]? Salt { get; }
		[DisplayName("Salt (text)")]
		public string? SaltText => this.Salt?.ToHexString();

		internal KdcEncryptionTypeInfo(EType etype, EncProfile encProfile, byte[] salt)
		{
			this.EType = etype;
			this.encProfile = encProfile;
			this.Salt = salt;
		}
	}

	class PreauthInfo
	{
		public PreauthInfo(
			KerberosClient client,
			KerberosCredential? credential,
			IKerberosCallback? callback = null
			)
		{
			this._client = client;
			this._credential = credential;
			this._callback = callback;
		}

		private readonly KerberosClient _client;
		private readonly KerberosCredential? _credential;
		private readonly IKerberosCallback? _callback;

		/// <summary>
		/// Processes preauthentication data returned by the AS.
		/// </summary>
		/// <param name="paList">List of <see cref="PA_DATA"/></param>
		internal bool ProcessPadata(IList<PA_DATA>? paList)
		{
			if (paList != null)
			{
				this.etypesFromKdc = new List<KdcEncryptionTypeInfo>();

				bool hasSupportedPreauth = false;
				foreach (var padata in paList)
				{
					bool isSupported = this.ProcessPadata(padata);
					hasSupportedPreauth |= isSupported;
				}
				return hasSupportedPreauth;
			}

			return false;
		}

		public bool ProcessPadata(PA_DATA padata)
		{
			this.paTypes.Add((PadataType)padata.padata_type);

			// TODO: Implement the rest of these types
			switch ((PadataType)padata.padata_type)
			{
				case PadataType.TgsReq:
					break;
				case PadataType.EncTimestamp:
					this.ProcessEncTimestamp(padata.padata_value);
					return true;
				case PadataType.PasswordSalt:
					this.ProcessPasswordSalt(padata.padata_value);
					return true;
				case PadataType.ETypeInfo:
					this.ProcessETypeInfo(padata.padata_value);
					return true;
				case PadataType.PkASreqOld:
					break;
				case PadataType.PkASrepOld:
					break;
				case PadataType.PkASReq:
					break;
				case PadataType.PkASRep:
					break;
				case PadataType.ETypeInfo2:
					this.ProcessETypeInfo2(padata.padata_value);
					return true;
				case PadataType.PacRequest:
					break;
				case PadataType.SvrReferralInfo:
					break;
				case PadataType.FxCookie:
					break;
				case PadataType.FxFast:
					break;
				case PadataType.FxError:
					break;
				case PadataType.EncryptedChallenge:
					break;
				case PadataType.SupportedEncTypes:
					break;
				case PadataType.PacOptions:
					break;
				case PadataType.KerbKeyListReq:
					break;
				case PadataType.KerbKeyListRep:
					break;
				default:
					break;
			}

			return false;
		}

		private List<PadataType> paTypes = new List<PadataType>();
		public bool SupportsPAType(PadataType patype)
		{
			return this.paTypes != null && this.paTypes.Contains(patype);
		}

		private void ProcessPasswordSalt(byte[] padata_value)
		{
			// TODO: Implement
			throw new NotImplementedException();
		}

		internal List<KdcEncryptionTypeInfo>? etypesFromKdc;
		internal bool _requestPac;

		public KdcEncryptionTypeInfo TryGetSupportedEncProfile()
		{
			if (this.etypesFromKdc != null)
			{
				foreach (var etype in this.etypesFromKdc)
				{
					if (etype.encProfile != null)
						return etype;
				}
			}
			return null;
		}

		private void ProcessETypeInfo2(byte[] padata_value)
		{
			var etypes = (this.etypesFromKdc ??= new List<KdcEncryptionTypeInfo>());
			var etypeInfos = Asn1DerDecoder.Decode<Asn1SequenceOf<ETYPE_INFO2_ENTRY>>(padata_value).Values;
			this._callback?.OnProcessETypes(etypeInfos);
			foreach (var elem in etypeInfos)
			{
				etypes.Add(new KdcEncryptionTypeInfo(
					(EType)elem.etype,
					this._client.TryGetEncProfile((EType)elem.etype),
					elem.salt != null ? Encoding.UTF8.GetBytes(elem.salt) : null
				// TODO: Handle s2k parameters
				));
			}
		}

		private void ProcessETypeInfo(byte[] padata_value)
		{
			var etypes = (this.etypesFromKdc ??= new List<KdcEncryptionTypeInfo>());
			var etypeInfos = Asn1DerDecoder.Decode<Asn1SequenceOf<ETYPE_INFO_ENTRY>>(padata_value).Values;
			this._callback?.OnProcessETypes(etypeInfos);
			foreach (var elem in etypeInfos)
			{
				etypes.Add(new KdcEncryptionTypeInfo(
					(EType)elem.etype,
					this._client.TryGetEncProfile((EType)elem.etype),
					elem.salt
				));
			}
		}

		#region EncTimestamp
		private Memory<byte> EncryptTS()
		{
			if (this._credential == null)
				throw new InvalidOperationException("Cannot encrypt timestame because no credential was provided.");

			PA_ENC_TS_ENC tsenc = Structs.PAEnc_TSEnc(this.Skew);

			byte[] tsencBytes = Asn1DerEncoder.EncodeTlv(tsenc).ToArray();
			var encInfo = this.TryGetSupportedEncProfile();
			byte[]? salt = encInfo.Salt;

			var encProfile = encInfo.encProfile;
			var protoKey = this._credential.DeriveProtocolKeyFor(encProfile, salt);
			this._callback?.OnEncryptingTS(protoKey, salt);
			tsencBytes = protoKey.Encrypt(KeyUsage.AsreqPaEncTimestamp, tsencBytes).ToArray();

			var padataBytes = Asn1DerEncoder.EncodeTlv(Structs.EncryptedData(encProfile.EType, tsencBytes));

			return padataBytes;
		}

		internal PA_DATA? _tsenc;

		public TimeSpan Skew { get; internal set; }

		private void ProcessEncTimestamp(byte[] padata_value)
		{
			if (this._credential != null)
			{
				var tsenc = this.EncryptTS();
				this._tsenc = Structs.PAData_TSEnc(tsenc.ToArray());
			}
		}
		#endregion


		internal PA_DATA[] BuildPadataList()
		{
			List<PA_DATA> paList = new List<PA_DATA>(2);
			if (this._tsenc is not null)
				paList.Add(this._tsenc);

			if (this._requestPac)
				paList.Add(Structs.PAData_PacRequest(true));

			return paList.ToArray();
		}
	}
}
