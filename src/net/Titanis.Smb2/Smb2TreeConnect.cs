using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Smb2.Pdus;
using Titanis.Winterop;

namespace Titanis.Smb2
{
	/// <summary>
	/// Represents an SMB2 tree-connect.
	/// </summary>
	/// <remarks>
	/// A tree connect represents a connection to a share.
	/// </remarks>
	/// <seealso cref="Smb2Session.OpenTreeAsync(UncPath, bool, CancellationToken)"/>
	public class Smb2TreeConnect : IDisposable, IAsyncDisposable
	{
		internal Smb2TreeConnect(
			string shareName,
			Smb2Session session,
			uint treeId,
			in Pdus.Smb2ShareInfo shareInfo,
			bool mustEncryptData
			)
		{
			this.ShareName = shareName;
			this.Session = session;
			this.TreeId = treeId;
			this._shareInfo = shareInfo;
			this._mustEncryptData = mustEncryptData;
		}

		/// <summary>
		/// Gets the name of this share.
		/// </summary>
		public string ShareName { get; }

		/// <summary>
		/// Gets the session owning the tree-connect.
		/// </summary>
		public Smb2Session Session { get; }
		/// <summary>
		/// Gets the ID identifying the tree-connect within the session.
		/// </summary>
		public uint TreeId { get; }

		private readonly Pdus.Smb2ShareInfo _shareInfo;
		private readonly bool _mustEncryptData;

		/// <summary>
		/// Gets a <see cref="Smb2ShareType"/> value specifying the type of share.
		/// </summary>
		public Smb2ShareType ShareType => this._shareInfo.shareType;
		/// <summary>
		/// Gets a <see cref="Smb2ShareFlags"/> value specifying the behavior of the share.
		/// </summary>
		public Smb2ShareFlags ShareFlags => this._shareInfo.flags;
		/// <summary>
		/// Gets a <see cref="Smb2ShareCaps"/> value specifying capabilities of the share.
		/// </summary>
		public Smb2ShareCaps Capabilities => this._shareInfo.caps;
		/// <summary>
		/// Gets a value indicating whether this is a DFS share.
		/// </summary>
		public bool IsDfs => 0 != (this.Capabilities & Smb2ShareCaps.Dfs);

		/// <summary>
		/// Gets a value determining whether encryption is required, either by the server or by the user.
		/// </summary>
		/// <remarks>
		/// When calling <see cref="Smb2Session.OpenTreeAsync(UncPath, bool, CancellationToken)"/>,
		/// the caller may specify that encryption is required.
		/// </remarks>
		public bool MustEncryptData => this._mustEncryptData || 0 != (this._shareInfo.flags & Smb2ShareFlags.EncryptData);

		/// <summary>
		/// Gets a value indicating whether the tree-connect will close when the last open file has been closed.
		/// </summary>
		public bool AutoClose { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether requests to create or open directories
		/// request a lease.
		/// </summary>
		public bool PrefersDirectoryLeases { get; set; } = true;

		private List<Smb2OpenFileObjectBase> _openFiles = new List<Smb2OpenFileObjectBase>();

		/// <summary>
		/// Called when a <see cref="Smb2OpenFile"/> from this tree-connect has closed.
		/// </summary>
		/// <param name="file"></param>
		internal void OnFileClosed(Smb2OpenFileObjectBase file)
		{
			lock (this._openFiles)
			{
				this._openFiles.Remove(file);
			}

			if (this.AutoClose && this._openFiles.Count == 0)
			{
				this.Dispose();
			}
		}
		/// <summary>
		/// Creates a directory.
		/// </summary>
		/// <param name="dirName">Directory path, relative to the share</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="Smb2Directory"/> representing the directory</returns>
		public Task<Smb2Directory> CreateDirectoryAsync(string dirName, CancellationToken cancellationToken)
		{
			return this.CreateFileAsync<Smb2Directory, Smb2DirFactory>(dirName, new Smb2CreateInfo
			{
				Priority = Smb2Priority.CreateDir,
				CreateDisposition = Smb2CreateDisposition.Create,
				DesiredAccess = (uint)Smb2FileAccessRights.DefaultCreateDirAccess,
				ShareAccess = Smb2ShareAccess.ReadWrite,
				FileAttributes = Winterop.FileAttributes.Normal,
				CreateOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.SynchronousIoNonalert,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			}, FileAccess.Read, cancellationToken);
		}
		/// <summary>
		/// Opens a directory.
		/// </summary>
		/// <param name="dirName">Directory path, relative to the share</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="Smb2Directory"/> representing the directory</returns>
		public Task<Smb2Directory> OpenDirectoryAsync(string dirName, CancellationToken cancellationToken)
		{
			bool lease = this.Session.Connection.SupportsDirectoryLeasing && this.PrefersDirectoryLeases;
			return this.CreateFileAsync<Smb2Directory, Smb2DirFactory>(dirName, new Smb2CreateInfo
			{
				CreateDisposition = Smb2CreateDisposition.Open,
				Priority = Smb2Priority.OpenDir,
				DesiredAccess = (uint)Smb2FileAccessRights.DefaultOpenDirAccess,
				ShareAccess = Smb2ShareAccess.DefaultDirShare,
				FileAttributes = Winterop.FileAttributes.None,
				CreateOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.SynchronousIoNonalert,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				RequestMaximalAccess = true,
				QueryOnDiskId = true,
				OplockLevel = lease ? Smb2OplockLevel.Lease : Smb2OplockLevel.None,
				LeaseInfo = lease
					? new Smb2LeaseInfo()
					{
						LeaseState = Smb2LeaseState.ReadCaching | Smb2LeaseState.HandleCaching,
						UseV2Struct = this.Session.Connection.Dialect >= Smb2Dialect.Smb3_0
					}
					: null
			},
			FileAccess.Read,
			cancellationToken);
		}
		/// <summary>
		/// Removes a directory.
		/// </summary>
		/// <param name="dirName">Directory path, relative to the share</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task RemoveDirectoryAsync(string dirName, CancellationToken cancellationToken)
		{
			bool lease = this.Session.Connection.SupportsDirectoryLeasing && this.PrefersDirectoryLeases;
			await using (var dir = (await this.CreateFileAsync<Smb2Directory, Smb2DirFactory>(dirName, new Smb2CreateInfo
			{
				CreateDisposition = Smb2CreateDisposition.Open,
				Priority = 0,
				DesiredAccess = (uint)Smb2FileAccessRights.DefaultRemoveDirAccess,
				ShareAccess = Smb2ShareAccess.DefaultDirShare,
				FileAttributes = Winterop.FileAttributes.None,
				CreateOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.SynchronousIoNonalert | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.DeleteOnClose,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				RequestMaximalAccess = true,
				QueryOnDiskId = true,
				OplockLevel = lease ? Smb2OplockLevel.Lease : Smb2OplockLevel.None,
				LeaseInfo = lease
					? new Smb2LeaseInfo()
					{
						LeaseState = Smb2LeaseState.ReadCaching | Smb2LeaseState.HandleCaching,
						UseV2Struct = this.Session.Connection.Dialect >= Smb2Dialect.Smb3_0
					}
					: null
			},
			FileAccess.Read,
			cancellationToken).ConfigureAwait(false)).ConfigureAwait(false))
			{
				;
			}
		}
		/// <summary>
		/// Deletes a file.
		/// </summary>
		/// <param name="fileName">File path, relative to the share</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task DeleteFileAsync(string fileName, CancellationToken cancellationToken)
		{
			bool lease = true;
			await using (var dir = (await this.CreateFileAsync<Smb2Directory, Smb2DirFactory>(fileName, new Smb2CreateInfo
			{
				CreateDisposition = Smb2CreateDisposition.Open,
				Priority = 0,
				DesiredAccess = (uint)Smb2FileAccessRights.DefaultDeleteFileAccess,
				ShareAccess = Smb2ShareAccess.Delete,
				FileAttributes = 0,
				CreateOptions = Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.DeleteOnClose,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				RequestMaximalAccess = true,
				QueryOnDiskId = true,
				OplockLevel = Smb2OplockLevel.Lease,
				LeaseInfo = lease
					? new Smb2LeaseInfo()
					{
						LeaseState = Smb2LeaseState.ReadCaching | Smb2LeaseState.HandleCaching,
						UseV2Struct = this.Session.Connection.Dialect >= Smb2Dialect.Smb3_0
					}
					: null
			},
			FileAccess.Read,
			cancellationToken).ConfigureAwait(false)).ConfigureAwait(false))
			{
				;
			}
		}
		/// <summary>
		/// Creates a file.
		/// </summary>
		/// <param name="fileName">File path, relative to the share</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="Smb2OpenFile"/> representing the file.</returns>
		public async Task<Smb2OpenFile> CreateFileAsync(string fileName, Winterop.FileAttributes attributes, CancellationToken cancellationToken)
		{
			return (Smb2OpenFile)await this.CreateFileAsync(fileName, new Smb2CreateInfo
			{
				CreateDisposition = Smb2CreateDisposition.Supersede,
				DesiredAccess = (uint)Smb2FileAccessRights.DefaultCreateAccess,
				ShareAccess = Smb2ShareAccess.ReadWrite,
				FileAttributes = attributes,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				CreateOptions = Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert
			}, FileAccess.ReadWrite, cancellationToken).ConfigureAwait(false);
		}
		/// <summary>
		/// Opens a file.
		/// </summary>
		/// <param name="fileName">File path, relative to the share</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="Smb2OpenFile"/> representing the file.</returns>
		public async Task<Smb2OpenFile> OpenFileReadAsync(string fileName, CancellationToken cancellationToken)
		{
			return (Smb2OpenFile)await this.CreateFileAsync(fileName, new Smb2CreateInfo
			{
				CreateDisposition = Smb2CreateDisposition.Open,
				DesiredAccess = (uint)Smb2FileAccessRights.DefaultOpenReadAccess,
				ShareAccess = Smb2ShareAccess.Read,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				CreateOptions = Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert,
				FileAttributes = Winterop.FileAttributes.Normal
			}, FileAccess.Read, cancellationToken).ConfigureAwait(false);
		}
		/// <summary>
		/// Opens a pipe.
		/// </summary>
		/// <param name="pipeName">Pipe name (relative to the share)</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="Smb2Pipe"/> representing the pipe.</returns>
		public Task<Smb2Pipe> OpenPipeAsync(string pipeName, CancellationToken cancellationToken)
		{
			return this.CreateFileAsync<Smb2Pipe, Smb2PipeFactory>(pipeName, new Smb2CreateInfo
			{
				CreateDisposition = Smb2CreateDisposition.Open,
				ShareAccess = Smb2ShareAccess.ReadWriteDelete,
				FileAttributes = 0,
				DesiredAccess = (uint)0x0012019f,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			}, FileAccess.ReadWrite, cancellationToken);
		}

		private async Task<Pdus.Smb2CreateResponse> CreateFileAsyncCore(
			string fileName,
			Smb2CreateInfo createInfo,
			CancellationToken cancellationToken)
		{
			const int MaxSymlinkDepth = 32;
			int symlinkDepth = MaxSymlinkDepth;

			if(createInfo.OplockLevel == Smb2OplockLevel.Lease)
			{
				if (!this.Session.Connection.SupportsDirectoryLeasing)
					createInfo.OplockLevel = Smb2OplockLevel.Level2;
				else
					createInfo.LeaseInfo = new Smb2LeaseInfo()
					{
						LeaseState = Smb2LeaseState.ReadCaching | Smb2LeaseState.HandleCaching,
						UseV2Struct = this.Session.Connection.Dialect >= Smb2Dialect.Smb3_0
					};
			}

			// TODO: Allow symlink depth to be configurable
			do
			{
				try
				{
					return await this.CreateFileAsyncCore_NoFollow(fileName, createInfo, cancellationToken).ConfigureAwait(false);
				}
				catch (Smb2SymlinkException ex)
				{
					var unusedPath = fileName[^(ex.SymlinkInfo.UnusedPathLength / 2)..];
					if (0 != (ex.SymlinkInfo.Flags & Winterop.SymbolicLinkFlags.RelativePath))
						fileName = Path.GetDirectoryName(fileName);

					fileName = Path.Combine(fileName, ex.SymlinkInfo.SubstituteName);
				}
			} while (--symlinkDepth > 0);

			throw new Smb2SymlinkLoopException();
		}

		private async Task<Pdus.Smb2CreateResponse> CreateFileAsyncCore_NoFollow(
			string fileName,
			Smb2CreateInfo createInfo,
			CancellationToken cancellationToken)
		{
			if (createInfo == null)
			{
				createInfo = new Smb2CreateInfo();
			}

			if (fileName == null)
				throw new ArgumentNullException(nameof(fileName));

			Pdus.Smb2CreateRequest req = new Pdus.Smb2CreateRequest(fileName, createInfo);

			var resp = (Pdus.Smb2CreateResponse)await this.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);
			return resp;
		}

		internal interface IFileFactory<out TFile>
			where TFile : Smb2OpenFileObjectBase
		{
			TFile Create(Smb2TreeConnect tree, string shareRelativePath, Smb2FileOpenInfo info, FileAccess access);
		}
		struct Smb2PipeFactory : IFileFactory<Smb2Pipe>
		{
			public Smb2Pipe Create(Smb2TreeConnect tree, string shareRelativePath, Smb2FileOpenInfo info, FileAccess access)
				=> new Smb2Pipe(tree, shareRelativePath, info, access);
		}
		struct Smb2DirFactory : IFileFactory<Smb2Directory>
		{
			public Smb2Directory Create(Smb2TreeConnect tree, string shareRelativePath, Smb2FileOpenInfo info, FileAccess access)
				=> new Smb2Directory(tree, shareRelativePath, info);
		}
		struct Smb2FileFactory : IFileFactory<Smb2OpenFile>
		{
			public Smb2OpenFile Create(Smb2TreeConnect tree, string shareRelativePath, Smb2FileOpenInfo info, FileAccess access)
				=> new Smb2OpenFile(tree, shareRelativePath, info, access);
		}
		struct Smb2FileOrDirFactory : IFileFactory<Smb2OpenFileObjectBase>
		{
			public Smb2OpenFileObjectBase Create(Smb2TreeConnect tree, string shareRelativePath, Smb2FileOpenInfo info, FileAccess access)
				=> (0 != (info.attrs.fileAttributes & Winterop.FileAttributes.Directory))
					? new Smb2Directory(tree, shareRelativePath, info)
					: new Smb2OpenFile(tree, shareRelativePath, info, access);
		}

		internal async Task<TFile> CreateFileAsync<TFile, TFileFactory>(
			string fileName,
			Smb2CreateInfo createInfo,
			FileAccess access,
			CancellationToken cancellationToken
			)
			where TFile : Smb2OpenFileObjectBase
			where TFileFactory : struct, IFileFactory<TFile>
		{
			var createResp = await this.CreateFileAsyncCore(fileName, createInfo, cancellationToken).ConfigureAwait(false);
			var openFile = new TFileFactory().Create(this, fileName, createResp.body.fileInfo, access);
			openFile.HasDurableHandle = createResp.HasDurableHandle;
			openFile.MaximalAccessAllowed = createResp.MaximalAccessAllowed;
			openFile.FileId = createResp.FileId;
			openFile.VolumeId = createResp.VolumeId;
			lock (this._openFiles)
			{
				this._openFiles.Add(openFile);
			}
			return openFile;
		}

		public Task<Smb2OpenFileObjectBase> CreateFileAsync(
			string fileName,
			Smb2CreateInfo createInfo,
			FileAccess access,
			CancellationToken cancellationToken)
			=> this.CreateFileAsync<Smb2OpenFileObjectBase, Smb2FileOrDirFactory>(fileName, createInfo, access, cancellationToken);

		internal Task<Smb2Pdu> SendSyncPduAsync(Pdus.Smb2Pdu req, CancellationToken cancellationToken)
		{
			req.pduhdr.treeId = this.TreeId;
			return this.Session.SendSyncPduAsync(req, this.MustEncryptData, cancellationToken);
		}

		/// <summary>
		/// Gets a value indicating whether this tree-connect is disconnected.
		/// </summary>
		public bool IsDisconnected { get; private set; }
		/// <summary>
		/// Disconnects the tree-connect.
		/// </summary>
		/// <remarks>
		/// This method sends a disconnect request to the server.
		/// </remarks>
		public async Task DisconnectAsync(CancellationToken cancellationToken)
		{
			if (!this.IsDisconnected)
			{
				var req = new Pdus.Smb2TreeDisconnectRequest();
				var resp = await this.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);
				// TODO: Verify response?  Can it fail?

				this.IsDisconnected = true;

				this.Session.OnTreeDisconnected(this);
			}
		}

		public const int DefaultQueryNicsBufferSize = 64 * 1024;
		/// <summary>
		/// Queries a list of network interfaces on the server.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>An array of <see cref="Smb2NicInfo"/> objects.</returns>
		public Task<Smb2NicInfo[]> QueryNetworkInterfacesAsync(CancellationToken cancellationToken)
			=> this.QueryNetworkInterfacesAsync(DefaultQueryNicsBufferSize, cancellationToken);
		public async Task<Smb2NicInfo[]> QueryNetworkInterfacesAsync(int bufferSize, CancellationToken cancellationToken)
		{
			// do not dispose, fake file
			Smb2OpenFile file = new Smb2OpenFile(this, string.Empty, new Pdus.Smb2FileOpenInfo
			{
				fileId = new Smb2FileHandle { high = 0xFFFFFFFF_FFFFFFFF, low = 0xFFFFFFFF_FFFFFFFF }
			}, FileAccess.Read);

			byte[] outputBuffer = ArrayPool<byte>.Shared.Rent(bufferSize);
			try
			{
				var res = await file.FsctlAsync(
					(uint)Smb2FsctlCode.QueryNicInfo,
					default, default,
					default, outputBuffer.AsMemory().Slice(0, bufferSize),
					cancellationToken
					).ConfigureAwait(false);

				ByteMemoryReader reader = new ByteMemoryReader(outputBuffer.AsMemory().Slice(0, res.outputResponseSize));
				var nics = reader.ReadNicInfoList();
				return nics.ToArray();
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(outputBuffer);
			}
		}



		/// <summary>
		/// Queries DFS referrals for a share.
		/// </summary>
		/// <returns>An array of <see cref="Smb2NicInfo"/> objects.</returns>
		public Task<DfsReferral?> QueryDfsReferrals(UncPath path, CancellationToken cancellationToken)
			=> this.QueryDfsReferrals(path, 4096, cancellationToken);
		/// <summary>
		/// Queries DFS referrals for a share.
		/// </summary>
		/// <returns>An array of <see cref="Smb2NicInfo"/> objects.</returns>
		public async Task<DfsReferral?> QueryDfsReferrals(UncPath path, int bufferSize, CancellationToken cancellationToken)
		{
			if (path is null) throw new ArgumentNullException(nameof(path));

			Smb2OpenFile file = new Smb2OpenFile(this, string.Empty, new Pdus.Smb2FileOpenInfo
			{
				fileId = new Smb2FileHandle { high = 0xFFFFFFFF_FFFFFFFF, low = 0xFFFFFFFF_FFFFFFFF }
			}, FileAccess.Read);

			DfsGetReferralInfoRequest req = new DfsGetReferralInfoRequest(4, path.PathForDfsReferral);
			ByteWriter writer = new ByteWriter();
			req.WriteTo(writer);

			byte[] outputBuffer = ArrayPool<byte>.Shared.Rent(bufferSize);
			try
			{
				var res = await file.FsctlAsync(
					(uint)Smb2FsctlCode.DfsGetReferrals,
					writer.GetData(), default,
					default, outputBuffer.AsMemory().Slice(0, bufferSize),
					cancellationToken
					).ConfigureAwait(false);

				ByteMemoryReader reader = new ByteMemoryReader(outputBuffer.AsMemory().Slice(0, res.outputResponseSize));
				var resp = new DfsGetReferralInfoResponse();
				resp.ReadFrom(reader);

				var infos = resp.GetInfos();
				return new DfsReferral(resp.Flags, resp.PathConsumed, infos);
			}
			catch (NtstatusException ex) when (ex.StatusCode == Ntstatus.STATUS_NOT_FOUND)
			{
				return null;
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(outputBuffer);
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
					this.OnDisposingAsync().Wait();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				_isDisposed = true;
			}
		}

		private async Task OnDisposingAsync()
		{
			if (!this.IsDisconnected)
				await this.DisconnectAsync(CancellationToken.None).ConfigureAwait(false);
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

		public async ValueTask DisposeAsync()
		{
			await this.OnDisposingAsync().ConfigureAwait(false);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}