using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Titanis
{
	public static class SpanReadContext
	{
		public static SpanReadContext<T> FromSpan<T>(ReadOnlySpan<T> span) => new SpanReadContext<T>(span);


		public static int Peek(this ref SpanReadContext<char> ctx) => ctx.IsEof ? -1 : ctx.span[ctx.readIndex];
		public static int Peek(this ref SpanReadContext<char> ctx, int offset) => (offset >= ctx.LengthRemaining) ? -1 : ctx.span[ctx.readIndex + offset];
		public static bool StartsWith(this ref SpanReadContext<char> ctx, ReadOnlySpan<char> prefix, bool ignoreCase)
		{
			if (ctx.IsEof)
				return false;

			if (ignoreCase)
			{
				for (int i = 0; i < prefix.Length; i++)
				{
					if (char.ToUpper(ctx.span[ctx.readIndex + i]) != char.ToUpper(prefix[i]))
						return false;
				}
			}
			else
			{
				for (int i = 0; i < prefix.Length; i++)
				{
					if (ctx.span[ctx.readIndex + i] != prefix[i])
						return false;
				}
			}
			return true;
		}
	}


	public ref struct SpanReadContext<T>
	{
		public SpanReadContext(ReadOnlySpan<T> span)
		{
			this.span = span;
		}

		public readonly ReadOnlySpan<T> span;
		public int readIndex;

		public int Length => this.span.Length;
		public bool IsEof => this.readIndex >= this.span.Length;

		/// <summary>
		/// Gets the number of elements after the read index.
		/// </summary>
		public int LengthRemaining => this.Length - this.readIndex;

		public ReadOnlySpan<T> Remaining() => this.span.Slice(this.readIndex);
		public ReadOnlySpan<T> Remaining(int count) => this.span.Slice(this.readIndex, count);

		public T Peek(T defaultValue) => (!this.IsEof) ? this.span[this.readIndex] : defaultValue;
		public T PeekAt(int offset, T defaultValue) => (offset < this.LengthRemaining) ? this.span[this.readIndex + offset] : defaultValue;

		public void Advance(int count)
		{
			this.readIndex += count;
		}
		public bool AdvanceIf(T expected)
		{
			if (!this.IsEof && expected.Equals(this.span[this.readIndex]))
			{
				this.Advance(1);
				return true;
			}
			else
				return false;
		}
		public void Expect(T expected)
		{
			if (!this.IsEof && expected.Equals(this.span[this.readIndex]))
			{
				this.Advance(1);
			}
			else
			{
				throw new InvalidDataException($"Expected {expected} at offset {this.readIndex}, but encountered {(this.IsEof ? "<end>" : (this.span[this.readIndex]?.ToString() ?? "<null>"))}");
			}
		}
		public ReadOnlySpan<T> Consume(int count)
		{
			var consumed = this.span.Slice(this.readIndex, count);
			this.Advance(count);
			return consumed;
		}

		public int CountMatching(Func<T, bool> predicate)
		{
			int count;
			for (count = 0; count < this.LengthRemaining && predicate(this.PeekAt(count, default)); count++)
				;
			return count;
		}

		public int Skip(T valueToSkip)
		{
			int count;
			for (count = 0; count < this.LengthRemaining && valueToSkip.Equals(this.PeekAt(count, default)); count++)
				;
			this.Advance(count);
			return count;
		}
	}
}
