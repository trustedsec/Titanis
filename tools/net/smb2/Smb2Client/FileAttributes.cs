using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2.Cli
{
	/// <summary>
	/// Represents attributes of a windows File.
	/// </summary>
	[TypeConverter(typeof(FileAttributeSpecConverter))]
	public class FileAttributeSpec
	{
		public FileAttributeSpec(int value)
		{
			Attributes = (Winterop.FileAttributes)value;
		}

		public FileAttributeSpec(string value)
		{
			Attributes = ParseAttributeString(value);
		}

		public FileAttributeSpec(Winterop.FileAttributes value)
		{
			Attributes = value;
		}

		/// <summary>
		/// Gets the attributes
		/// </summary>
		public Winterop.FileAttributes Attributes{ get; }

		private static Winterop.FileAttributes ParseAttributeString(string attributes)
		{
			if (attributes == "")
			{
				return Winterop.FileAttributes.Normal; //If an explicit empty string is given, we intentionally send None
			}
			var invalidChars = new StringBuilder();
			var parsedAttributes = Winterop.FileAttributes.None;
			for (int i = 0; i < attributes.Length; i++)
			{
				switch (char.ToUpperInvariant(attributes[i]))
				{
					case 'R':
						parsedAttributes |= Winterop.FileAttributes.ReadOnly;
						break;
					case 'H':
						parsedAttributes |= Winterop.FileAttributes.Hidden;
						break;
					case 'S':
						parsedAttributes |= Winterop.FileAttributes.System;
						break;
					case 'A':
						parsedAttributes |= Winterop.FileAttributes.Archive;
						break;
					case 'T':
						parsedAttributes |= Winterop.FileAttributes.Temporary;
						break;
					case 'F':
						parsedAttributes |= Winterop.FileAttributes.SparseFile;
						break;
					case 'M':
						parsedAttributes |= Winterop.FileAttributes.ReparsePoint;
						break;
					case 'C':
						parsedAttributes |= Winterop.FileAttributes.Compressed;
						break;
					case 'O':
						parsedAttributes |= Winterop.FileAttributes.Offline;
						break;
					case 'I':
						parsedAttributes |= Winterop.FileAttributes.NotContentIndexed;
						break;
					case 'E':
						parsedAttributes |= Winterop.FileAttributes.Encrypted;
						break;
					case 'V':
						parsedAttributes |= Winterop.FileAttributes.IntegrityStream;
						break;
					case 'X':
						parsedAttributes |= Winterop.FileAttributes.NoScrubData;
						break;
					default:
						invalidChars.Append(attributes[i]);
						continue;
				}
			}
			if (invalidChars.Length > 0)
			{
				throw new ArgumentException($"Invalid attribute character(s) '{invalidChars}' in attribute string '{attributes}'");
			}
			return (parsedAttributes == Winterop.FileAttributes.None) ? Winterop.FileAttributes.Normal : parsedAttributes;
		}

	}


	/// <summary>
	/// Provides type conversion between <see cref="FileAttributeSpec"/> and <see cref="string"/>.
	/// </summary>
	public class FileAttributeSpecConverter : TypeConverter
	{

		/// <inheritdoc/>
		/// <remarks>
		/// This implementation only supports conversion from <see cref="string"/>.
		/// </remarks>
		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <inheritdoc/>
		/// <remarks>
		/// This implementation only supports conversion from <see cref="string"/>.
		/// </remarks>
		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
			{
				int intValue = 0;
				if (str.StartsWith("0x", StringComparison.InvariantCulture) && int.TryParse(str.Substring(2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out intValue))
				{
					return new FileAttributeSpec(intValue);
				}
				else if (int.TryParse(str, NumberStyles.Integer, CultureInfo.InvariantCulture, out intValue))
				{
					return new FileAttributeSpec(intValue);
				}
				return new FileAttributeSpec(str);
			}

			return base.ConvertFrom(context, culture, value);
		}
	}
}
