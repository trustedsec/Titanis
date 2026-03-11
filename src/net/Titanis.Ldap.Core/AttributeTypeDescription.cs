using System.Text;
using System.Text.RegularExpressions;

namespace Titanis.Ldap
{
	[Flags]
	public enum AttributeTypeDescriptionFlags
	{
		None = 0,
		Obsolete = 1,
		SingleValue = 2,
		Collective = 4,
		NotUserModifiable = 8,

	}

	// [RFC 4512] § 4.1.2
	// [MS-ADTS] § 3.1.1.3.1.1.1
	// NOTE: This implementation supports the full [RFC 4512] definition; [MS-ADTS] only supports a subset.
	/// <summary>
	/// Describes an attribute type.
	/// </summary>
	public partial class AttributeTypeDescription
	{
		public AttributeTypeDescription(
			AttributeTypeDescriptionFlags flags,
			string oid,
			string[]? names,
			string? description = null,
			string? supertype = null,
			string? equalityRule = null,
			string? orderingRule = null,
			string? substringRule = null,
			string? syntaxId = null,
			int? maxLength = null,
			string? usage = null,
			LdapSyntax? syntax = null
			)
		{
			Flags = flags;
			Oid = oid;
			Names = names ?? Array.Empty<string>();
			Description = description;
			Supertype = supertype;
			EqualityRule = equalityRule;
			OrderingRule = orderingRule;
			SubstringRule = substringRule;
			SyntaxId = syntaxId;
			Usage = usage;
			Syntax = syntax;
		}

		public AttributeTypeDescription(
			AttributeTypeDescriptionFlags flags,
			string name
			)
		{
			ArgumentNullException.ThrowIfNull(name);
			this.Flags = flags;
			this.Names = [name];
			this.Oid = name;
		}

		public AttributeTypeDescriptionFlags Flags { get; }
		public bool IsObsolete => (this.Flags & AttributeTypeDescriptionFlags.Obsolete) != 0;
		public bool IsSingleValued => (this.Flags & AttributeTypeDescriptionFlags.SingleValue) != 0;
		public bool IsCollective => (this.Flags & AttributeTypeDescriptionFlags.Collective) != 0;
		public bool IsNotUserModifiabled => (this.Flags & AttributeTypeDescriptionFlags.NotUserModifiable) != 0;

		public string Oid { get; }
		public string? Name => this.Names?.FirstOrDefault();
		private byte[]? _encodedName;
		public byte[]? EncodedName => (this._encodedName ??= this.EncodeName());

		private byte[]? EncodeName()
		{
			var name = this.Name;
			if (name != null)
				return Encoding.UTF8.GetBytes(name);
			else
				return Encoding.UTF8.GetBytes(this.Oid);
		}

		public string[]? Names { get; }
		public string? Description { get; }
		public string? Supertype { get; }
		public string? EqualityRule { get; }
		public string? OrderingRule { get; }
		public string? SubstringRule { get; }
		public string? SyntaxId { get; }
		public string? Usage { get; }
		public LdapSyntax? Syntax { get; }

		private string? _str;
		public sealed override string ToString() => (this._str ??= this.BuildString());

		private static string FormatQString(string str)
		{
			str = $"'{str.Replace(@"\", @"\5C").Replace("'", @"\27")}'";
			return str;
		}

		private string? BuildString()
		{
			StringBuilder sb = new StringBuilder("(");
			sb.Append(this.Oid);
			if (!this.Names.IsNullOrEmpty())
			{
				sb.Append(" NAME ");
				if (this.Names.Length == 1)
				{
					sb.Append($"'{this.Names[0]}'");
				}
				else
				{
					sb.Append('(');
					foreach (var name in this.Names)
					{
						sb.Append($" '{name}'");
					}
				}
			}
			if (this.Description != null)
				sb.Append(FormatQString(this.Description));
			if (this.IsObsolete)
				sb.Append(" OBSOLETE");
			if (this.Supertype != null)
				sb.Append($" SUP {this.Supertype}");
			if (this.EqualityRule != null)
				sb.Append($" EQUALITY {this.EqualityRule}");
			if (this.OrderingRule != null)
				sb.Append($" ORDERING {this.OrderingRule}");
			if (this.SubstringRule != null)
				sb.Append($" SUBSTR {this.SubstringRule}");
			if (this.SyntaxId != null)
				sb.Append($" SYNTAX {this.Syntax}");
			if (this.IsSingleValued)
				sb.Append($" SINGLE-VALUE");
			if (this.IsNotUserModifiabled)
				sb.Append($" NO-USER-MODIFICATION");

			sb.Append(')');
			return sb.ToString();
		}

		// qdescr ::= [a-zA-Z][a-zA-Z0-9\-]*
		// qstring ::= (\\27|\\5[cC]|[^\\'])*
		private static readonly Regex rgxDescr = DescrRegex();

		public static AttributeTypeDescription Parse(string str)
		{
			ArgumentException.ThrowIfNullOrEmpty(str);

			var m = rgxDescr.Match(str);
			if (!m.Success)
				throw new ArgumentException($"The string is not a valid attribute type description.", nameof(str));

			AttributeTypeDescriptionFlags flags = 0;
			if (m.Groups["obs"].Success) flags |= AttributeTypeDescriptionFlags.Obsolete;
			if (m.Groups["single"].Success) flags |= AttributeTypeDescriptionFlags.SingleValue;
			if (m.Groups["coll"].Success) flags |= AttributeTypeDescriptionFlags.Collective;
			if (m.Groups["nomod"].Success) flags |= AttributeTypeDescriptionFlags.NotUserModifiable;

			return new AttributeTypeDescription(
				flags,
				m.Groups["oid"].Value,
				m.Groups["name"].Captures.Select(r => r.Value).ToArray(),
				m.Groups["desc"].CapturedValueOrNull(),
				m.Groups["sup"].CapturedValueOrNull(),
				m.Groups["eq"].CapturedValueOrNull(),
				m.Groups["ord"].CapturedValueOrNull(),
				m.Groups["substr"].CapturedValueOrNull(),
				m.Groups["syntax"].CapturedValueOrNull(),
				m.Groups["maxlen"].CapturedInt32OrNull(),
				m.Groups["usage"].CapturedValueOrNull()
				);
		}

		[GeneratedRegex(@"^\(\s*(?<oid>\d+(\.\d+)*)(\s+NAME\s+('(?<name>[a-zA-Z][a-zA-Z0-9\-]*)'|(\((\s*'(?<name>[a-zA-Z][a-zA-Z0-9\-]*)')*\))))?(\s+DESC\s+'(?<desc>(\\27|\\5[cC]|[^\\'])*)')?(\s+(?<obs>OBSOLETE))?(\s+SUP\s+(?<sup>(\d+(\.\d+)*)|([a-zA-Z][a-zA-Z0-9\-]*)))?(\s+EQUALITY\s+(?<eq>(\d+(\.\d+)*)|([a-zA-Z][a-zA-Z0-9\-]*)))?(\s+ORDERING\s+(?<ord>(\d+(\.\d+)*)|([a-zA-Z][a-zA-Z0-9\-]*)))?(\s+SUBSTR\s+(?<substr>(\d+(\.\d+)*)|([a-zA-Z][a-zA-Z0-9\-]*)))?(\s+SYNTAX\s+(?<syntax>\d+(\.\d+)*)(\{(?<maxlen>\d+)\})?)?(?<single>\s+SINGLE-VALUE)?(?<coll>\s+COLLECTIVE)?(?<nomod>\s+NO-USER-MODIFICATION)?(\s+USAGE\s+(?<usage>\w+))?\s*\)")]
		private static partial Regex DescrRegex();
	}
}
