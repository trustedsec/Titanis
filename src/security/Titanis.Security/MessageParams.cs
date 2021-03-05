using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Security
{
	/// <summary>
	/// Specifies message security options.
	/// </summary>
	public enum MessageSecBufferOptions
	{
		/// <summary>
		/// None
		/// </summary>
		None = 0,
		/// <summary>
		/// Integrity is requested
		/// </summary>
		Integrity = 1,
		/// <summary>
		/// Privacy is requested
		/// </summary>
		Privacy = 2,
		ReadOnly = 4,
	}

	/// <summary>
	/// Specifies a message security buffer.
	/// </summary>
	public readonly ref struct SecBuffer
	{
		/// <summary>
		/// Initializes a new <see cref="SecBuffer"/>.
		/// </summary>
		/// <param name="buffer">Message buffer</param>
		/// <param name="options">Options specifying how the buffer is to be used</param>
		internal SecBuffer(
			Span<byte> buffer,
			MessageSecBufferOptions options
			)
		{
			this._span = buffer;
			this.Options = options;
		}

		public static SecBuffer Integrity(Span<byte> buffer) => new SecBuffer(buffer, MessageSecBufferOptions.Integrity | MessageSecBufferOptions.ReadOnly);
		public static SecBuffer PrivacyWithIntegrity(Span<byte> buffer) => new SecBuffer(buffer, MessageSecBufferOptions.Integrity | MessageSecBufferOptions.Privacy);

		/// <inheritdoc/>
		public readonly override string ToString()
			=> $"Length={this._span.Length}, Privacy={ShouldEncrypt}";

		public readonly bool IsReadOnly => 0 != (this.Options & MessageSecBufferOptions.ReadOnly);

		private readonly Span<byte> _span;
		/// <summary>
		/// Gets the size of the buffer, in bytes.
		/// </summary>
		public readonly int Length => this._span.Length;

		/// <summary>
		/// Gets the message buffer.
		/// </summary>
		public readonly Span<byte> Span => !this.IsReadOnly ? this._span : throw new InvalidOperationException("The buffer is not writable.");
		public readonly ReadOnlySpan<byte> ReadOnlySpan => this._span;
		/// <summary>
		/// Gets options specifying how the buffer is to be used.
		/// </summary>
		public readonly MessageSecBufferOptions Options { get; }
		/// <summary>
		/// Gets a value indicating whether message privacy is requested.
		/// </summary>
		public readonly bool ShouldEncrypt => (0 != (this.Options & MessageSecBufferOptions.Privacy));
		/// <summary>
		/// Gets a value indicating whether message integrity is requested.
		/// </summary>
		public readonly bool ShouldSign => (0 != (this.Options & MessageSecBufferOptions.Integrity));

	}

	/// <summary>
	/// Specifies message signing parameters.
	/// </summary>
	/// <remarks>
	/// This structure specifies a MAC buffer as well as
	/// up to 3 message segments to sign together.
	/// </remarks>
	public ref struct MessageSignParams
	{
		/// <summary>
		/// Initializes a new <see cref="MessageSignParams"/>
		/// </summary>
		/// <param name="macBuffer">Message authentication code buffer</param>
		/// <param name="buffers">List of buffers to sign</param>
		public MessageSignParams(
			Span<byte> macBuffer,
			in SecBufferList buffers
			)
		{
			this.MacBuffer = macBuffer;
			this.bufferList = buffers;
		}

		/// <summary>
		/// Gets the message authentication code buffer.
		/// </summary>
		public readonly Span<byte> MacBuffer { get; }

		/// <summary>
		/// Gets the list of buffers to sign.
		/// </summary>
		public SecBufferList bufferList;
	}

	/// <summary>
	/// Specifies message verification parameters.
	/// </summary>
	public ref struct MessageVerifyParams
	{
		/// <summary>
		/// Initializes a new <see cref="MessageVerifyParams"/>.
		/// </summary>
		/// <param name="macBuffer">Message authentication code buffer</param>
		/// <param name="buffers">List of buffers to sign</param>
		public MessageVerifyParams(
			ReadOnlySpan<byte> macBuffer,
			in SecBufferList buffers
			)
		{
			this.MacBuffer = macBuffer;
			this.bufferList = buffers;
		}
		/// <summary>
		/// Gets the message authentication code buffer.
		/// </summary>
		public readonly ReadOnlySpan<byte> MacBuffer { get; }

		/// <summary>
		/// Gets the buffer holding first segment of the message.
		/// </summary>
		public SecBufferList bufferList;
	}

	/// <summary>
	/// Specifies options affecting how a message is sealed.
	/// </summary>
	[Flags]
	public enum SealOptions
	{
		None = 0,

		GssWrap = 1,
	}

	public ref struct MessageSealParams
	{
		public MessageSealParams(
			Span<byte> header,
			in SecBufferList buffers,
			Span<byte> trailer
			)
		{
			this.Header = header;
			this.bufferList = buffers;
			this.Trailer = trailer;
		}

		public readonly Span<byte> Header { get; }
		public SecBufferList bufferList;
		public readonly Span<byte> Trailer { get; }
	}

	public interface ISecBufferList
	{
		SecBuffer GetBuffer(int index);
	}

	public readonly ref struct SecBufferList : ISecBufferList
	{
		public static SecBufferList Create(SecBuffer buf1) => new SecBufferList(1, buf1, default, default);
		public static SecBufferList Create(SecBuffer buf1, SecBuffer buf2) => new SecBufferList(2, buf1, buf2, default);
		public static SecBufferList Create(SecBuffer buf1, SecBuffer buf2, SecBuffer buf3) => new SecBufferList(3, buf1, buf2, buf3);
		private SecBufferList(
			int count,
			SecBuffer buf1,
			SecBuffer buf2,
			SecBuffer buf3
			)
		{
			this._myBufferCount = count;

			this.buf1 = buf1;
			this.buf2 = buf2;
			this.buf3 = buf3;
		}

		unsafe private SecBufferList(
			int bufferCount,
			SecBuffer buf1,
			SecBuffer buf2,
			SecBuffer buf3,
			SecBufferList* pExtension,
			int extensionIndex
			)
		{
			this._myBufferCount = bufferCount;

			if (pExtension != null && pExtension->BufferCount > 0)
			{
				this.pExtension = pExtension;
				this._extensionSize = pExtension->BufferCount;
				this._extensionIndex = extensionIndex;
			}

			this.buf1 = buf1;
			this.buf2 = buf2;
			this.buf3 = buf3;
		}

#if DEBUG
		public SecBufferList DeepCopy(MessageSecBufferOptions options)
		{
			if (this._extensionSize > 0)
				throw new InvalidOperationException();

			byte[] copy = ToArray(options);

			return new SecBufferList(this.BufferCount,
				new SecBuffer(copy.Slice(0, this.buf1.Length), this.buf1.Options),
				new SecBuffer(copy.Slice(this.buf1.Length, this.buf2.Length), this.buf2.Options),
				new SecBuffer(copy.Slice(this.buf1.Length + this.buf2.Length, this.buf3.Length), this.buf3.Options));

		}

		public byte[] ToArray(MessageSecBufferOptions options)
		{
			var cbData = this.TotalIntegrityLength;
			var copy = new byte[cbData];
			this.CopySectionTo(options, 0, copy);
			return copy;
		}
#endif

		private readonly unsafe SecBufferList* pExtension;
		private readonly int _extensionIndex;
		private readonly int _extensionSize;

		private readonly SecBuffer buf1;
		private readonly SecBuffer buf2;
		private readonly SecBuffer buf3;

		private readonly int _myBufferCount;
		public readonly int BufferCount => this._myBufferCount + this._extensionSize;
		public readonly SecBuffer GetBuffer(int index)
		{
			if ((uint)(index - this._extensionIndex) < (uint)this._extensionSize)
				return this.GetExtensionBuffer(index);

			if (index > this._extensionIndex)
				index -= this._extensionSize;

			return index switch
			{
				0 => this.buf1,
				1 => this.buf2,
				2 => this.buf3,
				_ => throw new ArgumentOutOfRangeException(nameof(index))
			};
		}

		private unsafe SecBuffer GetExtensionBuffer(int index)
		{
			Debug.Assert(this.pExtension != null);
			return this.pExtension->GetBuffer(index - this._extensionIndex);
		}

		public readonly int TotalPrivacyLength
		{
			get
			{
				int size = 0;
				for (int i = 0; i < this.BufferCount; i++)
				{
					var buf = this.GetBuffer(i);
					if (buf.ShouldEncrypt)
						size += buf.Length;
				}

				return size;
			}
		}

		public readonly int TotalIntegrityLength
		{
			get
			{
				int size = 0;
				for (int i = 0; i < this.BufferCount; i++)
				{
					var buf = this.GetBuffer(i);
					if (buf.ShouldSign)
						size += buf.Length;
				}

				return size;
			}
		}

		private readonly int FindBufferIndex(int ibuf, ref int offset, MessageSecBufferOptions options)
		{
			for (; ibuf < this.BufferCount; ibuf++)
			{
				var buf = this.GetBuffer(ibuf);
				if (0 == (buf.Options & options))
					continue;

				if (offset < buf.Length)
					return ibuf;
				else
					offset -= buf.Length;
			}

			return ibuf;
		}

		public readonly int CopySectionTo(
			MessageSecBufferOptions sourceOptions,
			int startOffset,
			Span<byte> target
			)
		{
			int ibuf = this.FindBufferIndex(0, ref startOffset, sourceOptions);

			int iWrite = 0;
			while (iWrite < target.Length && ibuf < this.BufferCount)
			{
				var buf = this.GetBuffer(ibuf);

				var cbChunk = Math.Min(buf.Length - startOffset, target.Length - iWrite);
				buf.ReadOnlySpan.Slice(startOffset, cbChunk)
					.CopyTo(target.Slice(iWrite, cbChunk));

				iWrite += cbChunk;

				ibuf = this.FindBufferIndex(ibuf + 1, ref startOffset, sourceOptions);
			}

			return iWrite;
		}

		public readonly int CopySectionFrom(
			Span<byte> source,
			MessageSecBufferOptions targetOptions,
			int startOffset
			)
		{
			int ibuf = this.FindBufferIndex(0, ref startOffset, targetOptions);

			int iRead = 0;
			while (iRead < source.Length && ibuf < this.BufferCount)
			{
				var buf = this.GetBuffer(ibuf);

				var cbChunk = Math.Min(buf.Length - startOffset, source.Length - iRead);
				source.Slice(iRead, cbChunk)
					.CopyTo(buf.Span.Slice(startOffset, cbChunk));

				iRead += cbChunk;

				ibuf = this.FindBufferIndex(ibuf + 1, ref startOffset, targetOptions);
			}

			return iRead;
		}

		public unsafe SecBufferList WithCombined(SecBuffer before, SecBuffer after)
		{
			fixed (SecBufferList* pThis = &this)
			{
				return new SecBufferList(
					2,
					before,
					after,
					default,
					pThis,
					1);
			}
		}
	}
}
