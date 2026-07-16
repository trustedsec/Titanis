using KerberosPreauthFramework;
using KerberosV5Spec2;
using PKIX1Explicit88;
using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Ldap;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{

	public class KdcEncryptionTypeInfo
	{
		public EType EType { get; }
		internal readonly EncProfile? encProfile;
		[Browsable(false)]
		public byte[]? Salt { get; }
		[DisplayName("Salt (text)")]
		public string? SaltText => (this.Salt is not null) ? Encoding.UTF8.GetString(this.Salt) : null;
		[DisplayName("Salt (hex)")]
		public string? SaltHex => this.Salt?.ToHexString();

		internal KdcEncryptionTypeInfo(EType etype, EncProfile? encProfile, byte[]? salt)
		{
			this.EType = etype;
			this.encProfile = encProfile;
			this.Salt = salt;
		}
	}

	abstract class PreauthContext
	{
		public PreauthContext(
			KerberosClient client,
			IKerberosCallback? callback = null
			)
		{
			this.Client = client;
			this.Callback = callback;
		}

		protected KerberosClient Client { get; }
		protected abstract KerberosCredential Credential { get; }
		protected IKerberosCallback? Callback { get; }

		public virtual SessionKey DeriveProtocolKey(EncProfile encProfile)
		{
			byte[]? salt = null;
			var encType = this.TryGetSupportedEncProfile();

			if (encType != null)
			{
				salt = encType.Salt;
			}
			else if (this.passwordSalt != null)
				salt = this.passwordSalt;

			var protoKey = this.Credential.DeriveProtocolKeyFor(encProfile, salt);
			return protoKey;
		}

		/// <summary>
		/// Gets the time skew sent by the AS.
		/// </summary>
		public TimeSpan Skew { get; internal set; }

		/// <summary>
		/// Processes preauthentication data returned by the AS.
		/// </summary>
		/// <param name="paList">List of <see cref="PA_DATA"/></param>
		/// <remarks>
		/// This is called by <see cref="KerberosClient"/> when it receives an <c>AS-REP</c> PDU.
		/// </remarks>
		internal bool TryProcessPadata(Guid correlationId, IList<PA_DATA>? paList)
		{
			if (paList != null)
			{
				this.etypesFromKdc = new List<KdcEncryptionTypeInfo>();

				bool hasSupportedPreauth = false;
				foreach (var padata in paList)
				{
					bool isSupported = this.ProcessPadata(correlationId, padata);
					hasSupportedPreauth |= isSupported;
				}

				return hasSupportedPreauth;
			}

			return false;
		}

		public SupportedEncryptionTypes? SupportedEncryptionTypes { get; set; }
		public PaSvrReferralInfo Referral { get; private set; }
		public PacOptions PacOptions { get; private set; }

		/// <summary>
		/// Processes a <see cref="PA_DATA"/> from the AS.
		/// </summary>
		/// <param name="padata"><see cref="PA_DATA"/> from the AS</param>
		/// <returns><see langword="true"/> if <see cref="PA_DATA"/> is supported and can be used to produce a <see cref="PA_DATA"/> authenticating the user.</returns>
		/// <remarks>
		/// The return value is used by the caller to determine whether any of the <see cref="PA_DATA"/> sent by the server are supported.
		/// </remarks>
		protected virtual bool ProcessPadata(Guid correlationId, PA_DATA padata)
		{
			PadataType patype = (PadataType)padata.padata_type;
			this.paTypes.Add(patype);

			// TODO: Implement the rest of these types
			switch (patype)
			{
				case PadataType.PasswordSalt:
					this.ProcessPasswordSalt(padata.padata_value);
					return true;
				case PadataType.ETypeInfo:
					this.ProcessETypeInfo(correlationId, padata.padata_value);
					return true;
				case PadataType.ETypeInfo2:
					this.ProcessETypeInfo2(correlationId, padata.padata_value);
					return true;

				case PadataType.SupportedEncTypes:
					this.ProcessSupportedEncTypes(padata.padata_value);
					break;

				case PadataType.SvrReferralInfo:
					this.ProcessReferral(padata.padata_value);
					break;

				case PadataType.PacOptions:
					this.ProcessPacOptions(padata.padata_value);
					break;

				case PadataType.TdCmsDigestAlgorithms:
					this.ProcessCmsAlgorithmList(padata.padata_value);
					break;

				case PadataType.FxFast:
					return this.ProcessFast(padata.padata_value);

				case PadataType.FxCookie:
					this.ProcessFastCookie(padata.padata_value);
					break;

				case PadataType.EncryptedChallenge:
					this.ProcessEncryptedChallenge(padata.padata_value);
					return true;

				case PadataType.TgsReq:
				case PadataType.PacRequest:
				case PadataType.FxError:
				case PadataType.KerbKeyListReq:
				case PadataType.KerbKeyListRep:
				default:
					break;
			}

			return false;
		}

		private bool _supportsEncryptedChallenge;
		private byte[]? _encryptedChallenge;
		private void ProcessEncryptedChallenge(byte[]? padata_value)
		{
			this._supportsEncryptedChallenge = true;
			this._encryptedChallenge = padata_value;
		}

		public bool SupportsFast { get; set; }
		public SessionKey? ArmorKey { get; set; }
		public SessionKey? ArmorStrengthenKey { get; set; }

		private bool ProcessFast(byte[] padata_value)
		{
			this.SupportsFast = true;
			bool authSupported = false;
			if (!padata_value.IsNullOrEmpty())
			{
				var fastReply = Asn1DerDecoder.DecodeTlv<PA_FX_FAST_REPLY>(padata_value);
				if (this.ArmorKey != null)
				{
					ReadOnlyMemory<byte> decData = this.ArmorKey.Decrypt(KeyUsage.FastRep, fastReply.Armored_data.enc_fast_rep);
					var rep = Asn1DerDecoder.DecodeTlv<KrbFastResponse>(decData);
					if (rep.strengthen_key != null)
					{
						this.ArmorStrengthenKey = this.Client.CreateSessionKeyFor(rep.strengthen_key);
					}
					if (rep.padata != null)
					{
						foreach (var padata in rep.padata)
						{
							if (this.ProcessPadata(Guid.Empty, padata))
								authSupported = true;
						}
					}
				}
			}

			return authSupported;
		}

		private byte[]? _fastCookie;
		private void ProcessFastCookie(byte[] padata_value)
		{
			this._fastCookie = padata_value;
		}

		public AlgorithmIdentifier[]? SupportCmsAlgorithms { get; private set; }
		private void ProcessCmsAlgorithmList(byte[] padata_value)
		{
			var algs = Asn1DerDecoder.DecodeTlv<Asn1SequenceOf<AlgorithmIdentifier>>(padata_value);
			this.SupportCmsAlgorithms = algs.Values;
		}

		private void ProcessPacOptions(byte[] padata_value)
		{
			this.PacOptions = (PacOptions)Asn1DerDecoder.DecodeTlv<PA_PAC_OPTIONS>(padata_value).flags.ToUInt32();
		}

		private void ProcessReferral(byte[] padata_value)
		{
			var referral = Asn1DerDecoder.DecodeTlv<PaSvrReferralInfo>(padata_value);
			this.Referral = referral;
		}

		internal static SupportedEncryptionTypes? ExtractSupportedEncTypes(ReadOnlySpan<byte> data)
		{
			if (data.Length == 4)
			{
				var etypes = (SupportedEncryptionTypes)BinaryPrimitives.ReadInt32LittleEndian(data);
				return etypes;
			}
			return default;

		}
		private void ProcessSupportedEncTypes(byte[] padata_value)
		{
			this.SupportedEncryptionTypes = ExtractSupportedEncTypes(padata_value);
		}

		private List<PadataType> paTypes = new List<PadataType>();
		public bool SupportsPAType(PadataType patype)
		{
			return this.paTypes != null && this.paTypes.Contains(patype);
		}

		internal byte[]? passwordSalt;
		private void ProcessPasswordSalt(byte[] padata_value)
		{
			this.passwordSalt = padata_value;
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

		private void ProcessETypeInfo2(Guid correlationId, byte[] padata_value)
		{
			var etypes = (this.etypesFromKdc ??= new List<KdcEncryptionTypeInfo>());
			var etypeInfos = Asn1DerDecoder.DecodeTlv<Asn1SequenceOf<ETYPE_INFO2_ENTRY>>(padata_value).Values;
			this.Callback?.OnProcessETypes(correlationId, etypeInfos);
			foreach (var elem in etypeInfos)
			{
				etypes.Add(new KdcEncryptionTypeInfo(
					(EType)elem.etype,
					this.Client.TryGetEncProfile((EType)elem.etype),
					elem.salt.HasValue ? Encoding.UTF8.GetBytes(elem.salt) : null
				// TODO: Handle s2k parameters
				));
			}
		}

		private void ProcessETypeInfo(Guid correlationId, byte[] padata_value)
		{
			var etypes = (this.etypesFromKdc ??= new List<KdcEncryptionTypeInfo>());
			var etypeInfos = Asn1DerDecoder.DecodeTlv<Asn1SequenceOf<ETYPE_INFO_ENTRY>>(padata_value).Values;
			this.Callback?.OnProcessETypes(correlationId, etypeInfos);
			foreach (var elem in etypeInfos)
			{
				etypes.Add(new KdcEncryptionTypeInfo(
					(EType)elem.etype,
					this.Client.TryGetEncProfile((EType)elem.etype),
					elem.salt
				));
			}
		}

		#region EncTimestamp
		protected virtual void ProcessEncTimestamp(Guid correlationId, byte[] padata_value, bool useArmor)
		{
			// Do nothing
		}
		#endregion

		private KDC_REQ_BODY? _lastReqBody;
		internal List<PA_DATA> BuildPadataList(KDC_REQ_BODY reqBody)
		{
			this._lastReqBody = reqBody;

			List<PA_DATA> paList = new List<PA_DATA>(2);
			this.BuildPadataList(reqBody, paList);
			return paList;
		}
		public PacOptions? PacRequestOptions { get; set; }
		protected virtual void BuildPadataList(KDC_REQ_BODY reqBody, List<PA_DATA> padataList)
		{
			if (this._requestPac)
			{
				padataList.Add(Structs.PAData_PacRequest(true));
				if (this.PacRequestOptions.HasValue)
					padataList.Add(Structs.PAData_PacOptions(this.PacRequestOptions.Value));
			}
			if (this._fastCookie != null)
			{
				padataList.Add(Structs.PAData_FastCookie(this._fastCookie));
				this._fastCookie = null;
			}
		}
	}

	class PreauthNullContext : PreauthContext
	{
		public PreauthNullContext(KerberosClient client, KerberosNullCredential credential, IKerberosCallback? callback = null) : base(client, callback)
		{
			Credential = credential;
		}

		protected override KerberosNullCredential Credential { get; }
	}
}
