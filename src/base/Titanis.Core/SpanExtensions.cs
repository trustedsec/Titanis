using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Provides extension methods for <see cref="Span{T}"/> and <see cref="ReadOnlySpan{T}"/>.
	/// </summary>
	public static class SpanExtensions
	{
		public static Span<T> SliceFromEnd<T>(this Span<T> span, int countFromEnd)
			=> span.Slice(span.Length - countFromEnd);
		public static ReadOnlySpan<T> SliceFromEnd<T>(this ReadOnlySpan<T> span, int countFromEnd)
			=> span.Slice(span.Length - countFromEnd);

		/// <summary>
		/// Companes the bytes within two spans.
		/// </summary>
		/// <param name="span">First span</param>
		/// <param name="other">Other span</param>
		/// <returns>
		/// A negative value if <paramref name="span"/> is less than <paramref name="other"/>;
		/// a positive value if <paramref name="span"/> is greater than <paramref name="other"/>;
		/// <c>0</c> if the bytes in <paramref name="span"/> and <paramref name="other"/> are equal.
		/// If <paramref name="span"/> and <paramref name="other"/> are not the same length,
		/// then only the bytes in common are compared, and if they are equal, the longer
		/// span is indicated to be the greater one.
		/// </returns>
		public static int CompareTo(this Span<byte> span, ReadOnlySpan<byte> other)
			=> CompareBytes(span, other);
		/// <summary>
		/// Companes the bytes within two spans.
		/// </summary>
		/// <param name="span">First span</param>
		/// <param name="other">Other span</param>
		/// <returns>
		/// A negative value if <paramref name="span"/> is less than <paramref name="other"/>;
		/// a positive value if <paramref name="span"/> is greater than <paramref name="other"/>;
		/// <c>0</c> if the bytes in <paramref name="span"/> and <paramref name="other"/> are equal.
		/// If <paramref name="span"/> and <paramref name="other"/> are not the same length,
		/// then only the bytes in common are compared, and if they are equal, the longer
		/// span is indicated to be the greater one.
		/// </returns>
		public static int CompareTo(this ReadOnlySpan<byte> span, ReadOnlySpan<byte> other)
			=> CompareBytes(span, other);
		/// <summary>
		/// Companes the bytes within two spans.
		/// </summary>
		/// <param name="span">First span</param>
		/// <param name="other">Other span</param>
		/// <returns>
		/// A negative value if <paramref name="span"/> is less than <paramref name="other"/>;
		/// a positive value if <paramref name="span"/> is greater than <paramref name="other"/>;
		/// <c>0</c> if the bytes in <paramref name="span"/> and <paramref name="other"/> are equal.
		/// If <paramref name="span"/> and <paramref name="other"/> are not the same length,
		/// then only the bytes in common are compared, and if they are equal, the longer
		/// span is indicated to be the greater one.
		/// </returns>
		public static int CompareBytes(ReadOnlySpan<byte> span, ReadOnlySpan<byte> other)
		{
			int max = Math.Min(span.Length, other.Length);
			for (int i = 0; i < max; i++)
			{
				var cmp = span[i] - other[i];
				if (cmp != 0)
					return cmp;
			}

			if (span.Length > max)
				return 1;
			else if (span.Length < max)
				return -1;
			else
				return 0;
		}
	}
}
