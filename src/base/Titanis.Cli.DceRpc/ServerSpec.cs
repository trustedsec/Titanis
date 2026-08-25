using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace Titanis.Cli
{
	[TypeConverter(typeof(ServerSpecConverter))]
	public abstract class ServerSpec
	{
		public abstract bool HasMultiple { get; }

		public abstract IEnumerable<string> GetTargets();
	}

	public class ServerSpecConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			return (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
		}

		private Regex rgxSimpleIpRange => new Regex(@"^(?<p>(\d+\.){1,3})(?<l>\d+)-(?<e>\d+)$");
		private Regex rgxIpRange => new Regex(@"^(?<s>\d+(\.\d+){0,3})-(?<e>\d+(\.\d+){1,3})$");
		private Regex rgxIpSubnet => new Regex(@"^(?<s>\d+(\.\d+){0,3})/(?<n>\d+)$");
		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
			{
				if (IPAddress.TryParse(str, out var addr))
					return new ServerIpAddressSpec(addr);
				else
				{
					Match m = rgxSimpleIpRange.Match(str);
					if (m.Success)
					{
						string prefix = m.Groups["p"].Value;
						string startOctet = m.Groups["l"].Value;
						string endOctet = m.Groups["e"].Value;
						return new ServerIpv4RangeSpec(IPAddress.Parse(prefix + startOctet), IPAddress.Parse(prefix + endOctet));
					}
					else
					{
						m = rgxIpRange.Match(str);
						if (m.Success)
						{
							return new ServerIpv4RangeSpec(IPAddress.Parse(m.Groups["s"].Value), IPAddress.Parse(m.Groups["e"].Value));
						}
						else
						{
							m = rgxIpSubnet.Match(str);
							if (m.Success)
							{
								return new ServerIpSubnetSpec(IPAddress.Parse(m.Groups["s"].Value), int.Parse(m.Groups["n"].Value));
							}
						}
					}
				}

				return new ServerHostNameSpec(str);
			}
			return base.ConvertFrom(context, culture, value);
		}
	}

	public sealed class ServerHostNameSpec : ServerSpec
	{
		public ServerHostNameSpec(string hostName)
		{
			ArgumentNullException.ThrowIfNull(hostName);
			HostName = hostName;
		}

		public string HostName { get; }
		public sealed override bool HasMultiple => false;

		public sealed override string ToString() => this.HostName;

		public sealed override IEnumerable<string> GetTargets()
		{
			yield return this.HostName;
		}
	}

	public sealed class ServerIpAddressSpec : ServerSpec
	{
		public ServerIpAddressSpec(IPAddress address)
		{
			Address = address;
		}

		public IPAddress Address { get; }
		public sealed override bool HasMultiple => false;

		public sealed override string ToString() => this.Address.ToString();

		public sealed override IEnumerable<string> GetTargets()
		{
			yield return this.Address.ToString();
		}
	}

	public sealed class ServerIpv4RangeSpec : ServerSpec
	{
		public ServerIpv4RangeSpec(IPAddress startAddress, IPAddress endAddress)
		{
			ArgumentNullException.ThrowIfNull(startAddress);
			ArgumentNullException.ThrowIfNull(endAddress);
			if (startAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
				throw new ArgumentException($"Address must be IPv4.", nameof(startAddress));
			if (endAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
				throw new ArgumentException($"Address must be IPv4.", nameof(endAddress));

			StartAddress = startAddress;
			EndAddress = endAddress;
		}

		public sealed override bool HasMultiple => true;

		public IPAddress StartAddress { get; }
		public IPAddress EndAddress { get; }

		public sealed override string ToString() => $"{this.StartAddress}-{this.EndAddress}";

		public sealed override IEnumerable<string> GetTargets()
		{
			var start = IPAddress.NetworkToHostOrder((int)this.StartAddress.Address);
			var end = IPAddress.NetworkToHostOrder((int)this.EndAddress.Address);
			for (int i = start; i <= end; i++)
			{
				yield return new IPAddress(IPAddress.HostToNetworkOrder(i)).ToString();
			}
		}
	}

	public class ServerIpSubnetSpec : ServerSpec
	{
		public ServerIpSubnetSpec(IPAddress subnetAddress, int netmaskLength)
		{
			if ((uint)(netmaskLength - 1) > (uint)(31 - 1))
				throw new ArgumentOutOfRangeException(nameof(netmaskLength));

			SubnetAddress = subnetAddress;
			NetmaskLength = netmaskLength;
		}

		public sealed override bool HasMultiple => true;

		public IPAddress SubnetAddress { get; }
		public int NetmaskLength { get; }

		public sealed override string ToString() => $"{this.SubnetAddress}/{this.SubnetAddress}";

		public sealed override IEnumerable<string> GetTargets()
		{
			var start = IPAddress.NetworkToHostOrder((int)this.SubnetAddress.Address);
			var mask = (1 << (this.NetmaskLength)) - 1;
			if ((start & mask) == 0)
				start |= 1;

			var end = (start | mask) - 1;
			for (int i = start; i < end; i++)
			{
				yield return new IPAddress(IPAddress.HostToNetworkOrder(i)).ToString();
			}
		}
	}
}
