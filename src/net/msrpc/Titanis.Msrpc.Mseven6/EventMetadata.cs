using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mseven6
{
	internal class EventMetadata
	{
		public ushort EventId { get; internal set; }
		public byte Version { get; internal set; }
		public byte ChannelId { get; internal set; }
		public byte Level { get; internal set; }
		public byte Opcode { get; internal set; }
		public ushort Task { get; internal set; }
		public ulong Keyword { get; internal set; }
		public ulong MessageId { get; internal set; }
		public string Template { get; internal set; }
	}
}
