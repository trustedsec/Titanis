using System.Buffers.Binary;
using System.Net;
using System.Text.RegularExpressions;
using Titanis.IO;

namespace Titanis.Ldap
{
	public sealed class DnsRecordSyntax : Asn1EncodedSyntax<DnsRecordInfo>
	{
		internal DnsRecordSyntax()
		{

		}


		private static readonly Regex rgxDns = new Regex(@"^(?<t>\w+)\(((;|(?<=\())(?<v>[^;]*))*\)");
		public static byte[] ParseDns(string value)
		{
			var m = rgxDns.Match(value);
			if (m.Success)
			{
				var recordTypeName = m.Groups["t"].Value;
				if (!Enum.TryParse(recordTypeName, true, out DnsRecordKind kind))
					throw new ArgumentException($"Unknown DNS record type: {recordTypeName}.");

				List<(string? name, string value)> fields = new List<(string?, string)>();
				int ttl = 600;
				int serial = 0;
				int flags = 0xF005;
				foreach (Capture capture in m.Groups["v"].Captures)
				{
					int isep = capture.Value.IndexOf('=');

					(var name, var fieldValue) = (isep > 0) ? (capture.Value.Substring(0, isep), capture.Value.Substring(isep + 1)) : (null, capture.Value);
					fields.Add((name, fieldValue));
				}

				byte[]? recordData;
				switch (kind)
				{
					case DnsRecordKind.A:
					case DnsRecordKind.Aaaa:
						{
							IPAddress? ip = null;
							foreach (var item in fields)
							{
								if (item.name is null)
									ip = IPAddress.Parse(item.value);
							}
							recordData = ip.GetAddressBytes();
						}
						break;
					case DnsRecordKind.CName:
						{
							string? name = null;
							foreach (var item in fields)
							{
								if (item.name is null)
									name = item.value;
							}
							recordData = (new DnsName(name)).ToBytes().ToArray();
						}
						break;
					default:
						throw new NotImplementedException();
				}

				var hdr = new DnsRecordHeader
				{
					Kind = kind,
					Data = recordData,
					DataSize = (ushort)recordData.Length,
					Flags = (uint)flags,
					dwTtlSeconds = BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness((uint)ttl) : (uint)ttl
				};
				ByteWriter writer = new ByteWriter();
				writer.WritePduStruct(hdr);
				return writer.GetData().ToArray();
			}
			else
				throw new NotImplementedException();

		}

		/// <inheritdoc/>
		public sealed override string? RfcName => "Binary";
		/// <inheritdoc/>
		public sealed override string RfcOid => "OctetString";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "Object(Replica-Link)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.10";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 127;
		/// <inheritdoc/>
		public sealed override string? OmObjectClass => "1.2.840.113556.1.1.1.6";

		protected sealed override DnsRecordInfo DecodeOctets(byte[] bytes)
		{
			return new DnsRecordInfo(bytes);
		}

		protected sealed override byte[] EncodeOctets(DnsRecordInfo value)
		{
			return value.Bytes;
		}

		public override object Parse(string text)
		{
			return new BinaryString(BinaryHelper.ParseHexString(text));
		}
	}

}
