using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Crypto
{
	public enum HashType
	{
		Other,
		Md2,
		Md4,
		Md5,
		Sha,
		Sha1,
		Sha256,
		Sha384,
		Sha512
	}

	public record class SignatureAlgorithmInfo
	{
		public SignatureAlgorithmInfo(string name, Oid oid, HashType hashAlgorithm)
		{
			Name = name;
			Oid = oid;
			HashAlgorithm = hashAlgorithm;
		}

		public string Name { get; }
		public Oid Oid { get; }
		public HashType HashAlgorithm { get; }
	}

	// [MS-GPNAP] § 2.4.1.6
	public static class SignatureAlgorithms
	{
		public static readonly Oid Sha1WithRsaSignature = new Oid("1.2.840.113549.1.1.5");
		public static readonly Oid Md5WithRsaEncryption = new Oid("1.2.840.113549.1.1.4");
		public static readonly Oid DsaWithSha1 = new Oid("1.2.840.10040.4.3");
		public static readonly Oid Sha1WithRsaEncryption = new Oid("1.3.14.3.2.29");
		public static readonly Oid ShaWithRsaSignature = new Oid("1.3.14.3.2.15");
		public static readonly Oid Md5WithRsa = new Oid("1.3.14.3.2.3");
		public static readonly Oid Md2WithRsaEncryption = new Oid("1.2.840.113549.1.1.2");
		public static readonly Oid Md4WithRsaEncryption = new Oid("1.2.840.113549.1.1.3");
		public static readonly Oid Md4WithRsa = new Oid("1.3.14.3.2.2");
		public static readonly Oid Md4WithRsaEncryption_2 = new Oid("1.3.14.3.2.4");
		public static readonly Oid Md2WithRsa = new Oid("1.3.14.7.2.3.1");
		public static readonly Oid DsaWithSha = new Oid("1.3.14.3.2.13");
		public static readonly Oid DsaWithSha1_2 = new Oid("1.3.14.3.2.27");
		public static readonly Oid MosaicUpdatedSig = new Oid("2.16.840.1.101.2.1.1.19");
		public static readonly Oid Sha1NoSign = new Oid("1.3.14.3.2.26");
		public static readonly Oid Md5NoSign = new Oid("1.2.840.113549.2.5");
		public static readonly Oid Sha256NoSign = new Oid("2.16.840.1.101.3.4.2.1");
		public static readonly Oid Sha384NoSign = new Oid("2.16.840.1.101.3.4.2.2");
		public static readonly Oid Sha512NoSign = new Oid("2.16.840.1.101.3.4.2.3");
		public static readonly Oid Sha256WithRsaEncryption = new Oid("1.2.840.113549.1.1.11");
		public static readonly Oid Sha384WithRsaEncryption = new Oid("1.2.840.113549.1.1.12");
		public static readonly Oid Sha512WithRsaEncryption = new Oid("1.2.840.113549.1.1.13");
		public static readonly Oid RsaSsaPss = new Oid("1.2.840.113549.1.1.10");
		public static readonly Oid EcdsaWithSha1 = new Oid("1.2.840.10045.4.1");
		public static readonly Oid EcdsaWithSha256 = new Oid("1.2.840.10045.4.3.2");
		public static readonly Oid EcdsaWithSha384 = new Oid("1.2.840.10045.4.3.3");
		public static readonly Oid EcdsaWithSha512 = new Oid("1.2.840.10045.4.3.4");
		public static readonly Oid EcdsaWithSha2 = new Oid("1.2.840.10045.4.3");

		private static readonly SignatureAlgorithmInfo[] allAlgorithms = new SignatureAlgorithmInfo[]
		{
			new SignatureAlgorithmInfo("Sha1WithRsaSignature", new Oid("1.2.840.113549.1.1.5"), HashType.Sha1),
			new SignatureAlgorithmInfo("Md5WithRsaEncryption", new Oid("1.2.840.113549.1.1.4"), HashType.Md5),
			new SignatureAlgorithmInfo("DsaWithSha1", new Oid("1.2.840.10040.4.3"), HashType.Sha1),
			new SignatureAlgorithmInfo("Sha1WithRsaEncryption", new Oid("1.3.14.3.2.29"), HashType.Sha1),
			new SignatureAlgorithmInfo("ShaWithRsaSignature", new Oid("1.3.14.3.2.15"), HashType.Sha),
			new SignatureAlgorithmInfo("Md5WithRsa", new Oid("1.3.14.3.2.3"), HashType.Md5),
			new SignatureAlgorithmInfo("Md2WithRsaEncryption", new Oid("1.2.840.113549.1.1.2"), HashType.Md2),
			new SignatureAlgorithmInfo("Md4WithRsaEncryption", new Oid("1.2.840.113549.1.1.3"), HashType.Md4),
			new SignatureAlgorithmInfo("Md4WithRsa", new Oid("1.3.14.3.2.2"), HashType.Md4),
			new SignatureAlgorithmInfo("Md4WithRsaEncryption", new Oid("1.3.14.3.2.4"), HashType.Md4),
			new SignatureAlgorithmInfo("Md2WithRsa", new Oid("1.3.14.7.2.3.1"), HashType.Md2),
			new SignatureAlgorithmInfo("DsaWithSha", new Oid("1.3.14.3.2.13"), HashType.Sha),
			new SignatureAlgorithmInfo("DsaWithSha1", new Oid("1.3.14.3.2.27"), HashType.Sha1),
			new SignatureAlgorithmInfo("MosaicUpdatedSig", new Oid("2.16.840.1.101.2.1.1.19"), HashType.Other),
			new SignatureAlgorithmInfo("Sha1NoSign", new Oid("1.3.14.3.2.26"), HashType.Sha1),
			new SignatureAlgorithmInfo("Md5NoSign", new Oid("1.2.840.113549.2.5"), HashType.Md5),
			new SignatureAlgorithmInfo("Sha256NoSign", new Oid("2.16.840.1.101.3.4.2.1"), HashType.Sha256),
			new SignatureAlgorithmInfo("Sha384NoSign", new Oid("2.16.840.1.101.3.4.2.2"), HashType.Sha384),
			new SignatureAlgorithmInfo("Sha512NoSign", new Oid("2.16.840.1.101.3.4.2.3"), HashType.Sha512),
			new SignatureAlgorithmInfo("Sha256WithRsaEncryption", new Oid("1.2.840.113549.1.1.11"), HashType.Sha256),
			new SignatureAlgorithmInfo("Sha384WithRsaEncryption", new Oid("1.2.840.113549.1.1.12"), HashType.Sha384),
			new SignatureAlgorithmInfo("Sha512WithRsaEncryption", new Oid("1.2.840.113549.1.1.13"), HashType.Sha512),
			new SignatureAlgorithmInfo("RsaSsaPss", new Oid("1.2.840.113549.1.1.10"), HashType.Other),
			new SignatureAlgorithmInfo("EcdsaWithSha1", new Oid("1.2.840.10045.4.1"), HashType.Sha1),
			new SignatureAlgorithmInfo("EcdsaWithSha256", new Oid("1.2.840.10045.4.3.2"), HashType.Sha256),
			new SignatureAlgorithmInfo("EcdsaWithSha384", new Oid("1.2.840.10045.4.3.3"), HashType.Sha384),
			new SignatureAlgorithmInfo("EcdsaWithSha512", new Oid("1.2.840.10045.4.3.4"), HashType.Sha512),
			new SignatureAlgorithmInfo("EcdsaWithSha256", new Oid("1.2.840.10045.4.3"), HashType.Sha256),
		};

		private static readonly Dictionary<string, SignatureAlgorithmInfo> _algsByOid = allAlgorithms.ToDictionary(r => r.Oid.Value);

		public static SignatureAlgorithmInfo? GetByOid(Oid oid)
		{
			if (oid is null) throw new ArgumentNullException(nameof(oid));
			_algsByOid.TryGetValue(oid.Value, out var alg);
			return alg;
		}
	}
}
