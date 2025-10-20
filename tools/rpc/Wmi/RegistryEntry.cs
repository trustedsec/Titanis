using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Cli;
using Titanis.Security.Kerberos;
using Titanis.Winterop.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wmi.Registry
{
	/// <summary>
	/// Specifies the data types used in the Windows registry.
	/// </summary>
	public enum RegistryValueKind : uint
	{
		REG_NONE = 0,
		REG_SZ = 1,
		REG_EXPAND_SZ = 2,
		REG_BINARY = 3,
		REG_DWORD = 4,
		REG_MULTI_SZ = 7,
		REG_QWORD = 11
	}


	public interface IHaveUInt64Value
	{
		ulong UInt64Value { get; }
	}

	/// <summary>
	/// Represents the data within a registry value, encapsulating both the data type and its associated value.
	/// </summary>
	/// <remarks>
	/// This type effectively serves as a variant within the context of the registry.
	/// </remarks>
	public abstract class RegistryData : IFormattable
	{
		/// <summary>
		/// Gets a <see cref="RegistryValueKind"/> value specifying the kind of data.
		/// </summary>
		public abstract RegistryValueKind Kind { get; }
		/// <summary>
		/// Gets the data associated with the registry entry.
		/// </summary>
		public abstract object UntypedValue { get; }

		/// <summary>
		/// Creates a <see cref="RegistryData"/> instance representing a REG_MULTI_SZ registry value.
		/// </summary>
		/// <remarks>This method splits the input string into an array of substrings based on the specified separator.</remarks>
		/// <param name="multiString">Array of strings to use as multisz value.</param>
		public static RegistryData CreateRegMultiString(string[] multiString) => new RegistryMultiString(multiString);

		/// <summary>
		/// Creates a <see cref="RegistryData"/> instance representing a REG_BINARY registry value.
		/// </summary>
		public static RegistryData CreateBinary(byte[] data) => new RegistryBinary(data, RegistryValueKind.REG_BINARY);
		/// <summary>
		/// Creates a <see cref="RegistryData"/> instance representing a REG_SZ registry value.
		/// </summary>
		public static RegistryData CreateString(string data) => new RegistryString(data);
		/// <summary>
		/// Creates a <see cref="RegistryData"/> instance representing a REG_EXPAND_SZ registry value.
		/// </summary>
		public static RegistryData CreateExpandableString(string data) => new RegistryExpandableString(data);
		/// <summary>
		/// Creates a <see cref="RegistryData"/> instance representing a REG_DWORD registry value.
		/// </summary>
		public static RegistryData CreateDword(uint data) => new RegistryDword(data);
		/// <summary>
		/// Creates a new <see cref="RegistryData"/> instance representing a REG_QWORD registry value.
		/// </summary>
		public static RegistryData CreateDword(ulong data) => new RegistryQword(data);

		const int WrapThreshold = 77;
		const string Indent = "  ";

		/// <summary>
		/// Exports a value as its hex representation.
		/// </summary>
		/// <param name="writer">Target writer</param>
		/// <param name="kind"></param>
		/// <param name="bytes">Bytes to write</param>
		/// <param name="lineOffset">Starting line offset</param>
		/// <remarks>
		/// <paramref name="lineOffset"/> is used to determine when to wrap lines.
		/// This method writes a line break after the value.
		/// </remarks>
		protected static void ExportAsHexTo(TextWriter writer, RegistryValueKind kind, ReadOnlySpan<byte> bytes, int lineOffset)
		{
			{
				string hexTypeString = GetExportTagForKind(kind);

				writer.Write(hexTypeString);
				lineOffset += hexTypeString.Length;
			}

			if (bytes.Length > 0)
			{
				// The first byte is on the same line as the value, regardless of the length
				writer.Write("{0:x2}", bytes[0]);
				lineOffset += 2;

				for (int i = 1; i < bytes.Length; i++)
				{
					writer.Write(',');
					lineOffset++;
					if (lineOffset >= WrapThreshold)
					{
						writer.WriteLine("\\");
						writer.Write(Indent);
						lineOffset = Indent.Length;
					}

					writer.Write("{0:x2}", bytes[i]);
					lineOffset += 2;
				}
			}

			writer.WriteLine();
		}

		private static string GetExportTagForKind(RegistryValueKind kind)
		{
			return kind switch
			{
				RegistryValueKind.REG_BINARY => "hex:",
				_ => $"hex({((int)kind):x}):"
			};
		}

		/// <summary>
		/// Exports the data as in a <c>.reg</c> file.
		/// </summary>
		/// <param name="writer">Target writer</param>
		/// <param name="lineOffset">Line offset of <paramref name="writer"/></param>
		public abstract void ExportTo(TextWriter writer, int lineOffset);
		/// <inheritdoc/>
		public sealed override string ToString() => this.ToString(null, null);
		/// <inheritdoc/>
		public abstract string ToString(string? format, IFormatProvider? formatProvider);
	}

	/// <summary>
	/// Represents a <see cref="RegistryValueKind.REG_QWORD"/> value.
	/// </summary>
	public sealed class RegistryQword : RegistryData, IHaveUInt64Value
	{
		/// <summary>
		/// Initializes a new <see cref="RegistryQword"/>.
		/// </summary>
		/// <param name="value">Value</param>
		public RegistryQword(ulong value)
		{
			this._value = value;
		}

		/// <inheritdoc/>
		public sealed override RegistryValueKind Kind => RegistryValueKind.REG_QWORD;

		private ulong _value;
		/// <summary>
		/// Gets the value as a <see langword="ulong"/>.
		/// </summary>
		public ulong Value => _value;
		/// <inheritdoc/>
		public override object UntypedValue => this.Value;

        /// <inheritdoc/>
		ulong IHaveUInt64Value.UInt64Value => this.Value;

        /// <inheritdoc/>
        public override string ToString(string? format, IFormatProvider? formatProvider) => this.Value.ToString(format, formatProvider);

		/// <inheritdoc/>
		public sealed override void ExportTo(TextWriter writer, int lineOffset)
		{
			var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this._value, 1));
			ExportAsHexTo(writer, this.Kind, bytes, lineOffset);
		}
	}
	/// <summary>
	/// Represents a <see cref="RegistryValueKind.REG_DWORD"/> value.
	/// </summary>
	public sealed class RegistryDword : RegistryData, IHaveUInt64Value
	{
		/// <summary>
		/// Initializes a new <see cref="RegistryDword"/>.
		/// </summary>
		/// <param name="value">Value</param>
		public RegistryDword(uint value)
		{
			Value = value;
		}

		/// <inheritdoc/>
		public sealed override RegistryValueKind Kind => RegistryValueKind.REG_DWORD;

		/// <summary>
		/// Gets the value as a <see langword="ulong"/>.
		/// </summary>
		public uint Value { get; }
		/// <inheritdoc/>
		ulong IHaveUInt64Value.UInt64Value => this.Value;
		/// <inheritdoc/>
		public override object UntypedValue => this.Value;
		/// <inheritdoc/>
		public override string ToString(string? format, IFormatProvider? formatProvider) => this.Value.ToString(format, formatProvider);
		/// <inheritdoc/>
		public override void ExportTo(TextWriter writer, int lineOffset)
		{
			writer.WriteLine($"dword:{this.Value:x8}");
		}
	}
	/// <summary>
	/// Represents a <see cref="RegistryValueKind.REG_DWORD"/> value.
	/// </summary>
	public sealed class RegistryString : RegistryData
	{
		/// <summary>
		/// Initializes a new <see cref="RegistryString"/>.
		/// </summary>
		/// <param name="value">Value</param>
		public RegistryString(string value)
		{
			Value = value;
		}

		/// <inheritdoc/>
		public sealed override RegistryValueKind Kind => RegistryValueKind.REG_SZ;

		/// <summary>
		/// Gets the value as a <see langword="ulong"/>.
		/// </summary>
		public string Value { get; }
		/// <inheritdoc/>
		public override object UntypedValue => this.Value;
		/// <inheritdoc/>
		public override string ToString(string? format, IFormatProvider? formatProvider) => this.Value;

		/// <inheritdoc/>
		public override void ExportTo(TextWriter writer, int lineOffset)
		{
			writer.WriteLine($"\"{this.Value}\"");
		}
	}
	/// <summary>
	/// Represents a <see cref="RegistryValueKind.REG_DWORD"/> value.
	/// </summary>
	public sealed class RegistryExpandableString : RegistryData
	{
		/// <summary>
		/// Initializes a new <see cref="RegistryExpandableString"/>.
		/// </summary>
		/// <param name="value">Value</param>
		public RegistryExpandableString(string value)
		{
			Value = value;
		}

		/// <inheritdoc/>
		public sealed override RegistryValueKind Kind => RegistryValueKind.REG_EXPAND_SZ;

		/// <summary>
		/// Gets the value as a <see langword="ulong"/>.
		/// </summary>
		public string Value { get; }
		/// <inheritdoc/>
		public override object UntypedValue => this.Value;
		/// <inheritdoc/>
		public override string ToString(string? format, IFormatProvider? formatProvider) => this.Value;


		/// <inheritdoc/>
		public override void ExportTo(TextWriter writer, int lineOffset)
		{
			var bytes = Encoding.Unicode.GetBytes(this.Value + '\0');
			ExportAsHexTo(writer, this.Kind, bytes, lineOffset);
		}
	}
	/// <summary>
	/// Represents a <see cref="RegistryValueKind.REG_MULTI_SZ"/> value.
	/// </summary>
	public sealed class RegistryMultiString : RegistryData
	{
		/// <summary>
		/// Initializes a new <see cref="RegistryMultiString"/>.
		/// </summary>
		/// <param name="values">Value</param>
		public RegistryMultiString(string[] values)
		{
			Strings = ImmutableArray.Create(values);
		}

		/// <inheritdoc/>
		public sealed override RegistryValueKind Kind => RegistryValueKind.REG_MULTI_SZ;

		/// <summary>
		/// Gets the value as a <see langword="ulong"/>.
		/// </summary>
		public ImmutableArray<string> Strings { get; }
		/// <inheritdoc/>
		public override object UntypedValue => this.Strings;
		/// <inheritdoc/>
		public override string ToString(string? format, IFormatProvider? formatProvider) => string.Join("\\0", this.Strings);

		/// <inheritdoc/>
		public override void ExportTo(TextWriter writer, int lineOffset)
		{
			var stringval = string.Join("\0", this.Strings) + "\0\0";
			writer.WriteLine(Encoding.Unicode.GetBytes(stringval));
		}
	}
	/// <summary>
	/// Represents registry data as raw bytes value.
	/// </summary>
	/// <remarks>
	/// This class supports <see cref="RegistryValueKind.REG_BINARY"/> as well data of undefined types or invalid values.
	/// </remarks>
	public sealed class RegistryBinary : RegistryData
	{
		/// <summary>
		/// Initializes a new <see cref="RegistryQword"/>.
		/// </summary>
		/// <param name="bytes">Value</param>
		/// <param name="kind">Kind of value</param>
		public RegistryBinary(byte[] bytes, RegistryValueKind kind)
		{
			Bytes = bytes;
			this.Kind = kind;
		}

		/// <inheritdoc/>
		public sealed override RegistryValueKind Kind { get; }

		/// <summary>
		/// Gets the value as a <see langword="ulong"/>.
		/// </summary>
		public byte[] Bytes { get; }
		/// <inheritdoc/>
		public override object UntypedValue => this.Bytes;
		/// <inheritdoc/>
		public override string ToString(string? format, IFormatProvider? formatProvider) => BinaryHelper.ToHexString(this.Bytes);

		/// <inheritdoc/>
		public override void ExportTo(TextWriter writer, int lineOffset)
		{
			ExportAsHexTo(writer, this.Kind, this.Bytes, lineOffset);
		}
	}

	/// <summary>
	/// Represents an entry in the Windows registry, including its path, security descriptor, and associated data.
	/// </summary>
	public class RegistryEntry
	{
		[DisplayName("Root")]
		public PredefinedKey Root { get; }

		[DisplayName("Key Path")]
		public string SubKey { get; }

		//TODO: Should be re-added once SecurityDescriptor support is added to WMI registry provider
		//public SecurityDescriptor SecurityDescriptor { get; set; }
		//public SecurityIdentifier? Owner => this.SecurityDescriptor?.Owner;
		//public SecurityIdentifier? Group => this.SecurityDescriptor?.Group;
		//[DisplayName("DACL")]
		//public string? Dacl => this.SecurityDescriptor?.ToSddlString(SecurityDescriptorSections.Access);
		//[DisplayName("SACL")]
		//public string? Sacl => this.SecurityDescriptor?.ToSddlString(SecurityDescriptorSections.Audit);

		[DisplayName("Value")]
		public string? ValueName { get; }

		[DisplayName("Data")]
		public RegistryData? Data { get; }

		/// <summary>
		/// Creates a new <see cref="RegistryEntry"/> instance.
		/// </summary>
		/// <param name="root">root registry key for this entry</param>
		/// <param name="subKey">path to subkey relative to root</param>
		/// <param name="valueName">Name of value for this entry if present</param>
		/// <param name="data"><see cref="RegistryData"/> of this entry if present</param>
		public RegistryEntry(PredefinedKey root, string subKey, string? valueName = null, RegistryData? data = null)
		{
			Root = root;
			SubKey = subKey;
			ValueName = valueName;
			Data = data;
		}

		public RegistryEntry(RegistryPath basePath, string? valueName = null, RegistryData? data = null)
		{
			Root = basePath.Root;
			SubKey = basePath.KeyPath;
			ValueName = valueName;
			Data = data;
		}

		[DisplayName("Type")]
		public RegistryValueKind Kind => Data?.Kind ?? RegistryValueKind.REG_NONE;

		//reg.exe export does "valuename"=hex(optional type number if not REG_BINARY):BB,
		//splits are made just before 80 characters.  First line with valuename can be longer and will include first byte + , prior to \\\n
		// Subsequent lines are indented with 2 spaces.

		/// <summary>
		/// Gets the string to represent this value in a <c>.reg</c> file.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotSupportedException"></exception>
		internal string GetExportString()
		{
			StringWriter writer = new StringWriter();
			ExportTo(writer);
			return writer.ToString();
		}
		internal void ExportTo(TextWriter writer)
		{
			if (this.Data is null)
				return;

			string valueNameEscaped = this.ValueName == null || this.ValueName == "" ? "@" : $"\"{this.ValueName.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"=";
			writer.Write(valueNameEscaped);

			this.Data.ExportTo(writer, valueNameEscaped.Length);
		}
	}
}
