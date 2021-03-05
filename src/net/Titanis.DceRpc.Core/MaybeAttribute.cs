using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc
{
	// [C706] § 4.2.22.3 - The maybe Attribute
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class MaybeAttribute : Attribute
	{
		/// <inheritdoc/>
		public sealed override bool Match(object? obj) => (obj is MaybeAttribute);
	}
}
