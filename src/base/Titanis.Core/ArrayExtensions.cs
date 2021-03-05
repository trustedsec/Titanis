using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Provides extension methods for arrays.
	/// </summary>
	public static class ArrayExtensions
	{
		/// <summary>
		/// Gets a value indicating whether an array variable is null or empty.
		/// </summary>
		/// <typeparam name="T">Element type</typeparam>
		/// <param name="array">Array variable</param>
		/// <returns><see langword="true"/> if <paramref name="array"/> is <see langword="null"/> or has no elements.</returns>
		public static bool IsNullOrEmpty<T>(this T[]? array)
			=> (array is null || array.Length == 0);

		/// <summary>
		/// Gets a value indicating whether a list variable is null or empty.
		/// </summary>
		/// <typeparam name="T">Element type</typeparam>
		/// <param name="array">List variable</param>
		/// <returns><see langword="true"/> if <paramref name="array"/> is <see langword="null"/> or has no elements.</returns>
		public static bool IsNullOrEmpty<T>(this IReadOnlyList<T>? array)
			=> (array is null || array.Count == 0);

		/// <summary>
		/// Concatenates an array with an element.
		/// </summary>
		/// <typeparam name="T">Type of array element</typeparam>
		/// <param name="array">Array to concatenate</param>
		/// <param name="element">Element to concatenate</param>
		/// <returns>An array of <typeparamref name="T"/> consisting of the elements of <paramref name="array"/> 
		/// followed by <paramref name="element"/>.</returns>
		/// <remarks>
		/// <paramref name="array"/> is not modified.
		/// <para>
		/// If <paramref name="array"/> is <c>null</c>, no exception is thrown, and an array
		/// with the single element <paramref name="element"/> is returned.
		/// </para>
		/// </remarks>
		public static T[]? Concat<T>(this T[]? array, T element)
		{
			if (array != null)
			{
				T[] concat = new T[array.Length + 1];
				Array.Copy(array, 0, concat, 0, array.Length);
				concat[array.Length] = element;
				return concat;
			}
			else
			{
				return new T[] { element };
			}
		}

		/// <summary>
		/// Concatenates an array with an element.
		/// </summary>
		/// <typeparam name="T">Type of array element</typeparam>
		/// <param name="array">Array to concatenate</param>
		/// <param name="element">Element to concatenate</param>
		/// <returns>An array of <typeparamref name="T"/> consisting of <paramref name="element"/>
		/// followed by the elements of <paramref name="array"/>.</returns>
		/// <remarks>
		/// <paramref name="array"/> is not modified.
		/// </remarks>
		/// <remarks>
		/// <paramref name="array"/> is not modified.
		/// <para>
		/// If <paramref name="array"/> is <c>null</c>, no exception is thrown, and an array
		/// with the single element <paramref name="element"/> is returned.
		/// </para>
		/// </remarks>
		public static T[] PrependWith<T>(this T[]? array, T element)
		{
			if (array != null)
			{
				T[] concat = new T[array.Length + 1];
				concat[0] = element;
				Array.Copy(array, 0, concat, 1, array.Length);
				return concat;
			}
			else
			{
				return new T[] { element };
			}
		}

		/// <summary>
		/// Concatenates two arrays.
		/// </summary>
		/// <typeparam name="T">Type of array element</typeparam>
		/// <param name="array">First array to concatenate</param>
		/// <param name="array2">Second array to concatenate</param>
		/// <returns>A new array consisting of the elements of <paramref name="array"/> followed by
		/// <paramref name="array2"/>.</returns>
		/// <remarks>
		/// If <paramref name="array"/> or <paramref name="array2"/> are <c>null</c>, this does
		/// not caus an error, and the resulting array is a clone of the other array, if it isn't <c>null</c>.
		/// If both <paramref name="array"/> and <paramref name="array2"/> are <c>null</c>,
		/// an empty array is returned.
		/// </remarks>
		public static T[]? Concat<T>(this T[]? array, T[]? array2)
		{
			if (array2.IsNullOrEmpty())
				return (array == null) ? Array.Empty<T>() : (T[])array.Clone();
			else if (array.IsNullOrEmpty())
				return (array2 == null) ? Array.Empty<T>() : (T[])array2.Clone();
			else
			{
				T[] concat = new T[array!.Length + array2!.Length];
				Array.Copy(array, 0, concat, 0, array.Length);
				Array.Copy(array2, 0, concat, array.Length, array2.Length);
				return concat;
			}
		}
		/// <summary>
		/// Attempts to get an element from an array.
		/// </summary>
		/// <typeparam name="T">Type of element</typeparam>
		/// <param name="array">Array containing element</param>
		/// <param name="index">Index of element</param>
		/// <param name="defaultValue">Default value, if the array does not contain the element</param>
		/// <returns>The element at <paramref name="index"/> in <paramref name="array"/>, if
		/// <paramref name="array"/> is not <c>null</c>, and <paramref name="index"/> is within
		/// <paramref name="array"/>; otherwise, <paramref name="defaultValue"/>.</returns>
		public static bool TryGetElement<T>(this T[]? array, int index, [MaybeNull] out T? defaultValue)
		{
			if (array != null && index < array.Length)
			{
				defaultValue = array[index];
				return true;
			}
			else
			{
				defaultValue = default(T);
				return false;
			}
		}

		/// <summary>
		/// Attempts to get an element from an array.
		/// </summary>
		/// <typeparam name="T">Type of element</typeparam>
		/// <param name="array">Array containing element</param>
		/// <param name="index">Index of element</param>
		/// <returns>The element at <paramref name="index"/> in <paramref name="array"/>, if
		/// <paramref name="array"/> is not <c>null</c>, and <paramref name="index"/> is within
		/// <paramref name="array"/>; otherwise, the default value of <typeparamref name="T"/>.</returns>
		public static T? TryGetElement<T>(this T[]? array, int index)
		{
			if (array != null && index < array.Length)
			{
				return array[index];
			}
			else
			{
				return default(T);
			}
		}

		/// <summary>
		/// Computes a hash code over elements in an array.
		/// </summary>
		/// <typeparam name="T">Type of element</typeparam>
		/// <param name="array">Array containing elements</param>
		/// <returns>A hash code computed by combining the hash codes of individual elements.</returns>
		/// <remarks>
		/// If <paramref name="array"/> is <c>null</c>, the method returns <c>0</c>.
		/// </remarks>
		public static int GetElementsHashCode<T>(this T[]? array)
			=> array.GetElementsHashCode(EqualityComparer<T>.Default);
		/// <summary>
		/// Computes a hash code over elements in an array.
		/// </summary>
		/// <typeparam name="T">Type of element</typeparam>
		/// <param name="array">Array containing elements</param>
		/// <param name="equalityComparer"><see cref="IEqualityComparer{T}"/> to provide hash codes for each element</param>
		/// <returns>A hash code computed by combining the hash codes of individual elements.</returns>
		/// <remarks>
		/// If <paramref name="array"/> is <c>null</c>, the method returns <c>0</c>.
		/// </remarks>
		public static int GetElementsHashCode<T>(this T[]? array, IEqualityComparer<T>? equalityComparer)
		{
			if (array.IsNullOrEmpty())
				return 0;
			if (equalityComparer == null)
				equalityComparer = EqualityComparer<T>.Default;

			int hash = 0;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
			for (int i = 0; i < array.Length; i++)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
			{
				hash = HashCode.Combine(hash, equalityComparer.GetHashCode(array[i]));
			}
			return hash;
		}

		/// <summary>
		/// Checks whether two arrays contain identical elements.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static bool ElementsEqual<T>(this T[]? x, T[]? y)
			=> x.ElementsEqual(y, EqualityComparer<T>.Default);
		/// <summary>
		/// Checks whether two arrays contain identical elements.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="equalityComparer"><paramref name="equalityComparer"/> to compare each element</param>
		/// <returns></returns>
		public static bool ElementsEqual<T>(this T[]? x, T[]? y, IEqualityComparer<T>? equalityComparer)
		{
			if (x == y)
				return true;
			if ((x == null) || (y == null))
				return false;
			if (equalityComparer == null)
				equalityComparer = EqualityComparer<T>.Default;

			if (x.Length != y.Length)
				return false;

			for (int i = 0; i < x.Length; i++)
			{
				T xElem = x[i];
				T yElem = y[i];

				if (!object.Equals(xElem, yElem))
					return false;
			}

			return true;
		}

		#region Span stuff
		/// <summary>
		/// Creates a span containing a range of elements within an array.
		/// </summary>
		/// <typeparam name="T">Type of element</typeparam>
		/// <param name="array">Array containing the elements</param>
		/// <param name="startIndex">Index of first element of range</param>
		/// <returns>A <see cref="Span{T}"/> containing the range of elements of <paramref name="array"/>
		/// starting with the element at <paramref name="startIndex"/>.</returns>
		public static Span<T> Slice<T>(this T[] array, int startIndex)
			=> new Span<T>(array, startIndex, array.Length - startIndex);
		/// <summary>
		/// Creates a read-only span containing a range of elements within an array.
		/// </summary>
		/// <typeparam name="T">Type of element</typeparam>
		/// <param name="array">Array containing the elements</param>
		/// <param name="startIndex">Index of first element of range</param>
		/// <returns>A <see cref="ReadOnlySpan{T}"/> containing the range of elements of <paramref name="array"/>
		/// starting with the element at <paramref name="startIndex"/>.</returns>
		public static ReadOnlySpan<T> SliceReadOnly<T>(this T[] array, int startIndex)
			=> new ReadOnlySpan<T>(array, startIndex, array.Length - startIndex);

		/// <summary>
		/// Creates a span containing a range of elements within an array.
		/// </summary>
		/// <typeparam name="T">Type of element</typeparam>
		/// <param name="array">Array containing the elements</param>
		/// <param name="startIndex">Index of first element of range</param>
		/// <param name="count">Number of elements to include in range</param>
		/// <returns>A <see cref="Span{T}"/> containing the range of elements of <paramref name="array"/>
		/// starting with the element at <paramref name="startIndex"/>.</returns>
		public static Span<T> Slice<T>(this T[] array, int startIndex, int count)
			=> new Span<T>(array, startIndex, count);
		/// <summary>
		/// Creates a read-only span containing a range of elements within an array.
		/// </summary>
		/// <typeparam name="T">Type of element</typeparam>
		/// <param name="array">Array containing the elements</param>
		/// <param name="startIndex">Index of first element of range</param>
		/// <param name="count">Number of elements to include in range</param>
		/// <returns>A <see cref="Span{T}"/> containing the range of elements of <paramref name="array"/>
		/// starting with the element at <paramref name="startIndex"/>.</returns>
		public static ReadOnlySpan<T> SliceReadOnly<T>(this T[] array, int startIndex, int count)
			=> new ReadOnlySpan<T>(array, startIndex, count);
		/// <summary>
		/// Creates a span containing a range of elements within an array.
		/// </summary>
		/// <typeparam name="T">Type of element</typeparam>
		/// <param name="array">Array containing the elements</param>
		/// <param name="countFromEnd">Number of elements from end of array</param>
		/// <returns>A <see cref="Span{T}"/> containing the range of elements of <paramref name="array"/>
		/// starting with the element <paramref name="countFromEnd"/> from the end of the array.</returns>
		public static Span<T> SliceEnd<T>(this T[] array, int countFromEnd)
			=> new Span<T>(array, array.Length - countFromEnd, countFromEnd);
		/// <summary>
		/// Creates a read-only span containing a range of elements within an array.
		/// </summary>
		/// <typeparam name="T">Type of element</typeparam>
		/// <param name="array">Array containing the elements</param>
		/// <param name="countFromEnd">Number of elements from end of array</param>
		/// <returns>A <see cref="Span{T}"/> containing the range of elements of <paramref name="array"/>
		/// starting with the element <paramref name="countFromEnd"/> from the end of the array.</returns>
		public static ReadOnlySpan<T> SliceEndReadOnly<T>(this T[] array, int countFromEnd)
			=> new ReadOnlySpan<T>(array, array.Length - countFromEnd, countFromEnd);

		// SOURCE: Derived from mscorlib/Array.cs
		private static int GetMedian(int low, int hi)
			// Note both may be negative, if we are dealing with arrays w/ negative lower bounds.
			=> low + ((hi - low) >> 1);

		public static int BinarySearch<T, TSearch, TComparer>(
			this T[] array,
			TSearch value,
			TComparer comparer)
			where TComparer : IComparer<T, TSearch>
			=> array.BinarySearch(0, array.Length, value, comparer);
		// SOURCE: Derived from mscorlib/Array.cs
		public static int BinarySearch<T, TSearch, TComparer>(
			this T[] array,
			int index,
			int length,
			TSearch value,
			TComparer comparer)
			where TComparer : IComparer<T, TSearch>
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));

			int lo = index;
			int hi = index + length - 1;
			while (lo <= hi)
			{
				// i might overflow if lo and hi are both large positive numbers. 
				int i = GetMedian(lo, hi);

				int c = comparer.Compare(array[i], value);
				if (c == 0) return i;
				if (c < 0)
				{
					lo = i + 1;
				}
				else
				{
					hi = i - 1;
				}
			}

			return ~lo;
		}
		#endregion
	}
	/// <summary>
	/// Provides functionality for comparing two values that are not of the same type.
	/// </summary>
	/// <typeparam name="T">Type of elements in array to search</typeparam>
	/// <typeparam name="TSearch">Type of search value</typeparam>
	public interface IComparer<T, TSearch>
	{
		int Compare(T x, TSearch y);
	}
}
