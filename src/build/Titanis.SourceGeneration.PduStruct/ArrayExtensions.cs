using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Titanis.SourceGen
{
	internal static class ArrayExtensions
	{
		public static ImmutableArray<TOutput> ConvertAll<TInput, TOutput>(this ImmutableArray<TInput> array, Converter<TInput, TOutput> converter)
		{
			var builder = ImmutableArray.CreateBuilder<TOutput>(array.Length);
			foreach (var elem in array)
			{
				builder.Add(converter(elem));
			}

			return builder.ToImmutable();
		}
	}
}
