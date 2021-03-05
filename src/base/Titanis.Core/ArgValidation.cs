using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Provides methods for validating arguments.
	/// </summary>
	public static class ArgValidation
	{
		public static TValue IsNonNull<TValue>(TValue value, [CallerArgumentExpression(nameof(value))] string? argName = null)
			where TValue : class
		{
			if (value == null) throw new ArgumentNullException(argName);
			return value;
		}
		/// <summary>
		/// Checks whether an argument is negative.  That is, checks that an argument is positive or zero.
		/// </summary>
		/// <param name="value">Argument value to check</param>
		/// <param name="argName">Name of argument</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
		public static void IsNonnegative(int value, string argName)
		{
			if (value < 0)
				throw new ArgumentOutOfRangeException(argName, Messages.ArgNonnegRequiredMessage);
		}
		/// <summary>
		/// Checks whether an argument is greater than zero.
		/// </summary>
		/// <param name="value">Argument value to check</param>
		/// <param name="argName">Name of argument</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
		public static void IsPositiveNonzero(int value, string argName)
		{
			if (value <= 0)
				throw new ArgumentOutOfRangeException(argName, Messages.ArgPositiveNonzeroRequiredMessage);
		}
		/// <summary>
		/// Checks whether one parameter is greater than another.
		/// </summary>
		/// <param name="min">Minimum value</param>
		/// <param name="max">Maximum value</param>
		/// <param name="argName">Name of argument</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="max"/> is less than <paramref name="min"/></exception>
		public static void IsMinMax(int min, int max, string argName)
		{
			if (max < min)
				throw new ArgumentOutOfRangeException(argName, Messages.ArgMinMaxInvalidMessage);
		}
		/// <summary>
		/// Checks whether an index and length specify a valid range within an array.
		/// </summary>
		/// <typeparam name="T">Type of array</typeparam>
		/// <param name="array">Array to check against</param>
		/// <param name="startIndex">Start index of range</param>
		/// <param name="count">Number of elements in range</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is past the end of the array</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is past the end of the array</exception>
		/// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c></exception>
		/// <remarks>
		/// If <paramref name="startIndex"/> is equal to the length of <paramref name="array"/> and
		/// <paramref name="count"/> is <c>0</c>, the check succeeds.
		/// </remarks>
		public static void IsValidRange<T>(T[] array, int startIndex, int count)
		{
			if (array is null) throw new ArgumentNullException(nameof(array));
			if (startIndex >= array.Length)
				throw new ArgumentOutOfRangeException(nameof(startIndex), Messages.ArrayStartIndexOutOfRangeMessage);
			if ((startIndex + count) > array.Length)
				throw new ArgumentOutOfRangeException(nameof(startIndex), Messages.ArrayLengthOutOfRangeMessage);
		}
		/// <summary>
		/// Checks whether an index and length specify a valid range within a list.
		/// </summary>
		/// <typeparam name="T">Type of list element</typeparam>
		/// <param name="list">List to check against</param>
		/// <param name="startIndex">Start index of range</param>
		/// <param name="count">Number of elements in range</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is past the end of the list</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is past the end of the list</exception>
		/// <exception cref="ArgumentNullException"><paramref name="list"/> is <c>null</c></exception>
		/// <remarks>
		/// If <paramref name="startIndex"/> is equal to the length of <paramref name="list"/> and
		/// <paramref name="count"/> is <c>0</c>, the check succeeds.
		/// </remarks>
		public static void IsValidRange<T>(IList<T> list, int startIndex, int count)
		{
			if (list is null) throw new ArgumentNullException(nameof(list));
			if (startIndex >= list.Count)
				throw new ArgumentOutOfRangeException(nameof(startIndex), Messages.ArrayStartIndexOutOfRangeMessage);
			if ((startIndex + count) > list.Count)
				throw new ArgumentOutOfRangeException(nameof(startIndex), Messages.ArrayLengthOutOfRangeMessage);
		}

		/// <summary>
		/// Validates buffer parameters.
		/// </summary>
		/// <typeparam name="T">Type of array element</typeparam>
		/// <param name="buffer">Buffer</param>
		/// <param name="startIndex">Start index of range</param>
		/// <param name="count">Number of elements in range</param>
		/// <param name="bufferName">Name of buffer parameter</param>
		/// <param name="startIndexName">Name of start index parameter</param>
		/// <param name="countName">Name of count parameter</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public static void IsValidBufferParams<T>(
			T[] buffer,
			int startIndex,
			int count,
			string bufferName,
			string startIndexName,
			string countName)
		{
			if (buffer == null)
				throw new ArgumentNullException(bufferName);
			if (startIndex < 0)
				throw new ArgumentOutOfRangeException(startIndexName, startIndex, Messages.ArgNonnegRequiredMessage);
			if (count < 0)
				throw new ArgumentOutOfRangeException(countName, startIndex, Messages.ArgNonnegRequiredMessage);
			if ((buffer.Length - startIndex) < count)
				throw new ArgumentException(Messages.BufferSpanOutOfRangeMessage);
		}
	}
}
