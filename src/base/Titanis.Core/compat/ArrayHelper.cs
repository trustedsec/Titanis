using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Titanis
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
	public static class ArrayHelper
	{
		public static TOutput[] ConvertAll<TSource, TOutput>(this TSource[] source, Func<TSource, TOutput> converter)
		{
			if (source is null)
				throw new ArgumentNullException(nameof(source));
			if (converter is null)
				throw new ArgumentNullException(nameof(converter));

			TOutput[] output = new TOutput[source.Length];
			for (int i = 0; i < source.Length; i++)
			{
				output[i] = converter(source[i]);
			}
			return output;
		}
	}
}
