using System.Buffers.Binary;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Titanis.Cli;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp.Cli
{
	[TypeConverter(typeof(RegistryValueSpecConverter))]
	abstract class RegistryItemSpec
	{
		internal abstract void Accept(IRegistryItemVisitor visitor);
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
	}

	interface IRegistryItemVisitor
	{
		void Visit(RegistryKeySpec key);
		void Visit(RegistryValueSpec value);
	}

	sealed class RegistryKeySpec : RegistryItemSpec
	{
		public RegistryKeySpec(RegistryRootKey root, string? keyPath, string? className)
		{
			this.Root = root;
			this.KeyPath = keyPath;
		}
		public RegistryRootKey Root { get; }
		public string? KeyPath { get; }

		internal sealed override void Accept(IRegistryItemVisitor visitor) => visitor.Visit(this);
	}
	sealed class RegistryValueSpec : RegistryItemSpec
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

		internal sealed override void Accept(IRegistryItemVisitor visitor) => visitor.Visit(this);
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
			if (value is string str)
			{
				foreach (var c in str)
				{
					if (c is ':')
					{
						return ParseValueSpec(str, context);
					}
					else if (c is '/' or '\\')
					{
						return ParseKeySpec(str, c);
					}
				}

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
					RegistryValueType.MultiString => RegistryValueEncoding.Utf16,
					RegistryValueType.DwordLE or
					RegistryValueType.DwordBE => RegistryValueEncoding.Dword,
					RegistryValueType.Qword => RegistryValueEncoding.Qword,
					RegistryValueType.None or
					RegistryValueType.Binary or
					_ => RegistryValueEncoding.Hex
				};
			}

			RegistryValueEncoding enc;

			{
				var genc = m.Groups["e"];
				if (genc.Success)
				{
					var encText = genc.Value;
					if (!Enum.TryParse(encText, true, out enc))
						throw new FormatException($"Bad value encoding type: {encText}");
				}
				else
				{
					enc = defaultEnc;
				}
			}

			var valueName = m.Groups["n"].Value;
			valueName = StringHelper.UnescapeCStyle(valueName, true);

			var dataSpec = m.Groups["d"].Value;

			var services = (context?.Instance as IServiceProvider);
			var log = services?.GetService<ILog>();

			byte[] valueData = enc switch
			{
				RegistryValueEncoding.C => ParseCStringData(dataSpec, false),
				RegistryValueEncoding.Cz => ParseCStringData(dataSpec, true),
				RegistryValueEncoding.Utf16 => ParseUtfStringData(dataSpec, false),
				RegistryValueEncoding.Utf16Z => ParseUtfStringData(dataSpec, true),
				RegistryValueEncoding.Dword => ParseDword(dataSpec),
				RegistryValueEncoding.Qword => ParseQword(dataSpec),
				RegistryValueEncoding.Hex => BinaryHelper.ParseHexString(dataSpec),
				RegistryValueEncoding.File => ParseFileData(dataSpec, services?.GetService<IFileAccess>(), log),
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

			RegistryRootKey root = RegistryRootKey.Invalid;
			string? keyRelativePath = null;
			RegistryKeyCommand.ParseKeyPath(path, out root, out keyRelativePath);
			if (root == RegistryRootKey.Invalid)
				throw new FormatException($"Invalid root specified in {path}");

			return new RegistryKeySpec(root, keyRelativePath, null);
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

		private byte[] ParseFileData(string filePath, IFileAccess fileAccess, ILog? log)
		{
            ArgumentNullException.ThrowIfNull(fileAccess);

            filePath = fileAccess?.ResolveFsPath(filePath);

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

			uint u = (uint)Command.GetScalarParamConverter(typeof(ulong)).ConvertFrom(valueText);
			BinaryPrimitives.WriteUInt32LittleEndian(bytes, u);
			return bytes;
		}
	}
}
