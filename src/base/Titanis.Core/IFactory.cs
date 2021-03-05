using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Provides functionality for creating an object.
	/// </summary>
	/// <typeparam name="T">Type of object to create</typeparam>
	public interface IFactory<T>
	{
		/// <summary>
		/// Creates a new object.
		/// </summary>
		/// <returns>The newly-created object</returns>
		T Create();
	}

	/// <summary>
	/// Provides a default implementation for <see cref="IFactory{T}"/>
	/// </summary>
	/// <typeparam name="T">Type of object to create</typeparam>
	/// <remarks>
	/// The default implementation simply returns the default value for <typeparamref name="T"/>.
	/// </remarks>
	public struct DefaultFactory<T> : IFactory<T>
		where T : new()
	{
		public T Create()
		{
			return new T();
		}

		/// <summary>
		/// Gets the global instance of <see cref="DefaultFactory{T}"/>.
		/// </summary>
		public static DefaultFactory<T> Instance { get; } = new DefaultFactory<T>();
	}

	/// <summary>
	/// Implements a factory that creates <see cref="Dictionary{TKey, TValue}"/>.
	/// </summary>
	/// <typeparam name="TKey">Type of dictionary key</typeparam>
	/// <typeparam name="TValue">Type of dictionary value</typeparam>
	public struct DictionaryFactory<TKey, TValue> : IFactory<Dictionary<TKey, TValue>>
		where TKey : notnull
	{
		/// <summary>
		/// Initializes a new <see cref="DictionaryFactory{TKey, TValue}"/>.
		/// </summary>
		/// <param name="keyComparer">The <see cref="IEqualityComparer{TKey}"/> implementation to use when
		/// comparing keys, or null to use the default <see cref="IEqualityComparer{TKey}"/>
		/// for the type of the key.</param>
		public DictionaryFactory(IEqualityComparer<TKey>? keyComparer)
		{
			this.KeyComparer = keyComparer;
		}

		/// <summary>
		/// Gets the <see cref="IEqualityComparer{T}"/> to use to compare key values.
		/// </summary>
		public IEqualityComparer<TKey>? KeyComparer { get; }

		/// <summary>
		/// Creates a new <see cref="Dictionary{TKey, TValue}"/>.
		/// </summary>
		/// <returns></returns>
		public Dictionary<TKey, TValue> Create()
		{
			return new Dictionary<TKey, TValue>(this.KeyComparer);
		}
	}
}
