using System.Buffers.Binary;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Cli.Registry
{
	[TypeConverter(typeof(RegistryValueSpecConverter))]
	public abstract class RegistryItemSpec
	{
		public abstract void Accept(IRegistryItemVisitor visitor);
	}

	public enum RegistryValueEncoding
	{
		Unspecified = 0,
		C,
		Cz,
		Utf16,
		Utf16Z,
		Dword,
		Qword,
		Hex,
		File,
		Sddl,
		Multi
	}

	public interface IRegistryItemVisitor
	{
		void Visit(RegistryKeySpec key);
		void Visit(RegistryValueSpec value);
	}

	public sealed class RegistryKeySpec : RegistryItemSpec
	{
		public RegistryKeySpec(PredefinedKey root, string? keyPath, string? className)
		{
			this.Root = root;
			this.KeyPath = keyPath;
		}
		public PredefinedKey Root { get; }
		public string? KeyPath { get; }

		public sealed override void Accept(IRegistryItemVisitor visitor) => visitor.Visit(this);

		public override string ToString() => $"{Root}\\{KeyPath}";

	}
	public sealed class RegistryValueSpec : RegistryItemSpec
	{
		public RegistryValueSpec(string valueName, RegistryValueType valueType, byte[] valueData)
		{
			this.ValueName = valueName;
			this.ValueType = valueType;
			this.ValueData = valueData;
		}

		public string? ValueName { get; }
		public RegistryValueType ValueType { get; }
		public byte[] ValueData { get; }

		public sealed override void Accept(IRegistryItemVisitor visitor) => visitor.Visit(this);
	}

	partial class RegistryValueSpecConverter : TypeConverter
	{
		private static readonly Regex rgxValueSpec = SpecRegex();

		[GeneratedRegex(@"^(?<t>[^;:]+)(;(?<e>[^:]+))?:(?<n>([^\\=]|(\\.))*)=(?<d>.*)$")]
		private static partial Regex SpecRegex();

		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
			=> (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);

		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			var nameOnly = (context as ParameterConverterContext)?.PropertyDescriptor?.Attributes[typeof(ValueNameOnlyAttribute)] != null;
			if (value is string str)
			{
				foreach (var c in str)
				{
					if (c is ':' && !nameOnly)
					{
						return ParseValueSpec(str, context);
					}
					else if (c is '/' or '\\')
					{
						return ParseKeySpec(str, c);
					}
				}
				if (nameOnly)
					return new RegistryValueSpec(str, RegistryValueType.None, Array.Empty<byte>());

				throw new FormatException($"The argument '{str}' does not appear to be a valid key name or value specification.");
			}
			else
				return base.ConvertFrom(context, culture, value);
		}

		private object? ParseValueSpec(string valueSpec, ITypeDescriptorContext? context)
		{
			var m = rgxValueSpec.Match(valueSpec);
			if (!m.Success)
				throw new FormatException($"The text '{valueSpec}' is not a valid value specification.");

			var typeName = m.Groups["t"].Value;
			if (typeName.StartsWith("REG_", StringComparison.OrdinalIgnoreCase))
				typeName = typeName.Substring(4);

			RegistryValueType valueType;
			RegistryValueEncoding defaultEnc;
			if (typeName.StartsWith("0x"))
			{
				int typeValue = int.Parse(typeName.AsSpan(2), NumberStyles.HexNumber);
				valueType = (RegistryValueType)typeValue;
				defaultEnc = RegistryValueEncoding.Hex;
			}
			else if (uint.TryParse(typeName, out var decValue))
			{
				valueType = (RegistryValueType)decValue;
				defaultEnc = RegistryValueEncoding.Hex;
			}
			else
			{
				if (Enum.TryParse<RegistryValueTypeAlt>(typeName, true, out var valueTypeAlt))
				{
					valueType = (RegistryValueType)valueTypeAlt;
				}
				else if (Enum.TryParse(typeName, true, out valueType))
				{
				}
				else
					throw new FormatException($"The type '{typeName}' is not a supported registry value type.");

				defaultEnc = valueType switch
				{
					RegistryValueType.ExpandString or
					RegistryValueType.String => RegistryValueEncoding.Utf16Z,
					RegistryValueType.MultiString => RegistryValueEncoding.Multi,
					RegistryValueType.DwordLE or
					RegistryValueType.DwordBE => RegistryValueEncoding.Dword,
					RegistryValueType.Qword => RegistryValueEncoding.Qword,
					RegistryValueType.None or
					RegistryValueType.Binary or
					_ => RegistryValueEncoding.Hex
				};
			}

			RegistryValueEncoding enc;
			char multiSep = ',';
			{
				var genc = m.Groups["e"];
				if (genc.Success)
				{
					var encText = genc.Value;
					if (encText.ToLower().StartsWith("multi"))
					{
						multiSep = (encText.Length > 5) ? encText[5] : ',';
						enc = RegistryValueEncoding.Multi;
					}
					else if (!Enum.TryParse(encText, true, out enc))
					{
						throw new FormatException($"Bad value encoding type: {encText}");
					}
				}
				else
				{
					enc = defaultEnc;
				}
			}

			var valueName = m.Groups["n"].Value;
			valueName = StringHelper.UnescapeCStyle(valueName, true);

			var dataSpec = m.Groups["d"].Value;

			var log = context?.GetService<ILog>();

			byte[] valueData = enc switch
			{
				RegistryValueEncoding.C => ParseCStringData(dataSpec, false),
				RegistryValueEncoding.Cz => ParseCStringData(dataSpec, true),
				RegistryValueEncoding.Utf16 => ParseUtfStringData(dataSpec, false),
				RegistryValueEncoding.Utf16Z => ParseUtfStringData(dataSpec, true),
				RegistryValueEncoding.Multi => ParseMultiStrData(dataSpec, multiSep),
				RegistryValueEncoding.Dword => ParseDword(dataSpec),
				RegistryValueEncoding.Qword => ParseQword(dataSpec),
				RegistryValueEncoding.Hex => BinaryHelper.ParseHexString(dataSpec),
				RegistryValueEncoding.File => ParseFileData(new FileSpec(dataSpec), context?.GetService<IFileAccess>(), log),
				RegistryValueEncoding.Sddl => ParseSecurityDescriptor(dataSpec),
				RegistryValueEncoding.Unspecified or _ => throw new FormatException("No encoding type specified"),
			};

			return new RegistryValueSpec(valueName, valueType, valueData);
		}

		private byte[] ParseSecurityDescriptor(string dataSpec)
		{
			var sd = (SecurityDescriptor)(new SecurityDescriptorConverter()).ConvertFrom(dataSpec);
			return sd.ToByteArray();
		}

		private RegistryItemSpec ParseKeySpec(string path, char pathSep)
		{
			if (pathSep != '\\')
				path = path.UnescapeCStyle().Replace(pathSep, '\\');

			var keyPath = RegistryPath.Parse(path);
			return new RegistryKeySpec(keyPath.Root, keyPath.KeyPath, null);
		}

		private static byte[] ParseCStringData(string valueText, bool nullTerminate)
		{
			valueText = valueText.UnescapeCStyle();
			if (nullTerminate && !valueText.EndsWith('\0'))
				valueText += '\0';

			return Encoding.Unicode.GetBytes(valueText);
		}

		private static byte[] ParseUtfStringData(string valueText, bool nullTerminate)
		{
			if (nullTerminate && !valueText.EndsWith('\0'))
				valueText += '\0';

			return Encoding.Unicode.GetBytes(valueText);
		}

		//TODO: Provide an option to read MULTI_SZ entries from a text file
		private static byte[] ParseMultiStrData(string value, char separator)
		{
			var values = value.Split(separator);
			return Encoding.Unicode.GetBytes(string.Join('\0', values) + "\0\0");
		}

		private byte[] ParseFileData(FileSpec filePath, IFileAccess fileAccess, ILog? log)
		{
			ArgumentNullException.ThrowIfNull(fileAccess);

			log.WriteDiagnostic($"Reading file {filePath}");
			var data = fileAccess.ReadAllBytesFrom(filePath);
			return data;
		}

		private byte[] ParseDword(string valueText)
		{
			byte[] bytes = new byte[4];

			uint u = (uint)Command.GetScalarParamConverter(typeof(uint)).ConvertFrom(valueText);
			BinaryPrimitives.WriteUInt32LittleEndian(bytes, u);
			return bytes;
		}

		private byte[] ParseQword(string valueText)
		{
			byte[] bytes = new byte[8];

			ulong u = (ulong)Command.GetScalarParamConverter(typeof(ulong)).ConvertFrom(valueText);
			BinaryPrimitives.WriteUInt64LittleEndian(bytes, u);
			return bytes;
		}
	}
}
