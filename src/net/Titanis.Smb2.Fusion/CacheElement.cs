using System;

namespace Titanis.Smb2.Fusion;

/// <summary>
/// Describes an element in a cache.
/// </summary>
/// <typeparam name="T">Type of element</typeparam>
/// <remarks>
/// This structure tracks an object of type <typeparamref name="T"/>
/// along with an expiration.  When a caller fetches this element,
/// it first checks the expiration, and if the element has expired,
/// it is retrieved from the source.
/// </remarks>
internal struct CacheElement<T>
{
	public CacheElement(Func<CancellationToken, Task<T>> factory)
	{
		ArgumentNullException.ThrowIfNull(factory);

		_factory = factory;
	}

	private readonly Func<CancellationToken, Task<T>> _factory;

	public DateTime _fetched;
	private T _cachedValue;

	/// <summary>
	/// Gets the cached value.
	/// </summary>
	/// <param name="expirationInterval">Amount of time to consider the cached value valid</param>
	/// <returns>The cached value</returns>
	/// <remarks>
	/// If the value was cached more than <paramref name="expirationInterval"/> ago, a new value is fetched before returning.
	/// </remarks>
	public async ValueTask<T> GetValue(TimeSpan expirationInterval, CancellationToken cancellationToken)
	{
		if ((this._fetched + expirationInterval) < DateTime.UtcNow)
		{
			this._cachedValue = await _factory(cancellationToken).ConfigureAwait(false);
			this._fetched = DateTime.UtcNow;
		}

		return this._cachedValue;
	}
}
