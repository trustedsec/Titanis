using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1.Serialization;

namespace Titanis.Certificates
{
	public class CertificateBuilder
	{
		public CertificateBuilder(X500DistinguishedName subject)
		{
		}
		public CertificateBuilder(X509Certificate2 templateCert)
		{
			var bytes = templateCert.Export(X509ContentType.Cert);
			var cert = Asn1DerDecoder.DecodeTlv<PKIX1Explicit88.Certificate>(bytes);
		}

		public X509Certificate2 Build()
		{
			var serialNumber = new BigInteger(1);
			//PKIX1Explicit88.TBSCertificate tbs = new PKIX1Explicit88.TBSCertificate(
			//	serialNumber,
			//	new PKIX1Explicit88.AlgorithmIdentifier(
			//		new Asn1.Asn1Oid("2.16.840.1.101.3.4.2.1")
			//	)
			//	);

			throw new NotImplementedException();
		}
	}
}
