using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Titanis.Crypto;
using Titanis.IO;

namespace Titanis.Security
{
	/// <summary>
	/// Implements the <c>tls-server-end-point</c> binding.
	/// </summary>
	// [RFC 5929] § 4. The 'tls-server-end-point' Channel Binding Type
	public class TlsServerEndPointChannelBinding : ChannelBinding
	{
		public TlsServerEndPointChannelBinding(X509Certificate2 serverCertificate)
		{
			if (serverCertificate is null) throw new ArgumentNullException(nameof(serverCertificate));
			ServerCertificate = serverCertificate;

			this._hashAlg = GetHashAlg(serverCertificate.SignatureAlgorithm);
		}

		public override string Name => "tls-server-end-point";

		public X509Certificate2 ServerCertificate { get; }
		private HashAlgorithm _hashAlg;

		public static HashAlgorithm? GetHashAlg(Oid algId)
		{
			var hashAlg = TryGetHashAlg(algId);
			if (hashAlg is null)
				throw new NotSupportedException("The signature algorithm indicated by the certificate is not supported.");
			return hashAlg;
		}
		public static HashAlgorithm? TryGetHashAlg(Oid algId)
		{
			var alg = SignatureAlgorithms.GetByOid(algId);
			HashAlgorithm? hashAlg = null;
			if (alg != null)
			{
				switch (alg.HashAlgorithm)
				{
					case HashType.Sha1:
					case HashType.Md5:
					case HashType.Sha256:
						hashAlg = SHA256.Create();
						break;
					case HashType.Sha384:
						hashAlg = SHA384.Create();
						break;
					case HashType.Sha512:
						hashAlg = SHA512.Create();
						break;
					default:
						// TODO: Hash algorithm not supported, warn
						break;
				}
			}

			return hashAlg;

		}

		const string ChannelBindingPrefix = "tls-server-end-point:";
		public override int RequiredLength => (5 * 4) + ChannelBindingPrefix.Length + (this._hashAlg.HashSize / 8);

		/// <inheritdoc/>
		public override int GetBytes(Span<byte> buffer)
		{
			int cbReq = this.RequiredLength;

			X509Certificate2 remoteCert = this.ServerCertificate;
			HashAlgorithm? hashAlg = this._hashAlg;
			BinaryPrimitives.WriteInt32LittleEndian(buffer.Slice(4 * 4, 4), (ChannelBindingPrefix.Length + (hashAlg.HashSize / 8)));
			var hash = hashAlg.ComputeHash(remoteCert.RawData);
			Encoding.UTF8.GetBytes(ChannelBindingPrefix.AsSpan(), buffer.Slice(5 * 4));
			hash.CopyTo(buffer.Slice((5 * 4) + ChannelBindingPrefix.Length));

			return cbReq;
		}
	}
}
