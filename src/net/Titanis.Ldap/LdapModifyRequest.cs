using Lightweight_Directory_Access_Protocol_V3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop.Security;

namespace Titanis.Ldap
{
	/// <summary>
	/// Specifies a change operation.
	/// </summary>
	public enum LdapChangeType
	{
		/// <summary>
		/// Value(s) are added to the attribute.
		/// </summary>
		Add = ModifyRequest_Tagged6_Changes_Element_Operation.Add,
		/// <summary>
		/// Value(s) replace the current value(s) of the attribute.
		/// </summary>
		Replace = ModifyRequest_Tagged6_Changes_Element_Operation.Replace,
		/// <summary>
		/// Value(s) are removed from the current attribute value(s).
		/// </summary>
		Delete = ModifyRequest_Tagged6_Changes_Element_Operation.Delete,
	}

	/// <summary>
	/// Describes a change to an attribute.
	/// </summary>
	public class LdapAttributeChange
	{
		/// <summary>
		/// Initializes a new <see cref="LdapAttributeChange"/>.
		/// </summary>
		/// <param name="name">Name of attribute to change</param>
		/// <param name="value">Value to change</param>
		/// <param name="changeType">Change operation</param>
		/// <remarks>
		/// <paramref name="value"/> may either be a scalar or an array.  If it is an array, it is interpreted as a multi-valued attribute.
		/// </remarks>
		public LdapAttributeChange(string name, object? value, LdapChangeType changeType)
		{
			Name = name;
			if (value is object[] values)
				this.Values = values;
			else if (value != null)
				this.Values = [value];
			else
				this.Values = Array.Empty<object>();

			this.ChangeType = changeType;
		}

		/// <summary>
		/// Gets the name of the attribute to change.
		/// </summary>
		public string Name { get; }
		/// <summary>
		/// Gets the values to change.
		/// </summary>
		public object[]? Values { get; }
		/// <summary>
		/// Gets the change operation to perform.
		/// </summary>
		public LdapChangeType ChangeType { get; }
	}

	public interface ILdapModifyRequest
	{
		void AddChange(string attributeName, object[] values, LdapChangeType changeType);
	}

	/// <summary>
	/// Describes a modification to a directory entry.
	/// </summary>
	public class LdapModifyRequest : ILdapModifyRequest
	{
		public LdapModifyRequest(LdapDistinguishedName dn)
		{
			this.DistinguishedName = dn;
		}

		public LdapDistinguishedName DistinguishedName { get; }
		public SecurityInfo? SecuritySections { get; set; }

		internal readonly List<LdapAttributeChange> _changes = new List<LdapAttributeChange>();
		public IReadOnlyList<LdapAttributeChange> Changes => this._changes;

		public void AddChange(string attributeName, object[] values, LdapChangeType changeType)
			=> this.AddChange(new LdapAttributeChange(attributeName, values, changeType));

		public LdapModifyRequest AddChange(LdapAttributeChange change)
		{
			ArgumentNullException.ThrowIfNull(change);
			this._changes.Add(change);
			return this;
		}

		private List<KeyValuePair<string, object>> _addValues;
		public LdapModifyRequest AddValue(string attribute, object value)
		{
			ArgumentNullException.ThrowIfNull(value);

			(this._addValues ??= new List<KeyValuePair<string, object>>()).Add(new KeyValuePair<string, object>(attribute, value));
			this._changes.Add(new LdapAttributeChange(attribute, value, LdapChangeType.Add));
			return this;
		}

		private List<KeyValuePair<string, object>> _replaceValues;
		public LdapModifyRequest ReplaceValue(string attribute, object value)
		{
			ArgumentNullException.ThrowIfNull(value);

			(this._replaceValues ??= new List<KeyValuePair<string, object>>()).Add(new KeyValuePair<string, object>(attribute, value));
			this._changes.Add(new LdapAttributeChange(attribute, value, LdapChangeType.Replace));
			return this;
		}

		private List<KeyValuePair<string, object>> _deleteValues;
		public LdapModifyRequest DeleteValue(string attribute, object value)
		{
			ArgumentNullException.ThrowIfNull(value);

			(this._deleteValues ??= new List<KeyValuePair<string, object>>()).Add(new KeyValuePair<string, object>(attribute, value));
			this._changes.Add(new LdapAttributeChange(attribute, value, LdapChangeType.Delete));
			return this;
		}
	}
}
