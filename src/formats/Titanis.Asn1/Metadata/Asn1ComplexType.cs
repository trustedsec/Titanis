using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public abstract class Asn1ComplexType : Asn1ConstructedType
	{
		private IList<Asn1Field> _members;
		private int _extensionIndex = -1;

		public Asn1Field[] GetMembers() => this._members.ToArray();
		public int MemberCount => this._members.Count;

		public bool HasDynamicMember { get; private set; }

		private protected Asn1ComplexType(IList<Asn1Field> members)
		{
			this._members = members;
			this.HasDynamicMember = members.Any(m => !m.FieldType.HasStaticTag);
		}
		private protected Asn1ComplexType()
		{
			this._members = new List<Asn1Field>();
		}

		internal void AddField(Asn1Field field)
		{
			if (field is null)
				throw new ArgumentNullException(nameof(field));
			this._members.Add(field);
			this.HasDynamicMember |= !field.FieldType.HasStaticTag;
		}

		internal void SetExtension()
		{
			this._extensionIndex = this._members.Count;
		}

		protected override void OnAttachedOverride()
		{
			base.OnAttachedOverride();

			if (this._members != null)
			{
				foreach (var member in this._members)
				{
					member.FieldType.OnAttaching(this.Module, member.Name);
					member.FieldType.OnAttached(this, member.Name);
				}
			}
		}
	}
}
