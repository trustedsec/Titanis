using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop.Security;

namespace Titanis.Security.Kerberos
{
	public class UpnDnsInfo
	{
		internal UPN_DNS_INFO dnsInfo;

		public string? Upn { get => this.dnsInfo.Upn; }
		public string? DnsDomainName { get => this.dnsInfo.DnsDomainName; set => this.dnsInfo.DnsDomainName = value; }
		public string? SamName { get => this.dnsInfo.SamName; set => this.dnsInfo.SamName = value; }
		public SecurityIdentifier? Sid { get => this.dnsInfo.Sid; set => this.dnsInfo.Sid = value; }
	}
}
