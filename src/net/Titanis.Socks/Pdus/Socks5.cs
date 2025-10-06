using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Socks.Pdus
{
	public enum SocksVersion : byte
	{
		Socks5 = 5
	}
	public enum AuthMethod : byte
	{
		None = 0,
		Gssapi = 1,
		UsernamePassword = 2,
		NoneAccepted = 0xFF
	}
	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct InitRequestPdu
	{
		internal SocksVersion version;
		internal byte methodCount;
		[PduArraySize(nameof(methodCount))]
		internal AuthMethod[] methods;
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct InitResponsePdu
	{
		internal byte version;
		internal AuthMethod authMethod;
	}

	public enum Socks5Command : byte
	{
		Connect = 1,
		Bind = 2,
		UdpAssociate = 3,
	}
	public enum Socks5AddressType : byte
	{
		Ipv4 = 1,
		DomainName = 3,
		Ipv6 = 4,
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct Socks5RequestPdu
	{
		internal SocksVersion version;
		internal Socks5Command command;
		private byte reserved;
		internal Socks5EndPoint endpoint;
	}

	struct Socks5EndPoint : IPduStruct
	{
		public Socks5EndPoint(Socks5Address address, ushort port)
		{
			Address = address;
			Port = port;
		}

		internal Socks5AddressType AddressType
		{
			get => this.Address.AddressType;
			set
			{
				this.Address = value switch
				{
					Socks5AddressType.Ipv4 => new Socks5Ipv4Address(),
					Socks5AddressType.Ipv6 => new Socks5Ipv6Address(),
					Socks5AddressType.DomainName => new Socks5DnsAddress(),
					_ => throw new ArgumentException($"Unsupported address type: {value}.")
				};
			}
		}
		public Socks5Address Address { get; private set; }
		public ushort Port { get; private set; }

		public void ReadFrom(IByteSource reader)
		{
			this.AddressType = (Socks5AddressType)reader.ReadByte();
			this.Address.ReadFrom(reader);
			this.Port = reader.ReadUInt16BE();
		}

		public void ReadFrom(IByteSource reader, PduByteOrder byteOrder)
			=> this.ReadFrom(reader);

		public void WriteTo(ByteWriter writer)
		{
			writer.WriteByte((byte)this.AddressType);
			this.Address.WriteTo(writer);
			writer.WriteUInt16BE(this.Port);
		}

		public void WriteTo(ByteWriter writer, PduByteOrder byteOrder)
			=> this.WriteTo(writer);

		public EndPoint ToSocketEndpoint()
			=> this.Address.ToEndPoint(this.Port);
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct Socks5ResponsePdu
	{
		internal SocksVersion version;
		internal Socks5ResultCode resultCode;
		private byte reserved;
		internal Socks5EndPoint bindEP;
	}

	abstract class Socks5Address : IPduStruct
	{
		public abstract Socks5AddressType AddressType { get; }

		public abstract void ReadFrom(IByteSource reader);

		public void ReadFrom(IByteSource reader, PduByteOrder byteOrder)
			=> this.ReadFrom(reader);

		public abstract void WriteTo(ByteWriter writer);

		public void WriteTo(ByteWriter writer, PduByteOrder byteOrder)
			=> this.WriteTo(writer);
		public abstract EndPoint ToEndPoint(ushort port);
	}

	sealed class Socks5Ipv4Address : Socks5Address
	{
		public Socks5Ipv4Address() { }
		public Socks5Ipv4Address(IPAddress address)
		{
			ArgumentNullException.ThrowIfNull(address);
			if (address.AddressFamily is not System.Net.Sockets.AddressFamily.InterNetwork)
				throw new ArgumentException("The address must use AddressFamily.InterNetwork", nameof(address));

			this.Address = address;
		}
		public sealed override Socks5AddressType AddressType => Socks5AddressType.Ipv4;

		public IPAddress? Address { get; private set; }

		public sealed override EndPoint ToEndPoint(ushort port)
			=> new IPEndPoint(this.Address, port);

		public sealed override void ReadFrom(IByteSource reader)
		{
			var value = reader.ReadBytes(4);
			this.Address = new IPAddress(value);
		}

		public sealed override void WriteTo(ByteWriter writer)
		{
			if (this.Address == null)
				throw new InvalidOperationException("The Socks5Ipv4Address has not been initialized and cannot be written.");

			var bytes = this.Address.GetAddressBytes();
			Debug.Assert(bytes.Length == 4);
			writer.WriteBytes(bytes);
		}
	}

	class Socks5Ipv6Address : Socks5Address
	{
		public Socks5Ipv6Address() { }
		public Socks5Ipv6Address(IPAddress address)
		{
			ArgumentNullException.ThrowIfNull(address);
			if (address.AddressFamily is not System.Net.Sockets.AddressFamily.InterNetworkV6)
				throw new ArgumentException("The address must use AddressFamily.InterNetworkV6", nameof(address));

			this.Address = address;
		}

		public sealed override Socks5AddressType AddressType => Socks5AddressType.Ipv6;
		public IPAddress? Address { get; private set; }

		public sealed override EndPoint ToEndPoint(ushort port)
			=> new IPEndPoint(this.Address, port);

		public sealed override void ReadFrom(IByteSource reader)
		{
			var value = reader.ReadBytes(16);
			this.Address = new IPAddress(value);
		}

		public sealed override void WriteTo(ByteWriter writer)
		{
			if (this.Address == null)
				throw new InvalidOperationException("The Socks5Ipv6Address has not been initialized and cannot be written.");

			var bytes = this.Address.GetAddressBytes();
			Debug.Assert(bytes.Length == 16);
			writer.WriteBytes(bytes);
		}
	}

	class Socks5DnsAddress : Socks5Address
	{
		public Socks5DnsAddress() { }
		public Socks5DnsAddress(string hostName)
		{
			ArgumentNullException.ThrowIfNull(hostName);
			if (hostName.Length > 255)
				throw new ArgumentException("The hostName exceeds 255 octets and cannot be used.", nameof(hostName));

			this.HostName = hostName;
		}

		public sealed override Socks5AddressType AddressType => Socks5AddressType.DomainName;
		public string? HostName { get; private set; }

		public sealed override EndPoint ToEndPoint(ushort port)
			=> new DnsEndPoint(this.HostName, port);

		public override void ReadFrom(IByteSource reader)
		{
			var cbName = reader.ReadByte();
			var bytes = reader.Consume(cbName);
			this.HostName = Encoding.UTF8.GetString(bytes);
		}

		public override void WriteTo(ByteWriter writer)
		{
			if (this.HostName == null)
				throw new InvalidOperationException("The Socks5DnsAddress has not been initialized and cannot be written.");

			Debug.Assert(this.HostName != null);
			var bytes = Encoding.UTF8.GetBytes(this.HostName);
			Debug.Assert(bytes.Length <= 255);
			writer.WriteByte((byte)bytes.Length);
			writer.WriteBytes(bytes);
		}
	}
}
