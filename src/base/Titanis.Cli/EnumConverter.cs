using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Titanis.Cli
{
	public class EnumConverter : TypeConverter
	{
		public EnumConverter(Type enumType)
		{
			EnumType = enumType;
		}

		public Type EnumType { get; }

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string str)
				return Enum.Parse(this.EnumType, str, true);
			return base.ConvertFrom(context, culture, value);
		}
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType);
		}
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return base.CanConvertFrom(context, sourceType);
		}
	}
}
