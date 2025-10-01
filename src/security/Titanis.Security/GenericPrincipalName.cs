using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titanis.Security
{
	/// <summary>
	/// Represents a principal name.
	/// </summary>
	public sealed class GenericPrincipalName : SecurityPrincipalName
	{
		public GenericPrincipalName(PrincipalNameType nameType, string[] nameParts)
		{
			if (nameParts is null || nameParts.Length is 0 || nameParts.Contains(null))
				throw new ArgumentNullException(nameof(nameParts));

			this.NameType = nameType;
			this.NameParts = nameParts;
		}

		/// <inheritdoc/>
		public sealed override PrincipalNameType NameType { get; }
		public string[] NameParts { get; }

		private string? _str;
		/// <inheritdoc/>
		public sealed override string ToString() => (this._str ??= string.Join("/", this.NameParts));

		/// <inheritdoc/>
		public sealed override string[] GetNameParts() => this.NameParts;
		/// <inheritdoc/>
		public sealed override int NamePartCount => this.NameParts.Length;
		/// <inheritdoc/>
		public sealed override string GetNamePart(int index) => this.NameParts[index];
	}
}
