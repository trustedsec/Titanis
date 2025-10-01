using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security
{
	/// <summary>
	/// Represents a principal name.
	/// </summary>
	public sealed class SimplePrincipalName : SecurityPrincipalName
	{
		public SimplePrincipalName(string name)
		{
			Name = name;
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		public string Name { get; }
		/// <inheritdoc/>
		public sealed override string ToString() => this.Name;
		/// <inheritdoc/>
		public override PrincipalNameType NameType => PrincipalNameType.Principal;

		/// <inheritdoc/>
		public override string[] GetNameParts() => new string[] { this.Name };

		/// <inheritdoc/>
		public sealed override int NamePartCount => 1;
		/// <inheritdoc/>
		public sealed override string GetNamePart(int index) => index switch
		{
			0 => this.Name,
			_ => throw new ArgumentOutOfRangeException(nameof(index))
		};

	}
}
