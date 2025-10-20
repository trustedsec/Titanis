using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	/// <summary>
	/// Represents a type composed of fields.
	/// </summary>
	public abstract class Asn1ComplexType : Asn1ConstructedType
	{
		private protected Asn1ComplexType(IReadOnlyList<Asn1Field> members, int extensionIndex = -1)
		{
			this._members = members;
			this.HasDynamicMember = members.Any(m => !m.FieldType.HasStaticTag);
			this._extensionIndex = extensionIndex;
		}
		private protected Asn1ComplexType()
		{
			this._members = Array.Empty<Asn1Field>();
		}

		private IReadOnlyList<Asn1Field> _members;
		private int _extensionIndex = -1;

		/// <summary>
		/// Gets the members of the type.
		/// </summary>
		/// <returns></returns>
		public Asn1Field[] GetMembers() => this._members.ToArray();
		/// <summary>
		/// Gets the number of members.
		/// </summary>
		public int MemberCount => this._members.Count;
		/// <summary>
		/// Gets a value indicating whether the type has a member without a static tag.
		/// </summary>
		public bool HasDynamicMember { get; }
		/// <summary>
		/// Gets a value indicating whether the type is extensible.
		/// </summary>
		public abstract bool IsExtensible { get; }

		protected override void OnAttachedOverride()
		{
			base.OnAttachedOverride();

			if (this._members != null)
			{
				foreach (var member in this._members)
				{
					member.FieldType.OnAttached(this.DeclaringModule, this, member.Name);
				}
			}
		}
	}
}
