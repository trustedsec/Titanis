using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc
{
	// [C706] § 4.2.22.2 - The broadcast attribute
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class BroadcastAttribute : Attribute
	{
		/// <inheritdoc/>
		public sealed override bool Match(object? obj) => (obj is BroadcastAttribute);
	}
}
