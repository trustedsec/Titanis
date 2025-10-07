using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Crypto;
using Titanis.IO;
using Titanis.Security;
using Titanis.Smb2.Pdus;

namespace Titanis.Smb2
{
	public class Smb2Session : IDisposable, IAsyncDisposable
	{
		private static readonly byte[] EncryptionKeyLabel_Smb311_S2C = Encoding.UTF8.GetBytes("SMBS2CCipherKey\0");
		private static readonly byte[] EncryptionKeyLabel_Smb311_C2S = Encoding.UTF8.GetBytes("SMBC2SCipherKey\0");
		private static readonly byte[] EncryptionKeyLabel_Smb30 = Encoding.UTF8.GetBytes("SMB2AESCCM\0");
		private static readonly byte[] EncryptionKeyContext_Smb30_C2S = Encoding.UTF8.GetBytes("ServerIn \0");
		private static readonly byte[] EncryptionKeyContext_Smb30_S2C = Encoding.UTF8.GetBytes("ServerOut\0");

		private static readonly byte[] PreauthLabel = Encoding.UTF8.GetBytes("ServerOut\0");

		private static readonly byte[] SigningKeyLabel_Smb30 = Encoding.UTF8.GetBytes("SMB2AESCMAC\0");
		private static readonly byte[] SigningKeyLabel_Smb311 = Encoding.UTF8.GetBytes("SMBSigningKey\0");
		private static readonly byte[] SigningKeyContext_Smb30 = Encoding.UTF8.GetBytes("SmbSign\0");

		private static readonly byte[] AppKeyLabel_Smb30 = Encoding.UTF8.GetBytes("SMB2APP\0");
		private static readonly byte[] AppKeyLabel_Smb311 = Encoding.UTF8.GetBytes("SMBAppKey\0");
		private static readonly byte[] AppKeyContext_Smb30 = Encoding.UTF8.GetBytes("SmbRpc\0");

		internal Smb2Session(
			Smb2Connection conn,
			ulong sessionId,
			AuthClientContext authContext,
			bool signingRequired,
			bool mustEncryptData,
			byte[] preauthIntegrityValue,
			Pdus.ValidateNegotiateInfo? validateNegotiateInfo)
		{
			this._authContext = authContext;
			this.SessionId = sessionId;
			this.SigningRequired = signingRequired;
			this.MustEncryptData = mustEncryptData;
			this._preauthIntegrityValue = preauthIntegrityValue;

			if (authContext.HasSessionKey)
			{
				var authSessionKey = authContext.GetSessionKey();
				this._fullSessionKey = authSessionKey.ToArray();

				byte[] smb2SessionBaseKey;
				if (authSessionKey.Length == 16)
				{
					smb2SessionBaseKey = this._fullSessionKey;
				}
				else
				{
					smb2SessionBaseKey = new byte[16];
					authSessionKey.Slice(0, Math.Min(16, authSessionKey.Length)).CopyTo(smb2SessionBaseKey);
				}
				this._sessionBaseKey = smb2SessionBaseKey;

				// TODO: Check for guest session

				Smb2Dialect dialect = conn.Dialect;
				if (dialect >= Smb2Dialect.Smb3_0)
				{
					// [MS-SMB2] § 3.1.4.2 - Generating Cryptographic Keys

					// Encryption keys
					bool aes256 = conn.CipherId is Cipher.Aes256Ccm or Cipher.Aes256Gcm;
					var kdk =
						(dialect == Smb2Dialect.Smb3_1_1 && aes256) ? this._fullSessionKey
						: smb2SessionBaseKey;
					byte[] encLabel = (dialect >= Smb2Dialect.Smb3_1_1)
						? EncryptionKeyLabel_Smb311_C2S
						: EncryptionKeyLabel_Smb30
						;
					byte[] decLabel = (dialect >= Smb2Dialect.Smb3_1_1)
						? EncryptionKeyLabel_Smb311_S2C
						: EncryptionKeyLabel_Smb30
						;
					byte[] encContext = dialect >= Smb2Dialect.Smb3_1_1
						 ? preauthIntegrityValue
						 : EncryptionKeyContext_Smb30_C2S
						 ;
					byte[] decContext = dialect >= Smb2Dialect.Smb3_1_1
						 ? preauthIntegrityValue
						 : EncryptionKeyContext_Smb30_S2C
						 ;
					int cipherKeySize = aes256 ? 256 : 128;

					SigningAlgorithm signingAlgo;
					Cipher cipher;
					if (dialect >= Smb2Dialect.Smb3_1_1)
					{
						signingAlgo = conn.SigningAlgorithm;
						cipher = conn.CipherId;
					}
					else
					{
						signingAlgo = SigningAlgorithm.AesCmac;
						cipher = Cipher.Aes128Ccm;
					}

					this.CryptInfo = new Smb3SessionCryptInfo(
						smb2SessionBaseKey,
						Sp800_108.KdfCtr(encLabel, encContext, cipherKeySize, new HMACSHA256(kdk)),
						Sp800_108.KdfCtr(decLabel, decContext, cipherKeySize, new HMACSHA256(kdk)),
						signingAlgo,
						cipher,
						this.SigningRequired && !mustEncryptData
						);

					// App key
					var appLabel = (dialect >= Smb2Dialect.Smb3_1_1)
						? AppKeyLabel_Smb311
						: AppKeyLabel_Smb30
						;
					var appContext = (dialect >= Smb2Dialect.Smb3_1_1)
						 ? preauthIntegrityValue
						 : AppKeyContext_Smb30;
					this._sessionAppKey = Sp800_108.KdfCtr(appLabel, appContext, 16 * 8, new HMACSHA256(smb2SessionBaseKey));
				}
				else
				{
					this.CryptInfo = new Smb2SessionCryptInfo(this.SigningRequired);
					this._sessionAppKey = smb2SessionBaseKey;
				}
			}

			byte[]? signingKey = this.DeriveSigningKey(conn.Dialect, preauthIntegrityValue, false);

			this.primaryChannelBinding = new Smb2ChannelBindingInfo(0, conn, signingKey, validateNegotiateInfo);
		}

		private byte[] DeriveSigningKey(Smb2Dialect dialect, byte[] preauthIntegrityValue, bool binding)
		{
			byte[] signingKey;
			if (dialect >= Smb2Dialect.Smb3_0)
			{
				byte[] signLabel = (dialect >= Smb2Dialect.Smb3_1_1 && !binding)
					? SigningKeyLabel_Smb311
					: SigningKeyLabel_Smb30
					;
				byte[] signContext = (dialect >= Smb2Dialect.Smb3_1_1 && !binding)
					 ? preauthIntegrityValue
					 : SigningKeyContext_Smb30
					 ;
				signingKey = Sp800_108.KdfCtr(signLabel, signContext, 16 * 8, new HMACSHA256(this._sessionBaseKey));
			}
			else
			{
				signingKey = this._sessionBaseKey;
			}

			return signingKey;
		}

		private byte[]? _preauthIntegrityValue;
		private AuthClientContext _authContext;
		internal Smb2ChannelBindingInfo primaryChannelBinding;
		public Smb2Connection Connection => this.primaryChannelBinding.connection;

		private int _nextChannelId;

		private List<Smb2ChannelBindingInfo> _channelList = new List<Smb2ChannelBindingInfo>();
		public async Task EstablishNewChannel(
			Smb2Connection connection,
			CancellationToken cancellationToken
			)
		{
			if (connection is null) throw new ArgumentNullException(nameof(connection));
			if (connection.Dialect < Smb2Dialect.Smb3_1_1)
				throw new NotSupportedException("The connection must be SMB 3.1.1+ to support session binding.");
			if (connection.Dialect != this.Connection.Dialect)
				throw new ArgumentException("The provided connection does not use the same SMB dialect as the original connection, which is required for session binding.");

			var authContext = this._authContext.Duplicate();
			await connection.AuthenticateAsync(authContext, this.MustEncryptData, this, (ushort)Interlocked.Increment(ref this._nextChannelId), cancellationToken).ConfigureAwait(false);
			this._channelList.Add(new Smb2ChannelBindingInfo(0, connection, null, null));
		}
		internal void BindChannel(
			Smb2Connection connection,
			byte[] preauthIntegrityValue,
			ValidateNegotiateInfo? secneginfo)
		{
			this._preauthIntegrityValue = preauthIntegrityValue;
			var binding = new Smb2ChannelBindingInfo(
				0,
				connection,
				this.DeriveSigningKey(connection.Dialect, preauthIntegrityValue, true),
				secneginfo);
			this.primaryChannelBinding = binding;
			this._channelList.Add(binding);
		}

		// TODO: Make this a struct rather than an array
		private byte[] _sessionBaseKey;
		private byte[] _sessionAppKey;
		private byte[]? _fullSessionKey;
		internal Smb2SessionCryptInfoBase? CryptInfo { get; }

		public byte[] GetSessionKey() => this._sessionAppKey;
		public byte[] GetSessionBaseKey() => this._sessionBaseKey;
		public byte[]? GetFullSessionKey() => this._fullSessionKey;

		public ulong SessionId { get; set; }

		public bool SigningRequired { get; }
		public bool MustEncryptData { get; }

		private List<Smb2TreeConnect> _openTrees = new List<Smb2TreeConnect>();

		public bool AutoClose { get; set; }

		public async Task<Smb2TreeConnect> OpenTreeAsync(UncPath path, bool encrypt, CancellationToken cancellationToken)
		{
			if (path is null) throw new ArgumentNullException(nameof(path));
			if (string.IsNullOrEmpty(path.ShareName)) throw new ArgumentException("The UNC path does not include a share name.", nameof(path));

			Pdus.Smb2TreeConnectRequest req = new Pdus.Smb2TreeConnectRequest
			{
				path = path.SharePath
			};

			Pdus.Smb2TreeConnectResponse connectResp = (Pdus.Smb2TreeConnectResponse)await this.SendSyncPduAsync(req, this.MustEncryptData || encrypt, cancellationToken).ConfigureAwait(false);
			var tree = new Smb2TreeConnect(path.ShareName, this, connectResp.pduhdr.treeId, connectResp.body.shareInfo, encrypt || this.MustEncryptData);
			lock (this._openTrees)
			{
				this._openTrees.Add(tree);
			}

			if (this.primaryChannelBinding.validateNegotiateInfo != null)
			{
				Smb2OpenFileObjectBase obj = new Smb2OpenFileObjectBase(tree, string.Empty, new Smb2FileOpenInfo()
				{
					fileId = new Smb2FileHandle(0xFFFFFFFF_FFFFFFFF, 0xFFFFFFFF_FFFFFFFF)
				});

				ByteWriter writer = new ByteWriter();
				this.primaryChannelBinding.validateNegotiateInfo.WriteTo(writer);

				byte[] outputBuffer = new byte[24];
				await obj.FsctlAsync(
					(uint)Smb2FsctlCode.ValidateNegotiateInfo,
					writer.GetBuffer(), default,
					default, outputBuffer,
					cancellationToken
					).ConfigureAwait(false);

				this.primaryChannelBinding.validateNegotiateInfo = null;
			}

			return tree;
		}

		internal void OnTreeDisconnected(Smb2TreeConnect tree)
		{
			lock (this._openTrees)
			{
				this._openTrees.Remove(tree);
			}

			if (this.AutoClose && this._openTrees.Count == 0)
			{
				this.Dispose();
			}
		}

		private bool _firstPdu = true;
		internal Task<Smb2Pdu> SendSyncPduAsync(Pdus.Smb2Pdu req, bool encrypt, CancellationToken cancellationToken)
		{
			if (this._firstPdu || req.Command == Smb2Command.TreeConnect)
			{
				if (!encrypt)
					req.pduhdr.flags |= Smb2PduFlags.Signed;
				this._firstPdu = false;
			}

			req.SessionId = this.SessionId;
			return this.Connection.SendSyncPduAsync(
				req,
				cancellationToken,
				this.CryptInfo,
				this.primaryChannelBinding,
				encrypt
				);
		}

		public async Task EchoAsync(CancellationToken cancellationToken)
		{
			var req = new Pdus.Smb2EchoRequest();
			var resp = await this.SendSyncPduAsync(req, this.MustEncryptData, cancellationToken).ConfigureAwait(false);
		}

		public bool IsLoggedOff { get; private set; }
		public async Task LogOffAsync(CancellationToken cancellationToken)
		{
			if (!this.IsLoggedOff)
			{
				var req = new Pdus.Smb2LogoffRequest();
				var resp = await this.SendSyncPduAsync(req, this.MustEncryptData, cancellationToken).ConfigureAwait(false);

				this.IsLoggedOff = true;

				this.Connection.OnSessionEnded(this);
			}
		}

		#region Dispose pattern
		private bool _isDisposed;

		protected virtual void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				if (disposing)
				{
					OnDisposingAsync().Wait();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				_isDisposed = true;
			}
		}

		public async ValueTask DisposeAsync()
		{
			await this.OnDisposingAsync().ConfigureAwait(false);
			GC.SuppressFinalize(this);
		}

		private async Task OnDisposingAsync()
		{
			if (!this.IsLoggedOff)
			{
				await this.LogOffAsync(CancellationToken.None).ConfigureAwait(false);
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~Smb2Session()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}