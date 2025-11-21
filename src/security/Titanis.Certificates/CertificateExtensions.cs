using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Certificates
{
	public static class CertificateExtensions
	{
		public static readonly Oid EnhancedKeyUsage = new Oid("2.5.29.37");

		public static bool HasEku(this X509Certificate2 certificate, Oid usage)
		{
			ArgumentNullException.ThrowIfNull(certificate);
			ArgumentNullException.ThrowIfNull(usage);

			var ekuExtension = certificate.Extensions.OfType<X509EnhancedKeyUsageExtension>().FirstOrDefault();

			if (ekuExtension == null)
				return false;

			bool hasEku = ekuExtension.EnhancedKeyUsages.OfType<Oid>().Any(s => usage.Value == s.Value);
			return hasEku;
		}
	}
}
