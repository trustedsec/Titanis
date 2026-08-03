using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies the name of a file as a command line argument.
	/// </summary>
	[TypeConverter(typeof(FileSpecConverter))]
	public class FileSpec
	{
		public FileSpec(string fileName, bool resolved = false)
		{
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));
			FileName = fileName;
			IsResolved = resolved;
		}

		public string FileName { get; }
		public bool IsResolved { get; }

		public string? Extension => Path.GetExtension(this.FileName);

		/// <inheritdoc/>
		public sealed override string ToString() => this.FileName;
	}

	public class FileSpecConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
		}
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string str)
			{
				return new FileSpec(str);
			}
			else
				return base.ConvertFrom(context, culture, value);
		}
	}
}
