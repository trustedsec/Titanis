using PKIX1Implicit88;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

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

		public static string? TryGetSubjectAltName(this X509Certificate2 certificate)
		{
			foreach (var ext in certificate.Extensions)
			{
				if (ext is X509SubjectAlternativeNameExtension altName)
				{
					var decoded = SubjectAltName.TryReadFrom(altName.RawData);
					return decoded;
				}
			}

			return null;
		}

		public static string? Decode(this X509SubjectAlternativeNameExtension altName)
		{
			var decoded = SubjectAltName.TryReadFrom(altName.RawData);
			return decoded;
		}

		public static X509SubjectAlternativeNameExtension ToSubjectAltName(this string subjectAltName)
		{
			GeneralName genName = new GeneralName()
			{
				OtherName = new AnotherName(new Asn1Oid(SubjectAltName.SubjectAltNameOid), Asn1Any.CreateFromObject(new UTF8String(subjectAltName)))
			};

			var bytes = Asn1DerEncoder.EncodeTlv(new Asn1SequenceOf<GeneralName>([genName])).ToArray();

			return new X509SubjectAlternativeNameExtension(bytes, false);
		}
	}
}
