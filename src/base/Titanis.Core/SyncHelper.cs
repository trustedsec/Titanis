using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Provides methods for synchronization.
	/// </summary>
	public static class SyncHelper
	{
		/// <summary>
		/// Gets or creates an object, using another object for synchronization.
		/// </summary>
		/// <typeparam name="T">Type of object to create</typeparam>
		/// <param name="value">Value to receive newly-created object</param>
		/// <param name="sync">Synchronization object</param>
		/// <returns>The newly-created object</returns>
		/// <exception cref="ArgumentNullException"><paramref name="sync"/> is <c>null</c></exception>
		/// <remarks>
		/// This method ensures that only one instance of an object is created, and subsequent requests
		/// will return the same object.
		/// </remarks>
		public static T GetOrCreate<T>(ref T? value, object sync)
			where T : class, new()
		{
			if (sync is null)
				throw new ArgumentNullException(nameof(sync));

			return GetOrCreate(ref value, sync, DefaultFactory<T>.Instance);
		}

		/// <summary>
		/// Gets or creates an object, using another object for synchronization.
		/// </summary>
		/// <typeparam name="T">Type of object to create</typeparam>
		/// <param name="value">Value to receive newly-created object</param>
		/// <param name="sync">Synchronization object</param>
		/// <param name="factory">Factory to create new object</param>
		/// <typeparam name="TFactory">Type of factory</typeparam>
		/// <returns>The newly-created object</returns>
		/// <exception cref="ArgumentNullException"><paramref name="sync"/> is <c>null</c></exception>
		/// <remarks>
		/// This method ensures that only one instance of an object is created, and subsequent requests
		/// will return the same object.
		/// <para>
		/// If <paramref name="value"/> holds a non-null value, this value is returned.
		/// If not, <paramref name="sync"/> is locked, an object is created using <paramref name="factory"/>,
		/// and the newly-created object is stored in <paramref name="value"/>.
		/// </para>
		/// </remarks>
		public static T GetOrCreate<T, TFactory>(ref T? value, object sync, TFactory factory)
			where T : class
			where TFactory : IFactory<T>
		{
			if (sync is null)
				throw new ArgumentNullException(nameof(sync));

			if (value != null)
				return value;
			else
			{
				lock (sync)
				{
					if (value != null)
						return value;
					else
						return (value = factory.Create());
				}
			}
		}
	}
}
