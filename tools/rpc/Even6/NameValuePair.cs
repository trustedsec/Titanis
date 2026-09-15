using System.ComponentModel;
using System.Globalization;

namespace Even6;

[TypeConverter(typeof(NameValuePairConverter))]
public class NameValuePair
{
	public NameValuePair(string name, string value, bool isApproximate = false)
	{
		this.Name = name;
		this.Value = value;
		this.IsApproximate = isApproximate;
	}

	public string Name { get; }
	public string Value { get; }
	public bool IsApproximate { get; }
}

public class NameValuePairConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
	{
		return (sourceType == typeof(string)) && base.CanConvertFrom(context, sourceType);
	}
	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
	{
		if (value is string str)
		{
			int isep = str.IndexOf('=');
			if (isep <= 0)
				throw new FormatException($"Expected name=value, found {str}");

			string valuePart = str.Substring(isep + 1);
			bool isApprox = (str[isep - 1] == '~');
			if (isApprox)
				isep--;
			return new NameValuePair(str.Substring(0, isep), valuePart, isApprox);
		}
		else
			return base.ConvertFrom(context, culture, value);
	}
}