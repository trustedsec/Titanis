using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Titanis.Certificates;
using Titanis.Cli;

namespace Cert;

public enum HashAlgorithm
{
	Md5,
	Sha1,
	Sha256,
	Sha384,
	Sha512,
	Sha3_256,
	Sha3_384,
	Sha3_512,
}

[Command]
[Description("Create a self-signed certificate")]
internal class SelfCertCommand : Command
{
	[Parameter(0)]
	[Mandatory]
	[Description("Subject name as an X.500 string")]
	public string Subject { get; set; }

	[Parameter]
	[DefaultValue(2048)]
	[Description("Size of key, in bits")]
	public int KeySizeBits { get; set; }

	[Parameter]
	[Description("Hash algorithm")]
	public HashAlgorithm? HashAlgorithm { get; set; }

	[Parameter]
	[Description("Name of file containing certificate to copy")]
	public FileSpec? TemplateFile { get; set; }

	[Parameter]
	[Description("Subject alternate name")]
	public string? SubjectAltName { get; set; }

	[Parameter]
	[Mandatory]
	[Description("Name of .pfx file")]
	public FileSpec PfxFileName { get; set; }

	[Parameter]
	[Description("Name of certificate file (.pem or .cer)")]
	public FileSpec? CertFileName { get; set; }


	private static readonly HashAlgorithmName[] hashAlgs = new HashAlgorithmName[]
	{
		HashAlgorithmName.MD5,
		HashAlgorithmName.SHA1,
		HashAlgorithmName.SHA256,
		HashAlgorithmName.SHA384,
		HashAlgorithmName.SHA512,
		HashAlgorithmName.SHA3_256,
		HashAlgorithmName.SHA3_384,
		HashAlgorithmName.SHA3_512
	};

	private static HashAlgorithmName GetHashAlgName(HashAlgorithm alg)
	{
		var n = (int)alg;
		if ((uint)n < hashAlgs.Length)
			return hashAlgs[n];
		else
			throw new ArgumentOutOfRangeException(nameof(alg));
	}

	protected override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		X509Certificate2? templateCert;
		HashAlgorithmName hashAlg;
		X500DistinguishedName? subject;

		if (this.TemplateFile != null)
		{
			var certFileName = this.ResolveFsPath(this.TemplateFile);
			this.WriteDiagnostic($"Loading template certificate from '{certFileName}'");

			byte[] certBytes = File.ReadAllBytes(certFileName);
			if (certBytes.Length == 0)
				throw new ArgumentException($"File {certFileName} does not contain any data.", nameof(certFileName));

			var certs = CertificateHelper.LoadFrom(certBytes);
			templateCert = certs[0];
			this.WriteDiagnostic($"Loaded certificate with subject `{templateCert.Subject}'.");
		}
		else
			templateCert = null;

		hashAlg = GetHashAlgName(this.HashAlgorithm.Value);
		subject = new X500DistinguishedName(this.Subject);


		RSA rsa = RSA.Create(this.KeySizeBits);
		var builder = new CertificateRequest(subject, rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

		bool hasAltName = false;
		bool userWantsAltName = !string.IsNullOrEmpty(this.SubjectAltName);
		if (templateCert != null)
		{
			foreach (var ext in templateCert.Extensions)
			{
				if (userWantsAltName && ext is X509SubjectAlternativeNameExtension altNameExt)
				{
					hasAltName = true;
					var altName = altNameExt.Decode();
					this.WriteDiagnostic($"Replacing subject alternate name '{altName}' with '{this.SubjectAltName}'");

					builder.CertificateExtensions.Add(this.SubjectAltName.ToSubjectAltName());

					hasAltName = true;
				}
				else
					builder.CertificateExtensions.Add(ext);
			}
		}

		if (userWantsAltName && !hasAltName)
		{
			builder.CertificateExtensions.Add(this.SubjectAltName.ToSubjectAltName());
		}

		this.WriteDiagnostic("Generating certificate");
		var newCert = builder.CreateSelfSigned(DateTime.Today, DateTime.Today + TimeSpan.FromDays(90));

		// Write PFX
		this.FileAccessService.WriteAllBytesTo(this.PfxFileName, newCert.Export(X509ContentType.Pfx));

		if (this.CertFileName != null)
		{
			string? outExt = this.CertFileName.Extension?.ToUpper();

			if (outExt.Equals(".PEM", StringComparison.OrdinalIgnoreCase))
				this.FileAccessService.WriteAllTextTo(this.CertFileName, newCert.ExportCertificatePem());
			else
				this.FileAccessService.WriteAllBytesTo(this.CertFileName, newCert.Export(X509ContentType.Cert));
		}


		return Task.FromResult(0);
	}
}
