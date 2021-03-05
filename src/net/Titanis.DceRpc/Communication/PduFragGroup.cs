using System;
using System.IO;
using System.Threading.Tasks;
using Titanis.DceRpc.WireProtocol;
using Titanis.IO;

namespace Titanis.DceRpc.Communication
{
	class PduFragGroup
	{
		internal readonly PduHeader header;
		private ByteWriter stream;

		public PduFragGroup(PduHeader header)
		{
			this.header = header;
			this.stream = new ByteWriter(0x1000);
		}

		internal int CallId => this.header.callId;

		internal void AppendChunk(ReadOnlySpan<byte> chunk)
		{
			this.stream.WriteBytes(chunk);
		}

		internal Memory<byte> Reassemble()
		{
			return this.stream.GetData();
		}
	}
}
