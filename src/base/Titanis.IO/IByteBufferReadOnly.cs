using System;

namespace Titanis.IO
{
	/// <summary>
	/// Exposes functionality to interact with the underlying
	/// memory buffer backing a byte source.
	/// </summary>
	public interface IByteBufferReadOnly : IByteSource
	{
		/// <summary>
		/// Gets the memory backing the byte source.
		/// </summary>
		/// <returns>A <see cref="ReadOnlySpan{T}"/> of bytes backing the byte source</returns>
		ReadOnlySpan<byte> GetByteSpanReadOnly();
		/// <summary>
		/// Gets a range of memory backing the byte source.
		/// </summary>
		/// <param name="startIndex">Index of start of range</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is not within the backing memory</exception>
		/// <returns>A <see cref="ReadOnlySpan{T}"/> backing the byte source starting at <paramref name="startIndex"/> and extending to the end of the buffer</returns>
		/// <remarks>
		/// Note that <paramref name="startIndex"/> is relative to the backing store, not to <see cref="IByteSource.Position"/>.
		/// Due to <see cref="ReadOnlySpan{T}"/> being limited to a size of <see cref="int"/>,
		/// the returned range may not include the entire range expected.
		/// </remarks>
		ReadOnlySpan<byte> GetByteSpanReadOnly(long startIndex);
		/// <summary>
		/// Gets a range of memory backing the byte source.
		/// </summary>
		/// <param name="startIndex">Index of start of range</param>
		/// <param name="count">Number of bytes to include in range</param>
		/// <exception cref="ArgumentOutOfRangeException">The range specified by <paramref name="startIndex"/> and <paramref name="count"/> are not within the backing memory</exception>
		/// <returns>A <see cref="ReadOnlySpan{T}"/> of bytes backing the byte source starting at <paramref name="startIndex"/> and extending for <paramref name="count"/> bytes</returns>
		/// <remarks>
		/// Note that <paramref name="startIndex"/> is relative to the backing store, not to <see cref="IByteSource.Position"/>.
		/// </remarks>
		ReadOnlySpan<byte> GetBytesReadOnly(long startIndex, int count);
	}
}
