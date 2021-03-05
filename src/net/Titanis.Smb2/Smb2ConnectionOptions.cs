using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2
{
	public class Smb2ConnectionOptions
	{
		public Smb2ConnectionOptions()
		{

		}

		public static readonly Smb2Dialect[] DefaultSupportedDialects = new Smb2Dialect[]
		{
			Smb2Dialect.Smb2_0_2,
			Smb2Dialect.Smb2_1,
			Smb2Dialect.Smb3_0,
			Smb2Dialect.Smb3_0_2,
			Smb2Dialect.Smb3_1_1
		};

		public const Smb2Capabilities DefaultSmb3Caps = 0
			| Smb2Capabilities.Dfs
			| Smb2Capabilities.Leasing
			| Smb2Capabilities.LargeMtu
			| Smb2Capabilities.MultiChannel
			| Smb2Capabilities.PersistentHandles
			| Smb2Capabilities.DirectoryLeasing
			| Smb2Capabilities.Encryption
			;
		/// <summary>
		/// Gets or sets the supported capabilities.
		/// </summary>
		public Smb2Capabilities Capabilities { get; set; } = DefaultSmb3Caps;

		/// <summary>
		/// Gets or sets a value determining whether the client enforces version compliance.
		/// </summary>
		/// <remarks>
		/// Version compliance is enforced by default.  Disabling enforcement allows the caller
		/// to incorrectly configure the SMB2 client.
		/// </remarks>
		public bool EnforcesVersionCompliance { get; set; } = true;
		/// <summary>
		/// Gets or sets a value specifying which SMB2 dialects are supported.
		/// </summary>
		public Smb2Dialect[] SupportedDialects { get; set; } = DefaultSupportedDialects;
		/// <summary>
		/// Gets or sets a value specifying the security mode for new connections.
		/// </summary>
		public Smb2SecurityMode SecurityMode { get; set; } = Smb2SecurityMode.SigningEnabled;
		/// <summary>
		/// Gets a value indicating whether the client requires message signing.
		/// </summary>
		/// <seealso cref="SecurityMode"/>
		public bool RequiresMessageSigning => 0 != (this.SecurityMode & Smb2SecurityMode.SigningRequired);
		/// <summary>
		/// Gets or sets a value indicating whether the client requires secure negotiation.
		/// </summary>
		public bool RequiresSecureNegotiate { get; set; }
		/// <summary>
		/// Gets or sets the client ID.
		/// </summary>
		public Guid ClientGuid { get; set; } = Guid.NewGuid();
		/// <summary>
		/// Gets or sets a value specifying the supported hash algorithms.
		/// </summary>
		public PreauthHashAlgorithm[]? SupportedHashAlgs { get; set; } = new PreauthHashAlgorithm[] { PreauthHashAlgorithm.Sha512 };

		public const int DefaultPreauthSaltLength = 32;

		/// <summary>
		/// Gets or sets the lenght of the salt for preauthentication.
		/// </summary>
		public int PreauthSaltLength { get; set; } = DefaultPreauthSaltLength;

		public static readonly Cipher[] DefaultCiphers = new Cipher[]
		{
			Cipher.Aes128Gcm,
			Cipher.Aes128Ccm,
			Cipher.Aes256Gcm,
			Cipher.Aes256Ccm
		};
		public static readonly SigningAlgorithm[] DefaultSigningAlgorithms = new SigningAlgorithm[] { SigningAlgorithm.AesGmac, SigningAlgorithm.AesCmac };

		/// <summary>
		/// Gets or sets a value specifying supported ciphers.
		/// </summary>
		public Cipher[]? SupportedCiphers { get; set; } = (Cipher[])DefaultCiphers.Clone();
		/// <summary>
		/// Gets or sets a value specifying supported ciphers.
		/// </summary>
		public SigningAlgorithm[]? SupportedSigningAlgorithms { get; set; } = DefaultSigningAlgorithms;


		public const CompressionCaps DefaultCompressionCaps = CompressionCaps.None;


		/// <summary>
		/// Gets or sets a value specifying compression capabilities.
		/// </summary>
		public CompressionCaps CompressionCaps { get; set; } = DefaultCompressionCaps;

		public static readonly CompressionAlgorithm[] DefaultCompressionAlgorithms = new CompressionAlgorithm[] {
			CompressionAlgorithm.Pattern_V1,
			CompressionAlgorithm.Lz77,
			CompressionAlgorithm.Lz77_Huffman,
			CompressionAlgorithm.Lznt1,
		};
		/// <summary>
		/// Gets or sets a value specifying supported compression algorithms.
		/// </summary>
		public CompressionAlgorithm[]? SupportedCompressionAlgorithms { get; set; } = DefaultCompressionAlgorithms;
		/// <summary>
		/// Gets or sets the supported RDMA transforms.
		/// </summary>
		public RdmaTransformId[]? SupportedRdmaTransforms { get; set; } = new RdmaTransformId[]
		{
			RdmaTransformId.Encryption,
			RdmaTransformId.Signing,
		};

		internal void VerifyConnectionReady()
		{
			if (this.SupportedDialects.IsNullOrEmpty())
				throw new InvalidOperationException(Messages.Smb2_NoDialects);

			var maxDialect = this.SupportedDialects.Max();

			if (maxDialect >= Smb2Dialect.Smb3_1_1)
			{

				if (!this.EnforcesVersionCompliance)
				{
					if (this.SupportedHashAlgs.IsNullOrEmpty())
						throw new InvalidOperationException(Messages.Smb2_NoHashAlgs);

					if (this.SecurityMode == 0)
						throw new InvalidOperationException(Messages.Smb2_Smb311VersionSigningRequired);

					if (this.PreauthSaltLength <= 0)
						throw new InvalidOperationException(Messages.Smb2_BadPreauthSaltLength);
				}
			}
		}
	}
}
