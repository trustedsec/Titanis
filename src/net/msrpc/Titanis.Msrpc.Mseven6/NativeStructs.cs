using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Msrpc.Mseven6
{
	[PduStruct]
	internal partial struct EVENT_DESCRIPTOR
	{
		public ushort Id;
		public byte Version;
		public byte Channel;
		public EventLevel Level;
		public byte Opcode;
		public ushort Task;

		public ulong Keyword;

		internal byte[] ToBytes()
		{
			if (BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref this, 1)).ToArray();
			}
			else
			{
				ByteWriter writer = new ByteWriter(PduStructSize);
				this.WriteTo(writer);
				return writer.GetData().ToArray();
			}
		}
	}
}
