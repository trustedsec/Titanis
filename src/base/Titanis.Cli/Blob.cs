using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies how a blob was specified.
	/// </summary>
	[TypeConverter]
	public enum BlobSpecFormat
	{
		/// <summary>
		/// Blob specified as a byte array with no string specification.
		/// </summary>
		Bytes = 0,
		/// <summary>
		/// Blob specified as a Base64 string prefixed with <c>b64:</c>.
		/// </summary>
		Base64,
		/// <summary>
		/// Blob specified as a hexadecimal string prefixed with <c>hex:</c>.
		/// </summary>
		Hex,
		/// <summary>
		/// Blob specified as a file name.
		/// </summary>
		FileName
	}
	/// <summary>
	/// Describes a binary object.
	/// </summary>
	/// <remarks>
	/// A blob encapsulates an array of bytes.  The bytes may be specified
	/// as a file name, a base64 string, or a hex string.
	/// </remarks>
	[TypeConverter(typeof(BlobConverter))]
	public struct Blob
	{
		/// <summary>
		/// Initializes a new <see cref="Blob"/>.
		/// </summary>
		/// <param name="bytes">Bytes constituting the blob</param>
		public Blob(byte[] bytes)
		{
			this.Bytes = bytes;
		}
		/// <summary>
		/// Initializes a new <see cref="Blob"/>.
		/// </summary>
		/// <param name="bytes">Bytes constituting the blob</param>
		/// <param name="specFormat"></param>
		/// <param name="originalSpec"></param>
		public Blob(byte[] bytes, BlobSpecFormat specFormat, string originalSpec)
		{
			this.Bytes = bytes;
			this.OriginalSpecFormat = specFormat;
			this.OriginalSpec = originalSpec;
		}

		/// <inheritdoc/>
		public override string ToString()
			=> $"size={this.Bytes?.Length ?? 0}, spec={this.OriginalSpec}";

		/// <summary>
		/// Gets the bytes constituting the blob.
		/// </summary>
		public byte[] Bytes { get; }
		/// <summary>
		/// Gets the original specification of the blob as a string.
		/// </summary>
		public string? OriginalSpec { get; }
		/// <summary>
		/// Gets a <see cref="BlobSpecFormat"/> describing the format of <see cref="OriginalSpec"/>.
		/// </summary>
		public BlobSpecFormat OriginalSpecFormat { get; }

		/// <summary>
		/// Parses a string as a <see cref="Blob"/>.
		/// </summary>
		/// <param name="text">Binary data represented either as hex or Base64</param>
		/// <returns>A <see cref="Blob"/> with the parsed bytes</returns>
		/// <exception cref="ArgumentException"><paramref name="text"/> is not a valid blob string.</exception>
		/// <remarks>
		/// The string must begin with <c>0x</c> to denote a hexadecimal string or <c>b64:</c> to denote a Base64 string.
		/// </remarks>
		public static Blob Parse(string text, string? basePath)
		{
			if (text.Length == 0)
				return new Blob(Array.Empty<byte>());
			else if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
				return new Blob(BinaryHelper.ParseHexString(text.AsSpan().Slice(2)), BlobSpecFormat.Hex, text);
			else
			{
				ReadOnlySpan<char> chars;
				if (text.StartsWith("b64:", StringComparison.OrdinalIgnoreCase))
					chars = text.AsSpan().Slice(4);
				else if (text.StartsWith("base64:", StringComparison.OrdinalIgnoreCase))
					chars = text.AsSpan().Slice(7);
				else
				{
					// Interpret as a file name
					string fileName = text;
					if (basePath != null)
						fileName = Path.Combine(basePath, fileName);

					try
					{
						var content = File.ReadAllBytes(fileName);
						return new Blob(content, BlobSpecFormat.FileName, fileName);
					}
					catch (FileNotFoundException ex)
					{
						throw new ArgumentException("A string value must be prefixed with '0x' to interpret as a hex string or 'b64:' to interpret as a Base64 string, or a valid file name.", nameof(text), ex);
					}
				}

				var bytes = BinaryHelper.ParseBase64(chars, out _);
				return new Blob(bytes, BlobSpecFormat.Base64, text);

			}
		}
	}

	/// <summary>
	/// Implements a <see cref="TypeConverter"/> for <see cref="Blob"/>.
	/// </summary>
	public class BlobConverter : TypeConverter
	{
		/// <inheritdoc/>
		public sealed override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (sourceType == typeof(byte[]) || sourceType == typeof(string));
		}

		/// <inheritdoc/>
		public sealed override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return (destinationType == typeof(Blob));
		}

		/// <inheritdoc/>
		public sealed override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is null)
				throw new ArgumentNullException(nameof(value));

			if (value is byte[] bytes)
			{
				return new Blob(bytes);
			}
			else if (value is string text)
			{
				string? workingDir = null;
				if (context?.Instance is Command cmd)
					workingDir = cmd.Context?.WorkingDirectory;

				return Blob.Parse(text, workingDir);
			}
			else
			{
				throw new ArgumentException($"Cannot convert object of type {value.GetType().FullName} to a {nameof(HexString)}.");
			}
		}
	}
}
