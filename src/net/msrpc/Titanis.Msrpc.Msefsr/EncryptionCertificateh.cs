using ms_efsr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msefsr
{
	public class EncryptionCertificate
	{
		private const int BLOB_X509 = 1;

		public EncryptionCertificate(
			SecurityIdentifier user,
			X509Certificate2 certificate
			)
		{
			ArgumentNullException.ThrowIfNull(user);
			ArgumentNullException.ThrowIfNull(certificate);

			this.User = user;
			this.Certificate = certificate;
		}

		public SecurityIdentifier User { get; }
		public X509Certificate2 Certificate { get; }

		internal RpcPointer<ENCRYPTION_CERTIFICATE> ToRpc()
		{
			var certBytes = this.Certificate.RawData;

			return new RpcPointer<ENCRYPTION_CERTIFICATE>(new ENCRYPTION_CERTIFICATE
			{
				// TODO: What is the actual size?
				cbTotalLength = 0,
				UserSid = new RpcPointer<ms_dtyp.RPC_SID>(this.User.ToRpcSid()),
				CertBlob = new RpcPointer<EFS_CERTIFICATE_BLOB>(new EFS_CERTIFICATE_BLOB
				{
					dwCertEncodingType = BLOB_X509,
					cbData = (uint)certBytes.Length,
					bData = new RpcPointer<byte[]>(certBytes)
				})
			});
		}
	}
}
