using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Provides extension methods for <see cref="IEnumerable{T}"/>.
	/// </summary>
	public static class Enumerable
	{
		// TODO: Review why this method is needed.
		// It is used in Titanis.Net.Analysis
		[return: MaybeNull]
		public static IEnumerable<TElement>? SelectManyOrNull<TSource, TElement>(
			this IEnumerable<TSource>? enumerable,
			Func<TSource, IEnumerable<TElement>> selector)
		{
			return enumerable?.SelectMany(selector);
		}
	}
}
