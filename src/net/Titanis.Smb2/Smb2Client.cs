using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Spnego;
using Titanis.Smb2.Pdus;

namespace Titanis.Smb2
{
	/// <summary>
	/// Implements an SMB2 protocol client.
	/// </summary>
	public partial class Smb2Client
	{
		/// <summary>
		/// Initializes a new <see cref="Smb2Client"/>.
		/// </summary>
		/// <param name="socketService"><see cref="ISocketService"/> implementation</param>
		public Smb2Client(
			IClientCredentialService credentialService,
			ISocketService? socketService = null,
			INameResolverService? nameResolver = null,
			ISmbOptionsService? optionsService = null,
			ISmb2TraceCallback? traceCallback = null,
			ILog? log = null
			)
		{
			this.socketService = socketService ?? Singleton.SingleInstance<PlatformSocketService>();
			this.credentialService = credentialService;
			this.nameResolver = nameResolver ?? new PlatformNameResolverService(log: log);
			this.optionsService = optionsService;
			this.traceCallback = traceCallback;
		}

		/// <summary>
		/// Name of the service class for composing SPNs.
		/// </summary>
		public const string ServiceClass = ServiceClassNames.Cifs;

		/// <summary>
		/// Gets or sets the connection options to use for new connections.
		/// </summary>
		public Smb2ConnectionOptions DefaultConnectionOptions { get; set; } = new Smb2ConnectionOptions();
		public Smb2SessionOptions DefaultSessionOptions { get; set; } = new Smb2SessionOptions(false);
		public Smb2ShareOptions DefaultShareOptions { get; set; } = new Smb2ShareOptions(false);

		/// <summary>
		/// Provides RNG services throughout the implementation.
		/// </summary>
		internal static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();


		// [MS-SMB2] § 1.9 Standards Assignments
		public const int TcpPort = 445;

		private readonly ISocketService socketService;
		private readonly IClientCredentialService credentialService;
		private readonly INameResolverService nameResolver;
		private readonly ISmbOptionsService? optionsService;
		private readonly ISmb2TraceCallback? traceCallback;

		/// <summary>
		/// Connects to an SMB2 server.
		/// </summary>
		/// <param name="serverEP">Server endpoint</param>
		/// <param name="serverName">Server host name</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns>An <see cref="Smb2Connection"/> representing the connection</returns>
		/// <exception cref="ArgumentNullException"><paramref name="serverEP"/> is <see langword="null"/>.</exception>
		public async Task<Smb2Connection> ConnectToAsync(
			EndPoint serverEP,
			string serverName,
			Smb2ConnectionOptions options,
			CancellationToken cancellationToken)
		{
			if (serverEP is null)
				throw new ArgumentNullException(nameof(serverEP));
			if (options is null) throw new ArgumentNullException(nameof(options));

			options.VerifyConnectionReady();

			ISocket? clientSocket = null;
			try
			{
				clientSocket = this.socketService.CreateTcpSocket(serverEP.AddressFamilyOrDefault(AddressFamily.InterNetwork));
				this.traceCallback?.OnConnecting(serverEP, serverName, options);
				await clientSocket.ConnectAsync(serverEP, cancellationToken).ConfigureAwait(false);

				Stream? stream = null;
				try
				{
					stream = clientSocket.GetStream(true);
					clientSocket = null;

					var conn = await Smb2Connection.ConnectToAsync(
						stream,
						serverName,
						options,
						this,
						cancellationToken).ConfigureAwait(false);
					this.traceCallback?.OnConnected(serverEP, conn);
					stream = null;
					return conn;
				}
				finally
				{
					stream?.Dispose();
				}
			}
			finally
			{
				clientSocket?.Dispose();
			}
		}

		#region Connections
		struct ConnectionKey(string serverName, int port) : IEquatable<ConnectionKey>
		{
			public string ServerName => serverName;
			public int Port => port;

			public override bool Equals(object? obj)
				=> obj is ConnectionKey key && this.Equals(key);

			public bool Equals(ConnectionKey other)
				=> this.ServerName.Equals(other.ServerName, StringComparison.OrdinalIgnoreCase) &&
					   this.Port == other.Port;

			public override int GetHashCode()
				=> System.HashCode.Combine(this.ServerName.GetHashCode(StringComparison.OrdinalIgnoreCase), this.Port);

			public static bool operator ==(ConnectionKey left, ConnectionKey right)
				=> left.Equals(right);

			public static bool operator !=(ConnectionKey left, ConnectionKey right)
				=> !(left == right);
		}
		private Dictionary<ConnectionKey, ConnectionGroup> _connections = new Dictionary<ConnectionKey, ConnectionGroup>();

		// connGroup.connections[connGroup.selectIndex++ % connGroup.connections.Count]
		internal async Task<ConnectionGroup> GetConnectionAsync(
			string serverName,
			int port,
			CancellationToken cancellationToken)
		{
			if (this._connections.TryGetValue(new ConnectionKey(serverName, port), out var connGroup))
				return connGroup;
			else
				return await this.ConnectToAsync(serverName, port, cancellationToken).ConfigureAwait(false);
		}

		public bool UseMultiChannel { get; set; }

		private async Task<ConnectionGroup> ConnectToAsync(string serverName, int port, CancellationToken cancellationToken)
		{
			// Resolve the host address
			IPAddress[]? addrs = await this.nameResolver.ResolveAsync(serverName, cancellationToken).ConfigureAwait(false);

			Smb2ConnectionOptions options = null;
			if (this.optionsService != null)
			{
				options = this.optionsService.GetConnectionOptionsFor(serverName);
			}

			if (options is null)
				options = this.DefaultConnectionOptions;

			// Try each of the resolved addresses
			Exception? exception = null;
			this._connections.TryGetValue(new ConnectionKey(serverName, port), out ConnectionGroup? connGroup);
			for (int i = 0; i < addrs.Length; i++)
			{
				IPAddress? addr = addrs[i];
				var remoteEP = new IPEndPoint(addr, port);
				try
				{
					// Connect to the server
					var conn = await this.ConnectToAsync(remoteEP, serverName, options, cancellationToken).ConfigureAwait(false);
					if (connGroup == null)
					{
						connGroup ??= new ConnectionGroup(conn);
						this._connections.Add(new ConnectionKey(serverName, port), connGroup);
					}
					else
					{
						connGroup.connections.Add(conn);
					}

					if (!this.UseMultiChannel || conn.Dialect < Smb2Dialect.Smb3_1_1)
						break;
				}
				catch (Exception ex)
				{
					exception = ex;
				}
			}

			if (connGroup != null)
				return connGroup;

			throw new InvalidOperationException($"Unable to connect to {serverName}: {exception.Message}", exception);
		}
		#endregion
		#region Sessions
		struct SessionKey(ConnectionKey connectionKey) : IEquatable<SessionKey>
		{
			public ConnectionKey ConnectionKey => connectionKey;

			public override bool Equals(object? obj)
				=> obj is SessionKey key && this.Equals(key);

			public bool Equals(SessionKey other)
				=> this.ConnectionKey == other.ConnectionKey;

			public override int GetHashCode()
				=> System.HashCode.Combine(this.ConnectionKey);

			public static bool operator ==(SessionKey left, SessionKey right)
				=> left.Equals(right);

			public static bool operator !=(SessionKey left, SessionKey right)
				=> !(left == right);
		}

		private Dictionary<SessionKey, Smb2Session> _sessions = new Dictionary<SessionKey, Smb2Session>();

		public async ValueTask<Smb2Session> GetSession(
			string serverName,
			int port,
			Smb2SessionOptions options,
			CancellationToken cancellationToken)
		{
			if (this._sessions.TryGetValue(new SessionKey(new ConnectionKey(serverName, port)), out var session))
				return session;
			else
				return await this.OpenSessionAsync(serverName, port, options, cancellationToken).ConfigureAwait(false);
		}

		private async ValueTask<Smb2Session> OpenSessionAsync(
			string serverName,
			int port,
			Smb2SessionOptions options,
			CancellationToken cancellationToken
			)
		{
			var connGroup = await this.GetConnectionAsync(serverName, port, cancellationToken).ConfigureAwait(false);
			var conn0 = connGroup.SelectConnection();

			var authContext = this.credentialService.GetAuthContextForService(new ServicePrincipalName(ServiceClass, serverName));

			const int SpnegoRpcType = 9;
			if (authContext == null)
				throw new InvalidOperationException($"No credential is available for server `{serverName}`.");
			else if (authContext.RpcAuthType != SpnegoRpcType)
			{
				SpnegoClientContext spnego = new SpnegoClientContext();
				spnego.Contexts.Add(authContext);
				authContext = spnego;
			}

			var session = await conn0.AuthenticateAsync(authContext, options.MustEncryptData, null, 0, cancellationToken).ConfigureAwait(false);
			this.traceCallback?.OnSessionAuthenticated(session);

			this._sessions.Add(new SessionKey(new ConnectionKey(serverName, port)), session);

			if (this.UseMultiChannel && conn0.Dialect >= Smb2Dialect.Smb3_1_1)
			{
				for (int i = 1; i < connGroup.connections.Count; i++)
				{
					var conn = connGroup.SelectConnection();
					await session.EstablishNewChannel(conn, cancellationToken).ConfigureAwait(false);
				}
			}

			//var conn2 = await this.ConnectToAsync(serverName, port, false, cancellationToken).ConfigureAwait(false);
			//await conn2.AuthenticateAsync(this.credentialService.GetAuthContextForServer(ResourceTypes.Server, serverName), false, session, cancellationToken).ConfigureAwait(false);

			return session;
		}
		#endregion
		#region Shares
		struct ShareKey(SessionKey sessionKey, string shareName) : IEquatable<ShareKey>
		{
			public SessionKey SessionKey => sessionKey;
			public string ShareName => shareName;

			public override bool Equals(object? obj)
			{
				return obj is ShareKey key && this.Equals(key);
			}

			public bool Equals(ShareKey other)
				=> this.SessionKey.Equals(other.SessionKey) &&
					   this.ShareName.Equals(other.ShareName, StringComparison.OrdinalIgnoreCase);

			public override int GetHashCode()
				=> System.HashCode.Combine(this.SessionKey, this.ShareName.GetHashCode(StringComparison.OrdinalIgnoreCase));

			public static bool operator ==(ShareKey left, ShareKey right)
				=> left.Equals(right);
			public static bool operator !=(ShareKey left, ShareKey right)
				=> !(left == right);
		}
		private Dictionary<ShareKey, Smb2TreeConnect> _shares = new Dictionary<ShareKey, Smb2TreeConnect>();

		public async ValueTask<Smb2TreeConnect> GetShare(UncPath uncPath, CancellationToken cancellationToken)
		{
			Smb2TreeConnect share;
			if (TryGetCachedShare(uncPath, out share))
				return share;
			else
			{
				return await ConnectToShareAsync(uncPath, cancellationToken).ConfigureAwait(false);
			}
		}

		private bool TryGetCachedShare(UncPath uncPath, out Smb2TreeConnect share)
		{
			return this._shares.TryGetValue(new ShareKey(new SessionKey(new ConnectionKey(uncPath.ServerName, uncPath.Port)), uncPath.ShareName), out share);
		}

		private async Task<Smb2TreeConnect> ConnectToShareAsync(UncPath uncPath, CancellationToken cancellationToken)
		{
			var session = await this.GetSession(uncPath.ServerName, uncPath.Port, this.DefaultSessionOptions, cancellationToken).ConfigureAwait(false);
			var share = await session.OpenTreeAsync(uncPath, this.DefaultShareOptions.MustEncryptData, cancellationToken).ConfigureAwait(false);
			this.traceCallback?.OnShareConnected(uncPath, share);
			this._shares.Add(new ShareKey(new SessionKey(new ConnectionKey(uncPath.ServerName, uncPath.Port)), uncPath.ShareName), share);
			return share;
		}
		#endregion

		public bool FollowDfs { get; set; } = true;
		public const string IpcName = "IPC$";

		private Dictionary<string, DfsReferral> _dfsReferralCache = new Dictionary<string, DfsReferral>(StringComparer.OrdinalIgnoreCase);

		// Windows default
		public int DfsReferralBufferSize { get; set; } = 4096;

		private async Task<(Smb2TreeConnect share, UncPath resolvedPath)> ResolvePath(UncPath uncPath, CancellationToken cancellationToken)
		{
			if (this.FollowDfs && !uncPath.ShareName.EndsWith('$') && !IpcName.Equals(uncPath.ShareName, StringComparison.OrdinalIgnoreCase))
			{
				if (this.TryGetCachedShare(uncPath, out var cachedShare) && !cachedShare.IsDfs)
					return (cachedShare, uncPath);

				var ipc = await this.GetShare(new UncPath(uncPath.ServerName, uncPath.Port, IpcName, null), cancellationToken).ConfigureAwait(false);
				UncPath sharePath = uncPath.GetShare();

				var rootReferral = await ipc.QueryDfsReferrals(sharePath, DfsReferralBufferSize, cancellationToken).ConfigureAwait(false);
				if (rootReferral != null)
				{
					this.traceCallback?.OnDfsReferralReceived(sharePath, rootReferral);

					// Search for a referral with either the exact same name,
					// or one that looks like an FQDN beginning with the server name


					(var rootRefShare, var rootRefPath) = await this.FollowDfsReferral(uncPath.GetShare(), rootReferral, cancellationToken).ConfigureAwait(false);
					if (rootRefShare != null)
					{
						var linkPath = new UncPath(rootRefPath.ServerName, rootRefPath.Port, rootRefPath.ShareName, uncPath.ShareRelativePath);
						var linkReferral = await ipc.QueryDfsReferrals(linkPath, this.DfsReferralBufferSize, cancellationToken).ConfigureAwait(false);

						(var linkRefShare, var linkRefPath) = await this.FollowDfsReferral(linkPath, linkReferral, cancellationToken).ConfigureAwait(false);

						return (linkRefShare, linkRefPath);
					}
				}
			}

			var share = await this.GetShare(uncPath, cancellationToken).ConfigureAwait(false);
			return (share, uncPath);
		}

		private async Task<(Smb2TreeConnect? refShare, UncPath refPath)> FollowDfsReferral(
			UncPath uncPath,
			DfsReferral referral,
			CancellationToken cancellationToken
			)
		{
			Smb2TreeConnect? refShare = null;
			UncPath? refPath = null;
			{
				DfsReferralEntry? preferred = SelectPreferredReferral(uncPath, referral);
				if (preferred != null)
				{
					// Normalize the server name
					// This accommodates cases where the user provides the address for the host name
					// but not the FQDN
					var refPath_ = UncPath.Parse(preferred.DfsTarget + uncPath.PathForDfsReferral.Substring(referral.PathConsumed / 2));
					refPath_ = new UncPath(uncPath.ServerName, uncPath.Port, refPath_.ShareName, refPath_.ShareRelativePath);

					try
					{
						refShare = await this.GetShare(refPath_, cancellationToken).ConfigureAwait(false);
						refPath = refPath_;

						this.traceCallback?.OnDfsReferralFollowed(uncPath, refShare, refPath);
					}
					catch (Exception ex)
					{
						this.traceCallback?.OnDfsReferralConnectFailed(uncPath, referral, preferred, refPath_, ex);
					}

				}

				if (refShare == null)
				{
					foreach (var entry in referral.Entries)
					{
						try
						{
							refShare = await this.GetShare(entry.DfsTarget, cancellationToken).ConfigureAwait(false);
							refPath = entry.DfsTarget;

							this.traceCallback?.OnDfsReferralFollowed(uncPath, refShare, refPath);

							break;
						}
						catch (Exception ex)
						{
							this.traceCallback?.OnDfsReferralConnectFailed(uncPath, referral, entry, refPath, ex);
							continue;
						}
					}
				}
			}

			return (refShare, refPath);
		}

		private static DfsReferralEntry? SelectPreferredReferral(UncPath uncPath, DfsReferral? rootReferral)
		{
			DfsReferralEntry? exactNameMatch = null;
			DfsReferralEntry? partialNameMatch = null;
			foreach (var entry in rootReferral.Entries)
			{
				if (entry.DfsTarget.ServerName.Equals(uncPath.ServerName, StringComparison.OrdinalIgnoreCase))
					exactNameMatch = entry;
				else if (entry.DfsTarget.ServerName.StartsWith(uncPath.ServerName + '.', StringComparison.OrdinalIgnoreCase))
					partialNameMatch = entry;
			}
			DfsReferralEntry? preferred = exactNameMatch ?? partialNameMatch;
			return preferred;
		}

		public Task<Smb2Directory> OpenDirectoryAsync(string uncPath, CancellationToken cancellationToken)
			=> OpenDirectoryAsync(UncPath.Parse(uncPath), cancellationToken);
		public async Task<Smb2Directory> OpenDirectoryAsync(UncPath uncPath, CancellationToken cancellationToken)
		{
			if (uncPath is null) throw new ArgumentNullException(nameof(uncPath));

			(var share, var resolvedPath) = await this.ResolvePath(uncPath, cancellationToken).ConfigureAwait(false);
			return await share.OpenDirectoryAsync(resolvedPath.ShareRelativePath, cancellationToken).ConfigureAwait(false);
		}

		public Task<Smb2Pipe> OpenPipeAsync(string uncPath, CancellationToken cancellationToken)
			=> OpenPipeAsync(UncPath.Parse(uncPath), cancellationToken);
		public async Task<Smb2Pipe> OpenPipeAsync(UncPath uncPath, CancellationToken cancellationToken)
		{
			if (uncPath is null) throw new ArgumentNullException(nameof(uncPath));

			(var share, var resolvedPath) = await this.ResolvePath(uncPath, cancellationToken).ConfigureAwait(false);
			return await share.OpenPipeAsync(resolvedPath.ShareRelativePath, cancellationToken).ConfigureAwait(false);
		}

		public Task<Smb2OpenFile> OpenFileReadAsync(string uncPath, CancellationToken cancellationToken)
			=> OpenFileReadAsync(UncPath.Parse(uncPath), cancellationToken);
		public async Task<Smb2OpenFile> OpenFileReadAsync(UncPath uncPath, CancellationToken cancellationToken)
		{
			if (uncPath is null) throw new ArgumentNullException(nameof(uncPath));

			(var share, var resolvedPath) = await this.ResolvePath(uncPath, cancellationToken).ConfigureAwait(false);
			return await share.OpenFileReadAsync(resolvedPath.ShareRelativePath, cancellationToken).ConfigureAwait(false);
		}

		public Task<Smb2OpenFile> CreateFileAsync(string uncPath, CancellationToken cancellationToken)
			=> CreateFileAsync(UncPath.Parse(uncPath), cancellationToken);
		public Task<Smb2OpenFile> CreateFileAsync(UncPath uncPath, CancellationToken cancellationToken)
			=> this.CreateFileAsync(uncPath, Winterop.FileAttributes.Normal, cancellationToken);
		public async Task<Smb2OpenFile> CreateFileAsync(UncPath uncPath, Winterop.FileAttributes attributes, CancellationToken cancellationToken)
		{
			if (uncPath is null) throw new ArgumentNullException(nameof(uncPath));

			(var share, var resolvedPath) = await this.ResolvePath(uncPath, cancellationToken).ConfigureAwait(false);
			return await share.CreateFileAsync(resolvedPath.ShareRelativePath, attributes, cancellationToken).ConfigureAwait(false);
		}
		public async Task<Smb2OpenFileObjectBase> CreateFileAsync(
			UncPath uncPath,
			Smb2CreateInfo createInfo,
			FileAccess access,
			CancellationToken cancellationToken)
		{
			if (uncPath is null) throw new ArgumentNullException(nameof(uncPath));

			(var share, var resolvedPath) = await this.ResolvePath(uncPath, cancellationToken).ConfigureAwait(false);
			return await share.CreateFileAsync(resolvedPath.ShareRelativePath, createInfo, access, cancellationToken).ConfigureAwait(false);
		}
		public async Task<Smb2Directory> CreateDirectoryAsync(
			UncPath uncPath,
			CancellationToken cancellationToken)
		{
			if (uncPath is null) throw new ArgumentNullException(nameof(uncPath));

			(var share, var resolvedPath) = await this.ResolvePath(uncPath, cancellationToken).ConfigureAwait(false);
			return await share.CreateDirectoryAsync(resolvedPath.ShareRelativePath, cancellationToken).ConfigureAwait(false);
		}
		public async Task RemoveDirectoryAsync(
			UncPath uncPath,
			CancellationToken cancellationToken)
		{
			if (uncPath is null) throw new ArgumentNullException(nameof(uncPath));

			(var share, var resolvedPath) = await this.ResolvePath(uncPath, cancellationToken).ConfigureAwait(false);
			await share.RemoveDirectoryAsync(resolvedPath.ShareRelativePath, cancellationToken).ConfigureAwait(false);
		}
		public async Task DeleteFileAsync(
			UncPath uncPath,
			CancellationToken cancellationToken)
		{
			if (uncPath is null) throw new ArgumentNullException(nameof(uncPath));

			(var share, var resolvedPath) = await this.ResolvePath(uncPath, cancellationToken).ConfigureAwait(false);
			await share.DeleteFileAsync(resolvedPath.ShareRelativePath, cancellationToken).ConfigureAwait(false);
		}

		// Observed from TYPE command on Windows 11
		public const int DefaultChunkSize = 32768;

		/// <summary>
		/// Copies a local file to a UNC path over SMB2.
		/// </summary>
		/// <param name="sourceFileName">Local path of the source file</param>
		/// <param name="destinationFileName">Destination UNC path</param>
		/// <param name="overwrite"><see langword="true"/> to overwrite the file at <paramref name="destinationFileName"/> if it exists</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>
		/// A <see cref="Smb2CreateAction"/> value indicating whether the file existed.
		/// </returns>
		/// <remarks>
		/// In addition to copying the file data, this method also copies the file write time
		/// and file attributes.
		/// </remarks>
		public async Task<Smb2CreateAction> PutFileAsync(
			string sourceFileName,
			UncPath destinationFileName,
			bool overwrite,
			CancellationToken cancellationToken,
			int chunkSize = DefaultChunkSize)
		{
			if (string.IsNullOrEmpty(sourceFileName)) throw new ArgumentException($"'{nameof(sourceFileName)}' cannot be null or empty.", nameof(sourceFileName));
			ArgumentNullException.ThrowIfNull(destinationFileName);

			// Open the source file
			using (var sourceStream = File.OpenRead(sourceFileName))
			{
				// Get source file attributes
				Winterop.FileAttributes attrs = string.IsNullOrEmpty(sourceFileName)
					? Winterop.FileAttributes.Normal
					: (Winterop.FileAttributes)File.GetAttributes(sourceFileName);

				// Check whether the remote target is a directory
				bool isDestDir = false;
				bool fileExists = false;
				try
				{
					var file = await this.CreateFileAsync(destinationFileName, new Smb2CreateInfo
					{
						OplockLevel = Smb2OplockLevel.None,
						ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
						DesiredAccess = (uint)Smb2FileAccessRights.ReadAttributes,
						FileAttributes = 0,
						ShareAccess = Smb2ShareAccess.ReadWriteDelete,
						CreateDisposition = Smb2CreateDisposition.Open,
						CreateOptions = Smb2FileCreateOptions.OpenReparsePoint,
						RequestMaximalAccess = true,
						QueryOnDiskId = true
					}, FileAccess.Read, cancellationToken).ConfigureAwait(false);

					await using (file)
					{
						isDestDir = file.IsDirectory;
						if (isDestDir)
						{
							string sourceFilePart = Path.GetFileName(sourceFileName);
							destinationFileName = destinationFileName.Append(Path.GetFileName(sourceFilePart));
						}
						else
						{
							fileExists = true;
						}
					}
				}
				catch { }

				// Check whether remote target exists
				// If the user-provided name is a directory, the previous step appended the path name
				if (!fileExists && isDestDir)
				{
					try
					{
						var file = await CreateFileAsync(destinationFileName, new Smb2CreateInfo
						{
							OplockLevel = Smb2OplockLevel.None,
							ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
							DesiredAccess = (uint)Smb2FileAccessRights.ReadAttributes,
							FileAttributes = 0,
							ShareAccess = Smb2ShareAccess.ReadWriteDelete,
							CreateDisposition = Smb2CreateDisposition.Open,
							CreateOptions = Smb2FileCreateOptions.OpenReparsePoint,
							RequestMaximalAccess = true,
							QueryOnDiskId = true
						}, FileAccess.Read, cancellationToken).ConfigureAwait(false);
						await using (file)
						{
							if (file.IsDirectory)
								throw new IOException($"The target path `{destinationFileName}' is a directory, not a file.");

							fileExists = true;
						}
					}
					catch
					{
						// More of a courtesy, don't report error on this step
					}
				}

				if (fileExists && !overwrite)
					throw new IOException($"The file `{destinationFileName}' already exists.");

				{
					var file = await CreateFileAsync(destinationFileName, attrs, cancellationToken).ConfigureAwait(false);
					var createAction = file.CreateAction;
					await using (file)
					{
						if (sourceStream.CanSeek)
						{
							await file.SetLengthAsync(sourceStream.Length, cancellationToken).ConfigureAwait(false);
						}

						var destStream = file.GetStream(false);
						await using (destStream)
						{
							await sourceStream.CopyToAsync2(destStream, chunkSize, cancellationToken).ConfigureAwait(false);
						}

						DateTime dateTime = File.GetLastWriteTimeUtc(sourceFileName);
						await file.SetBasicInfoAsync(
							null,
							dateTime,
							dateTime,
							(Winterop.FileAttributes)File.GetAttributes(sourceFileName),
							cancellationToken
							).ConfigureAwait(false);
					}

					if (isDestDir)
						createAction |= Smb2CreateAction.IsDirectory;

					return createAction;
				}
			}
		}
	}

	partial class Smb2Client : ISmb2ConnectionOwner
	{
		void ISmb2ConnectionOwner.OnConnectionAborted(Smb2Connection smb2Connection, Exception exception)
		{
		}
	}

	partial class Smb2Client : IAsyncDisposable, IDisposable
	{
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~Smb2Client()
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

		public async ValueTask DisposeAsync()
		{
			if (!this.disposedValue)
			{
				foreach (var share in this._shares.Values)
				{
					await share.DisposeAsync().ConfigureAwait(false);
				}
				foreach (var session in this._sessions.Values)
				{
					await session.DisposeAsync().ConfigureAwait(false);
				}
				foreach (var connGroup in this._connections.Values)
				{
					foreach (var conn2 in connGroup.connections)
					{
						await conn2.DisposeAsync().ConfigureAwait(false);
					}
				}

				this.disposedValue = true;
			}
		}
	}

	class ConnectionGroup
	{
		internal readonly List<Smb2Connection> connections;
		internal int selectIndex;

		internal ConnectionGroup(Smb2Connection connection)
		{
			this.connections = new List<Smb2Connection>() { connection };
		}

		internal Smb2Connection SelectConnection()
		{
			return this.connections[this.selectIndex++ % this.connections.Count];
		}
	}

}
