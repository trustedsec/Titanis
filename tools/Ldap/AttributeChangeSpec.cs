using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

public enum AttributeEncoding
{
	Unspecified = 0,
	File,
	Hex,
	Base64,
}

[TypeConverter(typeof(AttributeChangeSpecConverter))]
class AttributeChangeSpec
{
	public AttributeChangeSpec(string name, LdapChangeType changeType, AttributeEncoding encoding, string value)
	{
		Name = name;
		ChangeType = changeType;
		Encoding = encoding;
		Value = value;
	}

	public string Name { get; }
	public AttributeEncoding Encoding { get; set; }
	public LdapChangeType ChangeType { get; }
	public string Value { get; }
}

partial class AttributeChangeSpecConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		=> sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
	{
		if (value is string str)
		{
			var m = rgxSpec.Match(str);
			if (!m.Success)
				throw new FormatException($"Invalid change specification '{str}'.  The change spec must be formatted as <name>?=<value> where ?= is one of =, +=, or -=.");
			var changeType = m.Groups["op"].Value switch
			{
				"=" => LdapChangeType.Replace,
				"+=" => LdapChangeType.Add,
				"-=" => LdapChangeType.Delete
			};
			var name = m.Groups["n"].Value;
			var encName = m.Groups["enc"].Value;
			var enc = encName switch
			{
				"" => AttributeEncoding.Unspecified,
				"file" => AttributeEncoding.File,
				"hex" => AttributeEncoding.Hex,
				"base64" => AttributeEncoding.Base64,
				_ => throw new FormatException($"Encoding type '{encName}' is not supported.  Use 'file' or 'hex'.")
			};
			var newValue = m.Groups["v"].Value;
			return new AttributeChangeSpec(name, changeType, enc, newValue);
		}
		return base.ConvertFrom(context, culture, value);
	}

	private static readonly Regex rgxSpec = SpecRegex();

	[GeneratedRegex(@"^(?<n>(\w|-\w)+)(:(?<enc>\w+))?(?<op>=|-=|\+=)(?<v>.*)$")]
	private static partial Regex SpecRegex();
}