using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc
{
	// [C706] Š 4.2.22.1 - The idempotent Attribute
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class IdempotentAttribute : Attribute
	{
		/// <inheritdoc/>
		public sealed override bool Match(object? obj) => (obj is IdempotentAttribute);
	}
}
