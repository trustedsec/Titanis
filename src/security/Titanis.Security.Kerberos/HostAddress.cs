using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos
{
	// [RFC 4120] Š 7.5.3 - AddressTypes
	public enum HostAddressType
	{
		Ipv4 = 2,
		Directional = 3,
		ChaosNet = 5,
		Xns = 6,
		Iso = 7,
		DecnetPhase4 = 12,
		AppletalkDdp = 16,
		Netbios = 20,
		Ipv6 = 24,
	}
	public abstract class HostAddress
	{

		public abstract HostAddressType AddressType { get; }
		public abstract byte[] GetBytes();

		internal KerberosV5Spec2.HostAddress ToKrb5HostAddress() => new KerberosV5Spec2.HostAddress((int)this.AddressType, this.GetBytes());

		public static HostAddress FromNetbiosName(string name) => new NetbiosHostAddress(name);
		public static IPHostAddress FromIPAddress(IPAddress address) => new IPHostAddress(address);
	}

	public sealed class NetbiosHostAddress : HostAddress
	{
		public NetbiosHostAddress(string hostName)
		{
			ArgumentNullException.ThrowIfNull(hostName);
			if (hostName.Length > 15) throw new ArgumentException($"The host name cannot exceed 15 characters");

			HostName = hostName.ToUpper();
		}

		public sealed override HostAddressType AddressType => HostAddressType.Netbios;

		public string HostName { get; }

		public sealed override byte[] GetBytes() => Encoding.UTF8.GetBytes(this.HostName.PadRight(16));
	}

	public sealed class IPHostAddress : HostAddress
	{
		public IPHostAddress(IPAddress address)
		{
			ArgumentNullException.ThrowIfNull(address);
			this.Address = address;
		}

		public sealed override HostAddressType AddressType => this.Address.AddressFamily switch
		{
			AddressFamily.InterNetwork => HostAddressType.Ipv4,
			AddressFamily.InterNetworkV6 => HostAddressType.Ipv6,
			_ => throw new NotSupportedException($"Unsupported address family {this.Address.AddressFamily}.")
		};

		public IPAddress Address { get; }

		public sealed override string ToString() => this.Address.ToString();

		public sealed override byte[] GetBytes() => this.Address.GetAddressBytes();
	}

	// [RFC 4120] § 7.1
	public sealed class GenericHostAddress : HostAddress
	{
		public GenericHostAddress(HostAddressType addressType, byte[] encodedBytes, string? displayText)
		{
			ArgumentNullException.ThrowIfNull(encodedBytes);
			AddressType = addressType;
			EncodedBytes = encodedBytes;
			DisplayText = displayText;
		}

		public sealed override HostAddressType AddressType { get; }
		public sealed override byte[] GetBytes() => this.EncodedBytes;

		public byte[] EncodedBytes { get; }
		public string? DisplayText { get; }
	}
}
