using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Ssh
{
	internal partial struct VersionString : IPduStruct
	{
		public VersionString(Version version, string software, string? comment)
		{
			this.version = version;
			this.software = software;
			this.comment = comment;
		}

		internal Version version;
		internal string software;
		internal string? comment;
		private const string SshPrefix = "SSH-";

		public void ReadFrom(IByteSource reader)
		{
			// [RFC4253] § 4.2 - Protocol Version Exchange

			byte b;

			// SSH-version
			for (int i = 0; i < SshPrefix.Length; i++)
			{
				b = reader.ReadByte();
				if (b != SshPrefix[i])
					throw new InvalidDataException("The stream does not contain a valid SSH version.");
			}

			StringBuilder sb = new StringBuilder();
			while ((b = reader.ReadByte()) is not (byte)'-' && sb.Length < 255)
			{
				sb.Append((char)b);
			}

			string strVersion = sb.ToString();
			if (!Version.TryParse(strVersion, out this.version))
				throw new InvalidDataException($"The stream contained the version string '{strVersion}', which is not valid.");

			// Software
			sb.Clear();
			while ((b = reader.ReadByte()) is not (byte)' ' and not (byte)'\r')
			{
				sb.Append((char)b);
			}
			this.software = sb.ToString();

			// Comment
			if (b != '\r')
			{
				sb.Clear();
				while ('\r' != (b = reader.ReadByte()))
				{
					sb.Append((char)b);
				}
				this.comment = sb.ToString();
			}

			if (reader.PeekByte() == '\n') reader.ReadByte();
		}

		public void ReadFrom(IByteSource reader, PduByteOrder byteOrder)
			=> this.ReadFrom(reader);

		public void WriteTo(ByteWriter writer)
		{
			writer.WriteStringAnsi(SshPrefix);
			writer.WriteStringAnsi(this.version.ToString(2));
			writer.WriteByte((byte)'-');
			writer.WriteStringAnsi(this.software);
			if (!string.IsNullOrEmpty(this.comment))
			{
				writer.WriteByte((byte)' ');
				writer.WriteStringAnsi(this.comment);
			}
			writer.WriteByte((byte)'\r');
			writer.WriteByte((byte)'\n');
		}

		public void WriteTo(ByteWriter writer, PduByteOrder byteOrder)
			=> this.WriteTo(writer);
	}
}
