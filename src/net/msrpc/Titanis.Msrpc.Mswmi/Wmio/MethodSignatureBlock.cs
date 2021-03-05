using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.PduStruct;

namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.70 - MethodSignatureBlock
	[PduStruct]
	internal partial struct MethodSignatureBlock
	{
		internal uint cbObj;

		[PduPosition]
		internal long position;

		private readonly bool HasObject => this.cbObj > 0 || this.obj.HasValue;

		[PduConditional(nameof(HasObject))]
		internal ObjectBlock? obj;

		partial void OnAfterReadPdu(IByteSource source)
		{
			int offObjEnd = (int)(this.position + cbObj);
			Debug.Assert(source.Position <= offObjEnd);
			source.Position = offObjEnd;
		}

		partial void OnAfterWritePdu(ByteWriter writer)
		{
			int offEnd = writer.Position;
			var cbObj = offEnd - this.position;
			writer.SetPosition((int)(this.position - 4));
			writer.WriteInt32LE((int)cbObj);
			writer.SetPosition(offEnd);
		}
	}
}
