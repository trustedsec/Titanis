using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Smb2.Pdus;
using Titanis.Winterop;

namespace Titanis.Smb2
{
	/// <summary>
	/// Common base class for file-like objects that contain data.
	/// </summary>
	public class Smb2OpenFileBase : Smb2OpenFileObjectBase
	{
		internal Smb2OpenFileBase(
			Smb2TreeConnect tree,
			string shareRelativePath,
			in Smb2FileOpenInfo fileInfo)
			: base(tree, shareRelativePath, fileInfo)
		{
		}
		/// <summary>
		/// Reads bytes from the file.
		/// </summary>
		/// <param name="startOffset">Offset at which to begin reading</param>
		/// <param name="buffer">Buffer to contain the bytes read</param>
		/// <param name="minCount">Minimum number of bytes to read before returning</param>
		/// <param name="options"><see cref="Smb2ReadOptions"/> value specifying options</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>Number of bytes read into <paramref name="buffer"/></returns>
		public async Task<int> ReadAsync(
			long startOffset,
			Memory<byte> buffer,
			int minCount,
			Smb2ReadOptions options,
			CancellationToken cancellationToken
			)
		{
			Smb2ReadRequest req = new Smb2ReadRequest()
			{
				body = new Smb2ReadRequestBody
				{
					options = options & this.Tree.Session.Connection.AllowedReadOptions,
					length = buffer.Length,
					offset = startOffset,
					handle = this.Handle,
					minCount = minCount,
					// TODO: Add channel support
				},
				receiveBuffer = buffer
			};

			var resp = (Pdus.Smb2ReadResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);
			return resp.body.dataLength;
		}

		/// <summary>
		/// Writes bytes to the file.
		/// </summary>
		/// <param name="startOffset">Offset at which to begin writing</param>
		/// <param name="buffer">Buffer containing the bytes to write</param>
		/// <param name="options"><see cref="Smb2WriteOptions"/> value specifying options</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>Number of bytes written to the file</returns>
		public async Task<int> WriteAsync(
			long startOffset,
			ReadOnlyMemory<byte> buffer,
			Smb2WriteOptions options,
			CancellationToken cancellationToken
			)
		{
			Smb2WriteRequest req = new Smb2WriteRequest()
			{
				body = new Smb2WriteRequestBody
				{
					writeOffset = (ulong)startOffset,
					fileHandle = this.Handle,
					flags = options
				},
				buffer = buffer
			};

			var resp = (Pdus.Smb2WriteResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);
			return (int)resp.body.count;
		}
		/// <summary>
		/// Requests the server to flush all cached file info to persistent storage.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task FlushAsync(CancellationToken cancellationToken)
		{
			Smb2FlushRequest req = new Smb2FlushRequest
			{
				body = new Smb2FlushRequestBody
				{
					handle = this.Handle
				}
			};

			var resp = (Smb2FlushResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);
		}
	}
}