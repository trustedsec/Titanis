using System.Buffers.Binary;
using System.Net;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using System.Text;
using Titanis.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Titanis.Ldap
{
	// [MS-DNSP] § 2.2.2.2.4 - DNS_RPC_RECORD_DATA
	public class DnsRecordData
	{

	}

	// [MS-DNSP] § 2.2.2.2.4.18 - DNS_RPC_RECORD_SRV
	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct DNS_RPC_RECORD_SRV
	{
		public ushort Priority { get; set; }
		public ushort Weight { get; set; }
		public ushort Port { get; set; }
		public DnsName Target { get; set; }
	}

	// [MS-DNSP] § 2.2.2.2.4.18 - DNS_RPC_RECORD_SRV
	public class DnsService : DnsRecordData
	{
		private readonly DNS_RPC_RECORD_SRV _srv;

		internal DnsService(in DNS_RPC_RECORD_SRV srv)
		{
			this._srv = srv;
		}

		public ushort Priority => this._srv.Priority;
		public ushort Weight => this._srv.Weight;
		public ushort Port => this._srv.Port;
		public string Target => this._srv.Target.Name;

		public override string ToString() => $"{this.Target}:{this.Port};priority={this.Priority};w={this.Weight}";
	}

	// [MS-DNSP] § 2.2.2.2.4.1 - DNS_RPC_RECORD_A
	public class DnsIpAddress : DnsRecordData
	{
		public DnsIpAddress(IPAddress address)
		{
			ArgumentNullException.ThrowIfNull(address);
			Address = address;
		}

		public IPAddress Address { get; }
		public override string ToString() => this.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 ? $"[{this.Address}]" : this.Address.ToString();
	}

	public enum IpProtocol : ushort
	{
		Tcp = 6,
		Udp = 17
	}
	// [MS-DNSP] § 2.2.2.2.4.5 - DNS_RPC_RECORD_WKS
	public class DnsWksRecord : DnsRecordData
	{
		public DnsWksRecord(IPAddress address, IpProtocol protocol, ushort[] ports)
		{
			Address = address;
			Protocol = protocol;
			Ports = ports;
		}

		public IPAddress Address { get; }
		public IpProtocol Protocol { get; }
		public ushort[] Ports { get; }

		public override string ToString() => $"{this.Address}:{this.Protocol}:{string.Join(",", this.Ports)}";
	}

	partial struct DnsName : IPduStruct
	{
		public string Name { get; private set; }

		public void ReadFrom<TSource>(TSource reader) where TSource : class, IByteSource
		{
			// First octet is the total name length, including the terminating null
			var cbName = reader.ReadByte();
			StringBuilder sb = new StringBuilder(cbName - 2);

			var bytes = reader.ReadBytes(cbName + 1);

			// Not used
			var cLabels = bytes[0];


			int readIndex = 1;
			while (readIndex < bytes.Length)
			{
				var cchLabel = bytes[readIndex];
				if (cchLabel == 0)
					break;

				var label = Encoding.UTF8.GetString(bytes.Slice(readIndex + 1, cchLabel));
				if (sb.Length > 0)
					sb.Append('.');
				sb.Append(label);
				readIndex += 1 + cchLabel;
			}

			this.Name = sb.ToString();
		}

		public void WriteTo(ByteWriter writer)
		{
			throw new NotImplementedException();
		}
	}

	// [MS-DNSP] § 2.2.2.2.4.3 - DNS_RPC_RECORD_SOA
	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct DNS_RPC_RECORD_SOA
	{
		internal uint serialNumber;
		internal uint refreshInterval;
		internal uint retryInterval;
		internal uint expirationDuration;
		internal uint minimumTtl;
		internal DnsName primaryServer;
		internal DnsName adminEmail;
	}

	// [MS-DNSP] § 2.2.2.2.4.3 - DNS_RPC_RECORD_SOA
	public class DnsSoaData : DnsRecordData
	{
		private DNS_RPC_RECORD_SOA _info;

		internal DnsSoaData(in DNS_RPC_RECORD_SOA info)
		{
			this._info = info;
		}

		public uint SerialNumber => this._info.serialNumber;
		public Duration RefreshInterval => new Duration(TimeSpan.FromSeconds(this._info.refreshInterval));
		public Duration RetryInternval => new Duration(TimeSpan.FromSeconds(this._info.retryInterval));
		public Duration ExpirationDuration => new Duration(TimeSpan.FromSeconds(this._info.expirationDuration));
		public Duration MinimumTtl => new Duration(TimeSpan.FromSeconds(this._info.minimumTtl));
		public string PrimaryServer => this._info.primaryServer.Name;
		public string AdminEmail => this._info.adminEmail.Name;

		public override string ToString() => $"{this.PrimaryServer};serno={this.SerialNumber};refresh={this.RefreshInterval};retry={this.RetryInternval};expire={this.ExpirationDuration};minttl={this.MinimumTtl};admin={this.AdminEmail}";
	}

	// [MS-DNSP] § 2.2.2.2.4.2 - DNS_RPC_RECORD_NAME
	public class DnsNameRecord : DnsRecordData
	{
		public DnsNameRecord(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public override string ToString() => this.Name;
	}

	public class DnsOctets : DnsRecordData
	{
		public DnsOctets(byte[] data)
		{
			Data = data;
		}

		public byte[] Data { get; }

		public override string ToString() => BinaryHelper.ToHexString(this.Data);
	}

	// [MS-DNSP] § 2.2.2.1.1 DNS_RECORD_TYPE
	public enum DnsRecordKind : ushort
	{
		A = 0x0001,
		Ns = 0x0002,
		Md = 0x0003,
		Mf = 0x0004,
		CName = 0x0005,
		Soa = 0x0006,
		Mb = 0x0007,
		Mg = 0x0008,
		Mr = 0x0009,
		Null = 0x000A,
		Wks = 0x000B,
		Ptr = 0x000C,
		Hinfo = 0x000D,
		Minfo = 0x000E,
		Mx = 0x000F,
		Txt = 0x0010,
		Rp = 0x0011,
		Afsdb = 0x0012,
		X25 = 0x0013,
		Isdn = 0x0014,
		Rt = 0x0015,
		Sig = 0x0018,
		Key = 0x0019,
		Loc = 0x001D,
		Nxt = 0x001E,
		Atma = 0x0022,
		Naptr = 0x0023,
		Dname = 0x0027,
		Ds = 0x002B,
		Rrsig = 0x002E,
		Nsec = 0x002F,
		Dnskey = 0x0030,
		Dhcid = 0x0031,
		Nsec3 = 0x0032,
		Nsec3Param = 0x0033,
		Tlsa = 0x0034,
		All = 0x00FF,
		Wins = 0xFF01,
		Winsr = 0xFF02,

		// [RFC] 3596
		Aaaa = 0x001C,

		// [RFC 2782]
		Srv = 0x0021,
	}

	enum DnsRecordFormat
	{
		Unknown = 0,
		Ipv4Address,
		Ipv6Address,
		DnsName,
		Soa,
		Wks,
		Srv,
	}

	// [MS-DNSP] § 2.2.2.2.5 - DNS_RPC_RECORD
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct DnsRecordHeader
	{
		public ushort DataSize { get; set; }

		public DnsRecordKind Kind { get; set; }

		public uint Flags { get; set; }
		public uint Serial { get; set; }
		public uint dwTtlSeconds { get; set; }
		public uint dwTimeStamp { get; set; }
		private uint Reserved { get; set; }

		[PduArraySize(nameof(DataSize))]
		public byte[] Data { get; set; }
	}

	public class DnsRecordInfo
	{
		public DnsRecordInfo(byte[] bytes)
		{
			this.Bytes = bytes;

			if (bytes.Length >= 24)
			{
				try
				{
					DnsRecordHeader header = new ByteMemoryReader(bytes).ReadPduStruct<DnsRecordHeader>();
					this.Kind = header.Kind;

					var format = (header.Kind, header.DataSize) switch
					{
						(DnsRecordKind.A, >= 4) => DnsRecordFormat.Ipv4Address,
						(DnsRecordKind.Aaaa, >= 16) => DnsRecordFormat.Ipv6Address,
						(DnsRecordKind.Soa, _) => DnsRecordFormat.Soa,
						(DnsRecordKind.Wks, _) => DnsRecordFormat.Wks,
						(DnsRecordKind.Srv, _) => DnsRecordFormat.Srv,
						(DnsRecordKind.Ptr or DnsRecordKind.Ns or DnsRecordKind.CName or DnsRecordKind.Dname or DnsRecordKind.Mb or DnsRecordKind.Mr or DnsRecordKind.Mg or DnsRecordKind.Md or DnsRecordKind.Mf, > 3) => DnsRecordFormat.DnsName,
						_ => DnsRecordFormat.Unknown
					};

					this.RecordData = format switch
					{
						DnsRecordFormat.Ipv4Address or DnsRecordFormat.Ipv6Address => new DnsIpAddress(new IPAddress(header.Data)),
						DnsRecordFormat.Wks => new DnsWksRecord(new IPAddress(header.Data.Slice(0, 4)), (IpProtocol)BinaryPrimitives.ReadUInt16LittleEndian(header.Data.Slice(4, 2)), ReadPorts(header.Data.Slice(6))),
						DnsRecordFormat.DnsName => new DnsNameRecord(ReadDnsName(header.Data)),
						DnsRecordFormat.Soa => new DnsSoaData(new ByteMemoryReader(header.Data).ReadPduStruct<DNS_RPC_RECORD_SOA>()),
						DnsRecordFormat.Srv => new DnsService(new ByteMemoryReader(header.Data).ReadPduStruct<DNS_RPC_RECORD_SRV>()),
						_ => new DnsOctets(header.Data)
					};
				}
				catch
				{
					// Ignore error and display bytes
				}
			}
		}


		private static ushort[] ReadPorts(ReadOnlySpan<byte> span)
		{
			return [];
		}

		// [MS-DNSP] § 2.2.2.2.2 - DNS_COUNT_NAME
		private string ReadDnsName(byte[] data)
		{
			return new ByteMemoryReader(data).ReadPduStruct<DnsName>().Name;
		}
		//private string ReadDnsName(byte[] data)
		//{
		//	// First octet is the total name length, including the terminating null
		//	var cch = data[0] - 2;
		//	StringBuilder sb = new StringBuilder(cch);

		//	// Not used
		//	var cLabels = data[1];

		//	int readIndex = 2;
		//	while (readIndex < data.Length)
		//	{
		//		var cchLabel = data[readIndex];
		//		if (cchLabel == 0)
		//			break;

		//		var label = Encoding.UTF8.GetString(data.Slice(readIndex + 1, cchLabel));
		//		if (sb.Length > 0)
		//			sb.Append('.');
		//		sb.Append(label);
		//		readIndex += 1 + cchLabel;
		//	}
		//	return sb.ToString();
		//}

		public byte[] Bytes { get; }
		public DnsRecordKind Kind { get; }
		public DnsRecordData? RecordData { get; }

		public override string ToString()
		{
			if (this.Kind == 0)
				return BinaryHelper.ToHexString(this.Bytes);
			else
				return $"{this.Kind}({this.RecordData})";
		}
	}
}