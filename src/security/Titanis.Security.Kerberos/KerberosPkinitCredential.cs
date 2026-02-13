using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Titanis.Crypto.DiffieHellman;

namespace Titanis.Security.Kerberos
{
	public class KerberosPkinitCredential : KerberosCredential
	{
		public KerberosPkinitCredential(UserPrincipalName upn, X509Certificate2 certificate)
			: base(upn)
		{
			this.Certificate = certificate;
		}

		public X509Certificate2 Certificate { get; }

		public override SessionKey DeriveProtocolKeyFor(EncProfile profile, byte[]? salt)
		{
			throw new NotImplementedException();
		}

		internal sealed override bool SupportsPreauthType(PadataType preauthType)
			=> preauthType is PadataType.PkASReq or PadataType.PkASrepOld;

		public override bool SupportsProfile(EType etype) => true;

		internal override PreauthContext CreatePreauthContext(KerberosClient client, IKerberosCallback? callback)
			=> new PreauthPkinitContext(client, this, callback);
	}
}
