using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.PduStruct;

namespace Titanis.Msrpc.Mswmi.Wmio
{
	[PduStruct]
	internal partial struct EncodedObject
	{
		internal int encodingLength;
		[PduPosition]
		internal long position;
		internal ObjectBlock objBlock;

		partial void OnAfterReadPdu(IByteSource source)
		{
			var offEnd = this.position + this.encodingLength;
			Debug.Assert(source.Position <= offEnd);
			source.Position = offEnd;
		}
		partial void OnAfterWritePdu(ByteWriter writer)
		{
			var offEnd = writer.Position;
			writer.SetPosition(offEnd - 4);
			writer.WriteInt32LE((int)(offEnd - this.position));
			writer.SetPosition(offEnd);
		}
	}
}
