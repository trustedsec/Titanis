using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Provides extension methods for <see cref="Dictionary{TKey, TValue}"/>.
	/// </summary>
	public static class DictionaryExtensions
	{
		/// <summary>
		/// Attempts to get a value from a dictionary.
		/// </summary>
		/// <typeparam name="TKey">Type of key</typeparam>
		/// <typeparam name="TValue">Type of value</typeparam>
		/// <param name="dictionary">Dictionary containing value</param>
		/// <param name="key">Key to lookup</param>
		/// <returns>Value looked up by <paramref name="key"/>, if found; otherwise, the default value for <typeparamref name="TValue"/>.</returns>
		[return:MaybeNull]
		public static TValue TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
			where TKey : notnull
		{
			dictionary.TryGetValue(key, out TValue value);
			return value;
		}
	}
}
