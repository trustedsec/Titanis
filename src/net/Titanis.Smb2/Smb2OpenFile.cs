using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Smb2.Pdus;
using Titanis.Winterop;

namespace Titanis.Smb2
{
	/// <summary>
	/// Represents a file open over an SMB2 share.
	/// </summary>
	/// <seealso cref="Smb2TreeConnect.OpenFileReadAsync(string, System.Threading.CancellationToken)"/>
	public sealed class Smb2OpenFile : Smb2OpenFileBase
	{
		private readonly FileAccess _access;

		internal Smb2OpenFile(
			Smb2TreeConnect tree,
			string shareRelativePath,
			in Smb2FileOpenInfo fileInfo,
			FileAccess access
			)
			: base(tree, shareRelativePath, fileInfo)
		{
			this._access = access;
		}

		/// <summary>
		/// Gets a stream to access the data within the file.
		/// </summary>
		/// <param name="ownsFile"><c>true</c> to transfer ownership to the stream</param>
		/// <returns>A <see cref="Smb2FileStream"/> allowing access to the data within the file.</returns>
		/// <remarks>
		/// If <paramref name="ownsFile"/> is <c>true</c>, then calling <see cref="System.IO.Stream.Close"/>
		/// on the returned object will close this instance.
		/// </remarks>
		public Smb2FileStream GetStream(bool ownsFile)
		{
			return new Smb2FileStream(this, this._access, ownsFile);
		}

		/// <summary>
		/// Sets the length of the file.
		/// </summary>
		/// <param name="length">New length of the file</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task SetLengthAsync(
			long length,
			CancellationToken cancellationToken
			)
		{
			var req = new Smb2SetInfoRequest(this.Handle, new EndOfFileInfo(length));

			var resp = (Pdus.Smb2SetInfoResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Sets the allocation size of the file.
		/// </summary>
		/// <param name="size">New allocation size of the file</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		public async Task SetAllocationSizeAsync(
			long size,
			CancellationToken cancellationToken
			)
		{
			var req = new Smb2SetInfoRequest(this.Handle, new FileAllocInfo(size));

			var resp = (Pdus.Smb2SetInfoResponse)await this.Tree.SendSyncPduAsync(req, cancellationToken).ConfigureAwait(false);
		}
	}
}