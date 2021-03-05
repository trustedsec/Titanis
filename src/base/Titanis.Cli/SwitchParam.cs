using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Titanis.Cli
{
	public enum SwitchParamFlags
	{
		None = 0,
		Specified = 1,
		Set = 2,
	}

	/// <summary>
	/// Represents the value of a switch parameter.
	/// </summary>
	[TypeConverter(typeof(SwitchParamConverter))]
	public struct SwitchParam
	{

		public SwitchParam(SwitchParamFlags flags)
		{
			this.Flags = flags;
		}

		/// <summary>
		/// Gets a <see cref="SwitchParamFlags"/> value describing the state of the parameter.
		/// </summary>
		public SwitchParamFlags Flags { get; }
		/// <summary>
		/// The switch is specified.
		/// </summary>
		public bool IsSpecified => 0 != (this.Flags & SwitchParamFlags.Specified);
		/// <summary>
		/// The switch parameter is set.
		/// </summary>
		public bool IsSet => 0 != (this.Flags & SwitchParamFlags.Set);

		/// <summary>
		/// Gets the value of the switch.
		/// </summary>
		/// <param name="defaultValue">Default if not set</param>
		/// <returns>Value of switch if set; otherwise, <paramref name="defaultValue"/></returns>
		public bool GetValue(bool defaultValue) => this.IsSpecified ? this.IsSet : defaultValue;

		public static SwitchParam Parse(string text, bool asDefault)
		{
			var specFlag = asDefault ? SwitchParamFlags.None : SwitchParamFlags.Specified;

			if (bool.TryParse(text, out var b))
				return new SwitchParam(specFlag | (b ? SwitchParamFlags.Set : SwitchParamFlags.None));
			else if (TrueValues.Contains(text, StringComparer.OrdinalIgnoreCase))
				return new SwitchParam(specFlag | SwitchParamFlags.Set);
			else if (FalseValues.Contains(text, StringComparer.OrdinalIgnoreCase))
				return new SwitchParam(specFlag);
			else
				throw new ArgumentException($"The value '{text}' is not a valid switch value.  Valid values are true/false, yes/no, or on/off");

		}

		private static readonly string[] TrueValues = new string[]
		{
			"true",
			"yes",
			"on"
		};
		private static readonly string[] FalseValues = new string[]
		{
			"false",
			"no",
			"off"
		};
	}

	public class SwitchParamConverter : TypeConverter
	{
		public sealed override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (Type.GetTypeCode(sourceType) is TypeCode.Boolean or TypeCode.String)
				return true;
			else
				return base.CanConvertFrom(context, sourceType);
		}
		public sealed override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var asDefault = (context is null);
			var specFlag = asDefault ? SwitchParamFlags.Specified : SwitchParamFlags.None;
			if (value is Boolean b)
				return new SwitchParam(specFlag | SwitchParamFlags.Set);
			else if (value is string str)
			{
				return SwitchParam.Parse(str, asDefault);
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
