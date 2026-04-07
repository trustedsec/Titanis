using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using Titanis.Asn1.Serialization;

namespace Titanis.Security.Kerberos
{
	class PreauthKeyContext : PreauthContext
	{
		public PreauthKeyContext(KerberosClient client, KerberosKeyCredentialBase credential, IKerberosCallback? callback = null) : base(client, callback)
		{
			this.Credential = credential;
		}

		protected override KerberosKeyCredentialBase Credential { get; }

		protected override void BuildPadataList(KDC_REQ_BODY reqBody, List<PA_DATA> padataList)
		{
			if (this._tsenc is not null)
				padataList.Add(this._tsenc);

			base.BuildPadataList(reqBody, padataList);
		}

		#region Encrypted timestamp
		private PA_DATA? _tsenc;

		private Memory<byte> EncryptTimestamp(Guid correlationId)
		{
			var cred = this.Credential;
			if (cred == null)
				throw new InvalidOperationException("Cannot encrypt timestame because no credential was provided.");

			PA_ENC_TS_ENC tsenc = Structs.PAEnc_TSEnc(this.Skew);

			byte[] tsencBytes = Asn1DerEncoder.EncodeTlv(tsenc).ToArray();
			var encInfo = this.TryGetSupportedEncProfile();
			byte[]? salt = encInfo.Salt;

			var encProfile = encInfo.encProfile;
			var protoKey = cred.DeriveProtocolKeyFor(encProfile, salt);
			this.Callback?.OnEncryptingTS(correlationId, protoKey, salt);
			var tsencData = protoKey.EncryptAndWrap(KeyUsage.AsreqPaEncTimestamp, tsencBytes);

			var padataBytes = Asn1DerEncoder.EncodeTlv(tsencData);

			return padataBytes;
		}

		protected override void ProcessEncTimestamp(Guid correlationId, byte[] padata_value)
		{
			var cred = this.Credential;
			if (cred != null && cred.SupportsPreauthType(PadataType.EncTimestamp))
			{
				var tsenc = this.EncryptTimestamp(correlationId);
				this._tsenc = Structs.PAData_TSEnc(tsenc.ToArray());
			}
		}
		#endregion

		protected override bool ProcessPadata(Guid correlationId, PA_DATA padata)
		{
			switch ((PadataType)padata.padata_type)
			{
				case PadataType.EncTimestamp:
					this.ProcessEncTimestamp(correlationId, padata.padata_value);
					return true;
			}
			return base.ProcessPadata(correlationId, padata);
		}
	}
}
