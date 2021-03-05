using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Describes an entry in a <see cref="CacheTable{TKey, TValue}"/>
	/// </summary>
	/// <typeparam name="TValue">Type of value</typeparam>
	/// <seealso cref="CacheTable{TKey, TValue}"/>
	struct CacheEntry<TValue>
	{
		public CacheEntry(TValue value)
		{
			this.value = value;
		}

		internal TValue value;
	}
	/// <summary>
	/// Implements a cache table.
	/// </summary>
	/// <typeparam name="TKey">Type of key</typeparam>
	/// <typeparam name="TValue">Type of value</typeparam>
	/// <remarks>
	/// A cache table is a lookup table where values are fetched from an external source when needed,
	/// but then cached, so future lookups for the same value are readily available and do not need to
	/// be fetched.
	/// </remarks>
	public class CacheTable<TKey, TValue>
	{
		private readonly Func<TKey, TValue> _fetcher;

		/// <summary>
		/// Initializes a new <see cref="CacheTable{TKey, TValue}"/>
		/// </summary>
		/// <param name="fetch">Function to fetch values, as needed</param>
		/// <param name="keyComparer">Key comparer</param>
		/// <exception cref="ArgumentNullException"><paramref name="fetch"/> is <c>null</c></exception>
		public CacheTable(Func<TKey, TValue> fetch, IEqualityComparer<TKey>? keyComparer = null)
		{
			if (fetch is null) throw new ArgumentNullException(nameof(fetch));
			this._fetcher = fetch;

			this._table = new Dictionary<TKey, CacheEntry<TValue>>(keyComparer);
		}

		private Dictionary<TKey, CacheEntry<TValue>> _table;

		/// <summary>
		/// Looks up a value.
		/// </summary>
		/// <param name="key">Key of value to look up</param>
		/// <returns>The value identified by <paramref name="key"/></returns>
		/// <remarks>
		/// If the value is already in the cache, the cached value is returned.
		/// If, not, the value is fetched, cached, and returned.
		/// </remarks>
		public TValue GetOrCreateValue(TKey key)
		{
			if (this._table.TryGetValue(key, out CacheEntry<TValue> entry))
			{
				return entry.value;
			}
			else
			{
				lock (this._table)
				{
					if (this._table.TryGetValue(key, out entry))
					{
						return entry.value;
					}
					else
					{
						TValue value = this._fetcher(key);
						this._table.Add(key, new CacheEntry<TValue>(value));
						return value;
					}
				}
			}
		}
	}
}
