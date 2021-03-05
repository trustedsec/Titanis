using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Provides values for a parameter.
	/// </summary>
	public abstract class ValueListProvider
	{
		/// <summary>
		/// Gets a list of values for a parameter.
		/// </summary>
		/// <param name="parameter">Parameter</param>
		/// <param name="command">Command instance</param>
		/// <param name="context">Command metadata context</param>
		/// <returns>An array of possible parameter values.</returns>
		public abstract Array? GetValueListFor(ParameterMetadata parameter, object? command, CommandMetadataContext context);
	}

	sealed class EnumListProvider : ValueListProvider
	{
		public sealed override Array GetValueListFor(ParameterMetadata parameter, object? command, CommandMetadataContext context)
		{
			var elemType = parameter.ElementType;
			Debug.Assert(elemType.IsEnum);

			return context.Resolver.GetEnumValues(elemType);
		}
	}
}
