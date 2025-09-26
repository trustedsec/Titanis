using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Smb2.Pdus;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Smb2
{
	/// <summary>
	/// Represents an open file or directory.
	/// </summary>
	/// <remarks>
	/// This is the base class for SMB objects with a file handle.
	/// </remarks>
	public partial class Smb2OpenFileObjectBase
	{
		internal Smb2OpenFileObjectBase(
			Smb2TreeConnect tree,
			string shareRelativePath,
			in Smb2FileOpenInfo fileInfo)
		{
			this.Tree = tree;
			this.ShareRelativePath = shareRelativePath;
			this._info = fileInfo;
		}

		private Smb2FileOpenInfo _info;

		/// <summary>
		/// Gets the tree containing this object.
		/// </summary>
		public Smb2TreeConnect Tree { get; }

		public const int DefaultReparseInfoBufferSize = 13686;

		/// <summary>
		/// Gets the path of this object relative to the share containing it.
		/// </summary>
		public string ShareRelativePath { get; }
		/// <summary>
		/// Gets the file handle.
		/// </summary>
		internal Smb2FileHandle Handle => this._info.fileId;

		/// <summary>
		/// Gets a value indicating whether this object is a directory.
		/// </summary>
		/// <remarks>
		/// </remarks>
		public bool IsDirectory => (0 != (this.FileAttributes & Winterop.FileAttributes.Directory));
		/// <summary>
		/// Gets the level of oplock held on this object.
		/// </summary>
		public Smb2OplockLevel OplockLevel => this._info.oplockLevel;
		internal Smb2CreateResponseFlags Flags => this._info.flags;
		public Smb2CreateAction CreateAction => this._info.createAction;

		public DateTime CreationTime => DateTime.FromFileTimeUtc(this._info.attrs.creationTime);
		public DateTime LastAccessTime => DateTime.FromFileTimeUtc(this._info.attrs.lastAccessTime);
		public DateTime LastWriteTime => DateTime.FromFileTimeUtc(this._info.attrs.lastWriteTime);
		public DateTime ChangeTime => DateTime.FromFileTimeUtc(this._info.attrs.changeTime);
		public long AllocationSize => this._info.attrs.allocationSize;
		public long Length => this._info.attrs.endOfFile;

		public Winterop.FileAttributes FileAttributes => this._info.attrs.fileAttributes;

		private const int DefaultMaxResponseSize = 1024;

		/// <summary>
		/// Refreshes file size information.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task RefreshFileSizeAsync(CancellationToken cancellationToken)
		{
			byte[] outputBuffer = new byte[DefaultMaxResponseSize];
			var req = new Smb2QueryInfoRequest(this.Handle, Smb2QueryFileInfo.Generic(Smb2FileInfoType.File,
FileInfoClass.NetworkOpenInfo), DefaultMaxResponseSize)
			{ outputBuffer = outputBuffer };
			var resp = (Pdus.Smb2QueryInfoResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);

			if (resp.outputBuffer.Length >= 8)
			{
				var reader = new ByteMemoryReader(resp.outputBuffer);
				var dirInfo = reader.ReadFileNetOpenInfo();

				ref var fileInfo = ref this._info.attrs;
				fileInfo.creationTime = dirInfo.creationTime;
				fileInfo.lastAccessTime = dirInfo.lastAccessTime;
				fileInfo.lastWriteTime = dirInfo.lastWriteTime;
				fileInfo.changeTime = dirInfo.changeTime;
				fileInfo.allocationSize = (long)dirInfo.allocationSize;
				fileInfo.endOfFile = (long)dirInfo.endOfFile;
				fileInfo.fileAttributes = dirInfo.fileAttributes;
			}
		}


		public bool HasDurableHandle { get; internal set; }
		public Smb2FileAccessRights MaximalAccessAllowed { get; internal set; }
		public ulong FileId { get; internal set; }
		public ulong VolumeId { get; internal set; }

		public struct Smb2IoctlResult
		{
			public uint ctlCode;
			public int inputResponseSize;
			public int outputResponseSize;
		}
		/// <summary>
		/// Requests a FSCTL operation on the file.
		/// </summary>
		/// <param name="ctlCode"></param>
		/// <param name="inputBuffer"></param>
		/// <param name="outputBuffer"></param>
		/// <param name="inputResponseBuffer"></param>
		/// <param name="outputResponseBuffer"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Smb2IoctlResult> FsctlAsync(
			uint ctlCode,
			ReadOnlyMemory<byte> inputBuffer,
			ReadOnlyMemory<byte> outputBuffer,
			Memory<byte> inputResponseBuffer,
			Memory<byte> outputResponseBuffer,
			CancellationToken cancellationToken
			)
		{
			var req = new Smb2IoctlRequest()
			{
				inputBuffer = inputBuffer,
				outputBuffer = outputBuffer,
				inputResponseBuffer = inputResponseBuffer,
				outputResponseBuffer = outputResponseBuffer,
				body = new Smb2IoctlRequestBody
				{
					ctlCode = ctlCode,
					maxInputResponse = inputResponseBuffer.Length,
					maxOutputResponse = outputResponseBuffer.Length,
					fileHandle = this.Handle,
					flags = Smb2IoctlOptions.IsFsctl
				},
			};

			var resp = (Pdus.Smb2IoctlResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);
			return new Smb2IoctlResult
			{
				ctlCode = resp.body.ctlCode,
				inputResponseSize = resp.InputResponseSize,
				outputResponseSize = resp.OutputResponseSize,
			};
		}

		/// <summary>
		/// Gets the size of the extended attribute information for the file.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>The size of the extended attribute information, in bytes.</returns>
		public Task ReadExtendedAttributesAsync(CancellationToken cancellationToken)
			=> this.ReadExtendedAttributeSizeAsync(16384, cancellationToken);
		/// <summary>
		/// Gets the size of the extended attribute information for the file.
		/// </summary>
		/// <param name="maxResponseSize">Max size of the response, in bytes</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>The size of the extended attribute information, in bytes.</returns>
		public async Task<int> ReadExtendedAttributeSizeAsync(int maxResponseSize, CancellationToken cancellationToken)
		{
			var req = new Smb2QueryInfoRequest(this.Handle, Smb2QueryFileInfo.Generic(Smb2FileInfoType.File, FileInfoClass.FullEaInfo), maxResponseSize);
			var resp = (Pdus.Smb2QueryInfoResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);

			if (resp.outputBuffer.Length >= 4)
			{
				return BinaryPrimitives.ReadInt32LittleEndian(resp.outputBuffer.Span);
			}
			return 0;
		}

		public async Task<SecurityDescriptor?> GetSecurityAsync(
			SecurityInfo securityInfo,
			int maxResponseSize,
			CancellationToken cancellationToken)
		{
			byte[] buffer = new byte[maxResponseSize];
			var req = new Smb2QueryInfoRequest(this.Handle, Smb2QueryFileInfo.Generic(Smb2FileInfoType.Security, 0), maxResponseSize)
			{
				Additional = securityInfo,
				outputBuffer = buffer
			};
			var resp = (Pdus.Smb2QueryInfoResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);

			if (resp.outputBuffer.Length >= 4)
			{
				SecurityDescriptor sd = new SecurityDescriptor(resp.outputBuffer.Span);
				return sd;
			}
			return null;
		}

		public const int DefaultStreamInfoSize = 16384;
		/// <summary>
		/// Gets a list of data streams in the file.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>An array of <see cref="FileStreamInfo"/> describing the data streams</returns>
		public Task<FileStreamInfo[]> GetStreamsInfoAsync(
			CancellationToken cancellationToken)
			=> this.GetStreamsInfoAsync(DefaultStreamInfoSize, cancellationToken);
		/// <summary>
		/// Gets a list of data streams in the file.
		/// </summary>
		/// <param name="maxResponseSize">Size of response buffer to allocate</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>An array of <see cref="FileStreamInfo"/> describing the data streams</returns>
		public async Task<FileStreamInfo[]> GetStreamsInfoAsync(
			int maxResponseSize,
			CancellationToken cancellationToken)
		{
			byte[] buffer = new byte[maxResponseSize];
			var req = new Smb2QueryInfoRequest(this.Handle, Smb2QueryFileInfo.Generic(Smb2FileInfoType.File, FileInfoClass.FileStreamInfo), maxResponseSize)
			{
				outputBuffer = buffer
			};
			var resp = (Pdus.Smb2QueryInfoResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);

			return ReadStreamsInfo(resp.outputBuffer.Span);
		}

		private static FileStreamInfo[] ReadStreamsInfo(ReadOnlySpan<byte> buffer)
		{
			List<FileStreamInfo> streams = new List<FileStreamInfo>();

			int offset = 0;
			while ((offset + 24) < buffer.Length)
			{
				int cbName = BinaryPrimitives.ReadInt32LittleEndian(buffer.Slice(offset + 4, 4));
				string name = Encoding.Unicode.GetString(buffer.Slice(offset + 24, cbName));
				streams.Add(new FileStreamInfo(
					name,
					BinaryPrimitives.ReadInt64LittleEndian(buffer.Slice(offset + 8, 8)),
					BinaryPrimitives.ReadInt64LittleEndian(buffer.Slice(offset + 16, 8))
					));

				int offNext = BinaryPrimitives.ReadInt32LittleEndian(buffer.Slice(offset, 4));
				if (offNext == 0)
					break;
				offset += offNext;
			}

			return streams.ToArray();
		}

		/// <summary>
		/// Gets the reparse info for the file.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token that may be used to cancel the operation.</param>
		/// <returns>The <see cref="ReparseInfo"/> describing the reparse point.</returns>
		public Task<ReparseInfo> GetReparseInfoAsync(CancellationToken cancellationToken)
			=> this.GetReparseInfoAsync(DefaultReparseInfoBufferSize, cancellationToken);
		/// <summary>
		/// Gets the reparse info for the file.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token that may be used to cancel the operation.</param>
		/// <returns>The <see cref="ReparseInfo"/> describing the reparse point.</returns>
		public async Task<ReparseInfo> GetReparseInfoAsync(int bufferSize, CancellationToken cancellationToken)
		{
			byte[] outputBuffer = ArrayPool<byte>.Shared.Rent(bufferSize);
			try
			{
				var result = await this.FsctlAsync(
					(uint)Smb2FsctlCode.GetReparsePoint,
					default, default,
					default, outputBuffer.AsMemory().Slice(0, bufferSize),
					cancellationToken).ConfigureAwait(false);
				return ReparseInfo.Parse(outputBuffer.Slice(0, result.outputResponseSize));
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(outputBuffer);
			}
		}

		/// <summary>
		/// Sets the allocation size of the file.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task SetBasicInfoAsync(
			DateTime? createTime,
			DateTime? lastWriteTime,
			DateTime? lastChangeTime,
			Winterop.FileAttributes attributes,
			CancellationToken cancellationToken
			)
		{
			var req = new Smb2SetInfoRequest(this.Handle, new FileBasicInfo(
				createTime,
				null,
				lastWriteTime,
				lastChangeTime,
				attributes));

			var resp = (Pdus.Smb2SetInfoResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets the Basic file information of the file
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task<FileBasicInfo> GetBasicInfoAsync( CancellationToken cancellationToken )
		{
			var buffer = new byte[FileBasicInfoStruct.StructSize];
			var req = new Smb2QueryInfoRequest(this.Handle, Smb2QueryFileInfo.Generic(Smb2FileInfoType.File, FileInfoClass.BasicInfo), DefaultMaxResponseSize)
			{
				outputBuffer = buffer
			};
			var response = (Pdus.Smb2QueryInfoResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken ).ConfigureAwait(false);
			var res = new ByteMemoryReader(response.outputBuffer);
			var basicInfo = new FileBasicInfo(res.ReadFileBasicInfo());
			return basicInfo;
		}

		/// <summary>
		/// Sets the symbolic link info.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task SetSymlinkInfoAsync(
			SymbolicLinkFlags flags,
			string substitutePath,
			string printPath,
			CancellationToken cancellationToken
			)
		{
			await this.SetReparseInfoAsync(new SymbolicLinkInfo(flags, substitutePath, printPath), cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Sets the volume mount point info.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task SetVolumeMountPointInfoAsync(
			string substitutePath,
			string printPath,
			CancellationToken cancellationToken
			)
		{
			await this.SetReparseInfoAsync(new MountPointInfo(substitutePath, printPath), cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Sets the reparse info for the file.
		/// </summary>
		/// <param name="reparseInfo">Reparse info to set</param>
		/// <param name="cancellationToken">A cancellation token that may be used to cancel the operation.</param>
		/// <returns>The <see cref="ReparseInfo"/> describing the reparse point.</returns>
		public async Task SetReparseInfoAsync(ReparseInfo reparseInfo, CancellationToken cancellationToken)
		{
			if (reparseInfo is null) throw new ArgumentNullException(nameof(reparseInfo));
			var reparseBytes = reparseInfo.ToByteArray();
			var result = await this.FsctlAsync(
				(uint)Smb2FsctlCode.SetReparsePoint,
				reparseBytes, default,
				default, default,
				cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets the snapshot info for the file.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token that may be used to cancel the operation.</param>
		/// <returns>The <see cref="FileSnapshotsInfo"/> describing the available snapshots.</returns>
		public Task<FileSnapshotsInfo> GetSnapshotInfoAsync(CancellationToken cancellationToken)
			=> this.GetSnapshotInfoAsync(2048, cancellationToken);
		/// <summary>
		/// Gets the snapshot info for the file.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token that may be used to cancel the operation.</param>
		/// <returns>The <see cref="FileSnapshotsInfo"/> describing the available snapshots.</returns>
		public async Task<FileSnapshotsInfo> GetSnapshotInfoAsync(int bufferSize, CancellationToken cancellationToken)
		{
			byte[] outputBuffer = ArrayPool<byte>.Shared.Rent(bufferSize);
			try
			{
				var result = await this.FsctlAsync(
					(uint)Smb2FsctlCode.SrvEnumerateSnapshots,
					default, default,
					default, outputBuffer.AsMemory().Slice(0, bufferSize),
					cancellationToken).ConfigureAwait(false);

				return ReadSnapshotInfo(outputBuffer.Slice(0, result.outputResponseSize));
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(outputBuffer);
			}
		}


		/// <summary>
		/// Gets a resume key for the file.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token that may be used to cancel the operation.</param>
		/// <returns>A resume key that may be used for server-side operations.</returns>
		public Task<ResumeKey> GetResumeKeyAsync(CancellationToken cancellationToken)
			=> this.GetResumeKeyAsync(32, cancellationToken);
		/// <summary>
		/// Gets the snapshot info for the file.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token that may be used to cancel the operation.</param>
		/// <returns>The <see cref="FileSnapshotsInfo"/> describing the available snapshots.</returns>
		public async Task<ResumeKey> GetResumeKeyAsync(int bufferSize, CancellationToken cancellationToken)
		{
			byte[] outputBuffer = ArrayPool<byte>.Shared.Rent(bufferSize);
			try
			{
				var result = await this.FsctlAsync(
					(uint)Smb2FsctlCode.SrvRequestResumeKey,
					default, default,
					default, outputBuffer.AsMemory().Slice(0, bufferSize),
					cancellationToken).ConfigureAwait(false);

				return new ResumeKey(ref MemoryMarshal.AsRef<ResumeKey.ResumeKeyData>(outputBuffer.Slice(0, ResumeKey.KeySize)));
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(outputBuffer);
			}
		}

		public async Task CopyChunkFromAsync(ResumeKey resumeKey, CopyChunk[] chunks, CancellationToken cancellationToken)
		{
			if (resumeKey is null) throw new ArgumentNullException(nameof(resumeKey));
			if (chunks is null || chunks.Length == 0) throw new ArgumentNullException(nameof(chunks));

			int cbInput = ResumeKey.KeySize + 4 + 4 + CopyChunk.StructSize * chunks.Length;
			byte[] inputBuffer = new byte[cbInput];

			MemoryMarshal.AsRef<ResumeKey.ResumeKeyData>(inputBuffer) = resumeKey.keyData;
			BinaryPrimitives.WriteInt32LittleEndian(inputBuffer.Slice(24, 4), chunks.Length);
			for (int i = 0; i < chunks.Length; i++)
			{
				MemoryMarshal.AsRef<ResumeKey.ResumeKeyData>(inputBuffer.Slice(32 + i * CopyChunk.StructSize)) = resumeKey.keyData;
			}

			const int bufferSize = 32;

			byte[] outputBuffer = ArrayPool<byte>.Shared.Rent(bufferSize);
			try
			{
				var result = await this.FsctlAsync(
					(uint)Smb2FsctlCode.SrvCopyChunk,
					inputBuffer, default,
					default, outputBuffer.AsMemory().Slice(0, bufferSize),
					cancellationToken).ConfigureAwait(false);


			}
			finally
			{
				ArrayPool<byte>.Shared.Return(outputBuffer);
			}
		}

		private static FileSnapshotsInfo ReadSnapshotInfo(ReadOnlySpan<byte> bytes)
		{
			int cSnapshots = BinaryPrimitives.ReadInt32LittleEndian(bytes.Slice(0, 4));
			int cSnapshotsReturned = BinaryPrimitives.ReadInt32LittleEndian(bytes.Slice(4, 4));
			int cbArray = BinaryPrimitives.ReadInt32LittleEndian(bytes.Slice(8, 4));

			var tokenArray = MemoryMarshal.Cast<byte, ushort>(bytes.Slice(12, cbArray));
			List<FileSnapshotInfo> tokens = new List<FileSnapshotInfo>(cSnapshotsReturned);
			StringBuilder sb = new StringBuilder(24);
			for (int i = 0; i <= tokenArray.Length; i++)
			{
				var c = (i < tokenArray.Length) ? (char)tokenArray[i] : '\0';
				if (c == 0)
				{
					if (sb.Length > 0)
					{
						var token = sb.ToString();

						// @GMT-YYYY.MM.DD-HH.MM.SS
						try
						{
							tokens.Add(FileSnapshotInfo.Parse(token));
						}
						catch { }

						sb.Clear();
					}
				}
				else
				{
					sb.Append(c);
				}
			}

			return new FileSnapshotsInfo(cSnapshots, tokens.ToArray());
		}

		/// <summary>
		/// Clears reparse information.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task DeleteReparseInfoAsync(
			ReparseTag tag,
			CancellationToken cancellationToken
			)
		{
			byte[] bytes = new byte[8];
			BinaryPrimitives.WriteUInt32LittleEndian(bytes, (uint)tag);
			var result = await this.FsctlAsync(
				(uint)Smb2FsctlCode.DeleteReparsePoint,
				bytes, default,
				default, default,
				cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Clears reparse information for a mount point.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public Task DeleteMountPointInfoAsync(
			CancellationToken cancellationToken
			)
			=> this.DeleteReparseInfoAsync(ReparseTag.MountPoint, cancellationToken);

		/// <summary>
		/// Clears reparse information for a symbolic link.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public Task DeleteSymbolicLinktInfoAsync(
			CancellationToken cancellationToken
			)
			=> this.DeleteReparseInfoAsync(ReparseTag.SymbolicLink, cancellationToken);

		public Task CloseAsync(CancellationToken cancellationToken)
			=> CloseAsync(Smb2CloseOptions.None, cancellationToken);

		public async Task<Smb2OpenFileAttributes> CloseAsync(
			Smb2CloseOptions options,
			CancellationToken cancellationToken)
		{
			Smb2CloseRequest req = new Smb2CloseRequest
			{
				body = new Smb2CloseRequestBody
				{
					flags = options,
					handle = this.Handle
				}
			};

			var resp = (Smb2CloseResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);

			this.Tree.OnFileClosed(this);

			return resp.body.attrs;
		}

		public bool IsClosed { get; private set; }

	}

	partial class Smb2OpenFileObjectBase : IDisposable, IAsyncDisposable
	{
		#region Dispose pattern
		private bool _isDisposed;

		protected virtual void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				if (disposing)
				{
					if (this.Handle != Smb2FileHandle.Invalid != !this.IsClosed)
						this.CloseAsync(CancellationToken.None);
				}

				_isDisposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public async ValueTask DisposeAsync()
		{
			if (!this._isDisposed)
			{
				await this.CloseAsync(CancellationToken.None).ConfigureAwait(false);
				this._isDisposed = true;
			}
		}
		#endregion

	}
}