using KerberosV5Spec2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

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
		internal PA_DATA[]? TryProcessPadata(IList<PA_DATA>? paList)
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

				if (hasSupportedPreauth)
					return this.BuildPadataList(this._lastReqBody);
			}

			return null;
		}

		/// <summary>
		/// Processes a <see cref="PA_DATA"/> from the AS.
		/// </summary>
		/// <param name="padata"><see cref="PA_DATA"/> from the AS</param>
		/// <returns><see langword="true"/> if <see cref="PA_DATA"/> is supported and can be used to produce a <see cref="PA_DATA"/> authenticating the user.</returns>
		/// <remarks>
		/// The return value is used by the caller to determine whether any of the <see cref="PA_DATA"/> sent by the server are supported.
		/// </remarks>
		protected virtual bool ProcessPadata(PA_DATA padata)
		{
			this.paTypes.Add((PadataType)padata.padata_type);

			// TODO: Implement the rest of these types
			switch ((PadataType)padata.padata_type)
			{
				case PadataType.PasswordSalt:
					this.ProcessPasswordSalt(padata.padata_value);
					return true;
				case PadataType.ETypeInfo:
					this.ProcessETypeInfo(padata.padata_value);
					return true;
				case PadataType.ETypeInfo2:
					this.ProcessETypeInfo2(padata.padata_value);
					return true;


				case PadataType.TgsReq:
				case PadataType.PacRequest:
				case PadataType.SvrReferralInfo:
				case PadataType.FxCookie:
				case PadataType.FxFast:
				case PadataType.FxError:
				case PadataType.EncryptedChallenge:
				case PadataType.SupportedEncTypes:
				case PadataType.PacOptions:
				case PadataType.KerbKeyListReq:
				case PadataType.KerbKeyListRep:
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

		private void ProcessETypeInfo2(byte[] padata_value)
		{
			var etypes = (this.etypesFromKdc ??= new List<KdcEncryptionTypeInfo>());
			var etypeInfos = Asn1DerDecoder.DecodeTlv<Asn1SequenceOf<ETYPE_INFO2_ENTRY>>(padata_value).Values;
			this.Callback?.OnProcessETypes(etypeInfos);
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

		private void ProcessETypeInfo(byte[] padata_value)
		{
			var etypes = (this.etypesFromKdc ??= new List<KdcEncryptionTypeInfo>());
			var etypeInfos = Asn1DerDecoder.DecodeTlv<Asn1SequenceOf<ETYPE_INFO_ENTRY>>(padata_value).Values;
			this.Callback?.OnProcessETypes(etypeInfos);
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
		protected virtual void ProcessEncTimestamp(byte[] padata_value)
		{
			// Do nothing
		}
		#endregion

		private KDC_REQ_BODY? _lastReqBody;
		internal PA_DATA[] BuildPadataList(KDC_REQ_BODY reqBody)
		{
			this._lastReqBody = reqBody;

			List<PA_DATA> paList = new List<PA_DATA>(2);
			this.BuildPadataList(reqBody, paList);
			return paList.ToArray();
		}
		protected virtual void BuildPadataList(KDC_REQ_BODY reqBody, List<PA_DATA> padataList)
		{
			if (this._requestPac)
				padataList.Add(Structs.PAData_PacRequest(true));
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
