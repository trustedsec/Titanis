using System.IO;
using System.Text;
using Titanis.IO;


namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.78 - Encoded-String
	partial struct EncodedString : IPduStruct
	{
		public EncodedString(string value)
		{
			this.value = value;
		}

		public string? value;
		public override string ToString()
			=> this.value;
		public void ReadFrom(IByteSource reader)
		{
			var flag = (EncodedStringFlag)reader.ReadByte();
			StringBuilder sb = new StringBuilder();
			if (flag == EncodedStringFlag.Compressed)
			{
				// Compressed
				byte b;
				while (0 != (b = reader.ReadByte()))
				{
					sb.Append((char)b);
				}
			}
			else if (flag == EncodedStringFlag.Utf16)
			{
				// UTF-16
				ushort n;
				while (0 != (n = reader.ReadUInt16LE()))
				{
					sb.Append((char)n);
				}
			}
			else
			{
				throw new InvalidDataException($"The encoded string is prefix with an unknown flag ({(byte)flag}).");
			}

			this.value = sb.ToString();
		}

		public void ReadFrom(IByteSource reader, PduByteOrder byteOrder)
			=> this.ReadFrom(reader);

		public void WriteTo(ByteWriter writer)
		{
			string str = this.value ?? string.Empty;

			bool isSingleByte = str.IsSingleByteString();
			if (isSingleByte)
			{
				writer.WriteByte((byte)EncodedStringFlag.Compressed);

				foreach (var c in str)
				{
					writer.WriteByte((byte)c);
				}
				writer.WriteByte(0);
			}
			else
			{
				writer.WriteByte((byte)EncodedStringFlag.Utf16);

				writer.WriteStringUni(str);
				writer.WriteUInt16LE(0);
			}
		}

		public void WriteTo(ByteWriter writer, PduByteOrder byteOrder)
			=> this.WriteTo(writer);
	}
}
