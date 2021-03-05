using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.IO
{
	/// <summary>
	/// Implements a text reader that provides access to buffered text.
	/// </summary>
	public abstract class BufferedTextReader : AsyncTextReader
	{
		/// <summary>
		/// Fills the text buffer from the underlying source.
		/// </summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests</param>
		/// <returns><c>true</c> if enough data was read to fill the buffer; otherwise, <c>false</c>.</returns>
		public abstract ValueTask<bool> FillBufferAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Fills the text buffer from the underlying source.
		/// </summary>
		/// <returns><c>true</c> if enough data was read to fill the buffer; otherwise, <c>false</c>.</returns>
		public ValueTask<bool> FillBufferAsync()
			=> this.FillBufferAsync(CancellationToken.None);

		/// <summary>
		/// Fills the text buffer from the underlying source.
		/// </summary>
		/// <returns><c>true</c> if enough data was read to fill the buffer; otherwise, <c>false</c>.</returns>
		public abstract bool FillBuffer();

		/// <summary>
		/// Gets the buffered text.
		/// </summary>
		/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="char"/> containing the buffered text</returns>
		public abstract ReadOnlySpan<char> GetBufferedText();
		/// <summary>
		/// Advances the buffer.
		/// </summary>
		/// <param name="cch">Number of characters to advance by.</param>
		protected abstract void AdvanceCore(int cch);
		/// <summary>
		/// Advances the buffer.
		/// </summary>
		/// <param name="cch">Number of characters to advance by.</param>
		public void Advance(int cch)
		{
			this.AdvanceCore(cch);
		}
	}
}
