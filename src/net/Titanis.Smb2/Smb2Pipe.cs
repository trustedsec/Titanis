using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Smb2.Pdus;

namespace Titanis.Smb2
{
	/// <summary>
	/// Represents a named pipe opened over an SMB2 share.
	/// </summary>
	/// <seealso cref="Smb2TreeConnect.OpenPipeAsync(string, System.Threading.CancellationToken)"/>
	public sealed class Smb2Pipe : Smb2OpenFileBase
	{
		private readonly FileAccess _access;

		internal Smb2Pipe(
			Smb2TreeConnect tree,
			string shareRelativePath,
			in Smb2FileOpenInfo fileInfo,
			FileAccess access
			) : base(tree, shareRelativePath, fileInfo)
		{
			this._access = access;
		}

		/// <summary>
		/// Gets a stream to access the data within the pipe.
		/// </summary>
		/// <param name="ownsFile"><c>true</c> to transfer ownership to the stream</param>
		/// <returns>A <see cref="Smb2PipeStream"/> allowing access to the data within the pipe.</returns>
		/// <remarks>
		/// If <paramref name="ownsFile"/> is <c>true</c>, then calling <see cref="System.IO.Stream.Close"/>
		/// on the returned object will close this instance.
		/// </remarks>
		public Smb2PipeStream GetStream(bool ownsFile)
		{
			return new Smb2PipeStream(this, this._access, ownsFile);
		}

		public async Task<int> TransceiveAsync(
			ReadOnlyMemory<byte> sendBuffer,
			Memory<byte> receiveBuffer,
			CancellationToken cancellationToken)
		{
			return (await this.FsctlAsync(
				(uint)Smb2FsctlCode.Transceive,
				sendBuffer, default,
				default, receiveBuffer,
				cancellationToken).ConfigureAwait(false)).outputResponseSize;
		}
	}
}