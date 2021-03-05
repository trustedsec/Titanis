using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies a <see cref="ValueListProvider"/> for a property or type.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class ValueListProviderAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new <see cref="ValueListProviderAttribute"/>.
		/// </summary>
		/// <param name="providerType">Type that provides a list of values</param>
		/// <exception cref="ArgumentNullException"><paramref name="providerType"/> is <see langword="null"/>.</exception>
		public ValueListProviderAttribute(Type providerType)
		{
			if (providerType is null) throw new ArgumentNullException(nameof(providerType));
			ProviderType = providerType;
		}

		/// <summary>
		/// Gets the type that provides a list of parameter values.
		/// </summary>
		public Type ProviderType { get; }
	}
}
