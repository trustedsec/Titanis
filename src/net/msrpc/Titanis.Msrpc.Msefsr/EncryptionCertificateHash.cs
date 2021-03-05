using ms_efsr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msefsr
{
	public class EncryptionCertificateHash
	{
		internal EncryptionCertificateHash(
			string displayInformation,
			SecurityIdentifier user,
			byte[] hash
			)
		{
			this.DisplayInformation = displayInformation;
			this.User = user;
			this.Hash = hash;
		}

		public string DisplayInformation { get; }
		public SecurityIdentifier User { get; }
		public byte[] Hash { get; }

		internal RpcPointer<ENCRYPTION_CERTIFICATE_HASH> ToRpc()
		{
			return new RpcPointer<ENCRYPTION_CERTIFICATE_HASH>(new ENCRYPTION_CERTIFICATE_HASH
			{
				// TODO: What is the actual size?
				cbTotalLength = 0,
				UserSid = new RpcPointer<ms_dtyp.RPC_SID>(this.User.ToRpcSid()),
				lpDisplayInformation = new RpcPointer<string>(this.DisplayInformation),
				Hash = new RpcPointer<EFS_HASH_BLOB>(new EFS_HASH_BLOB() { cbData = (uint)this.Hash.Length, bData = new RpcPointer<byte[]>(this.Hash) })
			});
		}
	}
}
