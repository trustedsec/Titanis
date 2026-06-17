using Microsoft.Win32;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Titanis.Winterop.Registry
{
	interface IHaveUInt64Value
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
		/// Gets a <see cref="RegistryValueType"/> value specifying the kind of data.
		/// </summary>
		public abstract RegistryValueType Kind { get; }
		/// <summary>
		/// Gets the data associated with the registry entry.
		/// </summary>
		public abstract object UntypedValue { get; }

		public abstract RegistryValueInfo AsRegistryValueInfo(string name);

		/// <summary>
		/// Determines whether the data matches a filter.
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns><see langword="true"/> if the data matches <paramref name="filter"/>; otherwise, <see langword="false"/></returns>
		public abstract bool Matches(RegistrySearchFilter filter);

		#region Factory methods
		/// <summary>
		/// Creates a <see cref="RegistryData"/> instance representing a REG_MULTI_SZ registry value.
		/// </summary>
		/// <remarks>This method splits the input string into an array of substrings based on the specified separator.</remarks>
		/// <param name="multiString">Array of strings to use as multisz value.</param>
		public static RegistryData CreateRegMultiString(string[] multiString) => new RegistryMultiString(multiString);
		public static RegistryData CreateRegMultiString(ImmutableArray<string> multiString) => new RegistryMultiString(multiString);
		public static RegistryData CreateRegMultiString(byte[] multiStringBuffer) =>
			CreateRegMultiString(Encoding.Unicode.GetString(multiStringBuffer).Split('\0', StringSplitOptions.RemoveEmptyEntries));

		/// <summary>
		/// Creates a <see cref="RegistryData"/> instance representing a REG_BINARY registry value.
		/// </summary>
		public static RegistryData CreateBinary(byte[] data) => new RegistryBinary(data, RegistryValueType.Binary);
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
		public static RegistryData CreateQword(ulong data) => new RegistryQword(data);

		public static RegistryData CreateOther(byte[] data, RegistryValueType kind) => new RegistryBinary(data, kind);

		/// <summary>
		/// Creates a typed registry object based on raw input <see cref="RegistryValueInfo"/>
		/// Uses the object that was already decoded by the RegistryKey provider that populated this type
		/// </summary>
		/// <param name="value">RegistryValueInfo as returned by <see cref="IRegistryKey.GetValue(string?, CancellationToken)"/></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public static RegistryData CreateRegValue(RegistryValueInfo value)
		{
			return value.ValueType switch
			{
				RegistryValueType.String => CreateString((string?)value.TypedValue ?? string.Empty),
				RegistryValueType.ExpandString => CreateExpandableString((string?)value.TypedValue ?? string.Empty),
				RegistryValueType.Binary => CreateBinary(value.Bytes ?? Array.Empty<byte>()),
				RegistryValueType.DwordLE => CreateDword((uint?)value.TypedValue ?? 0u),
				RegistryValueType.DwordBE => CreateDword((uint?)value.TypedValue ?? 0u),
				RegistryValueType.MultiString => (value.TypedValue is string[] strs) ? CreateRegMultiString(strs) : CreateRegMultiString((ImmutableArray<string>?)value.TypedValue ?? ImmutableArray<string>.Empty),
				RegistryValueType.Qword => CreateQword((ulong?)value.TypedValue ?? 0ul),
				_ => CreateOther(value.Bytes ?? Array.Empty<byte>(), value.ValueType)
			};
		}
		#endregion

		#region Export
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
		protected static void ExportAsHexTo(TextWriter writer, RegistryValueType kind, ReadOnlySpan<byte> bytes, int lineOffset)
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

		private static string GetExportTagForKind(RegistryValueType kind)
		{
			return kind switch
			{
				RegistryValueType.Binary => "hex:",
				_ => $"hex({(int)kind:x}):"
			};
		}

		/// <summary>
		/// Exports the data as in a <c>.reg</c> file.
		/// </summary>
		/// <param name="writer">Target writer</param>
		/// <param name="lineOffset">Line offset of <paramref name="writer"/></param>
		public abstract void ExportTo(TextWriter writer, int lineOffset);
		#endregion

		/// <inheritdoc/>
		public sealed override string ToString() => ToString(null, null);
		/// <inheritdoc/>
		public abstract string ToString(string? format, IFormatProvider? formatProvider);
	}

	/// <summary>
	/// Represents a <see cref="RegistryValueType.Qword"/> value.
	/// </summary>
	public sealed class RegistryQword : RegistryData, IHaveUInt64Value
	{
		/// <summary>
		/// Initializes a new <see cref="RegistryQword"/>.
		/// </summary>
		/// <param name="value">Value</param>
		public RegistryQword(ulong value)
		{
			_value = value;
		}

		/// <inheritdoc/>
		public sealed override RegistryValueType Kind => RegistryValueType.Qword;

		private ulong _value;
		/// <summary>
		/// Gets the value as a <see langword="ulong"/>.
		/// </summary>
		public ulong Value => _value;
		/// <inheritdoc/>
		public override object UntypedValue => Value;

		/// <inheritdoc/>
		ulong IHaveUInt64Value.UInt64Value => Value;

		/// <inheritdoc/>
		public sealed override bool Matches(RegistrySearchFilter filter) => filter.Matches(_value);

		/// <inheritdoc/>
		public override string ToString(string? format, IFormatProvider? formatProvider) => Value.ToString(format, formatProvider);

		/// <inheritdoc/>
		public sealed override void ExportTo(TextWriter writer, int lineOffset)
		{
			var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref _value, 1));
			ExportAsHexTo(writer, Kind, bytes, lineOffset);
		}

		public override RegistryValueInfo AsRegistryValueInfo(string name)
		{
			byte[] rawData = new byte[8];
			BinaryPrimitives.WriteUInt64LittleEndian(rawData, _value);
			return new RegistryValueInfo(name, Kind, 8, rawData, _value);
		}
	}
	/// <summary>
	/// Represents a <see cref="RegistryValueType.DwordLE"/> value.
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
		public sealed override RegistryValueType Kind => RegistryValueType.DwordLE;

		/// <summary>
		/// Gets the value as a <see langword="ulong"/>.
		/// </summary>
		public uint Value { get; }
		/// <inheritdoc/>
		ulong IHaveUInt64Value.UInt64Value => Value;
		/// <inheritdoc/>
		public override object UntypedValue => Value;

		/// <inheritdoc/>
		public sealed override bool Matches(RegistrySearchFilter filter) => filter.Matches(Value);

		/// <inheritdoc/>
		public override string ToString(string? format, IFormatProvider? formatProvider) => Value.ToString(format, formatProvider);
		/// <inheritdoc/>
		public override void ExportTo(TextWriter writer, int lineOffset)
		{
			writer.WriteLine($"dword:{Value:x8}");
		}

		public override RegistryValueInfo AsRegistryValueInfo(string name)
		{
			byte[] rawData = new byte[4];
			BinaryPrimitives.WriteUInt32LittleEndian(rawData, Value);
			return new RegistryValueInfo(name, Kind, 4, rawData, Value);
		}
	}
	/// <summary>
	/// Represents a <see cref="RegistryValueType.String"/> value.
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
		public sealed override RegistryValueType Kind => RegistryValueType.String;

		/// <summary>
		/// Gets the value as a <see langword="ulong"/>.
		/// </summary>
		public string Value { get; }
		/// <inheritdoc/>
		public override object UntypedValue => Value;

		/// <inheritdoc/>
		public sealed override bool Matches(RegistrySearchFilter filter) => filter.Matches(Value);

		/// <inheritdoc/>
		public override string ToString(string? format, IFormatProvider? formatProvider) => Value;

		/// <inheritdoc/>
		public override void ExportTo(TextWriter writer, int lineOffset)
		{
			writer.WriteLine($"\"{Value}\"");
		}

		public override RegistryValueInfo AsRegistryValueInfo(string name)
		{
			var rawData = Encoding.Unicode.GetBytes(Value);
			return new RegistryValueInfo(name, Kind, rawData.Length, rawData, Value);
		}
	}
	/// <summary>
	/// Represents a <see cref="RegistryValueType.ExpandString"/> value.
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
		public sealed override RegistryValueType Kind => RegistryValueType.ExpandString;

		/// <summary>
		/// Gets the value as a <see langword="ulong"/>.
		/// </summary>
		public string Value { get; }
		/// <inheritdoc/>
		public override object UntypedValue => Value;

		/// <inheritdoc/>
		public sealed override bool Matches(RegistrySearchFilter filter) => filter.Matches(Value);

		/// <inheritdoc/>
		public override string ToString(string? format, IFormatProvider? formatProvider) => Value;


		/// <inheritdoc/>
		public override void ExportTo(TextWriter writer, int lineOffset)
		{
			var bytes = Encoding.Unicode.GetBytes(Value + '\0');
			ExportAsHexTo(writer, Kind, bytes, lineOffset);
		}

		public override RegistryValueInfo AsRegistryValueInfo(string name)
		{
			var rawData = Encoding.Unicode.GetBytes(Value);
			return new RegistryValueInfo(name, Kind, rawData.Length, rawData, Value);
		}
	}
	/// <summary>
	/// Represents a <see cref="RegistryValueType.MultiString"/> value.
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

		public RegistryMultiString(ImmutableArray<string> values)
		{
			Strings = values;
		}

		/// <inheritdoc/>
		public sealed override RegistryValueType Kind => RegistryValueType.MultiString;

		/// <summary>
		/// Gets the value as a <see langword="ulong"/>.
		/// </summary>
		public ImmutableArray<string> Strings { get; }

		/// <inheritdoc/>
		public sealed override bool Matches(RegistrySearchFilter filter) => filter.Matches(Strings);

		/// <inheritdoc/>
		public override object UntypedValue => Strings;
		/// <inheritdoc/>
		public override string ToString(string? format, IFormatProvider? formatProvider) => string.Join("\\0", Strings);

		/// <inheritdoc/>
		public override void ExportTo(TextWriter writer, int lineOffset)
		{
			var stringval = string.Join("\0", Strings) + "\0\0";
			byte[] bytes = Encoding.Unicode.GetBytes(stringval);
			ExportAsHexTo(writer, Kind, bytes, lineOffset);
		}

		public override RegistryValueInfo AsRegistryValueInfo(string name)
		{
			using (var ms = new MemoryStream())
			{
				using (var bw = new BinaryWriter(ms))
				{
					for (int i = 0; i < Strings.Length; i++)
					{
						if (i > 0)
						{
							bw.Write((short)0);
						}
						var rawString = Encoding.Unicode.GetBytes(Strings[i]);
						var length = rawString.Length;
						while (length > 0 && rawString[length - 1] == '\0') //A string shouldn't have multiple nulls at the end, but it could
						{
							length--;
						}
						bw.Write(rawString, 0, length);
					}
					bw.Write((uint)0);
					var rawData = ms.ToArray();
					return new RegistryValueInfo(name, Kind, rawData.Length, rawData, Strings);
				}
			}
		}
	}
	/// <summary>
	/// Represents registry data as raw bytes value.
	/// </summary>
	/// <remarks>
	/// This class supports <see cref="RegistryValueType.Binary"/> as well data of undefined types or invalid values.
	/// </remarks>
	public sealed class RegistryBinary : RegistryData
	{
		/// <summary>
		/// Initializes a new <see cref="RegistryQword"/>.
		/// </summary>
		/// <param name="bytes">Value</param>
		/// <param name="kind">Kind of value</param>
		public RegistryBinary(byte[] bytes, RegistryValueType kind)
		{
			Bytes = bytes;
			Kind = kind;
		}

		/// <inheritdoc/>
		public sealed override RegistryValueType Kind { get; }

		/// <summary>
		/// Gets the value as a <see langword="ulong"/>.
		/// </summary>
		public byte[] Bytes { get; }
		/// <inheritdoc/>
		public override object UntypedValue => Bytes;

		/// <inheritdoc/>
		public sealed override bool Matches(RegistrySearchFilter filter) => filter.Matches(Bytes);

		/// <inheritdoc/>
		public override string ToString(string? format, IFormatProvider? formatProvider) => Bytes.ToHexString();

		/// <inheritdoc/>
		public override void ExportTo(TextWriter writer, int lineOffset)
		{
			ExportAsHexTo(writer, Kind, Bytes, lineOffset);
		}

		public override RegistryValueInfo AsRegistryValueInfo(string name)
		{
			return new RegistryValueInfo(name, Kind, Bytes.Length, Bytes, Bytes);
		}
	}
}
