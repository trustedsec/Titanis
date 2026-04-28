using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Smb2
{
	/// <summary>
	/// Provides a <see cref="Stream"/> for accessing data within an <see cref="Smb2OpenFile"/>
	/// </summary>
	/// <seealso cref="Smb2OpenFile.GetStream(bool)"/>
	public sealed class Smb2FileStream : Smb2FileStreamBase
	{
		internal Smb2FileStream(Smb2OpenFile file, FileAccess access, bool ownsFile) : base(file, access, ownsFile)
		{
			this.File = file;
		}

		public Smb2OpenFile File { get; }

		/// <inheritdoc/>
		public sealed override bool CanSeek => true;
		/// <inheritdoc/>
		public sealed override long Length => this.File.Length;
		/// <inheritdoc/>
		public sealed override long Position { get; set; }

		/// <inheritdoc/>
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
				case SeekOrigin.Begin:
					break;
				case SeekOrigin.Current:
					offset += this.Position;
					break;
				case SeekOrigin.End:
					offset = (this.Length - offset);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(origin));
			}

			return this.Position = offset;
		}

		/// <inheritdoc/>
		public sealed override void SetLength(long value)
		{
			this.File.SetLengthAsync(value, CancellationToken.None).Wait();
		}
	}
}