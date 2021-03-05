using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Net
{
	/// <summary>
	/// Implements a stream over a <see cref="ISocket"/> object.
	/// </summary>
	public class SocketStream : Stream
	{
		private readonly FileAccess _access;
		private readonly bool _ownsSocket;

		public SocketStream(ISocket socket, FileAccess access, bool ownsSocket)
		{
			if (socket is null) throw new ArgumentNullException(nameof(socket));
			this.Socket = socket;
			this._access = access;
			this._ownsSocket = ownsSocket;
		}

		/// <summary>
		/// Gets the underlying <see cref="ISocket"/>.
		/// </summary>
		public ISocket Socket { get; }

		private int _disposed;
		/// <inheritdoc/>
		protected override void Dispose(bool disposing)
		{
			if (Interlocked.Exchange(ref _disposed, 1) != 1)
			{
				if (this._ownsSocket)
				{
					this.Socket.Shutdown(SocketShutdown.Both);
					this.Socket.Dispose();
				}
			}

			base.Dispose(disposing);
		}

		private void VerifyNotDisposed()
		{
			if (this._disposed != 0)
				throw new ObjectDisposedException(this.GetType().FullName);
		}

		/// <inheritdoc/>
		public override bool CanRead => 0 != (this._access & FileAccess.Read);
		/// <inheritdoc/>
		public override bool CanWrite => 0 != (this._access & FileAccess.Write);
		/// <inheritdoc/>
		public override bool CanSeek => false;
		/// <inheritdoc/>
		public override bool CanTimeout => true;
		/// <inheritdoc/>
		/// <seealso cref="ISocket.ReceiveTimeout"/>
		public override int ReadTimeout
		{
			get => this.Socket.ReceiveTimeout;
			set => this.Socket.ReceiveTimeout = value;
		}
		/// <inheritdoc/>
		/// <seealso cref="ISocket.SendTimeout"/>
		public override int WriteTimeout
		{
			get => this.Socket.SendTimeout;
			set => this.Socket.SendTimeout = value;
		}

		/// <inheritdoc/>
		public override void Flush()
		{
			// Do nothing
		}
		/// <inheritdoc/>
		public override Task FlushAsync(CancellationToken cancellationToken)
			=> Task.CompletedTask;
		/// <inheritdoc/>
		public override void SetLength(long value)
			=> throw new NotSupportedException();
		/// <inheritdoc/>
		public override long Length => throw new NotSupportedException();
		/// <inheritdoc/>
		public override long Position
		{
			get => throw new NotSupportedException();
			set => throw new NotSupportedException();
		}
		/// <inheritdoc/>
		public override long Seek(long offset, SeekOrigin origin)
			=> throw new NotSupportedException();

		#region Read
		/// <inheritdoc/>
		public override int ReadByte()
		{
			byte b = 0;
			int res = this.Read(MemoryMarshal.CreateSpan(ref b, 1));
			return (res == 0) ? -1 : b;
		}
		/// <inheritdoc/>
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.VerifyNotDisposed();
			return this.Socket.Receive(buffer.AsSpan(offset, count), SocketFlags.None);
		}
		/// <inheritdoc/>
		public override int Read(Span<byte> buffer)
		{
			this.VerifyNotDisposed();
			return this.Socket.Receive(buffer, SocketFlags.None);
		}
		/// <inheritdoc/>
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
		{
			this.VerifyNotDisposed();
			return this.Socket.ReceiveAsync(buffer, SocketFlags.None, cancellationToken);
		}
		/// <inheritdoc/>
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.VerifyNotDisposed();
			return ReadAsyncInner(buffer, offset, count, cancellationToken);
		}

		private async Task<int> ReadAsyncInner(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return await this.Socket.ReceiveAsync(buffer.AsMemory().Slice(offset, count), SocketFlags.None, cancellationToken).ConfigureAwait(false);
		}
		#endregion
		#region Write
		/// <inheritdoc/>
		public override void WriteByte(byte value)
		{
			var span = MemoryMarshal.CreateReadOnlySpan<byte>(ref value, 1);
			this.Write(span);
		}
		/// <inheritdoc/>
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.VerifyNotDisposed();

			// TODO: What happens if return value < count?
			this.Socket.Send(buffer.AsSpan(offset, count), SocketFlags.None);
		}
		/// <inheritdoc/>
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this.VerifyNotDisposed();

			// TODO: What happens if return value < count?
			this.Socket.Send(buffer, SocketFlags.None);
		}
		/// <inheritdoc/>
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.VerifyNotDisposed();

			return WriteAsyncInner(buffer, offset, count, cancellationToken);
		}
		private async Task WriteAsyncInner(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			await this.WriteAsyncInner(buffer.AsMemory(offset, count), cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
		{
			this.VerifyNotDisposed();
			return this.WriteAsyncInner(buffer, cancellationToken);
		}
		private async ValueTask WriteAsyncInner(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
		{
			await this.Socket.SendAsync(buffer, SocketFlags.None, cancellationToken).ConfigureAwait(false);
		}
		#endregion
	}
}
