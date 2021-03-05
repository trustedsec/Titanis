using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Security;

namespace Titanis.Smb2
{
	public abstract class Smb2FileStreamBase : Stream, ISecureChannel
	{
		private readonly Smb2OpenFileBase _file;
		private readonly bool _ownsFile;
		private readonly FileAccess _access;

		protected internal Smb2FileStreamBase(Smb2OpenFileBase file, FileAccess access, bool ownsFile)
		{
			this._file = file;
			this._ownsFile = ownsFile;
			this._access = access;
		}

		/// <summary>
		/// Gets or sets a <see cref="Smb2ReadOptions"/> value that specifies read options.
		/// </summary>
		public Smb2ReadOptions ReadOptions { get; set; }

		/// <inheritdoc/>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._ownsFile)
				{
					this._file.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <inheritdoc/>
		public override async ValueTask DisposeAsync()
		{
			if (this._ownsFile)
			{
				await this._file.DisposeAsync().ConfigureAwait(false);
			}
			await base.DisposeAsync().ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public sealed override bool CanRead => 0 != (this._access & FileAccess.Read);
		/// <inheritdoc/>
		public sealed override bool CanWrite => 0 != (this._access & FileAccess.Write);

		/// <inheritdoc/>
		public sealed override void Flush()
		{
			this._file.FlushAsync(CancellationToken.None).Wait();
		}

		/// <inheritdoc/>
		public sealed override Task FlushAsync(CancellationToken cancellationToken)
			=> this._file.FlushAsync(cancellationToken);

		/// <inheritdoc/>
		public sealed override int Read(byte[] buffer, int offset, int count)
			=> this.ReadAsync(buffer, offset, count).Result;

		private long PosValue => (this.CanSeek ? this.Position : 0);

		/// <inheritdoc/>
		public sealed override async Task<int> ReadAsync(
			byte[] buffer,
			int offset,
			int count,
			CancellationToken cancellationToken)
		{
			if (!this.CanRead)
				throw new NotSupportedException("The stream does not support reading.");

			try
			{
				int cbRead = await this._file.ReadAsync(
					this.PosValue,
					new Memory<byte>(buffer, offset, count),
					1,
					this.ReadOptions,
					cancellationToken).ConfigureAwait(false);
				if (this.CanSeek)
					this.Position += cbRead;

				return cbRead;
			}
			catch (EndOfStreamException)
			{
				return 0;
			}
		}

		/// <inheritdoc/>
		public sealed override void Write(byte[] buffer, int offset, int count)
			// TODO: Check that all data was written
			=> this.WriteAsync(buffer, offset, count).Wait();

		/// <inheritdoc/>
		public sealed override async Task WriteAsync(
			byte[] buffer,
			int offset,
			int count,
			CancellationToken cancellationToken)
		{
			if (!this.CanRead)
				throw new NotSupportedException("The stream does not support writing.");

			int cbWrite = await this._file.WriteAsync(
				this.PosValue,
				new Memory<byte>(buffer, offset, count),
				Smb2WriteOptions.None,
				cancellationToken).ConfigureAwait(false);
			if (this.CanSeek)
				this.Position += cbWrite;
		}

		#region ISecureChannel
		private Smb2Session SmbSession => this._file.Tree.Session;

		AuthLevel ISecureChannel.AuthLevel
			=> (this.SmbSession.SigningRequired) ? AuthLevel.Integrity : AuthLevel.Authenticated;

		bool ISecureChannel.HasSessionKey => (this.SmbSession.GetSessionKey() != null);
		byte[] ISecureChannel.GetSessionKey()
		{
			return this.SmbSession.GetSessionKey();
		}
		#endregion
	}
}