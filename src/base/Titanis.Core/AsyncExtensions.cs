using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis
{
	/// <summary>
	/// Provides extension methods for using <see cref="IAsyncEnumerable{T}"/>.
	/// </summary>
	public static class AsyncExtensions
	{
		/// <summary>
		/// Converts <see cref="IEnumerable{T}"/> to <see cref="IAsyncEnumerable{T}"/>.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerable"></param>
		/// <returns></returns>
		public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> enumerable)
		{
			foreach (var item in enumerable)
			{
				yield return item;
			}
		}
		public static async Task<T[]> ToArray<T>(this IAsyncEnumerable<T> enumerable, CancellationToken cancellationToken)
		{
			List<T> list = new List<T>();
			await foreach (var item in enumerable.WithCancellation(cancellationToken).ConfigureAwait(false))
			{
				list.Add(item);
			}
			return list.ToArray();
		}
	}
}
