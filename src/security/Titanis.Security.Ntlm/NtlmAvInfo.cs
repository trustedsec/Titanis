using System;
using System.Text;
using Titanis.IO;

namespace Titanis.Security.Ntlm
{
	public class NtlmAvInfo
	{
		public string? NbComputerName;
		public string? NbDomainName;
		public string? DnsComputerName;
		public string? DnsDomainName;
		public string? DnsTreeName;
		public NtlmAuthFlags flags;
		public DateTime? timestamp;
		public string? targetName;
		public SingleHostData? singleHost;
		public Guid? channelBinding;

		public Memory<byte> ToBytes()
		{
			int cb = Measure();

			ByteWriter writer = new ByteWriter(cb);
			writer.WriteAvInfo(this);
			return writer.GetData();
		}

		public int Measure()
		{
			int cb = 0;
			if (this.NbComputerName != null) cb += 4 + Encoding.Unicode.GetByteCount(this.NbComputerName);
			if (this.NbDomainName != null) cb += 4 + Encoding.Unicode.GetByteCount(this.NbDomainName);
			if (this.DnsComputerName != null) cb += 4 + Encoding.Unicode.GetByteCount(this.DnsComputerName);
			if (this.DnsDomainName != null) cb += 4 + Encoding.Unicode.GetByteCount(this.DnsDomainName);
			if (this.DnsTreeName != null) cb += 4 + Encoding.Unicode.GetByteCount(this.DnsTreeName);
			if (this.flags != 0) cb += 4 + 4;
			if (this.timestamp.HasValue) cb += 4 + 8;
			if (this.targetName != null) cb += 4 + Encoding.Unicode.GetByteCount(this.targetName);
			if (this.singleHost.HasValue) cb += 4 + 0x30;
			if (this.channelBinding.HasValue) cb += 4 + 0x10;
			cb += 4;    // End
			return cb;
		}
	}
}