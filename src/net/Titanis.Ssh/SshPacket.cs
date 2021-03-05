using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.PduStruct;

namespace Titanis.Ssh
{
	// [RFC4253] § 6. Binary Packet Protocol
	[PduStruct]
	internal partial struct SshPacket
	{
		internal const int HeaderSize = 4;

		[PduParameter]
		internal int macLength;

		internal uint packetLength;
		internal byte paddingLength;

		private int PayloadSize => (int)(this.packetLength - this.paddingLength - 1);
		[PduArraySize(nameof(PayloadSize))]
		internal byte[] payload;

		[PduArraySize(nameof(paddingLength))]
		internal byte[] padding;

		[PduArraySize(nameof(macLength))]
		internal byte[] mac;
	}
}
